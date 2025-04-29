using CrashEdit.CE.Properties;

namespace CrashEdit.CE
{
    public partial class HelpWindow : AltUI.Forms.DarkForm
    {
        public HelpWindow()
        {
            Icon = Embeds.GetIcon("HelpSymbol");
            InitializeComponent();

            lbEntityListBox.Text = Resources.EntityBox_tipLists;
            lbProperties.Text = Resources.EntityPropertyBox_tipProperties;
            lbSavedProperties.Text = Resources.EntityPropertyBox_tipSavedProperties;
            lbHexViewer.Text = Resources.HexView_tip;
            lbTextureChunk.Text = Resources.TextureChunkBox_TipText;
            lbTextureViewer.Text = Resources.TextureViewer_tipViewer;
            lbNSDBox.Text = Resources.NSDBox_tipSpawnPoint;
        }
    }
}
