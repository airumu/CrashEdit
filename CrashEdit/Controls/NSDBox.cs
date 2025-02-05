using AltUI.Forms;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public partial class NSDBox : UserControl
    {
        public LevelWorkspace Workspace { get; }
        public NSF NSF { get; }
        public NSD NSD { get; }
        public NSDController NSDController { get; }

        private readonly int ColZoneEID = 0;
        private readonly int ColCamera = 1;
        private readonly int ColUnknown = 2;
        private readonly int ColSpawnX = 3;
        private readonly int ColSpawnY = 4;
        private readonly int ColSpawnZ = 5;

        public NSDBox(IUserInterface ui, LevelWorkspace ws)
        {
            Workspace = ws;
            NSD = ws.NSD;
            NSF = ws.NSF;
            //NSDController = (NSDController)RootController.SubcontrollerGroups[0].Members[0].Legacy;
        }

        public NSDBox(NSDController nsdController)
        {
            NSDController = nsdController;
            NSD = NSDController.NSD;
            InitializeComponent();

            UpdateSpawnPoint();
            txtID.Text = NSD.ID.ToString("X2");
        }

        private void UpdateSpawnPoint()
        {
            DoubleBufferedDataGridView.Initialize(dgvSpawns);
            dgvSpawns.Columns.Add("EID", "EID");
            dgvSpawns.Columns.Add("Camera", "Camera");
            dgvSpawns.Columns.Add("Unknown", "Unknown");
            dgvSpawns.Columns.Add("X", "X");
            dgvSpawns.Columns.Add("Y", "Y");
            dgvSpawns.Columns.Add("Z", "Z");

            dgvSpawns.Columns[ColUnknown].Visible = false;

            foreach (var spawn in NSD.Spawns)
            {
                DataGridViewRow row = new();
                row.CreateCells(dgvSpawns, Entry.EIDToEName(spawn.ZoneEID), spawn.Camera, spawn.Unknown, spawn.SpawnX.ToString("X"), spawn.SpawnY.ToString("X"), spawn.SpawnZ.ToString("X"));
                dgvSpawns.Rows.Add(row);
            }
            foreach (DataGridViewColumn column in dgvSpawns.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 60;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private readonly string Title_InputError = "Input Error";
        private readonly string GenerateSpawnPoint_Title = "Generate Spawn Point";

        private void dgvSpawns_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!(dgvSpawns.SelectedCells.Count > 0)) return;
            string inputValue = e.FormattedValue.ToString();

            if (e.ColumnIndex == 0)
            {
                string checkEID = Entry.CheckEIDErrors(inputValue, true);
                if (checkEID != string.Empty)
                {
                    DarkMessageBox.ShowError($"Invalid EID '{inputValue}'. {checkEID}", Title_InputError);
                    e.Cancel = true;
                }
            }
            else
            {
                if (int.TryParse(inputValue, System.Globalization.NumberStyles.HexNumber, null, out int newValue))
                {
                    int maxValue = int.MaxValue;
                    int minValue = int.MinValue;
                    if (newValue > maxValue)
                    {
                        DarkMessageBox.ShowError($"The value must be less than or equal to {maxValue}.", Title_InputError);
                        e.Cancel = true;
                    }
                    else if (newValue < minValue)
                    {
                        DarkMessageBox.ShowError($"The value must be greater than or equal to {minValue}.", Title_InputError);
                        e.Cancel = true;
                    }
                }
                else
                {
                    DarkMessageBox.ShowError($"Invalid input.", Title_InputError);
                    e.Cancel = true;
                }
            }
        }

        private void dgvSpawns_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var row = dgvSpawns.Rows[e.RowIndex];
            var cell = row.Cells[e.ColumnIndex].Value;
            var og = NSD.Spawns[e.RowIndex];

            switch (e.ColumnIndex)
            {
                case 0: // ZoneEID
                    og.ZoneEID = Entry.ENameToEID(cell.ToString());
                    break;
                case 1: // Camera
                    og.Camera = Convert.ToInt32(cell.ToString(), 16);
                    break;
                case 2: // Unknown
                    og.Unknown = Convert.ToInt32(cell.ToString(), 16);
                    break;
                case 3: // SpawnX
                    og.SpawnX = Convert.ToInt32(cell.ToString(), 16);
                    break;
                case 4: // SpawnY
                    og.SpawnY = Convert.ToInt32(cell.ToString(), 16);
                    break;
                case 5: // SpawnZ
                    og.SpawnZ = Convert.ToInt32(cell.ToString(), 16);
                    break;
            }
        }

        private void cmdGetSpawn_Click(object sender, EventArgs e)
        {
            if (!(dgvSpawns.SelectedCells.Count > 0)) return;

            var row = dgvSpawns.Rows[dgvSpawns.SelectedCells[0].RowIndex];

            using (InputWindow inputWindow = new InputWindow("Enter entity ID:", GenerateSpawnPoint_Title, string.Empty))
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
                                        using (InputWindow inputWindows = new InputWindow($"Enter camera index [0-{cameraMaxIdx}]:", GenerateSpawnPoint_Title, "0"))
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
                                                    DarkMessageBox.ShowError("Invalid camera index.", GenerateSpawnPoint_Title);
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
                        DarkMessageBox.ShowError("Entity not found.", GenerateSpawnPoint_Title);
                        return;
                    }
                    else
                    {
                        DarkMessageBox.ShowError("Invalid entity ID.", GenerateSpawnPoint_Title);
                        return;
                    }
                }
            }
        }
    }
}
