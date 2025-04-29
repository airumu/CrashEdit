namespace CrashEdit.Crash
{
    public class ModelTransformedTriangle
    {
        public ModelTransformedTriangle(int v1, int v2, int v3, int c1, int c2, int c3, int tex, int type, int subtype, bool animated)
        {
            Vertex = new int[3] { v1, v2, v3 };
            Color = new int[3] { c1, c2, c3 };
            Texture = tex;
            Type = type;
            Subtype = subtype;
            Animated = animated;
        }

        public int[] Vertex { get; set; }
        public int[] Color { get; set; }
        public int Texture { get; set; }
        public int Type { get; set; }
        public int Subtype { get; set; }
        public bool Animated { get; set; }
    }
}
