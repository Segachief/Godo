using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Omnichange
{
    public class AccuracyChange
    {
        public static byte AdjustAccuracy(byte baseAccuracy, int accuracyModifier, Random rnd)
        {
            if (baseAccuracy == 255)
            {
                baseAccuracy -= 100;
            }
            int accuracyUpper = baseAccuracy + 50;
            int accuracyLower = baseAccuracy - 50;
            if (accuracyModifier == 1)
            { // Guarantees a higher than original base accuracy (except for 255% weapons)
                accuracyUpper += 50;
                accuracyLower += 50;
            }
            baseAccuracy = (byte)rnd.Next(accuracyLower, accuracyUpper);

            return baseAccuracy;
        }
    }
}
