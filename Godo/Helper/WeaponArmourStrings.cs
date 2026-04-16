using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo.Helper
{
    public class EquipmentStrings
    {

        public static byte[] EnglishEquipmentParameterString(byte[][] attributes, int r, int att)
        {
            byte[] parameterString;
            //r = equipment id, att = attribute type (strength, etc.)
            switch (attributes[r][att])
            {
                // STR
                case 0:
                    parameterString = new byte[] { 0x33, 0x34, 0x32 };
                    break;

                // VIT
                case 1:
                    parameterString = new byte[] { 0x36, 0x29, 0x34 };
                    break;

                // MAG
                case 2:
                    parameterString = new byte[] { 0x2D, 0x21, 0x27 };
                    break;

                // SPR
                case 3:
                    parameterString = new byte[] { 0x33, 0x30, 0x32 };
                    break;

                // DEX
                case 4:
                    parameterString = new byte[] { 0x24, 0x25, 0x38 };
                    break;

                // LCK
                case 5:
                    parameterString = new byte[] { 0x2C, 0x23, 0x2B };
                    break;

                // ???, if this is printed in-game then something's wrong
                default:
                    parameterString = new byte[] { 0x1F, 0x1F, 0x1F };
                    break;
            }
            return parameterString;
        }

        public static byte[] FrenchEquipmentParameterString(byte[][] attributes, int r, int att)
        {
            byte[] parameterString;
            //r = equipment id, att = attribute type (strength, etc.)
            switch (attributes[r][att])
            {
                // STR/FRC
                case 0:
                    parameterString = new byte[] { 0x26, 0x32, 0x23 };
                    break;

                // VIT
                case 1:
                    parameterString = new byte[] { 0x36, 0x29, 0x34 };
                    break;

                // MAG
                case 2:
                    parameterString = new byte[] { 0x2D, 0x21, 0x27 };
                    break;

                // SPR/ESP
                case 3:
                    parameterString = new byte[] { 0x25, 0x33, 0x30 };
                    break;

                // DEX
                case 4:
                    parameterString = new byte[] { 0x24, 0x25, 0x38 };
                    break;

                // LCK/CHN
                case 5:
                    parameterString = new byte[] { 0x23, 0x28, 0x2E };
                    break;

                // ???, if this is printed in-game then something's wrong
                default:
                    parameterString = new byte[] { 0x1F, 0x1F, 0x1F };
                    break;
            }
            return parameterString;
        }

        public static byte[] GermanEquipmentParameterString(byte[][] attributes, int r, int att)
        {
            byte[] parameterString;
            //r = equipment id, att = attribute type (strength, etc.)
            switch (attributes[r][att])
            {
                // STR/STK
                case 0:
                    parameterString = new byte[] { 0x33, 0x34, 0x2B };
                    break;

                // VIT
                case 1:
                    parameterString = new byte[] { 0x36, 0x29, 0x34 };
                    break;

                // MAG
                case 2:
                    parameterString = new byte[] { 0x2D, 0x21, 0x27 };
                    break;

                // SPR/SLE
                case 3:
                    parameterString = new byte[] { 0x33, 0x2C, 0x25};
                    break;

                // DEX/GSK
                case 4:
                    parameterString = new byte[] { 0x27, 0x33, 0x2B };
                    break;

                // LCK/GLK
                case 5:
                    parameterString = new byte[] { 0x27, 0x2C, 0x2B };
                    break;

                // ???, if this is printed in-game then something's wrong
                default:
                    parameterString = new byte[] { 0x1F, 0x1F, 0x1F };
                    break;
            }
            return parameterString;
        }

        public static byte[] SpanishEquipmentParameterString(byte[][] attributes, int r, int att)
        {
            byte[] parameterString;
            //r = equipment id, att = attribute type (strength, etc.)
            switch (attributes[r][att])
            {
                // STR
                case 0:
                    parameterString = new byte[] { 0x3, 0x34, 0x32 };
                    break;

                // VIT
                case 1:
                    parameterString = new byte[] { 0x36, 0x29, 0x34 };
                    break;

                // MAG
                case 2:
                    parameterString = new byte[] { 0x2D, 0x21, 0x27 };
                    break;

                // SPR
                case 3:
                    parameterString = new byte[] { 0x33, 0x30, 0x32 };
                    break;

                // DEX
                case 4:
                    parameterString = new byte[] { 0x24, 0x25, 0x38 };
                    break;

                // LCK
                case 5:
                    parameterString = new byte[] { 0x2C, 0x23, 0x2B };
                    break;

                // ???, if this is printed in-game then something's wrong
                default:
                    parameterString = new byte[] { 0x1F, 0x1F, 0x1F };
                    break;
            }
            return parameterString;
        }

        public static byte[] JapaneseEquipmentParameterString(byte[][] attributes, int r, int att)
        {
            byte[] parameterString;
            //r = equipment id, att = attribute type (strength, etc.)
            switch (attributes[r][att])
            {
                // STR
                case 0:
                    parameterString = new byte[] { 0x3, 0x34, 0x32 };
                    break;

                // VIT
                case 1:
                    parameterString = new byte[] { 0x36, 0x29, 0x34 };
                    break;

                // MAG
                case 2:
                    parameterString = new byte[] { 0x2D, 0x21, 0x27 };
                    break;

                // SPR
                case 3:
                    parameterString = new byte[] { 0x33, 0x30, 0x32 };
                    break;

                // DEX
                case 4:
                    parameterString = new byte[] { 0x24, 0x25, 0x38 };
                    break;

                // LCK
                case 5:
                    parameterString = new byte[] { 0x2C, 0x23, 0x2B };
                    break;

                // ???, if this is printed in-game then something's wrong
                default:
                    parameterString = new byte[] { 0x1F, 0x1F, 0x1F };
                    break;
            }
            return parameterString;
        }

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

                /// Error, if this is printed in-game then something's wrong
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
                // KO/Mort
                case 0:
                    statusString = new byte[] { 0x2D, 0x4F, 0x52, 0x54 };
                    break;

                // Near-KO/Unused
                case 1:
                    statusString = new byte[] { 0x21 };
                    break;

                // Sleep/Sommeil
                case 2:
                    statusString = new byte[] { 0x33, 0x4F, 0x4D, 0x4D, 0x45, 0x49, 0x4C };
                    break;

                // Poison
                case 3:
                    statusString = new byte[] { 0x30, 0x4F, 0x49, 0x53, 0x4F, 0x4E };
                    break;

                // Sadness/Triste
                case 4:
                    statusString = new byte[] { 0x34, 0x52, 0x49, 0x53, 0x54, 0x45 };
                    break;

                // Fury/Furie
                case 5:
                    statusString = new byte[] { 0x26, 0x55, 0x52, 0x49, 0x45 };
                    break;

                // Confu
                case 6:
                    statusString = new byte[] { 0x23, 0x4F, 0x4E, 0x46, 0x55 };
                    break;

                // Silence
                case 7:
                    statusString = new byte[] { 0x2D, 0x55, 0x54, 0x45 };
                    break;

                // Haste/Unused
                case 8:
                    statusString = new byte[] { 0x21 };
                    break;

                // Slow/Lent
                case 9:
                    statusString = new byte[] { 0x2C, 0x45, 0x4E, 0x54 };
                    break;

                // Stop
                case 10:
                    statusString = new byte[] { 0x33, 0x54, 0x4F, 0x50 };
                    break;

                // Frog/Gren
                case 11:
                    statusString = new byte[] { 0x27, 0x52, 0x45, 0x4E };
                    break;

                // Mini
                case 12:
                    statusString = new byte[] { 0x2D, 0x49, 0x4E, 0x49 };
                    break;

                // Slow-Numb/PetGrad
                case 13:
                    statusString = new byte[] { 0x30, 0x45, 0x54, 0x27, 0x52, 0x41, 0x44 };
                    break;

                // Petrify/Pet
                case 14:
                    statusString = new byte[] { 0x30, 0x45, 0x54 };
                    break;

                // Regen/Unused
                case 15:
                    statusString = new byte[] { 0x21 };
                    break;

                // Barrier/Unused
                case 16:
                    statusString = new byte[] { 0x21 };
                    break;

                // MBarrier/Unused
                case 17:
                    statusString = new byte[] { 0x21 };
                    break;

                // Reflect/Unused
                case 18:
                    statusString = new byte[] { 0x21 };
                    break;

                // Dual/Unused
                case 19:
                    statusString = new byte[] { 0x21 };
                    break;

                // Shield/Unused
                case 20:
                    statusString = new byte[] { 0x21 };
                    break;

                // Death Sentence/C. à mort (ToDo)
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

                // Peerless/Unused
                case 24:
                    statusString = new byte[] { 0x21 };
                    break;

                // Paralysis/Paralysie
                case 25:
                    statusString = new byte[] { 0x30, 0x41, 0x52, 0x41, 0x4C, 0x59, 0x53, 0x49, 0x45 };
                    break;

                // Blind/Aveuglé (ToDo)
                case 26:
                    statusString = new byte[] { 0x22, 0x4C, 0x49, 0x4E, 0x44 };
                    break;

                // Dual-Drain/Unused
                case 27:
                    statusString = new byte[] { 0x21 };
                    break;

                // Death Force/Unused
                case 28:
                    statusString = new byte[] { 0x21 };
                    break;

                // Resist/Unused
                case 29:
                    statusString = new byte[] { 0x21 };
                    break;

                // Auto-Crits/Unused
                case 30:
                    statusString = new byte[] { 0x21 };
                    break;

                // Imprisoned/Unused
                case 31:
                    statusString = new byte[] { 0x21 };
                    break;

                /// Error, if this is printed in-game then something's wrong
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
                // KO/Tod
                case 0:
                    statusString = new byte[] { 0x34, 0x4F, 0x44 };
                    break;

                // Near-KO
                case 1:
                    statusString = new byte[] { 0x21 };
                    break;

                // Sleep/Schlaf
                case 2:
                    statusString = new byte[] { 0x33, 0x43, 0x48, 0x4C, 0x41, 0x46 };
                    break;

                // Poison/Gift
                case 3:
                    statusString = new byte[] { 0x27, 0x49, 0x46, 0x54 };
                    break;

                // Sadness/Trauer
                case 4:
                    statusString = new byte[] { 0x34, 0x52, 0x41, 0x55, 0x45, 0x52 };
                    break;

                // Fury/Wut
                case 5:
                    statusString = new byte[] { 0x37, 0x55, 0x54 };
                    break;

                // Confu/Verwirr
                case 6:
                    statusString = new byte[] { 0x36, 0x45, 0x52, 0x57, 0x49, 0x52, 0x52 };
                    break;

                // Silence/Stumm
                case 7:
                    statusString = new byte[] { 0x33, 0x54, 0x55, 0x4D, 0x4D };
                    break;

                // Haste
                case 8:
                    statusString = new byte[] { 0x21 };
                    break;

                // Slow/Gemach
                case 9:
                    statusString = new byte[] { 0x27, 0x45, 0x4D, 0x41, 0x43, 0x48 };
                    break;

                // Stop
                case 10:
                    statusString = new byte[] { 0x33, 0x54, 0x4F, 0x50 };
                    break;

                // Frog/Frosch
                case 11:
                    statusString = new byte[] { 0x26, 0x52, 0x4F, 0x53, 0x43, 0x48 };
                    break;

                // Mini
                case 12:
                    statusString = new byte[] { 0x2D, 0x49, 0x4E, 0x49 };
                    break;

                // Slow-Numb/Verstein
                case 13:
                    statusString = new byte[] { 0x36, 0x45, 0x52, 0x53, 0x54, 0x45, 0x49, 0x4E };
                    break;

                // Petrify/Stein
                case 14:
                    statusString = new byte[] { 0x33, 0x54, 0x45, 0x49, 0x4E };
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

                // Death Sentence/Todschlag
                case 21:
                    statusString = new byte[] { 0x34, 0x4F, 0x44, 0x53, 0x43, 0x48, 0x4C, 0x41, 0x47 };
                    break;

                // Manip
                case 22:
                    statusString = new byte[] { 0x21 };
                    break;

                // Berserk/Berserker
                case 23:
                    statusString = new byte[] { 0x22, 0x45, 0x52, 0x53, 0x45, 0x52, 0x4B, 0x45, 0x52 };
                    break;

                // Peerless
                case 24:
                    statusString = new byte[] { 0x21 };
                    break;

                // Paralysis
                case 25:
                    statusString = new byte[] { 0x30, 0x41, 0x52, 0x41, 0x4C, 0x59, 0x53, 0x45 };
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

                /// Error, if this is printed in-game then something's wrong
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

                /// Error, if this is printed in-game then something's wrong
                default:
                    statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }
            return statusString;
        }

        public static byte[] JapaneseWeaponArmourStatusString(byte[][] attributes, int r, int att)
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

                /// Error, if this is printed in-game then something's wrong
                default:
                    statusString = new byte[] { 0x25, 0x52, 0x52, 0x4F, 0x52 };
                    break;
            }

            return statusString;
        }
    }
}
