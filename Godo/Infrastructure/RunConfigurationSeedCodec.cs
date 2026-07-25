using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace Godo.Infrastructure
{
    public static class RunConfigurationSeedCodec
    {
        public const string Prefix = "GODO1-";

        private const int ChecksumLength = 8;
        private const int MaximumEncodedLength = 2048;
        private const int MaximumPayloadLength = 2048;

        public static string Encode(RunConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            byte[] payload = Serialize(configuration);
            byte[] checksum = SHA256.HashData(payload);
            byte[] compressedPayload = Compress(payload);
            byte[] packet =
                new byte[ChecksumLength + compressedPayload.Length];

            Array.Copy(checksum, packet, ChecksumLength);
            Array.Copy(
                compressedPayload,
                0,
                packet,
                ChecksumLength,
                compressedPayload.Length);

            return Prefix + GetLanguageCode(configuration.Language) + "-" +
                Convert.ToBase64String(packet)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }

        public static RunConfiguration Decode(string encodedSeed)
        {
            if (string.IsNullOrWhiteSpace(encodedSeed))
            {
                throw InvalidSeed();
            }

            string trimmedSeed = encodedSeed.Trim();
            if (!trimmedSeed.StartsWith(Prefix, StringComparison.Ordinal) ||
                trimmedSeed.Length > MaximumEncodedLength)
            {
                throw InvalidSeed();
            }

            string encodedPayload =
                trimmedSeed.Substring(Prefix.Length);
            if (!TryReadLanguagePrefix(
                encodedPayload,
                out GameLanguage namedLanguage))
            {
                throw InvalidSeed();
            }
            encodedPayload = encodedPayload.Substring(4);

            try
            {
                byte[] packet = DecodeBase64Url(encodedPayload);
                if (packet.Length <= ChecksumLength)
                {
                    throw InvalidSeed();
                }

                byte[] payload = Decompress(
                    packet,
                    ChecksumLength,
                    packet.Length - ChecksumLength);
                byte[] checksum = SHA256.HashData(payload);

                if (!CryptographicOperations.FixedTimeEquals(
                    packet.AsSpan(0, ChecksumLength),
                    checksum.AsSpan(0, ChecksumLength)))
                {
                    throw InvalidSeed();
                }

                RunConfiguration configuration = Deserialize(payload);
                if (namedLanguage != configuration.Language)
                {
                    throw InvalidSeed();
                }

                return configuration;
            }
            catch (Exception ex) when (
                ex is ArgumentException ||
                ex is EndOfStreamException ||
                ex is FormatException ||
                ex is InvalidDataException ||
                ex is IOException)
            {
                throw InvalidSeed(ex);
            }
        }

        public static bool LooksLikePortableSeed(string seed)
        {
            return seed?.Trim().StartsWith(
                "GODO",
                StringComparison.OrdinalIgnoreCase) == true;
        }

        internal static string GetShortSeedName(string portableSeed)
        {
            if (string.IsNullOrWhiteSpace(portableSeed) ||
                !portableSeed.StartsWith(Prefix, StringComparison.Ordinal))
            {
                throw new ArgumentException(
                    "A valid portable seed is required.",
                    nameof(portableSeed));
            }

            int payloadOffset = Prefix.Length;
            string encodedPayload =
                portableSeed.Substring(payloadOffset);
            if (!TryReadLanguagePrefix(encodedPayload, out _))
            {
                throw new ArgumentException(
                    "The portable seed does not contain a valid language code.",
                    nameof(portableSeed));
            }
            payloadOffset += 4;

            int nameLength = Math.Min(
                portableSeed.Length,
                payloadOffset + 5);
            return portableSeed.Substring(0, nameLength);
        }

        private static byte[] Serialize(RunConfiguration configuration)
        {
            using MemoryStream stream = new MemoryStream();
            using BinaryWriter writer = new BinaryWriter(stream);

            writer.Write(configuration.Seed);
            writer.Write((byte)configuration.Language);
            writer.Write(CreateQuickOptionMask(configuration.QuickOptions));
            WriteSettings(writer, configuration.Spells);
            WriteSettings(writer, configuration.Summons);
            WriteSettings(writer, configuration.EnemySkills);
            WriteSettings(writer, configuration.AttackItems);
            WriteSettings(writer, configuration.HealItems);
            WriteSettings(writer, configuration.StatusItems);
            WriteSettings(writer, configuration.Weapons);
            WriteSettings(writer, configuration.Armour);
            WriteSettings(writer, configuration.Accessories);
            WriteSettings(writer, configuration.Materia);
            WriteSettings(writer, configuration.CharacterStats);
            WriteSettings(writer, configuration.LimitBreaks);
            WriteSettings(writer, configuration.StartingEquipment);
            WriteSettings(writer, configuration.ModelSwap);
            WriteSettings(writer, configuration.EnemyStats);
            WriteSettings(writer, configuration.EnemyAttacks);
            WriteSettings(writer, configuration.EnemyItems);
            WriteSettings(writer, configuration.Formations);
            WriteSettings(writer, configuration.Balancing);
            WriteSettings(writer, configuration.Challenges);
            WriteSettings(writer, configuration.SpecialHacks);
            writer.Write(configuration.RngOption);

            return stream.ToArray();
        }

        private static RunConfiguration Deserialize(byte[] payload)
        {
            using MemoryStream stream = new MemoryStream(payload, false);
            using BinaryReader reader = new BinaryReader(stream);

            int seed = reader.ReadInt32();
            GameLanguage language = (GameLanguage)reader.ReadByte();
            QuickOptions quickOptions = ReadQuickOptions(reader.ReadByte());
            OptionSettings spells = ReadSettings(reader, 5, 3);
            OptionSettings summons = ReadSettings(reader, 5, 3);
            OptionSettings enemySkills = ReadSettings(reader, 5, 3);
            OptionSettings attackItems = ReadSettings(reader, 3, 1);
            OptionSettings healItems = ReadSettings(reader, 3, 1);
            OptionSettings statusItems = ReadSettings(reader, 2, 0);
            OptionSettings weapons = ReadSettings(reader, 14, 8);
            OptionSettings armour = ReadSettings(reader, 13, 9);
            OptionSettings accessories = ReadSettings(reader, 6, 2);
            OptionSettings materia = ReadSettings(reader, 4, 1);
            OptionSettings characterStats = ReadSettings(reader, 18, 9, 9);
            OptionSettings limitBreaks = ReadSettings(reader, 21, 0, 9);
            OptionSettings startingEquipment =
                ReadSettings(reader, 4, 1, 9);
            OptionSettings modelSwap = ReadSettings(reader, 4, 0);
            OptionSettings enemyStats = ReadSettings(reader, 16, 13);
            OptionSettings enemyAttacks = ReadSettings(reader, 9, 3);
            OptionSettings enemyItems = ReadSettings(reader, 6, 0);
            OptionSettings formations = ReadSettings(reader, 3, 0);
            OptionSettings balancing = ReadSettings(reader, 4, 2);
            OptionSettings challenges = ReadSettings(reader, 10, 0);
            OptionSettings specialHacks = ReadSettings(reader, 5, 2);
            bool rngOption = reader.ReadBoolean();

            if (stream.Position != stream.Length)
            {
                throw InvalidSeed();
            }

            return new RunConfiguration(
                seed,
                language,
                quickOptions,
                spells,
                summons,
                enemySkills,
                attackItems,
                healItems,
                statusItems,
                weapons,
                armour,
                accessories,
                materia,
                characterStats,
                limitBreaks,
                startingEquipment,
                modelSwap,
                enemyStats,
                enemyAttacks,
                enemyItems,
                formations,
                balancing,
                challenges,
                specialHacks,
                rngOption);
        }

        private static void WriteSettings(
            BinaryWriter writer,
            OptionSettings settings)
        {
            foreach (bool option in settings.Options)
            {
                writer.Write(option);
            }

            foreach (int parameter in settings.Parameters)
            {
                writer.Write(parameter);
            }

            foreach (bool selection in settings.Selections)
            {
                writer.Write(selection);
            }
        }

        private static OptionSettings ReadSettings(
            BinaryReader reader,
            int optionCount,
            int parameterCount,
            int selectionCount = 0)
        {
            bool[] options = new bool[optionCount];
            int[] parameters = new int[parameterCount];
            bool[] selections = new bool[selectionCount];

            for (int index = 0; index < options.Length; index++)
            {
                options[index] = reader.ReadBoolean();
            }

            for (int index = 0; index < parameters.Length; index++)
            {
                parameters[index] = reader.ReadInt32();
            }

            for (int index = 0; index < selections.Length; index++)
            {
                selections[index] = reader.ReadBoolean();
            }

            return new OptionSettings(options, parameters, selections);
        }

        private static byte CreateQuickOptionMask(QuickOptions options)
        {
            byte mask = 0;
            SetBit(ref mask, 0, options.Weapons);
            SetBit(ref mask, 1, options.Armour);
            SetBit(ref mask, 2, options.Accessories);
            SetBit(ref mask, 3, options.CharacterStats);
            SetBit(ref mask, 4, options.StartingMateria);
            SetBit(ref mask, 5, options.EnemyStats);
            SetBit(ref mask, 6, options.EnemyItems);
            return mask;
        }

        private static QuickOptions ReadQuickOptions(byte mask)
        {
            if ((mask & 0x80) != 0)
            {
                throw InvalidSeed();
            }

            return new QuickOptions(
                IsBitSet(mask, 0),
                IsBitSet(mask, 1),
                IsBitSet(mask, 2),
                IsBitSet(mask, 3),
                IsBitSet(mask, 4),
                IsBitSet(mask, 5),
                IsBitSet(mask, 6));
        }

        private static void SetBit(ref byte mask, int bit, bool value)
        {
            if (value)
            {
                mask |= (byte)(1 << bit);
            }
        }

        private static bool IsBitSet(byte mask, int bit)
        {
            return (mask & (1 << bit)) != 0;
        }

        private static string GetLanguageCode(GameLanguage language)
        {
            return language switch
            {
                GameLanguage.English => "ENG",
                GameLanguage.German => "DEU",
                GameLanguage.Spanish => "ESP",
                GameLanguage.French => "FRA",
                GameLanguage.Japanese => "JPN",
                _ => throw new ArgumentOutOfRangeException(
                    nameof(language),
                    language,
                    "The language is not supported.")
            };
        }

        private static bool TryReadLanguagePrefix(
            string encodedPayload,
            out GameLanguage language)
        {
            language = default;
            if (encodedPayload.Length < 4 ||
                encodedPayload[3] != '-')
            {
                return false;
            }

            switch (encodedPayload.Substring(0, 3))
            {
                case "ENG":
                    language = GameLanguage.English;
                    return true;
                case "DEU":
                    language = GameLanguage.German;
                    return true;
                case "ESP":
                    language = GameLanguage.Spanish;
                    return true;
                case "FRA":
                    language = GameLanguage.French;
                    return true;
                case "JPN":
                    language = GameLanguage.Japanese;
                    return true;
                default:
                    return false;
            }
        }

        private static byte[] Compress(byte[] payload)
        {
            using MemoryStream output = new MemoryStream();
            using (DeflateStream compressor = new DeflateStream(
                output,
                CompressionLevel.SmallestSize,
                true))
            {
                compressor.Write(payload, 0, payload.Length);
            }

            return output.ToArray();
        }

        private static byte[] Decompress(
            byte[] packet,
            int offset,
            int count)
        {
            using MemoryStream input =
                new MemoryStream(packet, offset, count, false);
            using DeflateStream decompressor =
                new DeflateStream(input, CompressionMode.Decompress);
            using MemoryStream output = new MemoryStream();
            byte[] buffer = new byte[256];
            int bytesRead;

            while ((bytesRead = decompressor.Read(
                buffer,
                0,
                buffer.Length)) > 0)
            {
                if (output.Length + bytesRead > MaximumPayloadLength)
                {
                    throw InvalidSeed();
                }

                output.Write(buffer, 0, bytesRead);
            }

            return output.ToArray();
        }

        private static byte[] DecodeBase64Url(string value)
        {
            string base64 = value
                .Replace('-', '+')
                .Replace('_', '/');
            int paddingLength = (4 - base64.Length % 4) % 4;
            base64 = base64.PadRight(base64.Length + paddingLength, '=');
            return Convert.FromBase64String(base64);
        }

        private static FormatException InvalidSeed(Exception inner = null)
        {
            const string message =
                "The portable seed is invalid, damaged, or uses an unsupported version.";
            return inner == null
                ? new FormatException(message)
                : new FormatException(message, inner);
        }
    }
}
