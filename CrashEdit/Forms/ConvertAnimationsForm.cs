using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using Frame = CrashEdit.Crash.Frame;

namespace CrashEdit.CE
{
    public partial class ConvertAnimationsForm : DarkForm
    {
        private List<UnprocessedEntry> loadedEntries = new List<UnprocessedEntry>();

        private OldModelEntry oldModel;

        private const int ColFileName = 0;
        private const int ColFilePath = 1;
        private const int ColType = 2;
        private const int ColAnimEID = 3;
        private const int ColModelEID = 4;

        private const int TypeModel = 0;
        private const int TypeAnimation = 1;

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

            cmbType.Items.AddRange(new object[]
            {
                "Model",
                "Animation"
            });
            cmbType.SelectedIndex = TypeModel;
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            if (cmbType.SelectedIndex < 0) return;

            List<OldModelEntry> oldModelEntries = new List<OldModelEntry>();
            List<OldAnimationEntry> oldAnimationEntries = new List<OldAnimationEntry>();
            List<ColoredAnimationEntry> coloredAnimationEntries = new List<ColoredAnimationEntry>();

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

                            if (cmbType.SelectedIndex == TypeModel)
                            {
                                string type = string.Empty;
                                DataGridViewRow newRow = new DataGridViewRow();
                                var _entry = entry.Process(GameVersion.Crash1);

                                if (_entry is OldModelEntry oldModelEntry && (cmbMode.SelectedIndex == 0 || cmbMode.SelectedIndex == 1))
                                {
                                    type = "OldModel";
                                    newRow.Tag = Entry.EIDToEName(oldModelEntry.EID);

                                    oldModelEntries.Add(oldModelEntry);
                                }
                                else if (_entry is OldAnimationEntry oldAnimationEntry && (cmbMode.SelectedIndex == 0 || cmbMode.SelectedIndex == 2))
                                {
                                    type = "OldAnimation";
                                    newRow.Tag = Entry.EIDToEName(oldAnimationEntry.Frames[0].ModelEID);

                                    if (cmbMode.SelectedIndex == 0)
                                    {
                                        oldAnimationEntries.Add(oldAnimationEntry);
                                    }
                                    else
                                    {
                                        newRow.CreateCells(dgvAnim, Path.GetFileNameWithoutExtension(fileInfo.Name), fileInfo.FullName, type, string.Empty, string.Empty);
                                        dgvAnim.Rows.Add(newRow);
                                        continue;
                                    }
                                }
                                else if (_entry is ColoredAnimationEntry coloredAnimationEntry && (cmbMode.SelectedIndex == 1 || cmbMode.SelectedIndex == 3))
                                {
                                    type = "ColoredAnimation";
                                    newRow.Tag = Entry.EIDToEName(coloredAnimationEntry.Frames[0].ModelEID);

                                    if (cmbMode.SelectedIndex == 1)
                                    {
                                        coloredAnimationEntries.Add(coloredAnimationEntry);
                                    }
                                    else
                                    {
                                        newRow.CreateCells(dgvAnim, Path.GetFileNameWithoutExtension(fileInfo.Name), fileInfo.FullName, type, string.Empty, string.Empty);
                                        dgvAnim.Rows.Add(newRow);
                                        continue;
                                    }
                                }
                                else
                                {
                                    throw new InvalidOperationException("Invalid entry type.");
                                }

                                foreach (OldModelEntry model in oldModelEntries)
                                {
                                    dynamic anims = cmbMode.SelectedIndex == 0 ? oldAnimationEntries : coloredAnimationEntries;
                                    foreach (var anim in anims)
                                    {
                                        if (model.EID == anim.Frames[0].ModelEID)
                                        {
                                            newRow.CreateCells(dgvAnim, Path.GetFileNameWithoutExtension(fileInfo.Name), fileInfo.FullName, type, Entry.EIDToEName(anim.EID), Entry.EIDToEName(model.EID));
                                            newRow.Tag = new KeyValuePair<object, object>(anim, model);
                                            foreach (DataGridViewRow row in dgvAnim.Rows)
                                            {
                                                if (Convert.ToString(row.Cells[ColAnimEID].Value) == Entry.EIDToEName(anim.EID))
                                                {
                                                    throw new InvalidOperationException("Duplicate entry.");
                                                }
                                            }
                                            dgvAnim.Rows.Add(newRow);
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                int vertexcount = BitConv.FromInt32(entry.Items[0], 8);
                                string type = vertexcount == 0 ? "Crash3" : "Crash2";

                                string animEID = Entry.EIDToEName(BitConv.FromInt32(fileBytes, 4));
                                string modelEID = string.Empty;
                                if (chkSetModelEID.Checked)
                                {
                                    modelEID = animEID.Substring(0, animEID.Length - 1) + "G";
                                }
                                DataGridViewRow row = new DataGridViewRow();
                                row.CreateCells(dgvAnim, Path.GetFileNameWithoutExtension(fileInfo.Name), fileInfo.FullName, type, animEID, modelEID);
                                dgvAnim.Rows.Add(row);
                            }
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
            try
            {
                OldModelEntry? oldModelEntry = null;
                OldAnimationEntry? oldAnimationEntry = null;
                ColoredAnimationEntry? coloredAnimationEntry = null;
                if (cmbType.SelectedIndex == TypeModel)
                {
                    foreach (DataGridViewRow row in dgvAnim.Rows)
                    {
                        string? fileName = row.Cells[ColFileName].Value?.ToString();
                        string? filePath = row.Cells[ColFilePath].Value?.ToString();
                        string? type = row.Cells[ColType].Value?.ToString();

                        FileInfo fileInfo = new FileInfo(filePath);

                        byte[] file = File.ReadAllBytes(filePath);
                        string? saveDirectory = Path.GetDirectoryName(filePath);
                        if (saveDirectory == null)
                        {
                            throw new InvalidOperationException("Failed to get directory name from file path.");
                        }

                        if (cmbMode.SelectedIndex == 0 || cmbMode.SelectedIndex == 1) // [OldModel & OldAnimation -> Model], [OldModel & ColoredAnimation -> Model]
                        {
                            if (cmbMode.SelectedIndex == 0)
                            {
                                if (row.Tag is KeyValuePair<object, object> pair)
                                {
                                    oldAnimationEntry = (OldAnimationEntry)pair.Key;
                                    oldModelEntry = (OldModelEntry)pair.Value;
                                }
                                else
                                {
                                    throw new InvalidOperationException("Invalid tag.");
                                }
                            }
                            else
                            {
                                if (row.Tag is KeyValuePair<object, object> pair)
                                {
                                    coloredAnimationEntry = (ColoredAnimationEntry)pair.Key;
                                    oldModelEntry = (OldModelEntry)pair.Value;
                                }
                                else
                                {
                                    throw new InvalidOperationException("Invalid tag.");
                                }
                            }

                            {
                                ModelEntry modelEntry = null!;
                                if (cmbMode.SelectedIndex == 0) // OldAnimation
                                {
                                    modelEntry = ModelConverter.ConvertOldModelEntry(oldModelEntry, oldAnimationEntry, coloredAnimationEntry, false);
                                }
                                else // ColoredAnimation
                                {
                                    modelEntry = ModelConverter.ConvertOldModelEntry(oldModelEntry, oldAnimationEntry, coloredAnimationEntry, true);
                                }

                                Console.WriteLine("Saving...");
                                byte[] fileBytes = modelEntry.Save();

                                string savePath = Path.Combine(saveDirectory, $"{Entry.EIDToEName(oldModelEntry.EID)}_C1toC2.nsentry");
                                File.WriteAllBytes(savePath, fileBytes);
                                Console.WriteLine($"    Saved entry: {savePath}");
                            }
                            {
                                dynamic anim = cmbMode.SelectedIndex == 0 ? oldAnimationEntry : coloredAnimationEntry;

                                List<Frame> Frames = new List<Frame>();
                                foreach (OldFrame frame in anim.Frames)
                                {
                                    List<FrameVertex> vertices = new();
                                    foreach (OldFrameVertex vertex in frame.Vertices)
                                    {
                                        // swap Y and Z axes
                                        vertices.Add(new FrameVertex(vertex.X, vertex.Z, vertex.Y));
                                    }
                                    byte[] verts = vertices.SelectMany(v => v.Save()).ToArray();
                                    int length = verts.Length;
                                    int paddedLength = (length % 4 == 0) ? length : (length / 4 + 1) * 4;
                                    byte[] paddedVertices = new byte[paddedLength];
                                    Array.Copy(verts, paddedVertices, length);
                                    for (int j = length; j < paddedLength; j++)
                                    {
                                        paddedVertices[j] = 0x00;
                                    }

                                    short unknown = -1; // ?
                                    int headersize = 0x40;
                                    List<FrameCollision> collisions = new List<FrameCollision> { frame.collision };
                                    int specialvertexcount = 0;

                                    bool[] temporals = new bool[paddedVertices.Length / 4 * 32];
                                    for (int i = 0; i < paddedVertices.Length / 4; ++i)
                                    {
                                        int val = BitConv.FromInt32(paddedVertices, i * 4);
                                        for (int j = 0; j < 32; ++j)
                                        {
                                            temporals[i * 32 + j] = (val >> (31 - j) & 0x1) == 1;
                                        }
                                    }

                                    Frames.Add(new Frame((short)frame.XOffset, (short)frame.YOffset, (short)frame.ZOffset, unknown, frame.ModelEID, headersize, collisions, vertices, specialvertexcount, temporals, false));
                                }
                                AnimationEntry animationEntry = new AnimationEntry(Frames, false, anim.EID);

                                Console.WriteLine("Saving...");
                                byte[] fileBytes = animationEntry.Save();

                                string savePath = Path.Combine(saveDirectory, $"{Entry.EIDToEName(anim.EID)}_C1toC2.nsentry");
                                File.WriteAllBytes(savePath, fileBytes);
                                Console.WriteLine($"    Saved entry: {savePath}");
                            }
                        }
                        else if (cmbMode.SelectedIndex == 2 || cmbMode.SelectedIndex == 3) // [OldAnimation -> Animation], [ColoredAnimation -> Animation]
                        {
                            UnprocessedEntry entry = Entry.Load(file);
                            dynamic anim = null!;
                            string animString = string.Empty;
                            if (cmbMode.SelectedIndex == 2)
                            {
                                anim = (OldAnimationEntry)entry.Process(GameVersion.Crash1);
                                animString = "normals";
                            }
                            else
                            {
                                anim = (ColoredAnimationEntry)entry.Process(GameVersion.Crash1);
                                animString = "colors";
                            }

                            Console.WriteLine($"Loaded entry: {fileInfo.Name}");

                            for (int i = 0; i < anim.Frames.Count; i++)
                            {
                                List<byte[]> vertices = new List<byte[]>();
                                List<byte[]> colors = new List<byte[]>();
                                foreach (OldFrameVertex vertex in anim.Frames[i].Vertices)
                                {   
                                    // swap Y and Z axes
                                    vertices.Add(new byte[] { vertex.X, vertex.Z, vertex.Y });
                                    colors.Add(new byte[] { vertex.R, vertex.G, vertex.B, 0x00 });
                                }

                                byte[] verts = vertices.SelectMany(v => v).ToArray();
                                int length = verts.Length;
                                int paddedLength = (length % 4 == 0) ? length : (length / 4 + 1) * 4;
                                byte[] paddedVertices = new byte[paddedLength];
                                Array.Copy(verts, paddedVertices, length);
                                for (int j = length; j < paddedLength; j++)
                                {
                                    paddedVertices[j] = 0x00;
                                }

                                string savePath = Path.Combine(saveDirectory, $"{Path.GetFileNameWithoutExtension(fileInfo.Name)}[{i}]_vertices");
                                File.WriteAllBytes(savePath, paddedVertices);

                                savePath = Path.Combine(saveDirectory, $"{Path.GetFileNameWithoutExtension(fileInfo.Name)}[{i}]_{animString}");
                                File.WriteAllBytes(savePath, colors.SelectMany(v => v).ToArray());

                                Console.WriteLine("Saved.");
                            }
                        }
                    }
                }
                else
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
            }
            catch (Exception ex)
            {
                DarkMessageBox.ShowError($"{ex.Message}", Resources.Title_Error);
            }
        }

        private void ClearRows()
        {
            if (loadedEntries != null) loadedEntries.Clear();
            else loadedEntries = new List<UnprocessedEntry>();

            dgvAnim.Rows.Clear();
            dgvAnim.ScrollBars = ScrollBars.None;
            cmdClear.Enabled =
            cmdProcess.Enabled = false;
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            if (DarkMessageBox.ShowWarning("Are you sure you want to clear the list?", Text, DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                ClearRows();
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
            if (cmbMode.SelectedIndex < 0) return;

            ClearRows();

            if (cmbType.SelectedIndex == TypeModel)
            {
                if (cmbMode.SelectedIndex == 0 || cmbMode.SelectedIndex == 1)
                {
                    dgvAnim.Columns[ColFileName].Visible = false;
                    dgvAnim.Columns[ColType].Visible = false;
                    dgvAnim.Columns[ColModelEID].Visible = true;
                    dgvAnim.Columns[ColAnimEID].Visible = true;
                }
                else
                {
                    dgvAnim.Columns[ColFileName].Visible = true;
                    dgvAnim.Columns[ColType].Visible = true;
                    dgvAnim.Columns[ColModelEID].Visible = false;
                    dgvAnim.Columns[ColAnimEID].Visible = false;
                }
            }
            else
            {
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

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedIndex < 0) return;

            ClearRows();
            cmbMode.Items.Clear();
            if (cmbType.SelectedIndex == TypeModel)
            {
                cmbMode.Items.AddRange(new object[]
                {
                    "OldModel & OldAnim → Model",
                    "OldModel & ColoredAnim → Model",
                    "OldAnim Info",
                    "ColoredAnim Info"
                });
                cmbMode.SelectedIndex = 0;

                //dgvAnim.Columns[ColModelEID].Visible =
                //dgvAnim.Columns[ColAnimEID].Visible = false;
                chkSetModelEID.Enabled = false;
                dgvAnim.Columns[ColFileName].Visible = false;
                dgvAnim.Columns[ColType].Visible = false;
            }
            else
            {
                cmbMode.Items.AddRange(new object[]
                {
                    "Crash3 → Crash2",
                    "Crash2 → Crash2",
                    "Crash2 → Crash3",
                    "Crash3 → Crash3"
                });
                cmbMode.SelectedIndex = ModeC3toC2;

                //dgvAnim.Columns[ColModelEID].Visible =
                //dgvAnim.Columns[ColAnimEID].Visible = true;
                chkSetModelEID.Enabled = true;
                dgvAnim.Columns[ColFileName].Visible = true;
                dgvAnim.Columns[ColType].Visible = true;
            }
        }
    }

    public static class ModelConverter
    {
        private static List<int> texturePages = new List<int>();

        public static ModelEntry ConvertOldModelEntry(OldModelEntry oldEntry, OldAnimationEntry oldAnimationEntries, ColoredAnimationEntry coloredAnimationEntries, bool isColored)
        {
            if (oldEntry == null)
                throw new ArgumentNullException(nameof(oldEntry));

            Console.WriteLine($"Start converting entry: {oldEntry.EName}");

            // Convert polygons to triangles
            Console.WriteLine($"Converting {oldEntry.Polygons.Count} OldModelPolygon to ModelTransformedTriangle...");
            List<ModelTransformedTriangle> triangles = ConvertPolygonToTriangle(oldEntry.Polygons, oldEntry.Structs);

            // Reverse calculation for ModelStructs using ModelTriangle.Load logic
            Console.WriteLine($"Converting {triangles.Count} ModelTransformedTriangle to ModelStruct...");
            List<ModelStruct> modelStructs = ConvertTriangleToModelStruct(triangles);

            // Convert ModelStructs to PolyData
            Console.WriteLine($"Converting {modelStructs.Count + 1} ModelStruct to PolyData...");
            uint[] polyData = ConvertModelStructToPolyData(modelStructs);

            // Load texture and color information from OldModelStruct
            Console.WriteLine($"Converting {oldEntry.Structs.Count} OldModelStructs to ModelEntry data...");
            ConvertOldModelStructs(oldEntry, isColored, coloredAnimationEntries, out List <SceneryColor> colors, out List<ModelTexture> textures);

            // AnimatedTextures not handled
            List<ModelExtendedTexture> animatedTextures = new List<ModelExtendedTexture>();

            // Convert Info field
            byte[] info = new byte[0x50];
            BitConv.ToInt32(info, 0, oldEntry.ScaleX);
            BitConv.ToInt32(info, 4, oldEntry.ScaleY);
            BitConv.ToInt32(info, 8, oldEntry.ScaleZ);
            for (int i = 0; i < texturePages.Count; i++)
            {
                BitConv.ToInt32(info, 12 + i * 4, texturePages[i]);
            }
            BitConv.ToInt32(info, 0x2C, polyData.Length);
            BitConv.ToInt32(info, 0x30, 0x1); // ?
            BitConv.ToInt32(info, 0x34, textures.Count);
            if (isColored)
            {
                BitConv.ToInt32(info, 0x38, coloredAnimationEntries.Frames[0].Vertices.Count);
            }
            else
            {
                BitConv.ToInt32(info, 0x38, oldAnimationEntries.Frames[0].Vertices.Count);
            }
            BitConv.ToInt32(info, 0x3C, colors.Count);
            BitConv.ToInt32(info, 0x40, texturePages.Count);
            BitConv.ToInt32(info, 0x44, triangles.Count);

            // Debug log for ConvertIndices data validation
            Console.WriteLine("Colors Count = " + colors.Count);
            Console.WriteLine("Textures Count = " + textures.Count);

            // Create ModelEntry
            Console.WriteLine("Creating ModelEntry...");
            return new ModelEntry(
                info,
                polyData,
                colors,
                textures,
                animatedTextures,
                null, // Positions not handled
                oldEntry.EID
            );
        }

        private static List<ModelStruct> ConvertTriangleToModelStruct(List<ModelTransformedTriangle> triangles)
        {
            List<ModelStruct> modelStructs = new List<ModelStruct>();
            Dictionary<byte, int> pos = new Dictionary<byte, int>();
            List<int> vtx = new List<int>();
            int lastValidCC = -3; // dirty hack
            int lastCCpos = -1;
            int lastAApos = -1;
            int lastcolor = -1;
            int lastNonBB = -1;

            for (int i = 0; i < triangles.Count; i++)
            {
                var triangle = triangles[i];
                //for (int j = 0; j < 3; j++)
                //{
                //    if (!vertexMap.ContainsKey((byte)triangle.Vertex[j]))
                //    {
                //        vertexMap[(byte)triangle.Vertex[i]] = vertexCounter++;
                //    }
                //}

                if (i == 0)
                {
                    modelStructs.Add(new ModelColor(0, 0)); // Placeholder for color data (adjust as needed)
                }

                var modelTriangle = new ModelTriangle(
                    (byte)(triangle.Texture / 3), // TODO: Implement logic for calculating TextureIndex
                    triangle.Animated,
                    (byte)i, // TODO: Implement logic for calculating ColorIndex
                    ModelTriangle.NullPtr, // Key TODO: Implement logic for calculating Key
                    unknown: 0,
                    (byte)ModelTriangle.IndexType.Original, // Type TODO: Implement logic for calculating Type
                    flag: true,
                    tritype: (byte)((triangle.Type << 2) | triangle.Subtype) // tritype TODO: Implement logic for calculating tritype
                );
                modelStructs.Add(modelTriangle);
            }

            return modelStructs;
        }

        private static List<int[]> modelVertices = new List<int[]>();

        private static List<ModelTransformedTriangle> ConvertPolygonToTriangle(IList<OldModelPolygon> polygons, IList<OldModelStruct> structs)
        {
            List<ModelTransformedTriangle> triangles = new List<ModelTransformedTriangle>();
            List<ModelTransformedTriangle> existingTriangles = new List<ModelTransformedTriangle>();

            foreach (var polygon in polygons)
            {
                modelVertices.Add(new int[] { polygon.VertexA, polygon.VertexB, polygon.VertexC });
            }

            for (int i = 0; i < polygons.Count; i++)
            {
                var polygon = polygons[i];

                // Decode Type and Subtype based on additional vertex information
                byte type = CalculateTriType(polygon.VertexA, polygon.VertexB, polygon.VertexC, existingTriangles, polygons.Count, i);
                byte subtype = CalculateTriSubtype(polygon.VertexA, polygon.VertexB, polygon.VertexC);

                // Convert OldModelPolygon to ModelTransformedTriangle
                var triangle = new ModelTransformedTriangle(
                    polygon.VertexA / 6, // Vertex 1
                    polygon.VertexB / 6, // Vertex 2
                    polygon.VertexC / 6, // Vertex 3
                    0, 0, 0, // Placeholder for colors (to be calculated if needed)
                    polygon.TexInfo, // Texture
                    type, // Type
                    subtype, // Subtype
                    false // Animated flag
                );

                triangles.Add(triangle);
                existingTriangles.Add(triangle);
                Console.WriteLine($"[{i}] {polygon.VertexA / 6}, {polygon.VertexB / 6}, {polygon.VertexC / 6}, Type: {type}, Subtype: {subtype}");
            }

            return triangles;
        }


        private static byte CalculateTriType(int vertexA, int vertexB, int vertexC, List<ModelTransformedTriangle> existingTriangles, int polygonCount, int idx)
        {
            // First triangle always starts with CC
            if (existingTriangles.Count <= 2)
            {
                return 2; // CC
            }
            // TODO
            if (idx >= polygonCount - 3)
            {
                return 0; // AA
            }

            // Check if the triangle follows AA rules
            if (vertexA > vertexB && vertexB > vertexC)
            {
                return 0; // AA
            }

            // Check if the triangle follows BB rules
            if (vertexB > vertexA && vertexC < vertexB)
            {
                return 1; // BB
            }

            // Default to CC if no other rules match
            return 2; // CC
        }


        private static byte CalculateTriSubtype(int vertexA, int vertexB, int vertexC)
        {
            // Determine subtype dynamically based on forward face and vertex indices
            //if (vertexA > vertexB || vertexB > vertexC)
            //{
            //    return 1; // Counter-clockwise
            //}
            //if (vertexA < vertexB || vertexB < vertexC)
            //{
            //    return 3; // Clockwise
            //}

            return 2; // Double-sided
        }

        private static uint[] ConvertModelStructToPolyData(List<ModelStruct> modelStructs)
        {
            uint[] polyData = new uint[modelStructs.Count + 1];
            for (int i = 0; i < modelStructs.Count; i++)
            {
                if (modelStructs[i] is ModelTriangle modelTriangle)
                {
                    polyData[i] = modelTriangle.Save();
                }
                else if (modelStructs[i] is ModelColor modelColor)
                {
                    polyData[i] = modelColor.Save();
                }
            }
            polyData[^1] = 0xFFFFFFFF; // End marker

            return polyData;
        }

        private static void ConvertOldModelStructs(OldModelEntry oldEntry, bool isColored, ColoredAnimationEntry coloredAnimationEntries, out List<SceneryColor> colors, out List<ModelTexture> textures)
        {
            colors = new List<SceneryColor>();
            textures = new List<ModelTexture>();
            foreach (OldModelStruct oldStruct in oldEntry.Structs)
            {
                if (oldStruct is OldSceneryColor oldColor)
                {
                    if (!isColored)
                    {
                        colors.Add(new SceneryColor(oldColor.R, oldColor.G, oldColor.B, 0));
                    }
                }
                else if (oldStruct is OldModelTexture oldTexture)
                {
                    if (!isColored)
                    {
                        colors.Add(new SceneryColor(oldTexture.R, oldTexture.G, oldTexture.B, 0));
                    }
                    if (!texturePages.Contains(oldTexture.EID))
                    {
                        texturePages.Add(oldTexture.EID);
                        if (texturePages.Count > 8)
                        {
                            throw new InvalidOperationException("Too many texture pages.");
                        }
                    }

                    int maxU = Math.Max(oldTexture.U1, Math.Max(oldTexture.U2, oldTexture.U3));
                    int maxV = Math.Max(oldTexture.V1, Math.Max(oldTexture.V2, oldTexture.V3));

                    byte u1 = (byte)(oldTexture.U1 == maxU ? maxU - 1 : oldTexture.U1);
                    byte v1 = (byte)(oldTexture.V1 == maxV ? maxV - 1 : oldTexture.V1);
                    byte clutx = (byte)oldTexture.ClutX;
                    byte cluty1 = (byte)((oldTexture.ClutY & 0x3) << 2);
                    byte cluty2 = (byte)(oldTexture.ClutY >> 2);
                    byte u2 = (byte)(oldTexture.U2 == maxU ? maxU - 1 : oldTexture.U2);
                    byte v2 = (byte)(oldTexture.V2 == maxV ? maxV - 1 : oldTexture.V2);
                    byte segment = (byte)oldTexture.Segment;
                    byte blendmode = (byte)oldTexture.BlendMode;
                    byte colormode = (byte)oldTexture.ColorMode;
                    byte textureoffset = (byte)texturePages.IndexOf(oldTexture.EID);
                    byte u3 = (byte)(oldTexture.U3 == maxU ? maxU - 1 : oldTexture.U3);
                    byte v3 = (byte)(oldTexture.V3 == maxV ? maxV - 1 : oldTexture.V3);
                    byte u4 = 0;
                    byte v4 = 0;
                    textures.Add(new ModelTexture(u1, v1, cluty1, clutx, cluty2, u2, v2, colormode, blendmode, segment, textureoffset, u3, v3, u4, v4));
                    //Console.WriteLine($"{u1}, {v1}, {u2}, {v2}, {u3}, {v3}");
                }
            }
            if (isColored)
            {
                // TODO Fix this

                //int cur_vert_idx = 0;
                foreach (OldFrameVertex vert in coloredAnimationEntries.Frames[0].Vertices)
                {
                    //Rgba old_rgba = ;
                    //colors.Add(new SceneryColor(
                    //    (byte)(old_rgba.r * 2 * vert.Red),
                    //    (byte)(old_rgba.g * 2 * vert.Green),
                    //    (byte)(old_rgba.b * 2 * vert.Blue),
                    //    0));
                    //cur_vert_idx++;
                    colors.Add(new SceneryColor(vert.R, vert.G, vert.B, 0));
                }
            }
        }
    }
}
