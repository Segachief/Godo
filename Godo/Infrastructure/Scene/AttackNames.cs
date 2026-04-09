using Godo.Helper;
using System;

namespace Godo.Infrastructure.Scene
{
    public class AttackNames
    {
        public static byte[] SceneAttackNames(byte[] data, int o, Random rnd,
            bool[] enemyAttackOptions)
        {
            int c = 0;
            byte[] nameBytes;
            while (c < 32)
            {
                // Attack Name, 32 bytes ascii
                if (data[o] != 255 && enemyAttackOptions[5])
                {
                    nameBytes = Misc.NameGenerate(rnd);
                    data[o] = nameBytes[0]; o++;
                    data[o] = nameBytes[1]; o++;
                    data[o] = nameBytes[2]; o++;
                    data[o] = nameBytes[3]; o++;
                    int rngID = rnd.Next(2); // Chance to append a longer name
                    if (rngID == 1)
                    {
                        data[o] = nameBytes[4]; o++;
                        data[o] = nameBytes[5]; o++;
                        data[o] = nameBytes[6]; o++;
                        data[o] = nameBytes[7]; o++;
                    }
                    else
                    {
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                        data[o] = 255; o++;
                    }
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++;
                    data[o] = 255; o++; // Empty - Use FF to terminate the string
                }
                else
                {
                    o += 32;
                }
                c++;
            }
            return data;
        }
    }
}
