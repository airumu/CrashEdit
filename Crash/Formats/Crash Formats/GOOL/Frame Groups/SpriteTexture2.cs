namespace CrashEdit.Crash
{
    public class SpriteTexture2
    {
        public static SpriteTexture2 Load(int packed1, int packed2, int packed3, int packed4)
        {
            int r = (packed1) & 0xff;
            int g = (packed1 >> 8) & 0xff;
            int b = (packed1 >> 16) & 0xff;
            int unk1 = (packed1 >> 24) & 0x1;
            int blendMode = (packed1 >> 25) & 0x1;
            int primType = (packed1 >> 26) & 0x3f;
            int u1 = (packed2) & 0xff;
            int v1 = (packed2 >> 8) & 0xff;
            int clutX = (packed2 >> 16) & 0xf;
            int unk2 = (packed2 >> 20) & 0x3;
            int clutY = (packed2 >> 22) & 0x7f;
            int unk3 = (packed2 >> 29) & 0x7;
            int u2 = (packed3) & 0xff;
            int v2 = (packed3 >> 8) & 0xff;
            int segment = (packed3 >> 16) & 0x3;
            int unk4 = (packed3 >> 18) & 0x7;
            int additive = (packed3 >> 21) & 0x1;
            int unk5 = (packed3 >> 22) & 0x1;
            int colorMode = (packed3 >> 23) & 0x3;
            int unk6 = (packed3 >> 25) & 0x7f;
            int u3 = (packed4) & 0xff;
            int v3 = (packed4 >> 8) & 0xff;
            int u4 = (packed4 >> 16) & 0xff;
            int v4 = (packed4 >> 24) & 0xff;

            return new SpriteTexture2(r, g, b, unk1, blendMode, primType, u1, v1, clutX, unk2, clutY, unk3, u2, v2, segment, unk4, additive, unk5, colorMode, unk6, u3, v3, u4, v4);
        }

        public SpriteTexture2(int r, int g, int b, int unk1, int blendMode, int primType, int u1, int v1, int clutX, int unk2, int clutY, int unk3, int u2, int v2, int segment, int unk4, int additive, int unk5, int colorMode, int unk6, int u3, int v3, int u4, int v4)
        {
            R = r;
            G = g;
            B = b;
            Unk1 = unk1;
            BlendMode = blendMode;
            PrimType = primType;
            U1 = u1;
            V1 = v1;
            ClutX = clutX;
            Unk2 = unk2;
            ClutY = clutY;
            Unk3 = unk3;
            U2 = u2;
            V2 = v2;
            Segment = segment;
            Unk4 = unk4;
            Additive = additive;
            Unk5 = unk5;
            ColorMode = colorMode;
            Unk6 = unk6;
            U3 = u3;
            V3 = v3;
            U4 = u4;
            V4 = v4;

            int xoff = (1 << (2 - ColorMode)) * 64 * Segment;
            Left = Math.Min(U1, Math.Min(U2, U3)) + xoff;
            Top = Math.Min(V1, Math.Min(V2, V3));
            Width = Math.Max(U1, Math.Max(U2, U3)) + xoff - Left;
            Width++; // fake value
            Height = Math.Max(V1, Math.Max(V2, V3)) - Top;
            Height++; // fake value
            int tx1 = U1 + xoff;
            int tx2 = U2 + xoff;
            int tx3 = U3 + xoff;
            int tx4 = U4 + xoff;
            if (tx1 > tx2 || tx1 > tx3) ++tx1;
            if (tx2 > tx1 || tx2 > tx3) ++tx2;
            if (tx3 > tx2 || tx3 > tx1) ++tx3;
            if (tx4 > tx2 || tx4 > tx3 || tx4 > tx1) ++tx4;
            X1 = tx1;
            X2 = tx2;
            X3 = tx3;
            X4 = tx4;
            int ty1 = V1;
            int ty2 = V2;
            int ty3 = V3;
            int ty4 = V4;
            if (ty1 > ty2 || ty1 > ty3) ++ty1;
            if (ty2 > ty1 || ty2 > ty3) ++ty2;
            if (ty3 > ty2 || ty3 > ty1) ++ty3;
            if (ty4 > ty2 || ty4 > ty3 || ty4 > ty1) ++ty4;
            Y1 = ty1;
            Y2 = ty2;
            Y3 = ty3;
            Y4 = ty4;
        }

        public SpriteTexture2(int packed1, int packed2, int packed3, int packed4)
        {
            PackedValue1 = packed1;
            PackedValue2 = packed2;
            PackedValue3 = packed3;
            PackedValue4 = packed4;
        }

        public int PackedValue1 { get; set; }
        public int PackedValue2 { get; set; }
        public int PackedValue3 { get; set; }
        public int PackedValue4 { get; set; }

        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public int Unk1 { get; set; }
        public int BlendMode { get; set; }
        public int PrimType { get; set; }
        public int U1 { get; set; }
        public int V1 { get; set; }
        public int ClutX { get; set; }
        public int Unk2 { get; set; }
        public int ClutY { get; set; }
        public int Unk3 { get; set; }
        public int U2 { get; set; }
        public int V2 { get; set; }
        public int Segment { get; set; }
        public int Unk4 { get; set; }
        public int Additive { get; set; }
        public int Unk5 { get; set; }
        public int ColorMode { get; set; }
        public int Unk6 { get; set; }
        public int U3 { get; set; }
        public int V3 { get; set; }
        public int U4 { get; set; }
        public int V4 { get; set; }

        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float X1 { get; set; }
        public float X2 { get; set; }
        public float X3 { get; set; }
        public float X4 { get; set; }
        public float Y1 { get; set; }
        public float Y2 { get; set; }
        public float Y3 { get; set; }
        public float Y4 { get; set; }
    }
}
