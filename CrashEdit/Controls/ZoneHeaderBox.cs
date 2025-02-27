using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using MetroSet_UI.Controls;

namespace CrashEdit.CE
{
    public partial class ZoneHeaderBox : UserControl
    {
        ZoneHeaderController controller;
        ZoneHeader header;

        private DataGridView currentDataGridView;
        private MetroSetTabControl tbcTabs;
        private TabPage tbpHeader;
        private TabPage tbpHex;

        private int spLoadListCount;
        private BindingList<string> spLoadList;
        private DarkToolTip tipSPLoadList;

        private int maxZoneCount;
        private bool firstInit = true;

        internal Stack<bool> dirty = new Stack<bool>();
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        public ZoneHeaderBox(ZoneHeaderController controller)
        {
            this.controller = controller;
            header = controller.ZoneHeader;
            InitializeComponent();
            CreateTabs();

            DoubleBufferedDataGridView.Initialize(dgvZones);
            DoubleBufferedDataGridView.Initialize(dgvWorlds);

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem appendRowItem = new ToolStripMenuItem("Append Row");
            ToolStripMenuItem deleteRowItem = new ToolStripMenuItem("Remove Last Row");
            appendRowItem.Click += AppendRowItem_Click;
            deleteRowItem.Click += DeleteRowItem_Click;
            contextMenu.Items.Add(appendRowItem);
            contextMenu.Items.Add(deleteRowItem);
            dgvZones.ContextMenuStrip = contextMenu;
            dgvWorlds.ContextMenuStrip = contextMenu;
            dgvZones.CellMouseDown += DataGridView_CellMouseDown;
            dgvWorlds.CellMouseDown += DataGridView_CellMouseDown;

            dgvZones.ShowCellToolTips = false;
            dgvWorlds.ShowCellToolTips = false;

            tipSPLoadList = new DarkToolTip();
            tipSPLoadList.SetToolTip(lbSPLoadList, Resources.EntityBox_tipLists);

            HeaderInit();
            maxZoneCount = header.IsNew ? 16 : 8;
            firstInit = false;
        }

        private void CreateTabs()
        {
            tbcTabs = new MetroSetTabControl()
            {
                BackgroundColor = Color.FromArgb(31, 31, 32),
                Dock = DockStyle.Fill,
                IsDerivedStyle = false,
                ItemSize = new Size(100, 28),
                Style = MetroSet_UI.Enums.Style.Dark,
                TabStyle = MetroSet_UI.Enums.TabStyle.Style1
            };

            {
                tbpHex = new TabPage("Hex");
                HexInit();
                tbpHex.Enter += tbpHex_Enter;
                tbcTabs.TabPages.Add(tbpHex);
            }
            {
                tbpHeader = new TabPage("Header");
                tbpHeader.Controls.Add(pnHeader);
                tbpHeader.BackColor = Color.FromArgb(31, 31, 32);
                tbpHeader.Enter += tbpHeader_Enter;
                tbcTabs.TabPages.Add(tbpHeader);
            }
            tbcTabs.SelectedIndex = 1;
            Controls.Add(tbcTabs);
        }

        private void HeaderInit()
        {
            UpdateZones();
            UpdateWorlds();
            UpdateMusic();
            UpdateZoneFlags();
            if (Settings.Default.EnableC2TTEditor)
            {
                UpdateSPLoadLists();
            }
            else
            {
                fraSpecialLoadList.Visible = false;
            }
        }

        private void HexInit()
        {
            header.Data = header.Save();
            tbpHex.Controls.Clear();
            tbpHex.Controls.Add(new HexView(header.Data, HexView_DataChangeHandler) { Dock = DockStyle.Fill });
        }

        private void ZoneHeaderBox_Leave(object sender, EventArgs e)
        {
            tbcTabs.SelectedIndex = 1;
        }

        private void tbpHeader_Enter(object sender, EventArgs e)
        {
            HeaderInit();
        }

        private void tbpHex_Enter(object sender, EventArgs e)
        {
            HexInit();
        }

        private void UpdateZones()
        {
            if (firstInit)
            {
                dgvZones.Columns.Add("Index", "Index");
                dgvZones.Columns.Add("EID", "EID");
                dgvZones.Columns.Add("Flag", "Flag");
                foreach (DataGridViewColumn column in dgvZones.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvZones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    column.Width = 60;
                }
                dgvZones.Columns[0].DefaultCellStyle.ForeColor = Color.Gray;
            }

            dgvZones.Rows.Clear();
            for (int i = 0; i < header.ZoneCount; ++i)
            {
                dgvZones.Rows.Add(i, Entry.EIDToEName(header.Zones[i]), header.ZoneLinkTypes[i].ToString("X2"));
            }
        }

        private void UpdateWorlds()
        {
            if (firstInit)
            {
                dgvWorlds.Columns.Add("Index", "Index");
                dgvWorlds.Columns.Add("EID", "EID");
                foreach (DataGridViewColumn column in dgvWorlds.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvWorlds.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    column.Width = 60;
                }
                dgvWorlds.Columns[0].DefaultCellStyle.ForeColor = Color.Gray;
            }

            dgvWorlds.Rows.Clear();
            for (int i = 0; i < header.WorldCount; ++i)
            {
                dgvWorlds.Rows.Add(i, Entry.EIDToEName(header.Worlds[i]));
            }
        }

        private void UpdateMusic()
        {
            dirty.Push(true);
            txtMusic.Text = Entry.EIDToEName(header.Music);
            dirty.Pop();
        }

        private void UpdateZoneFlags()
        {
            dirty.Push(true);
            txtZoneFlags.Text = header.ZoneFlags.ToString("X");
            dirty.Pop();
        }

        private void UpdateSPLoadLists()
        {
            spLoadListCount = BitConv.FromInt32(header.Chunk1, 0x8);
            spLoadList = new BindingList<string>();
            for (int i = 0; i < spLoadListCount; ++i)
            {
                int eid = BitConv.FromInt32(header.Chunk1, 0xC + i * 0x4);
                spLoadList.Add(Entry.EIDToEName(eid));
            }
            lbSPLoadList.DataSource = spLoadList;

            if (spLoadListCount >= 39)
                cmdAppendSP.Enabled = false;
            if (spLoadListCount == 0)
            {
                cmdRemoveSP.Enabled = false;
                txtSPLoadList.Enabled = false;
            }
        }

        private void DataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                currentDataGridView = sender as DataGridView;
                if (e.RowIndex >= 0)
                {
                    currentDataGridView.ClearSelection();
                    currentDataGridView.Rows[e.RowIndex].Selected = true;
                    currentDataGridView.CurrentCell = currentDataGridView.Rows[e.RowIndex].Cells[0];
                }
            }
        }

        private void AppendRowItem_Click(object sender, EventArgs e)
        {
            if (currentDataGridView == null)
            {
                DarkMessageBox.ShowError("No row is selected.", Resources.Title_Error);
                return;
            }
            if (currentDataGridView.Rows.Count >= maxZoneCount)
            {
                DarkMessageBox.ShowError($"You cannot add more than {maxZoneCount} rows.", Resources.Title_Error);
                return;
            }

            if (currentDataGridView == dgvZones)
            {
                if (header.ZoneCount == 0)
                {
                    dgvZones.Rows.Add(0, "00000", "00");
                    //header.Zones.Add(0);
                    //header.ZoneLinkTypes.Add(0);
                    header.Zones[0] = 0;
                    header.ZoneLinkTypes[0] = 0;
                }
                else
                {
                    int idx = header.ZoneCount - 1;
                    dgvZones.Rows.Add(header.ZoneCount, Entry.EIDToEName(header.Zones[idx]), header.ZoneLinkTypes[idx].ToString("X2"));
                    //header.Zones.Add(header.Zones[idx]);
                    //header.ZoneLinkTypes.Add(header.ZoneLinkTypes[idx]);
                    header.Zones[header.ZoneCount] = header.Zones[idx];
                    header.ZoneLinkTypes[header.ZoneCount] = header.ZoneLinkTypes[idx];
                }
                ++header.ZoneCount;
            }
            else if (currentDataGridView == dgvWorlds)
            {
                if (header.WorldCount == 0)
                {
                    dgvWorlds.Rows.Add(0, "00000");
                    //header.Worlds.Add(0);
                    header.Worlds[0] = 0;
                }
                else
                {
                    int idx = header.WorldCount - 1;
                    dgvWorlds.Rows.Add(header.WorldCount, Entry.EIDToEName(header.Worlds[idx]));
                    //header.Worlds.Add(header.Worlds[idx]);
                    header.Worlds[header.WorldCount] = header.Worlds[idx];
                }
                ++header.WorldCount;
            }
        }

        private void DeleteRowItem_Click(object sender, EventArgs e)
        {
            if (currentDataGridView == null)
            {
                DarkMessageBox.ShowError("No row is selected.", Resources.Title_Error);
                return;
            }
            if (currentDataGridView.Rows.Count == 0) return;

            if (currentDataGridView == dgvZones)
            {
                int idx = header.ZoneCount - 1;
                dgvZones.Rows.RemoveAt(dgvZones.RowCount - 1);
                //header.Zones.RemoveAt(idx);
                //header.ZoneLinkTypes.RemoveAt(idx);
                header.Zones[idx] = 0;
                header.ZoneLinkTypes[idx] = 0;
                --header.ZoneCount;
            }
            else if (currentDataGridView == dgvWorlds)
            {
                int idx = header.WorldCount - 1;
                dgvWorlds.Rows.RemoveAt(dgvWorlds.RowCount - 1);
                //header.Worlds.RemoveAt(idx);
                header.Worlds[idx] = 0;
                --header.WorldCount;
            }
        }

        private void dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Cancel = true;
            }
        }

        private void dgvZones_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string inputValue = e.FormattedValue.ToString();

            if (e.ColumnIndex == 1)
            {
                string eid = Entry.CheckEIDErrors(inputValue, true);
                if (eid != string.Empty)
                {
                    DarkMessageBox.ShowError("Please enter a valid EID.", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
            else if (e.ColumnIndex == 2)
            {
                if (!Regex.IsMatch(inputValue, @"\A\b[0-9a-fA-F]+\b\Z"))
                {
                    DarkMessageBox.ShowError("Please enter a valid hexadecimal value.", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
        }

        private void dgvZones_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var cellValue = dgvZones.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            if (e.ColumnIndex == 1)
            {
                header.Zones[e.RowIndex] = Entry.ENameToEID(cellValue.ToString());
            }
            else if (e.ColumnIndex == 2)
            {
                header.ZoneLinkTypes[e.RowIndex] = Convert.ToInt32(cellValue);
            }
        }

        private void dgvZones_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (e.Value is string inputValue)
                {
                    e.Value = Convert.ToInt32(inputValue, 16);
                    e.ParsingApplied = true;
                }
            }
        }

        private void dgvZones_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (e.Value is int intValue)
                {
                    e.Value = intValue.ToString("X2");
                    e.FormattingApplied = true;
                }
            }
        }

        private void dgvWorlds_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string inputValue = e.FormattedValue.ToString();

            if (e.ColumnIndex == 1)
            {
                string eid = Entry.CheckEIDErrors(inputValue, true);
                if (eid != string.Empty)
                {
                    DarkMessageBox.ShowError("Please enter a valid EID.", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
        }

        private void dgvWorlds_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var cellValue = dgvWorlds.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            if (e.ColumnIndex == 1)
            {
                header.Worlds[e.RowIndex] = Entry.ENameToEID(cellValue.ToString());
            }
        }

        private void txtMusic_TextChanged(object sender, EventArgs e)
        {
            if (Dirty) return;

            lblEIDError.Visible = true;
            lblEIDError.Text = Entry.CheckEIDErrors(txtMusic.Text, true);
            if (lblEIDError.Text != string.Empty) return;

            header.Music = Entry.ENameToEID(txtMusic.Text);
        }

        private void txtZoneFlags_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !Regex.IsMatch(e.KeyChar.ToString(), "[0-9A-Fa-f]"))
            {
                e.Handled = true;
            }
        }

        private void txtZoneFlags_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    string originalText = textBox.Text;
                    string validText = Regex.Replace(originalText, "[^0-9A-Fa-f]", "");
                    if (originalText != validText)
                    {
                        int cursorPosition = textBox.SelectionStart - (originalText.Length - validText.Length);
                        textBox.Text = validText;
                        textBox.SelectionStart = Math.Max(0, cursorPosition);
                    }
                    header.ZoneFlags = Convert.ToInt32(validText, 16);
                }
                else
                {
                    header.ZoneFlags = 0;
                }
            }
        }

        private void UpdateSPLoadListsCount()
        {
            byte[] bytes = BitConverter.GetBytes(spLoadListCount);
            Array.Copy(bytes, 0, header.Chunk1, 0x8, 0x4);
        }

        private void UpdateSPLoadListsEID(bool delete)
        {
            byte[] bytes = new byte[spLoadListCount * 0x4];
            for (int i = 0; i < spLoadListCount; ++i)
            {
                int eid = Entry.NullEID;
                if (spLoadList.Count > 0)
                    eid = Entry.ENameToEID(spLoadList[i]);
                byte[] eidBytes = BitConverter.GetBytes(eid);
                Array.Copy(eidBytes, 0, bytes, i * 0x4, 0x4);
            }
            Array.Copy(bytes, 0, header.Chunk1, 0xC, bytes.Length);

            if (delete)
            {
                byte[] nullbytes = new byte[0x4];
                Array.Copy(nullbytes, 0, header.Chunk1, 0xC + spLoadListCount * 0x4, 0x4);
                txtSPLoadList.Text = string.Empty;
            }
        }

        private void ClearSPLoadListEID()
        {
            byte[] nullbytes = new byte[spLoadListCount * 0x4];
            Array.Copy(nullbytes, 0, header.Chunk1, 0xC, nullbytes.Length);
            txtSPLoadList.Text = string.Empty;
        }

        private void cmdAppendSP_Click(object sender, EventArgs e)
        {
            dirty.Push(true);

            ++spLoadListCount;
            string eid = Entry.NullEName;
            if (spLoadList.Count > 0)
                eid = spLoadList[spLoadList.Count - 1];
            spLoadList.Add(eid);
            UpdateSPLoadListsCount();
            UpdateSPLoadListsEID(false);

            lbSPLoadList.SelectedIndex = lbSPLoadList.Items.Count - 1;
            txtSPLoadList.Text = lbSPLoadList.SelectedItem.ToString();

            if (spLoadListCount >= 39)
            {
                cmdAppendSP.Enabled = false;
            }
            cmdRemoveSP.Enabled = true;
            txtSPLoadList.Enabled = true;

            dirty.Pop();
        }

        private void cmdRemoveSP_Click(object sender, EventArgs e)
        {
            var selectedItem = lbSPLoadList.SelectedItem;
            if (selectedItem == null) return;

            dirty.Push(true);

            int selctedIndex = lbSPLoadList.SelectedIndex;

            --spLoadListCount;
            spLoadList.RemoveAt(selctedIndex);
            UpdateSPLoadListsCount();
            UpdateSPLoadListsEID(true);

            if (lbSPLoadList.Items.Count > 0)
            {
                if (selctedIndex >= lbSPLoadList.Items.Count)
                    selctedIndex = lbSPLoadList.Items.Count - 1;
                lbSPLoadList.SelectedIndex = selctedIndex;
                txtSPLoadList.Text = lbSPLoadList.SelectedItem.ToString();
            }

            if (spLoadListCount == 0)
            {
                cmdRemoveSP.Enabled = false;
                txtSPLoadList.Enabled = false;
            }
            cmdAppendSP.Enabled = true;

            dirty.Pop();
        }

        private void lbSPLoadList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSPLoadList.SelectedIndex < 0) return;

            dirty.Push(true);
            txtSPLoadList.Text = lbSPLoadList.SelectedItem.ToString();
            lblEIDErrorSP.Visible = false;
            dirty.Pop();
        }

        private void lbSPLoadList_KeyDown(object sender, KeyEventArgs e)
        {
            dirty.Push(true);
            // copy list
            if ((e.KeyCode == Keys.C || e.KeyCode == Keys.X) && (e.Modifiers & Keys.Control) == Keys.Control && (e.Modifiers & Keys.Shift) == Keys.Shift)
            {
                if (spLoadList.Count <= 0) return;

                StringBuilder sb = new StringBuilder();
                foreach (object item in spLoadList)
                {
                    sb.Append(item + Environment.NewLine);
                }
                if (sb.Length > 0)
                    Clipboard.SetDataObject(sb.ToString(), true, 10, 100);

                if (e.KeyCode == Keys.X) // clear
                {
                    spLoadList.Clear();
                    ClearSPLoadListEID();
                    spLoadListCount = 0;
                    UpdateSPLoadListsCount();
                    cmdAppendSP.Enabled = true;
                    cmdRemoveSP.Enabled = false;
                    txtSPLoadList.Enabled = false;
                }
            }
            // paste list
            else if (e.KeyCode == Keys.V && (e.Modifiers & Keys.Control) == Keys.Control && (e.Modifiers & Keys.Shift) == Keys.Shift)
            {
                bool wasEmpty = false;
                if (spLoadList.Count == 0)
                {
                    wasEmpty = true;
                }
                StringReader sr = new StringReader(Clipboard.GetText());
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (spLoadList.Count >= 39)
                    {
                        cmdAppendSP.Enabled = false;
                        break;
                    }
                    if (CheckEname(line).Length > 0)
                    {
                        spLoadList.Add(line);
                        ++spLoadListCount;
                        cmdRemoveSP.Enabled = true;
                        txtSPLoadList.Enabled = true;
                    }
                }
                UpdateSPLoadListsCount();
                UpdateSPLoadListsEID(false);
                if (spLoadList.Count > 0 && wasEmpty)
                {
                    lbSPLoadList.SelectedIndex = 0;
                    txtSPLoadList.Text = lbSPLoadList.SelectedItem.ToString();
                }
            }
            // copy selected item's eid
            else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                if (spLoadList.Count <= 0) return;
                var selectedItem = lbSPLoadList.SelectedItem;
                if (selectedItem == null) return;

                string s = selectedItem.ToString();
                Clipboard.SetDataObject(s, true, 10, 100);
            }
            // paste eid to selected item
            else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                if (spLoadList.Count <= 0) return;
                var selectedItem = lbSPLoadList.SelectedItem;
                if (selectedItem == null) return;

                string s = Clipboard.GetText();
                if (CheckEname(s).Length > 0)
                {
                    int selctedIndex = lbSPLoadList.SelectedIndex;
                    spLoadList[selctedIndex] = s;
                    UpdateSPLoadListsEID(false);
                }
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (spLoadList.Count <= 0) return;
                txtSPLoadList.Focus();
                txtSPLoadList.SelectAll();
            }
            dirty.Pop();
        }

        private void lbSPLoadList_DoubleClick(object sender, EventArgs e)
        {
            if (spLoadList.Count <= 0) return;
            txtSPLoadList.Focus();
            txtSPLoadList.SelectAll();
        }

        public static string CheckEname(string ename)
        {
            if (ename.Length != 5)
            {
                return string.Empty;
            }
            int eid = Entry.NullEID;
            try
            {
                eid = Entry.ENameToEID(ename);
            }
            catch (ArgumentException)
            {
                return string.Empty;
            }
            return ename;
        }

        private void txtSPLoadList_TextChanged(object sender, EventArgs e)
        {
            if (Dirty || lbSPLoadList.SelectedIndex < 0) return;

            lblEIDErrorSP.Visible = true;
            lblEIDErrorSP.Text = Entry.CheckEIDErrors(txtSPLoadList.Text, true);
            if (lblEIDErrorSP.Text != string.Empty) return;

            int value = Entry.ENameToEID(txtSPLoadList.Text);
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Copy(bytes, 0, header.Chunk1, 0xC + lbSPLoadList.SelectedIndex * 0x4, 0x4);

            int selectedIndex = lbSPLoadList.SelectedIndex;
            spLoadList[selectedIndex] = txtSPLoadList.Text;
        }

        private bool HexView_DataChangeHandler(int destOffset, int destLength, byte[] source)
        {
            var data = header.Data;

            if (destLength != source.Length)
                throw new ArgumentException();
            if (destOffset < 0 || destOffset >= data.Length)
                throw new ArgumentException();

            Array.Copy(source, 0, data, destOffset, destLength);
            header = header.IsNew ? ZoneHeader.LoadNew(header.Data) : ZoneHeader.Load(header.Data);
            return true;
        }

        private void txtSPLoadList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lbSPLoadList.Focus();
            }
        }

        private void cmdAppendZone_Click(object sender, EventArgs e)
        {
            currentDataGridView = dgvZones;
            AppendRowItem_Click(sender, e);
        }

        private void cmdRemoveZone_Click(object sender, EventArgs e)
        {
            currentDataGridView = dgvZones;
            DeleteRowItem_Click(sender, e);
        }

        private void cmdAppendWorld_Click(object sender, EventArgs e)
        {
            currentDataGridView = dgvWorlds;
            AppendRowItem_Click(sender, e);
        }

        private void cmdRemoveWorld_Click(object sender, EventArgs e)
        {
            currentDataGridView = dgvWorlds;
            DeleteRowItem_Click(sender, e);
        }
    }
}
