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

                RunPipeline(workspace, 123456789);
                string[] firstHashes = GetOutputHashes(workspace.OutputDirectory);

                RunPipeline(workspace, 123456789);
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

        private static void RunPipeline(RunWorkspace workspace, int seed)
        {
            workspace.Prepare();

            try
            {
                bool[] interimOptions = Enumerable.Repeat(true, 7).ToArray();
                bool[] languageOptions = { true, false, false, false, false };
                Random random = new Random(seed);

                byte[] kernelLookup = GZipper.PrepareScene(
                    Path.Combine(workspace.RuntimeDirectory, "Default Files", "scene.bin"),
                    Path.Combine(workspace.OutputDirectory, "scene.bin"),
                    new bool[4],
                    new bool[16], new int[13],
                    new bool[9], new int[3],
                    new bool[6],
                    new bool[3],
                    new bool[4], new int[2],
                    new bool[10],
                    new bool[5], new int[2],
                    interimOptions,
                    random);

                GZipper.PrepareKernel(
                    Path.Combine(workspace.RuntimeDirectory, "Default Files", "kernel.bin"),
                    Path.Combine(workspace.RuntimeDirectory, "Default Files", "kernel2.bin"),
                    Path.Combine(workspace.OutputDirectory, "kernel.bin"),
                    Path.Combine(workspace.OutputDirectory, "kernel2.bin"),
                    kernelLookup,
                    new bool[5], new int[3],
                    new bool[5], new int[3],
                    new bool[5], new int[3],
                    new bool[3], new int[1],
                    new bool[3], new int[1],
                    new bool[2], new int[0],
                    new bool[14], new int[8],
                    new bool[13], new int[9],
                    new bool[6], new int[2],
                    new bool[4], new int[1],
                    new bool[18], new int[9], new bool[9],
                    new bool[21], new bool[9],
                    new bool[4], new int[1], new bool[9],
                    new bool[10],
                    new bool[5], new int[2],
                    interimOptions,
                    languageOptions,
                    new bool[1],
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
