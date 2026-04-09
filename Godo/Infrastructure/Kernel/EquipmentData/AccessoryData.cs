using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Godo.Omnichange;

namespace Godo.Infrastructure.Kernel.EquipmentData
{
    public class AccessoryData
    {
        public static byte[] RandomiseAccessories(byte[] data, bool[] options, int[] parameters, bool[] languageOptions, Random rnd)
        {
            #region Section Information
            /* Accessory Data
             * 
             * The data available to modify (44 bytes each):
             * Stat Type        (2)
             * Stat Boost       (2)
             * Elem Strength    (1) - 00 absorb, 01 null, 02 halve
             * Special Effect   (1) - 00 > 06 auto-haste > auto-wall
             * Elemental Mask   (2)
             * Status Mask      (4)
             * Equip Mask       (2)
             * Restrict Mask    (2)
            */
            #endregion

            int r = 0;
            int o = 0;

            //int picker;
            //int[] array = new int[] { 1, 2, 4, 8, 16, 32, 64, 128 };

            byte[][] accessoryAttributes = new byte[32][];

            try
            {
                while (r < 32)
                {
                    #region Accessory Attributes Values
                    // Initialises the jagged array for this cycle
                    // 0: Stat Type A
                    // 1: Stat Value A
                    // 2: Stat Type B
                    // 3: Stat Value B
                    // 4: Element Type
                    // 5: Special Effect
                    // 6: Elem Byte A
                    // 7: Elem Byte B
                    // 8: Status Byte A
                    // 9: Status Byte B
                    // 10: Status Byte C
                    // 11: Status Byte D
                    // 12: Equip A
                    // 13: Equip B
                    // 14: Restrict A
                    // 15: Restrict C
                    #endregion
                    accessoryAttributes[r] = new byte[16];

                    #region Accessory Balancer Values
                    // Initialises the jagged array for this cycle
                    // 0: Stat A
                    // 1: Stat B
                    // 2: Element Defence
                    // 3: Special Effect
                    // 4: Status Effect
                    #endregion
                    var accessoryBalancer = AccessoryChange.AssignAccessoryBalancing(rnd);

                    data[o] = 255;
                    data[o + 1] = 255;
                    data[o + 2] = 255;
                    data[o + 3] = 255;

                    // Stat Bonus Type
                    data[o] = (byte)rnd.Next(0, 5);
                    accessoryAttributes[r][0] = data[o];
                    o++;
                    
                    data[o] = (byte)rnd.Next(0, 5);
                    accessoryAttributes[r][2] = data[o];
                    o++;

                    // Stat Bonus Value
                    data[o] = AccessoryChange.AdjustAccessoryStats(data[o], accessoryBalancer[0], rnd);
                    accessoryAttributes[r][1] = data[o];
                    o++;

                    data[o] = AccessoryChange.AdjustAccessoryStats(data[o], accessoryBalancer[1], rnd);
                    accessoryAttributes[r][3] = data[o];
                    o++;


                    // Element Type - FF Normal, 00 Absorb, 01 Null, 02 Halve
                    data[o] = 255;
                    if (accessoryBalancer[2] == 1)
                    {
                        if (rnd.Next(0, 10) >= 8)
                        {
                            data[o] = 0;
                        }
                        else if (rnd.Next(0, 10) >= 6)
                        {
                            data[o] = 1;
                        }
                        else
                        {
                            data[o] = 2;
                        }
                        accessoryAttributes[r][4] = data[o];
                    }
                    o++;

                    // Special Effect
                    o++;

                    // Defence Element
                    data[o] = 0;
                    data[o + 1] = 0; // Clears pre-existing elemental defences
                    if (accessoryBalancer[2] == 1)
                    {
                        var chooseElementByte = rnd.Next(15);
                        if (chooseElementByte >= 7)
                        {
                            data[o] = AccessoryChange.PickAccessoryElement(rnd, 0);
                        }
                        else
                        {
                            data[o + 1] = AccessoryChange.PickAccessoryElement(rnd, 1);
                        }
                    }
                    accessoryAttributes[r][6] = data[o];
                    accessoryAttributes[r][7] = data[o + 1];
                    o += 2;

                    // Status Effects
                    data[o] = 0;
                    data[o + 1] = 0;
                    data[o + 2] = 0;
                    data[o + 3] = 0;
                    if (accessoryBalancer[4] == 1)
                    {
                        var chooseStatusByte = rnd.Next(15);
                        if (chooseStatusByte >= 8)
                        {
                            data[o] = AccessoryChange.PickEquipmentStatus(rnd, 0);
                        }
                        else if (chooseStatusByte >= 3)
                        {
                            data[o + 1] = AccessoryChange.PickEquipmentStatus(rnd, 1);
                        }
                        else if (chooseStatusByte >= 2)
                        {
                            data[o + 2] = AccessoryChange.PickEquipmentStatus(rnd, 2);
                        }
                        else
                        {
                            data[o + 3] = AccessoryChange.PickEquipmentStatus(rnd, 3);
                        }
                    }
                    accessoryAttributes[r][8] = data[o];
                    accessoryAttributes[r][9] = data[o + 1];
                    accessoryAttributes[r][10] = data[o + 2];
                    accessoryAttributes[r][11] = data[o + 3];
                    o += 4;

                    // Equip Mask
                    o += 2;

                    // Restrict Mask
                    o += 2;
                    r++;
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #7 (Accessory Data) has failed to randomise");
            }

            KernelTextRewriter.AccessoryDescriptionRewrite(accessoryAttributes, languageOptions);

            return data;
        }
    }
}
