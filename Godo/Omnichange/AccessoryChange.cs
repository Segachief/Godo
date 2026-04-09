using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Omnichange
{
    public class AccessoryChange
    {
        public static byte[] AssignAccessoryBalancing(Random rnd)
        {
            int c = 0;
            int k = 0;
            byte[] accessoryBalancer = new byte[5];
            while (c < 5)
            {
                if (k < 2 && accessoryBalancer[c] != 1)
                {
                    accessoryBalancer[c] = (byte)rnd.Next(0, 2);
                    if (accessoryBalancer[c] == 1)
                    {
                        k++;
                    }
                }
                // Loops again if all points weren't assigned
                if (c == 4 && k < 2)
                {
                    c = 0;
                }
                c++;
            }
            return accessoryBalancer;
        }

        public static byte AdjustAccessoryStats(byte baseStat, int statModifier, Random rnd)
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

        public static byte PickAccessoryElement(Random seed, int set)
        {
            #region List of enabled elements
            /* Element Data
             * Byte #1
             * Fire:        0x01 - ENABLED
             * Ice:         0x02 - ENABLED
             * Lightning:   0x04 - ENABLED
             * Earth:       0x08 - ENABLED
             * Poison:      0x10 - ENABLED
             * Gravity:     0x20 - ENABLED
             * Water:       0x40 - ENABLED
             * Wind:        0x80 - ENABLED
             *
             * Byte #2
             * Holy:        0x01 - ENABLED
             * Restorative: 0x02 - DISABLED
             * Cut:         0x04 - ENABLED
             * Hit:         0x08 - ENABLED
             * Punch:       0x10 - ENABLED
             * Shoot:       0x20 - ENABLED
             * Shout:       0x40 - ENABLED
             * Hidden:      0x80 - ENABLED
            */
            #endregion
            byte[] elementSetA = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x40, 0x80 };
            byte[] elementSetB = { 0x01, 0x80 };
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

        public static byte PickEquipmentStatus(Random seed, int set)
        {
            #region List of enabled statuses (4 bytes)
            /* Weapon Data
             * Death:       0x01 - ENABLED
             * Near-Death:  0x02 - DISABLED
             * Sleep:       0x04 - ENABLED
             * Poison:      0x08 - ENABLED
             * Sadness:     0x10 - ENABLED
             * Fury:        0x20 - ENABLED
             * Confu:       0x40 - ENABLED
             * Silence:     0x80 - ENABLED
             *
             * Haste:       0x01 - DISABLED
             * Slow:        0x02 - ENABLED
             * Stop:        0x04 - ENABLED
             * Frog:        0x08 - ENABLED
             * Mini:        0x10 - ENABLED
             * Slow-Numb:   0x20 - DISABLED
             * Petrify:     0x40 - ENABLED
             * Regen:       0x80 - DISABLED
             *
             * Barrier:     0x01 - DISABLED
             * MBarrier:    0x02 - DISABLED
             * Reflect:     0x04 - DISABLED
             * Dual:        0x08 - DISABLED
             * Shield:      0x10 - DISABLED
             * Doom:        0x20 - DISABLED
             * Manip:       0x40 - DISABLED
             * Berserk:     0x80 - ENABLED
             *
             * Peerless:    0x01 - DISABLED
             * Paralysis:   0x02 - ENABLED
             * Blind:       0x04 - ENABLED
             * Dual-Drain:  0x08 - DISABLED
             * Death Force: 0x10 - DISABLED
             * Resist:      0x20 - DISABLED
             * Auto-Crits:  0x40 - DISABLED
             * Imprisoned:  0x80 - DISABLED
            */
            #endregion
            byte[] statusSetA = { 0x01, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };
            byte[] statusSetB = { 0x02, 0x04, 0x08, 0x10, 0x40 };
            byte[] statusSetC = { 0x80 };
            byte[] statusSetD = { 0x02, 0x04 };
            byte statusPick;

            if (set == 0)
            {
                statusPick = statusSetA[seed.Next(statusSetA.Length)];
            }
            else if (set == 1)
            {
                statusPick = statusSetB[seed.Next(statusSetB.Length)];
            }
            else if (set == 2)
            {
                //statusPick = statusSetC[seed.Next(statusSetC.Length)];
                statusPick = 0x80;
            }
            else
            {
                statusPick = statusSetD[seed.Next(statusSetD.Length)];
            }
            return statusPick;
        }
    }
}
