using System.Text;

namespace CrashEdit.Crash
{
    public sealed class TextGroup : GOOLFrameGroup<string>
    {
        public override short Type() => 4;

        public static TextGroup Load(byte[] data, ref int index)
        {
            if (BitConv.FromInt16(data, index) != 4)
            {
                ErrorManager.SignalError("Text frame group version is wrong");
            }
            int idx = index;
            index += 2;

            short framecount = BitConv.FromInt16(data, index);
            index += 2;

            int eid = BitConv.FromInt32(data, index);
            index += 4;

            int font = BitConv.FromInt32(data, index);
            index += 4;

            List<string> frames = new();
            for (int i = 0; i < framecount; ++i)
            {
                int end = Array.IndexOf(data, (byte)0, index);
                frames.Add(Encoding.UTF8.GetString(data, index, end - index));
                index = end + 1;
            }

            Aligner.Align(ref index, 4);

            return new TextGroup(frames, eid, font, idx);
        }

        public TextGroup(List<string> frames, int eid, int font, int index) : base(frames, eid)
        {
            Frames = frames;
            Font = font;
            Index = index;
        }

        public List<string> Frames { get; }
        public int Font { get; set; }
        public int Index { get; set; }

        public override byte[] Save()
        {
            byte[] data = new byte[12];
            BitConv.ToInt16(data, 0, Type());
            BitConv.ToInt16(data, 2, FrameCount);
            BitConv.ToInt32(data, 4, EID);
            BitConv.ToInt32(data, 8, Font);
            for (int i = 0; i < FrameCount; ++i)
            {
                // lazy way to do this, but it's not like crash has enormous amounts of text.
                var s = Encoding.UTF8.GetBytes(Frames[i]);
                Array.Resize(ref data, data.Length + s.Length + 1);
                Array.Copy(s, 0, data, data.Length - s.Length - 1, s.Length);
            }
            Array.Resize(ref data, Aligner.Align(data.Length, 4));
            return data;
        }
    }
}
