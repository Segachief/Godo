using Godo.Omnichange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo.Infrastructure.Kernel
{
    public class MateriaData
    {
        public static byte[] RandomiseMateria(byte[] data, bool[] options, int[] parameters, bool[] specialOptions, Random rnd)
        {
            #region Section Information
            /* Materia Data
             * 
             * The data available to modify (44 bytes each):
             * Level Up         (8) - Multiples of 100 (4x WORD)
             * Equip Effect ID  (1)
             * Status Effects   (3) - Only 24 statuses
             * Element Index    (1)
             * Materia Type     (1)
             * Attributes       (6)
            */
            #endregion

            int r = 0;
            int o = 0;
            int c = 0;

            try
            {
                // Nulls all Materia for No Materia option
                //Disabled for later phase
                //if (specialOptions[5] != false)
                //{
                //    while (r < 91)
                //    {
                //        while (c < 21)
                //        {
                //            data[o] = 255; o++;
                //            c++;
                //        }
                //        r++;
                //        c = 0;
                //    }
                //    r = 0;
                //    o = 0;
                //}

                while (r < 91)
                {

                    // AP
                    //Disabled until later phase
                    o += 8;
                    //if (options[0])
                    //{
                    //    int i = 0;
                    //    while (i < 4)
                    //    {
                    //        if (data[o] == 255 && data[o + 1] == 255)
                    //        {
                    //            o += 2;
                    //        }
                    //        else
                    //        {
                    //            data[o] = (byte)APChange.AdjustAP(data[o], parameters[0], rnd); o++;
                    //            data[o] = (byte)APChange.AdjustAP(data[o], parameters[0], rnd); o++;
                    //        }
                    //        i++;
                    //    }
                    //}
                    //else
                    //{
                    //    o += 8;
                    //}

                    // Equip Effect ID
                    //if(options[1])
                    //{
                        data[o] = (byte)rnd.Next(16); o++;
                    //}
                    //else
                    //{
                    //    o++;
                    //}

                    // Status Effects
                    //Disabled until later phase
                    o += 3;
                    //if (options[3])
                    //{
                    //    int picker = rnd.Next(3);
                    //    int[] status = new int[] { 1, 2, 4, 8, 16, 32, 64, 128 };

                    //    if (picker == 0)
                    //    {
                    //        picker = rnd.Next(0, 7);
                    //        data[o] = (byte)status[picker]; o++;
                    //        data[o] = 0; o++;
                    //        data[o] = 0; o++;
                    //    }
                    //    else if (picker == 1)
                    //    {
                    //        picker = rnd.Next(0, 7); // Prevents Regen being set
                    //        data[o] = 0; o++;
                    //        data[o] = (byte)status[picker]; o++;
                    //        data[o] = 0; o++;
                    //    }
                    //    else
                    //    {
                    //        picker = rnd.Next(0, 7); // Prevents Peerless being set
                    //        data[o] = 0; o++;
                    //        data[o] = 0; o++;
                    //        data[o] = (byte)status[picker]; o++;
                    //    }
                    //}
                    //else
                    //{
                    //    o += 3;
                    //}

                    // Element Index
                    //Disabled until later phase
                    o++;
                    //if (options[2])
                    //{
                    //    data[o] = (byte)rnd.Next(16); o++;
                    //}
                    //else
                    //{
                    //    o++;
                    //}

                    // Materia Type
                    data[o] = data[o]; o++;

                    // Attributes
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;
                    data[o] = data[o]; o++;

                    r++;
                }
                o = 0;
            }
            catch
            {
                MessageBox.Show("Kernel Section #8 (Materia Data) has failed to randomise");
            }
            return data;
        }
    }
}
