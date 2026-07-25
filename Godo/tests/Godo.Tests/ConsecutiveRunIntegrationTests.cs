using Godo.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Godo.Tests
{
    [TestClass]
    [DoNotParallelize]
    public class ConsecutiveRunIntegrationTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void TwoConsecutiveRunsFromRestoredSeedProduceIdenticalOutputHashes()
        {
            string sourceRuntime = Environment.GetEnvironmentVariable("GODO_TEST_RUNTIME");
            if (string.IsNullOrWhiteSpace(sourceRuntime))
            {
                Assert.Inconclusive(
                    "Set GODO_TEST_RUNTIME to a runtime directory containing Default Files.");
            }

            string sourceDefaultFiles = Path.Combine(sourceRuntime, "Default Files");
            if (!Directory.Exists(sourceDefaultFiles))
            {
                Assert.Inconclusive(
                    "GODO_TEST_RUNTIME does not contain a Default Files directory.");
            }

            string testRuntime = Path.Combine(
                Path.GetTempPath(),
                "Godo.Tests",
                Guid.NewGuid().ToString("N"));
            string originalCurrentDirectory = Directory.GetCurrentDirectory();

            try
            {
                CopyDirectory(
                    sourceDefaultFiles,
                    Path.Combine(testRuntime, "Default Files"));

                string unrelatedCurrentDirectory =
                    Path.Combine(testRuntime, "Unrelated Current Directory");
                Directory.CreateDirectory(unrelatedCurrentDirectory);
                Directory.SetCurrentDirectory(unrelatedCurrentDirectory);
                RunConfiguration firstConfiguration =
                    TestRunConfigurations.Create(123456789);

                RunWorkspace firstWorkspace =
                    new RunWorkspace(
                        testRuntime,
                        firstConfiguration);
                RunPipeline(firstWorkspace, firstConfiguration);
                string[] firstHashes = GetOutputHashes(
                    firstWorkspace.PublishedOutputDirectory);

                string portableSeed =
                    RunConfigurationSeedCodec.Encode(firstConfiguration);
                RunConfiguration reproducedConfiguration =
                    RunConfigurationSeedCodec.Decode(portableSeed);
                RunWorkspace secondWorkspace =
                    new RunWorkspace(
                        testRuntime,
                        reproducedConfiguration);
                RunPipeline(secondWorkspace, reproducedConfiguration);
                string[] secondHashes = GetOutputHashes(
                    secondWorkspace.PublishedOutputDirectory);

                CollectionAssert.AreEqual(firstHashes, secondHashes);
                Assert.AreNotEqual(
                    firstWorkspace.RunDirectory,
                    secondWorkspace.RunDirectory);
                Assert.IsFalse(
                    Directory.Exists(firstWorkspace.RunDirectory));
                Assert.IsFalse(
                    Directory.Exists(secondWorkspace.RunDirectory));
                Assert.AreEqual(
                    RunWorkspaceState.Cleaned,
                    firstWorkspace.State);
                Assert.AreEqual(
                    RunWorkspaceState.Cleaned,
                    secondWorkspace.State);
            }
            finally
            {
                Directory.SetCurrentDirectory(originalCurrentDirectory);
                if (Directory.Exists(testRuntime))
                {
                    Directory.Delete(testRuntime, true);
                }
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void FailedRunLeavesPreviouslyPublishedOutputUnchanged()
        {
            string sourceRuntime =
                Environment.GetEnvironmentVariable("GODO_TEST_RUNTIME");
            if (string.IsNullOrWhiteSpace(sourceRuntime))
            {
                Assert.Inconclusive(
                    "Set GODO_TEST_RUNTIME to a runtime directory containing Default Files.");
            }

            string sourceDefaultFiles =
                Path.Combine(sourceRuntime, "Default Files");
            if (!Directory.Exists(sourceDefaultFiles))
            {
                Assert.Inconclusive(
                    "GODO_TEST_RUNTIME does not contain a Default Files directory.");
            }

            string testRuntime = Path.Combine(
                Path.GetTempPath(),
                "Godo.Tests",
                Guid.NewGuid().ToString("N"));

            try
            {
                string testDefaultFiles =
                    Path.Combine(testRuntime, "Default Files");
                CopyDirectory(sourceDefaultFiles, testDefaultFiles);

                RunConfiguration configuration =
                    TestRunConfigurations.Create(987654321);
                DateTimeOffset generatedAt =
                    new DateTimeOffset(
                        2026,
                        7,
                        25,
                        14,
                        55,
                        0,
                        TimeSpan.Zero);
                RunWorkspace successfulWorkspace =
                    new RunWorkspace(
                        testRuntime,
                        configuration,
                        generatedAt);
                RunPipeline(successfulWorkspace, configuration);
                string[] originalPublishedHashes =
                    GetPublishedTreeHashes(
                        successfulWorkspace.PublishedOutputDirectory);

                File.WriteAllBytes(
                    Path.Combine(testDefaultFiles, "scene.bin"),
                    new byte[63]);
                RunWorkspace failedWorkspace =
                    new RunWorkspace(
                        testRuntime,
                        configuration,
                        generatedAt);

                Assert.ThrowsException<EndOfStreamException>(
                    () => RunPipeline(failedWorkspace, configuration));

                CollectionAssert.AreEqual(
                    originalPublishedHashes,
                    GetPublishedTreeHashes(
                        successfulWorkspace.PublishedOutputDirectory));
                Assert.AreEqual(
                    RunWorkspaceState.Cleaned,
                    failedWorkspace.State);
                Assert.IsFalse(
                    Directory.Exists(failedWorkspace.RunDirectory));
                Assert.AreEqual(
                    1,
                    Directory.GetDirectories(
                        successfulWorkspace.OutputRootDirectory).Length);
            }
            finally
            {
                if (Directory.Exists(testRuntime))
                {
                    Directory.Delete(testRuntime, true);
                }
            }
        }

        private static void RunPipeline(
            RunWorkspace workspace,
            RunConfiguration configuration)
        {
            workspace.Prepare();

            try
            {
                Random random = new Random(configuration.Seed);

                byte[] kernelLookup = GZipper.PrepareScene(
                    Path.Combine(workspace.RuntimeDirectory, "Default Files", "scene.bin"),
                    workspace,
                    configuration,
                    random);

                GZipper.PrepareKernel(
                    Path.Combine(workspace.RuntimeDirectory, "Default Files", "kernel.bin"),
                    Path.Combine(workspace.RuntimeDirectory, "Default Files", "kernel2.bin"),
                    kernelLookup,
                    workspace,
                    configuration,
                    random);
                workspace.PublishOutputs();
            }
            finally
            {
                workspace.CleanupScratchFiles();
            }
        }

        private static string[] GetOutputHashes(string outputDirectory)
        {
            string[] outputFiles = { "scene.bin", "kernel.bin", "kernel2.bin" };
            return outputFiles
                .Select(file => Convert.ToHexString(
                    SHA256.HashData(File.ReadAllBytes(Path.Combine(outputDirectory, file)))))
                .ToArray();
        }

        private static string[] GetPublishedTreeHashes(
            string outputDirectory)
        {
            return Directory
                .GetFiles(outputDirectory)
                .OrderBy(
                    file => Path.GetFileName(file),
                    StringComparer.Ordinal)
                .Select(file =>
                    Path.GetFileName(file) + "=" +
                    Convert.ToHexString(
                        SHA256.HashData(File.ReadAllBytes(file))))
                .ToArray();
        }

        private static void CopyDirectory(string sourceDirectory, string destinationDirectory)
        {
            Directory.CreateDirectory(destinationDirectory);

            foreach (string sourceFile in Directory.GetFiles(sourceDirectory))
            {
                File.Copy(
                    sourceFile,
                    Path.Combine(destinationDirectory, Path.GetFileName(sourceFile)));
            }
        }
    }
}
