using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Godo.Infrastructure
{
    public enum RunWorkspaceState
    {
        Created,
        Prepared,
        Published,
        Cleaned
    }

    public sealed class RunWorkspace
    {
        public RunWorkspace(
            string runtimeDirectory,
            RunConfiguration configuration,
            DateTimeOffset? generatedAt = null)
        {
            if (string.IsNullOrWhiteSpace(runtimeDirectory))
            {
                throw new ArgumentException("A runtime directory is required.", nameof(runtimeDirectory));
            }
            ArgumentNullException.ThrowIfNull(configuration);

            RuntimeDirectory = Path.GetFullPath(runtimeDirectory);
            PortableSeed =
                RunConfigurationSeedCodec.Encode(configuration);
            GeneratedAt = generatedAt ?? DateTimeOffset.Now;
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
        public DateTimeOffset GeneratedAt { get; }
        public string OutputFolderName { get; private set; }
        public RunWorkspaceState State { get; private set; }
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
            if (State != RunWorkspaceState.Created)
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
            State = RunWorkspaceState.Prepared;
        }

        public void PublishOutputs()
        {
            EnsurePrepared();

            string[] requiredFiles =
            {
                "scene.bin",
                "kernel.bin",
                "kernel2.bin",
                "seed.txt"
            };

            foreach (string outputFile in requiredFiles)
            {
                string stagedFile =
                    Path.Combine(OutputDirectory, outputFile);
                if (!File.Exists(stagedFile))
                {
                    throw new InvalidOperationException(
                        "The run did not produce the expected output file '" +
                        outputFile + "'.");
                }

                if (new FileInfo(stagedFile).Length == 0)
                {
                    throw new InvalidOperationException(
                        "The run produced an empty output file '" +
                        outputFile + "'.");
                }
            }

            WriteOutputManifest(requiredFiles);
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
                    State = RunWorkspaceState.Published;
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
                if (State != RunWorkspaceState.Created)
                {
                    State = RunWorkspaceState.Cleaned;
                }
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

            State = RunWorkspaceState.Cleaned;

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
            if (State != RunWorkspaceState.Prepared ||
                !Directory.Exists(RunDirectory))
            {
                throw new InvalidOperationException(
                    "The run workspace has not been prepared.");
            }
        }

        private static string CreateOutputFolderName(
            string portableSeed,
            DateTimeOffset generatedAt)
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

        private void WriteOutputManifest(string[] requiredFiles)
        {
            var files = requiredFiles.Select(outputFile =>
            {
                string filePath =
                    Path.Combine(OutputDirectory, outputFile);
                FileInfo fileInfo = new FileInfo(filePath);
                return new
                {
                    name = outputFile,
                    length = fileInfo.Length,
                    sha256 = Convert.ToHexString(
                        SHA256.HashData(File.ReadAllBytes(filePath)))
                };
            }).ToArray();
            var manifest = new
            {
                formatVersion = 1,
                portableSeed = PortableSeed,
                generatedAt = GeneratedAt.ToString(
                    "O",
                    CultureInfo.InvariantCulture),
                files
            };
            string manifestJson = JsonSerializer.Serialize(
                manifest,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(
                Path.Combine(
                    OutputDirectory,
                    "output-manifest.json"),
                manifestJson,
                new UTF8Encoding(false));
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
