using AltUI.Controls;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public sealed class MusicBox : UserControl
    {
        private MusicEntryController controller;
        private MusicEntry musicentry;

        private TableLayoutPanel pnOptions;
        private DarkTextBox txtMusic;
        private ListView listView1;
        private Label lblEIDError;
        public MusicBox(MusicEntryController controller)
        {
            this.controller = controller;
            musicentry = controller.MusicEntry;

            listView1 = new ListView()
            {
                View = View.Details,
                Size = new Size(120, 200),
                FullRowSelect = true,
            };
            listView1.Click += listView1_Click;
            listView1.Columns.Add("Item");
            listView1.Columns.Add("EID");

            ListViewItem newitem = new ListViewItem("VH");
            newitem.SubItems.Add(Entry.EIDToEName(musicentry.VHEID));
            listView1.Items.Add(newitem);
            newitem = new ListViewItem("VB 0");
            newitem.SubItems.Add(Entry.EIDToEName(musicentry.VB0EID));
            listView1.Items.Add(newitem);
            newitem = new ListViewItem("VB 1");
            newitem.SubItems.Add(Entry.EIDToEName(musicentry.VB1EID));
            listView1.Items.Add(newitem);
            newitem = new ListViewItem("VB 2");
            newitem.SubItems.Add(Entry.EIDToEName(musicentry.VB2EID));
            listView1.Items.Add(newitem);
            newitem = new ListViewItem("VB 3");
            newitem.SubItems.Add(Entry.EIDToEName(musicentry.VB3EID));
            listView1.Items.Add(newitem);
            newitem = new ListViewItem("VB 4");
            newitem.SubItems.Add(Entry.EIDToEName(musicentry.VB4EID));
            listView1.Items.Add(newitem);
            newitem = new ListViewItem("VB 5");
            newitem.SubItems.Add(Entry.EIDToEName(musicentry.VB5EID));
            listView1.Items.Add(newitem);
            newitem = new ListViewItem("VB 6");
            newitem.SubItems.Add(Entry.EIDToEName(musicentry.VB6EID));
            listView1.Items.Add(newitem);

            //foreach (ColumnHeader column in listView1.Columns)
            //{
            //    column.Width = -2;
            //}
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            txtMusic = new DarkTextBox()
            {
                Enabled = false,
                MaxLength = 5
            };
            txtMusic.TextChanged += txtMusic_TextChanged;
            txtMusic.KeyDown += txtMusic_KeyDown;
            txtMusic.LostFocus += txtMusic_LostFocus;

            lblEIDError = new Label()
            {
                AutoSize = true,
                ForeColor = Color.Red
            };

            pnOptions = new TableLayoutPanel()
            {
                ColumnCount = 1,
                RowCount = 3,
                Dock = DockStyle.Fill,
   
            };
            pnOptions.Controls.Add(listView1, 0, 0);
            pnOptions.Controls.Add(txtMusic, 0, 1);
            pnOptions.Controls.Add(lblEIDError, 0, 2);
            Controls.Add(pnOptions);
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            txtMusic.Enabled = true;
            txtMusic.Text = listView1.SelectedItems[0].SubItems[1].Text;
        }

        private void txtMusic_TextChanged(object sender, EventArgs e)
        {
            lblEIDError.Text = Entry.CheckEIDErrors(txtMusic.Text, true);
        }

        private void UpdateEID()
        {
            if (lblEIDError.Text != string.Empty) return;
            var idx = listView1.SelectedIndices[0];
            listView1.SelectedItems[0].SubItems[1].Text = txtMusic.Text;
            if (idx == 0) musicentry.VHEID = Entry.ENameToEID(txtMusic.Text);
            else if (idx == 1) musicentry.VB0EID = Entry.ENameToEID(txtMusic.Text);
            else if (idx == 2) musicentry.VB1EID = Entry.ENameToEID(txtMusic.Text);
            else if (idx == 3) musicentry.VB2EID = Entry.ENameToEID(txtMusic.Text);
            else if (idx == 4) musicentry.VB3EID = Entry.ENameToEID(txtMusic.Text);
            else if (idx == 5) musicentry.VB4EID = Entry.ENameToEID(txtMusic.Text);
            else if (idx == 6) musicentry.VB5EID = Entry.ENameToEID(txtMusic.Text);
            else if (idx == 7) musicentry.VB6EID = Entry.ENameToEID(txtMusic.Text);
        }

        private void txtMusic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                UpdateEID();
        }

        private void txtMusic_LostFocus(object sender, EventArgs e)
        {
            UpdateEID();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
