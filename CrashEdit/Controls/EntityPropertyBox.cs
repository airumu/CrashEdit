using System.ComponentModel;
using System.Text.Json;
using System.Text.RegularExpressions;
using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public partial class EntityPropertyBox : UserControl
    {
        private EntityController controller;
        private Entity entity;

        // Predefined and editable fields
        private BindingList<string> listKnownFields;
        // Predefined fields, includes not editable ones
        private BindingList<string> listAllKnownFields;

        // Saved items list
        private List<ListItem> savedItems = new List<ListItem>();

        private DarkToolTip tipProperties = new DarkToolTip();
        private DarkToolTip tipSavedProperties = new DarkToolTip();
        private DarkToolTip tipReloadTPage = new DarkToolTip();
        private DataGridView currentDataGridView;
        private object selectedField;

        private bool propertyStyle = false;
        private bool metavalueDirty = false;
        private bool valueDirty = false;
        private bool propertyShowAsHex = true;

        private const string NullMeta = "-";
        private const string FilePath = "CrashEdit.exe.savedentityproperties.json";

        public class FieldData
        {
            public short Id { get; set; }
            public string FieldType { get; set; }
            public object Field { get; set; }
            public string Comment { get; set; }
        }
        public class ListItem
        {
            public string Name { get; set; }
            public List<FieldData> Fields { get; set; } = new List<FieldData>();
        }

        public EntityPropertyBox(EntityController controller)
        {
            this.controller = controller;
            entity = controller.Entity;
        }

        public void OnTabSelected()
        {
            EntityPropertyBox_Enter(this, EventArgs.Empty);
        }

        private void EntityPropertyBox_Enter(object sender, EventArgs e)
        {
            InitializeComponent();

            DoubleBufferedDataGridView.Initialize(dgvPropertyMetaValues);
            DoubleBufferedDataGridView.Initialize(dgvPropertyValues);
            DoubleBufferedDataGridView.Initialize(dgvSavePropertyValues);

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem insertRowItem = new ToolStripMenuItem("Insert Row");
            ToolStripMenuItem deleteRowItem = new ToolStripMenuItem("Delete Row");

            insertRowItem.Click += InsertRowItem_Click;
            deleteRowItem.Click += DeleteRowItem_Click;
            contextMenu.Items.Add(insertRowItem);
            contextMenu.Items.Add(deleteRowItem);

            dgvPropertyMetaValues.ContextMenuStrip = contextMenu;
            dgvPropertyValues.ContextMenuStrip = contextMenu;
            dgvPropertyMetaValues.CellMouseDown += DataGridView_CellMouseDown;
            dgvPropertyValues.CellMouseDown += DataGridView_CellMouseDown;

            tipSavedProperties.SetToolTip(lbProperties, Resources.EntityPropertyBox_tipProperties);
            tipSavedProperties.SetToolTip(lbSavedProperties, Resources.EntityPropertyBox_tipSavedProperties);
            tipReloadTPage.SetToolTip(rbtReload, "Reload");

            chkPropertyShowAsHex.Checked = propertyShowAsHex;
            CreatePropertyHeaderColumns();
            CreatePropertyMetaValuesColumns();
            CreateSavedPropertyListColumns();
            CreateSavedPropertyValuesColumns();
            UpdatePropertyIDList();
            LoadItemsFromFile();
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
                }
            }
        }

        private void InsertRowItem_Click(object sender, EventArgs e)
        {
            if (currentDataGridView == null)
            {
                DarkMessageBox.ShowError("Please select a row to insert.", Resources.Title_Error);
                return;
            }
            if (lbProperties.SelectedItem == null) return;

            dynamic field = selectedField;
            if (field == null) return;

            if (currentDataGridView == dgvPropertyMetaValues)
            {
                dynamic newRow = null!;
                int selectedRow = -1;

                if (!(dgvPropertyMetaValues.SelectedCells.Count > 0)) return;
                if (dgvPropertyMetaValues.SelectedCells[0].Value == NullMeta)
                {
                    DarkMessageBox.ShowError("Meta values cannot be added if the MetaValues flag is not set.", Resources.Title_Error);
                    return;
                }

                if (field == null || field.RowCount == 0)
                {
                    if (field is EntityVictimProperty)
                    {
                        field = new EntityVictimProperty();
                        field.Rows.Add(new EntityPropertyRow<EntityVictim>());
                    }
                    else if (field is EntityInt32Property)
                    {
                        field = new EntityInt32Property();
                        field.Rows.Add(new EntityPropertyRow<int>());
                    }
                    else if (field is EntityUInt32Property)
                    {
                        field = new EntityUInt32Property();
                        field.Rows.Add(new EntityPropertyRow<uint>());
                    }
                    else if (field is EntitySettingProperty)
                    {
                        field = new EntitySettingProperty();
                        field.Rows.Add(new EntityPropertyRow<EntitySetting>());
                    }
                    else if (field is EntityUInt8Property)
                    {
                        field = new EntityUInt8Property();
                        field.Rows.Add(new EntityPropertyRow<byte>());
                    }
                    field.Rows[field.RowCount - 1].MetaValue = 0;
                    Console.WriteLine("Added MetaValue.");
                }
                else
                {
                    selectedRow = dgvPropertyMetaValues.SelectedCells[0].RowIndex;
                    if (field is EntityVictimProperty)
                    {
                        newRow = new EntityPropertyRow<EntityVictim>();
                    }
                    else if (field is EntityInt32Property)
                    {
                        newRow = new EntityPropertyRow<int>();
                    }
                    else if (field is EntityUInt32Property)
                    {
                        newRow = new EntityPropertyRow<uint>();
                    }
                    else if (field is EntitySettingProperty)
                    {
                        newRow = new EntityPropertyRow<EntitySetting>();
                    }
                    else if (field is EntityUInt8Property)
                    {
                        newRow = new EntityPropertyRow<byte>();
                    }
                    newRow.MetaValue = field.Rows[selectedRow].MetaValue;
                    foreach (var val in field.Rows[selectedRow].Values)
                        newRow.Values.Add(val);
                    field.Rows.Insert(selectedRow, newRow);
                    Console.WriteLine("Inserted MetaValue.");
                }

                if (selectedRow == -1)
                    dgvPropertyMetaValues.Rows.Add("0");
                else
                    dgvPropertyMetaValues.Rows.Insert(selectedRow, $"{newRow.MetaValue}");
            }
            else if (currentDataGridView == dgvPropertyValues)
            {
                dynamic newValue = null!;
                int selectedRow = -1;
                int rowindex = dgvPropertyMetaValues.SelectedCells[0].RowIndex;

                if (field is EntityVictimProperty)
                {
                    if (field.Rows[rowindex].Values.Count == 0)
                        field.Rows[rowindex].Values.Add(new EntityVictim());
                    else
                    {
                        selectedRow = dgvPropertyValues.SelectedCells[0].RowIndex;
                        newValue = field.Rows[rowindex].Values[selectedRow];
                        field.Rows[rowindex].Values.Insert(selectedRow, newValue);
                        newValue = newValue.VictimID; // for fixing the row value
                    }
                }
                else if (field is EntitySettingProperty)
                {
                    if (field.Rows[rowindex].Values.Count == 0)
                        field.Rows[rowindex].Values.Add(new EntitySetting());
                    else
                    {
                        selectedRow = dgvPropertyValues.SelectedCells[0].RowIndex;
                        newValue = field.Rows[rowindex].Values[selectedRow];
                        field.Rows[rowindex].Values.Insert(selectedRow, newValue);
                        newValue = newValue.Value; // for fixing the row value
                    }
                }
                else
                {
                    if (field.Rows[rowindex].Values.Count == 0)
                        field.Rows[rowindex].Values.Add(0);
                    else
                    {
                        selectedRow = dgvPropertyValues.SelectedCells[0].RowIndex;
                        newValue = field.Rows[rowindex].Values[selectedRow];
                        field.Rows[rowindex].Values.Insert(selectedRow, newValue);
                    }
                }

                if (field is EntityVictimProperty || field is EntitySettingProperty)
                {
                    if (selectedRow == -1)
                        dgvPropertyValues.Rows.Add("0");
                    else
                        dgvPropertyValues.Rows.Insert(selectedRow, newValue.ToString("X"));
                }
                else if (field is EntityUInt8Property)
                {
                    if (selectedRow == -1)
                        dgvPropertyValues.Rows.Add("00");
                    else
                        dgvPropertyValues.Rows.Insert(selectedRow, newValue.ToString("X2"));
                }
                else
                {
                    if (selectedRow == -1)
                        dgvPropertyValues.Rows.Add("0", "00", "00", "00", "00");
                    else
                    {
                        byte byte0 = (byte)(newValue & 0xFF);
                        byte byte1 = (byte)((newValue >> 8) & 0xFF);
                        byte byte2 = (byte)((newValue >> 16) & 0xFF);
                        byte byte3 = (byte)((newValue >> 24) & 0xFF);
                        dgvPropertyValues.Rows.Insert(selectedRow, newValue.ToString("X"), byte0.ToString("X2"), byte1.ToString("X2"), byte2.ToString("X2"), byte3.ToString("X2"));
                    }
                }
            }
            UpdatePropertyHeaderAndRaw();
        }

        private void DeleteRowItem_Click(object sender, EventArgs e)
        {
            if (currentDataGridView == null)
            {
                DarkMessageBox.ShowError("Please select a row to delete.", Resources.Title_Error);
                return;
            }
            if (lbProperties.SelectedItem == null || !(currentDataGridView.SelectedRows.Count > 0) || !(dgvPropertyMetaValues.SelectedCells.Count > 0)) return;
            int rowindex = dgvPropertyMetaValues.SelectedCells[0].RowIndex;

            dynamic field = selectedField;
            if (field == null) return;

            foreach (DataGridViewRow selectedRow in currentDataGridView.SelectedRows)
            {
                if (!selectedRow.IsNewRow)
                {
                    if (currentDataGridView == dgvPropertyMetaValues)
                    {
                        field.Rows.RemoveAt(selectedRow.Index);
                        if (field.RowCount == 0)
                        {
                            string str = lbProperties.SelectedItem.ToString();
                            short id = Convert.ToInt16(str, 16);
                            NullifyField(id);
                            entity.KnownProperties.Remove(id);
                            listKnownFields.Remove(str);
                            listAllKnownFields.Remove(str);
                        }
                    }
                    else if (currentDataGridView == dgvPropertyValues)
                    {
                        field.Rows[rowindex].Values.RemoveAt(selectedRow.Index);
                    }

                    currentDataGridView.Rows.Remove(selectedRow);
                }
            }
            UpdatePropertyHeaderAndRaw();
        }

        private void NullifyField(short id)
        {
            switch (id)
            {
                case 0x119: entity.Panning = null!; break;
                case 0x131: entity.Field0x131 = null!; break;
                case 0x142: entity.CameraDistance = null!; break;
                case 0x162: entity.Field0x162 = null!; break;
                case 0x16D: entity.Field0x16D = null!; break;
                case 0x16E: entity.Field0x16E = null!; break;
                case 0x176: entity.PathLinks = null!; break;
                case 0x183: entity.Field0x183 = null!; break;
                case 0x185: entity.Flags = null!; break;
                case 0x186: entity.Water = null!; break;
                case 0x198: entity.EventSender = null!; break;
                case 0x1A8: entity.Transitions = null!; break;
                case 0x1AA: entity.Field0x1AA = null!; break;
                case 0x1B5: entity.Particles1 = null!; break;
                case 0x1B6: entity.Particles2 = null!; break;
                case 0x1B7: entity.Field0x1B7 = null!; break;
                case 0x1B8: entity.FXControl = null!; break;
                case 0x1F9: entity.Field0x1F9 = null!; break;
                case 0x1DE: entity.FogDistance = null!; break;
                case 0x1FA: entity.Backgrounds = null!; break;
                case 0x252: entity.Field0x252 = null!; break;
                case 0x254: entity.Field0x254 = null!; break;
                case 0x27F: entity.SceneryUpdates = null!; break;
                case 0x297: entity.Mirrors = null!; break;
                case 0x2AA: entity.Stars = null!; break;
            }
        }

        private TProperty InitializeProperty<TProperty, TRow>()
            where TProperty : new()
            where TRow : new()
        {
            dynamic property = new TProperty();
            property.Rows.Add(new EntityPropertyRow<TRow>());
            property.Rows[0].MetaValue = 0;
            return property;
        }

        private object AddField(short id)
        {
            switch (id)
            {
                case 0x119:
                    entity.Panning = InitializeProperty<EntityVictimProperty, EntityVictim>();
                    return entity.Panning;
                case 0x131:
                    entity.Field0x131 = InitializeProperty<EntityVictimProperty, EntityVictim>();
                    return entity.Field0x131;
                case 0x142:
                    entity.CameraDistance = InitializeProperty<EntityVictimProperty, EntityVictim>();
                    return entity.CameraDistance;
                case 0x162:
                    entity.Field0x162 = InitializeProperty<EntityUInt32Property, uint>();
                    return entity.Field0x162;
                case 0x16D:
                    entity.Field0x16D = InitializeProperty<EntitySettingProperty, EntitySetting>();
                    return entity.Field0x16D;
                case 0x16E:
                    entity.Field0x16E = InitializeProperty<EntityVictimProperty, EntityVictim>();
                    return entity.Field0x16E;
                case 0x176:
                    entity.PathLinks = InitializeProperty<EntityVictimProperty, EntityVictim>();
                    return entity.PathLinks;
                case 0x1AA:
                    entity.Field0x1AA = InitializeProperty<EntityVictimProperty, EntityVictim>();
                    return entity.Field0x1AA;
                case 0x183:
                    entity.Field0x183 = InitializeProperty<EntityVictimProperty, EntityVictim>();
                    return entity.Field0x183;
                case 0x185:
                    entity.Flags = InitializeProperty<EntityUInt32Property, uint>();
                    return entity.Flags;
                case 0x186:
                    entity.Water = InitializeProperty<EntityUInt32Property, uint>();
                    return entity.Water;
                case 0x198:
                    entity.EventSender = InitializeProperty<EntitySettingProperty, EntitySetting>();
                    return entity.EventSender;
                case 0x1A8:
                    entity.Transitions = InitializeProperty<EntityUInt32Property, uint>();
                    return entity.Transitions;
                case 0x1B5:
                    entity.Particles1 = InitializeProperty<EntityVictimProperty, EntityVictim>();
                    return entity.Particles1;
                case 0x1B6:
                    entity.Particles2 = InitializeProperty<EntityUInt32Property, uint>();
                    return entity.Particles2;
                case 0x1B7:
                    entity.Field0x1B7 = InitializeProperty<EntityUInt32Property, uint>();
                    return entity.Field0x1B7;
                case 0x1B8:
                    entity.FXControl = InitializeProperty<EntityInt32Property, int>();
                    return entity.FXControl;
                case 0x1DE:
                    entity.FogDistance = InitializeProperty<EntityUInt32Property, uint>();
                    return entity.FogDistance;
                case 0x1F9:
                    entity.Field0x1F9 = InitializeProperty<EntityVictimProperty, EntityVictim>();
                    return entity.Field0x1F9;
                case 0x1FA:
                    entity.Backgrounds = InitializeProperty<EntityUInt32Property, uint>();
                    return entity.Backgrounds;
                case 0x252:
                    entity.Field0x252 = InitializeProperty<EntityUInt32Property, uint>();
                    return entity.Field0x252;
                case 0x254:
                    entity.Field0x254 = InitializeProperty<EntityVictimProperty, EntityVictim>();
                    return entity.Field0x254;
                case 0x27F:
                    entity.SceneryUpdates = InitializeProperty<EntityUInt8Property, byte>();
                    return entity.SceneryUpdates;
                case 0x297:
                    entity.Mirrors = InitializeProperty<EntityUInt32Property, uint>();
                    return entity.Mirrors;
                case 0x2AA:
                    entity.Stars = InitializeProperty<EntityVictimProperty, EntityVictim>();
                    return entity.Stars;
                default:
                    throw new ArgumentException("Unsupported or invalid field.");
            }
        }

        private void ReplaceField(short id, dynamic field)
        {
            switch (id)
            {
                case 0x119:
                    entity.Panning = field;
                    return;
                case 0x131:
                    entity.Field0x131 = field;
                    return;
                case 0x142:
                    entity.CameraDistance = field;
                    return;
                case 0x162:
                    entity.Field0x162 = field;
                    return;
                case 0x16D:
                    entity.Field0x16D = field;
                    return;
                case 0x16E:
                    entity.Field0x16E = field;
                    return;
                case 0x176:
                    entity.PathLinks = field;
                    return;
                case 0x1AA:
                    entity.Field0x1AA = field;
                    return;
                case 0x183:
                    entity.Field0x183 = field;
                    return;
                case 0x185:
                    entity.Flags = field;
                    return;
                case 0x186:
                    entity.Water = field;
                    return;
                case 0x198:
                    entity.EventSender = field;
                    return;
                case 0x1A8:
                    entity.Transitions = field;
                    return;
                case 0x1B5:
                    entity.Particles1 = field;
                    return;
                case 0x1B6:
                    entity.Particles2 = field;
                    return;
                case 0x1B7:
                    entity.Field0x1B7 = field;
                    return;
                case 0x1B8:
                    entity.FXControl = field;
                    return;
                case 0x1DE:
                    entity.FogDistance = field;
                    return;
                case 0x1F9:
                    entity.Field0x1F9 = field;
                    return;
                case 0x1FA:
                    entity.Backgrounds = field;
                    return;
                case 0x252:
                    entity.Field0x252 = field;
                    return;
                case 0x254:
                    entity.Field0x254 = field;
                    return;
                case 0x27F:
                    entity.SceneryUpdates = field;
                    return;
                case 0x297:
                    entity.Mirrors = field;
                    return;
                case 0x2AA:
                    entity.Stars = field;
                    return;
                default:
                    throw new ArgumentException("Unsupported or invalid field.");
            }
        }

        // Camera 1
        private const short Field0x162 = 0x162;     // ?
        private const short Field0x16D = 0x16D;     // ?
        private const short PathLinks = 0x176;
        private const short EventSender = 0x198;    // Sends an event with args to the player, used in secret warp transitions
        private const short Transitions = 0x1A8;    // Transitions related
        private const short Field0x1AA = 0x1AA;     // ?
        private const short SceneryUpdates = 0x27F; // Scenery position/drawing updates
        // Camera 2
        private const short Panning = 0x119;
        private const short Field0x131 = 0x131;     // ?
        private const short CameraDistance = 0x142;
        private const short Field0x16E = 0x16E;     // ?
        private const short Field0x183 = 0x183;     // ?
        private const short Flags = 0x185;
        private const short Water = 0x186;          // Water(ponds/river) related [{Y coord}, {colors}, {?}, {z-index above the water surface}, {z-index below the water surface}]
        private const short Particles1 = 0x1B5;     // Particles(like rain and snow) properties [{X vel}, {Y vel}, {Z vel}, {amount}, {Y offset}, {?}]
        private const short Particles2 = 0x1B6;     // Particles properties [{upper color}, {lower color}, {?}]
        private const short Field0x1B7 = 0x1B7;     // ? Used in Totally Bear and night jungle levels, darkness related?
        private const short FXControl = 0x1B8;      // Vertex FX related, such as controlling sewer pond vertices
        private const short FogDistance = 0x1DE;
        private const short Field0x1F9 = 0x1F9;     // ?
        private const short Backgrounds = 0x1FA;
        private const short Field0x252 = 0x252;     // Camer free movement related?
        private const short Field0x254 = 0x254;     // Camer free movement related
        private const short Mirrors = 0x297;        // Mirror related [{Y coord}, {objects z-index}, {reflections z-index}]
        private const short Stars = 0x2AA;

        private readonly HashSet<short> validFields = new HashSet<short> { Panning, Field0x131, CameraDistance, Field0x162, Field0x16D, Field0x16E, PathLinks, Field0x183,
            Flags, Water, EventSender, Transitions, Field0x1AA, Particles1, Particles2, Field0x1B7, FXControl, FogDistance, Field0x1F9, Backgrounds, Field0x252, Field0x254, SceneryUpdates, Mirrors, Stars };

        private object GetField(short id)
        {
            return id switch
            {
                Panning => entity.Panning,
                Field0x131 => entity.Field0x131,
                CameraDistance => entity.CameraDistance,
                Field0x162 => entity.Field0x162,
                Field0x16D => entity.Field0x16D,
                Field0x16E => entity.Field0x16E,
                PathLinks => entity.PathLinks,
                Field0x183 => entity.Field0x183,
                Flags => entity.Flags,
                Water => entity.Water,
                EventSender => entity.EventSender,
                Transitions => entity.Transitions,
                Field0x1AA => entity.Field0x1AA,
                Particles1 => entity.Particles1,
                Field0x1B7 => entity.Field0x1B7,
                Particles2 => entity.Particles2,
                FXControl => entity.FXControl,
                FogDistance => entity.FogDistance,
                Field0x1F9 => entity.Field0x1F9,
                Backgrounds => entity.Backgrounds,
                Field0x252 => entity.Field0x252,
                Field0x254 => entity.Field0x254,
                SceneryUpdates => entity.SceneryUpdates,
                Mirrors => entity.Mirrors,
                Stars => entity.Stars,
                _ => null!
            };
        }

        private void CreatePropertyHeaderColumns()
        {
            lvPropertyHeader.Columns.Add("Type");
            lvPropertyHeader.Columns.Add("Element Size");
            lvPropertyHeader.Columns.Add("Row Count");
            lvPropertyHeader.Columns.Add("Sparse");
            lvPropertyHeader.Columns.Add("MetaValues");
            lvPropertyHeader.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void CreatePropertyMetaValuesColumns()
        {
            dgvPropertyMetaValues.Columns.Clear();
            string columnMetaValue = "Position";
            dgvPropertyMetaValues.Columns.Add(columnMetaValue, columnMetaValue);
            dgvPropertyMetaValues.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void CreatePropertyValuesColumns()
        {
            dgvPropertyValues.Columns.Clear();

            string columnValue = "Value";
            dgvPropertyValues.Columns.Add(columnValue, columnValue);

            if (!(selectedField is EntityVictimProperty || selectedField is EntitySettingProperty || selectedField is EntityUInt8Property))
            {
                string columnValue1 = "Seg1";
                string columnValue2 = "Seg2";
                string columnValue3 = "Seg3";
                string columnValue4 = "Seg4";
                dgvPropertyValues.Columns.Add(columnValue1, columnValue1);
                dgvPropertyValues.Columns.Add(columnValue2, columnValue2);
                dgvPropertyValues.Columns.Add(columnValue3, columnValue3);
                dgvPropertyValues.Columns.Add(columnValue4, columnValue4);
                if (propertyStyle)
                {
                    dgvPropertyValues.Columns[1].Visible = false;
                    dgvPropertyValues.Columns[2].Visible = false;
                    dgvPropertyValues.Columns[3].Visible = false;
                    dgvPropertyValues.Columns[4].Visible = false;
                }
                else
                {
                    dgvPropertyValues.Columns[0].Visible = false;
                }
            }

            foreach (DataGridViewColumn column in dgvPropertyValues.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void CreateSavedPropertyListColumns()
        {
        }

        private void CreateSavedPropertyValuesColumns()
        {
            dgvSavePropertyValues.Columns.Add("ID", "ID");
            dgvSavePropertyValues.Columns.Add("Comment", "Comment");
            dgvSavePropertyValues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvSavePropertyValues.Columns[0].Width = 40;
            dgvSavePropertyValues.Columns[1].Width = 378;
            dgvSavePropertyValues.Visible = false;
        }

        private void UpdatePropertyIDList()
        {
            if (entity.KnownProperties != null && entity.KnownProperties.Count > 0)
            {
                lbProperties.Items.Clear();
                listAllKnownFields = new BindingList<string>();
                foreach (var item in entity.KnownProperties)
                {
                    listAllKnownFields.Add(item.Key.ToString("X"));
                }

                listKnownFields = new BindingList<string>(listAllKnownFields.Where(item =>
                    short.TryParse(item, System.Globalization.NumberStyles.HexNumber, null, out short field) &&
                    validFields.Contains(field)).ToList());

                lbProperties.DataSource = listKnownFields;
                LoadFieldFromSelectedItem();

                if (listKnownFields.Count == 0)
                {
                    dgvPropertyMetaValues.Visible =
                    dgvPropertyValues.Visible = false;
                    cmdRemoveProperty.Enabled =
                    cmdCopyProperty.Enabled = false;
                }
            }
        }

        private void AddSavedItem(string itemName, List<FieldData> fields)
        {
            var newItem = new ListItem
            {
                Name = itemName,
                Fields = fields
            };
            savedItems.Add(newItem);
            try
            {
                SaveItemsToFile();
                DarkMessageBox.ShowInformation($"Saved selected {fields.Count} field(s) successfully.", "Save Properties");
            }
            catch (Exception ex)
            {
                DarkMessageBox.ShowError($"Error saving fields: {ex.Message}", Resources.Title_Error);
            }
        }

        private void chkPropertyShowAllFields_CheckedChanged(object sender, EventArgs e)
        {
            string? selectedItem = lbProperties.SelectedItem?.ToString();

            lbProperties.DataSource = chkPropertyShowAllFields.Checked ? listAllKnownFields : listKnownFields;

            if (selectedItem != null && lbProperties.Items.Contains(selectedItem))
            {
                lbProperties.SelectedItem = selectedItem;
            }

            if ((chkPropertyShowAllFields.Checked && !(listAllKnownFields.Count > 0)) ||
                (!chkPropertyShowAllFields.Checked && !(listKnownFields.Count > 0)))
            {
                ClearPropertyControls();
            }
            else
            {
                cmdRemoveProperty.Enabled =
                cmdCopyProperty.Enabled = true;
            }
        }

        private void LoadFieldFromSelectedItem()
        {
            if (lbProperties.SelectedItem == null) return;
            short id = Convert.ToInt16(lbProperties.SelectedItem.ToString(), 16);

            chkPropertyMetaValue.Enabled = true;
            dgvPropertyMetaValues.Visible =
            dgvPropertyValues.Visible = true;

            object field = GetField(id);
            if (field is EntityVictimProperty victimProperty)
            {
                selectedField = victimProperty;
                lblFieldType.Text = "(short)";
            }
            else if (field is EntityInt32Property int32Property)
            {
                selectedField = int32Property;
                lblFieldType.Text = "(int32)";
            }
            else if (field is EntityUInt32Property uint32Property)
            {
                selectedField = uint32Property;
                lblFieldType.Text = "(uint32)";
            }
            else if (field is EntitySettingProperty entitySettingProperty)
            {
                selectedField = entitySettingProperty;
                lblFieldType.Text = "(int32)";
            }
            else if (field is EntityUInt8Property uint8Property)
            {
                selectedField = uint8Property;
                lblFieldType.Text = "(uint8)";
            }
            else
            {
                selectedField = null!;
                lblFieldType.Text = "";
                chkPropertyMetaValue.Enabled = false;
                dgvPropertyMetaValues.Visible =
                dgvPropertyValues.Visible = false;
            }

            string name = string.Empty;
            string text = string.Empty;
            lblUnsupportedProperty.ForeColor = Color.Red;
            if (entity.PropertyFields.ContainsKey(id))
            {
                name = $"({entity.PropertyFields[id].Name})";
                if (selectedField == null)
                {
                    text = $"Unsupported property field!\n{name}";
                }
                else
                {
                    text = $"\n{name}";
                    lblUnsupportedProperty.ForeColor = Color.Turquoise;
                }
            }
            else if (id == 0x29)
            {
                name = "(cameramode)";
                text = $"Unsupported property field!\n{name}";
            }
            else
            {
                text = $"Unknown property field!";
            }
            lblUnsupportedProperty.Visible = true;
            lblUnsupportedProperty.Text = text;
        }

        private void UpdatePropertyHeaderAndRaw()
        {
            if (lbProperties.SelectedItem == null) return;
            short id = Convert.ToInt16(lbProperties.SelectedItem.ToString(), 16);
            if (entity.KnownProperties.Keys.Contains(id))
            {
                var item = entity.KnownProperties[id];
                {
                    lvPropertyHeader.Items.Clear();

                    ListViewItem newitem = new ListViewItem(item.Type.ToString());
                    newitem.SubItems.Add(item.ElementSize.ToString());
                    newitem.SubItems.Add(item.RowCount.ToString());
                    newitem.SubItems.Add(item.IsSparse.ToString());
                    newitem.SubItems.Add(item.HasMetaValues.ToString());
                    lvPropertyHeader.Items.Add(newitem);
                }
                {
                    lbPropertyRaw.Items.Clear();

                    byte[] values = item.Save();
                    string result = string.Join(" ", values.Select(b => b.ToString("X2")));

                    lbPropertyRaw.Items.Add(result);
                }
            }
        }

        private void UpdatePropertyMetaValues()
        {
            if (lbProperties.SelectedItem == null) return;

            dgvPropertyMetaValues.Rows.Clear();

            dynamic field = selectedField;
            if (field == null) return;

            foreach (var row in field.Rows)
            {
                DataGridViewRow dgvRow = new DataGridViewRow();
                if (row.MetaValue == null)
                    dgvRow.CreateCells(dgvPropertyMetaValues, NullMeta);
                else
                    dgvRow.CreateCells(dgvPropertyMetaValues, row.MetaValue);
                dgvPropertyMetaValues.Rows.Add(dgvRow);

                metavalueDirty = true;
                chkPropertyMetaValue.Checked = row.MetaValue != null;
                metavalueDirty = false;
            }
        }

        private void UpdatePropertyValues()
        {
            if (lbProperties.SelectedItem == null) return;
            valueDirty = true;
            dgvPropertyValues.Rows.Clear();

            if (!(dgvPropertyMetaValues.SelectedCells.Count > 0) || !(dgvPropertyMetaValues.Rows.Count > 0)) return;
            int rowIndex = dgvPropertyMetaValues.SelectedCells[0].RowIndex;

            dynamic field = selectedField;
            if (field == null) return;

            if (field.Rows[rowIndex].Values.Count > 0)
            {
                foreach (var value in field.Rows[rowIndex].Values)
                {
                    if (propertyShowAsHex)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        if (field is EntityVictimProperty)
                        {
                            row.CreateCells(dgvPropertyValues, value.VictimID.ToString("X"));
                        }
                        else if (field is EntitySettingProperty)
                        {
                            row.CreateCells(dgvPropertyValues, value.Value);
                        }
                        else if (field is EntityUInt8Property)
                        {
                            row.CreateCells(dgvPropertyValues, value.ToString("X2"));
                        }
                        else
                        {
                            byte byte0 = (byte)(value & 0xFF);
                            byte byte1 = (byte)((value >> 8) & 0xFF);
                            byte byte2 = (byte)((value >> 16) & 0xFF);
                            byte byte3 = (byte)((value >> 24) & 0xFF);
                            row.CreateCells(dgvPropertyValues, value.ToString("X"), byte0.ToString("X2"), byte1.ToString("X2"), byte2.ToString("X2"), byte3.ToString("X2"));
                        }
                        dgvPropertyValues.Rows.Add(row);
                    }
                    else
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        if (field is EntityVictimProperty)
                        {
                            row.CreateCells(dgvPropertyValues, value.VictimID);
                        }
                        else if (field is EntitySettingProperty)
                        {
                            row.CreateCells(dgvPropertyValues, value.Value);
                        }
                        else if (field is EntityUInt8Property)
                        {
                            row.CreateCells(dgvPropertyValues, value);
                        }
                        else
                        {
                            byte byte0 = (byte)(value & 0xFF);
                            byte byte1 = (byte)((value >> 8) & 0xFF);
                            byte byte2 = (byte)((value >> 16) & 0xFF);
                            byte byte3 = (byte)((value >> 24) & 0xFF);
                            row.CreateCells(dgvPropertyValues, value, byte0, byte1, byte2, byte3);
                        }
                        dgvPropertyValues.Rows.Add(row);
                    }

                }
            }
            valueDirty = false;
        }

        private void dgvPropertyMetaValues_SelectionChanged(object sender, EventArgs e)
        {
            CreatePropertyValuesColumns();
            UpdatePropertyValues();
        }

        private void dgvPropertyMetaValues_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvPropertyMetaValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == NullMeta)
            {
                DarkMessageBox.ShowError("This cell cannot be edited.", Resources.Title_Error);
                e.Cancel = true;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
                   e.KeyChar != (char)Keys.Back &&
                   e.KeyChar != '-')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '-' && ((TextBox)sender).Text.Contains('-')) e.Handled = true;
            if (e.KeyChar == '-' && ((TextBox)sender).SelectionStart != 0) e.Handled = true;

            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
        }

        private void dgvPropertyMetaValues_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textbox)
            {
                textbox.KeyPress -= TextBox_KeyPress;
                textbox.KeyPress += TextBox_KeyPress;
            }
        }

        private void dgvPropertyMetaValues_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string inputValue = e.FormattedValue.ToString();
            if (inputValue == NullMeta) return;

            if (!Regex.IsMatch(inputValue, @"^-?[0-9]+$"))
            {
                DarkMessageBox.ShowError("Please enter a valid decimal value.", Resources.Title_InputError);
                dgvPropertyValues.CancelEdit();
                e.Cancel = true;
            }

            short minValue = short.MinValue;
            short maxValue = short.MaxValue;
            if (long.TryParse(inputValue, out long newValue))
            {
                if (newValue > maxValue)
                {
                    DarkMessageBox.ShowError($"The value must be less than or equal to\n{maxValue}.", Resources.Title_InputError);
                    dgvPropertyValues.CancelEdit();
                    e.Cancel = true;
                }
                else if (newValue < minValue)
                {
                    DarkMessageBox.ShowError($"The value must be greater than or equal to\n{minValue}.", Resources.Title_InputError);
                    dgvPropertyValues.CancelEdit();
                    e.Cancel = true;
                }
            }
        }

        private void dgvPropertyMetaValues_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (lbProperties.SelectedItem == null || dgvPropertyMetaValues.CurrentCell == null) return;
            if (dgvPropertyMetaValues.CurrentCell.Value == NullMeta) return;
            short newValue = Convert.ToInt16(dgvPropertyMetaValues.CurrentCell.Value);

            dynamic field = selectedField;
            if (field == null) return;

            field.Rows[e.RowIndex].MetaValue = newValue;
            UpdatePropertyHeaderAndRaw();
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (propertyShowAsHex)
            {
                if (!char.IsDigit(e.KeyChar) &&
                    (e.KeyChar < 'A' || e.KeyChar > 'F') &&
                    (e.KeyChar < 'a' || e.KeyChar > 'f') &&
                    e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar) &&
                    e.KeyChar != (char)Keys.Back &&
                    e.KeyChar != '-')
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '-' && ((TextBox)sender).Text.Contains('-')) e.Handled = true;
                if (e.KeyChar == '-' && ((TextBox)sender).SelectionStart != 0) e.Handled = true;
            }

            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
        }

        private void dgvPropertyValues_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvPropertyValues.CurrentCell == null || valueDirty) return;
            if (e.Control is TextBox textbox)
            {
                textbox.KeyPress -= TextBox2_KeyPress;
                textbox.KeyPress += TextBox2_KeyPress;

                if (propertyShowAsHex)
                {
                    if (dgvPropertyValues.CurrentCell.ColumnIndex != 0 || selectedField is EntityUInt8Property)
                        textbox.MaxLength = 2;
                    else if (selectedField is EntityVictimProperty)
                        textbox.MaxLength = 4;
                    else
                        textbox.MaxLength = 8;
                }
            }
        }

        private void dgvPropertyValues_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (valueDirty) return;
            string inputValue = e.FormattedValue.ToString();
            if (propertyShowAsHex)
            {
                if (!Regex.IsMatch(inputValue, @"\A\b[0-9a-fA-F]+\b\Z"))
                {
                    DarkMessageBox.ShowError("Please enter a valid hexadecimal value.", Resources.Title_InputError);
                    dgvPropertyValues.CancelEdit();
                    e.Cancel = true;
                }
            }
            else
            {
                if (!Regex.IsMatch(inputValue, @"^-?[0-9]+$"))
                {
                    DarkMessageBox.ShowError("Please enter a valid decimal value.", Resources.Title_InputError);
                    dgvPropertyValues.CancelEdit();
                    e.Cancel = true;
                }

                dynamic field = selectedField;
                if (field == null) return;

                long minValue = 0; long maxValue = 0;
                if (field is EntityVictimProperty)
                {
                    minValue = short.MinValue; maxValue = short.MaxValue;
                }
                else if (field is EntityInt32Property)
                {
                    if (propertyStyle)
                    {
                        minValue = int.MinValue; maxValue = int.MaxValue;
                    }
                    else
                    {
                        minValue = byte.MinValue; maxValue = byte.MaxValue;
                    }
                }
                else if (field is EntityUInt32Property)
                {
                    if (propertyStyle)
                    {
                        minValue = uint.MinValue; maxValue = uint.MaxValue;
                    }
                    else
                    {
                        minValue = byte.MinValue; maxValue = byte.MaxValue;
                    }
                }
                else if (field is EntitySettingProperty)
                {
                    minValue = int.MinValue; maxValue = int.MaxValue;
                }
                else if (field is EntityUInt8Property)
                {
                    minValue = byte.MinValue; maxValue = byte.MaxValue;
                }

                if (long.TryParse(inputValue, out long newValue))
                {
                    if (newValue > maxValue)
                    {
                        DarkMessageBox.ShowError($"The value must be less than or equal to\n{maxValue}.", Resources.Title_InputError);
                        dgvPropertyValues.CancelEdit();
                        e.Cancel = true;
                    }
                    else if (newValue < minValue)
                    {
                        DarkMessageBox.ShowError($"The value must be greater than or equal to\n{minValue}.", Resources.Title_InputError);
                        dgvPropertyValues.CancelEdit();
                        e.Cancel = true;
                    }
                }
            }
        }

        private void dgvPropertyValues_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (lbProperties.SelectedItem == null || dgvPropertyValues.CurrentCell == null || dgvPropertyMetaValues.CurrentCell == null ||
                e.RowIndex < 0 || e.ColumnIndex < 0 || valueDirty) return;
            dynamic field = selectedField;
            if (field == null) return;

            int rowIndex = dgvPropertyMetaValues.CurrentCell.RowIndex;
            object? cellValue = dgvPropertyValues.CurrentCell.Value;

            if (field is EntityVictimProperty)
            {
                field.Rows[rowIndex].Values[e.RowIndex] = new EntityVictim(Convert.ToInt16(cellValue));
            }
            else if (field is EntityInt32Property)
            {
                if (e.ColumnIndex == 0)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] = Convert.ToInt32(cellValue);
                }
                else if (e.ColumnIndex == 1)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] &= 0xFFFFFF00;
                    field.Rows[rowIndex].Values[e.RowIndex] |= (int)((byte)Convert.ToByte(cellValue) << 0);
                }
                else if (e.ColumnIndex == 2)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] &= 0xFFFF00FF;
                    field.Rows[rowIndex].Values[e.RowIndex] |= (int)((byte)Convert.ToByte(cellValue) << 8);
                }
                else if (e.ColumnIndex == 3)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] &= 0xFF00FFFF;
                    field.Rows[rowIndex].Values[e.RowIndex] |= (int)((byte)Convert.ToByte(cellValue) << 16);
                }
                else if (e.ColumnIndex == 4)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] &= 0x00FFFFFF;
                    field.Rows[rowIndex].Values[e.RowIndex] |= (int)((byte)Convert.ToByte(cellValue) << 24);
                }
            }
            else if (field is EntityUInt32Property)
            {
                if (e.ColumnIndex == 0)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] = Convert.ToUInt32(cellValue);
                }
                else if (e.ColumnIndex == 1)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] &= 0xFFFFFF00;
                    field.Rows[rowIndex].Values[e.RowIndex] |= (uint)((byte)Convert.ToByte(cellValue) << 0);
                }
                else if (e.ColumnIndex == 2)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] &= 0xFFFF00FF;
                    field.Rows[rowIndex].Values[e.RowIndex] |= (uint)((byte)Convert.ToByte(cellValue) << 8);
                }
                else if (e.ColumnIndex == 3)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] &= 0xFF00FFFF;
                    field.Rows[rowIndex].Values[e.RowIndex] |= (uint)((byte)Convert.ToByte(cellValue) << 16);
                }
                else if (e.ColumnIndex == 4)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] &= 0x00FFFFFF;
                    field.Rows[rowIndex].Values[e.RowIndex] |= (uint)((byte)Convert.ToByte(cellValue) << 24);
                }
            }
            else if (field is EntitySettingProperty)
            {
                field.Rows[rowIndex].Values[e.RowIndex] = new EntitySetting(Convert.ToInt32(cellValue));
            }
            else if (field is EntityUInt8Property)
            {
                field.Rows[rowIndex].Values[e.RowIndex] = Convert.ToByte(cellValue);
            }
            UpdatePropertyHeaderAndRaw();
        }

        private void dgvPropertyValues_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (propertyShowAsHex)
            {
                if (e.Value is uint uintValue)
                {
                    if (propertyStyle)
                        e.Value = uintValue.ToString("X");
                    else
                        e.Value = uintValue.ToString("X2");
                    e.FormattingApplied = true;
                }
                else if (e.Value is int intValue)
                {
                    if (propertyStyle || selectedField is EntitySettingProperty)
                        e.Value = intValue.ToString("X");
                    else
                        e.Value = intValue.ToString("X2");
                    e.FormattingApplied = true;
                }
                else if (e.Value is short shortValue)
                {
                    e.Value = shortValue.ToString("X");
                    e.FormattingApplied = true;
                }
                else if (e.Value is byte byteValue)
                {
                    e.Value = byteValue.ToString("X2");
                    e.FormattingApplied = true;
                }
            }
        }

        private void dgvPropertyValues_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (propertyShowAsHex)
            {
                if (e.Value is string inputValue)
                {
                    try
                    {
                        if (selectedField is EntityVictimProperty)
                        {
                            int parsedValue = Convert.ToInt32(inputValue, 16);

                            if (parsedValue > short.MaxValue)
                            {
                                e.Value = (short)(parsedValue - 0x10000);
                            }
                            else
                            {
                                e.Value = (short)parsedValue;
                            }
                        }
                        else if (selectedField is EntityUInt32Property)
                        {
                            ulong parsedValue = Convert.ToUInt64(inputValue, 16);

                            if (parsedValue > uint.MaxValue)
                            {
                                DarkMessageBox.ShowWarning("The entered value exceeds the range of a 32-bit unsigned integer.", Resources.Title_InputError);
                                e.Value = uint.MaxValue;
                            }
                            else
                            {
                                e.Value = (uint)parsedValue;
                            }
                        }
                        else
                        {
                            e.Value = Convert.ToInt32(inputValue, 16);
                            e.ParsingApplied = true;
                        }

                        e.ParsingApplied = true;
                    }
                    catch
                    {
                        DarkMessageBox.ShowError("Invalid hex value. Please enter a valid 16-bit hex value.", Resources.Title_InputError);
                        e.ParsingApplied = false;
                    }
                }
            }
            else
            {
                if (e.Value == null || string.IsNullOrWhiteSpace(e.Value.ToString())) return;

                // Strip any leading zeros
                string inputValue = e.Value.ToString();
                if (long.TryParse(inputValue, out long parsedValue))
                {
                    e.Value = parsedValue.ToString();
                    e.ParsingApplied = true;
                }
            }
        }

        private void lbProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePropertyControls();
        }

        private void chkPropertyStyle_CheckedChanged(object sender, EventArgs e)
        {
            propertyStyle = chkPropertyStyle.Checked;
            UpdatePropertyControls();
        }

        private void UpdatePropertyControls()
        {
            valueDirty = true;
            LoadFieldFromSelectedItem();
            UpdatePropertyHeaderAndRaw();
            UpdatePropertyMetaValues();
            valueDirty = false;
        }

        private void ClearPropertyControls()
        {
            cmdRemoveProperty.Enabled =
            cmdCopyProperty.Enabled = false;
            lblUnsupportedProperty.Visible = false;
            lvPropertyHeader.Items.Clear();
            lbPropertyRaw.Items.Clear();
            dgvPropertyMetaValues.Rows.Clear();
            dgvPropertyValues.Rows.Clear();
            dgvPropertyMetaValues.Visible =
            dgvPropertyValues.Visible = false;
        }

        private void chkPropertyMetaValue_Click(object sender, EventArgs e)
        {
            if (lbProperties.SelectedItem == null || dgvPropertyMetaValues.CurrentCell == null || !(dgvPropertyMetaValues.Rows.Count > 0) || metavalueDirty) return;

            if (dgvPropertyMetaValues.Rows.Count > 1)
            {
                chkPropertyMetaValue.Checked = true;
                DarkMessageBox.ShowError("The MetaValues flag cannot be toggled while other meta values exist.", Resources.Title_Error);
                return;
            }

            dynamic field = selectedField;
            if (field == null) return;

            if (dgvPropertyMetaValues.CurrentCell.Value != NullMeta)
            {
                field.Rows[0].MetaValue = null;
                dgvPropertyMetaValues.CurrentCell.Value = NullMeta;
                chkPropertyMetaValue.Checked = false;
            }
            else
            {
                field.Rows[0].MetaValue = 0;
                dgvPropertyMetaValues.CurrentCell.Value = 0;
                chkPropertyMetaValue.Checked = true;
            }
            UpdatePropertyHeaderAndRaw();
        }

        private void txtProperty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar >= 'A' && e.KeyChar <= 'F') || (e.KeyChar >= 'a' && e.KeyChar <= 'f')))
            {
                e.Handled = true;
            }
        }

        private void txtProperty_TextChanged(object sender, EventArgs e)
        {
            string hexValue = txtProperty.Text;
            string validHexValue = "";

            foreach (char c in hexValue)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f'))
                {
                    validHexValue += c;
                }
            }

            if (validHexValue != hexValue)
            {
                txtProperty.Text = validHexValue;
                txtProperty.SelectionStart = txtProperty.Text.Length;
            }
        }

        private void cmdAppendProperty_Click(object sender, EventArgs e)
        {
            short id;
            try
            {
                id = Convert.ToInt16(txtProperty.Text.ToString(), 16);
            }
            catch
            {
                DarkMessageBox.ShowError("Invalid field value.", Resources.Title_Error);
                return;
            }

            if (entity.KnownProperties.Keys.Contains(id))
            {
                DarkMessageBox.ShowError("The field already exists.", Resources.Title_Error);
                return;
            }

            dynamic field;
            try
            {
                field = AddField(id);
            }
            catch (ArgumentException ex)
            {
                DarkMessageBox.ShowError(ex.Message, Resources.Title_Error);
                return;
            }

            listKnownFields.Add(id.ToString("X"));
            listAllKnownFields.Add(id.ToString("X"));

            entity.KnownProperties.Add(id, field);
            Console.WriteLine($"Added field: {id:X}");

            cmdRemoveProperty.Enabled =
            cmdCopyProperty.Enabled = true;
            UpdatePropertyControls();
        }

        private void cmdRemoveProperty_Click(object sender, EventArgs e)
        {
            if (lbProperties.SelectedIndex < 0) return;

            var selectedItemsCopy = lbProperties.SelectedItems.Cast<object>().ToList();
            foreach (var selectedItem in selectedItemsCopy)
            {
                string str = selectedItem.ToString();
                short id = Convert.ToInt16(str, 16);
                NullifyField(id);
                entity.KnownProperties.Remove(id);
                listKnownFields.Remove(str);
                listAllKnownFields.Remove(str);
                Console.WriteLine($"Removed field: {id:X}");
            }

            UpdatePropertyControls();
            if (!(lbProperties.Items.Count > 0))
            {
                ClearPropertyControls();
            }
        }

        private void chkPropertyShowAsHex_Click(object sender, EventArgs e)
        {
            propertyShowAsHex = chkPropertyShowAsHex.Checked;
            UpdatePropertyValues();
        }

        private void cmdCopyProperty_Click(object sender, EventArgs e)
        {
            if (lbProperties.SelectedIndex < 0) return;

            List<FieldData> fieldsToSave = new List<FieldData>();

            foreach (var selectedItem in lbProperties.SelectedItems)
            {
                short id = Convert.ToInt16(selectedItem.ToString(), 16);
                object field = GetField(id);

                if (field == null)
                {
                    DarkMessageBox.ShowError($"Unsupported field for ID {id:X}.", Resources.Title_Error);
                    continue;
                }

                fieldsToSave.Add(new FieldData
                {
                    Id = id,
                    FieldType = field.ToString(),
                    Field = field,
                    Comment = string.Empty
                });
            }

            if (fieldsToSave.Count > 0)
            {
                AddSavedItem(GetUniqueName("Item"), fieldsToSave);
                LoadItemsFromFile();
            }
            else
            {
                DarkMessageBox.ShowError("No valid fields were selected.", Resources.Title_Error);
            }
        }

        private string GetUniqueName(string baseName)
        {
            string uniqueName = baseName;
            int i = 1;

            while (lbSavedProperties.Items.Cast<object>().Any(item => string.Equals(item.ToString(), uniqueName, StringComparison.Ordinal)))
            {
                uniqueName = $"{baseName} ({i})";
                i++;
            }

            return uniqueName;

        }

        private void CopyFieldsFromList(List<FieldData> loadedFieldDataList)
        {
            foreach (var loadedFieldData in loadedFieldDataList)
            {
                short id = loadedFieldData.Id;
                string fieldType = loadedFieldData.FieldType;
                dynamic loadedField = loadedFieldData.Field;

                dynamic field = null!;
                if (fieldType.Contains("CrashEdit.Crash.EntityVictimProperty"))
                {
                    field = EntityPropertyConverter.ConvertJsonToEntityVictimProperty(loadedField);
                }
                else if (fieldType.Contains("CrashEdit.Crash.EntityInt32Property"))
                {
                    field = EntityPropertyConverter.ConvertJsonToEntityInt32Property(loadedField);
                }
                else if (fieldType.Contains("CrashEdit.Crash.EntityUInt32Property"))
                {
                    field = EntityPropertyConverter.ConvertJsonToEntityUInt32Property(loadedField);
                }
                else if (fieldType.Contains("CrashEdit.Crash.EntitySettingProperty"))
                {
                    field = EntityPropertyConverter.ConvertJsonToEntitySettingProperty(loadedField);
                }
                else if (fieldType.Contains("CrashEdit.Crash.EntityUInt8Property"))
                {
                    field = EntityPropertyConverter.ConvertJsonToEntityUInt8Property(loadedField);
                }

                if (entity.KnownProperties.Keys.Contains(id))
                {
                    entity.KnownProperties.Remove(id);
                    NullifyField(id);

                    entity.KnownProperties.Add(id, field);
                    ReplaceField(id, field);
                    Console.WriteLine($"Replaced field: {id:X}");
                }
                else
                {
                    listKnownFields.Add(id.ToString("X"));
                    listAllKnownFields.Add(id.ToString("X"));

                    entity.KnownProperties.Add(id, field);
                    ReplaceField(id, field);
                    Console.WriteLine($"Added field: {id:X}");

                    cmdRemoveProperty.Enabled =
                    cmdCopyProperty.Enabled = true;
                }
                UpdatePropertyControls();
            }
        }

        private void lbProperties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (lbProperties.SelectedItems.Count > 0)
                {
                    cmdCopyProperty.PerformClick();
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                cmdRemoveProperty.PerformClick();
                e.Handled = true;
            }
            //else if (e.Control && e.KeyCode == Keys.X)
            //{
            //    if (lbProperties.SelectedItems.Count > 0)
            //    {
            //        cmdCopyProperty.PerformClick();
            //    }
            //    e.Handled = true;
            //}
        }

        private void lbSavedProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSavedProperties.SelectedIndex < 0)
            {
                dgvSavePropertyValues.Visible = false;
                pnControlsSaved.Enabled = false;
                return;
            }
            dgvSavePropertyValues.Visible = true;
            pnControlsSaved.Enabled = true;

            string selectedItemName = lbSavedProperties.SelectedItem.ToString();
            var selectedItem = savedItems.FirstOrDefault(item => string.Equals(item.Name, selectedItemName, StringComparison.Ordinal));
            if (selectedItem != null)
            {
                dgvSavePropertyValues.Rows.Clear();
                foreach (var field in selectedItem.Fields)
                {
                    dgvSavePropertyValues.Rows.Add(field.Id.ToString("X"), field.Comment);
                }
            }
        }

        private void SaveItemsToFile()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(savedItems, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, jsonString);
                Console.WriteLine("Properties list saved successfully.");
            }
            catch (Exception ex)
            {
                DarkMessageBox.ShowError($"Error saving properties list: {ex.Message}", Resources.Title_Error);
            }
        }

        private void LoadItemsFromFile()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string jsonString = File.ReadAllText(FilePath);
                    savedItems = JsonSerializer.Deserialize<List<ListItem>>(jsonString) ?? new List<ListItem>();

                    lbSavedProperties.Items.Clear();
                    foreach (var item in savedItems)
                    {
                        lbSavedProperties.Items.Add(item.Name);
                    }

                    Console.WriteLine("Properties list loaded successfully.");
                }
            }
            catch (Exception ex)
            {
                DarkMessageBox.ShowError($"Error loading properties list: {ex.Message}", Resources.Title_Error);
            }
        }

        private void cmdCopyFromSaved_Click(object sender, EventArgs e)
        {
            if (lbSavedProperties.SelectedIndex < 0) return;

            string selectedItemName = lbSavedProperties.SelectedItem.ToString();
            var selectedItem = savedItems.FirstOrDefault(item => string.Equals(item.Name, selectedItemName, StringComparison.Ordinal));
            if (selectedItem != null)
            {
                CopyFieldsFromList(selectedItem.Fields);
            }
        }

        private void cmdRenameSavedList_Click(object sender, EventArgs e)
        {
            if (lbSavedProperties.SelectedIndex < 0) return;

            string selectedItemName = lbSavedProperties.SelectedItem.ToString();
            var selectedItem = savedItems.FirstOrDefault(item => string.Equals(item.Name, selectedItemName, StringComparison.Ordinal));
            if (selectedItem != null)
            {
                using (InputWindow inputWindows = new InputWindow("Rename", "Modify", "Enter new name:", selectedItemName, -1))
                {
                    if (inputWindows.ShowDialog() == DialogResult.OK)
                    {
                        string newName = inputWindows.Input;
                        if (!string.IsNullOrWhiteSpace(newName) &&
                            !string.Equals(newName, selectedItemName, StringComparison.Ordinal))
                        {
                            string uniqueName = GetUniqueName(newName);

                            selectedItem.Name = uniqueName;
                            SaveItemsToFile();
                            int selectedIndex = lbSavedProperties.SelectedIndex;
                            lbSavedProperties.Items[selectedIndex] = string.Empty;
                            lbSavedProperties.Items[selectedIndex] = uniqueName;
                        }
                    }
                }
               
            }
        }

        private void cmdRemoveSavedList_Click(object sender, EventArgs e)
        {
            if (lbSavedProperties.SelectedIndex < 0) return;

            if (DarkMessageBox.ShowWarning("Are you sure you want to remove the selected items?", Resources.Delete_ConfirmationPrompt, DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                foreach (var selectedItem in lbSavedProperties.SelectedItems.Cast<string>().ToList())
                {
                    savedItems.RemoveAll(item => string.Equals(item.Name, selectedItem, StringComparison.Ordinal));
                    lbSavedProperties.Items.Remove(selectedItem);
                }
                SaveItemsToFile();
            }
        }

        private void lbSavedProperties_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbSavedProperties.SelectedIndex < 0) return;

            if (e.KeyCode == Keys.F2)
            {
                cmdRenameSavedList.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                cmdRemoveSavedList.PerformClick();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                cmdCopyFromSaved.PerformClick();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                  ReloadSavedProperties();
                e.Handled = true;
            }
        }

        private void dgvSavePropertyValues_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DarkMessageBox.ShowError("This cell cannot be edited.", Resources.Title_Error);
                e.Cancel = true;
            }
        }

        private void dgvSavePropertyValues_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (lbSavedProperties.SelectedIndex < 0) return;

            string selectedItemName = lbSavedProperties.SelectedItem.ToString();
            var selectedItem = savedItems.FirstOrDefault(item => string.Equals(item.Name, selectedItemName, StringComparison.Ordinal));
            if (selectedItem != null)
            {
                selectedItem.Fields[e.RowIndex].Comment = dgvSavePropertyValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? string.Empty;
                SaveItemsToFile();
            }
        }

        private void ReloadSavedProperties()
        {
            string? selectedItem = lbSavedProperties.SelectedItem?.ToString();
            LoadItemsFromFile();
            if (selectedItem != null && lbSavedProperties.Items.Contains(selectedItem))
            {
                lbSavedProperties.SelectedItem = selectedItem;
            }
            else
            {
                dgvSavePropertyValues.Visible = false;
                pnControlsSaved.Enabled = false;
            }
        }

        private void rbtReload_Click(object sender, EventArgs e)
        {
            ReloadSavedProperties();
            rbtReload.Checked = false;
        }
    }

    public sealed class EntityPropertyConverter
    {
        public static EntityVictimProperty ConvertJsonToEntityVictimProperty(JsonElement jsonElement)
        {
            var entityVictimProperty = new EntityVictimProperty();
            if (jsonElement.TryGetProperty("Rows", out JsonElement rowsElement))
            {
                foreach (var rowElement in rowsElement.EnumerateArray())
                {
                    short? metaValue = null;
                    if (rowElement.TryGetProperty("MetaValue", out JsonElement metaValueElement) &&
                        metaValueElement.ValueKind != JsonValueKind.Null)
                    {
                        metaValue = metaValueElement.GetInt16();
                    }
                    var valuesElement = rowElement.GetProperty("Values");
                    var row = new EntityPropertyRow<EntityVictim>
                    {
                        MetaValue = metaValue
                    };

                    foreach (var valueElement in valuesElement.EnumerateArray())
                    {
                        if (valueElement.TryGetProperty("VictimID", out JsonElement victimIdElement))
                        {
                            var entityVictim = new EntityVictim(victimIdElement.GetInt16());
                            row.Values.Add(entityVictim);
                        }
                    }
                    entityVictimProperty.Rows.Add(row);
                }
            }
            else
            {
                Console.WriteLine("Rows property not found in the JSON.");
            }
            return entityVictimProperty;
        }

        public static EntityInt32Property ConvertJsonToEntityInt32Property(JsonElement jsonElement)
        {
            var entityInt32Property = new EntityInt32Property();
            if (jsonElement.TryGetProperty("Rows", out JsonElement rowsElement))
            {
                foreach (var rowElement in rowsElement.EnumerateArray())
                {
                    short? metaValue = null;
                    if (rowElement.TryGetProperty("MetaValue", out JsonElement metaValueElement) &&
                        metaValueElement.ValueKind != JsonValueKind.Null)
                    {
                        metaValue = metaValueElement.GetInt16();
                    }
                    var valuesElement = rowElement.GetProperty("Values");
                    var row = new EntityPropertyRow<int>
                    {
                        MetaValue = metaValue
                    };

                    foreach (var valueElement in valuesElement.EnumerateArray())
                    {
                        int value = valueElement.GetInt32();
                        row.Values.Add(value);
                    }
                    entityInt32Property.Rows.Add(row);
                }
            }
            else
            {
                Console.WriteLine("Rows property not found in the JSON.");
            }
            return entityInt32Property;
        }

        public static EntityUInt32Property ConvertJsonToEntityUInt32Property(JsonElement jsonElement)
        {
            var entityUInt32Property = new EntityUInt32Property();
            if (jsonElement.TryGetProperty("Rows", out JsonElement rowsElement))
            {
                foreach (var rowElement in rowsElement.EnumerateArray())
                {
                    short? metaValue = null;
                    if (rowElement.TryGetProperty("MetaValue", out JsonElement metaValueElement) &&
                        metaValueElement.ValueKind != JsonValueKind.Null)
                    {
                        metaValue = metaValueElement.GetInt16();
                    }
                    var valuesElement = rowElement.GetProperty("Values");
                    var row = new EntityPropertyRow<uint>
                    {
                        MetaValue = metaValue
                    };

                    foreach (var valueElement in valuesElement.EnumerateArray())
                    {
                        uint value = valueElement.GetUInt32();
                        row.Values.Add(value);
                    }
                    entityUInt32Property.Rows.Add(row);
                }
            }
            else
            {
                Console.WriteLine("Rows property not found in the JSON.");
            }
            return entityUInt32Property;
        }

        public static EntitySettingProperty ConvertJsonToEntitySettingProperty(JsonElement jsonElement)
        {
            var entitySettingProperty = new EntitySettingProperty();
            if (jsonElement.TryGetProperty("Rows", out JsonElement rowsElement))
            {
                foreach (var rowElement in rowsElement.EnumerateArray())
                {
                    short? metaValue = null;
                    if (rowElement.TryGetProperty("MetaValue", out JsonElement metaValueElement) &&
                        metaValueElement.ValueKind != JsonValueKind.Null)
                    {
                        metaValue = metaValueElement.GetInt16();
                    }
                    var valuesElement = rowElement.GetProperty("Values");
                    var row = new EntityPropertyRow<EntitySetting>
                    {
                        MetaValue = metaValue
                    };

                    foreach (var valueElement in valuesElement.EnumerateArray())
                    {
                        if (valueElement.TryGetProperty("Value", out JsonElement _valueElement))
                        {
                            var entitySetting = new EntitySetting(_valueElement.GetInt32());
                            row.Values.Add(entitySetting);
                        }
                    }

                    entitySettingProperty.Rows.Add(row);
                }
            }
            else
            {
                Console.WriteLine("Rows property not found in the JSON.");
            }
            return entitySettingProperty;
        }

        public static EntityUInt8Property ConvertJsonToEntityUInt8Property(JsonElement jsonElement)
        {
            var entityUInt8Property = new EntityUInt8Property();
            if (jsonElement.TryGetProperty("Rows", out JsonElement rowsElement))
            {
                foreach (var rowElement in rowsElement.EnumerateArray())
                {
                    short? metaValue = null;
                    if (rowElement.TryGetProperty("MetaValue", out JsonElement metaValueElement) &&
                        metaValueElement.ValueKind != JsonValueKind.Null)
                    {
                        metaValue = metaValueElement.GetInt16();
                    }
                    var valuesElement = rowElement.GetProperty("Values");
                    var row = new EntityPropertyRow<byte>
                    {
                        MetaValue = metaValue
                    };

                    foreach (var valueElement in valuesElement.EnumerateArray())
                    {
                        byte value = valueElement.GetByte();
                        row.Values.Add(value);
                    }
                    entityUInt8Property.Rows.Add(row);
                }
            }
            else
            {
                Console.WriteLine("Rows property not found in the JSON.");
            }
            return entityUInt8Property;
        }
    }
}
