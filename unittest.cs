
public class NBOUnitTest
{
    static public bool RunTest()
    {

        int[] shortinputs = { 0x0000, 0x0001, 0x0002, 0x007f, 0x0080, 0x0081, 0x1234, 0x7fff, 0x8000, 0x8001, 0x8002, 0xfedc, 0xffee, 0xffff };
        int[] shortoutputs = { 0x0000, 0x0100, 0x0200, 0x7f00, 0x8000, 0x8100, 0x3412, 0xff7f, 0x0080, 0x0180, 0x0280, 0xdcfe, 0xeeff, 0xffff };

        long[] intinputs = { 0x00000000, 0x00000001, 0x00000002, 0x12345678, 0x87654321, 0x99887766, 0xfffffffe, 0xffffffff };
        long[] intoutputs = { 0x00000000, 0x01000000, 0x02000000, 0x78563412, 0x21436587, 0x66778899, 0xfeffffff, 0xffffffff };

        ulong[] longinputs = { 0, 0x0000000000000001, 0x0000000000000002, 0x1234567812345678, 0x8765432187654321, 0xfffffffffffffffe, 0xffffffffffffffff };
        ulong[] longoutputs = { 0, 0x0100000000000000, 0x0200000000000000, 0x7856341278563412, 0x2143658721436587, 0xfeffffffffffffff, 0xffffffffffffffff };


        for (int i = 0; i < shortinputs.Length; i++)
        {
            short input = (short)shortinputs[i];
            short expected = (short)shortoutputs[i];
            short actual = NBO.ConvertShort(input);

            if (actual != expected)
            {
                return false;
            }

            ushort uinput = (ushort)((short)shortinputs[i]);
            ushort uexpected = (ushort)((short)shortoutputs[i]);
            ushort uactual = NBO.ConvertUShort(uinput);

            if (uactual != uexpected)
            {
                return false;
            }
        }

        for (int i = 0; i < intinputs.Length; i++)
        {
            int input = (int)intinputs[i];
            int expected = (int)intoutputs[i];
            int actual = NBO.ConvertInt(input);

            if (actual != expected)
            {
                return false;
            }

            uint uinput = (uint)((int)intinputs[i]);
            uint uexpected = (uint)((int)intoutputs[i]);
            uint uactual = NBO.ConvertUInt(uinput);

            if (uactual != uexpected)
            {
                return false;
            }
        }

        for (int i = 0; i < longinputs.Length; i++)
        {
            long input = (long)longinputs[i];
            long expected = (long)longoutputs[i];
            long actual = NBO.ConvertLong(input);

            if (actual != expected)
            {
                return false;
            }

            ulong uinput = longinputs[i];
            ulong uexpected = longoutputs[i];
            ulong uactual = NBO.ConvertULong(uinput);

            if (uactual != uexpected)
            {
                return false;
            }
        }

        return true;
    }

};

