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
    public class ElementStrings
    {
        public static byte[] EnglishElementString(byte[][] attributes, int r, int attA, int attB)
        {
            byte[] elementString = new byte[9];
            switch (attributes[r][attA])
            {
                // No value
                case 0:
                    break;

                // Fire
                case 1:
                    elementString = new byte[] { 0x26, 0x49, 0x52, 0x45 };
                    break;

                // Ice
                case 2:
                    elementString = new byte[] { 0x29, 0x43, 0x45 };
                    break;

                // Lightning
                case 4:
                    elementString = new byte[] { 0x22, 0x4F, 0x4C, 0x54 };
                    break;

                // Earth
                case 8:
                    elementString = new byte[] { 0x25, 0x41, 0x52, 0x54, 0x48 };
                    break;

                // Poison
                case 16:
                    elementString = new byte[] { 0x22, 0x49, 0x4F };
                    break;

                // Gravity
                case 32:
                    elementString = new byte[] { 0x27 };
                    break;

                // Water
                case 64:
                    elementString = new byte[] { 0x37, 0x41, 0x54, 0x45, 0x52 };
                    break;

                // Wind
                case 128:
                    elementString = new byte[] { 0x37, 0x49, 0x4E, 0x44 };
                    break;

                /// ???, if this is printed in-game then something's wrong
                default:
                    elementString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }
            switch (attributes[r][attB])
            {
                // No value
                case 0:
                    break;

                // Holy
                case 1:
                    elementString = new byte[] { 0x28, 0x4F, 0x4C, 0x59 };
                    break;

                // Restorative
                case 2:
                    elementString = new byte[] { 0x32 };
                    break;

                // Cut
                case 4:
                    elementString = new byte[] { 0x23, 0x55, 0x54 };
                    break;

                // Hit
                case 8:
                    elementString = new byte[] { 0x28, 0x49, 0x54 };
                    break;

                // Punch
                case 16:
                    elementString = new byte[] { 0x30, 0x55, 0x4E, 0x43, 0x48 };
                    break;

                // Shoot
                case 32:
                    elementString = new byte[] { 0x33, 0x48, 0x4F, 0x4F, 0x54 };
                    break;

                // Shout
                case 64:
                    elementString = new byte[] { 0x33, 0x48, 0x4F, 0x55, 0x54 };
                    break;

                // Hidden
                case 128:
                    elementString = new byte[] { 0x28, 0x49, 0x44, 0x44, 0x45, 0x4E };
                    break;

                /// ???, if this is printed in-game then something's wrong
                default:
                    elementString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }
            return elementString;
        }

        public static byte[] FrenchElementString(byte[][] attributes, int r, int attA, int attB)
        {
            byte[] elementString = new byte[9];
            switch (attributes[r][attA])
            {
                // No value
                case 0:
                    break;

                // Fire
                case 1:
                    elementString = new byte[] { 0x26, 0x49, 0x52, 0x45 };
                    break;

                // Ice
                case 2:
                    elementString = new byte[] { 0x29, 0x43, 0x45 };
                    break;

                // Lightning
                case 4:
                    elementString = new byte[] { 0x22, 0x4F, 0x4C, 0x54 };
                    break;

                // Earth
                case 8:
                    elementString = new byte[] { 0x25, 0x41, 0x52, 0x54, 0x48 };
                    break;

                // Poison
                case 16:
                    elementString = new byte[] { 0x22, 0x49, 0x4F };
                    break;

                // Gravity
                case 32:
                    elementString = new byte[] { 0x27 };
                    break;

                // Water
                case 64:
                    elementString = new byte[] { 0x37, 0x41, 0x54, 0x45, 0x52 };
                    break;

                // Wind
                case 128:
                    elementString = new byte[] { 0x37, 0x49, 0x4E, 0x44 };
                    break;

                /// ???, if this is printed in-game then something's wrong
                default:
                    elementString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }
            switch (attributes[r][attB])
            {
                // No value
                case 0:
                    break;

                // Holy
                case 1:
                    elementString = new byte[] { 0x28, 0x4F, 0x4C, 0x59 };
                    break;

                // Restorative
                case 2:
                    elementString = new byte[] { 0x32 };
                    break;

                // Cut
                case 4:
                    elementString = new byte[] { 0x23, 0x55, 0x54 };
                    break;

                // Hit
                case 8:
                    elementString = new byte[] { 0x28, 0x49, 0x54 };
                    break;

                // Punch
                case 16:
                    elementString = new byte[] { 0x30, 0x55, 0x4E, 0x43, 0x48 };
                    break;

                // Shoot
                case 32:
                    elementString = new byte[] { 0x33, 0x48, 0x4F, 0x4F, 0x54 };
                    break;

                // Shout
                case 64:
                    elementString = new byte[] { 0x33, 0x48, 0x4F, 0x55, 0x54 };
                    break;

                // Hidden
                case 128:
                    elementString = new byte[] { 0x28, 0x49, 0x44, 0x44, 0x45, 0x4E };
                    break;

                /// ???, if this is printed in-game then something's wrong
                default:
                    elementString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }
            return elementString;
        }

        public static byte[] GermanElementString(byte[][] attributes, int r, int attA, int attB)
        {
            byte[] elementString = new byte[9];
            switch (attributes[r][attA])
            {
                // No value
                case 0:
                    break;

                // Fire
                case 1:
                    elementString = new byte[] { 0x26, 0x49, 0x52, 0x45 };
                    break;

                // Ice
                case 2:
                    elementString = new byte[] { 0x29, 0x43, 0x45 };
                    break;

                // Lightning
                case 4:
                    elementString = new byte[] { 0x22, 0x4F, 0x4C, 0x54 };
                    break;

                // Earth
                case 8:
                    elementString = new byte[] { 0x25, 0x41, 0x52, 0x54, 0x48 };
                    break;

                // Poison
                case 16:
                    elementString = new byte[] { 0x22, 0x49, 0x4F };
                    break;

                // Gravity
                case 32:
                    elementString = new byte[] { 0x27 };
                    break;

                // Water
                case 64:
                    elementString = new byte[] { 0x37, 0x41, 0x54, 0x45, 0x52 };
                    break;

                // Wind
                case 128:
                    elementString = new byte[] { 0x37, 0x49, 0x4E, 0x44 };
                    break;

                /// ???, if this is printed in-game then something's wrong
                default:
                    elementString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }
            switch (attributes[r][attB])
            {
                // No value
                case 0:
                    break;

                // Holy
                case 1:
                    elementString = new byte[] { 0x28, 0x4F, 0x4C, 0x59 };
                    break;

                // Restorative
                case 2:
                    elementString = new byte[] { 0x32 };
                    break;

                // Cut
                case 4:
                    elementString = new byte[] { 0x23, 0x55, 0x54 };
                    break;

                // Hit
                case 8:
                    elementString = new byte[] { 0x28, 0x49, 0x54 };
                    break;

                // Punch
                case 16:
                    elementString = new byte[] { 0x30, 0x55, 0x4E, 0x43, 0x48 };
                    break;

                // Shoot
                case 32:
                    elementString = new byte[] { 0x33, 0x48, 0x4F, 0x4F, 0x54 };
                    break;

                // Shout
                case 64:
                    elementString = new byte[] { 0x33, 0x48, 0x4F, 0x55, 0x54 };
                    break;

                // Hidden
                case 128:
                    elementString = new byte[] { 0x28, 0x49, 0x44, 0x44, 0x45, 0x4E };
                    break;

                /// ???, if this is printed in-game then something's wrong
                default:
                    elementString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }
            return elementString;
        }

        public static byte[] SpanishElementString(byte[][] attributes, int r, int attA, int attB)
        {
            byte[] elementString = new byte[9];
            switch (attributes[r][attA])
            {
                // No value
                case 0:
                    break;

                // Fire
                case 1:
                    elementString = new byte[] { 0x26, 0x55, 0x45, 0x47, 0x4F };
                    break;

                // Ice
                case 2:
                    elementString = new byte[] { 0x26, 0x52, 0x49, 0x4F };
                    break;

                // Lightning
                case 4:
                    elementString = new byte[] { 0x32, 0x45, 0x4C, 0x41, 0x4D, 0x50, 0x41, 0x47, 0x4F };
                    break;

                // Earth
                case 8:
                    elementString = new byte[] { 0x34, 0x49, 0x45, 0x52, 0x52, 0x41 };
                    break;

                // Poison
                case 16:
                    elementString = new byte[] { 0x36, 0x45, 0x4E, 0x45, 0x4E, 0x4F };
                    break;

                // Gravity
                case 32:
                    elementString = new byte[] { 0x27 };
                    break;

                // Water
                case 64:
                    elementString = new byte[] { 0x21, 0x47, 0x55, 0x41 };
                    break;

                // Wind
                case 128:
                    elementString = new byte[] { 0x36, 0x49, 0x45, 0x4E, 0x54, 0x4F };
                    break;

                /// ???, if this is printed in-game then something's wrong
                default:
                    elementString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }
            switch (attributes[r][attB])
            {
                // No value
                case 0:
                    break;

                // Holy
                case 1:
                    elementString = new byte[] { 0x33, 0x41, 0x4E, 0x54, 0x4F };
                    break;

                // Restorative
                case 2:
                    elementString = new byte[] { 0x32 };
                    break;

                // Cut
                case 4:
                    elementString = new byte[] { 0x23, 0x4F, 0x52, 0x54, 0x41, 0x52 };
                    break;

                // Hit
                case 8:
                    elementString = new byte[] { 0x27, 0x4F, 0x4C, 0x50, 0x45 };
                    break;

                // Punch
                case 16:
                    elementString = new byte[] { 0x30, 0x45, 0x52, 0x46, 0x4F, 0x52, 0x41 };
                    break;

                // Shoot
                case 32:
                    elementString = new byte[] { 0x24, 0x49, 0x53, 0x50, 0x41, 0x52, 0x4F};
                    break;

                // Shout
                case 64:
                    elementString = new byte[] { 0x27, 0x52, 0x49, 0x54, 0x4F };
                    break;

                // Hidden
                case 128:
                    elementString = new byte[] { 0x25, 0x53, 0x43, 0x4F, 0x4E, 0x44, 0x49, 0x44, 0x4F };
                    break;

                /// ???, if this is printed in-game then something's wrong
                default:
                    elementString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }
            return elementString;
        }

        public static byte[] EnglishElementTypeString(byte[][] attributes, int r, int att)
        {
            byte[] elementTypeString;
            switch (attributes[r][att])
            {
                // Drain
                case 0:
                    elementTypeString = new byte[] { 0x3C, 0x24, 0x52, 0x41, 0x49, 0x4E };
                    break;

                // Null
                case 1:
                    elementTypeString = new byte[] { 0x3C, 0x2E, 0x55, 0x4C, 0x4C };
                    break;

                // Half
                case 2:
                    elementTypeString = new byte[] { 0x3C, 0x28, 0x41, 0x4C, 0x46 };
                    break;

                /// ???, if this is printed in-game then something's wrong
                default:
                    elementTypeString = new byte[] { 0x3C, 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }
            return elementTypeString;
        }

        public static byte[] FrenchElementTypeString(byte[][] attributes, int r, int att)
        {
            byte[] elementTypeString;
            switch (attributes[r][att])
            {
                // Drain
                case 0:
                    elementTypeString = new byte[] { 0x3C, 0x24, 0x52, 0x41, 0x49, 0x4E };
                    break;

                // Null
                case 1:
                    elementTypeString = new byte[] { 0x3C, 0x2E, 0x55, 0x4C, 0x4C };
                    break;

                // Half
                case 2:
                    elementTypeString = new byte[] { 0x3C, 0x28, 0x41, 0x4C, 0x46 };
                    break;

                /// ???, if this is printed in-game then something's wrong
                default:
                    elementTypeString = new byte[] { 0x3C, 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }
            return elementTypeString;
        }

        public static byte[] GermanElementTypeString(byte[][] attributes, int r, int att)
        {
            byte[] elementTypeString;
            switch (attributes[r][att])
            {
                // Drain
                case 0:
                    elementTypeString = new byte[] { 0x3C, 0x24, 0x52, 0x41, 0x49, 0x4E };
                    break;

                // Null
                case 1:
                    elementTypeString = new byte[] { 0x3C, 0x2E, 0x55, 0x4C, 0x4C };
                    break;

                // Half
                case 2:
                    elementTypeString = new byte[] { 0x3C, 0x28, 0x41, 0x4C, 0x46 };
                    break;

                /// ???, if this is printed in-game then something's wrong
                default:
                    elementTypeString = new byte[] { 0x3C, 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }
            return elementTypeString;
        }

        public static byte[] SpanishElementTypeString(byte[][] attributes, int r, int att)
        {
            byte[] elementTypeString;
            switch (attributes[r][att])
            {
                // Drain
                case 0:
                    elementTypeString = new byte[] { 0x3C, 0x24, 0x52, 0x41, 0x49, 0x4E, 0x45, 0x52 };
                    break;

                // Null
                case 1:
                    elementTypeString = new byte[] { 0x3C, 0x2E, 0x55, 0x4C, 0x4F };
                    break;

                // Half
                case 2:
                    elementTypeString = new byte[] { 0x3C, 0x2D, 0x49, 0x54, 0x41, 0x44 };
                    break;

                /// ???, if this is printed in-game then something's wrong
                default:
                    elementTypeString = new byte[] { 0x3C, 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }
            return elementTypeString;
        }
    }
}
