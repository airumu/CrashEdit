using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(T21Entry))]
    public sealed class T21EntryController : EntryController
    {
        public T21EntryController(T21Entry t21entry, SubcontrollerGroup parentGroup) : base(t21entry, parentGroup)
        {
            T21Entry = t21entry;
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new T21EntryBox(this);
        }

        public T21Entry T21Entry { get; }
    }
}
