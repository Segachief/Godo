using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Indexing
{
    public class StatusAilmentIndex
    {
        public static byte AssignStatusAilment(int picker, Random rnd)
        {
            #region Statuses
            /* Statuses (by flag)
                * 1 = Death
                * 2 = Near-Death
                * 4 = Sleep
                * 8 = Poison
                * 16 = Sadness
                * 32 = Fury
                * 64 = Confusion
                * 128 = Silence

                * 1 = Haste
                * 2 = Slow
                * 4 = Stop
                * 8 = Frog
                * 16 = Mini
                * 32 = Slow-Numb
                * 64 = Petrify
                * 128 = Regen

                * 1 = Barrier
                * 2 = MBarrier
                * 4 = Reflect
                * 8 = Dual
                * 16 = Shield
                * 32 = D. Sentence
                * 64 = Manip
                * 128 = Berserk

                * 1 = Peerless
                * 2 = Paralysis
                * 4 = Darkness
                * 8 = Dual-Drain
                * 16 = Death Force
                * 32 = Resist
                * 64 = Lucky Girl
                * 128 = Imprisoned
                */
            #endregion

            int[] status = new int[] { 1, 2, 4, 8, 16, 32, 64, 128 };

            if (picker == 0)
            {
                picker = rnd.Next(2, 8); // Prevents Death and Near-Death being set
                return (byte)status[picker];

            }
            else if (picker == 1)
            {
                picker = rnd.Next(2, 7); // Prevents Petrify, and Regen being set
                return (byte)status[picker];
            }
            else if (picker == 2)
            {
                return 255;
            }
            else
            {
                picker = 4; // Only Darkness set
                return (byte)status[picker];
            }
        }
    }
}
