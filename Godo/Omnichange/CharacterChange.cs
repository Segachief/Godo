using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godo.Helper;

namespace Godo.Omnichange
{
    public class CharacterChange
    {
        public static byte[] AssignCharacterBalancing(Random rnd)
        {
            int c = 0;
            int k = 0;
            byte[] characterBalancer = new byte[9];
            while (c < 9)
            {
                if (k < 4 && characterBalancer[c] != 1)
                {
                    characterBalancer[c] = (byte)rnd.Next(0, 2);
                    if (characterBalancer[c] == 1)
                    {
                        k++;
                    }
                }

                // Loops again if 4 points weren't assigned
                if (c == 8 && k < 4)
                {
                    c = 0;
                }

                c++;
            }

            return characterBalancer;
        }

        public static byte AdjustCharacterStats(string stat, int statModifier, Random rnd)
        {
            byte baseStat;
            if (stat == "Strength" || stat == "Magic")
            {
                int statUpper = 25;
                int statLower = 5;
                if (statModifier == 1)
                {
                    statUpper += 25;
                    statLower += 25;
                }

                baseStat = (byte)rnd.Next(statLower, statUpper);
            }
            else if (stat == "Vitality" || stat == "Spirit")
            {
                int statUpper = 25;
                int statLower = 5;
                if (statModifier == 1)
                {
                    statUpper += 50;
                    statLower += 45;
                }

                baseStat = (byte)rnd.Next(statLower, statUpper);
            }
            else // Dexterity & Luck
            {
                int statUpper = 25;
                int statLower = 5;
                if (statModifier == 1)
                {
                    statUpper += 75;
                    statLower += 70;
                }

                baseStat = (byte)rnd.Next(statLower, statUpper);
            }

            return baseStat;
        }

        public static ulong AdjustCharacterHPMP(string stat, int statModifier, Random rnd)
        {
            ulong parameter;
            if (stat == "HP")
            {
                int statUpper = 500;
                int statLower = 100;
                if (statModifier == 1)
                {
                    statUpper += 500;
                    statLower += 500;
                }
                parameter = (ulong)rnd.Next(statLower, statUpper);
            }
            else // MP
            {
                int statUpper = 50;
                int statLower = 10;
                if (statModifier == 1)
                {
                    statUpper += 50;
                    statLower += 50;
                }
                parameter = (ulong)rnd.Next(statLower, statUpper);
            }
            return parameter;
        }
    }
}
