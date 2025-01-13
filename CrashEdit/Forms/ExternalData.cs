using System.Text.Json;
using AltUI.Forms;
using CrashEdit.CE.Properties;

namespace CrashEdit.CE
{
    public partial class ExternalData : DarkForm
    {
        private Dictionary<string, Dictionary<string, List<KeyValuePair<int, int>>>> groups;

        private string groupIndex;

        private readonly string externalFileName = "CrashEdit.exe.externaldata.json";
        private readonly string defaultName = "None";

        private readonly string titleError = "Error";
        private readonly string titleSuccess = "Success";

        public List<KeyValuePair<int, int>> Group
        {
            get
            {
                if (cmbGroups.SelectedItem != null &&
                    groups.TryGetValue(groupIndex, out var groupDict) &&
                    groupDict.TryGetValue(cmbGroups.SelectedItem.ToString(), out var list))
                {
                    return list;
                }

                return new List<KeyValuePair<int, int>>();
            }
        }
        public bool outputResult;

        public ExternalData(string name)
        {
            InitializeComponent();
            this.Text = name;
            groupIndex = name;

            EnableDoubleBuffering(dgvGroups);
            SetDarkTheme(dgvGroups);
            InitializeDataGridView(dgvGroups);

            LoadExternalData();
            UpdateComboBox();
        }

        private void InitializeDataGridView(DataGridView dgv)
        {
            dgv.Columns.Clear();
            var columns = new[]
            {
                new { Header = "Type", MaxLength = 4 },
                new { Header = "Subtype", MaxLength = 4 }
            };

            foreach (var col in columns)
            {
                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = col.Header,
                    MaxInputLength = col.MaxLength,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    Width = 60,
                });
            }
        }

        private void UpdateComboBox()
        {
            cmbGroups.DataSource = groups[groupIndex].Keys.ToList();
        }

        private void LoadExternalData()
        {
            try
            {
                groups = LoadGroups();
            }
            catch (Exception ex)
            {
                DarkMessageBox.ShowError($"Failed to load groups: {ex.Message}", titleError);
                groups = CreateDefaultGroups();
            }
        }

        private Dictionary<string, Dictionary<string, List<KeyValuePair<int, int>>>> LoadGroups()
        {
            if (File.Exists(externalFileName))
            {
                var jsonString = File.ReadAllText(externalFileName);
                return JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, List<KeyValuePair<int, int>>>>>(jsonString)
                       ?? CreateDefaultGroups();
            }
            return CreateDefaultGroups();
        }

        private Dictionary<string, Dictionary<string, List<KeyValuePair<int, int>>>> CreateDefaultGroups()
        {
            return new Dictionary<string, Dictionary<string, List<KeyValuePair<int, int>>>>
            {
                { "Fix Nitro Detonators", new Dictionary<string, List<KeyValuePair<int, int>>>
                    { { defaultName, new List<KeyValuePair<int, int>>() } } },
                { "Fix Box Count", new Dictionary<string, List<KeyValuePair<int, int>>>
                    { { defaultName, new List<KeyValuePair<int, int>>() } } }
            };
        }

        private void SaveGroups()
        {
            try
            {
                if (cmbGroups.SelectedItem != null &&
                    groups.TryGetValue(groupIndex, out var groupDict) &&
                    groupDict.TryGetValue(cmbGroups.SelectedItem.ToString(), out var list))
                {
                    list.Clear();
                    foreach (DataGridViewRow row in dgvGroups.Rows)
                    {
                        if (row.Cells[0].Value != null && row.Cells[1].Value != null &&
                            int.TryParse(row.Cells[0].Value.ToString(), out var type) &&
                            int.TryParse(row.Cells[1].Value.ToString(), out var subtype))
                        {
                            list.Add(new KeyValuePair<int, int>(type, subtype));
                        }
                    }
                }
                File.WriteAllText(externalFileName, JsonSerializer.Serialize(groups, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch (Exception ex)
            {
                DarkMessageBox.ShowError($"Failed to save groups: {ex.Message}", titleError);
            }
        }

        private void cmbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGroups.SelectedItem != null &&
                groups.TryGetValue(groupIndex, out var groupDict) &&
                groupDict.TryGetValue(cmbGroups.SelectedItem.ToString(), out var list))
            {
                dgvGroups.Rows.Clear();
                foreach (var kvp in list)
                {
                    dgvGroups.Rows.Add(kvp.Key, kvp.Value);
                }
            }

            txtGroups.Text = cmbGroups.SelectedItem?.ToString();
            bool isDefault = cmbGroups.SelectedItem?.ToString() == defaultName;

            cmdRename.Enabled = cmdRemove.Enabled = !isDefault;
            dgvGroups.Visible = !isDefault;
        }

        private void cmdAppend_Click(object sender, EventArgs e)
        {
            string newGroupName = txtGroups.Text.Trim();
            if (!string.IsNullOrWhiteSpace(newGroupName) && !groups[groupIndex].ContainsKey(newGroupName))
            {
                groups[groupIndex][newGroupName] = new List<KeyValuePair<int, int>>();
                cmbGroups.DataSource = groups[groupIndex].Keys.ToList();
                cmbGroups.SelectedItem = newGroupName;

                DarkMessageBox.ShowInformation("Group added successfully.", titleSuccess);
                SaveGroups();
            }
            else
            {
                DarkMessageBox.ShowError("Invalid or duplicate group name.", titleError);
            }
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            if (cmbGroups.SelectedItem != null)
            {
                string selectedGroup = cmbGroups.SelectedItem.ToString();
                var result = DarkMessageBox.ShowWarning($"Are you sure you want to delete the group\r\n'{selectedGroup}'?", Resources.Delete_ConfirmationPrompt, DarkDialogButton.YesNo);

                if (result == DialogResult.Yes)
                {
                    dgvGroups.Rows.Clear();
                    groups[groupIndex].Remove(selectedGroup);
                    cmbGroups.DataSource = groups[groupIndex].Keys.ToList();
                    SaveGroups();
                }
            }
            else
            {
                DarkMessageBox.ShowError("Please select a group to remove.", titleError);
            }
        }

        private void cmdRename_Click(object sender, EventArgs e)
        {
            if (cmbGroups.SelectedItem != null)
            {
                string selectedGroup = cmbGroups.SelectedItem.ToString();
                string newGroupName = txtGroups.Text.Trim();

                if (!string.IsNullOrWhiteSpace(newGroupName) && !groups[groupIndex].ContainsKey(newGroupName))
                {
                    var groupData = groups[groupIndex][selectedGroup];
                    groups[groupIndex].Remove(selectedGroup);
                    groups[groupIndex][newGroupName] = groupData;

                    cmbGroups.DataSource = groups[groupIndex].Keys.ToList();
                    cmbGroups.SelectedItem = newGroupName;
                    SaveGroups();
                }
                else
                {
                    DarkMessageBox.ShowError("Invalid or duplicate group name.", titleError);
                }
            }
            else
            {
                DarkMessageBox.ShowError("Please select a group to rename.", titleError);
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void chkShowEditor_CheckedChanged(object sender, EventArgs e)
        {
            fraEditor.Visible = chkShowEditor.Checked;
        }

        private void chkOutputResult_CheckedChanged(object sender, EventArgs e)
        {
            outputResult = chkOutputResult.Checked;
        }

        private void dgvGroups_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (cmbGroups.SelectedItem?.ToString() == defaultName)
            {
                DarkMessageBox.ShowError("This group cannot be edited.", titleError);
                e.Cancel = true;
            }
        }

        private void dgvGroups_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SaveGroups();
        }

        private void dgvGroups_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textbox)
            {
                textbox.KeyPress -= TextBox_KeyPress;
                textbox.KeyPress += TextBox_KeyPress;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }


        private void EnableDoubleBuffering(DataGridView dataGridView)
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, dataGridView, new object[] { true });
        }

        private void SetDarkTheme(DataGridView dataGridView)
        {
            Color clrBackground = Color.FromArgb(40, 40, 40);
            Color clrAltBackground = Color.FromArgb(34, 34, 34);
            Color clrSelectionBackground = Color.FromArgb(70, 70, 70);
            Color clrText = Color.Gainsboro;

            // Background color of the entire grid
            dataGridView.BackgroundColor = Color.FromArgb(31, 31, 32);

            // Color of the grid lines
            dataGridView.GridColor = Color.FromArgb(50, 50, 50);

            // Default style for cells
            dataGridView.DefaultCellStyle.BackColor = clrBackground;
            dataGridView.DefaultCellStyle.ForeColor = clrText;
            dataGridView.DefaultCellStyle.SelectionBackColor = clrSelectionBackground;
            dataGridView.DefaultCellStyle.SelectionForeColor = clrText;

            // Style for column headers
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = clrText;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = clrText;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Style for row headers
            dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView.RowHeadersDefaultCellStyle.ForeColor = clrText;
            dataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dataGridView.RowHeadersDefaultCellStyle.SelectionForeColor = clrText;

            // Background color for odd and even rows
            dataGridView.RowsDefaultCellStyle.BackColor = clrBackground;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = clrAltBackground;

            // Row border style
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Header and gridline styles
            dataGridView.EnableHeadersVisualStyles = false;

            // Additional settings
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }

    }
}
