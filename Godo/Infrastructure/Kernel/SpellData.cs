using Godo.Helper;
using Godo.Indexing;
using Godo.Omnichange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo.Infrastructure.Kernel
{
    public class SpellData
    {
        // Section 1: Attack Data - Spells
        public static byte[] RandomiseSpells(byte[] data,
            bool[] spellOptions, int[] spellParameters,
            bool[] summonOptions, int[] summonParameters,
            bool[] skillOptions, int[] skillParameters,
            bool[] specialOptions, Random rnd)
        {
            #region Section Information
            /* Player-available attacks. It should be noted that text strings are not stored with the
             * related data in 99% of cases, but instead at the back of the kernel in sections.
             * 
             * There are 128 actions that can be edited; the tool appears to list 256 but the remaining 128
             * do not exist and are actually stored in the .EXE (Limit Breaks). They have text pointers in
             * the kernel but nothing else.
             * 
             * The data available to modify (28 bytes each):
             * Hit %            (1)
             * Impact Effect    (1)
             * Target Hurt Anim (1)
             * Unknown          (1)
             * MP Cost          (2)
             * Impact Sound     (2)
             * Camera Single    (2)
             * Camera Multi     (2)
             * Target Flag      (1)
             * Attack Anim ID   (1)
             * Damage Calc      (1)
             * Base Power       (1)
             * Restore Type     (1)
             * Status Toggle Type   (1)
             * Additional Effects   (1)
             * ^ Modifier           (1)
             * Status Effects Mask  (4)
             * Elements Mask        (2)
             * Special Attack Flags (2)
            */
            #endregion

            int r = 0;
            int o = 0;

            try
            {
                while (r < 54)
                {
                    if (r == 25 || r == 26)
                    {
                        // Skip Escape and Remove
                        o += 4;
                        if (specialOptions[3])
                        {
                            data[o] = 0; o++;
                            data[o] = 0; o++;
                        }
                        else
                        {
                            o += 2;
                        }
                        o += 22;
                    }
                    else
                    {
                        // Attack %
                        if (spellOptions[0])
                        {
                            data[o] = AccuracyChange.AdjustAccuracy(data[o], spellParameters[0], rnd); o++;
                        }
                        else
                        {
                            o++;
                        }

                        // Impact Effect
                        data[o] = data[o]; o++;

                        // Target Hurt Anim
                        data[o] = data[o]; o++;

                        // Unknown
                        data[o] = data[o]; o++;

                        // MP Cost
                        // Spellspring - No MP Cost
                        if (specialOptions[3])
                        {
                            data[o] = 0; o++;
                            data[o] = 0; o++;
                        }
                        else if (spellOptions[1])
                        {
                            // Randomise MP
                            data[o] = MPChange.AdjustMP(data[o], spellParameters[1], rnd); o++;
                            data[o] = 0; o++;
                        }
                        else
                        {
                            // No change
                            o += 2;
                        }

                        // Impact Sound
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Camera Single
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Camera Multiple
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Target Flag
                        data[o] = data[o]; o++;

                        // Anim ID
                        // Spells
                        if (spellOptions[3])
                        {
                            data[o] = (byte)SpellIndex.CheckValidSpellIndex(rnd); o++;
                        }
                        else
                        {
                            o++;
                        }

                        // Damage Calc
                        if (spellOptions[4])
                        {
                            data[o] = FormulaChange.PickSpellDamageFormula(rnd); o++;
                        }
                        else
                        {
                            o++;
                        }

                        // Base Power
                        // First, check if the previous Damage Calc assigned with %-based damage
                        if (spellOptions[2])
                        {
                            data[o] = WeaponChange.AdjustBasePower(data[o], data[o - 1], spellParameters[2], rnd); o++;
                        }
                        else
                        {
                            o++;
                        }

                        // Restore Type
                        data[o] = data[o]; o++;

                        // Status Toggle Type
                        data[o] = data[o]; o++;

                        // Additional Effects
                        data[o] = data[o]; o++;

                        // Modifier for Additional Effects
                        data[o] = data[o]; o++;

                        // Status Effect Mask
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Elements Mask
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;

                        // Special Attack Flags
                        data[o] = data[o]; o++;
                        data[o] = data[o]; o++;
                        // Just setting this to get it to compile to test form
                        // Need to redo if checks anyway when new forms are up
                    }
                    r++;
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #1 (Attack Data: Spells) has failed to randomise");
            }

            // Need to skip two blank entries
            r = 56;
            o += 56;

            // Summons
            try
            {
                while (r > 55 && r < 72)
                {
                    // Attack %
                    if (summonOptions[0] != false)
                    {
                        data[o] = AccuracyChange.AdjustAccuracy(data[o], summonParameters[0], rnd); o++;
                    }
                    else
                    {
                        o++;
                    }

                    // Impact Effect
                    data[o] = data[o]; o++;

                    // Target Hurt Anim
                    data[o] = data[o]; o++;

                    // Unknown
                    data[o] = data[o]; o++;

                    // MP Cost
                    if (specialOptions[3] != false)
                    {
                        // Spellspring - No MP Cost
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                    }
                    else if (summonOptions[1] != false)
                    {
                        // Randomise MP
                        data[o] = MPChange.AdjustMP(data[o], summonParameters[1], rnd); o++;
                        data[o] = 0; o++;
                    }
                    else
                    {
                        // No change
                        o += 2;
                    }

                    // Impact Sound
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Camera Single
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Camera Multiple
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Target Flag
                    data[o] = data[o]; o++;

                    // Anim ID
                    if (summonOptions[3] != false)
                    {
                        data[o] = (byte)SpellIndex.CheckValidSummonIndex(rnd); o++;
                    }
                    else
                    {
                        o++;
                    }

                    // Damage Calc
                    if (summonOptions[4] != false)
                    {
                        data[o] = FormulaChange.PickSpellDamageFormula(rnd); o++;
                    }
                    else
                    {
                        o++;
                    }

                    // Base Power
                    // First, check if the previous Damage Calc assigned with %-based damage
                    if (summonOptions[2] != false)
                    {
                        data[o] = WeaponChange.AdjustBasePower(data[o], data[o - 1], summonParameters[2], rnd); o++;
                    }
                    else
                    {
                        o++;
                    }

                    // Restore Type
                    data[o] = data[o]; o++;

                    // Status Toggle Type
                    data[o] = data[o]; o++;

                    // Additional Effects
                    data[o] = data[o]; o++;

                    // Modifier for Additional Effects
                    data[o] = data[o]; o++;

                    // Status Effect Mask
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Elements Mask
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Special Attack Flags
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    // Just setting this to get it to compile to test form
                    // Need to redo if checks anyway when new forms are up
                    r++;
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #1 (Attack Data: Summons) has failed to randomise");
            }

            // Enemy Skills
            try
            {
                while (r > 71 && r < 96)
                {
                    // Attack %
                    if (skillOptions[0])
                    {
                        data[o] = AccuracyChange.AdjustAccuracy(data[o], skillParameters[0], rnd); o++;
                    }
                    else
                    {
                        o++;
                    }

                    // Impact Effect
                    data[o] = data[o]; o++;

                    // Target Hurt Anim
                    data[o] = data[o]; o++;

                    // Unknown
                    data[o] = data[o]; o++;

                    // MP Cost
                    if (specialOptions[3])
                    {
                        // Spellspring - No MP Cost
                        data[o] = 0; o++;
                        data[o] = 0; o++;
                    }
                    else if (skillOptions[1])
                    {
                        // Randomise MP
                        data[o] = MPChange.AdjustMP(data[o], skillParameters[1], rnd); o++;
                        data[o] = 0; o++;
                    }
                    else
                    {
                        // No change
                        o += 2;
                    }

                    // Impact Sound
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Camera Single
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Camera Multiple
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Target Flag
                    data[o] = data[o]; o++;

                    // Anim ID
                    // Spells
                    if (skillOptions[3] != false)
                    {
                        data[o] = (byte)SpellIndex.CheckValidEnemySkillIndex(rnd); o++;
                    }
                    else
                    {
                        o++;
                    }

                    // Damage Calc
                    if (skillOptions[4] != false)
                    {
                        data[o] = FormulaChange.PickSpellDamageFormula(rnd); o++;
                    }
                    else
                    {
                        o++;
                    }

                    // Base Power
                    // First, check if the previous Damage Calc assigned with %-based damage
                    if (skillOptions[2] != false)
                    {
                        data[o] = WeaponChange.AdjustBasePower(data[o], data[o - 1], skillParameters[2], rnd); o++;
                    }
                    else
                    {
                        o++;
                    }

                    // Restore Type
                    data[o] = data[o]; o++;

                    // Status Toggle Type
                    data[o] = data[o]; o++;

                    // Additional Effects
                    data[o] = data[o]; o++;

                    // Modifier for Additional Effects
                    data[o] = data[o]; o++;

                    // Status Effect Mask
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Elements Mask
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Special Attack Flags
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    // Just setting this to get it to compile to test form
                    // Need to redo if checks anyway when new forms are up
                    r++;
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #1 (Attack Data: Enemy Skills) has failed to randomise");
            }

            return data;
        }
    }
}
