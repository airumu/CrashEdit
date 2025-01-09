namespace CrashEdit.Crash
{
    public class ImageTexture2 : SpriteTexture2
    {
        public short X1 { get; set; }
        public short Y1 { get; set; }
        public short X2 { get; set; }
        public short Y2 { get; set; }

        public ImageTexture2(int packed1, int packed2, int packed3, int packed4, short x1, short y1, short x2, short y2)
            : base(packed1, packed2, packed3, packed4)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }
    }

}
