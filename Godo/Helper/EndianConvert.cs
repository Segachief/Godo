using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Helper
{
    static class EndianConvert // used to be public class, changed to static to add WriteInt methods. Change back if issues.
    {

        // This converts little endian values to int
        public static int GetLittleEndianInt(byte[] data, int startIndex)
        {
            return (data[startIndex + 3] << 24)
                 | (data[startIndex + 2] << 16)
                 | (data[startIndex + 1] << 8)
                 | data[startIndex];
        }

        public static int GetLittleEndianIntTwofer(byte[] data, int startIndex)
        {
            return (data[startIndex + 1] << 8)
                 | data[startIndex];
        }

        public static int GetPreviousLittleEndianInt(byte[] data, int startIndex)
        {
            return (data[startIndex - 1] << 24)
                 | (data[startIndex - 2] << 16)
                 | (data[startIndex - 3] << 8)
                 | data[startIndex - 4];
        }

        public static void WriteInt(byte[] data, int offset, int value)
        {
            data[offset + 0] = (byte)(value & 0xff);
            data[offset + 1] = (byte)((value >> 8) & 0xff);
            data[offset + 2] = (byte)((value >> 16) & 0xff);
            data[offset + 3] = (byte)((value >> 24) & 0xff);
        }

        public static void WriteInt(this System.IO.Stream s, int i)
        {
            var data = BitConverter.GetBytes(i);
            s.Write(data, 0, 4);
        }

        // This converts a ulong value (16-bit number) to a 2-byte little endian value (8-bit per byte)
        public static byte[] GetLittleEndianConvert(ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            // If it was big endian, reverse it
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }

        // This converts an int value (32-bit number) to a 4-byte little endian value (8-bit per byte)
        public static byte[] GetLittleEndianIntConvert(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            // If it was big endian, reverse it
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }

        public static byte[] AddLittleEndian(byte[] a, byte[] b)
        {
            List<byte> result = new List<byte>();
            if (a.Length < b.Length)
            {
                byte[] t = a;
                a = b;
                b = t;
            }
            int carry = 0;
            for (int i = 0; i < b.Length; ++i)
            {
                int sum = a[i] + b[i] + carry;
                result.Add((byte)(sum & 0xFF));
                carry = sum >> 8;
            }
            for (int i = b.Length; i < a.Length; ++i)
            {
                int sum = a[i] + carry;
                result.Add((byte)(sum & 0xFF));
                carry = sum >> 8;
            }
            if (carry > 0)
            {
                result.Add((byte)carry);
            }
            return result.ToArray();
        }
    }
}