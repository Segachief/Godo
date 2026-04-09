using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Omnichange
{
    public class MPChange
    {
        public static byte AdjustMP(byte mpCost, int parameter, Random rnd)
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
                    if ((mpCost * decParameter) != 0 && (mpCost * (decParameter + 1) < 255))
                    {
                        decParameter += 1;
                        mpCost = (byte)(mpCost * decParameter);
                    }
                    else
                    {
                        mpCost = 0;
                    }
                }
                else if (picker == 1)
                {
                    if ((mpCost * decParameter) != 0 && mpCost != 255)
                    {
                        decParameter = 1 - decParameter;
                        decParameter = mpCost * decParameter;
                        int convert = decimal.ToInt32(decParameter);
                        mpCost = (byte)convert;
                    }
                    else
                    {
                        return mpCost;
                    }
                }
            }
            return mpCost;
        }
    }
}
