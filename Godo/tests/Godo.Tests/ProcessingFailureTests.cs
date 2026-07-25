using Godo.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.IO.Compression;

namespace Godo.Tests
{
    [TestClass]
    public class ProcessingFailureTests
    {
        [TestMethod]
        public void ExactDecompressionRejectsShortOutput()
        {
            using MemoryStream compressed = Compress(new byte[31]);
            using GZipStream decompressor =
                new GZipStream(compressed, CompressionMode.Decompress);

            InvalidDataException exception =
                Assert.ThrowsException<InvalidDataException>(
                    () => GZipper.ReadExactlyAndEnsureEnd(
                        decompressor,
                        new byte[32],
                        "test data"));

            StringAssert.Contains(exception.Message, "shorter than expected");
            Assert.IsInstanceOfType(
                exception.InnerException,
                typeof(EndOfStreamException));
        }

        [TestMethod]
        public void ExactDecompressionRejectsLongOutput()
        {
            using MemoryStream compressed = Compress(new byte[33]);
            using GZipStream decompressor =
                new GZipStream(compressed, CompressionMode.Decompress);

            InvalidDataException exception =
                Assert.ThrowsException<InvalidDataException>(
                    () => GZipper.ReadExactlyAndEnsureEnd(
                        decompressor,
                        new byte[32],
                        "test data"));

            StringAssert.Contains(exception.Message, "longer than expected");
        }

        [TestMethod]
        public void ProcessingExceptionIsWrappedAndPropagated()
        {
            string missingOutputDirectory = Path.Combine(
                Path.GetTempPath(),
                "Godo.Tests",
                Guid.NewGuid().ToString("N"),
                "missing");

            InvalidOperationException exception =
                Assert.ThrowsException<InvalidOperationException>(
                    () => KernelTextRewriter.CommandDescriptionRewrite(
                        new bool[0],
                        Path.Combine(missingOutputDirectory, "command.bin")));

            Assert.AreEqual(
                "Command description rewrite failed.",
                exception.Message);
            Assert.IsInstanceOfType(
                exception.InnerException,
                typeof(DirectoryNotFoundException));
        }

        private static MemoryStream Compress(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            using (GZipStream compressor =
                new GZipStream(output, CompressionMode.Compress, true))
            {
                compressor.Write(data, 0, data.Length);
            }

            output.Position = 0;
            return output;
        }
    }
}
