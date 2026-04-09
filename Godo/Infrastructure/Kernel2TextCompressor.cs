using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godo.Helper;

namespace Godo.Infrastructure
{
    public class Kernel2TextCompressor
    {
        public static void Kernel2Decompress(byte[] input)
        {
            var ms = new MemoryStream();
            var uncompress = new MemoryStream(input);
            uncompress.Position = 4; // Starts at 4th byte to skip header - Kernel2 is one giant LZS file with a single 4byte header giving length
            Lzs.Decode(uncompress, ms); // All the data is now decoded
            System.Diagnostics.Debug.WriteLine("FF:Kernel2Decompress:LZS expanded {0} bytes to {1} bytes", input.Length,
                ms.Length);

            string kernel2StringsOutput = Directory.GetCurrentDirectory() + "\\Kernel2 Strings\\";
            byte[] uncompressedKernel2 = new byte[ms.Length];
            int uncompressedSize;
            byte[] header = new byte[4];
            int headerOffset = 0;
            int r = 9;

            ms.Position = 0;
            ms.Read(uncompressedKernel2, 0, uncompressedKernel2.Length);

            while (r < 27)
            {
                using (var outputStream = File.Create(kernel2StringsOutput + "kernel2.bin" + r))
                {
                    // Writes the kernel2 section using header to get length
                    outputStream.Seek(0, SeekOrigin.Begin);

                    header[0] = uncompressedKernel2[headerOffset];
                    header[1] = uncompressedKernel2[headerOffset + 1];
                    header[2] = uncompressedKernel2[headerOffset + 2];
                    header[3] = uncompressedKernel2[headerOffset + 3];

                    // Section 26 can't have the 4 bytes sheared off the end; one of life's mysteries
                    if (r == 27)
                    {
                        uncompressedSize = EndianConvert.GetLittleEndianInt(header, 0);
                        outputStream.Write(uncompressedKernel2, headerOffset + 4, uncompressedSize);
                    }
                    else
                    {
                        uncompressedSize = EndianConvert.GetLittleEndianInt(header, 0);
                        outputStream.Write(uncompressedKernel2, headerOffset + 4, uncompressedSize);
                    }

                    headerOffset += uncompressedSize + 4;
                }

                r++;
            }
        }

        public static void Kernel2Recompress(byte[] input)
        {
            // Re-encodes the data as LZSS format
            var ms = new MemoryStream(input);
            var compress = new MemoryStream();
            ms.Position = 0;
            Lzs.Encode(ms, compress);
            System.Diagnostics.Debug.WriteLine("FF:Kernel2Decompress:LZS shrank {0} bytes to {1} bytes", input.Length,
                compress.Length);

            // Container for our compressed data; adds 4 to include header space
            byte[] data = new byte[compress.Length + 4];

            // Produces the header (4 bytes)
            byte[] header = BitConverter.GetBytes(data.Length - 4);
            data[0] = header[0];
            data[1] = header[1];
            data[2] = header[2];
            data[3] = header[3];

            // Writes the encoded data to a byte array
            compress.Position = 0;
            compress.Read(data, 4, (int)compress.Length);

            // Produces the finished file
            string produceFile = Directory.GetCurrentDirectory() + "\\Output Files\\kernel2.bin";
            using (var outputStream = File.Create(produceFile))
            {
                outputStream.Position = 0;
                outputStream.Write(data, 0, data.Length);
            }
        }

        // To use the LZS Decompress on random misc files; unrelated to randomiser
        public static void MiscDecompress(byte[] input)
        {
            var ms = new MemoryStream();
            var uncompress = new MemoryStream(input);
            uncompress.Position = 4; // Starts at 4th byte to skip header
            Lzs.Decode(uncompress, ms); // All the data is now decoded
            System.Diagnostics.Debug.WriteLine("FF:MiscFile:LZS expanded {0} bytes to {1} bytes", input.Length,
                ms.Length);

            byte[] timHeader =
            {
                0x10, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x2C, 0x00, 0x00, 0x00, 0x10, 0x00,
                0xEF, 0x01, 0x10, 0x00, 0x01, 0x00, 0xFF, 0x7F, 0xF8, 0x0D, 0xD7, 0x4A, 0x74, 0x3E,
                0x73, 0x15, 0xF0, 0x35, 0xEE, 0x00, 0x8D, 0x29, 0x6B, 0x49, 0xE9, 0x18, 0x67, 0x08,
                0xE5, 0x2C, 0x45, 0x00, 0x82, 0x20, 0x21, 0x04, 0x00, 0x00, 0x0C, 0x03, 0x00, 0x00,
                0xF8, 0x03, 0x50, 0x00, 0x08, 0x00, 0x30, 0x00
            };
            int r = 0;
            while (r < 127)
            {
                string miscFileOutput = Directory.GetCurrentDirectory() + "\\MiscOutput\\image" + r;
                byte[] uncompressedMiscFile = new byte[ms.Length];

                ms.Position = 16;
                ms.Read(uncompressedMiscFile, 0, 512);

                using (var outputStream = File.Create(miscFileOutput))
                {
                    outputStream.Seek(0, SeekOrigin.Begin);
                    outputStream.Write(timHeader, 0, timHeader.Length);
                    outputStream.Write(uncompressedMiscFile, 0, 512);
                }
                r++;
            }
        }
    }
}
