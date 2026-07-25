using Godo.Infrastructure;

namespace Godo.Tests
{
    internal static class TestRunConfigurations
    {
        internal static RunConfiguration Create(
            int seed = 123456789,
            OptionSettings weapons = null,
            GameLanguage language = GameLanguage.English)
        {
            return new RunConfiguration(
                seed: seed,
                language: language,
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

        internal static RunConfiguration CreatePopulated()
        {
            return new RunConfiguration(
                seed: -987654321,
                language: GameLanguage.Japanese,
                quickOptions: new QuickOptions(
                    true,
                    false,
                    true,
                    false,
                    true,
                    false,
                    true),
                spells: CreateSettings(5, 3, 0, 1),
                summons: CreateSettings(5, 3, 0, 2),
                enemySkills: CreateSettings(5, 3, 0, 3),
                attackItems: CreateSettings(3, 1, 0, 4),
                healItems: CreateSettings(3, 1, 0, 5),
                statusItems: CreateSettings(2, 0, 0, 6),
                weapons: CreateSettings(14, 8, 0, 7),
                armour: CreateSettings(13, 9, 0, 8),
                accessories: CreateSettings(6, 2, 0, 9),
                materia: CreateSettings(4, 1, 0, 10),
                characterStats: CreateSettings(18, 9, 9, 11),
                limitBreaks: CreateSettings(21, 0, 9, 12),
                startingEquipment: CreateSettings(4, 1, 9, 13),
                modelSwap: CreateSettings(4, 0, 0, 14),
                enemyStats: CreateSettings(16, 13, 0, 15),
                enemyAttacks: CreateSettings(9, 3, 0, 16),
                enemyItems: CreateSettings(6, 0, 0, 17),
                formations: CreateSettings(3, 0, 0, 18),
                balancing: CreateSettings(4, 2, 0, 19),
                challenges: CreateSettings(10, 0, 0, 20),
                specialHacks: CreateSettings(5, 2, 0, 21),
                rngOption: true);
        }

        private static OptionSettings CreateSettings(
            int optionCount,
            int parameterCount,
            int selectionCount,
            int offset)
        {
            bool[] options = new bool[optionCount];
            int[] parameters = new int[parameterCount];
            bool[] selections = new bool[selectionCount];

            for (int index = 0; index < options.Length; index++)
            {
                options[index] = (index + offset) % 3 == 0;
            }

            for (int index = 0; index < parameters.Length; index++)
            {
                parameters[index] = (index + 1) * offset *
                    (index % 2 == 0 ? 1 : -1);
            }

            for (int index = 0; index < selections.Length; index++)
            {
                selections[index] = (index + offset) % 2 == 0;
            }

            return new OptionSettings(options, parameters, selections);
        }
    }
}
