using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(OldModelEntry))]
    public sealed class OldModelEntryController : EntryController
    {
        public OldModelEntryController(OldModelEntry oldmodelentry, SubcontrollerGroup parentGroup) : base(oldmodelentry, parentGroup)
        {
            OldModelEntry = oldmodelentry;
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new OldModelBox(this);
        }

        public OldModelEntry OldModelEntry { get; }
    }
}
