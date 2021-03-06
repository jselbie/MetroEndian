MetroEndian
===========
It doesn't appear that .NET on Windows RT (Windows 8 Metro) provides an implementation of IPAddress.HostToNetworkOrder or IPAddress.NetworkToHostOrder for doing conversions between Big and Little Endian numbers.  Useful for socket programming.

So I just put together this simple class called NBO (Network Byte Order).  Just call:

     x = NBO.Convert(x);

Or you can call one of the type specific flavors of the Convert method:

     static public short ConvertShort(short x);
     static public int ConvertInt(int x);
     static public long ConvertLong(long x);
     static public ushort ConvertUShort(ushort x);
     static public uint ConvertUInt(uint x);
     static public ulong ConvertULong(ulong x);

You'll likely notice that I didn't provide seperate functions for "Host To Network" and "Network To Host" operations. That is because, on all modern platforms (big or little endian), these functions are equivalent.

Note: Should you ever wind up using this code on a Big Endian architecture (should one ever exist), the Convert method becomes a no-op and just returns the value passed in.

Enjoy.
