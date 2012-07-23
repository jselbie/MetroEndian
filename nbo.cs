using System;

interface EndianConverter
{
    short ConvertShort(short x);
    int ConvertInt(int x);
    long ConvertLong(long x);
}

class ForBigEndian : EndianConverter
{
    public short ConvertShort(short x) { return x; }
    public int ConvertInt(int x) { return x; }
    public long ConvertLong(long x) { return x; }
}


class ForLittleEndian : EndianConverter
{
    public short ConvertShort(short x)
    {
        int x1 = (x >> 8) & 0x00ff;
        int x2 = x  & 0x00ff;
        int result = x1 | (x2 << 8);
        return (short)result;
    }

    public int ConvertInt(int x)
    {
        int x1 = (x >> 24) & 0x00ff;
        int x2 = (x >> 16) & 0x00ff;
        int x3 = (x >> 8) & 0x00ff;
        int x4 = x & 0x00ff;
        int result = x1 | (x2 << 8) | (x3 << 16) | (x4 << 24);
        return result;
    }

    public long ConvertLong(long x)
    {
        long x1 = (x >> 56) & 0x00ff;
        long x2 = (x >> 48) & 0x00ff;
        long x3 = (x >> 40) & 0x00ff;
        long x4 = (x >> 32) & 0x00ff;
        long x5 = (x >> 24) & 0x00ff;
        long x6 = (x >> 16) & 0x00ff;
        long x7 = (x >> 8) & 0x00ff;
        long x8 = x & 0x00ff;
        long result = x1 | (x2 << 8) | (x3 << 16) | (x4 << 24) | (x5 << 32) | (x6 << 40) | (x7 << 48) | (x8 << 56);
        return result;
    }
}




public static class NBO
{
    static public bool IsLittleEndian { get; private set; }
    static EndianConverter _converter;


#if NBO_USE_SELF_DETECT
    static void SelfDetectLittleEndian()
    {
        System.IO.MemoryStream stream = new MemoryStream();
        System.IO.BinaryWriter writer = new BinaryWriter(stream, new UTF8Encoding(), true);

        uint value = 0x01020304;
        int firstbyte = 0;
        writer.Write(value);
        writer.Dispose();
        stream.Seek(0, SeekOrigin.Begin);
        firstbyte = stream.ReadByte();
        IsLittleEndian = (firstbyte == 0x04);
        stream.Dispose();
    }
#endif


    static NBO()
    {
#if NBO_USE_SELF_DETECT
        SelfDetectLittleEndian();
#else
        IsLittleEndian = BitConverter.IsLittleEndian;
#endif

        if (IsLittleEndian)
        {
            _converter = new ForLittleEndian();
        }
        else
        {
            _converter = new ForBigEndian();
        }

    }

    static public short ConvertShort(short x) { return _converter.ConvertShort(x); }
    static public int ConvertInt(int x) { return _converter.ConvertInt(x); }
    static public long ConvertLong(long x) { return _converter.ConvertLong(x); }

    static public ushort ConvertUShort(ushort x) { return (ushort)_converter.ConvertShort((short)x); }
    static public uint ConvertUInt(uint x) { return (uint)_converter.ConvertInt((int)x); }
    static public ulong ConvertULong(ulong x) { return (ulong)_converter.ConvertLong((long)x); }

    // overloaded versions. I really despise overloaded methods in general, because you can mistakenly call the wrong version without realizing it
    // but someone might prefer using this class this way
    static public short Convert(short x) { return _converter.ConvertShort(x); }
    static public int Convert(int x) { return _converter.ConvertInt(x); }
    static public long Convert(long x) { return _converter.ConvertLong(x); }

    static public ushort Convert(ushort x) { return (ushort)_converter.ConvertShort((short)x); }
    static public uint Convert(uint x) { return (uint)_converter.ConvertInt((int)x); }
    static public ulong Convert(ulong x) { return (ulong)_converter.ConvertLong((long)x); }
};


