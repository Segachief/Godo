using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Helper
{
    public class FormulaChange
    {
        public static byte PickDamageFormula(Random seed)
        {
            // List of valid damage formulae
            byte[] animSet = { 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17,
                                0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27,
                                0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
                                0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47,
                                0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57,
                                0x60, 0x61, 0x66, 0x68, 0x69, 0x6B,
                                0x70, 0x71, 0x76, 0x78, 0x79, 0x7B,
                                0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8,
                                0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7};

            // Some formulas removed from player pool for being too strong, like Target HP - 1, or unsuitable (Target's Materis * 1111)
            // Kept here for future ref
            byte[] excluded = { 0x6A, 0x6C, 0x6D, 0x7A, 0x7C, 0x7D };

            // Grab a random entry from the array we made
            byte pickAnim = animSet[seed.Next(animSet.Length)];

            return pickAnim;
        }

        public static byte PickWeaponFormula(Random seed)
        {
            // List of valid damage formulae
            byte[] animSet = { 0x11, 0x12, 0x16, 0x17,
                                0x21, 0x22, 0x26, 0x27,
                                0x31, 0x32, 0x36, 0x37,
                                0x41, 0x42, 0x46, 0x47,
                                0x51, 0x52, 0x56, 0x57,
                                0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8,
                                0xB1, 0xB2, 0xB6, 0xB7};

            // Grab a random entry from the array we made
            byte pickAnim = animSet[seed.Next(animSet.Length)];

            return pickAnim;
        }
    }
}
