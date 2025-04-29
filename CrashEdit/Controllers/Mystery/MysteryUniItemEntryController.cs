using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(MysteryUniItemEntry))]
    public class MysteryUniItemEntryController : EntryController
    {
        public MysteryUniItemEntryController(MysteryUniItemEntry mysteryentry, SubcontrollerGroup parentGroup) : base(mysteryentry, parentGroup)
        {
            MysteryEntry = mysteryentry;
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new HexView(this, MysteryEntry.Data, HexView_DataChangeHandler);
        }

        private bool HexView_DataChangeHandler(int destOffset, int destLength, byte[] source)
        {
            var data = MysteryEntry.Data;

            if (destLength != source.Length)
                throw new ArgumentException();
            if (destOffset < 0 || destOffset >= data.Length)
                throw new ArgumentException();

            Array.Copy(source, 0, data, destOffset, destLength);
            return true;
        }

        public void ReplaceData(byte[] source)
        {
            MysteryEntry.Data = source;
        }

        public MysteryUniItemEntry MysteryEntry { get; }
    }
}
