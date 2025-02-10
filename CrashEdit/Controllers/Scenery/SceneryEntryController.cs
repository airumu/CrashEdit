using AltUI.Forms;
using CrashEdit.CE.Controls;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using MetroSet_UI.Controls;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(SceneryEntry))]
    public sealed class SceneryEntryController : EntryController
    {
        public SceneryEntryController(SceneryEntry sceneryentry, SubcontrollerGroup parentGroup) : base(sceneryentry, parentGroup)
        {
            SceneryEntry = sceneryentry;
            AddMenuSeparator();
            AddMenu("Export as Wavefront OBJ", Menu_Export_OBJ);
            AddMenu("Export as Stanford PLY", Menu_Export_PLY);
            //AddMenu("Export as COLLADA",Menu_Export_COLLADA);
            AddMenuSeparator();
            AddMenu("Fix coords imported from Crash 3", "Calculator", Menu_Fix_WGEOv3);
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new SceneryEntryViewer(GetNSF(), Entry.EID);
        }

        public SceneryEntry SceneryEntry { get; }

        private void Menu_Export_OBJ()
        {
            if (DarkMessageBox.ShowWarning(Resources.Scenery_ExportOBJ, Resources.Scenery_ExportOBJ_Title, DarkDialogButton.YesNo) != DialogResult.Yes)
            {
                return;
            }
            FileUtil.SaveFile(SceneryEntry.ToOBJ(), FileFilters.OBJ, FileFilters.Any);
        }

        private void Menu_Export_PLY()
        {
            if (DarkMessageBox.ShowWarning(Resources.Scenery_ExportPLY, Resources.Scenery_ExportPLY_Title, DarkDialogButton.YesNo) != DialogResult.Yes)
            {
                return;
            }
            FileUtil.SaveFile(SceneryEntry.ToPLY(), FileFilters.PLY, FileFilters.Any);
        }

        /*private void Menu_Export_COLLADA()
        {
            if (MessageBox.Show("Exporting to COLLADA (.dae) is experimental.\nTexture and quad information will not be exported.\n\nContinue anyway?", "Export as OBJ", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            FileUtil.SaveFile(sceneryentry.ToCOLLADA(), FileFilters.COLLADA, FileFilters.Any);
        }*/

        private void Menu_Fix_WGEOv3()
        {
            for (int i = 0; i < SceneryEntry.Vertices.Count; i++)
            {
                SceneryVertex vtx = SceneryEntry.Vertices[i];
                SceneryEntry.Vertices[i] = new SceneryVertex(
                    (vtx.X & 0xFFF) - 0x800,
                    (vtx.Y & 0xFFF) - 0x800,
                    (vtx.Z & 0xFFF) - 0x800,
                    vtx.UnknownX,
                    vtx.UnknownY,
                    vtx.UnknownZ
                );
            }

            SceneryEntry.XOffset += 0x8000;
            SceneryEntry.YOffset += 0x8000;
            SceneryEntry.ZOffset += 0x8000;
        }
    }
}
