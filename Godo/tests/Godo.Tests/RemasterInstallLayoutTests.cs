using Godo.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace Godo.Tests
{
    [TestClass]
    public class RemasterInstallLayoutTests
    {
        [TestMethod]
        public void DerivesEverySupportedLanguagePathFromRoot()
        {
            string root = CreateTemporaryRoot();
            try
            {
                CreateCompleteLanguage(root, "lang-en");

                Assert.IsTrue(RemasterInstallLayout.TryCreate(
                    root,
                    out RemasterInstallLayout layout,
                    out string errorMessage),
                    errorMessage);

                var expectedDirectories =
                    new Dictionary<GameLanguage, string>
                    {
                        [GameLanguage.German] = "lang-de",
                        [GameLanguage.English] = "lang-en",
                        [GameLanguage.Spanish] = "lang-es",
                        [GameLanguage.French] = "lang-fr",
                        [GameLanguage.Japanese] = "lang-ja"
                    };

                Assert.AreEqual(5, layout.LanguagePaths.Count);
                foreach (var expected in expectedDirectories)
                {
                    RemasterLanguagePaths paths =
                        layout.GetPaths(expected.Key);
                    string languageRoot = Path.Combine(
                        Path.GetFullPath(root),
                        "workingdir",
                        "data",
                        expected.Value);

                    Assert.AreEqual(
                        Path.Combine(languageRoot, "battle"),
                        paths.BattleDirectory);
                    Assert.AreEqual(
                        Path.Combine(languageRoot, "kernel"),
                        paths.KernelDirectory);
                    Assert.AreEqual(
                        Path.Combine(languageRoot, "battle", "scene.bin"),
                        paths.SceneFile);
                    Assert.AreEqual(
                        Path.Combine(languageRoot, "kernel", "kernel.bin"),
                        paths.KernelFile);
                    Assert.AreEqual(
                        Path.Combine(languageRoot, "kernel", "kernel2.bin"),
                        paths.Kernel2File);
                }
            }
            finally
            {
                Directory.Delete(root, true);
            }
        }

        [TestMethod]
        public void AcceptsRootWhenOneLanguageHasCompleteFileSet()
        {
            string root = CreateTemporaryRoot();
            try
            {
                CreateCompleteLanguage(root, "lang-ja");

                Assert.IsTrue(RemasterInstallLayout.TryCreate(
                    root,
                    out RemasterInstallLayout layout,
                    out string errorMessage),
                    errorMessage);
                Assert.IsTrue(
                    layout.GetPaths(GameLanguage.Japanese)
                        .HasCompleteFileSet);
                Assert.IsFalse(
                    layout.GetPaths(GameLanguage.English)
                        .HasCompleteFileSet);
            }
            finally
            {
                Directory.Delete(root, true);
            }
        }

        [TestMethod]
        public void AcceptsSteamEditionParentAndDerivesPathsFromNestedFf7Folder()
        {
            string temporaryRoot = CreateTemporaryRoot();
            string steamEditionRoot = Path.Combine(
                temporaryRoot,
                "FINAL FANTASY VII Steam Edition");
            string gameRoot = Path.Combine(steamEditionRoot, "ff7");
            try
            {
                Directory.CreateDirectory(steamEditionRoot);
                CreateCompleteLanguage(gameRoot, "lang-en");

                Assert.IsTrue(RemasterInstallLayout.TryCreate(
                    steamEditionRoot,
                    out RemasterInstallLayout layout,
                    out string errorMessage),
                    errorMessage);

                Assert.AreEqual(
                    Path.GetFullPath(steamEditionRoot),
                    layout.RootDirectory);
                Assert.AreEqual(
                    Path.GetFullPath(gameRoot),
                    layout.GameDirectory);
                Assert.AreEqual(
                    Path.Combine(
                        gameRoot,
                        "workingdir",
                        "data",
                        "lang-en",
                        "battle",
                        "scene.bin"),
                    layout.GetPaths(GameLanguage.English).SceneFile);
            }
            finally
            {
                Directory.Delete(temporaryRoot, true);
            }
        }

        [TestMethod]
        public void RejectsRootWithOnlyAnIncompleteLanguageFileSet()
        {
            string root = CreateTemporaryRoot();
            try
            {
                string battleDirectory = Path.Combine(
                    root,
                    "workingdir",
                    "data",
                    "lang-fr",
                    "battle");
                string kernelDirectory = Path.Combine(
                    root,
                    "workingdir",
                    "data",
                    "lang-fr",
                    "kernel");
                Directory.CreateDirectory(battleDirectory);
                Directory.CreateDirectory(kernelDirectory);
                File.WriteAllBytes(
                    Path.Combine(battleDirectory, "scene.bin"),
                    new byte[] { 1 });
                File.WriteAllBytes(
                    Path.Combine(kernelDirectory, "kernel.bin"),
                    new byte[] { 1 });

                Assert.IsFalse(RemasterInstallLayout.TryCreate(
                    root,
                    out RemasterInstallLayout layout,
                    out string errorMessage));
                Assert.IsNull(layout);
                StringAssert.Contains(errorMessage, "kernel2.bin");
            }
            finally
            {
                Directory.Delete(root, true);
            }
        }

        [TestMethod]
        public void RejectsMissingRoot()
        {
            string missingRoot = Path.Combine(
                Path.GetTempPath(),
                "Godo-Remaster-Missing-" + Guid.NewGuid().ToString("N"));

            Assert.IsFalse(RemasterInstallLayout.TryCreate(
                missingRoot,
                out RemasterInstallLayout layout,
                out string errorMessage));
            Assert.IsNull(layout);
            StringAssert.Contains(errorMessage, "does not exist");
        }

        internal static string CreateTemporaryRoot()
        {
            string root = Path.Combine(
                Path.GetTempPath(),
                "Godo-Remaster-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(root);
            return root;
        }

        internal static void CreateCompleteLanguage(
            string root,
            string languageDirectoryName)
        {
            string battleDirectory = Path.Combine(
                root,
                "workingdir",
                "data",
                languageDirectoryName,
                "battle");
            string kernelDirectory = Path.Combine(
                root,
                "workingdir",
                "data",
                languageDirectoryName,
                "kernel");
            Directory.CreateDirectory(battleDirectory);
            Directory.CreateDirectory(kernelDirectory);
            File.WriteAllBytes(
                Path.Combine(battleDirectory, "scene.bin"),
                new byte[] { 1 });
            File.WriteAllBytes(
                Path.Combine(kernelDirectory, "kernel.bin"),
                new byte[] { 1 });
            File.WriteAllBytes(
                Path.Combine(kernelDirectory, "kernel2.bin"),
                new byte[] { 1 });
        }
    }
}
