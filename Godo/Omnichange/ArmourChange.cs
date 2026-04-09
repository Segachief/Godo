using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Omnichange
{
    public class ArmourChange
    {
        public static byte AdjustDefence(byte baseValue, int modifier, Random rnd)
        {
            int upper = baseValue + 30;
            int lower = baseValue;
            if (modifier == 1)
            {
                upper += 20;
                lower += 10;
            }
            baseValue = (byte)rnd.Next(lower, upper);

            return baseValue;
        }

        public static byte AdjustMagicDefence(byte baseValue, int modifier, Random rnd)
        {
            int upper = baseValue + 30;
            int lower = baseValue;
            if (modifier == 1)
            {
                upper += 20;
                lower += 10;
            }
            baseValue = (byte)rnd.Next(lower, upper);

            return baseValue;
        }

        public static byte AdjustEvasion(byte baseValue, int modifier, Random rnd)
        {
            int upper = baseValue + 15;
            int lower = baseValue;
            if (modifier == 1)
            {
                upper += 10;
                lower += 5;
            }
            baseValue = (byte)rnd.Next(lower, upper);

            return baseValue;
        }

        public static byte AdjustMagicEvasion(byte baseValue, int modifier, Random rnd)
        {
            int upper = baseValue + 15;
            int lower = baseValue;
            if (modifier == 1)
            {
                upper += 15;
                lower += 5;
            }
            baseValue = (byte)rnd.Next(lower, upper);

            return baseValue;
        }

        public static byte[] AssignArmourBalancing(Random rnd)
        {
            int c = 0;
            int k = 0;
            byte[] armourBalancer = new byte[9];
            while (c < 9)
            {
                if (k < 4 && armourBalancer[c] != 1)
                {
                    armourBalancer[c] = (byte)rnd.Next(0, 2);
                    if (armourBalancer[c] == 1)
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
            return armourBalancer;
        }

        public static byte PickArmourElement(Random seed, int set)
        {
            #region List of enabled elements
            /* Armour Element Data
             * Byte #1
             * Fire:        0x01 - ENABLED
             * Ice:         0x02 - ENABLED
             * Lightning:   0x04 - ENABLED
             * Earth:       0x08 - ENABLED
             * Poison:      0x10 - ENABLED
             * Gravity:     0x20 - DISABLED
             * Water:       0x40 - ENABLED
             * Wind:        0x80 - ENABLED
             *
             * Byte #2
             * Holy:        0x01 - ENABLED
             * Restorative: 0x02 - DISABLED
             * Cut:         0x04 - DISABLED
             * Hit:         0x08 - DISABLED
             * Punch:       0x10 - DISABLED
             * Shoot:       0x20 - DISABLED
             * Shout:       0x40 - DISABLED
             * Hidden:      0x80 - ENABLED
            */
            #endregion
            byte[] elementSetA = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x40, 0x80};
            byte[] elementSetB = { 0x01, 0x80};
            byte elementPick;

            if (set == 0)
            {
                elementPick = elementSetA[seed.Next(elementSetA.Length)];
            }
            else
            {
                elementPick = elementSetB[seed.Next(elementSetB.Length)];
            }
            return elementPick;
        }

        public static byte AdjustArmourStats(byte baseStat, int statModifier, Random rnd)
        {
            int statUpper = baseStat + 10;
            int statLower = baseStat + 5;
            if (statModifier == 1)
            {
                statUpper += 15;
                statLower += 10;
            }
            baseStat = (byte)rnd.Next(statLower, statUpper);
            return baseStat;
        }
    }
}
