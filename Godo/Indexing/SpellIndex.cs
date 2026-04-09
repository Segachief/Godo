using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Indexing
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

                    // Escape
                    case 25:
                        break;

                    // Remove
                    case 26:
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

        public static int CheckValidEnemySkillIndex(Random rnd)
        {
            bool valid = false;
            int picker = 0;
            while (valid == false)
            {
                picker = rnd.Next(0, 15);
                switch (picker)
                {
                    // Prevent use of these IDs

                    default:
                        valid = true;
                        break;
                }
            }
            return picker;
        }

        public static int CheckValidEnemyAttackAnimIndex(Random rnd)
        {
            bool valid = false;
            int picker = 0;
            while (valid == false)
            {
                picker = rnd.Next(0, 192);
                switch (picker)
                {
                    // Prevent use of these IDs
                    // Uncommented = Not Listed/Tested in my notes
                    case 0x03:
                    case 0x04:
                    case 0x05:
                    case 0x07:
                    case 0x08:
                    case 0x0E:
                    case 0x11:
                    case 0x12:
                    case 0x15:
                    case 0x17: // Drain - Breaks on Multi
                    case 0x18:
                    case 0x1C:
                    case 0x1E:
                    case 0x1F:
                    case 0x22:
                    case 0x23:
                    case 0x27:
                    case 0x29:
                    case 0x2D:
                    case 0x2E:
                    case 0x2F:
                    case 0x30:
                    case 0x31:
                    case 0x32:
                    case 0x33:
                    case 0x34:
                    case 0x35:
                    case 0x36:
                    case 0x37:
                    case 0x3A:
                    case 0x3F:
                    case 0x40:
                    case 0x41: // Aspil - Breaks on Multi
                    case 0x42:
                    case 0x43:
                    case 0x48:
                    case 0x49:
                    case 0x4E:
                    case 0x53:
                    case 0x58:
                    case 0x5E:
                    case 0x5F:
                    case 0x62:
                    case 0x68:
                    case 0x69:
                    case 0x6D:
                    case 0x6F:
                    case 0x71:
                    case 0x74:
                    case 0x7A:
                    case 0x7B:
                    case 0x7C:
                    case 0x7D:
                    case 0x7E:
                    case 0x7F:
                    case 0x80:
                    case 0x83:
                    case 0x8B:
                    case 0x8F: // Supernova - Breaks outside of specific BattleBG (is also very long)
                    case 0x91:
                    case 0x93:
                    case 0x94:
                    case 0x96:
                    case 0x97:
                    case 0xA0:
                    case 0xA3:
                    case 0xA8:
                    case 0xA9:
                    case 0xB2:
                    case 0xB3:
                    case 0xB4:
                    case 0xB5:
                    case 0xB6:
                    case 0xB7:
                    case 0xB8:
                    case 0xB9:
                    case 0xBC:
                    case 0xC8:
                    case 0xC9:
                    case 0xCA:
                    case 0xCB:
                    case 0xCC:
                    case 0xCD:
                    case 0xCE:
                    case 0xD6:
                    case 0xD7:
                    case 0xD8:
                    case 0xD9:
                    case 0xDA:
                    case 0xDB:
                    case 0xDC:
                    case 0xDE:
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
