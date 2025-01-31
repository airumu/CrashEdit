namespace CrashEdit.Crash
{
    public class OldSceneryColor : OldModelStruct
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public bool N { get; set; }

        public static OldSceneryColor Load(byte[] data)
        {
            byte r = data[0];
            byte g = data[1];
            byte b = data[2];
            bool n = (data[3] & 0x10) != 0;
            return new OldSceneryColor(r, g, b, n);
        }

        public OldSceneryColor(byte r, byte g, byte b, bool n)
        {
            R = r;
            G = g;
            B = b;
            N = n;
        }

        public byte[] Save()
        {
            byte[] result = new byte[4];
            result[0] = R;
            result[1] = G;
            result[2] = B;
            result[3] = (byte)(0x60 | (Convert.ToByte(N) << 4));
            return result;
        }
    }
}
