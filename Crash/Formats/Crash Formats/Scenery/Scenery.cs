namespace CrashEdit.Crash
{
    public sealed class Scenery : IResource
    {
        public static Scenery Load(byte[][] data)
        {
            return new Scenery(data);
        }

        public Scenery(byte[][] data)
        {
            Data = data;
        }

        public string Title => "Scenery";

        public string ImageKey => "Arrow";

        public byte[][] Data { get; }
    }
}
