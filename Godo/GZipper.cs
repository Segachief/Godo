using ICSharpCode.SharpZipLib.GZip;
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
    public class GZipper
    {
        public static void PrepareScene(string filename)
        {
            string gzipFileName = filename;                                                                 // Opens the specified file
            string targetDir = Path.GetDirectoryName(filename);                                             // Get directory where the target file resides
            string kernel = Path.GetDirectoryName("Kernel.Bin");                                            // The kernel.bin for updating the lookup table
            string finalScene = Path.Combine(targetDir, Path.GetFileNameWithoutExtension("finalscene"));    // This is the finished scene.bin

            byte[] header = new byte[64];                       /* Stores the block header
                                                                 * [0-4] = Offset for first GZipped data file (3 enemies per file)
                                                                 * Header total size must be 40h - empty entries == FF FF FF FF
                                                                */

            int[][] jaggedSceneInfo = new int[256][];           // An array of arrays, containing offset, size, and absoluteoffset for each scene file
            ArrayList listedSceneData = new ArrayList();        // Contains all the compressed scene data

            int size;                                           // The size of the compressed file
            int offset;                                         // Stores the current scene offset
            int nextOffset;                                     // Stores the next scene offset
            int absoluteOffset;                                 // Stores the scene's absolute offset in the scene.bin
            int finalOffset = 0;                                // Stores the scene's adjusted offset in the scene.bin
            int headerOffset = 0;                               // Offset of the current block header; goes up in 2000h (8192) increments

            byte[] padder = new byte[1];                        // Scene files, after compression, need to be FF padded to make them multiplicable by 4
            padder[0] = 255;

            byte[] kernelLookup = new byte[64];                 // Stores the new lookup table to be written to the kernel.bin; blank values are FF
            int i = 0;
            while (i < 64)
            {
                kernelLookup[i] = 255;
                i++;
            }

            int r = 0;  // C'mon get up and make some noise
            int o = 0;  // while your whiles get looped by
            int c = 0;  // the var street boys
            int k = 0;  // *DJ scratching noises*
            int s = 0;  // *DJ scratching noises intensify*


            /* Step 1: Read the Scene.bin and retrieve its data for use later
             * The goal of this step is to build an array containing information about each scene.
             * We then use this information to derive other important information (for instance, adjusting the header offsets)
             * To get the info we need, the header of each 'block' is read (2000h per block) and this tells us where to find each scene.
             * We need to use GZip compression to get the data out, but we cannot let the Gzipper hit the header or it will break.
            */

            // Entire file is read; offsets and sizes for scenes are extracted and placed in a jagged array (an array of arrays)
            while (headerOffset < 262144) // 32 blocks of 2000h/8192 bytes each
            {
                // Opens and reads the default scene.bin
                FileStream stepOne = new FileStream(gzipFileName, FileMode.Open, FileAccess.Read);
                stepOne.Seek(headerOffset, SeekOrigin.Begin);
                stepOne.Read(header, 0, 64); // Header never exceeds 64 bytes
                stepOne.Close();

                // Max of 16 sections in a header (is usually less however)
                while (r < 16)
                {
                    // If the 2nd byte of the current header is FF then assume there are no more valid scene headers in this block
                    if (header[c + 1] != 0xFF)
                    {
                        // Fetches the current header
                        byte[] currentHeader = new byte[4];
                        currentHeader[0] = header[c];
                        currentHeader[1] = header[c + 1];
                        currentHeader[2] = header[c + 2];
                        currentHeader[3] = header[c + 3];

                        // Fetches the next header - ignored if we're at the end of the block header
                        byte[] nextHeader = new byte[4];
                        if (r < 15)
                        {
                            nextHeader[0] = header[c + 4];
                            nextHeader[1] = header[c + 5];
                            nextHeader[2] = header[c + 6];
                            nextHeader[3] = header[c + 7];
                        }

                        // Converts the current offset and the next offset into integer
                        offset = AllMethods.GetLittleEndianInt(currentHeader, 0);
                        nextOffset = AllMethods.GetLittleEndianInt(nextHeader, 0);

                        // Checks that next header is not empty or if we are at the last header
                        if (currentHeader[1] == 0xFF || nextHeader[1] == 0xFF || r == 15)
                        {
                            // If next header is FF FF FF FF, then we're at the end of file and should deduct 2000h to get current file size
                            size = 8192 - (offset * 4);
                        }
                        else
                        {
                            // Difference between this offset and next offset provides size; offsets need to be *4 to get actual offset
                            size = (nextOffset - offset) * 4;
                        }
                        // Gets absolute offset in the scene.bin for the current scene
                        absoluteOffset = (offset * 4) + headerOffset;
                        // Store our retrieved/derived information in our jagged array (watch out for the pointy bits)
                        jaggedSceneInfo[o] = new int[] { offset, size, absoluteOffset, finalOffset };
                        o++;
                    }
                    c += 4;
                    r++;
                }
                headerOffset += 8192;
                r = 0;
                c = 0;
            }
            o = 0;
            headerOffset = 0;


            /* Step 2: Randomising the scene data
             * Using absolute offset + compressed size, we locate and decompress the file.
             * We run the scene data through the randomiser.
             * We then recompress the returned data.
             * The size will now have changed; we will update this later while generating our new scene.bin
            */

            while (r < 256)
            {
                int bytesRead;
                byte[] uncompressedScene = new byte[7808]; // Used to hold the decompressed scene file

                using (BinaryReader brg = new BinaryReader(new FileStream(gzipFileName, FileMode.Open)))
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
                                }
                                zipInput.Close();
                            }
                            decompressedOutput.Close();
                        }
                        inputWrapper.Close();
                    }
                    brg.Close();
                }

                // Sends decompressed scene data to be randomised
                Scene.RandomiseScene(uncompressedScene);

                // Recompress the altered uncompressed data back into GZip
                byte[] recompressedScene;
                using (var result = new MemoryStream())
                {
                    using (var compressionStream = new GZipStream(result, CompressionMode.Compress))
                    {
                        compressionStream.Write(uncompressedScene, 0, uncompressedScene.Length);
                        compressionStream.Close();
                    }
                    recompressedScene = result.ToArray();
                    result.Close();
                }

                // Checks that the file is divisible by 4; FF padding is applied otherwise
                if (recompressedScene.Length % 4 == 3)      // Remainder of 3, add 1 FF
                {
                    recompressedScene = recompressedScene.Concat(padder).ToArray();
                }
                else if(recompressedScene.Length % 4 == 2)  // Remainder of 2, add 2 FFs
                {
                    recompressedScene = recompressedScene.Concat(padder).ToArray();
                    recompressedScene = recompressedScene.Concat(padder).ToArray();
                }
                else if (recompressedScene.Length % 4 == 1)  // Remainder of 1, add 3 FFs
                {
                    recompressedScene = recompressedScene.Concat(padder).ToArray();
                    recompressedScene = recompressedScene.Concat(padder).ToArray();
                    recompressedScene = recompressedScene.Concat(padder).ToArray();
                }

                // The size is updated with the newly compressed/padded scene's length
                jaggedSceneInfo[o][1] = recompressedScene.Length;

                // Byte array is added to the ArrayList
                listedSceneData.Add(recompressedScene);
                o++;
                r++;
            }
            r = 0;
            o = 0;


            /* Step 3: Rebuilding the Scene.bin
             * We dynamically put scenes into a block until it would exceed 8192 bytes; then we create a new block.
             * The header is constantly updated with each new scene added to the block, using previous header to determine size.
             * When all 255 scenes are allocated, we finish up by padding off the last block to get a 40,000h/262,144 byte file.
             * The size will now have changed; we will update this later while generating our new scene.bin
            */

            int sizeLimit = 8193; // Want to start by making a new header so we set size higher than limit to trigger that
            int headerInt;
            byte[] finalHeader = new byte[64];

            using (var outputStream = File.Create(finalScene))
            {
                int blockCount = 0; // Counts blocks for the kernel lookup table index
                // Loops until all 255 scenes are assigned to a block
                while (r < 256)
                {
                    // Checks if the next scene will 'fit' into the current block.
                    // No scene is added yet at this time, that is only done if there's space.
                    sizeLimit += jaggedSceneInfo[o][1];

                    // If this returns true, then our block is 'full' and now needs to be padded to 8192 bytes exactly
                    // 's' represents the number of scenes currently in the block, only 16 scenes can fit into one block
                    if (sizeLimit >= 8192 || s == 16)
                    {
                        kernelLookup[blockCount] = (byte)s;
                        blockCount++;

                        // Pads the end of the block until it hits a divisor of 8192.
                        outputStream.Position = outputStream.Length;
                        while (outputStream.Length % 8192 > 0)
                        {
                            outputStream.Write(padder, 0, 1);
                        }

                        // A new blank header of FFs is made for the start of the next new block
                        while (c < 64)
                        {
                            finalHeader[c] = 255;
                            c++;
                        }
                        finalHeader[0] = 16; //First offset is always 16h in a header
                        finalHeader[1] = 0;
                        finalHeader[2] = 0;
                        finalHeader[3] = 0;


                        if (s != 0)
                        {
                            headerOffset += 8192; // Increment headerOffset
                        }

                        // Writes the header to the file at 8192 increments
                        outputStream.Position = outputStream.Length;
                        outputStream.Write(finalHeader, 0, 64);

                        c = 0;
                        k = 0;
                        s = 0;
                        sizeLimit = jaggedSceneInfo[o][1]; // Resets size to that of the first added scene in this new block
                        sizeLimit += 64;
                    }

                    // When we write a compressed file in, we want to update the header for the next file.
                    // We'll have a +4 incrementing value to write it into the appropriate header address.
                    // This needs to avoid writing to the first header offset.

                    // Takes the byte data from the ArrayList, converts it into a stream, and then appends it to the file-in-progress
                    byte[] sceneData = (byte[])listedSceneData[o];
                    outputStream.Position = outputStream.Length;
                    outputStream.Write(sceneData, 0, sceneData.Length);

                    // Skips offset calculation if it is the first scene in the block as this is always 16h and has been written already
                    if (s != 0)
                    {
                        // Calculates this scene's offset using the previous offset + that offset's file size.
                        headerInt = AllMethods.GetPreviousLittleEndianInt(finalHeader, k);
                        headerInt *= 4;
                        headerInt += jaggedSceneInfo[o - 1][1];
                        headerInt /= 4;

                        // Now we have new offset calculated, time to convert it into bytes and write to header.
                        byte[] bytes = BitConverter.GetBytes(headerInt);
                        finalHeader[k] = bytes[0];
                        finalHeader[k + 1] = bytes[1];
                        finalHeader[k + 2] = bytes[2];
                        finalHeader[k + 3] = bytes[3];

                        // Write bytes to offset of k + headerOffset to our file now.
                        outputStream.Position = headerOffset;
                        outputStream.Write(finalHeader, 0, 64);
                    }
                    r++;
                    o++;
                    k += 4;
                    s++;
                }
                r = 0;
                o = 0;
                c = 0;
                k = 0;
                s = 0;

                // All scenes allocated, the final file must now be padded to 8192 bytes
                outputStream.Position = outputStream.Length;
                while (outputStream.Length % 262144 > 0)
                {
                    outputStream.Write(padder, 0, 1);
                }
                // New scene.bin ready to go. Hopefully.
                outputStream.Close();
            }

            //TODO: Open the kernel.bin and write in the updated lookup table here.
            using (BinaryWriter bw = new BinaryWriter(File.Open(kernel, FileMode.Open)))
            {
                // Test this, think it's 3904 offset
                bw.BaseStream.Position = 0x003904;
                bw.Write(kernelLookup, 0, 64);
            }

        }
    }
}