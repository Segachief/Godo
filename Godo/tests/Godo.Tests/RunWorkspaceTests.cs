using Godo.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

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
        public void PrepareCreatesDirectoriesAndDeletesOnlyGeneratedScratchFiles()
        {
            RunWorkspace workspace = new RunWorkspace(_testDirectory);
            Directory.CreateDirectory(workspace.KernelStringsDirectory);
            Directory.CreateDirectory(workspace.Kernel2StringsDirectory);

            string kernelScratch = Path.Combine(workspace.KernelStringsDirectory, "kernel2.bin9");
            string modifiedScratch = Path.Combine(workspace.KernelStringsDirectory, "kernel2Modified.bin26");
            string kernel2Scratch = Path.Combine(workspace.Kernel2StringsDirectory, "kernel2.bin12");
            string unrelatedFile = Path.Combine(workspace.KernelStringsDirectory, "keep-me.txt");

            File.WriteAllText(kernelScratch, "generated");
            File.WriteAllText(modifiedScratch, "generated");
            File.WriteAllText(kernel2Scratch, "generated");
            File.WriteAllText(unrelatedFile, "user data");

            workspace.Prepare();

            Assert.IsTrue(Directory.Exists(workspace.OutputDirectory));
            Assert.IsFalse(File.Exists(kernelScratch));
            Assert.IsFalse(File.Exists(modifiedScratch));
            Assert.IsFalse(File.Exists(kernel2Scratch));
            Assert.IsTrue(File.Exists(unrelatedFile));
        }

        [TestMethod]
        public void CleanupScratchFilesSupportsConsecutiveRuns()
        {
            RunWorkspace workspace = new RunWorkspace(_testDirectory);
            workspace.Prepare();

            for (int run = 0; run < 2; run++)
            {
                string kernelScratch = Path.Combine(workspace.KernelStringsDirectory, "kernel2.bin9");
                string modifiedScratch = Path.Combine(workspace.KernelStringsDirectory, "kernel2Modified.bin12");
                string kernel2Scratch = Path.Combine(workspace.Kernel2StringsDirectory, "kernel2.bin26");

                File.WriteAllText(kernelScratch, "generated");
                File.WriteAllText(modifiedScratch, "generated");
                File.WriteAllText(kernel2Scratch, "generated");

                workspace.CleanupScratchFiles();

                Assert.IsFalse(File.Exists(kernelScratch));
                Assert.IsFalse(File.Exists(modifiedScratch));
                Assert.IsFalse(File.Exists(kernel2Scratch));
            }
        }

        [TestMethod]
        public void CleanupScratchFilesReportsTheLockedFile()
        {
            RunWorkspace workspace = new RunWorkspace(_testDirectory);
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
    }
}
