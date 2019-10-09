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
        public static void RandomiseScene(string fileName)
        {
            /* Scene File Breakdown
             * The scene.bin comprises of 256 indvidual 'scene' files in a gzip format. Each scene contains 3 enemies and 4 formations.
             * The size of each scene is the same, as any unused data is padded with FF.
             */

            try
            {
                using (BinaryWriter bw = new BinaryWriter(File.Open(fileName, FileMode.Open)))
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
                                                                //TODO Consider using an array of varying size to handle the AI, although probably be better off leaving it alone
                                                                // If need to change attack IDs in the AI then could use a Dictionary or something to search out the value of 61 ##ID or similar to make the change.

                    int r = 0; // For iterating scene records (256 of them)
                    int o = 0; // For iterating array indexes
                    int c = 0; // For iterating records
                    int k = 0; //


                    //Note: Remember to call the index in the array for repeating data, such as Enemy IDs in the formation placement data.

                    byte[] array; // For conversion of int array to byte array
                    byte[] nameBytes; // For assigning FF7 Ascii bytes after method processing
                    Random rnd = new Random(Guid.NewGuid().GetHashCode()); // TODO: Have it take a seed as argument


                    while (r <= 0) // Iterates 256 times for each scene, temporarily disabled as only editing 1 file at a time currently
                    {
                        #region Enemy IDs
                        // Enemy ID A
                        enemyIDs[o] = rnd.Next(2); o++;
                        enemyIDs[o] = rnd.Next(256); o++;

                        // Enemy ID B
                        enemyIDs[o] = rnd.Next(2); o++;
                        enemyIDs[o] = rnd.Next(256); o++;

                        // Enemy ID C
                        enemyIDs[o] = rnd.Next(2); o++;
                        enemyIDs[o] = rnd.Next(256); o++;
                        //o += 6;

                        array = enemyIDs.Select(b => (byte)b).ToArray();
                        bw.BaseStream.Position = 0x00000;
                        bw.Write(array, 0, array.Length);
                        o = 0;
                        #endregion

                        #region Battle Setup Flags
                        // Battle Location
                        battleSetup[o] = rnd.Next(60); o++;
                        battleSetup[o] = 0; o++; // Always 0; despite being a 2-byte value, valid values never exceed 59h

                        // Next Formation ID, this transitions to another enemy formation directly after current enemies defeated; like Battle Square but not random.
                        battleSetup[o] = 255; o++; // FFFF by default, no new battle will load
                        battleSetup[o] = 255; o++;

                        // Escape Counter; value of 0009 makes battle unescapable; 2-byte but value never exceeds 0009
                        battleSetup[o] = 01; o++;
                        battleSetup[o] = 00; o++;

                        // Unused - 2byte
                        battleSetup[o] = 255; o++;
                        battleSetup[o] = 255; o++;

                        // Battle Square - Possible Next Battles (4x 2-byte formation IDs, one is selected at random; default value for no battle is 03E7
                        battleSetup[o] = 231; o++;
                        battleSetup[o] = 03; o++; // Value of 03E7h

                        battleSetup[o] = 231; o++;
                        battleSetup[o] = 03; o++;

                        battleSetup[o] = 231; o++;
                        battleSetup[o] = 03; o++;

                        battleSetup[o] = 231; o++;
                        battleSetup[o] = 03; o++;

                        // Escapable Flag (misc flags such as disabling pre-emptive)
                        battleSetup[o] = 253; o++;
                        battleSetup[o] = 255; o++; // Value of FDFF; FE F9 would prevent pre-emptives

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
                        battleSetup[o] = 0; o++;

                        // Indexed pre-battle camera position (where the camera starts from when battle loads in)
                        battleSetup[o] = rnd.Next(256); o++;

                        array = battleSetup.Select(b => (byte)b).ToArray();
                        bw.BaseStream.Position = 0x00008;
                        bw.Write(array, 0, array.Length);
                        o = 0;
                        #endregion

                        #region Camera Placement Data

                        // Primary Battle Idle Camera Position
                        cameraData[o] = rnd.Next(256); o++; // Camera X Position
                        cameraData[o] = rnd.Next(256); o++;

                        cameraData[o] = rnd.Next(256); o++; // Camera Y Position
                        cameraData[o] = rnd.Next(256); o++;

                        cameraData[o] = rnd.Next(256); o++; // Camera Z Position
                        cameraData[o] = rnd.Next(256); o++;

                        cameraData[o] = rnd.Next(256); o++; // Camera X Direction
                        cameraData[o] = rnd.Next(256); o++;

                        cameraData[o] = rnd.Next(256); o++; // Camera y Direction
                        cameraData[o] = rnd.Next(256); o++;

                        cameraData[o] = rnd.Next(256); o++; // Camera Z Direction
                        cameraData[o] = rnd.Next(256); o++;


                        // Secondary Battle Idle Camera Position
                        cameraData[o] = rnd.Next(256); o++; // Camera X Position
                        cameraData[o] = rnd.Next(256); o++;

                        cameraData[o] = rnd.Next(256); o++; // Camera Y Position
                        cameraData[o] = rnd.Next(256); o++;

                        cameraData[o] = rnd.Next(256); o++; // Camera Z Position
                        cameraData[o] = rnd.Next(256); o++;

                        cameraData[o] = rnd.Next(256); o++; // Camera X Direction
                        cameraData[o] = rnd.Next(256); o++;

                        cameraData[o] = rnd.Next(256); o++; // Camera y Direction
                        cameraData[o] = rnd.Next(256); o++;

                        cameraData[o] = rnd.Next(256); o++; // Camera Z Direction
                        cameraData[o] = rnd.Next(256); o++;


                        // Tertiary Battle Idle Camera Position
                        cameraData[o] = rnd.Next(256); o++; // Camera X Position
                        cameraData[o] = rnd.Next(256); o++;

                        cameraData[o] = rnd.Next(256); o++; // Camera Y Position
                        cameraData[o] = rnd.Next(256); o++;

                        cameraData[o] = rnd.Next(256); o++; // Camera Z Position
                        cameraData[o] = rnd.Next(256); o++;

                        cameraData[o] = rnd.Next(256); o++; // Camera X Direction
                        cameraData[o] = rnd.Next(256); o++;

                        cameraData[o] = rnd.Next(256); o++; // Camera y Direction
                        cameraData[o] = rnd.Next(256); o++;

                        cameraData[o] = rnd.Next(256); o++; // Camera Z Direction
                        cameraData[o] = rnd.Next(256); o++;


                        // Unused Battle Camera Position - FF Padding
                        for (int i = 0; i < 12; i++)
                        {
                            cameraData[o] = 256; o++;
                        }

                        array = cameraData.Select(b => (byte)b).ToArray();
                        bw.BaseStream.Position = 0x00058;
                        bw.Write(array, 0, array.Length);
                        o = 0;
                        #endregion

                        #region Battle Formation Data

                        // Sets the enemy IDs established at beginning of the scene so they can be referenced by formation array
                        int enemyA1 = enemyIDs[o]; o++; // First byte of the ID
                        int enemyA2 = enemyIDs[o]; o++; // Second byte of the ID
                        int enemyB1 = enemyIDs[o]; o++; // Hacky? Byte me
                        int enemyB2 = enemyIDs[o]; o++;
                        int enemyC1 = enemyIDs[o]; o++;
                        int enemyC2 = enemyIDs[o]; o++;
                        int[] enemyIDList = new int[6];
                        enemyIDList[0] = enemyA1;
                        enemyIDList[1] = enemyA2;
                        enemyIDList[2] = enemyB1;
                        enemyIDList[3] = enemyB2;
                        enemyIDList[4] = enemyC1;
                        enemyIDList[5] = enemyC2;
                        int rngID = 0;

                        // Enemy ID for this placement (there are 6 possible placements total)
                        rngID = rnd.Next(2);
                        formationPlacement[o] = enemyIDList[rngID]; o++;

                        // We don't want placement coordinates if the enemy ID is 'null' (FF FF)
                        if (formationPlacement[o - 1] != 255 && formationPlacement[o - 2] != 255)
                        {
                            // X Coordinate
                            formationPlacement[o] = rnd.Next(256); o++;
                            formationPlacement[o] = rnd.Next(256); o++;

                            // Y Coordinate
                            formationPlacement[o] = rnd.Next(256); o++;
                            formationPlacement[o] = rnd.Next(256); o++;

                            // Z Coordinate
                            formationPlacement[o] = rnd.Next(256); o++;
                            formationPlacement[o] = rnd.Next(256); o++;

                            // Row
                            formationPlacement[o] = rnd.Next(256); o++;
                            formationPlacement[o] = rnd.Next(256); o++;

                            // Cover Flags (should be related to Row)
                            formationPlacement[o] = rnd.Next(256); o++;
                            formationPlacement[o] = rnd.Next(256); o++;

                            // Initial Condition Flags; only the last 5 bits are considered - FF FF FF FF is default
                            formationPlacement[o] = 255; o++;
                            formationPlacement[o] = 255; o++;
                            formationPlacement[o] = 255; o++;
                            formationPlacement[o] = 255; o++;
                        }

                        array = formationPlacement.Select(b => (byte)b).ToArray();
                        bw.BaseStream.Position = 0x00118;
                        bw.Write(array, 0, array.Length);
                        o = 0;
                        #endregion

                        #region Enemy Data

                        while (c < 3)
                        {
                            // Enemy Name, 32 bytes ascii
                            nameBytes = AllMethods.NameGenerate(rnd);
                            enemyData[o] = nameBytes[0]; o++;
                            enemyData[o] = nameBytes[1]; o++;
                            enemyData[o] = nameBytes[2]; o++;
                            enemyData[o] = nameBytes[3]; o++;
                            enemyData[o] = nameBytes[4]; o++;
                            enemyData[o] = nameBytes[5]; o++;
                            enemyData[o] = nameBytes[6]; o++;
                            enemyData[o] = nameBytes[7]; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 0; o++;
                            enemyData[o] = 255; o++; // Empty - Use FF to terminate the string

                            // Enemy Level
                            enemyData[o] = rnd.Next(10, 63); o++;

                            // Enemy Speed
                            enemyData[o] = rnd.Next(10, 256); o++;

                            // Enemy Luck
                            enemyData[o] = rnd.Next(0, 256); o++;

                            // Enemy Evade
                            enemyData[o] = rnd.Next(10, 256); o++;

                            // Enemy Strength
                            enemyData[o] = rnd.Next(10, 256); o++;

                            // Enemy Defence
                            enemyData[o] = rnd.Next(10, 256); o++;

                            // Enemy Magic
                            enemyData[o] = rnd.Next(10, 256); o++;

                            // Enemy Magic Defence
                            enemyData[o] = rnd.Next(10, 256); o++;

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
                            enemyData[o] = rnd.Next(0, 63); o++; // 4 elemental/status properties have been set
                            enemyData[o] = rnd.Next(0, 63); o++;
                            enemyData[o] = rnd.Next(0, 63); o++;
                            enemyData[o] = rnd.Next(0, 63); o++;
                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;

                            // Elemental Rates/Modifiers
                            /*
                                00h - Death
                                02h - Double Damage
                                04h - Half Damage
                                05h - Nullify Damage
                                06h - Absorb 100%
                                07h - Full Cure
                                FFh - Nothing
                             */
                            enemyData[o] = rnd.Next(0, 7); o++;
                            enemyData[o] = rnd.Next(0, 7); o++;
                            enemyData[o] = rnd.Next(0, 7); o++;
                            enemyData[o] = rnd.Next(0, 7); o++;
                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;

                            // Action Animation Index
                            /* This needs a lot of logic to get running in a safe way. Each enemy Index needs to be loaded up with valid IDs for different types
                               of attack with random variances for instances where enemy has multiple animations for an attack type (Behemoth's 4 physicals for instance).
                               TODO: Pass the enemy ID from here to a method that can then check the valid animation indices, then return an array of values that can be
                               fed into our enemyData[] array for bytewriting.
                             */
                            o += 16;

                            // Enemy Attack IDs for matching to Animation IDs - 2bytes per attack ID
                            o += 32;

                            // Enemy Camera Override IDs for matching to Animation IDs - 2bytes per Camera Override ID - FFFF by default
                            for (int i = 0; i < 32; i++)
                            {
                                enemyData[o] = 255; o++;
                            }

                            // Obtain Rates
                            // 1 byte per item, 4 items. Values below 80 are Drop Items (#/63). Values above 80 are Steal Items (#63)
                            enemyData[o] = 255; o++; // Item 1
                            enemyData[o] = 255; o++; // Item 2
                            enemyData[o] = 255; o++; // Item 3
                            enemyData[o] = 255; o++; // Item 4

                            // Item IDs to be matched to the above drop/steal rates
                            // Item 1
                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;

                            // Item 2
                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;

                            // Item 3
                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;

                            // Item 4
                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;

                            // Manipulate/Berserk Attack IDs
                            // The first listed attack is the Berserk option; all 3 attacks can be selected for use under Manipulate
                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;

                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;

                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;

                            // Unknown Data
                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;

                            // Enemy MP
                            enemyData[o] = rnd.Next(0, 256); o++;
                            enemyData[o] = rnd.Next(0, 256); o++;

                            // Enemy AP
                            enemyData[o] = rnd.Next(0, 256); o++;
                            enemyData[o] = rnd.Next(0, 256); o++;

                            // Enemy Morph Item ID - FFFF means no morph
                            enemyData[o] = rnd.Next(0, 256); o++;
                            enemyData[o] = rnd.Next(0, 256); o++;

                            // Back Attack multiplier
                            enemyData[o] = rnd.Next(0, 33); o++;

                            // Alignment FF
                            enemyData[o] = 255; o++;

                            // Enemy HP
                            enemyData[o] = rnd.Next(0, 256); o++;
                            enemyData[o] = rnd.Next(0, 256); o++;
                            enemyData[o] = rnd.Next(0, 256); o++;
                            enemyData[o] = rnd.Next(0, 256); o++;

                            // EXP Points
                            enemyData[o] = rnd.Next(0, 256); o++;
                            enemyData[o] = rnd.Next(0, 256); o++;
                            enemyData[o] = rnd.Next(0, 256); o++;
                            enemyData[o] = rnd.Next(0, 256); o++;

                            // Gil
                            enemyData[o] = rnd.Next(0, 256); o++;
                            enemyData[o] = rnd.Next(0, 256); o++;
                            enemyData[o] = rnd.Next(0, 256); o++;
                            enemyData[o] = rnd.Next(0, 256); o++;


                            // Status Immunities
                            enemyData[o] = rnd.Next(0, 256); o++;
                            enemyData[o] = rnd.Next(0, 256); o++;
                            enemyData[o] = rnd.Next(0, 256); o++;
                            enemyData[o] = rnd.Next(0, 256); o++;

                            // Padding FF
                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;
                            enemyData[o] = 255; o++;

                            c++;
                        }

                        array = enemyIDs.Select(b => (byte)b).ToArray();
                        bw.BaseStream.Position = 0x00298;
                        bw.Write(array, 0, array.Length);
                        o = 0;
                        c = 0;
                        #endregion

                        #region Attack Data
                        while (c < 32)
                        {
                            // Attack %
                            attackData[o] = rnd.Next(0, 256); o++;

                            // Impact Effect ID - Must be FF if Attack Effect ID is not FF
                            attackData[o] = rnd.Next(0, 256); o++;

                            // Target Hurt Action Index
                            // 00 = Standard
                            // 01 = Stunned
                            // 02 = Heavy
                            // 03 = Ejected
                            attackData[o] = rnd.Next(0, 4); o++;

                            // Unknown
                            attackData[o] = 255; o++;

                            // Casting Cost
                            attackData[o] = rnd.Next(0, 256); o++;
                            attackData[o] = rnd.Next(0, 256); o++;

                            // Impact Sound - Must be FFFF if Attack Effect ID is not FF
                            attackData[o] = rnd.Next(0, 256); o++;
                            attackData[o] = rnd.Next(0, 256); o++;

                            // Camera Movement ID for single target - FFFF if none
                            attackData[o] = 255; o++;
                            attackData[o] = 255; o++;

                            // Camera Movement ID for multi target - FFFF if none
                            attackData[o] = 255; o++;
                            attackData[o] = 255; o++;

                            // Target Flags - Logic will be tough for this one; will depend on attack element + attack type as some aren't designed for multi-target
                            attackData[o] = rnd.Next(0, 256); o++;

                            // Attack Effect ID - Must be FF if Impact Effect is not FF
                            attackData[o] = rnd.Next(0, 256); o++;

                            // Damage Calculation
                            attackData[o] = rnd.Next(0, 256); o++;

                            // Base Power
                            attackData[o] = rnd.Next(0, 256); o++;

                            // Condition Sub-Menu Flags
                            // 00 = Party HP
                            // 01 = Party MP
                            // 02 = Party Status
                            // Other = None
                            attackData[o] = rnd.Next(0, 4); o++;

                            // Status Effect Change
                            // 00-3F = Chance to inflict/heal status (#/63)
                            // 40 = Remove Status
                            // 80 - Toggle Status
                            attackData[o] = rnd.Next(0, 4); o++;

                            // Attack Additional Effects
                            attackData[o] = rnd.Next(0, 256); o++;

                            // Additional Effects Modifier Value
                            attackData[o] = rnd.Next(0, 256); o++;

                            // Statuses
                            attackData[o] = rnd.Next(0, 256); o++;
                            attackData[o] = rnd.Next(0, 256); o++;
                            attackData[o] = rnd.Next(0, 256); o++;
                            attackData[o] = rnd.Next(0, 256); o++;

                            // Elements
                            attackData[o] = rnd.Next(0, 256); o++;
                            attackData[o] = rnd.Next(0, 256); o++;

                            // Special Attack Flags
                            attackData[o] = rnd.Next(0, 256); o++;
                            attackData[o] = rnd.Next(0, 256); o++;

                            c++;
                        }

                        array = attackData.Select(b => (byte)b).ToArray();
                        bw.BaseStream.Position = 0x004C0;
                        bw.Write(array, 0, array.Length);
                        o = 0;
                        c = 0;
                        #endregion

                        #region Attack IDs
                        while (c < 32)
                        {
                            // Attack ID - These should match the ones referenced in AI and Animation Attack IDs
                            attackIDs[o] = rnd.Next(0, 256); o++;
                            c++;
                        }
                        //array = attackIDs.Select(b => (byte)b).ToArray();
                        //bw.BaseStream.Position = 0x00880;
                        //bw.Write(array, 0, array.Length);
                        o = 0;
                        c = 0;
                        #endregion

                        #region Attack Names
                        while (c < 32)
                        {
                            // Attack Name, 32 bytes ascii
                            nameBytes = AllMethods.NameGenerate(rnd);
                            attackNames[o] = nameBytes[0]; o++;
                            attackNames[o] = nameBytes[1]; o++;
                            attackNames[o] = nameBytes[2]; o++;
                            attackNames[o] = nameBytes[3]; o++;
                            attackNames[o] = nameBytes[4]; o++;
                            attackNames[o] = nameBytes[5]; o++;
                            attackNames[o] = nameBytes[6]; o++;
                            attackNames[o] = nameBytes[7]; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 0; o++;
                            attackNames[o] = 255; o++; // Empty - Use FF to terminate the string
                            c++;
                        }
                        array = attackNames.Select(b => (byte)b).ToArray();
                        bw.BaseStream.Position = 0x00880;
                        bw.Write(array, 0, array.Length);
                        o = 0;
                        c = 0;
                        #endregion

                        #region Formation AI Script Offsets
                        // These need to match the location of each one
                        formationAIOffset[o] = 0; o++;
                        formationAIOffset[o] = 0; o++;
                        formationAIOffset[o] = 0; o++;
                        formationAIOffset[o] = 0; o++;

                        //array = formationAIOffsets.Select(b => (byte)b).ToArray();
                        //bw.BaseStream.Position = 0x000C80;
                        //bw.Write(array, 0, array.Length);
                        o = 0;
                        #endregion

                        #region Formation AI
                        // This is likely best served from a notepad containing AI scripts, though formation AI itself is very rarely used (Final Sephiroth fight)
                        //array = formationAI.Select(b => (byte)b).ToArray();
                        //bw.BaseStream.Position = 0x0000C88;
                        //bw.Write(array, 0, array.Length);
                        #endregion

                        #region Enemy AI Offsets
                        // These need to match the location of each one
                        enemyAIOffset[o] = 0; o++;
                        enemyAIOffset[o] = 0; o++;
                        enemyAIOffset[o] = 0; o++;

                        //array = enemyAIOffset.Select(b => (byte)b).ToArray();
                        //bw.BaseStream.Position = 0x000E80;
                        //bw.Write(array, 0, array.Length);
                        o = 0;
                        #endregion

                        #region Enemy AI
                        // This is likely best served from a notepad containing AI scripts
                        //array = formationAI.Select(b => (byte)b).ToArray();
                        //bw.BaseStream.Position = 0x0000E86;
                        //bw.Write(array, 0, array.Length);
                        #endregion

                        r++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error: Try-Catch failed");
            }
        }
    }
}
