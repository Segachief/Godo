using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Godo.Infrastructure
{
    public sealed class RunWorkspace
    {
        private bool _hasBeenPrepared;

        public RunWorkspace(
            string runtimeDirectory,
            RunConfiguration configuration,
            DateTime? generatedAt = null)
        {
            if (string.IsNullOrWhiteSpace(runtimeDirectory))
            {
                throw new ArgumentException("A runtime directory is required.", nameof(runtimeDirectory));
            }
            ArgumentNullException.ThrowIfNull(configuration);

            RuntimeDirectory = Path.GetFullPath(runtimeDirectory);
            PortableSeed =
                RunConfigurationSeedCodec.Encode(configuration);
            GeneratedAt = generatedAt ?? DateTime.Now;
            OutputFolderName = CreateOutputFolderName(
                PortableSeed,
                GeneratedAt);
            RunId = Guid.NewGuid().ToString("N");
            ScratchRootDirectory =
                Path.Combine(RuntimeDirectory, ".godo-runs");
            RunDirectory = Path.Combine(ScratchRootDirectory, RunId);
            KernelStringsDirectory =
                Path.Combine(RunDirectory, "Kernel Strings");
            Kernel2StringsDirectory =
                Path.Combine(RunDirectory, "Kernel2 Strings");
            OutputDirectory = Path.Combine(RunDirectory, "Output Files");
            OutputRootDirectory =
                Path.Combine(RuntimeDirectory, "Output Files");
            PublishedOutputDirectory =
                Path.Combine(OutputRootDirectory, OutputFolderName);
        }

        public string RuntimeDirectory { get; }
        public string PortableSeed { get; }
        public DateTime GeneratedAt { get; }
        public string OutputFolderName { get; private set; }
        public string RunId { get; }
        public string ScratchRootDirectory { get; }
        public string RunDirectory { get; }
        public string KernelStringsDirectory { get; }
        public string Kernel2StringsDirectory { get; }
        public string OutputDirectory { get; }
        public string OutputRootDirectory { get; }
        public string PublishedOutputDirectory { get; private set; }
        public string SceneOutputFile =>
            Path.Combine(OutputDirectory, "scene.bin");
        public string KernelOutputFile =>
            Path.Combine(OutputDirectory, "kernel.bin");
        public string Kernel2OutputFile =>
            Path.Combine(OutputDirectory, "kernel2.bin");

        public void Prepare()
        {
            if (_hasBeenPrepared)
            {
                throw new InvalidOperationException(
                    "A run workspace can only be prepared once.");
            }

            Directory.CreateDirectory(KernelStringsDirectory);
            Directory.CreateDirectory(Kernel2StringsDirectory);
            Directory.CreateDirectory(OutputDirectory);
            File.WriteAllText(
                Path.Combine(OutputDirectory, "seed.txt"),
                PortableSeed);
            _hasBeenPrepared = true;
        }

        public void PublishOutputs()
        {
            EnsurePrepared();

            string[] outputFiles =
            {
                "scene.bin",
                "kernel.bin",
                "kernel2.bin",
                "seed.txt"
            };

            foreach (string outputFile in outputFiles)
            {
                string stagedFile =
                    Path.Combine(OutputDirectory, outputFile);
                if (!File.Exists(stagedFile))
                {
                    throw new InvalidOperationException(
                        "The run did not produce the expected output file '" +
                        outputFile + "'.");
                }
            }

            Directory.CreateDirectory(OutputRootDirectory);

            for (int duplicate = 0; ; duplicate++)
            {
                string candidateName = duplicate == 0
                    ? OutputFolderName
                    : OutputFolderName + "(" +
                        duplicate.ToString(
                            CultureInfo.InvariantCulture) +
                        ")";
                string candidateDirectory =
                    Path.Combine(OutputRootDirectory, candidateName);

                try
                {
                    // Moving the complete staged directory reserves the name
                    // and publishes the output set as one filesystem action.
                    Directory.Move(
                        OutputDirectory,
                        candidateDirectory);
                    OutputFolderName = candidateName;
                    PublishedOutputDirectory = candidateDirectory;
                    return;
                }
                catch (IOException) when (
                    Directory.Exists(candidateDirectory) ||
                    File.Exists(candidateDirectory))
                {
                    // The name was already used by another completed run.
                }
            }
        }

        public void CleanupScratchFiles()
        {
            if (!Directory.Exists(RunDirectory))
            {
                return;
            }

            ScratchCleanupException cleanupFailure = null;

            foreach (string scratchFile in Directory
                .EnumerateFiles(
                    RunDirectory,
                    "*",
                    SearchOption.AllDirectories)
                .OrderBy(file => file, StringComparer.Ordinal))
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

            if (cleanupFailure == null)
            {
                try
                {
                    Directory.Delete(RunDirectory, true);
                }
                catch (Exception ex) when (
                    ex is IOException ||
                    ex is UnauthorizedAccessException)
                {
                    cleanupFailure =
                        new ScratchCleanupException(RunDirectory, ex);
                }
            }

            if (cleanupFailure != null)
            {
                throw cleanupFailure;
            }

            if (Directory.Exists(ScratchRootDirectory) &&
                !Directory.EnumerateFileSystemEntries(
                    ScratchRootDirectory).Any())
            {
                try
                {
                    Directory.Delete(ScratchRootDirectory);
                }
                catch (IOException)
                {
                    // Another run may have created its workspace after the
                    // emptiness check.
                }
                catch (UnauthorizedAccessException)
                {
                    // Another run may have created its workspace after the
                    // emptiness check. That workspace remains independently
                    // owned and must not make this run's cleanup fail.
                }
            }
        }

        internal void EnsurePrepared()
        {
            if (!_hasBeenPrepared ||
                !Directory.Exists(RunDirectory))
            {
                throw new InvalidOperationException(
                    "The run workspace has not been prepared.");
            }
        }

        private static string CreateOutputFolderName(
            string portableSeed,
            DateTime generatedAt)
        {
            int seedNameLength = Math.Min(
                portableSeed.Length,
                RunConfigurationSeedCodec.Prefix.Length + 5);
            string seedName =
                portableSeed.Substring(0, seedNameLength);
            return seedName + "-" + generatedAt.ToString(
                "dd-MM-yy-HHmm",
                CultureInfo.InvariantCulture);
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
