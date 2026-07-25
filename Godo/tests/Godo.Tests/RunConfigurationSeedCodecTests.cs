using Godo.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Godo.Tests
{
    [TestClass]
    public class RunConfigurationSeedCodecTests
    {
        [TestMethod]
        public void PortableSeedRoundTripPreservesEntireConfiguration()
        {
            RunConfiguration expected =
                TestRunConfigurations.CreatePopulated();

            string seed = RunConfigurationSeedCodec.Encode(expected);
            RunConfiguration actual =
                RunConfigurationSeedCodec.Decode(seed);

            StringAssert.StartsWith(
                seed,
                RunConfigurationSeedCodec.Prefix);
            Assert.AreEqual(expected.Seed, actual.Seed);
            Assert.AreEqual(expected.Language, actual.Language);
            Assert.AreEqual(expected.RngOption, actual.RngOption);
            AssertQuickOptionsEqual(
                expected.QuickOptions,
                actual.QuickOptions);

            OptionSettings[] expectedSettings = GetSettings(expected);
            OptionSettings[] actualSettings = GetSettings(actual);
            Assert.AreEqual(expectedSettings.Length, actualSettings.Length);

            for (int index = 0; index < expectedSettings.Length; index++)
            {
                CollectionAssert.AreEqual(
                    expectedSettings[index].Options.ToArray(),
                    actualSettings[index].Options.ToArray());
                CollectionAssert.AreEqual(
                    expectedSettings[index].Parameters.ToArray(),
                    actualSettings[index].Parameters.ToArray());
                CollectionAssert.AreEqual(
                    expectedSettings[index].Selections.ToArray(),
                    actualSettings[index].Selections.ToArray());
            }
        }

        [TestMethod]
        public void PortableSeedEncodingIsDeterministic()
        {
            RunConfiguration configuration =
                TestRunConfigurations.CreatePopulated();

            Assert.AreEqual(
                RunConfigurationSeedCodec.Encode(configuration),
                RunConfigurationSeedCodec.Encode(configuration));
        }

        [TestMethod]
        public void PortableSeedRejectsCorruptedData()
        {
            string seed = RunConfigurationSeedCodec.Encode(
                TestRunConfigurations.CreatePopulated());
            int changedIndex =
                RunConfigurationSeedCodec.Prefix.Length + 5;
            char replacement = seed[changedIndex] == 'A' ? 'B' : 'A';
            string corrupted = seed.Substring(0, changedIndex) +
                replacement +
                seed.Substring(changedIndex + 1);

            FormatException exception =
                Assert.ThrowsException<FormatException>(
                    () => RunConfigurationSeedCodec.Decode(corrupted));
            StringAssert.StartsWith(
                exception.Message,
                "The portable seed is invalid");
        }

        [TestMethod]
        public void PortableSeedRejectsUnsupportedVersion()
        {
            string seed = RunConfigurationSeedCodec.Encode(
                TestRunConfigurations.Create());
            string unsupported = "GODO2-" +
                seed.Substring(RunConfigurationSeedCodec.Prefix.Length);

            Assert.ThrowsException<FormatException>(
                () => RunConfigurationSeedCodec.Decode(unsupported));
        }

        private static OptionSettings[] GetSettings(
            RunConfiguration configuration)
        {
            return new[]
            {
                configuration.Spells,
                configuration.Summons,
                configuration.EnemySkills,
                configuration.AttackItems,
                configuration.HealItems,
                configuration.StatusItems,
                configuration.Weapons,
                configuration.Armour,
                configuration.Accessories,
                configuration.Materia,
                configuration.CharacterStats,
                configuration.LimitBreaks,
                configuration.StartingEquipment,
                configuration.ModelSwap,
                configuration.EnemyStats,
                configuration.EnemyAttacks,
                configuration.EnemyItems,
                configuration.Formations,
                configuration.Balancing,
                configuration.Challenges,
                configuration.SpecialHacks
            };
        }

        private static void AssertQuickOptionsEqual(
            QuickOptions expected,
            QuickOptions actual)
        {
            Assert.AreEqual(expected.Weapons, actual.Weapons);
            Assert.AreEqual(expected.Armour, actual.Armour);
            Assert.AreEqual(expected.Accessories, actual.Accessories);
            Assert.AreEqual(
                expected.CharacterStats,
                actual.CharacterStats);
            Assert.AreEqual(
                expected.StartingMateria,
                actual.StartingMateria);
            Assert.AreEqual(expected.EnemyStats, actual.EnemyStats);
            Assert.AreEqual(expected.EnemyItems, actual.EnemyItems);
        }
    }
}
