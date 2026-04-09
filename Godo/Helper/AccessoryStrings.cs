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
    public class AccessoryStrings
    {
        public static byte[] EnglishAccessoryStatusString(byte[][] attributes, int r, int att)
        {
            byte[] statusString;
            if (att == 8)
            {
                switch (attributes[r][att])
                {
                    // KO
                    case 1:
                        statusString = new byte[] { 0x24, 0x45, 0x41, 0x54, 0x48 };
                        break;

                    // Near-KO
                    case 2:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Sleep
                    case 4:
                        statusString = new byte[] { 0x33, 0x4C, 0x45, 0x45, 0x50 };
                        break;

                    // Poison
                    case 8:
                        statusString = new byte[] { 0x30, 0x4F, 0x49, 0x53, 0x4F, 0x4E };
                        break;

                    // Sadness
                    case 16:
                        statusString = new byte[] { 0x33, 0x41, 0x44 };
                        break;

                    // Fury
                    case 32:
                        statusString = new byte[] { 0x26, 0x55, 0x52, 0x59 };
                        break;

                    // Confu
                    case 64:
                        statusString = new byte[] { 0x23, 0x4F, 0x4E, 0x46, 0x55 };
                        break;

                    // Silence
                    case 128:
                        statusString = new byte[] { 0x2D, 0x55, 0x54, 0x45 };
                        break;

                    /// ???, if this is printed in-game then something's wrong
                    default:
                        statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                        break;
                }
            }
            else if (att == 9)
            {
                switch (attributes[r][att])
                {
                    // Haste
                    case 1:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Slow
                    case 2:
                        statusString = new byte[] { 0x33, 0x4C, 0x4F, 0x57 };
                        break;

                    // Stop
                    case 4:
                        statusString = new byte[] { 0x33, 0x54, 0x4F, 0x50 };
                        break;

                    // Frog
                    case 8:
                        statusString = new byte[] { 0x26, 0x52, 0x4F, 0x47 };
                        break;

                    // Mini
                    case 16:
                        statusString = new byte[] { 0x2D, 0x49, 0x4E, 0x49 };
                        break;

                    // Slow-Numb
                    case 32:
                        statusString = new byte[] { 0x27, 0x30, 0x45, 0x54, 0x52, 0x49, 0x46, 0x59 };
                        break;

                    // Petrify
                    case 64:
                        statusString = new byte[] { 0x30, 0x45, 0x54, 0x52, 0x49, 0x46, 0x59 };
                        break;

                    // Regen
                    case 128:
                        statusString = new byte[] { 0x21 };
                        break;

                    /// ???, if this is printed in-game then something's wrong
                    default:
                        statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                        break;
                }
            }
            else if (att == 10)
            {
                switch (attributes[r][att])
                {
                    // Barrier
                    case 1:
                        statusString = new byte[] { 0x21 };
                        break;

                    // MBarrier
                    case 2:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Reflect
                    case 4:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Dual
                    case 8:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Shield
                    case 16:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Death Sentence
                    case 32:
                        statusString = new byte[] { 0x24, 0x4F, 0x4F, 0x4D };
                        break;

                    // Manip
                    case 64:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Berserk
                    case 128:
                        statusString = new byte[] { 0x22, 0x45, 0x52, 0x53, 0x45, 0x52, 0x4B };
                        break;

                    /// ???, if this is printed in-game then something's wrong
                    default:
                        statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                        break;
                }
            }
            else
            {
                switch (attributes[r][att])
                {
                    // Peerless
                    case 1:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Paralysis
                    case 2:
                        statusString = new byte[] { 0x30, 0x41, 0x52, 0x41, 0x4C, 0x59, 0x53, 0x49, 0x53 };
                        break;

                    // Blind
                    case 4:
                        statusString = new byte[] { 0x22, 0x4C, 0x49, 0x4E, 0x44 };
                        break;

                    // Dual-Drain
                    case 8:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Death Force
                    case 16:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Resist
                    case 32:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Auto-Crits
                    case 64:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Imprisoned
                    case 128:
                        statusString = new byte[] { 0x21 };
                        break;

                    /// ???, if this is printed in-game then something's wrong
                    default:
                        statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                        break;
                }
            }
            return statusString;
        }

        public static byte[] FrenchAccessoryStatusString(byte[][] attributes, int r, int att)
        {
            byte[] statusString;
            if (att == 8)
            {
                switch (attributes[r][att])
                {
                    // KO
                    case 1:
                        statusString = new byte[] { 0x24, 0x45, 0x41, 0x54, 0x48 };
                        break;

                    // Near-KO
                    case 2:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Sleep
                    case 4:
                        statusString = new byte[] { 0x33, 0x4C, 0x45, 0x45, 0x50 };
                        break;

                    // Poison
                    case 8:
                        statusString = new byte[] { 0x30, 0x4F, 0x49, 0x53, 0x4F, 0x4E };
                        break;

                    // Sadness
                    case 16:
                        statusString = new byte[] { 0x33, 0x41, 0x44 };
                        break;

                    // Fury
                    case 32:
                        statusString = new byte[] { 0x26, 0x55, 0x52, 0x59 };
                        break;

                    // Confu
                    case 64:
                        statusString = new byte[] { 0x23, 0x4F, 0x4E, 0x46, 0x55 };
                        break;

                    // Silence
                    case 128:
                        statusString = new byte[] { 0x2D, 0x55, 0x54, 0x45 };
                        break;

                    /// ???, if this is printed in-game then something's wrong
                    default:
                        statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                        break;
                }
            }
            else if (att == 9)
            {
                switch (attributes[r][att])
                {
                    // Haste
                    case 1:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Slow
                    case 2:
                        statusString = new byte[] { 0x33, 0x4C, 0x4F, 0x57 };
                        break;

                    // Stop
                    case 4:
                        statusString = new byte[] { 0x33, 0x54, 0x4F, 0x50 };
                        break;

                    // Frog
                    case 8:
                        statusString = new byte[] { 0x26, 0x52, 0x4F, 0x47 };
                        break;

                    // Mini
                    case 16:
                        statusString = new byte[] { 0x2D, 0x49, 0x4E, 0x49 };
                        break;

                    // Slow-Numb
                    case 32:
                        statusString = new byte[] { 0x27, 0x30, 0x45, 0x54, 0x52, 0x49, 0x46, 0x59 };
                        break;

                    // Petrify
                    case 64:
                        statusString = new byte[] { 0x30, 0x45, 0x54, 0x52, 0x49, 0x46, 0x59 };
                        break;

                    // Regen
                    case 128:
                        statusString = new byte[] { 0x21 };
                        break;

                    /// ???, if this is printed in-game then something's wrong
                    default:
                        statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                        break;
                }
            }
            else if (att == 10)
            {
                switch (attributes[r][att])
                {
                    // Barrier
                    case 1:
                        statusString = new byte[] { 0x21 };
                        break;

                    // MBarrier
                    case 2:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Reflect
                    case 4:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Dual
                    case 8:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Shield
                    case 16:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Death Sentence
                    case 32:
                        statusString = new byte[] { 0x24, 0x4F, 0x4F, 0x4D };
                        break;

                    // Manip
                    case 64:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Berserk
                    case 128:
                        statusString = new byte[] { 0x22, 0x45, 0x52, 0x53, 0x45, 0x52, 0x4B };
                        break;

                    /// ???, if this is printed in-game then something's wrong
                    default:
                        statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                        break;
                }
            }
            else
            {
                switch (attributes[r][att])
                {
                    // Peerless
                    case 1:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Paralysis
                    case 2:
                        statusString = new byte[] { 0x30, 0x41, 0x52, 0x41, 0x4C, 0x59, 0x53, 0x49, 0x53 };
                        break;

                    // Blind
                    case 4:
                        statusString = new byte[] { 0x22, 0x4C, 0x49, 0x4E, 0x44 };
                        break;

                    // Dual-Drain
                    case 8:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Death Force
                    case 16:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Resist
                    case 32:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Auto-Crits
                    case 64:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Imprisoned
                    case 128:
                        statusString = new byte[] { 0x21 };
                        break;

                    /// ???, if this is printed in-game then something's wrong
                    default:
                        statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                        break;
                }
            }
            return statusString;
        }

        public static byte[] GermanAccessoryStatusString(byte[][] attributes, int r, int att)
        {
            byte[] statusString;
            if (att == 8)
            {
                switch (attributes[r][att])
                {
                    // KO
                    case 1:
                        statusString = new byte[] { 0x24, 0x45, 0x41, 0x54, 0x48 };
                        break;

                    // Near-KO
                    case 2:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Sleep
                    case 4:
                        statusString = new byte[] { 0x33, 0x4C, 0x45, 0x45, 0x50 };
                        break;

                    // Poison
                    case 8:
                        statusString = new byte[] { 0x30, 0x4F, 0x49, 0x53, 0x4F, 0x4E };
                        break;

                    // Sadness
                    case 16:
                        statusString = new byte[] { 0x33, 0x41, 0x44 };
                        break;

                    // Fury
                    case 32:
                        statusString = new byte[] { 0x26, 0x55, 0x52, 0x59 };
                        break;

                    // Confu
                    case 64:
                        statusString = new byte[] { 0x23, 0x4F, 0x4E, 0x46, 0x55 };
                        break;

                    // Silence
                    case 128:
                        statusString = new byte[] { 0x2D, 0x55, 0x54, 0x45 };
                        break;

                    /// ???, if this is printed in-game then something's wrong
                    default:
                        statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                        break;
                }
            }
            else if (att == 9)
            {
                switch (attributes[r][att])
                {
                    // Haste
                    case 1:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Slow
                    case 2:
                        statusString = new byte[] { 0x33, 0x4C, 0x4F, 0x57 };
                        break;

                    // Stop
                    case 4:
                        statusString = new byte[] { 0x33, 0x54, 0x4F, 0x50 };
                        break;

                    // Frog
                    case 8:
                        statusString = new byte[] { 0x26, 0x52, 0x4F, 0x47 };
                        break;

                    // Mini
                    case 16:
                        statusString = new byte[] { 0x2D, 0x49, 0x4E, 0x49 };
                        break;

                    // Slow-Numb
                    case 32:
                        statusString = new byte[] { 0x27, 0x30, 0x45, 0x54, 0x52, 0x49, 0x46, 0x59 };
                        break;

                    // Petrify
                    case 64:
                        statusString = new byte[] { 0x30, 0x45, 0x54, 0x52, 0x49, 0x46, 0x59 };
                        break;

                    // Regen
                    case 128:
                        statusString = new byte[] { 0x21 };
                        break;

                    /// ???, if this is printed in-game then something's wrong
                    default:
                        statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                        break;
                }
            }
            else if (att == 10)
            {
                switch (attributes[r][att])
                {
                    // Barrier
                    case 1:
                        statusString = new byte[] { 0x21 };
                        break;

                    // MBarrier
                    case 2:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Reflect
                    case 4:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Dual
                    case 8:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Shield
                    case 16:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Death Sentence
                    case 32:
                        statusString = new byte[] { 0x24, 0x4F, 0x4F, 0x4D };
                        break;

                    // Manip
                    case 64:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Berserk
                    case 128:
                        statusString = new byte[] { 0x22, 0x45, 0x52, 0x53, 0x45, 0x52, 0x4B };
                        break;

                    /// ???, if this is printed in-game then something's wrong
                    default:
                        statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                        break;
                }
            }
            else
            {
                switch (attributes[r][att])
                {
                    // Peerless
                    case 1:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Paralysis
                    case 2:
                        statusString = new byte[] { 0x30, 0x41, 0x52, 0x41, 0x4C, 0x59, 0x53, 0x49, 0x53 };
                        break;

                    // Blind
                    case 4:
                        statusString = new byte[] { 0x22, 0x4C, 0x49, 0x4E, 0x44 };
                        break;

                    // Dual-Drain
                    case 8:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Death Force
                    case 16:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Resist
                    case 32:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Auto-Crits
                    case 64:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Imprisoned
                    case 128:
                        statusString = new byte[] { 0x21 };
                        break;

                    /// ???, if this is printed in-game then something's wrong
                    default:
                        statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                        break;
                }
            }
            return statusString;
        }

        public static byte[] SpanishAccessoryStatusString(byte[][] attributes, int r, int att)
        {
            byte[] statusString;
            if (att == 8)
            {
                switch (attributes[r][att])
                {
                    // KO
                    case 1:
                        statusString = new byte[] { 0x2D, 0x55, 0x45, 0x52, 0x54, 0x45 };
                        break;

                    // Near-KO
                    case 2:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Sleep
                    case 4:
                        statusString = new byte[] { 0x33, 0x55, 0x45, 0x4E, 0x4F };
                        break;

                    // Poison
                    case 8:
                        statusString = new byte[] { 0x36, 0x45, 0x4E, 0x45, 0x4E, 0x4F };
                        break;

                    // Sadness
                    case 16:
                        statusString = new byte[] { 0x34, 0x52, 0x49, 0x53, 0x54, 0x45 };
                        break;

                    // Fury
                    case 32:
                        statusString = new byte[] { 0x26, 0x55, 0x52, 0x49, 0x41 };
                        break;

                    // Confu
                    case 64:
                        statusString = new byte[] { 0x23, 0x4F, 0x4E, 0x46, 0x55, 0x4E, 0x44, 0x49, 0x52 };
                        break;

                    // Silence
                    case 128:
                        statusString = new byte[] { 0x2D, 0x55, 0x44, 0x4F };
                        break;

                    /// ???, if this is printed in-game then something's wrong
                    default:
                        statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                        break;
                }
            }
            else if (att == 9)
            {
                switch (attributes[r][att])
                {
                    // Haste
                    case 1:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Slow
                    case 2:
                        statusString = new byte[] { 0x2C, 0x45, 0x4E, 0x54, 0x4F };
                        break;

                    // Stop
                    case 4:
                        statusString = new byte[] { 0x30, 0x41, 0x52, 0x41, 0x52 };
                        break;

                    // Frog
                    case 8:
                        statusString = new byte[] { 0x32, 0x41, 0x4E, 0x41 };
                        break;

                    // Mini
                    case 16:
                        statusString = new byte[] { 0x30, 0x45, 0x51, 0x55, 0x45, 0x4E, 0x4F };
                        break;

                    // Slow-Numb
                    case 32:
                        statusString = new byte[] { 0x30, 0x49, 0x45, 0x44, 0x52, 0x41 };
                        break;

                    // Petrify
                    case 64:
                        statusString = new byte[] { 0x30, 0x49, 0x45, 0x44, 0x52, 0x41 };
                        break;

                    // Regen
                    case 128:
                        statusString = new byte[] { 0x21 };
                        break;

                    /// ???, if this is printed in-game then something's wrong
                    default:
                        statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                        break;
                }
            }
            else if (att == 10)
            {
                switch (attributes[r][att])
                {
                    // Barrier
                    case 1:
                        statusString = new byte[] { 0x21 };
                        break;

                    // MBarrier
                    case 2:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Reflect
                    case 4:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Dual
                    case 8:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Shield
                    case 16:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Death Sentence
                    case 32:
                        statusString = new byte[] { 0x21, 0x44, 0x4F, 0x52 };
                        break;

                    // Manip
                    case 64:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Berserk
                    case 128:
                        statusString = new byte[] { 0x34, 0x52 };
                        break;

                    /// ???, if this is printed in-game then something's wrong
                    default:
                        statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                        break;
                }
            }
            else
            {
                switch (attributes[r][att])
                {
                    // Peerless
                    case 1:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Paralysis
                    case 2:
                        statusString = new byte[] { 0x30, 0x41, 0x52, 0x41, 0x4C, 0x59, 0x53, 0x49, 0x53 };
                        break;

                    // Blind
                    case 4:
                        statusString = new byte[] { 0x2F, 0x53, 0x43 };
                        break;

                    // Dual-Drain
                    case 8:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Death Force
                    case 16:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Resist
                    case 32:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Auto-Crits
                    case 64:
                        statusString = new byte[] { 0x21 };
                        break;

                    // Imprisoned
                    case 128:
                        statusString = new byte[] { 0x21 };
                        break;

                    /// ???, if this is printed in-game then something's wrong
                    default:
                        statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                        break;
                }
            }
            return statusString;
        }
    }
}
