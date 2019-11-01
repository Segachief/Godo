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
        public static byte[] RandomiseScene(byte[] data)
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
                int[] formationAIOffset = new int[8];      // 8 bytes per formation AI script offset, 4 offsets
                int[] formationAI = new int[504];           // 504 bytes for Formation AI, 4 sets
                int[] enemyAIOffset = new int[6];           // 6 bytes per enemy AI script offset, 3 offsets
                int[] enemyAI = new int[4096];              // 4096 bytes for Enemy AI, 3 sets

                // Stores the current values in the file so they can be referred to for internal randomisation logic
                byte[] enemyIDsCurrent = new byte[8];
                byte[] battleSetupCurrent = new byte[80];
                byte[] cameraDataCurrent = new byte[192];
                byte[] formationPlacementCurrent = new byte[384];
                byte[] enemyDataCurrent = new byte[552];
                byte[] attackDataCurrent = new byte[896];
                byte[] attackIDsCurrent = new byte[64];
                byte[] attackNamesCurrent = new byte[1024];
                byte[] formationAIOffsetCurrent = new byte[8];
                byte[] formationAICurrent = new byte[504];
                byte[] enemyAIOffsetCurrent = new byte[6];
                byte[] enemyAICurrent = new byte[4096];

                int rngID = 0; // Stores a randomly generated number

                int r = 0; // For iterating scene records (256 of them)
                int o = 0; // For iterating array indexes
                int c = 0; // For iterating records
                int k = 0; //

                //Note: Remember to call the index in the array for repeating data, such as Enemy IDs in the formation placement data.

                byte[] array; // For conversion of int array to byte array
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
                //while (r < 4)
                //{
                //    if (battleSetupCurrent[o] != 0xFF)
                //    {
                //        // Battle Location
                //        battleSetup[o] = rnd.Next(96); o++;
                //        battleSetup[o] = 0; o++; // Always 0; despite being a 2-byte value, valid values never exceed 59h

                //        // Next Formation ID, this transitions to another enemy formation directly after current enemies defeated; like Battle Square but not random.
                //        battleSetup[o] = battleSetupCurrent[o]; o++; // FFFF by default, no new battle will load
                //        battleSetup[o] = battleSetupCurrent[o]; o++;

                //        // Escape Counter; value of 0009 makes battle unescapable; 2-byte but value never exceeds 0009
                //        battleSetup[o] = rnd.Next(1, 9); o++;
                //        battleSetup[o] = battleSetupCurrent[o]; o++;

                //        // Unused - 2byte
                //        battleSetup[o] = battleSetupCurrent[o]; o++;
                //        battleSetup[o] = battleSetupCurrent[o]; o++;

                //        // Battle Square - Possible Next Battles (4x 2-byte formation IDs, one is selected at random; default value for no battle is 03E7
                //        battleSetup[o] = battleSetupCurrent[o]; o++;
                //        battleSetup[o] = battleSetupCurrent[o]; o++; // Value of 03E7h

                //        battleSetup[o] = battleSetupCurrent[o]; o++;
                //        battleSetup[o] = battleSetupCurrent[o]; o++;

                //        battleSetup[o] = battleSetupCurrent[o]; o++;
                //        battleSetup[o] = battleSetupCurrent[o]; o++;

                //        battleSetup[o] = battleSetupCurrent[o]; o++;
                //        battleSetup[o] = battleSetupCurrent[o]; o++;

                //        // Escapable Flag (misc flags such as disabling pre-emptive)
                //        battleSetup[o] = battleSetupCurrent[o]; o++;
                //        battleSetup[o] = battleSetupCurrent[o]; o++; // Value of FDFF; FE F9 would prevent pre-emptives

                //        // Battle Layout Type: Side attack, pincer, back attack, etc. 9 types.
                //        /*
                //            00 - Normal fight
                //            01 - Preemptive
                //            02 - Back attack
                //            03 - Side attack
                //            04 - Attacked from both sides (pincer attack, reverse side attack)
                //            05 - Another attack from both sides battle (different maybe?)
                //            06 - Another side attack
                //            07 - A third side attack
                //            08 - Normal battle that locks you in the front row, change command is disabled
                //        */
                //        battleSetup[o] = battleSetupCurrent[o]; o++;

                //        // Indexed pre-battle camera position (where the camera starts from when battle loads in)
                //        battleSetup[o] = rnd.Next(255); o++;
                //    }
                //    else
                //    {
                //        // Populate this entry with unaltered data
                //        while (c < 20)
                //        {
                //            battleSetup[o] = 255; o++;
                //            c++;
                //        }
                //        c = 0;
                //    }
                //    r++;
                //}
                //r = 0;

                //array = battleSetup.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x00008;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                #endregion

                #region Camera Placement Data

                //while (r < 4)
                //{
                //    if (cameraDataCurrent[o] != 255 && cameraDataCurrent[o + 1] != 255)
                //    {
                //        // Using the byte array to retain camera data
                //        // Primary Battle Idle Camera Position
                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera X Position
                //        cameraData[o] = cameraDataCurrent[o]; o++;

                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera Y Position
                //        cameraData[o] = cameraDataCurrent[o]; o++;

                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera Z Position
                //        cameraData[o] = cameraDataCurrent[o]; o++;

                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera X Direction
                //        cameraData[o] = cameraDataCurrent[o]; o++;

                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera y Direction
                //        cameraData[o] = cameraDataCurrent[o]; o++;

                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera Z Direction
                //        cameraData[o] = cameraDataCurrent[o]; o++;


                //        // Secondary Battle Idle Camera Position
                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera X Position
                //        cameraData[o] = cameraDataCurrent[o]; o++;

                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera Y Position
                //        cameraData[o] = cameraDataCurrent[o]; o++;

                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera Z Position
                //        cameraData[o] = cameraDataCurrent[o]; o++;

                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera X Direction
                //        cameraData[o] = cameraDataCurrent[o]; o++;

                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera y Direction
                //        cameraData[o] = cameraDataCurrent[o]; o++;

                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera Z Direction
                //        cameraData[o] = cameraDataCurrent[o]; o++;


                //        // Tertiary Battle Idle Camera Position
                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera X Position
                //        cameraData[o] = cameraDataCurrent[o]; o++;

                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera Y Position
                //        cameraData[o] = cameraDataCurrent[o]; o++;

                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera Z Position
                //        cameraData[o] = cameraDataCurrent[o]; o++;

                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera X Direction
                //        cameraData[o] = cameraDataCurrent[o]; o++;

                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera y Direction
                //        cameraData[o] = cameraDataCurrent[o]; o++;

                //        cameraData[o] = cameraDataCurrent[o]; o++; // Camera Z Direction
                //        cameraData[o] = cameraDataCurrent[o]; o++;


                //        // Unused Battle Camera Position - FF Padding
                //        for (int i = 0; i < 12; i++)
                //        {
                //            cameraData[o] = 255; o++;
                //        }
                //    }
                //    else
                //    {
                //        while (c < 48)
                //        {
                //            cameraData[o] = cameraDataCurrent[o]; o++;
                //            c++;
                //        }
                //        c = 0;
                //    }
                //    r++;
                //}
                //r = 0;

                //array = cameraData.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x00058;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                #endregion

                #region Battle Formation Data
                // Sets the enemy IDs established at beginning of the scene so they can be referenced by formation array
                //int[] enemyIDList = new int[6];
                //enemyIDList[0] = enemyIDs[o]; o++;
                //enemyIDList[1] = enemyIDs[o]; o++;
                //enemyIDList[2] = enemyIDs[o]; o++;
                //enemyIDList[3] = enemyIDs[o]; o++;
                //enemyIDList[4] = enemyIDs[o]; o++;
                //enemyIDList[5] = enemyIDs[o]; o++;
                //o = 0;
                //while (r < 4)
                //{
                //    //This randomises formation data for each enemy, but has been dummied out as it doesn't fit current project requirements
                //    while (c < 6)
                //    {
                //        rngID = rnd.Next(3);
                //        if (formationPlacement[o] != 255 && formationPlacement[o + 1] != 255)
                //        {
                //            if (rngID == 0)
                //            {
                //                // Sets enemy A as the formation enemy ID
                //                formationPlacement[o] = enemyIDList[0]; o++;
                //                formationPlacement[o] = enemyIDList[1]; o++;
                //            }
                //            else if (rngID == 1)
                //            {
                //                // Sets enemy B as the formation enemy ID
                //                formationPlacement[o] = enemyIDList[2]; o++;
                //                formationPlacement[o] = enemyIDList[3]; o++;
                //            }
                //            else
                //            {
                //                // Sets enemy C as the formation enemy ID
                //                formationPlacement[o] = enemyIDList[4]; o++;
                //                formationPlacement[o] = enemyIDList[5]; o++;
                //            }

                //            // X Coordinate
                //            formationPlacement[o] = formationPlacementCurrent[o]; o++;
                //            formationPlacement[o] = formationPlacementCurrent[o]; o++;

                //            // Y Coordinate
                //            formationPlacement[o] = formationPlacementCurrent[o]; o++;
                //            formationPlacement[o] = formationPlacementCurrent[o]; o++;

                //            // Z Coordinate
                //            formationPlacement[o] = formationPlacementCurrent[o]; o++;
                //            formationPlacement[o] = formationPlacementCurrent[o]; o++;

                //            // Row
                //            formationPlacement[o] = formationPlacementCurrent[o]; o++;
                //            formationPlacement[o] = formationPlacementCurrent[o]; o++;

                //            // Cover Flags (should be related to Row)
                //            formationPlacement[o] = formationPlacementCurrent[o]; o++;
                //            formationPlacement[o] = formationPlacementCurrent[o]; o++;

                //            // Initial Condition Flags; only the last 5 bits are considered - FF FF FF FF is default
                //            formationPlacement[o] = 255; o++;
                //            formationPlacement[o] = 255; o++;
                //            formationPlacement[o] = 255; o++;
                //            formationPlacement[o] = 255; o++;
                //        }
                //        else
                //        {
                //            while (k < 16)
                //            {
                //                formationPlacement[o] = formationPlacementCurrent[o];
                //                o++;
                //                k++;
                //            }
                //            k = 0;
                //        }
                //        c++;
                //    }
                //    c = 0;
                //    r++;
                //}
                //r = 0;
                //array = formationPlacement.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x00118;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                #endregion

                #region Enemy Data
                //while (r < 3)
                //{
                //    // If enemy name is empty, assume no enemy is there and just retain pre-existing data
                //    if (enemyDataCurrent[o] != 255)
                //    {
                //        // Enemy Name, 32 bytes ascii
                //        nameBytes = AllMethods.NameGenerate(rnd);
                //        enemyData[o] = nameBytes[0]; o++;
                //        enemyData[o] = nameBytes[1]; o++;
                //        enemyData[o] = nameBytes[2]; o++;
                //        enemyData[o] = nameBytes[3]; o++;

                //        rngID = rnd.Next(2); // Chance to append a longer name
                //        if (rngID == 1)
                //        {
                //            enemyData[o] = nameBytes[4]; o++;
                //            enemyData[o] = nameBytes[5]; o++;
                //            enemyData[o] = nameBytes[6]; o++;
                //            enemyData[o] = nameBytes[7]; o++;
                //        }
                //        else
                //        {
                //            enemyData[o] = 255; o++;
                //            enemyData[o] = 255; o++;
                //            enemyData[o] = 255; o++;
                //            enemyData[o] = 255; o++;
                //        }

                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++; // Empty - Use FF to terminate the string

                //        // Enemy Level - This'll likely be set via AI
                //        enemyData[o] = rnd.Next(10, 63); o++;

                //        // Enemy Speed
                //        enemyData[o] = rnd.Next(10, 256); o++;

                //        // Enemy Luck
                //        enemyData[o] = rnd.Next(0, 256); o++;

                //        // Enemy Evade
                //        enemyData[o] = rnd.Next(10, 256); o++;

                //        // Enemy Strength  - This'll likely be set via AI
                //        enemyData[o] = rnd.Next(10, 127); o++;

                //        // Enemy Defence  - This'll likely be set via AI
                //        enemyData[o] = rnd.Next(10, 127); o++;

                //        // Enemy Magic  - This'll likely be set via AI
                //        enemyData[o] = rnd.Next(10, 127); o++;

                //        // Enemy Magic Defence  - This'll likely be set via AI
                //        enemyData[o] = rnd.Next(10, 127); o++;

                //        // Enemy Elemental Types
                //        /*
                //            00h - Fire
                //            01h - Ice
                //            02h - Bolt
                //            03h - Earth
                //            04h - Bio
                //            05h - Gravity
                //            06h - Water
                //            07h - Wind
                //            08h - Holy
                //            09h - Health
                //            0Ah - Cut
                //            0Bh - Hit
                //            0Ch - Punch
                //            0Dh - Shoot
                //            0Eh - Scream
                //            0Fh - HIDDEN
                //            10h-1Fh - No Effect
                //            20h-3Fh - Statuses (Damage done by actions that inflict these statuses will be modified)
                //            FFh - No element
                //        */
                //        enemyData[o] = rnd.Next(0, 63); o++; // 4 elemental/status properties have been set
                //        enemyData[o] = rnd.Next(0, 63); o++;
                //        enemyData[o] = rnd.Next(0, 63); o++;
                //        enemyData[o] = rnd.Next(0, 63); o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;

                //        // Elemental Rates/Modifiers
                //        /*
                //            00h - Death
                //            02h - Double Damage
                //            04h - Half Damage
                //            05h - Nullify Damage
                //            06h - Absorb 100%
                //            07h - Full Cure
                //            FFh - Nothing
                //         */
                //        enemyData[o] = rnd.Next(2, 7); o++;
                //        enemyData[o] = rnd.Next(2, 7); o++;
                //        enemyData[o] = rnd.Next(2, 7); o++;
                //        enemyData[o] = rnd.Next(2, 7); o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;


                //        // Action Animation Index
                //        /* This needs a lot of logic to get running in a safe way. Each enemy Index needs to be loaded up with valid IDs for different types
                //           of attack with random variances for instances where enemy has multiple animations for an attack type (Behemoth's 4 physicals for instance).
                //           TODO: Pass the enemy ID from here to a method that can then check the valid animation indices, then return an array of values that can be
                //           fed into our enemyData[] array for bytewriting.
                //         */
                //        while (c < 16)
                //        {
                //            enemyData[o] = enemyDataCurrent[o]; o++;
                //            c++;
                //        }
                //        c = 0;

                //        // Enemy Attack IDs for matching to Animation IDs - 2bytes per attack ID
                //        while (c < 16)
                //        {
                //            enemyData[o] = enemyDataCurrent[o]; o++;
                //            enemyData[o] = enemyDataCurrent[o]; o++;
                //            c++;
                //        }
                //        c = 0;

                //        // Enemy Camera Override IDs for matching to Animation IDs - 2bytes per Camera Override ID - FFFF by default
                //        while (c < 16)
                //        {
                //            enemyData[o] = enemyDataCurrent[o]; o++;
                //            enemyData[o] = enemyDataCurrent[o]; o++;
                //            c++;
                //        }
                //        c = 0;

                //        // Obtain Rates
                //        // 1 byte per item, 4 items. Values below 80 are Drop Items (#/63). Values above 80 are Steal Items (#63)
                //        enemyData[o] = rnd.Next(2, 7); o++; // Item 1
                //        enemyData[o] = 255; o++; // Item 2
                //        enemyData[o] = 255; o++; // Item 3
                //        enemyData[o] = 255; o++; // Item 4

                //        // Item IDs to be matched to the above drop/steal rates
                //        // Item 1
                //        enemyData[o] = 0; o++;
                //        enemyData[o] = 127; o++;

                //        // Item 2
                //        enemyData[o] = 0; o++;
                //        enemyData[o] = 127; o++;

                //        // Item 3
                //        enemyData[o] = 0; o++;
                //        enemyData[o] = 127; o++;

                //        // Item 4
                //        enemyData[o] = 0; o++;
                //        enemyData[o] = 127; o++;

                //        // Manipulate/Berserk Attack IDs
                //        // The first listed attack is the Berserk option; all 3 attacks can be selected for use under Manipulate
                //        enemyData[o] = enemyDataCurrent[o]; o++;
                //        enemyData[o] = enemyDataCurrent[o]; o++;

                //        enemyData[o] = enemyDataCurrent[o]; o++;
                //        enemyData[o] = enemyDataCurrent[o]; o++;

                //        enemyData[o] = enemyDataCurrent[o]; o++;
                //        enemyData[o] = enemyDataCurrent[o]; o++;

                //        // Unknown Data
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;

                //        // Enemy MP
                //        enemyData[o] = rnd.Next(0, 11); o++;
                //        enemyData[o] = rnd.Next(0, 184); o++;

                //        // Enemy AP
                //        enemyData[o] = 0; o++;
                //        enemyData[o] = rnd.Next(0, 64); o++;

                //        // Enemy Morph Item ID - FFFF means no morph
                //        enemyData[o] = 0; o++;
                //        enemyData[o] = rnd.Next(0, 127); o++;

                //        // Back Attack multiplier
                //        enemyData[o] = rnd.Next(0, 33); o++;

                //        // Alignment FF
                //        enemyData[o] = 255; o++;

                //        // Enemy HP - Should probbly be set by AI
                //        enemyData[o] = 0; o++;
                //        enemyData[o] = 0; o++;
                //        enemyData[o] = rnd.Next(0, 27); o++;
                //        enemyData[o] = rnd.Next(0, 256); o++;

                //        // EXP Points
                //        enemyData[o] = 0; o++;
                //        enemyData[o] = 0; o++;
                //        enemyData[o] = rnd.Next(0, 3); o++;
                //        enemyData[o] = rnd.Next(0, 232); o++;

                //        // Gil
                //        enemyData[o] = 0; o++;
                //        enemyData[o] = 0; o++;
                //        enemyData[o] = rnd.Next(0, 3); o++;
                //        enemyData[o] = rnd.Next(0, 232); o++;


                //        // Status Immunities
                //        enemyData[o] = rnd.Next(0, 256); o++;
                //        enemyData[o] = rnd.Next(0, 256); o++;
                //        enemyData[o] = rnd.Next(0, 256); o++;
                //        enemyData[o] = rnd.Next(0, 256); o++;

                //        // Padding FF
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //        enemyData[o] = 255; o++;
                //    }
                //    else
                //    {
                //        while (c < 184)
                //        {
                //            enemyData[o] = enemyDataCurrent[o]; o++;
                //            c++;
                //        }
                //        c = 0;
                //    }
                //    r++;
                //}
                //r = 0;

                //array = enemyIDs.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x00298;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                #endregion

                #region Attack Data
                //while (r < 32)
                //{
                //    // If the base power is 255 or 0 then we can assume it is not an attack, but a special action (i.e., animation handling)
                //    if (attackDataCurrent[o + 15] != 255 || attackDataCurrent[o + 15] != 0)
                //    {
                //        // Attack %
                //        attackData[o] = rnd.Next(50, 150); o++;

                //        // Impact Effect ID - Must be FF if Attack Effect ID is not FF
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        attackData[o] = attackDataCurrent[o]; o++;

                //        // Target Hurt Action Index
                //        // 00 = Standard
                //        // 01 = Stunned
                //        // 02 = Heavy
                //        // 03 = Ejected
                //        //attackData[o] = rnd.Next(0, 4); o++;
                //        attackData[o] = attackDataCurrent[o]; o++;

                //        // Unknown
                //        attackData[o] = 255; o++;

                //        // Casting Cost
                //        attackData[o] = 0; o++;
                //        attackData[o] = rnd.Next(0, 256); o++;

                //        // Impact Sound - Must be FFFF if Attack Effect ID is not FF
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        attackData[o] = attackDataCurrent[o]; o++;
                //        attackData[o] = attackDataCurrent[o]; o++;

                //        // Camera Movement ID for single target - FFFF if none
                //        //attackData[o] = 255; o++;
                //        //attackData[o] = 255; o++;
                //        attackData[o] = attackDataCurrent[o]; o++;
                //        attackData[o] = attackDataCurrent[o]; o++;

                //        // Camera Movement ID for multi target - FFFF if none
                //        //attackData[o] = 255; o++;
                //        //attackData[o] = 255; o++;
                //        attackData[o] = attackDataCurrent[o]; o++;
                //        attackData[o] = attackDataCurrent[o]; o++;

                //        // Target Flags - Logic will be tough for this one; will depend on attack element + attack type as some aren't designed for multi-target
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        attackData[o] = attackDataCurrent[o]; o++;

                //        // Attack Effect ID - Must be FF if Impact Effect is not FF
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        attackData[o] = attackDataCurrent[o]; o++;

                //        // Damage Calculation
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        attackData[o] = attackDataCurrent[o]; o++;

                //        // Base Power
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        attackData[o] = attackDataCurrent[o]; o++;

                //        // Condition Sub-Menu Flags
                //        // 00 = Party HP
                //        // 01 = Party MP
                //        // 02 = Party Status
                //        // Other = None
                //        //attackData[o] = rnd.Next(0, 3); o++;
                //        attackData[o] = attackDataCurrent[o]; o++;

                //        // Status Effect Change
                //        // 00-3F = Chance to inflict/heal status (#/63)
                //        // 40 = Remove Status
                //        // 80 - Toggle Status
                //        //attackData[o] = rnd.Next(0, 4); o++;
                //        attackData[o] = attackDataCurrent[o]; o++;

                //        // Attack Additional Effects
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        attackData[o] = attackDataCurrent[o]; o++;

                //        // Additional Effects Modifier Value
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        attackData[o] = attackDataCurrent[o]; o++;


                //        // Produce an enum class that holds the specific values for each status, then pick one of those or more and 
                //        // pipe it into the statuses/elements; true random here would be too much + death/imprisoned/petrify can creep in

                //        // Statuses
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        attackData[o] = attackDataCurrent[o]; o++;
                //        attackData[o] = attackDataCurrent[o]; o++;
                //        attackData[o] = attackDataCurrent[o]; o++;
                //        attackData[o] = attackDataCurrent[o]; o++;

                //        // Elements
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        attackData[o] = attackDataCurrent[o]; o++;
                //        attackData[o] = attackDataCurrent[o]; o++;

                //        // Special Attack Flags
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        //attackData[o] = rnd.Next(0, 256); o++;
                //        attackData[o] = attackDataCurrent[o]; o++;
                //        attackData[o] = attackDataCurrent[o]; o++;
                //    }
                //    else
                //    {
                //        while (c < 28)
                //        {
                //            attackData[o] = attackDataCurrent[o]; o++;
                //            c++;
                //        }
                //        c = 0;
                //    }
                //    r++;
                //}
                //r = 0;

                //array = attackData.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x004C0;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                //#endregion

                //#region Attack IDs
                //while (r < 32)
                //{
                //    // Attack ID - These should match the ones referenced in AI and Animation Attack IDs
                //    //attackIDs[o] = rnd.Next(0, 256); o++;
                //    attackIDs[o] = attackIDsCurrent[o]; o++;
                //    attackIDs[o] = attackIDsCurrent[o]; o++;
                //    r++;
                //}
                //r = 0;

                //array = attackIDs.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x00840;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                #endregion

                #region Attack Names
                //while (c < 32)
                //{
                //    // Attack Name, 32 bytes ascii
                //    nameBytes = AllMethods.NameGenerate(rnd);
                //    attackNames[o] = nameBytes[0]; o++;
                //    attackNames[o] = nameBytes[1]; o++;
                //    attackNames[o] = nameBytes[2]; o++;
                //    attackNames[o] = nameBytes[3]; o++;
                //    rngID = rnd.Next(2); // Chance to append a longer name
                //    if (rngID == 1)
                //    {
                //        attackNames[o] = nameBytes[4]; o++;
                //        attackNames[o] = nameBytes[5]; o++;
                //        attackNames[o] = nameBytes[6]; o++;
                //        attackNames[o] = nameBytes[7]; o++;
                //    }
                //    else
                //    {
                //        attackNames[o] = 0; o++;
                //        attackNames[o] = 0; o++;
                //        attackNames[o] = 0; o++;
                //        attackNames[o] = 0; o++;
                //    }
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 0; o++;
                //    attackNames[o] = 255; o++; // Empty - Use FF to terminate the string
                //    c++;
                //}
                //array = attackNames.Select(b => (byte)b).ToArray();
                //bw.BaseStream.Position = 0x00880;
                //bw.Write(array, 0, array.Length);
                //o = 0;
                //c = 0;
                #endregion

                #region Formation AI Script Offsets
                // These need to match the location of each one
                //formationAIOffset[o] = 0; o++;
                //formationAIOffset[o] = 0; o++;
                //formationAIOffset[o] = 0; o++;
                //formationAIOffset[o] = 0; o++;
                //formationAIOffset[o] = formationAIOffsetCurrent[o]; o++;
                //formationAIOffset[o] = formationAIOffsetCurrent[o]; o++;
                //formationAIOffset[o] = formationAIOffsetCurrent[o]; o++;
                //formationAIOffset[o] = formationAIOffsetCurrent[o]; o++;

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
