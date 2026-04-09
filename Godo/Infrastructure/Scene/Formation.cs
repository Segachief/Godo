using System;

namespace Godo.Infrastructure.Scene
{
    public class Formation
    {
        public static byte[] SceneFormation(byte[] data, int o, bool[] formationOptions, bool[] specialHackOptions, bool bossGroup, byte[] initCam, int k, Random rnd)
        {
            int r = 0;
            while (r < 4)
            {
                // If first value is FF (Battle BG), then assume it is an empty formation and skip
                if (data[o] != 255)
                {
                    // Battle BG/Location
                    if (formationOptions[2])
                    {
                        // Excludes Safer & Final BGs
                        do
                        {
                            data[o] = (byte)rnd.Next(89); // ID of the Battle BG
                        } while (data[o] == 0x4E && data[o] == 0x39);
                        o++;
                        data[o] = data[o]; o++; // Always 0; despite being a 2-byte value, valid values never exceed 59h
                    }
                    else
                    {
                        // Retain the Battle BG
                        o += 2;
                    }

                    // Next Formation ID, this transitions to another enemy formation directly after current enemies defeated; like Battle Square but not random.
                    data[o] = data[o]; o++; // FFFF by default, no new battle will load
                    data[o] = data[o]; o++;

                    // Escape Counter; value of 0009 makes battle unescapable; 2-byte but value never exceeds 0009
                    if (specialHackOptions[1] != false)
                    {
                        data[o] = 9; o++;
                        data[o] = 0; o++;
                    }
                    else
                    {
                        o += 2;
                    }

                    // Unused - 2byte
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Battle Square - Possible Next Battles (4x 2-byte formation IDs, one is selected at random; default value for no battle is 03E7
                    data[o] = data[o]; o++; // Battle 1
                    data[o] = data[o]; o++;

                    data[o] = data[o]; o++; // Battle 2
                    data[o] = data[o]; o++;

                    data[o] = data[o]; o++; // Battle 3
                    data[o] = data[o]; o++;

                    data[o] = data[o]; o++; // Battle 4
                    data[o] = data[o]; o++;

                    // Escapable Flag (misc flags such as disabling pre-emptive)
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++; // Value of FDFF; FE F9 would prevent pre-emptives

                    // Battle Layout Type: Side attack, pincer, back attack, etc. 9 types.
                    /*
                        00 - Normal fight
                        01 - Preemptive
                        02 - Back attack
                        03 - Side attack
                        04 - Attacked from both sides (pincer attack, reverse side attack)
                        05 - Another attack from both sides battle (different maybe?)
                        06 - Another side attack
                        07 - A third side attack
                        08 - Normal battle that locks you in the front row, change command is disabled
                    */
                    if (specialHackOptions[0] != false && bossGroup == false)
                    {
                        // If using enemy swarm, we're updating coords so drop the Pincers/Backs etc.
                        // Unless it's a boss fight like Air Buster
                        data[o] = 0; o++;
                    }
                    else
                    {
                        data[o] = data[o]; o++;
                    }

                    if (formationOptions[0] != false)
                    {
                        // Indexed pre-battle camera position
                        // This is linked to the camera data, need to be careful what value is used
                        // Array has 4 bytes for the four formations, should iterate 4 times and no more.
                        data[o] = initCam[k]; o++; k++;
                    }
                    else
                    {
                        o++;
                    }
                }
                else
                {
                    o += 20;
                    k++; // Initial Camera increment
                }
                r++;
            }
            return data;
        }
    }
}
