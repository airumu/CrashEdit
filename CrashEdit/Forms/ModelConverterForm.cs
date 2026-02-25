using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.Crash;
using CrashEdit.Crash.GOOLIns;
using NAudio.Gui;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Media.Media3D;
using static CrashEdit.CE.BlenderModelConverter;
using static CrashEdit.CE.ModelConverterForm;
using static CrashEdit.CE.TextureAtlasPacker;
using static CrashEdit.CE.TriangleStripBuilder;

namespace CrashEdit.CE
{
    public partial class ModelConverterForm : DarkForm
    {
        private static readonly string Version = "1.0.0";

        public const float BASE_SCALE_FACTOR = 255.0f;
        public const float BASE_MODEL_SCALE = 0x646;
        public const float BASE_PRODUCT = BASE_SCALE_FACTOR * BASE_MODEL_SCALE;
        public const float TOLERANCE = 0.01f;

        public const float BASE_COLLISION_SCALE = 50944.0f;
        public const float BASE_FRAME_SCALE = 127.0f;
        public const float BASE_COLLISION_PRODUCT = BASE_FRAME_SCALE * BASE_MODEL_SCALE;

        private readonly Debug debug = new()
        {
            DebugMode = false,
            TestCompression = false
        };

        private Dictionary<string, ModelItem> modelItems = [];
        private List<ModelItem> oldModelItems = [];

        private int currentIndex = 0;
        private int compressionMethod = 0;

        private ModelSettings modelSettings;
        private string modelPath;
        private string settingsPath;
        private string exporterVersion;

        private FileSystemWatcher? watcher;
        private readonly System.Windows.Forms.Timer reloadTimer;

        private readonly DarkToolTip toolTip1 = new();
        private readonly DarkToolTip toolTip2 = new();
        private readonly DarkToolTip toolTip3 = new();
        private readonly DarkToolTip toolTip4 = new();
        private readonly DarkToolTip toolTip5 = new();
        private readonly DarkToolTip toolTip6 = new();
        private readonly DarkToolTip toolTip7 = new();
        private readonly DarkToolTip toolTip8 = new();

        internal Stack<bool> dirty = new();
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        public ModelConverterForm()
        {
            InitializeComponent();
            Icon = Embeds.GetIcon("Plugin");

            DgvBatchInit();
            lblPath.Text = "";
            lblExportPath.Text = "";
            lblModel.Text = "";
            lblVersion.Text = $"\r\nConverter: v{Version}";

            toolTip1.SetToolTip(lblStripIterations, "Number of iterations to generate triangle strips.");
            toolTip2.SetToolTip(lblMaxKeyWeight, "Penalty weight for longer-living position keys.");
            toolTip3.SetToolTip(chkCompressModel, "Enables the model compression and sets the method.");
            toolTip4.SetToolTip(chkSkipOddFrames, "Skips output for every odd frame.\r\nUseful when frame interpolation is enabled in GOOL.");
            toolTip5.SetToolTip(cmdOpen, "You can also drag and drop a file onto this form.");
            toolTip6.SetToolTip(lblScaleMod, "Use this only if the model scale in Blender is incorrect.");
            toolTip7.SetToolTip(chkAutoSave, "Saves the settings file automatically before conversion.");
            toolTip8.SetToolTip(chkTestCompression, "Tries all compression methods to find the most efficient one.");

            cmdSetExportPath.Image = new Bitmap(Embeds.Bitmaps["FolderOpen"], new Size(16, 16));

            numScaleX.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);
            numScaleY.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);
            numScaleZ.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);
            numScaleFX.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);
            numScaleFY.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);
            numScaleFZ.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);

            numMaxLiveKeysWeight.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);
            numMaxStripIterations.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);

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
                openFile(ofd.FileName);
        }

        private void openFile(string path)
        {
            string saveDirectory = Path.GetDirectoryName(path)!;
            string fileName = Path.GetFileNameWithoutExtension(path);

            Console.WriteLine();
            Console.WriteLine("Selected file: " + path);
            // try load settings file
            try
            {
                modelSettings = ModelSettingsIO.Load(path);
                settingsPath = path;
                LoadSettings();
                Console.WriteLine("Loaded settings.");
            }
            // if invalid settings file, try load as model json file
            catch (Exception)
            {
                try
                {
                    Console.WriteLine("Could not load settings, trying to load model JSON file...");
                    List<Crash2Json> jsons = LoadModelJson(path);

                    settingsPath = Path.Combine(saveDirectory, $"{fileName}_settings.json");

                    // load existing settings
                    if (File.Exists(settingsPath))
                    {
                        modelSettings = ModelSettingsIO.Load(settingsPath);
                        LoadSettings();
                        Console.WriteLine("Found and loaded existing settings.");
                    }
                    else
                    {
                        // use default settings
                        dirty.Push(true);
                        numScaleX.Value = (decimal)BASE_MODEL_SCALE;
                        numScaleY.Value = (decimal)BASE_MODEL_SCALE;
                        numScaleZ.Value = (decimal)BASE_MODEL_SCALE;
                        numScaleFX.Value = (decimal)BASE_SCALE_FACTOR;
                        numScaleFY.Value = (decimal)BASE_SCALE_FACTOR;
                        numScaleFZ.Value = (decimal)BASE_SCALE_FACTOR;
                        numScaleMod.Value = 1.0M;
                        chkSkipOddFrames.Checked = false;
                        numMaxStripIterations.Value = 64.0M;
                        numMaxLiveKeysWeight.Value = 1000.0M;
                        numAvgKeysWeight.Value = 100.0M;
                        numStripCountWeight.Value = 10.0M;
                        chkCompressModel.Checked = false;
                        radioButton1.Checked = true;

                        modelSettings = new();

                        modelPath = path;
                        lblExportPath.Text = saveDirectory;

                        CreateRows(jsons);
                        CreateModelObjects();

                        SaveSettings();
                        LoadSettings();

                        dirty.Pop();

                        Console.WriteLine("No existing settings found, created new default settings.");
                    }
                }
                catch (Exception ex)
                {
                    DarkMessageBox.ShowError($"The selected file is not a valid model JSON file.\n\nDetails: {ex.Message}", "Invalid File");
                    return;
                }
            }

            pnBottom.Enabled =
            fraSettings.Enabled = true;

            StartWatching(modelPath);
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
                        ModelScales = [(int)BASE_MODEL_SCALE, (int)BASE_MODEL_SCALE, (int)BASE_MODEL_SCALE],
                        ScaleFactor = [BASE_SCALE_FACTOR, BASE_SCALE_FACTOR, BASE_SCALE_FACTOR],
                        ScaleMod = 1.0f
                    });
                    Console.WriteLine($"Added model item for EID: {modelEID}");
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
                    ScaleMod = item.ScaleMod
                });
            }
        }

        private void CreateLists()
        {
            //var oldModelItems = new Dictionary<string, ModelItem>();
            //foreach (ModelItem model in modelSettings.ModelItems)
            //    oldModelItems.Add(model.ModelEID, model);

            oldModelItems = [];
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
                            ModelScales = [(int)BASE_MODEL_SCALE, (int)BASE_MODEL_SCALE, (int)BASE_MODEL_SCALE],
                            ScaleFactor = [BASE_SCALE_FACTOR, BASE_SCALE_FACTOR, BASE_SCALE_FACTOR],
                            ScaleMod = 1.0f
                        });
                        //Console.WriteLine($"Added model item for EID: {modelEID}");
                    }
                }
            }
        }

        private void LoadSettings()
        {
            lblPath.Text = settingsPath;
            lblExportPath.Text = modelSettings.ExportPath;
            modelPath = modelSettings.ModelPath;

            var jsons = LoadModelJson(modelPath);

            CreateRows(jsons);
            CreateLists();

            if (modelSettings.CompressionMethod >= 0)
            {
                chkCompressModel.Checked = true;
                int method = modelSettings.CompressionMethod;

                if (method == 0) radioButton1.Checked = true;
                else if (method == 1) radioButton2.Checked = true;
                else if (method == 2) radioButton3.Checked = true;
            }
            else
            {
                chkCompressModel.Checked = false;
            }

            numMaxStripIterations.Value = modelSettings.MaxIterations;
            numAvgKeysWeight.Value = (decimal)modelSettings.AvgKeysPenalty;
            numMaxLiveKeysWeight.Value = (decimal)modelSettings.MaxKeysPenalty;
            numStripCountWeight.Value = (decimal)modelSettings.StripCountPenalty;

            chkSkipOddFrames.Checked = modelSettings.SkipOddFrames;

            exporterVersion = jsons[0].version;
            lblVersion.Text = $"Exporter: v{exporterVersion}\r\nConverter: v{Version}";
        }

        private void CreateRows(List<Crash2Json> jsons)
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
                str = json.name;
                if (((str.Length >= 6 && str[^6] == '_') || str.Length == 5) && str.EndsWith('V')) // try to get eid from object name
                    animName = str[^5..];
                if (Entry.CheckEIDErrors(animName, true) != string.Empty) // if invalid eid, use default
                {
                    animName = GetDefaultEID('V', defaultAnimCount);
                    defaultAnimCount++;
                }

                string modelName = "";
                if (json.collection == null) // if collection is null, treat it as a single-object and try to get model name from anim name
                {
                    var sb = new StringBuilder(animName);
                    sb[4] = 'G';
                    modelName = sb.ToString();
                }
                else
                {
                    str = json.collection;
                    if (((str.Length >= 6 && str[^6] == '_') || str.Length == 5) && str.EndsWith('G'))
                        modelName = str[^5..];
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
                int index = dgvBatch.SelectedCells[0].RowIndex;
                dirty.Push(true);

                currentIndex = index;
                string modelEID = dgvBatch.Rows[index].Cells[1].Value.ToString();
                lblModel.Text = modelEID;

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

                        fraModel.Enabled = true;
                        UpdateScaleRatioState();
                    }
                    else
                    {
                        numScaleX.Value = (decimal)BASE_MODEL_SCALE;
                        numScaleY.Value = (decimal)BASE_MODEL_SCALE;
                        numScaleZ.Value = (decimal)BASE_MODEL_SCALE;
                        numScaleFX.Value = (decimal)BASE_SCALE_FACTOR;
                        numScaleFY.Value = (decimal)BASE_SCALE_FACTOR;
                        numScaleFZ.Value = (decimal)BASE_SCALE_FACTOR;
                        numScaleMod.Value = 1.0M;
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
            modelSettings.ModelPath = modelPath;
            modelSettings.ExportPath = lblExportPath.Text;

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
                    ScaleMod = item.ScaleMod
                });
            }

            modelSettings.CompressionMethod = chkCompressModel.Checked ? compressionMethod : -1;
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
                        ScaleMod = item.ScaleMod
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
            if (error != string.Empty)
            {
                DarkMessageBox.ShowError(error, "EID Error");
                e.Cancel = true;
            }
            if (!txtBox.Text.Contains('_'))
            {
                DarkMessageBox.ShowError("EID must contain one '_' charater.", "EID Error");
                e.Cancel = true;
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

        private void ScrollHandlerFunction2(object sender, MouseEventArgs e)
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

        private void dgvBatch_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!(dgvBatch.SelectedCells.Count > 0)) return;
            if (dgvBatch.SelectedCells[0].ColumnIndex == 0) e.Cancel = true;
        }

        private void chkDebug_CheckedChanged(object sender, EventArgs e)
        {
            debug.DebugMode = chkDebug.Checked;
        }

        private void chkTestCompression_CheckedChanged(object sender, EventArgs e)
        {
            debug.TestCompression = chkTestCompression.Checked;
        }

        private void UpdateScaleRatioState()
        {
            float baseProduct = BASE_PRODUCT * (float)numScaleMod.Value;
            float scalex = (float)numScaleFX.Value * (float)numScaleX.Value / baseProduct;
            float diffx = Math.Abs(scalex - 1f);
            lblRatioX.Text = $"Scale:{scalex:F4}  diff:{diffx:F4}";
            lblRatioX.ForeColor = diffx <= TOLERANCE ? Color.SpringGreen : Color.Crimson;

            float scaley = (float)numScaleFY.Value * (float)numScaleY.Value / baseProduct;
            float diffy = Math.Abs(scaley - 1f);
            lblRatioY.Text = $"Scale:{scaley:F4}  diff:{diffy:F4}";
            lblRatioY.ForeColor = diffy <= TOLERANCE ? Color.SpringGreen : Color.Crimson;

            float scalez = (float)numScaleFZ.Value * (float)numScaleZ.Value / baseProduct;
            float diffz = Math.Abs(scalez - 1f);
            lblRatioZ.Text = $"Scale:{scalez:F4}  diff:{diffz:F4}";
            lblRatioZ.ForeColor = diffz <= TOLERANCE ? Color.SpringGreen : Color.Crimson;
        }

        private void numScaleFactor_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            DarkNumericUpDown num = sender as DarkNumericUpDown ?? throw new InvalidOperationException("Sender is not a DarkNumericUpDown");

            dirty.Push(true);
            float baseProduct = BASE_PRODUCT * (float)numScaleMod.Value;
            ModelItem? item = modelItems.TryGetValue(lblModel.Text, out ModelItem? value) ? value : null;

            if (chkLinkScaleFactor.Checked)
            {
                numScaleFX.Value = num.Value;
                numScaleFY.Value = num.Value;
                numScaleFZ.Value = num.Value;
                item.ScaleFactor = [(float)numScaleFX.Value, (float)numScaleFY.Value, (float)numScaleFZ.Value];


                if (chkAutoScale.Checked)
                {
                    numScaleX.Value = (decimal)Math.Round(baseProduct / (float)numScaleFX.Value);
                    numScaleY.Value = (decimal)Math.Round(baseProduct / (float)numScaleFY.Value);
                    numScaleZ.Value = (decimal)Math.Round(baseProduct / (float)numScaleFZ.Value);
                    item.ModelScales = [(int)numScaleX.Value, (int)numScaleY.Value, (int)numScaleZ.Value];
                }
            }
            else
            {
                if (num == numScaleFX)
                {
                    item.ScaleFactor[0] = (float)numScaleFX.Value;

                    if (chkAutoScale.Checked)
                    {
                        numScaleX.Value = (decimal)Math.Round(baseProduct / (float)numScaleFX.Value);
                        item.ModelScales[0] = (int)numScaleX.Value;
                    }
                }
                else if (num == numScaleFY)
                {
                    item.ScaleFactor[1] = (float)numScaleFY.Value;

                    if (chkAutoScale.Checked)
                    {
                        numScaleY.Value = (decimal)Math.Round(baseProduct / (float)numScaleFY.Value);
                        item.ModelScales[1] = (int)numScaleY.Value;
                    }
                }
                else if (num == numScaleFZ)
                {
                    item.ScaleFactor[2] = (float)numScaleFZ.Value;

                    if (chkAutoScale.Checked)
                    {
                        numScaleZ.Value = (decimal)Math.Round(baseProduct / (float)numScaleFZ.Value);
                        item.ModelScales[2] = (int)numScaleZ.Value;
                    }
                }
            }
            UpdateScaleRatioState();
            dirty.Pop();
        }

        private void numModelScale_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            DarkNumericUpDown num = sender as DarkNumericUpDown ?? throw new InvalidOperationException("Sender is not a DarkNumericUpDown");

            dirty.Push(true);
            float baseProduct = BASE_PRODUCT * (float)numScaleMod.Value;
            ModelItem? item = modelItems.TryGetValue(lblModel.Text, out ModelItem? value) ? value : null;

            if (chkLinkModelScale.Checked)
            {
                numScaleX.Value = num.Value;
                numScaleY.Value = num.Value;
                numScaleZ.Value = num.Value;
                item.ModelScales = [(int)numScaleX.Value, (int)numScaleY.Value, (int)numScaleZ.Value];
                if (chkAutoScale.Checked)
                {
                    numScaleFX.Value = (decimal)Math.Round(baseProduct / (float)numScaleX.Value, 2);
                    numScaleFY.Value = (decimal)Math.Round(baseProduct / (float)numScaleY.Value, 2);
                    numScaleFZ.Value = (decimal)Math.Round(baseProduct / (float)numScaleZ.Value, 2);
                    item.ScaleFactor = [(float)numScaleFX.Value, (float)numScaleFY.Value, (float)numScaleFZ.Value];
                }
            }
            else
            {
                if (num == numScaleX)
                {
                    item.ModelScales[0] = (int)numScaleX.Value;

                    if (chkAutoScale.Checked)
                    {
                        numScaleFX.Value = (decimal)Math.Round(baseProduct / (float)numScaleX.Value, 2);
                        item.ScaleFactor[0] = (float)numScaleFX.Value;
                    }
                }
                else if (num == numScaleY)
                {
                    item.ModelScales[1] = (int)numScaleY.Value;

                    if (chkAutoScale.Checked)
                    {
                        numScaleFY.Value = (decimal)Math.Round(baseProduct / (float)numScaleY.Value, 2);
                        item.ScaleFactor[1] = (float)numScaleFY.Value;
                    }
                }
                else if (num == numScaleZ)
                {
                    item.ModelScales[2] = (int)numScaleZ.Value;

                    if (chkAutoScale.Checked)
                    {
                        numScaleFZ.Value = (decimal)Math.Round(baseProduct / (float)numScaleZ.Value, 2);
                        item.ScaleFactor[2] = (float)numScaleFZ.Value;
                    }
                }
            }
            UpdateScaleRatioState();
            dirty.Pop();
        }

        private void numScaleMod_ValueChanged(object sender, EventArgs e)
        {
            ModelItem? item = modelItems.TryGetValue(lblModel.Text, out ModelItem? value) ? value : null;
            if (item != null)
                item.ScaleMod = (float)numScaleMod.Value;
        }

        private void chkCompressModel_CheckedChanged(object sender, EventArgs e)
        {
            pnCompressModel.Enabled = chkCompressModel.Checked;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton ?? throw new InvalidOperationException("Sender is not a RadioButton");
            if (rb != null && rb.Checked && rb.Tag != null)
            {
                if (int.TryParse(rb.Tag.ToString(), out int v))
                {
                    compressionMethod = v;
                }
            }
        }

        public static string GetDefaultEID(char c, int i)
        {
            string pattern = "00_0" + c;
            return pattern.Replace("_", Convert62(i + 1).ToString());
        }

        private static char Convert62(int n)
        {
            if (n < 0 || n >= 62)
                throw new ArgumentOutOfRangeException(nameof(n));

            if (n < 10)          // 0–9
                return (char)('0' + n);

            if (n < 36)          // a–z
                return (char)('a' + (n - 10));

            return (char)('A' + (n - 36)); // A–Z
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
            openFile(file);
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

    public class ListItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }

    public class Debug
    {
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
    }

    public class ModelSettings
    {
        public string ConverterVersion { get; set; }
        public string ExporterVersion { get; set; }
        public string ModelPath { get; set; }
        public string ExportPath { get; set; }

        public bool SkipOddFrames { get; set; }

        public int CompressionMethod { get; set; }
        public int MaxIterations { get; set; }
        public double MaxKeysPenalty { get; set; }
        public double AvgKeysPenalty { get; set; }
        public double StripCountPenalty { get; set; }

        public List<ModelObject> ModelObjects { get; set; }
        public List<ModelItem> ModelItems { get; set; }

        public List<ModelItem> OldModelItems { get; set; }
    }

    public class Crash2Triangle
    {
        public int[] v { get; set; }
        public float[] normal { get; set; }
        public float[][] uv { get; set; }
        public int[] c { get; set; }
        public int material { get; set; } // TextureIndex
    }

    public class Crash2Material
    {
        public string name { get; set; }
        public string texture { get; set; }
    }

    public class Crash2Collision
    {
        public float[] min { get; set; }
        public float[] max { get; set; }
    }

    public class Crash2Marker
    {
        public string name { get; set; }
        public float[] pos { get; set; }
    }

    public class Crash2Json
    {
        public string version { get; set; }
        public string? collection { get; set; }
        public string name { get; set; }
        public List<float[]> vertices { get; set; }
        public List<Crash2Triangle> triangles { get; set; }
        public List<List<float[]>> frames { get; set; }
        public List<int[]> colors { get; set; }
        public List<Crash2Material> materials { get; set; }
        public List<List<Crash2Collision>> collisions { get; set; }
        public List<List<Crash2Marker>> markers { get; set; }
        public List<List<Crash2Marker>> groups { get; set; }
    }

    public class Tri
    {
        public int v0, v1, v2;
        public bool used = false;
        public List<int> adj = []; // adjacent tris
        public int degree;
    }

    public readonly struct TriangleKey(int material, byte[] uv0, byte[] uv1, byte[] uv2)
    {
        public readonly int Material = material;
        public readonly byte U0 = uv0[0], V0 = uv0[1];
        public readonly byte U1 = uv1[0], V1 = uv1[1];
        public readonly byte U2 = uv2[0], V2 = uv2[1];
    }

    public readonly struct StructureInfo(byte textureIndex, byte faceOrientation, bool isCC)
    {
        public readonly byte TextureIndex = textureIndex;
        public readonly byte FaceOrientation = faceOrientation;
        public readonly bool IsCC = isCC;
    }

    public readonly struct PackedTexture(int index, string name, string filePath, int bpp, int clutX, int clutY, int destX, int destY, int w, int h, int tpage, MaterialInfo info)
    {
        public readonly int Index = index;
        public readonly string Name = name;
        public readonly string FilePath = filePath;
        public readonly int Bpp = bpp;
        public readonly int ClutX = clutX, ClutY = clutY;
        public readonly int DestX = destX, DestY = destY;
        public readonly int Width = w;
        public readonly int Height = h;
        public readonly int TPage = tpage;
        public readonly MaterialInfo Info = info;
    }

    public readonly struct MaterialInfo(int face, int blend, int offset, int count, int speed, int delay, int repeat, int repeats)
    {
        public readonly int FaceOrientation = face;
        public readonly int BlendMode = blend;
        public readonly int AnimOffset = offset; // split texture offset
        public readonly int AnimCount = count;
        public readonly int AnimSpeed = speed;
        public readonly int AnimDelay = delay;
        public readonly int AnimRepeat = repeat;
        public readonly int TotalAnimRepeats = repeats;
    }

    public readonly struct ModelMaterial(string name, MaterialInfo info, int aniTexIdx, List<ModelTexture> texture)
    {
        public readonly string Name = name;
        public readonly MaterialInfo Info = info;
        public readonly int AnimatedTextureIndex = aniTexIdx;
        public readonly List<ModelTexture> Texture = texture;
    }

    public static class TriangleStripBuilder
    {
        public static(int, int) Edge(int x, int y)
                   => x < y ? (x, y) : (y, x);

        static int FindBestNextTri(List<Tri> tris, int currentTriIndex, int v0, int v1)
        {
            // find adjacent triangle that shares the edge (v0, v1)
            foreach (var adjIdx in tris[currentTriIndex].adj)
            {
                var t = tris[adjIdx];
                if (t.used) continue;

                int match = 0;
                if (t.v0 == v0 || t.v0 == v1) match++;
                if (t.v1 == v0 || t.v1 == v1) match++;
                if (t.v2 == v0 || t.v2 == v1) match++;

                if (match >= 2)
                    return adjIdx; // found
            }

            return -1;
        }

        private static List<int> BuildStrip(List<Tri> tris, int start)
        {
            tris[start].used = true;
            var t0 = tris[start];

            var strip = new List<int> { t0.v0, t0.v1, t0.v2 };

            Grow(tris, strip, forward: true, start);
            Grow(tris, strip, forward: false, start);

            return strip;
        }

        public static List<List<int>> BuildAllStripsBestOfAttempts(List<Tri> trisTemplate, ModelSettings settings, bool output)
        {
            var bestStrips = new List<List<int>>();
            int bestScore = int.MaxValue;

            BuildAdjacency(trisTemplate, output);
            UnifyWinding(trisTemplate);

            // strategy 1: degree descending
            {
                var tris = CopyTris(trisTemplate);
                var strips = BuildAllStripsWithStrategy(tris, settings, 1);
                int score = EvaluateStrips(strips, settings);
                if (score < bestScore)
                {
                    bestScore = score;
                    bestStrips = strips;
                }
                if (output)
                    Console.WriteLine($"Strategy 1: {strips.Count} strips, score: {score}");
            }

            // strategy 2: degree ascending
            {
                var tris = CopyTris(trisTemplate);
                var strips = BuildAllStripsWithStrategy(tris, settings, 2);
                int score = EvaluateStrips(strips, settings);
                if (score < bestScore)
                {
                    bestScore = score;
                    bestStrips = strips;
                }
                if (output)
                    Console.WriteLine($"Strategy 2: {strips.Count} strips, score: {score}");
            }

            //// strategy 3: random
            //{
            //    var random = new Random(123456);
            //    Console.WriteLine($"Strategy 3: Random attempts (seed: {random.GetHashCode()}); first 10 attempts:");
            //    for (int iteration = 0; iteration < settings.MaxIterations; iteration++)
            //    {
            //        var tris = CopyTris(trisTemplate);
            //        var strips = BuildAllStripsWithStrategy(tris, settings, 3, random);
            //        int score = EvaluateStrips(strips, settings);
            //        if (score < bestScore)
            //        {
            //            bestScore = score;
            //            bestStrips = strips;
            //        }
            //        if (iteration < 10)
            //            Console.WriteLine($"    Attempt {iteration}: {strips.Count} strips, score: {score}");
            //    }
            //}

            if (output)
                Console.WriteLine($"[Strip Generation] Best: {bestStrips.Count} strips with score {bestScore}");
            return bestStrips;
        }

        private static int EvaluateStrips(List<List<int>> strips, ModelSettings settings)
        {
            var currentLive = new HashSet<int>();
            var globalRemaining = new Dictionary<int, int>();

            foreach (var s in strips)
                foreach (var v in s)
                    globalRemaining[v] = globalRemaining.TryGetValue(v, out int cnt) ? cnt + 1 : 1;

            int maxLive = 0;
            int totalLive = 0;
            int sampleCount = 0;

            foreach (var strip in strips)
            {
                foreach (var v in strip)
                {
                    if (!currentLive.Contains(v))
                        currentLive.Add(v);
                    globalRemaining[v]--;
                    if (globalRemaining[v] == 0)
                        currentLive.Remove(v);

                    totalLive += currentLive.Count;
                    sampleCount++;
                    if (currentLive.Count > maxLive)
                        maxLive = currentLive.Count;
                }
            }

            int avgLive = sampleCount > 0 ? totalLive / sampleCount : 0;

            int score = (int)(maxLive * settings.MaxKeysPenalty
                      + avgLive * settings.AvgKeysPenalty
                      + strips.Count * settings.StripCountPenalty);

            return score;
        }

        public static List<List<int>> ReorderStripsGreedy(IList<List<int>> strips, int keyOffset, bool output)
        {
            if (strips == null) return new List<List<int>>();
            var remaining = new List<List<int>>(strips);
            var result = new List<List<int>>(strips.Count);

            var keyMap = new Dictionary<int, int>();
            var freeKeys = new SortedSet<int>();
            int nextKey = keyOffset;
            int currentMaxKey = keyOffset - 1;

            var globalRemaining = new Dictionary<int, int>();
            foreach (var s in strips)
                foreach (var v in s)
                    globalRemaining[v] = globalRemaining.TryGetValue(v, out var cnt) ? cnt + 1 : 1;

            var vertexRemaining = new Dictionary<int, int>(globalRemaining);

            int GetOrCreateKeySimulated(int vertexIndex)
            {
                if (keyMap.TryGetValue(vertexIndex, out int existingKey))
                {
                    vertexRemaining[vertexIndex]--;
                    if (vertexRemaining[vertexIndex] == 0)
                    {
                        keyMap.Remove(vertexIndex);
                        freeKeys.Add(existingKey);
                    }
                    return existingKey;
                }

                int assigned;
                if (freeKeys.Count > 0)
                {
                    assigned = freeKeys.Min;
                    freeKeys.Remove(assigned);
                }
                else
                {
                    while (nextKey == ModelTriangle.NullPtr) nextKey++;
                    assigned = nextKey++;
                }

                keyMap[vertexIndex] = assigned;

                if (assigned > currentMaxKey)
                    currentMaxKey = assigned;

                vertexRemaining[vertexIndex]--;
                if (vertexRemaining[vertexIndex] == 0)
                {
                    keyMap.Remove(vertexIndex);
                    freeKeys.Add(assigned);
                }

                return assigned;
            }

            while (remaining.Count > 0)
            {
                int bestIdx = -1;
                int bestPeakKey = int.MaxValue;
                int bestNew = int.MaxValue;
                int bestReuse = -1;
                int bestScoreLen = -1;

                for (int i = 0; i < remaining.Count; i++)
                {
                    var s = remaining[i];
                    int newVerts = 0;
                    int reusedVerts = 0;
                    var seen = new HashSet<int>();

                    var tempKeyMap = new Dictionary<int, int>(keyMap);
                    var tempFreeKeys = new SortedSet<int>(freeKeys);
                    var tempVertexRemaining = new Dictionary<int, int>(vertexRemaining);
                    int tempNextKey = nextKey;
                    int peakKey = currentMaxKey;

                    foreach (var v in s)
                    {
                        if (seen.Add(v))
                        {
                            if (!keyMap.ContainsKey(v))
                                newVerts++;
                            else
                                reusedVerts++;

                            int assignedKey;
                            if (tempKeyMap.TryGetValue(v, out int existingKey))
                            {
                                assignedKey = existingKey;
                                tempVertexRemaining[v]--;
                                if (tempVertexRemaining[v] == 0)
                                {
                                    tempKeyMap.Remove(v);
                                    tempFreeKeys.Add(existingKey);
                                }
                            }
                            else
                            {
                                if (tempFreeKeys.Count > 0)
                                {
                                    assignedKey = tempFreeKeys.Min;
                                    tempFreeKeys.Remove(assignedKey);
                                }
                                else
                                {
                                    while (tempNextKey == ModelTriangle.NullPtr) tempNextKey++;
                                    assignedKey = tempNextKey++;
                                }

                                tempKeyMap[v] = assignedKey;

                                if (assignedKey > peakKey)
                                    peakKey = assignedKey;

                                tempVertexRemaining[v]--;
                                if (tempVertexRemaining[v] == 0)
                                {
                                    tempKeyMap.Remove(v);
                                    tempFreeKeys.Add(assignedKey);
                                }
                            }
                        }
                    }

                    bool withinLimit = peakKey < ModelTriangle.NullPtr;
                    bool currentBestWithinLimit = bestPeakKey < ModelTriangle.NullPtr;

                    bool isBetter = false;

                    if (withinLimit && !currentBestWithinLimit)
                    {
                        isBetter = true;
                    }
                    else if (withinLimit == currentBestWithinLimit)
                    {
                        if (peakKey < bestPeakKey ||
                            (peakKey == bestPeakKey && newVerts < bestNew) ||
                            (peakKey == bestPeakKey && newVerts == bestNew && reusedVerts > bestReuse) ||
                            (peakKey == bestPeakKey && newVerts == bestNew && reusedVerts == bestReuse && s.Count > bestScoreLen))
                        {
                            isBetter = true;
                        }
                    }

                    if (isBetter)
                    {
                        bestPeakKey = peakKey;
                        bestNew = newVerts;
                        bestReuse = reusedVerts;
                        bestIdx = i;
                        bestScoreLen = s.Count;
                    }
                }

                var pick = remaining[bestIdx];
                remaining.RemoveAt(bestIdx);
                result.Add(pick);

                foreach (var v in pick)
                {
                    GetOrCreateKeySimulated(v);
                }
            }

            //if (output)
            //    Console.WriteLine($"[Reorder] Peak key number: {currentMaxKey} (offset: {keyOffset}, max allowed: {ModelTriangle.NullPtr - 1})");

            return result;
        }

        private static List<Tri> CopyTris(List<Tri> trisTemplate)
        {
            var tris = new List<Tri>(trisTemplate.Count);
            for (int i = 0; i < trisTemplate.Count; i++)
            {
                var t = trisTemplate[i];
                tris.Add(new Tri
                {
                    v0 = t.v0,
                    v1 = t.v1,
                    v2 = t.v2,
                    used = false,
                    adj = new List<int>(t.adj),
                    degree = t.degree
                });
            }
            return tris;
        }

        private static List<List<int>> BuildAllStripsWithStrategy(List<Tri> tris, ModelSettings settings, int strategy, Random random = null)
        {
            var strips = new List<List<int>>();

            List<int> order;
            switch (strategy)
            {
                case 1: // boundary priority, high degree first
                    order = tris
                        .Select((t, i) => new { Index = i, IsBoundary = t.adj.Count < 3, Degree = t.adj.Count })
                        .OrderByDescending(x => x.IsBoundary)
                        .ThenByDescending(x => x.Degree)
                        .Select(x => x.Index)
                        .ToList();
                    break;

                case 2: // boundary priority, low degree first
                    order = tris
                        .Select((t, i) => new { Index = i, IsBoundary = t.adj.Count < 3, Degree = t.adj.Count })
                        .OrderByDescending(x => x.IsBoundary)
                        .ThenBy(x => x.Degree)
                        .Select(x => x.Index)
                        .ToList();
                    break;

                case 3: // smart random - randomize within degree groups
                    var grouped = tris
                        .Select((t, i) => new { Index = i, IsBoundary = t.adj.Count < 3, Degree = t.adj.Count })
                        .GroupBy(x => x.Degree)
                        .OrderByDescending(g => g.Key) // high degree first
                        .ToList();

                    order = [];
                    foreach (var group in grouped)
                    {
                        var shuffled = group.OrderBy(x => random.Next()).Select(x => x.Index).ToList();
                        order.AddRange(shuffled);
                    }
                    break;

                default:
                    order = Enumerable.Range(0, tris.Count).ToList();
                    break;
            }

            foreach (var idx in order)
            {
                if (tris[idx].used) continue;
                var strip = BuildStrip(tris, idx);
                if (strip.Count >= 3)
                {
                    strips.Add(strip);
                }
            }

            // remaining tris (should not happen)
            for (int i = 0; i < tris.Count; i++)
            {
                if (!tris[i].used)
                {
                    var strip = new List<int> { tris[i].v0, tris[i].v1, tris[i].v2 };
                    tris[i].used = true;
                    strips.Add(strip);
                }
            }

            return strips;
        }

        private static void Grow(List<Tri> tris, List<int> strip, bool forward, int currentTriIndex)
        {
            while (true)
            {
                int v0, v1;
                if (forward)
                {
                    v0 = strip[^2];
                    v1 = strip[^1];
                }
                else
                {
                    v0 = strip[1];
                    v1 = strip[0];
                }

                int next = FindBestNextTri(tris, currentTriIndex, v0, v1);
                if (next == -1) break;

                var nt = tris[next];
                var (a, b, c) = OrientToEdge(nt, v0, v1);

                // add vertex c to the strip if edge direction matches,
                // end the strip if direction does not match (do not use degenerate triangle)
                if (forward)
                {

                    if (a == v0 && b == v1)
                        strip.Add(c);
                    else
                        break;
                }
                else
                {
                    if (a == v1 && b == v0)
                        strip.Insert(0, c);
                    else
                        break;
                }

                nt.used = true;
                currentTriIndex = next;
            }
        }

        private static void BuildAdjacency(List<Tri> tris, bool output)
        {
            foreach (var t in tris)
            {
                t.adj.Clear();
            }

            var map = new Dictionary<(int, int), List<int>>();

            for (int i = 0; i < tris.Count; i++)
            {
                var t = tris[i];
                foreach (var e in new[] { Edge(t.v0, t.v1), Edge(t.v1, t.v2), Edge(t.v2, t.v0) })
                {
                    if (!map.TryGetValue(e, out var list))
                        map[e] = list = [];
                    list.Add(i);
                }
            }

            foreach (var kv in map)
            {
                var list = kv.Value;
                if (list.Count == 2)
                {
                    tris[list[0]].adj.Add(list[1]);
                    tris[list[1]].adj.Add(list[0]);
                }
            }

            foreach (var t in tris)
                t.degree = t.adj.Count;

            int sharedEdges = map.Count(kv => kv.Value.Count == 2);
            int isolated = tris.Count(t => t.adj.Count == 0);
            if (output)
                Console.WriteLine($"[Adjacency] Shared edges: {sharedEdges}, Isolated tris: {isolated}");
        }

        private static (int a, int b, int c) OrientToEdge(Tri t, int e0, int e1)
        {
            int[] v = { t.v0, t.v1, t.v2 };
            for (int i = 0; i < 3; i++)
            {
                int x = v[i], y = v[(i + 1) % 3], z = v[(i + 2) % 3];
                if (x == e0 && y == e1) return (x, y, z);
                if (x == e1 && y == e0) return (y, x, z);
            }
            return (t.v0, t.v1, t.v2);
        }

        private static void UnifyWinding(List<Tri> tris)
        {
            var visited = new bool[tris.Count];
            var stack = new Stack<int>();
            stack.Push(0);
            visited[0] = true;

            while (stack.Count > 0)
            {
                int i = stack.Pop();
                var t = tris[i];

                foreach (var j in t.adj)
                {
                    if (visited[j]) continue;

                    if (!SameEdgeDirection(t, tris[j]))
                    {
                        Swap(tris[j]); // swap v1 and v2
                    }

                    visited[j] = true;
                    stack.Push(j);
                }
            }
        }

        private static bool SameEdgeDirection(Tri a, Tri b)
        {
            int[] av = { a.v0, a.v1, a.v2 };
            int[] bv = { b.v0, b.v1, b.v2 };

            // look for each edge of a
            for (int i = 0; i < 3; i++)
            {
                int a0 = av[i];
                int a1 = av[(i + 1) % 3];

                // whether b has the same edge
                for (int j = 0; j < 3; j++)
                {
                    int b0 = bv[j];
                    int b1 = bv[(j + 1) % 3];

                    if (a0 == b0 && a1 == b1)
                        return true;   // same direction (abnormal)

                    if (a0 == b1 && a1 == b0)
                        return false;  // reverse direction (normal)
                }
            }

            return false; // no shared edges (usually not)
        }

        private static void Swap(Tri t)
        {
            (t.v1, t.v2) = (t.v2, t.v1);
        }

        public static void RemapJsonToOutputOrder(Crash2Json json, Dictionary<int, int> originalVertexToOutputIndex)
        {
            int oldCount = json.vertices.Count;
            int assignedCount = originalVertexToOutputIndex.Count;

            int[] oldToNew = Enumerable.Repeat(-1, oldCount).ToArray();

            foreach (var kv in originalVertexToOutputIndex)
                oldToNew[kv.Key] = kv.Value;

            // assign unassigned old vertices sequentially to the end
            int nextIdx = assignedCount;
            for (int i = 0; i < oldCount; i++)
            {
                if (oldToNew[i] == -1)
                    oldToNew[i] = nextIdx++;
            }

            int newCount = nextIdx;

            // reconstruct the vertex array
            var newVertices = new List<float[]>(newCount);
            for (int i = 0; i < newCount; i++) newVertices.Add(null!);
            for (int old = 0; old < oldCount; old++)
                newVertices[oldToNew[old]] = json.vertices[old];
            json.vertices = newVertices;

            // if colors exist for each vertex, sort them
            if (json.colors != null && json.colors.Count == oldCount)
            {
                var newColors = new List<int[]>(newCount);
                for (int i = 0; i < newCount; i++) newColors.Add(null!);
                for (int old = 0; old < oldCount; old++)
                    newColors[oldToNew[old]] = json.colors[old];
                json.colors = newColors;
            }

            // frames: sort the vertex lists within each frame
            if (json.frames != null)
            {
                for (int fi = 0; fi < json.frames.Count; fi++)
                {
                    var oldFrame = json.frames[fi];
                    if (oldFrame == null) continue;
                    var newFrame = new List<float[]>(newCount);
                    for (int i = 0; i < newCount; i++) newFrame.Add(null!);
                    for (int old = 0; old < oldFrame.Count; old++)
                        newFrame[oldToNew[old]] = oldFrame[old];
                    json.frames[fi] = newFrame;
                }
            }

            // triangles: replace the indexes from old to new
            foreach (var tri in json.triangles)
            {
                for (int k = 0; k < tri.v.Length; k++)
                {
                    int oldIdx = tri.v[k];
                    tri.v[k] = oldToNew[oldIdx];
                }
            }
        }
    }

    public static class BlenderModelConverter
    {
        public static List<Crash2Json> LoadModelJson(string path)
        {
            WaitForStableFile(path);

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    using var fs = new FileStream(
                        path,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.ReadWrite);

                    using var sr = new StreamReader(fs);
                    string json = sr.ReadToEnd();

                    return JsonSerializer.Deserialize<List<Crash2Json>>(json)!;
                }
                catch (IOException)
                {
                    Thread.Sleep(100);
                }
                catch (JsonException)
                {
                    Thread.Sleep(100);
                }
            }

            throw new Exception("JSON did not stabilize.");
        }

        private static void WaitForStableFile(string path)
        {
            long lastSize = -1;
            int stableCount = 0;

            while (stableCount < 5)
            {
                long size = new FileInfo(path).Length;

                if (size == lastSize)
                    stableCount++;
                else
                    stableCount = 0;

                lastSize = size;
                Thread.Sleep(100);
            }
        }

        //
        // model
        //
        private static (uint[], Dictionary<TriangleKey, StructureInfo>, int) BuildPolyDataFromStrip
            (Crash2Json json, Dictionary<SceneryColor, int> colors, Dictionary<TriangleKey, ModelMaterial> materials, bool compressed, int spVcount, ModelSettings settings, bool debug)
        {
            if (debug)
                Console.WriteLine("[Strips]");

            // TODO: verify
            byte header = (byte)colors.Count;
            int keyOffset = (header + 1) / 2;
            int maxAllowedKeys = ModelTriangle.NullPtr - keyOffset;

            // build initial tris
            List<Tri> tris = [];
            for (int i = 0; i < json.triangles.Count; i++)
            {
                var t = json.triangles[i];
                tris.Add(new Tri { v0 = t.v[0], v1 = t.v[1], v2 = t.v[2] });
            }

            // tris index map (tris -> json)
            Dictionary<(int, int, int), int> triMap = [];
            for (int i = 0; i < json.triangles.Count; i++)
            {
                var t = json.triangles[i];
                int a = t.v[0],
                    b = t.v[1],
                    c = t.v[2];
                var key = (a, b, c);
                if (!triMap.ContainsKey(key))
                    triMap[key] = i;
            }

            var strips = BuildAllStripsBestOfAttempts(tris, settings, false);
            strips = ReorderStripsGreedy(strips, keyOffset, false);

            if (debug)
            {
                for (int i = 0; i < strips.Count; i++)
                    Console.WriteLine($"Strip [{i}]: {string.Join(", ", strips[i])}");
                Console.WriteLine();

                var edgeUse = new Dictionary<(int, int), int>();
                foreach (var t in tris)
                {
                    foreach (var e in new[] { Edge(t.v0, t.v1), Edge(t.v1, t.v2), Edge(t.v2, t.v0) })
                    {
                        edgeUse.TryAdd(e, 0);
                        edgeUse[e]++;
                    }
                }
                int border = edgeUse.Count(e => e.Value == 1);
                int manifold = edgeUse.Count(e => e.Value == 2);
                int broken = edgeUse.Count(e => e.Value > 2);
                Console.WriteLine($"border:{border}  manifold:{manifold}  broken:{broken}");
            }

            // pre-pass: determine originalVertexToOutputIndex (performed before creating ModelTriangle)
            Dictionary<int, int> keyMapTemp = [];
            byte nextKeyTemp = 0;
            int GetOrCreateKeyTemp(int vertexIndex, out ModelTriangle.IndexType idxType)
            {
                if (keyMapTemp.TryGetValue(vertexIndex, out int existing))
                {
                    idxType = ModelTriangle.IndexType.Duplicate;
                    return existing;
                }
                while (nextKeyTemp == ModelTriangle.NullPtr) nextKeyTemp++;
                int assigned = nextKeyTemp++;
                keyMapTemp[vertexIndex] = assigned;
                idxType = ModelTriangle.IndexType.Original;
                return assigned;
            }

            Dictionary<int, int> originalVertexToOutputIndex = [];
            int nextOutputIndex = 0;

            // scan strips first to determine the “first appearance order”
            foreach (var strip in strips)
            {
                for (int j = 0; j < strip.Count; j++)
                {
                    int v = strip[j];
                    var _ = GetOrCreateKeyTemp(v, out ModelTriangle.IndexType idt);
                    if (idt == ModelTriangle.IndexType.Original)
                    {
                        if (!originalVertexToOutputIndex.ContainsKey(v))
                            originalVertexToOutputIndex[v] = nextOutputIndex++;
                    }
                }
            }

            // sort JSON by output order (destructive)
            RemapJsonToOutputOrder(json, originalVertexToOutputIndex);

            // rebuild tris/trimap/strips after sorting (with new indexes)
            tris.Clear();
            for (int i = 0; i < json.triangles.Count; i++)
            {
                var t = json.triangles[i];
                tris.Add(new Tri { v0 = t.v[0], v1 = t.v[1], v2 = t.v[2] });
            }

            triMap.Clear();
            for (int i = 0; i < json.triangles.Count; i++)
            {
                var t = json.triangles[i];
                int[] verts = [t.v[0], t.v[1], t.v[2]];
                Array.Sort(verts);
                var key = (verts[0], verts[1], verts[2]);
                if (!triMap.ContainsKey(key))
                    triMap[key] = i;
            }

            strips = BuildAllStripsBestOfAttempts(tris, settings, true);
            strips = ReorderStripsGreedy(strips, keyOffset, true);
            if (debug)
            {
                Console.WriteLine();
                Console.WriteLine("[Remapped Strips]");
                for (int i = 0; i < strips.Count; i++)
                    Console.WriteLine($"Strip [{i}]: {string.Join(", ", strips[i])}");
            }

            var vertexRemaining = new Dictionary<int, int>();
            foreach (var strip in strips)
            {
                foreach (var v in strip)
                {
                    if (!vertexRemaining.TryGetValue(v, out int cnt)) cnt = 0;
                    vertexRemaining[v] = cnt + 1;
                }
            }

            // PositionKey management — prioritize reuse of the smallest available key
            var keyMap = new Dictionary<int, int>(); // vertex -> key
            var freeKeys = new SortedSet<int>();
            int nextKey = keyOffset;

            int AllocateNewKey()
            {
                while (nextKey == ModelTriangle.NullPtr) nextKey++;

                if (nextKey > ModelTriangle.NullPtr)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[WARNING] Allocating key {nextKey} which exceeds NullPtr ({ModelTriangle.NullPtr})");
                    Console.ForegroundColor = ConsoleColor.White;
                    throw new InvalidOperationException("Exceeded maximum key limit. Consider reducing the number of vertices or colors.");
                }

                return nextKey++;
            }

            int GetOrCreateKey(int vertexIndex, out ModelTriangle.IndexType idxType)
            {
                // if already have the key -> Duplicate
                if (keyMap.TryGetValue(vertexIndex, out int existingKey))
                {
                    idxType = ModelTriangle.IndexType.Duplicate;

                    if (vertexRemaining.TryGetValue(vertexIndex, out int rem))
                    {
                        rem--;
                        vertexRemaining[vertexIndex] = rem;
                        if (rem == 0)
                        {
                            // release the key to make it reusable
                            keyMap.Remove(vertexIndex);
                            freeKeys.Add(existingKey);
                        }
                    }

                    return existingKey;
                }

                // use the smallest available key first
                int assigned;
                if (freeKeys.Count > 0)
                {
                    assigned = freeKeys.Min;
                    freeKeys.Remove(assigned);
                }
                else
                {
                    assigned = AllocateNewKey();
                }

                keyMap[vertexIndex] = assigned;
                idxType = ModelTriangle.IndexType.Original;

                if (vertexRemaining.TryGetValue(vertexIndex, out int remainingNow))
                {
                    remainingNow--;
                    vertexRemaining[vertexIndex] = remainingNow;
                    if (remainingNow == 0)
                    {
                        // if it won't be used in the future, release it immediately and make it reusable
                        keyMap.Remove(vertexIndex);
                        freeKeys.Add(assigned);
                        assigned = ModelTriangle.NullPtr;
                    }
                }

                return assigned;
            }

            var structureInfo = new Dictionary<TriangleKey, StructureInfo>();
            byte texIdx = 1;
            byte animTexIdx = 0;

            var tempEntries = new List<(bool isColor, int Color1, int Color2, int Key, int Vertex, ModelTriangle.IndexType IdxType, int ColorIndex, byte TriangleType, byte TriangleSubtype, byte TextureIndex, bool Animated)>();
            var entries = new List<(bool isColor, int Color1, int Color2, int Key, int Vertex, ModelTriangle.IndexType IdxType, int ColorIndex, byte TriangleType, byte TriangleSubtype, byte TextureIndex, bool Animated)>();
            var data = new List<uint>();

            // header
            ModelColor initColor = new() { Color1 = header };
            data.Add(initColor.SaveHeader());

            HashSet<int> actualUsedKeys = [];
            List<int> lastColors = [0, 0];

            // if compressed, create deg tris for sp verts
            if (compressed)
            {
                for (int spV = 0; spV < spVcount; spV++)
                {
                    var idt = ModelTriangle.IndexType.Original;
                    tempEntries.Add((false, -1, -1, Key: keyOffset, Vertex: spV, IdxType: idt, ColorIndex: 0, TriangleType: 2, TriangleSubtype: 3, TextureIndex: 0, Animated: false));
                    if (spV < spVcount - 1)
                    {
                        // og-og-dupl (a-b-b)
                        spV++;
                        tempEntries.Add((false, -1, -1, Key: keyOffset, Vertex: spV, IdxType: idt, ColorIndex: 0, TriangleType: 2, TriangleSubtype: 3, TextureIndex: 0, Animated: false));
                        idt = ModelTriangle.IndexType.Duplicate;
                        tempEntries.Add((false, -1, -1, Key: keyOffset, Vertex: spV, IdxType: idt, ColorIndex: 0, TriangleType: 2, TriangleSubtype: 3, TextureIndex: 0, Animated: false));
                    }
                    else
                    {
                        // og-dupl-dupl (a-a-a)
                        idt = ModelTriangle.IndexType.Duplicate;
                        tempEntries.Add((false, -1, -1, Key: keyOffset, Vertex: spV, IdxType: idt, ColorIndex: 0, TriangleType: 2, TriangleSubtype: 3, TextureIndex: 0, Animated: false));
                        tempEntries.Add((false, -1, -1, Key: keyOffset, Vertex: spV, IdxType: idt, ColorIndex: 0, TriangleType: 2, TriangleSubtype: 3, TextureIndex: 0, Animated: false));
                    }
                }
                entries.AddRange(from e in tempEntries
                                 select e);
            }

            for (int si = 0; si < strips.Count; si++)
            {
                //DebugLog($"Strip [{si}]", true, debug);

                var strip = strips[si];
                tempEntries.Clear();
                int offset = 0;

                // create each vertex entry (textures, etc. are undetermined)
                for (int j = 0; j < strip.Count; j++)
                {
                    int v = strip[j];
                    int assigned = GetOrCreateKey(v, out ModelTriangle.IndexType idt);

                    //if (idt == ModelTriangle.IndexType.Duplicate)
                    //    actualUsedKeys.Add(assigned);
                    if (assigned != ModelTriangle.NullPtr)
                        actualUsedKeys.Add(assigned);

                    int colorIndex = 0; // temp

                    byte triType = (byte)(j < 3 ? 2 : 0); // first 3 tris are CC(2)
                    byte triSubtype = 0; // temp

                    tempEntries.Add((false, -1, -1, Key: assigned, Vertex: v, IdxType: idt, ColorIndex: colorIndex, TriangleType: triType, triSubtype, TextureIndex: 0, Animated: false));
                }

                // determine color/texture/animated/triSubtype
                for (int j = 2; j < strip.Count; j++)
                {
                    int a, b, c;

                    bool isCC = j == 2;
                    if (isCC)
                    {
                        // CC
                        a = strip[j - 2];
                        b = strip[j - 1];
                        c = strip[j];
                    }
                    else
                    {
                        // AA
                        a = strip[j];
                        b = strip[j - 1];
                        c = strip[j - 2];
                    }

                    byte color0 = 0;
                    byte color1 = 0;
                    byte color2 = 0;
                    byte textureIndex = 0;
                    bool animated = false;
                    byte triSubtype = 0; // temp default
                    bool overrideSubtype = false;

                    int[] verts = [a, b, c];
                    Array.Sort(verts);
                    var key = (verts[0], verts[1], verts[2]);

                    if (triMap.TryGetValue(key, out int triIndex))
                    {
                        var srcTri = json.triangles[triIndex];

                        // get index (0-2) in the triangle
                        int ia = Array.IndexOf(srcTri.v, a);
                        int ib = Array.IndexOf(srcTri.v, b);
                        int ic = Array.IndexOf(srcTri.v, c);

                        if (ia >= 0 && ib >= 0 && ic >= 0)
                        {
                            color0 = (byte)srcTri.c[ia];
                            color1 = (byte)srcTri.c[ib];
                            color2 = (byte)srcTri.c[ic];

                            // TODO
                            if (isCC)
                            {
                                // CC[0]-CC[1]-CC[2]
                                lastColors[0] = color2; // CC[2]
                                lastColors[1] = color1; // CC[1]
                            }
                            else
                            {
                                if (lastColors[0] != color1 || lastColors[1] != color2)
                                {
                                    tempEntries.Insert(j + offset, (isColor: true, color1, color2, 0, 0, 0, 0, 0, 0, 0, false)); // ModelColor
                                    offset++;
                                    lastColors[0] = color1;
                                    lastColors[1] = color2;
                                }
                            }

                            // if not CC, swap uv0 and uv2
                            byte[] uv0 = !isCC ? ToUVByte(srcTri.uv[ic]) : ToUVByte(srcTri.uv[ia]);
                            byte[] uv1 = ToUVByte(srcTri.uv[ib]);
                            byte[] uv2 = !isCC ? ToUVByte(srcTri.uv[ia]) : ToUVByte(srcTri.uv[ic]);

                            // flip v
                            byte v0 = uv0[1], v1 = uv1[1], v2 = uv2[1];
                            byte minV = Math.Min(v0, Math.Min(v1, v2));
                            byte maxV = Math.Max(v0, Math.Max(v1, v2));
                            uv0[1] = v0 == minV ? maxV : minV;
                            uv1[1] = v1 == minV ? maxV : minV;
                            uv2[1] = v2 == minV ? maxV : minV;

                            if (srcTri.material >= 0)
                            {
                                TriangleKey tkey = new(srcTri.material, uv0, uv1, uv2);
                                if (!structureInfo.ContainsKey(tkey))
                                {
                                    textureIndex = texIdx;

                                    // search for if animated
                                    foreach (var oldKvp in materials)
                                    {
                                        TriangleKey oldKey = oldKvp.Key;
                                        if (tkey.Material == oldKey.Material)
                                        {
                                            ModelMaterial mat = oldKvp.Value;

                                            int fo = mat.Info.FaceOrientation;
                                            if (fo > 0 && fo <= 3)
                                            {
                                                triSubtype = (byte)fo;
                                                overrideSubtype = true;
                                            }

                                            // if animated
                                            if (mat.Info.AnimCount > 0)
                                            {
                                                animated = true;
                                                textureIndex = animTexIdx;
                                                animTexIdx++;
                                            }
                                            else
                                            {
                                                texIdx++;
                                            }

                                            break;
                                        }
                                    }

                                    structureInfo.Add(tkey, new StructureInfo(textureIndex, triSubtype, isCC));

                                    //DebugLog($"        Created new texture: {textureIndex}", true, debug);
                                }
                                else if (structureInfo.TryGetValue(tkey, out StructureInfo str))
                                {
                                    textureIndex = str.TextureIndex;

                                    foreach (var oldKvp in materials)
                                    {
                                        TriangleKey oldKey = oldKvp.Key;
                                        if (tkey.Material == oldKey.Material)
                                        {
                                            ModelMaterial mat = oldKvp.Value;
                                            if (mat.Info.AnimCount > 0)
                                                animated = true;

                                            int fo = str.FaceOrientation;
                                            if (fo > 0 && fo <= 3)
                                            {
                                                triSubtype = (byte)fo;
                                                overrideSubtype = true;
                                            }
                                            break;
                                        }
                                    }
                                    //DebugLog($"        Found exist texture: {textureIndex}", true, debug);
                                }
                                else
                                {
                                    //DebugLog($"        Could not find texture.", true, debug);
                                }
                            }

                            // determine the subtype from the winding (using the remapped vertices)
                            if (!overrideSubtype)
                            {
                                try
                                {
                                    Vector3 va = new(json.vertices[a][0], json.vertices[a][1], json.vertices[a][2]);
                                    Vector3 vb = new(json.vertices[b][0], json.vertices[b][1], json.vertices[b][2]);
                                    Vector3 vc = new(json.vertices[c][0], json.vertices[c][1], json.vertices[c][2]);

                                    Vector3 normalFromBlender = srcTri != null && srcTri.normal != null && srcTri.normal.Length >= 3
                                        ? new Vector3(srcTri.normal[0], srcTri.normal[1], srcTri.normal[2])
                                        : Vector3.Zero;

                                    // true -> orientation matches (considered CCW） => subtype = 1
                                    // false -> reversed orientation (CW) => subtype = 3
                                    bool correct = IsWindingCorrect(va, vb, vc, normalFromBlender);
                                    if (isCC)
                                        triSubtype = correct ? (byte)3 : (byte)1;
                                    else
                                        triSubtype = correct ? (byte)1 : (byte)3;
                                }
                                catch
                                {
                                    Console.WriteLine($"[Warning] Failed to determine triangle subtype for tri with vertices {a}, {b}, {c}.");
                                    triSubtype = 0;
                                }
                            }
                        }
                    }

                    int i0 = j + offset - 2;

                    // if CC-CC-CC
                    if (i0 == 0)
                    {
                        tempEntries[i0] = (false, -1, -1, tempEntries[i0].Key, tempEntries[i0].Vertex, tempEntries[i0].IdxType, color0, tempEntries[i0].TriangleType, triSubtype, textureIndex, animated);
                        tempEntries[i0 + 1] = (false, -1, -1, tempEntries[i0 + 1].Key, tempEntries[i0 + 1].Vertex, tempEntries[i0 + 1].IdxType, color1, tempEntries[i0 + 1].TriangleType, triSubtype, textureIndex, animated);
                        tempEntries[i0 + 2] = (false, -1, -1, tempEntries[i0 + 2].Key, tempEntries[i0 + 2].Vertex, tempEntries[i0 + 2].IdxType, color2, tempEntries[i0 + 2].TriangleType, triSubtype, textureIndex, animated);
                    }
                    else
                    {
                        if (!tempEntries[i0 + 2].isColor)
                            tempEntries[i0 + 2] = (false, -1, -1, tempEntries[i0 + 2].Key, tempEntries[i0 + 2].Vertex, tempEntries[i0 + 2].IdxType, color0, tempEntries[i0 + 2].TriangleType, triSubtype, textureIndex, animated);
                    }

                }

                entries.AddRange(from e in tempEntries
                                 select e);
            }

            int maxKey = actualUsedKeys.Count > 0 ? actualUsedKeys.Max() : keyOffset - 1;
            int uniqueKeys = actualUsedKeys.Count;
            Console.WriteLine();
            Console.WriteLine("[Result]");
            Console.WriteLine($"Strips: {strips.Count}");
            Console.WriteLine($"Max key number: {maxKey} (offset: {keyOffset}, unique: {uniqueKeys})");

            if (maxKey > ModelTriangle.NullPtr)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Warning: The keys exceeded {ModelTriangle.NullPtr} (NullPtr). Exceeded by {maxKey - ModelTriangle.NullPtr}.");
                Console.ForegroundColor = ConsoleColor.White;
            }

            //int row = 0;
            foreach (var e in entries)
            {
                if (e.isColor)
                {
                    var mc = new ModelColor
                    {
                        Color1 = (byte)e.Color1,
                        Color2 = (byte)e.Color2
                    };

                    data.Add(mc.Save());
                    //DebugLog($"{row}: color1={e.Color1} color2={e.Color2}", true, debug);
                }
                else
                {
                    //byte posKey = keyMap[e.Vertex];
                    var mt = new ModelTriangle(
                        texture: e.TextureIndex,
                        animated: e.Animated,
                        color: (byte)e.ColorIndex,
                        key: (byte)e.Key,
                        unknown: 0,
                        type: (byte)e.IdxType,
                        flag: true,
                        tritype: (byte)((e.TriangleSubtype & 0x3) | ((e.TriangleType & 0x3) << 2))
                    );

                    data.Add(mt.Save());
                    //DebugLog($"{row}: v{e.Vertex} posKey=0x{e.Key:X3} idxType={e.IdxType} triType={e.TriangleType} triSub={e.TriangleSubtype} tex={e.TextureIndex} col={e.ColorIndex}", true, debug);
                }
                //row++;
            }

            // footer
            data.Add(0xFFFFFFFF);
            return (data.ToArray(), structureInfo, strips.Count);
        }

        private static (List<ModelTexture>, List<ModelExtendedTexture>) BuildTexture(Dictionary<TriangleKey, StructureInfo> structureInfo, Dictionary<TriangleKey, ModelMaterial> materials, bool debug)
        {
            List<ModelTexture> textures = [];
            List<ModelExtendedTexture> animatedtextures = [];

            // split animated and normal textures to add normal textures first
            var normalTextures = new List<ModelTexture>();
            var animatedTextureData = new List<(List<ModelTexture> textures, MaterialInfo info)>();

            foreach (var kvp in structureInfo)
            {
                TriangleKey key = kvp.Key;
                foreach (var oldKvp in materials)
                {
                    TriangleKey oldKey = oldKvp.Key;
                    if (key.Material == oldKey.Material)
                    {
                        var mat = oldKvp.Value;
                        List<ModelTexture> modelTextures = mat.Texture;

                        if (mat.Info.AnimCount > 0)
                        {
                            // animated texture: create ModelTexture list and store with anim info to add later
                            var texList = new List<ModelTexture>();
                            foreach (ModelTexture tex in modelTextures)
                            {
                                texList.Add(CreateModelTexture(tex, key));
                            }
                            animatedTextureData.Add((texList, mat.Info));
                        }
                        else
                        {
                            // normal texture
                            foreach (ModelTexture tex in modelTextures)
                            {
                                normalTextures.Add(CreateModelTexture(tex, key));
                            }
                        }
                        break;
                    }
                }
            }

            // add normal textures first
            textures.AddRange(normalTextures);

            // add animated textures
            foreach (var (texList, info) in animatedTextureData)
            {
                int startOffset = textures.Count + 1;
                textures.AddRange(texList);

                animatedtextures.Add(new ModelExtendedTexture(0)
                {
                    Offset = startOffset,
                    Mask = texList.Count - 1,
                    Delay = info.AnimDelay,
                    Latency = info.AnimSpeed
                });
            }

            return (textures, animatedtextures);
        }

        private static ModelTexture CreateModelTexture(ModelTexture tex, TriangleKey key)
        {
            int blendMode = tex.BlendMode;
            int colorMode = tex.ColorMode;
            int clutY2 = tex.ClutY >> 2;
            int clutY1 = (tex.ClutY & 0x3) << 2;
            int clutX = tex.ClutX;

            int DestX = Math.Min(tex.U1, Math.Min(tex.U2, tex.U3));
            int DestY = Math.Min(tex.V1, Math.Min(tex.V2, tex.V3));
            int Width = Math.Max(tex.U1, Math.Max(tex.U2, tex.U3)) - DestX;
            int Height = Math.Max(tex.V1, Math.Max(tex.V2, tex.V3)) - DestY;

            // UVs must be 0 or 1.
            int u1 = key.U0 != 0 ? DestX + Width : DestX;
            int v1 = key.V0 != 0 ? DestY + Height : DestY;
            int u2 = key.U1 != 0 ? DestX + Width : DestX;
            int v2 = key.V1 != 0 ? DestY + Height : DestY;
            int u3 = key.U2 != 0 ? DestX + Width : DestX;
            int v3 = key.V2 != 0 ? DestY + Height : DestY;

            int tpage = tex.Page;

            return new ModelTexture(
                u1: (byte)u1,
                v1: (byte)v1,
                cluty1: (byte)clutY1,
                clutx: (byte)clutX,
                cluty2: (byte)clutY2,
                u2: (byte)u2,
                v2: (byte)v2,
                colormode: (byte)colorMode,
                blendmode: (byte)blendMode,
                segment: tex.Segment,
                textureoffset: (byte)tpage,
                u3: (byte)u3,
                v3: (byte)v3,
                u4: 0,
                v4: 0
            );
        }

        private static ModelEntry BuildModelEntry(Crash2Json json, Dictionary<TriangleKey, ModelMaterial> materials, int eid, string tpageName, int[] modelScales, bool compressed, int spVcount, ModelSettings settings, Debug debug)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine($"Building model...");
            Console.ForegroundColor = ConsoleColor.White;

            // colors
            Dictionary<SceneryColor, int> colors = [];
            int colorIndex = 0;
            foreach (var color in json.colors)
            {
                SceneryColor col = new()
                {
                    Red = (byte)color[0],
                    Green = (byte)color[1],
                    Blue = (byte)color[2],
                    Extra = 0
                };
                if (colors.TryAdd(col, colorIndex))
                {
                    colorIndex++;
                }
            }

            if (colorIndex > 127)
                throw new Exception($"Too many colors in model ({colorIndex} colors, must be <= 127)");

            var colorsList = colors.Keys.ToList();

            // poly
            var polys = BuildPolyDataFromStrip(json, colors, materials, compressed, spVcount, settings, debug.DebugMode);
            uint[] poly = polys.Item1;
            var structureInfo = polys.Item2;
            int stripCount = polys.Item3;

            // textures
            var textureSet = BuildTexture(structureInfo, materials, debug.DebugMode);
            List<ModelTexture> textures = textureSet.Item1;
            List<ModelExtendedTexture> animatedtextures = textureSet.Item2;

            // info
            byte[] info = new byte[0x50];

            int spVertCount = spVcount; // for compressed model
            int polyCount = poly.Length;
            int textureCount = textures.Count;
            int vertexCount = json.vertices.Count + (spVertCount * 2);
            int colorCount = colorsList.Count;
            int triCount = json.triangles.Count;
            int animTexCount = animatedtextures.Count;

            int tpageCount = 1; // temp
            int tpage1 = Entry.ENameToEID(tpageName);

            // TODO: calculate correct scale values
            BitConv.ToInt32(info, 0x0, modelScales[0]);  // scaleX
            BitConv.ToInt32(info, 0x4, modelScales[1]);  // scaleY
            BitConv.ToInt32(info, 0x8, modelScales[2]);  // scaleZ
            BitConv.ToInt32(info, 0xC, tpage1);          // TPage1                  temp
            BitConv.ToInt32(info, 0x2C, polyCount);      // ModelStructCount
            BitConv.ToInt32(info, 0x30, stripCount);     // StripCount ?
            BitConv.ToInt32(info, 0x34, textureCount);   // TextureCount
            BitConv.ToInt32(info, 0x38, vertexCount);    // VertexCount
            BitConv.ToInt32(info, 0x3C, colorCount);     // ColorCount
            BitConv.ToInt32(info, 0x40, tpageCount);     // TPageCount
            BitConv.ToInt32(info, 0x44, triCount);       // PolyCount
            BitConv.ToInt32(info, 0x48, animTexCount);   // AnimatedTextureCount
            BitConv.ToInt32(info, 0x4C, spVertCount);    // Special vertex count

            return new ModelEntry(
                info,
                poly,
                colorsList,
                textures,
                animatedtextures,
                positions: compressed ? new List<ModelPosition>() : null!,
                eid
            );
        }

        private static void DebugLog(string message, bool addLine, bool debug)
        {
            if (debug)
            {
                if (addLine)
                    Console.WriteLine(message);
                else
                    Console.Write(message);
            }
        }

        //
        // anim
        //

        private static Frame BuildFrame(Crash2Json json, int frameIndex, int eid, float[] scaleFactors, int[] modelScales, bool compressed, bool debug)
        {
             //DebugLog($"[Frame {frameIndex}]", true, debug);

            // special vertex
            List<float[]> spVerts = [];
            foreach (var marker in json.markers[frameIndex])
                spVerts.Add(marker.pos);
            foreach (var marker in json.groups[frameIndex])
                spVerts.Add(marker.pos);

            int spVertCount = spVerts.Count;

            // vertices
            List<float[]> frameVerts = spVerts;
            frameVerts.AddRange(json.frames[frameIndex]);

            // vertices
            // only the vertex count is matters ?
            FrameVertex[] vertices = new FrameVertex[frameVerts.Count];

            float scaleX = 1.0f / scaleFactors[0];
            float scaleY = 1.0f / scaleFactors[1];
            float scaleZ = 1.0f / scaleFactors[2];

            int count = frameVerts.Count;
            var rawX = new int[count];
            var rawY = new int[count];
            var rawZ = new int[count];

            int minRawX = int.MaxValue, maxRawX = int.MinValue;
            int minRawY = int.MaxValue, maxRawY = int.MinValue;
            int minRawZ = int.MaxValue, maxRawZ = int.MinValue;

            for (int i = 0; i < count; i++)
            {
                var v = frameVerts[i];
                // change axis order (x, y, z) -> (x, z, -y)
                int rx = (int)Math.Round(v[0] / scaleX);
                int ry = (int)Math.Round(v[2] / scaleY);
                int rz = (int)Math.Round(-v[1] / scaleZ);

                rawX[i] = rx;
                rawY[i] = ry;
                rawZ[i] = rz;

                if (rx < minRawX) minRawX = rx;
                if (rx > maxRawX) maxRawX = rx;
                if (ry < minRawY) minRawY = ry;
                if (ry > maxRawY) maxRawY = ry;
                if (rz < minRawZ) minRawZ = rz;
                if (rz > maxRawZ) maxRawZ = rz;
            }

            // choose offsets so that local = raw - offset >= 0
            int frameOffsetRawX = minRawX;
            int frameOffsetRawY = minRawY;
            int frameOffsetRawZ = minRawZ;

            short frameOffsetX = (short)(frameOffsetRawX * 4);
            short frameOffsetY = (short)(frameOffsetRawY * 4);
            short frameOffsetZ = (short)(frameOffsetRawZ * 4);

            List<byte> lstVerts = new();
            List<int> overflowedVerts = new();
            for (int i = 0; i < count; i++)
            {
                int localX = rawX[i] - frameOffsetRawX;
                int localY = rawY[i] - frameOffsetRawY;
                int localZ = rawZ[i] - frameOffsetRawZ;

                byte x = ToByte(localX, out bool ox);
                byte y = ToByte(localY, out bool oy);
                byte z = ToByte(localZ, out bool oz);

                // swap Y and Z
                lstVerts.Add(x);
                lstVerts.Add(z);
                lstVerts.Add(y);

                if (ox || oy || oz)
                    overflowedVerts.Add(i);
            }

            if (overflowedVerts.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[Frame {frameIndex}] Warning: Vertices overflowed ({string.Join(", ", overflowedVerts)})");
                Console.ForegroundColor = ConsoleColor.White;
            }

            int byteCount = frameVerts.Count * 3;
            byte[] verts = new byte[(byteCount + 3) / 4 * 4]; // align to 4 bytes
            Array.Copy(lstVerts.ToArray(), verts, lstVerts.ToArray().Length);

            bool[] temporals = new bool[verts.Length / 4 * 32];
            for (int i = 0; i < verts.Length / 4; i++)
            {
                int val = BitConv.FromInt32(verts, i * 4);
                for (int j = 0; j < 32; j++) // reverse endianness for decompression
                {
                    temporals[i * 32 + j] = (val >> (31 - j) & 0x1) == 1;
                }
            }

            // collisions
            float[] collScales =
            [
                scaleFactors[0] * modelScales[0] / BASE_COLLISION_PRODUCT,
                scaleFactors[1] * modelScales[1] / BASE_COLLISION_PRODUCT,
                scaleFactors[2] * modelScales[2] / BASE_COLLISION_PRODUCT,
            ];
            List<FrameCollision> lstColl = [];
            foreach (var coll in json.collisions[frameIndex])
            {
                lstColl.Add(BuildCollision(coll.min, coll.max, collScales));
            }
            FrameCollision[] collisions = lstColl.ToArray();

            // HeaderSize
            int headersize = 0x18 + (collisions.Length * 0x28) + (spVertCount * 3);

            return new Frame(
                xoffset: frameOffsetX,
                yoffset: frameOffsetY,
                zoffset: frameOffsetZ,
                unknown: 0,
                modeleid: eid,
                headersize: headersize,
                collision: collisions,
                vertices: vertices,
                specialvertexcount: spVertCount,
                temporals: temporals,
                isnew: false
            );
        }

        private static IList<Position> GetVertices(Frame frame)
        {
            IList<Position> verts = new Position[frame.Vertices.Count];

            // uncompressed frame
            bool[] uncompressedbitstream = new bool[frame.Temporals.Length];
            for (int i = 0; i < uncompressedbitstream.Length / 32; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    for (int k = 0; k < 8; ++k)
                    {
                        uncompressedbitstream[32 * i + 24 - j * 8 + k] = frame.Temporals[32 * i + j * 8 + k]; // replace this with a cool formula one day
                    }
                }
            }
            int bi = 0;
            for (int i = 0; i < frame.Vertices.Count; ++i)
            {
                byte x = 0;
                for (int j = 0; j < 8; ++j)
                {
                    x |= (byte)(Convert.ToByte(uncompressedbitstream[bi++]) << (7 - j));
                }

                byte y = 0;
                for (int j = 0; j < 8; ++j)
                {
                    y |= (byte)(Convert.ToByte(uncompressedbitstream[bi++]) << (7 - j));
                }

                byte z = 0;
                for (int j = 0; j < 8; ++j)
                {
                    z |= (byte)(Convert.ToByte(uncompressedbitstream[bi++]) << (7 - j));
                }

                verts[i] = new Position(x, y, z);
            }

            return verts;
        }

        private static void BuildOptimalSharedPositions(List<Frame> frames, List<ModelPosition> sharedPos, int method)
        {
            int spVcount = frames[0].SpecialVertexCount;
            int vcount = frames[0].Vertices.Count;

            for (int i = 0; i < vcount; i++)
            {
                var diffsX = new List<int>();
                var diffsY = new List<int>();
                var diffsZ = new List<int>();
                int prevX = 0, prevY = 0, prevZ = 0;

                foreach (var frame in frames)
                {
                    var v = GetVertices(frame)[i];

                    int tx = (int)v.X;
                    int ty = (int)v.Y;
                    int tz = (int)v.Z;

                    diffsX.Add(WrapDiff(tx, prevX));
                    diffsY.Add(WrapDiff(ty, prevY));
                    diffsZ.Add(WrapDiff(tz, prevZ));

                    prevX = tx;
                    prevY = ty;
                    prevZ = tz;
                }

                int x, y, z;

                switch (method)
                {
                    case 1: // average
                        x = (int)diffsX.Average();
                        y = (int)diffsY.Average();
                        z = (int)diffsZ.Average();
                        break;
                    case 2: // all 0
                        x = 0;
                        y = 0;
                        z = 0;
                        break;
                    default: // median
                        x = Median(diffsX);
                        y = Median(diffsY);
                        z = Median(diffsZ);
                        break;
                }
                ;

                sharedPos[i].X = (byte)(x >> 1); // X is scaled to twice
                sharedPos[i].Y = (byte)y;
                sharedPos[i].Z = (byte)z;
            }
        }

        public static (List<Frame>, List<ModelPosition>) CompressFrames(List<Frame> frames, int method)
        {
            if (frames == null || frames.Count == 0)
                return (frames, new List<ModelPosition>());

            int spVcount = frames[0].SpecialVertexCount;
            int vcount = frames[0].Vertices.Count;

            List<ModelPosition> sharedPos = [];
            for (int i = 0; i < vcount; i++)
                sharedPos.Add(new ModelPosition(0));

            BuildOptimalSharedPositions(frames, sharedPos, method);

            var maxXBits = new int[vcount];
            var maxYBits = new int[vcount];
            var maxZBits = new int[vcount];

            foreach (var frame in frames)
            {
                int x_acc = 0, y_acc = 0, z_acc = 0;
                var verts = GetVertices(frame);

                for (int i = 0; i < vcount; i++)
                {
                    var p = sharedPos[i];
                    var v = verts[i];

                    int targetX, targetY, targetZ;

                    targetX = (int)v.X;
                    targetY = (int)v.Y;
                    targetZ = (int)v.Z;

                    int predX = (x_acc + ((p.XBits == 7) ? 0 : (p.X << 1))) & 0xFF;
                    int predY = (y_acc + ((p.YBits == 7) ? 0 : p.Y)) & 0xFF;
                    int predZ = (z_acc + ((p.ZBits == 7) ? 0 : p.Z)) & 0xFF;

                    int diffX = WrapDiff(targetX, predX);
                    int diffY = WrapDiff(targetY, predY);
                    int diffZ = WrapDiff(targetZ, predZ);

                    int xBits = BitsNeededSigned(diffX);
                    int yBits = BitsNeededSigned(diffY);
                    int zBits = BitsNeededSigned(diffZ);

                    if (xBits > maxXBits[i]) maxXBits[i] = xBits;
                    if (yBits > maxYBits[i]) maxYBits[i] = yBits;
                    if (zBits > maxZBits[i]) maxZBits[i] = zBits;

                    x_acc = targetX;
                    y_acc = targetY;
                    z_acc = targetZ;
                }
            }

            for (int i = 0; i < vcount; i++)
            {
                if (i == 0)
                {
                    // the first one must always be reset
                    sharedPos[i].X = 0;
                    sharedPos[i].Y = 0;
                    sharedPos[i].Z = 0;
                    sharedPos[i].XBits = 7;
                    sharedPos[i].YBits = 7;
                    sharedPos[i].ZBits = 7;
                }
                else
                {
                    sharedPos[i].XBits = (byte)Math.Clamp(maxXBits[i], 0, 7);
                    sharedPos[i].YBits = (byte)Math.Clamp(maxYBits[i], 0, 7);
                    sharedPos[i].ZBits = (byte)Math.Clamp(maxZBits[i], 0, 7);
                }
            }

            for (int f = 0; f < frames.Count; f++)
            {
                //Console.WriteLine($"Frame [{f}]");

                var temporals = new List<bool>();
                int x_acc = 0, y_acc = 0, z_acc = 0;

                var frame = frames[f];
                var verts = GetVertices(frame);

                for (int s = 0; s < spVcount; s++)
                {
                    // these vertices are NEVER compressed
                    WriteSignedBits(frame.Vertices[s].X, 7, temporals);
                    WriteSignedBits(frame.Vertices[s].Y, 7, temporals);
                    WriteSignedBits(frame.Vertices[s].Z, 7, temporals);
                }

                for (int i = 0; i < vcount; i++)
                {
                    //Console.WriteLine($"Vertex [{i}]");
                    var p = sharedPos[i];
                    var v = verts[i];

                    int targetX = (int)v.X;
                    int targetY = (int)v.Y;
                    int targetZ = (int)v.Z;

                    int predX = (x_acc + ((p.XBits == 7) ? 0 : (p.X << 1))) & 0xFF;
                    int predY = (y_acc + ((p.YBits == 7) ? 0 : p.Y)) & 0xFF;
                    int predZ = (z_acc + ((p.ZBits == 7) ? 0 : p.Z)) & 0xFF;

                    int diffX = WrapDiff(targetX, predX);
                    int diffY = WrapDiff(targetY, predY);
                    int diffZ = WrapDiff(targetZ, predZ);

                    if (p.XBits == 7)
                    {
                        sharedPos[i].X = 0;
                        diffX = targetX;
                    }
                    if (p.YBits == 7)
                    {
                        sharedPos[i].Y = 0;
                        diffY = targetY;
                    }
                    if (p.ZBits == 7)
                    {
                        sharedPos[i].Z = 0;
                        diffZ = targetZ;
                    }

                    if (i == 0)
                    {
                        WriteSignedBits(targetX, 7, temporals);
                        WriteSignedBits(targetZ, 7, temporals);
                        WriteSignedBits(targetY, 7, temporals);
                    }
                    else
                    {
                        WriteSignedBits(diffX, p.XBits, temporals);
                        WriteSignedBits(diffZ, p.ZBits, temporals);
                        WriteSignedBits(diffY, p.YBits, temporals);
                    }

                    x_acc = targetX;
                    y_acc = targetY;
                    z_acc = targetZ;
                    //Console.WriteLine($"x_acc={x_acc}, y_acc={y_acc}, z_acc={z_acc}");
                    //Console.WriteLine($"dx={diffX}, XBits={p.XBits}\ndy={diffY}, YBits={p.YBits}\ndz={diffZ}, ZBits={p.ZBits}\n");
                }

                while (temporals.Count % 32 != 0)
                    temporals.Add(false);

                frame.Temporals = temporals.ToArray();
                frames[f] = frame;
            }

            return (frames, sharedPos);
        }

        private static void WriteSignedBits(int value, int bits, List<bool> temporals)
        {
            if (bits == 7)
            {
                byte raw = (byte)value;
                for (int b = 7; b >= 0; b--)
                    temporals.Add(((raw >> b) & 1) != 0);
                return;
            }

            bool sign = value < 0;
            temporals.Add(sign);
            int mag = sign ? value + (1 << bits) : value;

            for (int b = bits - 1; b >= 0; b--)
                temporals.Add(((mag >> b) & 1) != 0);
        }

        private static int Median(List<int> v)
        {
            v.Sort();
            return v[v.Count / 2];
        }

        private static int WrapDiff(int a, int b)
        {
            int d = (a - b) & 0xFF;
            if (d >= 128) d -= 256;
            return d;
        }

        private static int BitsNeededSigned(int v)
        {
            for (int bits = 0; bits <= 6; bits++)
            {
                int min = -(1 << bits);
                int max = (1 << bits) - 1;
                if (v >= min && v <= max)
                    return bits;
            }
            return 7;
        }

        private static byte ToByte(int v, out bool overflow)
        {
            overflow = v < 0 || v > 255;
            return (byte)Math.Clamp(v, 0, 255);
        }

        private static bool IsWindingCorrect(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 normalFromBlender)
        {
            Vector3 e1 = v1 - v0;
            Vector3 e2 = v2 - v0;

            Vector3 geomNormal = Vector3.Cross(e1, e2);
            float dot = Vector3.Dot(geomNormal, normalFromBlender);
            return dot >= 0f; // true = Orientation matches, false = Orientation is reversed
        }

        private static FrameCollision BuildCollision(float[] min, float[] max, float[] collScales)
        {
            const float scale = BASE_COLLISION_SCALE;
            float sx = scale * collScales[0];
            float sy = scale * collScales[1];
            float sz = scale * collScales[2];

            // change axis order (x, y, z) -> (x, z, -y)
            float cx = (min[0] + max[0]) * 0.5f;
            float cy = (min[2] + max[2]) * 0.5f;
            float cz = (-min[1] + -max[1]) * 0.5f;

            float ex = (max[0] - min[0]) * 0.5f;
            float ey = (max[2] - min[2]) * 0.5f;
            float ez = (-max[1] - -min[1]) * 0.5f;

            return new FrameCollision
            {
                U = 0, // ?
                XOffset = ClampToInt32(cx * sx),
                YOffset = ClampToInt32(cy * sy),
                ZOffset = ClampToInt32(cz * sz),
                X1 = ClampToInt32(-ex * sx),
                Y1 = ClampToInt32(-ey * sy),
                Z1 = ClampToInt32(ez * sz),
                X2 = ClampToInt32(ex * sx),
                Y2 = ClampToInt32(ey * sy),
                Z2 = ClampToInt32(-ez * sz)
            };
        }

        private static int ClampToInt32(float v)
        {
            int i = (int)Math.Round(v);
            Math.Clamp(i, int.MinValue, int.MaxValue);
            return i;
        }

        private static List<Frame> BuildFrames(Crash2Json json, int modelEID, float[] scaleFactors, int[] modelScales, bool compressed, ModelSettings settings, Debug debug)
        {
            List<Frame> frames = [];
            bool skipOdd = settings.SkipOddFrames;
            int i = 0;
            foreach (var f in json.frames)
            {
                if (!skipOdd || (skipOdd && (i & 1) == 0))
                    frames.Add(BuildFrame(json, i, modelEID, scaleFactors, modelScales, compressed, debug.DebugMode));
                i++;
            }

            return frames;
        }

        private static AnimationEntry BuildAnimationEntry(Crash2Json json, int modelEID, int animEID, float[] scaleFactors, int[] modelScales, bool compressed, ModelSettings settings, Debug debug)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine($"Building animation with {json.frames.Count} frames...");
            Console.ForegroundColor = ConsoleColor.White;

            float scaleX = 1.0f / scaleFactors[0];
            float scaleY = 1.0f / scaleFactors[1];
            float scaleZ = 1.0f / scaleFactors[2];
            Console.WriteLine($"Scale: X={scaleX}, Y={scaleY}, Z={scaleZ}");

            List<Frame> frames = BuildFrames(json, modelEID, scaleFactors, modelScales, compressed, settings, debug);

            // non-compressed
            return new AnimationEntry(frames, false, animEID);
        }

        //
        // materials
        //
        private static void GetXOff(int colorMode, int value, out int segment, out int xoff)
        {
            int xoffUnit = (1 << (2 - colorMode)) * 64;
            segment = value / xoffUnit;
            xoff = xoffUnit * segment;
        }

        private static ModelTexture BuildModelTexture(Crash2Triangle tri, PackedTexture packed)
        {
            int blendMode = packed.Info.BlendMode;
            int colorMode = packed.Bpp == 4 ? 0 : 1;
            int clutY2 = packed.ClutY >> 2;
            int clutY1 = (packed.ClutY & 0x3) << 2;
            int clutX = packed.ClutX;

            int u1 = Math.Round(tri.uv[0][0]) > 0 ? packed.DestX + packed.Width - 1 : packed.DestX;
            int v1 = Math.Round(tri.uv[0][1]) > 0 ? packed.DestY + packed.Height - 1 : packed.DestY;
            int u2 = Math.Round(tri.uv[1][0]) > 0 ? packed.DestX + packed.Width - 1 : packed.DestX;
            int v2 = Math.Round(tri.uv[1][1]) > 0 ? packed.DestY + packed.Height - 1 : packed.DestY;
            int u3 = Math.Round(tri.uv[2][0]) > 0 ? packed.DestX + packed.Width - 1 : packed.DestX;
            int v3 = Math.Round(tri.uv[2][1]) > 0 ? packed.DestY + packed.Height - 1 : packed.DestY;

            // get x offset adjustments
            GetXOff(colorMode, u1, out int segment, out int xoff);
            u1 -= xoff;
            GetXOff(colorMode, u2, out _, out xoff);
            u2 -= xoff;
            GetXOff(colorMode, u3, out _, out xoff);
            u3 -= xoff;

            // flip y
            var minV = Math.Min(v1, Math.Min(v2, v3));
            var maxV = Math.Max(v1, Math.Max(v2, v3));
            v1 = v1 == minV ? maxV : minV;
            v2 = v2 == minV ? maxV : minV;
            v3 = v3 == minV ? maxV : minV;

            int tpage = packed.TPage;

            return new ModelTexture(
                u1: (byte)u1,
                v1: (byte)v1,
                cluty1: (byte)clutY1,
                clutx: (byte)clutX,
                cluty2: (byte)clutY2,
                u2: (byte)u2,
                v2: (byte)v2,
                colormode: (byte)colorMode,
                blendmode: (byte)blendMode,
                segment: (byte)segment,
                textureoffset: (byte)tpage,
                u3: (byte)u3,
                v3: (byte)v3,
                u4: 0,
                v4: 0
            );
        }

        private static Dictionary<TriangleKey, ModelMaterial> BuildMaterials(Crash2Json json, List<PackedTexture> packedTextures)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine($"Building materials...");
            Console.ForegroundColor = ConsoleColor.White;

            Dictionary<TriangleKey, ModelMaterial> materials = [];

            var normalMaterials = new List<(TriangleKey key, ModelMaterial material)>();
            var animatedMaterials = new List<(TriangleKey key, ModelMaterial material)>();

            var processedKeys = new HashSet<TriangleKey>();

            foreach (Crash2Triangle tri in json.triangles)
            {
                int materialIndex = tri.material;

                TriangleKey key = new(
                    materialIndex,
                    ToUVByte(tri.uv[0]),
                    ToUVByte(tri.uv[1]),
                    ToUVByte(tri.uv[2])
                );

                if (processedKeys.Contains(key))
                    continue;

                processedKeys.Add(key);

                // find the first packed texture for this material index
                PackedTexture? firstPacked = null;
                foreach (PackedTexture packed in packedTextures)
                {
                    if (packed.Index == materialIndex)
                    {
                        firstPacked = packed;
                        break;
                    }
                }

                if (firstPacked == null)
                    continue;

                List<ModelTexture> tex = [];

                // if animated, add split textures too
                if (firstPacked.Value.Info.AnimCount > 0)
                {
                    Console.Write($"Packing split textures for '{firstPacked.Value.Name}'...");
                    int count = 0;
                    foreach (PackedTexture p in packedTextures)
                    {
                        if (p.Name == firstPacked.Value.Name && p.Index == materialIndex)
                        {
                            tex.Add(BuildModelTexture(tri, p));
                            count++;
                        }
                    }
                    Console.WriteLine($"    Done. Total: {count}");

                    animatedMaterials.Add((key, new ModelMaterial(firstPacked.Value.Name, firstPacked.Value.Info, 0, tex)));
                }
                else
                {
                    tex.Add(BuildModelTexture(tri, firstPacked.Value));
                    normalMaterials.Add((key, new ModelMaterial(firstPacked.Value.Name, firstPacked.Value.Info, 0, tex)));
                }
            }

            // assign texture indices to normal materials
            int textureIndex = 1; // start from 1
            foreach (var (key, material) in normalMaterials)
            {
                materials.Add(key, new ModelMaterial(
                    material.Name,
                    material.Info,
                    textureIndex,
                    material.Texture
                ));
                textureIndex++;
            }

            // assign texture indices to animated materials
            int animatedTextureIndex = 0;
            foreach (var (key, material) in animatedMaterials)
            {
                materials.Add(key, new ModelMaterial(
                    material.Name,
                    material.Info,
                    animatedTextureIndex,
                    material.Texture
                ));
                animatedTextureIndex++;
            }

            return materials;
        }

        private static byte ClampToByte(float v)
        {
            int i = (int)Math.Round(v);
            return (byte)Math.Clamp(i, 0, 255);
        }

        private static byte[] ToUVByte(float[] uv)
        {
            byte u = ClampToByte(uv[0]);
            byte v = ClampToByte(uv[1]);
            return [u, v];
        }

        //
        // tpages
        //
        private static (List<TextureChunk>, List<PackedTexture>) BuildTPages(Crash2Json json, string tpageName)
        {
            List<TextureEntry> textures = [];
            List<List<Bitmap>> animTextures = [];

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("Loading textures...");
            Console.ForegroundColor = ConsoleColor.White;

            int indexOffset = 0;

            for (int i = 0; i < json.materials.Count; i++)
            {
                var mat = json.materials[i];
                string filePath = mat.texture;
                string name = mat.name;

                // skip null textures
                if (filePath == null)
                    continue;

                try
                {
                    var (rawImageData, palette, width, height) = TextureConv.ProcessPng(
                        null,
                        filePath,
                        isBGRA: true,
                        oldBpp: -1,
                        quantize: true
                    );

                    int bpp = palette.Length <= 0x40 ? 4 : 8;

                    int faceOrientation = -1;
                    int blendMode = 3; // default
                    int animCount = 0;
                    (int, int) grid = (0, 0);
                    int animDelay = 0;
                    int animSpeed = 0;
                    List<(int index, int repeat)> animSequence = [];

                    var gridMatch = Regex.Match(mat.name, @"_a(\d+)x(\d+)");
                    if (gridMatch.Success)
                    {
                        int cols = int.Parse(gridMatch.Groups[1].Value);
                        int rows = int.Parse(gridMatch.Groups[2].Value);
                        grid = (cols, rows);
                        animCount = cols * rows;
                    }

                    var paramMatches = Regex.Matches(mat.name, @"_([sdrmf])(\d+|=[^_]+)");
                    foreach (Match m in paramMatches)
                    {
                        string type = m.Groups[1].Value;
                        string valueStr = m.Groups[2].Value;

                        switch (type)
                        {
                            case "d":
                                animDelay = int.Parse(valueStr);
                                break;
                            case "s":
                                animSpeed = int.Parse(valueStr);
                                break;
                            case "r":
                                if (valueStr.StartsWith('='))
                                {
                                    var sequenceStr = valueStr[1..]; // remove '='
                                    var parts = sequenceStr.Split(',');

                                    foreach (var part in parts)
                                    {
                                        if (part.Length >= 2)
                                        {
                                            char indexChar = part[0];
                                            string repeatStr = part[1..];

                                            int index = char.IsUpper(indexChar)
                                                ? indexChar - 'A'
                                                : indexChar - 'a';

                                            if (int.TryParse(repeatStr, out int repeat))
                                            {
                                                animSequence.Add((index, repeat));
                                            }
                                        }
                                    }
                                }
                                break;
                            case "m":
                                blendMode = int.Parse(valueStr);
                                break;
                            case "f":
                                faceOrientation = int.Parse(valueStr);
                                break;
                        }
                    }

                    Console.WriteLine($"Loaded texture: {filePath} ({name}), {width,3:d}x{height,2:d}, {bpp} bpp");

                    if (animCount > 0)
                    {
                        // If no explicit sequence is provided, default to a simple sequential animation
                        if (animSequence.Count == 0)
                        {
                            for (int j = 0; j < animCount; j++)
                                animSequence.Add((j, 1));
                        }

                        List<Bitmap> splitTex = SplitPng(filePath, grid.Item1, grid.Item2);

                        int sequenceOffset = 0;
                        foreach (var (texIndex, repeat) in animSequence)
                        {
                            if (texIndex >= splitTex.Count)
                            {
                                Console.WriteLine($"Warning: Texture index {texIndex} out of range for '{name}'");
                                continue;
                            }

                            var image = TextureConv.ProcessPng(
                                splitTex[texIndex],
                                null,
                                isBGRA: true,
                                oldBpp: -1,
                                quantize: true
                            );

                            int w = bpp == 4 ? image.width : image.width * 2;
                            int h = image.height;

                            textures.Add(new TextureEntry
                            {
                                Index = i + indexOffset,
                                Name = mat.name,
                                FilePath = filePath,
                                Data = image.rawImageData,
                                Palette = image.palette,
                                Bpp = bpp,
                                Width = w,
                                Height = h,
                                Info = new MaterialInfo(
                                    faceOrientation,
                                    blendMode,
                                    texIndex,
                                    animCount,         // AnimCount
                                    animSpeed,
                                    animDelay,
                                    repeat,            // AnimRepeat
                                    animSequence.Sum(s => s.repeat)  // TotalAnimRepeats
                                )
                            });

                            Console.WriteLine($"    Sequence[{sequenceOffset}]: texture[{texIndex}] × {repeat} frames");
                            sequenceOffset++;
                        }
                    }
                    else
                    {
                        textures.Add(new TextureEntry
                        {
                            Index = i + indexOffset,
                            Name = mat.name,
                            FilePath = filePath,
                            Data = rawImageData,
                            Palette = palette,
                            Bpp = bpp,
                            Width = bpp == 4 ? width : width * 2,
                            Height = height,
                            Info = new MaterialInfo(faceOrientation, blendMode, 0, 0, 0, 0, 1, 1)
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to load texture '{filePath}': {ex.Message}");
                }
            }

            var tex = AllocateTextureAtlas(textures, tpageName);
            return (tex.Item1, tex.Item2);
        }

        private static List<Bitmap> SplitPng(string path, int cols, int rows)
        {
            Bitmap src = new(path);

            if (src.Width % cols != 0 || src.Height % rows != 0)
                throw new Exception("Image size not divisible by grid.");

            int frameWidth = src.Width / cols;
            int frameHeight = src.Height / rows;

            List<Bitmap> frames = new(cols * rows);

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    Rectangle rect = new(
                        x * frameWidth,
                        y * frameHeight,
                        frameWidth,
                        frameHeight
                    );

                    Bitmap frame = src.Clone(rect, src.PixelFormat);
                    frame.Palette = src.Palette;
                    frames.Add(frame);
                }
            }

            src.Dispose();
            return frames;
        }


        //
        // run
        //
        public static void ConvertModel(string path, ModelSettings settings, Debug debug)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("================================");
            Console.WriteLine("Starting conversion...");
            Console.WriteLine("================================");
            Console.ForegroundColor = ConsoleColor.White;

            List<Crash2Json> jsons = LoadModelJson(path);
            bool compressed = settings.CompressionMethod >= 0;

            string saveDirectory = settings.ExportPath;
            string fileName = Path.GetFileNameWithoutExtension(path);

            Dictionary<string, List<Frame>> allFrames = [];
            Dictionary<string, ModelEntry> modelsToSave = [];
            List<(AnimationEntry, string)> animationsToSave = [];
            Dictionary<int, TextureChunk> texturesToSave = [];
            byte[] fileBytes;
            string savePath;
            string modelName, animName, tpageName;
            Dictionary<string, string> usedNames = [];

            for (int p = 0; p < jsons.Count; p++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine();
                Console.WriteLine($"==== Object [{p}] ====");
                Console.ForegroundColor = ConsoleColor.White;

                Crash2Json json = jsons[p];

                ModelObject obj = settings.ModelObjects[p];
              
                animName = obj.AnimEID;
                modelName = obj.ModelEID;
                var sb = new StringBuilder(modelName);
                sb[4] = 'T';
                tpageName = sb.ToString();

                bool skipExport = p > 0;
                int spVcount = json.markers[0].Count + json.groups[0].Count;

                ModelItem item = settings.ModelItems.FirstOrDefault(m => m.ModelEID == modelName) ?? throw new InvalidOperationException("Model item not found for current model EID.");

                int[] modelScales = item.ModelScales;
                float[] scaleFactors = item.ScaleFactor;

                int modelEID = Entry.ENameToEID(modelName);
                int animEID = Entry.ENameToEID(animName);

                var tex = BuildTPages(json, tpageName);
                List<TextureChunk> tpages = tex.Item1;
                List<PackedTexture> packedTextures = tex.Item2;
                Dictionary<TriangleKey, ModelMaterial> materials = BuildMaterials(json, packedTextures);

                // if the same tpage (checksum) already exists, reuse the name to avoid duplicates
                foreach (var kvp in texturesToSave)
                {
                    if (kvp.Value.HashKey == tpages[0].HashKey)
                    {
                        tpageName = kvp.Value.EName;
                        break;
                    }
                }

                ModelEntry model = BuildModelEntry(json, materials, modelEID, tpageName, modelScales, compressed, spVcount, settings, debug);
                AnimationEntry animation = BuildAnimationEntry(json, modelEID, animEID, scaleFactors, modelScales, compressed, settings, debug);

                if (allFrames.TryAdd(modelName, new List<Frame>(animation.Frames)))
                {
                    // added new - create a copy of the frames list
                }
                else
                {
                    // already exists, append frames
                    allFrames[modelName].AddRange(animation.Frames);
                    Console.WriteLine($"Appended {animation.Frames.Count} frames to model '{modelName}' (total now {allFrames[modelName].Count} frames)");
                }

                // add model to list
                _ = modelsToSave.TryAdd(modelName, model);

                // add animation to list
                animationsToSave.Add((animation, modelName));

                // add tpages to list
                foreach (TextureChunk tpage in tpages)
                {
                    _ = texturesToSave.TryAdd(tpage.HashKey, tpage);
                }
            }

            Dictionary<string, List<Frame>>? allCompressedFrames = [];
            Dictionary<string, List<ModelPosition>>? allPositions = [];

            // compress frames if needed
            if (compressed)
            {
                Console.WriteLine();
                Console.WriteLine("[Model Compression]");
                (List<Frame>, List<ModelPosition>) compressedItem = new();

                foreach (var kvp in allFrames)
                {
                    modelName = kvp.Key;
                    List<Frame> frames = kvp.Value;
                    Console.WriteLine($"Compressing frames for model '{kvp.Key}'...");

                    if (debug.TestCompression)
                    {
                        int bestLength = int.MaxValue;
                        int bestMethod = 0;
                        for (int m = 0; m < 3; m++)
                        {
                            List<Frame> copy = frames
                                .Select(x => new Frame(x.XOffset, x.YOffset, x.ZOffset, x.Unknown, x.ModelEID, x.HeaderSize, x.Collision, x.Vertices, x.SpecialVertexCount, x.Temporals, x.IsNew))
                                .ToList();
                            var comp = CompressFrames(copy, m);

                            int newLength = comp.Item1[0].Temporals.Length;

                            // if better, store it
                            if (newLength < bestLength)
                            {
                                bestLength = newLength;
                                bestMethod = m;
                                compressedItem = comp;
                            }
                            Console.WriteLine($"    Method {m}: {newLength / 8} bytes");
                        }
                        Console.WriteLine($"    Best={bestMethod}");
                    }
                    else
                    {
                        compressedItem = CompressFrames(frames, settings.CompressionMethod);
                        Console.WriteLine($"    Method {settings.CompressionMethod}: {compressedItem.Item1[0].Temporals.Length / 8} bytes");
                    }

                    allCompressedFrames.Add(modelName, compressedItem.Item1);
                    allPositions.Add(modelName, compressedItem.Item2);
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine($"Saving...");
            Console.ForegroundColor = ConsoleColor.White;

            // save models
            foreach (var kvp in modelsToSave)
            {
                ModelEntry model = kvp.Value;
                modelName = kvp.Key;

                if (compressed)
                {
                    List<ModelPosition> positions = allPositions[modelName];
                    foreach (var pos in positions)
                        model.Positions.Add(pos);
                }

                fileBytes = model.Save();
                savePath = Path.Combine(saveDirectory, $"{fileName}_Model_{modelName}.nsentry");
                File.WriteAllBytes(savePath, fileBytes);
                Console.WriteLine($"    Saved model entry: {savePath}");
            }

            // save animations
            Dictionary<string, int> modelOffsets = [];
            foreach (var item in animationsToSave)
            {
                AnimationEntry anim = item.Item1;
                string name = Entry.EIDToEName(anim.EID);
                modelName = item.Item2;

                if (!modelOffsets.ContainsKey(modelName))
                    modelOffsets[modelName] = 0;

                //Console.WriteLine($"Processing animation entry for saving: {name}, Model: {modelName}, {anim.Frames.Count} frames (offset: {modelOffsets[modelName]})");

                if (compressed)
                {
                    List<Frame> frames = allCompressedFrames[modelName];
                    int frameCount = anim.Frames.Count;
                    int currentOffset = modelOffsets[modelName];

                    for (int i = 0; i < frameCount; i++)
                    {
                        anim.Frames[i] = frames[i + currentOffset];
                    }

                    modelOffsets[modelName] += frameCount;
                }

                fileBytes = anim.Save();
                savePath = Path.Combine(saveDirectory, $"{fileName}_Anim_{name}.nsentry");
                File.WriteAllBytes(savePath, fileBytes);
                Console.WriteLine($"    Saved animation entry: {savePath}");
            }

            // save tpages
            foreach (var kvp in texturesToSave)
            {
                TextureChunk tpage = kvp.Value;
                fileBytes = tpage.Save();
                savePath = Path.Combine(saveDirectory, $"{fileName}_Tpage_{tpage.EName}.nschunk");
                File.WriteAllBytes(savePath, fileBytes);
                Console.WriteLine($"    Saved texture page:    {savePath}");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("Conversion completed!");
            Console.ForegroundColor = ConsoleColor.White;
            SystemSounds.Asterisk.Play();
        }
    }

    public class TextureAtlasPacker
    {
        public struct TextureEntry
        {
            public int Index;
            public string Name;
            public string FilePath;
            public byte[] Data;
            public byte[] Palette;
            public int Bpp;
            public int Width;
            public int Height;
            public MaterialInfo Info;
        }

        private struct SkylineNode
        {
            public int X;
            public int Y;
            public int Width;
        }

        private static Dictionary<int, List<SkylineNode>> Skylines = [];

        private static readonly Point AtlasSize = new()
        {
            X = 1024,
            Y = 128
        };
        private static readonly int SegmentWidth = 256;

        private static bool TryPlaceInSegment(
         int seg,
         int w, int h,
         int usableHeight,
         out int localX, out int localY)
        {
            localX = localY = 0;
            var skyline = Skylines[seg];

            int bestY = int.MaxValue;
            int bestX = 0;
            int bestIndex = -1;

            for (int i = 0; i < skyline.Count; i++)
            {
                var n = skyline[i];
                if (w > n.Width) continue;

                int x = n.X;
                int y = n.Y;

                if (y + h > usableHeight) continue;

                if (y < bestY || (y == bestY && x < bestX))
                {
                    bestY = y;
                    bestX = x;
                    bestIndex = i;
                }
            }

            if (bestIndex == -1)
                return false;

            localX = bestX;
            localY = bestY;
            AddSkyline(seg, bestIndex, localX, localY + h, w);
            return true;
        }

        private static void AddSkyline(int seg, int index, int x, int y, int width)
        {
            var skyline = Skylines[seg];
            var node = skyline[index];

            skyline[index] = new SkylineNode
            {
                X = node.X + width,
                Y = node.Y,
                Width = node.Width - width
            };

            skyline.Insert(index, new SkylineNode
            {
                X = x,
                Y = y,
                Width = width
            });

            skyline.RemoveAll(n => n.Width <= 0);

            for (int i = 0; i < skyline.Count - 1; i++)
            {
                var a = skyline[i];
                var b = skyline[i + 1];
                if (a.Y == b.Y && a.X + a.Width == b.X)
                {
                    skyline[i] = new SkylineNode
                    {
                        X = a.X,
                        Y = a.Y,
                        Width = a.Width + b.Width
                    };
                    skyline.RemoveAt(i + 1);
                    i--;
                }
            }
        }

        public static (List<TextureChunk>, List<PackedTexture>) AllocateTextureAtlas(List<TextureEntry> textures, string tpageName)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("Packing textures into atlas...");
            Console.ForegroundColor = ConsoleColor.White;

            List<TextureChunk> tpages = [];
            List<PackedTexture> packedTextures = [];

            Skylines = [];
            int segmentCount = AtlasSize.X / SegmentWidth;
            for (int i = 0; i < segmentCount; i++)
            {
                Skylines[i] =
                [
                    new SkylineNode { X = 0, Y = 0, Width = SegmentWidth }
                ];
            }

            // this identifies a unique physical texture regardless of how many times it's used in the animation sequence
            var physicalTextureMap = new Dictionary<(int Index, int AnimOffset, string FilePath), PackedTexture>();

            // sort textures by size
            List<int> sorted = [];
            for (int i = 0; i < textures.Count; i++)
                sorted.Add(i);

            sorted = sorted
                .OrderBy(i => textures[i].Info.AnimCount > 1) // non-animated > animated
                .ThenByDescending(i => textures[i].Width * textures[i].Height) // descending order by area
                .ToList();

            int eid = Entry.ENameToEID(tpageName);
            byte[] header = {
                0x34, 0x12, 0x01, 0x00,
                (byte)(eid & 0xFF), (byte)((eid >> 8) & 0xFF), (byte)((eid >> 16) & 0xFF), (byte)((eid >> 24) & 0xFF),
                0x05, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
            };

            byte[] newchunk = new byte[0x10000];
            Array.Copy(header, 0, newchunk, 0, header.Length);
            TextureChunk tpage = new(newchunk);
            int tpageIndex = 0; // temp

            int _4bppCount = 1; // reserve 1 for tpage header
            int _8bppCount = 0;
            HashSet<string> unique = [];
            foreach (var tex in textures)
            {
                if (unique.Contains(tex.FilePath))
                    continue;
                unique.Add(tex.FilePath);

                if (tex.Bpp == 4)
                    _4bppCount++;
                else if (tex.Bpp == 8)
                    _8bppCount++;
            }

            int curClutX = 1,
                curClutY = 0,
                oldClutX = -1,
                oldClutY = -1;
            int clutYRow = (_4bppCount + 15) / 16;
            int ClutHeight = clutYRow + _8bppCount;
            int usableHeight = AtlasSize.Y - ClutHeight;

            if (textures.Count > 0)
            {
                Console.WriteLine($"4bpp textures: {_4bppCount - 1}, 8bpp textures: {_8bppCount}");
                Console.WriteLine($"CLUT Height: {ClutHeight} rows");
            }

            foreach (int i in sorted)
            {
                var tex = textures[i];
                var info = tex.Info;

                if (info.AnimDelay > 0)
                {
                    // if animated texture with delay, search for a non-delayed texture
                    string baseName = Regex.Replace(tex.Name, @"_[d]\d+", "");
                    bool found = false;
                    foreach (var pt in packedTextures.ToList())
                    {
                        if (pt.Name == baseName &&
                            pt.Info.AnimOffset == info.AnimOffset)
                        {
                            packedTextures.Add(new PackedTexture(
                                index: tex.Index,
                                name: tex.Name,
                                filePath: pt.FilePath,
                                bpp: pt.Bpp,
                                clutX: pt.ClutX,
                                clutY: pt.ClutY,
                                destX: pt.DestX,
                                destY: pt.DestY,
                                w: pt.Width,
                                h: pt.Height,
                                tpage: pt.TPage,
                                info: info
                            ));

                            found = true;
                            break;
                        }
                    }

                    if (!found)
                        throw new Exception($"Could not find a non-delayed counterpart for animated texture '{tex.FilePath}'.");

                    continue;
                }

                // check if an identical texture (same file and same anim offset) has already been placed, if so reuse it
                var physicalKey = (tex.Index, info.AnimOffset, tex.FilePath);

                if (physicalTextureMap.TryGetValue(physicalKey, out var existingPacked))
                {
                    var reusedPacked = new PackedTexture(
                        index: tex.Index,
                        name: tex.Name,
                        filePath: existingPacked.FilePath,
                        bpp: existingPacked.Bpp,
                        clutX: existingPacked.ClutX,
                        clutY: existingPacked.ClutY,
                        destX: existingPacked.DestX,
                        destY: existingPacked.DestY,
                        w: existingPacked.Width,
                        h: existingPacked.Height,
                        tpage: existingPacked.TPage,
                        info: info  // use the current texture's info, which may differ in anim count/repeat, but has the same anim offset
                    );

                    for (int r = 0; r < info.AnimRepeat; r++)
                    {
                        packedTextures.Add(reusedPacked);
                    }

                    Console.WriteLine($"Reused texture [{info.AnimOffset}] {Path.GetFileName(tex.FilePath)} ({tex.Name}) × {info.AnimRepeat} frames (same as offset {existingPacked.Info.AnimOffset})");
                    continue;
                }

                // place the texture in the atlas
                Point result = new();
                bool placed = false;
                for (int seg = 0; seg < segmentCount && !placed; seg++)
                {
                    if (TryPlaceInSegment(seg, tex.Width, tex.Height, usableHeight, out int lx, out int ly))
                    {
                        int worldX = seg * SegmentWidth + lx;
                        int topY = AtlasSize.Y - (ly + tex.Height);

                        result.X = worldX;
                        result.Y = topY;

                        placed = true;
                    }
                }
                if (!placed)
                    throw new Exception("Atlas overflow (all segments full)");

                string filePath = tex.FilePath;
                int bpp = tex.Bpp;
                bool addNewClut = true;
                if (info.AnimCount > 0)
                {
                    if (info.AnimOffset < info.AnimCount - 1)
                        addNewClut = false;
                }

                if (bpp == 8)
                {
                    if (oldClutX == -1 || oldClutY == -1)
                    {
                        // save CLUT position
                        oldClutX = curClutX;
                        oldClutY = curClutY;
                    }
                    curClutX = 0;
                    curClutY = clutYRow;
                }
                else
                {
                    if (oldClutX != -1 || oldClutY != -1)
                    {
                        // restore CLUT position
                        curClutX = oldClutX;
                        curClutY = oldClutY;
                        oldClutX = -1;
                        oldClutY = -1;
                    }
                }

                int width = bpp == 8 ? tex.Width / 2 : tex.Width;
                int height = tex.Height;

                int destX = result.X;
                int destY = result.Y;

                tpage.Data = TextureConv.ReplaceTextureFromViewer(tpage.Data, tex.Data, tex.Palette, width, height, destX, destY, addNewClut, bpp, curClutX, curClutY);

                var packedTex = new PackedTexture(
                    index: tex.Index,
                    name: tex.Name,
                    filePath: filePath,
                    bpp: bpp,
                    clutX: curClutX,
                    clutY: curClutY,
                    destX: bpp == 8 ? result.X / 2 : result.X,
                    destY: destY,
                    w: width,
                    h: height,
                    tpage: tpageIndex,
                    info: info
                );

                // store the physical texture info
                physicalTextureMap[physicalKey] = packedTex;

                for (int r = 0; r < info.AnimRepeat; r++)
                {
                    packedTextures.Add(packedTex);
                }

                Console.WriteLine($"Allocated texture [{info.AnimOffset}] {Path.GetFileName(tex.FilePath)} ({tex.Name}) at ({result.X,4:d}, {result.Y,3:d}), {width,3:d}x{height,3:d}, CLUT: Y{curClutY,2:d} - X{curClutX,2:d} × {info.AnimRepeat} frames");

                if (addNewClut)
                {
                    if (bpp == 8)
                    {
                        clutYRow += 1;
                    }
                    else
                    {
                        curClutX += 1;
                        if (curClutX > 15)
                        {
                            curClutX = 0;
                            curClutY += 1;
                        }
                    }
                }
            }

            int correct_checksum = Chunk.CalculateChecksum(tpage.Data);
            BitConv.ToInt32(tpage.Data, 12, correct_checksum);

            tpages.Add(tpage);
            return (tpages, packedTextures);
        }
    }
}

