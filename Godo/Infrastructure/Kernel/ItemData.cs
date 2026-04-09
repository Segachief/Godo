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
    public class ItemData
    {
        public static byte[] RandomiseItems(byte[] data,
            bool[] attackOptions, int[] attackParameters,
            bool[] healOptions, int[] healParameters,
            bool[] statusOptions, int[] statusParameters,
            bool[] challengeOptions,
            Random rnd)
        {
            #region Section Information
            /* Item Data
             * 
             * The data available to modify (28 bytes each):
             * 8x FF Mask       (8)
             * Camera Move ID   (2)
             * Restriction Mask (2) - 01 can be sold, 02 can be used in battle, 04 can be used in menu
             * Target Flags     (1)
             * Attack Effect ID (1)
             * Damage Calc.     (1)
             * Base Power       (1)
             * Conditions       (1) - 00 party HP, 01 party MP, 02 party status, other: none
             * Status Chance    (1) - 3F chance to inflict (/63), 40 Cure, 80 Toggle
             * Attack Additional Effects   (1)
             * Additional Effects Modifier (1)
             * Status Effects   (4)
             * Attack Element   (2)
             * Special Attack Flags (2)
            */
            #endregion

            int r = 0;
            int o = 0;
            int itemType = 0;
            int[] status = new int[] { 1, 2, 4, 8, 16, 32, 64, 128 };
            int picker;

            try
            {
                while (r < 105)
                {
                    itemType = ItemHelper.ItemType(r);

                    // FF Mask
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Camera Move ID
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Use Restriction Mask   
                    if (challengeOptions[4] != false)
                    {
                        data[o] = 254; o++;
                        data[o] = 255; o++;
                    }
                    else
                    {
                        o += 2;
                    }

                    // Target Flags
                    data[o] = data[o]; o++;

                    // Skip this item (Smoke Bomb, etc.)
                    if (itemType == 0)
                    {
                        o += 3;
                    }

                    // Attack Type
                    if (itemType == 1)
                    {
                        // Attack Effect ID
                        if (attackOptions[1] != false)
                        {
                            data[o] = (byte)ItemIndex.CheckValidItemAnimationIndex(rnd); o++;
                        }
                        else
                        {
                            o++;
                        }

                        // Damage Calc
                        if (attackOptions[2] != false)
                        {
                            data[o] = FormulaChange.PickItemDamageFormula(rnd); o++;
                        }
                        else
                        {
                            o++;
                        }

                        // Base Power
                        if (attackOptions[0] != false)
                        {
                            data[o] = WeaponChange.AdjustBasePower(data[o], data[o - 1], attackParameters[0], rnd); o++;
                        }
                        else
                        {
                            o++;
                        }
                    }
                    // Heal Type
                    else if (itemType == 2)
                    {
                        if (healOptions[1] != false)
                        {
                            data[o] = (byte)ItemIndex.CheckValidItemAnimationIndex(rnd); o++;
                        }
                        else
                        {
                            o++;
                        }

                        // Damage Calc
                        if (healOptions[2] != false)
                        {
                            data[o] = FormulaChange.PickItemDamageFormula(rnd); o++;
                        }
                        else
                        {
                            o++;
                        }

                        // Base Power
                        if (healOptions[0] != false)
                        {
                            data[o] = WeaponChange.AdjustBasePower(data[o], data[o - 1], healParameters[0], rnd); o++;
                        }
                        else
                        {
                            o++;
                        }
                    }
                    // Status Type
                    else if (itemType == 3)
                    {
                        if (statusOptions[0] != false)
                        {
                            data[o] = (byte)ItemIndex.CheckValidItemAnimationIndex(rnd); o++;
                        }
                        else
                        {
                            o++;
                        }
                        // Damage Calc & Base Power
                        o+=2;
                    }

                    // Conditions
                    data[o] = data[o]; o++;

                    // Status Chance
                    if (itemType == 3)
                    {
                        data[o] = (byte)rnd.Next(5, 25);
                    }
                    else
                    {
                        o++;
                    }

                    // Additional Effects
                    data[o] = data[o]; o++;

                    // Additional Effects Modifier
                    data[o] = data[o]; o++;

                    // Status Effects
                    if (itemType == 3)
                    {
                        picker = rnd.Next(4);
                        if (picker == 0)
                        {
                            picker = rnd.Next(0, 8);
                            data[o] = (byte)status[picker]; o++;
                            o++;
                            o++;
                            o++;

                        }
                        else if (picker == 1)
                        {
                            picker = rnd.Next(0, 8);
                            o++;
                            data[o] = (byte)status[picker]; o++;
                            o++;
                            o++;
                        }
                        else if (picker == 2)
                        {
                            picker = rnd.Next(0, 8);
                            o++;
                            o++;
                            data[o] = (byte)status[picker]; o++;
                            o++;
                        }
                        else
                        {
                            picker = rnd.Next(0, 7); // Prevents 'Imprisoned/Disabled' flag
                            if(picker == 3)
                            {
                                data[o - 1] = 3; // Sets Dual Flag on previous status byte
                            }
                            o++;
                            o++;
                            o++;
                            data[o] = (byte)status[picker]; o++;
                        }
                    }
                    else
                    {
                        o += 4;
                    }

                    // Attack Element
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Special Attack Flags
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    r++;
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #4 (Item Data) has failed to randomise");
            }
            return data;
        }
    }
}
