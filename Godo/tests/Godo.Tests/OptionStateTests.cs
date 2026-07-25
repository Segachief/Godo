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
