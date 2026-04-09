using System;
using System.Runtime.Remoting.Messaging;
using Godo.FormsEquipmentData;
using Godo.Helper;

namespace Godo.Omnichange
{
    public class EnemyChange
    {
        public static byte[] AssignEnemyBalancing(Random rnd)
        {
            int c = 0;
            int k = 0;
            byte[] enemyBalancer = new byte[7];
            while (c < 7)
            {
                if (k < 2 && enemyBalancer[c] != 1)
                {
                    enemyBalancer[c] = (byte)rnd.Next(0, 2);
                    if (enemyBalancer[c] == 1)
                    {
                        k++;
                    }
                }

                // Loops again if all points weren't assigned
                if (c == 6 && k < 2)
                {
                    c = 0;
                }
                c++;
            }
            return enemyBalancer;
        }

        //Enemy Renaming, 32 bytes ascii
        public static byte[] EnemyNamingway(byte[] data, int o, Random rnd)
        {
            int i = 0;
            int longerNameChance = 0;

            byte[] nameBytes = Misc.NameGenerate(rnd);
            data[o] = nameBytes[0]; o++;
            data[o] = nameBytes[1]; o++;
            data[o] = nameBytes[2]; o++;
            data[o] = nameBytes[3]; o++;

            longerNameChance = rnd.Next(2); // Chance to append a longer name
            if (longerNameChance == 1)
            {
                data[o] = nameBytes[4]; o++;
                data[o] = nameBytes[5]; o++;
                data[o] = nameBytes[6]; o++;
                data[o] = nameBytes[7]; o++;

                while (i < 23)
                {
                    data[o] = 255; o++;
                    i++;
                }
                data[o] = 255; // Empty - Use FF to terminate the string
            }
            else
            {
                while (i < 27)
                {
                    data[o] = 255; o++;
                    i++;
                }
                data[o] = 255; // Empty - Use FF to terminate the string
            }
            return data;
        }


        // As enemies are mostly ordered by progression through the story, their Scene ID can be used as the basis to establish scaling.
        // However, there are some stray formations and debug formations featuring actual enemies that need to be handled.
        // Also, World Map enemies appear above Field Map enemies in the ordering so this also needs to be handled.
        // The first encounter in the game is at ID 75 which is where scaling will start from.
        public static int SceneIdAdjust(int sceneID, int enemySlot)
        {
            if (sceneID == 5 || sceneID == 6)
            {
                // Unused Mighty Grunt encounter; given first actual appearance sceneID to prevent misbalancing
                // as the enemy consistency will prevent the later instances from being given intended values
                sceneID = 104;
            }
            else if (sceneID == 6)
            {
                // Unused Adamantaimai encounter
                sceneID = 151;
            }
            else if (sceneID == 7 || sceneID == 62 || sceneID == 63 || sceneID == 64)
            {
                // Unused Grunt encounters
                sceneID = 77;
            }
            else if (sceneID == 67 || sceneID == 68 || sceneID == 69 || sceneID == 72)
            {
                // World Map Mystery Ninja & CMD Grand Horn encounters
                sceneID = 127;
            }
            else if (sceneID < 75)
            {
                // World Map enemies
                sceneID += 107;
            }
            else if (sceneID == 235)
            {
                // Stray formation for Junon placed deep in the Scene.bin
                sceneID = 186;
            }
            else if (sceneID == 237)
            {
                // Stray formation for North Crater placed below end bosses
                sceneID = 226;
            }
            else if (sceneID == 245 && enemySlot == 0)
            {
                // Diamond Weapon; enemy was passed in order to avoid Ruby Weapon
                // being affected by this clause as both occupy the same scene ID
                sceneID += 137;
            }

            // This final adjustment is so that we're effectively treating SceneID 75 as 1 for scaling
            sceneID -= 74;

            return sceneID;
        }


        public static byte AdjustLevel(byte baseStat, int sceneID, int enemySlot, bool toughFlag, Random rnd)
        {
            // Current formula; level increases by 0.5 (always rounds down) for each scene.

            // Examples of formula output
            // First enemies encountered on the first field will be Lv.1
            // Last boss of Midgar, Motorball, will be Lv.21
            //      Scene ID 117, -74, /2, = 21.5 (21)
            // Enemies in Nibelheim World Map will be roughly Lv.28
            // Enemies inside Shinra Mansion will be roughly Lv.30
            // Final boss of the game, Safer Sephiroth, will be Lv.78
            //      Scene ID 231, -74, /2, = 78.5 (78)

            sceneID = SceneIdAdjust(sceneID, enemySlot);

            // ToDo; hard option reduces this divisor for higher level enemies
            int divisor = 2;

            var newStat = sceneID / divisor;
            if (newStat == 0)
            {
                newStat = 1;
            }

            return (byte)newStat;
        }

        public static byte AdjustSpeed(byte baseStat, int sceneID, int enemySlot, bool toughFlag, Random rnd)
        {
            sceneID = SceneIdAdjust(sceneID, enemySlot);

            // Disc 1 - Midgar
            int newStat = 50;

            // Disc 1 - Reactor 1
            // Special balancing due to limited party
            if (sceneID >= 75 && sceneID <= 82)
            {
                newStat = 25;
            }

            // Disc 1 - Leaving Midgar
            // World Map encounters are located before SceneID 75
            if (sceneID > 117 || sceneID < 75)
            {
                newStat = 75;
            }

            // Disc 2
            if (sceneID > 167)
            {
                newStat = 100;
            }

            // Disc 3
            if (sceneID > 216)
            {
                newStat = 125;
            }

            int statUpper = newStat * 2;
            int statLower = newStat;
            
            if (toughFlag)
            {
                statUpper += 50;
                statLower += 50;
            }

            statUpper = Misc.CapIntForByte(statUpper);
            statLower = Misc.CapIntForByte(statLower);

            newStat = (byte)rnd.Next(statLower, statUpper);
            return (byte)newStat;
        }

        public static byte AdjustLuck(byte baseStat, int statModifier, int sceneID, int enemySlot, bool toughFlag, Random rnd)
        {
            // Luck has two effects;
            // Lucky Hit: Luck/4 chance to land an automatic critical hit (bypasses all evasion checks, including Lucky Dodge)
            // Lucky Dodge: Luck/4 chance to evade any ability with a physical-set damage formula
            // Maxed at 255, Luck grants a 63% chance to land critical hits + dodge physical abilities.

            sceneID = SceneIdAdjust(sceneID, enemySlot);

            int newStat;
            if (sceneID >= 75 && sceneID <= 82)
            {
                // Reactor 1 enemies + MPs on bridge; will be set to 0 Luck to prevent an early difficulty spike
                newStat = 0;
            }
            else
            {
                int statUpper = 20;
                int statLower = 0;
                if (statModifier == 1)
                {
                    statUpper = 80;
                    statLower = 40;
                }

                newStat = (byte)rnd.Next(statLower, statUpper);
            }
            return (byte)newStat;
        }

        public static byte AdjustEvade(byte baseStat, int statModifier, int sceneID, int enemySlot, bool toughFlag, Random rnd)
        {
            sceneID = SceneIdAdjust(sceneID, enemySlot);

            int newStat;
            if (sceneID >= 75 && sceneID <= 82)
            {
                // Reactor 1 enemies + MPs on bridge; will be set to 0 Evade to prevent an early difficulty spike
                newStat = 0;
            }
            else
            {
                int statUpper = 20;
                int statLower = 0;
                if (statModifier == 1)
                {
                    statUpper = 80;
                    statLower = 40;
                }

                newStat = (byte)rnd.Next(statLower, statUpper);
            }
            return (byte)newStat;
        }

        public static byte AdjustStrength(byte baseStat, int statModifier, int sceneID, int enemySlot, bool toughFlag, Random rnd)
        {
            sceneID = SceneIdAdjust(sceneID, enemySlot);

            // Disc 1 - Midgar
            int newStat = 30;

            // Disc 1 - Reactor 1
            // Special balancing due to limited party
            if(sceneID >= 75 && sceneID <= 82)
            {
                newStat = 10;
            }

            // Disc 1 - Leaving Midgar
            // World Map encounters are located before SceneID 75
            if (sceneID > 117 || sceneID < 75)
            {
                newStat = 60;
            }

            // Disc 2
            if (sceneID > 167)
            {
                newStat = 90;
            }

            // Disc 3
            if (sceneID > 216)
            {
                newStat = 120;
            }

            int statUpper = newStat * 5;
            statUpper = statUpper / 4;
            int statLower = newStat;

            if (statModifier == 1)
            {
                statUpper += 30;
                statLower += 30;
            }

            if (toughFlag)
            {
                statUpper += 30;
                statLower += 30;
            }

            statUpper = Misc.CapIntForByte(statUpper);
            statLower = Misc.CapIntForByte(statLower);
          
            newStat = (byte)rnd.Next(statLower, statUpper);
            return (byte)newStat;
        }

        public static byte AdjustDefence(byte baseStat, int statModifier, int sceneID, int enemySlot, bool toughFlag, Random rnd)
        {
            sceneID = SceneIdAdjust(sceneID, enemySlot);

            // Disc 1 - Midgar
            int newStat = 20;

            // Disc 1 - Reactor 1
            // Special balancing due to limited party
            if(sceneID >= 75 && sceneID <= 82)
            {
                newStat = 15;
            }

            // Disc 1 - Leaving Midgar
            // World Map encounters are located before SceneID 75
            if (sceneID > 117 || sceneID < 75)
            {
                newStat = 40;
            }

            // Disc 2
            if (sceneID > 167)
            {
                newStat = 60;
            }

            // Disc 3
            if (sceneID > 216)
            {
                newStat = 80;
            }

            int statUpper = newStat * 2;
            int statLower = newStat;
            if (statModifier == 1)
            {
                statUpper = newStat * 3;
                statLower = newStat * 2;
            }
            if (toughFlag)
            {
                statUpper += 40;
                statLower += 40;
            }

            statUpper = Misc.CapIntForByte(statUpper);
            statLower = Misc.CapIntForByte(statLower);
          
            newStat = (byte)rnd.Next(statLower, statUpper);
            return (byte)newStat;
        }

        public static byte AdjustMagic(byte baseStat, int statModifier, int sceneID, int enemySlot, int level,
            bool toughFlag, Random rnd)
        {

            sceneID = SceneIdAdjust(sceneID, enemySlot);

            // Disc 1 - Midgar
            int newStat = 20;

            // Disc 1 - Reactor 1
            // Special balancing due to limited party
            if (sceneID >= 75 && sceneID <= 82)
            {
                newStat = 5;
            }

            // Disc 1 - Leaving Midgar
            // World Map encounters are located before SceneID 75
            if (sceneID > 117 || sceneID < 75)
            {
                newStat = 50;
            }

            // Disc 2
            if (sceneID > 167)
            {
                newStat = 80;
            }

            // Disc 3
            if (sceneID > 216)
            {
                newStat = 110;
            }

            int statUpper = newStat * 5;
            statUpper = statUpper / 4;
            int statLower = newStat;

            if (statModifier == 1)
            {
                statUpper += 20;
                statLower += 20;
            }

            if (toughFlag)
            {
                statUpper += 20;
                statLower += 20;
            }

            statUpper = Misc.CapIntForByte(statUpper);
            statLower = Misc.CapIntForByte(statLower);

            newStat = (byte)rnd.Next(statLower, statUpper);
            return (byte)newStat;
        }

        public static byte AdjustMagicDefence(byte baseStat, int statModifier, int sceneID, int enemySlot, bool toughFlag, Random rnd)
        {
            sceneID = SceneIdAdjust(sceneID, enemySlot);

            // Disc 1 - Midgar
            int newStat = 20;

            // Disc 1 - Reactor 1
            // Special balancing due to limited party
            if (sceneID >= 75 && sceneID <= 82)
            {
                newStat = 15;
            }

            // Disc 1 - Leaving Midgar
            // World Map encounters are located before SceneID 75
            if (sceneID > 117 || sceneID < 75)
            {
                newStat = 40;
            }

            // Disc 2
            if (sceneID > 167)
            {
                newStat = 60;
            }

            // Disc 3
            if (sceneID > 216)
            {
                newStat = 80;
            }

            int statUpper = newStat * 2;
            int statLower = newStat;
            if (statModifier == 1)
            {
                statUpper = newStat * 3;
                statLower = newStat * 2;
            }
            if (toughFlag)
            {
                statUpper += 40;
                statLower += 40;
            }

            statUpper = Misc.CapIntForByte(statUpper);
            statLower = Misc.CapIntForByte(statLower);

            newStat = (byte)rnd.Next(statLower, statUpper);
            return (byte)newStat;
        }

        public static int AdjustHP(int baseStat, int statModifier, int sceneID, int enemySlot, bool toughFlag, Random rnd)
        {
            int statUpper = (baseStat * 10) / 8; // +20% Max increase
            int statLower = baseStat; // No change
            if (statModifier == 1)
            {
                statUpper = (baseStat * 3) / 2; // +50% Max Increase
                statLower = (baseStat * 10) / 8; // +20% floor
            }
            baseStat = rnd.Next(statLower, statUpper);
            return baseStat;
        }

        public static byte[] AdjustElemental(byte[] data, int o, bool toughFlag, Random rnd)
        {
            // Enemy Elemental Types
            /*
                00h - Fire
                01h - Ice
                02h - Bolt
                03h - Earth
                04h - Bio
                05h - Gravity
                06h - Water
                07h - Wind
                08h - Holy
                09h - Health
                0Ah - Cut
                0Bh - Hit
                0Ch - Punch
                0Dh - Shoot
                0Eh - Shout
                0Fh - HIDDEN
                10h-1Fh - No Effect
                20h-3Fh - Statuses (Damage done by actions that inflict these statuses will be modified)
                FFh - No element
            */

            // 4 elemental properties have been set; rest are cleared)
            data[o] = (byte)rnd.Next(0, 16); o++;
            data[o] = (byte)rnd.Next(0, 16); o++;
            data[o] = (byte)rnd.Next(0, 16); o++;
            data[o] = (byte)rnd.Next(0, 16); o++;
            data[o] = 255; o++;
            data[o] = 255; o++;
            data[o] = 255; o++;
            data[o] = 255; o++;

            // Elemental Rates/Modifiers
            /*
                00h - Death
                02h - Double Damage
                03h - Unknown
                04h - Half Damage
                05h - Nullify Damage
                06h - Absorb 100%
                07h - Full Cure
                FFh - Nothing
             */

            // Sets two weaknesses, one resistance, and one null
            data[o] = 2; o++;
            data[o] = 2; o++;
            data[o] = 4; o++;
            data[o] = 5; o++;
            data[o] = 255; o++;
            data[o] = 255; o++;
            data[o] = 255; o++;
            data[o] = 255;

            return data;
        }

        public static byte[] AdjustItems(byte[] data, int o, bool toughFlag, int sceneID, Random rnd)
        {
            // Item Obtain Rates
            // 1 byte per item, 4 items. Values below 80h/128d are Drop Items (#/63 chance)
            // Values above 80h/128d are Steal Items (#/63 chance)

            // We need to add equipment that can only be acquired via drops to bosses
            // or something, and a rare chance for a 2nd steal to be added that is a
            // piece of progression-consistent equipment

            // Item 1 - Drop
            data[o] = (byte)rnd.Next(20, 50); o++;

            // Item 2 - Steal
            data[o] = (byte)rnd.Next(148, 178); o++;

            data[o] = 255; o++; // Item 3
            data[o] = 255; o++; // Item 4

            // Set Item IDs for the above Drop/Steal Rates
            int r = 0;
            while (r < 2)
            {
                string itemType;
                if (toughFlag)
                {
                    int selectType;
                    // Carry Armour onwards
                    if (sceneID >= 195)
                    {
                        selectType = rnd.Next(0, 4);
                        switch (selectType)
                        {
                            case 0:
                                itemType = "Weapon Tier C";
                                break;
                            case 1:
                                itemType = "Armour Tier C";
                                break;
                            case 2:
                                itemType = "Accessory Tier C";
                                break;
                            case 3:
                                itemType = "Limit Breaks";
                                break;
                            default:
                                itemType = "Strong Utility";
                                break;
                        }
                    }
                    // Cave of the Gi onwards
                    else if (sceneID >= 135)
                    {
                        selectType = rnd.Next(0, 6);
                        switch (selectType)
                        {
                            case 0:
                                itemType = "Weapon Tier B";
                                break;
                            case 1:
                                itemType = "Armour Tier B";
                                break;
                            case 2:
                                itemType = "Accessory Tier B";
                                break;
                            case 3:
                                itemType = "Strong Healing";
                                break;
                            case 4:
                                itemType = "Strong Utility";
                                break;
                            default:
                                itemType = "Strong Utility";
                                break;
                        }
                    }
                    else
                    {
                        // Before Cave of the Gi
                        selectType = rnd.Next(0, 3);
                        switch (selectType)
                        {
                            case 0:
                                itemType = "Weapon Tier A";
                                break;
                            case 1:
                                itemType = "Armour Tier A";
                                break;
                            case 2:
                                itemType = "Accessory Tier A";
                                break;
                            default:
                                itemType = "Strong Utility";
                                break;
                        }
                    }
                }
                else
                {
                    itemType = rnd.Next(0, 2) == 0 ? "Common Healing" : "Common Utility";
                }

                data[o] = ItemHelper.ItemFilter(itemType, rnd); o++;

                // Assign significant byte as 1 if assigned item was armour or an accessory
                if (itemType.Contains("Armour") || itemType.Contains("Accessory"))
                {
                    data[o] = 1;
                    o++;
                }
                else
                {
                    data[o] = 0;
                    o++;
                }
                r++;
            }

            // Item IDs 3 + 4 are removed
            data[o] = 255; o++;
            data[o] = 255; o++;
            data[o] = 255; o++;
            data[o] = 255;

            // Sets max rate
            //if (balancingOptions[2])
            //{
            //    o -= 12;
            //    while (c < 4)
            //    {
            //        if (data[o] < 128)
            //        {
            //            data[o] = 127;
            //            o++;
            //        }
            //        else
            //        {
            //            data[o] = 255;
            //            o++;
            //        }

            //        c++;
            //    }

            //    o += 8;
            //    c = 0;
            //}

            // Poverty Mode, wipe all the items
            //if (specialHackOptions[2])
            //{
            //    o -= 12;
            //    // Rates
            //    data[o] = 255;
            //    o++;
            //    data[o] = 255;
            //    o++;
            //    data[o] = 255;
            //    o++;
            //    data[o] = 255;
            //    o++;

            //    // Item IDs 1-4
            //    data[o] = 255;
            //    o++;
            //    data[o] = 255;
            //    o++;
            //    data[o] = 255;
            //    o++;
            //    data[o] = 255;
            //    o++;
            //    data[o] = 255;
            //    o++;
            //    data[o] = 255;
            //    o++;
            //    data[o] = 255;
            //    o++;
            //    data[o] = 255;
            //    o++;
            //}

            return data;
        }

        public static byte[] AdjustMorph(byte[] data, int o, Random rnd)
        {
            // Enemy Morph Item ID - FFFF means no morph
            //if (enemyItemOptions[1])
            //{
            //    // Prevents the setting of empty item IDs.
            //    ulong itemIDInt = (ulong)rnd.Next(320);

            //    // Prevents the setting of empty item IDs.
            //    while (itemIDInt > 104 && itemIDInt < 128)
            //    {
            //        itemIDInt = (ulong)rnd.Next(320);
            //    }

            //    // Converts into little endian
            //    byte[] converted = EndianConvert.GetLittleEndianConvert(itemIDInt);
            //    byte first = converted[0];
            //    byte second = converted[1];

            //    data[o] = first;
            //    o++;
            //    data[o] = second;
            //    o++;
            //}
            //// Poverty Mode, clear Morph
            //else if (specialHackOptions[2] != false)
            //{
            //    data[o] = 255;
            //    o++;
            //    data[o] = 255;
            //    o++;
            //}
            //else
            //{
            //    // Retain Morph data
            //    o += 2;
            //}

            string itemType;
            int morphPicker = rnd.Next(0, 20);
            if (morphPicker > 18)
            {
                itemType = "Rare Utility";
            }
            else if (morphPicker > 16)
            {
                itemType = "Rare Healing";
            }
            else if (morphPicker > 10)
            {
                itemType = "Common Utility";
            }
            else
            {
                itemType = "Common Healing";
            }
            data[o] = ItemHelper.ItemFilter(itemType, rnd); o++;
            data[o] = 0;
            return data;
        }
    }
}