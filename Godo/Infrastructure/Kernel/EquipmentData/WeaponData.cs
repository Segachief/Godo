using Godo.Helper;
using Godo.Omnichange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Godo.FormsInitialisationData;

namespace Godo.Infrastructure.Kernel.EquipmentData
{
    public class WeaponData
    {
        public static byte[] RandomiseWeapons(byte[] data, bool[] options, int[] parameters, bool[] languageOptions, byte[] startingEquipment, Random rnd, bool interimOptions)
        {
            #region Section information
            /* Weapon Data
             * 
             * The data available to modify (44 bytes each):
             * Target Flags     (1)
             * Always FF        (1)
             * Damage Calc.     (1)
             * Always FF        (1)
             * Base Power       (1)
             * Status Attack    (1)
             * Growth Rate      (1)
             * Crit%            (1)
             * Acc%             (1)
             * Model ID         (1) - Upper nybble = attack animation modifier (barret/vince only), lower nybble: model ID
             * Always FF        (1)
             * Sound ID Mask    (1)
             * Camera ID        (2) - Always FFFF
             * Equip Mask       (2) 1, 2, 4, 8, 10, 20, 40, 80, 100, 200, 400 = Cloud > Sephiroth
             * Attack Element   (2)
             * Always FFFF      (2)
             * Stat Type        (4) FF None, 00 STR > 05 LCK
             * Stat Boost       (4)
             * Slots            (8) 00 > 07 none > right-linked growth
             * Sound ID Hit     (1)
             * Sound ID Crit    (1)
             * Sound ID Miss    (1)
             * Impact ID        (1)
             * Special Flags    (2)
             * Restrict Mask    (2) 01 can sell, 02 can throw, 04 can Menu
            */
            #endregion

            int r = 0;
            int o = 0;
            bool hasSlots = true;

            //int picker;
            //int[] element = new int[] { 1, 2, 4, 8, 16, 32, 64, 128 };
            //byte[] propertiesA = new byte[] { 0xFE, 0xEF, 0xDF, 0x7F };
            //byte[] propertiesB = new byte[] { 0xFB, 0xFD, 0xDF };

            // This stores the attribute bonuses given to weapons to be used in rewriting weapon descriptions
            byte[][] weaponAttributes = new byte[128][];

            try
            {
                while (r < 128)
                {
                    #region Weapon Attributes Values
                    // 0: Stat Type A
                    // 1: Stat Value A
                    // 2: Stat Type B
                    // 3: Stat Value B
                    // 4: Stat Type C
                    // 5: Stat Value C
                    // 6: Stat Type D
                    // 7: Stat Value D
                    // 8: Crit%
                    // 9: Elem Byte A
                    // 10: Elem Byte B
                    // 11: Status Ailment
                    #endregion
                    weaponAttributes[r] = new byte[12]; // Used for Kernel2 string writing (stat + bonus)

                    #region Weapon Balancer Values
                    //Weapon Balancing
                    // 0 = Base Power
                    // 1 = Crit%
                    // 2 = Acc%
                    // 3 = Stat A
                    // 4 = Stat B
                    // 5 = Status Ailment
                    // 6 = Materia Slots
                    // 7 = Materia Growth
                    #endregion
                    var weaponBalancer = WeaponChange.AssignWeaponBalancing(rnd);

                    // Target Flags
                    data[o] = data[o]; o++;

                    // Always FF
                    data[o] = data[o]; o++;

                    // Damage Calc
                    o++;

                    // Always FF
                    data[o] = data[o]; o++;

                    // Base Power
                    data[o] = WeaponChange.AdjustBasePower(data[o], data[o - 2], weaponBalancer[0], rnd); o++;

                    // Status Attack
                    if (weaponBalancer[5] == 1)
                    {
                        data[o] = WeaponChange.PickEquipmentStatus(rnd);
                    }
                    weaponAttributes[r][11] = data[o];
                    o++;

                    // Growth Rate
                    bool hasGrowth;
                    if (weaponBalancer[7] == 1)
                    {
                        if (rnd.Next(0, 10) >= 3)
                        {
                            // Double
                            data[o] = 2;
                            hasGrowth = true;
                        }
                        else
                        {
                            // Triple
                            data[o] = 3;
                            hasGrowth = true;
                        }
                    }
                    else
                    {
                        if (rnd.Next(0, 10) >= 2)
                        {
                            // Normal
                            data[o] = 1;
                            hasGrowth = true;
                        }
                        else
                        {
                            // None
                            data[o] = 0;
                            hasGrowth = false;
                        }
                    }
                    o++;

                    // Crit%
                    if (weaponBalancer[1] == 1)
                    {
                        data[o] = (byte)rnd.Next(15, 25);
                    }
                    else
                    {
                        data[o] = 0;
                    }
                    weaponAttributes[r][8] = data[o];
                    o++;

                    // Acc%
                    data[o] = WeaponChange.AdjustAccuracy(data[o], weaponBalancer[2], rnd); o++;

                    // Model ID (Used only for Barret & Vincent weapons)
                    data[o] = data[o]; o++;

                    // Always FF
                    data[o] = data[o]; o++;

                    // Sound ID
                    data[o] = data[o]; o++;

                    // Camera ID - Always FF
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Equip Mask
                    o += 2;

                    // Attack Element
                    weaponAttributes[r][9] = data[o]; o++;
                    weaponAttributes[r][10] = data[o]; o++;

                    // Always FF
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    // Stat Boost Type: A, B, C, D
                    data[o] = (byte)rnd.Next(1, 5);
                    weaponAttributes[r][0] = data[o]; o++;

                    data[o] = (byte)rnd.Next(1, 5);
                    weaponAttributes[r][2] = data[o]; o++;

                    data[o] = 255; // Disabled
                    weaponAttributes[r][4] = data[o]; o++;

                    data[o] = 255; // Disabled
                    weaponAttributes[r][6] = data[o]; o++;

                    // Stat Boost Value
                    data[o] = WeaponChange.AdjustWeaponStats(data[o], weaponBalancer[3], rnd);
                    weaponAttributes[r][1] = data[o]; o++;

                    data[o] = WeaponChange.AdjustWeaponStats(data[o], weaponBalancer[4], rnd);
                    weaponAttributes[r][3] = data[o]; o++;

                    data[o] = 255; // Disabled
                    weaponAttributes[r][5] = data[o]; o++;

                    data[o] = 255; // Disabled
                    weaponAttributes[r][7] = data[o]; o++;

                    // Slots
                    int slots = 4;
                    if (weaponBalancer[6] == 1)
                    {
                        slots = 8;
                    }

                    int countWrite = 0;
                    int firstSlots = 1;

                    // Clears the existing slots before assignment
                    data[o] = 0;
                    data[o + 1] = 0;
                    data[o + 2] = 0;
                    data[o + 3] = 0;
                    data[o + 4] = 0;
                    data[o + 5] = 0;
                    data[o + 6] = 0;
                    data[o + 7] = 0;

                    bool matchedEquipment = Array.Exists(startingEquipment, element => element == r);

                    while (slots > 0)
                    {
                        if (firstSlots == 1 && weaponBalancer[7] == 1)
                        {
                            // Adds a guaranteed pair of linked slots
                            data[o] = 6; o++;
                            data[o] = 7; o++;
                            countWrite += 2;
                            slots -= 2;
                            firstSlots = 0;
                        }
                        else if (firstSlots == 1 && matchedEquipment)
                        {
                            if (hasGrowth)
                            {
                                // Adds a guaranteed pair of linked slots
                                data[o] = 6; o++;
                                data[o] = 7; o++;
                                countWrite += 2;
                                slots -= 2;
                                firstSlots = 0;
                            }
                            else
                            {
                                // Adds a guaranteed pair of linked no-growth slots
                                data[o] = 2; o++;
                                data[o] = 3; o++;
                                countWrite += 2;
                                slots -= 2;
                                firstSlots = 0;
                            }
                        }
                        else
                        {
                            var makeSlot = rnd.Next(10);

                            // Make a set of linked slots
                            // We must be on an even-numbered slot to make a linked slot (slots divisible by 2)
                            if (makeSlot >= 6 && slots > 1 && slots % 2 == 0)
                            {
                                if (hasGrowth)
                                {
                                    data[o] = 6; o++;
                                    data[o] = 7; o++;
                                }
                                else
                                {
                                    data[o] = 2; o++;
                                    data[o] = 3; o++;
                                }

                                countWrite += 2;
                                slots -= 2;
                            }
                            // Make a single slot (1 slot must be available)
                            else if (makeSlot >= 3)
                            {
                                if (hasGrowth)
                                {
                                    data[o] = 5; o++;
                                }
                                else
                                {
                                    data[o] = 1; o++;
                                }
                                countWrite += 1;
                                slots--;
                            }
                            // No slot & stop making slots
                            else
                            {
                                if (countWrite == 0)
                                {
                                    hasSlots = false;
                                }
                                slots = 0;
                            }
                        }
                    }
                    //Increments data with empty slots until all 8 bytes are assigned
                    while (countWrite < 8)
                    {
                        data[o] = 0; o++;
                        countWrite++;
                    }
                    // Reassigns growth to None if no Materia slots were generated
                    if (hasSlots == false)
                    {
                        data[o - 30] = 0;
                    }
                    // Resets for next loop
                    hasSlots = true;

                    // Sound ID Hit
                    data[o] = data[o]; o++;

                    // Sound ID Crit
                    data[o] = data[o]; o++;

                    // Sound ID Miss
                    data[o] = data[o]; o++;

                    // Impact ID
                    data[o] = data[o]; o++;

                    // Special Flags
                    o += 2;

                    // Restrict Mask
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    r++;
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #5 (Weapon Data) has failed to randomise");
            }

            KernelTextRewriter.WeaponDescriptionRewrite(weaponAttributes, languageOptions);

            return data;
        }
    }
}