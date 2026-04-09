using Godo.Helper;
using Godo.Indexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Godo.Omnichange;

namespace Godo.Infrastructure.Kernel.InitialisationData
{
    public class Initialisation
    {
        // Section 3: Character Record & Savemap Initialisation
        #region Section Information
        // Start Location (Uncompressed): 0x004A4
        // Cloud Record: 0x00000
        // Barret Record: 0x00084
        // Tifa Record: 0x00108
        // Aeristh Record: 0x0018C
        // Red XIII Record: 0x00210
        // Yuffie Record: 0x00294
        // Young Cloud Record: 0x00318
        // Sephiroth Record: 0x0039C
        // Cid Record: 0x00420
        #endregion

        public static byte[] RandomiseInitialisation(byte[] data,
            bool[] statOptions, int[] statParameters, bool[] characterSelectStats,
            bool[] equipOptions, int[] equipParameters, bool[] characterSelectEquip,
            bool[] languageOptions, byte[]startingEquipment,
            Random rnd, bool characterStatChange, bool startingMateriaChange)
        {
            try
            {
                #region Initialisation Data (kernel.3) struct
                //int[] charRecord = new int[132];    // Character Data is 132bytes in length
                //int[] miscRecord = new int[4];      // Handles 4 misc bytes between character record and item record
                //int[] itemRecord = new int[640];    // Item Data
                //int[] materiaRecord = new int[800]; // Materia Data
                //int[] stolenRecord = new int[192];  // Stolen Materia Data

                int r = 0; // For counting characters
                int o = 0; // For counting bytes in the character record

                byte[] characterBalancer = new byte[8];
                string stat = "";

                #region Character Records (132bytes, 9 times)
                while (r < 9) // Iterates 9 times for each character; 0 = Cloud, 8 = Cid
                {
                    #region Accessory Balancer Values
                    // Initialises the jagged array for this cycle
                    // 0: Strength
                    // 1: Vitality
                    // 2: Magic
                    // 3: Spirit
                    // 4: Dexterity
                    // 5: Luck
                    // 6: HP
                    // 7: MP
                    #endregion
                    characterBalancer = CharacterChange.AssignCharacterBalancing(rnd);

                    // Character ID
                    o++;

                    // Character Stats
                    // Level
                    o++;

                    // Strength
                    // Currently dealt with through Stat Growth Curves (so all starting stats set to 0)
                    // In future, a variant could be added to set them here with no growth
                    if (characterStatChange == true)
                    {
                        stat = "Strength";
                        data[o] = CharacterChange.AdjustCharacterStats(stat, characterBalancer[0], rnd); o++;

                        // Vitality
                        stat = "Vitality";
                        data[o] = CharacterChange.AdjustCharacterStats(stat, characterBalancer[1], rnd); o++;

                        // Magic
                        stat = "Magic";
                        data[o] = CharacterChange.AdjustCharacterStats(stat, characterBalancer[2], rnd); o++;

                        // Spirit
                        stat = "Spirit";
                        data[o] = CharacterChange.AdjustCharacterStats(stat, characterBalancer[3], rnd); o++;

                        // Dexterity
                        stat = "Dexterity";
                        data[o] = CharacterChange.AdjustCharacterStats(stat, characterBalancer[4], rnd); o++;

                        // Luck
                        stat = "Luck";
                        data[o] = CharacterChange.AdjustCharacterStats(stat, characterBalancer[5], rnd); o++;
                    }
                    else
                    {
                        o += 6;
                    }


                    // Sources used - There are 6 values (1byte) for each Source type, redundant to change so skipped.
                    // Source-boosted Dex behaves differently to natural Dex, however.

                    //Strength (Sources)
                    o++;

                    //Vitality (Sources)
                    o++;

                    //Magic (Sources)
                    o++;

                    //Spirit (Sources)
                    o++;

                    //Dexterity (Sources)
                    o++;

                    //Luck (Sources)
                    o++;


                    // Current Limit Level
                    o++;

                    // Current Limit Gauge
                    o++;

                    // Character Name
                    // This is an internal name that is overriden when the Character Naming screen is called in-game
                    // Can likely only see the starting internal name in-game by bypassing scripts with glitches, etc.
                    o += 12;

                    // Equipped Weapon ID
                    // Characters can equip each other's weapons, as the model used is based on a shared ID
                    // If Barret 'equips' Buster Sword, for instance, it will take on the form of Gatling Gun
                    // as Buster Sword has Model ID 0. Characters with fewer weapons have their base weapon as
                    // placeholders in the files so it's safe to assign, say, Ultima Weapon to Aeris.
                    
                    #region Switch-Case for Weapon Ranges
                    if (r == 0)
                    {
                        // Cloud - 0-14
                        //data[o] = (byte)rnd.Next(0, 3);
                    }
                    if (r == 1)
                    {
                        // Barret - 32, 46
                        //data[o] = (byte)rnd.Next(32, 36);
                    }
                    if (r == 2)
                    {
                        // Tifa - 16, 30
                        //data[o] = (byte)rnd.Next(16, 20);
                    }
                    if (r == 3)
                    {
                        // Aeristh - 62, 71
                        //data[o] = (byte)rnd.Next(62, 66);
                    }
                    if (r == 4)
                    {
                        // Red XIII - 48, 60
                        //data[o] = (byte)rnd.Next(48, 51);
                    }
                    if (r == 5)
                    {
                        // Yuffers - 87, 99
                        //data[o] = (byte)rnd.Next(87, 90);
                    }
                    if (r == 6)
                    {
                        // Young Cloud
                        //data[o] = (byte)rnd.Next(0, 14);
                    }
                    if (r == 7)
                    {
                        // Sephiroth
                        //data[o] = 0;
                    }
                    if (r == 8)
                    {
                        // Cid - 73, 85
                        //data[o] = (byte)rnd.Next(73, 77);
                    }
                    startingEquipment[r] = data[o];
                    o++;
                    #endregion

                    // Equipped Armour ID
                    startingEquipment[r + 9] = data[o];
                    o++;

                    // Equipped Accessory ID
                    o++;

                    // Status Flag - 0 by default. Sets Fury or Sadness.
                    o++;

                    // Row Flag - Front/Back Row, default value is FF
                    o++;

                    // LvlProgressBar - Visual only, separate from actual EXP values (updates on results screen)
                    o++;

                    // Learned Limit Skills
                    // There are actually 3 Limits per level rather than 2; 1-3, 2-3, 3-3, and 4-2/4-3 are unused
                    // A character can only learn 5-6 Limits and will stop learning any more after that even if conditions are met,
                    // so if you set 1-3, 2-3, etc. then you can lock yourself out from learning the 4-1.
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++; // 2nd byte

                    // Number of Kills
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++; // 2nd byte

                    // Times Limit 1-1 used
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++; // 2nd byte

                    // Times Limit 2-1 used
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++; // 2nd byte

                    // Times Limit 3-1 used
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++; // 2nd byte

                    // Current HP - 100 is the game's functional minimum
                    stat = "HP";
                    ulong convertedHP = CharacterChange.AdjustCharacterHPMP(stat, characterBalancer[6], rnd);
                    byte[] convertedHPByte = EndianConvert.GetLittleEndianConvert(convertedHP);
                    data[o] = convertedHPByte[0]; o++;
                    data[o] = convertedHPByte[1]; o++;

                    // Base HP - Character's 'real' HP
                    data[o] = data[o - 2]; o++;
                    data[o] = data[o - 2]; o++;

                    // Current MP - 10 is game's functional minimum.
                    stat = "MP";
                    ulong convertedMP = CharacterChange.AdjustCharacterHPMP(stat, characterBalancer[7], rnd);
                    byte[] convertedMPByte = EndianConvert.GetLittleEndianConvert(convertedMP);
                    data[o] = convertedMPByte[0]; o++;
                    data[o] = convertedMPByte[1]; o++;

                    // Base MP - Starting MP on New Game - Setting this to same as Current MP
                    data[o] = data[o - 2]; o++;
                    data[o] = data[o - 2]; o++;

                    // Unknown, 4 bytes in length - Defaults are 0s
                    o += 4;

                    // Max HP
                    data[o] = data[o - 10]; o++;
                    data[o] = data[o - 10]; o++;

                    // Max MP
                    data[o] = data[o - 8]; o++;
                    data[o] = data[o - 8]; o++;

                    // Current EXP
                    // Try to have it be within the right place of the Level curve to avoid visual bugs with
                    // the EXP gauge (though this should resolve after levelling up once
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    #region Equipment Materia Slots
                    // Weapon Materia Slots
                    // Contains the ID + AP of the Materia; 4x 255 = No Materia in the Slot

                    // Skip Sephiroth's Materia in ID 7
                    if (r != 7 && startingMateriaChange == true)
                    {
                        // Weapon
                        int picker = MateriaIndex.SelectCuratedMateria(rnd);
                        // ID of the Materia
                        // If Cloud, force Sense Materia as first Materia
                        data[o] = r == 0 ? data[o] = 0x25 : (byte)picker; o++;

                        data[o] = (byte)rnd.Next(256); o++; // AP of the Materia, 3-bytes
                        data[o] = (byte)rnd.Next(11); o++;
                        data[o] = (byte)rnd.Next(0); o++;

                        picker = MateriaIndex.SelectCuratedMateria(rnd);
                        data[o] = (byte)picker; o++;
                        data[o] = (byte)rnd.Next(256); o++;
                        data[o] = (byte)rnd.Next(11); o++;
                        data[o] = (byte)rnd.Next(0); o++;

                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;

                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;

                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;

                        // Armour
                        picker = MateriaIndex.SelectCuratedMateria(rnd);
                        data[o] = (byte)picker; o++;
                        data[o] = (byte)rnd.Next(256); o++;
                        data[o] = (byte)rnd.Next(11); o++;
                        data[o] = (byte)rnd.Next(0); o++;
                        
                        picker = MateriaIndex.SelectCuratedMateria(rnd);
                        data[o] = (byte)picker; o++;
                        data[o] = (byte)rnd.Next(256); o++;
                        data[o] = (byte)rnd.Next(11); o++;
                        data[o] = (byte)rnd.Next(0); o++;
                        
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                    }
                    else
                    {
                        o += 64;
                    }
                    #endregion

                    // EXP to Next Level - If not correct then only causes temporary visual glitch with the gauge. Will be very difficult to synch
                    // as each character requires different amount of EXP, and the current EXP/Level will vary. May stick with default level.
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    r++;
                }
                #endregion

                #region Misc Bytes (4bytes)
                // Miscellaneous 4-bytes

                // Starting Party composition (Overriden by field script at start of the game)
                o += 3;

                // Padding of 1 byte, default value is FF
                o++;
                #endregion

                #region Item, Materia, and Stolen Materia (1632 bytes)
                // Starting Item stock
                // This array is massive, 2*320 bytes to handle Item ID + Quantity with no absolute position within inventory

                // Adjust to fill empty space in item inventory to FF, 640 is entire item inventory is empty
                //c = 640;
                //while (c != 0)
                //{
                //    data[o] = 255; o++; // Unused values must be FF
                //    c--;
                //}

                // Starting Materia stock
                // Even larger at 4*200 bytes, same deal as items

                // Adjust to fill empty space in Materia inventory to FF, 800 is entire Materia inventory is empty
                //c = 800;
                //while (c != 0)
                //{
                //    materiaRecord[o] = 255; o++;
                //    c--;
                //}

                // Stolen Materia stock - 4*48 for 192 bytes.
                // Storage for the Materia that Yuffie steals during the Wutai segment

                // Adjust to fill empty space in Materia inventory to FF, 192 sets entire Stolen Materia to blank
                //c = 192;
                //while (c != 0)
                //{
                //    // There is a mysterious set of values in this array by default; Materia at offset 0xB28(F0 00 00 00)
                //    // F0 isn't a valid Materia ID, as valid index IDs go up to around 95 decimal or so.
                //    // Avoid editing these 4 values; perhaps they have a different function
                //    stolenRecord[o] = 255; o++;
                //    c--;
                //}
                #endregion
                
                #endregion
            }
            catch
            {
                MessageBox.Show("Kernel Section #3 (Initial Data) has failed to randomise");
            }
            return data;
        }
    }
}