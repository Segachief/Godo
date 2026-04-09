using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Godo.Helper
{
    public class ItemHelper
    {
        public static byte ItemFilter(string itemType, Random seed)
        {
            byte itemPick = new byte();
            if (itemType == "Common Healing")
            {
                #region List of common healing items

                /*
                0x00 Potion
                0x01 Hi-Potion
                0x03 Ether
                0x07 Phoenix Down
                0x08 Antidote
                0x09 Soft
                0x0A Maiden's Kiss
                0x0B Cornucopia
                0x0C Echo Screen
                0x0D Hyper
                0x0E Tranquilizer
                0x0F Remedy
                0x34 Eye drop
                0x46 Tent
                */
                #endregion

                byte[] itemSet =
                {
                    0x00, 0x01, 0x03, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x34, 0x46
                };
                itemPick = itemSet[seed.Next(itemSet.Length)];
            }

            if (itemType == "Strong Healing")
            {
                #region List of strong healing items

                /*
                0x02 X-Potion
                0x04 Turbo Ether
                0x05 Elixir
                0x06 Megalixir
                */
                #endregion

                byte[] itemSet =
                {
                    0x02, 0x04, 0x05, 0x06
                };
                itemPick = itemSet[seed.Next(itemSet.Length)];
            }

            if (itemType == "Common Utility")
            {
                #region List of common utility items

                /*
                0x10 Smoke Bomb
                0x11 Speed Drink
                0x13 Vaccine
                0x14 Grenade
                0x15 Shrapnel
                0x17 Hourglass
                0x18 Kiss of Death
                0x19 Spider Web
                0x1A Dream Powder
                0x1B Mute Mask
                0x1C War Gong
                0x1D Loco Weed
                0x1E Fire Fang
                0x20 Antarctic Wind
                0x22 Bolt Plume
                0x24 Earth Drum
                0x26 Deadly Waste
                0x29 Vampire Fang
                0x2A Ghost Hand
                0x2B Vagyrisk Claw
                0x2C Light Curtain
                0x2D Lunar Curtain
                0x2E Mirror
                0x2F Holy Torch
                0x32 Impaler
                0x33 Shrivel
                0x34 Eye drop
                0x35 Molotov
                0x38 Graviball
                0x39 T/S Bomb
                0x3A Ink
                0x3B Dazers
                */
                #endregion

                byte[] itemSet =
                {
                    0x00, 0x01, 0x03, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
                    0x10, 0x11, 0x13, 0x14, 0x15, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x20, 0x22, 0x24,
                    0x26, 0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F, 0x32, 0x33, 0x34, 0x35, 0x38, 0x39, 0x3A, 0x3B, 0x46
                };
                itemPick = itemSet[seed.Next(itemSet.Length)];
            }

            if (itemType == "Strong Utility")
            {
                #region List of strong utility items

                /*
                0x12 Hero Drink
                0x16 Right Arm
                0x1F Fire Veil
                0x21 Ice Crystal
                0x23 Swift Bolt
                0x25 Earth Mallet
                0x27 M-Tentacles
                0x28 Stardust
                0x30 Bird Wing
                0x31 Dragon Scales
                0x36 S-mine
                0x37 8inch Cannon
                0x3C Dragon Fang
                0x3D Cauldron
                0x47 Power Source
                0x48 Guard Source
                0x49 Magic Source
                0x4A Mind Source
                0x4B Speed Source
                0x4C Luck Source
                */
                #endregion

                byte[] itemSet =
                {
                    0x12, 0x16, 0x1F, 0x21, 0x23, 0x25, 0x27, 0x28, 0x30, 0x31, 0x36, 0x37,
                    0x3C, 0x3D, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C
                };
                itemPick = itemSet[seed.Next(itemSet.Length)];
            }

            if (itemType == "Greens & Nuts")
            {
                #region List of greens/nuts items

                /*
                0x3E Sylkis Greens
	            0x3F Reagan Greens
	            0x40 Mimett Greens
	            0x41 Curiel Greens
	            0x42 Pahsana Greens
	            0x43 Tantal Greens
	            0x44 Krakka Greens
	            0x45 Gysahl Greens
                0x4D Zeio Nut
	            0x4E Carob Nut
	            0x4F Porov Nut
	            0x50 Pram Nut
	            0x51 Lasan Nut
	            0x52 Saraha Nut
	            0x53 Luchile Nut
	            0x54 Pepio Nut
                */
                #endregion

                byte[] itemSet =
                {
                    0x3E, 0x3F, 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x4D,
                    0x4E, 0x4F, 0x50, 0x51, 0x52, 0x53, 0x54
                };
                itemPick = itemSet[seed.Next(itemSet.Length)];
            }

            if (itemType == "Limit Breaks")
            {
                #region List of limit break items
                /*
                0x57 Omnislash
                0x58 Catastrophe
                0x59 Final Heaven
                0x5A Great Gospel
                0x5B Cosmo Memory
                0x5C All Creation
                0x5D Chaos
                0x5E Highwind
                */
                #endregion

                byte[] itemSet =
                {
                    0x57, 0x58, 0x59, 0x5A, 0x5B, 0x5C, 0x5D, 0x5E
                };
                itemPick = itemSet[seed.Next(itemSet.Length)];
            }

            if (itemType == "Misc")
            {
                #region List of misc items
                /*
                0x5F 1/35 Soldier
	            0x60 Super Sweeper
	            0x61 Masamune Blade
	            0x62 Save Crystal
	            0x63 Combat Diary
	            0x64 Autograph
	            0x65 Gambler
	            0x66 Desert Rose
                0x67 Earth Harp
	            0x68 Guide Book
                */
                #endregion

                byte[] itemSet =
                {
                    0x5F, 0x60, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67,
                    0x68, 0x69, 0x6A, 0x6B, 0x6C, 0x6D, 0x6E
                };
                itemPick = itemSet[seed.Next(itemSet.Length)];
            }

            if (itemType == "Weapon Tier A")
            {
                #region List of armour
                /*
	            0x81 Mythril Saber
	            0x82 Hardedge
	            0x83 Butterfly Edge
	            0x87 Force Stealer
                0x88 Rune Blade

                0x91 Metal Knuckle
                0x92 Mythril Claw
                0x93 Grand Glove
                0x94 Tiger Fang
                0x98 Motor Drive
                0x99 Platinum Fist

                0xA1 Assault Gun
                0xA2 Cannon Ball
                0xA3 Atomic Scissors
                0xA4 Heavy Vulcan
                0xA8 W-Machine Gun
                0xA9 Drill Arm
                0xAC Enemy Launcher

                0xB1 Diamond Pin
                0xB2 Silver Barette
                0xB6 Magic Comb
                0xB7 Plus Barette

                0xBF Mythril Rod
                0xC0 Full Metal Staff
                0xC1 Striking Staff
                0xC2 Prism Staff
                0xC4 Wizard Staff
                0xC5 Wizer Staff

                0xD8 Boomerang
                0xD9 Pinwheel
                0xDD Wind Slash
                0xDE Twin Viper

                0xE6 Green M-Phone
                0xEA White M-Phone
                0xEB Black M-Phone
                0XEC Silver M-Phone

                */
                #endregion

                byte[] itemSet =
                {
                    0x81, 0x82, 0x83, 0x87, 0x85, 0x88,
                    0x91, 0x92, 0x93, 0x94, 0x98, 0x99,
                    0xA1, 0xA2, 0xA3, 0xA4, 0xA8, 0xA9, 0xAC,
                    0xB1, 0xB2, 0xB6, 0xB7,
                    0xBF, 0xC0, 0xC1, 0xC2, 0xC4, 0xC5,
                    0xD8, 0xD9, 0xDD, 0xDE,
                    0xE6, 0xEA, 0xEB, 0xEC

                };
                itemPick = itemSet[seed.Next(itemSet.Length)];
            }

            if (itemType == "Weapon Tier B")
            {
                #region List of armour
                /*
	            0x84 Enhance Sword
	            0x85 Organics
	            0x86 Crystal Sword
                0x89 Murasame
                0x8A Nail Bat
                0x8B Yoshiyuki

                0x95 Diamond Knuckle
                0x96 Dragon Claw
                0x97 Crystal Glove
                0x9A Kaiser Knuckle
                0x9B Work Glove
                0x9C Powersoul

                0xA5 Chainsaw
                0xA6 Microlaser
                0xA7 A-M Cannon
                0xAA Solid Bazooka
                0xAB Rocket Punch

                0xB3 Gold Barette
                0xB4 Adaman Clip
                0xB5 Crystal Comb
                0xB8 Centclip
                0xB9 Hairpin
                0xBA Seraph Comb

                0xC3 Aurora Rod
                0xC6 Fairy Tale
                0xC7 Umbrella
                0xC8 Princess Guard

                0xCA Slash Lance
                0xCB Trident
                0xCC Mast Ax
                0xCD Partisan
                0xCE Viper Halberd
                0xCF Javelin
                0xD0 Grow Lance
                0xD1 Mop
                0xD2 Dragoon Lance

                0xDA Razor Ring
                0xDB Hawkeye
                0xDC Crystal Cross
                0xDF Spiral Shuriken
                0xE0 Superball
                0xE1 Magic Shuriken
                0xE2 Rising Sun

                0xE7 Blue M-Phone
                0xE8 Red M-Phone
                0xE9 Crystal M-Phone
                0xED Trumpet Shell
                0xEE Gold M-Phone

                0xF3 Shotgun
                0xF4 Shortbarrel
                0xF5 Lariat
                0xF6 Winchester
                0xF7 Peacemaker
                0xF8 Buntline
                0xF9 Long Barrel R
                0xFA Silver Rifle
                0xFB Sniper CR

                0xFF Masamune

                */
                #endregion

                byte[] itemSet =
                {
                    0x84, 0x85, 0x86, 0x89, 0x8A, 0x8B,
                    0x95, 0x96, 0x97, 0x9A, 0x9B, 0x9C,
                    0xA5, 0xA6, 0xA7, 0xAA, 0xAB,
                    0xB3, 0xB4, 0xB5, 0xB8, 0xB9, 0xBA,
                    0xC3, 0xC6, 0xC7, 0xC8,
                    0xCA, 0xCB, 0xCC, 0xCD, 0xCE, 0xCF, 0xD0, 0xD1, 0xD2,
                    0xDA, 0xDB, 0xDC, 0xDF, 0xE0, 0xE1, 0xE2,
                    0xE7, 0xE8, 0xE9, 0xED, 0xEE,
                    0xF3, 0xF4, 0xF5, 0xF6, 0xF7, 0xF8, 0xF9, 0xFA, 0xFB

                };
                itemPick = itemSet[seed.Next(itemSet.Length)];
            }

            if (itemType == "Weapon Tier C")
            {
                #region List of armour
                /*
                0x8C Apocalypse
                0x8D Heaven's Cloud
                0x8E Ragnarok
                0x8F Ultima Weapon

                0x9D Master Fist
                0x9E God's Hand
                0x9F Premium Heart

                0xAD Pile Banger
                0xAE Max Ray
                0xAF Missing Score

                0xBB Behemoth Horn
                0xBC Spring Gun Clip
                0xBD Limited Moon

                0xD4 Flayer
                0xD5 Spirit Lance
                0xD6 Venus Gospel

                0xE3 Oritsuru
                0xE4 Conformer

                0xEF Battle Trumpet
                0xF0 Starlight Phone
                0xF1 HP Shout

                0xFC Supershot ST
                0xFD Outsider
                0xFE Death Penalty

                0xFF Masamune

                */
                #endregion

                byte[] itemSet =
                {
                    0x8C, 0x8D, 0x8E, 0x8F,
                    0x9D, 0x9E, 0x9F,
                    0xAD, 0xAE, 0xAF,
                    0xBB, 0xBC, 0xBD,
                    0xD4, 0xD5, 0xD6,
                    0xE3, 0xE4,
                    0xEF, 0xF0, 0xF1,
                    0xFC, 0xFD, 0xFE,
                    0xFF
                };
                itemPick = itemSet[seed.Next(itemSet.Length)];
            }

            if (itemType == "Armour Tier A")
            {
                #region List of armour
                /*
                0x00 Bronze Bangle
	            0x01 Iron Bangle
	            0x02 Titan Bangle
	            0x03 Mythril Armlet
	            0x04 Carbon Bangle
	            0x05 Silver Armlet
	            0x06 Gold Armlet
	            0x09 Platinum Bangle
                0x0A Rune Armlet
                0x13 Shinra Beta
                0x15 Four Slots
                0x1E Precious Watch
                0x1F Chocobracelet
                */
                #endregion

                byte[] itemSet =
                {
                    0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x09,
                    0x0A, 0x13, 0x15, 0x1E, 0x1F
                };
                itemPick = itemSet[seed.Next(itemSet.Length)];
            }

            if (itemType == "Armour Tier B")
            {
                #region List of armour
                /*
	            0x07 Diamond Bangle
                0x08 Crystal Bangle
                0x0B Edincoat
                0x0C Wizard Bracelet
                0x0D Adaman Bangle
                0x0E Gigas Armlet
                0x14 Shinra Alpha
                0x16 Fire Armlet
                0x17 Aurora Armlet
                0x18 Bolt Armlet
                0x19 Dragon Armlet
                */
                #endregion

                byte[] itemSet =
                {
                    0x07, 0x08, 0x0B, 0x0C, 0x0D, 0x0E, 0x14, 0x16, 0x17, 0x18, 0x19
                };
                itemPick = itemSet[seed.Next(itemSet.Length)];
            }

            if (itemType == "Armour Tier C")
            {
                #region List of armour
                /*
                0x0F Imperial Guard
                0x10 Aegis Armlet
                0x11 Fourth Bracelet
                0x12 Warrior Bangle
                0x1A Minerva Band
                0x1B Escort Guard
                0x1C Mystile
                0x1D Ziedrich
                */
                #endregion

                byte[] itemSet =
                {
                    0x0F, 0x10, 0x11, 0x12, 0x1A, 0x1B, 0x1C, 0x1D
                };
                itemPick = itemSet[seed.Next(itemSet.Length)];
            }

            if (itemType == "Accessory Tier A")
            {
                #region List of accessories
                /*
                0x20 Power Wrist
	            0x21 Protect Vest
	            0x22 Earring
	            0x23 Talisman
	            0x24 Choco Feather
	            0x25 Amulet
	            0x27 Poison Ring
                0x2A Star Pendant
                0x2B Silver Glasses
                0x2C Headband
                0x2E Jem Ring
                0x2F White Cape
                0x31 Peace Ring
                0x37 Safety Bit
                0x3B Cat's Bell
                */
                #endregion

                byte[] itemSet =
                {
                    0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x07, 0x0A,
                    0x0B, 0x0C, 0x0E, 0x0F, 0x11, 0x17, 0x1B
                };
                itemPick = itemSet[seed.Next(itemSet.Length)];
            }

            if (itemType == "Accessory Tier B")
            {
                #region List of accessories
                /*
                0x2D Fairy Ring
                0x30 Sprint Shoes
                0x33 Fire Ring
                0x34 Ice Ring
                0x35 Bolt Ring
                0x37 Safety Bit
                0x38 Fury Ring
                0x3A Protect Ring
                0x3C Reflect Ring
                0x3D Water Ring
                0x3E Sneak Glove
                0x3F Hypnocrown
                */
                #endregion

                byte[] itemSet =
                {
                    0x0D, 0x10, 0x13, 0x14, 0x15, 0x17, 0x18, 0x1A,
                    0x1C, 0x1D, 0x1E, 0x1F
                };
                itemPick = itemSet[seed.Next(itemSet.Length)];
            }

            if (itemType == "Accessory Tier C")
            {
                #region List of accessories
                /*
	            0x26 Champion Belt
                0x28 Touph Ring
                0x29 Circlet
                0x32 Ribbon
                0x36 Tetra Elemental
                0x39 Curse Ring
                */
                #endregion

                byte[] itemSet =
                {
                    0x06, 0x08, 0x09, 0x12, 0x16, 0x19
                };
                itemPick = itemSet[seed.Next(itemSet.Length)];
            }

            return itemPick;
        }

        public static int ItemType(int itemID)
        {
            // Identify Item Type
            // 0 = Exclude, 1 = Attack, 2 = Heal, 3 = Status
            int type = 0;
            switch (itemID)
            {
                case 16: // Smoke Bomb
                case 18: // Hero Drink
                    type = 0;
                    break;

                // Attack
                case 20:
                case 21:
                case 22:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                case 48:
                case 49:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 60:
                    type = 1;
                    break;

                // Heal
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                    type = 2;
                    break;

                // Status
                case 17:
                case 19:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 50:
                case 51:
                case 52:
                case 58:
                case 59:
                case 61:
                    type = 3;
                    break;

                default:
                    type = 0;
                    break;
            }
            return type;
        }
    }
}
