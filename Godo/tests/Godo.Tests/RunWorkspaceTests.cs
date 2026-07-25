using Godo.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;

namespace Godo.Tests
{
    [TestClass]
    public class RunWorkspaceTests
    {
        private string _testDirectory;

        [TestInitialize]
        public void Initialize()
        {
            _testDirectory = Path.Combine(
                Path.GetTempPath(),
                "Godo.Tests",
                Guid.NewGuid().ToString("N"));
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(_testDirectory))
            {
                Directory.Delete(_testDirectory, true);
            }
        }

        [TestMethod]
        public void PrepareCreatesAUniquePrivateRunDirectory()
        {
            RunConfiguration configuration =
                TestRunConfigurations.Create();
            DateTime generatedAt =
                new DateTime(2026, 7, 25, 14, 55, 0);
            RunWorkspace first =
                new RunWorkspace(
                    _testDirectory,
                    configuration,
                    generatedAt);
            RunWorkspace second =
                new RunWorkspace(
                    _testDirectory,
                    configuration,
                    generatedAt);

            first.Prepare();
            second.Prepare();

            Assert.AreNotEqual(first.RunId, second.RunId);
            Assert.AreNotEqual(first.RunDirectory, second.RunDirectory);
            Assert.IsTrue(Directory.Exists(first.KernelStringsDirectory));
            Assert.IsTrue(Directory.Exists(first.Kernel2StringsDirectory));
            Assert.IsTrue(Directory.Exists(first.OutputDirectory));
            Assert.AreEqual(
                RunWorkspaceState.Prepared,
                first.State);
            Assert.AreEqual(
                first.PublishedOutputDirectory,
                second.PublishedOutputDirectory);
        }

        [TestMethod]
        public void CleanupScratchFilesDeletesOnlyTheOwningRun()
        {
            RunConfiguration configuration =
                TestRunConfigurations.Create();
            RunWorkspace first =
                new RunWorkspace(_testDirectory, configuration);
            RunWorkspace second =
                new RunWorkspace(_testDirectory, configuration);
            first.Prepare();
            second.Prepare();
            string firstScratch =
                Path.Combine(first.KernelStringsDirectory, "generated.bin");
            string secondScratch =
                Path.Combine(second.KernelStringsDirectory, "generated.bin");
            File.WriteAllText(firstScratch, "first");
            File.WriteAllText(secondScratch, "second");

            first.CleanupScratchFiles();

            Assert.IsFalse(Directory.Exists(first.RunDirectory));
            Assert.AreEqual(
                RunWorkspaceState.Cleaned,
                first.State);
            Assert.IsTrue(Directory.Exists(second.RunDirectory));
            Assert.IsTrue(File.Exists(secondScratch));
        }

        [TestMethod]
        public void WorkspaceCannotBePreparedForASecondRun()
        {
            RunWorkspace workspace = CreateWorkspace();
            workspace.Prepare();
            workspace.CleanupScratchFiles();

            Assert.ThrowsException<InvalidOperationException>(
                () => workspace.Prepare());
        }

        [TestMethod]
        public void CleanupScratchFilesReportsTheLockedFile()
        {
            RunWorkspace workspace = CreateWorkspace();
            workspace.Prepare();
            string lockedFile = Path.Combine(workspace.KernelStringsDirectory, "kernel2.bin9");
            File.WriteAllText(lockedFile, "generated");

            using (FileStream lockStream = new FileStream(
                lockedFile,
                FileMode.Open,
                FileAccess.Read,
                FileShare.None))
            {
                ScratchCleanupException exception =
                    Assert.ThrowsException<ScratchCleanupException>(
                        () => workspace.CleanupScratchFiles());

                Assert.AreEqual(lockedFile, exception.FilePath);
                Assert.IsInstanceOfType(exception.InnerException, typeof(IOException));
            }
        }

        [TestMethod]
        public void PublishOutputsRequiresACompleteOutputSet()
        {
            RunWorkspace workspace = CreateWorkspace();
            workspace.Prepare();
            string publishedScene = Path.Combine(
                workspace.PublishedOutputDirectory,
                "scene.bin");
            Directory.CreateDirectory(
                workspace.PublishedOutputDirectory);
            File.WriteAllText(publishedScene, "previous");
            File.WriteAllText(
                Path.Combine(workspace.OutputDirectory, "scene.bin"),
                "replacement");

            Assert.ThrowsException<InvalidOperationException>(
                () => workspace.PublishOutputs());

            Assert.AreEqual("previous", File.ReadAllText(publishedScene));
            Assert.AreEqual(
                RunWorkspaceState.Prepared,
                workspace.State);
        }

        [TestMethod]
        public void PublishOutputsMovesTheCompletedSetToSharedOutput()
        {
            RunWorkspace workspace = CreateWorkspace();
            workspace.Prepare();
            string[] outputFiles =
            {
                "scene.bin",
                "kernel.bin",
                "kernel2.bin"
            };

            foreach (string outputFile in outputFiles)
            {
                File.WriteAllText(
                    Path.Combine(workspace.OutputDirectory, outputFile),
                    outputFile);
            }

            workspace.PublishOutputs();

            Assert.AreEqual(
                RunWorkspaceState.Published,
                workspace.State);
            foreach (string outputFile in outputFiles)
            {
                Assert.AreEqual(
                    outputFile,
                    File.ReadAllText(Path.Combine(
                        workspace.PublishedOutputDirectory,
                        outputFile)));
                Assert.IsFalse(File.Exists(
                    Path.Combine(workspace.OutputDirectory, outputFile)));
            }

            Assert.AreEqual(
                workspace.PortableSeed,
                File.ReadAllText(Path.Combine(
                    workspace.PublishedOutputDirectory,
                    "seed.txt")));

            string manifestFile = Path.Combine(
                workspace.PublishedOutputDirectory,
                "output-manifest.json");
            Assert.IsTrue(File.Exists(manifestFile));
            using JsonDocument manifest =
                JsonDocument.Parse(File.ReadAllText(manifestFile));
            Assert.AreEqual(
                1,
                manifest.RootElement
                    .GetProperty("formatVersion")
                    .GetInt32());
            Assert.AreEqual(
                workspace.PortableSeed,
                manifest.RootElement
                    .GetProperty("portableSeed")
                    .GetString());
            Assert.AreEqual(
                4,
                manifest.RootElement
                    .GetProperty("files")
                    .GetArrayLength());
            JsonElement sceneManifest = manifest.RootElement
                .GetProperty("files")
                .EnumerateArray()
                .Single(file =>
                    file.GetProperty("name").GetString() ==
                    "scene.bin");
            string publishedScene = Path.Combine(
                workspace.PublishedOutputDirectory,
                "scene.bin");
            Assert.AreEqual(
                new FileInfo(publishedScene).Length,
                sceneManifest.GetProperty("length").GetInt64());
            Assert.AreEqual(
                Convert.ToHexString(SHA256.HashData(
                    File.ReadAllBytes(publishedScene))),
                sceneManifest.GetProperty("sha256").GetString());

            Assert.ThrowsException<InvalidOperationException>(
                () => workspace.PublishOutputs());
        }

        [TestMethod]
        public void PublishOutputsRejectsEmptyFilesWithoutExposure()
        {
            RunWorkspace workspace = CreateWorkspace();
            workspace.Prepare();
            File.WriteAllBytes(
                workspace.SceneOutputFile,
                Array.Empty<byte>());
            File.WriteAllText(workspace.KernelOutputFile, "kernel");
            File.WriteAllText(workspace.Kernel2OutputFile, "kernel2");

            Assert.ThrowsException<InvalidOperationException>(
                () => workspace.PublishOutputs());

            Assert.AreEqual(
                RunWorkspaceState.Prepared,
                workspace.State);
            Assert.IsFalse(
                Directory.Exists(workspace.PublishedOutputDirectory));
            Assert.IsFalse(File.Exists(Path.Combine(
                workspace.OutputDirectory,
                "output-manifest.json")));
        }

        [TestMethod]
        public void OutputFolderUsesSeedPrefixAndGenerationTimestamp()
        {
            DateTime generatedAt =
                new DateTime(2026, 7, 25, 14, 55, 0);
            RunWorkspace workspace = new RunWorkspace(
                _testDirectory,
                TestRunConfigurations.Create(),
                generatedAt);
            const string expectedLanguagePrefix = "GODO1-ENG-";
            StringAssert.StartsWith(
                workspace.PortableSeed,
                expectedLanguagePrefix);
            string expectedSeedName =
                workspace.PortableSeed.Substring(
                    0,
                    expectedLanguagePrefix.Length + 5);

            Assert.AreEqual(
                expectedSeedName + "-25-07-26-1455",
                workspace.OutputFolderName);
        }

        [TestMethod]
        public void PublishOutputsAppendsDuplicateNumber()
        {
            DateTime generatedAt =
                new DateTime(2026, 7, 25, 14, 55, 0);
            RunConfiguration configuration =
                TestRunConfigurations.Create();
            RunWorkspace first = new RunWorkspace(
                _testDirectory,
                configuration,
                generatedAt);
            RunWorkspace second = new RunWorkspace(
                _testDirectory,
                configuration,
                generatedAt);
            RunWorkspace third = new RunWorkspace(
                _testDirectory,
                configuration,
                generatedAt);

            PrepareAndStageOutputs(first);
            PrepareAndStageOutputs(second);
            PrepareAndStageOutputs(third);
            first.PublishOutputs();
            second.PublishOutputs();
            third.PublishOutputs();

            Assert.IsFalse(first.OutputFolderName.EndsWith(")"));
            Assert.AreEqual(
                first.OutputFolderName + "(1)",
                second.OutputFolderName);
            Assert.AreEqual(
                first.OutputFolderName + "(2)",
                third.OutputFolderName);
            Assert.IsTrue(
                Directory.Exists(first.PublishedOutputDirectory));
            Assert.IsTrue(
                Directory.Exists(second.PublishedOutputDirectory));
            Assert.IsTrue(
                Directory.Exists(third.PublishedOutputDirectory));
        }

        private RunWorkspace CreateWorkspace()
        {
            return new RunWorkspace(
                _testDirectory,
                TestRunConfigurations.Create());
        }

        private static void PrepareAndStageOutputs(
            RunWorkspace workspace)
        {
            workspace.Prepare();
            File.WriteAllText(workspace.SceneOutputFile, "scene");
            File.WriteAllText(workspace.KernelOutputFile, "kernel");
            File.WriteAllText(workspace.Kernel2OutputFile, "kernel2");
        }
    }
}
