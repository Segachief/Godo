using Godo.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Godo.Tests
{
    [TestClass]
    public class RunConfigurationTests
    {
        [TestMethod]
        public void OptionSettingsCopiesMutableSourceArrays()
        {
            bool[] options = new bool[14];
            int[] parameters = new int[8];
            options[0] = true;
            parameters[0] = 42;

            OptionSettings settings = new OptionSettings(options, parameters);
            RunConfiguration configuration =
                TestRunConfigurations.Create(weapons: settings);

            options[0] = false;
            parameters[0] = 0;

            Assert.IsTrue(configuration.Weapons.Options[0]);
            Assert.AreEqual(42, configuration.Weapons.Parameters[0]);
        }

        [TestMethod]
        public void OptionSettingsExposesReadOnlyCollections()
        {
            RunConfiguration configuration = TestRunConfigurations.Create();
            IList<bool> options =
                (IList<bool>)configuration.Weapons.Options;

            Assert.ThrowsException<NotSupportedException>(
                () => options[0] = true);
        }

        [TestMethod]
        public void RunConfigurationRejectsInvalidOptionDimensions()
        {
            OptionSettings invalidWeapons =
                new OptionSettings(new bool[13], new int[8]);

            ArgumentException exception =
                Assert.ThrowsException<ArgumentException>(
                    () => TestRunConfigurations.Create(
                        weapons: invalidWeapons));

            Assert.AreEqual("weapons", exception.ParamName);
            StringAssert.Contains(
                exception.Message,
                "Options must contain exactly 14 values");
        }
    }
}
