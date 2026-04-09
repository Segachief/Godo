using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Helper
{
    public class ModelFilters
    {
        public static bool CheckExcludedScene(int sceneID)
        {
            // List of excluded scenes from random allocation
            int[] excluded = {
                // Icicle Scene - Icicle Model has a reliance on Evilheads when ending the fight
                179
                };
            if (excluded.Contains(sceneID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckExcludedModel(ulong modelID)
        {
            // List of models that shouldn't be swapped in or out
            ulong[] excluded =
            {
                // Pyramids - May work, but excluded anyway
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9,

                // Diamond Weapon - Idle/Hurt 7/8
                10,
                
                // Ruby Weapon - Tentacles in Ground Anims lock the game; has anims that interact
                11,

                // Emerald Weapon - Idle/Hurt 7/8
                13,

                // Emerald Weapon 'Leg' - Model part, has no anims
                14,

                // Emerald Weapon Eye - Has anims and works, but is invisible
                15,

                // Guard Scorpion - Idle/Hurt 7/8
                22,
                
                // Air Buster
                33, 

                // Aps - Hurt #9
                37,
                
                // Sahagin
                38,

                // Reno + Pyramid - Reno: Fireball interacts, Pyramid: No valid anims
                48, 49,

                // Warning Board + Turrets - No Anims on board, AI Refs
                50, 51, 52,

                // Hammer Blaster + Hole - Joint Model Dependency
                53, 54,

                // Mighty Grunt A B
                57, 58,

                // Sample H0512 + Opts - Animations affect other actors
                64, 65,

                 // Hundred/Heli Gunner
                66, 67,

                // Rufus + Helicopter - Rufus: Report interacts, Helicopter: No valid anims
                68, 70,
                
                // Motorball
                71,

                // Prowler
                75,

                // Mu + Hole - Joint Model Dependency
                77, 78,

                // Midgar Zolom
                81,

                // Zemzellet
                86,

                // Bottomswell + Waterpolo - Bottomswell: Fury Attack interacts, Waterpolo: No valid anims
                91, 92,

                // Grangalan
                96, 97, 98,

                // Dyne
                111,

                // Flower Prong
                121, 122, 123,

                // Griffin
                130,

                // Golem
                131,

                // Desert Sahagin
                133,
               
                // Soul Fire + Gi Nattak - Anims interact with each other, like Sample H0512
                139, 140,

                // Valron
                143,

                // Jersey - Idle/Hurt 7/8
                147,

                // Ghirofelgo + Chain - Likely a synch between their idles
                149, 150,

                // Ying + Yang + Ying/Yang - Models all depend on each other, AI Refs
                151, 152, 153,

                // Lost Number A B C  - Models all depend on each other, AI Refs
                154, 155, 156,

                // Palmer + Bronco + Truck - Palmer's Report, Bronco/Truck have no valid anims
                164, 165, 166,

                // BizarreBug
                170,

                // Gorkii
                178,

                // Godo
                182,

                // Demons Gate
                195,

                // Trickplay + Hole - Joint Model Dependency
                198, 199,

                // Grimguard - Hurt 1, 6, 8, 12
                202,

                // Magnade + L/R Shield - Magnade attacks interact with shields, which don't have anims 
                212, 213, 214,

                // Icicle - Report attack locks battle, interacts with other enemies, AI Refs
                217,

                // Tonberry
                233,

                // Schizo R L
                228, 229,

                // Roulette Cannon + Pedestal - Joint Model Dependency
                235, 236,

                // Scissors, Upper, Lower - AI Refs
                240, 241, 242,

                // Guard System + Turrets - No anims on board, AI refs
                243, 244, 245,

                // Guardian + L/R Fist - Attacks interact with fists, which don't have anims
                255, 256, 257,

                // Carry Armour + Arms - Idle/Hurt 8/9, 13/14, 18/19 (all same)
                258, 259, 260,

                // Hippogriff - Idle/Hurt
                276,

                // Spiral - Idle/Hurt
                278,

                 // Manhole + Lid - Anim dependency, transfers self
                287, 288,

                // Grozzpanzer- AI Refs, idle synch
                291, 292, 293,

                // Gargoyle - Statue has no anims
                294, 295,

                // Elena, Reno, Rude Midgar
                296, 297, 298,

                // Proud Clod + Jamar - Anim synch, armour has no anims
                299, 300,

                // X-Cannon - Idle/Hurt 6/7
                302,

                // Blue ball, unused
                303,

                // Helletic Hojo + Arms - Anim synch, AI refs
                306, 307, 308,

                // Christopher + Gighee - They have an anim that synchs to each other
                310, 311,
                
                // Armored Golem - Idle/Hurt 10/11
                316,

                // Mover - Anim synchs after attacks, game hangs if they aren't other Movers
                319,

                // Jenova SYNTHESIS - Model synchs to copies of itself, will interfere with others
                324,

                // Bizarro - Model synchs to copies of itself, will interfere with others
                325, 326, 327, 328, 329, 330,

                // Safer - Idle/Hurt 8/9 - Death Anims will lock fight (but he can die regularly)
                331,
                
                // Final Sephiroth - Report will lock fight
                332,

                // Ultimate Weapon
                333, 334, 335, 336,

                // Chocobos - AI Refs, Idle/Hurt 7/8
                339, 340, 341, 342, 343, 344, 347, 349, 350,

                // Mystery Ninja
                353, 354, 355, 356, 357, 358,
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

        public static bool CheckSwarm (ulong modelID)
        {
            // List of bosses that shouldn't be swarmed
            ulong[] excluded =
            {
                // Pyramids - May work, but excluded anyway
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9,

                // Diamond Weapon - Idle/Hurt 7/8
                10,
                
                // Ruby Weapon - Tentacles in Ground Anims lock the game; has anims that interact
                11,

                // Emerald Weapon - Idle/Hurt 7/8
                13,

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

                // Mighty Grunt A B
                57, 58,

                // Sample H0512 + Opts - Animations affect other actors
                64, 65,

                 // Hundred/Heli Gunner
                66, 67,

                // Rufus + Helicopter - Rufus: Report interacts, Helicopter: No valid anims
                68, 70,

                // Mu + Hole - Joint Model Dependency
                77, 78,

                // Bottomswell + Waterpolo - Bottomswell: Fury Attack interacts, Waterpolo: No valid anims
                91, 92,

                // Grangalan
                96, 97, 98,

                // Dyne
                111,

                // Flower Prong
                121, 122, 123,

                // Soul Fire + Gi Nattak - Anims interact with each other, like Sample H0512
                139, 140,

                // Ghirofelgo + Chain - Likely a synch between their idles
                149, 150,

                // Ying + Yang + Ying/Yang - Models all depend on each other, AI Refs
                151, 152, 153,

                // Lost Number A B C  - Models all depend on each other, AI Refs
                154, 155, 156,

                // Palmer + Bronco + Truck - Palmer's Report, Bronco/Truck have no valid anims
                164, 165, 166,

                // Trickplay + Hole - Joint Model Dependency
                198, 199,

                // Magnade + L/R Shield - Magnade attacks interact with shields, which don't have anims 
                212, 213, 214,

                // Icicle - Report attack locks battle, interacts with other enemies, AI Refs
                217,

                // Schizo R L
                228, 229,

                // Roulette Cannon + Pedestal - Joint Model Dependency
                235, 236,

                // Scissors, Upper, Lower - AI Refs
                240, 241, 242,

                // Guard System + Turrets - No anims on board, AI refs
                243, 244, 245,

                // Guardian + L/R Fist - Attacks interact with fists, which don't have anims
                255, 256, 257,

                // Carry Armour + Arms - Idle/Hurt 8/9, 13/14, 18/19 (all same)
                258, 259, 260,

                 // Manhole + Lid - Anim dependency, transfers self
                287, 288,

                // Grozzpanzer- AI Refs, idle synch
                291, 292, 293,

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

                // Christopher + Gighee - They have an anim that synchs to each other
                310, 311,

                // Mover - Anim synchs after attacks, game hangs if they aren't other Movers
                319,

                // Jenova SYNTHESIS - Model synchs to copies of itself, will interfere with others
                324,

                // Bizarro - Model synchs to copies of itself, will interfere with others
                325, 326, 327, 328, 329, 330,

                // Safer - Idle/Hurt 8/9 - Death Anims will lock fight (but he can die regularly)
                331,
                
                // Final Sephiroth - Report will lock fight
                332,

                // Ultimate Weapon
                333, 334, 335, 336,

                // Chocobos - AI Refs, Idle/Hurt 7/8
                339, 340, 341, 342, 343, 344, 347, 349, 350,

                // Mystery Ninja
                353, 354, 355, 356, 357, 358,

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

        public static bool CheckRisk(ulong modelID)
        {
            // List of models that shouldn't be swapped in or out
            ulong[] excluded =
            {
                0
            };
            return false;
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
    }
}
