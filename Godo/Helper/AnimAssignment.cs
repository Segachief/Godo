using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo.Helper
{
    public class AnimAssignment
    {
        public static int ReassignAnimations(byte[] modelID, int y, byte[] data, byte[] enemyIDList, int enemyAttackListOffset, int[][] jaggedAttackType, int[][][] jaggedModelAttackTypes, Random rnd)
        {
            // Convert it into an int so we can use it as an array index
            int modelIDInt = EndianConvert.GetLittleEndianIntTwofer(modelID, 0);

            // This is where the list of 16 Animation Indexes get updated to match the enemy's 16 registered AttackIDs
            // Iterate through the 16 attacks of the model and update the data

            // Identifies the Attack ID set for the enemy, converts it into an int, so we can locate it in our Attack Type array
            byte[] attackID = new byte[2];
            attackID = data.Skip(enemyAttackListOffset + y).Take(2).ToArray();
            int attackIDInt = EndianConvert.GetLittleEndianIntTwofer(attackID, 0);


            int anim; // Anim ID
            int terminate = 0; // Terminates random selection if no valid animation can be found for the required type

            // Does this work? Had trouble with checking 65535 in the past; double check this
            if (attackIDInt != 65535)
            {
                // If the Attack ID has a type of 0 (Physical)
                if (jaggedAttackType[attackIDInt][0] == 0)
                {
                    // Execute at least once, and then again until either condition is met or 32 loops made
                    do
                    {
                        anim = rnd.Next(0, jaggedModelAttackTypes[modelIDInt][0].Length);
                        terminate++;
                    } while (terminate < 32 && jaggedModelAttackTypes[modelIDInt][0][anim] == 0);
                    if (terminate < 32)
                    {
                        return jaggedModelAttackTypes[modelIDInt][0][anim];
                    }
                    else
                    {
                        // Universally, all models have an animation of #3.
                        // But this is a risk as the animation may not be suitable.
                        // Possible solution: Track back and revert ModelID at start and in formation ref
                        // (also any changed entries here would need reverted.
                        return 3;
                    }
                }
                // If the Attack ID has a type of 1 (Magical)
                else if (jaggedAttackType[attackIDInt][0] == 1)
                {
                    // Execute at least once, and then again until either condition is met or 32 loops made
                    do
                    {
                        anim = rnd.Next(0, jaggedModelAttackTypes[modelIDInt][1].Length);
                        terminate++;
                    } while (terminate < 32 && jaggedModelAttackTypes[modelIDInt][1][anim] == 0);
                    if (terminate < 32)
                    {
                        return jaggedModelAttackTypes[modelIDInt][1][anim];
                    }
                    else
                    {
                        return 3;
                    }
                }
                // If the Attack ID has a type of 2 (Misc)
                else if (jaggedAttackType[attackIDInt][0] == 2)
                {
                    // Execute at least once, and then again until either condition is met or 32 loops made
                    do
                    {
                        anim = rnd.Next(0, jaggedModelAttackTypes[modelIDInt][2].Length);
                        terminate++;
                    } while (terminate < 32 && jaggedModelAttackTypes[modelIDInt][2][anim] == 0);
                    if (terminate < 32)
                    {
                        return jaggedModelAttackTypes[modelIDInt][2][anim];
                    }
                    else
                    {
                        // This is probably the riskiest assignment as a misc attack has FF on both its Impact + Attack Effect ID Flags
                        // Perhaps a var can be set here to add values to the attack's data in order to prevent a crash if this gets hit?
                        // It would be a bit odd for a misc to have either, but at least it would keep the game running.
                        return 3;
                    }
                }
                else
                {
                    // If this is hit, the AttackType Indexer did not store an AttackID correctly
                    MessageBox.Show("The Animation Indexer for Model Swap failed to identify an AttackID; a backup animation value was set for stability");
                    return 3;
                }
            }
            else
            {
                // If Attack ID was FFFF then Animation Index should also be FF.
                // Setting value directly instead of skipping helps identify left-over assignments if Attack IDs are set but have FF for the Animation Index.
                return 255;
            }
        }
    }
}
