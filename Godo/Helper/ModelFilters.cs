using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Helper
{
    public class ModelFilters
    {
        // Try and retire this method once model logic is stronger
        public static bool CheckExcludedScene(int sceneID)
        {
            // List of excluded scenes from random allocation
            int[] excluded = { 12, 48, 73, 74, 103, 106, 107, 108,
                114, 116, 120, 138, 139, 143, 144, 150, 174, 185, 186, 189, 194, 195, 205, 209, 210,
                211, 214, 215, 227, 228, 229, 230, 231, 232, 233, 234, 235, 243 };
            if (excluded.Contains(sceneID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // ToDo - Come up with a single enemy filter; if model swapped in has the Single Enemy Filter flag,
        // then rewrite the formation to only have that 1 enemy. This will re-enable some of the exclusions.

        public static bool CheckExcludedModel(ulong modelID)
        {
            // List of models that shouldn't be swapped in or out
            ulong[] excluded =
            {
                // Pyramids - May work, but excluded anyway
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
                
                // Ruby Weapon - Tentacles in Ground Anims lock the game; has anims that interact
                11,

                // Emerald Weapon 'Leg' - Model part, has no anims
                14,

                // Emerald Weapon Eye - Has anims and works, but is invisible
                15,

                // Reno + Pyramid - Reno: Fireball interacts, Pyramid: No valid anims
                48, 49,

                // Warning Board + Turrets - No Anims on board, AI Refs
                50, 51, 52,

                // Hammer Blaster + Hole - Joint Model Dependency
                53, 54,
                
                // Sample H0512 + Opts - Animations affect other actors
                64, 65,

                // Rufus + Helicopter - Rufus: Report interacts, Helicopter: No valid anims
                44, 46,

                33, 38, 64, 65, 68, 70,

                // Mu + Hole - Joint Model Dependency
                77, 78,

                // Bottomswell + Waterpolo - Bottomswell: Fury Attack interacts, Waterpolo: No valid anims
                91, 92, 
               
                // Soul Fire + Gi Nattak - Anims interact with each other, like Sample H0512
                138, 139, 

                // Ghirofelgo + Chain - Likely a synch between their idles
                149, 150,

                // Ying + Yang + Ying/Yang - Models all depend on each other, AI Refs
                151, 152, 153,

                // Palmer + Bronco + Truck - Palmer's Report, Bronco/Truck have no valid anims
                164, 165, 166,

                // Trickplay + Hole - Joint Model Dependency
                198, 199,

                // Magnade + L/R Shield - Magnade attacks interact with shields, which don't have anims 
                212, 213, 214,

                // Icicle - Report attack locks battle, interacts with other enemies, AI Refs
                217,

                // Roulette Cannon + Pedestal - Joint Model Dependency
                235, 236,

                // Guard System + Turrets - No anims on board, AI refs
                243, 244, 245,

                // Guardian + L/R Fist - Attacks interact with fists, which don't have anims
                254, 255, 256,

                // Manhole + Lid - Anim dependency, transfers self
                287, 288,

                // Gargoyle - Statue has no anims
                294, 295,

                // Elena, Reno, Rude Midgar
                296, 297, 298,

                // Proud Clod + Jamar - Anim synch, armour has no anims
                299, 300,

                // Blue ball, unused
                303,

                // Helletic Hojo + Arms - Anim synch, AI refs
                306, 307, 308,

                // Mover - Anim synchs after attacks, game hangs if they aren't other Movers
                319,

                // Jenova SYNTHESIS - Model synchs to copies of itself, will interfere with others
                324,

                // Bizarro - Model synchs to copies of itself, will interfere with others
                325, 326, 327, 328, 329, 330,

                // Final Sephiroth - Report will lock fight
                332,

                // Null Model - FFFF
                65535
            };

            if (excluded.Contains(modelID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckSwapIn(ulong modelID)
        {
            // List of models that can be swapped in, but not swapped out safely
            ulong[] swapIn =
            {
                // Diamond Weapon - Idle/Hurt 7/8
                10,

                // Emerald Weapon - Idle/Hurt 7/8
                13,

                // Guard Scorpion - Idle/Hurt 7/8
                22,

                // Air Buster - Idle/Hurt 0/3, 10/1, 10/3
                33,

                // Aps - Hurt #9
                37,

                // Sahagin - Hurt #5
                38,

                // Hundred Gunner - Idle/Hurt 7/8 12/13
                66, 

                // Heli Gunner - Idle/Hurt 7/8
                67,

                // Motorball - Idle/Hurt 7/8
                71,

                // Midgar Zolom - Idle/Hurt 5/6
                81,

                // Dyne - Idle #10
                111,

                // Griffin - Idle/Hurt 6/7
                130, 

                // Desert Sahagin - Hurt #5
                133,

                // Valron - Idle/Hurt 5/6
                143,

                // Jersey - Idle/Hurt 7/8
                147,

                // Bizarre Bug - Idle/Hurt 5/6
                170,

                // Gorkii - Idle/Hurt 5/6
                178,

                // Godo - Idle/Hurt 6/7 11/12
                182,

                // Demons Gate - Idle/Hurt 7/8
                195,

                // Grimguard - Hurt 1, 6, 8, 12
                202,

                // Schzio R/L - AI Refs, Idle/Hurt R: 0/3, 13/14, L: 0/4  18/19
                228, 229,

                // Scissors, Upper, Lower - AI Refs
                240, 241, 242,

                // Carry Armour + Arms - Idle/Hurt 8/9, 13/14, 18/19 (all same)
                258, 259, 260,

                // Hippogriff - Idle/Hurt
                276,

                // Spiral - Idle/Hurt
                278,

                // Grosspanzer - AI Refs, idle synch
                293, 294, 295,

                // X-Cannon - Idle/Hurt 6/7
                302,

                // Christopher + Gighee - They have an anim that synchs to each other
                310, 311,
                
                // Armored Golem - Idle/Hurt 10/11
                316,

                // Safer - Idle/Hurt 8/9
                331,

                // Chocobos - AI Refs, Idle/Hurt 7/8
                339, 340, 341, 342, 343, 344, 347, 349, 350
            };

            if (swapIn.Contains(modelID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // CheckSwapOut = Can be swapped out, but not swapped in

        public static bool CheckBossSet(ulong modelID)
        {
            // Bosses - Used to apply different stat scaling
            ulong[] bossSet = { 10, 11, 13, 15, 22, 33, 37, 48, 64, 66, 67,
                68, 71, 81, 91, 95, 111, 127, 128, 139, 154, 155, 156, 163,
                164, 177, 178, 179, 180, 181, 182, 193, 195, 196, 228, 229,
                234, 258, 259, 260, 265, 266, 267, 268, 271, 272, 273, 274,
                275, 281, 296, 297, 298, 299, 300, 305, 306, 309, 324, 325,
                326, 327, 328, 329, 330, 331, 333, 334, 335, 336
            };

            if (bossSet.Contains(modelID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
