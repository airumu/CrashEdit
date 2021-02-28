using System;

namespace Crash
{
    public struct FrameVertex : IPosition
    {
        public static FrameVertex Load(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (data.Length != 3)
                throw new ArgumentException("Value must be 3 bytes long.", "data");
            byte x = data[0];
            byte y = data[1];
            byte z = data[2];
            return new FrameVertex(x, y, z);
        }

        public FrameVertex(byte x,byte y,byte z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public byte X { get; }
        public byte Y { get; }
        public byte Z { get; }

        double IPosition.X => X;
        double IPosition.Y => Y;
        double IPosition.Z => Z;

        public byte[] Save()
        {
            byte[] data = new byte[3];
            data[0] = X;
            data[1] = Y;
            data[2] = Z;
            return data;
        }
    }
}
