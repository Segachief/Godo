using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Indexing
{
    public class MateriaIndex
    {
        public static int SelectCuratedMateria(Random rnd)
        {
            bool valid = false;
            int picker = 0;
            while (valid == false)
            {
                //Cuts off Summon materia after Ifrit; max is 91
                picker = rnd.Next(77);
                switch (picker)
                {
                    // Invalid Materia; no data
                    case 22:
                        break;

                    case 38:
                        break;

                    case 45:
                        break;

                    case 46:
                        break;

                    case 47:
                        break;

                    case 63:
                        break;

                    case 66:
                        break;

                    case 67:
                        break;

                    // Overpowered Materia
                    //Double-Cut
                    case 15:
                        break;

                    //W-Magic
                    case 19:
                        break;

                    //W-Summon
                    case 20:
                        break;

                    //W-Item
                    case 21:
                        break;

                    //Master Command
                    case 48:
                        break;

                    //Comet
                    case 64:
                        break;

                    //Contain
                    case 69:
                        break;

                    //Ultima
                    case 72:
                        break;

                    //Master Magic
                    case 73:
                        break;

                    default:
                        valid = true;
                        break;
                }
            }
            return picker;
        }
    }
}
