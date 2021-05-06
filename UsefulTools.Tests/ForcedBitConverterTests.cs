using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UsefulTools.Tests
{
    [TestClass]
    public class ForcedBitConverterTests
    {
        [TestMethod]
        public void GetUshortLittleEndian_0()
        {
            byte[] bytes = { 0b_00000000, 0b_00000000 };
            int startIndex = 0;
            ushort expected = 0;

            ushort actual = ForcedBitConverter.GetUshortLittleEndian(bytes, startIndex);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetUshortLittleEndian_0_withStartIndex()
        {
            byte[] bytes = { byte.MaxValue, 0b_00000000, 0b_00000000 };
            int startIndex = 1;
            ushort expected = 0;

            ushort actual = ForcedBitConverter.GetUshortLittleEndian(bytes, startIndex);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetUshortLittleEndian_42()
        {
            byte[] bytes = { 0b_00101010, 0b_00000000 };
            int startIndex = 0;
            ushort expected = 42;

            ushort actual = ForcedBitConverter.GetUshortLittleEndian(bytes, startIndex);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetUshortLittleEndian_42_withStartIndex()
        {
            byte[] bytes = { byte.MaxValue, byte.MaxValue, 0b_00101010, 0b_00000000, byte.MaxValue };
            int startIndex = 2;
            ushort expected = 42;

            ushort actual = ForcedBitConverter.GetUshortLittleEndian(bytes, startIndex);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetUshortLittleEndian_65322()
        {
            byte[] bytes = { 0b_00101010, 0b_11111111 };
            int startIndex = 0;
            ushort expected = 65322;

            ushort actual = ForcedBitConverter.GetUshortLittleEndian(bytes, startIndex);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetUint64LittleEndian_0()
        {
            byte[] bytes = { 0x_00, 0x_00, 0x_00, 0x_00, 0x_00, 0x_00, 0x_00, 0x_00 };
            int startIndex = 0;
            ulong expected = 0;

            ulong actual = ForcedBitConverter.GetUint64LittleEndian(bytes, startIndex);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetUint64LittleEndian_0_withStartIndex()
        {
            byte[] bytes = { byte.MaxValue, 0x_00, 0x_00, 0x_00, 0x_00, 0x_00, 0x_00, 0x_00, 0x_00 };
            int startIndex = 1;
            ulong expected = 0;

            ulong actual = ForcedBitConverter.GetUint64LittleEndian(bytes, startIndex);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetUint64LittleEndian_42()
        {
            byte[] bytes = { 0x_2A, 0x_00, 0x_00, 0x_00, 0x_00, 0x_00, 0x_00, 0x_00 };
            int startIndex = 0;
            ulong expected = 42;

            ulong actual = ForcedBitConverter.GetUint64LittleEndian(bytes, startIndex);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetUint64LittleEndian_42_withStartIndex()
        {
            byte[] bytes = { byte.MaxValue, byte.MaxValue, 0x_2A, 0x_00, 0x_00, 0x_00, 0x_00, 0x_00, 0x_00, 0x_00, byte.MaxValue };
            int startIndex = 2;
            ulong expected = 42;

            ulong actual = ForcedBitConverter.GetUint64LittleEndian(bytes, startIndex);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetUint64LittleEndian_0xFF0000000000002A()
        {
            byte[] bytes = { 0x_2A, 0x_00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF };
            int startIndex = 0;
            ulong expected = 0xFF0000000000002A;

            ulong actual = ForcedBitConverter.GetUint64LittleEndian(bytes, startIndex);

            Assert.AreEqual(expected, actual);
        }
    }
}
