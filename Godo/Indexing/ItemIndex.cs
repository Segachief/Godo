using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Indexing
{
    public class ItemIndex
    {
        public static int CheckValidItemAnimationIndex(Random rnd)
        {
            bool valid = false;
            int picker = 0;
            while (valid == false)
            {
                picker = rnd.Next(0, 61);
                switch (picker)
                {

                    default:
                        valid = true;
                        break;
                }
            }
            return picker;
        }
    }
}
