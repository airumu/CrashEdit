using CrashEdit.Crash;
using CrashEdit.Exporters;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(OldFrame))]
    public sealed class OldFrameController : LegacyController
    {
        public OldFrameController(OldFrame oldframe, SubcontrollerGroup parentGroup) : base(parentGroup, oldframe)
        {
            OldFrame = oldframe;
            AddMenuSeparator();
            AddMenu(CrashUI.Properties.Resources.AnimationEntryController_AcExportAsOBJ, Menu_Export_OBJ);
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new OldFrameBox(this);
        }

        public OldAnimationEntryController OldAnimationEntryController => Modern.Parent.Legacy as OldAnimationEntryController;
        public ColoredAnimationEntryController ColoredAnimationEntryController => Modern.Parent.Legacy as ColoredAnimationEntryController;
        public OldFrame OldFrame { get; }
        public bool IsColored => Modern.Parent.Text.Contains("Colored Animation");

        private void Menu_Export_OBJ()
        {
            if (!FileUtil.SelectSaveFile(out string filename, FileFilters.OBJ, FileFilters.Any))
                return;

            if (IsColored)
            {
                ToOBJ_Colored(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename), GetNSF(), OldFrame);
            }
            else
            {
                ToOBJ_Old(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename), GetNSF(), OldFrame);
            }
        }

        public static void ToOBJ_Old(string path, string modelname, NSF nsf, OldFrame oldFrame)
        {
            Dictionary<int, int> textureEIDs = new Dictionary<int, int>();
            Dictionary<string, TexInfoUnpacked> objTranslate = new Dictionary<string, TexInfoUnpacked>();

            var exporter = new OBJExporter();

            exporter.AddFrame_Old(nsf, oldFrame, ref textureEIDs, ref objTranslate);
            exporter.Export(path, modelname);
        }

        public static void ToOBJ_Colored(string path, string modelname, NSF nsf, OldFrame oldFrame)
        {
            Dictionary<int, int> textureEIDs = new Dictionary<int, int>();
            Dictionary<string, TexInfoUnpacked> objTranslate = new Dictionary<string, TexInfoUnpacked>();

            var exporter = new OBJExporter();

            exporter.AddFrame_Colored(nsf, oldFrame, ref textureEIDs, ref objTranslate);
            exporter.Export(path, modelname);
        }
    }
}
