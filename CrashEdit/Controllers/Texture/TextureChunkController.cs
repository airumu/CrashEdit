using System.Media;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(TextureChunk))]
    public sealed class TextureChunkController : ChunkController
    {
        public TextureChunkController(TextureChunk texturechunk, SubcontrollerGroup parentGroup) : base(texturechunk, parentGroup)
        {
            TextureChunk = texturechunk;
            AddMenu(CrashUI.Properties.Resources.TextureChunkController_AcRename, "Modify", Menu_Rename_Entry);
            AddMenu(CrashUI.Properties.Resources.TextureChunkController_AcRecalcChecksum, "Calculator", Menu_Recalculate_Checksum);
            AddMenu(CrashUI.Properties.Resources.TextureChunkController_AcOpenViewer, "Painting", Menu_Open_Viewer);
        }

        public override bool EditorAvailable => Type.GetType("Mono.Runtime") == null;

        public override Control CreateEditor()
        {
            // Hack for Mono so it doesn't crash.
            if (Type.GetType("Mono.Runtime") != null)
                return base.CreateEditor();
            return new TextureChunkBox(TextureChunk);
        }

        public TextureChunk TextureChunk { get; }

        private void Menu_Recalculate_Checksum()
        {
            //int correct_checksum = Chunk.CalculateChecksum(TextureChunk.Data);
            //BitConv.ToInt32(TextureChunk.Data, 12, correct_checksum);
            SystemSounds.Asterisk.Play();
            int current_checksum = BitConv.FromInt32(TextureChunk.Data, 12);
            int correct_checksum = Chunk.CalculateChecksum(TextureChunk.Data);
            if (current_checksum == correct_checksum)
            {
                Console.WriteLine("Checksum was already correct.");
                return;
            }
            BitConv.ToInt32(TextureChunk.Data, 12, correct_checksum);
            Console.WriteLine("Checksum was incorrect and has been corrected.");
        }

        private void Menu_Rename_Entry()
        {
            using (NewEntryForm newentrywindow = new NewEntryForm(GetNSF(), GameVersion))
            {
                newentrywindow.Text = "Rename Entry";
                newentrywindow.SetRenameMode(TextureChunk.EName);
                if (newentrywindow.ShowDialog() == DialogResult.OK)
                {
                    TextureChunk.EID = newentrywindow.EID;
                    BitConv.ToInt32(TextureChunk.Data, 12, Chunk.CalculateChecksum(TextureChunk.Data));
                }
            }
        }

        private TextureViewer frmViewer = null;

        private void Menu_Open_Viewer()
        {
            if (frmViewer == null)
            {
                frmViewer = new TextureViewer(TextureChunk);
                frmViewer.FormClosing += delegate (object sender2, FormClosingEventArgs e2)
                {
                    frmViewer = null;
                };
                frmViewer.Show();
            }
            else
                frmViewer.Select();
        }
    }
}
