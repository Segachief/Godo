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
            ArrayList listedCameraData = new ArrayList();

            // Is equal to the absolute upper limit of possible enemy models (023Ah/675 = ZZDA)
            int[][][] jaggedAttackInfo = new int[675][][];

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

                                    // Proposed logic:
                                    /* 1) Pull out the Model ID first, and check if it exists in the array. If it does, skip.
                                     * 
                                     * 2) Pull out the associated attack data and check if Impact Effect isn't FF (phys) or if Animation ID isn't FF (Mag); if both are, then it's a Misc.
                                     * This gives us 32 entries of 0, 1, or 2.
                                     *
                                     * 3) Look at the model's registered attacks; using the data from step 2, we can determine what anims the model has and which belong to which attack type.
                                     * A match has us add AnimID + '#' to a string that can be parsed later.
                                     * 
                                     * 4) When reassignment comes, we can check the new Model ID and its registered attacks then set an approproate animation.
                                     */
                                    int modelIndexA = 0;
                                    int modelIndexB = 0;
                                    int modelIndexC = 0;
                                    int attackIndex = 0;
                                    if (uncompressedScene[0] != 255 && uncompressedScene[1] != 255)
                                    {
                                        byte[] modelIDs = new byte[2];
                                        modelIDs[0] = uncompressedScene[0];
                                        modelIDs[1] = uncompressedScene[1];
                                        modelIndexA = AllMethods.GetLittleEndianInt(modelIDs, 0);
                                    }
                                    if (uncompressedScene[2] != 255 && uncompressedScene[3] != 255)
                                    {
                                        byte[] modelIDs = new byte[2];
                                        modelIDs[0] = uncompressedScene[0];
                                        modelIDs[1] = uncompressedScene[1];
                                        modelIndexB = AllMethods.GetLittleEndianInt(modelIDs, 0);
                                    }
                                    if (uncompressedScene[4] != 255 && uncompressedScene[5] != 255)
                                    {
                                        byte[] modelIDs = new byte[2];
                                        modelIDs[0] = uncompressedScene[0];
                                        modelIDs[1] = uncompressedScene[1];
                                        modelIndexC = AllMethods.GetLittleEndianInt(modelIDs, 0);
                                    }
                                    while (c < 32)
                                    {
                                        if (uncompressedScene[16] != 255 && uncompressedScene[17] != 255)
                                        {
                                            // Now we need to check the attacks and identify what they are
                                            byte[] attackIDs = new byte[2];
                                            attackIDs[0] = uncompressedScene[16];
                                            attackIDs[1] = uncompressedScene[17];
                                            attackIndex = AllMethods.GetLittleEndianInt(attackIDs, 0);

                                            if (uncompressedScene[255] != 255)
                                            {

                                            }

                                        }
                                    }
                                    /*
                                    In Enemy
                                        Attack Anim: 2D0, 388, 440
                                        Attack IDs:  2E0, 398, 450

                                    In Attack
                                        Attack Data
                                        Impact Effect ID: 4C2
                                        Attack Effect ID: 4CE
                                        Attack ID: 840, 2bytes x32
                                    */


                                    jaggedAttackInfo[modelIndexA] = new int[1][] { new[] { 3, 4 } }; // phys
                                    jaggedAttackInfo[modelIndexA] = new int[1][] { new[] { 5, 6 } }; // mag
                                    jaggedAttackInfo[modelIndexA] = new int[1][] { new[] { 7 } };    // misc



                                    if (uncompressedScene[0] != 255 && uncompressedScene[88] != 2)
                                    {
                                        //ModelIDs = uncompressedScene.Skip(0).Take(2).ToArray();
                                        //listedCameraData.Add(attacks);
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
    }
}


