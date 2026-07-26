using System;
using System.Buffers.Binary;
using System.IO;

namespace Godo.Infrastructure
{
    public sealed class SceneSection
    {
        public const int ExpectedLength = 7808;
        public const int EnemyIdsOffset = 0x0000;
        public const int EnemyIdsLength = 8;
        public const int BattleSetupOffset = 0x0008;
        public const int BattleSetupLength = 80;
        public const int CameraPlacementOffset = 0x0058;
        public const int CameraPlacementLength = 192;
        public const int FormationPlacementOffset = 0x0118;
        public const int FormationPlacementLength = 384;
        public const int EnemyDataOffset = 0x0298;
        public const int EnemyDataLength = 552;
        public const int AttackDataOffset = 0x04C0;
        public const int AttackDataLength = 896;
        public const int AttackIdsOffset = 0x0840;
        public const int AttackIdsLength = 64;
        public const int AttackNamesOffset = 0x0880;
        public const int AttackNamesLength = 1024;
        public const int FormationAiOffsetsOffset = 0x0C80;
        public const int FormationAiOffsetsLength = 8;
        public const int FormationAiOffset = 0x0C88;
        public const int FormationAiLength = 504;
        public const int EnemyAiOffsetsOffset = 0x0E80;
        public const int EnemyAiOffsetsLength = 6;
        public const int EnemyAiOffset = 0x0E86;
        public const int EnemyAiLength =
            ExpectedLength - EnemyAiOffset;

        public SceneSection(int id, byte[] data)
        {
            if (id < 0 || id > 255)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(id),
                    id,
                    "A scene identifier must be between 0 and 255.");
            }
            ArgumentNullException.ThrowIfNull(data);
            if (data.Length != ExpectedLength)
            {
                throw new InvalidDataException(
                    "Scene " + id + " has length " + data.Length +
                    "; expected " + ExpectedLength + " bytes.");
            }

            Id = id;
            Data = data;
        }

        public int Id { get; }
        public byte[] Data { get; }
        public Memory<byte> Memory => Data;
        public Memory<byte> EnemyIds =>
            Data.AsMemory(EnemyIdsOffset, EnemyIdsLength);
        public Memory<byte> BattleSetup =>
            Data.AsMemory(BattleSetupOffset, BattleSetupLength);
        public Memory<byte> CameraPlacement =>
            Data.AsMemory(CameraPlacementOffset, CameraPlacementLength);
        public Memory<byte> FormationPlacement =>
            Data.AsMemory(
                FormationPlacementOffset,
                FormationPlacementLength);
        public Memory<byte> Enemies =>
            Data.AsMemory(EnemyDataOffset, EnemyDataLength);
        public Memory<byte> Attacks =>
            Data.AsMemory(AttackDataOffset, AttackDataLength);
        public Memory<byte> AttackIds =>
            Data.AsMemory(AttackIdsOffset, AttackIdsLength);
        public Memory<byte> AttackNames =>
            Data.AsMemory(AttackNamesOffset, AttackNamesLength);
        public Memory<byte> FormationAiOffsets =>
            Data.AsMemory(
                FormationAiOffsetsOffset,
                FormationAiOffsetsLength);
        public Memory<byte> FormationAi =>
            Data.AsMemory(FormationAiOffset, FormationAiLength);
        public Memory<byte> EnemyAiOffsets =>
            Data.AsMemory(
                EnemyAiOffsetsOffset,
                EnemyAiOffsetsLength);
        public Memory<byte> EnemyAi =>
            Data.AsMemory(EnemyAiOffset, EnemyAiLength);

        public ushort ReadUInt16LittleEndian(int offset)
        {
            return BinaryPrimitives.ReadUInt16LittleEndian(
                GetSpan(offset, sizeof(ushort)));
        }

        public void WriteUInt16LittleEndian(int offset, ushort value)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(
                GetSpan(offset, sizeof(ushort)),
                value);
        }

        public Span<byte> GetSpan(int offset, int length)
        {
            ValidateRange(offset, length, Data.Length);
            return Data.AsSpan(offset, length);
        }

        private static void ValidateRange(
            int offset,
            int length,
            int dataLength)
        {
            if (offset < 0 ||
                length < 0 ||
                offset > dataLength - length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(offset),
                    "The requested scene region is outside the section.");
            }
        }
    }

    public enum KernelSectionType
    {
        Commands = 0,
        Attacks = 1,
        BattleAndGrowth = 2,
        InitialData = 3,
        Items = 4,
        Weapons = 5,
        Armour = 6,
        Accessories = 7,
        Materia = 8,
        CommandDescriptions = 9,
        MagicDescriptions = 10,
        ItemDescriptions = 11,
        WeaponDescriptions = 12,
        ArmourDescriptions = 13,
        AccessoryDescriptions = 14,
        MateriaDescriptions = 15,
        KeyItemDescriptions = 16,
        CommandNames = 17,
        MagicNames = 18,
        ItemNames = 19,
        WeaponNames = 20,
        ArmourNames = 21,
        AccessoryNames = 22,
        MateriaNames = 23,
        KeyItemNames = 24,
        BattleText = 25,
        SummonAttackNames = 26
    }

    public sealed class KernelSection
    {
        public const int SectionCount = 27;
        public const int FirstTextSectionIndex = 9;

        public KernelSection(
            int index,
            int sectionId,
            int expectedLength,
            byte[] data)
        {
            if (index < 0 || index >= SectionCount)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(index),
                    index,
                    "A kernel section index must be between 0 and 26.");
            }
            if (sectionId < 0 || sectionId >= SectionCount)
            {
                throw new InvalidDataException(
                    "Kernel section " + index +
                    " has invalid identifier " + sectionId + ".");
            }
            int expectedHeaderId =
                Math.Min(index, FirstTextSectionIndex);
            if (sectionId != expectedHeaderId)
            {
                throw new InvalidDataException(
                    "Kernel section at index " + index +
                    " declares header identifier " + sectionId +
                    "; expected " + expectedHeaderId + ".");
            }
            if (expectedLength < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(expectedLength));
            }
            ArgumentNullException.ThrowIfNull(data);
            if (data.Length != expectedLength)
            {
                throw new InvalidDataException(
                    "Kernel section " + index + " has length " +
                    data.Length + "; expected " + expectedLength +
                    " bytes.");
            }

            Index = index;
            HeaderId = sectionId;
            OriginalLength = expectedLength;
            Data = data;
        }

        public int Index { get; }
        public int HeaderId { get; }
        public KernelSectionType SectionType =>
            (KernelSectionType)Index;
        public int OriginalLength { get; }
        public bool IsText => Index >= FirstTextSectionIndex;
        public byte[] Data { get; private set; }
        public Memory<byte> Memory => Data;

        public ushort ReadUInt16LittleEndian(int offset)
        {
            return BinaryPrimitives.ReadUInt16LittleEndian(
                GetSpan(offset, sizeof(ushort)));
        }

        public void WriteUInt16LittleEndian(int offset, ushort value)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(
                GetSpan(offset, sizeof(ushort)),
                value);
        }

        public Span<byte> GetSpan(int offset, int length)
        {
            if (offset < 0 ||
                length < 0 ||
                offset > Data.Length - length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(offset),
                    "The requested kernel region is outside the section.");
            }

            return Data.AsSpan(offset, length);
        }

        public void ReplaceTextData(byte[] data)
        {
            if (!IsText)
            {
                throw new InvalidOperationException(
                    "Only kernel text sections can change length.");
            }
            ArgumentNullException.ThrowIfNull(data);
            Data = data;
        }

        public void EnsureSectionType(
            KernelSectionType expectedSectionType)
        {
            if (SectionType != expectedSectionType)
            {
                throw new InvalidOperationException(
                    "Kernel section " + Index + " is " +
                    SectionType + "; expected " +
                    expectedSectionType + ".");
            }
        }
    }
}
