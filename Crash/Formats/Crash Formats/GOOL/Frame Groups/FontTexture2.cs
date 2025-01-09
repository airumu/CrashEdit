namespace CrashEdit.Crash
{
    public class FontTexture2 : SpriteTexture2
    {
        public short Width { get; set; }
        public short Height { get; set; }

        public FontTexture2(int packed1, int packed2, int packed3, int packed4, short width, short height)
            : base(packed1, packed2, packed3, packed4)
        {
            Width = width; 
            Height = height;
        }
    }

}
