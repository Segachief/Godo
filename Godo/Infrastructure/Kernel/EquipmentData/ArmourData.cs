using Godo.Helper;
using Godo.Omnichange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo.Infrastructure.Kernel.EquipmentData
{
    public class ArmourData
    {
        public static byte[] RandomiseArmour(byte[] data, bool[] options, int[] parameters, bool[] languageOptions, byte[] startingEquipment, Random rnd)
        {
            #region Section Information
            /* Armour Data
             * 
             * The data available to modify (44 bytes each):
             * Unknown          (1)
             * Damage Type      (1) - FF norm, 00 absorb, 01 null, 02 halve
             * Defence          (1)
             * MDef             (1)
             * Defence%         (1)
             * MDef%            (1)
             * Status Def       (1)
             * Unknown          (2)
             * Slots            (8) - 00 > 07 none > right-linked growth
             * Growth           (1)
             * Equip Mask       (2) - 1, 2, 4, 8, 10, 20, 40, 80, 100, 200, 400 = Cloud > Sephiroth
             * Elem Def         (2)
             * Always FF        (2)
             * Stat Type        (4)
             * Stat Boost       (4)
             * Restrict Mask    (2)
             * Always FF        (2)
            */
            #endregion

            int r = 0;
            int o = 0;
            bool hasSlots = true;

            //int picker;
            //int[] element = new int[] { 1, 2, 4, 8, 16, 32, 64, 128 };

            // This stores the attribute bonuses given to armour to be used in rewriting armour descriptions
            byte[][] armourAttributes = new byte[32][];
            byte[] armourBalancer = new byte[9];

            try
            {
                while (r < 32)
                {

                    #region Armour Attributes Values
                    // Initialises the jagged array for this cycle
                    // 0: Stat Type A
                    // 1: Stat Value A
                    // 2: Stat Type B
                    // 3: Stat Value B
                    // 4: Stat Type C
                    // 5: Stat Value C
                    // 6: Stat Type D
                    // 7: Stat Value D
                    // 8: Elem Byte A
                    // 9: Elem Byte B
                    // 10: Elem Defence Type
                    #endregion
                    armourAttributes[r] = new byte[11];

                    #region Armour Balancing Values
                    //Armour Balancing
                    // 0 = Defence
                    // 1 = Magic Defence
                    // 2 = Evasion
                    // 3 = Magic Evasion
                    // 4 = Stat A
                    // 5 = Stat B
                    // 6 = Materia Slots
                    // 7 = Materia Growth
                    // 8 = Elemental Defence
                    #endregion
                    armourBalancer = ArmourChange.AssignArmourBalancing(rnd);

                    // Unknown
                    o++;

                    // Element Type - FF Normal, 00 Absorb, 01 Null, 02 Halve
                    data[o] = 255;
                    if (armourBalancer[8] == 1)
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
                        armourAttributes[r][10] = data[o];
                    }
                    o++;

                    // Defence
                    data[o] = ArmourChange.AdjustDefence(data[o], armourBalancer[0], rnd); o++;

                    // Magic Defence
                    data[o] = ArmourChange.AdjustMagicDefence(data[o], armourBalancer[1], rnd); o++;

                    // Evasion%
                    data[o] = ArmourChange.AdjustEvasion(data[o], armourBalancer[2], rnd); o++;

                    // Magic Evasion%
                    data[o] = ArmourChange.AdjustMagicEvasion(data[o], armourBalancer[3], rnd); o++;

                    // Status Defence
                    o++;

                    // Unknown
                    o += 2;

                    // Slots
                    // Assign a Growth Rate
                    int growthRate;
                    bool hasGrowth;
                    if (armourBalancer[7] == 1)
                    {
                        if (rnd.Next(0, 10) >= 3)
                        {
                            // Double
                            growthRate = 2;
                            hasGrowth = true;
                        }
                        else
                        {
                            // Triple
                            growthRate = 3;
                            hasGrowth = true;
                        }
                    }
                    else
                    {
                        if (rnd.Next(0, 10) >= 2)
                        {
                            // Normal
                            growthRate = 1;
                            hasGrowth = true;
                        }
                        else
                        {
                            // None
                            growthRate = 0;
                            hasGrowth = false;
                        }
                    }

                    int slots = 4;
                    if (armourBalancer[6] == 1)
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
                        if (firstSlots == 1 && armourBalancer[7] == 1)
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

                            // Make a set of linked slots - 4/10 chance, must be on even number of slots
                            // We must be on an even-numbered slot to make a linked slot (slots divisible by 2)
                            if (makeSlot >= 6 && slots > 1 && slots % 2 == 0)
                            {
                                if (growthRate != 0)
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
                            // Make a single slot (1 slot must be available) - 7/10 chance
                            else if (makeSlot >= 3)
                            {
                                if (growthRate != 0)
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
                            // No slot & stop making slots - 2/10 chance
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

                    // Growth Rate
                    if (hasSlots == false)
                    {
                        data[o] = 0;
                    }
                    else
                    {
                        data[o] = (byte)growthRate;
                    }
                    hasSlots = true;
                    o++;

                    // Equip Mask
                    o += 2;

                    // Defence Element
                    data[o] = 0;
                    data[o + 1] = 0; // Clears pre-existing elemental defences
                    if (armourBalancer[8] == 1)
                    {
                        var chooseElementByte = rnd.Next(9);
                        if (chooseElementByte >= 2)
                        {
                            data[o] = ArmourChange.PickArmourElement(rnd, 0);
                        }
                        else
                        {
                            data[o + 1] = ArmourChange.PickArmourElement(rnd, 1);
                        }
                    }
                    armourAttributes[r][8] = data[o];
                    armourAttributes[r][9] = data[o + 1];
                    o += 2;
                    

                    // Always FF
                    o += 2;

                    // Stat Boost Type
                    //STR VIT MAG SPR DEX LCK
                    data[o] = 255;
                    data[o + 1] = 255;
                    data[o + 2] = 255;
                    data[o + 3] = 255;

                    // Loops to prevent VIT and SPR being assigned
                    while (data[o] == 255 || data[o] == 1 || data[o] == 3)
                    {
                        data[o] = (byte)rnd.Next(0, 5);
                    }
                    armourAttributes[r][0] = data[o]; o++;

                    // Loops to prevent VIT and SPR being assigned
                    while (data[o] == 255 || data[o] == 1 || data[o] == 3)
                    {
                        data[o] = (byte)rnd.Next(0, 5);
                    }
                    armourAttributes[r][2] = data[o]; o++;

                    armourAttributes[r][4] = data[o]; o++;

                    armourAttributes[r][6] = data[o]; o++;

                    // Stat Boost Value
                    data[o] = ArmourChange.AdjustArmourStats(data[o], armourBalancer[4], rnd);
                    armourAttributes[r][1] = data[o]; o++;

                    data[o] = ArmourChange.AdjustArmourStats(data[o], armourBalancer[5], rnd);
                    armourAttributes[r][3] = data[o]; o++;

                    data[o] = 255;
                    armourAttributes[r][5] = data[o]; o++;

                    data[o] = 255;
                    armourAttributes[r][7] = data[o]; o++;

                    // Restrict Mask
                    o += 2;

                    // Always FF
                    o += 2;

                    r++;
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #6 (Armour Data) has failed to randomise");
            }

            KernelTextRewriter.ArmourDescriptionRewrite(armourAttributes, languageOptions);

            return data;
        }
    }
}
