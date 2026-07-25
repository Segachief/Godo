using Godo.Infrastructure;

namespace Godo.Tests
{
    internal static class TestRunConfigurations
    {
        internal static RunConfiguration Create(
            int seed = 123456789,
            OptionSettings weapons = null)
        {
            return new RunConfiguration(
                seed: seed,
                language: GameLanguage.English,
                quickOptions: new QuickOptions(
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true),
                spells: new OptionSettings(new bool[5], new int[3]),
                summons: new OptionSettings(new bool[5], new int[3]),
                enemySkills: new OptionSettings(new bool[5], new int[3]),
                attackItems: new OptionSettings(new bool[3], new int[1]),
                healItems: new OptionSettings(new bool[3], new int[1]),
                statusItems: new OptionSettings(new bool[2]),
                weapons: weapons ??
                    new OptionSettings(new bool[14], new int[8]),
                armour: new OptionSettings(new bool[13], new int[9]),
                accessories: new OptionSettings(new bool[6], new int[2]),
                materia: new OptionSettings(new bool[4], new int[1]),
                characterStats: new OptionSettings(
                    new bool[18],
                    new int[9],
                    new bool[9]),
                limitBreaks: new OptionSettings(
                    new bool[21],
                    selections: new bool[9]),
                startingEquipment: new OptionSettings(
                    new bool[4],
                    new int[1],
                    new bool[9]),
                modelSwap: new OptionSettings(new bool[4]),
                enemyStats: new OptionSettings(new bool[16], new int[13]),
                enemyAttacks: new OptionSettings(new bool[9], new int[3]),
                enemyItems: new OptionSettings(new bool[6]),
                formations: new OptionSettings(new bool[3]),
                balancing: new OptionSettings(new bool[4], new int[2]),
                challenges: new OptionSettings(new bool[10]),
                specialHacks: new OptionSettings(new bool[5], new int[2]));
        }
    }
}
