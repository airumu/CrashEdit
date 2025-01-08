using AltUI.Controls;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public sealed class MusicBox : UserControl
    {
        private MusicEntryController controller;
        private MusicEntry musicentry;

        private TableLayoutPanel pnMain;
        private TableLayoutPanel pnSub1;
        private TableLayoutPanel pnSub2;
        private DoubleBufferedListView lstMusic;
        private DarkTextBox txtMusic;
        private Label lblEIDError;
        private Label lblMasterVolume;
        private DarkNumericUpDown numMasterVolume;
        private Label lblMasterPan;
        private DarkNumericUpDown numMasterPan;

        public MusicBox(MusicEntryController controller)
        {
            this.controller = controller;
            musicentry = controller.MusicEntry;

            lstMusic = new DoubleBufferedListView()
            {
                BorderStyle = BorderStyle.FixedSingle,
                FullRowSelect = true,
                UseCompatibleStateImageBehavior = false,
                View = View.Details,
                Size = new Size(120, 200),
               
            };
            lstMusic.Click += lstMusic_Click;
            lstMusic.Columns.Add("Item");
            lstMusic.Columns.Add("EID");

            ListViewItem newitem = new ListViewItem("VH");
            newitem.SubItems.Add(Entry.EIDToEName(musicentry.VHEID));
            lstMusic.Items.Add(newitem);
            newitem = new ListViewItem("VB [0]");
            newitem.SubItems.Add(Entry.EIDToEName(musicentry.VB0EID));
            lstMusic.Items.Add(newitem);
            newitem = new ListViewItem("VB [1]");
            newitem.SubItems.Add(Entry.EIDToEName(musicentry.VB1EID));
            lstMusic.Items.Add(newitem);
            newitem = new ListViewItem("VB [2]");
            newitem.SubItems.Add(Entry.EIDToEName(musicentry.VB2EID));
            lstMusic.Items.Add(newitem);
            newitem = new ListViewItem("VB [3]");
            newitem.SubItems.Add(Entry.EIDToEName(musicentry.VB3EID));
            lstMusic.Items.Add(newitem);
            newitem = new ListViewItem("VB [4]");
            newitem.SubItems.Add(Entry.EIDToEName(musicentry.VB4EID));
            lstMusic.Items.Add(newitem);
            newitem = new ListViewItem("VB [5]");
            newitem.SubItems.Add(Entry.EIDToEName(musicentry.VB5EID));
            lstMusic.Items.Add(newitem);
            newitem = new ListViewItem("VB [6]");
            newitem.SubItems.Add(Entry.EIDToEName(musicentry.VB6EID));
            lstMusic.Items.Add(newitem);

            //foreach (ColumnHeader column in lstMusic.Columns)
            //{
            //    column.Width = -2;
            //}
            lstMusic.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

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

            if (musicentry.VH != null)
            {
                lblMasterVolume = new Label()
                {
                    Text = "Master Volume"
                };
                numMasterVolume = new DarkNumericUpDown()
                {
                    Minimum = 0,
                    Maximum = 127,
                    Value = musicentry.VH.Volume
                };
                numMasterVolume.ValueChanged += (sender, e) =>
                {
                    musicentry.VH.Volume = (byte)numMasterVolume.Value;
                };

                lblMasterPan = new Label()
                {
                    Text = "Master Pan"
                };
                numMasterPan = new DarkNumericUpDown()
                {
                    Minimum = 0,
                    Maximum = 127,
                    Value = musicentry.VH.Panning
                };
                numMasterPan.ValueChanged += (sender, e) =>
                {
                    musicentry.VH.Panning = (byte)numMasterPan.Value;
                };
            }

            pnMain = new TableLayoutPanel()
            {
                ColumnCount = 2,
                RowCount = 1,
                Dock = DockStyle.Fill
            };
            pnSub1 = new TableLayoutPanel()
            {
                ColumnCount = 1,
                RowCount = 3,
                Dock = DockStyle.Fill
            };
            pnSub2 = new TableLayoutPanel()
            {
                ColumnCount = 1,
                RowCount = 4,
                Dock = DockStyle.Fill
            };
            pnSub1.Controls.Add(lstMusic, 0, 0);
            pnSub1.Controls.Add(txtMusic, 0, 1);
            pnSub1.Controls.Add(lblEIDError, 0, 2);
            if (musicentry.VH != null)
            {
                pnSub2.Controls.Add(lblMasterVolume, 0, 0);
                pnSub2.Controls.Add(numMasterVolume, 0, 1);
                pnSub2.Controls.Add(lblMasterPan, 0, 2);
                pnSub2.Controls.Add(numMasterPan, 0, 3);
            }
            pnMain.Controls.Add(pnSub1);
            pnMain.Controls.Add(pnSub2);
            Controls.Add(pnMain);
        }
        private void UpdateEID()
        {
            if (lblEIDError.Text != string.Empty) return;
            var idx = lstMusic.SelectedIndices[0];
            lstMusic.SelectedItems[0].SubItems[1].Text = txtMusic.Text;
            if (idx == 0) musicentry.VHEID = Entry.ENameToEID(txtMusic.Text);
            else if (idx == 1) musicentry.VB0EID = Entry.ENameToEID(txtMusic.Text);
            else if (idx == 2) musicentry.VB1EID = Entry.ENameToEID(txtMusic.Text);
            else if (idx == 3) musicentry.VB2EID = Entry.ENameToEID(txtMusic.Text);
            else if (idx == 4) musicentry.VB3EID = Entry.ENameToEID(txtMusic.Text);
            else if (idx == 5) musicentry.VB4EID = Entry.ENameToEID(txtMusic.Text);
            else if (idx == 6) musicentry.VB5EID = Entry.ENameToEID(txtMusic.Text);
            else if (idx == 7) musicentry.VB6EID = Entry.ENameToEID(txtMusic.Text);
        }

        private void lstMusic_Click(object? sender, EventArgs e)
        {
            txtMusic.Enabled = true;
            txtMusic.Text = lstMusic.SelectedItems[0].SubItems[1].Text;
        }

        private void txtMusic_TextChanged(object? sender, EventArgs e)
        {
            lblEIDError.Text = Entry.CheckEIDErrors(txtMusic.Text, true);
        }

        private void txtMusic_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                UpdateEID();
        }

        private void txtMusic_LostFocus(object? sender, EventArgs e)
        {
            UpdateEID();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
    public class DoubleBufferedListView : ListView
    {
        public DoubleBufferedListView()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }
    }
}
