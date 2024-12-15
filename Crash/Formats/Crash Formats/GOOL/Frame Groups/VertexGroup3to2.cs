namespace CrashEdit.Crash
{
    public sealed class VertexGroup3to2(bool lerp, short frames, int eid, bool is3to2) : GOOLFrameGroupWithChunk(eid)
    {
        public override short Type() => 1;

        public static VertexGroup3to2 Load(byte[] data, ref int index)
        {
            if (BitConv.FromInt16(data, index) != 1)
            {
                ErrorManager.SignalError("Vertex frame group version is wrong");
            }
            index += 2;
            
            short frames = BitConv.FromInt16(data, index);
            index += 2;

            int eid = BitConv.FromInt32(data, index);
            index += 4;

            int lerp = BitConv.FromInt32(data, index);
            index += 2;

            int is3to2 = BitConv.FromInt32(data, index);
            index += 2;

            index += 8;

            return new VertexGroup3to2(lerp != 0, frames, eid, is3to2 != 0);
        }

        public short FrameCount { get; set; } = frames;
        public bool Interpolated { get; set; } = lerp;
        public bool Is3to2 { get; set; } = is3to2;

        public override byte[] Save()
        {
            byte[] data = new byte[20];
            BitConv.ToInt16(data, 0, Type());
            BitConv.ToInt16(data, 2, FrameCount);
            BitConv.ToInt32(data, 4, EID);
            BitConv.ToInt32(data, 8, Interpolated ? 1 : 0);
            BitConv.ToInt32(data, 10, Is3to2 ? 1 : 0);
            return data;
        }
    }
}
