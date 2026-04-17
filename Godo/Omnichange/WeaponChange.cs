using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Omnichange
{
    public class WeaponChange
    {
        public static byte[] AssignWeaponBalancing(Random rnd)
        {
            int c = 0;
            int k = 0;
            byte[] weaponBalancer = new byte[8];
            while (c < 8)
            {
                if (k < 4 && weaponBalancer[c] != 1)
                {
                    weaponBalancer[c] = (byte)rnd.Next(0, 2);
                    if (weaponBalancer[c] == 1)
                    {
                        k++;
                    }
                }
                if (c == 7 && k < 4)
                {
                    c = 0;
                }
                c++;
            }
            return weaponBalancer;
        }

        public static byte AdjustBasePower(byte basePower, byte damageFormula, int powerModifier, Random rnd)
        {
            // If the weapon formula uses a %-based formula, then assigns between 2-8 (8 = 25% max)
            if (damageFormula == 0x13
                                 || damageFormula == 0x14
                                 || damageFormula == 0x23
                                 || damageFormula == 0x24
                                 || damageFormula == 0x33
                                 || damageFormula == 0x34
                                 || damageFormula == 0x44
                                 || damageFormula == 0x44
                                 || damageFormula == 0x53
                                 || damageFormula == 0x54
                                 || damageFormula == 0xB3
                                 || damageFormula == 0xB4)
            {
                basePower = (byte)rnd.Next(2, 8);
            }
            else
            {
                int powerUpper = basePower + 10;
                int powerLower = basePower - 10;
                if (powerModifier == 1)
                {
                    powerUpper += 10;
                    powerLower += 10;
                }
                basePower = (byte)rnd.Next(powerLower, powerUpper);
            }
            return basePower;
        }

        public static byte PickEquipmentStatus(Random seed)
        {
            #region List of enabled statuses
            /* Weapon Data
             * Death:       0x00 - ENABLED
             * Near-Death:  0x01 - DISABLED
             * Sleep:       0x02 - ENABLED
             * Poison:      0x03 - ENABLED
             * Sadness:     0x04 - ENABLED
             * Fury:        0x05 - ENABLED
             * Confu:       0x06 - ENABLED
             * Silence:     0x07 - ENABLED
             * Haste:       0x08 - DISABLED
             * Slow:        0x09 - ENABLED
             * Stop:        0x0A - ENABLED
             * Frog:        0x0B - ENABLED
             * Mini:        0x0C - ENABLED
             * Slow-Numb:   0x0D - ENABLED
             * Petrify:     0x0E - ENABLED
             * Regen:       0x0F - DISABLED
             * Barrier:     0x10 - DISABLED
             * MBarrier:    0x11 - DISABLED
             * Reflect:     0x12 - DISABLED
             * Dual:        0x13 - DISABLED
             * Shield:      0x14 - DISABLED
             * Doom:        0x15 - ENABLED
             * Manip:       0x16 - DISABLED
             * Berserk:     0x17 - ENABLED
             * Peerless:    0x18 - DISABLED
             * Paralysis:   0x19 - ENABLED
             * Blind:       0x1A - ENABLED
             * Dual-Drain:  0x1B - DISABLED
             * Death Force: 0x1C - DISABLED
             * Resist:      0x1D - DISABLED
             * Auto-Crits:  0x1E - DISABLED
             * Imprisoned:  0x1F - DISABLED
            */
            #endregion
            byte[] statusSet = { 0x00, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x09, 0x0A, 0x0B,
                0x0C, 0x0D, 0x0E, 0x15, 0x17, 0x19, 0x1A};

            byte pickStatus = statusSet[seed.Next(statusSet.Length)];

            return pickStatus;
        }

        public static byte AdjustAccuracy(byte baseAccuracy, int accuracyModifier, Random rnd)
        {
            if (baseAccuracy == 255)
            {
                baseAccuracy -= 100;
            }
            int accuracyUpper = baseAccuracy + 50;
            int accuracyLower = baseAccuracy - 50;
            if (accuracyModifier == 1)
            {
                accuracyUpper += 30;
                accuracyLower += 30;
            }
            baseAccuracy = (byte)rnd.Next(accuracyLower, accuracyUpper);

            return baseAccuracy;
        }

        public static byte AdjustWeaponStats(byte baseStat, int statModifier, Random rnd)
        {
            int statUpper = baseStat + 10;
            int statLower = baseStat + 5;
            if (statModifier == 1)
            {
                statUpper += 10;
                statLower += 5;
            }
            baseStat = (byte)rnd.Next(statLower, statUpper);
            return baseStat;
        }
    }
}
