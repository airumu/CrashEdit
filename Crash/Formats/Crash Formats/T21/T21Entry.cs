namespace CrashEdit.Crash
{
    public sealed class T21Entry : Entry
    {
        public T21Entry(IEnumerable<byte[]> items, int eid) : base(eid)
        {
            Items.AddRange(items);
        }

        public override string Title => $"T21 ({EName})";
        public override string ImageKey => "Painting";

        public override int Type => 21;

        public List<byte[]> Items { get; } = [];

        public override UnprocessedEntry Unprocess()
        {
            byte[][] items = new byte[Items.Count][];
            for (int i = 0; i < Items.Count; i++)
            {
                items[i] = Items[i];
            }
            return new UnprocessedEntry(items, EID, Type);
        }
    }
}
