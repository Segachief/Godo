using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Helper
{
    public static class Lzs
    {
        private const int N = 4096;
        private const int F = 18;
        private const int THRESHOLD = 2;
        private const int NIL = N;

        // Input is a stream of bytes that is either LZS compressed or decompressed.
        // It then runs it through the Encode/Decode Context methods in this class.
        public static void Encode(Stream input, Stream output)
        {
            new EncodeContext().Encode(input, output);
        }
        public static void Decode(Stream input, Stream output)
        {
            new EncodeContext().Decode(input, output);
        }

        private class EncodeContext
        {
            public byte[] buffer = new byte[N + F];
            public int MatchPos, MatchLen;
            public int[] Lson = new int[N + 1];
            public int[] Rson = new int[N + 257];
            public int[] Dad = new int[N + 1];

            public void InitTree()
            {
                for (int i = N + 1; i <= N + 256; i++) Rson[i] = NIL;
                for (int i = 0; i < N; i++) Dad[i] = NIL;
            }

            public void InsertNode(int r)
            {
                int i, p, cmp;
                int key = r;
                cmp = 1;
                p = N + 1 + buffer[key];
                Rson[r] = Lson[r] = NIL;
                MatchLen = 0;

                while (true)
                {
                    if (cmp >= 0)
                    {
                        if (Rson[p] != NIL)
                            p = Rson[p];
                        else
                        {
                            Rson[p] = r;
                            Dad[r] = p;
                            return;
                        }
                    }
                    else
                    {
                        if (Lson[p] != NIL)
                            p = Lson[p];
                        else
                        {
                            Lson[p] = r;
                            Dad[r] = p;
                            return;
                        }
                    }
                    for (i = 1; i < F; i++)
                        if ((cmp = buffer[key + i] - buffer[p + i]) != 0) break;
                    if (i > MatchLen)
                    {
                        MatchPos = p;
                        if ((MatchLen = i) >= F) break;
                    }
                }
                Dad[r] = Dad[p]; Lson[r] = Lson[p]; Rson[r] = Rson[p];
                Dad[Lson[p]] = r; Dad[Rson[p]] = r;
                if (Rson[Dad[p]] == p)
                    Rson[Dad[p]] = r;
                else
                    Lson[Dad[p]] = r;
                Dad[p] = NIL;
            }

            public void DeleteNode(int p)
            {
                int q;
                if (Dad[p] == NIL) return;
                if (Rson[p] == NIL)
                    q = Lson[p];
                else if (Lson[p] == NIL)
                    q = Rson[p];
                else
                {
                    q = Lson[p];
                    if (Rson[q] != NIL)
                    {
                        do
                        {
                            q = Rson[q];
                        } while (Rson[q] != NIL);
                        Rson[Dad[q]] = Lson[q]; Dad[Lson[q]] = Dad[q];
                        Lson[q] = Lson[p]; Dad[Lson[p]] = q;
                    }
                    Rson[q] = Rson[p]; Dad[Rson[p]] = q;
                }
                Dad[q] = Dad[p];
                if (Rson[Dad[p]] == p)
                    Rson[Dad[p]] = q;
                else
                    Lson[Dad[p]] = q;
                Dad[p] = NIL;
            }

            // Was Encode(void)
            public void Encode(Stream input, Stream output)
            {
                int i, c, len, r, s, last_match_length, code_buf_ptr;
                byte[] code_buf = new byte[17];
                byte mask;

                // Intialise Trees
                InitTree();

                /* Note on code_buf
                 * code_buf[1..16] saves eight units of code.
                 * code_buf[0] works as eight flags; 1 = unencoded letter (1byte), 0 a position-and-length pair (2bytes)
                 * Thus, eight units require at most 16 bytes of code
                 */
                code_buf[0] = 0;
                code_buf_ptr = mask = 1;
                s = 0; r = N - F;
                for (i = s; i < r; i++) buffer[i] = 0;
                for (len = 0; len < F && (c = input.ReadByte()) != -1; len++)
                    buffer[r + len] = (byte)c;              // Read F bytes into the last F bytes of the buffer
                if (len == 0) return;                       // Text of size 0
                for (i = 1; i <= F; i++) InsertNode(r - i);  /* Insert the F strings,
		                                                        each of which begins with one or more 'space' characters.
		                                                        Note the order in which these strings are inserted.
		                                                        This way, degenerate trees will be less likely to occur. */

                // Finally, insert the whole string just read.
                // The global variables match_length and match_position are set.
                InsertNode(r);
                do
                {
                    // match_length may be spuriously long near the end of text.
                    if (MatchLen > len) MatchLen = len;
                    if (MatchLen <= THRESHOLD)
                    {
                        // Not long enough match; send one byte.
                        MatchLen = 1;
                        // 'send one byte' flag
                        code_buf[0] |= mask;
                        // Send uncoded
                        code_buf[code_buf_ptr++] = buffer[r];
                    }
                    else
                    {
                        code_buf[code_buf_ptr++] = (byte)MatchPos;
                        // Send position and length pair. Note match_length > THRESHOLD.
                        code_buf[code_buf_ptr++] = (byte)(((MatchPos >> 4) & 0xf0) | (MatchLen - (THRESHOLD + 1)));
                    }
                    if ((mask <<= 1) == 0)
                    {  // Shift mask left one bit.
                        // Send at most 8 units of code together
                        for (i = 0; i < code_buf_ptr; i++)
                            output.WriteByte(code_buf[i]);
                        code_buf[0] = 0; code_buf_ptr = mask = 1;
                    }
                    last_match_length = MatchLen;
                    for (i = 0; i < last_match_length &&
                            (c = input.ReadByte()) != -1; i++)
                    {
                        // Delete old strings and read new bytes
                        DeleteNode(s);
                        buffer[s] = (byte)c;

                        // If the position is near the end of buffer,
                        //  extend the buffer to make string comparison easier.
                        if (s < F - 1) buffer[s + N] = (byte)c;
                        s = (s + 1) & (N - 1); r = (r + 1) & (N - 1);
                        // Since this is a ring buffer, increment the position modulo N.
                        // Register the string in text_buf[r..r+F-1]
                        InsertNode(r);
                    }
                    while (i++ < last_match_length)
                    {	// After the end of text, no need to read, but buffer may not be empty
                        DeleteNode(s);
                        s = (s + 1) & (N - 1); r = (r + 1) & (N - 1);
                        if ((--len) != 0) InsertNode(r);
                    }
                    // Until length is 0
                } while (len > 0);
                if (code_buf_ptr > 1)
                {		/* Send remaining code. */
                    for (i = 0; i < code_buf_ptr; i++) output.WriteByte(code_buf[i]);
                }
                return;
            }


            // was Decode(void) - Just the reverse of Encode().
            public void Decode(Stream input, Stream output)
            {
                int i, j, k, r, c;
                int flags;

                for (i = 0; i < N - F; i++) buffer[i] = 0;
                r = N - F; flags = 0;
                for (; ; )
                {
                    if (((flags >>= 1) & 256) == 0)
                    {
                        // Uses higher byte to count 8
                        if ((c = input.ReadByte()) == -1) break;
                        flags = c | 0xff00;
                    }
                    if ((flags & 1) != 0)
                    {
                        if ((c = input.ReadByte()) == -1) break;
                        output.WriteByte((byte)c); buffer[r++] = (byte)c; r &= (N - 1);
                    }
                    else
                    {
                        if ((i = input.ReadByte()) == -1) break;
                        if ((j = input.ReadByte()) == -1) break;
                        i |= ((j & 0xf0) << 4); j = (j & 0x0f) + THRESHOLD;
                        for (k = 0; k <= j; k++)
                        {
                            c = buffer[(i + k) & (N - 1)];
                            output.WriteByte((byte)c); buffer[r++] = (byte)c; r &= (N - 1);
                        }
                    }
                }
                return;
            }
        }
    }
}
