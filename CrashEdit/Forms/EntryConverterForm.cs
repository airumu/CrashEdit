using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using MetroSet_UI.Animates;
using System;
using System.ComponentModel;
using System.Media;
using System.Text;
using System.Windows.Media.Media3D;

namespace CrashEdit.CE
{
    public partial class EntryConverterForm : DarkForm
    {
        private const int ColFileName = 0;
        private const int ColFilePath = 1;
        private const int ColType = 2;
        private const int ColModelEID = 3;
        private const int ColAnimEID = 4;

        private const int TypeCrash2 = 0;
        private const int TypeCrash3 = 1;

        private const int AnimC3toC2 = 0;
        private const int AnimC2toC2 = 1;

        private const int AnimC2toC3 = 0;
        private const int AnimC3toC3 = 1;

        private const int Unknown2Index = 8;
        private const int Unknown2Length = 8;
        private const int ModelEIDIndex = 16;
        private const int ModelEIDLength = 4;

        public EntryConverterForm()
        {
            InitializeComponent();
            Icon = Embeds.GetIcon("Plugin");
            DoubleBufferedDataGridView.Initialize(dgvModel);
            DoubleBufferedDataGridView.Initialize(dgvAnim);

            dgvModel.Columns.Add("Name", "File Name");
            dgvModel.Columns.Add("Path", "File Path");
            dgvModel.Columns.Add("Type", "Type");
            dgvModel.Columns.Add("ModelEID", "Model EID");
            dgvModel.Columns.Add("AnimEID", "Anim EID");

            dgvAnim.Columns.Add("Name", "File Name");
            dgvAnim.Columns.Add("Path", "File Path");
            dgvAnim.Columns.Add("Type", "Type");
            dgvAnim.Columns.Add("ModelEID", "Model EID");
            dgvAnim.Columns.Add("AnimEID", "Anim EID");

            dgvModel.Columns[ColFilePath].Visible =
            dgvModel.Columns[ColType].Visible =
            dgvModel.Columns[ColAnimEID].Visible =
            dgvAnim.Columns[ColFilePath].Visible =
            dgvAnim.Columns[ColType].Visible = false;

            cmbType.Items.AddRange(
            [
                "Crash 2",
                "Crash 3"
            ]);
            cmbType.SelectedIndex = TypeCrash2;
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            if (cmbType.SelectedIndex < 0) return;

            using OpenFileDialog ofd = new();
            ofd.Multiselect = true;
            ofd.Filter = "All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                dgvAnim.SuspendLayout();
                dgvModel.SuspendLayout();
                dgvAnim.ScrollBars = ScrollBars.None;
                dgvModel.ScrollBars = ScrollBars.None;

                foreach (string filePath in ofd.FileNames)
                {
                    try
                    {
                        FileInfo fileInfo = new(filePath);
                        byte[] fileBytes = File.ReadAllBytes(filePath);
                        UnprocessedEntry unEntry = Entry.Load(fileBytes);

                        string type = "";
                        string modelEID = "";
                        string animEID = "";
                        DataGridViewRow newRow = new();

                        GameVersion version;
                        if (cmbType.SelectedIndex == TypeCrash2)
                            version = cmbMode.SelectedIndex == AnimC3toC2 ? GameVersion.Crash3 : GameVersion.Crash2;
                        else
                            version = cmbMode.SelectedIndex == AnimC3toC3 ? GameVersion.Crash3 : GameVersion.Crash2;
                        
                        var entry = unEntry.Process(version);

                        if (entry is AnimationEntry anim)
                        {
                            int vertexcount = BitConv.FromInt32(unEntry.Items[0], 8);
                            type = vertexcount == 0 ? "Crash3" : "Crash2";

                            int index = cmbMode.SelectedIndex;

                            bool invalid = false;
                            if (cmbType.SelectedIndex == TypeCrash2)
                            {
                                if (index == AnimC2toC2)
                                    invalid = type != "Crash2";
                                else if (index == AnimC3toC2)
                                    invalid = type != "Crash3";
                            }
                            else
                            {
                                if (index == AnimC2toC3)
                                    invalid = type != "Crash2";
                                else if (index == AnimC3toC3)
                                    invalid = type != "Crash3";
                            }
                            if (invalid)
                            {
                                throw new InvalidOperationException("Invalid game version for the animation entry.\n");
                            }

                            animEID = Entry.EIDToEName(BitConv.FromInt32(fileBytes, 4));

                            bool skip = false;
                            foreach (DataGridViewRow row in dgvAnim.Rows)
                            {
                                if (row.Cells[ColFilePath].Value?.ToString() == filePath)
                                {
                                    skip = true;
                                    break;
                                }
                            }
                            if (skip) continue;

                            if (chkSetModelEID.Checked)
                            {
                                var sb = new StringBuilder(animEID);
                                sb[4] = 'G';
                                modelEID = sb.ToString();
                            }

                            newRow.CreateCells(dgvAnim, Path.GetFileNameWithoutExtension(fileInfo.Name), fileInfo.FullName, type, modelEID, animEID);
                            dgvAnim.Rows.Add(newRow);
                        }
                        else if (entry is ModelEntry model)
                        {
                            modelEID = Entry.EIDToEName(BitConv.FromInt32(fileBytes, 4));

                            bool skip = false;
                            foreach (DataGridViewRow row in dgvModel.Rows)
                            {
                                if (row.Cells[ColFilePath].Value?.ToString() == filePath)
                                {
                                    skip = true;
                                    break;
                                }
                            }
                            if (skip) continue;

                            newRow.CreateCells(dgvModel, Path.GetFileNameWithoutExtension(fileInfo.Name), fileInfo.FullName, type, modelEID, animEID);
                            dgvModel.Rows.Add(newRow);
                        }
                        else
                        {
                            throw new InvalidOperationException("Invalid entry type.");
                        }
                    }
                    catch (Exception ex)
                    {
                        DarkMessageBox.ShowError($"Error reading file {filePath}\n\n{ex.Message}", Resources.Title_Error);
                    }
                }

               
                dgvAnim.ScrollBars = ScrollBars.Vertical;
                dgvModel.ScrollBars = ScrollBars.Vertical;
                dgvAnim.ResumeLayout();
                dgvModel.ResumeLayout();
            }
        }

        private void cmdProcess_Click(object sender, EventArgs e)
        {
            //try
            {
                int version = cmbType.SelectedIndex;
                int index = cmbMode.SelectedIndex;
                byte[] fileBytes = [];
                string mode = string.Empty;
                string saveDirectory;
                string savePath;

                if (version == TypeCrash2)
                {
                    foreach (DataGridViewRow row in dgvAnim.Rows)
                    {
                        string fileName = row.Cells[ColFileName].Value.ToString();
                        string filePath = row.Cells[ColFilePath].Value.ToString();
                        string modelEID = row.Cells[ColModelEID].Value.ToString();
                        string animEID = row.Cells[ColAnimEID].Value.ToString();

                        Console.WriteLine($"Processing entry: {fileName}");

                        byte[] file = File.ReadAllBytes(filePath);
                        UnprocessedEntry entry = Entry.Load(file);

                        for (int i = 0; i < entry.Items.Count; i++)
                        {
                            byte[] data = entry.Items[i];

                            if (index == AnimC3toC2)
                            {
                                const int xoffIndex = 0;
                                const int yoffIndex = 2;
                                const int zoffIndex = 4;
                                const int headerSizeIndex = 20;

                                // Remove unknown2 (8 bytes at index 8)
                                byte[] withoutUnknown2 = new byte[data.Length - Unknown2Length];
                                Array.Copy(data, 0, withoutUnknown2, 0, Unknown2Index);
                                Array.Copy(data, Unknown2Index + Unknown2Length, withoutUnknown2, Unknown2Index, data.Length - (Unknown2Index + Unknown2Length));

                                // Insert space for ModelEID (4 bytes at index 16)
                                byte[] withModelEID = new byte[withoutUnknown2.Length + ModelEIDLength];
                                Array.Copy(withoutUnknown2, 0, withModelEID, 0, ModelEIDIndex);
                                Array.Copy(withoutUnknown2, ModelEIDIndex, withModelEID, ModelEIDIndex + ModelEIDLength, withoutUnknown2.Length - ModelEIDIndex);

                                // Set ModelEID value
                                int eid = Entry.ENameToEID(modelEID);
                                BitConv.ToInt32(withModelEID, ModelEIDIndex, eid);

                                // Recalculate values for Crash2 format
                                // multiply offsets by 8
                                short xoff = (short)(BitConv.FromInt16(withModelEID, xoffIndex) >> 3);
                                BitConv.ToInt16(withModelEID, xoffIndex, xoff);
                                short yoff = (short)(BitConv.FromInt16(withModelEID, yoffIndex) >> 3);
                                BitConv.ToInt16(withModelEID, yoffIndex, yoff);
                                short zoff = (short)(BitConv.FromInt16(withModelEID, zoffIndex) >> 3);
                                BitConv.ToInt16(withModelEID, zoffIndex, zoff);

                                // Adjust header size
                                int headerSize = BitConv.FromInt32(withModelEID, headerSizeIndex) - 4;
                                BitConv.ToInt32(withModelEID, headerSizeIndex, headerSize);

                                entry.Items[i] = withModelEID;
                                mode = "C3toC2";
                                fileBytes = entry.Save();
                            }
                            else if (index == AnimC2toC2)
                            {
                                // Set ModelEID
                                int eid = Entry.ENameToEID(modelEID);
                                BitConv.ToInt32(data, ModelEIDIndex, eid);

                                entry.Items[i] = data;
                                mode = "C2toC2";
                                fileBytes = entry.Save();
                            }
                        }

                        // Set AnimEID
                        BitConv.ToInt32(fileBytes, 4, Entry.ENameToEID(animEID));

                        saveDirectory = Path.GetDirectoryName(filePath);
                        savePath = Path.Combine(saveDirectory, $"{fileName}_{mode}.nsentry");
                        File.WriteAllBytes(savePath, fileBytes);
                        Console.WriteLine($"    Saved entry: {savePath}");
                    }

                    foreach (DataGridViewRow row in dgvModel.Rows)
                    {
                        string fileName = row.Cells[ColFileName].Value.ToString();
                        string filePath = row.Cells[ColFilePath].Value.ToString();
                        string modelEID = row.Cells[ColModelEID].Value.ToString();
                        string animEID = row.Cells[ColAnimEID].Value.ToString();

                        Console.WriteLine($"Processing entry: {fileName}");

                        byte[] file = File.ReadAllBytes(filePath);
                        UnprocessedEntry entry = Entry.Load(file);

                        if (index == AnimC3toC2)
                        {
                            ModelEntry model = (ModelEntry)entry.Process(GameVersion.Crash3);

                            // Multiply scale by 8
                            model.ScaleX *= 8;
                            model.ScaleY *= 8;
                            model.ScaleZ *= 8;

                            mode = "C3toC2";
                            fileBytes = model.Save();
                        }
                        else if (index == AnimC2toC2)
                        {
                            mode = "C2toC2";
                            fileBytes = entry.Save();
                        }

                        // Set modelEID
                        BitConv.ToInt32(fileBytes, 4, Entry.ENameToEID(modelEID));

                        saveDirectory = Path.GetDirectoryName(filePath);
                        savePath = Path.Combine(saveDirectory, $"{fileName}_{mode}.nsentry");
                        File.WriteAllBytes(savePath, fileBytes);
                        Console.WriteLine($"    Saved entry: {savePath}");
                    }

                    SystemSounds.Asterisk.Play();
                }

                else if (version == TypeCrash3)
                {
                    foreach (DataGridViewRow row in dgvAnim.Rows)
                    {
                        string? fileName = row.Cells[ColFileName].Value?.ToString();
                        string? filePath = row.Cells[ColFilePath].Value?.ToString();
                        string? animEID = row.Cells[ColAnimEID].Value?.ToString();
                        string? modelEID = row.Cells[ColModelEID].Value?.ToString();

                        Console.WriteLine($"Processing entry: {fileName}");

                        byte[] file = File.ReadAllBytes(filePath);
                        UnprocessedEntry entry = Entry.Load(file);

                        for (int i = 0; i < entry.Items.Count; i++)
                        {
                            byte[] data = entry.Items[i];

                            if (index == AnimC2toC3)
                            {
                                const int xoffIndex = 0;
                                const int yoffIndex = 2;
                                const int zoffIndex = 4;
                                const int headerSizeIndex = 24;

                                // Remove ModelEID (4 bytes at index 16)
                                byte[] withoutModelEID = new byte[data.Length - ModelEIDLength];
                                Array.Copy(data, 0, withoutModelEID, 0, ModelEIDIndex);
                                Array.Copy(data, ModelEIDIndex + ModelEIDLength, withoutModelEID, ModelEIDIndex, data.Length - (ModelEIDIndex + ModelEIDLength));

                                // Insert space for unknown2 (8 bytes at index 8)
                                byte[] withUnknown2 = new byte[withoutModelEID.Length + Unknown2Length];
                                Array.Copy(withoutModelEID, 0, withUnknown2, 0, Unknown2Index);
                                Array.Copy(withoutModelEID, Unknown2Index, withUnknown2, Unknown2Index + Unknown2Length, withoutModelEID.Length - Unknown2Index);

                                // Recalculate values for Crash3 format
                                // divide offsets by 8
                                short xoff = (short)(BitConv.FromInt16(withUnknown2, xoffIndex) << 3);
                                BitConv.ToInt16(withUnknown2, xoffIndex, xoff);
                                short yoff = (short)(BitConv.FromInt16(withUnknown2, yoffIndex) << 3);
                                BitConv.ToInt16(withUnknown2, yoffIndex, yoff);
                                short zoff = (short)(BitConv.FromInt16(withUnknown2, zoffIndex) << 3);
                                BitConv.ToInt16(withUnknown2, zoffIndex, zoff);

                                // Adjust header size
                                int headerSize = BitConv.FromInt32(withUnknown2, headerSizeIndex) + 4;
                                BitConv.ToInt32(withUnknown2, headerSizeIndex, headerSize);

                                entry.Items[i] = withUnknown2;
                                mode = "C2toC3";
                                fileBytes = entry.Save();
                            }
                            else if (index == AnimC3toC3)
                            {
                                // Do nothing
                                mode = "C3toC3";
                                fileBytes = entry.Save();
                            }
                        }

                        // Set AnimEID
                        BitConv.ToInt32(fileBytes, 4, Entry.ENameToEID(animEID));

                        saveDirectory = Path.GetDirectoryName(filePath);
                        savePath = Path.Combine(saveDirectory, $"{fileName}_{mode}.nsentry");
                        File.WriteAllBytes(savePath, fileBytes);
                        Console.WriteLine($"    Saved entry: {savePath}");
                    }

                    foreach (DataGridViewRow row in dgvModel.Rows)
                    {
                        string fileName = row.Cells[ColFileName].Value.ToString();
                        string filePath = row.Cells[ColFilePath].Value.ToString();
                        string modelEID = row.Cells[ColModelEID].Value.ToString();
                        string animEID = row.Cells[ColAnimEID].Value.ToString();

                        Console.WriteLine($"Processing entry: {fileName}");

                        byte[] file = File.ReadAllBytes(filePath);
                        UnprocessedEntry entry = Entry.Load(file);

                        if (index == AnimC2toC3)
                        {
                            ModelEntry model = (ModelEntry)entry.Process(GameVersion.Crash2);

                            // Divide scale by 8
                            model.ScaleX /= 8;
                            model.ScaleY /= 8;
                            model.ScaleZ /= 8;

                            mode = "C2toC3";
                            fileBytes = model.Save();
                        }
                        else if (index == AnimC3toC3)
                        {
                            mode = "C3toC3";
                            fileBytes = entry.Save();
                        }

                        // Set modelEID
                        BitConv.ToInt32(fileBytes, 4, Entry.ENameToEID(modelEID));

                        saveDirectory = Path.GetDirectoryName(filePath);
                        savePath = Path.Combine(saveDirectory, $"{fileName}_{mode}.nsentry");
                        File.WriteAllBytes(savePath, fileBytes);
                        Console.WriteLine($"    Saved entry: {savePath}");
                    }

                    SystemSounds.Asterisk.Play();
                }
            }
        }

        private void ClearRows()
        {
            dgvModel.Rows.Clear();
            dgvModel.ScrollBars = ScrollBars.None;
            dgvAnim.Rows.Clear();
            dgvAnim.ScrollBars = ScrollBars.None;
            cmdClear.Enabled =
            cmdProcess.Enabled = false;
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            ClearRows();
        }

        private void dgvAnim_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            cmdClear.Enabled =
            cmdProcess.Enabled = true;
            //dgvAnim.ScrollBars = ScrollBars.Vertical;
            //if (dgvAnim.Rows.Count <= 16)
            //{
            //    int scrollBarHeight = SystemInformation.HorizontalScrollBarHeight;
            //    dgvAnim.Height = dgvAnim.ColumnHeadersHeight + (dgvAnim.Rows.Count * dgvAnim.Rows[0].Height) + scrollBarHeight;
            //}
        }

        private void dgvModel_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            cmdClear.Enabled =
            cmdProcess.Enabled = true;
            //dgvModel.ScrollBars = ScrollBars.Vertical;
            //if (dgvModel.Rows.Count <= 16)
            //{
            //    int scrollBarHeight = SystemInformation.HorizontalScrollBarHeight;
            //    dgvModel.Height = dgvModel.ColumnHeadersHeight + (dgvModel.Rows.Count * dgvModel.Rows[0].Height) + scrollBarHeight;
            //}
        }

        private void dgvAnim_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == ColFileName || e.ColumnIndex == ColFilePath || e.ColumnIndex == ColType)
                e.Cancel = true;
        }

        private void dgvModel_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == ColFileName || e.ColumnIndex == ColFilePath || e.ColumnIndex == ColType)
                e.Cancel = true;
        }

        private void EID_Validating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != ColModelEID && e.ColumnIndex != ColAnimEID)
                return;

            string error = Entry.CheckEIDErrors(e.FormattedValue.ToString(), true);
            if (error != string.Empty)
            {
                DarkMessageBox.ShowError(error, "EID Error");
                e.Cancel = true;
            }
        }

        private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbMode.SelectedIndex;
            if (index < 0) return;

            ClearRows();
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbType.SelectedIndex;
            if (index < 0) return;

            ClearRows();
            cmbMode.Items.Clear();
            if (index == TypeCrash2)
            {
                cmbMode.Items.AddRange(
                [
                    "Crash3 -> Crash2",
                    "Crash2 -> Crash2"
                ]);
                cmbMode.SelectedIndex = AnimC3toC2;
            }
            else
            {
                cmbMode.Items.AddRange(
                [
                    "Crash2 -> Crash3",
                    "Crash3 -> Crash3"
                ]);
                cmbMode.SelectedIndex = AnimC2toC3;
            }
        }
    }
}
