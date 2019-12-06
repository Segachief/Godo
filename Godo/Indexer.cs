﻿using System;
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
            int s = 0;
            int z = 0;
            ArrayList listedAttackData = new ArrayList();

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

                            using (GZipStream zipInput = new GZipStream(inputWrapper, CompressionMode.Decompress, true))
                            {
                                while ((bytesRead = zipInput.Read(uncompressedScene, 0, 7808)) != 0)
                                {
                                    while (s < 3)
                                    {
                                        decompressedOutput.Write(uncompressedScene, 0, bytesRead);
                                        int[][][] jaggedAttacks = new int[65534][][];
                                        byte[] modelID = new byte[4];
                                        byte[] attackID = new byte[4];
                                        byte[] animID = new byte[4];

                                        // Checks if enemy ID is null
                                        if (uncompressedScene[z] != 255 && uncompressedScene[z + 1] != 255)
                                        {
                                            modelID = uncompressedScene.Skip(z).Take(2).ToArray();
                                            y = AllMethods.GetLittleEndianInt(modelID, 0);

                                            // Checks AttackID isn't blank and then takes it, converts it into Int for array index
                                            if (uncompressedScene[2112 + z] != 255 && uncompressedScene[2113 + z] != 255)
                                            {
                                                attackID = uncompressedScene.Skip(2112 + z).Take(2).ToArray();
                                                c = AllMethods.GetLittleEndianInt(attackID, 0);

                                                // Checks if an Anim was set for this AttackID (99% of cases one will be)
                                                if (uncompressedScene[736 + s] != 255)
                                                {
                                                    animID = uncompressedScene.Skip(736 + s).Take(1).ToArray();
                                                    k = AllMethods.GetLittleEndianInt(animID, 0);

                                                    // Using the Model ID, and the AttackID, as the array indices, we place the Animation Index Value in there.
                                                    // Now we need to figure out what kind of attack this is.
                                                    jaggedAttacks[y][c] = new int[] { k };
                                                }
                                            }
                                        }
                                        z += 2;
                                        s++;
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
            return listedAttackData;
        }
    }
}


