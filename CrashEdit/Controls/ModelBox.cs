using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Globalization;
using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using HslColor = Cyotek.Windows.Forms.HslColor;

namespace CrashEdit.CE.Controls
{
    public partial class ModelBox : UserControl
    {
        private readonly DataGridViewCellStyle styleDefault = new DataGridViewCellStyle
        {
            ForeColor = Color.Gainsboro
        };
        private readonly DataGridViewCellStyle styleRegionEnd = new DataGridViewCellStyle
        {
            ForeColor = Color.Turquoise
        };
        private readonly DataGridViewCellStyle styleIndex = new DataGridViewCellStyle
        {
            ForeColor = Color.Gray
        };

        private dynamic controller;
        private dynamic model;
        private TextureChunk chunk { get; set; }

        private readonly List<dynamic> structs = new List<dynamic>();

        private Rectangle selectedregion;

        private DarkToolTip tipReloadTPage;

        private CancellationTokenSource _debounceTokenSource;
        private readonly int DebounceDelay = 50;

        private bool isScenery;
        private bool globalControlMode;

        private bool simpleMode;
        private bool BGRAMode;
        private bool replaceCLUT;
        private int selectedRegionX;
        private int selectedRegionY;
        private int currentColorMode;

        private readonly int ColTexture = 0;
        private readonly int ColColor = 1;
        private readonly int ColAnimated = 2;
        private readonly int ColPositionKey = 3;
        private readonly int ColTriangleType = 4;
        private readonly int ColTriangleSubtype = 5;
        private readonly int ColUnknown = 6;
        private readonly int ColFlag = 7;
        private readonly int ColType = 8;

        private readonly int ColVertexA = 0;
        private readonly int ColVertexB = 1;
        private readonly int ColVertexC = 2;
        private readonly int ColColorA = 3;
        private readonly int ColColorB = 4;
        private readonly int ColColorC = 5;
        private readonly int ColTriTexture = 6;
        private readonly int ColTriType = 7;
        private readonly int ColTriSubtype = 8;
        private readonly int ColTriAnimated = 9;

        private readonly int ColPage = 0;
        private readonly int ColClutX = 1;
        private readonly int ColClutY = 2;
        private readonly int ColLeft = 3;
        private readonly int ColTop = 4;
        private readonly int ColWidth = 5;
        private readonly int ColHeight = 6;
        private readonly int ColX1 = 7;
        private readonly int ColX2 = 8;
        private readonly int ColX3 = 9;
        private readonly int ColX4 = 10;
        private readonly int ColY1 = 11;
        private readonly int ColY2 = 12;
        private readonly int ColY3 = 13;
        private readonly int ColY4 = 14;
        private readonly int ColBlendMode = 15;
        private readonly int ColColorMode = 16;

        private readonly int ColOffset = 0;
        private readonly int ColIsLOD = 1;
        private readonly int ColMask = 2;
        private readonly int ColDelay = 3;
        private readonly int ColLatency = 4;
        private readonly int ColLeap = 5;
        private readonly int ColLOD0 = 6;
        private readonly int ColLOD1 = 7;
        private readonly int ColLOD2 = 8;
        private readonly int ColLOD3 = 9;
        private readonly int ColLOD4 = 10;
        private readonly int ColLOD5 = 11;
        private readonly int ColLOD6 = 12;
        private readonly int ColLOD7 = 13;

        private readonly int ColIndex = 0;
        private readonly int ColX = 1;
        private readonly int ColY = 2;
        private readonly int ColZ = 3;
        private readonly int ColXBits = 4;
        private readonly int ColYBits = 5;
        private readonly int ColZBits = 6;

        private double MasterHue => colorEditorGlobal.HslColor.H;
        private double MasterSaturation => colorEditorGlobal.HslColor.S;
        private double MasterLightness => colorEditorGlobal.HslColor.L;

        private readonly Color clrBackground = Color.FromArgb(40, 40, 40);
        private readonly Color clrAltBackground = Color.FromArgb(34, 34, 34);
        private readonly Color clrSelectionBackground = Color.FromArgb(70, 70, 70);
        private readonly Color clrText = Color.Gainsboro;

        internal Stack<bool> dirty = new Stack<bool>();
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        #region Init

        public ModelBox(ModelEntryController controller)
        {
            MainInit(controller, false);
        }

        public ModelBox(SceneryEntryController controller)
        {
            MainInit(controller, true);
        }

        private void MainInit(object controller, bool isScenery)
        {
            InitializeComponent();
            DoubleBuffered = true;
            this.controller = controller;
            this.isScenery = isScenery;

            dirty.Push(true);
            if (isScenery)
            {
                model = this.controller.SceneryEntry;

                fraScales.Visible = false;
                lblModelInfo.Visible = false;

                SetCVal(numOffsetX, model.XOffset);
                SetCVal(numOffsetY, model.YOffset);
                SetCVal(numOffsetZ, model.ZOffset);

                tabModel.Controls.Remove(tbpPolygons);
                tbpPolygons.Dispose();
                tabModel.Controls.Remove(tbpPositions);
                tbpPositions.Dispose();
            }
            else
            {
                model = this.controller.ModelEntry;

                fraOffsets.Visible = false;

                SetCVal(numScaleX, model.ScaleX);
                SetCVal(numScaleY, model.ScaleY);
                SetCVal(numScaleZ, model.ScaleZ);
                UpdateInfo();

                if (model.Positions == null)
                {
                    tabModel.Controls.Remove(tbpPositions);
                    tbpPositions.Dispose();
                }
            }

            if (!(model.Textures.Count > 0))
            {
                tabModel.Controls.Remove(tbpTextures);
                tbpTextures.Dispose();

            }
            if (!(model.AnimatedTextures.Count > 0))
            {
                tabModel.Controls.Remove(tbpExtendedTextures);
                tbpExtendedTextures.Dispose();
            }
            dirty.Pop();
        }

        private void tbpPolygons_Enter(object sender, EventArgs e)
        {
            DoubleBufferedDataGridView.Initialize(dgvStructs);
            DoubleBufferedDataGridView.Initialize(dgvPolygons);
            UpdateStructs();
            UpdatePolygons();
            tbpPolygons.Enter -= tbpPolygons_Enter;
        }

        private async void tbpColors_Enter(object sender, EventArgs e)
        {
            ResetColorSliders();
            await UpdateColorListAsync();
            tbpColors.Enter -= tbpColors_Enter;
        }

        private async void tbpTextures_Enter(object sender, EventArgs e)
        {
            tipReloadTPage = new DarkToolTip();
            tipReloadTPage.SetToolTip(rbtReloadTPage, "Reload");
            DoubleBufferedDataGridView.Initialize(dgvTextures);
            if (isScenery)
            {
                dgvTextures.Width = 612;
                pnTextureControls.Location = new Point(759, 0);
            }
            CreateTextureListColumns();
            UpdateTPageList();
            await UpdateTextureListAsync(true);
            if (dgvTextures.Rows.Count > 0)
            {
                UpdateTPageButtons();
                fraSwitches.Enabled =
                fraReplace.Enabled =
                fraReplaceTexture.Enabled = true;
                trkPictureSize.Visible = true;
                pnPicture.AutoScroll = true;
            }

            BGRAMode =
            replaceCLUT = true;

            numReplaceTo.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);

            tbpTextures.Enter -= tbpTextures_Enter;
        }

        private async void tbpExtendedTextures_Enter(object sender, EventArgs e)
        {
            DoubleBufferedDataGridView.Initialize(dgvExtendedTextures);
            CreateExtendedTextureColumns();
            await UpdateExtendedTextureAsync();

            tbpExtendedTextures.Enter -= tbpExtendedTextures_Enter;
        }

        private async void tbpPositions_Enter(object sender, EventArgs e)
        {
            DoubleBufferedDataGridView.Initialize(dgvPositions);
            CreatePositionColumns();
            await UpdatePositionAsync();

            tbpPositions.Enter -= tbpPositions_Enter;
        }
        #endregion

        #region General

        internal void SetCVal(DarkNumericUpDown num, long val)
        {
            dirty.Push(true);
            // this is fucking stupid
            if (num.Hexadecimal)
            {
                if (val > 0xFFFFFFFF) val = 0xFFFFFFFF;
                else if (val < 0) val &= 0xFFFFFFFF;
                num.Value = unchecked((uint)val);
            }
            else
            {
                if (val > 0xFFFFFFFF) val = 0x7FFFFFFF;
                else if (val > 0x7FFFFFFF) val = -0x100000000 + val;
                else if (val < -0x80000000) val = -0x80000000;
                num.Value = unchecked((int)val);
            }
            dirty.Pop();
        }

        private void UpdateInfo()
        {
            if (model.Positions == null)
            {
                lblModelInfo.Text = string.Format("Polygon count: {0}\nVertex count: {1}", model.PolyCount, model.VertexCount);
            }
            else
            {
                int totalbits = model.Positions.Count * 8 * 3;
                int bits = 0;
                foreach (ModelPosition pos in model.Positions)
                {
                    bits += 1 + pos.XBits;
                    bits += 1 + pos.YBits;
                    bits += 1 + pos.ZBits;
                }
                lblModelInfo.Text = string.Format("Polygon count: {0}\nVertex count: {1}\nCompression ratio: {2:P1} ({3}/{4})", model.PolyCount, model.VertexCount, (float)bits / totalbits, bits, totalbits);
            }
        }

        private void numScaleX_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                SetCVal(numScaleX, (long)numScaleX.Value);
                model.ScaleX = ((long)numScaleX.Value).UInt32ToInt32();
            }
        }

        private void numScaleY_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                SetCVal(numScaleY, (long)numScaleY.Value);
                model.ScaleY = ((long)numScaleY.Value).UInt32ToInt32();
            }
        }

        private void numScaleZ_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                SetCVal(numScaleZ, (long)numScaleZ.Value);
                model.ScaleZ = ((long)numScaleZ.Value).UInt32ToInt32();
            }
        }

        private void chkScalesAsHex_CheckedChanged(object sender, EventArgs e)
        {
            numScaleX.Hexadecimal =
            numScaleY.Hexadecimal =
            numScaleZ.Hexadecimal = chkScalesShowAsHex.Checked;
            SetCVal(numScaleX, (long)numScaleX.Value);
            SetCVal(numScaleY, (long)numScaleY.Value);
            SetCVal(numScaleZ, (long)numScaleZ.Value);
        }

        private void numOffsetX_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                SetCVal(numOffsetX, (long)numOffsetX.Value);
                model.XOffset = ((long)numOffsetX.Value).UInt32ToInt32();
            }
        }

        private void numOffsetY_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                SetCVal(numOffsetY, (long)numOffsetY.Value);
                model.YOffset = ((long)numOffsetY.Value).UInt32ToInt32();
            }
        }

        private void numOffsetZ_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                SetCVal(numOffsetZ, (long)numOffsetZ.Value);
                model.ZOffset = ((long)numOffsetZ.Value).UInt32ToInt32();
            }
        }

        private void chkOffsetsAsHex_CheckedChanged(object sender, EventArgs e)
        {
            numOffsetX.Hexadecimal =
            numOffsetY.Hexadecimal =
            numOffsetZ.Hexadecimal = chkOffsetsShowAsHex.Checked;
            SetCVal(numOffsetX, (long)numOffsetX.Value);
            SetCVal(numOffsetY, (long)numOffsetY.Value);
            SetCVal(numOffsetZ, (long)numOffsetZ.Value);
        }
        #endregion

        #region Structs

        private void UpdateStructs()
        {
            dgvStructs.ColumnHeadersHeight = 36;
            dgvStructs.Columns.Add("TextureIndex", "Texture /\nColor1");
            dgvStructs.Columns.Add("ColorIndex", "Color /\nColor2");
            dgvStructs.Columns.Add("Animated", "Animated");
            dgvStructs.Columns.Add("PositionKey", "Key");
            dgvStructs.Columns.Add("TriangleType", "TriType");
            dgvStructs.Columns.Add("TriangleType", "TriSubtype");
            dgvStructs.Columns.Add("Unknown", "Unknown");
            dgvStructs.Columns.Add("Flag", "Flag");
            dgvStructs.Columns.Add("Type", "Type");
            for (int i = 0; i < model.PolyData.Length; i++)
            {
                ModelStruct s = ModelEntry.ConvertPolyItem(model.PolyData[i]);
                DataGridViewRow row = new DataGridViewRow();
                if (s == null) // footer
                {
                    structs.Add(null!);
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                    row.CreateCells(dgvStructs, "FOOTER");
                }
                else if (s is ModelColor c) // color
                {
                    structs.Add(c);
                    //lastcolor = i;
                    row.DefaultCellStyle.ForeColor = Color.Turquoise;
                    row.CreateCells(dgvStructs, c.Color1, c.Color2);
                }
                else if (s is ModelTriangle t) // index
                {
                    structs.Add(t);
                    row.CreateCells(dgvStructs, t.TextureIndex, t.ColorIndex, t.Animated, t.PositionKey, t.TriangleType, t.TriangleSubtype, t.Unknown, t.Flag, t.Type);
                }
                dgvStructs.Rows.Add(row);
            }
            foreach (DataGridViewColumn column in dgvStructs.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 60;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void UpdatePolygons()
        {
            dgvPolygons.Columns.Add("VertexA", "Vertex A");
            dgvPolygons.Columns.Add("VertexB", "Vertex B");
            dgvPolygons.Columns.Add("VertexC", "Vertex C");
            dgvPolygons.Columns.Add("ColorA", "Color A");
            dgvPolygons.Columns.Add("ColorB", "Color B");
            dgvPolygons.Columns.Add("ColorC", "Color C");
            dgvPolygons.Columns.Add("Texture", "Texture");
            dgvPolygons.Columns.Add("Type", "Type");
            dgvPolygons.Columns.Add("Subtype", "Subtype");
            dgvPolygons.Columns.Add("Animated", "Animated");
            foreach (var tri in model.Triangles)
            {
                dgvPolygons.Rows.Add(tri.Vertex[0], tri.Vertex[1], tri.Vertex[2], tri.Color[0], tri.Color[1], tri.Color[2], tri.Texture, tri.Type, tri.Subtype, tri.Animated);
            }
            foreach (DataGridViewColumn column in dgvPolygons.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 60;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void dgvStructs_SelectionChanged(object sender, EventArgs e)
        {
            if (!(dgvStructs.SelectedCells.Count > 0)) return;

            var row = dgvStructs.Rows[dgvStructs.SelectedCells[0].RowIndex];
            if (Convert.ToString(row.Cells[ColTexture].Value) == "FOOTER")
            {
                lblStruct.ForeColor = Color.Gray;
                lblStruct.Text = "[FOOTER]";
            }
            else if (string.IsNullOrEmpty(Convert.ToString(row.Cells[ColType].Value)))
            {
                lblStruct.ForeColor = Color.Turquoise;
                lblStruct.Text = "[ModelColor]";
            }
            else
            {
                lblStruct.ForeColor = SystemColors.ControlText;
                lblStruct.Text = "[ModelTriangle]";
            }
            lblStruct.Visible = true;


        }

        private void dgvStructs_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string? text = Convert.ToString(dgvStructs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            if (string.IsNullOrEmpty(text) || text == "FOOTER")
            {
                e.Cancel = true;
            }
        }

        private void dgvStructsGetMaxValue(int columnIndex, out int minValue, out int maxValue)
        {
            maxValue = 0; minValue = 0;
            switch (columnIndex)
            {
                case 0: // Texture / Color1
                case 1: // Color / Color2
                case 3: // PositionKey
                case 6: // Unknown
                    maxValue = 255;
                    break;
                case 4: // TriangleType
                    maxValue = 2;
                    break;
                case 5: // TriangleSubtype
                    maxValue = 3;
                    break;
            }
        }

        private void dgvStructs_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!(dgvStructs.SelectedCells.Count > 0)) return;
            string inputValue = e.FormattedValue.ToString();
            if (string.IsNullOrEmpty(inputValue) || inputValue == "FOOTER") return;

            if (e.ColumnIndex == ColType)
            {
                if (!(inputValue == "0" || inputValue == "1" || inputValue.Equals("Original", StringComparison.InvariantCultureIgnoreCase) || inputValue.Equals("Duplicate", StringComparison.InvariantCultureIgnoreCase)))
                {
                    DarkMessageBox.ShowError("The value must be 'Original' or 'Duplicate'.", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
            else if (e.ColumnIndex == ColAnimated || e.ColumnIndex == ColFlag)
            {
                if (!(inputValue.Equals("True", StringComparison.InvariantCultureIgnoreCase) || inputValue.Equals("False", StringComparison.InvariantCultureIgnoreCase)))
                {
                    DarkMessageBox.ShowError("The value must be 'True' or 'False'.", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
            else
            {
                if (int.TryParse(inputValue, out int newValue))
                {
                    dgvStructsGetMaxValue(e.ColumnIndex, out int minValue, out int maxValue);
                    if (newValue > maxValue)
                    {
                        DarkMessageBox.ShowError($"The value must be less than or equal to {maxValue}.", Resources.Title_InputError);
                        e.Cancel = true;
                    }
                    else if (newValue < minValue)
                    {
                        DarkMessageBox.ShowError($"The value must be greater than or equal to {minValue}.", Resources.Title_InputError);
                        e.Cancel = true;
                    }
                }
                else
                {
                    DarkMessageBox.ShowError($"Invalid input. Please enter an integer.", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
        }

        private void dgvStructs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            Console.WriteLine($"Old: {model.PolyData[e.RowIndex]:X}");
            var cell = dgvStructs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            if (dgvStructs.Rows[e.RowIndex].Cells[8].Value == null)
            {
                ModelColor str = structs[e.RowIndex];

                switch (e.ColumnIndex)
                {
                    case 0: // Color1
                        str.Color1 = Convert.ToByte(cell);
                        break;
                    case 1: // Color2
                        str.Color2 = Convert.ToByte(cell);
                        break;
                }
                model.PolyData[e.RowIndex] = str.Save();
                Console.WriteLine($"New: {str.Save():X}");
            }
            else
            {
                ModelTriangle str = structs[e.RowIndex];

                switch (e.ColumnIndex)
                {
                    case 0: // TextureIndex
                        str.TextureIndex = Convert.ToByte(cell);
                        break;
                    case 1: // ColorIndex
                        str.ColorIndex = Convert.ToByte(cell);
                        break;
                    case 2: // Animated
                        str.Animated = Convert.ToBoolean(cell);
                        break;
                    case 3: // PositionKey
                        str.PositionKey = Convert.ToByte(cell);
                        break;
                    case 4: // TriangleType
                        str.TriangleType = Convert.ToByte(cell);
                        break;
                    case 5: // TriangleSubtype
                        str.TriangleSubtype = Convert.ToByte(cell);
                        break;
                    case 6: // Unknown
                        str.Unknown = Convert.ToByte(cell);
                        break;
                    case 7: // Flag
                        str.Flag = Convert.ToBoolean(cell);
                        break;
                    case 8: // Type
                        if (cell.ToString() == "0" || cell.ToString().Equals("Original", StringComparison.InvariantCultureIgnoreCase))
                        {
                            cell = 0;
                        }
                        else
                        {
                            cell = 1;
                        }
                        str.Type = (ModelTriangle.IndexType)Enum.Parse(typeof(ModelTriangle.IndexType), cell.ToString());
                        break;
                }
                model.PolyData[e.RowIndex] = str.Save();
                Console.WriteLine($"New: {str.Save():X}");
            }
        }

        private void dgvPolygonsGetMaxValue(int columnIndex, out int minValue, out int maxValue)
        {
            maxValue = 0; minValue = 0;
            switch (columnIndex)
            {
                case 0: // VertexA
                case 1: // VertexB
                case 2: // VertexC
                case 3: // ColorA
                case 4: // ColorB
                case 5: // ColorC
                case 6: // Texture
                    maxValue = int.MaxValue;
                    minValue = int.MinValue;
                    break;
                case 7: // TriangleType
                    maxValue = 2;
                    break;
                case 8: // TriangleSubtype
                    maxValue = 3;
                    break;
            }
        }

        private void dgvPolygons_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!(dgvStructs.SelectedCells.Count > 0)) return;
            string inputValue = e.FormattedValue.ToString();

            if (e.ColumnIndex == ColTriAnimated)
            {
                if (!(inputValue.Equals("True", StringComparison.InvariantCultureIgnoreCase) || inputValue.Equals("False", StringComparison.InvariantCultureIgnoreCase)))
                {
                    DarkMessageBox.ShowError("The value must be 'True' or 'False'.", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
            else
            {
                if (int.TryParse(inputValue, out int newValue))
                {
                    dgvPolygonsGetMaxValue(e.ColumnIndex, out int minValue, out int maxValue);
                    if (newValue > maxValue)
                    {
                        DarkMessageBox.ShowError($"The value must be less than or equal to {maxValue}.", Resources.Title_InputError);
                        e.Cancel = true;
                    }
                    else if (newValue < minValue)
                    {
                        DarkMessageBox.ShowError($"The value must be greater than or equal to {minValue}.", Resources.Title_InputError);
                        e.Cancel = true;
                    }
                }
                else
                {
                    DarkMessageBox.ShowError($"Invalid input. Please enter an integer.", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
        }

        private void dgvPolygons_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var tri = model.Triangles[e.RowIndex];
            var row = dgvPolygons.Rows[e.RowIndex];

            switch (e.ColumnIndex)
            {
                case 0: // Vertex A
                    tri.Vertex[0] = Convert.ToInt32(row.Cells[e.ColumnIndex].Value);
                    break;
                case 1: // Vertex B
                    tri.Vertex[1] = Convert.ToInt32(row.Cells[e.ColumnIndex].Value);
                    break;
                case 2: // Vertex C
                    tri.Vertex[2] = Convert.ToInt32(row.Cells[e.ColumnIndex].Value);
                    break;
                case 3: // Color A
                    tri.Color[0] = Convert.ToInt32(row.Cells[e.ColumnIndex].Value);
                    break;
                case 4: // Color B
                    tri.Color[1] = Convert.ToInt32(row.Cells[e.ColumnIndex].Value);
                    break;
                case 5: // Color C
                    tri.Color[2] = Convert.ToInt32(row.Cells[e.ColumnIndex].Value);
                    break;
                case 6: // Texture
                    tri.Texture = Convert.ToInt32(row.Cells[e.ColumnIndex].Value);
                    break;
                case 7: // Type
                    tri.Type = Convert.ToInt32(row.Cells[e.ColumnIndex].Value);
                    break;
                case 8: // Subtype
                    tri.Subtype = Convert.ToInt32(row.Cells[e.ColumnIndex].Value);
                    break;
                case 9: // Animated
                    tri.Animated = Convert.ToBoolean(row.Cells[e.ColumnIndex].Value);
                    break;

            }
        }

        private void dgvPolygons_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                if (dgvPolygons.SelectedCells.Count == 0) return;

                int selectedColumnIndex = dgvPolygons.SelectedCells[0].ColumnIndex;
                string clipboardData = Clipboard.GetText();
                string[] rows = clipboardData.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < rows.Length && i < dgvPolygons.Rows.Count; i++)
                {
                    dgvPolygons.Rows[i].Cells[selectedColumnIndex].Value = rows[i];
                }
            }
        }
        #endregion

        #region Colors

        private async Task UpdateColorListAsync()
        {
            if (lstColor.Items.Count > 0) return;

            int colorCount = model.Colors.Count;

            if (isScenery)
            {
                var rows = await Task.Run(() =>
                {
                    var rowsToAdd = new ConcurrentBag<(int Index, ListViewItem Row)>();

                    Parallel.For(0, colorCount, i =>
                    {
                        var color = model.Colors[i];
                        byte[] item = { color.Red, color.Green, color.Blue };

                        ListViewItem lsi = new();
                        lsi.Text = Convert.ToHexString(item);
                        lsi.BackColor = Color.FromArgb(item[0], item[1], item[2]);
                        lsi.ForeColor = getBrightness(lsi.BackColor) >= 0.5 ? Color.Black : Color.White;
                        lsi.Tag = i;

                        lsi.SubItems.Add(Convert.ToHexString(item)); // Original color
                        lsi.SubItems.Add(Convert.ToHexString(item)); // Copy

                        rowsToAdd.Add((i, lsi));
                    });
                    return rowsToAdd.OrderBy(pair => pair.Index).Select(pair => pair.Row).ToList();
                });

                lstColor.BeginUpdate();
                lstColor.Items.AddRange(rows.ToArray());
                lstColor.EndUpdate();
            }
            else
            {
                int colorcount = model.Colors.Count;
                for (int i = 0; i < colorcount; ++i)
                {
                    byte[] item = [model.Colors[i].Red, model.Colors[i].Green, model.Colors[i].Blue];
                    ListViewItem lsi = new();
                    lsi.Text = Convert.ToHexString(item);
                    lsi.BackColor = Color.FromArgb(item[0], item[1], item[2]);
                    lsi.ForeColor = getBrightness(lsi.BackColor) >= 0.5 ? Color.Black : Color.White;
                    lsi.Tag = i;
                    lsi.SubItems.Add(Convert.ToHexString(item)); // Original color
                    lsi.SubItems.Add(Convert.ToHexString(item)); // Copy
                    lstColor.Items.Add(lsi);
                }
            }
        }

        private async Task ResetColorListAsync()
        {
            if (isScenery)
            {
                await Task.Run(() =>
                {
                    Parallel.For(0, lstColor.Items.Count, i =>
                    {
                        byte[] item = StringToByteArray(lstColor.Items[i].SubItems[1].Text);
                        SetModelColor(Color.FromArgb(item[0], item[1], item[2]), i);
                    });
                });
            }
            else
            {
                for (int i = 0; i < lstColor.Items.Count; i++)
                {
                    byte[] item = StringToByteArray(lstColor.Items[i].SubItems[1].Text);
                    SetModelColor(Color.FromArgb(item[0], item[1], item[2]), i);
                }
            }
            lstColor.Invoke(() => lstColor.Items.Clear());
            await UpdateColorListAsync();
        }

        private void UpdateColorCopy()
        {
            lstColor.Invoke(() =>
            {
                for (int i = 0; i < lstColor.Items.Count; i++)
                {
                    lstColor.Items[i].SubItems[1].Text = lstColor.Items[i].SubItems[0].Text;
                }
            });
        }

        private void lstColor_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter;
            using (StringFormat sf = new StringFormat())
            {
                bool isSelected = e.Item.Selected;

                e.DrawBackground();

                sf.Alignment = StringAlignment.Center;

                if (isSelected)
                {
                    //e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
                    using (Brush brush = new SolidBrush(e.Item.ForeColor))
                    {
                        e.Graphics.DrawString(e.Item.Text, lstColor.Font, brush, e.Bounds, sf);
                    }
                }
                else
                {
                    e.DrawText(flags);
                }
            }
        }

        private void lstColor_MouseDown(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.Location;
            for (int i = 0; i < lstColor.Items.Count; i++)
            {
                ListViewItem item = lstColor.Items[i];
                Rectangle itemBounds = item.Bounds;
                if (itemBounds.Contains(mousePosition))
                {
                    lstColor.SelectedItems.Clear();
                    item.Selected = true;
                    break;
                }
            }
        }

        private void lstColor_MouseUp(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.Location;
            for (int i = 0; i < lstColor.Items.Count; i++)
            {
                ListViewItem item = lstColor.Items[i];
                Rectangle itemBounds = item.Bounds;
                if (itemBounds.Contains(mousePosition))
                {
                    lstColor.SelectedItems.Clear();
                    item.Selected = true;
                    break;
                }
            }
        }

        private void SetModelColor(Color color, int i)
        {
            SceneryColor updatedColor = model.Colors[i];
            updatedColor.Red = color.R;
            updatedColor.Green = color.G;
            updatedColor.Blue = color.B;
            updatedColor.Extra = 0;
            model.Colors[i] = updatedColor;
        }

        private void OnValueChanged(bool isAll)
        {
            OnValueChanged(isAll, Color.Empty);
        }

        private void OnValueChanged(bool isAll, Color clr)
        {
            _debounceTokenSource?.Cancel();

            _debounceTokenSource = new CancellationTokenSource();

            Task.Delay(DebounceDelay).ContinueWith(t =>
            {
                if (!_debounceTokenSource.Token.IsCancellationRequested)
                {
                    Invoke(new Action(() =>
                    {
                        if (isAll)
                            UpdateAllColors();
                        else
                            UpdateSelectedColor(clr);
                    }));
                }
            }, _debounceTokenSource.Token);
        }

        private void UpdateSelectedColor(Color clr)
        {
            if (lstColor.SelectedItems.Count <= 0)
            {
                pnSliders.Enabled = false;
                return;
            }

            Color color = clr;
            var i = (int)lstColor.SelectedItems[0].Tag;
            SetModelColor(color, i);
            lstColor.Items[i].SubItems[0].Text = Convert.ToHexString(new byte[] { color.R, color.G, color.B });
            lstColor.Items[i].SubItems[0].BackColor = color;
            lstColor.Items[i].SubItems[0].ForeColor = getBrightness(lstColor.Items[i].SubItems[0].BackColor) >= 0.5 ? Color.Black : Color.White;
            UpdateColorCopy();

            colorEditor.Color = color;
            //colorWheel.Color = color;
        }

        private void UpdateAllColors()
        {
            if (globalControlMode)
            {
                for (int i = 0; i < lstColor.Items.Count; i++)
                {
                    byte[] item = StringToByteArray(lstColor.Items[i].SubItems[1].Text);
                    Color itemColor = (Color.FromArgb(item[0], item[1], item[2]));

                    HslColor hslColor = new HslColor(itemColor);

                    hslColor = ChangeHue(hslColor, hslColor.H + (MasterHue));
                    hslColor.S += (double)(MasterSaturation - 0.5) / 1.0;
                    hslColor.L += (double)(MasterLightness - 0.5) / 1.0;

                    Color newColor = hslColor.ToRgbColor();

                    SetModelColor(newColor, i);
                    lstColor.Items[i].SubItems[0].Text = Convert.ToHexString(new byte[] { newColor.R, newColor.G, newColor.B });
                    lstColor.Items[i].SubItems[0].BackColor = newColor;
                    lstColor.Items[i].SubItems[0].ForeColor = getBrightness(lstColor.Items[i].SubItems[0].BackColor) >= 0.5 ? Color.Black : Color.White;
                }
            }
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        // color brightness as perceived:
        float getBrightness(Color c)
        { return (c.R * 0.299f + c.G * 0.587f + c.B * 0.114f) / 256f; }

        private async void tglGlobalControl_SwitchedChanged(object sender)
        {
            globalControlMode = tglGlobalControl.Switched;
            if (globalControlMode)
            {
                lblColorIndex.Text = "Index: -";
                pnSliders.Enabled = false;
                pnGlobalControl.Enabled =
                cmdApply.Enabled =
                cmdCancel.Enabled = true;
            }
            else
            {
                pnGlobalControl.Enabled =
                cmdApply.Enabled =
                cmdCancel.Enabled = false;
                await ResetColorListAsync();
                ResetColorSliders();
            }
        }

        private void ResetColorSliders()
        {
            var hslColor = colorEditorGlobal.HslColor;
            hslColor.H = 180;
            hslColor.S = 0.5;
            hslColor.L = 0.5;
            colorEditorGlobal.HslColor = hslColor;
        }

        private Color GetSelectedItemColor()
        {
            string hexcolor = lstColor.SelectedItems[0].Text;
            int color = Int32.Parse(hexcolor.Replace("#", ""), NumberStyles.HexNumber);
            int alpha = 255;
            Color result = Color.FromArgb(alpha, Color.FromArgb(color));
            return result;
        }

        private void lstColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstColor.SelectedItems.Count <= 0 || globalControlMode)
            {
                //pnSliders.Enabled = false;
                lblColorIndex.Text = "Index: -";
                return;
            }
            pnSliders.Enabled = true;
            Color color = GetSelectedItemColor();
            colorEditor.Color = color;
            var hslColor = colorEditor.HslColor;
            if (hslColor.S == 0)
            {
                hslColor.H = 180F;
                colorEditor.HslColor = hslColor;
            }
            //colorWheel.Color = color;
            lblColorIndex.Text = $"Index: {lstColor.SelectedItems[0].Index}";
        }

        private void colorWheel_ColorChanged(object sender, EventArgs e)
        {
            if (!globalControlMode)
                OnValueChanged(false, colorWheel.Color);
        }

        private void colorEditor_ColorChanged(object sender, EventArgs e)
        {
            if (!globalControlMode)
                OnValueChanged(false, colorEditor.Color);
        }

        private void colorEditorGlobal_ColorChanged(object sender, EventArgs e)
        {
            OnValueChanged(true);
        }

        internal static HslColor ChangeHue(HslColor color, double increment)
        {
            HslColor copy;
            double value;

            copy = new HslColor(color);
            value = copy.H + increment;

            if (increment > 0 && value > 359)
            {
                value -= 360;
            }
            else if (increment < 0 && value < 0)
            {
                value += 360;
            }

            copy.H = value;
            return copy;
        }

        private void ApplyChanges()
        {
            foreach (ListViewItem item in lstColor.Items)
            {
                var i = (int)item.Tag;
                SetModelColor(item.BackColor, i);
            }
            UpdateColorCopy();
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            ApplyChanges();
            tglGlobalControl.Switched = false;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            tglGlobalControl.Switched = false;
        }
        #endregion

        #region Textures

        private void UpdateTPageList()
        {
            lstTPages.Columns.Add("Index");
            lstTPages.Columns.Add("Page");
            for (int i = 0; i < model.TPAGCount; ++i)
            {
                ListViewItem newitem = new ListViewItem(i.ToString());
                newitem.SubItems.Add(Entry.EIDToEName(model.GetTPAG(i)));
                lstTPages.Items.Add(newitem);
            }

            if (lstTPages.Items.Count > 0)
            {
                rbtReloadTPage.Enabled = true;
                dpdTPage.Enabled = true;
                List<Chunk> chunks = null;
                chunks = controller.GetNSF().Chunks;
                foreach (Chunk chunk in chunks)
                {
                    if (chunk is TextureChunk t)
                    {
                        dpdTPage.Items.Add(Entry.EIDToEName(t.EID));
                    }
                }
            }
        }

        private void UpdateTPageButtons()
        {
            if (dgvTextures.Rows.Count > 0)
            {
                if (model.TPAGCount > 7 || model.TPAGCount == 0)
                    cmdAppendTPage.Enabled = false;
                else
                    cmdAppendTPage.Enabled = true;

                if (model.TPAGCount == 0)
                    cmdRemoveTPage.Enabled = false;
                else
                    cmdRemoveTPage.Enabled = true;

                int maxIndex = 0;
                foreach (DataGridViewRow row in dgvTextures.Rows)
                {
                    int curIndex = Convert.ToInt32(row.Cells[ColPage].Value.ToString());
                    if (curIndex > maxIndex)
                        maxIndex = curIndex;
                }
                if (lstTPages.Items.Count <= maxIndex + 1)
                    cmdRemoveTPage.Enabled = false;
                else
                    cmdRemoveTPage.Enabled = true;
            }
            else
            {
                if (lstTPages.Items.Count > 0)
                {
                    lstTPages.Items[0].Selected = true;
                    dpdTPage.Text = lstTPages.SelectedItems[0].SubItems[1].Text;
                }
                cmdAppendTPage.Enabled = false;
                cmdRemoveTPage.Enabled = false;
            }
        }

        private void CreateTextureListColumns()
        {
            dgvTextures.Columns.Add("Page", "Page");
            dgvTextures.Columns.Add("ClutX", "Clut X");
            dgvTextures.Columns.Add("ClutY", "Clut Y");
            dgvTextures.Columns.Add("Left", "X  ");
            dgvTextures.Columns.Add("Top", "Y  ");
            dgvTextures.Columns.Add("Width", "Width");
            dgvTextures.Columns.Add("Height", "Height");
            dgvTextures.Columns.Add("X1", "X1");
            dgvTextures.Columns.Add("X2", "X2");
            dgvTextures.Columns.Add("X3", "X3");
            dgvTextures.Columns.Add("X4", "X4");
            dgvTextures.Columns.Add("Y1", "Y1");
            dgvTextures.Columns.Add("Y2", "Y2");
            dgvTextures.Columns.Add("Y3", "Y3");
            dgvTextures.Columns.Add("Y4", "Y4");
            dgvTextures.Columns.Add("BlendMode", "Blend");
            dgvTextures.Columns.Add("ColorMode", "Color");

            for (int i = ColLeft; i <= ColHeight; i++)
            {
                dgvTextures.Columns[i].Visible = false;
            }
            if (!isScenery)
            {
                dgvTextures.Columns[ColX4].Visible = false;
                dgvTextures.Columns[ColY4].Visible = false;
            }

            foreach (DataGridViewColumn column in dgvTextures.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 44;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void chkRegionEndFlag_Click(object sender, EventArgs e)
        {
            if (!(dgvTextures.SelectedCells.Count > 0)) return;

            foreach (DataGridViewCell cell in dgvTextures.SelectedCells)
            {
                if ((cell.ColumnIndex >= ColX1 && cell.ColumnIndex <= ColY4))
                {
                    cell.Style = chkRegionEndFlag.Checked ? styleRegionEnd : styleDefault;
                }
            }
        }

        private void SetRegionEndTag(int start, int end)
        {
            int startColumnIndex = start;
            int endColumnIndex = end;

            Parallel.For(0, dgvTextures.Rows.Count, rowIndex =>
            {
                var row = dgvTextures.Rows[rowIndex];
                double maxValue = double.MinValue;

                for (int col = startColumnIndex; col <= endColumnIndex; col++)
                {
                    var cellValue = row.Cells[col].Value;

                    if (cellValue != null && double.TryParse(cellValue.ToString(), out double value))
                    {
                        if (value > maxValue)
                        {
                            maxValue = value;
                        }
                    }
                }
                for (int col = startColumnIndex; col <= endColumnIndex; col++)
                {
                    var cellValue = row.Cells[col].Value;

                    if (cellValue != null && double.TryParse(cellValue.ToString(), out double value))
                    {
                        if (value == maxValue)
                        {
                            row.Cells[col].Style = styleRegionEnd;
                        }
                    }
                }
            });
        }

        private async Task UpdateTextureListAsync(bool setMaxTags)
        {
            dgvTextures.SuspendLayout();
            dgvTextures.ScrollBars = ScrollBars.None;
            Stopwatch stopwatch = Stopwatch.StartNew();
            var seenTags = new ConcurrentDictionary<string, bool>();

            var rows = await Task.Run(() =>
            {
                var rowsToAdd = new ConcurrentBag<(int Index, DataGridViewRow Row)>();

                Parallel.ForEach(Enumerable.Range(0, (int)model.Textures.Count), (int i) =>
                {
                    var item = model.Textures[i];
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgvTextures, item.Page, item.ClutX, item.ClutY, item.Left, item.Top, item.Width, item.Height, item.X1, item.X2, item.X3, item.X4, item.Y1, item.Y2, item.Y3, item.Y4, item.BlendMode, item.ColorMode);

                    var tagValue = $"{item.ClutX}, {item.ClutY}, {item.Left}, {item.Top}";
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Tag = tagValue;
                    }

                    rowsToAdd.Add((i, row));
                });
                return rowsToAdd.OrderBy(pair => pair.Index).Select(pair => pair.Row).ToList();
            });

            dgvTextures.Rows.Clear();

            int visibleRowIndex = 0;
            foreach (var row in rows)
            {
                dgvTextures.Rows.Add(row);
                if (simpleMode)
                {
                    string tagValue = row.Cells[0].Tag as string;
                    if (!seenTags.ContainsKey(tagValue))
                    {
                        seenTags.TryAdd(tagValue, true);
                        row.DefaultCellStyle.BackColor = (visibleRowIndex % 2 == 0) ? clrBackground : clrAltBackground;
                        visibleRowIndex++;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
            }

            if (isScenery)
            {
                SetRegionEndTag(ColX1, ColX4);
                SetRegionEndTag(ColY1, ColY4);
            }
            else
            {
                SetRegionEndTag(ColX1, ColX3);
                SetRegionEndTag(ColY1, ColY3);
            }

            stopwatch.Stop();
            int count = simpleMode ? seenTags.Count : rows.Count;
            Console.WriteLine($"Row count: {count}");
            Console.WriteLine($"Processing time: {stopwatch.Elapsed.TotalSeconds:F3} seconds");
            dgvTextures.ScrollBars = ScrollBars.Vertical;
            dgvTextures.ResumeLayout();
        }

        private void dgvTextures_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTextures.SelectedCells.Count > 0)
            {
                int rowIndex = dgvTextures.SelectedCells[0].RowIndex;
                var row = dgvTextures.Rows[rowIndex];
                var cell = dgvTextures.SelectedCells[0];

                chkRegionEndFlag.Enabled = (cell.ColumnIndex >= ColX1 && cell.ColumnIndex <= ColY4) ? true : false;
                chkRegionEndFlag.Checked = cell.Style == styleRegionEnd ? true : false;

                var pageIndex = Convert.ToInt32(row.Cells[ColPage].Value);
                string cid = lstTPages.Items[pageIndex].SubItems[1].Text;

                UpdatePicture();

                numReplace.Value = Convert.ToInt32(dgvTextures.CurrentCell.Value);
                numReplaceTo.Value = numReplace.Value;
                numRowIndex.Value = dgvTextures.CurrentCell.RowIndex;
                lstTPages.SelectedItems.Clear();
                lstTPages.Items[pageIndex].Selected = true;
                //lstPages.EnsureVisible(pageIndex);

                if (Settings.Default.OutputModelTextureInfo && dgvTextures.CurrentCell.Tag is string tags)
                {
                    Console.WriteLine($"Row {dgvTextures.CurrentCell.RowIndex} Tags: {string.Join(", ", tags)}");
                }
            }
        }

        private void dgvTextures_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textbox)
            {
                textbox.KeyPress -= TextBox_KeyPress;
                textbox.KeyPress += TextBox_KeyPress;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private async void cmdLoadTexture_Click(object sender, EventArgs e)
        {
            await UpdateTextureListAsync(true);

            if (dgvTextures.Rows.Count > 0)
            {
                UpdateTPageButtons();
                fraSwitches.Enabled = true;
                fraReplace.Enabled = true;
                fraReplaceTexture.Enabled = true;
            }
        }

        private async void ToggleSimpleMode()
        {
            if (dgvTextures.IsCurrentCellInEditMode)
                dgvTextures.CancelEdit();
            dgvTextures.SuspendLayout();
            dgvTextures.ScrollBars = ScrollBars.None;

            await UpdateTextureListAsync(false);

            int endColX = isScenery ? ColX4 : ColX3;
            int endColY = isScenery ? ColY4 : ColY3;
            if (simpleMode)
            {
                for (int i = ColLeft; i <= ColHeight; i++)
                    dgvTextures.Columns[i].Visible = true;

                for (int i = ColX1; i <= endColX; i++)
                    dgvTextures.Columns[i].Visible = false;
                for (int i = ColY1; i <= endColY; i++)
                    dgvTextures.Columns[i].Visible = false;
            }
            else
            {
                for (int i = ColLeft; i <= ColHeight; i++)
                    dgvTextures.Columns[i].Visible = false;

                for (int i = ColX1; i <= endColX; i++)
                    dgvTextures.Columns[i].Visible = true;
                for (int i = ColY1; i <= endColY; i++)
                    dgvTextures.Columns[i].Visible = true;
            }

            fraReplace.Enabled = !simpleMode;
            dgvTextures.ScrollBars = ScrollBars.Vertical;
            dgvTextures.ResumeLayout();
        }

        private void cmdReplaceTexture_Click(object sender, EventArgs e)
        {
            if (selectedRegionX < 32 && selectedRegionY == 0)
            {
                DarkMessageBox.ShowError("Textures cannot be replaced on the header.", Resources.Title_TextureReplacement);
                return;
            }
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.bmp;*.png;|All Files|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string extension = Path.GetExtension(filePath).ToLower();

                    int destX = selectedRegionX;
                    int destY = selectedRegionY;
                    if (dgvTextures.SelectedCells.Count > 0)
                    {
                        var row = dgvTextures.Rows[dgvTextures.SelectedCells[0].RowIndex];
                        int clutX = Convert.ToInt32(row.Cells[ColClutX].Value);
                        int clutY = Convert.ToInt32(row.Cells[ColClutY].Value);

                        int oldBpp = 4;
                        if (Convert.ToInt32(row.Cells[ColColorMode].Value) == 1)
                        {
                            oldBpp = 8;
                            destX *= 2;
                            clutX = 0;
                        }

                        chunk.Data = TextureConv.ReplaceTextureFromFile(filePath, extension, BGRAMode, chunk.Data, destX, destY, replaceCLUT, oldBpp, clutX, clutY);
                        UpdatePicture();
                    }
                }
            }
        }

        private void dgvTexturesGetMaxValue(int rowIndex, int columnIndex, int newValue, out int minValue, out int maxValue, out bool isMaxCell)
        {
            isMaxCell = dgvTextures.Rows[rowIndex].Cells[columnIndex].Style == styleRegionEnd;
            maxValue = 0; minValue = 0;
            // Page
            if (columnIndex == ColPage)
                maxValue = lstTPages.Items.Count - 1;
            // ClutX
            else if (columnIndex == ColClutX)
                maxValue = 15;
            // ClutY
            else if (columnIndex == ColClutY)
                maxValue = 127;
            // X (Left)
            else if (columnIndex == ColLeft)
            {
                int width = (int)dgvTextures.Rows[rowIndex].Cells[ColWidth].Value;
                GetXOff(currentColorMode, newValue, out int xoffUnit, out int segment, out int xoff);

                int pw = 256 << (2 - currentColorMode);
                if (newValue >= (isMaxCell ? pw + width : pw))
                {
                    maxValue = isMaxCell ? pw : pw - width;
                    return;
                }

                int xoffEnd = xoff + xoffUnit;
                maxValue = xoffEnd - width;
                if (Settings.Default.OutputModelTextureInfo)
                    Console.WriteLine($"Segment {segment}, maxValue {maxValue}");
            }
            // Y (Top)
            else if (columnIndex == ColTop)
                maxValue = 128 - (int)dgvTextures.Rows[rowIndex].Cells[ColHeight].Value;
            // Width
            else if (columnIndex == ColWidth)
            {
                int value = (int)dgvTextures.Rows[rowIndex].Cells[ColLeft].Value;
                GetXOff(currentColorMode, value, out int xoffUnit, out int segment, out int xoff);

                maxValue = xoffUnit - (value - xoff);
                minValue = 4;
                if (Settings.Default.OutputModelTextureInfo)
                    Console.WriteLine($"Segment {segment}, maxValue {maxValue}");
            }
            // Height
            else if (columnIndex == ColHeight)
            {
                maxValue = 128 - (int)dgvTextures.Rows[rowIndex].Cells[ColTop].Value;
                minValue = 4;
            }
            // Blend Mode
            else if (columnIndex == ColBlendMode)
                maxValue = 3;
            // Color Mode
            else if (columnIndex == ColColorMode)
                maxValue = 2;
            // X1-X4
            else if (columnIndex >= ColX1 && columnIndex <= ColX4)
            {
                int width = (int)dgvTextures.Rows[rowIndex].Cells[ColWidth].Value;
                GetXOff(currentColorMode, isMaxCell ? newValue - width : newValue, out int xoffUnit, out int segment, out int xoff);
                int xoffEnd = xoff + xoffUnit;

                int pw = 256 << (2 - currentColorMode);
                if (newValue >= (isMaxCell ? pw + width : pw))
                {
                    maxValue = isMaxCell ? pw : pw - width;
                    return;
                }

                if (isMaxCell)
                {
                    maxValue = xoffEnd;
                    minValue = xoff + width;
                }
                else
                {
                    maxValue = xoffEnd - width;
                    minValue = xoff;
                }
                if (Settings.Default.OutputModelTextureInfo)
                    Console.WriteLine($"Segment {segment}, minValue {minValue}, maxValue {maxValue}");
            }
            // Y1-Y4
            else if (columnIndex >= ColY1 && columnIndex <= ColY4)
                maxValue = 128 - (int)(isMaxCell ? 0 : dgvTextures.Rows[rowIndex].Cells[ColHeight].Value);

        }

        private void dgvTextures_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (int.TryParse(e.FormattedValue.ToString(), out int newValue))
            {
                dgvTexturesGetMaxValue(e.RowIndex, e.ColumnIndex, newValue, out int minValue, out int maxValue, out bool isMaxCell);
                if (newValue > maxValue)
                {
                    if (e.ColumnIndex >= ColLeft && e.ColumnIndex <= ColHeight)
                        DarkMessageBox.ShowError($"The UV does not fit within the segment. The value must be less than or equal to {maxValue}.", Resources.Title_InputError);
                    else
                        DarkMessageBox.ShowError($"The value must be less than or equal to {maxValue}.", Resources.Title_InputError);
                    e.Cancel = true;
                }
                else if (newValue < minValue)
                {
                    DarkMessageBox.ShowError($"The value must be greater than or equal to {minValue}.", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
            else
            {
                DarkMessageBox.ShowError($"Invalid input. Please enter an integer.", Resources.Title_InputError);
                e.Cancel = true;
            }
        }

        private async void cmdReplace_Click(object sender, EventArgs e)
        {
            if (dgvTextures.SelectedCells.Count > 0)
            {
                int rowIndex = (int)numRowIndex.Value;
                int columnIndex = dgvTextures.CurrentCell.ColumnIndex;
                var row = dgvTextures.Rows[rowIndex];
                string? editedCellTag = dgvTextures.SelectedCells[0].Tag?.ToString();

                int newValue = (int)numReplaceTo.Value;
                dgvTexturesGetMaxValue(rowIndex, columnIndex, newValue, out int minValue, out int maxValue, out bool isMaxCell);
                if (newValue > maxValue)
                {
                    if (columnIndex >= ColLeft && columnIndex <= ColHeight)
                        DarkMessageBox.ShowError($"The UV does not fit within the segment. The value must be less than or equal to {maxValue}.", Resources.Title_InputError);
                    else
                        DarkMessageBox.ShowError($"The value must be less than or equal to {maxValue}.", Resources.Title_InputError);
                    return;
                }

                int endColX = isScenery ? ColX4 : ColX3;
                int endColY = isScenery ? ColY4 : ColY3;
                if (columnIndex >= ColX1 && columnIndex <= endColX)
                {
                    if (isMaxCell)
                        newValue -= (int)row.Cells[ColWidth].Value;
                    if (newValue < 0)
                    {
                        DarkMessageBox.ShowError($"The value must be greater than or equal to {row.Cells[ColWidth].Value}.", Resources.Title_InputError);
                        return;
                    }
                    await UpdateRowsXYAsync(rowIndex, newValue, newValue + (int)row.Cells[ColWidth].Value, true, editedCellTag);
                }
                else if (columnIndex >= ColY1 && columnIndex <= endColY)
                {
                    if (isMaxCell)
                        newValue -= (int)row.Cells[ColHeight].Value;
                    if (newValue < 0)
                    {
                        DarkMessageBox.ShowError($"The value must be greater than or equal to {row.Cells[ColHeight].Value}.", Resources.Title_InputError);
                        return;
                    }
                    await UpdateRowsXYAsync(rowIndex, newValue, newValue + (int)row.Cells[ColHeight].Value, false, editedCellTag);
                }
                else if (columnIndex == ColColorMode)
                {
                    await UpdateRowsColorModeAsync(rowIndex, newValue, editedCellTag);
                }
                else
                {
                    await UpdateCellsByTagAsync(columnIndex, columnIndex, newValue, editedCellTag);
                }
                UpdatePicture();
            }
        }

        private async void dgvTextures_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTextures.SelectedCells.Count > 0 && simpleMode)
            {
                var editedRow = dgvTextures.Rows[e.RowIndex];
                int newValue = Convert.ToInt32(editedRow.Cells[e.ColumnIndex].Value);
                string? editedCellTag = editedRow.Cells[e.ColumnIndex].Tag?.ToString();

                if (editedCellTag != null)
                {
                    if (e.ColumnIndex == ColLeft)
                    {
                        await UpdateRowsXYAsync(e.RowIndex, newValue, newValue + (int)editedRow.Cells[ColWidth].Value, true, editedCellTag);
                    }
                    else if (e.ColumnIndex == ColWidth)
                    {
                        await UpdateRowsXYAsync(e.RowIndex, (int)editedRow.Cells[ColLeft].Value, (int)editedRow.Cells[ColLeft].Value + newValue, true, editedCellTag);
                    }
                    else if (e.ColumnIndex == ColTop)
                    {
                        await UpdateRowsXYAsync(e.RowIndex, newValue, newValue + (int)editedRow.Cells[ColHeight].Value, false, editedCellTag);
                    }
                    else if (e.ColumnIndex == ColHeight)
                    {
                        await UpdateRowsXYAsync(e.RowIndex, (int)editedRow.Cells[ColTop].Value, (int)editedRow.Cells[ColTop].Value + newValue, false, editedCellTag);
                    }
                    else if (e.ColumnIndex == ColColorMode)
                    {
                        await UpdateRowsColorModeAsync(e.RowIndex, newValue, editedCellTag);
                    }
                    else
                    {
                        await UpdateCellsByTagAsync(e.ColumnIndex, e.ColumnIndex, newValue, editedCellTag);
                    }
                }
                UpdatePicture();
            }
        }

        private async Task UpdateCellsByTagAsync(int startColumn, int endColumn, int newValue, string editedCellTag)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var filteredRows = dgvTextures.Rows.Cast<DataGridViewRow>()
                  .Where(row =>
                  {
                      var cell = row.Cells[0];
                      return cell.Tag is string tags && tags.Contains(editedCellTag);
                  })
                  .ToList();

            foreach (var row in filteredRows)
            {
                for (int col = startColumn; col <= endColumn; col++)
                {
                    row.Cells[col].Value = newValue;
                    if (Settings.Default.OutputModelTextureInfo)
                        Console.WriteLine($"Tags match in row {row.Index}");
                }
            }

            if (Settings.Default.OutputModelTextureInfo)
                Console.WriteLine($"Finished updating cells with tag: {editedCellTag}");

            stopwatch.Stop();
            if (Settings.Default.OutputModelTextureInfo)
                Console.WriteLine($"Processing time: {stopwatch.Elapsed.TotalSeconds:F3} seconds");
        }

        private void GetXOff(int colorMode, int value, out int xoffUnit, out int segment, out int xoff)
        {
            xoffUnit = (1 << (2 - colorMode)) * 64;
            segment = value / xoffUnit;
            xoff = xoffUnit * segment;
        }

        private void dgvTextures_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var og = model.Textures[e.RowIndex];
            var item = dgvTextures.Rows[e.RowIndex];
            int colorMode = Convert.ToInt32(item.Cells[ColColorMode].Value);

            // Page
            if (e.ColumnIndex == ColPage)
            {
                og.Page = Convert.ToByte(item.Cells[ColPage].Value);
                UpdateTPageButtons();
            }
            // ClutX
            else if (e.ColumnIndex == ColClutX)
                og.ClutX = Convert.ToByte(item.Cells[ColClutX].Value);
            // ClutY
            else if (e.ColumnIndex == ColClutY)
            {
                byte value = Convert.ToByte(item.Cells[ColClutY].Value);
                og.ClutY1 = (byte)((value & 0x3) << 2);
                og.ClutY2 = (byte)(value >> 2);
            }
            // X (Left)
            else if (e.ColumnIndex == ColLeft)
                og.Left = Convert.ToInt32(item.Cells[ColLeft].Value);
            // Width
            else if (e.ColumnIndex == ColWidth)
                og.Width = Convert.ToInt32(item.Cells[ColWidth].Value);
            // Y (Top)
            else if (e.ColumnIndex == ColTop)
                og.Top = Convert.ToInt32(item.Cells[ColTop].Value);
            // Height
            else if (e.ColumnIndex == ColHeight)
                og.Height = Convert.ToInt32(item.Cells[ColHeight].Value);
            // Blend Mode
            else if (e.ColumnIndex == ColBlendMode)
                og.BlendMode = Convert.ToByte(item.Cells[ColBlendMode].Value);
            // Color Mode
            else if (e.ColumnIndex == ColColorMode)
                og.ColorMode = Convert.ToByte(item.Cells[ColColorMode].Value);
            // X1-X4
            else if (e.ColumnIndex >= ColX1 && e.ColumnIndex <= ColX4)
            {
                int value = Convert.ToInt32(item.Cells[e.ColumnIndex].Value);

                if (e.ColumnIndex == ColX1) og.X1 = value;
                else if (e.ColumnIndex == ColX2) og.X2 = value;
                else if (e.ColumnIndex == ColX3) og.X3 = value;
                else if (e.ColumnIndex == ColX4) og.X4 = value;

                GetXOff(colorMode, value - 1, out int xoffUnit, out int segment, out int xoff);
                int newU = value - xoff;

                if (item.Cells[e.ColumnIndex].Style == styleRegionEnd)
                {
                    newU--;
                    og.Segment = (byte)segment;
                }

                if (e.ColumnIndex == ColX1) og.U1 = (byte)newU;
                else if (e.ColumnIndex == ColX2) og.U2 = (byte)newU;
                else if (e.ColumnIndex == ColX3) og.U3 = (byte)newU;
                else if (e.ColumnIndex == ColX4) og.U4 = (byte)newU;
            }
            // Y1-Y4
            else if (e.ColumnIndex >= ColY1 && e.ColumnIndex <= ColY4)
            {
                int value = Convert.ToInt32(item.Cells[e.ColumnIndex].Value);

                if (e.ColumnIndex == ColY1) og.Y1 = value;
                else if (e.ColumnIndex == ColY2) og.Y2 = value;
                else if (e.ColumnIndex == ColY3) og.Y3 = value;
                else if (e.ColumnIndex == ColY4) og.Y4 = value;

                int newV = value;

                if (item.Cells[e.ColumnIndex].Style == styleRegionEnd)
                {
                    newV--;
                }

                if (e.ColumnIndex == ColY1) og.V1 = (byte)newV;
                else if (e.ColumnIndex == ColY2) og.V2 = (byte)newV;
                else if (e.ColumnIndex == ColY3) og.V3 = (byte)newV;
                else if (e.ColumnIndex == ColY4) og.V4 = (byte)newV;
            }
        }

        private async Task UpdateRowsXYAsync(int rowIndex, int newMinUV, int newMaxUV, bool targetIsX, string editedCellTag)
        {
            if (rowIndex < 0 || rowIndex >= model.Textures.Count)
                return;

            int Col1, Col2, Col3, Col4, ColStart, ColLength;
            if (targetIsX)
            {
                Col1 = ColX1;
                Col2 = ColX2;
                Col3 = ColX3;
                Col4 = ColX4;
                ColStart = ColLeft;
                ColLength = ColWidth;
            }
            else
            {
                Col1 = ColY1;
                Col2 = ColY2;
                Col3 = ColY3;
                Col4 = ColY4;
                ColStart = ColTop;
                ColLength = ColHeight;
            }

            var targetRow = dgvTextures.Rows[rowIndex];

            Stopwatch stopwatch = Stopwatch.StartNew();
            var updatedRows = new ConcurrentBag<(int RowIndex, int UV1, int UV2, int UV3, int UV4)>();

            await Task.Run(() =>
            {
                var filteredRows = dgvTextures.Rows.Cast<DataGridViewRow>()
                     .Where(row =>
                    {
                        var cell = row.Cells[0];
                        return cell.Tag is string tags && tags.Contains(editedCellTag);
                    })
                    .ToList();

                Parallel.ForEach(filteredRows, row =>
                {
                    int UV4 = 0;
                    if (int.TryParse(row.Cells[Col1].Value?.ToString(), out int UV1) &&
                        int.TryParse(row.Cells[Col2].Value?.ToString(), out int UV2) &&
                        int.TryParse(row.Cells[Col3].Value?.ToString(), out int UV3) &&
                        (!isScenery || int.TryParse(row.Cells[Col4].Value?.ToString(), out UV4)))
                    {
                        int minUV = isScenery ? Math.Min(UV1, Math.Min(UV2, Math.Min(UV3, UV4))) : Math.Min(UV1, Math.Min(UV2, UV3));
                        int maxUV = isScenery ? Math.Max(UV1, Math.Max(UV2, Math.Max(UV3, UV4))) : Math.Max(UV1, Math.Max(UV2, UV3));

                        UV1 = (UV1 == minUV) ? newMinUV : newMaxUV;
                        UV2 = (UV2 == minUV) ? newMinUV : newMaxUV;
                        UV3 = (UV3 == minUV) ? newMinUV : newMaxUV;
                        if (isScenery) UV4 = (UV4 == minUV) ? newMinUV : newMaxUV;

                        updatedRows.Add((row.Index, UV1, UV2, UV3, UV4));
                    }
                });
            });

            dgvTextures.Invoke(() =>
            {
                foreach (var (rowIndex, UV1, UV2, UV3, UV4) in updatedRows)
                {
                    var row = dgvTextures.Rows[rowIndex];
                    row.Cells[Col1].Value = UV1;
                    row.Cells[Col2].Value = UV2;
                    row.Cells[Col3].Value = UV3;
                    if (isScenery) row.Cells[Col4].Value = UV4;

                    var texture = model.Textures[rowIndex];
                    if (targetIsX)
                    {
                        texture.Left = newMinUV;
                        texture.Width = newMaxUV - newMinUV;
                    }
                    else
                    {
                        texture.Top = newMinUV;
                        texture.Height = newMaxUV - newMinUV;
                    }

                    if (Settings.Default.OutputModelTextureInfo)
                        Console.WriteLine($"Tags match in row {row.Index}");
                }

                targetRow.Cells[ColStart].Value = newMinUV;
                targetRow.Cells[ColLength].Value = newMaxUV - newMinUV;
            });

            if (Settings.Default.OutputModelTextureInfo)
                Console.WriteLine($"Finished updating cells with tag: {editedCellTag}");

            stopwatch.Stop();
            if (Settings.Default.OutputModelTextureInfo)
                Console.WriteLine($"Processing time: {stopwatch.Elapsed.TotalSeconds:F3} seconds");
        }

        private async Task UpdateRowsColorModeAsync(int rowIndex, int newValue, string editedCellTag)
        {
            if (rowIndex < 0 || rowIndex >= model.Textures.Count)
                return;

            var targetRow = dgvTextures.Rows[rowIndex];
            int oldColorMode = Convert.ToInt32(targetRow.Cells[ColColorMode].Value);

            Stopwatch stopwatch = Stopwatch.StartNew();

            var updatedRows = await Task.Run(() =>
            {
                var result = new ConcurrentBag<(DataGridViewRow Row, int[] UpdatedValues, int newLeft)>();

                var filteredRows = dgvTextures.Rows.Cast<DataGridViewRow>()
                    .Where(row =>
                    {
                        var cell = row.Cells[0];
                        return cell.Tag is string tags && tags.Contains(editedCellTag);
                    })
                    .ToList();

                Parallel.ForEach(filteredRows, row =>
                {
                    var texture = model.Textures[row.Index];
                    int newLeft = newLeft = Convert.ToInt32(row.Cells[ColLeft].Value);
                    bool trimed = false;

                    var updatedValues = new int[(isScenery ? ColX4 : ColX3) - ColX1 + 1];

                    if ((int)row.Cells[ColLeft].Value + (int)row.Cells[ColWidth].Value > (256 << (2 - newValue)))
                    {
                        newLeft = (256 << (2 - newValue)) - Convert.ToInt32(row.Cells[ColWidth].Value);
                        trimed = true;
                    }

                    for (int i = ColX1; i <= (isScenery ? ColX4 : ColX3); i++)
                    {
                        int value = Convert.ToInt32(row.Cells[i].Value);
                        if (trimed)
                        {
                            value = newLeft;
                            if (row.Cells[i].Style == styleRegionEnd)
                            {
                                value += (int)row.Cells[ColWidth].Value;
                            }
                        }
                        updatedValues[i - ColX1] = value;
                    }

                    result.Add((row, updatedValues, newLeft));
                });

                return result;
            });

            dgvTextures.Invoke(() =>
            {
                foreach (var (row, updatedValues, newLeft) in updatedRows)
                {
                    for (int i = ColX1; i <= (isScenery ? ColX4 : ColX3); i++)
                    {
                        row.Cells[i].Value = updatedValues[i - ColX1];
                    }
                    row.Cells[ColClutX].Value = 0;
                    row.Cells[ColLeft].Value = newLeft;
                    //model.Textures[row.Index].Left = newLeft;

                    row.Cells[ColColorMode].Value = Convert.ToByte(newValue);

                    if (Settings.Default.OutputModelTextureInfo)
                        Console.WriteLine($"Tags match in row {row.Index}");
                }
            });

            if (Settings.Default.OutputModelTextureInfo)
                Console.WriteLine($"Finished updating cells with tag: {editedCellTag}");

            stopwatch.Stop();
            Console.WriteLine($"Processing time: {stopwatch.Elapsed.TotalSeconds:F3} seconds");
        }

        private void trkPictureSize_ValueChanged(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                float zoom = trkPictureSize.Value / 100f;
                pictureBox1.Width = (int)(pictureBox1.Image.Width * zoom);
                pictureBox1.Height = (int)(pictureBox1.Image.Height * zoom);
                pictureBox1.Invalidate();
            }
        }

        private void tglSimpleMode_SwitchedChanged(object sender)
        {
            simpleMode = tglSimpleMode.Switched;
            ToggleSimpleMode();
        }

        private void lstPages_ColumnWidthChangingHandler(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lstTPages.Columns[e.ColumnIndex].Width;
        }

        private void chkBGRA_CheckedChanged(object sender, EventArgs e)
        {
            BGRAMode = chkBGRA.Checked;
        }

        private void cmdAppendTPage_Click(object sender, EventArgs e)
        {
            if (lstTPages.Items.Count < 8)
            {
                int index = lstTPages.Items.Count;
                string name = lstTPages.Items[index - 1].SubItems[1].Text;
                model.SetTPAG(index, Entry.ENameToEID(name));
                ++model.TPAGCount;

                ListViewItem newitem = new ListViewItem((index).ToString());
                newitem.SubItems.Add(name);
                lstTPages.Items.Add(newitem);
                UpdateTPageButtons();
            }
        }

        private void cmdRemoveTPage_Click(object sender, EventArgs e)
        {
            if (lstTPages.Items.Count > 0)
            {
                int index = lstTPages.Items.Count;
                int name = 0;
                model.SetTPAG(index - 1, name);
                --model.TPAGCount;

                lstTPages.Items.RemoveAt(index - 1);
                UpdateTPageButtons();
            }
        }

        private void lstTPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTPages.Items.Count > 0 && lstTPages.SelectedItems.Count > 0)
                dpdTPage.Text = lstTPages.SelectedItems[0].SubItems[1].Text;
        }

        private void chkReplaceCLUT_CheckedChanged(object sender, EventArgs e)
        {
            replaceCLUT = chkReplaceCLUT.Checked;
        }

        private void dpdTPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = dpdTPage.Text;
            if (lstTPages.SelectedItems.Count > 0)
                lstTPages.SelectedItems[0].SubItems[1].Text = text;
            model.SetTPAG(lstTPages.SelectedIndices[0], Entry.ENameToEID(text));
            UpdatePicture();
        }

        private void rbtReloadTPage_Click(object sender, EventArgs e)
        {
            if (lstTPages.Items.Count > 0)
            {
                dpdTPage.Items.Clear();
                List<Chunk> chunks = null;
                chunks = controller.GetNSF().Chunks;
                foreach (Chunk chunk in chunks)
                {
                    if (chunk is TextureChunk t)
                    {
                        dpdTPage.Items.Add(Entry.EIDToEName(t.EID));
                    }
                }
                if (lstTPages.Items.Count > 0 && lstTPages.SelectedItems.Count > 0)
                    dpdTPage.Text = lstTPages.SelectedItems[0].SubItems[1].Text;
            }
            rbtReloadTPage.Checked = false;
        }

        private void numReplaceTo_Click(object sender, EventArgs e)
        {
            numReplaceTo.Select(0, numReplaceTo.Text.Length);
        }

        private void ScrollHandlerFunction(object sender, MouseEventArgs e)
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

        private void UpdatePicture()
        {
            if (!(dgvTextures.SelectedCells.Count > 0)) return;

            var cell = dgvTextures.Rows[dgvTextures.SelectedCells[0].RowIndex];
            var pageIndex = Convert.ToInt32(cell.Cells[ColPage].Value);
            string cid = lstTPages.Items[pageIndex].SubItems[1].Text;

            int TexCX = Convert.ToInt32(cell.Cells[ColClutX].Value);
            int TexCY = Convert.ToInt32(cell.Cells[ColClutY].Value);
            int TexX = Convert.ToInt32(cell.Cells[ColLeft].Value);
            int TexY = Convert.ToInt32(cell.Cells[ColTop].Value);
            int TexW = Convert.ToInt32(cell.Cells[ColWidth].Value);
            int TexH = Convert.ToInt32(cell.Cells[ColHeight].Value);
            int colormode = Convert.ToInt32(cell.Cells[ColColorMode].Value);
            int blendmode = Convert.ToInt32(cell.Cells[ColBlendMode].Value);
            chunk = controller.GetEntry<TextureChunk>(Entry.ENameToEID(cid));
            int pw = 256 << (2 - colormode);
            int ph = 128;
            Bitmap bitmap = new Bitmap(pw + 2, ph + 2, PixelFormat.Format32bppArgb);
            Rectangle brect = new Rectangle(Point.Empty, bitmap.Size);
            BitmapData bdata = bitmap.LockBits(brect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int[] palette = null;
            if (colormode == 0)
            {
                int clutx = TexCX;
                int cluty = TexCY;
                palette = new int[16];
                for (int x = 0; x < 16; ++x)
                {
                    palette[x] = PixelConv.Convert5551_8888(BitConv.FromInt16(chunk.Data, cluty * 512 + (clutx * 16 + x) * 2), blendmode);
                }
            }
            else if (colormode == 1)
            {
                int cluty = TexCY;
                palette = new int[256];
                for (int x = 0; x < 256; ++x)
                {
                    palette[x] = PixelConv.Convert5551_8888(BitConv.FromInt16(chunk.Data, cluty * 512 + x * 2), blendmode);
                }
            }
            try
            {
                for (int y = 0; y < ph; y++)
                {
                    for (int x = 0; x < pw; x++)
                    {
                        int pixel = colormode == 0 ? palette[chunk.Data[x / 2 + y * 512] >> ((x & 1) == 0 ? 0 : 4) & 0xF] :
                        colormode == 1 ? palette[chunk.Data[x + y * 512]] :
                                    colormode == 2 ? PixelConv.Convert5551_8888(BitConv.FromInt16(chunk.Data, x * 2 + y * 512), blendmode)
                                    : throw new Exception("invalid colormode");
                        System.Runtime.InteropServices.Marshal.WriteInt32(bdata.Scan0, x * 4 + y * bdata.Stride, pixel);
                    }
                }
            }
            finally
            {
                bitmap.UnlockBits(bdata);
            }
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                int x = TexX;
                int y = TexY;
                int w = TexW;
                int h = TexH;
                using (var brush = new SolidBrush(Color.FromArgb(127, 0, 0, 0)))
                using (var pen = new Pen(Color.Black))
                {
                    int minh = Math.Min(h, ph - y);
                    g.FillRectangles(brush, new Rectangle[4]
                    {
                        new Rectangle(0, 0, pw, y),
                        new Rectangle(0, y, x, minh),
                        new Rectangle(x+w, y, Math.Max(pw-(x+w),0), minh),
                        new Rectangle(0, y+h, pw, Math.Max(ph-(y+h),0))
                    });
                    g.DrawRectangles(pen, new Rectangle[2]
                    {
                        new Rectangle(x-1,y-1,w+1,h+1),
                        new Rectangle(x-3,y-3,w+5,h+5)
                    });
                    pen.Color = Color.White;
                    g.DrawRectangle(pen, new Rectangle(x - 2, y - 2, w + 3, h + 3));
                }
                selectedregion.X = x;
                selectedregion.Y = y;
                selectedregion.Width = w;
                selectedregion.Height = h;
                selectedRegionX = x;
                selectedRegionY = y;
            }
            pictureBox1.Image = bitmap;

            float zoom = trkPictureSize.Value / 100f;
            pictureBox1.Width = (int)(pictureBox1.Image.Width * zoom);
            pictureBox1.Height = (int)(pictureBox1.Image.Height * zoom);

            currentColorMode = colormode;
        }
        #endregion

        #region ExtendedTextures

        private void CreateExtendedTextureColumns()
        {
            dgvExtendedTextures.Columns.Add("Offset", "Offset");
            dgvExtendedTextures.Columns.Add("IsLOD", "LOD");
            dgvExtendedTextures.Columns.Add("Mask", "Mask");
            dgvExtendedTextures.Columns.Add("Delay", "Delay");
            dgvExtendedTextures.Columns.Add("Latency", "Latency");
            dgvExtendedTextures.Columns.Add("Leap", "Leap");
            dgvExtendedTextures.Columns.Add("LOD0", "LOD 0");
            dgvExtendedTextures.Columns.Add("LOD1", "LOD 1");
            dgvExtendedTextures.Columns.Add("LOD2", "LOD 2");
            dgvExtendedTextures.Columns.Add("LOD3", "LOD 3");
            dgvExtendedTextures.Columns.Add("LOD4", "LOD 4");
            dgvExtendedTextures.Columns.Add("LOD5", "LOD 5");
            dgvExtendedTextures.Columns.Add("LOD6", "LOD 6");
            dgvExtendedTextures.Columns.Add("LOD7", "LOD 7");

            foreach (DataGridViewColumn column in dgvExtendedTextures.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 48;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private async Task UpdateExtendedTextureAsync()
        {
            dgvExtendedTextures.SuspendLayout();
            dgvExtendedTextures.ScrollBars = ScrollBars.None;

            var rows = await Task.Run(() =>
            {
                var rowsToAdd = new ConcurrentBag<(int Index, DataGridViewRow Row)>();

                Parallel.ForEach(Enumerable.Range(0, (int)model.AnimatedTextures.Count), (int i) =>
                {
                    DataGridViewRow row = new DataGridViewRow();
                    var item = model.AnimatedTextures[i];
                    if (item.IsLOD)
                    {
                        row.CreateCells(dgvExtendedTextures, item.Offset, item.IsLOD, "-", "-", "-", "-",
                            item.LOD0, item.LOD1, item.LOD2, item.LOD3, item.LOD4, item.LOD5, item.LOD6, item.LOD7);
                    }
                    else
                    {
                        row.CreateCells(dgvExtendedTextures, item.Offset, item.IsLOD, item.Mask, item.Delay, item.Latency, item.Leap,
                            "-", "-", "-", "-", "-", "-", "-", "-");

                    }

                    rowsToAdd.Add((i, row));
                });
                return rowsToAdd.OrderBy(pair => pair.Index).Select(pair => pair.Row).ToList();
            });

            foreach (var row in rows)
            {
                dgvExtendedTextures.Rows.Add(row);
            }

            dgvExtendedTextures.ScrollBars = ScrollBars.Vertical;
            dgvExtendedTextures.ResumeLayout();
        }

        private void dgvExtendedTextures_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!(dgvExtendedTextures.SelectedCells.Count > 0)) return;

            if (dgvExtendedTextures.SelectedCells[0].Value.ToString() == "-")
            {
                DarkMessageBox.ShowError("This cell cannot be edited.", Resources.Title_InputError);
                e.Cancel = true;
            }
        }

        private void dgvExtendedTexturesGetMaxValue(int columnIndex, out int minValue, out int maxValue)
        {
            maxValue = 0; minValue = 0;
            switch (columnIndex)
            {
                case 0: // ColOffset
                    maxValue = 2047;
                    break;
                case 1: // ColIsLOD
                    maxValue = 1;
                    break;
                case 2: // ColMask
                    maxValue = 127;
                    break;
                case 3: // ColDelay
                    maxValue = 127;
                    break;
                case 4: // ColLatency
                    maxValue = 31;
                    break;
                case 5: // ColLeap
                    maxValue = 1;
                    break;
                case 6: // ColLOD0
                case 7: // ColLOD1
                case 8: // ColLOD2
                case 9: // ColLOD3
                case 10: // ColLOD4
                case 11: // ColLOD5
                case 12: // ColLOD6
                case 13: // ColLOD7
                    maxValue = 3;
                    break;
            }
        }

        private void dgvExtendedTextures_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!(dgvExtendedTextures.SelectedCells.Count > 0)) return;
            if (dgvExtendedTextures.SelectedCells[0].Value.ToString() == "-") return;

            string inputValue = e.FormattedValue.ToString();

            if (e.ColumnIndex == ColIsLOD || e.ColumnIndex == ColLeap)
            {
                if (dgvExtendedTextures.SelectedCells[0].Value.ToString() == "-") return;

                if (!(inputValue == "True" || inputValue == "False" || inputValue == "true" || inputValue == "false"))
                {
                    DarkMessageBox.ShowError($"Invalid string: {inputValue}", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
            else
            {
                if (int.TryParse(inputValue, out int newValue))
                {
                    dgvExtendedTexturesGetMaxValue(e.ColumnIndex, out int minValue, out int maxValue);
                    if (newValue > maxValue)
                    {
                        DarkMessageBox.ShowError($"The value must be less than or equal to {maxValue}.", Resources.Title_InputError);
                        e.Cancel = true;
                    }
                    else if (newValue < minValue)
                    {
                        DarkMessageBox.ShowError($"The value must be greater than or equal to {minValue}.", Resources.Title_InputError);
                        e.Cancel = true;
                    }
                }
                else
                {
                    DarkMessageBox.ShowError($"Invalid input. Please enter an integer.", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
        }

        private void dgvExtendedTextures_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var og = model.AnimatedTextures[e.RowIndex];
            var item = dgvExtendedTextures.Rows[e.RowIndex];

            switch (e.ColumnIndex)
            {
                case 0: // ColOffset
                    og.Offset = Convert.ToInt32(item.Cells[ColOffset].Value);
                    break;
                case 1: // ColIsLOD
                    if (bool.TryParse(item.Cells[ColIsLOD].Value.ToString(), out bool islod))
                    {
                        og.IsLOD = islod;
                    }
                    break;
                case 2: // ColMask
                    og.Mask = Convert.ToInt32(item.Cells[ColMask].Value);
                    break;
                case 3: // ColDelay
                    og.Delay = Convert.ToInt32(item.Cells[ColDelay].Value);
                    break;
                case 4: // ColLatency
                    og.Latency = Convert.ToInt32(item.Cells[ColLatency].Value);
                    break;
                case 5: // ColLeap
                    if (bool.TryParse(item.Cells[ColLeap].Value.ToString(), out bool leap))
                    {
                        og.Leap = leap;
                    }
                    break;
                case 6: // ColLOD0
                    og.LOD0 = Convert.ToInt32(item.Cells[ColLOD0].Value);
                    break;
                case 7: // ColLOD1
                    og.LOD1 = Convert.ToInt32(item.Cells[ColLOD1].Value);
                    break;
                case 8: // ColLOD2
                    og.LOD2 = Convert.ToInt32(item.Cells[ColLOD2].Value);
                    break;
                case 9: // ColLOD3
                    og.LOD3 = Convert.ToInt32(item.Cells[ColLOD3].Value);
                    break;
                case 10: // ColLOD4
                    og.LOD4 = Convert.ToInt32(item.Cells[ColLOD4].Value);
                    break;
                case 11: // ColLOD5
                    og.LOD5 = Convert.ToInt32(item.Cells[ColLOD5].Value);
                    break;
                case 12: // ColLOD6
                    og.LOD6 = Convert.ToInt32(item.Cells[ColLOD6].Value);
                    break;
                case 13: // ColLOD7
                    og.LOD7 = Convert.ToInt32(item.Cells[ColLOD7].Value);
                    break;
            }
        }

        private void dgvExtendedTextures_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvExtendedTextures.SelectedCells.Count > 0)
            {
                if (e.Control is TextBox textbox)
                {
                    int col = dgvExtendedTextures.SelectedCells[0].ColumnIndex;
                    if (col == ColIsLOD || col == ColLeap)
                    {
                        textbox.KeyPress -= TextBox_KeyPress;
                    }
                    else
                    {
                        textbox.KeyPress -= TextBox_KeyPress;
                        textbox.KeyPress += TextBox_KeyPress;
                    }
                }
            }
        }
        #endregion

        #region Positions

        private void CreatePositionColumns()
        {
            dgvPositions.Columns.Add("Index", "Index");
            dgvPositions.Columns.Add("X", "X");
            dgvPositions.Columns.Add("Y", "Y");
            dgvPositions.Columns.Add("Z", "Z");
            dgvPositions.Columns.Add("XBits", "X Bits");
            dgvPositions.Columns.Add("YBits", "Y Bits");
            dgvPositions.Columns.Add("ZBits", "Z Bits");

            foreach (DataGridViewColumn column in dgvPositions.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 48;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private async Task UpdatePositionAsync()
        {
            dgvPositions.SuspendLayout();
            dgvPositions.ScrollBars = ScrollBars.None;

            var rows = await Task.Run(() =>
            {
                var rowsToAdd = new ConcurrentBag<(int Index, DataGridViewRow Row)>();

                Parallel.ForEach(Enumerable.Range(0, (int)model.Positions.Count), (int i) =>
                {
                    DataGridViewRow row = new DataGridViewRow();
                    var item = model.Positions[i];
                    row.CreateCells(dgvPositions, string.Empty, item.X, item.Y, item.Z, item.XBits, item.YBits, item.ZBits);

                    rowsToAdd.Add((i, row));
                });
                return rowsToAdd.OrderBy(pair => pair.Index).Select(pair => pair.Row).ToList();
            });

            for (int i = 0; i < rows.Count; i++)
            {
                rows[i].Cells[ColIndex].Value = i + 1;
                rows[i].Cells[ColIndex].Style = styleIndex;
                dgvPositions.Rows.Add(rows[i]);
            }

            dgvPositions.ScrollBars = ScrollBars.Vertical;
            dgvPositions.ResumeLayout();
        }

        private void dgvPositions_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!(dgvPositions.SelectedCells.Count > 0)) return;
            if (e.ColumnIndex == ColIndex) e.Cancel = true;
        }

        private void dgvPositions_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!(dgvPositions.SelectedCells.Count > 0)) return;
            if (e.ColumnIndex == ColIndex) return;

            if (int.TryParse(e.FormattedValue.ToString(), out int newValue))
            {
                int maxValue = 255;
                int minValue = 0;
                if (newValue > maxValue)
                {
                    DarkMessageBox.ShowError($"The value must be less than or equal to {maxValue}.", Resources.Title_InputError);
                    e.Cancel = true;
                }
                else if (newValue < minValue)
                {
                    DarkMessageBox.ShowError($"The value must be greater than or equal to {minValue}.", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
            else
            {
                DarkMessageBox.ShowError($"Invalid input. Please enter an integer.", Resources.Title_InputError);
                e.Cancel = true;
            }
        }

        private void dgvPositions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var og = model.Positions[e.RowIndex];
            var item = dgvPositions.Rows[e.RowIndex];

            switch (e.ColumnIndex)
            {
                case 1: // X
                    og.X = Convert.ToByte(item.Cells[ColX].Value);
                    break;
                case 2: // Y
                    og.Y = Convert.ToByte(item.Cells[ColY].Value);
                    break;
                case 3: // Z
                    og.Z = Convert.ToByte(item.Cells[ColZ].Value);
                    break;
                case 4: // XBits
                    og.XBits = Convert.ToByte(item.Cells[ColXBits].Value);
                    break;
                case 5: // YBits
                    og.YBits = Convert.ToByte(item.Cells[ColYBits].Value);
                    break;
                case 6: // ZBits
                    og.ZBits = Convert.ToByte(item.Cells[ColZBits].Value);
                    break;
            }
        }

        private void dgvPositions_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvPositions.SelectedCells.Count > 0)
            {
                if (e.Control is TextBox textbox)
                {
                    textbox.KeyPress -= TextBox_KeyPress;
                    textbox.KeyPress += TextBox_KeyPress;
                }
            }
        }

        private void dgv_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (e.Value is string strValue)
            {
                if (int.TryParse(strValue, out int result))
                {
                    e.Value = result.ToString();
                    e.ParsingApplied = true;
                }
            }
        }
        #endregion
    }
}