namespace CrashEdit.Crash
{
    public sealed class VertexGroup3to2(bool lerp, short frames, int eid, int index) : GOOLFrameGroupWithChunk(eid)
    {
        public override short Type() => 1;

        public static VertexGroup3to2 Load(byte[] data, ref int index)
        {
            if (BitConv.FromInt16(data, index) != 1)
            {
                ErrorManager.SignalError("Vertex frame group version is wrong");
            }
            int idx = index;
            index += 2;
            
            short frames = BitConv.FromInt16(data, index);
            index += 2;

            int eid = BitConv.FromInt32(data, index);
            index += 4;

            int lerp = BitConv.FromInt32(data, index);
            index += 4;

            index += 8;

            return new VertexGroup3to2(lerp != 0, frames, eid, idx);
        }

        public short FrameCount { get; set; } = frames;
        public bool Interpolated { get; set; } = lerp;
        public int Index { get; set; } = index;

        public override byte[] Save()
        {
            byte[] data = new byte[20];
            BitConv.ToInt16(data, 0, Type());
            BitConv.ToInt16(data, 2, FrameCount);
            BitConv.ToInt32(data, 4, EID);
            BitConv.ToInt32(data, 8, Interpolated ? 1 : 0);
            BitConv.ToInt32(data, 12, 0);
            return data;
        }
    }
}
