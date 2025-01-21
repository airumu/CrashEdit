namespace CrashEdit.Crash
{
    public sealed class ZoneEntry : Entry
    {
        public ZoneEntry(ZoneHeader zoneheader, byte[] layout, IEnumerable<Entity> entities, int eid) : base(eid)
        {
            Zoneheader  = zoneheader;
            Layout = layout;
            Entities.AddRange(entities);
        }

        public override string Title => $"Zone ({EName})";
        public override string ImageKey => "ThingViolet";

        public override int Type => 7;

        [SubresourceSlot]
        public ZoneHeader Zoneheader { get; set; }

        [SubresourceSlot]
        public byte[] Layout { get; set; }

        [SubresourceList]
        public List<Entity> Entities { get; } = new List<Entity>();

        public int WorldCount
        {
            get => Zoneheader.WorldCount;
            set => Zoneheader.WorldCount = value;
        }

        public int InfoCount
        {
            get => Zoneheader.InfoCount;
            set => Zoneheader.InfoCount = value;
        }

        public int CameraCount
        {
            get => Zoneheader.CameraCount;
            set => Zoneheader.CameraCount = value;
        }

        public int EntityCount
        {
            get => Zoneheader.EntityCount;
            set => Zoneheader.EntityCount = value;
        }

        public int ZoneCount
        {
            get => Zoneheader.ZoneCount;
            set => Zoneheader.ZoneCount = value;
        }

        public int GetLinkedWorld(int idx) => Zoneheader.Worlds[idx];
        public int GetLinkedZone(int idx) => Zoneheader.Zones[idx];

        public int X
        {
            get => BitConv.FromInt32(Layout, 0);
            set => BitConv.ToInt32(Layout, 0, value);
        }
        public int Y
        {
            get => BitConv.FromInt32(Layout, 4);
            set => BitConv.ToInt32(Layout, 4, value);
        }
        public int Z
        {
            get => BitConv.FromInt32(Layout, 8);
            set => BitConv.ToInt32(Layout, 8, value);
        }
        public int Width
        {
            get => BitConv.FromInt32(Layout, 12);
            set => BitConv.ToInt32(Layout, 12, value);
        }
        public int Height
        {
            get => BitConv.FromInt32(Layout, 16);
            set => BitConv.ToInt32(Layout, 0, value);
        }
        public int Depth
        {
            get => BitConv.FromInt32(Layout, 20);
            set => BitConv.ToInt32(Layout, 20, value);
        }

        public ushort CollisionDepthX
        {
            get => BitConv.FromUInt16(Layout, 0x1E);
            set => BitConv.ToInt16(Layout, 0x1E, (short)value);
        }

        public ushort CollisionDepthY
        {
            get => BitConv.FromUInt16(Layout, 0x20);
            set => BitConv.ToInt16(Layout, 0x20, (short)value);
        }

        public ushort CollisionDepthZ
        {
            get => BitConv.FromUInt16(Layout, 0x22);
            set => BitConv.ToInt16(Layout, 0x22, (short)value);
        }
        public override UnprocessedEntry Unprocess()
        {
            byte[][] items = new byte[2 + Entities.Count][];
            items[0] = Zoneheader.Save();
            items[1] = Layout;
            for (int i = 0; i < Entities.Count; i++)
            {
                items[2 + i] = Entities[i].Save();
            }
            return new UnprocessedEntry(items, EID, Type);
        }
    }
}
