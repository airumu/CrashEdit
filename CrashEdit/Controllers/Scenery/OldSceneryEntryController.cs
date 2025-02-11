using CrashEdit.Crash;
using CrashEdit.Exporters;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(OldSceneryEntry))]
    public sealed class OldSceneryEntryController : EntryController
    {
        public OldSceneryEntryController(OldSceneryEntry oldsceneryentry, SubcontrollerGroup parentGroup) : base(oldsceneryentry, parentGroup)
        {
            OldSceneryEntry = oldsceneryentry;
            AddMenuSeparator();
            AddMenu(CrashUI.Properties.Resources.AnimationEntryController_AcExportAsOBJ, Menu_Export_OBJ);
            //AddMenu("Export as COLLADA", Menu_Export_COLLADA);
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new OldSceneryEntryViewer(GetNSF(), Entry.EID);
        }

        public OldSceneryEntry OldSceneryEntry { get; }

        private void Menu_Export_OBJ()
        {
            if (!FileUtil.SelectSaveFile(out string filename, FileFilters.OBJ, FileFilters.Any))
                return;

            ToOBJ(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename), GetNSF(), OldSceneryEntry);
        }

        public static void ToOBJ(string path, string modelname, NSF nsf, OldSceneryEntry scenery)
        {
            var exporter = new OBJExporter();

            // detect how many textures are used and their eids to prepare the image
            Dictionary<int, int> textureEIDs = new();
            Dictionary<string, TexInfoUnpacked> objTranslate = new Dictionary<string, TexInfoUnpacked>();

            exporter.AddScenery(nsf, scenery, ref textureEIDs, ref objTranslate);

            exporter.Export(path, modelname);
        }

        /*private void Menu_Export_COLLADA()
        {
            if (DarkMessageBox.ShowWarning(Resources.Scenery_ExportCOLLADA, Resources.Scenery_ExportCOLLADA_Title, DarkDialogButton.YesNo) != DialogResult.Yes)
            {
                return;
            }
            FileUtil.SaveFile(OldSceneryEntry.ToCOLLADA(), FileFilters.COLLADA, FileFilters.Any);
        }*/
    }
}
