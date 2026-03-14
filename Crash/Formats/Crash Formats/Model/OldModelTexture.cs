namespace CrashEdit.Crash
{
    public class OldModelTexture : OldModelStruct
    {
        public static OldModelTexture Load(byte[] data)
        {
            ArgumentNullException.ThrowIfNull(data);
            if (data.Length != 12)
                throw new ArgumentException("Value must be 12 bytes long.", nameof(data));
            byte r = data[0];
            byte g = data[1];
            byte b = data[2];
            byte blendmode = (byte)((data[3] >> 5) & 0x3);
            bool n = (data[3] & 0x10) != 0;
            byte clutx = (byte)(data[3] & 0xF);
            int eid = BitConv.FromInt32(data, 4);
            int texinfo = BitConv.FromInt32(data, 8);
            int uvindex = (texinfo >> 22) & 0x3FF;
            byte colormode = (byte)(texinfo >> 20 & 3);
            byte segment = (byte)(texinfo >> 18 & 3);
            byte xoffu = (byte)(texinfo >> 13 & 0x1F);
            byte cluty = (byte)(texinfo >> 6 & 0x7F);
            bool unknown = (texinfo >> 5 & 1) != 0;
            byte yoffu = (byte)(texinfo & 0x1F);
            return new OldModelTexture(uvindex, clutx, cluty, xoffu, yoffu, colormode, blendmode, segment, r, g, b, n, eid, unknown);
        }
        public OldModelTexture(int uvindex, byte clutx, byte cluty, byte xoffu, byte yoffu, byte colormode, byte blendmode, byte segment, byte r, byte g, byte b, bool n, int eid, bool unknown)
        {
            UVIndex = uvindex;
            ClutX = clutx;
            ClutY = cluty;
            XOffU = xoffu;
            YOffU = yoffu;
            Segment = segment;
            BlendMode = blendmode;
            ColorMode = colormode;
            R = r;
            G = g;
            B = b;
            N = n;
            EID = eid;
            Unknown = unknown;

            int w = 4 << (UVIndex % 5);
            int h = 4 << ((UVIndex / 5) % 5);
            int xoff = ((64 << (2 - ColorMode)) * Segment) + ((2 << (2 - ColorMode)) * XOffU);
            int yoff = YOffU * 4;
            int winding = UVIndex / 25;
            U1 = w * ((0x30FF0C >> winding) & 1) + xoff;
            U2 = w * ((0x8799E1 >> winding) & 1) + xoff;
            U3 = w * ((0x4B66D2 >> winding) & 1) + xoff;
            V1 = h * ((0xF3CC30 >> winding) & 1) + yoff;
            V2 = h * ((0x9E7186 >> winding) & 1) + yoff;
            V3 = h * ((0x6DB249 >> winding) & 1) + yoff;

            Left = Math.Min(U1, Math.Min(U2, U3));
            Top = Math.Min(V1, Math.Min(V2, V3));
            Width = Math.Max(U1, Math.Max(U2, U3)) - Left;
            Height = Math.Max(V1, Math.Max(V2, V3)) - Top;
        }

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public bool N { get; set; }

        public int EID { get; set; }

        public byte ColorMode { get; set; }
        public int UVIndex { get; set; }
        public byte ClutX { get; set; } // 16-color (32-byte) segments
        public byte ClutY { get; set; }
        public byte XOffU { get; set; }
        public byte YOffU { get; set; }
        public byte BlendMode { get; set; }
        public byte Segment { get; set; }
        public bool Unknown { get; set; }
        public int U1 { get; set; }
        public int V1 { get; set; }
        public int U2 { get; set; }
        public int V2 { get; set; }
        public int U3 { get; set; }
        public int V3 { get; set; }

        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public byte[] Save()
        {
            byte[] result = new byte[12];
            result[0] = R;
            result[1] = G;
            result[2] = B;
            result[3] = (byte)(0x80 | (BlendMode << 5) | (Convert.ToByte(N) << 4) | ClutX);
            BitConv.ToInt32(result, 4, EID);
            uint texinfo = (uint)(Unknown ? 0x20 : 0) | ((uint)UVIndex << 22) | ((uint)ColorMode << 20) | ((uint)Segment << 18) | ((uint)XOffU << 13) | ((uint)ClutY << 6) | YOffU;
            BitConv.ToInt32(result, 8, (int)texinfo);
            return result;
        }
    }

}
