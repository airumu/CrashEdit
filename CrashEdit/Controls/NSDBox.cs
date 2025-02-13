using System.Text.RegularExpressions;
using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public partial class NSDBox : UserControl
    {
        private NSF NSF { get; }
        private NSD NSD { get; }
        private NSDController NSDController { get; }
        private string FileName { get; }

        private DarkToolTip tipReload;

        private int rowIndexFromMouseDown = -1;
        private int rowIndexToDrop = -1;
        private Point mouseDownPoint = Point.Empty;
        private Pen dropLinePen = new Pen(Color.DarkTurquoise, 2);

        private readonly int ColZoneEID = 0;
        private readonly int ColCamera = 1;
        private readonly int ColPoint = 2;
        private readonly int ColSpawnX = 3;
        private readonly int ColSpawnY = 4;
        private readonly int ColSpawnZ = 5;

        internal Stack<bool> dirty = new Stack<bool>();
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        public NSDBox(NSDController nsdController)
        {
            NSDController = nsdController;
            NSD = NSDController.NSD;
            NSF = nsdController.GetNSF();
            FileName = nsdController.GetFileName();
            InitializeComponent();
            MainInit();
        }

        private void MainInit()
        {
            dirty.Push(true);

            DoubleBufferedDataGridView.Initialize(dgvSpawns);
            CreateSpawnsColumns();
            UpdateSpawnPoint();
            txtID.Text = NSD.ID.ToString("X2");
            lblEntityCount.Text = NSD.EntityCount.ToString();

            tipReload = new DarkToolTip();
            tipReload.SetToolTip(rbtReload, "Reload");

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem appendRowItem = new ToolStripMenuItem("Append Row");
            ToolStripMenuItem deleteRowItem = new ToolStripMenuItem("Delete Row");
            appendRowItem.Click += AppendRowItem_Click;
            deleteRowItem.Click += DeleteRowItem_Click;
            contextMenu.Items.Add(appendRowItem);
            contextMenu.Items.Add(deleteRowItem);
            dgvSpawns.ContextMenuStrip = contextMenu;
            dgvSpawns.CellMouseDown += new DataGridViewCellMouseEventHandler(dgvSpawns_CellMouseDown);

            dirty.Pop();
        }

        private void dgvSpawns_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    dgvSpawns.ClearSelection();
                    dgvSpawns.Rows[e.RowIndex].Selected = true;
                    dgvSpawns.CurrentCell = dgvSpawns.Rows[e.RowIndex].Cells[0];
                }
            }
        }

        private void AppendRowItem_Click(object? sender, EventArgs e)
        {
            if (dgvSpawns.Rows.Count >= int.MaxValue)
            {
                DarkMessageBox.ShowError($"You cannot add more than {int.MaxValue} rows.", Resources.Title_Error);
                return;
            }

            dgvSpawns.Rows.Add(Entry.NullEName, 0, 0, 0, 0, 0);
            NSD.Spawns.Add(new NSDSpawnPoint(Entry.NullEID, 0, 0, 0, 0, 0));
        }

        private void DeleteRowItem_Click(object? sender, EventArgs e)
        {
            if (!(dgvSpawns.SelectedCells.Count > 0)) return;

            if (dgvSpawns.Rows.Count == 1)
            {
                DarkMessageBox.ShowError("There must be at least one row.", Resources.Title_Error);
                return;
            }

            int idx = dgvSpawns.SelectedCells[0].RowIndex;
            dgvSpawns.Rows.RemoveAt(idx);
            NSD.Spawns.RemoveAt(idx);
        }

        private void CreateSpawnsColumns()
        {
            dgvSpawns.Columns.Add("EID", "EID");
            dgvSpawns.Columns.Add("Camera", "Camera");
            dgvSpawns.Columns.Add("Point", "Point");
            dgvSpawns.Columns.Add("X", "X");
            dgvSpawns.Columns.Add("Y", "Y");
            dgvSpawns.Columns.Add("Z", "Z");

            foreach (DataGridViewColumn column in dgvSpawns.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            dgvSpawns.Columns[ColZoneEID].Width = 60;
            dgvSpawns.Columns[ColCamera].Width = 60;
            dgvSpawns.Columns[ColPoint].Width = 60;
            dgvSpawns.Columns[ColSpawnX].Width = 72;
            dgvSpawns.Columns[ColSpawnY].Width = 72;
            dgvSpawns.Columns[ColSpawnZ].Width = 72;
        }

        private void UpdateSpawnPoint()
        {
            dgvSpawns.ClearSelection();
            dgvSpawns.Rows.Clear();
            foreach (var spawn in NSD.Spawns)
            {
                DataGridViewRow row = new();
                row.CreateCells(dgvSpawns, Entry.EIDToEName(spawn.ZoneEID), spawn.Camera.ToString(), spawn.Point.ToString(), spawn.SpawnX.ToString("X"), spawn.SpawnY.ToString("X"), spawn.SpawnZ.ToString("X"));
                dgvSpawns.Rows.Add(row);
            }
        }

        private void dgvSpawns_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!(dgvSpawns.SelectedCells.Count > 0)) return;
            string? inputValue = e.FormattedValue?.ToString();

            if (e.ColumnIndex == ColZoneEID)
            {
                string checkEID = Entry.CheckEIDErrors(inputValue, true);
                if (checkEID != string.Empty)
                {
                    DarkMessageBox.ShowError($"Invalid EID; {checkEID}", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
            else if (e.ColumnIndex == ColCamera || e.ColumnIndex == ColPoint)
            {
                if (int.TryParse(inputValue, out int newValue))
                {
                    return;
                }
                DarkMessageBox.ShowError($"Invalid input value.", Resources.Title_InputError);
                e.Cancel = true;
            }
            else
            {
                if (int.TryParse(inputValue, System.Globalization.NumberStyles.HexNumber, null, out int newValue))
                {
                    return;
                }
                DarkMessageBox.ShowError($"Invalid input value.", Resources.Title_InputError);
                e.Cancel = true;
            }
        }

        private void dgvSpawns_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Dirty || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var cell = dgvSpawns.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
            if (cell == null)
            {
                DarkMessageBox.ShowError("Cell value cannot be null.", Resources.Title_InputError);
                return;
            }

            var og = NSD.Spawns[e.RowIndex];
            switch (e.ColumnIndex)
            {
                case 0: // ZoneEID
                    og.ZoneEID = Entry.ENameToEID(cell);
                    break;
                case 1: // Camera
                    og.Camera = Convert.ToInt32(cell);
                    break;
                case 2: // Point
                    og.Point = Convert.ToInt32(cell);
                    break;
                case 3: // SpawnX
                    og.SpawnX = Convert.ToInt32(cell, 16);
                    break;
                case 4: // SpawnY
                    og.SpawnY = Convert.ToInt32(cell, 16);
                    break;
                case 5: // SpawnZ
                    og.SpawnZ = Convert.ToInt32(cell, 16);
                    break;
            }
        }

        private void cmdGetSpawn_Click(object sender, EventArgs e)
        {
            if (!(dgvSpawns.SelectedCells.Count > 0)) return;
            var row = dgvSpawns.Rows[dgvSpawns.SelectedCells[0].RowIndex];

            using (InputWindow inputWindow = new InputWindow(Resources.GenerateSpawnPoint_Title, "Calculator", "Enter entity ID:", string.Empty, 4))
            {
                if (inputWindow.ShowDialog() == DialogResult.OK)
                {
                    string input = inputWindow.Input;
                    if (string.IsNullOrEmpty(input)) return;

                    if (int.TryParse(input, out int targetID))
                    {
                        foreach (ZoneEntry entry in NSF.GetEntries<ZoneEntry>())
                        {
                            foreach (Entity entity in entry.Entities)
                            {
                                if (entity.ID == targetID)
                                {
                                    int zone = entry.EID;
                                    int cameraIdx = 0;
                                    string cameraIndex = string.Empty;
                                    if (entry.CameraCount > 3)
                                    {
                                        int cameraMaxIdx = (entry.CameraCount - 1) / 3;
                                        using (InputWindow inputWindows = new InputWindow(Resources.GenerateSpawnPoint_Title, "Calculator", $"Enter camera index [0-{cameraMaxIdx}]:", "0", 2))
                                        {
                                            if (inputWindows.ShowDialog() == DialogResult.OK)
                                            {
                                                bool valid = false;
                                                if (int.TryParse(inputWindows.Input, out cameraIdx))
                                                {
                                                    if (cameraIdx >= 0 && cameraIdx <= cameraMaxIdx)
                                                    {
                                                        valid = true;
                                                        cameraIndex = $" [Camera: {cameraIdx}]";
                                                    }
                                                }

                                                if (!valid)
                                                {
                                                    DarkMessageBox.ShowError("Invalid camera index.", Resources.GenerateSpawnPoint_Title);
                                                    return;
                                                }
                                            }
                                            else return;
                                        }
                                    }
                                    int x = (entry.X + 4 * entity.Positions[0].X) << 8;
                                    int y = (entry.Y + 4 * entity.Positions[0].Y) << 8;
                                    int z = (entry.Z + 4 * entity.Positions[0].Z) << 8;

                                    row.Cells[ColZoneEID].Value = Entry.EIDToEName(zone);
                                    row.Cells[ColCamera].Value = cameraIdx;
                                    row.Cells[ColSpawnX].Value = x.ToString("X");
                                    row.Cells[ColSpawnY].Value = y.ToString("X");
                                    row.Cells[ColSpawnZ].Value = z.ToString("X");
                                    return;
                                }
                            }
                        }
                        DarkMessageBox.ShowError("Entity not found.", Resources.GenerateSpawnPoint_Title);
                        return;
                    }
                    else
                    {
                        DarkMessageBox.ShowError("Invalid entity ID.", Resources.GenerateSpawnPoint_Title);
                        return;
                    }
                }
            }
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            if (!(dgvSpawns.SelectedCells.Count > 0)) return;
            var row = dgvSpawns.Rows[dgvSpawns.SelectedCells[0].RowIndex];

            byte[] data = new byte[24];
            BitConv.ToInt32(data, 0, Entry.ENameToEID(row.Cells[ColZoneEID].Value.ToString()));
            BitConv.ToInt32(data, 4, Convert.ToInt32(row.Cells[ColCamera].Value.ToString()));
            BitConv.ToInt32(data, 8, Convert.ToInt32(row.Cells[ColPoint].Value.ToString()));
            BitConv.ToInt32(data, 12, Convert.ToInt32(row.Cells[ColSpawnX].Value.ToString(), 16));
            BitConv.ToInt32(data, 16, Convert.ToInt32(row.Cells[ColSpawnY].Value.ToString(), 16));
            BitConv.ToInt32(data, 20, Convert.ToInt32(row.Cells[ColSpawnZ].Value.ToString(), 16));
            string result = BitConverter.ToString(data).Replace("-", "");
            Clipboard.SetDataObject(result, true, 10, 100);
            Console.WriteLine("Copied to clipboard.");
        }

        private void cmdPaste_Click(object sender, EventArgs e)
        {
            if (!(dgvSpawns.SelectedCells.Count > 0)) return;
            var row = dgvSpawns.Rows[dgvSpawns.SelectedCells[0].RowIndex];

            string str = Clipboard.GetText();
            bool isHex = Regex.IsMatch(str, @"\A\b[0-9A-Fa-f]+\b\Z");
            if (str.Length != 48 || !isHex)
            {
                DarkMessageBox.ShowError("Invalid spawn point structure.", Resources.Title_Error);
                return;
            }
            byte[] bytes = Enumerable.Range(0, str.Length / 2)
                                     .Select(i => Convert.ToByte(str.Substring(i * 2, 2), 16))
                                     .ToArray();

            row.Cells[ColZoneEID].Value = Entry.EIDToEName(Convert.ToInt32(BitConv.FromInt32(bytes, 0)));
            row.Cells[ColCamera].Value = BitConv.FromInt32(bytes, 4).ToString();
            row.Cells[ColPoint].Value = BitConv.FromInt32(bytes, 8).ToString();
            row.Cells[ColSpawnX].Value = BitConv.FromInt32(bytes, 12).ToString("X");
            row.Cells[ColSpawnY].Value = BitConv.FromInt32(bytes, 16).ToString("X");
            row.Cells[ColSpawnZ].Value = BitConv.FromInt32(bytes, 20).ToString("X");
            Console.WriteLine("Pasted from clipboard.");
        }

        private void dgvSpawns_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownPoint = e.Location;
            rowIndexFromMouseDown = dgvSpawns.HitTest(e.X, e.Y).RowIndex;
        }

        private void dgvSpawns_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int distance = Math.Abs(e.X - mouseDownPoint.X) + Math.Abs(e.Y - mouseDownPoint.Y);
                if (distance > SystemInformation.DragSize.Width / 2 && rowIndexFromMouseDown >= 0)
                {
                    dgvSpawns.DoDragDrop(dgvSpawns.Rows[rowIndexFromMouseDown], DragDropEffects.Move);
                    mouseDownPoint = Point.Empty;
                }
            }
        }

        private void dgvSpawns_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;

            Point clientPoint = dgvSpawns.PointToClient(new Point(e.X, e.Y));
            int newRowIndex = dgvSpawns.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (newRowIndex >= 0 && newRowIndex != rowIndexToDrop)
            {
                dgvSpawns.ClearSelection();
                dgvSpawns.Rows[rowIndexFromMouseDown].Selected = true;
                rowIndexToDrop = newRowIndex;
                dgvSpawns.Invalidate();
            }
        }

        private void dgvSpawns_Paint(object sender, PaintEventArgs e)
        {
            if (rowIndexToDrop >= 0)
            {
                Rectangle rowRect = dgvSpawns.GetRowDisplayRectangle(rowIndexToDrop, true);
                e.Graphics.DrawLine(dropLinePen, rowRect.Left, rowRect.Bottom, rowRect.Right, rowRect.Bottom);
            }
        }

        private void dgvSpawns_DragDrop(object sender, DragEventArgs e)
        {
            Point clientPoint = dgvSpawns.PointToClient(new Point(e.X, e.Y));
            int dropIndex = dgvSpawns.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (rowIndexFromMouseDown >= 0 && dropIndex >= 0 && rowIndexFromMouseDown != dropIndex)
            {
                DataGridViewRow row = dgvSpawns.Rows[rowIndexFromMouseDown];
                NSDSpawnPoint spawn = NSD.Spawns[rowIndexFromMouseDown];
                dgvSpawns.Rows.RemoveAt(rowIndexFromMouseDown);
                dgvSpawns.Rows.Insert(dropIndex, row);

                NSD.Spawns.RemoveAt(rowIndexFromMouseDown);
                NSD.Spawns.Insert(dropIndex, spawn);

                BeginInvoke(new Action(() =>
                {
                    dgvSpawns.ClearSelection();
                    dgvSpawns.Rows[dropIndex].Selected = true;
                    dgvSpawns.CurrentCell = dgvSpawns.Rows[dropIndex].Cells[0];
                }));
            }

            rowIndexFromMouseDown = -1;
            rowIndexToDrop = -1;
            dgvSpawns.Invalidate();
        }

        private void dgvSpawns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                cmdCopy.PerformClick();
                e.Handled = true; // To prevent copying the cell value.
            }
            else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                cmdPaste.PerformClick();
            }
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            string filteredText = new string(txtID.Text.Where(c => Uri.IsHexDigit(c)).ToArray());
            if (txtID.Text != filteredText)
            {
                txtID.Text = filteredText;
                txtID.SelectionStart = txtID.Text.Length;
            }

            string text = txtID.Text.ToUpper();
            txtID.Text = text;
            txtID.SelectionStart = txtID.Text.Length;
        }

        private void UpdateID()
        {
            if (Dirty) return;

            string text = txtID.Text;
            if (string.IsNullOrEmpty(text))
            {
                text = "00";
            }
            if (text.Length == 1)
            {
                text = "0" + text;
            }
            txtID.Text = text;

            NSD.ID = Convert.ToInt32(txtID.Text, 16);
        }

        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateID();
            }
        }

        private void txtID_LostFocus(object sender, EventArgs e)
        {
            UpdateID();
        }

        private void rbtReload_Click(object sender, EventArgs e)
        {
            UpdateSpawnPoint();
            txtID.Text = NSD.ID.ToString("X2");
            lblEntityCount.Text = NSD.EntityCount.ToString();

            rbtReload.Checked = false;
        }
    }
}
