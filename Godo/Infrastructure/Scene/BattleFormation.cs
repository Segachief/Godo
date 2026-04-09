using Godo.Helper;

namespace Godo.Infrastructure.Scene
{
    public class BattleFormation
    {
        public static byte[] SceneBattleFormation(byte[] data, int o, bool[] specialHackOptions, int[] specialHackParameters,
            byte[] form, int sceneID, byte[] enemyIDList, bool bossInScene, bool excludeSwarm, ulong enemyA, ulong enemyB, ulong enemyC)
        {
            int r = 0;
            int c = 0;
            int k = 0;
            int i = 0; // Used to count Enemies in this formation
            if(sceneID == 122)
            {
                //catch
            }
            while (r < 4)
            {
                // First, to allocate the Enemy IDs to the 6 possible formation slots.
                // This is complicated by Model Swapping and the Enemy Swarm functions.
                // First, we do the Model Swap (if any exist).
                // Next, we do the enemy swarm function which looks at the current Model IDs and uses those.
                while (c < 6)
                {
                    // Checks that the current enemy placement entry is not null
                    if (data[o] != 255 && data[o + 1] != 255)
                    {
                        // If we changed the enemy IDs, then we need to match the old IDs to the new ones
                        // so that we're replacing the references to them correctly in the formation

                        // We get the current Model ID first from this formation entry
                        byte[] currentModelID = new byte[2];
                        currentModelID[0] = data[o];
                        currentModelID[1] = data[o + 1];
                        ulong currentModelIDInt = (ulong)EndianConvert.GetLittleEndianIntTwofer(currentModelID, 0);

                        // It gets compared to the original Model IDs that we collected at the start before
                        // randomisation to determine if it is Enemy A, B, or C.
                        if (currentModelIDInt == enemyA)
                        {
                            data[o] = enemyIDList[0]; o++;
                            data[o] = enemyIDList[1]; o++;
                            i++;
                        }
                        else if (currentModelIDInt == enemyB)
                        {
                            // This can't be null, because the check at start would have hit it
                            data[o] = enemyIDList[2]; o++;
                            data[o] = enemyIDList[3]; o++;
                            i++;
                        }
                        else if (currentModelIDInt == enemyC)
                        {
                            // Same here
                            data[o] = enemyIDList[4]; o++;
                            data[o] = enemyIDList[5]; o++;
                            i++;
                        }
                        // Enemy Swarm - Set it to Enemy A's ID
                        else if ((specialHackOptions[0] || specialHackOptions[4]) && excludeSwarm == false)
                        {
                            // We can assume that the first enemy is never null, very few scenes have Enemy A as no entry
                            // And those that do are unused
                            data[o] = enemyIDList[0]; o++;
                            data[o] = enemyIDList[1]; o++;
                        }
                        else
                        {
                            // If we reached this point, then the Enemy ID wasn't null yet didn't match the stored IDs
                            // Seems to fire when modded files are used. Removed Error Message for now.
                            o += 2;
                        }

                        // XYZ + Row/Cover Flags for the enemy, populated by a pre-built array from Indexer.cs
                        // Is only used if Enemy Swarm is enabled for giving the new enemies coords
                        if (specialHackOptions[0] && bossInScene == false && excludeSwarm == false)
                        {
                            // X Coordinate
                            data[o] = form[k]; o++; k++;
                            data[o] = form[k]; o++; k++;

                            // Y Coordinate
                            data[o] = form[k]; o++; k++;
                            data[o] = form[k]; o++; k++;

                            // Z Coordinate
                            data[o] = form[k]; o++; k++;
                            data[o] = form[k]; o++; k++;

                            // Row
                            data[o] = form[k]; o++; k++;
                            data[o] = form[k]; o++; k++;

                            // Cover Flags (should be related to Row)
                            data[o] = form[k]; o++; k++;
                            data[o] = form[k]; o++; k++;
                        }
                        else if(specialHackOptions[4] && bossInScene && excludeSwarm == false)
                        {
                            // X Coordinate
                            data[o] = form[k]; o++; k++;
                            data[o] = form[k]; o++; k++;

                            // Y Coordinate
                            data[o] = form[k]; o++; k++;
                            data[o] = form[k]; o++; k++;

                            // Z Coordinate
                            data[o] = form[k]; o++; k++;
                            data[o] = form[k]; o++; k++;

                            // Row
                            data[o] = form[k]; o++; k++;
                            data[o] = form[k]; o++; k++;

                            // Cover Flags (should be related to Row)
                            data[o] = data[o]; o++; k++;
                            data[o] = data[o]; o++; k++;
                        }
                        else
                        {
                            // X Coordinate
                            data[o] = data[o]; o++;
                            data[o] = data[o]; o++;

                            // Y Coordinate
                            data[o] = data[o]; o++;
                            data[o] = data[o]; o++;

                            // Z Coordinate
                            data[o] = data[o]; o++;
                            data[o] = data[o]; o++;

                            // Row
                            data[o] = data[o]; o++;
                            data[o] = data[o]; o++;

                            // Cover Flags
                            data[o] = data[o]; o++;
                            data[o] = data[o]; o++;
                        }

                        // Initial Condition Flags; only the last 5 bits are considered - FF FF FF FF is default
                        // Best this is disabled and it just retains what's there, prevents issues
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                    }
                    // If Enemy Swarm is enabled, we attempt to add a new enemy here as the current entry is null
                    // We don't want to do this, however, if the Boss flag was enabled at any point
                    // Entries are added until the counter matches/exceeds the Parameter for swarm
                    else if (specialHackOptions[0] && i < specialHackParameters[0] && bossInScene == false && excludeSwarm == false)
                    {
                        // For now, just going to add Enemy A as the duped enemy.
                        // Later, will revive the RND for Enemy A, B, C
                        data[o] = enemyIDList[0]; o++;
                        data[o] = enemyIDList[1]; o++;
                        i++;

                        // X Coordinate
                        data[o] = form[k]; o++; k++;
                        data[o] = form[k]; o++; k++;

                        // Y Coordinate
                        data[o] = form[k]; o++; k++;
                        data[o] = form[k]; o++; k++;

                        // Z Coordinate
                        data[o] = form[k]; o++; k++;
                        data[o] = form[k]; o++; k++;

                        // Row
                        data[o] = form[k]; o++; k++;
                        data[o] = form[k]; o++; k++;

                        // Cover Flags (should be related to Row)
                        data[o] = form[k]; o++; k++;
                        data[o] = form[k]; o++; k++;

                        // Initial Condition Flags; only the last 5 bits are considered - FF FF FF FF is default
                        // As above, disabling this as it isn't a good idea - Retain flags instead
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                    }
                    // If Boss Swarm is enabled, and this is a boss formation, add extra copies of the boss
                    else if (specialHackOptions[4] && i < specialHackParameters[1] && bossInScene && excludeSwarm == false)
                    {
                        // For now, just going to add Enemy A as the duped enemy.
                        // Later, will revive the RND for Enemy A, B, C
                        data[o] = enemyIDList[0]; o++;
                        data[o] = enemyIDList[1]; o++;
                        i++;

                        // X Coordinate
                        data[o] = form[k]; o++; k++;
                        data[o] = form[k]; o++; k++;

                        // Y Coordinate
                        data[o] = form[k]; o++; k++;
                        data[o] = form[k]; o++; k++;

                        // Z Coordinate
                        data[o] = form[k]; o++; k++;
                        data[o] = form[k]; o++; k++;

                        // Row
                        data[o] = form[k]; o++; k++;
                        data[o] = form[k]; o++; k++;

                        // Cover Flags (should be related to Row)
                        data[o] = form[k]; o++; k++;
                        data[o] = form[k]; o++; k++;

                        // Initial Condition Flags; only the last 5 bits are considered - FF FF FF FF is default
                        // As above, disabling this as it isn't a good idea - Retain flags instead
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                    }
                    else
                    {
                        // Entry left unchanged
                        o += 16;
                        k += 10;
                    }
                    c++;
                }
                i = 0;
                c = 0;
                k = 0;
                r++;
            }
            return data;
        }
    }
}
