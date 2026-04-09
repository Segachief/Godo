using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Indexing
{
    public class CameraIndex
    {
        public static byte[] InitialCamera(bool[] formationOptions)
        {
            // Standardised camera
            if (formationOptions[0])
            {
                // Using dupe values for stability/testing, restore original later
                byte[] initialCamera =
                {
                //0x4A, 0x4A, 0x00, 0x2D, 0x03
                0x4A, 0x4A, 0x4A, 0x4A, 0x4A
                };
                return initialCamera;
            }
            // 1st-Person camera
            else if (formationOptions[1])
            {
                // Using dupe values for stability/testing, restore original later
                byte[] initialCamera =
                {
                //0x4A, 0x4A, 0x00, 0x2D, 0x03
                0x03, 0x03, 0x03, 0x03, 0x03
                };
                return initialCamera;
            }
            else
            {
                // Should never be able to fire
                byte[] initialCamera = new byte[5];
                return initialCamera;
            }
        }

        // This uses specific camera settings instead of all of them
        public static ArrayList GetCameraData(int[][] jaggedSceneInfo, string targetScene, bool[] formationOptions)
        {
            // Standardised camera
            if (formationOptions[0])
            {
                ArrayList listedCameraData = new ArrayList();

                byte[] cameraA =
                {
                0x98, 0x26, 0xF8, 0xF3, 0x6F, 0x14, 0x00, 0x00, 0xCE, 0xFE, 0x4C, 0xFD,
                0x10, 0x27, 0x78, 0xEC, 0x70, 0x17, 0x00, 0x00, 0x90, 0x01, 0x2C, 0x01,
                0x10, 0x27, 0x78, 0xEC, 0x70, 0x17, 0x00, 0x00, 0x90, 0x01, 0x2C, 0x01
            };

                byte[] cameraB =
                {

                0x62, 0x12, 0xB9, 0xFB, 0x11, 0x23, 0x00, 0x00, 0xDF, 0xFD, 0xAD, 0x00,
                0x10, 0x27, 0x78, 0xEC, 0x70, 0x17, 0x00, 0x00, 0x90, 0x01, 0x2C, 0x01,
                0x10, 0x27, 0x78, 0xEC, 0x70, 0x17, 0x00, 0x00, 0x90, 0x01, 0x2C, 0x01
            };

                byte[] cameraC =
               {
                0xCD, 0x15, 0x38, 0xFE, 0x47, 0x2F, 0x00, 0x00, 0x10, 0xFC, 0x2C, 0x01,
                0x10, 0x27, 0x78, 0xEC, 0x70, 0x17, 0x00, 0x00, 0x90, 0x01, 0x2C, 0x01,
                0x10, 0x27, 0x78, 0xEC, 0x70, 0x17, 0x00, 0x00, 0x90, 0x01, 0x2C, 0x01
            };

                byte[] cameraD =
               {
                0x25, 0x37, 0xF9, 0xF8, 0x9F, 0x19, 0x00, 0x00, 0x0F, 0xFC, 0x8C, 0xFF,
                0x10, 0x27, 0x78, 0xEC, 0x70, 0x17, 0x00, 0x00, 0x90, 0x01, 0x2C, 0x01,
                0x10, 0x27, 0x78, 0xEC, 0x70, 0x17, 0x00, 0x00, 0x90, 0x01, 0x2C, 0x01
            };

                byte[] cameraE =
               {
                0x00, 0x00, 0xF8, 0xF8, 0xAC, 0x28, 0x00, 0x00, 0xD0, 0xFE, 0xAC, 0xFC,
                0x10, 0x27, 0x78, 0xEC, 0x70, 0x17, 0x00, 0x00, 0x90, 0x01, 0x2C, 0x01,
                0x10, 0x27, 0x78, 0xEC, 0x70, 0x17, 0x00, 0x00, 0x90, 0x01, 0x2C, 0x01
            };

                listedCameraData.Add(cameraA);
                listedCameraData.Add(cameraB);
                listedCameraData.Add(cameraC);
                listedCameraData.Add(cameraD);
                listedCameraData.Add(cameraE);

                return listedCameraData;
            }
            // 1st-Person Camera
            else if (formationOptions[1])
            {
                ArrayList listedCameraData = new ArrayList();

                byte[] cameraA =
                {
                0x30, 0xFF, 0xD8, 0xFC, 0xAC, 0x0A, 0x00, 0x00, 0xD0, 0xFE, 0xAC, 0xFC,
                0x10, 0x27, 0x78, 0xEC, 0x70, 0x17, 0x00, 0x00, 0x90, 0x01, 0x2C, 0x01,
                0x10, 0x27, 0x78, 0xEC, 0x70, 0x17, 0x00, 0x00, 0x90, 0x01, 0x2C, 0x01
            };

                listedCameraData.Add(cameraA);
                listedCameraData.Add(cameraA);
                listedCameraData.Add(cameraA);
                listedCameraData.Add(cameraA);
                listedCameraData.Add(cameraA);

                return listedCameraData;
            }
            else
            {
                // This should never fire; need to re-do logic of this class
                ArrayList listedCameraData = new ArrayList();
                return listedCameraData;
            }
        }

        // Old Method; retrieves camera data from the scene.bin before modification.
        // Contains methods for acquiring scene data for calculations/assignment
        //public static ArrayList GetCameraData(int[][] jaggedSceneInfo, string targetScene)
        //{
        //    int r = 0;
        //    int o = 0;
        //    ArrayList listedCameraData = new ArrayList();

        //    while (r < 256)
        //    {
        //        int bytesRead;
        //        byte[] uncompressedScene = new byte[7808]; // Used to hold the decompressed scene file

        //        using (BinaryReader brg = new BinaryReader(new FileStream(targetScene, FileMode.Open)))
        //        {
        //            // Calls method to convert little endian values into an integer
        //            byte[] compressedScene = new byte[jaggedSceneInfo[o][1]]; // Used to hold the compressed scene file, where [o][1] is scene size        
        //            brg.BaseStream.Seek(jaggedSceneInfo[o][2], SeekOrigin.Begin); // Starts reading the compressed scene file
        //            brg.Read(compressedScene, 0, compressedScene.Length);

        //            using (MemoryStream inputWrapper = new MemoryStream(compressedScene))
        //            {
        //                using (MemoryStream decompressedOutput = new MemoryStream())
        //                {
        //                    using (GZipStream zipInput = new GZipStream(inputWrapper, CompressionMode.Decompress, true))
        //                    {
        //                        while ((bytesRead = zipInput.Read(uncompressedScene, 0, 7808)) != 0)
        //                        {
        //                            decompressedOutput.Write(uncompressedScene, 0, bytesRead);
        //                            // If this scene has valid camera data, then pull it out.
        //                            byte[] camera = new byte[48];
        //                            if (uncompressedScene[87] != 255 && uncompressedScene[88] != 255)
        //                            {
        //                                camera = uncompressedScene.Skip(88).Take(48).ToArray();
        //                                listedCameraData.Add(camera);
        //                            }
        //                        }
        //                        zipInput.Close();
        //                    }
        //                    decompressedOutput.Close();
        //                }
        //                inputWrapper.Close();
        //            }
        //            brg.Close();
        //        }
        //        r++;
        //        o++;
        //    }
        //    return listedCameraData;
        //}
    }
}
