using Godo.FormsEnemyData;
using Godo.Helper;
using Godo.Indexing;
using Godo.Infrastructure.Scene;
using Godo.Omnichange;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo.Infrastructure
{
    public class SceneProcessing
    {
        // Randomises the Scene.Bin
        public static byte[] RandomiseScene(byte[] data, byte[] camera, int sceneID,
            bool[] swapOptions,
            bool[] enemyStatOptions, int[] enemyStatParameters,
            bool[] enemyAttackOptions, int[] enemyAttackParameters,
            bool[] enemyItemOptions,
            bool[] formationOptions,
            bool[] balancingOptions, int[] balancingParameters,
            bool[] challengeOptions,
            bool[] specialHackOptions, int[] specialHackParameters,
            Random rnd, int[][][] jaggedModelAttackTypes, byte[] initCam, byte[][] jaggedEnemyData,
            bool[] interimOptions)
        {
            /* Scene File Breakdown
             * The scene.bin comprises of 256 indvidual 'scene' files in a gzip format. Each scene contains 3 enemies and 4 formations.
             * The size of each scene is the same, as any unused data is padded with FF.
             */

            // Identifies where a try-catch was triggered in the scene
            string error = "";

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

                int r = 0; // For iterating scene records (256 of them)
                int o = 0; // For iterating array indexes
                int k = 0; // See above

                bool excludedScene = false;
                bool excludedModel = false;
                bool excludeSwarm = false;
                bool swapInModel = false;
                bool bossGroup = false;
                bool bossInScene = false;
                bool validModel = false;

                // Used to ascertain which models were swapped for which when writing to formation
                ulong enemyA = 0;
                ulong enemyB = 0;
                ulong enemyC = 0;

                byte[] nameBytes = new byte[8]; // For assigning FF7 Ascii bytes after method processing
                                                //Random rnd = new Random(Guid.NewGuid().GetHashCode()); // TODO: Have it take a seed as argument

                // Two formations to handle 6 enemies each; A: two line, B: triangle
                ArrayList listedFormationData = new ArrayList();
                byte[] formationA =
                {
                    0xEC, 0xFA, 0x00, 0x00, 0x88, 0xFA, 0x01, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x50, 0xFB, 0x01, 0x00, 0x00, 0x00,
                    0x14, 0x05, 0x00, 0x00, 0x88, 0xFA, 0x01, 0x00, 0x00, 0x00,
                    0xEC, 0xFA, 0x00, 0x00, 0x10, 0xF5, 0x02, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x10, 0xF5, 0x02, 0x00, 0x00, 0x00,
                    0x14, 0x05, 0x00, 0x00, 0x10, 0xF5, 0x02, 0x00, 0x00, 0x00
                };

                byte[] formationB =
                {
                    0x00, 0x00, 0x00, 0x00, 0x50, 0xFB, 0x01, 0x00, 0x00, 0x00,
                    0x0C, 0xFE, 0x00, 0x00, 0x68, 0xF7, 0x01, 0x00, 0x00, 0x00,
                    0xF4, 0x01, 0x00, 0x00, 0x68, 0xF7, 0x01, 0x00, 0x00, 0x00,
                    0x18, 0xFC, 0x00, 0x00, 0x80, 0xF3, 0x02, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x80, 0xF3, 0x02, 0x00, 0x00, 0x00,
                    0xE8, 0x03, 0x00, 0x00, 0x80, 0xF3, 0x02, 0x00, 0x00, 0x00
                };

                listedFormationData.Add(formationA);
                listedFormationData.Add(formationB);
                int rand = (byte)rnd.Next(listedFormationData.Count);
                byte[] form = (byte[])listedFormationData[rand];

                int[][] jaggedAttackType = new int[1280][];
                int enemyAttackListOffset = 736;

                // If first enemy is null, assume an empty/unused scene and skip entire assignment
                if (data[o] != 255 && data[o + 1] != 255)
                {
                    #region Enemy IDs

                    //error = "Enemy IDs";
                    //// Enemy IDs - Model Swap
                    //while (r < 3)
                    //{
                    //    byte[] currentModelID = new byte[2];
                    //    currentModelID[0] = data[o];
                    //    currentModelID[1] = data[o + 1];
                    //    ulong currentModelIDInt = (ulong)EndianConvert.GetLittleEndianIntTwofer(currentModelID, 0);

                    //    // Stores the original Model ID for potential use later in Battle Formation section
                    //    if (r == 0)
                    //    {
                    //        enemyA = currentModelIDInt;
                    //    }
                    //    else if (r == 1)
                    //    {
                    //        enemyB = currentModelIDInt;
                    //    }
                    //    else if (r == 2)
                    //    {
                    //        enemyC = currentModelIDInt;
                    //    }

                    //    // Check what the original model's requirements are; if its ID matches any in these lists,
                    //    // it has certain rules applied to its potential replacement (if allowed).

                    //    // Models excluded from being swapped out
                    //    excludedModel = ModelFilters.CheckExcludedModel(currentModelIDInt);

                    //    // Models excluded from being swapped out - If ever set to true, will not check again for this scene
                    //    if (excludeSwarm == false)
                    //    {
                    //        excludeSwarm = ModelFilters.CheckExcludedModel(currentModelIDInt);
                    //    }

                    //    // Enemies that require multiple idle/damaged animations or other requirements
                    //    //swapInModel = ModelFilters.CheckSwapIn(currentModelIDInt);

                    //    // Check if the current Model ID matches a Boss Model ID
                    //    // Also used for stat calculation and other options
                    //    bossGroup = ModelFilters.CheckBossSet(currentModelIDInt);
                    //    if(bossGroup == true)
                    //    {
                    //        bossInScene = true;
                    //    }

                    //    do // Checks that model ID assigned exists/is valid - Terminates when validModel is True
                    //    {
                    //        // Model Swap options
                    //        if (swapOptions[0])
                    //        {
                    //            if (excludedModel == true)
                    //            {
                    //                // Current model is excluded from being swapped
                    //                o += 2;
                    //                validModel = true;
                    //            }
                    //            else
                    //            {
                    //                // Swap is made and checked until a valid Model ID is picked
                    //                while (validModel != true)
                    //                {
                    //                    ulong modelIDCheck = (ulong)rnd.Next(676);
                    //                    excludedModel = ModelFilters.CheckExcludedModel(modelIDCheck);
                    //                    if (jaggedModelAttackTypes[modelIDCheck] != null && excludedModel != true)
                    //                    {
                    //                        byte[] model = EndianConvert.GetLittleEndianConvert(modelIDCheck);
                    //                        data[o] = model[0]; o++;
                    //                        data[o] = model[1]; o++;
                    //                        validModel = true;
                    //                    }
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            // Model Swap not enabled, proceed without reallocation
                    //            validModel = true;
                    //            o += 2;
                    //        }
                    //    } while (validModel != true);
                    //    validModel = false;
                    //    r++;
                    //}
                    //Skipping, delete this when re-enabling
                    o += 6;

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
                    data[o] = 255;
                    o++;
                    data[o] = 255;
                    o++;

                    //array = enemyIDs.Select(b => (byte)b).ToArray();
                    //bw.BaseStream.Position = 0x00000;
                    //bw.Write(array, 0, array.Length);
                    //o = 0;

                    #endregion

                    #region Battle Setup Flags

                    error = "Battle Setup Flags";
                    //bw.BaseStream.Position = 0x00008;
                    // Sends Data and Position (along with flags) to modify this section of data

                    //Disabled until later phase
                    //Formation.SceneFormation(data, o, formationOptions, specialHackOptions, bossGroup, initCam, k, rnd);
                    o += 80;

                    #endregion

                    #region Camera Placement Data

                    error = "Camera Placement";
                    //bw.BaseStream.Position = 0x00058;

                    //Disabled until later phase
                    //CameraPlacement.SceneCamera(data, o, formationOptions, k, camera);
                    o += 192;

                    #endregion

                    #region Battle Formation Data

                    error = "Battle Formation";
                    //bw.BaseStream.Position = 0x00118;

                    //Disabled until later phase
                    //if (excludedScene != true)
                    //{
                    //    BattleFormation.SceneBattleFormation(data, o, specialHackOptions, specialHackParameters,
                    //        form, sceneID, enemyIDList, bossInScene, excludeSwarm, enemyA, enemyB, enemyC);
                    //}
                    o += 384;

                    #endregion


                    #region Enemy Data

                    error = "Enemy Data";
                    //bw.BaseStream.Position = 0x00298;

                    //Builds an array that assigns attack types to each attack stored in this scene
                    if (swapOptions[0] || swapOptions[1] || swapOptions[2])
                    {
                        jaggedAttackType = EnemyData.SceneAttackArray(data, jaggedAttackType);
                    }

                    EnemyData.SceneEnemyData(data, o, nameBytes, rnd,
                            enemyStatOptions, enemyStatParameters,
                            balancingOptions, balancingParameters,
                            challengeOptions,
                            enemyItemOptions, specialHackOptions,
                            swapOptions, excludedScene, excludedModel, swapOptions, swapInModel, bossGroup,
                            enemyIDList, enemyAttackListOffset, jaggedAttackType, jaggedModelAttackTypes,
                            sceneID, interimOptions
                        );


                    EnemyHelper.EnemyConsistency(enemyIDList, jaggedEnemyData, data, o);
                    o += 552;
                    #endregion

                    #region Attack Data
                    error = "Attack Data";
                    //bw.BaseStream.Position = 0x004C0;

                    //Disabled until later phase
                    //AttackData.SceneAttacks(data, o, rnd, enemyAttackOptions, enemyAttackParameters, sceneID);
                    o += 960; // Includes AttackID iteration
                    #endregion

                    #region Attack IDs
                    //error = "Attack IDs";
                    ////bw.BaseStream.Position = 0x00840;
                    //while (r < 32)
                    //{
                    //    // Attack ID - These should match the ones referenced in AI and Animation Attack IDs
                    //    //attackIDs[o] = rnd.Next(0, 256); o++;
                    //    data[o] = data[o]; o++;
                    //    data[o] = data[o]; o++;
                    //    r++;
                    //}
                    //r = 0;
                    #endregion
                    o += 64;

                    #region Attack Names
                    error = "Attack Names";
                    //bw.BaseStream.Position = 0x00880;

                    //Disabled until later phase
                    //AttackNames.SceneAttackNames(data, o, rnd, enemyAttackOptions);
                    o += 1024;
                    #endregion

                    #region Formation AI Script Offsets
                    error = "Formation AI Offsets";
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
                    error = "Formation AI";
                    // This is likely best served from a notepad containing AI scripts, though formation AI itself is very rarely used (Final Sephiroth fight)
                    //array = formationAI.Select(b => (byte)b).ToArray();
                    //bw.BaseStream.Position = 0x000C88;
                    //bw.Write(array, 0, array.Length);
                    //o = 0;
                    #endregion

                    #region Enemy AI Offsets
                    error = "Enemy AI Offsets";
                    // These need to match the location of each one
                    //if(options[45] != false){}
                    //enemyAIOffset[o] = 0; o++;
                    //enemyAIOffset[o] = 0; o++;
                    //enemyAIOffset[o] = 0; o++;

                    //array = enemyAIOffset.Select(b => (byte)b).ToArray();
                    //bw.BaseStream.Position = 0x000E80;
                    //bw.Write(array, 0, array.Length);
                    //o = 0;
                    #endregion

                    #region Enemy AI
                    error = "Enemy AI";
                    // This is likely best served from a notepad containing AI scripts
                    //array = formationAI.Select(b => (byte)b).ToArray();
                    //bw.BaseStream.Position = 0x000E86;
                    //bw.Write(array, 0, array.Length);
                    #endregion
                    //}
                }
                else
                {
                    // If first enemy ID is null, assume scene is unused; no operation
                }
            }
            catch
            {
                MessageBox.Show("Scene ID: " + sceneID + " has failed to randomise; point of error: " + error);
            }
            return data;
        }
    }
}