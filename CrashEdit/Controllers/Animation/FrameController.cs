using CrashEdit.Crash;
using CrashEdit.Exporters;
using System.Media;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(Frame))]
    public sealed class FrameController : LegacyController
    {
        public FrameController(Frame frame, SubcontrollerGroup parentGroup) : base(parentGroup, frame)
        {
            Frame = frame;
            AddMenuSeparator();
            AddMenu(CrashUI.Properties.Resources.AnimationEntryController_AcExportAsOBJ, Menu_Export_OBJ);
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            var entry = AnimationEntryController.AnimationEntry;
            if (!Frame.IsNew)
            {
                return new FrameBox(this);
            }
            else
            {
                return new AnimationEntryViewer(GetNSF(), entry.EID, entry.Frames.IndexOf(Frame));
            }
        }

        public AnimationEntryController AnimationEntryController => (AnimationEntryController)Modern.Parent.Legacy;
        public Frame Frame { get; }

        private void Menu_Export_OBJ()
        {
            if (!FileUtil.SelectSaveFile(out string filename, FileFilters.OBJ, FileFilters.Any))
                return;

            Console.WriteLine($"Exporting Frame...");
            ToOBJ(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename), GetNSF(), Frame, AnimationEntryController.AnimationEntry);
            Console.WriteLine("Done.");
            SystemSounds.Asterisk.Play();
        }

        /// <summary>
        /// Exports the model to the OBJ file format ready to be used with other software
        ///
        /// TODO: MAYBE IMPLEMENT AN FBX EXPORT OR SOMETHING ELSE THAT IS A BIT MORE FLEXIBLE?
        ///
        /// This function resides here because access to GameScales is required, and the Frame object does not have access to it
        /// a good improvement might be to move this there
        /// </summary>
        /// <returns></returns>
        public static void ToOBJ(string path, string modelname, NSF nsf, Frame frame, AnimationEntry anim)
        {
            Dictionary<int, int> textureEIDs = new();
            Dictionary<string, TexInfoUnpacked> objTranslate = new Dictionary<string, TexInfoUnpacked>();

            var exporter = new OBJExporter();

            exporter.AddFrame(nsf, frame, anim, ref textureEIDs, ref objTranslate);
            exporter.Export(path, modelname);
        }
    }
}
