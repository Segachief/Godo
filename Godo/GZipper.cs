using ICSharpCode.SharpZipLib.GZip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo
{
    public class GZipper
    {
        public static void PrepareKernel(string filename)
        {
            /* WorkFlow
             * Target the Kernel.bin file.
             * From the header, store the compressed size, uncompressed size, and section ID.
             * Decompressing; Use the compressed size to get the full target section.
             * Recompressing; Add the section header afterwards.
             */

            string gzipFileName = filename;                     // Opens the specified file; to be replaced with automation
            string targetDir = Path.GetDirectoryName(filename); // Get directory where the target file resides
            string[] inputFilePaths = new string[27];

            byte[] header = new byte[6]; /* Stores the section header
                                          * [0][1] = Compressed Size
                                          * [2][3] = uncompressed Size
                                          * [4][5] = Section ID - in practice, only [4] will be used as section ID never exceeds 255
                                          */

            byte[] compressedSize = new byte[6];    // Stores the compressed size of the file
            byte[] uncompressedSize = new byte[6];  // Stores the uncompressed size of the file
            byte[] sectionID = new byte[6];         // Stores the section ID of the file
            int absoluteHeaderOffset = 0;           // Stores the absolute offset value for each section's header (updated on each loop)
            int absoluteSectionOffset = 0;          // Stores the absolute offset value for each section's GZIP contents (updated on each loop)

            int sectionCount = 0; // Iteration of Section within Kernel
            while (sectionCount < 27)
            {

                if (sectionCount == 0)
                {
                    // First section's header starts at offset 0
                    FileStream hfs = new FileStream(gzipFileName, FileMode.Open, FileAccess.Read);
                    hfs.Seek(0, SeekOrigin.Begin);
                    hfs.Read(header, 0, 5);
                    hfs.Close();
                }
                else
                {
                    // Now each section must add its offset to reach the intended destination in original Kernel.bin
                    int headerOffset = AllMethods.GetLittleEndianInt(compressedSize, 0);
                    absoluteHeaderOffset = absoluteHeaderOffset + headerOffset + 6; // Adds 6 as compressedSize in header doesn't account for header size (6 bytes)
                    absoluteSectionOffset = absoluteHeaderOffset + 6; // Takes the absolute header offset and adds 6 to reach GZIP of this section
                    FileStream hfs = new FileStream(gzipFileName, FileMode.Open, FileAccess.Read);
                    hfs.Seek(absoluteHeaderOffset, SeekOrigin.Begin);
                    hfs.Read(header, 0, 5);
                    hfs.Close();
                }

                // Creates three new file paths; one is the uncompressed data, a mid-step to recompression, and finally the recompressed data + header
                string kernelSectionUncompressed = Path.Combine(targetDir, Path.GetFileNameWithoutExtension("uncompressed" + sectionCount));
                string kernelSectionInterim = Path.Combine(targetDir, Path.GetFileNameWithoutExtension("interim" + sectionCount));
                string kernelSectionRecompressed = Path.Combine(targetDir, Path.GetFileNameWithoutExtension("recompressed" + sectionCount));

                inputFilePaths[sectionCount] = kernelSectionRecompressed;

                // Copies header byte data into these separate arrays so they can be parsed to int easier
                compressedSize[0] = header[0];
                compressedSize[1] = header[1];
                uncompressedSize[0] = header[2];
                uncompressedSize[1] = header[3];
                sectionID[0] = header[4];
                sectionID[1] = header[5];

                // STAGE 1: Opens the file, reads its bytes, creates an interim compressed file of a single section
                using (BinaryReader brg = new BinaryReader(new FileStream(gzipFileName, FileMode.Open)))
                {
                    // Calls method to convert little endian values into an integer
                    int compressedIntSize = AllMethods.GetLittleEndianInt(uncompressedSize, 0);
                    byte[] compressedSection = new byte[compressedIntSize - 6]; // Array that uses the compressed size of section with the header trimmed off (6 bytes)

                    if (sectionCount == 0)
                    {
                        brg.BaseStream.Seek(6, SeekOrigin.Begin); // Starts a new reading from offset 0x6, past the header, which is where Gzip file starts
                    }
                    else
                    {
                        brg.BaseStream.Seek(absoluteSectionOffset, SeekOrigin.Begin); // Starts a new reading from offset 0x6, past the header, which is where Gzip file starts
                    }
                    brg.Read(compressedSection, 0, compressedSection.Length); // From specified offset of 0x06 above, reads from 0 and reads for length of the section.

                    // Opens a FileStream to the file where we will put out Compressed Kernel Section bytes
                    using (var fs = new FileStream(kernelSectionUncompressed, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(compressedSection, 0, compressedSection.Length); // Writes in the bytes to the file
                        fs.Close();
                    }

                    // STAGE 2: This is where the Interim Compressed file is used to create an Uncompressed file
                    using (MemoryStream msi = new MemoryStream())
                    {
                        msi.Write(compressedSection, 0, compressedSection.Length);
                        msi.Position = 0;

                        // SharpZipLib GZip method called
                        using (Stream decompressStream = new GZipInputStream(msi))
                        {
                            int uncompressedIntSize = AllMethods.GetLittleEndianInt(uncompressedSize, 0); // Gets little endian value of uncompressed size into an integer
                            byte[] uncompressBuffer = new byte[uncompressedIntSize]; // Buffer is set to uncompressed size
                            int size = decompressStream.Read(uncompressBuffer, 0, uncompressBuffer.Length); // Stream is decompressed and read

                            // Uncompressed bytes written out here using SharpZipLib's GZipInputStream
                            using (var fs = new FileStream(kernelSectionUncompressed, FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(uncompressBuffer, 0, uncompressBuffer.Length);
                                fs.Close();
                            }
                            decompressStream.Close();

                            // STAGE 3: A finalised compressed file is made here
                            FileStream srcFile = File.OpenRead(kernelSectionUncompressed);
                            GZipOutputStream zipFile = new GZipOutputStream(File.Open(kernelSectionInterim, FileMode.Create));
                            try
                            {
                                byte[] FileData = new byte[srcFile.Length];
                                srcFile.Read(FileData, 0, (int)srcFile.Length);
                                zipFile.Write(FileData, 0, FileData.Length);
                            }
                            catch
                            {
                                MessageBox.Show("Failed to compress", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                zipFile.Close();
                                FileStream comFile = File.OpenRead(kernelSectionInterim);
                                // Adjusts the header's held values for compressed section size ([0][1])
                                long newCompressedSize = comFile.Length;
                                byte[] bytes = BitConverter.GetBytes(newCompressedSize);
                                header[0] = bytes[0];
                                header[1] = bytes[1];

                                // Adjusts the header's held values for uncompressed section size ([2][3])
                                long newUncompressedSize = srcFile.Length;
                                bytes = BitConverter.GetBytes(newUncompressedSize);
                                header[2] = bytes[0];
                                header[3] = bytes[1];

                                comFile.Close();
                                srcFile.Close();
                            }

                            // STAGE 4: An updated header is made, and attached to the finalised compressed section file
                            int headerLen = header.Length;
                            using (var newFile = new FileStream(kernelSectionRecompressed, FileMode.CreateNew, FileAccess.Write))
                            {
                                for (var i = 0; i < headerLen; i++)
                                {
                                    newFile.WriteByte(header[i]);
                                }
                                using (var oldFile = new FileStream(kernelSectionInterim, FileMode.Open, FileAccess.Read))
                                {
                                    oldFile.CopyTo(newFile);
                                    oldFile.Close();
                                }
                                newFile.Close();
                            }
                        }
                    }
                    brg.Close();
                }
                if (sectionCount == 26)
                {
                    using (var outputStream = File.Create("C:\\Users\\stewart.melville\\Documents\\GZip\\gzip\\ff7_gzip\\SharpZipLibTest\\TARGET.BIN"))
                    {
                        foreach (var inputFilePath in inputFilePaths)
                        {
                            using (var inputStream = File.OpenRead(inputFilePath))
                            {
                                // Buffer size can be passed as the second argument.
                                inputStream.CopyTo(outputStream);
                                inputStream.Close();
                                File.Delete(inputFilePath);
                            }
                        }
                    }
                }
                File.Delete(kernelSectionInterim);
                File.Delete(kernelSectionUncompressed);
                sectionCount++;
            }
        }

        public static void PrepareScene(string filename)
        {
            string gzipFileName = filename;                     // Opens the specified file; to be replaced with automation
            string targetDir = Path.GetDirectoryName(filename); // Get directory where the target file resides
            string[] inputFilePaths = new string[16];           // Stores all the recompressed files, so that they can be built into a new scene.bin
            string[] inputFinalPaths = new string[32];          // Stores all the block sections for generating the final scene.bin
            string finalScene = Path.Combine(targetDir, Path.GetFileNameWithoutExtension("finalscene")); // This is the finished scene.bin

            byte[] header = new byte[64];                       /* Stores the block header
                                                                 * [0-4] = Offset for first GZipped data file (3 enemies per file)
                                                                 * Header total size must be 40h, FF padded
                                                                 */

            // In order to calculate the new header values, math needs to be performed with known values to derive all the correct values
            byte[] thisSceneOffset = new byte[4];   // Pointer to current file
            byte[] nextSceneOffset = new byte[4];   // Pointer to next file - used with currentScene to work out size of current file
            byte[] prevSceneOffset = new byte[4];   // Pointer of the previous offset, used to derive current pointer for header adjustment

            int thisSceneInt;                       // Int converted value of currentSceneOffset
            int nextSceneInt;                       // Int converted value of nextSceneOffset
            int prevSceneInt;                       // Int converted value of previousSceneOffset

            int compressedSceneSize;                // Stores the compressed size of the current target section
            int absoluteOffset;                     // Stores the absolute offset value for section's header
            long newSceneOffset;                    // Used to calculate, in bytes, the offset to be read into header
            long prevSceneSize = 0;                 // Stores the size of the previous scene

            int thisHeaderCounter = 0;              // Used to determine location offset of where to write in 4-byte header value for current scene pointer
            int prevHeaderCounter = 0;              // Header values for the previous section; used to calculate size

            byte[] padder = new byte[3];            // Scene files, after compression, need to be FF padded to make them multiplicable by 4
            padder[0] = 255; padder[1] = 255; padder[2] = 255;

            int blockCount = 0;                     // Counts how many blocks have been produced, there should be 32
            int headerOffset = 0;                   // Offset of the current block header; goes up in 2000h (8192) increments


            // Scene decompression/recompression begins here
            while (headerOffset < 262144)           // This is the total number of bytes a scene.bin will be (32 potential blocks of 8192 bytes a-piece)
            {
                string sceneNewHeader = Path.Combine(targetDir, Path.GetFileNameWithoutExtension("header" + blockCount)); // Produces a temporary file we'll use to store our recompressed block
                inputFinalPaths[blockCount] = sceneNewHeader; // Sets the header path into an array so we can produce a final scene.bin at the end of this class

                FileStream hfs = new FileStream(gzipFileName, FileMode.Open, FileAccess.Read); // Opens and reads the default scene.bin
                hfs.Seek(headerOffset, SeekOrigin.Begin); // Should never exceed 40h/64d as header max size is 40h and each enemy needs a 4-byte offset. So that's room for 16 enemies only.
                hfs.Read(header, 0, 64);
                hfs.Close();

                int sectionCount = 0; // Iteration of Section within Block - There are up to 16 sections within a block but can be less due to varying size of Scenes
                while (sectionCount < 16)
                {
                    // For storing uncompressed scene, recompressed scene, and an updated header
                    string sceneFileUncompressed = Path.Combine(targetDir, Path.GetFileNameWithoutExtension("uncompressed" + sectionCount));
                    string sceneFileRecompressed = Path.Combine(targetDir, Path.GetFileNameWithoutExtension("recompressed" + sectionCount));

                    inputFilePaths[sectionCount] = sceneFileRecompressed;

                    // Copies header byte data into these separate arrays so they can be parsed to int easier
                    thisSceneOffset[0] = header[thisHeaderCounter];
                    thisSceneOffset[1] = header[thisHeaderCounter + 1];
                    thisSceneOffset[2] = header[thisHeaderCounter + 2];
                    thisSceneOffset[3] = header[thisHeaderCounter + 3];

                    if (sectionCount < 15) // If we're at 15, then there are no more headers to record + we're at capacity of our 64-byte header array
                    {
                        nextSceneOffset[0] = header[thisHeaderCounter + 4];
                        nextSceneOffset[1] = header[thisHeaderCounter + 5];
                        nextSceneOffset[2] = header[thisHeaderCounter + 6];
                        nextSceneOffset[3] = header[thisHeaderCounter + 7];
                    }
                    else
                    {
                        nextSceneOffset[0] = 255;
                        nextSceneOffset[1] = 255;
                        nextSceneOffset[2] = 255;
                        nextSceneOffset[3] = 255;
                    }

                    if (sectionCount != 0) // Plan is to get the prev offsets here to keep everything together
                    {
                        prevSceneOffset[0] = header[prevHeaderCounter];
                        prevSceneOffset[1] = header[prevHeaderCounter + 1];
                        prevSceneOffset[2] = header[prevHeaderCounter + 2];
                        prevSceneOffset[3] = header[prevHeaderCounter + 3];
                        prevHeaderCounter += 4;
                    }

                    thisSceneInt = AllMethods.GetLittleEndianInt(thisSceneOffset, 0);
                    nextSceneInt = AllMethods.GetLittleEndianInt(nextSceneOffset, 0);
                    if ((nextSceneOffset[0] == 0xFF && nextSceneOffset[1] == 0xFF && nextSceneOffset[2] == 0xFF && nextSceneOffset[3] == 0xFF) || sectionCount == 15)
                    {
                        // If next header is FF FF FF FF, or if we're on last section header, then we're dealing with the prev file and should deduct 2000h to get its size
                        compressedSceneSize = 8192 - (thisSceneInt * 4);
                    }
                    else
                    {
                        compressedSceneSize = (nextSceneInt - thisSceneInt) * 4;
                    }
                    absoluteOffset = (thisSceneInt * 4) + headerOffset; // Gets the starting offset of the GZipped file

                    if (thisSceneOffset[0] == 0xFF && thisSceneOffset[1] == 0xFF && thisSceneOffset[2] == 0xFF && thisSceneOffset[3] == 0xFF)
                    {
                        // If the current header has somehow become FF FF FF FF then this terminates everything, but indicates a flaw in logic
                        sectionCount++;
                        break;
                    }

                    // STAGE 1: Opens the file, reads its bytes, creates an interim compressed file of a single section
                    using (BinaryReader brg = new BinaryReader(new FileStream(gzipFileName, FileMode.Open)))
                    {
                        // Calls method to convert little endian values into an integer
                        byte[] compressedSection = new byte[compressedSceneSize]; // Array that uses the compressed size of section with the header trimmed off (6 bytes)        
                        brg.BaseStream.Seek(absoluteOffset, SeekOrigin.Begin); // Starts reading the compressed scene file
                        brg.Read(compressedSection, 0, compressedSection.Length);

                        // Opens a FileStream to the file where we will output the compressed scene file
                        using (var fs = new FileStream(sceneFileUncompressed, FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(compressedSection, 0, compressedSection.Length); // Writes in the bytes to the file
                            fs.Close();
                        }

                        // STAGE 2: This is where the compressed file is used to create an Uncompressed file
                        using (MemoryStream msi = new MemoryStream())
                        {
                            msi.Write(compressedSection, 0, compressedSection.Length);
                            msi.Position = 0;

                            // SharpZipLib GZip method called
                            using (Stream decompressStream = new GZipInputStream(msi))
                            {
                                byte[] uncompressBuffer = new byte[7808]; // Buffer is set; each decompressed scene is always 7808 bytes, padded with FFs
                                int size = decompressStream.Read(uncompressBuffer, 0, uncompressBuffer.Length); // Stream is decompressed and read

                                // Uncompressed bytes written out here using SharpZipLib's GZipInputStream
                                using (var fs = new FileStream(sceneFileUncompressed, FileMode.Create, FileAccess.Write))
                                {
                                    fs.Write(uncompressBuffer, 0, uncompressBuffer.Length);
                                    fs.Close();
                                }
                                decompressStream.Close();

                                // Edits - Current problem; compressed file is coming in at double size; 80k bytes instead of 40k bytes
                                // File returns at correct size, however...
                                Scene.RandomiseScene(sceneFileUncompressed);

                                // STAGE 3: After edits are made (if applicable) the file is recompressed
                                FileStream srcFile = File.OpenRead(sceneFileUncompressed);
                                GZipOutputStream zipFile = new GZipOutputStream(File.Open(sceneFileRecompressed, FileMode.Create));
                                try
                                {
                                    byte[] FileData = new byte[srcFile.Length];
                                    srcFile.Read(FileData, 0, (int)srcFile.Length);
                                    zipFile.Write(FileData, 0, FileData.Length);
                                }
                                catch
                                {
                                    MessageBox.Show("Failed to compress", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                finally
                                {
                                    zipFile.Close();
                                    FileStream comFile = File.OpenRead(sceneFileRecompressed);

                                    if (sectionCount == 0)
                                    {
                                        newSceneOffset = 16; // First offset in a header block is always 10h
                                        prevSceneSize = comFile.Length;
                                    }
                                    else
                                    {
                                        prevSceneInt = AllMethods.GetLittleEndianInt(prevSceneOffset, 0);
                                        newSceneOffset = ((prevSceneInt * 4) + prevSceneSize) / 4;
                                        prevSceneSize = comFile.Length;
                                    }

                                    comFile.Close();

                                    byte[] bytes = BitConverter.GetBytes(newSceneOffset);
                                    header[thisHeaderCounter] = bytes[0];
                                    header[thisHeaderCounter + 1] = bytes[1];
                                    header[thisHeaderCounter + 2] = bytes[2];
                                    header[thisHeaderCounter + 3] = bytes[3];

                                    //The compressed file must be a multiple of 4, so FF bytes are added to end
                                    if (prevSceneSize % 4 == 3)  // Remainder of 3, add 1 FF
                                    {
                                        using (var append = new FileStream(sceneFileRecompressed, FileMode.Append))
                                        {
                                            append.Write(padder, 0, 1);
                                            prevSceneSize += 1;
                                        }
                                    }
                                    else if (prevSceneSize % 4 == 2) // Remainder of 2, add 2 FFs
                                    {
                                        using (var append = new FileStream(sceneFileRecompressed, FileMode.Append))
                                        {
                                            append.Write(padder, 0, 2);
                                            prevSceneSize += 2;
                                        }
                                    }
                                    else if (prevSceneSize % 4 == 1) // Remainder of 1, add 3 FFs
                                    {
                                        using (var append = new FileStream(sceneFileRecompressed, FileMode.Append))
                                        {
                                            append.Write(padder, 0, 3);
                                            prevSceneSize += 3;
                                        }
                                    }
                                    else
                                    {
                                    }
                                    srcFile.Close();
                                }
                            }
                            msi.Close();
                        }
                        brg.Close();
                    }
                    //File.Delete(sceneFileUncompressed);
                    sectionCount++;
                    thisHeaderCounter += 4;
                    if (nextSceneOffset[0] == 0xFF && nextSceneOffset[1] == 0xFF && nextSceneOffset[2] == 0xFF && nextSceneOffset[3] == 0xFF)
                    {
                        sectionCount = 16;
                    }
                }
                if (sectionCount == 16)
                {
                    using (var outputStream = File.Create(sceneNewHeader))
                    {
                        outputStream.Write(header, 0, 64);

                        foreach (var inputFilePath in inputFilePaths)
                        {
                            if (inputFilePath != null)
                            {
                                using (var inputStream = File.OpenRead(inputFilePath))
                                {
                                    // Buffer size can be passed as the second argument.
                                    inputStream.CopyTo(outputStream);
                                    inputStream.Close();
                                    //File.Delete(inputFilePath);
                                }
                            }
                        }
                        while (outputStream.Length % 8192 > 0)  // Remainder of 3, add 1 FF
                        {
                            outputStream.Write(padder, 0, 1);
                        }
                        outputStream.Close();
                    }
                }
                Array.Clear(inputFilePaths, 0, 16);
                thisHeaderCounter = 0;
                prevHeaderCounter = 0;
                blockCount++;
                headerOffset += 8192;
            }
            using (var outputStream = File.Create(finalScene))
            {
                foreach(var inputFinalPath in inputFinalPaths)
                {
                    using (var inputStream = File.OpenRead(inputFinalPath))
                    {
                        inputStream.CopyTo(outputStream);
                        inputStream.Close();
                        //File.Delete(inputFinalPath);
                    }
                }
            }
        }
    }
}