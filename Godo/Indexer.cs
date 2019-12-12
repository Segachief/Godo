using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static ArrayList GetAttackData(int[][] jaggedSceneInfo, string targetScene)
        {
            int r = 0;
            int o = 0;
            int c = 0;
            int k = 0;
            int y = 0;
            ArrayList listedAttackData = new ArrayList();
            int[][][] jaggedModelAttackAnim = new int[3000][][]; // Model ID > AttackID > Animation Indice
            int[][] jaggedAttackType = new int[1024][]; // Attack ID > Attack Type
            int[][][] jaggedModelAttackTypes = new int[3000][][]; // Model ID > Attack Type > Animation Indices - Target to Return

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
                                  * 
                                  * Rules
                                  * ) Any attack ID that has a value less than 0100 is a kernel-derived attack and is mag-type.
                                  * ) Duplicate attacks overwrite each other, keeping consistency.
                                  * ) Vanilla attacks don't exceed 0400 but have made array large enough for IDs up to FFFE in case of modded scene.bin
                                  *       But I suspect 0400h (1024) will be the actual limit for Attack IDs.
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
                                            if (uncompressedScene[1218 + y] != 255)
                                            {
                                                type = 0; // Assigns this AttackID as Physical
                                                jaggedAttackType[attackIDInt] = new int[] { type };
                                            }
                                            else if (uncompressedScene[1230 + y] != 255)
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


                                    /* Step 2: Create an array with all AttackIDs and associated Animation Indexes
                               * To build an array of valid animation indexes for an enemy, we need to get a record of what anim indexes
                               * have already been set for each of its associated attacks. This data can then be used to 
                               * 0 = Phys, 1 = Mag, 2 = Misc
                               * 
                               * Rules
                               * ) Any attack ID that has a value less than 0100 is a kernel-derived attack and is mag-type.
                               * ) Duplicate attacks overwrite each other, keeping consistency.
                               * ) Vanilla attacks don't exceed 0400 but have made array large enough for IDs up to FFFE in case of modded scene.bin
                               *       But I suspect 0400h (1024) will be the actual limit for Attack IDs. 
                               */

                                    int enemyCount = 0;
                                    while (enemyCount < 3) // Iterates through the 3 registerable enemy slots in this scene
                                    {
                                        decompressedOutput.Write(uncompressedScene, 0, bytesRead);
                                        byte[] modelID = new byte[2];
                                        byte[] attackID = new byte[2];
                                        byte[] animID = new byte[1];
                                        int attackCount = 0;

                                        // Checks if enemy ID is null
                                        if (uncompressedScene[c] != 255 && uncompressedScene[c + 1] != 255)
                                        {
                                            modelID = uncompressedScene.Skip(k).Take(2).ToArray();
                                            int modelIDInt = AllMethods.GetLittleEndianIntTwofer(modelID, 0);

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

                                                        // The 3D Jagged Array specifies the ModelID as the initial indice with storage for any AttackID.
                                                        // The 2nd indice is the attack ID, which contains a value (this model's animation ID for the attack).
                                                        // Later, when we match this to the other array we can check ModelID and AttackID between them for a match.
                                                        jaggedModelAttackAnim[modelIDInt] = new int[1024][];
                                                        jaggedModelAttackAnim[modelIDInt][attackIDInt] = new int[1];
                                                        jaggedModelAttackAnim[modelIDInt][attackIDInt][0] = animID[0];
                                                    }
                                                }
                                                // What we're doing is checking two separate lists that are in different locations, but which rely on each other.
                                                // One is the Attack ID registered to an enemy, the other is the Animation Index to use with that Attack ID.
                                                // One is 2-bytes, one is 1-byte, but they're in separate places so need to iterate through them separately too.
                                                k += 2; // Gets us to the next registered attack offset
                                                y++;
                                                attackCount++;
                                            }
                                            jaggedModelAttackTypes[modelIDInt] = new int[3][];
                                            //jaggedModelAttackTypes[modelIDInt][attackIDInt] = new int[];

                                        // JaggedAttackType: AttackID > AttackType
                                        // JaggedModelAttackAnim: ModelID > AttackID > AnimIndice

                                        //So: Where JaggedModelAttackAnim.ModelID.AttackID == jaggedAttackType.AttackID
                                        // If JaggedAttackType == 0
                                        // Put JaggedModelAttackAnim.AnimIndice into JaggedModelAttackType.AnimType[0]
                                        
                                        // AttackID is the the point of association.

                                        }
                                        else // No enemy, so we move to the data for the next one
                                        {
                                            k += 32; // Tracks location of AttackID
                                            y += 16; // Tracks location of Animation Indice
                                        }

                                        c += 2; // Next enemy ID offset
                                        attackCount = 0; // Reset attack counter
                                        enemyCount++; // Reset attack counter
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

            // So we use these two jagged arrays to create an additional array that contains:
            // Model ID[#] > AnimType[3] > {Phys}, {Mag}, {Misc}
            // jaggedModelAttackAnim[modelIDInt][attackIDInt][0] = animID[0];
            // jaggedAttackType[attackIDInt] = new int[] { type };


            return listedAttackData;
        }
    }
}


