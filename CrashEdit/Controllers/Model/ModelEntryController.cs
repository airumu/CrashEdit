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
            AddMenuSeparator();
            AddMenu("Decompress Model", "Container", Menu_Decompress);
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new ModelBox(this);
        }

        private void Menu_Decompress()
        {
            ModelEntry newModel = ModelEntry.DecompressModel(ModelEntry);
            FileUtil.SaveFile(newModel.Save(), FileFilters.NSEntry, FileFilters.Any);
        }

        public ModelEntry ModelEntry { get; }
    }
}
