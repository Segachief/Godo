using Godo.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Godo.Tests
{
    [TestClass]
    [DoNotParallelize]
    public class RemasterInstallUiTests
    {
        [TestMethod]
        public void MainFormProvidesRemasterDirectoryButtonAndStatus()
        {
            RunWithSavedRoot(string.Empty, () =>
            {
                using var mainForm = new MainForm();
                Button button = GetPrivateField<Button>(
                    mainForm,
                    "btnInstallDirectoryRemaster");
                Label status = GetPrivateField<Label>(
                    mainForm,
                    "lblRemasterInstallDirectory");

                Assert.AreSame(mainForm, button.Parent);
                Assert.IsTrue(button.Enabled);
                Assert.AreEqual(
                    "Install Directory - Remaster",
                    button.Text);
                Assert.AreEqual(
                    "Remaster install: Not configured",
                    status.Text);
            });
        }

        [TestMethod]
        public void MainFormRestoresAndRevalidatesSavedRoot()
        {
            string root = RemasterInstallLayoutTests.CreateTemporaryRoot();
            try
            {
                RemasterInstallLayoutTests.CreateCompleteLanguage(
                    root,
                    "lang-de");
                RunWithSavedRoot(root, () =>
                {
                    using var mainForm = new MainForm();
                    RemasterInstallLayout layout =
                        GetPrivateField<RemasterInstallLayout>(
                            mainForm,
                            "_remasterInstallLayout");
                    Label status = GetPrivateField<Label>(
                        mainForm,
                        "lblRemasterInstallDirectory");

                    Assert.AreEqual(Path.GetFullPath(root), layout.RootDirectory);
                    Assert.AreEqual(
                        "Remaster install: " + Path.GetFullPath(root),
                        status.Text);
                });
            }
            finally
            {
                Directory.Delete(root, true);
            }
        }

        [TestMethod]
        public void MissingSavedRootShowsUnavailableStatusWithoutThrowing()
        {
            string missingRoot = Path.Combine(
                Path.GetTempPath(),
                "Godo-Remaster-Missing-" + Guid.NewGuid().ToString("N"));

            RunWithSavedRoot(missingRoot, () =>
            {
                using var mainForm = new MainForm();
                Assert.IsNull(GetPrivateField<RemasterInstallLayout>(
                    mainForm,
                    "_remasterInstallLayout"));
                Assert.AreEqual(
                    "Remaster install: Saved directory unavailable",
                    GetPrivateField<Label>(
                        mainForm,
                        "lblRemasterInstallDirectory").Text);
            });
        }

        [TestMethod]
        public void InvalidSelectionDoesNotReplaceValidLayout()
        {
            string validRoot = RemasterInstallLayoutTests.CreateTemporaryRoot();
            string invalidRoot = RemasterInstallLayoutTests.CreateTemporaryRoot();
            try
            {
                RemasterInstallLayoutTests.CreateCompleteLanguage(
                    validRoot,
                    "lang-es");
                RunWithSavedRoot(string.Empty, () =>
                {
                    using var mainForm = new MainForm();
                    Assert.IsTrue(InvokeTrySet(
                        mainForm,
                        validRoot,
                        out string validError),
                        validError);
                    Assert.IsFalse(InvokeTrySet(
                        mainForm,
                        invalidRoot,
                        out _));

                    RemasterInstallLayout layout =
                        GetPrivateField<RemasterInstallLayout>(
                            mainForm,
                            "_remasterInstallLayout");
                    Assert.AreEqual(
                        Path.GetFullPath(validRoot),
                        layout.RootDirectory);
                });
            }
            finally
            {
                Directory.Delete(validRoot, true);
                Directory.Delete(invalidRoot, true);
            }
        }

        [TestMethod]
        public void RemasterRootSettingIsUserScoped()
        {
            PropertyInfo setting = typeof(Properties.Settings).GetProperty(
                "RemasterInstallRoot",
                BindingFlags.Instance | BindingFlags.Public);

            Assert.IsNotNull(setting);
            Assert.IsNotNull(setting.GetCustomAttribute<UserScopedSettingAttribute>());
            Assert.IsTrue(setting.CanRead);
            Assert.IsTrue(setting.CanWrite);
        }

        private static bool InvokeTrySet(
            MainForm mainForm,
            string root,
            out string errorMessage)
        {
            MethodInfo method = typeof(MainForm).GetMethod(
                "TrySetRemasterInstallDirectory",
                BindingFlags.Instance | BindingFlags.NonPublic);
            object[] arguments = { root, false, null };
            bool result = (bool)method.Invoke(mainForm, arguments);
            errorMessage = (string)arguments[2];
            return result;
        }

        private static T GetPrivateField<T>(object target, string fieldName)
        {
            FieldInfo field = target.GetType().GetField(
                fieldName,
                BindingFlags.Instance | BindingFlags.NonPublic);
            return (T)field.GetValue(target);
        }

        private static void RunWithSavedRoot(
            string savedRoot,
            Action action)
        {
            RunInSta(() =>
            {
                string originalRoot =
                    Properties.Settings.Default.RemasterInstallRoot;
                try
                {
                    Properties.Settings.Default.RemasterInstallRoot = savedRoot;
                    action();
                }
                finally
                {
                    Properties.Settings.Default.RemasterInstallRoot =
                        originalRoot;
                }
            });
        }

        private static void RunInSta(Action action)
        {
            Exception failure = null;
            var thread = new Thread(() =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    failure = ex;
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            if (failure != null)
            {
                throw new AssertFailedException(
                    "STA test execution failed: " + failure,
                    failure);
            }
        }
    }
}
