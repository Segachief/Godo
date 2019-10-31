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
    public class AltGZipper
    {
        public static void PrepareScene(string filename)
        {
            string gzipFileName = filename;                         // Opens the specified file
            string targetDir = Path.GetDirectoryName(filename);     // Get directory where the target file resides

            byte[] header = new byte[64];                       /* Stores the block header
                                                                 * [0-4] = Offset for first GZipped data file (3 enemies per file)
                                                                 * Header total size must be 40h - empty entries == FF FF FF FF
                                                                 */

            int[][] jaggedSceneInfo = new int[256][];           // An array of arrays, containing offset, size, and absoluteoffset for each scene file
            ArrayList listedSceneData = new ArrayList();
            //List<byte[]> listedSceneData = new List<byte[]>();  // An list of byte arrays, containing all the compressed scene data
            int size;           // The size of the compressed file
            int offset;         // Stores the current scene offset
            int nextOffset;     // Stores the next scene offset
            int absoluteOffset; // Stores the scene's absolute offset in the scene.bin
            int headerOffset = 0; // Offset of the current block header; goes up in 2000h (8192) increments

            int r = 0;
            int o = 0;
            int c = 0;
            int k = 0;

            // Entire file is read; offsets and sizes for scenes are extracted and placed in a jagged array (an array of arrays)
            while (headerOffset < 262144) // 32 blocks of 2000h/8192 bytes each
            {
                FileStream stepOne = new FileStream(gzipFileName, FileMode.Open, FileAccess.Read); // Opens and reads the default scene.bin
                stepOne.Seek(headerOffset, SeekOrigin.Begin); // Should never exceed 40h/64d as header max size is 40h and each enemy needs a 4-byte offset. So that's room for 16 enemies only.
                stepOne.Read(header, 0, 64);
                stepOne.Close();

                while (r < 16) // Max of 16 sections (is usually less)
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

                        jaggedSceneInfo[o] = new int[] { offset, size, absoluteOffset };
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

            /*
             * Using absolute offset + compressed size, decompress the file.
             * Run the file through scene randomiser
             * Recompress the file
             * Update the size; this will be used to update offset in last step when files are allocated to blocks
            */

            while (r < 255)
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
                AltScene.RandomiseScene(uncompressedScene);

                // This may not be the correct compression format, but it may also be a more space-efficient GZIP format which would help
                byte[] recompressedScene = Ionic.Zlib.ZlibStream.CompressBuffer(uncompressedScene);
                listedSceneData.Add(recompressedScene);
                o++;
                r++;
            }

        }

        //FileStream srcFile = File.OpenRead(sceneFileUncompressed);
        //GZipOutputStream zipFile = new GZipOutputStream(File.Open(sceneFileRecompressed, FileMode.Create));
        //try
        //{
        //    byte[] FileData = new byte[srcFile.Length];
        //    srcFile.Read(FileData, 0, (int)srcFile.Length);
        //    zipFile.Write(FileData, 0, FileData.Length);
        //}
        //catch
        //{
        //    MessageBox.Show("Failed to compress", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}
        //finally
        //{
        //    zipFile.Close();
        //    FileStream comFile = File.OpenRead(sceneFileRecompressed);
    }
}