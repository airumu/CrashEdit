namespace CrashEdit.Crash
{
    public class SpriteTexture
    {
        public static SpriteTexture Load(int packed1, int packed2)
        {
            int r = (packed1) & 0xff;
            int g = (packed1 >> 8) & 0xff;
            int b = (packed1 >> 16) & 0xff;
            int clutX = (packed1 >> 24) & 0xf;
            int unk1 = (packed1 >> 28) & 0x1;
            int blendMode = (packed1 >> 29) & 0x3;
            int textured = (packed1 >> 31) & 0x1;
            int y = (packed2) & 0x1f;
            int unk2 = (packed2 >> 5) & 0x1;
            int clutY = (packed2 >> 6) & 0x7f;
            int x = (packed2 >> 13) & 0x1f;
            int segment = (packed2 >> 18) & 0x3;
            int colorMode = (packed2 >> 20) & 0x3;
            int uv = (packed2 >> 22) & 0x3ff;

            return new SpriteTexture(r, g, b, clutX, unk1, blendMode, textured, y, unk2, clutY, x, segment, colorMode, uv);
        }

        public SpriteTexture(int r, int g, int b, int clutX, int unk1, int blendMode, int textured, int y, int unk2, int clutY, int x, int segment, int colorMode, int uv)
        {
            R = r;
            G = g;
            B = b;
            ClutX = clutX;
            Unk1 = unk1;
            BlendMode = blendMode;
            Textured = textured;
            Y = y;
            Unk2 = unk2;
            ClutY = clutY;
            X = x;
            Segment = segment;
            ColorMode = colorMode;
            UV = uv;

        }

        public SpriteTexture(int packed1, int packed2)
        {
            PackedValue1 = packed1;
            PackedValue2 = packed2;
        }

        public int PackedValue1 { get; set; }
        public int PackedValue2 { get; set; }

        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public int ClutX { get; set; }
        public int Unk1 { get; set; }
        public int BlendMode { get; set; }
        public int Textured { get; set; }
        public int Y { get; set; }
        public int Unk2 { get; set; }
        public int ClutY { get; set; }
        public int X { get; set; }
        public int Segment { get; set; }
        public int ColorMode { get; set; }
        public int UV { get; set; }

        public byte[] Save()
        {
            byte[] result = new byte[8];
            PackedValue1 =
                (R & 0xff) |
                ((G & 0xff) << 8) |
                ((B & 0xff) << 16) |
                ((ClutX & 0xf) << 24) |
                ((Unk1 & 0x1) << 28) |
                ((BlendMode & 0x3) << 29) |
                ((Textured & 0x1) << 31);
            PackedValue2 =
                (Y & 0x1f) |
                ((Unk2 & 0x1) << 5) |
                ((ClutY & 0x7f) << 6) |
                ((X & 0x1f) << 13) |
                ((Segment & 0x3) << 18) |
                ((ColorMode & 0x3) << 20) |
                ((UV & 0x3ff) << 22);
            BitConv.ToInt32(result, 0, PackedValue1);
            BitConv.ToInt32(result, 4, PackedValue2);
            return result;
        }
    }
}
