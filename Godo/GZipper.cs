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
            FileStream tfs = File.OpenRead("C:\\Users\\stewart.melville\\Documents\\GZip\\gzip\\ff7_gzip\\SharpZipLibTest\\TARGET.BIN"); // Test Kernel for overwrites

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

            int sectionCount = 0;
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

                    if(sectionCount == 0)
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
                sectionCount++;
            }
        }
    }
}