using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Godo.Infrastructure
{
    public enum GameLanguage
    {
        English,
        French,
        German,
        Spanish,
        Japanese
    }

    public sealed class QuickOptions
    {
        public QuickOptions(
            bool weapons,
            bool armour,
            bool accessories,
            bool characterStats,
            bool startingMateria,
            bool enemyStats,
            bool enemyItems)
        {
            Weapons = weapons;
            Armour = armour;
            Accessories = accessories;
            CharacterStats = characterStats;
            StartingMateria = startingMateria;
            EnemyStats = enemyStats;
            EnemyItems = enemyItems;
        }

        public bool Weapons { get; }
        public bool Armour { get; }
        public bool Accessories { get; }
        public bool CharacterStats { get; }
        public bool StartingMateria { get; }
        public bool EnemyStats { get; }
        public bool EnemyItems { get; }

        internal bool[] ToArray()
        {
            return new[]
            {
                Weapons,
                Armour,
                Accessories,
                CharacterStats,
                StartingMateria,
                EnemyStats,
                EnemyItems
            };
        }
    }

    public sealed class OptionSettings
    {
        private readonly bool[] _options;
        private readonly int[] _parameters;
        private readonly bool[] _selections;
        private readonly ReadOnlyCollection<bool> _readOnlyOptions;
        private readonly ReadOnlyCollection<int> _readOnlyParameters;
        private readonly ReadOnlyCollection<bool> _readOnlySelections;

        public OptionSettings(
            bool[] options,
            int[] parameters = null,
            bool[] selections = null)
        {
            ArgumentNullException.ThrowIfNull(options);

            _options = (bool[])options.Clone();
            _parameters = parameters == null
                ? Array.Empty<int>()
                : (int[])parameters.Clone();
            _selections = selections == null
                ? Array.Empty<bool>()
                : (bool[])selections.Clone();

            _readOnlyOptions = Array.AsReadOnly(_options);
            _readOnlyParameters = Array.AsReadOnly(_parameters);
            _readOnlySelections = Array.AsReadOnly(_selections);
        }

        public IReadOnlyList<bool> Options => _readOnlyOptions;
        public IReadOnlyList<int> Parameters => _readOnlyParameters;
        public IReadOnlyList<bool> Selections => _readOnlySelections;

        internal bool[] CopyOptions()
        {
            return (bool[])_options.Clone();
        }

        internal int[] CopyParameters()
        {
            return (int[])_parameters.Clone();
        }

        internal bool[] CopySelections()
        {
            return (bool[])_selections.Clone();
        }
    }

    public sealed class RunConfiguration
    {
        public RunConfiguration(
            int seed,
            GameLanguage language,
            QuickOptions quickOptions,
            OptionSettings spells,
            OptionSettings summons,
            OptionSettings enemySkills,
            OptionSettings attackItems,
            OptionSettings healItems,
            OptionSettings statusItems,
            OptionSettings weapons,
            OptionSettings armour,
            OptionSettings accessories,
            OptionSettings materia,
            OptionSettings characterStats,
            OptionSettings limitBreaks,
            OptionSettings startingEquipment,
            OptionSettings modelSwap,
            OptionSettings enemyStats,
            OptionSettings enemyAttacks,
            OptionSettings enemyItems,
            OptionSettings formations,
            OptionSettings balancing,
            OptionSettings challenges,
            OptionSettings specialHacks,
            bool rngOption = false)
        {
            if (!Enum.IsDefined(language))
            {
                throw new ArgumentOutOfRangeException(nameof(language));
            }

            Seed = seed;
            Language = language;
            QuickOptions = quickOptions ??
                throw new ArgumentNullException(nameof(quickOptions));
            Spells = Validate(spells, nameof(spells), 5, 3);
            Summons = Validate(summons, nameof(summons), 5, 3);
            EnemySkills = Validate(enemySkills, nameof(enemySkills), 5, 3);
            AttackItems = Validate(attackItems, nameof(attackItems), 3, 1);
            HealItems = Validate(healItems, nameof(healItems), 3, 1);
            StatusItems = Validate(statusItems, nameof(statusItems), 2, 0);
            Weapons = Validate(weapons, nameof(weapons), 14, 8);
            Armour = Validate(armour, nameof(armour), 13, 9);
            Accessories = Validate(accessories, nameof(accessories), 6, 2);
            Materia = Validate(materia, nameof(materia), 4, 1);
            CharacterStats = Validate(
                characterStats,
                nameof(characterStats),
                18,
                9,
                9);
            LimitBreaks = Validate(
                limitBreaks,
                nameof(limitBreaks),
                21,
                0,
                9);
            StartingEquipment = Validate(
                startingEquipment,
                nameof(startingEquipment),
                4,
                1,
                9);
            ModelSwap = Validate(modelSwap, nameof(modelSwap), 4, 0);
            EnemyStats = Validate(enemyStats, nameof(enemyStats), 16, 13);
            EnemyAttacks = Validate(enemyAttacks, nameof(enemyAttacks), 9, 3);
            EnemyItems = Validate(enemyItems, nameof(enemyItems), 6, 0);
            Formations = Validate(formations, nameof(formations), 3, 0);
            Balancing = Validate(balancing, nameof(balancing), 4, 2);
            Challenges = Validate(challenges, nameof(challenges), 10, 0);
            SpecialHacks = Validate(specialHacks, nameof(specialHacks), 5, 2);
            RngOption = rngOption;
        }

        public int Seed { get; }
        public GameLanguage Language { get; }
        public QuickOptions QuickOptions { get; }
        public OptionSettings Spells { get; }
        public OptionSettings Summons { get; }
        public OptionSettings EnemySkills { get; }
        public OptionSettings AttackItems { get; }
        public OptionSettings HealItems { get; }
        public OptionSettings StatusItems { get; }
        public OptionSettings Weapons { get; }
        public OptionSettings Armour { get; }
        public OptionSettings Accessories { get; }
        public OptionSettings Materia { get; }
        public OptionSettings CharacterStats { get; }
        public OptionSettings LimitBreaks { get; }
        public OptionSettings StartingEquipment { get; }
        public OptionSettings ModelSwap { get; }
        public OptionSettings EnemyStats { get; }
        public OptionSettings EnemyAttacks { get; }
        public OptionSettings EnemyItems { get; }
        public OptionSettings Formations { get; }
        public OptionSettings Balancing { get; }
        public OptionSettings Challenges { get; }
        public OptionSettings SpecialHacks { get; }
        public bool RngOption { get; }

        internal bool[] CreateLanguageOptions()
        {
            bool[] languageOptions = new bool[5];
            languageOptions[(int)Language] = true;
            return languageOptions;
        }

        internal bool[] CreateRngOptions()
        {
            return new[] { RngOption };
        }

        private static OptionSettings Validate(
            OptionSettings settings,
            string parameterName,
            int optionCount,
            int parameterCount,
            int selectionCount = 0)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            ValidateCount(
                settings.Options.Count,
                optionCount,
                parameterName,
                nameof(settings.Options));
            ValidateCount(
                settings.Parameters.Count,
                parameterCount,
                parameterName,
                nameof(settings.Parameters));
            ValidateCount(
                settings.Selections.Count,
                selectionCount,
                parameterName,
                nameof(settings.Selections));

            return settings;
        }

        private static void ValidateCount(
            int actual,
            int expected,
            string parameterName,
            string memberName)
        {
            if (actual != expected)
            {
                throw new ArgumentException(
                    memberName + " must contain exactly " + expected +
                    " values; received " + actual + ".",
                    parameterName);
            }
        }
    }
}
