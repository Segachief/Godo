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
        public void PipelineSupportsTwoConsecutiveRunsInTheSameProcess()
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

                Directory.SetCurrentDirectory(testRuntime);
                RunWorkspace workspace = new RunWorkspace(testRuntime);
                RunConfiguration firstConfiguration =
                    TestRunConfigurations.Create(123456789);

                RunPipeline(workspace, firstConfiguration);
                string[] firstHashes = GetOutputHashes(workspace.OutputDirectory);

                string portableSeed =
                    RunConfigurationSeedCodec.Encode(firstConfiguration);
                RunConfiguration reproducedConfiguration =
                    RunConfigurationSeedCodec.Decode(portableSeed);
                RunPipeline(workspace, reproducedConfiguration);
                string[] secondHashes = GetOutputHashes(workspace.OutputDirectory);

                CollectionAssert.AreEqual(firstHashes, secondHashes);
                Assert.AreEqual(0, Directory.GetFiles(workspace.KernelStringsDirectory).Length);
                Assert.AreEqual(0, Directory.GetFiles(workspace.Kernel2StringsDirectory).Length);
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
                    Path.Combine(workspace.OutputDirectory, "scene.bin"),
                    configuration,
                    random);

                GZipper.PrepareKernel(
                    Path.Combine(workspace.RuntimeDirectory, "Default Files", "kernel.bin"),
                    Path.Combine(workspace.RuntimeDirectory, "Default Files", "kernel2.bin"),
                    Path.Combine(workspace.OutputDirectory, "kernel.bin"),
                    Path.Combine(workspace.OutputDirectory, "kernel2.bin"),
                    kernelLookup,
                    configuration,
                    random);
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
