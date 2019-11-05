using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo
{
    public class Scene
    {
        // Randomises the Scene.Bin
        public static byte[] RandomiseScene(byte[] data, byte[] camera)
        {
            /* Scene File Breakdown
             * The scene.bin comprises of 256 indvidual 'scene' files in a gzip format. Each scene contains 3 enemies and 4 formations.
             * The size of each scene is the same, as any unused data is padded with FF.
             */

            try
            {
                int[] enemyIDs = new int[8];                // 2 bytes per enemy ID, little endian so 260 would be 04 01 (104h), 3 enemies; includes 2 bytes of FF padding afterwards
                int[] battleSetup = new int[80];            // 4 records of 20 bytes each for Formations; Battle Setup Flags
                int[] cameraData = new int[192];            // 4 records of 38 bytes each for Formations; Camera Placement Data
                int[] formationPlacement = new int[384];    // 4 records of 96 bytes each for Formations; Enemy Placement Data (6 enemies per formation)
                int[] enemyData = new int[552];             // 3 records of 184 bytes each for Enemies; Enemy Data
                int[] attackData = new int[896];            // 32 records of 28 bytes each for Attacks; Enemy Attack Data
                int[] attackIDs = new int[64];              // 32 records of 2 bytes each for Attack IDs; Enemy Attack ID Data
                int[] attackNames = new int[1024];          // 32 records of 32 bytes each for Attack Names; Enemy Attack Name Data
                int[] formationAIOffset = new int[8];       // 8 bytes per formation AI script offset, 4 offsets
                int[] formationAI = new int[504];           // 504 bytes for Formation AI, 4 sets
                int[] enemyAIOffset = new int[6];           // 6 bytes per enemy AI script offset, 3 offsets
                int[] enemyAI = new int[4096];              // 4096 bytes for Enemy AI, 3 sets

                int rngID = 0; // Stores a randomly generated number

                int r = 0; // For iterating scene records (256 of them)
                int o = 0; // For iterating array indexes
                int c = 0; // For iterating records
                int k = 0; // See above

                byte[] nameBytes; // For assigning FF7 Ascii bytes after method processing
                Random rnd = new Random(Guid.NewGuid().GetHashCode()); // TODO: Have it take a seed as argument

                #region Enemy IDs
                // Enemy IDs
                while (r < 3)
                {
                    if (data[o] != 255 && data[o + 1] != 255) // Don't want to add an enemy if there's none here
                    {
                        data[o] = (byte)rnd.Next(256); o++;
                        data[o] = (byte)rnd.Next(2); o++;
                    }
                    else
                    {
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                    }
                    r++;
                }

                // Stores the enemy IDs for use later in enforcing consistency
                byte[] enemyIDList = new byte[6];
                enemyIDList[0] = data[o - 6];
                enemyIDList[1] = data[o - 5];
                enemyIDList[2] = data[o - 4];
                enemyIDList[3] = data[o - 3];
                enemyIDList[4] = data[o - 2];
                enemyIDList[5] = data[o - 1];
                r = 0;

                // FF padding
                data[o] = 255; o++;
                data[o] = 255; o++;

                //array = enemyIDs.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x00000;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                #endregion

                #region Battle Setup Flags
                while (r < 4)
                {
                    if (data[o] != 255)
                    {
                        // Battle Location
                        data[o] = (byte)rnd.Next(89); o++;
                        data[o] = data[o]; o++; // Always 0; despite being a 2-byte value, valid values never exceed 59h

                        // Next Formation ID, this transitions to another enemy formation directly after current enemies defeated; like Battle Square but not random.
                        data[o] = data[o]; o++; // FFFF by default, no new battle will load
                        data[o] = data[o]; o++;

                        // Escape Counter; value of 0009 makes battle unescapable; 2-byte but value never exceeds 0009
                        data[o] = (byte)rnd.Next(1, 9); o++;
                        data[o] = data[o]; o++;

                        // Unused - 2byte
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Battle Square - Possible Next Battles (4x 2-byte formation IDs, one is selected at random; default value for no battle is 03E7
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++; // Value of 03E7h

                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Escapable Flag (misc flags such as disabling pre-emptive)
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++; // Value of FDFF; FE F9 would prevent pre-emptives

                        // Battle Layout Type: Side attack, pincer, back attack, etc. 9 types.
                        /*
                            00 - Normal fight
                            01 - Preemptive
                            02 - Back attack
                            03 - Side attack
                            04 - Attacked from both sides (pincer attack, reverse side attack)
                            05 - Another attack from both sides battle (different maybe?)
                            06 - Another side attack
                            07 - A third side attack
                            08 - Normal battle that locks you in the front row, change command is disabled
                        */
                        data[o] = data[o]; o++;

                        // Indexed pre-battle camera position (where the camera starts from when battle loads in)
                        data[o] = data[o]; o++;
                    }
                    else
                    {
                        // Populate this entry with unaltered data
                        while (c < 20)
                        {
                            data[o] = data[o]; o++;
                            c++;
                        }
                        c = 0;
                    }
                    r++;
                }
                r = 0;

                //array = battleSetup.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x00008;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                #endregion

                #region Camera Placement Data

                while (r < 4)
                {
                    if (data[o] != 255 && data[o + 1] != 255)
                    {
                        // Using the byte array to retain camera data
                        // Primary Battle Idle Camera Position
                        data[o] = camera[k]; o++; k++; // Camera X Position
                        data[o] = camera[k]; o++; k++;

                        data[o] = camera[k]; o++; k++; // Camera Y Position
                        data[o] = camera[k]; o++; k++;

                        data[o] = camera[k]; o++; k++; // Camera Z Position
                        data[o] = camera[k]; o++; k++;

                        data[o] = camera[k]; o++; k++; // Focus X Direction
                        data[o] = camera[k]; o++; k++;

                        data[o] = camera[k]; o++; k++; // Focus Y Direction
                        data[o] = camera[k]; o++; k++;

                        data[o] = camera[k]; o++; k++;// Focus Z Direction
                        data[o] = camera[k]; o++; k++;


                        // Secondary Battle Idle Camera Position
                        data[o] = camera[k]; o++; k++; // Camera X Position
                        data[o] = camera[k]; o++; k++;

                        data[o] = camera[k]; o++; k++; // Camera Y Position
                        data[o] = camera[k]; o++; k++;

                        data[o] = camera[k]; o++; k++; // Camera Z Position
                        data[o] = camera[k]; o++; k++;

                        data[o] = camera[k]; o++; k++; // Focus X Direction
                        data[o] = camera[k]; o++; k++;

                        data[o] = camera[k]; o++; k++; // Focus Y Direction
                        data[o] = camera[k]; o++; k++;

                        data[o] = camera[k]; o++; k++; // Focus Z Direction
                        data[o] = camera[k]; o++; k++;


                        // Tertiary Battle Idle Camera Position
                        data[o] = camera[k]; o++; k++;// Camera X Position
                        data[o] = camera[k]; o++; k++;

                        data[o] = camera[k]; o++; k++;// Camera Y Position
                        data[o] = camera[k]; o++; k++;

                        data[o] = camera[k]; o++; k++; // Camera Z Position
                        data[o] = camera[k]; o++; k++;

                        data[o] = camera[k]; o++; k++;// Focus X Direction
                        data[o] = camera[k]; o++; k++;

                        data[o] = camera[k]; o++; k++;// Focus Y Direction
                        data[o] = camera[k]; o++; k++;

                        data[o] = camera[k]; o++; k++;// Focus Z Direction
                        data[o] = camera[k]; o++; k++;

                        // Unused Battle Camera Position - FF Padding
                        for (int i = 0; i < 12; i++)
                        {
                            data[o] = 255; o++; k++;
                        }
                    }
                    else
                    {
                        while (c < 48)
                        {
                            data[o] = data[o]; o++;
                            c++;
                        }
                        c = 0;
                    }
                    r++;
                    k = 0;
                }
                r = 0;

                //array = cameraData.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x00058;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                #endregion

                #region Battle Formation Data
                while (r < 4)
                {
                    //This randomises formation data for each enemy, but has been dummied out as it doesn't fit current project requirements
                    while (c < 6)
                    {
                        if (data[o] != 255 && data[o + 1] != 255)
                        {
                            // Set the rng so that a null enemy can't be picked
                            if (enemyData[2] == 255 && enemyData[3] == 255)
                            {
                                rngID = rnd.Next(1);
                            }
                            else if (enemyData[4] == 255 && enemyData[5] == 255)
                            {
                                rngID = rnd.Next(2);
                            }
                            else
                            {
                                rngID = rnd.Next(3);
                            }

                            // Pick a random enemy
                            if (rngID == 0)
                            {
                                // Sets enemy A as the formation enemy ID
                                data[o] = enemyIDList[0]; o++;
                                data[o] = enemyIDList[1]; o++;
                            }
                            else if (rngID == 1)
                            {
                                // Sets enemy B as the formation enemy ID
                                data[o] = enemyIDList[2]; o++;
                                data[o] = enemyIDList[3]; o++;
                            }
                            else
                            {
                                // Sets enemy C as the formation enemy ID
                                data[o] = enemyIDList[4]; o++;
                                data[o] = enemyIDList[5]; o++;
                            }

                            // X Coordinate
                            data[o] = data[o]; o++;
                            data[o] = data[o]; o++;

                            // Y Coordinate
                            data[o] = data[o]; o++;
                            data[o] = data[o]; o++;

                            // Z Coordinate
                            data[o] = data[o]; o++;
                            data[o] = data[o]; o++;

                            // Row
                            data[o] = data[o]; o++;
                            data[o] = data[o]; o++;

                            // Cover Flags (should be related to Row)
                            data[o] = data[o]; o++;
                            data[o] = data[o]; o++;

                            // Initial Condition Flags; only the last 5 bits are considered - FF FF FF FF is default
                            data[o] = 255; o++;
                            data[o] = 255; o++;
                            data[o] = 255; o++;
                            data[o] = 255; o++;
                        }
                        else
                        {
                            while (k < 16)
                            {
                                data[o] = data[o];
                                o++;
                                k++;
                            }
                            k = 0;
                        }
                        c++;
                    }
                    c = 0;
                    r++;
                }
                r = 0;
                //array = formationPlacement.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x00118;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                #endregion

                #region Enemy Data
                while (r < 3)
                {
                    // If enemy name is empty, assume no enemy is there and just retain pre-existing data
                    if (data[o] != 255)
                    {
                        // Enemy Name, 32 bytes ascii
                        nameBytes = AllMethods.NameGenerate(rnd);
                        data[o] = nameBytes[0]; o++;
                        data[o] = nameBytes[1]; o++;
                        data[o] = nameBytes[2]; o++;
                        data[o] = nameBytes[3]; o++;

                        rngID = rnd.Next(2); // Chance to append a longer name
                        if (rngID == 1)
                        {
                            data[o] = nameBytes[4]; o++;
                            data[o] = nameBytes[5]; o++;
                            data[o] = nameBytes[6]; o++;
                            data[o] = nameBytes[7]; o++;
                        }
                        else
                        {
                            data[o] = 255; o++;
                            data[o] = 255; o++;
                            data[o] = 255; o++;
                            data[o] = 255; o++;
                        }

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
                        data[o] = 255; o++; // Empty - Use FF to terminate the string

                        // Enemy Level - This'll likely be set via AI
                        data[o] = (byte)rnd.Next(10, 32); o++;

                        // Enemy Speed
                        data[o] = (byte)rnd.Next(10, 127); o++;

                        // Enemy Luck
                        data[o] = (byte)rnd.Next(0, 256); o++;

                        // Enemy Evade
                        data[o] = (byte)rnd.Next(0, 32); o++;

                        // Enemy Strength  - This'll likely be set via AI
                        data[o] = (byte)rnd.Next(10, 127); o++;

                        // Enemy Defence  - This'll likely be set via AI
                        data[o] = (byte)rnd.Next(10, 127); o++;

                        // Enemy Magic  - This'll likely be set via AI
                        data[o] = (byte)rnd.Next(10, 127); o++;

                        // Enemy Magic Defence  - This'll likely be set via AI
                        data[o] = (byte)rnd.Next(10, 127); o++;

                        // Enemy Elemental Types
                        /*
                            00h - Fire
                            01h - Ice
                            02h - Bolt
                            03h - Earth
                            04h - Bio
                            05h - Gravity
                            06h - Water
                            07h - Wind
                            08h - Holy
                            09h - Health
                            0Ah - Cut
                            0Bh - Hit
                            0Ch - Punch
                            0Dh - Shoot
                            0Eh - Scream
                            0Fh - HIDDEN
                            10h-1Fh - No Effect
                            20h-3Fh - Statuses (Damage done by actions that inflict these statuses will be modified)
                            FFh - No element
                        */
                        data[o] = (byte)rnd.Next(0, 17); o++; // 4 elemental/status properties have been set
                        data[o] = (byte)rnd.Next(0, 17); o++;
                        data[o] = (byte)rnd.Next(0, 17); o++;
                        data[o] = (byte)rnd.Next(0, 17); o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;

                        // Elemental Rates/Modifiers
                        /*
                            00h - Death
                            02h - Double Damage
                            03h - Unknown
                            04h - Half Damage
                            05h - Nullify Damage
                            06h - Absorb 100%
                            07h - Full Cure
                            FFh - Nothing
                         */
                        data[o] = (byte)rnd.Next(1, 7); o++;
                        data[o] = (byte)rnd.Next(1, 7); o++;
                        data[o] = (byte)rnd.Next(1, 7); o++;
                        data[o] = (byte)rnd.Next(1, 7); o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;


                        // Action Animation Index
                        /* This needs a lot of logic to get running in a safe way. Each enemy Index needs to be loaded up with valid IDs for different types
                           of attack with random variances for instances where enemy has multiple animations for an attack type (Behemoth's 4 physicals for instance).
                           TODO: Pass the enemy ID from here to a method that can then check the valid animation indices, then return an array of values that can be
                           fed into our data[] array for bytewriting.
                         */
                        while (c < 16)
                        {
                            int modelID = enemyIDs[0] + enemyIDs[1];
                            int attackID = data[o + 16] + data[o + 17];
                            int type = 0;
                            if (data[o + 466] != 255)
                            {
                                type = (int)Anim.phys;
                            }
                            else if (data[o + 479] != 255)
                            {
                                type = (int)Anim.mag;
                            }
                            else
                            {
                                type = (int)Anim.misc;
                            }
                            AllMethods.Indexer(modelID, attackID, type);
                            data[o] = data[o]; o++;
                            c++;
                        }
                        c = 0;

                        // Enemy Attack IDs for matching to Animation IDs - 2bytes per attack ID
                        while (c < 16)
                        {
                            data[o] = data[o]; o++;
                            data[o] = data[o]; o++;
                            c++;
                        }
                        c = 0;

                        // Enemy Camera Override IDs for matching to Animation IDs - 2bytes per Camera Override ID - FFFF by default
                        while (c < 16)
                        {
                            data[o] = data[o]; o++;
                            data[o] = data[o]; o++;
                            c++;
                        }
                        c = 0;

                        // Obtain Rates
                        // 1 byte per item, 4 items. Values below 80 are Drop Items (#/63). Values above 80 are Steal Items (#63)
                        data[o] = (byte)rnd.Next(2, 7); o++; // Item 1
                        data[o] = 255; o++; // Item 2
                        data[o] = 255; o++; // Item 3
                        data[o] = 255; o++; // Item 4

                        // Item IDs to be matched to the above drop/steal rates
                        // Item 1
                        data[o] = (byte)rnd.Next(2, 7); o++;
                        data[o] = (byte)rnd.Next(2, 7); o++;

                        // Item 2
                        data[o] = (byte)rnd.Next(2, 7); o++;
                        data[o] = (byte)rnd.Next(2, 7); o++;

                        // Item 3
                        data[o] = (byte)rnd.Next(2, 7); o++;
                        data[o] = (byte)rnd.Next(2, 7); o++;

                        // Item 4
                        data[o] = (byte)rnd.Next(2, 7); o++;
                        data[o] = (byte)rnd.Next(2, 7); o++;

                        // Manipulate/Berserk Attack IDs
                        // The first listed attack is the Berserk option; all 3 attacks can be selected for use under Manipulate
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Unknown Data
                        data[o] = 255; o++;
                        data[o] = 255; o++;

                        // Enemy MP
                        data[o] = (byte)rnd.Next(0, 11); o++;
                        data[o] = (byte)rnd.Next(0, 184); o++;

                        // Enemy AP
                        data[o] = (byte)rnd.Next(0, 64); o++;
                        data[o] = 0; o++;

                        // Enemy Morph Item ID - FFFF means no morph
                        data[o] = (byte)rnd.Next(0, 64); o++;
                        data[o] = (byte)rnd.Next(0, 64); o++;

                        // Back Attack multiplier
                        data[o] = (byte)rnd.Next(0, 33); o++;

                        // Alignment FF
                        data[o] = 255; o++;

                        // Enemy HP - Should probbly be set by AI
                        data[o] = (byte)rnd.Next(0, 256); o++;
                        data[o] = (byte)rnd.Next(0, 3); o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;

                        // EXP Points
                        data[o] = (byte)rnd.Next(0, 256); o++;
                        data[o] = (byte)rnd.Next(0, 3); o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;

                        // Gil
                        data[o] = (byte)rnd.Next(0, 256); o++;
                        data[o] = (byte)rnd.Next(0, 3); o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;


                        // Status Immunities
                        data[o] = (byte)rnd.Next(0, 256); o++;
                        data[o] = (byte)rnd.Next(0, 256); o++;
                        data[o] = (byte)rnd.Next(0, 256); o++;
                        data[o] = (byte)rnd.Next(0, 256); o++;

                        // Padding FF
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                    }
                    else
                    {
                        while (c < 184)
                        {
                            data[o] = data[o]; o++;
                            c++;
                        }
                        c = 0;
                    }
                    r++;
                }
                r = 0;

                //array = enemyIDs.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x00298;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                #endregion

                #region Attack Data
                while (r < 32)
                {
                    // If MP cost does not equal 65536 or Target flags are 0
                    // TODO: Need a way to ignore attacks that are animation handling
                    if (data[o + 4] != 255 && data[o + 5] != 255)
                    {
                        // Attack %
                        data[o] = (byte)rnd.Next(50, 150); o++;

                        // Impact Effect ID - Must be FF if Attack Effect ID is not FF
                        //data[o] = rnd.Next(0, 256); o++;
                        data[o] = data[o]; o++;

                        // Target Hurt Action Index
                        // 00 = Standard
                        // 01 = Stunned
                        // 02 = Heavy
                        // 03 = Ejected
                        //data[o] = rnd.Next(0, 4); o++;
                        data[o] = data[o]; o++;

                        // Unknown
                        data[o] = 255; o++;

                        // Casting Cost
                        data[o] = (byte)rnd.Next(0, 256); o++;
                        data[o] = 0; o++;

                        // Impact Sound - Must be FFFF if Attack Effect ID is not FF
                        //data[o] = rnd.Next(0, 256); o++;
                        //data[o] = rnd.Next(0, 256); o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Camera Movement ID for single target - FFFF if none
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Camera Movement ID for multi target - FFFF if none
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Target Flags - Logic will be tough for this one; will depend on attack element + attack type as some aren't designed for multi-target
                        data[o] = data[o]; o++;
                        //data[o] = data[o]; o++;

                        // Attack Effect ID - Must be FF if Impact Effect is not FF
                        data[o] = data[o]; o++;
                        //data[o] = data[o]; o++;

                        // Damage Calculation
                        data[o] = data[o]; o++;

                        // Base Power
                        data[o] = (byte)rnd.Next(0, 40); o++;

                        // Condition Sub-Menu Flags
                        // 00 = Party HP
                        // 01 = Party MP
                        // 02 = Party Status
                        // Other = None
                        data[o] = data[o]; o++;

                        // Status Effect Change
                        // 00-3F = Chance to inflict/heal status (#/63)
                        // 40 = Remove Status
                        // 80 - Toggle Status
                        //data[o] = (byte)rnd.Next(0, 4); o++;
                        data[o] = data[o]; o++;

                        // Attack Additional Effects
                        //data[o] = (byte)rnd.Next(0, 256); o++;
                        data[o] = data[o]; o++;

                        // Additional Effects Modifier Value
                        //data[o] = (byte)rnd.Next(0, 256); o++;
                        data[o] = data[o]; o++;


                        // Produce an enum class that holds the specific values for each status, then pick one of those or more and 
                        // pipe it into the statuses/elements; true random here would be too much + death/imprisoned/petrify can creep in

                        // Statuses
                        //data[o] = (byte)rnd.Next(0, 256); o++;
                        //data[o] = (byte)rnd.Next(0, 256); o++;
                        //data[o] = (byte)rnd.Next(0, 256); o++;
                        //data[o] = (byte)rnd.Next(0, 256); o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Elements
                        data[o] = (byte)rnd.Next(0, 256); o++;
                        data[o] = (byte)rnd.Next(0, 256); o++;
                        //data[o] = data[o]; o++;
                        //data[o] = data[o]; o++;

                        // Special Attack Flags
                        //data[o] = (byte)rnd.Next(0, 256); o++;
                        //data[o] = (byte)rnd.Next(0, 256); o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                    }
                    else
                    {
                        while (c < 28)
                        {
                            data[o] = data[o]; o++;
                            c++;
                        }
                        c = 0;
                    }
                    r++;
                }
                r = 0;

                //array = attackData.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x004C0;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                #endregion

                #region Attack IDs
                while (r < 32)
                {
                    // Attack ID - These should match the ones referenced in AI and Animation Attack IDs
                    //attackIDs[o] = rnd.Next(0, 256); o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    r++;
                }
                r = 0;

                //array = attackIDs.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x00840;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                #endregion

                #region Attack Names
                while (c < 32)
                {
                    // Attack Name, 32 bytes ascii
                    if (data[o] != 255)
                    {
                        nameBytes = AllMethods.NameGenerate(rnd);
                        data[o] = nameBytes[0]; o++;
                        data[o] = nameBytes[1]; o++;
                        data[o] = nameBytes[2]; o++;
                        data[o] = nameBytes[3]; o++;
                        rngID = rnd.Next(2); // Chance to append a longer name
                        if (rngID == 1)
                        {
                            data[o] = nameBytes[4]; o++;
                            data[o] = nameBytes[5]; o++;
                            data[o] = nameBytes[6]; o++;
                            data[o] = nameBytes[7]; o++;
                        }
                        else
                        {
                            data[o] = 0; o++;
                            data[o] = 0; o++;
                            data[o] = 0; o++;
                            data[o] = 0; o++;
                        }
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                        data[o] = 255; o++; // Empty - Use FF to terminate the string
                    }
                    else
                    {
                        o += 32;
                    }
                    c++;
                }
                //array = attackNames.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x00880;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                //c = 0;
                #endregion

                #region Formation AI Script Offsets
                // These need to match the location of each one
                //data[o] = 0; o++;
                //data[o] = 0; o++;
                //data[o] = 0; o++;
                //data[o] = 0; o++;
                //data[o] = data[o]; o++;
                //data[o] = data[o]; o++;
                //data[o] = data[o]; o++;
                //data[o] = data[o]; o++;

                //array = formationAIOffset.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x000C80;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                #endregion

                #region Formation AI
                // This is likely best served from a notepad containing AI scripts, though formation AI itself is very rarely used (Final Sephiroth fight)
                //array = formationAI.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x000C88;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                #endregion

                #region Enemy AI Offsets
                // These need to match the location of each one
                //enemyAIOffset[o] = 0; o++;
                //enemyAIOffset[o] = 0; o++;
                //enemyAIOffset[o] = 0; o++;

                //array = enemyAIOffset.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x000E80;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                #endregion

                #region Enemy AI
                // This is likely best served from a notepad containing AI scripts
                //array = formationAI.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x000E86;
                //bw.Write(array, 0, array.Length);
                #endregion
                //}
            }
            catch
            {
                MessageBox.Show("Error: Try-Catch failed");
            }
            return data;
        }
    }
}
