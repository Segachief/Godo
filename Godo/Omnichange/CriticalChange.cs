using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Omnichange
{
    public class CriticalChange
    {
        public static byte AdjustCritical(byte critical, int parameter, Random rnd)
        {
            // Turns paramater into a decimal
            decimal decParameter = parameter;
            decParameter /= 100;

            // Assign an increase or reduction
            if (parameter != 0)
            {
                int picker = rnd.Next(2);
                if (picker == 0)
                {
                    // If a datatype's value exceeds its limits in C#, it reverts to 0
                    // This checks for that and will use max value instead if it has occurred.
                    if ((critical * decParameter) != 0)
                    {
                        decParameter += 1;
                        critical = (byte)(critical + decParameter);
                    }
                    else
                    {
                        critical = 0;
                    }
                }
                else if (picker == 1)
                {
                    decParameter = 1 - decParameter;
                    critical = (byte)(critical / (decParameter));
                }
            }
            return critical;
        }
    }
}
