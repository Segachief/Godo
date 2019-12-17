using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo
{
    public class Kernel
    {
        // Section 0: Command Data
        public static byte[] RandomiseSection0(byte[] data, bool[] options, Random rnd)
        {
            /* Flags for commands like attack/magic. Not much to be changed here besides targeting parameters
             * and which menus open which sub-menus (for instance, Magic opens the magic sub-menu).
             * Would recommend leaving this data alone.
             * 
             * 32 commands in all, IDs start from 0:
             * 0) 'Left' - Dummy/unknown
             * 1) Attack
             * 2) Magic
             * 3) Summon
             * 4) Item
             * 5) Steal
             * 6) Sense
             * 7) Coin
             * 8) Throw
             * 9) Morph
             * 10) Deathblow
             * 11) Manip.
             * 12) Mime
             * 13) E. Skill
             * 14) All: (the all modifier that attach to spells)
             * 15) 4x: (The quadra modifier)
             * 16) Unknown
             * 17) Mug
             * 18) Change Row
             * 19) Defend
             * 20) Limit
             * 21) W-Magic
             * 22) W-Summon
             * 23) W-Item
             * 24) Slash-All
             * 25) 2x-Cut
             * 26) Flash
             * 27) 4x-Cut
             * 28-31) Dummy
             * 
             * The data available to modify (8 bytes each):
             * InitialCursor    (1)
             * Targeting Flag   (1)
             * Unknown          (2)
             * CameraID Single  (2)
             * CameraID Multi   (2)
            */

            int r = 0;
            int o = 0;

            try
            {
                while (r < 32)
                {
                    o += 8;
                    r++;
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #0 (Command Data) has failed to randomise");
            }
            return data;
        }

        // Section 1: Attack Data
        public static byte[] RandomiseSection1(byte[] data, bool[] options, Random rnd)
        {
            /* Player-available attacks. It should be noted that text strings are not stored with the
             * related data in 99% of cases, but instead at the back of the kernel in sections.
             * 
             * There are 128 actions that can be edited; the tool appears to list 256 but the remaining 128
             * do not exist and are actually stored in the .EXE (Limit Breaks). They have text pointers in
             * the kernel but nothing else.
             * 
             * The data available to modify (28 bytes each):
             * Hit %            (1)
             * Impact Effect    (1)
             * Target Hurt Anim (1)
             * Unknown          (1)
             * MP Cost          (2)
             * Impact Sound     (2)
             * Camera Single    (2)
             * Camera Multi     (2)
             * Target Flag      (1)
             * Attack Anim ID   (1)
             * Damage Calc      (1)
             * Base Power       (1)
             * Restore Type     (1)
             * Status Toggle Type   (1)
             * Additional Effects   (1)
             * ^ Modifier           (1)
             * Status Effects Mask  (4)
             * Elements Mask        (4)
             * Special Attack Flags (2)
            */
            //Random rnd = new Random(Guid.NewGuid().GetHashCode()); // TODO: Have it take a seed as argument
            int r = 0;
            int o = 0;

            try
            {
                while (r < 128)
                {
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    r++;
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #1 (Attack Data) has failed to randomise");
            }
            return data;
        }

        // Section 2: Battle & Growth Data + Kernel Lookup Table
        public static byte[] RandomiseSection2(byte[] data, bool[] options, Random rnd, byte[] kernelLookup)
        {
            /* Contains the following:
            * 1) Stat Curve IDs, Join Level Modifier, Limit IDs, Limit Requirements, and Gauge-fill resistance (9 for each character) (59 * 9)
            * STR > Vit > MAG > SPR > DEX > LCK > HP > MP > EXP Level Up Curve (9)
            * Padding (1)
            * Level Modifier (1)
            * Padding (1)
            * Limit ID 1-1 > 4-3 (1 each, 12 total) (not a typo; there are 3 limit slots for each Limit Level)
            * Required Uses 1-2 > 3-3 (2 each, 12 total)
            * Gauge Resistance 1 > 4 (4 each, 16 total)
            * 
            * 2) Curve Data
            * Random Bonus to Primary Stats (1 each, 12 total)
            * Random Bonus to HP (1 each, 12 total)
            * Random Bonus to MP (1 each, 12 total)
            * Curve Values  (16 each, 64 total)
            * 
            * 3) Character AI (2048)
            * 
            * 4) Random Number Lookup Table (256)
            * 
            * 5) Scene Lookup Table (64)
            * 
            * 6) Spell Order (56)
           */
            //Random rnd = new Random(Guid.NewGuid().GetHashCode()); // TODO: Have it take a seed as argument
            int r = 0;
            int o = 0;
            int c = 0;

            try
            {
                while (r < 9)
                {
                    // Stat Curve IDs
                    if (options[0] != false)
                    {
                        data[o] = (byte)rnd.Next(64); o++;      // STR
                        data[o] = (byte)rnd.Next(64); o++;      // VIT
                        data[o] = (byte)rnd.Next(64); o++;      // MAG
                        data[o] = (byte)rnd.Next(64); o++;      // SPR
                        data[o] = (byte)rnd.Next(64); o++;      // DEX
                        data[o] = (byte)rnd.Next(64); o++;      // LCK
                        data[o] = (byte)rnd.Next(64); o++;      // HP
                        data[o] = (byte)rnd.Next(64); o++;      // MP
                        data[o] = (byte)rnd.Next(64); o++;      // EXP
                    }
                    else
                    {
                        o += 9;
                    }

                    data[o] = data[o]; o++;                 // Padding
                    data[o] = data[o]; o++;                 // Joining Level Modifier
                    data[o] = data[o]; o++;                 // Padding

                    // These values should be 80h/128 and above - Characters can mostly use any Limit (albeit with skeleton flailing around)
                    // Avoid using 95h/149 > 9Bh/155 as these are Tifa's limits and will crash if not used by her
                    if (options[1] != false && (r != 2 || r != 6 || r != 7))
                    {
                        data[o] = (byte)rnd.Next(127, 256); o++;      // Limit ID 1-1
                        data[o] = (byte)rnd.Next(127, 256); o++;      // Limit ID 1-2
                        data[o] = (byte)rnd.Next(127, 256); o++;      // Limit ID 1-3
                        data[o] = (byte)rnd.Next(127, 256); o++;      // Limit ID 2-1
                        data[o] = (byte)rnd.Next(127, 256); o++;      // Limit ID 2-2
                        data[o] = (byte)rnd.Next(127, 256); o++;      // Limit ID 2-3
                        data[o] = (byte)rnd.Next(127, 256); o++;      // Limit ID 3-1
                        data[o] = (byte)rnd.Next(127, 256); o++;      // Limit ID 3-2
                        data[o] = (byte)rnd.Next(127, 256); o++;      // Limit ID 3-3
                        data[o] = (byte)rnd.Next(127, 256); o++;      // Limit ID 4-1
                        data[o] = (byte)rnd.Next(127, 256); o++;      // Limit ID 4-2
                        data[o] = (byte)rnd.Next(127, 256); o++;      // Limit ID 4-3
                    }
                    else
                    {
                        o += 12;
                    }

                    if (options[2] != false)
                    {
                        data[o] = (byte)rnd.Next(100); o++;     // Kills for Limit Level 2
                        data[o] = data[o]; o++;

                        data[o] = (byte)rnd.Next(100); o++;     // Kills for Limit Level 3
                        data[o] = data[o]; o++;

                        data[o] = (byte)rnd.Next(16); o++;      // Required used for 1-2
                        data[o] = data[o]; o++;

                        if (options[1] != false)
                        {
                            data[o] = (byte)rnd.Next(16); o++;  // Required used for 1-3
                            data[o] = data[o]; o++;
                        }
                        else
                        {
                            o += 2;
                        }

                        data[o] = (byte)rnd.Next(16); o++;      // Required used for 2-2
                        data[o] = data[o]; o++;

                        if (options[1] != false)
                        {
                            data[o] = (byte)rnd.Next(16); o++;  // Required used for 2-3
                            data[o] = data[o]; o++;
                        }
                        else
                        {
                            o += 2;
                        }

                        data[o] = (byte)rnd.Next(16); o++;      // Required used for 3-2
                        data[o] = data[o]; o++;

                        if (options[1] != false)
                        {
                            data[o] = (byte)rnd.Next(16); o++;  // Required used for 3-3
                            data[o] = data[o]; o++;
                        }
                        else
                        {
                            o += 2;
                        }
                    }
                    else
                    {
                        o += 16;
                    }

                    if (options[3] != false)
                    {
                        data[o] = (byte)rnd.Next(255); o++;     // Gauge Resistance for Limit Level 1
                        data[o] = (byte)rnd.Next(10); o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        data[o] = (byte)rnd.Next(255); o++;     // Gauge Resistance for Limit Level 2
                        data[o] = (byte)rnd.Next(10); o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        data[o] = (byte)rnd.Next(255); o++;     // Gauge Resistance for Limit Level 3
                        data[o] = (byte)rnd.Next(10); o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        data[o] = (byte)rnd.Next(255); o++;     // Gauge Resistance for Limit Level 4
                        data[o] = (byte)rnd.Next(10); o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                    }
                    else
                    {
                        o += 16;
                    }
                    r++;
                }
                r = 0;

                if (options[4] != false)
                {
                    // Random bonuses to primary stats (1 value per entry; default is 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3)
                    data[o] = (byte)rnd.Next(0, 1); o++;
                    data[o] = (byte)rnd.Next(0, 1); o++;
                    data[o] = (byte)rnd.Next(0, 1); o++;
                    data[o] = (byte)rnd.Next(0, 2); o++;
                    data[o] = (byte)rnd.Next(0, 2); o++;
                    data[o] = (byte)rnd.Next(0, 2); o++;
                    data[o] = (byte)rnd.Next(0, 3); o++;
                    data[o] = (byte)rnd.Next(0, 3); o++;
                    data[o] = (byte)rnd.Next(0, 4); o++;
                    data[o] = (byte)rnd.Next(0, 4); o++;
                    data[o] = (byte)rnd.Next(0, 5); o++;
                    data[o] = (byte)rnd.Next(0, 5); o++;

                    // Random bonuses to HP - Range from 0 - 160
                    data[o] = (byte)rnd.Next(40, 50); o++;
                    data[o] = (byte)rnd.Next(50, 60); o++;
                    data[o] = (byte)rnd.Next(60, 70); o++;
                    data[o] = (byte)rnd.Next(70, 80); o++;
                    data[o] = (byte)rnd.Next(80, 90); o++;
                    data[o] = (byte)rnd.Next(90, 100); o++;
                    data[o] = (byte)rnd.Next(100, 110); o++;
                    data[o] = (byte)rnd.Next(110, 120); o++;
                    data[o] = (byte)rnd.Next(120, 130); o++;
                    data[o] = (byte)rnd.Next(130, 140); o++;
                    data[o] = (byte)rnd.Next(140, 150); o++;
                    data[o] = (byte)rnd.Next(150, 160); o++;

                    // Random bonuses to MP - Range from 0 to 60
                    data[o] = (byte)rnd.Next(10, 20); o++;
                    data[o] = (byte)rnd.Next(10, 20); o++;
                    data[o] = (byte)rnd.Next(10, 20); o++;
                    data[o] = (byte)rnd.Next(20, 30); o++;
                    data[o] = (byte)rnd.Next(20, 30); o++;
                    data[o] = (byte)rnd.Next(20, 30); o++;
                    data[o] = (byte)rnd.Next(30, 40); o++;
                    data[o] = (byte)rnd.Next(30, 40); o++;
                    data[o] = (byte)rnd.Next(40, 50); o++;
                    data[o] = (byte)rnd.Next(40, 50); o++;
                    data[o] = (byte)rnd.Next(50, 60); o++;
                    data[o] = (byte)rnd.Next(50, 60); o++;

                }
                else
                {
                    o += 36;
                }

                // Stat Curve values; 64 of them, 16 bytes each
                if (options[5] != false)
                {
                    while (r < 64)
                    {
                        while (c < 16)
                        {
                            // Good luck
                            data[o] = (byte)rnd.Next(255); o++;
                            c++;
                        }
                        r++;
                    }
                }
                else
                {
                    o += 1024;
                }
                r = 0;
                c = 0;

                // Character AI
                // My recommendation is to make multiple AI scripts (innate abilities or whatever) and have the tool read these scripts (byte files).
                // Then have the program read these byte files, put them in an array, and write to the data here. Make sure to read up on the
                // AI structure on Qhimm Wiki (link available on Qhimm Forum's front page) to get the header logic together.
                if (options[6] != false)
                {
                    while (r < 2048)
                    {
                        data[o] = data[o]; o++;
                        r++;
                    }
                }
                else
                {
                    o += 2048;
                }
                r = 0;

                // Random Lookup Table
                // For encounters to pop in a different order, randomise this.
                // But to be honest, the impact of randomising the random table will, ironically, be minimal.
                if (options[7] != false)
                {
                    while (r < 256)
                    {
                        data[o] = (byte)rnd.Next(255); o++;
                        r++;
                    }
                }
                else
                {
                    while (r < 256)
                    {
                        data[o] = data[o]; o++;
                        r++;
                    }
                }

                // Scene Lookup Table
                // This must be updated with the lookup table data built in the Scene randomisation section.
                // If it isn't, the game will look for scenes in the wrong places and you'll get wrong encounters/softlocks from empty formations
                while (r < 64)
                {
                    data[o] = kernelLookup[c]; o++; c++;
                    r++;
                }
                r = o;
                c = 0;

                // Spell Order List
                // Not much point randomising this but included for completeness
                while (r < 56)
                {
                    data[o] = data[o]; o++;
                    r++;
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #2 (Growth & Lookup Data) has failed to randomise");
            }
            return data;
        }

        // Section 3: Character Record & Savemap Initialisation
        public static byte[] RandomiseSection3(byte[] data, bool[] options, Random rnd)
        {
            /* Kernel File Breakdown
             * The kernel.bin comprises multiple sections in a gzip-bin format.
             * This method focuses on the Initialisation section, which is copied to the game's savemap when starting a New Game
            */
            try
            {
                #region Initialisation Data (kernel.3) struct
                int[] charRecord = new int[132];    // Character Data is 132bytes in length
                int[] miscRecord = new int[4];      // Handles 4 misc bytes between character record and item record
                int[] itemRecord = new int[640];    // Item Data
                int[] materiaRecord = new int[800]; // Materia Data
                int[] stolenRecord = new int[192];  // Stolen Materia Data

                int r = 0; // For counting characters
                int o = 0; // For counting bytes in the character record
                int c = 0; // For Weapon minimum value ID
                int k = 0; // For Weapon maximum value ID

                byte[] nameBytes; // For assigning FF7 Ascii bytes after method processing
                //Random rnd = new Random(Guid.NewGuid().GetHashCode()); // TODO: Have it take a seed as argument

                #region Character Records (132bytes, 9 times)
                while (r < 9) // Iterates 9 times for each character; 0 = Cloud, 8 = Cid
                {
                    // Character ID
                    if (options[14] != false)
                    {
                        data[o] = (byte)rnd.Next(11); o++;
                    }
                    else
                    {
                        o++;
                    }

                    // Character Stats
                    if (options[15] != false)
                    {
                        // Level
                        data[o] = (byte)rnd.Next(1, 21); o++;

                        // Strength
                        data[o] = (byte)rnd.Next(51); o++;

                        // Vitality
                        data[o] = (byte)rnd.Next(51); o++;

                        // Magic
                        data[o] = (byte)rnd.Next(51); o++;

                        // Spirit
                        data[o] = (byte)rnd.Next(51); o++;

                        // Dexterity
                        data[o] = (byte)rnd.Next(51); o++;

                        // Luck
                        data[o] = (byte)rnd.Next(51); o++;

                        // Sources used - There are 6 values (1byte) for each Source type, redundant to change so skipped.
                        // A case could be made that Source-boosted Dex behaves differently to natural Dex, so added that.
                        o += 4; // Power to Spirit Sources, skipped and at 0
                        data[o] = (byte)rnd.Next(51); o++; // Dex sources
                        o++; // Luck Sources
                    }
                    else
                    {
                        o += 13;
                    }

                    /* Current Limit Lv - Skipped as this wouldn't serve much purpose. You can freely change Limit Level
                       if the Limits are learned but having a Limit Level equipped with no Limits in it would likely cause
                       a crash.
                    */
                    //data[o] = (byte)rnd.Next(1, 5);
                    data[o] = data[o]; o++;

                    // Current Limit Gauge
                    data[o] = (byte)rnd.Next(255); o++;

                    // Character Name: Gets two random 4-letter words from the method NameGenerate for the character name
                    if (options[16] != false)
                    {
                        nameBytes = AllMethods.NameGenerate(rnd);
                        data[o] = nameBytes[0]; o++;
                        data[o] = nameBytes[1]; o++;
                        data[o] = nameBytes[2]; o++;
                        data[o] = nameBytes[3]; o++;
                        data[o] = nameBytes[4]; o++;
                        data[o] = nameBytes[5]; o++;
                        data[o] = nameBytes[6]; o++;
                        data[o] = nameBytes[7]; o++;
                        data[o] = 0; o++;   // Empty - Note that names longer than 9 characters are stored but aren't retrieved properly by field script
                        data[o] = 0; o++;   // Empty - For instance, Ex-Soldier prints as 'Ex-Soldie' if his name is called by field script
                        data[o] = 0; o++;   // Empty
                        data[o] = 255; o++; // Empty - Use FF to terminate the string
                    }
                    else
                    {
                        o += 12;
                    }

                    // Equipped Weapon ID
                    /* Characters have a varying range for weapons, so this switch-case assigns the
                       the range for the randomisation. Characters are capable of using each other's
                       weapons without issue, but this helps eliminate late-game weapons from the mix.
                     */
                    #region Switch-Case for Weapon Ranges
                    if (options[17] != false)
                    {
                        switch (r)
                        { //TODO: Sort out the valid ranges
                            case 0:
                                c = 0;  // Cloud
                                k = 14;
                                break;
                            case 1:
                                c = 32; // Barret
                                k = 46;
                                break;
                            case 2:
                                c = 16; // Tifa
                                k = 30;
                                break;
                            case 3:
                                c = 62; // Aeristh
                                k = 71;
                                break;
                            case 4:
                                c = 48; // Red XIII
                                k = 60;
                                break;
                            case 5:
                                c = 87; // Yuffers
                                k = 99;
                                break;
                            case 6:
                                c = 0; // This is Young Cloud
                                k = 14;
                                break;
                            case 7:
                                c = 0; // This is Sephiroth
                                k = 127;
                                break;
                            case 8:
                                c = 73; // Cid
                                k = 85;
                                break;
                        }
                        data[o] = (byte)rnd.Next(c, k); o++;
                    }
                    else
                    {
                        o++;
                    }
                    #endregion

                    // Equipped Armour ID
                    if (options[18] != false)
                    {
                        data[o] = (byte)rnd.Next(31); o++;
                    }
                    else
                    {
                        o++;
                    }

                    // Equipped Accessory ID
                    if (options[19] != false)
                    {
                        data[o] = (byte)rnd.Next(31); o++;
                    }
                    else
                    {
                        o++;
                    }

                    // Status Flag - 0 by default, valid ranges? Any point in checking? This'll be for Fury/Sadness.
                    //data[o] = (byte)rnd.Next(1);
                    o++;

                    // Row Flag - No point changing this, default seems to be FF? Would have thought unchecked would = 0, and checked = 1 but guess not
                    data[o] = 255; o++;

                    // LvlProgressBar - No point changing this, it's purely visual.
                    data[o] = (byte)rnd.Next(64); o++;

                    // Learned Limit Skills - Bear in mind there are actually 3 Limits per level; 1-3, 2-3, 3-3, and 4-2/4-3 are unused
                    // Oddly, a character can only learn 5-6 Limits and will stop learning any more after that even if conditions are met.
                    // This was perhaps to prevent players learning the empty #-3 Limit by using the #-2 Limit 65535 times or something.
                    data[o] = 1; o++;
                    data[o] = 0; o++; // 2nd byte

                    // Number of Kills
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++; // 2nd byte

                    // Times Limit 1-1 used - If you hit the max value I think it unlocks 1-3
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++; // 2nd byte

                    // Times Limit 2-1 used - If you hit the max value I think it unlocks 2-3
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++; // 2nd byte

                    // Times Limit 3-1 used - If you hit the max value I think it unlocks 3-3
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++; // 2nd byte

                    // Current HP - Setting limit of 2000 for balance - 100 is game's functional minimum
                    if (options[20] != false)
                    {
                        data[o] = (byte)rnd.Next(99, 209); o++; // Returns 100HP minimum, 209 is D0 in hex
                        data[o] = (byte)rnd.Next(8); o++; // Random between 0 and 7, = 07D0 which is a range of 2000HP
                    }
                    else
                    {
                        o += 2;
                    }

                    // Base HP - Character's 'real' HP - Setting this to same as Current HP
                    data[o] = data[o - 2]; o++; // Sets it to the Current HP value
                    data[o] = data[o - 2]; o++;

                    // Current MP - Setting limit of 200 for balance - 10 is game's functional minimum.
                    if (options[21] != false)
                    {
                        data[o] = (byte)rnd.Next(11, 201); o++; o++; // Returns 10MP minimum, 200 max. 2nd byte is left as zero as we don't exceed 999MP
                    }
                    else
                    {
                        o++;
                    }

                    // Base MP - Starting MP on New Game - Setting this to same as Current MP
                    data[o] = data[o - 2]; o++; o++; // Sets it to the Current MP value

                    // Unknown, 4 bytes in length - Defaults are 0s
                    o += 4;

                    // Max HP - Set to 
                    data[o] = data[o - 10]; o++; // Set to Current HP value
                    data[o] = data[o - 10]; o++;

                    // Max MP - Setting limit of 200 for balance
                    data[o] = data[o - 8]; o++; o++; // Set to Current MP value

                    // Current EXP - Likely needs paired with Level to avoid oddities with the gauge
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    #region Equipment Materia Slots
                    if (options[22] != false)
                    {
                        // Randomising all 8 slots completely could be absolute chaos, some rules may need to be applied here
                        // Weapon Materia Slot #1 - Contains the ID + AP of the Materia, can be placed in an empty slot
                        data[o] = (byte)rnd.Next(91); o++;  // ID of the Materia
                        data[o] = (byte)rnd.Next(256); o++; // AP of the Materia, 3-byte
                        data[o] = (byte)rnd.Next(11); o++;
                        data[o] = (byte)rnd.Next(0); o++;

                        // Weapon Materia Slot #2 - Contains the ID + AP of the Materia, can be placed in an empty slot
                        data[o] = (byte)rnd.Next(91); o++;
                        data[o] = (byte)rnd.Next(256); o++;
                        data[o] = (byte)rnd.Next(11); o++;
                        data[o] = (byte)rnd.Next(0); o++;

                        // Weapon Materia Slot #3 - Contains the ID + AP of the Materia, can be placed in an empty slot
                        data[o] = (byte)rnd.Next(91); o++;
                        data[o] = (byte)rnd.Next(256); o++;
                        data[o] = (byte)rnd.Next(11); o++;
                        data[o] = (byte)rnd.Next(0); o++;

                        // Weapon Materia Slot #4 - Contains the ID + AP of the Materia, can be placed in an empty slot
                        data[o] = (byte)rnd.Next(91); o++;
                        data[o] = (byte)rnd.Next(256); o++;
                        data[o] = (byte)rnd.Next(11); o++;
                        data[o] = (byte)rnd.Next(0); o++;

                        // Weapon Materia Slot #5 - Contains the ID + AP of the Materia, can be placed in an empty slot
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;

                        // Weapon Materia Slot #6 - Contains the ID + AP of the Materia, can be placed in an empty slot
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;

                        // Weapon Materia Slot #7 - Contains the ID + AP of the Materia, can be placed in an empty slot
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;

                        // Weapon Materia Slot #8 - Contains the ID + AP of the Materia, can be placed in an empty slot
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;

                        // Armour Materia Slot #1 - Contains the ID + AP of the Materia, can be placed in an empty slot
                        data[o] = (byte)rnd.Next(91); o++; // ID of the Materia
                        data[o] = (byte)rnd.Next(256); o++; // AP of the Materia, 3-byte
                        data[o] = (byte)rnd.Next(256); o++;
                        data[o] = (byte)rnd.Next(0); o++;

                        // Armour Materia Slot #2 - Contains the ID + AP of the Materia, can be placed in an empty slot
                        data[o] = (byte)rnd.Next(91); o++;
                        data[o] = (byte)rnd.Next(256); o++;
                        data[o] = (byte)rnd.Next(256); o++;
                        data[o] = (byte)rnd.Next(0); o++;

                        // Armour Materia Slot #3 - Contains the ID + AP of the Materia, can be placed in an empty slot
                        data[o] = (byte)rnd.Next(91); o++;
                        data[o] = (byte)rnd.Next(256); o++;
                        data[o] = (byte)rnd.Next(256); o++;
                        data[o] = (byte)rnd.Next(0); o++;

                        // Armour Materia Slot #4 - Contains the ID + AP of the Materia, can be placed in an empty slot
                        data[o] = (byte)rnd.Next(91); o++;
                        data[o] = (byte)rnd.Next(256); o++;
                        data[o] = (byte)rnd.Next(256); o++;
                        data[o] = (byte)rnd.Next(0); o++;

                        // Armour Materia Slot #5 - Contains the ID + AP of the Materia, can be placed in an empty slot
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;

                        // Armour Materia Slot #6 - Contains the ID + AP of the Materia, can be placed in an empty slot
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;

                        // Armour Materia Slot #7 - Contains the ID + AP of the Materia, can be placed in an empty slot
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;

                        // Armour Materia Slot #8 - Contains the ID + AP of the Materia, can be placed in an empty slot
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

                    //#region Write Offset for Character Record

                    //// Randomised Character Record is now written into the kernel.3 bytestream
                    //// Casts the int array into a byte array
                    //array = data.Select(b => (byte)b).ToArray();

                    //switch (r)
                    //{
                    //    case 0:
                    //        bw.BaseStream.Position = 0x00000; // Sets the offset, using placeholders for now
                    //        bw.Write(array, 0, array.Length); // Overwrites the target with byte array
                    //        break;
                    //    case 1:
                    //        bw.BaseStream.Position = 0x00084; // Barret
                    //        bw.Write(array, 0, array.Length);
                    //        break;
                    //    case 2:
                    //        bw.BaseStream.Position = 0x00108; // Tifa
                    //        bw.Write(array, 0, array.Length);
                    //        break;
                    //    case 3:
                    //        bw.BaseStream.Position = 0x0018C; // Aeristh
                    //        bw.Write(array, 0, array.Length);
                    //        break;
                    //    case 4:
                    //        bw.BaseStream.Position = 0x00210; // Red XIII
                    //        bw.Write(array, 0, array.Length);
                    //        break;
                    //    case 5:
                    //        bw.BaseStream.Position = 0x00294; // Yuffie
                    //        bw.Write(array, 0, array.Length);
                    //        break;
                    //    case 6:
                    //        bw.BaseStream.Position = 0x00318; // Young Cloud
                    //        bw.Write(array, 0, array.Length);
                    //        break;
                    //    case 7:
                    //        bw.BaseStream.Position = 0x0039C; // Sephiroth
                    //        bw.Write(array, 0, array.Length);
                    //        break;
                    //    case 8:
                    //        bw.BaseStream.Position = 0x00420; // Cid
                    //        bw.Write(array, 0, array.Length);
                    //        break;
                    //}
                    //#endregion

                    // Character Record has been written, byte-counter (o) is reset and character counter (r) is incremented
                    //Array.Clear(data, 0, data.Length);
                    //o = 0;
                    r++;
                }
                #endregion

                #region Misc Bytes (4bytes)
                // Miscellaneous 4-bytes between character records and the item/materia arrays is handled here
                // Char ID Party Member Slot 1 - Party Leader should be Cloud, Tifa, or Cid
                if (options[22] != false)
                {
                    int slotA = rnd.Next(3);
                    int slotB = rnd.Next(9);
                    int slotC = rnd.Next(9);
                    if (slotA == 1)
                    {
                        data[o] = 0; o++; // Cloud
                    }
                    else if (slotA == 2)
                    {
                        data[o] = 2; o++; // Tifa
                    }
                    else if (slotA == 3)
                    {
                        data[o] = 8; o++; // Cid
                    }
                    else
                    {
                        o++;
                    }

                    // Char ID Party Member Slot 2
                    while (slotB == slotA) // Re-rolls until SlotB has a different ID to SlotA
                    {
                        slotB = rnd.Next(9);
                    }
                    data[o] = (byte)slotB; o++;

                    // Char ID Party Member Slot 3 - Unchanged as having unexpected party members can lock field scripts
                    while (slotC == slotA || slotC == slotB) // Re-rolls until SlotC has a different ID to SlotA and SlotB
                    {
                        slotC = rnd.Next(9);
                    }
                    data[o] = (byte)slotC; o++;
                }
                else
                {
                    data[o] = 0; o++; // Cloud
                    data[o] = 1; o++; // Barret
                    data[o] = 2; o++; // Tifa
                }

                // Padding of 1 byte, default value is FF
                data[o] = 255; o++;

                ////array = miscRecord.Select(b => (byte)b).ToArray();
                ////bw.BaseStream.Position = 0x004A4;
                ////bw.Write(array, 0, array.Length);
                #endregion

                //#region Item, Materia, and Stolen Materia (1632 bytes)
                //// Starting Item stock - This array is massive, 2*320 bytes to handle Item ID + Quantity with no absolute position within inventory
                //// Best approach is likely to restrict it to 5-10 items or so, from within item range only (not equipment)
                //c = 640;  // Adjust to fill empty space in item inventory to FF, 640 is entire item inventory is empty
                //while (c != 0)
                //{
                //    data[o] = 255; o++; // Unused values must be FF, unfortunately
                //    c--;
                //}
                //// TODO: Add the BaseStream writer when adding items, for now it can be left as-is.

                //// Starting Materia stock - Even larger at 4*200 bytes, same deal as items
                //c = 800;  // Adjust to fill empty space in Materia inventory to FF, 800 is entire Materia inventory is empty
                //while (c != 0)
                //{
                //    materiaRecord[o] = 255; o++; // Unused values must be FF, unfortunately
                //    c--;
                //}
                //// TODO: Will probably not update this part of kernel.3, but may go for this instead of equipping Materia by default
                //// If that happens, remember to add a BaseStream to write the array to the file

                //// Stolen Materia stock - 4*48 for 192 bytes. Unlikely to be changed but added for completeness
                //c = 192;  // Adjust to fill empty space in Materia inventory to FF, 192 sets entire Stolen Materia to blank
                //while (c != 0)
                //{
                //    // There is a mysterious set of values in this array by default; Materia at offset 0xB28(F0 00 00 00)
                //    // F0 isn't a valid Materia ID, as valid index IDs go up to around 95 decimal or so. Avoid editing these 4 values.
                //    stolenRecord[o] = 255; o++; // Unused values must be FF, unfortunately
                //    c--;
                //}
                //// TODO: Unlikely that this array will be edited, but add a BaseStream to update this part of kernel if you do.
                //#endregion

                //MessageBox.Show("Finished Randomising");
                #endregion
            }
            catch
            {
                MessageBox.Show("Kernel Section #3 (Initial Data) has failed to randomise");
            }
            return data;
        }

        public static byte[] RandomiseSection4(byte[] data, bool[] options, Random rnd)
        {
            /* Item Data
             * 
             * The data available to modify (28 bytes each):
             * 8x FF Mask       (8)
             * Camera Move ID   (2)
             * Restriction Mask (2) - 01 can be sold, 02 can be used in battle, 04 can be used in menu
             * Target Flags     (1)
             * Attack Effect ID (1)
             * Damage Calc.     (1)
             * Base Power       (1)
             * Conditions       (1) - 00 party HP, 01 party MP, 02 party status, other: none
             * Status Chance    (1) - 3F chance to inflict (/63), 40 Cure, 80 Toggle
             * Attack Additional Effects   (1)
             * Additional Effects Modifier (1)
             * Status Effects   (4)
             * Attack Element   (2)
             * Special Attack Flags (2)
            */
            //Random rnd = new Random(Guid.NewGuid().GetHashCode()); // TODO: Have it take a seed as argument
            int r = 0;
            int o = 0;

            try
            {
                if (options[9] != false)
                {
                    while (r < 128)
                    {
                        // FF Mask
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Camera Move ID
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Use Restriction Mask
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Target Flags
                        data[o] = data[o]; o++;

                        // Attack Effect ID
                        data[o] = data[o]; o++;

                        // Damage Calc
                        data[o] = data[o]; o++;

                        // Base Power
                        data[o] = data[o]; o++;

                        // Conditions
                        data[o] = data[o]; o++;

                        // Status Chance
                        data[o] = data[o]; o++;

                        // Additional Effects
                        data[o] = data[o]; o++;

                        // Additional Effects Modifier
                        data[o] = data[o]; o++;

                        // Status Effects
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Attack Element
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Special Attack Flags
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        r++;
                    }
                }
                else
                {
                    o += 3584;
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #4 (Item Data) has failed to randomise");
            }
            return data;
        }

        public static byte[] RandomiseSection5(byte[] data, bool[] options, Random rnd)
        {
            /* Weapon Data
             * 
             * The data available to modify (44 bytes each):
             * Target Flags     (1)
             * Always FF        (1)
             * Damage Calc.     (1)
             * Always FF        (1)
             * Base Power       (1)
             * Status Attack    (1)
             * Growth Rate      (1)
             * Crit%            (1)
             * Acc%             (1)
             * Model ID         (1) - Upper nybble = attack animation modifier (barret/vince only), lower nybble: model ID
             * Always FF        (1)
             * Sound ID Mask    (1)
             * Camera ID        (2) - Always FFFF
             * Equip Mask       (2) 1, 2, 4, 8, 10, 20, 40, 80, 100, 200, 400 = Cloud > Sephiroth
             * Attack Element   (2)
             * Always FFFF      (2)
             * Stat Type        (4) FF None, 00 STR > 05 LCK
             * Stat Boost       (4)
             * Slots            (8) 00 > 07 none > right-linked growth
             * Sound ID Hit     (1)
             * Sound ID Crit    (1)
             * Sound ID Miss    (1)
             * Impact ID        (1)
             * Special Flags    (2)
             * Restrict Mask    (2) 01 can sell, 02 can throw, 04 can Menu
            */
            //Random rnd = new Random(Guid.NewGuid().GetHashCode()); // TODO: Have it take a seed as argument
            int r = 0;
            int o = 0;

            try
            {
                if (options[10] != false)
                {
                    while (r < 128)
                    {
                        // Target Flags
                        data[o] = data[o]; o++;

                        // Always FF
                        data[o] = data[o]; o++;

                        // Damage Calc
                        data[o] = data[o]; o++;

                        // Always FF
                        data[o] = data[o]; o++;

                        // Base Power
                        data[o] = data[o]; o++;

                        // Status Attack
                        data[o] = data[o]; o++;

                        // Growth Rate
                        data[o] = data[o]; o++;

                        // Crit%
                        data[o] = data[o]; o++;

                        // Acc%
                        data[o] = data[o]; o++;

                        // Model ID - Barret, Vincent
                        data[o] = data[o]; o++;

                        // Always FF
                        data[o] = data[o]; o++;

                        // Sound ID
                        data[o] = data[o]; o++;

                        // Camera ID - Always FF
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Equip Mask
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Attack Element
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Always FF
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Stat Boost Type
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Stat Boost Value
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Slots
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Sound ID Hit
                        data[o] = data[o]; o++;

                        // Sound ID Crit
                        data[o] = data[o]; o++;

                        // Sound ID Miss
                        data[o] = data[o]; o++;

                        // Impact ID
                        data[o] = data[o]; o++;

                        // Special Flags
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Restrict Mask
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        r++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #5 (Weapon Data) has failed to randomise");
            }
            return data;
        }


        public static byte[] RandomiseSection6(byte[] data, bool[] options, Random rnd)
        {
            /* Armour Data
             * 
             * The data available to modify (44 bytes each):
             * Unknown          (1)
             * Damage Type      (1) - FF norm, 00 absorb, 01 null, 02 halve
             * Defence          (1)
             * MDef             (1)
             * Defence%         (1)
             * MDef%            (1)
             * Status Def       (1)
             * Unknown          (2)
             * Slots            (8) - 00 > 07 none > right-linked growth
             * Growth           (1)
             * Equip Mask       (2) - 1, 2, 4, 8, 10, 20, 40, 80, 100, 200, 400 = Cloud > Sephiroth
             * Elem Def         (2)
             * Always FF        (2)
             * Stat Type        (4)
             * Stat Boost       (4)
             * Restrict Mask    (2)
             * Always FF        (2)
            */
            //Random rnd = new Random(Guid.NewGuid().GetHashCode()); // TODO: Have it take a seed as argument
            int r = 0;
            int o = 0;

            try
            {
                if (options[11] != false)
                {
                    while (r < 32)
                    {
                        // Unknown
                        data[o] = data[o]; o++;

                        // Damage Type - FF Normal, 00 Absorb, 01 Null, 02 Halve
                        data[o] = data[o]; o++;

                        // Defence
                        data[o] = data[o]; o++;

                        // MDefence
                        data[o] = data[o]; o++;

                        // Defence%
                        data[o] = data[o]; o++;

                        // MDef%
                        data[o] = data[o]; o++;

                        // Status Defence
                        data[o] = data[o]; o++;

                        // Unknown
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Slots
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Growth
                        data[o] = data[o]; o++;

                        // Equip Mask
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Elem Def
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Always FF
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Stat Type
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Stat Boost
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Restrict Mask
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Always FF
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        r++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #6 (Armour Data) has failed to randomise");
            }
            return data;
        }

        public static byte[] RandomiseSection7(byte[] data, bool[] options, Random rnd)
        {
            /* Accessory Data
             * 
             * The data available to modify (44 bytes each):
             * Stat Type        (2)
             * Stat Boost       (2)
             * Elem Strength    (1) - 00 absorb, 01 null, 02 halve
             * Special Effect   (1) - 00 > 06 auto-haste > auto-wall
             * Elemental Mask   (2)
             * Status Mask      (4)
             * Equip Mask       (2)
             * Restrict Mask    (2)
            */
            //Random rnd = new Random(Guid.NewGuid().GetHashCode()); // TODO: Have it take a seed as argument
            int r = 0;
            int o = 0;

            try
            {
                if (options[12] != false)
                {
                    while (r < 32)
                    {
                        // Stat Type
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Stat Boost
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Elem Strength
                        data[o] = data[o]; o++;

                        // Special Effect
                        data[o] = data[o]; o++;

                        // Elem Mask
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Status Mask
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Equip Mask
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Restrict Mask
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        r++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #7 (Accessory Data) has failed to randomise");
            }
            return data;
        }

        public static byte[] RandomiseSection8(byte[] data, bool[] options, Random rnd)
        {
            /* Materia Data
             * 
             * The data available to modify (44 bytes each):
             * Level Up         (8) - Multiples of 100 (4x WORD)
             * Equip Effect ID  (1)
             * Status Effects   (3) - Only 24 statuses
             * Element Index    (1)
             * Materia Type     (1)
             * Attributes       (6)
            */
            //Random rnd = new Random(Guid.NewGuid().GetHashCode()); // TODO: Have it take a seed as argument
            int r = 0;
            int o = 0;

            try
            {
                if (options[13] != false)
                {
                    while (r < 95)
                    {
                        // Level Up
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Equip Effect ID
                        data[o] = data[o]; o++;

                        // Status Effects
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Element Index
                        data[o] = data[o]; o++;

                        // Materia Type
                        data[o] = data[o]; o++;

                        // Attributes
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        r++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #8 (Materia Data) has failed to randomise");
            }
            return data;
        }
    }
}
