using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public partial class ConvertAnimationsForm : DarkForm
    {
        private List<UnprocessedEntry> loadedEntries = new List<UnprocessedEntry>();

        private const int ColFileName = 0;
        private const int ColFilePath = 1;
        private const int ColType = 2;
        private const int ColAnimEID = 3;
        private const int ColModelEID = 4;

        private const int ModeC3toC2 = 0;
        private const int ModeC2toC2 = 1;
        private const int ModeC2toC3 = 2;
        private const int ModeC3toC3 = 3;

        private const int Unknown2Index = 8;
        private const int Unknown2Length = 8;
        private const int ModelEIDIndex = 16;
        private const int ModelEIDLength = 4;

        public ConvertAnimationsForm()
        {
            InitializeComponent();
            DoubleBufferedDataGridView.Initialize(dgvAnim);

            dgvAnim.Columns.Add("Name", "File Name");
            dgvAnim.Columns.Add("Path", "File Path");
            dgvAnim.Columns.Add("Type", "Type");
            dgvAnim.Columns.Add("AnimEID", "Anim EID");
            dgvAnim.Columns.Add("ModelEID", "Model EID");
            dgvAnim.Columns[ColFilePath].Visible = false;

            cmbMode.Items.AddRange(new object[]
            {
                "Crash3 → Crash2",
                "Crash2 → Crash2",
                "Crash2 → Crash3",
                "Crash3 → Crash3"
            });
            cmbMode.SelectedIndex = ModeC3toC2;
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "All Files (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string filePath in ofd.FileNames)
                    {
                        try
                        {
                            FileInfo fileInfo = new FileInfo(filePath);

                            byte[] fileBytes = File.ReadAllBytes(filePath);
                
                            UnprocessedEntry entry = Entry.Load(fileBytes);
                            loadedEntries.Add(entry);

                            int vertexcount = BitConv.FromInt32(entry.Items[0], 8);
                            string type = vertexcount == 0 ? "Crash3" : "Crash2";

                            string animEID = Entry.EIDToEName(BitConv.FromInt32(fileBytes, 4));
                            string modelEID = string.Empty;
                            if (chkSetModelEID.Checked)
                            {
                                modelEID = animEID.Substring(0, animEID.Length - 1) + "G";
                            }
                            dgvAnim.Rows.Add(Path.GetFileNameWithoutExtension(fileInfo.Name), fileInfo.FullName, type, animEID, modelEID);
                        }
                        catch (Exception ex)
                        {
                            DarkMessageBox.ShowError($"Error reading file {filePath}: {ex.Message}", Resources.Title_Error);
                        }
                    }
                }
            }
        }

        private void cmdProcess_Click(object sender, EventArgs e)
        {
            int successCount = 0;
            int errorCount = 0;
            foreach (DataGridViewRow row in dgvAnim.Rows)
            {
                string? fileName = row.Cells[ColFileName].Value?.ToString();
                string? filePath = row.Cells[ColFilePath].Value?.ToString();
                string? type = row.Cells[ColType].Value?.ToString();
                string? animEID = row.Cells[ColAnimEID].Value?.ToString();
                string? modelEID = row.Cells[ColModelEID].Value?.ToString();

                Console.WriteLine($"Processing entry: {fileName}");

                if (cmbMode.SelectedIndex == ModeC3toC2 || cmbMode.SelectedIndex == ModeC3toC3)
                {
                    if (type != "Crash3")
                    {
                        Console.WriteLine("    Error: Wrong entry type.");
                        ++errorCount;
                        continue;
                    }
                }
                else
                {
                    if (type != "Crash2")
                    {
                        Console.WriteLine("    Error: Wrong entry type.");
                        ++errorCount;
                        continue;
                    }
                }

                if (cmbMode.SelectedIndex == ModeC3toC2 || cmbMode.SelectedIndex == ModeC2toC2)
                {
                    string checkedEID = Entry.CheckEIDErrors(modelEID, true);
                    if (checkedEID != string.Empty)
                    {
                        Console.WriteLine("    Error: Invalid model EID.");
                        ++errorCount;
                        continue;
                    }
                }

                string mode = string.Empty;
                UnprocessedEntry entry = loadedEntries[row.Index];
                for (int i = 0; i < entry.Items.Count; i++)
                {
                    byte[] data = entry.Items[i];

                    if (cmbMode.SelectedIndex == ModeC3toC2)
                    {
                        // Remove unknown2
                        byte[] newArray = new byte[data.Length - Unknown2Length];
                        Array.Copy(data, 0, newArray, 0, Unknown2Index);
                        Array.Copy(data, Unknown2Index + Unknown2Length, newArray, Unknown2Index, data.Length - (Unknown2Index + Unknown2Length));

                        // Insert byte for ModelEID
                        byte[] newArray2 = new byte[newArray.Length + ModelEIDLength];
                        Array.Copy(newArray, 0, newArray2, 0, ModelEIDIndex);
                        Array.Copy(newArray, ModelEIDIndex, newArray2, ModelEIDIndex + ModelEIDLength, newArray.Length - ModelEIDIndex);

                        // Set ModelEID
                        int eid = Entry.ENameToEID(modelEID);
                        BitConv.ToInt32(newArray2, ModelEIDIndex, eid);

                        // Recalculate velues
                        short xoffIndex = 0;
                        short yoffIndex = 2;
                        short zoffIndex = 4;
                        int headerSizeIndex = 20;
                        short xoff = (short)(BitConv.FromInt16(newArray2, xoffIndex) >> 3);
                        BitConv.ToInt16(newArray2, xoffIndex, xoff);
                        short yoff = (short)(BitConv.FromInt16(newArray2, yoffIndex) >> 3);
                        BitConv.ToInt16(newArray2, yoffIndex, yoff);
                        short zoff = (short)(BitConv.FromInt16(newArray2, zoffIndex) >> 3);
                        BitConv.ToInt16(newArray2, zoffIndex, zoff);
                        int headerSize = BitConv.FromInt32(newArray2, headerSizeIndex) - 4;
                        BitConv.ToInt32(newArray2, headerSizeIndex, headerSize);

                        entry.Items[i] = newArray2;
                        mode = "C3toC2";
                    }
                    else if (cmbMode.SelectedIndex == ModeC2toC3)
                    {
                        // Remove ModelEID
                        byte[] newArray = new byte[data.Length - ModelEIDLength];
                        Array.Copy(data, 0, newArray, 0, ModelEIDIndex);
                        Array.Copy(data, ModelEIDIndex + ModelEIDLength, newArray, ModelEIDIndex, data.Length - (ModelEIDIndex + ModelEIDLength));

                        // Insert byte for unknown2
                        byte[] newArray2 = new byte[newArray.Length + Unknown2Length];
                        Array.Copy(newArray, 0, newArray2, 0, Unknown2Index);
                        Array.Copy(newArray, Unknown2Index, newArray2, Unknown2Index + Unknown2Length, newArray.Length - Unknown2Index);

                        // Recalculate velues
                        short xoffIndex = 0;
                        short yoffIndex = 2;
                        short zoffIndex = 4;
                        int headerSizeIndex = 24;
                        short xoff = (short)(BitConv.FromInt16(newArray2, xoffIndex) << 3);
                        BitConv.ToInt16(newArray2, xoffIndex, xoff);
                        short yoff = (short)(BitConv.FromInt16(newArray2, yoffIndex) << 3);
                        BitConv.ToInt16(newArray2, yoffIndex, yoff);
                        short zoff = (short)(BitConv.FromInt16(newArray2, zoffIndex) << 3);
                        BitConv.ToInt16(newArray2, zoffIndex, zoff);
                        int headerSize = BitConv.FromInt32(newArray2, headerSizeIndex) + 4;
                        BitConv.ToInt32(newArray2, headerSizeIndex, headerSize);

                        entry.Items[i] = newArray2;
                        mode = "C2toC3";
                    }
                    else if (cmbMode.SelectedIndex == ModeC2toC2)
                    {
                        // Set ModelEID
                        int eid = Entry.ENameToEID(modelEID);
                        BitConv.ToInt32(data, ModelEIDIndex, eid);

                        entry.Items[i] = data;
                        mode = "C2toC2";
                    }
                    else if (cmbMode.SelectedIndex == ModeC3toC3)
                    {
                        // Do nothing
                        mode = "C3toC3";
                    }
                }

                byte[] fileBytes = entry.Save();

                // Set AnimEID
                BitConv.ToInt32(fileBytes, 4, Entry.ENameToEID(animEID));

                string saveDirectory = Path.GetDirectoryName(filePath);
                string savePath = Path.Combine(saveDirectory, $"{fileName}_{mode}.nsentry");
                File.WriteAllBytes(savePath, fileBytes);
                Console.WriteLine($"    Saved entry: {savePath}");
                ++successCount;
            }

            if (errorCount == 0)
                DarkMessageBox.ShowInformation($"Processed {successCount} entries.", Text);
            else
                DarkMessageBox.ShowInformation($"Processed {successCount} entries with {errorCount} errors.", Text);
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            if (DarkMessageBox.ShowWarning("Are you sure you want to clear the list?", Text, DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                dgvAnim.Rows.Clear();
                dgvAnim.ScrollBars = ScrollBars.None;
                cmdClear.Enabled =
                cmdProcess.Enabled = false;
            }
        }

        private void chkShowFilePath_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowFilePath.Checked)
            {
                dgvAnim.Columns[ColFilePath].Visible = true;
            }
            else
            {
                dgvAnim.Columns[ColFilePath].Visible = false;
            }
        }

        private void dgvAnim_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            cmdClear.Enabled =
            cmdProcess.Enabled = true;
            dgvAnim.ScrollBars = ScrollBars.Both;
            if (dgvAnim.Rows.Count <= 16)
            {
                int scrollBarHeight = SystemInformation.HorizontalScrollBarHeight;
                dgvAnim.Height = dgvAnim.ColumnHeadersHeight + (dgvAnim.Rows.Count * dgvAnim.Rows[0].Height) + scrollBarHeight;
            }
        }

        private void dgvAnim_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == ColFileName || e.ColumnIndex == ColFilePath || e.ColumnIndex == ColType)
            {
                e.Cancel = true;
            }
        }

        private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMode.SelectedIndex <= 0) return;

            if (cmbMode.SelectedIndex == ModeC3toC2 || cmbMode.SelectedIndex == ModeC2toC2)
            {
                chkSetModelEID.Enabled = true;
                dgvAnim.Columns[ColModelEID].Visible = true;
            }
            else
            {
                chkSetModelEID.Enabled = false;
                dgvAnim.Columns[ColModelEID].Visible = false;
            }
        }
    }
}
