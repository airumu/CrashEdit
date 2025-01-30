namespace CrashEdit.Crash
{
    public sealed class OldModelEntry : Entry
    {
        private List<OldModelPolygon> polygons;
        private List<OldModelStruct> structs;

        public OldModelEntry(byte[] info, IEnumerable<OldModelPolygon> polygons, IEnumerable<OldModelStruct> structs, int eid) : base(eid)
        {
            ArgumentNullException.ThrowIfNull(polygons);
            Info = info ?? throw new ArgumentNullException(nameof(info));
            this.polygons = new List<OldModelPolygon>(polygons);
            this.structs = new List<OldModelStruct>(structs);
        }

        public override string Title => $"Old Model ({EName})";
        public override string ImageKey => "ThingCrimson";

        public override int Type => 2;
        public byte[] Info { get; set; }
        public IList<OldModelPolygon> Polygons => polygons;
        public IList<OldModelStruct> Structs => structs;

        public int PolygonsCount
        {
            get => BitConv.FromInt32(Info, 0);
            set => BitConv.ToInt32(Info, 0, value);
        }
        public int ScaleX
        {
            get => BitConv.FromInt32(Info, 4);
            set => BitConv.ToInt32(Info, 4, value);
        }
        public int ScaleY
        {
            get => BitConv.FromInt32(Info, 8);
            set => BitConv.ToInt32(Info, 8, value);
        }
        public int ScaleZ
        {
            get => BitConv.FromInt32(Info, 12);
            set => BitConv.ToInt32(Info, 12, value);
        }

        public override UnprocessedEntry Unprocess()
        {
            byte[][] items = new byte[2][];
            items[0] = Info;
            items[1] = new byte[polygons.Count * 8];
            for (int i = 0; i < polygons.Count; i++)
            {
                polygons[i].Save().CopyTo(items[1], i * 8);
            }
            return new UnprocessedEntry(items, EID, Type);
        }
    }
}
