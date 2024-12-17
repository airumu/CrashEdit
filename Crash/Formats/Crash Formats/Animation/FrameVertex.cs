namespace CrashEdit.Crash
{
    public readonly struct FrameVertex
    {
        public static FrameVertex Load(byte[] data)
        {
            ArgumentNullException.ThrowIfNull(data);
            if (data.Length != 6)
                throw new ArgumentException("Value must be 3 bytes long.", nameof(data));
            byte x = data[0];
            byte y = data[1];
            byte z = data[2];
            sbyte normalx = (sbyte)data[3];
            sbyte normaly = (sbyte)data[4];
            sbyte normalz = (sbyte)data[5];
            return new FrameVertex(x, y, z);
        }

        public FrameVertex(byte x, byte y, byte z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public byte X { get; }
        public byte Y { get; }
        public byte Z { get; }

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
