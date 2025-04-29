using System.Windows.Forms;

namespace CrashEdit
{

    public sealed class ByteArrayHexEditor : Editor
    {

        public override string Text => "Hex";

        public override bool ApplicableForSubject(Controller subj)
        {
            ArgumentNullException.ThrowIfNull(subj);

            return subj.Resource is byte[];
        }

        protected override Control MakeControl()
        {
            return new HexView(this, (byte[])Subject.Resource, HexView_DataChangeHandler);
        }

        private bool HexView_DataChangeHandler(int destOffset, int destLength, byte[] source)
        {
            var data = (byte[])Subject.Resource;

            if (destLength != source.Length)
                throw new ArgumentException();
            if (destOffset < 0 || destOffset >= data.Length)
                throw new ArgumentException();

            Array.Copy(source, 0, data, destOffset, destLength);
            return true;
        }

        public void ReplaceData(byte[] source)
        {
            var newRes = source;
            Subject.ParentGroup.Replace(Subject, newRes);
            Subject.UpdateResource(newRes);
        }

        public override void Sync()
        {
            ((HexView)Control).Invalidate();
        }

    }

}
