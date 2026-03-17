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

            Console.WriteLine($"Exporting Scenery...");
            ToOBJ(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename), GetNSF(), OldSceneryEntry);
            Console.WriteLine("Done.");
            SystemSounds.Asterisk.Play();
        }

        public static void ToOBJ(string path, string modelname, NSF nsf, OldSceneryEntry scenery)
        {
            var exporter = new OBJExporter();

            exporter.AddScenery(nsf, scenery);
            exporter.Export(path, modelname);
        }
    }
}
