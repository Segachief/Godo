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
            /* WorkFlow
             * Target the Scene.bin file.
             * Get the full header and store in an array to be referred to by [i]
             * Decompressing; Use header to get the target origin and where to stop reading
             * Recompressing; Needs to update the value in header array with its new value(s) - FF FF FF FF needs allocated for block ends
             */

            string gzipFileName = filename;                     // Opens the specified file; to be replaced with automation
            string targetDir = Path.GetDirectoryName(filename); // Get directory where the target file resides
            string[] inputFilePaths = new string[256];          // Stores all the recompressed files, so that they can be built into a new scene.bin

            byte[] header = new byte[40]; /* Stores the block header
                                          * [0-4] = Offset for first GZipped data file (3 enemies per file)
                                          * Header total size must be 40h, FF padded
                                          */

            byte[] thisSceneOffset = new byte[4];   // Pointer to current file
            byte[] nextSceneOffset = new byte[4];   // Pointer to next file - used with currentScene to work out size of current file
            byte[] prevSceneOffset = new byte[4];   // Pointer of the previous offset, used to derive current pointer for header adjustment

            int thisSceneInt;                       // Int converted value of currentSceneOffset
            int nextSceneInt;                       // Int converted value of nextSceneOffset
            int prevSceneInt;                       // Int converted value of previousSceneOffset

            int compressedSceneSize = 0;                // Stores the compressed size of the current target section
            int absoluteOffset;                     // Stores the absolute offset value for section's header

            long newCompressedSize = 0;             // Used to calculate, in bytes, the offset to be read into header

            int thisHeaderCounter = 0;                  // Used to determine location offset of where to write in 4-byte header value for current scene pointer
            int prevHeaderCounter = 0;                       // Header values for the previous section; used to calculate size

            byte[] padder = new byte[3];            // Scene files, after compression, need to be FF padded to make them multiplicable by 4
            padder[0] = 255; padder[1] = 255; padder[2] = 255;
            long adjustedCompressedSize;            // Compression size after padding is added


            int headerOffset = 0; // Offset of the current block header; goes up in 2000h (8192) increments
            while (headerOffset < 8192)
            {
                int blockCount = 0;
                string sceneNewHeader = Path.Combine(targetDir, Path.GetFileNameWithoutExtension("header" + blockCount));

                int sectionCount = 0; // Iteration of Section within Block - There are up to 16 sections within a block but can be less due to varying size of Scenes
                while (sectionCount < 16)
                {
                    FileStream hfs = new FileStream(gzipFileName, FileMode.Open, FileAccess.Read);
                    hfs.Seek(sectionCount * 4, SeekOrigin.Begin); // Should never exceed 40h/64d as header max size is 40h and each enemy needs a 4-byte offset. So that's room for 16 enemies only.
                    hfs.Read(header, headerOffset, 40);
                    hfs.Close();

                    // For storing uncompressed scene, recompressed scene, and an updated header
                    string sceneFileUncompressed = Path.Combine(targetDir, Path.GetFileNameWithoutExtension("uncompressed" + sectionCount));
                    string sceneFileRecompressed = Path.Combine(targetDir, Path.GetFileNameWithoutExtension("recompressed" + sectionCount));

                    inputFilePaths[sectionCount] = sceneFileRecompressed;

                    // Copies header byte data into these separate arrays so they can be parsed to int easier

                    // TODO - Need logic where we can get the correct header index on each pass, this doesn't work
                    thisSceneOffset[0] = header[thisHeaderCounter];
                    thisSceneOffset[1] = header[thisHeaderCounter + 1];
                    thisSceneOffset[2] = header[thisHeaderCounter + 2];
                    thisSceneOffset[3] = header[thisHeaderCounter + 3];

                    nextSceneOffset[0] = header[thisHeaderCounter + 4];
                    nextSceneOffset[1] = header[thisHeaderCounter + 5];
                    nextSceneOffset[2] = header[thisHeaderCounter + 6];
                    nextSceneOffset[3] = header[thisHeaderCounter + 7];

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
                    compressedSceneSize = (nextSceneInt - thisSceneInt) * 4;
                    absoluteOffset = thisSceneInt * 4; // Gets the starting offset of the GZipped file

                    if (thisSceneOffset[0] == 0xFF && thisSceneOffset[1] == 0xFF && thisSceneOffset[2] == 0xFF && thisSceneOffset[3] == 0xFF)
                    {
                        // If the current header has somehow become FF FF FF FF then this terminates, but indicates a flaw in logic
                        sectionCount++;
                        break;
                    }

                    if (nextSceneOffset[0] == 0xFF && nextSceneOffset[1] == 0xFF && nextSceneOffset[2] == 0xFF && nextSceneOffset[3] == 0xFF)
                    {
                        // If next header is FF FF FF FF then we're dealing with the prev file and should deduct 2000h to get its size
                        //sectionCount = 15;
                        compressedSceneSize = 0x2000 - thisSceneInt;
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
                                        newCompressedSize = 16; // First offset in a header block is always 10h
                                    }
                                    else
                                    {
                                        //prevSceneOffset[0] = header[prevHeaderCounter];
                                        //prevSceneOffset[1] = header[prevHeaderCounter + 1];
                                        //prevSceneOffset[2] = header[prevHeaderCounter + 2];
                                        //prevSceneOffset[3] = header[prevHeaderCounter + 3];
                                        //prevHeaderCounter += 4;

                                        prevSceneInt = AllMethods.GetLittleEndianInt(prevSceneOffset, 0);
                                        newCompressedSize = compressedSceneSize - prevSceneInt;
                                    }

                                    comFile.Close();
                                    srcFile.Close();

                                    // The compressed file must be a multiple of 4, so FF bytes are added to end
                                    if (newCompressedSize % 4 == 3)  // Remainder of 3, add 1 FF
                                    {
                                        using (var append = new FileStream(sceneFileRecompressed, FileMode.Append))
                                        {
                                            append.Write(padder, 0, 1);
                                            adjustedCompressedSize = newCompressedSize + 1;
                                        }
                                    }
                                    else if (newCompressedSize % 4 == 2) // Remainder of 2, add 2 FFs
                                    {
                                        using (var append = new FileStream(sceneFileRecompressed, FileMode.Append))
                                        {
                                            append.Write(padder, 0, 2);
                                            adjustedCompressedSize = newCompressedSize + 2;
                                        }
                                    }
                                    else if (newCompressedSize % 4 == 1) // Remainder of 1, add 3 FFs
                                    {
                                        using (var append = new FileStream(sceneFileRecompressed, FileMode.Append))
                                        {
                                            append.Write(padder, 0, 3);
                                            adjustedCompressedSize = newCompressedSize + 3;
                                        }
                                    }
                                    byte[] bytes = BitConverter.GetBytes(newCompressedSize);
                                    header[headerCounter] = bytes[0];
                                    header[headerCounter + 1] = bytes[1];
                                    header[headerCounter + 2] = bytes[2];
                                    header[headerCounter + 3] = bytes[3];
                                }
                            }
                        }
                        brg.Close();
                    }
                    //File.Delete(sceneFileUncompressed);
                    //File.Delete(sceneFileRecompressed);
                    sectionCount++;
                    headerCounter += 4;
                }
                if (sectionCount == 16)
                {
                    using (var outputStream = File.Create(sceneNewHeader))
                    {
                        outputStream.Write(header, 0, 40);

                        foreach (var inputFilePath in inputFilePaths)
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
                }
                headerOffset += 8192;
            }
        }
    }
}