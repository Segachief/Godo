using Godo.Helper;
using Godo.Omnichange;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Godo.Infrastructure.Scene
{
    public class EnemyData
    {
        public static int[][] SceneAttackArray(byte[] data, int[][] jaggedAttackType)
        {
            int c = 0;
            int k = 0;
            int y = 0;
            // Before processing enemy data, we first build an array for all this scene's attacks and their type if Enemy Swap is on

            // Iterate through all 32 entries for attacks in this scene
            while (c < 32)
            {
                byte[] attackID = new byte[2];
                int type;

                // Checks AttackID isn't blank and then takes it, converts it into Int for array index
                if (data[2113 + k] != 255)
                {
                    // Attack IDs are stored separately from the attack data, appearing after it; hence difference in offset
                    attackID = data.Skip(2112 + k).Take(2).ToArray();
                    int attackIDInt = EndianConvert.GetLittleEndianIntTwofer(attackID, 0);

                    // Checks impact effect ID to determine if physical
                    if (data[1217 + y] != 255)
                    {
                        type = 0; // Assigns this AttackID as Physical
                        jaggedAttackType[attackIDInt] = new int[] { type };
                    }
                    // Checks Attack Effect ID to determine if Magic
                    else if (data[1229 + y] != 255)
                    {
                        type = 1; // Assigns this AttackID as Magic
                        jaggedAttackType[attackIDInt] = new int[] { type };
                    }
                    else
                    {
                        type = 2; // Assigns this AttackID as Misc
                        jaggedAttackType[attackIDInt] = new int[] { type };
                    }
                }
                c++;
                k += 2; // Next Attack ID
                y += 28; // Next Attack Data
            }
            return jaggedAttackType;
        }

        public static byte[] SceneEnemyData(byte[] data, int o, byte[] nameBytes, Random rnd,
             bool[] enemyStatOptions, int[] enemyStatParameters,
             bool[] balancingOptions, int[] balancingParameters,
             bool[] challengeOptions,
             bool[] enemyItemOptions, bool[] specialHackOptions,
             bool[] swapOptions, bool excludedScene, bool excludedModel, bool[] swapOption, bool swapInModel, bool bossGroup,
             byte[] enemyIDList, int enemyAttackListOffset, int[][] jaggedAttackType, int[][][] jaggedModelAttackTypes, int sceneID,
             bool[] interimOptions
             )
        {
            int r = 0;
            int c = 0;
            int y = 0;
            string error = "";

            try
            {
                // Iterate through the 3 enemies and their specific data
                while (r < 3)
                {
                    int i = 0;

                    // If first byte is FF, assume no enemy is there and just retain pre-existing data
                    if (data[o] != 255)
                    {
                        #region Enemy Balancer Values
                        //Enemy Balancing
                        // 0 = Luck
                        // 1 = Evade
                        // 2 = Strength
                        // 3 = Defence
                        // 4 = Magic
                        // 5 = Magic Defence
                        // 6 = HP
                        #endregion
                        error = "balancer";
                        var enemyBalancer = EnemyChange.AssignEnemyBalancing(rnd);
                        bool toughFlag = EnemyHelper.CheckGravityResistance(data, o);

                        //EnemyChange.EnemyNamingway(data, o, rnd);
                        o += 32;

                        // Enemy Stats
                        // Level
                        if (interimOptions[5])
                        {
                            error = "Level";
                            data[o] = EnemyChange.AdjustLevel(data[o], sceneID, r, toughFlag, rnd);
                            int level = data[o];
                            o++;

                            // Speed
                            error = "Speed";
                            data[o] = EnemyChange.AdjustSpeed(data[o], sceneID, r, toughFlag, rnd);
                            o++;

                            // This sets enemy speed low if the Swarm hack is used to make it more manageable
                            //if (specialHackOptions[0])
                            //{
                            //    data[o - 1] = 10;
                            //}

                            // Luck
                            error = "Luck";
                            data[o] = EnemyChange.AdjustLuck(data[o], enemyBalancer[0], sceneID, r, toughFlag, rnd);
                            o++;

                            // Evade
                            error = "Evade";
                            data[o] = EnemyChange.AdjustEvade(data[o], enemyBalancer[1], sceneID, r, toughFlag, rnd);
                            o++;

                            // Strength
                            error = "Strength";
                            data[o] = EnemyChange.AdjustStrength(data[o], enemyBalancer[2], sceneID, r, toughFlag, rnd);
                            o++;

                            // Defence
                            error = "Defence";
                            data[o] = EnemyChange.AdjustDefence(data[o], enemyBalancer[3], sceneID, r, toughFlag, rnd);
                            o++;

                            // Magic
                            error = "Magic";
                            data[o] = EnemyChange.AdjustMagic(data[o], enemyBalancer[4], sceneID, r, level, toughFlag, rnd);
                            o++;

                            // Magic Defence
                            error = "Magic Defence";
                            data[o] = EnemyChange.AdjustMagicDefence(data[o], enemyBalancer[5], sceneID, r, toughFlag, rnd);
                            o++;
                        }
                        else
                        {
                            o += 8;
                        }

                        EnemyChange.AdjustElemental(data, o, toughFlag, rnd);
                        o += 16;



                        // Animation Index List
                        // Grab the current ModelID and identify if it is excluded/swap-in
                        //byte[] modelID = new byte[2];
                        //if (r == 0)
                        //{
                        //    modelID[0] = enemyIDList[0];
                        //    modelID[1] = enemyIDList[1];
                        //}
                        //else if (r == 1)
                        //{
                        //    modelID[0] = enemyIDList[2];
                        //    modelID[1] = enemyIDList[3];
                        //}
                        //else if (r == 2)
                        //{
                        //    modelID[0] = enemyIDList[4];
                        //    modelID[1] = enemyIDList[5];
                        //}
                        //ulong currentModelIDInt = (ulong)EndianConvert.GetLittleEndianIntTwofer(modelID, 0);
                        //excludedModel = ModelFilters.CheckSwapIn(currentModelIDInt);
                        //swapInModel = ModelFilters.CheckSwapIn(currentModelIDInt);

                        //// Excluded Scene/Model, do not reassign
                        //if (excludedScene == true || excludedModel == true)
                        //{
                        //    o += 16;
                        //    enemyAttackListOffset += 184; // Offset for next enemy's attack list
                        //}
                        //// If Risky Swap is off, and the model is registered as a swapInModel, do not reassign
                        //else if (swapOptions[1] == false && swapInModel == true)
                        //{
                        //    o += 16;
                        //    enemyAttackListOffset += 184; // Offset for next enemy's attack list
                        //}
                        //// If Model Swap is on, original was a boss, and boss swap not on then don't swap anims
                        //else if (swapOptions[0] && swapOptions[3] == false && bossGroup == true)
                        //{
                        //    o += 16;
                        //    enemyAttackListOffset += 184; // Offset for next enemy's attack list
                        //}
                        //else if (swapOptions[0])
                        //{
                        //    // If we made it here, and model swap is on, then reassign anims
                        //    y = 0;
                        //    while (c < 16)
                        //    {
                        //        data[o] = (byte)AnimAssignment.ReassignAnimations(modelID, y, data, enemyIDList, enemyAttackListOffset, jaggedAttackType, jaggedModelAttackTypes, rnd); o++;
                        //        y += 2; // Next Anim ID
                        //        c++;
                        //    }
                        //    c = 0;
                        //    enemyAttackListOffset += 184; // Offset for next enemy's attack list
                        //}
                        //else
                        //{
                        // Model Swap not enabled; leaves the Animation Index list alone
                        o += 16;
                        enemyAttackListOffset += 184; // Offset for next enemy's attack list
                        //}

                        // Enemy Attack IDs for matching to Animation IDs - 2bytes per attack ID
                        error = "Attack IDs to Animation IDs";
                        while (c < 16)
                        {
                            data[o] = data[o];
                            o++;
                            data[o] = data[o];
                            o++;
                            c++;
                        }
                        c = 0;

                        // Enemy Camera Override IDs for matching to Animation IDs - 2bytes per Camera Override ID - FFFF by default
                        error = "Camera Overrides";
                        while (c < 16)
                        {
                            data[o] = data[o];
                            o++;
                            data[o] = data[o];
                            o++;
                            c++;
                        }
                        c = 0;

                        if (interimOptions[6])
                        {
                            EnemyChange.AdjustItems(data, o, toughFlag, sceneID, rnd);
                        }
                        o += 12;


                        // Manipulate/Berserk Attack IDs
                        // The first listed attack is the Berserk option; all 3 attacks can be selected for use under Manipulate
                        // If Status Immunities option is on, we must make sure an attack is set in here for Berserk
                        error = "Berserk/Manipulate IDs";
                        //if (enemyStatOptions[15])
                        //{
                        //    // If no attack is set in slot 1, we get the first AttackID for this enemy and set that
                        //    if (data[o] == 255 && data[o + 1] == 255)
                        //    {
                        //        byte[] attackID = new byte[2];
                        //        attackID = data.Skip(736).Take(2).ToArray();

                        //        data[o] = attackID[0];
                        //        o++;
                        //        data[o] = attackID[1];
                        //        o++;
                        //    }
                        //    else
                        //    {
                        //        // Otherwise, leave it alone
                        //        o += 2;
                        //    }

                        //    // Rest are left alone
                        //    o += 4;
                        //}
                        //else
                        //{
                        // Attack 1
                        data[o] = data[o];
                        o++;
                        data[o] = data[o];
                        o++;

                        // Attack 2
                        data[o] = data[o];
                        o++;
                        data[o] = data[o];
                        o++;

                        // Attack 3
                        data[o] = data[o];
                        o++;
                        data[o] = data[o];
                        o++;
                        //}

                        // Unknown Data - Padding
                        data[o] = data[o];
                        o++;
                        data[o] = data[o];
                        o++;


                        // Enemy MP
                        error = "MP";
                        //if (enemyStatOptions[8])
                        //{
                        //    if (bossGroup == true)
                        //    {
                        //        // For bosses
                        //        //data[o] = EnemyChange.AdjustStats(data[o], enemyStatParameters[8], rnd); 
                        //        o++;
                        //        //data[o] = EnemyChange.AdjustStats(data[o], enemyStatParameters[8], rnd); 
                        //        o++;
                        //    }
                        //    else
                        //    {
                        //        // For standard enemies
                        //        //data[o] = EnemyChange.AdjustStats(data[o], enemyStatParameters[8], rnd);
                        //        o++;
                        //        //data[o] = EnemyChange.AdjustStats(data[o], enemyStatParameters[8], rnd);
                        //        o++;
                        //    }
                        //}
                        //else
                        //{
                        // Leave MP alone
                        o += 2;
                        //}

                        // Enemy AP
                        error = "AP";
                        // No AP Option
                        //if (challengeOptions[8])
                        //{
                        //    data[o] = 0;
                        //    o++;
                        //    data[o] = 0;
                        //    o++;
                        //}
                        //else if (balancingOptions[3])
                        //{
                        //    data[o] = 136;
                        //    o++;
                        //    data[o] = 19;
                        //    o++;
                        //}
                        //// Poverty Option - AP limited to 1 byte
                        //else if (specialHackOptions[2])
                        //{
                        //    data[o] = data[o];
                        //    o++;
                        //    data[o] = 0;
                        //    o++;
                        //}
                        //// Randomise AP
                        //else if (enemyStatOptions[11])
                        //{
                        //    if (bossGroup == true)
                        //    {
                        //        data[o] = APChange.AdjustAP(data[o], enemyStatParameters[11], rnd);
                        //        o++;
                        //        data[o] = APChange.AdjustAP(data[o], enemyStatParameters[11], rnd);
                        //        o++;
                        //    }
                        //    else
                        //    {
                        //        data[o] = APChange.AdjustAP(data[o], enemyStatParameters[11], rnd);
                        //        o++;
                        //        data[o] = APChange.AdjustAP(data[o], enemyStatParameters[11], rnd);
                        //        o++;
                        //    }
                        //}
                        //else
                        //{
                        // Leave AP alone
                        o += 2;
                        //}

                        if (interimOptions[6])
                        {
                            EnemyChange.AdjustMorph(data, o, rnd);
                        }
                        o += 2;

                        // Back Attack multiplier
                        o++;

                        // Alignment FF
                        o++;

                        // Enemy HP
                        error = "HP";
                        if (interimOptions[5])
                        {
                            var hpArray = new byte[4];
                            hpArray[0] = data[o];
                            hpArray[1] = data[o + 1];
                            hpArray[2] = data[o + 2];
                            hpArray[3] = data[o + 3];
                            int enemyHP = BitConverter.ToInt32(hpArray, 0);

                            enemyHP = EnemyChange.AdjustHP(enemyHP, enemyBalancer[6], sceneID, r, toughFlag, rnd);

                            // Converts into little endian
                            byte[] converted = EndianConvert.GetLittleEndianIntConvert(enemyHP);
                            data[o] = converted[0];
                            o++;
                            data[o] = converted[1];
                            o++;
                            data[o] = converted[2];
                            o++;
                            data[o] = converted[3];
                            o++;
                        }
                        else
                        {
                            o += 4;
                        }

                        //if (specialHackOptions[0] && bossGroup == false)
                        //{
                        //    // If swarm is on, reduce HP
                        //    data[o - 4] = data[o] != 0 ? (byte)(data[o - 4] / 2) : data[o];
                        //    if (data[o - 3] != 0)
                        //    {
                        //        data[o - 3] = data[o] != 0 ? (byte)(data[o - 4] / 2) : data[o];
                        //    }
                        //}

                        //if (specialHackOptions[4] && bossGroup == true)
                        //{
                        //    // If swarm is on, reduce HP
                        //    data[o - 4] = data[o] != 0 ? (byte)(data[o - 4] / 2) : data[o];
                        //    if (data[o - 3] != 0)
                        //    {
                        //        data[o - 3] = data[o] != 0 ? (byte)(data[o - 4] / 2) : data[o];
                        //    }
                        //}

                        // EXP Points
                        error = "EXP";
                        // No EXP option
                        //if (challengeOptions[6] != false)
                        //{
                        //    data[o] = 0;
                        //    o++;
                        //    data[o] = 0;
                        //    o++;
                        //    data[o] = 0;
                        //    o++;
                        //    data[o] = 0;
                        //    o++;
                        //}
                        //// Poverty Mode EXP - 1byte
                        //else if (specialHackOptions[2] != false)
                        //{
                        //    data[o] = data[o];
                        //    o++;
                        //    data[o] = 0;
                        //    o++;
                        //    data[o] = 0;
                        //    o++;
                        //    data[o] = 0;
                        //    o++;
                        //}
                        //// Randomise EXP
                        //else if (enemyStatOptions[9])
                        //{
                        //    if (bossGroup == true)
                        //    {
                        //        // For bosses
                        //        //data[o] = EnemyChange.AdjustStats(data[o], enemyStatParameters[9], rnd);
                        //        o++;
                        //        //data[o] = EnemyChange.AdjustStats(data[o], enemyStatParameters[9], rnd);
                        //        o++;
                        //        o++;
                        //        o++;
                        //    }
                        //    else
                        //    {
                        //        // For standard enemies
                        //        //data[o] = EnemyChange.AdjustStats(data[o], enemyStatParameters[9], rnd);
                        //        o++;
                        //        //data[o] = EnemyChange.AdjustStats(data[o], enemyStatParameters[9], rnd);
                        //        o++;
                        //        o++;
                        //        o++;
                        //    }
                        //}
                        //else
                        //{
                        // Leave EXP alone
                        o += 4;
                        //}

                        // Gil
                        error = "Gil";
                        // No Gil option
                        //if (challengeOptions[7] != false)
                        //{
                        //    data[o] = 0;
                        //    o++;
                        //    data[o] = 0;
                        //    o++;
                        //    data[o] = 0;
                        //    o++;
                        //    data[o] = 0;
                        //    o++;
                        //}
                        //// Poverty Mode - 1 byte
                        //else if (specialHackOptions[2] != false)
                        //{
                        //    data[o] = data[o];
                        //    o++;
                        //    data[o] = 0;
                        //    o++;
                        //    data[o] = 0;
                        //    o++;
                        //    data[o] = 0;
                        //    o++;
                        //}
                        //// Randomise Gil
                        //else if (enemyStatOptions[10])
                        //{
                        //    if (bossGroup == true)
                        //    {
                        //        // For bosses
                        //        //data[o] = EnemyChange.AdjustStats(data[o], enemyStatParameters[7], rnd);
                        //        o++;
                        //        //data[o] = EnemyChange.AdjustStats(data[o], enemyStatParameters[7], rnd);
                        //        o++;
                        //        o += 2;
                        //    }
                        //    else
                        //    {
                        //        // For standard enemies
                        //        //data[o] = EnemyChange.AdjustStats(data[o], enemyStatParameters[7], rnd);
                        //        o++;
                        //        //data[o] = EnemyChange.AdjustStats(data[o], enemyStatParameters[7], rnd);
                        //        o++;
                        //        o += 2;
                        //    }
                        //}
                        //else
                        //{
                        // Leave Gil alone
                        o += 4;
                        //}

                        // Status Immunities
                        error = "Statuses";
                        int picker = rnd.Next(4);
                        int[] status = new int[] { 1, 2, 4, 8, 16, 32, 64, 128 };

                        //if (enemyStatOptions[15])
                        //{
                        //    if (bossGroup == true)
                        //    {
                        //        if (picker == 0)
                        //        {
                        //            picker = rnd.Next(2, 8); // Prevents Death
                        //            data[o] = (byte)status[picker];
                        //            o++;
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = 0;
                        //            o++;

                        //        }
                        //        else if (picker == 1)
                        //        {
                        //            picker = rnd.Next(8);
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = (byte)status[picker];
                        //            o++;
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = 0;
                        //            o++;
                        //        }
                        //        else if (picker == 2)
                        //        {
                        //            picker = rnd.Next(0, 6); // Prevents Berserk/Manip
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = (byte)status[picker];
                        //            o++;
                        //            data[o] = 0;
                        //            o++;
                        //        }
                        //        else
                        //        {
                        //            picker = rnd.Next(2, 3); // Only Paralysis/Darkness available
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = (byte)status[picker];
                        //            o++;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (picker == 0)
                        //        {
                        //            picker = rnd.Next(0, 8);
                        //            data[o] = (byte)status[picker];
                        //            o++;
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = 0;
                        //            o++;

                        //        }
                        //        else if (picker == 1)
                        //        {
                        //            picker = rnd.Next(8);
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = (byte)status[picker];
                        //            o++;
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = 0;
                        //            o++;
                        //        }
                        //        else if (picker == 2)
                        //        {
                        //            picker = rnd.Next(8);
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = (byte)status[picker];
                        //            o++;
                        //            data[o] = 0;
                        //            o++;
                        //        }
                        //        else
                        //        {
                        //            picker = rnd.Next(8);
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = 0;
                        //            o++;
                        //            data[o] = (byte)status[picker];
                        //            o++;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        // Leave status immunity info alone
                        o += 4;
                        //}

                        // Padding FF
                        error = "End of file";
                        data[o] = 255;
                        o++;
                        data[o] = 255;
                        o++;
                        data[o] = 255;
                        o++;
                        data[o] = 255;
                        o++;
                    }
                    else
                    {
                        // Retain enemy info
                        o += 184;
                    }

                    r++;
                }
            }
            catch
            {
                MessageBox.Show("Enemy Data Section has failed to Randomise: " + error + sceneID);
            }

            return data;
        }
    }
}