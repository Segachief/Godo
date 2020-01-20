using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Helper
{
    class MateriaIndex
    {
        public static int CheckValidMateriaIndex(Random rnd)
        {
            bool valid = false;
            int picker = 0;
            while (valid == false)
            {
                picker = rnd.Next(91);
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

                    // OP Materia: KOTR and Master Command/Magic/Summon
                    case 48:
                        break;

                    case 73:
                        break;

                    case 89:
                        break;

                    case 90:
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
