using CrashEdit.CE.Controls;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(ModelEntry))]
    public sealed class ModelEntryController : EntryController
    {
        public ModelEntryController(ModelEntry modelentry, SubcontrollerGroup parentGroup) : base(modelentry, parentGroup)
        {
            ModelEntry = modelentry;
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new ModelBox(this);
        }

        public ModelEntry ModelEntry { get; }
    }
}
