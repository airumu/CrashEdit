using CrashEdit.CE.Forms;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(SEQ))]
    public sealed class SEQController : LegacyController
    {
        public SEQController(SEQ seq, SubcontrollerGroup parentGroup) : base(parentGroup, seq)
        {
            SEQ = seq;
            AddMenuSeparator();
            AddMenu("Import MIDI", "Import", Menu_Import_MIDI);
        }

        public MusicEntryController MusicEntryController => (MusicEntryController)Modern.Parent.Legacy;

        public SEQ SEQ { get; }

        private void Menu_Import_MIDI()
        {
            using OpenFileDialog dialog = new();
            dialog.Filter = FileFilters.MIDI + "|" + FileFilters.Any;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SEQTool seqtool = new(dialog.FileName);
                if (seqtool.ShowDialog() == DialogResult.OK)
                {
                    string text = Modern.Text.ToString();
                    int index = int.Parse(new string([.. text.Where(char.IsDigit)]));
                    MusicEntryController.MusicEntry.Tracks[index] = seqtool.convertedSEQ;
                }
            }
        }
    }
}
