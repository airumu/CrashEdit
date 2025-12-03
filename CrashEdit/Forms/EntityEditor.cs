using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using SelectionMode = System.Windows.Forms.SelectionMode;

namespace CrashEdit.CE.Forms
{
    public partial class EntityEditor : DarkForm
    {
        public NSF nsf;
        public ZoneEntry? zone;
        public Entity? entity;

        private int positionindex = 0;
        private int settingindex = 0;
        private int victimindex;
        private int victimlistindex => lbVictimID.SelectedIndex;

        private bool syncEditEnabled => tglSyncEdit.Switched;

        private List<string> originalItems = [];
        private int _lastSelectedZoneIndex = -1;

        private DarkForm? syncListForm;
        private List<string>? syncEntityList;

        private Dictionary<int, string> GOOLList = [];
        private readonly List<(int? Id, string? Name)> searchResults = [];
        private int searchIndex = 0;

        private DarkToolTip tipVictim;
        private DarkToolTip tipVictimDistance;
        private DarkToolTip tipOverrideId;
        private DarkToolTip tipOverrideMult;

        private DDAEditor? frmDDAEditor;
        private ZoneEntryViewer? zoneEntryViewer;

        private readonly int ColIndex = 0;
        private readonly int ColName = 1;
        private readonly int ColID = 2;
        private readonly int ColType = 3;
        private readonly int ColSubtype = 4;

        internal Stack<bool> dirty = [];
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        public EntityEditor(NSF nsf)
        {
            this.nsf = nsf;
            InitializeComponent();
            Icon = Embeds.GetIcon("Modify");
            KeyPreview = true;

            DoubleBufferedDataGridView.Initialize(dgvEntities);
            dgvEntitiesInit();

            lbZones.Font = new Font("Cascadia Code SemiLight", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkShowCameras.Checked = Settings.Default.ViewCamera;
            tslSearch.Image = Embeds.GetIcon("Find").ToBitmap();
            tsbEditDDA.Image = Embeds.GetIcon("List").ToBitmap();

            ContextMenuStrip contextMenuStrip = new();
            contextMenuStrip.Items.Add("Delete Entity", Embeds.GetIcon("Erase").ToBitmap(), MenuDeleteEntity);
            contextMenuStrip.Items.Add("Duplicate Entity", Embeds.GetIcon("Copy").ToBitmap(), MenuDuplicateEntity);
            contextMenuStrip.Items.Add("Add Entity", Embeds.GetIcon("Add").ToBitmap(), MenuAddEntity);
            contextMenuStrip.Items.Add("-");
            contextMenuStrip.Items.Add("Copy Positions", Embeds.GetIcon("List").ToBitmap(), MenuCopyPositions);
            contextMenuStrip.Items.Add("Paste Positions (Overwrite)", Embeds.GetIcon("Paste").ToBitmap(), MenuPastePositionsOverwrite);
            contextMenuStrip.Items.Add("Paste Positions (Apply Delta)", Embeds.GetIcon("Paste").ToBitmap(), MenuPastePositionsApplyDelta);
            contextMenuStrip.Items.Add("-");
            contextMenuStrip.Items.Add("Copy Arguments", Embeds.GetIcon("List").ToBitmap(), MenuCopyArguments);
            contextMenuStrip.Items.Add("Paste Arguments", Embeds.GetIcon("Paste").ToBitmap(), MenuPasteArguments);
            contextMenuStrip.Opening += ContextMenuStrip_Opening;
            dgvEntities.ContextMenuStrip = contextMenuStrip;

            numID.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);
            numType.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);
            numSubtype.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);

            dirty.Push(true);
            GetZones();
            //UpdateEntityList();
            pnProperties.Enabled = false;
            fraC2TTSet.Visible = Settings.Default.EnableC2TTEditor;
            tabSpecialInit();
            GetGOOL();
            dirty.Pop();
        }

        private void EntityEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.G) // Goto
            {
                FindEntityByID();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.F) // Find
            {
                tstSearch.Focus();
                tstSearch.SelectAll();
                e.Handled = true;
            }
            else if (e.Shift && e.KeyCode == Keys.F3) // Find Previous
            {
                if (searchResults.Count == 0) return;
                if (searchIndex == 0)
                {
                    DarkMessageBox.ShowError("No results before the current selection.", "Find");
                    return;
                }
                searchIndex--;
                MoveSearchResult();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F3) // Find Next
            {
                if (searchResults.Count == 0) return;
                if (searchIndex == searchResults.Count - 1)
                {
                    DarkMessageBox.ShowError("No results after the current selection.", "Find");
                    return;
                }
                searchIndex++;
                MoveSearchResult();
                e.Handled = true;
            }
        }

        private void tstSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                FindEntityByName();
                if (searchResults.Count == 0) return;
                MoveSearchResult();
            }
        }

        private void FindEntityByName()
        {
            searchIndex = 0;
            searchResults.Clear();
            string query = tstSearch.Text;
            if (query == string.Empty) return;

            foreach (ZoneEntry zone in nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity otherentity in zone.Entities)
                {
                    if (otherentity.Name == null || otherentity.ID == null) continue;

                    try
                    {
                        if (Regex.IsMatch(otherentity.Name, query, RegexOptions.IgnoreCase))
                        {
                            searchResults.Add((otherentity.ID, otherentity.Name));
                        }
                    }
                    catch (ArgumentException)
                    {
                    }
                }
            }

            if (searchResults.Count == 0)
            {
                DarkMessageBox.ShowError("No results found.", "Find");
            }
        }

        private void MoveSearchResult()
        {
            var target = searchResults[searchIndex];
            int? targetID = target.Id;
            string? targetName = target.Name;

            foreach (ZoneEntry zone in nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity otherentity in zone.Entities)
                {
                    if (otherentity.Name == null || otherentity.ID == null) continue;
                    if (otherentity.Name == targetName && otherentity.ID == targetID)
                    {
                        dirty.Push(true);
                        this.zone = zone;

                        lbZones.BeginUpdate();
                        lbZones.SelectedItem = zone.EName;
                        lbZones.EndUpdate();

                        int newIndex = lbZones.SelectedIndex;
                        if (newIndex != _lastSelectedZoneIndex)
                        {
                            _lastSelectedZoneIndex = newIndex;
                            UpdateEntityList();
                            ShowZone(true);
                        }
                        dirty.Pop();

                        foreach (DataGridViewRow row in dgvEntities.Rows)
                        {
                            if (row.Cells[ColName].Value?.ToString() == targetName && row.Cells[ColID].Value?.ToString() == targetID.ToString())
                            {
                                row.Selected = true;
                                dgvEntities.CurrentCell = row.Cells[ColName];
                            }
                        }
                        return;
                    }
                }
            }
        }

        private void FindEntityByID()
        {
            using (InputWindow inputWindow = new("Find", "Find", "Enter an entity ID:", string.Empty, 4))
            {
                if (inputWindow.ShowDialog() == DialogResult.OK)
                {
                    string input = inputWindow.Input;
                    if (int.TryParse(input, System.Globalization.NumberStyles.Integer, null, out int value))
                    {
                        foreach (ZoneEntry zone in nsf.GetEntries<ZoneEntry>())
                        {
                            foreach (Entity otherentity in zone.Entities)
                            {
                                if (otherentity.ID == null) continue;
                                if (otherentity.ID.Value == value)
                                {
                                    dirty.Push(true);
                                    this.zone = zone;

                                    lbZones.BeginUpdate();
                                    lbZones.SelectedItem = zone.EName;
                                    lbZones.EndUpdate();

                                    int newIndex = lbZones.SelectedIndex;
                                    if (newIndex != _lastSelectedZoneIndex)
                                    {
                                        _lastSelectedZoneIndex = newIndex;
                                        UpdateEntityList();
                                        ShowZone(true);
                                    }
                                    dirty.Pop();

                                    foreach (DataGridViewRow row in dgvEntities.Rows)
                                    {
                                        if (row.Cells[ColID].Value?.ToString() == value.ToString())
                                        {
                                            row.Selected = true;
                                            dgvEntities.CurrentCell = row.Cells[ColName];
                                        }
                                    }
                                    return;
                                }
                            }
                        }
                        DarkMessageBox.ShowError("No results found.", "Find");
                        return;
                    }
                    else
                    {
                        DarkMessageBox.ShowError("Invalid input.", "Find");
                        return;
                    }
                }
            }
        }

        public void FindEntityFromDDAList(Entity entity)
        {
            foreach (ZoneEntry zone in nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity otherentity in zone.Entities)
                {
                    if (otherentity == entity)
                    {
                        dirty.Push(true);
                        this.zone = zone;

                        lbZones.BeginUpdate();
                        lbZones.SelectedItem = zone.EName;
                        lbZones.EndUpdate();

                        int newIndex = lbZones.SelectedIndex;
                        if (newIndex != _lastSelectedZoneIndex)
                        {
                            _lastSelectedZoneIndex = newIndex;
                            UpdateEntityList();
                            ShowZone(false);
                        }
                        dirty.Pop();

                        foreach (DataGridViewRow row in dgvEntities.Rows)
                        {
                            if (row.Cells[ColName].Value?.ToString() == entity.Name && row.Cells[ColID].Value?.ToString() == entity.ID.ToString())
                            {
                                row.Selected = true;
                                dgvEntities.CurrentCell = row.Cells[ColName];
                            }
                        }
                        if (frmDDAEditor != null)
                            frmDDAEditor.Focus();
                        return;
                    }
                }
            }
        }

        private void MenuDeleteEntity(object sender, EventArgs e)
        {
            dirty.Push(true);

            int index = -1;
            int entityIdx = zone.Entities.IndexOf(entity);
            if (entityIdx < zone.CameraCount)
            {
                --zone.CameraCount;
            }
            else
            {
                index = entityIdx - zone.CameraCount;
                --zone.EntityCount;
            }

            zone.Entities.RemoveAt(entityIdx);

            int rowIdx = dgvEntities.SelectedCells[0].RowIndex;
            dgvEntities.Rows.RemoveAt(rowIdx);
            for (int i = 0; i < dgvEntities.Rows.Count; i++)
            {
                dgvEntities.Rows[i].Cells[ColIndex].Value = i + zone.CameraCount;
            }
            UpdateOtherEditors();

            if (entity.ID.HasValue)
            {
                foreach (var zone in nsf.GetEntries<ZoneEntry>())
                {
                    int zoneindex = -1;
                    for (int z = 0, s = zone.ZoneCount; z < s; ++z)
                    {
                        if (zone.GetLinkedZone(z) == zone.EID)
                        {
                            zoneindex = z;
                            break;
                        }
                    }
                    foreach (var otherentity in zone.Entities)
                    {
                        if (otherentity.DrawListA != null)
                        {
                            foreach (var row in otherentity.DrawListA.Rows)
                            {
                                for (int i = row.Values.Count - 1; i >= 0; --i)
                                {
                                    if ((row.Values[i] & 0xFFFF00) >> 8 == entity.ID.Value)
                                        row.Values.RemoveAt(i);
                                    else if ((row.Values[i] & 0xFF) == zoneindex && ((row.Values[i] & 0xFF000000) >> 24) > index)
                                    {
                                        int newindex = (int)(row.Values[i] & 0xFF000000) >> 24;
                                        row.Values[i] &= 0xFFFFFF;
                                        row.Values[i] |= --newindex << 24;
                                    }
                                }
                            }
                        }
                        if (otherentity.DrawListB != null)
                        {
                            foreach (var row in otherentity.DrawListB.Rows)
                            {
                                for (int i = row.Values.Count - 1; i >= 0; --i)
                                {
                                    if ((row.Values[i] & 0xFFFF00) >> 8 == entity.ID.Value)
                                        row.Values.RemoveAt(i);
                                    else if ((row.Values[i] & 0xFF) == zoneindex && ((row.Values[i] & 0xFF000000) >> 24) > index)
                                    {
                                        int newindex = (int)(row.Values[i] & 0xFF000000) >> 24;
                                        row.Values[i] &= 0xFFFFFF;
                                        row.Values[i] |= --newindex << 24;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            dirty.Pop();
        }

        private void MenuDuplicateEntity(object sender, EventArgs e)
        {
            if (!entity.ID.HasValue)
            {
                throw new GUIException("Only entities with ID's can be duplicated.");
            }
            int maxid = 1;
            List<EntityPropertyRow<int>> drawlists = [];
            foreach (ZoneEntry zone in nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity otherentity in zone.Entities)
                {
                    if (otherentity.ID.HasValue)
                    {
                        if (otherentity.ID.Value > maxid)
                        {
                            maxid = otherentity.ID.Value;
                        }
                    }
                    if (otherentity.DrawListA != null)
                    {
                        drawlists.AddRange(otherentity.DrawListA.Rows);
                    }
                    if (otherentity.DrawListB != null)
                    {
                        drawlists.AddRange(otherentity.DrawListB.Rows);
                    }
                }
            }
            maxid++;
            int newindex = zone.Entities.Count - zone.CameraCount;

            dirty.Push(true);
            ++zone.EntityCount;
            Entity newentity = Entity.Load(entity.Save());
            newentity.ID = maxid;
            newentity.AlternateID = null;
            zone.Entities.Add(newentity);
            foreach (EntityPropertyRow<int> drawlist in drawlists)
            {
                foreach (int value in drawlist.Values)
                {
                    if ((value & 0xFFFF00) >> 8 == entity.ID.Value)
                    {
                        unchecked
                        {
                            drawlist.Values.Add((value & 0xFF) | (maxid << 8) | (newindex << 24));
                        }
                        break;
                    }
                }
                if (drawlist.Values.Contains(entity.ID.Value))
                {
                    drawlist.Values.Add(maxid);
                }
            }

            DataGridViewRow row = new();
            row.CreateCells(
                dgvEntities,
                zone.CameraCount + zone.EntityCount - 1,
                entity.Name != null ? entity.Name : "-",
                newentity.ID,
                entity.Type != null ? entity.Type : "-",
                entity.Subtype != null ? entity.Subtype : "-"
            );
            if (entity.Name == null)
                row.Cells[1].Tag = "null";
            dgvEntities.Rows.Add(row);
            dirty.Pop();
        }

        private void MenuAddEntity(object sender, EventArgs e)
        {
            int maxid = 1;
            List<EntityPropertyRow<int>> drawlists = [];
            foreach (ZoneEntry zone in nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity otherentity in zone.Entities)
                {
                    if (otherentity.ID.HasValue)
                    {
                        if (otherentity.ID.Value > maxid)
                        {
                            maxid = otherentity.ID.Value;
                        }
                    }
                    if (otherentity.DrawListA != null)
                    {
                        drawlists.AddRange(otherentity.DrawListA.Rows);
                    }
                    if (otherentity.DrawListB != null)
                    {
                        drawlists.AddRange(otherentity.DrawListB.Rows);
                    }
                }
            }
            maxid++;
            int newindex = zone.Entities.Count - zone.CameraCount;

            dirty.Push(true);
            ++zone.EntityCount;
            Entity newentity = Entity.Load(new Entity(new Dictionary<short, EntityProperty>()).Save());
            newentity.ID = maxid;
            zone.Entities.Add(newentity);

            DataGridViewRow row = new();
            row.CreateCells(
                dgvEntities,
                zone.CameraCount + zone.EntityCount - 1,
                "-",
                newentity.ID,
                "-",
                "-"
            );
            row.Cells[1].Tag = "null";
            dgvEntities.Rows.Add(row);
            dirty.Pop();
        }

        private void MenuCopyPositions(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(dgvEntities.Rows[dgvEntities.CurrentCell.RowIndex].Cells[ColIndex].Value);
            Entity entity = zone.Entities[index];

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < entity.Positions.Count; i++)
            {
                sb.Append(entity.Positions[i].X);
                sb.Append(',');
                sb.Append(entity.Positions[i].Y);
                sb.Append(',');
                sb.Append(entity.Positions[i].Z);
                sb.AppendLine();
            }
            Clipboard.SetDataObject(sb.ToString(), true, 10, 100);
        }

        private void MenuPastePositionsOverwrite(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(dgvEntities.Rows[dgvEntities.CurrentCell.RowIndex].Cells[ColIndex].Value);

            string clipboardText = Clipboard.GetText();
            string[] lines = clipboardText.Split(["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries);

            List<short[]> numberSets = [];
            foreach (string line in lines)
            {
                string[] parts = line.Split([','], StringSplitOptions.RemoveEmptyEntries);
                short[] numbers;
                try
                {
                    numbers = parts.Select(p => short.Parse(p.Trim())).ToArray();
                }
                catch
                {
                    DarkMessageBox.ShowError($"Invalid number found in line: {line}", "Error");
                    return;
                }

                if (numbers.Length != 3)
                {
                    DarkMessageBox.ShowError($"Invalid length found in line: {line}", "Error");
                    return;
                }

                numberSets.Add(numbers);
            }
            if (numberSets.Count == 0) return;

            dirty.Push(true);
            Entity entity = zone.Entities[index];
            entity.Positions.Clear();
            for (int i = 0; i < numberSets.Count; i++)
            {
                entity.Positions.Add(new EntityPosition(numberSets[i][0], numberSets[i][1], numberSets[i][2]));
            }

            ApplyToSyncedEntities((other, rowIdx) =>
            {
                other.Positions.Clear();
                foreach (EntityPosition position in entity.Positions)
                {
                    other.Positions.Add(position);
                }
            });

            UpdatePosition();
            dirty.Pop();
        }

        private void MenuPastePositionsApplyDelta(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(dgvEntities.Rows[dgvEntities.CurrentCell.RowIndex].Cells[ColIndex].Value);
            Entity entity = zone.Entities[index];

            if (entity.Positions.Count == 0)
            {
                DarkMessageBox.ShowError($"The entity must have a position to apply a delta.", "Error");
                return;
            }

            string clipboardText = Clipboard.GetText();
            string[] lines = clipboardText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<short[]> numberSets = [];

            foreach (string line in lines)
            {
                string[] parts = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                short[] numbers;
                try
                {
                    numbers = parts.Select(p => short.Parse(p.Trim())).ToArray();
                }
                catch
                {
                    DarkMessageBox.ShowError($"Invalid number found in line: {line}", "Error");
                    return;
                }

                if (numbers.Length != 3)
                {
                    DarkMessageBox.ShowError($"Invalid length found in line: {line}", "Error");
                    return;
                }

                numberSets.Add(numbers);
            }
            if (numberSets.Count == 0) return;

            dirty.Push(true);
            EntityPosition basePos = entity.Positions[0];
            entity.Positions.Clear();

            short difX = (short)(Math.Clamp(Convert.ToInt16(basePos.X) - numberSets[0][0], short.MinValue, short.MaxValue));
            short difY = (short)(Math.Clamp(Convert.ToInt16(basePos.Y) - numberSets[0][1], short.MinValue, short.MaxValue));
            short difZ = (short)(Math.Clamp(Convert.ToInt16(basePos.Z) - numberSets[0][2], short.MinValue, short.MaxValue));
            for (int i = 0; i < numberSets.Count; i++)
            {
                entity.Positions.Add(new EntityPosition(
                    (short)(Math.Clamp(numberSets[i][0] + difX, short.MinValue, short.MaxValue)),
                    (short)(Math.Clamp(numberSets[i][1] + difY, short.MinValue, short.MaxValue)),
                    (short)(Math.Clamp(numberSets[i][2] + difZ, short.MinValue, short.MaxValue))
                    ));
            }

            ApplyToSyncedEntities((other, rowIdx) =>
            {
                if (other.Positions.Count == 0) return;

                EntityPosition basePos = other.Positions[0];
                other.Positions.Clear();

                short difX = (short)(Math.Clamp(Convert.ToInt16(basePos.X) - numberSets[0][0], short.MinValue, short.MaxValue));
                short difY = (short)(Math.Clamp(Convert.ToInt16(basePos.Y) - numberSets[0][1], short.MinValue, short.MaxValue));
                short difZ = (short)(Math.Clamp(Convert.ToInt16(basePos.Z) - numberSets[0][2], short.MinValue, short.MaxValue));
                for (int i = 0; i < numberSets.Count; i++)
                {
                    other.Positions.Add(new EntityPosition(
                        (short)(Math.Clamp(numberSets[i][0] + difX, short.MinValue, short.MaxValue)),
                        (short)(Math.Clamp(numberSets[i][1] + difY, short.MinValue, short.MaxValue)),
                        (short)(Math.Clamp(numberSets[i][2] + difZ, short.MinValue, short.MaxValue))
                        ));
                }
            });

            UpdatePosition();
            dirty.Pop();
        }

        private void MenuCopyArguments(object sender, EventArgs e)
        {
            cmdCopySetting.PerformClick();
        }

        private void MenuPasteArguments(object sender, EventArgs e)
        {
            cmdPasteSetting.PerformClick();
        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (lbZones.SelectedItem == null)
                e.Cancel = true;

            int[] indices = [0, 1, 3, 4, 5, 6];
            if (dgvEntities.SelectedCells.Count == 0)
            {
                foreach (int i in indices)
                    dgvEntities.ContextMenuStrip.Items[i].Visible = false;
            }
            else
            {
                foreach (int i in indices)
                    dgvEntities.ContextMenuStrip.Items[i].Visible = true;
            }
        }

        private void dgvEntities_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (e.Button == MouseButtons.Right)
            {
                dgvEntities.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                dgvEntities.CurrentCell = dgvEntities.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }

        private void GetGOOL()
        {
            GOOLList.Clear();
            foreach (GOOLEntry gool in nsf.GetEntries<GOOLEntry>())
            {
                if (!GOOLList.ContainsKey(gool.ID) && gool.Format == 1)
                {
                    GOOLList[gool.ID] = gool.EName;
                }
            }
        }

        private void tsbEditDDA_Click(object sender, EventArgs e)
        {
            if (frmDDAEditor == null || frmDDAEditor.IsDisposed)
            {
                frmDDAEditor = new(this);
                frmDDAEditor.FormClosed += (s, e) => frmDDAEditor = null;
            }

            if (!frmDDAEditor.Visible)
                frmDDAEditor.Show();
            else
                frmDDAEditor.Activate();
        }

        #region Zones

        private void GetZones()
        {
            lbZones.SuspendLayout();
            lbZones.Items.Clear();
            originalItems.Clear();

            foreach (var entry in nsf.GetEntries<ZoneEntry>())
            {
                if (chkHideNoEntityZone.Checked && entry.EntityCount == 0) continue;
                originalItems.Add(Entry.EIDToEName(entry.EID));
            }
            originalItems.Sort();
            lbZones.Items.AddRange(originalItems.ToArray());

            lbZones.ClearSelected();
            lbZones.ResumeLayout();
        }

        private void UpdateZones()
        {
            foreach (var entry in nsf.GetEntries<ZoneEntry>())
            {
                if (lbZones.SelectedItem?.ToString() == Entry.EIDToEName(entry.EID))
                {
                    zone = entry;
                }
            }
        }

        private void lbZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            if (lbZones.SelectedItem == null) return;

            int newIndex = lbZones.SelectedIndex;
            if (newIndex == _lastSelectedZoneIndex) return;
            _lastSelectedZoneIndex = newIndex;

            //dirty.Push(true);
            UpdateZones();
            UpdateEntityList();
            ShowZone(true);
            //dirty.Pop();
        }

        private void ShowZone(bool doFocus)
        {
            if (zoneEntryViewer != null)
            {
                zoneEntryViewer.Dispose();
                zoneEntryViewer = null;
                panel1.Controls.Clear();
            }
            if (chkShowZone.Checked)
            {
                zoneEntryViewer = new(nsf, zone.EID) { Dock = DockStyle.Fill };
                panel1.Controls.Add(zoneEntryViewer);
                if (doFocus)
                    lbZones.Focus();
            }
        }

        private void chkShowZone_CheckedChanged(object sender, EventArgs e)
        {
            if (lbZones.SelectedItem == null) return;
            ShowZone(true);
        }

        private void chkShowCameras_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ViewCamera = chkShowCameras.Checked;
            Settings.Default.Save();
        }

        private void txtFilter_Click(object sender, EventArgs e)
        {
            txtFilter.SelectAll();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string pattern = txtFilter.Text.ToLower();

            lbZones.Items.Clear();
            _lastSelectedZoneIndex = -1;

            if (string.IsNullOrWhiteSpace(pattern))
            {
                lbZones.Items.AddRange(originalItems.ToArray());
                return;
            }

            try
            {
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

                var filtered = originalItems
                    .Where(item => regex.IsMatch(item))
                    .ToArray();

                lbZones.Items.AddRange(filtered);
            }
            catch (ArgumentException)
            {
            }
        }

        private void chkHideNoEntityZones_CheckedChanged(object sender, EventArgs e)
        {
            dirty.Push(true);

            pnProperties.Enabled = false;
            dgvEntities.CurrentCell = null;
            dgvEntities.ClearSelection();
            dgvEntities.Rows.Clear();

            _lastSelectedZoneIndex = -1;
            txtFilter.Text = string.Empty;
            if (zoneEntryViewer != null)
            {
                zoneEntryViewer.Dispose();
                zoneEntryViewer = null;
                panel1.Controls.Clear();
            }
            GetZones();

            dirty.Pop();
        }

        #endregion

        #region Entity List

        private void dgvEntitiesInit()
        {
            dgvEntities.Columns.Add("", ""); // index
            dgvEntities.Columns.Add("Name", "Name");
            dgvEntities.Columns.Add("ID", "ID");
            dgvEntities.Columns.Add("Type", "Type");
            dgvEntities.Columns.Add("Sub", "Sub");

            foreach (DataGridViewColumn column in dgvEntities.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 48;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            dgvEntities.Columns[0].Width = 24;
            dgvEntities.Columns[1].Width = 140;
        }

        private void UpdateEntityList()
        {
            dgvEntities.SuspendLayout();
            dgvEntities.ScrollBars = ScrollBars.None;
            dgvEntities.Rows.Clear();

            int cameraCount = zone.CameraCount;
            int entityCount = zone.EntityCount;
            var entities = zone.Entities;

            if (entityCount > 0)
            {
                for (int i = cameraCount; i < entities.Count; i++)
                {
                    Entity e = entities[i];

                    var row = new DataGridViewRow();
                    row.CreateCells(
                        dgvEntities,
                        i,
                        e.Name ?? "-",
                        e.ID?.ToString() ?? "-",
                        e.Type?.ToString() ?? "-",
                        e.Subtype?.ToString() ?? "-"
                    );

                    if (e.Name == null)
                        row.Cells[1].Tag = "null";

                    dgvEntities.Rows.Add(row);
                }

                pnProperties.Enabled = true;
                var firstRow = dgvEntities.Rows[0];
                dgvEntities.CurrentCell = firstRow.Cells[ColIndex];
                firstRow.Cells[ColIndex].Selected = true;
            }
            else
            {
                pnProperties.Enabled = false;
                dgvEntities.CurrentCell = null;
                dgvEntities.ClearSelection();
            }
            dgvEntities.ScrollBars = ScrollBars.Vertical;
            dgvEntities.ResumeLayout();
        }


        private void GetEntity()
        {
            entity = zone.Entities[zone.SelectedEntity];
        }

        private void dgvEntities_SelectionChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            if (dgvEntities.SelectedCells.Count == 0 || lbZones.SelectedItem == null) return;

            dirty.Push(true);

            int rowindex = dgvEntities.SelectedCells[0].RowIndex;
            zone.SelectedEntity = Convert.ToInt32(dgvEntities.Rows[rowindex].Cells[0].Value);

            GetEntity();
            UpdateName();
            UpdateID();
            UpdateType();
            UpdateSubtype();
            UpdatePosition();
            UpdateSettings();
            UpdateZMod();
            UpdateC2TTSettings();
            UpdateSpecialTab();
            if (!pnProperties.Enabled)
                pnProperties.Enabled = true;
            if (frmDDAEditor != null)
                frmDDAEditor.UpdateDDASelection();

            dirty.Pop();
        }

        private void getMaxValue(int columnIndex, out int minValue, out int maxValue)
        {
            maxValue = 0; minValue = 0;
            switch (columnIndex)
            {
                case 2: // ID
                    maxValue = Int32.MaxValue;
                    minValue = Int32.MinValue;
                    break;
                case 3: // Type
                    maxValue = Byte.MaxValue;
                    minValue = Byte.MinValue;
                    break;
                case 4: // Subtype
                    maxValue = Int32.MaxValue;
                    minValue = Int32.MinValue;
                    break;
            }
        }

        private void dgvEntities_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string text = dgvEntities.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";
            if (text == "-")
            {
                if (e.ColumnIndex == ColName && dgvEntities.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag == null)
                    return;
                e.CellStyle.ForeColor = Color.Crimson;
                return;
            }

            if (e.ColumnIndex == 0)
            {
                e.CellStyle.ForeColor = Color.Gray;
            }
            else if (e.ColumnIndex == 3)
            {
                int value = Convert.ToInt32(dgvEntities.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                e.CellStyle.ForeColor = value switch
                {
                    3 => Color.LimeGreen,
                    34 => Color.Gold,
                    _ => Color.White,
                };
            }
        }

        private void dgvEntities_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == ColIndex)
                e.Cancel = true;
        }

        private void dgvEntities_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //int col = dgvEntities.CurrentCell.ColumnIndex;
            //if (col == ColID || col == ColType || col == ColSubtype)
            //{
            //    if (e.Control is TextBox tb)
            //    {
            //        tb.KeyPress -= textBox_CheckInput;
            //        tb.KeyPress += textBox_CheckInput;
            //    }
            //}
        }

        //private void textBox_CheckInput(object sender, KeyPressEventArgs e)
        //{
        //    if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) || e.KeyChar == '-') return;
        //    e.Handled = true;
        //}

        private void dgvEntities_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvEntities.SelectedCells.Count == 0) return;
            if (e.ColumnIndex == ColIndex) return;

            string inputValue = e.FormattedValue?.ToString() ?? "";
            if (inputValue == "-") return;

            if (e.ColumnIndex == ColName)
            {
                if (inputValue.Length > short.MaxValue)
                {
                    DarkMessageBox.ShowError($"Invalid input. Maximum length is {short.MaxValue} characters.", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
            else if (e.ColumnIndex == ColID || e.ColumnIndex == ColType || e.ColumnIndex == ColSubtype)
            {
                if (int.TryParse(inputValue, out int newValue))
                {
                    getMaxValue(e.ColumnIndex, out int minValue, out int maxValue);
                    if (newValue > maxValue)
                    {
                        DarkMessageBox.ShowError($"Invalid input. The value must be less than or equal to {maxValue}.", Resources.Title_InputError);
                        e.Cancel = true;
                    }
                    else if (newValue < minValue)
                    {
                        DarkMessageBox.ShowError($"Invalid input. The value must be greater than or equal to {minValue}.", Resources.Title_InputError);
                        e.Cancel = true;
                    }
                }
                else
                {
                    DarkMessageBox.ShowError($"Invalid input.", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
        }

        private void dgvEntities_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Dirty) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            dirty.Push(true);

            var cell = dgvEntities.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string text = cell.Value?.ToString() ?? "";
            bool isNull = text == "-";

            if (e.ColumnIndex == ColName)
            {
                entity.Name = text;
                UpdateSyncedEntitiesName();
                UpdateName();
            }
            else
            {
                int? value = isNull ? null : Convert.ToInt32(cell.Value);
                switch (e.ColumnIndex)
                {
                    case 2:
                        entity.ID = value;
                        if (dgvEntities.SelectedCells.Count > 1)
                        {
                            if (DarkMessageBox.ShowWarning("Are you sure you want to change the ID of the selected items?", "Confirmation Prompt",
                                DarkDialogButton.YesNo) == DialogResult.Yes)
                            {
                                UpdateSyncedEntitiesProps(ColID);
                            }
                        }
                        UpdateID();
                        break;
                    case 3:
                        entity.Type = value;
                        UpdateSyncedEntitiesProps(ColType);
                        UpdateType();
                        break;
                    case 4:
                        entity.Subtype = value;
                        UpdateSyncedEntitiesProps(ColSubtype);
                        UpdateSubtype();
                        break;
                }
            }

            dirty.Pop();
        }

        private void UpdateSyncedEntitiesProps(int colIdx)
        {
            ApplyToSyncedEntities((other, rowIdx) =>
            {
                switch (colIdx)
                {
                    case 2:
                        other.ID = entity.ID;
                        dgvEntities.Rows[rowIdx].Cells[ColID].Value = entity.ID?.ToString() ?? "-";
                        break;
                    case 3:
                        other.Type = entity.Type;
                        dgvEntities.Rows[rowIdx].Cells[ColType].Value = entity.Type?.ToString() ?? "-";
                        break;
                    case 4:
                        other.Subtype = entity.Subtype;
                        dgvEntities.Rows[rowIdx].Cells[ColSubtype].Value = entity.Subtype?.ToString() ?? "-";
                        break;
                }
            });
        }

        #endregion

        //
        // Entities
        //

        private void ApplyToSyncedEntities(Action<Entity, int> action)
        {
            if (!syncEditEnabled || dgvEntities.SelectedCells.Count <= 1) return;

            var rowsToEdit = new HashSet<int>();
            foreach (DataGridViewCell cell in dgvEntities.SelectedCells)
            {
                rowsToEdit.Add(cell.RowIndex);
            }

            foreach (int rowIndex in rowsToEdit)
            {
                var row = dgvEntities.Rows[rowIndex];

                if (row.Cells[ColIndex].Value is not int entityIndex) continue;
                if (entityIndex < 0 || entityIndex >= zone.Entities.Count) continue;

                Entity other = zone.Entities[entityIndex];
                if (other != entity)
                    action(other, rowIndex);
            }

        }

        // General

        #region Name

        private void UpdateName()
        {
            if (entity.Name != null)
            {
                txtName.Text = entity.Name;
                chkName.Checked = true;
            }
            else
            {
                txtName.Text = "";
                txtName.Enabled = false;
                chkName.Checked = false;
            }
        }

        private void chkName_CheckedChanged(object sender, EventArgs e)
        {
            dirty.Push(true);
            txtName.Enabled = chkName.Checked;
            entity.Name = chkName.Checked ? txtName.Text : null;
            dgvEntities.Rows[dgvEntities.SelectedCells[0].RowIndex].Cells[ColName].Value = entity.Name ?? "-";
            dgvEntities.Rows[dgvEntities.SelectedCells[0].RowIndex].Cells[ColName].Tag = chkName.Checked ? null : "null";
            UpdateSyncedEntitiesName();
            UpdateOtherEditors();
            dirty.Pop();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (Dirty) return;

            dirty.Push(true);
            entity.Name = txtName.Text;
            dgvEntities.Rows[dgvEntities.SelectedCells[0].RowIndex].Cells[ColName].Value = entity.Name;
            UpdateSyncedEntitiesName();
            UpdateOtherEditors();
            dirty.Pop();
        }

        private void UpdateSyncedEntitiesName()
        {
            ApplyToSyncedEntities((other, rowIdx) =>
            {
                other.Name = entity.Name;
                dgvEntities.Rows[rowIdx].Cells[ColName].Value = other.Name ?? "-";
                dgvEntities.Rows[rowIdx].Cells[ColName].Tag = other.Name != null ? null : "null";
            });
        }

        #endregion

        #region ID

        private void UpdateID()
        {
            if (entity.ID.HasValue)
                numID.Value = entity.ID.Value;
            else
                numID.Value = 0;

            numID.Enabled = entity.ID.HasValue;
            chkID.Checked = entity.ID.HasValue;
        }

        private void chkID_CheckedChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            dirty.Push(true);
            numID.Enabled = chkID.Checked;
            entity.ID = chkID.Checked ? (int)numID.Value : null;
            dgvEntities.Rows[dgvEntities.SelectedCells[0].RowIndex].Cells[ColID].Value = entity.ID?.ToString() ?? "-";
            if (dgvEntities.SelectedCells.Count > 1)
            {
                if (DarkMessageBox.ShowWarning("Are you sure you want to change the ID of the selected items?", "Confirmation Prompt",
                    DarkDialogButton.YesNo) == DialogResult.Yes)
                {
                    UpdateSyncedEntitiesID();
                }
            }
            UpdateOtherEditors();
            dirty.Pop();
        }

        private void numID_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            dirty.Push(true);
            entity.ID = (int)numID.Value;
            dgvEntities.Rows[dgvEntities.SelectedCells[0].RowIndex].Cells[ColID].Value = entity.ID;
            if (dgvEntities.SelectedCells.Count > 1)
            {
                if (DarkMessageBox.ShowWarning("Are you sure you want to change the ID of the selected items?", "Confirmation Prompt",
                    DarkDialogButton.YesNo) == DialogResult.Yes)
                {
                    UpdateSyncedEntitiesID();
                }
            }
            UpdateOtherEditors();
            dirty.Pop();
        }

        private void UpdateSyncedEntitiesID()
        {
            ApplyToSyncedEntities((other, rowIdx) =>
            {
                other.ID = entity.ID;
                dgvEntities.Rows[rowIdx].Cells[ColID].Value = other.ID?.ToString() ?? "-";
            });
        }

        #endregion

        #region Type & Subtype

        private void UpdateGOOLLabel()
        {
            if (!chkType.Checked)
                lblGOOL.Text = "";
            else
            {
                int type = (int)numType.Value;
                if (GOOLList.TryGetValue(type, out string? value))
                {
                    lblGOOL.ForeColor = Color.DarkTurquoise;
                    lblGOOL.Text = $"({value})";
                }
                else
                {
                    lblGOOL.ForeColor = Color.Crimson;
                    lblGOOL.Text = $"(NONE!)";
                }
            }
        }

        private void UpdateType()
        {
            if (entity.Type.HasValue)
                numType.Value = entity.Type.Value;
            else
                numType.Value = 0;

            numType.Enabled = entity.Type.HasValue;
            chkType.Checked = entity.Type.HasValue;
            UpdateGOOLLabel();
        }

        private void chkType_CheckedChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            dirty.Push(true);
            numType.Enabled = chkType.Checked;
            entity.Type = chkType.Checked ? (int)numType.Value : null;
            dgvEntities.Rows[dgvEntities.SelectedCells[0].RowIndex].Cells[ColType].Value = entity.Type?.ToString() ?? "-";
            UpdateSyncedEntitiesType();
            UpdateGOOLLabel();
            dirty.Pop();
        }

        private void numType_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            dirty.Push(true);
            entity.Type = (int)numType.Value;
            dgvEntities.Rows[dgvEntities.SelectedCells[0].RowIndex].Cells[ColType].Value = entity.Type;
            UpdateSyncedEntitiesType();
            UpdateGOOLLabel();
            dirty.Pop();
        }

        private void UpdateSyncedEntitiesType()
        {
            ApplyToSyncedEntities((other, rowIdx) =>
            {
                other.Type = entity.Type;
                dgvEntities.Rows[rowIdx].Cells[ColType].Value = other.Type?.ToString() ?? "-";
            });
        }

        private void UpdateSubtype()
        {
            if (entity.Subtype.HasValue)
                numSubtype.Value = entity.Subtype.Value;
            else
                numSubtype.Value = 0;

            numSubtype.Enabled = entity.Subtype.HasValue;
            chkSubtype.Checked = entity.Subtype.HasValue;
        }

        private void chkSubtype_CheckedChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            dirty.Push(true);
            numSubtype.Enabled = chkSubtype.Checked;
            entity.Subtype = chkSubtype.Checked ? (int)numSubtype.Value : null;
            dgvEntities.Rows[dgvEntities.SelectedCells[0].RowIndex].Cells[ColSubtype].Value = entity.Subtype?.ToString() ?? "-";
            UpdateSyncedEntitiesSubtype();
            dirty.Pop();
        }

        private void numSubtype_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            dirty.Push(true);
            entity.Subtype = (int)numSubtype.Value;
            dgvEntities.Rows[dgvEntities.SelectedCells[0].RowIndex].Cells[ColSubtype].Value = entity.Subtype;
            UpdateSyncedEntitiesSubtype();
            dirty.Pop();
        }

        private void UpdateSyncedEntitiesSubtype()
        {
            ApplyToSyncedEntities((other, rowIdx) =>
            {
                other.Subtype = entity.Subtype;
                dgvEntities.Rows[rowIdx].Cells[ColSubtype].Value = other.Subtype?.ToString() ?? "-";
            });
        }

        #endregion

        #region Positions

        private void UpdatePosition()
        {
            dirty.Push(true);

            if (positionindex >= entity.Positions.Count)
            {
                positionindex = entity.Positions.Count - 1;
            }
            // Do not make this else if,
            // sometimes both will run.
            // (this is intentional)
            if (positionindex < 0)
            {
                positionindex = 0;
            }
            // Do not remove this either
            if (positionindex >= entity.Positions.Count)
            {
                lblPositionIndex.Text = "-- / --";
                cmdPreviousPosition.Enabled =
                cmdNextPosition.Enabled =
                cmdInsertPosition.Enabled =
                cmdRemovePosition.Enabled =
                cmdEditPath.Enabled = false;
                lblX.Enabled = lblY.Enabled = lblZ.Enabled = numX.Enabled = numY.Enabled = numZ.Enabled = false;
                numX.Value = 0;
                numY.Value = 0;
                numZ.Value = 0;
            }
            else
            {
                lblPositionIndex.Text = $"{positionindex + 1} / {entity.Positions.Count}";
                cmdPreviousPosition.Enabled = positionindex > 0;
                cmdNextPosition.Enabled = positionindex < entity.Positions.Count - 1;
                cmdInsertPosition.Enabled = true;
                cmdRemovePosition.Enabled = true;
                lblX.Enabled = lblY.Enabled = lblZ.Enabled = numX.Enabled = numY.Enabled = numZ.Enabled = true;
                numX.Value = entity.Positions[positionindex].X;
                numY.Value = entity.Positions[positionindex].Y;
                numZ.Value = entity.Positions[positionindex].Z;
                cmdEditPath.Enabled = entity.Positions.Count >= 2;
            }

            dirty.Pop();
        }

        private void cmdPreviousPosition_Click(object sender, EventArgs e)
        {
            --positionindex;
            UpdatePosition();
        }

        private void cmdNextPosition_Click(object sender, EventArgs e)
        {
            ++positionindex;
            UpdatePosition();
        }

        private void cmdInsertPosition_Click(object sender, EventArgs e)
        {
            entity.Positions.Insert(positionindex, entity.Positions[positionindex]);
            UpdatePosition();
        }

        private void cmdRemovePosition_Click(object sender, EventArgs e)
        {
            entity.Positions.RemoveAt(positionindex);
            UpdatePosition();
        }

        private void cmdAppendPosition_Click(object sender, EventArgs e)
        {
            positionindex = entity.Positions.Count;
            if (entity.Positions.Count > 0)
            {
                entity.Positions.Add(entity.Positions[positionindex - 1]);
            }
            else
            {
                entity.Positions.Add(new EntityPosition(0, 0, 0));
            }
            UpdatePosition();
        }

        private T ValidateValue<T>(T value, T dif) where T : struct, IComparable<T>
        {
            T result;
            try
            {
                long tempResult = Convert.ToInt64(value) + Convert.ToInt64(dif);

                T max = (T)typeof(T).GetField("MaxValue")?.GetValue(null);
                T min = (T)typeof(T).GetField("MinValue")?.GetValue(null);

                tempResult = Math.Clamp(tempResult, Convert.ToInt64(min), Convert.ToInt64(max));
                result = (T)Convert.ChangeType(tempResult, typeof(T));
            }
            catch (OverflowException)
            {
                T max = (T)typeof(T).GetField("MaxValue")?.GetValue(null);
                T min = (T)typeof(T).GetField("MinValue")?.GetValue(null);
                result = dif.CompareTo(default(T)) > 0 ? max : min;
            }

            return result;
        }

        private void UpdateSyncedEntitiesPositions(int type, short dif)
        {
            ApplyToSyncedEntities((other, rowIdx) =>
            {
                for (int i = 0; i < other.Positions.Count; i++)
                {
                    EntityPosition pos = other.Positions[i];

                    short newX = pos.X;
                    short newY = pos.Y;
                    short newZ = pos.Z;

                    if (type == 0) newX = ValidateValue(pos.X, dif);
                    else if (type == 1) newY = ValidateValue(pos.Y, dif);
                    else if (type == 2) newZ = ValidateValue(pos.Z, dif);

                    other.Positions[i] = new EntityPosition(newX, newY, newZ);
                }
            });
        }

        private void numX_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                short oldV = entity.Positions[positionindex].X;
                short newV = (short)numX.Value;
                short dif = (short)(newV - oldV);
                if (chkSyncPositions.Checked)
                {
                    for (int i = 0; i < entity.Positions.Count; i++)
                    {
                        EntityPosition pos = entity.Positions[i];
                        short result = ValidateValue(pos.X, dif);
                        entity.Positions[i] = new EntityPosition(result, pos.Y, pos.Z);
                    }
                }
                else
                {
                    EntityPosition pos = entity.Positions[positionindex];
                    entity.Positions[positionindex] = new EntityPosition((short)numX.Value, pos.Y, pos.Z);
                }
                UpdateSyncedEntitiesPositions(0, dif);
            }
        }

        private void numY_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                short oldV = entity.Positions[positionindex].Y;
                short newV = (short)numY.Value;
                short dif = (short)(newV - oldV);
                if (chkSyncPositions.Checked)
                {
                    for (int i = 0; i < entity.Positions.Count; i++)
                    {
                        EntityPosition pos = entity.Positions[i];
                        short result = ValidateValue(pos.Y, dif);
                        entity.Positions[i] = new EntityPosition(pos.X, result, pos.Z);
                    }
                }
                else
                {
                    EntityPosition pos = entity.Positions[positionindex];
                    entity.Positions[positionindex] = new EntityPosition(pos.X, (short)numY.Value, pos.Z);
                }
                UpdateSyncedEntitiesPositions(1, dif);
            }
        }

        private void numZ_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                short oldV = entity.Positions[positionindex].Z;
                short newV = (short)numZ.Value;
                short dif = (short)(newV - oldV);
                if (chkSyncPositions.Checked)
                {
                    for (int i = 0; i < entity.Positions.Count; i++)
                    {
                        EntityPosition pos = entity.Positions[i];
                        short result = ValidateValue(pos.Z, dif);
                        entity.Positions[i] = new EntityPosition(pos.X, pos.Y, result);
                    }
                }
                else
                {
                    EntityPosition pos = entity.Positions[positionindex];
                    entity.Positions[positionindex] = new EntityPosition(pos.X, pos.Y, (short)numZ.Value);
                }
                UpdateSyncedEntitiesPositions(2, dif);
            }
        }

        private void cmdInterpolate_Click(object sender, EventArgs e)
        {
            Position[] pos = new Position[entity.Positions.Count];
            for (int i = 0; i < entity.Positions.Count; ++i)
            {
                pos[i] = new Position(entity.Positions[i].X, entity.Positions[i].Y, entity.Positions[i].Z);
            }
            using (InterpolatorForm interpolator = new InterpolatorForm(pos))
            {
                if (interpolator.ShowDialog() == DialogResult.OK)
                {
                    if (interpolator.Mode == 0)
                    {
                        for (int m = interpolator.Start - 1, i = interpolator.End - 2; i > m; --i)
                        {
                            entity.Positions.RemoveAt(i);
                        }
                        for (int i = 0; i < interpolator.Amount; ++i)
                        {
                            entity.Positions.Insert(i + interpolator.Start, new EntityPosition(interpolator.NewPositions[i + 1]));
                        }
                    }
                    else
                    {
                        entity.Positions.Clear();
                        for (int i = 0; i < interpolator.Amount + 1; ++i)
                        {
                            entity.Positions.Add(new EntityPosition(interpolator.NewPositions[i]));
                        }
                    }

                    UpdatePosition();
                }
            }
        }

        private void chkSyncEntities_CheckedChanged(object sender, EventArgs e)
        {
            cmdSyncEntities.Enabled = syncEditEnabled;
        }

        private void cmdSyncList_Click(object sender, EventArgs e)
        {
            if (syncListForm == null || syncListForm.IsDisposed)
            {
                syncListForm = new DarkForm()
                {
                    Text = "Sync Entities",
                    Icon = Embeds.GetIcon("ThingViolet"),
                    Size = new Size(200, 360),
                    MinimizeBox = false,
                    MaximizeBox = false,
                    AutoSize = true
                };
                syncListForm.FormClosing += (sender, e) =>
                {
                    syncListForm = null;
                };

                FlowLayoutPanel panel = new()
                {
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false
                };

                DarkGroupBox fraSyncedEntities = new()
                {
                    Text = "Synced Entities",
                    AutoSize = true,
                    Margin = new Padding(6, 3, 6, 19)
                };

                FlowLayoutPanel panel2 = new()
                {
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false
                };

                DarkListBox lstSyncedEntities = new()
                {
                    Size = new Size(200, 200),
                    SelectionMode = SelectionMode.MultiExtended
                };
                if (syncEntityList == null)
                    syncEntityList = new();
                else
                {
                    foreach (string item in syncEntityList)
                    {
                        lstSyncedEntities.Items.Add(item);
                    }
                }

                DarkButton cmdRemove = new()
                {
                    Text = "Remove"
                };

                DarkComboBox cmbZones = new()
                {
                    DropDownHeight = 220,
                    Margin = new Padding(6, 3, 6, 3)
                };
                foreach (ZoneEntry zone in nsf.GetEntries<ZoneEntry>())
                {
                    cmbZones.Items.Add(zone.EName);
                }

                DarkButton cmdAdd = new()
                {
                    Text = "Add",
                    Margin = new Padding(6, 3, 6, 3)
                };

                DarkListBox lstEntities = new()
                {
                    Size = new Size(200, 200),
                    SelectionMode = SelectionMode.MultiExtended,
                    Margin = new Padding(6, 3, 6, 6)
                };

                cmbZones.SelectedIndexChanged += (sender, e) =>
                {
                    lstEntities.Items.Clear();
                    ZoneEntry zone = nsf.GetEntry<ZoneEntry>(Entry.ENameToEID(cmbZones.SelectedItem.ToString()));
                    foreach (Entity otherentity in zone.Entities)
                    {
                        if (otherentity.ID.HasValue)
                        {
                            string text = $"{otherentity.Name} [ID {otherentity.ID}]";
                            if (!lstSyncedEntities.Items.Contains(text))
                            {
                                lstEntities.Items.Add(text);
                            }
                        }
                    }
                };

                cmdAdd.Click += (sender, e) =>
                {
                    foreach (var item in lstEntities.SelectedItems.Cast<object>().ToList())
                    {
                        lstSyncedEntities.Items.Add(item);
                        syncEntityList.Add((string)item);
                        lstEntities.Items.Remove(item);
                    }
                };

                cmdRemove.Click += (sender, e) =>
                {
                    List<string> fakeList = new();
                    ZoneEntry zone = nsf.GetEntry<ZoneEntry>(Entry.ENameToEID(cmbZones.SelectedItem.ToString()));
                    foreach (Entity otherentity in zone.Entities)
                    {
                        if (otherentity.ID.HasValue)
                        {
                            string text = $"{otherentity.Name} [ID {otherentity.ID}]";
                            fakeList.Add(text);
                        }
                    }

                    foreach (var item in lstSyncedEntities.SelectedItems.Cast<object>().ToList())
                    {
                        if (fakeList.Contains(item))
                            lstEntities.Items.Add(item);
                        syncEntityList.Remove((string)item);
                        lstSyncedEntities.Items.Remove(item);
                    }
                };

                //cmbZones.SelectedItem = contoller.ZoneEntry.EName;

                panel2.Controls.Add(lstSyncedEntities);
                panel2.Controls.Add(cmdRemove);
                fraSyncedEntities.Controls.Add(panel2);

                panel.Controls.Add(fraSyncedEntities);
                panel.Controls.Add(cmbZones);
                panel.Controls.Add(cmdAdd);
                panel.Controls.Add(lstEntities);

                syncListForm.Controls.Add(panel);
                syncListForm.FormBorderStyle = FormBorderStyle.FixedSingle;
                syncListForm.Show();
            }
            else
            {
                syncListForm.Select();
            }
        }

        #endregion

        #region Settings

        private void UpdateSettings()
        {
            dirty.Push(true);
            if (settingindex >= entity.Settings.Count)
            {
                settingindex = entity.Settings.Count - 1;
            }
            // Do not make this else if,
            // sometimes both will run.
            // (this is intentional)
            if (settingindex < 0)
            {
                settingindex = 0;
            }
            // Do not remove this either
            if (settingindex >= entity.Settings.Count)
            {
                lblSettingIndex.Text = "-- / --";
                lblArgAs.Enabled =
                cmdPreviousSetting.Enabled =
                cmdNextSetting.Enabled =
                cmdRemoveSetting.Enabled =
                numSettingA.Enabled =
                numSettingB.Enabled =
                numSettingC.Enabled =
                cmdCopySetting.Enabled = false;
                numSettingA.Value = 0;
                numSettingB.Value = 0;
                numSettingC.Value = 0;
            }
            else
            {
                lblSettingIndex.Text = $"{settingindex + 1} / {entity.Settings.Count}";
                lblArgAs.Text = MakeArgAsText();
                cmdPreviousSetting.Enabled = settingindex > 0;
                cmdNextSetting.Enabled = settingindex < entity.Settings.Count - 1;
                cmdRemoveSetting.Enabled =
                lblArgAs.Enabled =
                numSettingA.Enabled =
                numSettingB.Enabled =
                numSettingC.Enabled =
                cmdCopySetting.Enabled = true;
                numSettingA.Value = entity.Settings[settingindex].ValueA;
                numSettingB.Value = entity.Settings[settingindex].ValueB;
                SetCVal(entity.Settings[settingindex].Value);
            }
            dirty.Pop();
        }

        internal string MakeArgAsText()
        {
            int arg = entity.Settings.Count > 0 ? entity.Settings[settingindex].Value : 0;
            return string.Format(Resources.EntityBox_lblArgAs,
                arg / 256F,
                arg / (float)0x1000 * 360,
                arg / (OldMainForm.PAL ? 25F : 30F),
                arg / (256F * 400));
        }

        private void cmdPreviousSetting_Click(object sender, EventArgs e)
        {
            --settingindex;
            UpdateSettings();
        }

        private void cmdNextSetting_Click(object sender, EventArgs e)
        {
            ++settingindex;
            UpdateSettings();
        }

        private void cmdAddSetting_Click(object sender, EventArgs e)
        {
            entity.Settings.Add(new EntitySetting(0, 0));
            UpdateSyncedEntitiesSettings();
            UpdateSettings();
        }

        private void cmdRemoveSetting_Click(object sender, EventArgs e)
        {
            entity.Settings.RemoveAt(settingindex);
            UpdateSyncedEntitiesSettings();
            UpdateSettings();
        }

        private void UpdateSyncedEntitiesSettings()
        {
            ApplyToSyncedEntities((other, rowIdx) =>
            {
                //other.Settings[settingindex] = entity.Settings[settingindex];

                other.Settings.Clear();
                foreach (EntitySetting setting in entity.Settings)
                {
                    other.Settings.Add(setting);
                }
            });
        }

        private void numSettingA_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                EntitySetting s = entity.Settings[settingindex];
                entity.Settings[settingindex] = new EntitySetting((byte)numSettingA.Value, s.ValueB);
                SetCVal(entity.Settings[settingindex].Value);
                lblArgAs.Text = MakeArgAsText();
                UpdateSyncedEntitiesSettings();
            }
        }

        private void numSettingB_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                EntitySetting s = entity.Settings[settingindex];
                entity.Settings[settingindex] = new EntitySetting(s.ValueA, (int)numSettingB.Value);
                SetCVal(entity.Settings[settingindex].Value);
                lblArgAs.Text = MakeArgAsText();
                UpdateSyncedEntitiesSettings();
            }
        }

        internal void SetCVal(long val)
        {
            dirty.Push(true);
            // this is fucking stupid
            if (numSettingC.Hexadecimal)
            {
                if (val > 0xFFFFFFFF) val = 0xFFFFFFFF;
                else if (val < 0) val &= 0xFFFFFFFF;
                numSettingC.Value = unchecked((uint)val);
            }
            else
            {
                if (val > 0xFFFFFFFF) val = 0x7FFFFFFF;
                else if (val > 0x7FFFFFFF) val = -0x100000000 + val;
                else if (val < -0x80000000) val = -0x80000000;
                numSettingC.Value = unchecked((int)val);
            }
            dirty.Pop();
        }

        private void numSettingC_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                SetCVal((long)numSettingC.Value);
                entity.Settings[settingindex] = new EntitySetting(((long)numSettingC.Value).UInt32ToInt32());
                dirty.Push(true);
                numSettingA.Value = entity.Settings[settingindex].ValueA;
                numSettingB.Value = entity.Settings[settingindex].ValueB;
                dirty.Pop();
                lblArgAs.Text = MakeArgAsText();
                UpdateSyncedEntitiesSettings();
            }
        }

        private void cmdCopySetting_Click(object sender, EventArgs e)
        {
            string text = string.Join("\n", entity.Settings.Select(setting => setting.Value));
            Clipboard.SetDataObject(text, true, 10, 100);
        }

        private void cmdPasteSetting_Click(object sender, EventArgs e)
        {
            string[] lines = Clipboard.GetText().Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            settingindex = 0;
            entity.Settings.Clear();
            foreach (string line in lines)
            {
                if (int.TryParse(line, out int value))
                {
                    entity.Settings.Add(new EntitySetting(value));
                }
            }
            UpdateSyncedEntitiesSettings();
            UpdateSettings();
        }

        private void chkSettingHex_CheckedChanged(object sender, EventArgs e)
        {
            numSettingC.Hexadecimal = chkSettingHex.Checked;
            SetCVal((long)numSettingC.Value);
        }

        #endregion

        #region ZMod

        private void UpdateZMod()
        {
            if (entity.ZMod.HasValue)
                numZMod.Value = entity.ZMod.Value;
            else
                numZMod.Value = 0;

            numZMod.Enabled = entity.ZMod.HasValue;
            chkZMod.Checked = entity.ZMod.HasValue;
        }

        private void chkZMod_CheckedChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            numZMod.Enabled = chkZMod.Checked;
            if (chkZMod.Checked)
            {
                entity.ZMod = (int)numZMod.Value;
            }
            else
            {
                entity.ZMod = null;
            }
            UpdateSyncedEntitiesZMod();
        }

        private void numZMod_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            entity.ZMod = (int)numZMod.Value;
            UpdateSyncedEntitiesZMod();
        }

        private void UpdateSyncedEntitiesZMod()
        {
            ApplyToSyncedEntities((other, rowIdx) =>
            {
                other.ZMod = entity.ZMod;
            });
        }

        #endregion

        #region C2TT

        // C2-tweaked
        private void UpdateC2TTSettings()
        {
            UpdateC2TTType();
            UpdateC2TTYRot();
            UpdateC2TTBoxFlag();
            UpdateC2TTGhostTarget();
        }

        private void UpdateC2TTType()
        {
            if (entity.C2TTType.HasValue)
            {
                int value = entity.C2TTType.Value >> 8;
                cmbC2TTType.SelectedIndex = value >= cmbC2TTType.Items.Count ? 0 : value;
            }
            else
            {
                cmbC2TTType.SelectedItem = null;
            }
            cmbC2TTType.Enabled =
            chkC2TTType.Checked = entity.C2TTType.HasValue;
        }

        private void chkC2TTType_CheckedChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            cmbC2TTType.Enabled = chkC2TTType.Checked;
            if (chkC2TTType.Checked)
            {
                if (cmbC2TTType.SelectedIndex < 0)
                    cmbC2TTType.SelectedIndex = 0;

                entity.C2TTType = cmbC2TTType.SelectedIndex << 8;
            }
            else
            {
                entity.C2TTType = null;
            }
        }

        private void cmbC2TTType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            entity.C2TTType = cmbC2TTType.SelectedIndex << 8;
        }

        private void MakeArgAsC2TTYRot()
        {
            float deg = (float)numC2TTYRot.Value / 0x1000 * 360;
            lblC2TTYRot.Text = $"{Math.Round(deg, 1)} deg";
        }

        private void UpdateC2TTYRot()
        {
            if (entity.C2TTYRot.HasValue)
            {
                numC2TTYRot.Value = entity.C2TTYRot.Value >> 8;
                MakeArgAsC2TTYRot();
            }
            else
            {
                numC2TTYRot.Value = 0;
                lblC2TTYRot.Text = "";
            }
            numC2TTYRot.Enabled = entity.C2TTYRot.HasValue;
            chkC2TTYRot.Checked = entity.C2TTYRot.HasValue;
        }

        private void chkC2TTYRot_CheckedChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            numC2TTYRot.Enabled = chkC2TTYRot.Checked;
            if (chkC2TTYRot.Checked)
            {
                entity.C2TTYRot = (int)numC2TTYRot.Value << 8;
                MakeArgAsC2TTYRot();
            }
            else
            {
                entity.C2TTYRot = null;
                lblC2TTYRot.Text = "";
            }
        }

        private void numC2TTYRot_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            entity.C2TTYRot = (int)numC2TTYRot.Value << 8;
            MakeArgAsC2TTYRot();
        }

        private void UpdateC2TTBoxFlag()
        {
            if (entity.C2TTBoxFlag.HasValue)
                numC2TTFlags.Value = entity.C2TTBoxFlag.Value >> 8;
            else
                numC2TTFlags.Value = 0;

            numC2TTFlags.Enabled = entity.C2TTBoxFlag.HasValue;
            chkC2TTFlags.Checked = entity.C2TTBoxFlag.HasValue;
        }

        private void chkC2TTFlags_CheckedChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            numC2TTFlags.Enabled = chkC2TTFlags.Checked;
            if (chkC2TTFlags.Checked)
            {
                entity.C2TTBoxFlag = (int)numC2TTFlags.Value << 8;
            }
            else
            {
                entity.C2TTBoxFlag = null;
            }
        }

        private void numC2TTFlags_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            entity.C2TTBoxFlag = (int)numC2TTFlags.Value << 8;
        }

        private void UpdateC2TTGhostTarget()
        {
            if (entity.C2TTGhostTarget.HasValue)
                numC2TTGhostTarget.Value = entity.C2TTGhostTarget.Value >> 8;
            else
                numC2TTGhostTarget.Value = 0;

            numC2TTGhostTarget.Enabled = entity.C2TTGhostTarget.HasValue;
            chkC2TTGhostTarget.Checked = entity.C2TTGhostTarget.HasValue;
        }

        private void chkC2TTGhostTarget_CheckedChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            numC2TTGhostTarget.Enabled = chkC2TTGhostTarget.Checked;
            if (chkC2TTGhostTarget.Checked)
            {
                entity.C2TTGhostTarget = (int)numC2TTGhostTarget.Value << 8;
            }
            else
            {
                entity.C2TTGhostTarget = null;
            }
        }

        private void numC2TTGhostTarget_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            entity.C2TTGhostTarget = (int)numC2TTGhostTarget.Value << 8;
        }

        #endregion

        // Special

        private void tabSpecialInit()
        {
            tipVictim = new();
            tipVictimDistance = new();
            tipOverrideId = new();
            tipOverrideMult = new();

            picHelpVictimDistance.Image = Embeds.GetIcon("Hint").ToBitmap();
            picHelpOverrideId.Image = Embeds.GetIcon("Hint").ToBitmap();
            picHelpOverrideMult.Image = Embeds.GetIcon("Hint").ToBitmap();

            tipVictim.SetToolTip(lbVictimID, Resources.EntityBox_tipLists);
            tipVictim.SetToolTip(picHelpVictimDistance, "The default blast radius for TNT crates is 300.");
            tipOverrideId.SetToolTip(picHelpOverrideId, "c2export rebuild_dl: \nposition override ID\nWhen making draw lists,\nuses position of other entity\n(must be from same zone).");
            tipOverrideMult.SetToolTip(picHelpOverrideMult, "c2export rebuild_dl: \ndistance multiplier\nWhen making draw lists,\nallowed distance is\nmultipled by this / 100.");
        }

        private void UpdateSpecialTab()
        {
            LoadVictimList();
            UpdateVictim();
            UpdateBoxCount();
            UpdateDDASection();
            UpdateDDASettings();
            UpdateDrawOverride();
        }

        #region Victims

        private void UpdateVictim()
        {
            dirty.Push(true);
            if (victimindex >= entity.Victims.Count)
                victimindex = entity.Victims.Count - 1;
            // Do not make this else if,
            // sometimes both will run.
            // (this is intentional)
            if (victimindex < 0)
                victimindex = 0;
            if (victimindex >= entity.Victims.Count)
            {
                lblVictimIndex.Text = "-- / --";
                cmdRemoveVictim.Enabled =
                cmdClearAllVictims.Enabled = false;
            }
            else
            {
                lblVictimIndex.Text = $"{victimindex + 1} / {entity.Victims.Count}";
                cmdRemoveVictim.Enabled =
                cmdClearAllVictims.Enabled = true;
            }
            dirty.Pop();
        }

        private void LoadVictimList()
        {
            numEditVictimID.Value = 0;
            lbVictimID.Items.Clear();
            if (entity.Victims.Count > 0)
            {
                for (int i = 0; i < entity.Victims.Count; ++i)
                {
                    lbVictimID.Items.Add(entity.Victims[i].VictimID);
                }
                lbVictimID.SelectedIndex = 0;
            }
        }

        private void lbVictimID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            victimindex = lbVictimID.SelectedIndex;
            lblVictimIndex.Text = $"{victimindex + 1} / {entity.Victims.Count}";
        }

        private void lbVictimID_DoubleClick(object sender, EventArgs e)
        {
            EnableVictimEditor(sender);
        }

        private void lbVictimID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                EnableVictimEditor(sender);
        }

        private void lbVictimID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                EnableVictimEditor(sender);
            }
            // copy list
            else if ((e.KeyCode == Keys.C || e.KeyCode == Keys.X) && (e.Modifiers & Keys.Control) == Keys.Control && (e.Modifiers & Keys.Shift) == Keys.Shift)
            {
                if (lbVictimID.Items.Count <= 0) return;

                StringBuilder sb = new StringBuilder();
                foreach (object item in lbVictimID.Items)
                {
                    sb.Append(item + Environment.NewLine);
                }
                if (sb.Length > 0)
                    Clipboard.SetDataObject(sb.ToString(), true, 10, 100);

                if (e.KeyCode == Keys.X) // clear
                {
                    entity.Victims.Clear();
                    lbVictimID.Items.Clear();
                    UpdateVictim();
                }
            }
            // pate list
            else if (e.KeyCode == Keys.V && (e.Modifiers & Keys.Control) == Keys.Control && (e.Modifiers & Keys.Shift) == Keys.Shift)
            {
                StringReader sr = new StringReader(Clipboard.GetText());
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (lbVictimID.Items.Count >= 1023) break;
                    var stripped = Regex.Replace(line, "[^0-9]", "");
                    if (stripped.Length > 0)
                    {
                        short victimid = Convert.ToInt16(stripped);
                        entity.Victims.Add(new(victimid));
                        lbVictimID.Items.Add(victimid);
                    }
                }
                if (lbVictimID.Items.Count > 0 && lbVictimID.SelectedIndex == -1)
                    lbVictimID.SelectedIndex = 0;
                UpdateVictim();
            }
            // copy selected item's eid
            else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                if (lbVictimID.Items.Count <= 0) return;

                string s = lbVictimID.Items[victimlistindex].ToString();
                Clipboard.SetDataObject(s, true, 10, 100);
            }
            // paste eid to selected item
            else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                if (lbVictimID.Items.Count <= 0) return;

                string s = Clipboard.GetText();
                var match = Regex.Match(s, @"\d+");
                if (match.Success)
                {
                    short victimid = Convert.ToInt16(match.Value);
                    entity.Victims[victimlistindex] = new EntityVictim(victimid);
                    lbVictimID.Items[victimlistindex] = victimid;
                    UpdateVictim();
                }
            }
        }

        private void EnableVictimEditor(object sender)
        {
            if (lbVictimID.Items.Count <= 0) return;

            lbVictimID = (DarkListBox)sender;
            numEditVictimID.Enabled = true;
            numEditVictimID.Value = entity.Victims[victimlistindex].VictimID;
            numEditVictimID.Focus();
            numEditVictimID.Select(0, numEditVictimID.Text.Length);
            numEditVictimID.KeyPress += new KeyPressEventHandler(VictimEditor_EditOver);
            numEditVictimID.LostFocus += VictimEditor_FocusOver;
        }

        private void VictimEditor_FocusOver(object sender, EventArgs e)
        {
            UpdateVictimList(false);
        }

        private void VictimEditor_EditOver(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                // avoid to play "Ding" sound
                e.Handled = true;
                e.KeyChar = (char)Keys.D0;

                UpdateVictimList(false);
            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                UpdateVictimList(true);
            }
        }

        private void UpdateVictimList(bool cancel)
        {
            // if the input is empty
            if (numEditVictimID.Text == "")
            {
                numEditVictimID.Value = 0;
            }
            // if the number is invalid or pressed escape key
            else if (numEditVictimID.Value > 32767 || cancel)
            {
                numEditVictimID.Value = entity.Victims[victimlistindex].VictimID;
            }
            else
            {
                entity.Victims[victimlistindex] = new EntityVictim((short)numEditVictimID.Value);
                lbVictimID.Items[victimlistindex] = numEditVictimID.Value;
            }
            UpdateVictim();
            numEditVictimID.Enabled = false;
            lbVictimID.Focus();
        }

        private void cmdInsertVictim_Click(object sender, EventArgs e)
        {
            if (entity.Victims.Count > 0)
            {
                entity.Victims.Insert(victimlistindex, entity.Victims[victimlistindex]);
                lbVictimID.Items.Insert(victimlistindex, entity.Victims[victimlistindex].VictimID);
            }
            else
            {
                entity.Victims.Add(new EntityVictim(10));
                lbVictimID.Items.Add(10);
                victimindex = 0;
                lbVictimID.SelectedIndex = 0;
            }
            UpdateVictim();
        }

        private void cmdRemoveVictim_Click(object sender, EventArgs e)
        {
            int selectedindex = victimlistindex;
            entity.Victims.RemoveAt(victimlistindex);
            lbVictimID.Items.RemoveAt(victimlistindex);
            UpdateVictim();
            if (lbVictimID.Items.Count > 0)
            {
                if (selectedindex >= lbVictimID.Items.Count)
                    selectedindex = lbVictimID.Items.Count - 1;
                lbVictimID.Focus();
                lbVictimID.SelectedIndex = selectedindex;
            }
        }

        private void cmdClearAllVictims_Click(object sender, EventArgs e)
        {
            entity.Victims.Clear();
            lbVictimID.Items.Clear();
            UpdateVictim();
        }

        private bool IsWithinDistance(EntityPosition a, EntityPosition b)
        {
            int dx = a.X - b.X;
            int dy = a.Y - b.Y;
            int dz = a.Z - b.Z;
            long threshold = Convert.ToInt64(numVictimDistance.Value);

            long distanceSq = (long)dx * dx + (long)dy * dy + (long)dz * dz;
            long thresholdSq = threshold * threshold;

            return distanceSq < thresholdSq;
        }

        private bool IsExplosives(Entity ent)
        {
            return ent.Type == 34 && (ent.Subtype == 0 || ent.Subtype == 11 || ent.Subtype == 18);
        }

        private static readonly HashSet<int?> BreakableSubtypes = [0, 2, 3, 4, 6, 8, 9, 10, 11, 12, 17, 18];

        private bool IsBreakableCrates(Entity ent)
        {
            return ent.Type == 34 && BreakableSubtypes.Contains(ent.Subtype);
        }

        private void cmdCalculateVictims_Click(object sender, EventArgs e)
        {
            dirty.Push(true);

            List<short> affectedIds = new();
            List<Entity> affectedTNTs = new(); // Used to update victims

            HashSet<int?> processedTNTs = new();
            Queue<Entity> TNTQueue = new();

            affectedIds.Add(Convert.ToInt16(entity.ID));
            affectedTNTs.Add(entity);

            TNTQueue.Enqueue(entity);
            while (TNTQueue.Count > 0)
            {
                Entity currentTNT = TNTQueue.Dequeue();

                if (processedTNTs.Contains(currentTNT.ID))
                    continue;
                processedTNTs.Add(currentTNT.ID);

                EntityPosition basePos = currentTNT.Positions[0];

                foreach (Entity target in zone.Entities)
                {
                    if (!IsBreakableCrates(target) || target.Positions.Count == 0 || target.ID == null)
                        continue;

                    EntityPosition targetPos = target.Positions[0];

                    if (IsWithinDistance(basePos, targetPos))
                    {
                        short tid = Convert.ToInt16(target.ID);
                        if (!affectedIds.Contains(tid))
                            affectedIds.Add(tid);

                        if (IsExplosives(target))
                        {
                            if (!processedTNTs.Contains(target.ID))
                            {
                                TNTQueue.Enqueue(target);
                                if (target.Subtype == 0)
                                {
                                    affectedTNTs.Add(target);
                                }
                            }
                        }
                    }
                }
            }

            foreach (Entity ent in affectedTNTs)
            {
                ent.Victims.Clear();
            }
            lbVictimID.Items.Clear();

            foreach (short id in affectedIds)
            {
                foreach (Entity ent in affectedTNTs)
                {
                    ent.Victims.Add(new(id));
                }
                lbVictimID.Items.Add(id);
            }

            if (lbVictimID.Items.Count > 0 && lbVictimID.SelectedIndex == -1)
                lbVictimID.SelectedIndex = 0;
            UpdateVictim();
            dirty.Pop();
        }

        #endregion

        #region BoxCount

        private void UpdateBoxCount()
        {
            if (entity.BoxCount.HasValue)
                numBoxCount.Value = entity.BoxCount.Value.ValueB;
            else
                numBoxCount.Value = 0;

            numBoxCount.Enabled = entity.BoxCount.HasValue;
            chkBoxCount.Checked = entity.BoxCount.HasValue;

            if (entity.BonusBoxCount.HasValue)
                numBonusBoxCount.Value = entity.BonusBoxCount.Value.ValueB;
            else
                numBonusBoxCount.Value = 0;

            numBonusBoxCount.Enabled = entity.BonusBoxCount.HasValue;
            chkBonusBoxCount.Checked = entity.BonusBoxCount.HasValue;
        }

        private void chkBoxCount_CheckedChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            numBoxCount.Enabled = chkBoxCount.Checked;
            if (chkBoxCount.Checked)
            {
                entity.BoxCount = new EntitySetting(0, (int)numBoxCount.Value);
            }
            else
            {
                entity.BoxCount = null;
            }
        }

        private void numBoxCount_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            entity.BoxCount = new EntitySetting(0, (int)numBoxCount.Value);
        }

        private void chkBonusBoxCount_CheckedChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            numBonusBoxCount.Enabled = chkBonusBoxCount.Checked;
            if (chkBonusBoxCount.Checked)
            {
                entity.BonusBoxCount = new EntitySetting(0, (int)numBonusBoxCount.Value);
            }
            else
            {
                entity.BonusBoxCount = null;
            }
        }

        private void numBonusBoxCount_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            entity.BonusBoxCount = new EntitySetting(0, (int)numBonusBoxCount.Value);
        }

        #endregion

        #region DDASection

        private void UpdateDDASection()
        {
            if (entity.DDASection.HasValue)
                numDDASection.Value = entity.DDASection.Value;
            else
                numDDASection.Value = 0;

            numDDASection.Enabled = entity.DDASection.HasValue;
            chkDDASection.Checked = entity.DDASection.HasValue;
        }

        private void chkDDASection_CheckedChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            numDDASection.Enabled = chkDDASection.Checked;
            if (chkDDASection.Checked)
            {
                entity.DDASection = (int)numDDASection.Value;
            }
            else
            {
                entity.DDASection = null;
            }
            UpdateOtherEditors();
        }

        private void numDDASection_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            entity.DDASection = (int)numDDASection.Value;
            UpdateOtherEditors();
        }

        #endregion

        #region DDASettings

        private void UpdateDDASettings()
        {
            if (entity.DDASettings.HasValue)
                numDDASettings.Value = entity.DDASettings.Value >> 8;
            else
                numDDASettings.Value = 0;

            numDDASettings.Enabled = entity.DDASettings.HasValue;
            chkDDASettings.Checked = entity.DDASettings.HasValue;
        }

        private void chkDDASettings_CheckedChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            numDDASettings.Enabled = chkDDASettings.Checked;
            if (chkDDASettings.Checked)
            {
                entity.DDASettings = (int)numDDASettings.Value << 8;
            }
            else
            {
                entity.DDASettings = null;
            }
            UpdateOtherEditors();
        }

        private void numDDASettings_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            entity.DDASettings = (int)numDDASettings.Value << 8;
            UpdateOtherEditors();
        }

        #endregion

        #region DrawOverride

        private void UpdateDrawOverride()
        {
            if (entity.DrawOverrideID.HasValue)
                numDrawOverrideId.Value = entity.DrawOverrideID.Value.ValueB;
            else
                numDrawOverrideId.Value = 0;

            numDrawOverrideId.Enabled = entity.DrawOverrideID.HasValue;
            chkDrawOverrideId.Checked = entity.DrawOverrideID.HasValue;

            if (entity.DrawOverrideMult.HasValue)
                numDrawOverrideMult.Value = entity.DrawOverrideMult.Value.ValueB;
            else
                numDrawOverrideMult.Value = 0;

            numDrawOverrideMult.Enabled = entity.DrawOverrideMult.HasValue;
            chkDrawOverrideMult.Checked = entity.DrawOverrideMult.HasValue;
        }

        private void chkDrawOverrideId_Changed(object sender, EventArgs e)
        {
            if (Dirty) return;
            numDrawOverrideId.Enabled = chkDrawOverrideId.Checked;
            if (chkDrawOverrideId.Checked)
            {
                entity.DrawOverrideID = new EntitySetting(0, (int)numDrawOverrideId.Value);
            }
            else
            {
                entity.DrawOverrideID = null;
            }
        }

        private void numDrawOverrideId_Changed(object sender, EventArgs e)
        {
            if (Dirty) return;
            entity.DrawOverrideID = new EntitySetting(0, (int)numDrawOverrideId.Value);
        }

        private void chkDrawOverrideMult_Changed(object sender, EventArgs e)
        {
            if (Dirty) return;
            numDrawOverrideMult.Enabled = chkDrawOverrideMult.Checked;
            if (chkDrawOverrideMult.Checked)
            {
                entity.DrawOverrideMult = new EntitySetting(0, (int)numDrawOverrideMult.Value);
            }
            else
            {
                entity.DrawOverrideMult = null;
            }
        }

        private void numDrawOverrideMult_Changed(object sender, EventArgs e)
        {
            if (Dirty) return;
            entity.DrawOverrideMult = new EntitySetting(0, (int)numDrawOverrideMult.Value);
        }

        #endregion

        public void GetUpdateDDA(Entity entity, int colIdx, int? value)
        {
            dirty.Push(true);

            if (colIdx == 0)
                entity.DDASection = value;
            else if (colIdx == 1)
                entity.DDASettings = value << 8;

            if (this.entity == entity)
            {
                UpdateDDASection();
                UpdateDDASettings();
            }

            dirty.Pop();
        }

        private void UpdateOtherEditors()
        {
            if (frmDDAEditor != null)
                frmDDAEditor.UpdateDDAList();
        }

        private void ScrollHandlerFunction(object? sender, MouseEventArgs e)
        {
            if (sender is NumericUpDown numericUpDown)
            {
                HandledMouseEventArgs handledArgs = e as HandledMouseEventArgs;
                if (handledArgs != null)
                    handledArgs.Handled = true;

                decimal newValue = numericUpDown.Value;
                if (e.Delta > 0 && newValue < numericUpDown.Maximum)
                    newValue += numericUpDown.Increment;

                else if (e.Delta < 0 && newValue > numericUpDown.Minimum)
                    newValue -= numericUpDown.Increment;

                numericUpDown.Value = newValue;
            }
        }

        private void CloseEntityEditor()
        {
            frmDDAEditor?.Dispose();
        }

        
    }

    public class DDAEditor : DarkForm
    {
        private EntityEditor editor;
        private NSF nsf;

        private DataGridView dgvDDA;

        private readonly int ColDDASection = 0;
        private readonly int ColDDASettings = 1;
        private readonly int ColName = 2;
        private readonly int ColID = 3;
        private readonly int ColZone = 4;

        internal Stack<bool> dirty = [];
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        public DDAEditor(EntityEditor editor)
        {
            this.editor = editor;
            nsf = editor.nsf;
            Text = "DDA List";
            Icon = Embeds.GetIcon("List");
            Size = new Size(434, 598);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(300, 222);
            MaximumSize = new Size(Size.Width, 8192);
            TopMost = true;

            dgvDDA = new()
            {
                AllowUserToAddRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                ColumnHeadersHeight = 36,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                Dock = DockStyle.Fill,
                MultiSelect = false,
                RowHeadersWidth = 24,
                RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing,
                ShowCellToolTips = false,
                SelectionMode = DataGridViewSelectionMode.CellSelect
            };
            DoubleBufferedDataGridView.Initialize(dgvDDA);
            dgvDDA.CellFormatting += dgvDDA_CellFormatting;
            dgvDDA.SelectionChanged += dgvDDA_SelectionChanged;
            dgvDDA.CellBeginEdit += dgvDDA_CellBeginEdit;
            dgvDDA.CellValidating += dgvDDA_CellValidating;
            dgvDDA.CellValueChanged += dgvDDA_CellValueChanged;
            dgvDDA.CellPainting += dgv_CellPainting;

            dgvDDA.Columns.Add("DDA Section", "DDA Section");
            dgvDDA.Columns.Add("DDA Settings", "DDA Settings");
            dgvDDA.Columns.Add("Name", "Name");
            dgvDDA.Columns.Add("ID", "ID");
            dgvDDA.Columns.Add("Zone", "Zone");
            foreach (DataGridViewColumn column in dgvDDA.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 48;
            }
            dgvDDA.Columns[ColDDASection].Width = 60;
            dgvDDA.Columns[ColDDASettings].Width = 60;
            dgvDDA.Columns[ColName].Width = 160;

            UpdateDDAList();

            Controls.Add(dgvDDA);
        }

        private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgv = (DataGridView)sender;
            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.Selected)
            {
                Color defaultFore = dgv.DefaultCellStyle.ForeColor;
                Color defaultSelFore = dgv.DefaultCellStyle.SelectionForeColor;

                bool hasCustomColor = cell.Style.ForeColor != Color.Empty &&
                                      cell.Style.ForeColor != defaultFore;
                if (hasCustomColor)
                {
                    dgv.DefaultCellStyle.SelectionForeColor = cell.Style.ForeColor;
                }
                else
                {
                    dgv.DefaultCellStyle.SelectionForeColor = defaultFore;
                }
            }
        }


        public void UpdateDDASelection()
        {
            foreach (DataGridViewRow row in dgvDDA.Rows)
            {
                if (editor.entity == (Entity)row.Tag)
                {
                    row.Cells[ColName].Style.ForeColor = Color.Turquoise;
                }
                else
                {
                    row.Cells[ColName].Style.ForeColor = Color.White;
                }
            }
        }

        private void dgvDDA_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string text = dgvDDA.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";
            if (text == "-")
            {
                e.CellStyle.ForeColor = Color.Gray;
                return;
            }
        }

        private void dgvDDA_SelectionChanged(object? sender, EventArgs e)
        {
            if (Dirty) return;
            if (dgvDDA.SelectedCells.Count == 0) return;

            int rowIdx = dgvDDA.SelectedCells[0].RowIndex;
            editor.FindEntityFromDDAList((Entity)dgvDDA.Rows[rowIdx].Tag);
        }

        private void dgvDDA_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex >= ColName)
                e.Cancel = true;
        }

        private void dgvDDA_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvDDA.SelectedCells.Count == 0) return;
            if (e.ColumnIndex >= ColName) return;

            string inputValue = e.FormattedValue?.ToString() ?? "";
            if (inputValue == "-") return;

            if (int.TryParse(inputValue, out int newValue))
            {
                int maxValue = int.MaxValue;
                int minValue = int.MinValue;
                if (newValue > maxValue)
                {
                    DarkMessageBox.ShowError($"Invalid input. The value must be less than or equal to {maxValue}.", Resources.Title_InputError);
                    e.Cancel = true;
                }
                else if (newValue < minValue)
                {
                    DarkMessageBox.ShowError($"Invalid input. The value must be greater than or equal to {minValue}.", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
            else
            {
                DarkMessageBox.ShowError($"Invalid input.", Resources.Title_InputError);
                e.Cancel = true;
            }
        }

        private void dgvDDA_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Dirty) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            dirty.Push(true);

            var row = dgvDDA.Rows[e.RowIndex];
            var cell = row.Cells[e.ColumnIndex];
            string text = cell.Value?.ToString() ?? "";
            bool isNull = text == "-";

            int? value = isNull ? null : Convert.ToInt32(cell.Value);
            editor.GetUpdateDDA((Entity)row.Tag, e.ColumnIndex, value);

            string? name = row.Cells[ColName].Value?.ToString();
            string? id = row.Cells[ColID].Value?.ToString();
            string? zone = row.Cells[ColZone].Value?.ToString();
            UpdateDDAList();
            RestoreSelection(e.ColumnIndex, name, id, zone);

            dirty.Pop();
        }

        private void RestoreSelection(int colIndex, string? name, string? id, string? zone)
        {
            foreach (DataGridViewRow newRow in dgvDDA.Rows)
            {
                if (newRow.Cells[ColName].Value?.ToString() == name &&
                    newRow.Cells[ColID].Value?.ToString() == id &&
                    newRow.Cells[ColZone].Value?.ToString() == zone)
                {
                    newRow.Cells[colIndex].Selected = true;
                    dgvDDA.CurrentCell = newRow.Cells[colIndex];
                    return;
                }
            }
            dgvDDA.ClearSelection();
            dgvDDA.CurrentCell = null;
        }

        public void UpdateDDAList()
        {
            dirty.Push(true);
            dgvDDA.SuspendLayout();
            dgvDDA.ScrollBars = ScrollBars.None;
            dgvDDA.Rows.Clear();

            foreach (ZoneEntry zone in nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity entity in zone.Entities)
                {
                    string? ddaSection = entity.DDASection?.ToString() ?? "-";
                    string? ddaSettings = (entity.DDASettings >> 8)?.ToString() ?? "-";
                    string? name = entity.Name ?? "-";
                    string? id = entity.ID?.ToString() ?? "-";
                    if (ddaSection != "-" || ddaSettings != "-")
                    {
                        DataGridViewRow row = new();
                        row.CreateCells(
                            dgvDDA,
                            ddaSection,
                            ddaSettings,
                            name,
                            id,
                            zone.EName
                        );
                        row.Tag = entity;
                        dgvDDA.Rows.Add(row);
                    }
                }
            }

            SortRows();
            dgvDDA.ClearSelection();
            dgvDDA.CurrentCell = null;
            dgvDDA.ScrollBars = ScrollBars.Vertical;
            dgvDDA.ResumeLayout();

            UpdateDDASelection();
            dirty.Pop();
        }

        private void SortRows()
        {
            var rows = dgvDDA.Rows
                .Cast<DataGridViewRow>()
                .OrderBy(r => GetSafeInt(r.Cells[ColDDASection].Value, int.MaxValue))
                .ThenBy(r => GetSafeInt(r.Cells[ColDDASettings].Value, int.MinValue))
                .ToList();

            dgvDDA.Rows.Clear();
            dgvDDA.Rows.AddRange(rows.ToArray());

            int index = -1;
            int i = 0;
            foreach (DataGridViewRow row in dgvDDA.Rows)
            {
                if (int.TryParse(row.Cells[ColDDASection].Value.ToString(), out int value))
                {
                    if (index != value)
                    {
                        i = (i + 1) % 2;
                        index = value;
                    }

                    if (i == 0)
                        row.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
                    else
                        row.DefaultCellStyle.BackColor = Color.FromArgb(34, 34, 34);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
                }
            }
        }

        private int GetSafeInt(object value, int refval)
        {
            if (value is int i)
                return i;
            if (int.TryParse(value.ToString(), out int result))
                return result;

            return refval;
        }
    }

    public static class ListBoxExtensions
    {
        private const int WM_SETREDRAW = 0x0B;

        public static void BeginUpdate(this ListBox lb)
        {
            SendMessage(lb.Handle, WM_SETREDRAW, false, 0);
        }

        public static void EndUpdate(this ListBox lb)
        {
            SendMessage(lb.Handle, WM_SETREDRAW, true, 0);
            lb.Refresh();
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, bool wParam, int lParam);
    }

}
