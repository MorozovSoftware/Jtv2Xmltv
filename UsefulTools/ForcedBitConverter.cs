
namespace UsefulTools
{
    public class ForcedBitConverter
    {
        public static ushort GetUshortLittleEndian(byte[] value, int startindex)
        {
            return (ushort)((value[startindex]) | ((value[startindex + 1]) << 8));
        }

        public static ulong GetUint64LittleEndian(byte[] value, int startindex)
        {
            return (value[startindex]) |
                    ((ulong)value[startindex + 1]) << 8 |
                    ((ulong)value[startindex + 2]) << 16 |
                    ((ulong)value[startindex + 3]) << 24 |
                    ((ulong)value[startindex + 4]) << 32 |
                    ((ulong)value[startindex + 5]) << 40 |
                    ((ulong)value[startindex + 6]) << 48 |
                    ((ulong)value[startindex + 7]) << 56;
        }
    }
}
