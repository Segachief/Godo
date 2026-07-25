using System;
using System.Collections.Generic;
using System.IO;

namespace Godo.Infrastructure
{
    public sealed class RunWorkspace
    {
        private const int FirstTextSection = 9;
        private const int LastTextSection = 26;

        public RunWorkspace(string runtimeDirectory)
        {
            if (string.IsNullOrWhiteSpace(runtimeDirectory))
            {
                throw new ArgumentException("A runtime directory is required.", nameof(runtimeDirectory));
            }

            RuntimeDirectory = Path.GetFullPath(runtimeDirectory);
            KernelStringsDirectory = Path.Combine(RuntimeDirectory, "Kernel Strings");
            Kernel2StringsDirectory = Path.Combine(RuntimeDirectory, "Kernel2 Strings");
            OutputDirectory = Path.Combine(RuntimeDirectory, "Output Files");
        }

        public string RuntimeDirectory { get; }
        public string KernelStringsDirectory { get; }
        public string Kernel2StringsDirectory { get; }
        public string OutputDirectory { get; }

        public void Prepare()
        {
            Directory.CreateDirectory(KernelStringsDirectory);
            Directory.CreateDirectory(Kernel2StringsDirectory);
            Directory.CreateDirectory(OutputDirectory);
            CleanupScratchFiles();
        }

        public void CleanupScratchFiles()
        {
            ScratchCleanupException cleanupFailure = null;

            foreach (string scratchFile in EnumerateScratchFiles())
            {
                try
                {
                    File.Delete(scratchFile);
                }
                catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
                {
                    cleanupFailure ??= new ScratchCleanupException(scratchFile, ex);
                }
            }

            if (cleanupFailure != null)
            {
                throw cleanupFailure;
            }
        }

        private IEnumerable<string> EnumerateScratchFiles()
        {
            for (int section = FirstTextSection; section <= LastTextSection; section++)
            {
                yield return Path.Combine(KernelStringsDirectory, "kernel2.bin" + section);
                yield return Path.Combine(KernelStringsDirectory, "kernel2Modified.bin" + section);
                yield return Path.Combine(Kernel2StringsDirectory, "kernel2.bin" + section);
            }
        }
    }

    public sealed class ScratchCleanupException : IOException
    {
        public ScratchCleanupException(string filePath, Exception innerException)
            : base("Unable to remove generated scratch file '" + filePath + "'.", innerException)
        {
            FilePath = filePath;
        }

        public string FilePath { get; }
    }
}
