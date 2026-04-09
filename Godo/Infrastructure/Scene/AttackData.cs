using Godo.Indexing;
using Godo.Omnichange;
using System;

namespace Godo.Infrastructure.Scene
{
    public class AttackData
    {
        public static byte[] SceneAttacks(byte[] data, int o, Random rnd,
            bool[] enemyAttackOptions, int[] enemyAttackParameters,
            int sceneID)
        {
            int r = 0;
            int c = 0;

            while (r < 32)
            {
                // Skip if MP cost = 65536 or Target flags are 0 - indicates a blank attack entry
                if (data[o + 4] != 255 && data[o + 5] != 255)
                {
                    // Attack %
                    if (enemyAttackOptions[0])
                    {
                        data[o] = AccuracyChange.AdjustAccuracy(data[o], enemyAttackParameters[0], rnd);
                    }
                    o++;

                    // Impact Effect ID - Set to a value to catch mismatched attack-anim types
                    if (data[o] == 255)
                    {
                        data[o] = 0x2D;
                    }
                    o++;

                    // Target Hurt Action Index
                    // 00 = Standard
                    // 01 = Stunned
                    // 02 = Heavy
                    // 03 = Ejected
                    //data[o] = rnd.Next(0, 4); o++;
                    data[o] = data[o]; o++;

                    // Unknown
                    o++;

                    // Casting Cost
                    if (enemyAttackOptions[1])
                    {
                        data[o] = MPChange.AdjustMP(data[o], enemyAttackParameters[1], rnd); o++;
                        data[o] = 0; o++;
                    }
                    else
                    {
                        o += 2;
                    }

                    // Impact Sound - Must be FFFF if Attack Effect ID is not FF
                    if (data[o] == 255)
                    {
                        data[o] = 0x0F; o++;
                        data[o] = 0x00; o++;
                    }
                    else
                    {
                        o += 2;
                    }

                    // Camera Movement ID for single target - FFFF if none
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Camera Movement ID for multi target - FFFF if none
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Target Flags - Logic will be tough for this one; will depend on attack element + attack type as some aren't designed for multi-target
                    data[o] = data[o]; o++;

                    // Attack Effect ID - Must be FF if Impact Effect is not FF
                    if (data[o] == 255 || enemyAttackOptions[3])
                    {
                        data[o] = (byte)SpellIndex.CheckValidEnemyAttackAnimIndex(rnd);
                    }
                    o++;

                    // Damage Calculation - Excludes Reactor No.1 enemies
                    if (enemyAttackOptions[4] && (sceneID <= 74 || sceneID >= 83))
                    {
                        data[o] = FormulaChange.PickEnemyDamageFormula(rnd);
                    }
                    o++;

                    // Base Power - Excludes Reactor No.1 enemies
                    if (enemyAttackOptions[2] && sceneID <= 74 || sceneID >= 83)
                    {
                        data[o] = WeaponChange.AdjustBasePower(data[o], data[o - 1], enemyAttackParameters[2], rnd);
                    }
                    o++;

                    // Condition Sub-Menu Flags
                    // 00 = Party HP
                    // 01 = Party MP
                    // 02 = Party Status
                    // Other = None
                    data[o] = data[o]; o++;

                    // Status Effect Chance
                    // 00-3F = Chance to inflict/heal status (#/63)
                    // 40 = Remove Status
                    // 80 - Toggle Status
                    // This is changed later if Status Effects Safe/Unsafe are active
                    data[o] = data[o]; o++;

                    // Attack Additional Effects
                    data[o] = data[o]; o++;

                    // Additional Effects Modifier Value
                    data[o] = data[o]; o++;

                    // Ignore Reactor 1 scenes for balancing (solo Cloud is vulnerable, so is Cloud/Barret duo)
                    if (sceneID < 75 && sceneID > 82)
                    {

                        #region Status info
                        /* Statuses (by flag)
                         * 1 = Death
                         * 2 = Near-Death
                         * 4 = Sleep
                         * 8 = Poison
                         * 16 = Sadness
                         * 32 = Fury
                         * 64 = Confusion
                         * 128 = Silence

                         * 1 = Haste
                         * 2 = Slow
                         * 4 = Stop
                         * 8 = Frog
                         * 16 = Mini
                         * 32 = Slow-Numb
                         * 64 = Petrify
                         * 128 = Regen

                         * 1 = Barrier
                         * 2 = MBarrier
                         * 4 = Reflect
                         * 8 = Dual
                         * 16 = Shield
                         * 32 = D. Sentence
                         * 64 = Manip
                         * 128 = Berserk

                         * 1 = Peerless
                         * 2 = Paralysis
                         * 4 = Darkness
                         * 8 = Dual-Drain
                         * 16 = Death Force
                         * 32 = Resist
                         * 64 = Lucky Girl
                         * 128 = Imprisoned
                         */
                        #endregion

                        // All statuses including OHKO can be picked
                        if (enemyAttackOptions[8])
                        {
                            int picker = rnd.Next(4);
                            int[] status = new int[] { 1, 2, 4, 8, 16, 32, 64, 128 };

                            // Sets status chance
                            data[o - 3] = (byte)rnd.Next(0, 64);

                            if (picker == 0)
                            {
                                picker = rnd.Next(2, 8); // Prevents Death and Near-Death being set
                                data[o] = (byte)status[picker]; o++;
                                data[o] = 0; o++;
                                data[o] = 0; o++;
                                data[o] = 0; o++;

                            }
                            else if (picker == 1)
                            {
                                picker = rnd.Next(2, 7); // Prevents Petrify, and Regen being set
                                data[o] = 0; o++;
                                data[o] = (byte)status[picker]; o++;
                                data[o] = 0; o++;
                                data[o] = 0; o++;
                            }
                            else if (picker == 2)
                            {
                                data[o - 3] = 255;
                                o += 4;
                            }
                            else
                            {
                                picker = 4; // Only Darkness set
                                data[o] = 0; o++;
                                data[o] = 0; o++;
                                data[o] = 0; o++;
                                data[o] = (byte)status[picker]; o++;
                            }
                        }
                        // Mild - OHKOs/Disables not enabled
                        else if (enemyAttackOptions[7])
                        {
                            int picker = rnd.Next(4);
                            int[] status = new int[] { 1, 2, 4, 8, 16, 32, 64, 128 };

                            // Sets status chance
                            data[o - 3] = (byte)rnd.Next(0, 64);

                            if (picker == 0)
                            {
                                picker = rnd.Next(0, 7);
                                data[o] = (byte)status[picker]; o++;
                                data[o] = 0; o++;
                                data[o] = 0; o++;
                                data[o] = 0; o++;
                            }
                            else if (picker == 1)
                            {
                                picker = rnd.Next(0, 6); // Prevents Regen being set
                                data[o] = 0; o++;
                                data[o] = (byte)status[picker]; o++;
                                data[o] = 0; o++;
                                data[o] = 0; o++;
                            }
                            else if (picker == 2)
                            {
                                picker = rnd.Next(1, 7); // Prevents Barrier being set
                                data[o] = 0; o++;
                                data[o] = 0; o++;
                                data[o] = (byte)status[picker]; o++;
                                data[o] = 0; o++;
                            }
                            else
                            {
                                picker = rnd.Next(1, 7); // Prevents Peerless being set
                                if (picker == 8 && data[o - 1] != 8) // Checks for Dual-Drain, sets Dual on previous byte
                                {
                                    data[o - 1] = 8;
                                }
                                data[o] = 0; o++;
                                data[o] = 0; o++;
                                data[o] = 0; o++;
                                data[o] = (byte)status[picker]; o++;
                            }
                        }
                        else
                        {
                            // No status assignment enabled
                            o += 4;
                        }
                    }
                    else
                    {
                        // If Reactor 1 scenes, skip status assignment
                        o += 4;
                    }

                    // Elements
                    if (enemyAttackOptions[6])
                    {
                        int picker = rnd.Next(2);
                        int[] element = new int[] { 1, 2, 4, 8, 16, 32, 64, 128 };

                        if (picker == 0)
                        {
                            picker = rnd.Next(0, 7);
                            data[o] = (byte)element[picker]; o++;
                            data[o] = 0; o++;
                        }
                        else if (picker == 1)
                        {
                            picker = rnd.Next(0, 7);
                            data[o] = 0; o++;
                            data[o] = (byte)element[picker]; o++;
                        }
                        else
                        {
                            o += 2;
                        }
                    }
                    else
                    {
                        o += 2;
                    }

                    // Special Attack Flags
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                }
                else
                {
                    while (c < 28)
                    {
                        data[o] = data[o]; o++;
                        c++;
                    }
                    c = 0;
                }
                r++;
            }
            r = 0;
            return data;
        }
    }
}
