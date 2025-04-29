namespace CrashEdit.Crash
{
    public struct SceneryColor
    {
        public SceneryColor(byte red, byte green, byte blue) : this(red, green, blue, 0)
        {
        }

        public SceneryColor(byte red, byte green, byte blue, byte extra)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Extra = extra;
        }

        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public byte Extra { get; set; }
    }
}
