namespace CrashEdit.Crash
{
    public sealed class ModelEntry : Entry
    {
        private List<ModelTransformedTriangle> triangles;
        private List<SceneryColor> colors;
        private List<ModelTexture> textures;
        private List<ModelExtendedTexture> animatedtextures;
        private List<ModelPosition> positions;

        public ModelEntry(byte[] info, uint[] polygons, IEnumerable<SceneryColor> colors, IEnumerable<ModelTexture> textures, IEnumerable<ModelExtendedTexture> animatedtextures, IEnumerable<ModelPosition> positions, int eid) : base(eid)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
            PolyData = polygons ?? throw new ArgumentNullException(nameof(polygons));
            this.colors = new List<SceneryColor>(colors);
            this.textures = new List<ModelTexture>(textures);
            this.animatedtextures = new List<ModelExtendedTexture>(animatedtextures);
            if (positions != null)
                this.positions = new List<ModelPosition>(positions);
            else
                this.positions = null;
            ConvertIndices();
        }

        private void ConvertIndices()
        {
            triangles = new List<ModelTransformedTriangle>();
            Dictionary<byte, int> pos = new Dictionary<byte, int>();
            int vert = positions == null ? 0 : -BitConv.FromInt32(Info, 0x4C); // special vertex count, let'str get rid of it for compressed models
            List<int> vtx = new List<int>();
            int lastValidCC = -3; // dirty hack
            int lastCCpos = -1;
            int lastAApos = -1;
            int lastcolor = -1;
            int lastNonBB = -1;
            ModelStruct[] structs = new ModelStruct[PolyData.Length];
            for (int i = 0; i < PolyData.Length; ++i) // pre-pass (ugh)
            {
                ModelStruct str = ConvertPolyItem(PolyData[i]);
                if (str == null) // footer
                    break;
                else if (str is ModelColor col) // color
                    structs[i] = col;
                else if (str is ModelTriangle tri) // index
                {
                    if (tri.Type == ModelTriangle.IndexType.Original)
                    {
                        if (tri.PositionKey != ModelTriangle.NullPtr)
                        {
                            if (pos.ContainsKey(tri.PositionKey))
                                pos[tri.PositionKey] = vert;
                            else
                                pos.Add(tri.PositionKey, vert);
                        }
                        vtx.Add(vert++);
                    }
                    else if (tri.Type == ModelTriangle.IndexType.Duplicate)
                    {
                        vtx.Add(pos[tri.PositionKey]);
                    }
                    else
                        throw new Exception();
                    structs[i] = tri;
                }
                else
                    throw new Exception();
            }
            for (int i = 0, cur_str = 0; i < PolyData.Length; ++i)
            {
                ModelStruct str = structs[i];
                if (str == null) // footer
                    break;
                else if (str is ModelColor col) // color
                {
                    lastcolor = i;
                }
                else if (str is ModelTriangle tri) // index
                {
                    switch (tri.TriangleType)
                    {
                        case 0:
                            lastAApos = cur_str;
                            lastNonBB = i;
                            triangles.Add(new ModelTransformedTriangle(
                                vtx[cur_str],
                                vtx[cur_str - 1],
                                vtx[cur_str - 2],
                                tri.ColorIndex,
                                lastcolor + 1 == i ? ((ModelColor)structs[lastcolor]).Color1 : ((ModelTriangle)structs[i - 1]).ColorIndex,
                                lastcolor + 1 == i ? ((ModelColor)structs[lastcolor]).Color2 : (lastcolor + 2 == i ? ((ModelColor)structs[lastcolor]).Color1 : ((ModelTriangle)structs[i - 2]).ColorIndex),
                                tri.TextureIndex,
                                tri.TriangleType,
                                tri.TriangleSubtype,
                                tri.Animated));
                            break;
                        case 1:
                            int colorIndex = -1;
                            if (lastcolor < lastNonBB - 2) // 3
                                colorIndex = ((ModelTriangle)structs[lastNonBB - 2]).ColorIndex;
                            else if (lastcolor == lastNonBB - 2) // 1
                                colorIndex = ((ModelColor)structs[lastcolor]).Color1;
                            else if (lastcolor == lastNonBB - 1) // 2
                                colorIndex = ((ModelColor)structs[lastcolor]).Color2;
                            else if (lastcolor > lastNonBB - 2) // 4
                                colorIndex = ((ModelColor)structs[lastcolor]).Color2;
                            triangles.Add(new ModelTransformedTriangle(
                                vtx[cur_str],
                                vtx[cur_str - 1],
                                lastCCpos > lastAApos ? vtx[lastCCpos] : vtx[lastAApos - 2],
                                tri.ColorIndex,
                                lastcolor + 1 == i ? ((ModelColor)structs[lastcolor]).Color1 : ((ModelTriangle)structs[i - 1]).ColorIndex,
                                colorIndex,
                                tri.TextureIndex,
                                tri.TriangleType,
                                tri.TriangleSubtype,
                                tri.Animated));
                            break;
                        case 2:
                            if (i + 2 < PolyData.Length && lastValidCC + 2 < i)
                            {
                                lastValidCC = i;
                                triangles.Add(new ModelTransformedTriangle(
                                    vtx[cur_str],
                                    vtx[cur_str + 1],
                                    vtx[cur_str + 2],
                                    tri.ColorIndex,
                                    ((ModelTriangle)structs[i + 1]).ColorIndex,
                                    ((ModelTriangle)structs[i + 2]).ColorIndex,
                                    tri.TextureIndex,
                                    tri.TriangleType,
                                    tri.TriangleSubtype,
                                    tri.Animated));
                            }
                            lastCCpos = cur_str;
                            lastNonBB = i;
                            break;
                    }
                    ++cur_str;
                }
            }
        }

        public static ModelStruct ConvertPolyItem(uint item)
        {
            if (item == 0xFFFFFFFF)
            {
                return null;
            }
            else if ((item & 0xFFFF0000) != 0) // TODO check for a better mask
            {
                return ModelTriangle.Load(item);
            }
            else
            {
                return ModelColor.Load(item);
            }
        }

        public int GetFrameBitCount()
        {
            int result = 0;
            if (Positions != null)
            {
                foreach (var pos in Positions)
                {
                    result += pos.XBits + 1;
                    result += pos.YBits + 1;
                    result += pos.ZBits + 1;
                }
            }
            return result;
        }

        public override string Title =>
            (Positions == null) ?
            $"Model ({EName})" :
            $"Compressed Model ({EName})";

        public override string ImageKey =>
            (Positions == null) ?
            "ThingCrimson" :
            "ThingRed";

        public override int Type => 2;
        public byte[] Info { get; set; }
        public uint[] PolyData { get; set; }
        public IList<ModelTransformedTriangle> Triangles => triangles;
        public IList<SceneryColor> Colors => colors;
        public IList<ModelTexture> Textures => textures;
        public IList<ModelExtendedTexture> AnimatedTextures => animatedtextures;
        public IList<ModelPosition> Positions => positions;

        public int ScaleX
        {
            get => BitConv.FromInt32(Info, 0);
            set => BitConv.ToInt32(Info, 0, value);
        }
        public int ScaleY
        {
            get => BitConv.FromInt32(Info, 4);
            set => BitConv.ToInt32(Info, 4, value);
        }
        public int ScaleZ
        {
            get => BitConv.FromInt32(Info, 8);
            set => BitConv.ToInt32(Info, 8, value);
        }
        public int GetTPAG(int idx) => BitConv.FromInt32(Info, 0xC + 4 * idx);
        public void SetTPAG(int idx, int value) => BitConv.ToInt32(Info, 0xC + 4 * idx, value);
        public int VertexCount => BitConv.FromInt32(Info, 0x38);
        public int TPAGCount
        {
            get => BitConv.FromInt32(Info, 0x40);
            set => BitConv.ToInt32(Info, 0x40, value);
        }
        public int PolyCount => BitConv.FromInt32(Info, 0x44);

        public int AnimatedTextureCount
        {
            get => BitConv.FromInt32(Info, 0x48);
            set => BitConv.ToInt32(Info, 0x48, value);
        }

        public override UnprocessedEntry Unprocess()
        {
            byte itemcount = 5;
            if (Positions != null)
                itemcount = 6;
            byte[][] items = new byte[itemcount][];
            items[0] = Info;
            items[1] = new byte[PolyData.Length * 4];
            for (int i = 0; i < PolyData.Length; i++)
            {
                BitConv.ToInt32(items[1], i * 4, (int)PolyData[i]);
            }
            items[2] = new byte[colors.Count * 4];
            for (int i = 0; i < colors.Count; i++)
            {
                items[2][i * 4] = Colors[i].Red;
                items[2][i * 4 + 1] = Colors[i].Green;
                items[2][i * 4 + 2] = Colors[i].Blue;
                items[2][i * 4 + 3] = Colors[i].Extra;
            }
            items[3] = new byte[textures.Count * 12];
            for (int i = 0; i < textures.Count; i++)
            {
                textures[i].Save().CopyTo(items[3], i * 12);
            }
            items[4] = new byte[animatedtextures.Count * 4];
            for (int i = 0; i < animatedtextures.Count; i++)
            {
                animatedtextures[i].Save().CopyTo(items[4], i * 4);
            }
            if (itemcount == 6)
            {
                items[5] = new byte[positions.Count * 4];
                for (int i = 0; i < positions.Count; i++)
                {
                    positions[i].Save().CopyTo(items[5], i * 4);
                }
            }
            return new UnprocessedEntry(items, EID, Type);
        }
    }
}
