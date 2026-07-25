using Godo.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Godo.Tests
{
    [TestClass]
    [DoNotParallelize]
    public class OptionStateTests
    {
        [TestMethod]
        public void MainFormInitializesEveryOptionArray()
        {
            RunInSta(() =>
            {
                string originalCurrentDirectory = Environment.CurrentDirectory;
                try
                {
                    using (MainForm mainForm = new MainForm())
                    {
                        PropertyInfo[] optionProperties = typeof(MainForm)
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .Where(property =>
                                property.PropertyType == typeof(bool[]) ||
                                property.PropertyType == typeof(int[]))
                            .ToArray();

                        Assert.IsTrue(optionProperties.Length > 0);

                        foreach (PropertyInfo optionProperty in optionProperties)
                        {
                            Assert.IsNotNull(
                                optionProperty.GetValue(mainForm),
                                optionProperty.Name + " was not initialized.");
                        }
                    }
                }
                finally
                {
                    Environment.CurrentDirectory = originalCurrentDirectory;
                }
            });
        }

        [TestMethod]
        public void LanguageSelectionRemainsMutuallyExclusive()
        {
            RunInSta(() =>
            {
                string originalCurrentDirectory = Environment.CurrentDirectory;
                try
                {
                    using (MainForm mainForm = new MainForm())
                    {
                        CheckBox english = GetPrivateCheckBox(mainForm, "chkEnglish");
                        CheckBox french = GetPrivateCheckBox(mainForm, "chkFrench");
                        CheckBox german = GetPrivateCheckBox(mainForm, "chkGerman");
                        CheckBox spanish = GetPrivateCheckBox(mainForm, "chkSpanish");
                        CheckBox japanese = GetPrivateCheckBox(mainForm, "chkJapanese");

                        french.Checked = true;

                        Assert.IsFalse(english.Checked);
                        Assert.IsTrue(french.Checked);
                        Assert.IsFalse(german.Checked);
                        Assert.IsFalse(spanish.Checked);
                        Assert.IsFalse(japanese.Checked);
                    }
                }
                finally
                {
                    Environment.CurrentDirectory = originalCurrentDirectory;
                }
            });
        }

        [TestMethod]
        public void OptionBuildersClearPreviouslySelectedValues()
        {
            RunInSta(() =>
            {
                Type[] optionFormTypes = typeof(MainForm).Assembly
                    .GetTypes()
                    .Where(type =>
                        !type.IsAbstract &&
                        typeof(Form).IsAssignableFrom(type) &&
                        type != typeof(MainForm) &&
                        type.GetMethod(
                            "OptionsArrayBuild",
                            BindingFlags.Instance | BindingFlags.NonPublic) != null)
                    .ToArray();

                Assert.AreEqual(21, optionFormTypes.Length);

                foreach (Type optionFormType in optionFormTypes)
                {
                    using (Form optionForm = (Form)Activator.CreateInstance(optionFormType))
                    {
                        SetAllCheckBoxes(optionForm, false);
                        SetAllPublicBooleanArrays(optionForm, true);

                        MethodInfo builder = optionFormType.GetMethod(
                            "OptionsArrayBuild",
                            BindingFlags.Instance | BindingFlags.NonPublic);
                        bool[] options = (bool[])builder.Invoke(optionForm, null);

                        Assert.IsTrue(
                            options.All(option => !option),
                            optionFormType.Name + " retained an unchecked option.");
                    }
                }
            });
        }

        [TestMethod]
        public void CharacterSelectionBuildersClearPreviouslySelectedValues()
        {
            RunInSta(() =>
            {
                Type[] characterSelectionFormTypes = typeof(MainForm).Assembly
                    .GetTypes()
                    .Where(type =>
                        !type.IsAbstract &&
                        typeof(Form).IsAssignableFrom(type) &&
                        type.GetMethod(
                            "CharacterSelectArrayBuild",
                            BindingFlags.Instance | BindingFlags.NonPublic) != null)
                    .ToArray();

                Assert.AreEqual(3, characterSelectionFormTypes.Length);

                foreach (Type optionFormType in characterSelectionFormTypes)
                {
                    using (Form optionForm = (Form)Activator.CreateInstance(optionFormType))
                    {
                        SetAllCheckBoxes(optionForm, false);
                        SetAllPublicBooleanArrays(optionForm, true);

                        MethodInfo builder = optionFormType.GetMethod(
                            "CharacterSelectArrayBuild",
                            BindingFlags.Instance | BindingFlags.NonPublic);
                        bool[] selections = (bool[])builder.Invoke(optionForm, null);

                        Assert.IsTrue(
                            selections.All(selection => !selection),
                            optionFormType.Name + " retained an unchecked character.");
                    }
                }
            });
        }

        [TestMethod]
        public void CapturedRunConfigurationDoesNotTrackLaterUiChanges()
        {
            RunInSta(() =>
            {
                string originalCurrentDirectory = Environment.CurrentDirectory;
                try
                {
                    using (MainForm mainForm = new MainForm())
                    {
                        mainForm.weaponOptions[0] = true;
                        GetPrivateCheckBox(
                            mainForm,
                            "chkWeaponData").Checked = true;
                        GetPrivateCheckBox(
                            mainForm,
                            "chkFrench").Checked = true;

                        MethodInfo capture = typeof(MainForm).GetMethod(
                            "CaptureRunConfiguration",
                            BindingFlags.Instance | BindingFlags.NonPublic);
                        RunConfiguration configuration =
                            (RunConfiguration)capture.Invoke(
                                mainForm,
                                new object[] { 2468 });

                        mainForm.weaponOptions[0] = false;
                        GetPrivateCheckBox(
                            mainForm,
                            "chkWeaponData").Checked = false;
                        GetPrivateCheckBox(
                            mainForm,
                            "chkEnglish").Checked = true;

                        Assert.AreEqual(2468, configuration.Seed);
                        Assert.AreEqual(
                            GameLanguage.French,
                            configuration.Language);
                        Assert.IsTrue(configuration.QuickOptions.Weapons);
                        Assert.IsTrue(configuration.Weapons.Options[0]);
                    }
                }
                finally
                {
                    Environment.CurrentDirectory = originalCurrentDirectory;
                }
            });
        }

        [TestMethod]
        public void PortableSeedOverridesCurrentUiOptions()
        {
            RunInSta(() =>
            {
                string originalCurrentDirectory = Environment.CurrentDirectory;
                try
                {
                    using (MainForm mainForm = new MainForm())
                    {
                        RunConfiguration expected =
                            TestRunConfigurations.CreatePopulated();
                        string portableSeed =
                            RunConfigurationSeedCodec.Encode(expected);

                        Array.Fill(mainForm.weaponOptions, false);
                        GetPrivateCheckBox(
                            mainForm,
                            "chkEnglish").Checked = true;
                        GetPrivateCheckBox(
                            mainForm,
                            "chkWeaponData").Checked = false;

                        MethodInfo resolve = typeof(MainForm).GetMethod(
                            "ResolveRunConfiguration",
                            BindingFlags.Instance | BindingFlags.NonPublic);
                        RunConfiguration actual =
                            (RunConfiguration)resolve.Invoke(
                                mainForm,
                                new object[] { portableSeed });

                        Assert.AreEqual(expected.Seed, actual.Seed);
                        Assert.AreEqual(expected.Language, actual.Language);
                        Assert.AreEqual(
                            expected.QuickOptions.Weapons,
                            actual.QuickOptions.Weapons);
                        CollectionAssert.AreEqual(
                            expected.Weapons.Options.ToArray(),
                            actual.Weapons.Options.ToArray());
                    }
                }
                finally
                {
                    Environment.CurrentDirectory = originalCurrentDirectory;
                }
            });
        }

        [TestMethod]
        public void PortableSeedUpdatesEveryDetailedOptionControl()
        {
            RunInSta(() =>
            {
                string originalCurrentDirectory = Environment.CurrentDirectory;
                try
                {
                    using (MainForm mainForm = new MainForm())
                    {
                        RebuildAllDetailedOptionArrays(mainForm);
                        SetPatternOnEveryPublicBooleanArray(mainForm);
                        GetPrivateCheckBox(
                            mainForm,
                            "chkWeaponData").Checked = true;
                        GetPrivateCheckBox(
                            mainForm,
                            "chkAccessoryData").Checked = true;
                        GetPrivateCheckBox(
                            mainForm,
                            "chkStartingMateria").Checked = true;
                        GetPrivateCheckBox(
                            mainForm,
                            "chkEnemyItems").Checked = true;
                        GetPrivateCheckBox(
                            mainForm,
                            "chkJapanese").Checked = true;

                        RunConfiguration expected =
                            InvokeConfigurationMethod(
                                mainForm,
                                "CaptureRunConfiguration",
                                86420);
                        string portableSeed =
                            RunConfigurationSeedCodec.Encode(expected);

                        SetAllPublicBooleanArrays(mainForm, false);
                        SetAllCheckBoxes(mainForm, false);
                        GetPrivateCheckBox(
                            mainForm,
                            "chkEnglish").Checked = true;

                        InvokeConfigurationMethod(
                            mainForm,
                            "ResolveRunConfiguration",
                            portableSeed);
                        RebuildAllDetailedOptionArrays(mainForm);
                        RunConfiguration rebuilt =
                            InvokeConfigurationMethod(
                                mainForm,
                                "CaptureRunConfiguration",
                                expected.Seed);

                        Assert.AreEqual(
                            portableSeed,
                            RunConfigurationSeedCodec.Encode(rebuilt));
                        Assert.IsTrue(
                            GetPrivateCheckBox(
                                mainForm,
                                "chkWeaponData").Checked);
                        Assert.IsTrue(
                            GetPrivateCheckBox(
                                mainForm,
                                "chkJapanese").Checked);
                    }
                }
                finally
                {
                    Environment.CurrentDirectory = originalCurrentDirectory;
                }
            });
        }

        [TestMethod]
        public void NumericSeedRetainsLegacyCurrentOptionBehaviour()
        {
            RunInSta(() =>
            {
                string originalCurrentDirectory = Environment.CurrentDirectory;
                try
                {
                    using (MainForm mainForm = new MainForm())
                    {
                        mainForm.weaponOptions[0] = true;
                        GetPrivateCheckBox(
                            mainForm,
                            "chkFrench").Checked = true;

                        MethodInfo resolve = typeof(MainForm).GetMethod(
                            "ResolveRunConfiguration",
                            BindingFlags.Instance | BindingFlags.NonPublic);
                        RunConfiguration configuration =
                            (RunConfiguration)resolve.Invoke(
                                mainForm,
                                new object[] { "13579" });

                        Assert.AreEqual(13579, configuration.Seed);
                        Assert.AreEqual(
                            GameLanguage.French,
                            configuration.Language);
                        Assert.IsTrue(configuration.Weapons.Options[0]);
                    }
                }
                finally
                {
                    Environment.CurrentDirectory = originalCurrentDirectory;
                }
            });
        }

        private static void SetAllCheckBoxes(Control parent, bool isChecked)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is CheckBox checkBox)
                {
                    checkBox.Checked = isChecked;
                }

                SetAllCheckBoxes(control, isChecked);
            }
        }

        private static CheckBox GetPrivateCheckBox(MainForm mainForm, string fieldName)
        {
            FieldInfo field = typeof(MainForm).GetField(
                fieldName,
                BindingFlags.Instance | BindingFlags.NonPublic);
            return (CheckBox)field.GetValue(mainForm);
        }

        private static void SetAllPublicBooleanArrays(object target, bool value)
        {
            FieldInfo[] booleanArrayFields = target.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public)
                .Where(field => field.FieldType == typeof(bool[]))
                .ToArray();

            foreach (FieldInfo field in booleanArrayFields)
            {
                bool[] values = (bool[])field.GetValue(target);
                Array.Fill(values, value);
            }
        }

        private static void SetPatternOnEveryPublicBooleanArray(object target)
        {
            PropertyInfo[] booleanArrayProperties = target.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(property => property.PropertyType == typeof(bool[]))
                .ToArray();

            for (int propertyIndex = 0;
                propertyIndex < booleanArrayProperties.Length;
                propertyIndex++)
            {
                bool[] values = (bool[])booleanArrayProperties[propertyIndex]
                    .GetValue(target);
                for (int valueIndex = 0;
                    valueIndex < values.Length;
                    valueIndex++)
                {
                    values[valueIndex] =
                        (propertyIndex + valueIndex) % 3 == 0;
                }
            }
        }

        private static void RebuildAllDetailedOptionArrays(MainForm mainForm)
        {
            Form[] detailedForms = typeof(MainForm)
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(field => typeof(Form).IsAssignableFrom(field.FieldType))
                .Select(field => (Form)field.GetValue(mainForm))
                .ToArray();

            foreach (Form detailedForm in detailedForms)
            {
                InvokeBuilderIfPresent(detailedForm, "OptionsArrayBuild");
                InvokeBuilderIfPresent(detailedForm, "ParametersArrayBuild");
                InvokeBuilderIfPresent(
                    detailedForm,
                    "CharacterSelectArrayBuild");
            }
        }

        private static void InvokeBuilderIfPresent(
            Form form,
            string methodName)
        {
            MethodInfo builder = form.GetType().GetMethod(
                methodName,
                BindingFlags.Instance | BindingFlags.NonPublic);
            builder?.Invoke(form, null);
        }

        private static RunConfiguration InvokeConfigurationMethod(
            MainForm mainForm,
            string methodName,
            object argument)
        {
            MethodInfo method = typeof(MainForm).GetMethod(
                methodName,
                BindingFlags.Instance | BindingFlags.NonPublic);
            return (RunConfiguration)method.Invoke(
                mainForm,
                new[] { argument });
        }

        private static void RunInSta(Action action)
        {
            Exception failure = null;
            Thread thread = new Thread(() =>
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
