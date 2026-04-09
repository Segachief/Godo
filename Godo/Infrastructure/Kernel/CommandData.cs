using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo.Infrastructure.Kernel
{
    public class CommandData
    {
        public static byte[] RandomiseSection0(byte[] data, bool[] options)
        {
            #region Section Information
            /* Flags for commands like attack/magic. Not much to be changed here besides targeting parameters
             * and which menus open which sub-menus (for instance, Magic opens the magic sub-menu).
             * Would recommend leaving this data alone.
             * 
             * 32 commands in all, IDs start from 0:
             * 0) 'Left' - Dummy/unknown
             * 1) Attack
             * 2) Magic
             * 3) Summon
             * 4) Item
             * 5) Steal
             * 6) Sense
             * 7) Coin
             * 8) Throw
             * 9) Morph
             * 10) Deathblow
             * 11) Manip.
             * 12) Mime
             * 13) E. Skill
             * 14) All: (the all modifier that attach to spells)
             * 15) 4x: (The quadra modifier)
             * 16) Unknown
             * 17) Mug
             * 18) Change Row
             * 19) Defend
             * 20) Limit
             * 21) W-Magic
             * 22) W-Summon
             * 23) W-Item
             * 24) Slash-All
             * 25) 2x-Cut
             * 26) Flash
             * 27) 4x-Cut
             * 28-31) Dummy
             * 
             * The data available to modify (8 bytes each):
             * InitialCursor    (1)
             * Targeting Flag   (1)
             * Unknown          (2)
             * CameraID Single  (2)
             * CameraID Multi   (2)
            */
            #endregion

            int r = 0;
            int o = 0;

            try
            {
                while (r < 32)
                {
                    // No Physical Attacks
                    if (options[0] && r == 0)
                    {
                        data[o] = 255; o++;
                        o += 7;
                    }
                    // No Spells
                    if (options[2] && r == 2)
                    {
                        data[o] = 255; o++;
                        o += 7;
                    }
                    // No Summons
                    else if (options[3] && r == 3)
                    {
                        data[o] = 255; o++;
                        o += 7;
                    }
                    // No Items
                    else if (options[4] && r == 4)
                    {
                        data[o] = 255; o++;
                        o += 7;
                    }
                    else
                    {
                        o += 8;
                    }
                    r++;
                }
            }
            catch
            {
                MessageBox.Show("Kernel Section #0 (Command Data) has failed to randomise");
            }
            return data;
        }
    }
}
