using CrashEdit.Crash;
using CrashEdit.Exporters;
using System.Media;

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

            string path = Path.GetDirectoryName(filename);
            string modelname = Path.GetFileNameWithoutExtension(filename);

            Console.WriteLine($"Exporting Scenery...");

            var exporter = new OBJExporter();
            exporter.AddObject();
            ToOBJ(exporter, GetNSF(), OldSceneryEntry);
            exporter.Export(path, modelname, false);

            Console.WriteLine("Done.");
            SystemSounds.Asterisk.Play();
        }

        public static void ToOBJ(OBJExporter exporter, NSF nsf, OldSceneryEntry scenery)
        {
            exporter.AddScenery(nsf, scenery);
        }
    }
}
