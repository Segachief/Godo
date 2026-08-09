using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Godo.Infrastructure
{
    public sealed class RemasterLanguagePaths
    {
        internal RemasterLanguagePaths(
            GameLanguage language,
            string battleDirectory,
            string kernelDirectory)
        {
            Language = language;
            BattleDirectory = battleDirectory;
            KernelDirectory = kernelDirectory;
            SceneFile = Path.Combine(BattleDirectory, "scene.bin");
            KernelFile = Path.Combine(KernelDirectory, "kernel.bin");
            Kernel2File = Path.Combine(KernelDirectory, "kernel2.bin");
        }

        public GameLanguage Language { get; }
        public string BattleDirectory { get; }
        public string KernelDirectory { get; }
        public string SceneFile { get; }
        public string KernelFile { get; }
        public string Kernel2File { get; }

        public bool HasCompleteFileSet =>
            File.Exists(SceneFile) &&
            File.Exists(KernelFile) &&
            File.Exists(Kernel2File);
    }

    public sealed class RemasterInstallLayout
    {
        private static readonly IReadOnlyDictionary<GameLanguage, string>
            LanguageDirectoryNames =
                new ReadOnlyDictionary<GameLanguage, string>(
                    new Dictionary<GameLanguage, string>
                    {
                        [GameLanguage.German] = "lang-de",
                        [GameLanguage.English] = "lang-en",
                        [GameLanguage.Spanish] = "lang-es",
                        [GameLanguage.French] = "lang-fr",
                        [GameLanguage.Japanese] = "lang-ja"
                    });

        private readonly IReadOnlyDictionary<
            GameLanguage,
            RemasterLanguagePaths> _languagePaths;

        private RemasterInstallLayout(
            string rootDirectory,
            string gameDirectory)
        {
            RootDirectory = rootDirectory;
            GameDirectory = gameDirectory;
            string languageRoot = Path.Combine(
                GameDirectory,
                "workingdir",
                "data");
            _languagePaths =
                new ReadOnlyDictionary<GameLanguage, RemasterLanguagePaths>(
                    LanguageDirectoryNames.ToDictionary(
                        pair => pair.Key,
                        pair =>
                        {
                            string directory = Path.Combine(
                                languageRoot,
                                pair.Value);
                            return new RemasterLanguagePaths(
                                pair.Key,
                                Path.Combine(directory, "battle"),
                                Path.Combine(directory, "kernel"));
                        }));
        }

        public string RootDirectory { get; }
        public string GameDirectory { get; }

        public IReadOnlyDictionary<GameLanguage, RemasterLanguagePaths>
            LanguagePaths => _languagePaths;

        public RemasterLanguagePaths GetPaths(GameLanguage language)
        {
            if (!_languagePaths.TryGetValue(
                language,
                out RemasterLanguagePaths paths))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(language),
                    language,
                    "The language is not supported by the FF7 Remaster layout.");
            }

            return paths;
        }

        public static bool TryCreate(
            string rootDirectory,
            out RemasterInstallLayout layout,
            out string errorMessage)
        {
            layout = null;

            if (string.IsNullOrWhiteSpace(rootDirectory))
            {
                errorMessage = "Select the Final Fantasy VII installation folder.";
                return false;
            }

            string fullRoot;
            try
            {
                fullRoot = Path.GetFullPath(rootDirectory);
            }
            catch (Exception ex) when (
                ex is ArgumentException ||
                ex is NotSupportedException ||
                ex is PathTooLongException)
            {
                errorMessage = "The selected folder path is not valid.";
                return false;
            }

            if (!Directory.Exists(fullRoot))
            {
                errorMessage = "The selected folder does not exist.";
                return false;
            }

            var candidate = new RemasterInstallLayout(
                fullRoot,
                fullRoot);
            if (!HasCompleteLanguage(candidate))
            {
                string nestedGameDirectory = Path.Combine(fullRoot, "ff7");
                candidate = new RemasterInstallLayout(
                    fullRoot,
                    nestedGameDirectory);
                if (!HasCompleteLanguage(candidate))
                {
                    errorMessage =
                        "The selected folder does not contain a complete FF7 " +
                        "Remaster language installation. Select either the " +
                        "FINAL FANTASY VII Steam Edition folder or its ff7 " +
                        "folder. Expected scene.bin in " +
                        @"[ff7\]workingdir\data\lang-??\battle and " +
                        @"kernel.bin plus kernel2.bin in " +
                        @"[ff7\]workingdir\data\lang-??\kernel.";
                    return false;
                }
            }

            layout = candidate;
            errorMessage = null;
            return true;
        }

        private static bool HasCompleteLanguage(
            RemasterInstallLayout candidate)
        {
            return candidate.LanguagePaths.Values.Any(
                paths => paths.HasCompleteFileSet);
        }
    }
}
