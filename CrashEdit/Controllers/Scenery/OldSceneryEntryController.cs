using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(OldSceneryEntry))]
    public sealed class OldSceneryEntryController : EntryController
    {
        public OldSceneryEntryController(OldSceneryEntry oldsceneryentry, SubcontrollerGroup parentGroup) : base(oldsceneryentry, parentGroup)
        {
            OldSceneryEntry = oldsceneryentry;
            AddMenuSeparator();
            AddMenu("Export as OBJ", Menu_Export_OBJ);
            AddMenu("Export as COLLADA", Menu_Export_COLLADA);
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new OldSceneryEntryViewer(GetNSF(), Entry.EID);
        }

        public OldSceneryEntry OldSceneryEntry { get; }

        private void Menu_Export_OBJ()
        {
            if (DarkMessageBox.ShowWarning(Resources.Scenery_ExportOBJ, Resources.Scenery_ExportOBJ_Title, DarkDialogButton.YesNo) != DialogResult.Yes)
            {
                return;
            }
            FileUtil.SaveFile(OldSceneryEntry.ToOBJ(), FileFilters.OBJ, FileFilters.Any);
        }

        private void Menu_Export_COLLADA()
        {
            if (DarkMessageBox.ShowWarning(Resources.Scenery_ExportCOLLADA, Resources.Scenery_ExportCOLLADA_Title, DarkDialogButton.YesNo) != DialogResult.Yes)
            {
                return;
            }
            FileUtil.SaveFile(OldSceneryEntry.ToCOLLADA(), FileFilters.COLLADA, FileFilters.Any);
        }
    }
}
