using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Helper
{
    public class SpellIndex
    {
        public static int CheckValidSpellIndex(Random rnd)
        {
            bool valid = false;
            int picker = 0;
            while (valid == false)
            {
                picker = rnd.Next(0, 54);
                switch (picker)
                {
                    // Prevent use of these IDs
                    case 18:
                        break;

                    case 19:
                        break;

                    default:
                        valid = true;
                        break;
                }
            }
            return picker;
        }

        public static int CheckValidSummonIndex(Random rnd)
        {
            bool valid = false;
            int picker = 0;
            while (valid == false)
            {
                picker = rnd.Next(0, 15);
                switch (picker)
                {
                    // Prevent use of these IDs
                    case 10:
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
