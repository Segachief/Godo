using Godo.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Godo.Tests
{
    [TestClass]
    public class BinarySectionTests
    {
        [TestMethod]
        public void SceneSectionRequiresTheCanonicalLength()
        {
            InvalidDataException exception =
                Assert.ThrowsException<InvalidDataException>(
                    () => new SceneSection(
                        12,
                        new byte[SceneSection.ExpectedLength - 1]));

            StringAssert.Contains(exception.Message, "Scene 12");
            StringAssert.Contains(
                exception.Message,
                SceneSection.ExpectedLength.ToString());
        }

        [TestMethod]
        public void SceneSectionExposesNamedRegionsOverTheOwnedBuffer()
        {
            byte[] data = new byte[SceneSection.ExpectedLength];
            SceneSection section = new SceneSection(42, data);

            Assert.AreEqual(42, section.Id);
            Assert.AreEqual(
                SceneSection.EnemyDataLength,
                section.Enemies.Length);
            Assert.AreEqual(
                SceneSection.AttackDataLength,
                section.Attacks.Length);
            Assert.AreEqual(
                SceneSection.EnemyAiLength,
                section.EnemyAi.Length);

            section.AttackIds.Span[0] = 0x7A;
            Assert.AreEqual(
                0x7A,
                data[SceneSection.AttackIdsOffset]);

            section.WriteUInt16LittleEndian(
                SceneSection.EnemyIdsOffset,
                0x1234);
            Assert.AreEqual(
                0x1234,
                section.ReadUInt16LittleEndian(
                    SceneSection.EnemyIdsOffset));
        }

        [TestMethod]
        public void KernelSectionValidatesHeaderIdentityAndLength()
        {
            Assert.ThrowsException<InvalidDataException>(
                () => new KernelSection(
                    5,
                    6,
                    32,
                    new byte[32]));
            Assert.ThrowsException<InvalidDataException>(
                () => new KernelSection(
                    5,
                    5,
                    32,
                    new byte[31]));
        }

        [TestMethod]
        public void KernelSectionExposesKindAndPermitsOnlyTextReplacement()
        {
            KernelSection weaponSection =
                new KernelSection(5, 5, 32, new byte[32]);
            KernelSection textSection =
                new KernelSection(9, 9, 16, new byte[16]);

            Assert.AreEqual(
                KernelSectionType.Weapons,
                weaponSection.SectionType);
            Assert.IsFalse(weaponSection.IsText);
            Assert.ThrowsException<InvalidOperationException>(
                () => weaponSection.ReplaceTextData(new byte[8]));

            byte[] replacement = new byte[24];
            textSection.ReplaceTextData(replacement);

            Assert.IsTrue(textSection.IsText);
            Assert.AreSame(replacement, textSection.Data);
            Assert.AreEqual(16, textSection.OriginalLength);
            Assert.AreEqual(24, textSection.Data.Length);
        }
    }
}
