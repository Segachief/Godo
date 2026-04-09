using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Omnichange
{
    public class APChange
    {
        public static byte AdjustAP(byte ap, int parameter, Random rnd)
        {
            // Turns paramater into a decimal
            parameter = rnd.Next(parameter / 2, parameter) + 1;
            decimal decParameter = parameter;
            decParameter /= 100;

            // Assign an increase or reduction
            if (ap != 0)
            {
                int picker = rnd.Next(2);
                if (picker == 0)
                {
                    // If a datatype's value exceeds its limits in C#, it reverts to 0.
                    // This checks for that and will use max value instead if it has occurred.
                    if ((ap * decParameter) != 0)
                    {
                        decParameter += 1;
                        decParameter = ap * decParameter;
                        int convert = decimal.ToInt32(decParameter);
                        ap = (byte)convert;
                    }
                    else
                    {
                        // Return AP without adjustment
                        return ap;
                    }
                }
                else if (picker == 1)
                {
                    if ((ap * decParameter) != 0)
                    {
                        decParameter = 1 - decParameter;
                        decParameter = ap * decParameter;
                        int convert = decimal.ToInt32(decParameter);
                        ap = (byte)convert;
                    }
                    else
                    {
                        // Return AP without adjustment
                        return ap;
                    }
                }
            }
            return ap;
        }
    }
}
