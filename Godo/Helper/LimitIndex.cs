using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Helper
{
    public class LimitIndex
    {
        public static int CheckValidLimitIndex(Random rnd)
        {
            bool valid = false;
            int picker = 0;
            while (valid == false)
            {
                picker = rnd.Next(127, 187);
                switch (picker)
                {
                    // Prevent use of these IDs
                    case 149:
                        break;

                    case 150:
                        break;

                    case 151:
                        break;

                    case 152:
                        break;

                    case 153:
                        break;

                    case 154:
                        break;

                    case 155:
                        break;

                    case 171:
                        break;

                    case 172:
                        break;

                    case 173:
                        break;

                    case 174:
                        break;

                    case 175:
                        break;

                    case 176:
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
