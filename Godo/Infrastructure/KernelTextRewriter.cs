using Godo.Helper;
using System.IO;
using System.Windows.Forms;
using Godo.Omnichange;

namespace Godo.Infrastructure
{
    public class KernelTextRewriter
    {
        public static void WeaponDescriptionRewrite(byte[][] weaponAttributes, bool[] languageOptions)
        {
            int r = 0; // Current weapon ID being written for
            int i = 0; // Dealing with 2-digit values in stat bonuses
            int c = 0; // Used for writing the status string
            int e = 0; // Counts byte position of the string being written
            string weaponStringsFile = Directory.GetCurrentDirectory() + "\\Kernel Strings\\kernel2Modified.bin12";

            //Each weapon string is stored as a separate array so it can have its length counted quickly
            byte[][] weaponStrings = new byte[128][];

            // Starts with all the 2byte headers for each weapon; adds on string values later for total size

            // Tracks the actual length used within each string (using FF terminator)
            int[] stringSizes = new int[128];

            // Tracks the size of the string to create a header offset for it
            ulong stringSize = 0;
            byte[] parameterString = {0x1F, 0x1F, 0x1F};

            try
            {
                while (r < 128)
                {
                    // Temporary container for strings (64 bytes of space; can be extended but should be plenty)
                    // Kernel Text has an upper capacity so shouldn't be writing strings over that length repeatedly
                    weaponStrings[r] = new byte[64];

                    // Can iterate up to 4 times to handle the 4 stat bonuses available for weapons
                    // Currently, only 2 bonuses are being applied
                    // i must equal 0, 2, 4, 6 to represent the four stats, as the odd numbers are the VALUE of the stat bonus
                    while (i < 2)
                    {
                        if (languageOptions[0])
                        {
                            parameterString = WeaponArmourStrings.EnglishEquipmentParameterString(weaponAttributes, r, i * 2);
                        }
                        else if (languageOptions[1])
                        {
                            parameterString = WeaponArmourStrings.FrenchEquipmentParameterString(weaponAttributes, r, i * 2);
                        }
                        else if (languageOptions[2])
                        {
                            parameterString = WeaponArmourStrings.GermanEquipmentParameterString(weaponAttributes, r, i * 2);
                        }
                        else if (languageOptions[3])
                        {
                            parameterString = WeaponArmourStrings.SpanishEquipmentParameterString(weaponAttributes, r, i * 2);
                        }
                        else if (languageOptions[4])
                        {
                            parameterString = WeaponArmourStrings.JapaneseEquipmentParameterString(weaponAttributes, r, i * 2);
                        }

                        while (parameterString.Length > c)
                        {
                            weaponStrings[r][e] = parameterString[c]; e++; c++;
                        }
                        c = 0;

                        // Adds '+' to the string
                        weaponStrings[r][e] = 0x0B; e++;

                        if (weaponAttributes[r][1 + i * 2] < 0x0A)
                        {
                            // Print '0'
                            weaponStrings[r][e] = 0x10; e++;
                        }
                        else if (weaponAttributes[r][1 + i * 2] < 0x14)
                        {
                            // Print '1'
                            weaponStrings[r][e] = 0x11; e++;

                            // Adjust values so that 2nd digit can be printed cleanly
                            weaponAttributes[r][1 + i * 2] = (byte)(weaponAttributes[r][1 + i * 2] - 0x0A);
                        }
                        else if (weaponAttributes[r][1 + i * 2] < 0x1E)
                        {
                            // Print '2'
                            weaponStrings[r][e] = 0x12; e++;
                            weaponAttributes[r][1 + i * 2] = (byte)(weaponAttributes[r][1 + i * 2] - 0x14);
                        }
                        else if (weaponAttributes[r][1 + i * 2] < 0x28)
                        {
                            // Print '3'
                            weaponStrings[r][e] = 0x13; e++;
                            weaponAttributes[r][1 + i * 2] = (byte)(weaponAttributes[r][1 + i * 2] - 0x1E);
                        }
                        else if (weaponAttributes[r][1 + i * 2] < 0x32)
                        {
                            // Print '4'
                            weaponStrings[r][e] = 0x14; e++;
                            weaponAttributes[r][1 + i * 2] = (byte)(weaponAttributes[r][1 + i * 2] - 0x28);
                        }
                        else if (weaponAttributes[r][1 + i * 2] < 0x3C)
                        {
                            // Print '5'
                            weaponStrings[r][e] = 0x15; e++;
                            weaponAttributes[r][1 + (i * 2)] = (byte)(weaponAttributes[r][1 + i * 2] - 0x32);
                        }
                        else if (weaponAttributes[r][1 + (i * 2)] < 0x46)
                        {
                            // Print '6'
                            weaponStrings[r][e] = 0x16; e++;
                            weaponAttributes[r][1 + i * 2] = (byte)(weaponAttributes[r][1 + i * 2] - 0x3C);
                        }
                        else if (weaponAttributes[r][1 + i * 2] < 0x50)
                        {
                            // Print '7'
                            weaponStrings[r][e] = 0x17; e++;
                            weaponAttributes[r][1 + i * 2] = (byte)(weaponAttributes[r][1 + i * 2] - 0x46);
                        }
                        else if (weaponAttributes[r][1 + i * 2] < 0x5a)
                        {
                            // Print '8'
                            weaponStrings[r][e] = 0x18; e++;
                            weaponAttributes[r][1 + i * 2] = (byte)(weaponAttributes[r][1 + i * 2] - 0x50);
                        }
                        else if (weaponAttributes[r][1 + i * 2] < 0x64)
                        {
                            // Print '9'
                            weaponStrings[r][e] = 0x19; e++;
                            weaponAttributes[r][1 + i * 2] = (byte)(weaponAttributes[r][1 + i * 2] - 0x5A);
                        }
                        else
                        {
                            // Print '?'
                            weaponStrings[r][e] = 0x1F; e++;
                        }

                        // Print 2nd digit, with FF7 Unicode adjustment added
                        weaponStrings[r][e] = (byte)(weaponAttributes[r][1 + i * 2] + 0x10); e++;

                        // Adds ', ' to the string if first stat being added
                        if (i == 0)
                        {
                            weaponStrings[r][e] = 0x0C;
                            e++;
                            weaponStrings[r][e] = 0x00;
                            e++;
                        }
                        i++;
                    }

                    // Adds a status attack name to string if it has a value
                    if (weaponAttributes[r][11] != 0xFF)
                    {
                        // Adds ', ' to the string
                        weaponStrings[r][e] = 0x0C;
                        e++;
                        weaponStrings[r][e] = 0x00;
                        e++;

                        byte[] statusString = { };
                        if (languageOptions[0])
                        {
                            statusString = WeaponArmourStrings.EnglishWeaponArmourStatusString(weaponAttributes, r, 11);
                        }
                        else if (languageOptions[1])
                        {
                            statusString = WeaponArmourStrings.FrenchWeaponArmourStatusString(weaponAttributes, r, 11);
                        }
                        else if (languageOptions[2])
                        {
                            statusString = WeaponArmourStrings.GermanWeaponArmourStatusString(weaponAttributes, r, 11);
                        }
                        else if (languageOptions[3])
                        {
                            statusString = WeaponArmourStrings.SpanishWeaponArmourStatusString(weaponAttributes, r, 11);
                        }
                        else if (languageOptions[4])
                        {
                            statusString = WeaponArmourStrings.JapaneseWeaponArmourStatusString(weaponAttributes, r, 11);
                        }

                        while (statusString.Length > c)
                        {
                            weaponStrings[r][e] = statusString[c]; e++; c++;
                        }
                    }
                    // Adds crit% to string if it has a value
                    if (weaponAttributes[r][8] != 0x00)
                    {
                        // Adds ', ' to the string
                        weaponStrings[r][e] = 0x0C; e++;
                        weaponStrings[r][e] = 0x00; e++;

                        // Adds Crit+ to the string
                        if (languageOptions[0]) // English
                        {
                            weaponStrings[r][e] = 0x23; e++;
                            weaponStrings[r][e] = 0x52; e++;
                            weaponStrings[r][e] = 0x49; e++;
                            weaponStrings[r][e] = 0x54; e++;
                            weaponStrings[r][e] = 0x0B; e++;
                        }
                        else if (languageOptions[1]) // French
                        {
                            weaponStrings[r][e] = 0x23; e++;
                            weaponStrings[r][e] = 0x52; e++;
                            weaponStrings[r][e] = 0x49; e++;
                            weaponStrings[r][e] = 0x54; e++;
                            weaponStrings[r][e] = 0x0B; e++;
                        }
                        else if (languageOptions[2]) // German
                        {
                            weaponStrings[r][e] = 0x2B; e++;
                            weaponStrings[r][e] = 0x52; e++;
                            weaponStrings[r][e] = 0x49; e++;
                            weaponStrings[r][e] = 0x54; e++;
                            weaponStrings[r][e] = 0x0B; e++;
                        }
                        else if (languageOptions[3]) // Spanish
                        {
                            weaponStrings[r][e] = 0x23; e++;
                            weaponStrings[r][e] = 0x52; e++;
                            weaponStrings[r][e] = 0x49; e++;
                            weaponStrings[r][e] = 0x54; e++;
                            weaponStrings[r][e] = 0x0B; e++;
                        }
                        else if (languageOptions[4]) // Japanese
                        {
                            weaponStrings[r][e] = 0x23; e++;
                            weaponStrings[r][e] = 0x52; e++;
                            weaponStrings[r][e] = 0x49; e++;
                            weaponStrings[r][e] = 0x54; e++;
                            weaponStrings[r][e] = 0x0B; e++;
                        }
                        else // Default to English
                        {
                            weaponStrings[r][e] = 0x23; e++;
                            weaponStrings[r][e] = 0x52; e++;
                            weaponStrings[r][e] = 0x49; e++;
                            weaponStrings[r][e] = 0x54; e++;
                            weaponStrings[r][e] = 0x0B; e++;
                        }

                        if (weaponAttributes[r][8] < 0x0A)
                        {
                            // Print '0'
                            weaponStrings[r][e] = 0x10; e++;
                        }
                        else if (weaponAttributes[r][8] < 0x14)
                        {
                            // Print '1'
                            weaponStrings[r][e] = 0x11; e++;

                            // Adjust values so that 2nd digit can be printed cleanly
                            weaponAttributes[r][8] = (byte)(weaponAttributes[r][8] - 0x0A);
                        }
                        else if (weaponAttributes[r][8] < 0x1E)
                        {
                            // Print '2'
                            weaponStrings[r][e] = 0x12; e++;
                            weaponAttributes[r][8] = (byte)(weaponAttributes[r][8] - 0x14);
                        }
                        else if (weaponAttributes[r][8] < 0x28)
                        {
                            // Print '3'
                            weaponStrings[r][e] = 0x13; e++;
                            weaponAttributes[r][8] = (byte)(weaponAttributes[r][8] - 0x1E);
                        }
                        else if (weaponAttributes[r][8] < 0x32)
                        {
                            // Print '4'
                            weaponStrings[r][e] = 0x14; e++;
                            weaponAttributes[r][8] = (byte)(weaponAttributes[r][8] - 0x28);
                        }
                        else if (weaponAttributes[r][8] < 0x3C)
                        {
                            // Print '5'
                            weaponStrings[r][e] = 0x15; e++;
                            weaponAttributes[r][8] = (byte)(weaponAttributes[r][8] - 0x32);
                        }
                        else if (weaponAttributes[r][8] < 0x46)
                        {
                            // Print '6'
                            weaponStrings[r][e] = 0x16; e++;
                            weaponAttributes[r][8] = (byte)(weaponAttributes[r][8] - 0x3C);
                        }
                        else if (weaponAttributes[r][8] < 0x50)
                        {
                            // Print '7'
                            weaponStrings[r][e] = 0x17; e++;
                            weaponAttributes[r][8] = (byte)(weaponAttributes[r][8] - 0x46);
                        }
                        else if (weaponAttributes[r][8] < 0x5a)
                        {
                            // Print '8'
                            weaponStrings[r][e] = 0x18; e++;
                            weaponAttributes[r][8] = (byte)(weaponAttributes[r][8] - 0x50);
                        }
                        else if (weaponAttributes[r][8] < 0x64)
                        {
                            // Print '9'
                            weaponStrings[r][e] = 0x19; e++;
                            weaponAttributes[r][8] = (byte)(weaponAttributes[r][8] - 0x5A);
                        }
                        else
                        {
                            // Print '?'
                            weaponStrings[r][e] = 0x1F; e++;
                        }

                        // Print 2nd digit, with FF7 Unicode adjustment added
                        weaponStrings[r][e] = (byte)(weaponAttributes[r][8] + 0x10); e++;

                        // Adds '%' to the string
                        weaponStrings[r][e] = 0x05; e++;
                    }

                    weaponStrings[r][e] = 0xFF; e++;
                    stringSizes[r] = e;
                    i = 0;
                    c = 0;
                    e = 0;
                    r++;
                }
                r = 0;

                using (var outputStream = File.Create(weaponStringsFile))
                {
                    outputStream.Position = 0;

                    // Loops until all string headers are written
                    while (r < 128)
                    {
                        if (r != 0)
                        {
                            stringSize += (ulong)stringSizes[r - 1];
                        }
                        else
                        {
                            // First header always points to after the header block
                            stringSize = 256;
                        }

                        var stringHead = EndianConvert.GetLittleEndianConvert(stringSize);

                        // Takes the header data, converts it into a stream, and then appends it to the file-in-progress
                        outputStream.Position = outputStream.Length;
                        outputStream.Write(stringHead, 0, 2);
                        r++;
                    }
                    r = 0;
                    while (r < 128)
                    {
                        // Writes in the new weapon Description strings
                        outputStream.Position = outputStream.Length;
                        outputStream.Write(weaponStrings[r], 0, stringSizes[r]);
                        r++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Weapon Rewrite has encountered an issue");
            }
        }

        public static void ArmourDescriptionRewrite(byte[][] armourAttributes, bool[] languageOptions)
        {
            int r = 0; // Current armour ID being written for
            int i = 0; // Dealing with 2-digit values in stat bonuses
            int c = 0; // Used for writing the status string
            int e = 0; // Counts byte position of the string being written
            string armourStringsFile = Directory.GetCurrentDirectory() + "\\Kernel Strings\\kernel2Modified.bin13";

            //Each armour string is stored as a separate array so it can have its length counted quickly
            byte[][] armourStrings = new byte[32][];

            // Starts with all the 2byte headers for each armour; adds on string values later for total size

            // Tracks the actual length used within each 32-byte string (using FF terminator)
            int[] stringSizes = new int[32];

            // Tracks the size of the string to create a header offset for it
            ulong stringSize = 0;

            try
            {
                while (r < 32)
                {
                    armourStrings[r] = new byte[64];
                    while (i < 2)
                    {
                        switch (armourAttributes[r][0 + i * 2])
                        {
                            // STR
                            case 0:
                                armourStrings[r][e] = 0x33; e++;
                                armourStrings[r][e] = 0x34; e++;
                                armourStrings[r][e] = 0x32; e++;
                                break;

                            // VIT
                            case 1:
                                armourStrings[r][e] = 0x36; e++;
                                armourStrings[r][e] = 0x29; e++;
                                armourStrings[r][e] = 0x34; e++;
                                break;

                            // MAG
                            case 2:
                                armourStrings[r][e] = 0x2D; e++;
                                armourStrings[r][e] = 0x21; e++;
                                armourStrings[r][e] = 0x27; e++;
                                break;

                            // SPR
                            case 3:
                                armourStrings[r][e] = 0x33; e++;
                                armourStrings[r][e] = 0x30; e++;
                                armourStrings[r][e] = 0x32; e++;
                                break;

                            // DEX
                            case 4:
                                armourStrings[r][e] = 0x24; e++;
                                armourStrings[r][e] = 0x25; e++;
                                armourStrings[r][e] = 0x38; e++;
                                break;

                            // LCK
                            case 5:
                                armourStrings[r][e] = 0x2C; e++;
                                armourStrings[r][e] = 0x23; e++;
                                armourStrings[r][e] = 0x2B; e++;
                                break;

                            // ???, if this is printed in-game then something's wrong
                            default:
                                armourStrings[r][e] = 0x1F; e++;
                                armourStrings[r][e] = 0x1F; e++;
                                armourStrings[r][e] = 0x1F; e++;
                                break;
                        }

                        // Adds '+' to the string
                        armourStrings[r][e] = 0x0B; e++;

                        if (armourAttributes[r][1 + i * 2] < 0x0A)
                        {
                            // Print '0'
                            armourStrings[r][e] = 0x10; e++;
                        }
                        else if (armourAttributes[r][1 + i * 2] < 0x14)
                        {
                            // Print '1'
                            armourStrings[r][e] = 0x11; e++;

                            // Adjust values so that 2nd digit can be printed cleanly
                            armourAttributes[r][1 + i * 2] = (byte)(armourAttributes[r][1 + i * 2] - 0x0A);
                        }
                        else if (armourAttributes[r][1 + i * 2] < 0x1E)
                        {
                            // Print '2'
                            armourStrings[r][e] = 0x12; e++;
                            armourAttributes[r][1 + i * 2] = (byte)(armourAttributes[r][1 + i * 2] - 0x14);
                        }
                        else if (armourAttributes[r][1 + i * 2] < 0x28)
                        {
                            // Print '3'
                            armourStrings[r][e] = 0x13; e++;
                            armourAttributes[r][1 + i * 2] = (byte)(armourAttributes[r][1 + i * 2] - 0x1E);
                        }
                        else if (armourAttributes[r][1 + i * 2] < 0x32)
                        {
                            // Print '4'
                            armourStrings[r][e] = 0x14; e++;
                            armourAttributes[r][1 + i * 2] = (byte)(armourAttributes[r][1 + i * 2] - 0x28);
                        }
                        else if (armourAttributes[r][1 + i * 2] < 0x3C)
                        {
                            // Print '5'
                            armourStrings[r][e] = 0x15; e++;
                            armourAttributes[r][1 + (i * 2)] = (byte)(armourAttributes[r][1 + i * 2] - 0x32);
                        }
                        else if (armourAttributes[r][1 + (i * 2)] < 0x46)
                        {
                            // Print '6'
                            armourStrings[r][e] = 0x16; e++;
                            armourAttributes[r][1 + i * 2] = (byte)(armourAttributes[r][1 + i * 2] - 0x3C);
                        }
                        else if (armourAttributes[r][1 + i * 2] < 0x50)
                        {
                            // Print '7'
                            armourStrings[r][e] = 0x17; e++;
                            armourAttributes[r][1 + i * 2] = (byte)(armourAttributes[r][1 + i * 2] - 0x46);
                        }
                        else if (armourAttributes[r][1 + i * 2] < 0x5a)
                        {
                            // Print '8'
                            armourStrings[r][e] = 0x18; e++;
                            armourAttributes[r][1 + i * 2] = (byte)(armourAttributes[r][1 + i * 2] - 0x50);
                        }
                        else if (armourAttributes[r][1 + i * 2] < 0x64)
                        {
                            // Print '9'
                            armourStrings[r][e] = 0x19; e++;
                            armourAttributes[r][1 + i * 2] = (byte)(armourAttributes[r][1 + i * 2] - 0x5A);
                        }
                        else
                        {
                            // Print '?'
                            armourStrings[r][e] = 0x1F; e++;
                        }

                        // Print 2nd digit, with FF7 Unicode adjustment added
                        armourStrings[r][e] = (byte)(armourAttributes[r][1 + i * 2] + 0x10); e++;

                        // Adds ', ' to the string
                        armourStrings[r][e] = 0x0C; e++;
                        armourStrings[r][e] = 0x00; e++;
                        i++;
                    }

                    // Adds element name to string if it has a value
                    if (armourAttributes[r][8] != 0x00 || armourAttributes[r][9] != 0x00)
                    {
                        // https://i.imgur.com/XhEFRsb.jpg
                        byte[] elementString = { };
                        byte[] elementTypeString = { };
                        if (languageOptions[0])
                        {
                            elementString = ElementStrings.EnglishElementString(armourAttributes, r, 8, 9);
                            while (elementString.Length > c)
                            {
                                armourStrings[r][e] = elementString[c]; e++; c++;
                            }
                            // Handles the type of elemental defence: Halve, Null, Absorb
                            elementTypeString = ElementStrings.EnglishElementTypeString(armourAttributes, r, 10);
                        }
                        else if (languageOptions[1])
                        {
                            elementString = ElementStrings.FrenchElementString(armourAttributes, r, 8, 9);
                            while (elementString.Length > c)
                            {
                                armourStrings[r][e] = elementString[c]; e++; c++;
                            }
                            elementTypeString = ElementStrings.FrenchElementTypeString(armourAttributes, r, 10);
                        }
                        else if (languageOptions[2])
                        {
                            elementString = ElementStrings.GermanElementString(armourAttributes, r, 8, 9);
                            while (elementString.Length > c)
                            {
                                armourStrings[r][e] = elementString[c]; e++; c++;
                            }
                            elementTypeString = ElementStrings.GermanElementTypeString(armourAttributes, r, 10);
                        }
                        else if (languageOptions[3])
                        {
                            elementString = ElementStrings.SpanishElementString(armourAttributes, r, 8, 9);
                            while (elementString.Length > c)
                            {
                                armourStrings[r][e] = elementString[c]; e++; c++;
                            }
                            elementTypeString = ElementStrings.SpanishElementTypeString(armourAttributes, r, 10);
                        }
                        c = 0;
                        while (elementTypeString.Length > c)
                        {
                            armourStrings[r][e] = elementTypeString[c]; e++; c++;
                        }
                    }
                    armourStrings[r][e] = 0xFF; e++;
                    stringSizes[r] = e;
                    i = 0;
                    c = 0;
                    e = 0;
                    r++;
                }
                r = 0;

                using (var outputStream = File.Create(armourStringsFile))
                {
                    outputStream.Position = 0;

                    // Loops until all string headers are written
                    while (r < 32)
                    {
                        if (r != 0)
                        {
                            stringSize += (ulong)stringSizes[r - 1];
                        }
                        else
                        {
                            // First header always points to after the header block
                            stringSize = 64;
                        }

                        var stringHead = EndianConvert.GetLittleEndianConvert(stringSize);

                        // Takes the header data, converts it into a stream, and then appends it to the file-in-progress
                        outputStream.Position = outputStream.Length;
                        outputStream.Write(stringHead, 0, 2);
                        r++;
                    }
                    r = 0;
                    while (r < 32)
                    {
                        // Writes in the new Armour Description strings
                        outputStream.Position = outputStream.Length;
                        outputStream.Write(armourStrings[r], 0, stringSizes[r]);
                        r++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Armour Rewrite has encountered an issue");
            }
        }

        public static void AccessoryDescriptionRewrite(byte[][] accessoryAttributes, bool[] languageOptions)
        {
            int r = 0; // Current accessory ID being written for
            int i = 0; // Dealing with 2-digit values in stat bonuses
            int c = 0; // Used for writing the status string
            int e = 0; // Counts byte position of the string being written
            string accessoryStringsFile = Directory.GetCurrentDirectory() + "\\Kernel Strings\\kernel2Modified.bin14";

            //Each accessory string is stored as a separate array so it can have its length counted quickly
            byte[][] accessoryStrings = new byte[32][];

            // Starts with all the 2byte headers for each accessory; adds on string values later for total size

            // Tracks the actual length used within each string (using FF terminator)
            int[] stringSizes = new int[32];

            // Tracks the size of the string to create a header offset for it
            ulong stringSize = 0;

            try
            {
                while (r < 32)
                {
                    accessoryStrings[r] = new byte[64];
                    // Can iterate up to 4 times to handle the 4 stat bonuses available for accessorys
                    // Currently, only 2 bonuses are applied
                    while (i < 2)
                    { // + (i * size)
                        switch (accessoryAttributes[r][0 + i * 2])
                        {
                            // STR
                            case 0:
                                accessoryStrings[r][e] = 0x33; e++;
                                accessoryStrings[r][e] = 0x34; e++;
                                accessoryStrings[r][e] = 0x32; e++;
                                break;

                            // VIT
                            case 1:
                                accessoryStrings[r][e] = 0x36; e++;
                                accessoryStrings[r][e] = 0x29; e++;
                                accessoryStrings[r][e] = 0x34; e++;
                                break;

                            // MAG
                            case 2:
                                accessoryStrings[r][e] = 0x2D; e++;
                                accessoryStrings[r][e] = 0x21; e++;
                                accessoryStrings[r][e] = 0x27; e++;
                                break;

                            // SPR
                            case 3:
                                accessoryStrings[r][e] = 0x33; e++;
                                accessoryStrings[r][e] = 0x30; e++;
                                accessoryStrings[r][e] = 0x32; e++;
                                break;

                            // DEX
                            case 4:
                                accessoryStrings[r][e] = 0x24; e++;
                                accessoryStrings[r][e] = 0x25; e++;
                                accessoryStrings[r][e] = 0x38; e++;
                                break;

                            // LCK
                            case 5:
                                accessoryStrings[r][e] = 0x2C; e++;
                                accessoryStrings[r][e] = 0x23; e++;
                                accessoryStrings[r][e] = 0x2B; e++;
                                break;

                            // ???, if this is printed in-game then something's wrong
                            default:
                                accessoryStrings[r][e] = 0x1F; e++;
                                accessoryStrings[r][e] = 0x1F; e++;
                                accessoryStrings[r][e] = 0x1F; e++;
                                break;
                        }

                        // Adds '+' to the string
                        accessoryStrings[r][e] = 0x0B; e++;

                        if (accessoryAttributes[r][1 + i * 2] < 0x0A)
                        {
                            // Print '0'
                            accessoryStrings[r][e] = 0x10; e++;
                        }
                        else if (accessoryAttributes[r][1 + i * 2] < 0x14)
                        {
                            // Print '1'
                            accessoryStrings[r][e] = 0x11; e++;

                            // Adjust values so that 2nd digit can be printed cleanly
                            accessoryAttributes[r][1 + i * 2] = (byte)(accessoryAttributes[r][1 + i * 2] - 0x0A);
                        }
                        else if (accessoryAttributes[r][1 + i * 2] < 0x1E)
                        {
                            // Print '2'
                            accessoryStrings[r][e] = 0x12; e++;
                            accessoryAttributes[r][1 + i * 2] = (byte)(accessoryAttributes[r][1 + i * 2] - 0x14);
                        }
                        else if (accessoryAttributes[r][1 + i * 2] < 0x28)
                        {
                            // Print '3'
                            accessoryStrings[r][e] = 0x13; e++;
                            accessoryAttributes[r][1 + i * 2] = (byte)(accessoryAttributes[r][1 + i * 2] - 0x1E);
                        }
                        else if (accessoryAttributes[r][1 + i * 2] < 0x32)
                        {
                            // Print '4'
                            accessoryStrings[r][e] = 0x14; e++;
                            accessoryAttributes[r][1 + i * 2] = (byte)(accessoryAttributes[r][1 + i * 2] - 0x28);
                        }
                        else if (accessoryAttributes[r][1 + i * 2] < 0x3C)
                        {
                            // Print '5'
                            accessoryStrings[r][e] = 0x15; e++;
                            accessoryAttributes[r][1 + (i * 2)] = (byte)(accessoryAttributes[r][1 + i * 2] - 0x32);
                        }
                        else if (accessoryAttributes[r][1 + (i * 2)] < 0x46)
                        {
                            // Print '6'
                            accessoryStrings[r][e] = 0x16; e++;
                            accessoryAttributes[r][1 + i * 2] = (byte)(accessoryAttributes[r][1 + i * 2] - 0x3C);
                        }
                        else if (accessoryAttributes[r][1 + i * 2] < 0x50)
                        {
                            // Print '7'
                            accessoryStrings[r][e] = 0x17; e++;
                            accessoryAttributes[r][1 + i * 2] = (byte)(accessoryAttributes[r][1 + i * 2] - 0x46);
                        }
                        else if (accessoryAttributes[r][1 + i * 2] < 0x5a)
                        {
                            // Print '8'
                            accessoryStrings[r][e] = 0x18; e++;
                            accessoryAttributes[r][1 + i * 2] = (byte)(accessoryAttributes[r][1 + i * 2] - 0x50);
                        }
                        else if (accessoryAttributes[r][1 + i * 2] < 0x64)
                        {
                            // Print '9'
                            accessoryStrings[r][e] = 0x19; e++;
                            accessoryAttributes[r][1 + i * 2] = (byte)(accessoryAttributes[r][1 + i * 2] - 0x5A);
                        }
                        else
                        {
                            // Print '?'
                            accessoryStrings[r][e] = 0x1F; e++;
                        }

                        // Print 2nd digit, with FF7 Unicode adjustment added
                        accessoryStrings[r][e] = (byte)(accessoryAttributes[r][1 + i * 2] + 0x10); e++;

                        // Adds ', ' to the string
                        accessoryStrings[r][e] = 0x0C; e++;
                        accessoryStrings[r][e] = 0x00; e++;
                        i++;
                    }

                    // Adds element name to string if it has a value
                    if (accessoryAttributes[r][6] != 0x00 || accessoryAttributes[r][7] != 0x00)
                    {
                        byte[] elementString = { };
                        byte[] elementTypeString = { };
                        if (languageOptions[0])
                        {
                            elementString = ElementStrings.EnglishElementString(accessoryAttributes, r, 6, 7);
                            while (elementString.Length > c)
                            {
                                accessoryStrings[r][e] = elementString[c]; e++; c++;
                            }
                            // Handles the type of elemental defence: Halve, Null, Absorb
                            elementTypeString = ElementStrings.EnglishElementTypeString(accessoryAttributes, r, 4);
                        }
                        else if (languageOptions[1])
                        {
                            elementString = ElementStrings.FrenchElementString(accessoryAttributes, r, 6, 7);
                            while (elementString.Length > c)
                            {
                                accessoryStrings[r][e] = elementString[c]; e++; c++;
                            }
                            elementTypeString = ElementStrings.FrenchElementTypeString(accessoryAttributes, r, 4);
                        }
                        else if (languageOptions[2])
                        {
                            elementString = ElementStrings.GermanElementString(accessoryAttributes, r, 6, 7);
                            while (elementString.Length > c)
                            {
                                accessoryStrings[r][e] = elementString[c]; e++; c++;
                            }
                            elementTypeString = ElementStrings.GermanElementTypeString(accessoryAttributes, r, 4);
                        }
                        else if (languageOptions[3])
                        {
                            elementString = ElementStrings.SpanishElementString(accessoryAttributes, r, 6, 7);
                            while (elementString.Length > c)
                            {
                                accessoryStrings[r][e] = elementString[c]; e++; c++;
                            }
                            elementTypeString = ElementStrings.SpanishElementTypeString(accessoryAttributes, r, 4);
                        }
                        c = 0;
                        while (elementTypeString.Length > c)
                        {
                            accessoryStrings[r][e] = elementTypeString[c]; e++; c++;
                        }
                    }

                    // Adds a status defence name to string if it has a value
                    // 4 byte value, so check which have a value (will need to alter if you combine statuses later)
                    int att = 0;
                    if (accessoryAttributes[r][8] != 0x00)
                    {
                        att = 8;
                    }
                    else if (accessoryAttributes[r][9] != 0x00)
                    {
                        att = 9;
                    }
                    else if (accessoryAttributes[r][10] != 0x00)
                    {
                        att = 10;
                    }
                    else if (accessoryAttributes[r][11] != 0x00)
                    {
                        att = 11;
                    }

                    if (att != 0)
                    {
                        // Check if an element attribute was written and append a comma + space
                        if (accessoryAttributes[r][6] != 0x00 || accessoryAttributes[r][7] != 0x00)
                        {
                            // Adds ', ' to the string
                            accessoryStrings[r][e] = 0x0C; e++;
                            accessoryStrings[r][e] = 0x00; e++;
                        }

                        byte[] statusString = { };
                        if (languageOptions[0])
                        {
                            statusString = AccessoryStrings.EnglishAccessoryStatusString(accessoryAttributes, r, att);
                        }
                        else if (languageOptions[1])
                        {
                            statusString = AccessoryStrings.FrenchAccessoryStatusString(accessoryAttributes, r, att);
                        }
                        else if (languageOptions[2])
                        {
                            statusString = AccessoryStrings.GermanAccessoryStatusString(accessoryAttributes, r, att);
                        }
                        else if (languageOptions[3])
                        {
                            statusString = AccessoryStrings.SpanishAccessoryStatusString(accessoryAttributes, r, att);
                        }

                        c = 0;
                        while (statusString.Length > c)
                        {
                            accessoryStrings[r][e] = statusString[c]; e++; c++;
                        }
                    }
                    accessoryStrings[r][e] = 0xFF; e++;
                    stringSizes[r] = e;
                    i = 0;
                    c = 0;
                    e = 0;
                    r++;
                }
                r = 0;

                using (var outputStream = File.Create(accessoryStringsFile))
                {
                    outputStream.Position = 0;

                    // Loops until all string headers are written
                    while (r < 32)
                    {
                        if (r != 0)
                        {
                            stringSize += (ulong)stringSizes[r - 1];
                        }
                        else
                        {
                            // First header always points to after the header block
                            stringSize = 64;
                        }

                        var stringHead = EndianConvert.GetLittleEndianConvert(stringSize);

                        // Takes the header data, converts it into a stream, and then appends it to the file-in-progress
                        outputStream.Position = outputStream.Length;
                        outputStream.Write(stringHead, 0, 2);
                        r++;
                    }
                    r = 0;
                    while (r < 32)
                    {
                        // Writes in the new accessory Description strings
                        outputStream.Position = outputStream.Length;
                        outputStream.Write(accessoryStrings[r], 0, stringSizes[r]);
                        r++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Accessory Rewrite has encountered an issue");
            }
        }
    }
}
