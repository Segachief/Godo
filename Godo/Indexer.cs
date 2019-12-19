using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo
{
    public class Indexer
    {
        // Contains methods for acquiring scene data for calculations/assignment

        public static ArrayList GetCameraData(int[][] jaggedSceneInfo, string targetScene)
        {
            int r = 0;
            int o = 0;
            ArrayList listedCameraData = new ArrayList();

            while (r < 256)
            {
                int bytesRead;
                byte[] uncompressedScene = new byte[7808]; // Used to hold the decompressed scene file

                using (BinaryReader brg = new BinaryReader(new FileStream(targetScene, FileMode.Open)))
                {
                    // Calls method to convert little endian values into an integer
                    byte[] compressedScene = new byte[jaggedSceneInfo[o][1]]; // Used to hold the compressed scene file, where [o][1] is scene size        
                    brg.BaseStream.Seek(jaggedSceneInfo[o][2], SeekOrigin.Begin); // Starts reading the compressed scene file
                    brg.Read(compressedScene, 0, compressedScene.Length);

                    using (MemoryStream inputWrapper = new MemoryStream(compressedScene))
                    {
                        using (MemoryStream decompressedOutput = new MemoryStream())
                        {
                            using (GZipStream zipInput = new GZipStream(inputWrapper, CompressionMode.Decompress, true))
                            {
                                while ((bytesRead = zipInput.Read(uncompressedScene, 0, 7808)) != 0)
                                {
                                    decompressedOutput.Write(uncompressedScene, 0, bytesRead);
                                    // If this scene has valid camera data, then pull it out.
                                    byte[] camera = new byte[48];
                                    if (uncompressedScene[87] != 255 && uncompressedScene[88] != 255)
                                    {
                                        camera = uncompressedScene.Skip(88).Take(48).ToArray();
                                        listedCameraData.Add(camera);
                                    }
                                }
                                zipInput.Close();
                            }
                            decompressedOutput.Close();
                        }
                        inputWrapper.Close();
                    }
                    brg.Close();
                }
                r++;
                o++;
            }
            return listedCameraData;
        }

        public static int[][][] GetAttackData(int[][] jaggedSceneInfo, string targetScene)
        {
            int r = 0;
            int o = 0;
            int c = 0;
            int k = 0;
            int y = 0;
            int[][] jaggedAttackType = new int[1024][];             // Attack ID > Attack Type - Used to derive jaggedModelAttackTypes
            int[][][] jaggedModelAttackTypes = new int[3000][][];   // Model ID > Attack Type > Animation Indices

            while (r < 256)
            {
                int bytesRead;
                byte[] uncompressedScene = new byte[7808]; // Used to hold the decompressed scene file

                using (BinaryReader brg = new BinaryReader(new FileStream(targetScene, FileMode.Open)))
                {
                    // Calls method to convert little endian values into an integer
                    byte[] compressedScene = new byte[jaggedSceneInfo[o][1]]; // Used to hold the compressed scene file, where [o][1] is scene size        
                    brg.BaseStream.Seek(jaggedSceneInfo[o][2], SeekOrigin.Begin); // Starts reading the compressed scene file
                    brg.Read(compressedScene, 0, compressedScene.Length);

                    using (MemoryStream inputWrapper = new MemoryStream(compressedScene))
                    {
                        using (MemoryStream decompressedOutput = new MemoryStream())
                        {
                            using (GZipStream zipInput = new GZipStream(inputWrapper, CompressionMode.Decompress, true))
                            {
                                while ((bytesRead = zipInput.Read(uncompressedScene, 0, 7808)) != 0)
                                {
                                 /* Step 1: Create an array with all AttackIDs and AttackTypes
                                  * To determine attack type, we check the Impact Effect ID (phys) and Attack Effect ID (mag).
                                  * If either are FF then we can assume it is the other type. If both are FF, it is a Misc.
                                  * 0 = Phys, 1 = Mag, 2 = Misc
                                  */

                                    while (c < 32) // Iterate through all 32 entries for attack data in the scene
                                    {
                                        decompressedOutput.Write(uncompressedScene, 0, bytesRead);
                                        byte[] attackID = new byte[2];
                                        int type;

                                        // Checks AttackID isn't blank and then takes it, converts it into Int for array index
                                        if (uncompressedScene[2112 + k] != 255)
                                        {
                                            attackID = uncompressedScene.Skip(2112 + k).Take(2).ToArray();
                                            int attackIDInt = AllMethods.GetLittleEndianIntTwofer(attackID, 0);

                                            // Checks anim and impact to determine attack type
                                            if (uncompressedScene[1217 + y] != 255)
                                            {
                                                type = 0; // Assigns this AttackID as Physical
                                                jaggedAttackType[attackIDInt] = new int[] { type };
                                            }
                                            else if (uncompressedScene[1229 + y] != 255)
                                            {
                                                type = 1; // Assigns this AttackID as Magic
                                                jaggedAttackType[attackIDInt] = new int[] { type };
                                            }
                                            else
                                            {
                                                type = 2; // Assigns this AttackID as Misc
                                                jaggedAttackType[attackIDInt] = new int[] { type };
                                            }
                                        }
                                        c++;
                                        k += 2;
                                        y += 28;
                                    }
                                    c = 0;
                                    k = 0;
                                    y = 0;


                                    /* Step 2: Create an array that has the ModelIDs and their valid animations sorted into AttackTypes
                                     * To build an array of valid animation indexes for an enemy, we need to get a record of what anim indexes
                                     * have already been set for each of its associated attacks.
                                     * 0 = Phys, 1 = Mag, 2 = Misc
                                     */

                                    int enemyCount = 0;
                                    while (enemyCount < 3) // Iterates through the 3 registerable enemy slots in this scene
                                    {

                                        //if (r == 194 && enemyCount == 1)
                                        //{
                                        //    int breakpointCatcher = 0;
                                        //}

                                        decompressedOutput.Write(uncompressedScene, 0, bytesRead);
                                        byte[] modelID = new byte[2];
                                        byte[] attackID = new byte[2];
                                        byte[] animID = new byte[1];
                                        int attackCount = 0;

                                        // Checks if enemy ID is Null/FFFF
                                        if (uncompressedScene[c + 1] != 255)
                                        {
                                            modelID = uncompressedScene.Skip(c).Take(2).ToArray();
                                            int modelIDInt = AllMethods.GetLittleEndianIntTwofer(modelID, 0);
                                            jaggedModelAttackTypes[modelIDInt] = new int[3][]; // 3 entries for 3 attack types
                                            int[] physAnims = new int[16];
                                            int[] magAnims = new int[16];
                                            int[] miscAnims = new int[16];
                                            int physCount = 0;
                                            int magCount = 0;
                                            int miscCount = 0;

                                            while (attackCount < 16) // Iterates through the 16 registerable attack slots of this enemy
                                            {
                                                // Checks AttackID isn't blank and then takes it, converts it into Int for array index
                                                if (uncompressedScene[736 + k] != 255 && uncompressedScene[736 + k] != 255)
                                                {
                                                    attackID = uncompressedScene.Skip(736 + k).Take(2).ToArray();
                                                    int attackIDInt = AllMethods.GetLittleEndianIntTwofer(attackID, 0);

                                                    // Checks if an Anim was set for this AttackID (99% of cases one will be)
                                                    if (uncompressedScene[720 + y] != 255)
                                                    {
                                                        animID = uncompressedScene.Skip(720 + y).Take(1).ToArray();
                                                        
                                                        if (jaggedAttackType[attackIDInt][0] == 0) // Attack Type is physical
                                                        {
                                                            physAnims[physCount] = animID[0];
                                                            physCount++;
                                                        }
                                                        else if (jaggedAttackType[attackIDInt][0] == 1) // Attack type is magical
                                                        {
                                                            magAnims[magCount] = animID[0];
                                                            magCount++;
                                                        }
                                                        else if (jaggedAttackType[attackIDInt][0] == 2) // Attack type is miscellaneous
                                                        {
                                                            miscAnims[miscCount] = animID[0];
                                                            miscCount++;
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Error: An animation was not assigned correctly");
                                                        }
                                                    }
                                                }
                                                k += 2; // Tracks location of AttackID
                                                y++;    // Tracks location of Animation Indice
                                                attackCount++;
                                            }
                                            // Places the phys, mag, misc animations collected so far and places them into the jagged array
                                            jaggedModelAttackTypes[modelIDInt][0] = new int[] { physAnims[0], physAnims[1], physAnims[2], physAnims[3], physAnims[4], physAnims[5], physAnims[6], physAnims[7], physAnims[8], physAnims[9], physAnims[10], physAnims[11], physAnims[12], physAnims[13], physAnims[14], physAnims[15]};
                                            jaggedModelAttackTypes[modelIDInt][1] = new int[] { magAnims[0], magAnims[1], magAnims[2], magAnims[3], magAnims[4], magAnims[5], magAnims[6], magAnims[7], magAnims[8], magAnims[9], magAnims[10], magAnims[11], magAnims[12], magAnims[13], magAnims[14], magAnims[15] };
                                            jaggedModelAttackTypes[modelIDInt][2] = new int[] { miscAnims[0], miscAnims[1], miscAnims[2], miscAnims[3], miscAnims[4], miscAnims[5], miscAnims[6], miscAnims[7], miscAnims[8], miscAnims[9], miscAnims[10], miscAnims[11], miscAnims[12], miscAnims[13], miscAnims[14], miscAnims[15] };
                                        }
                                        else // No enemy, so we move to the data for the next one
                                        {
                                            k += 184; // Tracks location of AttackID
                                            y += 200; // Tracks location of Animation Indice
                                        }

                                        c += 2;          // Next enemy ID offset
                                        attackCount = 0;
                                        enemyCount++;
                                        k += 152;
                                        y += 168;
                                    }
                                    c = 0;
                                    k = 0;
                                    y = 0;
                                }
                                zipInput.Close();
                            }
                            decompressedOutput.Close();
                        }
                        inputWrapper.Close();
                    }
                    brg.Close();
                }
                r++;
                o++;
            }
            return jaggedModelAttackTypes;
        }
    }
}


