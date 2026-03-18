using CrashEdit.Crash;
using CrashEdit.Exporters;
using System.Media;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(SceneryEntry))]
    public sealed class SceneryEntryController : EntryController
    {
        public SceneryEntryController(SceneryEntry sceneryentry, SubcontrollerGroup parentGroup) : base(sceneryentry, parentGroup)
        {
            SceneryEntry = sceneryentry;
            AddMenuSeparator();
            AddMenu(CrashUI.Properties.Resources.AnimationEntryController_AcExportAsOBJ, Menu_Export_OBJ);
            AddMenuSeparator();
            AddMenu(CrashUI.Properties.Resources.SceneryEntryController_AcFixWGEOv3, "Calculator", Menu_Fix_WGEOv3);
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new SceneryEntryViewer(GetNSF(), Entry.EID);
        }

        public SceneryEntry SceneryEntry { get; }

        private void Menu_Export_OBJ()
        {
            if (!FileUtil.SelectSaveFile(out string filename, FileFilters.OBJ, FileFilters.Any))
                return;

            string path = Path.GetDirectoryName(filename);
            string modelname = Path.GetFileNameWithoutExtension(filename);

            Console.WriteLine($"Exporting Scenery...");

            var exporter = new OBJExporter();
            exporter.AddObject();
            ToOBJ(exporter, GetNSF(), SceneryEntry);
            exporter.Export(path, modelname, false);

            Console.WriteLine("Done.");
            SystemSounds.Asterisk.Play();
        }

        public static void ToOBJ(OBJExporter exporter, NSF nsf, SceneryEntry scenery)
        {
            exporter.AddScenery(nsf, scenery);
        }

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
