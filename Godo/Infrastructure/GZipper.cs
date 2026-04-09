using Godo.Helper;
using Godo.Indexing;
using System;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Schema;
using Godo.Infrastructure.Kernel.EquipmentData;
using Godo.Infrastructure.Kernel.InitialisationData;

namespace Godo.Infrastructure
{
    public class GZipper
    {
        public static byte[] PrepareScene(string inputScene, string outputScene,
            bool[] swapOptions,
            bool[] enemyStatOptions, int[] enemyStatParameters,
            bool[] enemyAttackOptions, int[] enemyAttackParameters,
            bool[] enemyItemOptions,
            bool[] formationOptions,
            bool[] balancingOptions, int[] balancingParameters,
            bool[] challengeOptions,
            bool[] specialHackOptions, int[] specialHackParameters,
            bool[] interimOptions,
            Random rnd)
        {
            //string sceneDirectory = directory + "\\Target Files\\";   // The folder where scene.bin will be placed
            //string targetScene = sceneDirectory + "scene.bin";   // The target file itself

            byte[] header = new byte[64];                       /* Stores the block header
                                                                 * [0-4] = Offset for first GZipped data file (3 enemies per file)
                                                                 * Header total size must be 40h - empty entries == FF FF FF FF
                                                                 */

            int[][] jaggedSceneInfo = new int[256][];           // An array of arrays, containing offset, size, and absoluteoffset for each scene file
            ArrayList listedSceneData = new ArrayList();        // Contains all the compressed scene data
            int[][][] jaggedModelAttackTypes = new int[3000][][];   // Contains all the uncompressed Attack Anim data
            byte[][] jaggedEnemyData = new byte[675][];         // Contains enemy data for each possible model ID (from AA to ZZ)

            long initialSize;                                   // The size of the initial scene.bin (can vary, up to 63 blocks but typically 32-33
            int size;                                           // The size of the compressed file
            int offset;                                         // Stores the current scene offset
            int nextOffset;                                     // Stores the next scene offset
            int absoluteOffset;                                 // Stores the scene's absolute offset in the scene.bin
            int finalOffset = 0;                                // Stores the scene's adjusted offset in the scene.bin
            int headerOffset = 0;                               // Offset of the current block header; goes up in 2000h (8192) increments

            byte[] padder = new byte[1];                        // Scene files, after compression, need to be FF padded to make them multiplicable by 4
            padder[0] = 255;

            //Random rnd = new Random(Guid.NewGuid().GetHashCode());

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
            FileStream fs = new FileStream(inputScene, FileMode.Open, FileAccess.Read);
            initialSize = fs.Length;
            fs.Close();
            while (headerOffset < initialSize) // 32 blocks of 2000h/8192 bytes each
            {
                // Opens and reads the default scene.bin
                FileStream stepOne = new FileStream(inputScene, FileMode.Open, FileAccess.Read);
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
                        offset = EndianConvert.GetLittleEndianInt(currentHeader, 0);
                        nextOffset = EndianConvert.GetLittleEndianInt(nextHeader, 0);

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

            // But first, we acquire the camera data of the target scene.bin
            ArrayList listedCameraData = CameraIndex.GetCameraData(jaggedSceneInfo, inputScene, formationOptions);

            // And the valid Animation Types for each ModelID
            //jaggedModelAttackTypes = AttackTypeIndex.GetAttackData(jaggedSceneInfo, inputScene, rnd);

            while (r < 256)
            {
                int bytesRead;
                byte[] uncompressedScene = new byte[7808]; // Used to hold the decompressed scene file

                using (BinaryReader brg = new BinaryReader(new FileStream(inputScene, FileMode.Open)))
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

                // Sends random camera data to be used
                int rand = (byte)rnd.Next(listedCameraData.Count);
                byte[] initCam = new byte[5];
                byte[] randCam = new byte[5];
                if (formationOptions[0] || formationOptions[1])
                {
                    initCam = CameraIndex.InitialCamera(formationOptions);
                    randCam = (byte[])listedCameraData[rand];
                }
                int sceneID = r;

                // Sends decompressed scene data to be randomised
                SceneProcessing.RandomiseScene(uncompressedScene, randCam, sceneID,
                    swapOptions,
                    enemyStatOptions, enemyStatParameters,
                    enemyAttackOptions, enemyAttackParameters,
                    enemyItemOptions,
                    formationOptions,
                    balancingOptions, balancingParameters,
                    challengeOptions,
                    specialHackOptions, specialHackParameters,
                    rnd, jaggedModelAttackTypes, initCam, jaggedEnemyData,
                    interimOptions);

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
                else if (recompressedScene.Length % 4 == 2)  // Remainder of 2, add 2 FFs
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
                r++;
                o++;
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

            using (var outputStream = File.Create(outputScene))
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
                        if (blockCount != 0)
                        {
                            s += kernelLookup[blockCount - 1];
                        }
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
                        headerInt = EndianConvert.GetPreviousLittleEndianInt(finalHeader, k);
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
                r = 0;  // ahhhh
                o = 0;  // We're gonna loop through whiles
                c = 0;  // all night
                k = 0;  // and try-catch every day
                s = 0;  // *Gene falls over a rogue assignment*

                // All scenes allocated, the final file must now be padded to 8192 bytes
                outputStream.Position = outputStream.Length;
                while (outputStream.Length % 8192 > 0)
                {
                    outputStream.Write(padder, 0, 1);
                }
                // New scene.bin ready to go. Hopefully.
                outputStream.Close();
            }

            return kernelLookup;
        }

        public static void PrepareKernel(
            string inputKernel, string inputKernel2,
            string outputKernel, string outputKernel2, byte[] kernelLookup,
            bool[] spellOptions, int[] spellParameters,
            bool[] summonOptions, int[] summonParameters,
            bool[] enemySkillOptions, int[] enemySkillParameters,
            bool[] attackItemOptions, int[] attackItemParameters,
            bool[] healItemOptions, int[] healItemParameters,
            bool[] statusItemOptions, int[] statusItemParameters,
            bool[] weaponOptions, int[] weaponParameters,
            bool[] armourOptions, int[] armourParameters,
            bool[] accessoryOptions, int[] accessoryParameters,
            bool[] materiaOptions, int[] materiaParameters,
            bool[] statOptions, int[] statParameters, bool[] characterSelectStats,
            bool[] limitOptions, bool[] characterSelectLimits,
            bool[] equipOptions, int[] equipParameters, bool[] characterSelectEquip,
            bool[] challengeOptions,
            bool[] specialHackOptions, int[] specialHackParameters,
            bool[] interimOptions,
            bool[] languageOptions,
            bool[] rngOption,
            Random rnd)
        {

            // An array of arrays, containing compressed size, uncompressed size, section ID
            int[][] jaggedKernelInfo = new int[27][];

            // Stores the starting weapons & armours of the characters
            byte[] startingEquipment = new byte[18];

            // Contains all the compressed kernel section data
            ArrayList listedKernelData = new ArrayList();

            //Retrieves header information for conversion to int
            byte[] header = new byte[4];

            int compressedSize;    // Stores the compressed size of the file
            int uncompressedSize;  // Stores the uncompressed size of the file
            int sectionID;         // Stores the section ID of the file
            int offset = 0;        // Tracks where we are in the kernel.bin

            int headerOffset = 0;   // Stores the absolute offset value for each section's header (updated on each loop)

            // A kernel text section (and kernel2.bin) can't exceed a certain size
            byte[] textData = new byte[27648];
            int indexPosition = 0;

            bool tooBig = false; // If kernel2 size will be too big, this diverts to using Kernel strings instead

            int r = 0;
            int o = 0;

            // Step 0: Read a kernel2.bin and send it off to unpack + produce unpacked section files
            using (BinaryReader ker = new BinaryReader(new FileStream(inputKernel2, FileMode.Open)))
            {
                // Retrieves and reads the kernel2 into memory
                FileInfo kernel2Info = new FileInfo(inputKernel2);
                byte[] compressedKernel2 = new byte[kernel2Info.Length];
                ker.Read(compressedKernel2, 0, (int)kernel2Info.Length);
                Kernel2TextCompressor.Kernel2Decompress(compressedKernel2);
                // We now have all the Kernel2 sections living within the Kernel2 Strings folder
            }

            // Step 1: Read the kernel headers
            while (r < 27) // 27 sections in the kernel
            {
                // Opens and reads the headers in the kernel.bin
                FileStream stepOne = new FileStream(inputKernel, FileMode.Open, FileAccess.Read);
                stepOne.Seek(headerOffset, SeekOrigin.Begin);

                stepOne.Read(header, 0, 2); // Header never exceeds 64 bytes
                compressedSize = EndianConvert.GetLittleEndianInt(header, 0);

                stepOne.Read(header, 0, 2); // Header never exceeds 64 bytes
                uncompressedSize = EndianConvert.GetLittleEndianInt(header, 0);

                stepOne.Read(header, 0, 2); // Header never exceeds 64 bytes
                sectionID = EndianConvert.GetLittleEndianInt(header, 0);

                // Stored kernel header information in a jaggy array
                jaggedKernelInfo[o] = new int[] { compressedSize, uncompressedSize, sectionID };
                stepOne.Close();

                headerOffset += compressedSize + 6;
                r++;
                o++;
                stepOne.Close();
            }
            r = 0;
            o = 0;


            // Step 2: Get the compressed data, uncompress it, and then randomise it
            while (r < 27)
            {
                int bytesRead;
                int size = jaggedKernelInfo[o][1];
                int textSize = 0; // Used to keep track of size of text sections for later
                byte[] uncompressedKernel = new byte[size]; // Used to hold the decompressed kernel section
                string kernelTextSection = Directory.GetCurrentDirectory() + "\\Kernel Strings\\kernel2.bin";
                string kernel2TextSection = Directory.GetCurrentDirectory() + "\\Kernel2 Strings\\kernel2.bin";
                string kernelNewTextSection = Directory.GetCurrentDirectory() + "\\Kernel Strings\\kernel2Modified.bin";
                int offsetTextStart = 0;

                using (BinaryReader brg = new BinaryReader(new FileStream(inputKernel, FileMode.Open)))
                {
                    // Calls method to convert little endian values into an integer
                    byte[] compressedKernel = new byte[jaggedKernelInfo[o][0]]; // Used to hold the compressed section file, where [o][1] is scene size

                    brg.BaseStream.Seek(offset + 6, SeekOrigin.Begin); // Starts reading the compressed section file
                    brg.Read(compressedKernel, 0, compressedKernel.Length);

                    using (MemoryStream inputWrapper = new MemoryStream(compressedKernel))
                    {
                        using (MemoryStream decompressedOutput = new MemoryStream())
                        {
                            using (GZipStream zipInput = new GZipStream(inputWrapper, CompressionMode.Decompress, true))
                            {
                                while ((bytesRead = zipInput.Read(uncompressedKernel, 0, size)) != 0)
                                {
                                    decompressedOutput.Write(uncompressedKernel, 0, bytesRead);

                                    if (r > 8)
                                    {
                                        if (r == 9)
                                        {
                                            // Stores the offset where Text sections begin
                                            // This isn't used anywhere past this point
                                            offsetTextStart = offset;
                                        }

                                        // Produces new kernel2 string files for modification
                                        using (var outputStream = File.Create(kernelTextSection + r))
                                        {
                                            outputStream.Position = 0;
                                            outputStream.Write(uncompressedKernel, 0, bytesRead);
                                            textSize += bytesRead; // this isn't used
                                        }
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

                // Sends decompressed kernel data to be randomised per section
                switch (r)
                {
                    case 0:
                        //Disabled until later phase
                        //CommandData.RandomiseSection0(uncompressedKernel, challengeOptions);
                        break;

                    case 1:
                        //Disabled until later phase
                        //SpellData.RandomiseSpells(uncompressedKernel,
                        //    spellOptions, spellParameters,
                        //    summonOptions, summonParameters,
                        //    enemySkillOptions, enemySkillParameters,
                        //    specialHackOptions, rnd);
                        //SummonData.RandomiseSummons(uncompressedKernel, summonOptions, summonParameters, specialHackOptions, rnd);
                        //EnemySkillData.RandomiseEnemySkills(uncompressedKernel, enemySkillOptions, enemySkillParameters, specialHackOptions, rnd);
                        break;

                    case 2:
                        GrowthAndLookUpTable.RandomiseSection2(uncompressedKernel,
                            statOptions, characterSelectStats,
                            limitOptions, characterSelectLimits,
                            challengeOptions,
                            languageOptions,
                            rngOption,
                            rnd, kernelLookup, interimOptions[3]);
                        break;

                    case 3:
                        Initialisation.RandomiseInitialisation(uncompressedKernel,
                            statOptions, statParameters, characterSelectStats,
                            equipOptions, equipParameters, characterSelectEquip,
                            languageOptions, startingEquipment,
                            rnd, interimOptions[3], interimOptions[4]);
                        break;

                    case 4:
                        //Disabled until later phase
                        //ItemData.RandomiseItems(uncompressedKernel,
                        //    attackItemOptions, attackItemParameters,
                        //    healItemOptions, healItemParameters,
                        //    statusItemOptions, statusItemParameters,
                        //    challengeOptions,
                        //    rnd);
                        break;

                    case 5:
                        if (interimOptions[0] == true)
                        {
                            WeaponData.RandomiseWeapons(uncompressedKernel, weaponOptions, weaponParameters, languageOptions, startingEquipment, rnd, interimOptions[0]);
                        }
                        break;

                    case 6:
                        if (interimOptions[1] == true)
                        {
                            ArmourData.RandomiseArmour(uncompressedKernel, armourOptions, armourParameters, languageOptions, startingEquipment, rnd);
                        }
                        break;

                    case 7:
                        if (interimOptions[2] == true)
                        {
                            AccessoryData.RandomiseAccessories(uncompressedKernel, accessoryOptions, accessoryParameters, languageOptions, rnd);
                        }
                        break;

                    case 8:
                        //MateriaData.RandomiseMateria(uncompressedKernel, materiaOptions, materiaParameters, challengeOptions, rnd);
                        break;

                    // Handles text sections
                    default:
                        // Add a try-catch here just in case a non-text section ends up in here

                        // Need a way to calculate in advance the total size of all the files that will be used.
                        // If it will exceed the limit, then we need to use the smaller Kernel.bin strings instead.
                        // German version's Kernel2.bin can't take all the equipment strings, but the original
                        // kernel can as it's smaller.

                        // Calculates the total size of the kernel2 before it is created; if it will be too large,
                        // the strings taken from the Kernel will be used instead and the rest is up to God.
                        if (r == 9)
                        {
                            int i = 9;
                            long totalSize = 0;
                            while (i < 27)
                            {
                                FileInfo kernel2FileSize = new FileInfo(kernel2TextSection + i);
                                FileInfo newTextFileSize = new FileInfo(kernelNewTextSection + i);

                                if (File.Exists(kernelNewTextSection + i))
                                {
                                    totalSize += newTextFileSize.Length;
                                }
                                else
                                {
                                    totalSize += kernel2FileSize.Length;
                                }

                                if (totalSize >= 27648)
                                {
                                    tooBig = true;
                                }
                                i++;
                            }
                        }

                        // Allows to detect the size of the file in bytes
                        FileInfo kernelTextFile = new FileInfo(kernelTextSection + r);
                        FileInfo kernel2TextFile = new FileInfo(kernel2TextSection + r);
                        FileInfo newTextFile = new FileInfo(kernelNewTextSection + r);

                        // Clears the array in preparation of resizing
                        Array.Clear(uncompressedKernel, 0, uncompressedKernel.Length);

                        // Resize array to fit the new header (for kernel2) and write either the original or modified section data
                        if (File.Exists(kernelNewTextSection + r))
                        {
                            // Gets the length of the current section to set as a header
                            // Use the new length from the modified file and not the current data
                            byte[] sectionHeader = BitConverter.GetBytes(newTextFile.Length);

                            // Has to skip 4 bytes because our modified Kernel sections have no 4-byte header
                            Array.Resize<byte>(ref uncompressedKernel, (int)newTextFile.Length + 4);
                            FileStream textWrite = new FileStream(kernelNewTextSection + r, FileMode.Open, FileAccess.Read);
                            uncompressedKernel[0] = sectionHeader[0];
                            uncompressedKernel[1] = sectionHeader[1];
                            uncompressedKernel[2] = sectionHeader[2];
                            uncompressedKernel[3] = sectionHeader[3];
                            textWrite.Seek(0, SeekOrigin.Begin);
                            textWrite.Read(uncompressedKernel, 4, (int)newTextFile.Length);
                        }
                        else if (tooBig != true) // Taken from Kernel2
                        {
                            byte[] sectionHeader = BitConverter.GetBytes(kernel2TextFile.Length);

                            Array.Resize<byte>(ref uncompressedKernel, (int)kernel2TextFile.Length + 4);
                            FileStream textWrite = new FileStream(kernel2TextSection + r, FileMode.Open, FileAccess.Read);
                            uncompressedKernel[0] = sectionHeader[0];
                            uncompressedKernel[1] = sectionHeader[1];
                            uncompressedKernel[2] = sectionHeader[2];
                            uncompressedKernel[3] = sectionHeader[3];
                            textWrite.Seek(0, SeekOrigin.Begin);
                            textWrite.Read(uncompressedKernel, 4, (int)kernel2TextFile.Length);
                        }
                        else // Taken from Kernel
                        {
                            byte[] sectionHeader = BitConverter.GetBytes(uncompressedKernel.Length);

                            Array.Resize<byte>(ref uncompressedKernel, (int)kernelTextFile.Length + 4);
                            FileStream textWrite = new FileStream(kernelTextSection + r, FileMode.Open, FileAccess.Read);
                            uncompressedKernel[0] = sectionHeader[0];
                            uncompressedKernel[1] = sectionHeader[1];
                            uncompressedKernel[2] = sectionHeader[2];
                            uncompressedKernel[3] = sectionHeader[3];
                            textWrite.Seek(0, SeekOrigin.Begin);
                            textWrite.Read(uncompressedKernel, 4, (int)kernelTextFile.Length);
                        }

                        uncompressedKernel.CopyTo(textData, indexPosition);
                        indexPosition += uncompressedKernel.Length;
                        jaggedKernelInfo[o][1] = uncompressedKernel.Length - 4;
                        break;
                }

                if (r == 26)
                {
                    Array.Resize(ref textData, indexPosition);
                    Kernel2TextCompressor.Kernel2Recompress(textData);
                }

                byte[] recompressedKernel;
                using (var result = new MemoryStream())
                {
                    if (r < 9)
                    {
                        using (var compressionStream = new GZipStream(result, CompressionMode.Compress))
                        {
                            compressionStream.Write(uncompressedKernel, 0, uncompressedKernel.Length);
                            compressionStream.Close();
                        }
                    }
                    else
                    {
                        using (var compressionStream = new GZipStream(result, CompressionMode.Compress))
                        {
                            compressionStream.Write(uncompressedKernel, 4, uncompressedKernel.Length - 4);
                            compressionStream.Close();
                        }
                    }
                    recompressedKernel = result.ToArray();
                    result.Close();

                }
                // Offset is updated for the next pass before we write in our new value
                offset += jaggedKernelInfo[o][0] + 6;

                // The size is updated with the newly compressed/padded section's length
                jaggedKernelInfo[o][0] = recompressedKernel.Length;

                // Byte array is added to the ArrayList
                listedKernelData.Add(recompressedKernel);
                r++;
                o++;
            }
            r = 0;
            o = 0;


            // Step 3: Rebuilding the Kernel.bin
            using (var outputStream = File.Create(outputKernel))
            {
                // Loops until all 27 sections are headered and written
                while (r < 27)
                {
                    // Write the header first
                    byte[] bytes = new byte[2];
                    byte[] kernelHead = new byte[6];
                    ulong comSize = (ulong)jaggedKernelInfo[o][0];
                    ulong uncomSize = (ulong)jaggedKernelInfo[o][1];
                    ulong sectID = (ulong)jaggedKernelInfo[o][2];

                    bytes = EndianConvert.GetLittleEndianConvert(comSize);
                    kernelHead[0] = bytes[0];
                    kernelHead[1] = bytes[1];

                    bytes = EndianConvert.GetLittleEndianConvert(uncomSize);
                    kernelHead[2] = bytes[0];
                    kernelHead[3] = bytes[1];

                    bytes = EndianConvert.GetLittleEndianConvert(sectID);
                    kernelHead[4] = bytes[0];
                    kernelHead[5] = bytes[1];

                    // Takes the header data, converts it into a stream, and then appends it to the file-in-progress
                    outputStream.Position = outputStream.Length;
                    outputStream.Write(kernelHead, 0, kernelHead.Length);

                    // Takes the byte data from the ArrayList, converts it into a stream, and then appends it to the file-in-progress
                    byte[] kernelData = (byte[])listedKernelData[o];
                    outputStream.Position = outputStream.Length;
                    outputStream.Write(kernelData, 0, kernelData.Length);

                    r++;
                    o++;
                }
            }
        }
    }
}