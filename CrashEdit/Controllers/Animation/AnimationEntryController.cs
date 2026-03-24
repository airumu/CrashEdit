using System.Media;
using CrashEdit.Crash;
using CrashEdit.Exporters;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(AnimationEntry))]
    public sealed class AnimationEntryController : EntryController
    {
        public AnimationEntryController(AnimationEntry animationentry, SubcontrollerGroup parentGroup) : base(animationentry, parentGroup)
        {
            AnimationEntry = animationentry;
            AddMenuSeparator();
            AddMenu("Compress Animtaion", "Container", Menu_Compress);
            AddMenu("Decompress Animtaion", "Container", Menu_Decompress);
            AddMenuSeparator();
            AddMenu(CrashUI.Properties.Resources.AnimationEntryController_AcExportAsOBJ, Menu_Export_OBJ);
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new AnimationEntryViewer(GetNSF(), Entry.EID);
        }

        public AnimationEntry AnimationEntry { get; }

        private void Menu_Compress()
        {
            int modelEID = AnimationEntry.Frames[0].ModelEID;
            ModelEntry? model = GetEntry<ModelEntry>(modelEID) ?? throw new InvalidOperationException("Linked model not found.");
            var animodel = ModelConverter.CompressFrames(AnimationEntry.Frames, 0);

            AnimationEntry newAnim = new(animodel.Item1, AnimationEntry.IsNew, AnimationEntry.EID);
            ModelEntry newModel = new(model.Info, model.PolyData, model.Colors, model.Textures, model.AnimatedTextures, animodel.Item2, model.EID);

            FileUtil.SaveFile(newAnim.Save(), FileFilters.NSEntry, FileFilters.Any);
            FileUtil.SaveFile(newModel.Save(), FileFilters.NSEntry, FileFilters.Any);
        }

        private void Menu_Decompress()
        {
            int modelEID = AnimationEntry.Frames[0].ModelEID;
            ModelEntry? model = GetEntry<ModelEntry>(modelEID) ?? throw new InvalidOperationException("Linked model not found.");
            var newAniModel = ModelEntry.ConvertCompressedToUncompressed(model, AnimationEntry);

            FileUtil.SaveFile(newAniModel.anim.Save(), FileFilters.NSEntry, FileFilters.Any);
        }

        private void Menu_Export_OBJ()
        {
            if (!FileUtil.SelectSaveFile(out string output, FileFilters.OBJ, FileFilters.Any))
                return;

            // modify the path to add a number before the extension
            string ext = Path.GetExtension(output);
            string filename = Path.GetFileNameWithoutExtension(output);
            string path = Path.GetDirectoryName(output);

            OBJExporter exporter = new();
            for (int i = 0; i < AnimationEntry.Frames.Count; i++)
            {
                exporter.AddObject();
                Frame frame = AnimationEntry.Frames[i];
                Console.WriteLine($"Exporting Frames[{i}]...");

                FrameController.ToOBJ(exporter, GetNSF(), frame, AnimationEntry);
            }
            exporter.Export(path, filename, true);

            Console.WriteLine("Done.");
            SystemSounds.Asterisk.Play();
        }
    }
}
