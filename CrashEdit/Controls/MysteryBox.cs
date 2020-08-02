using System;
using System.Windows.Forms;
using DarkUI.Controls;

namespace CrashEdit
{
    public sealed class MysteryBox : UserControl
    {
        private byte[] data;
        private bool saving;

        private DarkToolStrip tsToolbar;
        private ToolStripButton tbbExport;
        private HexBox hbData;

        public MysteryBox(byte[] data)
        {
            this.data = data;
            saving = false;

            tbbExport = new ToolStripButton();
            tbbExport.Text = "Export";
            tbbExport.Click += new EventHandler(tbbExport_Click);
            tbbExport.Size = new System.Drawing.Size(48, 23);

            tsToolbar = new DarkToolStrip();
            tsToolbar.Dock = DockStyle.Top;
            tsToolbar.Items.Add(tbbExport);

            hbData = new HexBox();
            hbData.Dock = DockStyle.Fill;
            hbData.Data = data;

            Controls.Add(hbData);
            Controls.Add(tsToolbar);
        }

        void tbbExport_Click(object sender,EventArgs e)
        {
            if (!saving)
            {
                saving = true;
                FileUtil.SaveFile(data, FileFilters.Any);
                saving = false;
            }
        }
    }
}
