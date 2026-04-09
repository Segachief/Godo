using Godo.Indexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Godo.Omnichange;
// ReSharper disable InconsistentNaming

namespace Godo.Infrastructure.Kernel.InitialisationData
{
    public class GrowthAndLookUpTable
    {
        // Section 2: Battle & Growth Data + Kernel Lookup Table
        public static byte[] RandomiseSection2(byte[] data,
            bool[] statOptions, bool[] characterSelectStats,
            bool[] limitOptions, bool[] characterSelectLimits,
            bool[] challengeOptions,
            bool[] languageOptions,
            bool[] rngOption,
            Random rnd, byte[] kernelLookup,
            bool characterStatChange)
        {
            #region Section Info
            /* Contains the following:
            * 1) General Data for 9 Characters (59 * 9)
            * Curve ID for STR > VIT > MAG > SPR > DEX > LCK > HP > MP > EXP (9)
            * Padding (1)
            * Level Modifier (1)
            * Padding (1)
            * Limit ID 1-1 > 4-3 (1 * 12; there are 3 limit slots for each Limit Level)
            * Required Limit Uses (2 * 6, 12)
            * Gauge Resistance (4 * 4, 16)
            * 
            * 2) Curve Data
            * Random Bonus to Primary Stats (1 * 12)
            * Random Bonus to HP (1 * 12)
            * Random Bonus to MP (1 * 12)
            * Curve Values  (16 * 4, 64)
            * 
            * 3) Character AI (2048)
            * 
            * 4) Random Number Lookup Table (256)
            * 
            * 5) Scene Lookup Table (64)
            * 
            * 6) Spell Order (56)
           */
            #endregion

            int r = 0;
            int o = 0;
            int c;

            int[] strongCurvesHP = { 37, 39, 41, 42, 43 };
            int[] weakCurvesHP = { 38, 40, 44, 45 };
            int[] strongCurvesMP = { 46, 49, 54 };
            int[] weakCurvesMP = { 47, 48, 50, 51, 52, 53 };

            try
            {
                while (r < 9)
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
                    var characterBalancer = CharacterChange.AssignCharacterBalancing(rnd);

                    // If this character was selected for stat changes
                    //if (characterSelectStats[r])
                    //{

                    if (characterStatChange == true)
                    {
                        // Stat Curve IDs
                        data[o] = characterBalancer[0] == 1 ? (byte)rnd.Next(0, 8) : (byte)rnd.Next(25, 31); o++; //STR
                        data[o] = characterBalancer[1] == 1 ? (byte)rnd.Next(0, 8) : (byte)rnd.Next(25, 31); o++; //VIT
                        data[o] = characterBalancer[2] == 1 ? (byte)rnd.Next(0, 8) : (byte)rnd.Next(25, 31); o++; //MAG
                        data[o] = characterBalancer[3] == 1 ? (byte)rnd.Next(0, 8) : (byte)rnd.Next(25, 31); o++; //SPR
                        data[o] = characterBalancer[4] == 1 ? (byte)rnd.Next(0, 8) : (byte)rnd.Next(32, 36); o++; //DEX
                        data[o] = characterBalancer[5] == 1 ? (byte)rnd.Next(0, 8) : (byte)rnd.Next(32, 36); o++; //LCK
                        data[o] = characterBalancer[6] == 1 ? (byte)strongCurvesHP[rnd.Next(strongCurvesHP.Length)] :
                            (byte)weakCurvesHP[rnd.Next(weakCurvesHP.Length)]; o++; //HP
                        data[o] = characterBalancer[7] == 1 ? (byte)strongCurvesMP[rnd.Next(strongCurvesMP.Length)] :
                            (byte)weakCurvesMP[rnd.Next(weakCurvesMP.Length)]; o++; //MP
                        o++; //EXP

                        o++; // Padding
                        data[o] = data[o]; o++; // Joining Level Modifier (what values does this use?)
                                                //data[o] = (byte)rnd.Next(0, 5); o++;
                        o++; // Padding
                    }
                    else
                    {
                        o += 12;
                    }

                    // These values should be 80h/128 and above - Characters can mostly use any Limit (albeit with skeleton flailing around)
                    // Avoid using 95h/149 > 9Bh/155 as these are Tifa's limits and will crash if not used by her

                    // Vincent and Tifa skip Limit assignment, even if specified
                    // Aeris has been temporarily locked pending tests
                    //if (characterSelectLimits[r] && r != 2 && r != 3 && r != 7)
                    //{
                    //    int picker = LimitIndex.CheckValidLimitIndex(rnd); // Limit 1-1
                    //    data[o] = limitOptions[0] ? (byte)picker : data[o] = data[o]; o++;

                    //    picker = LimitIndex.CheckValidLimitIndex(rnd); // Limit 1-2
                    //    data[o] = limitOptions[1] ? (byte)picker : data[o] = data[o]; o++;

                    //    picker = LimitIndex.CheckValidLimitIndex(rnd); // Limit 1-3
                    //    data[o] = limitOptions[2] ? (byte)picker : data[o] = data[o]; o++;

                    //    // Cait can't use random Limits past Lv.2 - Hardcoded to Slots
                    //    if (r != 6)
                    //    {
                    //        picker = LimitIndex.CheckValidLimitIndex(rnd); // Limit 2-1
                    //        data[o] = limitOptions[3] ? (byte)picker : data[o] = data[o]; o++;

                    //        picker = LimitIndex.CheckValidLimitIndex(rnd); // Limit 2-2
                    //        data[o] = limitOptions[4] ? (byte)picker : data[o] = data[o]; o++;

                    //        picker = LimitIndex.CheckValidLimitIndex(rnd); // Limit 2-3
                    //        data[o] = limitOptions[5] ? (byte)picker : data[o] = data[o]; o++;

                    //        picker = LimitIndex.CheckValidLimitIndex(rnd); // Limit 3-1
                    //        data[o] = limitOptions[6] ? (byte)picker : data[o] = data[o]; o++;

                    //        picker = LimitIndex.CheckValidLimitIndex(rnd); // Limit 3-2
                    //        data[o] = limitOptions[7] ? (byte)picker : data[o] = data[o]; o++;

                    //        picker = LimitIndex.CheckValidLimitIndex(rnd); // Limit 3-3
                    //        data[o] = limitOptions[8] ? (byte)picker : data[o] = data[o]; o++;

                    //        picker = LimitIndex.CheckValidLimitIndex(rnd); // Limit 4
                    //        data[o] = limitOptions[9] ? (byte)picker : data[o] = data[o]; o++;
                    //        o++; // 4-2, but cannot be unlocked
                    //        o++; // 4-3, but cannot be unlocked
                    //    }
                    //    else
                    //    {
                    //        // Skip Cait's remaining Limit data
                    //        o += 9;
                    //    }
                    //}
                    //else
                    //{
                    //    o += 12;
                    //}
                    o += 12;


                    //Disabled until later phase
                    o += 16;
                    //if (characterSelectLimits[r])
                    //{
                    //    if (limitOptions[15])
                    //    {
                    //        data[o] = (byte)rnd.Next(50, 100); o++;     // Kills for Limit Level 2
                    //        data[o] = data[o]; o++;
                    //    }
                    //    else
                    //    {
                    //        o += 2;
                    //    }
                    //}
                    //else
                    //{
                    //    o += 2;
                    //}

                    //// Prevents Cait from unlocking Lv.3
                    //if (characterSelectLimits[r] && r != 6)
                    //{
                    //    if (limitOptions[16])
                    //    {
                    //        data[o] = (byte)rnd.Next(101, 255); o++;    // Kills for Limit Level 3
                    //        data[o] = data[o]; o++;
                    //    }
                    //    else
                    //    {
                    //        o += 2;
                    //    }
                    //}
                    //else
                    //{
                    //    o += 2;
                    //}

                    //if (characterSelectLimits[r])
                    //{
                    //    if (limitOptions[9])
                    //    {
                    //        data[o] = (byte)rnd.Next(2, 8); o++;      // Required uses for 1-2
                    //        data[o] = 0; o++;
                    //    }
                    //    else
                    //    {
                    //        o += 2;
                    //    }
                    //}
                    //else
                    //{
                    //    o += 2;
                    //}

                    //// Prevents Tifa and Vincent from unlocking 1-3
                    //if (characterSelectLimits[r] && r != 2 && r != 7)
                    //{
                    //    if (limitOptions[10])
                    //    {
                    //        data[o] = (byte)rnd.Next(9, 16); o++;  // Required uses for 1-3
                    //        data[o] = 0; o++;
                    //    }
                    //    else
                    //    {
                    //        o += 2;
                    //    }
                    //}
                    //else
                    //{
                    //    o += 2;
                    //}

                    //// Prevents Cait unlocking 2-2
                    //if (characterSelectLimits[r] && r != 6)
                    //{
                    //    if (limitOptions[11])
                    //    {
                    //        data[o] = (byte)rnd.Next(2, 8); o++;      // Required uses for 2-2
                    //        data[o] = data[o]; o++;
                    //    }
                    //    else
                    //    {
                    //        o += 2;
                    //    }
                    //}
                    //else
                    //{
                    //    o += 2;
                    //}

                    //// Prevents Tifa, Cait, and Vincent from unlocking 2-3
                    //if (characterSelectLimits[r] && r != 2 && r != 6 && r != 7)
                    //{
                    //    if (limitOptions[12])
                    //    {
                    //        data[o] = (byte)rnd.Next(9, 16); o++;  // Required uses for 2-3
                    //        data[o] = 0; o++;
                    //    }
                    //    else
                    //    {
                    //        o += 2;
                    //    }
                    //}
                    //else
                    //{
                    //    o += 2;
                    //}

                    //// Prevents Cait and Vincent from unlocking 3-2
                    //if (characterSelectLimits[r] && r != 6 && r != 7)
                    //{
                    //    if (limitOptions[13])
                    //    {
                    //        data[o] = (byte)rnd.Next(2, 8); o++;      // Required uses for 3-2
                    //        data[o] = data[o]; o++;
                    //    }
                    //    else
                    //    {
                    //        o += 2;
                    //    }
                    //}
                    //else
                    //{
                    //    o += 2;
                    //}

                    //// Prevents the usual suspects from unlocking 3-3
                    //if (characterSelectLimits[r] && r!= 2 && r != 6 && r != 7)
                    //{
                    //    if (limitOptions[14])
                    //    {
                    //        data[o] = (byte)rnd.Next(9, 16); o++;  // Required uses for 3-3
                    //        data[o] = 0; o++;
                    //    }
                    //    else
                    //    {
                    //        o += 2;
                    //    }
                    //}
                    //else
                    //{
                    //    o += 2;
                    //}

                    //Disabled until later phase
                    o += 16;
                    //if (challengeOptions[1])
                    //{
                    //    // Limits will never fill
                    //    data[o] = 255; o++;
                    //    data[o] = 255; o++;
                    //    data[o] = 255; o++;
                    //    data[o] = 255; o++;

                    //    data[o] = 255; o++;
                    //    data[o] = 255; o++;
                    //    data[o] = 255; o++;
                    //    data[o] = 255; o++;

                    //    data[o] = 255; o++;
                    //    data[o] = 255; o++;
                    //    data[o] = 255; o++;
                    //    data[o] = 255; o++;

                    //    data[o] = 255; o++;
                    //    data[o] = 255; o++;
                    //    data[o] = 255; o++;
                    //    data[o] = 255; o++;
                    //}
                    //else
                    //{
                    //    if (limitOptions[17])
                    //    {
                    //        if (characterSelectLimits[r])
                    //        {
                    //            data[o] = (byte)rnd.Next(100, 255); o++;     // Gauge Resistance for Limit Level 1
                    //            data[o] = (byte)rnd.Next(1); o++;
                    //            data[o] = data[o]; o++;
                    //            data[o] = data[o]; o++;
                    //        }
                    //        else
                    //        {
                    //            o += 4;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        o += 4;
                    //    }
                    //    if (limitOptions[18])
                    //    {
                    //        if (characterSelectLimits[r])
                    //        {
                    //            data[o] = (byte)rnd.Next(100, 255); o++;     // Gauge Resistance for Limit Level 2
                    //            data[o] = (byte)rnd.Next(1); o++;
                    //            data[o] = data[o]; o++;
                    //            data[o] = data[o]; o++;
                    //        }
                    //        else
                    //        {
                    //            o += 4;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        o += 4;
                    //    }

                    //    if (limitOptions[18])
                    //    {
                    //        if (characterSelectLimits[r])
                    //        {
                    //            data[o] = (byte)rnd.Next(100, 255); o++;     // Gauge Resistance for Limit Level 3
                    //            data[o] = (byte)rnd.Next(1); o++;
                    //            data[o] = data[o]; o++;
                    //            data[o] = data[o]; o++;
                    //        }
                    //        else
                    //        {
                    //            o += 4;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        o += 4;
                    //    }

                    //    if (limitOptions[18])
                    //    {
                    //        if (characterSelectLimits[r])
                    //        {
                    //            data[o] = (byte)rnd.Next(100, 255); o++;     // Gauge Resistance for Limit Level 4
                    //            data[o] = (byte)rnd.Next(1); o++;
                    //            data[o] = data[o]; o++;
                    //            data[o] = data[o]; o++;
                    //        }
                    //        else
                    //        {
                    //            o += 4;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        o += 4;
                    //    }
                    //}
                    r++;
                }
                r = 0;


                // Random bonuses to primary stats (1 value per entry; default is 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3)
                o += 36;


                // Stat Curve values; 64 of them, 16 bytes each              
                o += 1024;
                r = 0;
                c = 0;

                // Character AI
                // My recommendation is to make multiple AI scripts (innate abilities or whatever) and have the tool read these scripts (byte files).
                // Then have the program read these byte files, put them in an array, and write to the data here. Make sure to read up on the
                // AI structure on Qhimm Wiki (link available on Qhimm Forum's front page) to get the header logic together.
                //if (options[6] != false)
                //{
                //    while (r < 2048)
                //    {
                //        data[o] = data[o]; o++;
                //        r++;
                //    }
                //}
                //else
                //{
                o += 2048;
                //}
                r = 0;

                // Random Lookup Table
                // For encounters to pop in a different order, randomise this; however, this was producing inoperable kernels
                // so there may be a logic to how the values must be randomised

                //Disabled, may be cause of broken kernels
                //if (rngOption[0])
                //{
                //    while (r < 256)
                //    {
                //        data[o] = (byte)rnd.Next(255); o++;
                //        r++;
                //    }
                //}
                //else
                //{
                //    while (r < 256)
                //    {
                //        data[o] = data[o]; o++;
                //        r++;
                //    }
                //}
                o += 256;
                r = 0;
                c = 0;

                // Scene Lookup Table
                // This must be updated with the lookup table data built in the Scene randomisation section.
                // If it isn't, the game will look for scenes in the wrong places and you'll get wrong encounters/softlocks from empty formations
                while (r < 64)
                {
                    data[o] = kernelLookup[c]; o++; c++;
                    r++;
                }
                r = 0;
                c = 0;

                // Spell Order List
                while (r < 56)
                {
                    o++;
                    r++;
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #2 (Growth & Lookup Data) has failed to randomise");
            }
            return data;
        }
    }
}
