using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.Crash;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using static CrashEdit.CE.ModelConverter;

namespace CrashEdit.CE
{
    public partial class ModelConverterForm : DarkForm
    {
        private static readonly string Version = "1.0.0";

        private int currentIndex = 0;
        private Dictionary<string, ModelItem> modelItems = [];
        private ModelSettings modelSettings;
        private string modelPath;
        private string settingsPath;
        private string exporterVersion;

        private FileSystemWatcher? watcher;
        private readonly System.Windows.Forms.Timer reloadTimer;

        private readonly Debug debug = new()
        {
            DebugMaterials = false,
            DebugTextures = false,
            DebugModels = false,
            DebugMode = false,
            TestCompression = false
        };

        private readonly DarkToolTip toolTip1 = new();
        private readonly DarkToolTip toolTip2 = new();
        private readonly DarkToolTip toolTip3 = new();
        private readonly DarkToolTip toolTip4 = new();
        private readonly DarkToolTip toolTip5 = new();
        private readonly DarkToolTip toolTip6 = new();
        private readonly DarkToolTip toolTip7 = new();
        private readonly DarkToolTip toolTip8 = new();
        private readonly DarkToolTip toolTip9 = new();
        private readonly DarkToolTip toolTip10 = new();

        internal Stack<bool> dirty = new();
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        public ModelConverterForm()
        {
            InitializeComponent();
            Icon = Embeds.GetIcon("Plugin");

            DgvBatchInit();
            lblPath.Text = "";
            lblExportPath.Text = "";
            lblModelPath.Text = "";
            lblModel.Text = "";
            lblObject.Text = "";
            lblVersion.Text = $"\r\nConverter: v{Version}";

            toolTip1.SetToolTip(lblStripIterations, "Number of iterations to generate triangle strips.");
            toolTip2.SetToolTip(lblMaxKeyWeight, "Penalty weight for longer-living position keys.");
            toolTip3.SetToolTip(chkCompressModel, "Enables the model compression and sets the method.");
            toolTip4.SetToolTip(chkSkipOddFrames, "Skips output on every odd frame.\r\nEnable this when using frame interpolation.");
            toolTip5.SetToolTip(cmdOpen, "You can also drag and drop a file onto this form.");
            toolTip6.SetToolTip(lblScaleMod, "Use this only if the model scale in Blender is incorrect.\r\nIt is recommended to fix the scale in Blender instead.");
            toolTip7.SetToolTip(chkAutoSave, "Saves the settings file automatically before conversion.");
            toolTip8.SetToolTip(chkTestCompression, "Tries all compression methods to find the most efficient one.\r\nRequires model compression to be enabled.");
            toolTip9.SetToolTip(lblBaseTpage, "Sets the base TPage name for the model's textures.\r\nThe converter replaces '_' in the name with the actual TPage index.");
            toolTip10.SetToolTip(chkBatchProcess, "When enabled, model setting changes will apply to all selected models in the object list.");

            cmdSetExportPath.Image = new Bitmap(Embeds.Bitmaps["FolderOpen"], new Size(16, 16));
            cmdSetModelPath.Image = new Bitmap(Embeds.Bitmaps["FolderOpen"], new Size(16, 16));

            numScaleFX.MouseWheel += new MouseEventHandler(ScrollHandlerFunction1);
            numScaleFY.MouseWheel += new MouseEventHandler(ScrollHandlerFunction1);
            numScaleFZ.MouseWheel += new MouseEventHandler(ScrollHandlerFunction1);
            numScaleX.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);
            numScaleY.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);
            numScaleZ.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);

            numScaleMod.MouseWheel += new MouseEventHandler(ScrollHandlerFunction3);

            reloadTimer = new()
            {
                Interval = 1000
            };
            reloadTimer.Tick += ReloadTimer_Tick;
        }

        private void StartWatching(string path)
        {
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
                watcher = null;
            }

            watcher = new FileSystemWatcher(Path.GetDirectoryName(path)!)
            {
                Filter = Path.GetFileName(path),
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
            };

            watcher.Changed += OnJsonChanged;
            watcher.Created += OnJsonChanged;
            watcher.Renamed += OnJsonChanged;
            watcher.EnableRaisingEvents = true;
        }

        private void OnJsonChanged(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(100);

            if (IsDisposed || Disposing) return;
            if (!IsHandleCreated) return;

            BeginInvoke((MethodInvoker)(() =>
            {
                reloadTimer.Stop();
                reloadTimer.Start();
            }));
        }

        private void ReloadTimer_Tick(object? sender, EventArgs e)
        {
            reloadTimer.Stop();

            if (IsDisposed) return;

            Console.WriteLine();
            Console.WriteLine("Model JSON file changed, reloading...");
            var jsons = LoadModelJson(modelPath);
            CreateRows(jsons);
            CreateLists();
        }

        private void DgvBatchInit()
        {
            DoubleBufferedDataGridView.Initialize(dgvBatch);

            dgvBatch.Columns.Add("Name", "Name");
            dgvBatch.Columns.Add("Model", "Model");
            dgvBatch.Columns.Add("Anim", "Anim");
            foreach (DataGridViewColumn column in dgvBatch.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            dgvBatch.Columns[0].Width = 120;
            dgvBatch.Columns[1].Width = 60;
            dgvBatch.Columns[2].Width = 60;
        }

        private void cmdOpen_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new();
            ofd.Filter = FileFilters.JSON;
            if (ofd.ShowDialog() == DialogResult.OK)
                TryOpenSettingsFile(ofd.FileName);
        }

        private void TryOpenSettingsFile(string path)
        {
            Console.WriteLine();
            Console.WriteLine($"Opening file: {path}");

            if (Path.GetFileNameWithoutExtension(path) == "export_zones")
            {
                Console.WriteLine("  Converting zones...");
                ZoneConverter.Import(path);
                return;
            }

            try
            {
                // try to load settings file
                modelSettings = ModelSettingsIO.Load(path);
                settingsPath = path;
                LoadSettings(true);
                Console.WriteLine("  Settings loaded.");
            }
            catch (Exception)
            {
                // if failed, try to load model JSON file and find/create settings file from it
                Console.Write("  Failed to load settings, trying to load the file as a model JSON file...  ");
                if (!TryOpenModelFiles(path, true))
                    return;
            }

            pnBottom.Enabled =
            fraSettings.Enabled = true;

            StartWatching(modelPath);
        }

        private bool TryOpenModelFiles(string path, bool updateSettings)
        {
            try
            {
                List<C2Json> jsons = LoadModelJson(path);

                string saveDirectory = Path.GetDirectoryName(path)!;
                string fileName = Path.GetFileNameWithoutExtension(path);
                settingsPath = Path.Combine(saveDirectory, $"{fileName}_settings.json");

                // try to load settings file for the model JSON, if it doesn't exist, create new settings with default values
                if (File.Exists(settingsPath))
                {
                    Console.WriteLine("Success.");
                    Console.WriteLine("  Found existing settings.");
                    modelSettings = ModelSettingsIO.Load(settingsPath);
                    LoadSettings(updateSettings);
                    Console.WriteLine("  Settings loaded.");
                }
                else
                {
                    Console.WriteLine("Success.");
                    Console.WriteLine("  No existing settings found, creating new default settings...");
                    dirty.Push(true);
                    numScaleX.Value = (decimal)BaseScales.ModelScale;
                    numScaleY.Value = (decimal)BaseScales.ModelScale;
                    numScaleZ.Value = (decimal)BaseScales.ModelScale;
                    numScaleFX.Value = (decimal)BaseScales.ModelScaleFactor;
                    numScaleFY.Value = (decimal)BaseScales.ModelScaleFactor;
                    numScaleFZ.Value = (decimal)BaseScales.ModelScaleFactor;
                    numScaleMod.Value = 1.0M;
                    chkSkipOddFrames.Checked = false;
                    numMaxStripIterations.Value = 64.0M;
                    numMaxLiveKeysWeight.Value = 1000.0M;
                    numAvgKeysWeight.Value = 100.0M;
                    numStripCountWeight.Value = 10.0M;
                    chkCompressModel.Checked = false;
                    radioButton1.Checked = true;
                    txtBaseTpage.Text = "00_0T";

                    modelSettings = new();

                    lblExportPath.Text = saveDirectory;
                    lblModelPath.Text = path;
                    modelPath = lblModelPath.Text;

                    CreateRows(jsons);
                    CreateModelObjects();

                    SaveSettings();
                    LoadSettings(true);

                    dirty.Pop();
                }
                return true;
            }
            catch (Exception ex)
            {
                DarkMessageBox.ShowError($"The selected file is not a valid model JSON file.\n\nDetails: {ex.Message}", "Invalid File");
                return false;
            }
        }

        private void CreateModelObjects()
        {
            modelSettings.ModelObjects = [];
            modelSettings.ModelItems = [];
            modelItems = [];
            for (int i = 0; i < dgvBatch.Rows.Count; i++)
            {
                string name = dgvBatch.Rows[i].Cells[0].Value.ToString();
                string modelEID = dgvBatch.Rows[i].Cells[1].Value.ToString();
                string animEID = dgvBatch.Rows[i].Cells[2].Value.ToString();

                modelSettings.ModelObjects.Add(new ModelObject()
                {
                    Name = name,
                    ModelEID = modelEID,
                    AnimEID = animEID
                });

                if (!modelItems.ContainsKey(modelEID))
                {
                    modelItems.Add(modelEID, new ModelItem()
                    {
                        ModelEID = modelEID,
                        ModelScales = [(int)BaseScales.ModelScale, (int)BaseScales.ModelScale, (int)BaseScales.ModelScale],
                        ScaleFactor = [BaseScales.ModelScaleFactor, BaseScales.ModelScaleFactor, BaseScales.ModelScaleFactor],
                        ScaleMod = 1.0f,
                        CompressionMethod = -1
                    });
                    Console.WriteLine($"    Added model item for EID: {modelEID}");
                }
            }

            foreach (var kvp in modelItems)
            {
                var item = kvp.Value;
                modelSettings.ModelItems.Add(new ModelItem()
                {
                    ModelEID = item.ModelEID,
                    ModelScales = item.ModelScales,
                    ScaleFactor = item.ScaleFactor,
                    ScaleMod = item.ScaleMod,
                    CompressionMethod = item.CompressionMethod
                });
            }
        }

        private void CreateLists()
        {
            modelItems = [];
            for (int i = 0; i < dgvBatch.Rows.Count; i++)
            {
                string modelEID = dgvBatch.Rows[i].Cells[1].Value.ToString();
                if (!modelItems.ContainsKey(modelEID))
                {
                    var value = modelSettings.OldModelItems.FirstOrDefault(m => m.ModelEID == modelEID);
                    if (value != null)
                    {
                        modelItems.Add(modelEID, value);
                        //Console.WriteLine($"Reused model item for EID: {modelEID}");
                    }
                    else
                    {
                        modelItems.Add(modelEID, new ModelItem()
                        {
                            ModelEID = modelEID,
                            ModelScales = [(int)BaseScales.ModelScale, (int)BaseScales.ModelScale, (int)BaseScales.ModelScale],
                            ScaleFactor = [BaseScales.ModelScaleFactor, BaseScales.ModelScaleFactor, BaseScales.ModelScaleFactor],
                            ScaleMod = 1.0f,
                            CompressionMethod = -1
                        });
                        //Console.WriteLine($"Added model item for EID: {modelEID}");
                    }
                }
            }
        }

        private void LoadSettings(bool updateSettings)
        {
            if (updateSettings)
                lblPath.Text = settingsPath;
            lblExportPath.Text = modelSettings.ExportPath;
            lblModelPath.Text = modelSettings.ModelPath;
            modelPath = lblModelPath.Text;

            txtBaseTpage.Text = modelSettings.BaseTPageName;

            var jsons = LoadModelJson(modelPath);

            CreateRows(jsons);
            CreateLists();

            numMaxStripIterations.Value = modelSettings.MaxIterations;
            numAvgKeysWeight.Value = (decimal)modelSettings.AvgKeysPenalty;
            numMaxLiveKeysWeight.Value = (decimal)modelSettings.MaxKeysPenalty;
            numStripCountWeight.Value = (decimal)modelSettings.StripCountPenalty;

            chkSkipOddFrames.Checked = modelSettings.SkipOddFrames;

            exporterVersion = jsons[0].version;
            lblVersion.Text = $"Exporter: v{exporterVersion}\r\nConverter: v{Version}";
        }

        private void CreateRows(List<C2Json> jsons)
        {
            dgvBatch.SuspendLayout();
            dgvBatch.Rows.Clear();

            string str;
            int defaultAnimCount = 0, defaultModelCount = 0;
            Dictionary<string, string> usedNames = [];

            for (int i = 0; i < jsons.Count; i++)
            {
                var json = jsons[i];

                string animName = "";
                string modelName = "";

                // try to get anim eid from object name
                str = json.name;
                if (((str.Length >= 6 && str[^6] == '_') || str.Length == 5) && str.EndsWith('V')) 
                    animName = str[^5..];

                // if invalid, use default
                if (Entry.CheckEIDErrors(animName, true) != string.Empty) 
                {
                    animName = GetDefaultEID('V', defaultAnimCount);
                    defaultAnimCount++;
                }

                
                // if collection is null, treat it as a single-object and use anim eid to get model eid
                if (json.collection == null) 
                {
                    var sb = new StringBuilder(animName);
                    sb[4] = 'G';
                    modelName = sb.ToString();
                }
                else
                {
                    // try to get model eid from collection name
                    str = json.collection;
                    if (((str.Length >= 6 && str[^6] == '_') || str.Length == 5) && str.EndsWith('G'))
                        modelName = str[^5..];

                    // if invalid, use default
                    if (Entry.CheckEIDErrors(modelName, true) != string.Empty)
                    {
                        if (usedNames.TryGetValue(str, out string? value))
                        {
                            modelName = value;
                        }
                        else
                        {
                            modelName = GetDefaultEID('G', defaultModelCount);
                            usedNames.Add(str, modelName);
                            defaultModelCount++;
                        }
                    }
                }

                DataGridViewRow row = new();
                row.CreateCells(dgvBatch, json.name, modelName, animName);
                dgvBatch.Rows.Add(row);
            }

            dgvBatch.ClearSelection();
            dgvBatch.CurrentCell = null;
            dgvBatch.ResumeLayout();

            fraModel.Enabled = false;
        }

        private void dgvBatch_SelectionChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            if (dgvBatch.SelectedCells.Count > 0)
            {
                dirty.Push(true);

                currentIndex = dgvBatch.SelectedCells[0].RowIndex;
                string modelEID = dgvBatch.Rows[currentIndex].Cells[1].Value.ToString();
                lblModel.Text = $"{modelEID}";
                lblObject.Text = $"(index {currentIndex}) - {dgvBatch.Rows[currentIndex].Cells[0].Value}";

                if (modelItems.Count > 0)
                {
                    ModelItem? item = modelItems.TryGetValue(modelEID, out ModelItem? value) ? value : null;

                    if (item != null)
                    {
                        numScaleX.Value = item.ModelScales[0];
                        numScaleY.Value = item.ModelScales[1];
                        numScaleZ.Value = item.ModelScales[2];
                        numScaleFX.Value = (decimal)item.ScaleFactor[0];
                        numScaleFY.Value = (decimal)item.ScaleFactor[1];
                        numScaleFZ.Value = (decimal)item.ScaleFactor[2];
                        numScaleMod.Value = (decimal)item.ScaleMod;

                        int method = item.CompressionMethod;
                        if (method >= 0)
                        {
                            chkCompressModel.Checked = true;

                            switch (method)
                            {
                                case 2:
                                    radioButton3.Checked = true;
                                    break;
                                case 1:
                                    radioButton2.Checked = true;
                                    break;
                                default:
                                    radioButton1.Checked = true;
                                    break;
                            }
                        }
                        else
                        {
                            chkCompressModel.Checked = false;
                        }

                        fraModel.Enabled = true;
                        UpdateScaleRatioState();
                    }
                    else
                    {
                        numScaleX.Value = (decimal)BaseScales.ModelScale;
                        numScaleY.Value = (decimal)BaseScales.ModelScale;
                        numScaleZ.Value = (decimal)BaseScales.ModelScale;
                        numScaleFX.Value = (decimal)BaseScales.ModelScaleFactor;
                        numScaleFY.Value = (decimal)BaseScales.ModelScaleFactor;
                        numScaleFZ.Value = (decimal)BaseScales.ModelScaleFactor;
                        numScaleMod.Value = 1.0M;
                        chkCompressModel.Checked = false;
                    }
                }

                dirty.Pop();
            }
        }

        private void cmdSaveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void cmdConvert_Click(object sender, EventArgs e)
        {
            if (chkAutoSave.Checked)
                SaveSettings();
            ConvertModel(modelPath, modelSettings, debug);
        }

        private void SaveSettings()
        {
            modelSettings.ConverterVersion = exporterVersion;
            modelSettings.ExporterVersion = Version;
            modelSettings.ExportPath = lblExportPath.Text;
            modelSettings.ModelPath = lblModelPath.Text;

            modelSettings.BaseTPageName = txtBaseTpage.Text;

            modelSettings.ModelObjects = [];
            for (int i = 0; i < dgvBatch.Rows.Count; i++)
            {
                modelSettings.ModelObjects.Add(new ModelObject()
                {
                    Name = dgvBatch.Rows[i].Cells[0].Value.ToString(),
                    ModelEID = dgvBatch.Rows[i].Cells[1].Value.ToString(),
                    AnimEID = dgvBatch.Rows[i].Cells[2].Value.ToString()
                });
            }

            modelSettings.ModelItems = [];
            foreach (var kvp in modelItems)
            {
                var item = kvp.Value;
                modelSettings.ModelItems.Add(new ModelItem()
                {
                    ModelEID = item.ModelEID,
                    ModelScales = item.ModelScales,
                    ScaleFactor = item.ScaleFactor,
                    ScaleMod = item.ScaleMod,
                    CompressionMethod = item.CompressionMethod
                });
            }

            modelSettings.MaxIterations = (int)numMaxStripIterations.Value;
            modelSettings.MaxKeysPenalty = (double)numMaxLiveKeysWeight.Value;
            modelSettings.AvgKeysPenalty = (double)numAvgKeysWeight.Value;
            modelSettings.StripCountPenalty = (double)numStripCountWeight.Value;
            modelSettings.SkipOddFrames = chkSkipOddFrames.Checked;

            modelSettings.OldModelItems ??= [];
            foreach (var kvp in modelItems)
            {
                var item = kvp.Value;
                if (!modelSettings.OldModelItems.Any(m => m.ModelEID == item.ModelEID))
                {
                    modelSettings.OldModelItems.Add(new ModelItem()
                    {
                        ModelEID = item.ModelEID,
                        ModelScales = item.ModelScales,
                        ScaleFactor = item.ScaleFactor,
                        ScaleMod = item.ScaleMod,
                        CompressionMethod = item.CompressionMethod
                    });
                }
            }

            ModelSettingsIO.Save(settingsPath, modelSettings);
            Console.WriteLine("Settings saved.");
        }

        private void EID_Validating(object sender, CancelEventArgs e)
        {
            TextBox txtBox = sender as TextBox ?? throw new InvalidOperationException("Sender is not a TextBox");
            string error = Entry.CheckEIDErrors(txtBox.Text, true);
            if (error != string.Empty)
            {
                DarkMessageBox.ShowError(error, "EID Error");
                e.Cancel = true;
            }
        }

        private void BaseEID_Validating(object sender, CancelEventArgs e)
        {
            TextBox txtBox = sender as TextBox ?? throw new InvalidOperationException("Sender is not a TextBox");
            string error = Entry.CheckEIDErrors(txtBox.Text, true);
            string s = txtBox.Text;
            if (error != string.Empty)
            {
                DarkMessageBox.ShowError(error, "EID Error");
                e.Cancel = true;
                return;
            }
            if (!s.Contains('_') || !(s.IndexOf('_') == s.LastIndexOf('_') && s.Contains('_')))
            {
                DarkMessageBox.ShowError("EID must contain only one '_' charater.", "EID Error");
                e.Cancel = true;
                return;
            }
        }

        private void cmdSetModelPath_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new();
            ofd.Filter = FileFilters.JSON;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string path = ofd.FileName;
                    List<C2Json> jsons = LoadModelJson(path);
                    TryOpenModelFiles(ofd.FileName, false);

                    lblModelPath.Text = path;
                    modelPath = lblModelPath.Text;

                    settingsPath = lblPath.Text;
                }
                catch (Exception ex)
                {
                    DarkMessageBox.ShowError($"The selected file is not a valid model JSON file.\n\nDetails: {ex.Message}", "Invalid File");
                    return;
                }
            }
        }

        private void cmdSetExportPath_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog fbd = new();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                lblExportPath.Text = fbd.SelectedPath;
            }
        }

        private void ScrollHandlerFunction1(object sender, MouseEventArgs e)
        {
            if (sender is NumericUpDown numericUpDown)
            {
                HandledMouseEventArgs handledArgs = e as HandledMouseEventArgs;
                if (handledArgs != null) handledArgs.Handled = true;

                decimal newValue = numericUpDown.Value;
                if (e.Delta > 0 && newValue + 4 < numericUpDown.Maximum)
                    newValue += 4;

                else if (e.Delta < 0 && newValue - 4 >= numericUpDown.Minimum)
                    newValue -= 4;

                numericUpDown.Value = newValue;
            }
        }

        private void ScrollHandlerFunction2(object sender, MouseEventArgs e)
        {
            if (sender is NumericUpDown numericUpDown)
            {
                HandledMouseEventArgs handledArgs = e as HandledMouseEventArgs;
                if (handledArgs != null) handledArgs.Handled = true;

                decimal newValue = numericUpDown.Value;
                if (e.Delta > 0 && newValue + 0x40 < numericUpDown.Maximum)
                    newValue += 0x40;

                else if (e.Delta < 0 && newValue - 0x40 >= numericUpDown.Minimum)
                    newValue -= 0x40;

                numericUpDown.Value = newValue;
            }
        }

        private void ScrollHandlerFunction3(object sender, MouseEventArgs e)
        {
            if (sender is NumericUpDown numericUpDown)
            {
                HandledMouseEventArgs handledArgs = e as HandledMouseEventArgs;
                if (handledArgs != null) handledArgs.Handled = true;

                decimal newValue = numericUpDown.Value;
                if (e.Delta > 0 && newValue + (decimal)0.1 < numericUpDown.Maximum)
                    newValue += (decimal)0.1;

                else if (e.Delta < 0 && newValue - (decimal)0.1 >= numericUpDown.Minimum)
                    newValue -= (decimal)0.1;

                numericUpDown.Value = newValue;
            }
        }


        private void dgvBatch_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!(dgvBatch.SelectedCells.Count > 0)) return;
            if (dgvBatch.SelectedCells[0].ColumnIndex == 0) e.Cancel = true;
        }

        private void chkTestCompression_CheckedChanged(object sender, EventArgs e)
        {
            debug.TestCompression = chkTestCompression.Checked;
        }

        private float GetModelProduct()
        {
            return BaseScales.ModelScaleFactor * BaseScales.ModelScale * (float)numScaleMod.Value;
        }

        private void UpdateScaleRatioState()
        {
            const float Tolerance = 0.01f;

            float baseProduct = GetModelProduct();
            float scalex = (float)numScaleFX.Value * (float)numScaleX.Value / baseProduct;
            float diffx = Math.Abs(scalex - 1f);
            lblRatioX.Text = $"[X] Scale:{scalex:F4}  diff:{diffx:F4}";
            lblRatioX.ForeColor = diffx <= Tolerance ? Color.SpringGreen : Color.Crimson;

            float scaley = (float)numScaleFY.Value * (float)numScaleY.Value / baseProduct;
            float diffy = Math.Abs(scaley - 1f);
            lblRatioY.Text = $"[Y] Scale:{scaley:F4}  diff:{diffy:F4}";
            lblRatioY.ForeColor = diffy <= Tolerance ? Color.SpringGreen : Color.Crimson;

            float scalez = (float)numScaleFZ.Value * (float)numScaleZ.Value / baseProduct;
            float diffz = Math.Abs(scalez - 1f);
            lblRatioZ.Text = $"[Z] Scale:{scalez:F4}  diff:{diffz:F4}";
            lblRatioZ.ForeColor = diffz <= Tolerance ? Color.SpringGreen : Color.Crimson;
        }

        private List<string> GetTargetModelEIDs()
        {
            if (!chkBatchProcess.Checked)
                return [lblModel.Text];

            var selectedEIDs = new HashSet<string>();

            if (dgvBatch.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in dgvBatch.SelectedCells)
                {
                    string modelEID = dgvBatch.Rows[cell.RowIndex].Cells[1].Value.ToString();
                    selectedEIDs.Add(modelEID);
                }
            }

            return [.. selectedEIDs];
        }

        private void numScaleFactor_ValueChanged(object sender, EventArgs e)
        {
            DarkNumericUpDown num = sender as DarkNumericUpDown ?? throw new InvalidOperationException("Sender is not a DarkNumericUpDown");
            if (Dirty) return;
            dirty.Push(true);

            float baseProduct = GetModelProduct();

            List<string> targetEIDs = GetTargetModelEIDs();
            foreach (var eid in targetEIDs)
            {
                ModelItem? item = modelItems.TryGetValue(eid, out ModelItem? value) ? value : null;
                if (item == null) continue;

                if (chkLinkScaleFactor.Checked)
                {
                    if (eid == lblModel.Text) // only update UI for the currently selected model
                    {
                        numScaleFX.Value = num.Value;
                        numScaleFY.Value = num.Value;
                        numScaleFZ.Value = num.Value;
                    }
                    item.ScaleFactor = [(float)num.Value, (float)num.Value, (float)num.Value];

                    if (chkAutoScale.Checked)
                    {
                        float roundedX = (float)Math.Round(baseProduct / (float)num.Value);
                        float roundedY = (float)Math.Round(baseProduct / (float)num.Value);
                        float roundedZ = (float)Math.Round(baseProduct / (float)num.Value);

                        if (eid == lblModel.Text)
                        {
                            numScaleX.Value = (decimal)roundedX;
                            numScaleY.Value = (decimal)roundedY;
                            numScaleZ.Value = (decimal)roundedZ;
                        }
                        item.ModelScales = [(int)roundedX, (int)roundedY, (int)roundedZ];
                    }
                }
                else
                {
                    if (num == numScaleFX)
                    {
                        item.ScaleFactor[0] = (float)numScaleFX.Value;
                        if (chkAutoScale.Checked)
                        {
                            float rounded = (float)Math.Round(baseProduct / (float)numScaleFX.Value);
                            if (eid == lblModel.Text)
                                numScaleX.Value = (decimal)rounded;
                            item.ModelScales[0] = (int)rounded;
                        }
                    }
                    else if (num == numScaleFY)
                    {
                        item.ScaleFactor[1] = (float)numScaleFY.Value;
                        if (chkAutoScale.Checked)
                        {
                            float rounded = (float)Math.Round(baseProduct / (float)numScaleFY.Value);
                            if (eid == lblModel.Text)
                                numScaleY.Value = (decimal)rounded;
                            item.ModelScales[1] = (int)rounded;
                        }
                    }
                    else if (num == numScaleFZ)
                    {
                        item.ScaleFactor[2] = (float)numScaleFZ.Value;
                        if (chkAutoScale.Checked)
                        {
                            float rounded = (float)Math.Round(baseProduct / (float)numScaleFZ.Value);
                            if (eid == lblModel.Text)
                                numScaleZ.Value = (decimal)rounded;
                            item.ModelScales[2] = (int)rounded;
                        }
                    }
                }
            }

            UpdateScaleRatioState();
            dirty.Pop();
        }

        private void numModelScale_ValueChanged(object sender, EventArgs e)
        {
            DarkNumericUpDown num = sender as DarkNumericUpDown ?? throw new InvalidOperationException("Sender is not a DarkNumericUpDown");
            if (Dirty) return;
            dirty.Push(true);

            float baseProduct = GetModelProduct();

            List<string> targetEIDs = GetTargetModelEIDs();
            foreach (var eid in targetEIDs)
            {
                ModelItem? item = modelItems.TryGetValue(eid, out ModelItem? value) ? value : null;
                if (item == null) continue;

                if (chkLinkModelScale.Checked)
                {
                    if (eid == lblModel.Text) // only update UI for the currently selected model
                    {
                        numScaleX.Value = num.Value;
                        numScaleY.Value = num.Value;
                        numScaleZ.Value = num.Value;
                    }
                    item.ModelScales = [(int)num.Value, (int)num.Value, (int)num.Value];

                    if (chkAutoScale.Checked)
                    {
                        float roundedX = (float)Math.Round(baseProduct / (float)num.Value, 2);
                        float roundedY = (float)Math.Round(baseProduct / (float)num.Value, 2);
                        float roundedZ = (float)Math.Round(baseProduct / (float)num.Value, 2);

                        if (eid == lblModel.Text)
                        {
                            numScaleFX.Value = (decimal)roundedX;
                            numScaleFY.Value = (decimal)roundedY;
                            numScaleFZ.Value = (decimal)roundedZ;
                        }
                        item.ScaleFactor = [roundedX, roundedY, roundedZ];
                    }
                }
                else
                {
                    if (num == numScaleX)
                    {
                        item.ModelScales[0] = (int)numScaleX.Value;
                        if (chkAutoScale.Checked)
                        {
                            float rounded = (float)Math.Round(baseProduct / (float)numScaleX.Value, 2);
                            if (eid == lblModel.Text)
                                numScaleFX.Value = (decimal)rounded;
                            item.ScaleFactor[0] = rounded;
                        }
                    }
                    else if (num == numScaleY)
                    {
                        item.ModelScales[1] = (int)numScaleY.Value;
                        if (chkAutoScale.Checked)
                        {
                            float rounded = (float)Math.Round(baseProduct / (float)numScaleY.Value, 2);
                            if (eid == lblModel.Text)
                                numScaleFY.Value = (decimal)rounded;
                            item.ScaleFactor[1] = rounded;
                        }
                    }
                    else if (num == numScaleZ)
                    {
                        item.ModelScales[2] = (int)numScaleZ.Value;
                        if (chkAutoScale.Checked)
                        {
                            float rounded = (float)Math.Round(baseProduct / (float)numScaleZ.Value, 2);
                            if (eid == lblModel.Text)
                                numScaleFZ.Value = (decimal)rounded;
                            item.ScaleFactor[2] = rounded;
                        }
                    }
                }
            }

            UpdateScaleRatioState();
            dirty.Pop();
        }

        private void numScaleMod_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            dirty.Push(true);

            List<string> targetEIDs = GetTargetModelEIDs();
            foreach (var eid in targetEIDs)
            {
                ModelItem? item = modelItems.TryGetValue(eid, out ModelItem? value) ? value : null;
                if (item != null)
                    item.ScaleMod = (float)numScaleMod.Value;
            }

            UpdateScaleRatioState();
            dirty.Pop();
        }

        private void chkCompressModel_CheckedChanged(object sender, EventArgs e)
        {
            pnCompressModel.Enabled = chkCompressModel.Checked;

            if (Dirty) return;
            dirty.Push(true);

            List<string> targetEIDs = GetTargetModelEIDs();
            foreach (var eid in targetEIDs)
            {
                if (modelItems.TryGetValue(eid, out ModelItem? item))
                {
                    if (!chkCompressModel.Checked)
                    {
                        item.CompressionMethod = -1;
                    }
                    else
                    {
                        int method = 0;
                        if (radioButton3.Checked)
                            method = 2;
                        else if (radioButton2.Checked)
                            method = 1;
                        else if (radioButton1.Checked)
                            method = 0;

                        item.CompressionMethod = method;
                    }
                }
            }

            dirty.Pop();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton ?? throw new InvalidOperationException("Sender is not a RadioButton");
            if (rb != null && rb.Checked && rb.Tag != null)
            {
                if (int.TryParse(rb.Tag.ToString(), out int v))
                {
                    if (Dirty) return;
                    dirty.Push(true);

                    List<string> targetEIDs = GetTargetModelEIDs();
                    foreach (var eid in targetEIDs)
                    {
                        if (modelItems.TryGetValue(eid, out ModelItem? item))
                        {
                            if (chkCompressModel.Checked)
                                item.CompressionMethod = v;
                        }
                    }

                    dirty.Pop();
                }
            }
        }

        public static string GetDefaultEID(char c, int i)
        {
            string base62 = ToBase62(i + 1, 4);
            return base62 + c;
        }

        public static char Convert62(int n)
        {
            if (n < 0 || n >= 62)
                throw new ArgumentOutOfRangeException(nameof(n));

            if (n < 10)          // 0–9
                return (char)('0' + n);

            if (n < 36)          // a–z
                return (char)('a' + (n - 10));

            return (char)('A' + (n - 36)); // A–Z
        }

        private static string ToBase62(int n, int width)
        {
            if (n == 0)
                return new string('0', width);

            var chars = new char[width];
            Array.Fill(chars, '0');

            int pos = width - 1;
            int c = 36; // 0-9, a-z
            while (n > 0 && pos >= 0)
            {
                chars[pos] = Convert62(n % c);
                n /= c;
                pos--;
            }

            return new string(chars);
        }

        private void ModelConverterForm_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 1)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            if (string.Equals(Path.GetExtension(files[0]), ".json", StringComparison.OrdinalIgnoreCase))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void ModelConverterForm_DragDrop(object sender, DragEventArgs e)
        {
            string file = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            TryOpenSettingsFile(file);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
                watcher = null;
            }
        }

        private void chkDebugTextures_CheckedChanged(object sender, EventArgs e)
        {
            debug.DebugTextures = chkDebugTextures.Checked;
        }

        private void chkDebugModels_CheckedChanged(object sender, EventArgs e)
        {
            debug.DebugModels = chkDebugModels.Checked;
        }

        private void chkDebugMaterials_CheckedChanged(object sender, EventArgs e)
        {
            debug.DebugMaterials = chkDebugMaterials.Checked;
        }

        
    }

    public static class ModelSettingsIO
    {
        private static readonly JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        public static void Save(string settingsPath, ModelSettings settings)
        {
            string json = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(settingsPath, json);
        }

        public static ModelSettings Load(string path)
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<ModelSettings>(json)!;
        }
    }

    public class Debug
    {
        public bool DebugTextures { get; set; }
        public bool DebugMaterials { get; set; }
        public bool DebugModels { get; set; }
        public bool DebugMode { get; set; }
        public bool TestCompression { get; set; }
    }

    public class ModelObject
    {
        public string Name { get; set; }
        public string ModelEID { get; set; }
        public string AnimEID { get; set; }
    }

    public class ModelItem
    {
        public string ModelEID { get; set; }
        public int[] ModelScales { get; set; }
        public float[] ScaleFactor { get; set; }
        public float ScaleMod { get; set; }
        public int CompressionMethod { get; set; }
    }

    public class ModelSettings
    {
        public string ConverterVersion { get; set; }
        public string ExporterVersion { get; set; }
        public string ModelPath { get; set; }
        public string ExportPath { get; set; }

        public string BaseTPageName { get; set; }

        public bool SkipOddFrames { get; set; }

        public int MaxIterations { get; set; }
        public double MaxKeysPenalty { get; set; }
        public double AvgKeysPenalty { get; set; }
        public double StripCountPenalty { get; set; }

        public List<ModelObject> ModelObjects { get; set; }
        public List<ModelItem> ModelItems { get; set; }

        public List<ModelItem> OldModelItems { get; set; }
    }

    public class C2Triangle
    {
        public int[] v { get; set; }
        public float[] normal { get; set; }
        public float[][] uv { get; set; }
        public int[] c { get; set; }
        public int material { get; set; } // TextureIndex
    }

    public class C2Material
    {
        public string name { get; set; }
        public string texture { get; set; }
    }

    public class C2Collision
    {
        public float[] min { get; set; }
        public float[] max { get; set; }
    }

    public class C2Marker
    {
        public string name { get; set; }
        public float[] pos { get; set; }
    }

    public class C2Json
    {
        public string version { get; set; }
        public string? collection { get; set; }
        public string name { get; set; }
        public List<float[]> vertices { get; set; }
        public List<C2Triangle> triangles { get; set; }
        public List<List<float[]>> frames { get; set; }
        public List<int[]> colors { get; set; }
        public List<C2Material> materials { get; set; }
        public List<List<C2Collision>> collisions { get; set; }
        public List<List<C2Marker>> markers { get; set; }
        public List<List<C2Marker>> groups { get; set; }
    }
}