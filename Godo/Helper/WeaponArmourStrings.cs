using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Helper
{
    public class WeaponArmourStrings
    {
        public static byte[] EnglishWeaponArmourStatusString(byte[][] attributes, int r, int att)
        {
            byte[] statusString;
            switch (attributes[r][att])
            {
                // KO
                case 0:
                    statusString = new byte[] { 0x24, 0x45, 0x41, 0x54, 0x48 };
                    break;

                // Near-KO
                case 1:
                    statusString = new byte[] { 0x21 };
                    break;

                // Sleep
                case 2:
                    statusString = new byte[] { 0x33, 0x4C, 0x45, 0x45, 0x50 };
                    break;

                // Poison
                case 3:
                    statusString = new byte[] { 0x30, 0x4F, 0x49, 0x53, 0x4F, 0x4E };
                    break;

                // Sadness
                case 4:
                    statusString = new byte[] { 0x33, 0x41, 0x44 };
                    break;

                // Fury
                case 5:
                    statusString = new byte[] { 0x26, 0x55, 0x52, 0x59 };
                    break;

                // Confu
                case 6:
                    statusString = new byte[] { 0x23, 0x4F, 0x4E, 0x46, 0x55 };
                    break;

                // Silence
                case 7:
                    statusString = new byte[] { 0x2D, 0x55, 0x54, 0x45 };
                    break;

                // Haste
                case 8:
                    statusString = new byte[] { 0x21 };
                    break;

                // Slow
                case 9:
                    statusString = new byte[] { 0x33, 0x4C, 0x4F, 0x57 };
                    break;

                // Stop
                case 10:
                    statusString = new byte[] { 0x33, 0x54, 0x4F, 0x50 };
                    break;

                // Frog
                case 11:
                    statusString = new byte[] { 0x26, 0x52, 0x4F, 0x47 };
                    break;

                // Mini
                case 12:
                    statusString = new byte[] { 0x2D, 0x49, 0x4E, 0x49 };
                    break;

                // Slow-Numb
                case 13:
                    statusString = new byte[] { 0x27, 0x30, 0x45, 0x54, 0x52, 0x49, 0x46, 0x59 };
                    break;

                // Petrify
                case 14:
                    statusString = new byte[] { 0x30, 0x45, 0x54, 0x52, 0x49, 0x46, 0x59 };
                    break;

                // Regen
                case 15:
                    statusString = new byte[] { 0x21 };
                    break;

                // Barrier
                case 16:
                    statusString = new byte[] { 0x21 };
                    break;

                // MBarrier
                case 17:
                    statusString = new byte[] { 0x21 };
                    break;

                // Reflect
                case 18:
                    statusString = new byte[] { 0x21 };
                    break;

                // Dual
                case 19:
                    statusString = new byte[] { 0x21 };
                    break;

                // Shield
                case 20:
                    statusString = new byte[] { 0x21 };
                    break;

                // Death Sentence
                case 21:
                    statusString = new byte[] { 0x24, 0x4F, 0x4F, 0x4D };
                    break;

                // Manip
                case 22:
                    statusString = new byte[] { 0x21 };
                    break;

                // Berserk
                case 23:
                    statusString = new byte[] { 0x22, 0x45, 0x52, 0x53, 0x45, 0x52, 0x4B };
                    break;

                // Peerless
                case 24:
                    statusString = new byte[] { 0x21 };
                    break;

                // Paralysis
                case 25:
                    statusString = new byte[] { 0x30, 0x41, 0x52, 0x41, 0x4C, 0x59, 0x53, 0x49, 0x53 };
                    break;

                // Blind
                case 26:
                    statusString = new byte[] { 0x22, 0x4C, 0x49, 0x4E, 0x44 };
                    break;

                // Dual-Drain
                case 27:
                    statusString = new byte[] { 0x21 };
                    break;

                // Death Force
                case 28:
                    statusString = new byte[] { 0x21 };
                    break;

                // Resist
                case 29:
                    statusString = new byte[] { 0x21 };
                    break;

                // Auto-Crits
                case 30:
                    statusString = new byte[] { 0x21 };
                    break;

                // Imprisoned
                case 31:
                    statusString = new byte[] { 0x21 };
                    break;

                /// ???, if this is printed in-game then something's wrong
                default:
                    statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }

            return statusString;
        }

        public static byte[] FrenchWeaponArmourStatusString(byte[][] attributes, int r, int att)
        {
            byte[] statusString;
            switch (attributes[r][att])
            {
                // KO
                case 0:
                    statusString = new byte[] { 0x24, 0x45, 0x41, 0x54, 0x48 };
                    break;

                // Near-KO
                case 1:
                    statusString = new byte[] { 0x21 };
                    break;

                // Sleep
                case 2:
                    statusString = new byte[] { 0x33, 0x4C, 0x45, 0x45, 0x50 };
                    break;

                // Poison
                case 3:
                    statusString = new byte[] { 0x30, 0x4F, 0x49, 0x53, 0x4F, 0x4E };
                    break;

                // Sadness
                case 4:
                    statusString = new byte[] { 0x33, 0x41, 0x44 };
                    break;

                // Fury
                case 5:
                    statusString = new byte[] { 0x26, 0x55, 0x52, 0x59 };
                    break;

                // Confu
                case 6:
                    statusString = new byte[] { 0x23, 0x4F, 0x4E, 0x46, 0x55 };
                    break;

                // Silence
                case 7:
                    statusString = new byte[] { 0x2D, 0x55, 0x54, 0x45 };
                    break;

                // Haste
                case 8:
                    statusString = new byte[] { 0x21 };
                    break;

                // Slow
                case 9:
                    statusString = new byte[] { 0x33, 0x4C, 0x4F, 0x57 };
                    break;

                // Stop
                case 10:
                    statusString = new byte[] { 0x33, 0x54, 0x4F, 0x50 };
                    break;

                // Frog
                case 11:
                    statusString = new byte[] { 0x26, 0x52, 0x4F, 0x47 };
                    break;

                // Mini
                case 12:
                    statusString = new byte[] { 0x2D, 0x49, 0x4E, 0x49 };
                    break;

                // Slow-Numb
                case 13:
                    statusString = new byte[] { 0x27, 0x30, 0x45, 0x54, 0x52, 0x49, 0x46, 0x59 };
                    break;

                // Petrify
                case 14:
                    statusString = new byte[] { 0x30, 0x45, 0x54, 0x52, 0x49, 0x46, 0x59 };
                    break;

                // Regen
                case 15:
                    statusString = new byte[] { 0x21 };
                    break;

                // Barrier
                case 16:
                    statusString = new byte[] { 0x21 };
                    break;

                // MBarrier
                case 17:
                    statusString = new byte[] { 0x21 };
                    break;

                // Reflect
                case 18:
                    statusString = new byte[] { 0x21 };
                    break;

                // Dual
                case 19:
                    statusString = new byte[] { 0x21 };
                    break;

                // Shield
                case 20:
                    statusString = new byte[] { 0x21 };
                    break;

                // Death Sentence
                case 21:
                    statusString = new byte[] { 0x24, 0x4F, 0x4F, 0x4D };
                    break;

                // Manip
                case 22:
                    statusString = new byte[] { 0x21 };
                    break;

                // Berserk
                case 23:
                    statusString = new byte[] { 0x22, 0x45, 0x52, 0x53, 0x45, 0x52, 0x4B };
                    break;

                // Peerless
                case 24:
                    statusString = new byte[] { 0x21 };
                    break;

                // Paralysis
                case 25:
                    statusString = new byte[] { 0x30, 0x41, 0x52, 0x41, 0x4C, 0x59, 0x53, 0x49, 0x53 };
                    break;

                // Blind
                case 26:
                    statusString = new byte[] { 0x22, 0x4C, 0x49, 0x4E, 0x44 };
                    break;

                // Dual-Drain
                case 27:
                    statusString = new byte[] { 0x21 };
                    break;

                // Death Force
                case 28:
                    statusString = new byte[] { 0x21 };
                    break;

                // Resist
                case 29:
                    statusString = new byte[] { 0x21 };
                    break;

                // Auto-Crits
                case 30:
                    statusString = new byte[] { 0x21 };
                    break;

                // Imprisoned
                case 31:
                    statusString = new byte[] { 0x21 };
                    break;

                /// ???, if this is printed in-game then something's wrong
                default:
                    statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }

            return statusString;
        }

        public static byte[] GermanWeaponArmourStatusString(byte[][] attributes, int r, int att)
        {
            byte[] statusString;
            switch (attributes[r][att])
            {
                // KO
                case 0:
                    statusString = new byte[] { 0x24, 0x45, 0x41, 0x54, 0x48 };
                    break;

                // Near-KO
                case 1:
                    statusString = new byte[] { 0x21 };
                    break;

                // Sleep
                case 2:
                    statusString = new byte[] { 0x33, 0x4C, 0x45, 0x45, 0x50 };
                    break;

                // Poison
                case 3:
                    statusString = new byte[] { 0x30, 0x4F, 0x49, 0x53, 0x4F, 0x4E };
                    break;

                // Sadness
                case 4:
                    statusString = new byte[] { 0x33, 0x41, 0x44 };
                    break;

                // Fury
                case 5:
                    statusString = new byte[] { 0x26, 0x55, 0x52, 0x59 };
                    break;

                // Confu
                case 6:
                    statusString = new byte[] { 0x23, 0x4F, 0x4E, 0x46, 0x55 };
                    break;

                // Silence
                case 7:
                    statusString = new byte[] { 0x2D, 0x55, 0x54, 0x45 };
                    break;

                // Haste
                case 8:
                    statusString = new byte[] { 0x21 };
                    break;

                // Slow
                case 9:
                    statusString = new byte[] { 0x33, 0x4C, 0x4F, 0x57 };
                    break;

                // Stop
                case 10:
                    statusString = new byte[] { 0x33, 0x54, 0x4F, 0x50 };
                    break;

                // Frog
                case 11:
                    statusString = new byte[] { 0x26, 0x52, 0x4F, 0x47 };
                    break;

                // Mini
                case 12:
                    statusString = new byte[] { 0x2D, 0x49, 0x4E, 0x49 };
                    break;

                // Slow-Numb
                case 13:
                    statusString = new byte[] { 0x27, 0x30, 0x45, 0x54, 0x52, 0x49, 0x46, 0x59 };
                    break;

                // Petrify
                case 14:
                    statusString = new byte[] { 0x30, 0x45, 0x54, 0x52, 0x49, 0x46, 0x59 };
                    break;

                // Regen
                case 15:
                    statusString = new byte[] { 0x21 };
                    break;

                // Barrier
                case 16:
                    statusString = new byte[] { 0x21 };
                    break;

                // MBarrier
                case 17:
                    statusString = new byte[] { 0x21 };
                    break;

                // Reflect
                case 18:
                    statusString = new byte[] { 0x21 };
                    break;

                // Dual
                case 19:
                    statusString = new byte[] { 0x21 };
                    break;

                // Shield
                case 20:
                    statusString = new byte[] { 0x21 };
                    break;

                // Death Sentence
                case 21:
                    statusString = new byte[] { 0x24, 0x4F, 0x4F, 0x4D };
                    break;

                // Manip
                case 22:
                    statusString = new byte[] { 0x21 };
                    break;

                // Berserk
                case 23:
                    statusString = new byte[] { 0x22, 0x45, 0x52, 0x53, 0x45, 0x52, 0x4B };
                    break;

                // Peerless
                case 24:
                    statusString = new byte[] { 0x21 };
                    break;

                // Paralysis
                case 25:
                    statusString = new byte[] { 0x30, 0x41, 0x52, 0x41, 0x4C, 0x59, 0x53, 0x49, 0x53 };
                    break;

                // Blind
                case 26:
                    statusString = new byte[] { 0x22, 0x4C, 0x49, 0x4E, 0x44 };
                    break;

                // Dual-Drain
                case 27:
                    statusString = new byte[] { 0x21 };
                    break;

                // Death Force
                case 28:
                    statusString = new byte[] { 0x21 };
                    break;

                // Resist
                case 29:
                    statusString = new byte[] { 0x21 };
                    break;

                // Auto-Crits
                case 30:
                    statusString = new byte[] { 0x21 };
                    break;

                // Imprisoned
                case 31:
                    statusString = new byte[] { 0x21 };
                    break;

                /// ???, if this is printed in-game then something's wrong
                default:
                    statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }

            return statusString;
        }

        public static byte[] SpanishWeaponArmourStatusString(byte[][] attributes, int r, int att)
        {
            byte[] statusString;
            switch (attributes[r][att])
            {
                // KO
                case 0:
                    statusString = new byte[] { 0x24, 0x45, 0x41, 0x54, 0x48 };
                    break;

                // Near-KO
                case 1:
                    statusString = new byte[] { 0x21 };
                    break;

                // Sleep
                case 2:
                    statusString = new byte[] { 0x33, 0x4C, 0x45, 0x45, 0x50 };
                    break;

                // Poison
                case 3:
                    statusString = new byte[] { 0x30, 0x4F, 0x49, 0x53, 0x4F, 0x4E };
                    break;

                // Sadness
                case 4:
                    statusString = new byte[] { 0x33, 0x41, 0x44 };
                    break;

                // Fury
                case 5:
                    statusString = new byte[] { 0x26, 0x55, 0x52, 0x59 };
                    break;

                // Confu
                case 6:
                    statusString = new byte[] { 0x23, 0x4F, 0x4E, 0x46, 0x55 };
                    break;

                // Silence
                case 7:
                    statusString = new byte[] { 0x2D, 0x55, 0x54, 0x45 };
                    break;

                // Haste
                case 8:
                    statusString = new byte[] { 0x21 };
                    break;

                // Slow
                case 9:
                    statusString = new byte[] { 0x33, 0x4C, 0x4F, 0x57 };
                    break;

                // Stop
                case 10:
                    statusString = new byte[] { 0x33, 0x54, 0x4F, 0x50 };
                    break;

                // Frog
                case 11:
                    statusString = new byte[] { 0x26, 0x52, 0x4F, 0x47 };
                    break;

                // Mini
                case 12:
                    statusString = new byte[] { 0x2D, 0x49, 0x4E, 0x49 };
                    break;

                // Slow-Numb
                case 13:
                    statusString = new byte[] { 0x27, 0x30, 0x45, 0x54, 0x52, 0x49, 0x46, 0x59 };
                    break;

                // Petrify
                case 14:
                    statusString = new byte[] { 0x30, 0x45, 0x54, 0x52, 0x49, 0x46, 0x59 };
                    break;

                // Regen
                case 15:
                    statusString = new byte[] { 0x21 };
                    break;

                // Barrier
                case 16:
                    statusString = new byte[] { 0x21 };
                    break;

                // MBarrier
                case 17:
                    statusString = new byte[] { 0x21 };
                    break;

                // Reflect
                case 18:
                    statusString = new byte[] { 0x21 };
                    break;

                // Dual
                case 19:
                    statusString = new byte[] { 0x21 };
                    break;

                // Shield
                case 20:
                    statusString = new byte[] { 0x21 };
                    break;

                // Death Sentence
                case 21:
                    statusString = new byte[] { 0x24, 0x4F, 0x4F, 0x4D };
                    break;

                // Manip
                case 22:
                    statusString = new byte[] { 0x21 };
                    break;

                // Berserk
                case 23:
                    statusString = new byte[] { 0x22, 0x45, 0x52, 0x53, 0x45, 0x52, 0x4B };
                    break;

                // Peerless
                case 24:
                    statusString = new byte[] { 0x21 };
                    break;

                // Paralysis
                case 25:
                    statusString = new byte[] { 0x30, 0x41, 0x52, 0x41, 0x4C, 0x59, 0x53, 0x49, 0x53 };
                    break;

                // Blind
                case 26:
                    statusString = new byte[] { 0x22, 0x4C, 0x49, 0x4E, 0x44 };
                    break;

                // Dual-Drain
                case 27:
                    statusString = new byte[] { 0x21 };
                    break;

                // Death Force
                case 28:
                    statusString = new byte[] { 0x21 };
                    break;

                // Resist
                case 29:
                    statusString = new byte[] { 0x21 };
                    break;

                // Auto-Crits
                case 30:
                    statusString = new byte[] { 0x21 };
                    break;

                // Imprisoned
                case 31:
                    statusString = new byte[] { 0x21 };
                    break;

                /// ???, if this is printed in-game then something's wrong
                default:
                    statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }

            return statusString;
        }
    }
}
