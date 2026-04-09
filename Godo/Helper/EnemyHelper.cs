using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo.Helper
{
    public class EnemyHelper
    {
        public static void EnemyConsistency(byte[] enemyIDList, byte[][] jaggedEnemyData, byte[] data, int o)
        {
            byte[] enemyOneModelID = new byte[2];
            byte[] enemyTwoModelID = new byte[2];
            byte[] enemyThreeModelID = new byte[2];

            enemyOneModelID[0] = enemyIDList[0];
            enemyOneModelID[1] = enemyIDList[1];

            enemyTwoModelID[0] = enemyIDList[2];
            enemyTwoModelID[1] = enemyIDList[3];

            enemyThreeModelID[0] = enemyIDList[4];
            enemyThreeModelID[1] = enemyIDList[5];

            ulong firstModelID = (ulong)EndianConvert.GetLittleEndianIntTwofer(enemyOneModelID, 0);
            ulong secondModelID = (ulong)EndianConvert.GetLittleEndianIntTwofer(enemyTwoModelID, 0);
            ulong thirdModelID = (ulong)EndianConvert.GetLittleEndianIntTwofer(enemyThreeModelID, 0);

            bool firstEnemyExists = false;
            bool secondEnemyExists = false;
            bool thirdEnemyExists = false;

            bool firstEnemyNull = false;
            bool secondEnemyNull = false;
            bool thirdEnemyNull = false;

            if (firstModelID > 675)
            {
                firstEnemyNull = true;
            }
            else if (jaggedEnemyData[firstModelID] != null)
            {
                firstEnemyExists = true;
            }

            if (secondModelID > 675)
            {
                secondEnemyNull = true;
            }
            else if (jaggedEnemyData[secondModelID] != null)
            {
                secondEnemyExists = true;
            }

            if (thirdModelID > 675)
            {
                thirdEnemyNull = true;
            }
            else if (jaggedEnemyData[thirdModelID] != null)
            {
                thirdEnemyExists = true;
            }

            // We rewrite enemy data here if it existed already, or we store the newly randomised data
            // to the associated Model ID for future use
            if (firstEnemyNull)
            {
                // Model ID is null/invalid, skip assignment altogether
                o += 184;
            }
            else if (firstEnemyExists)
            {
                // We want to assign the existing enemy data here
                int z = 0;
                while (z < 184)
                {
                    data[o] = jaggedEnemyData[firstModelID][z];
                    o++;
                    z++;
                }
            }
            else
            {
                // We want to assign the new enemy modelID data to our array for future use
                jaggedEnemyData[firstModelID] = new byte[185];
                int z = 0;
                while (z < 184)
                {
                    jaggedEnemyData[firstModelID][z] = data[o];
                    o++;
                    z++;
                }
            }

            if (secondEnemyNull)
            {
                o += 184;
            }
            else if (secondEnemyExists)
            {
                int z = 0;
                while (z < 184)
                {
                    data[o] = jaggedEnemyData[secondModelID][z];
                    o++;
                    z++;
                }
            }
            else
            {
                jaggedEnemyData[secondModelID] = new byte[185];
                int z = 0;
                while (z < 184)
                {
                    jaggedEnemyData[secondModelID][z] = data[o];
                    o++;
                    z++;
                }
            }

            if (thirdEnemyNull)
            {
                o += 184;
            }
            else if (thirdEnemyExists)
            {
                int z = 0;
                while (z < 184)
                {
                    data[o] = jaggedEnemyData[thirdModelID][z];
                    o++;
                    z++;
                }
            }
            else
            {
                jaggedEnemyData[thirdModelID] = new byte[185];
                int z = 0;
                while (z < 184)
                {
                    jaggedEnemyData[thirdModelID][z] = data[o];
                    o++;
                    z++;
                }
            }
        }

        public static bool CheckGravityResistance(byte[] data, int o)
        {
            // Checks for gravity resistance in the enemy's elemental information
            // Enemies that are tougher than usual, such as bosses or strong solo mobs,
            // tend to have gravity resistance and in lieu of any explicit marker that
            // an enemy is to be a boss or tough solo enemy, this should cover most cases

            // Set to the index where elemental resistances are stored
            o += 40;

            // Check for Gravity element (0x05)
            int[] checkGravity =
            {
                data[o], data[o + 1], data[o + 2], data[o + 3],
                data[o + 4], data[o + 5], data[o + 6], data[o + 7]
            };
            int gravityElementIndex = Array.IndexOf(checkGravity, 0x05);

            // Check that the Gravity element is set with Nullify (coincidentally 0x05 again)
            if (data[o + 8 + gravityElementIndex] == 0x05)
            {
                return true;
            }
            return false;
        }

        public static bool CheckBossSet(ulong modelID)
        {
            // Bosses - Used to apply different stat scaling
            ulong[] bossSet = { 10, 11, 13, 15, 22, 33, 37, 48, 64, 66, 67,
                68, 71, 81, 91, 95, 111, 127, 128, 139, 154, 155, 156, 163,
                164, 177, 178, 179, 180, 181, 182, 193, 195, 196, 228, 229,
                234, 258, 259, 260, 265, 266, 267, 268, 271, 272, 273, 274,
                275, 281, 296, 297, 298, 299, 300, 305, 306, 309, 324, 325,
                326, 327, 328, 329, 330, 331, 333, 334, 335, 336
            };

            if (bossSet.Contains(modelID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
