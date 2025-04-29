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
        public int StructCount
        {
            get => BitConv.FromInt32(Info, 16);
            set => BitConv.ToInt32(Info, 16, value);
        }

        public override UnprocessedEntry Unprocess()
        {
            byte[][] items = new byte[2][];
            items[0] = new byte[20 + structs.Count * 4];
            BitConv.ToInt32(items[0], 0, PolygonsCount);
            BitConv.ToInt32(items[0], 4, ScaleX);
            BitConv.ToInt32(items[0], 8, ScaleY);
            BitConv.ToInt32(items[0], 12, ScaleZ);
            BitConv.ToInt32(items[0], 16, StructCount);
            int texcount = 0;
            int colcount = 0;
            for (int i = 0; i < structs.Count; i++)
            {
                if (structs[i] is OldModelTexture tex)
                {
                    tex.Save().CopyTo(items[0], 20 + texcount * 12 + colcount * 4);
                    texcount++;
                }
                else if (structs[i] is OldSceneryColor col)
                {
                    col.Save().CopyTo(items[0], 20 + texcount * 12 + colcount * 4);
                    colcount++;
                }
            }
            items[1] = new byte[polygons.Count * 8];
            for (int i = 0; i < polygons.Count; i++)
            {
                polygons[i].Save().CopyTo(items[1], i * 8);
            }
            return new UnprocessedEntry(items, EID, Type);
        }
    }
}
