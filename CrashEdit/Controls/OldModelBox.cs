using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using Chunk = CrashEdit.Crash.Chunk;

namespace CrashEdit.CE
{
    public partial class OldModelBox : UserControl
    {
        DataGridViewCellStyle structColorStyle = new DataGridViewCellStyle
        {
            ForeColor = Color.Turquoise
        };
        DataGridViewCellStyle maxValueStyle = new DataGridViewCellStyle
        {
            ForeColor = Color.Turquoise
        };
        DataGridViewCellStyle defaultValueStyle = new DataGridViewCellStyle
        {
            ForeColor = Color.Gainsboro
        };

        private OldModelEntryController controller;
        private OldModelEntry model;
        private TextureChunk chunk = null!;

        private List<object> structs = new List<object>();

        private DarkToolTip tipReloadTPage;

        private Rectangle selectedregion;

        private bool simpleMode;
        private bool BGRAMode;
        private bool replaceCLUT;
        private int selectedRegionX;
        private int selectedRegionY;
        private int currentColorMode;

        private int ColIndex = 0;
        private int ColPage = 1;
        private int ColR = 2;
        private int ColG = 3;
        private int ColB = 4;
        private int ColN = 5;
        private int ColClutX = 6;
        private int ColClutY = 7;
        private int ColX = 8;
        private int ColY = 9;
        private int ColUV = 10;
        private int ColSegment = 11;
        private int ColBlendMode = 12;
        private int ColColorMode = 13;
        private int ColU1 = 14;
        private int ColU2 = 15;
        private int ColU3 = 16;
        private int ColV1 = 17;
        private int ColV2 = 18;
        private int ColV3 = 19;

        private Color clrBackground = Color.FromArgb(40, 40, 40);
        private Color clrAltBackground = Color.FromArgb(34, 34, 34);
        private Color clrSelectionBackground = Color.FromArgb(70, 70, 70);
        private Color clrText = Color.Gainsboro;

        internal Stack<bool> dirty = new Stack<bool>();
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        public OldModelBox(OldModelEntryController controller)
        {
            this.controller = controller;
            model = controller.OldModelEntry;
            MainInit();
        }

        private void MainInit()
        {
            InitializeComponent();
            DoubleBuffered = true;

            dirty.Push(true);
            SetCVal(numScaleX, model.ScaleX);
            SetCVal(numScaleY, model.ScaleY);
            SetCVal(numScaleZ, model.ScaleZ);
            UpdateInfo();
            dirty.Pop();
        }

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
            lblModelInfo.Text = string.Format("Polygon count: {0}", model.PolygonsCount);
        }

        private void tbpPolygons_Enter(object sender, EventArgs e)
        {
            UpdatePolygons();
            tbpPolygons.Enter -= tbpPolygons_Enter;
        }

        private void tbpTextures_Enter(object sender, EventArgs e)
        {
            updateTextures();
            tbpPolygons.Enter -= tbpTextures_Enter;
        }

        private void UpdatePolygons()
        {
            dirty.Push(true);
            DoubleBufferedDataGridView.Initialize(dgvPolygons);
            dgvPolygons.Columns.Add("VertexA", "Vertex A");
            dgvPolygons.Columns.Add("VertexB", "Vertex B");
            dgvPolygons.Columns.Add("VertexC", "Vertex C");
            dgvPolygons.Columns.Add("TexInfo", "Texture Info");
            dgvPolygons.Columns.Add("NoLight", "No Light");
            foreach (OldModelPolygon polygon in model.Polygons)
            {
                dgvPolygons.Rows.Add(polygon.VertexA / 6, polygon.VertexB / 6, polygon.VertexC / 6, polygon.TexInfo, polygon.NoLight);
            }
            dirty.Pop();
        }

        private async void updateTextures()
        {
            tipReloadTPage = new DarkToolTip();
            tipReloadTPage.SetToolTip(rbtReloadTPage, "Reload");
            DoubleBufferedDataGridView.Initialize(dgvTextures);
            CreateTextureListColumns();
            UpdateTPageList();
            await UpdateTextureListAsync(true);
            if (dgvTextures.Rows.Count > 0)
            {
                fraTPage.Enabled =
                fraSwitches.Enabled =
                fraReplace.Enabled =
                fraReplaceTexture.Enabled = true;
                trkPictureSize.Visible = true;
                pnPicture.AutoScroll = true;
            }

            //BGRAMode =
            //replaceCLUT = true;

            //numReplaceTo.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);

            tbpTextures.Enter -= tbpTextures_Enter;
        }

        private void UpdateTPageList()
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

        private void SetMaxValueTag(int start, int end)
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
                            //if (row.Cells[col].Tag is HashSet<string> tags)
                            //    tags.Add("MaxValue");
                            row.Cells[col].Style = maxValueStyle;
                        }
                    }
                }
            });
        }

        private void CreateTextureListColumns()
        {
            dgvTextures.Columns.Add("Index", "Index");
            dgvTextures.Columns.Add("Page", "Page");
            dgvTextures.Columns.Add("R", "R\u3000");
            dgvTextures.Columns.Add("G", "G\u3000");
            dgvTextures.Columns.Add("B", "B\u3000");
            dgvTextures.Columns.Add("N", "N\u3000");
            dgvTextures.Columns.Add("ClutX", "Clut X");
            dgvTextures.Columns.Add("ClutY", "Clut Y");
            dgvTextures.Columns.Add("X", "X\u3000");
            dgvTextures.Columns.Add("Y", "Y\u3000");
            dgvTextures.Columns.Add("UV", "UV\u3000");
            dgvTextures.Columns.Add("Segment", "Segment");
            dgvTextures.Columns.Add("BlendMode", "Blend");
            dgvTextures.Columns.Add("ColorMode", "Color");
            dgvTextures.Columns.Add("U1", "U1\u3000");
            dgvTextures.Columns.Add("U2", "U2\u3000");
            dgvTextures.Columns.Add("U3", "U3\u3000");
            dgvTextures.Columns.Add("V1", "V1\u3000");
            dgvTextures.Columns.Add("V2", "V2\u3000");
            dgvTextures.Columns.Add("V3", "V3\u3000");

            foreach (DataGridViewColumn column in dgvTextures.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
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

                Parallel.ForEach(Enumerable.Range(0, (int)model.Structs.Count), (int i) =>
                {
                    if (model.Structs[i] is OldModelTexture tex)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgvTextures,
                            0, Entry.EIDToEName(tex.EID), tex.R, tex.G, tex.B, tex.N ? 1 : 0, tex.ClutX, tex.ClutY,
                            tex.XOffU, tex.YOffU, tex.UVIndex, tex.Segment, tex.BlendMode, tex.ColorMode,
                            tex.U1, tex.U2, tex.U3, tex.V1, tex.V2, tex.V3);
                        var tagValue = $"{tex.ClutX}, {tex.ClutY}, {tex.XOffU}, {tex.YOffU}";
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            cell.Tag = tagValue;
                        }
                        rowsToAdd.Add((i, row));
                    }
                    else if (model.Structs[i] is OldSceneryColor col)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgvTextures, 1, string.Empty, col.R, col.G, col.B, col.N ? 1 : 0);
                        rowsToAdd.Add((i, row));
                    }
                    else
                    {
                    }
                });
                return rowsToAdd.OrderBy(pair => pair.Index).Select(pair => pair.Row).ToList();
            });
            dgvTextures.Rows.Clear();

            int visibleRowIndex = 0;
            int structIndex = 0;
            foreach (var row in rows)
            {
                if (Convert.ToInt32(row.Cells[ColIndex].Value) == 0) // OldModelTexture
                {
                    row.Cells[ColIndex].Value = structIndex;
                    structIndex += 3;
                }
                else // OldSceneryColor
                {
                    row.Cells[ColIndex].Style = structColorStyle;
                    row.Cells[ColIndex].Value = structIndex;
                    structIndex++;
                }
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

            SetMaxValueTag(ColU1, ColU3);
            SetMaxValueTag(ColV1, ColV3);

            stopwatch.Stop();
            int count = simpleMode ? seenTags.Count : rows.Count;
            Console.WriteLine($"Row count: {count}");
            Console.WriteLine($"Processing time: {stopwatch.Elapsed.TotalSeconds:F3} seconds");
            dgvTextures.ScrollBars = ScrollBars.Both;
            dgvTextures.ResumeLayout();
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

        private void chkScalesShowAsHex_CheckedChanged(object sender, EventArgs e)
        {
            numScaleX.Hexadecimal =
            numScaleY.Hexadecimal =
            numScaleZ.Hexadecimal = chkScalesShowAsHex.Checked;
            SetCVal(numScaleX, (long)numScaleX.Value);
            SetCVal(numScaleY, (long)numScaleY.Value);
            SetCVal(numScaleZ, (long)numScaleZ.Value);
        }

        private void dgvPolygons_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!Dirty)
            {
                var row = dgvPolygons.Rows[e.RowIndex];
                model.Polygons[e.RowIndex] = new OldModelPolygon(
                    (short)(6 * Convert.ToInt16(row.Cells[0].Value)),
                    (short)(6 * Convert.ToInt16(row.Cells[1].Value)),
                    (short)(6 * Convert.ToInt16(row.Cells[2].Value)),
                    Convert.ToInt16(row.Cells[3].Value),
                    Convert.ToBoolean(row.Cells[4].Value)
                    );
            }
        }

        private void dpdTPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = dpdTPage.Text;
            if (!Dirty)
            {
                if (dgvTextures.SelectedCells.Count > 0)
                {
                    int row = dgvTextures.SelectedCells[0].RowIndex;
                    dgvTextures.Rows[row].Cells[ColPage].Value = text;
                }
            }
            UpdatePicture();
        }

        private void dgvTextures_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTextures.SelectedCells.Count > 0)
            {
                dirty.Push(true);
                int rowIndex = dgvTextures.SelectedCells[0].RowIndex;
                var row = dgvTextures.Rows[rowIndex];
                var cell = dgvTextures.SelectedCells[0];

                chkMaxValueFlag.Enabled = (cell.ColumnIndex >= ColU1 && cell.ColumnIndex <= ColV3) ? true : false;
                chkMaxValueFlag.Checked = cell.Style == maxValueStyle ? true : false;

                UpdatePicture();

                //numReplace.Value = Convert.ToInt32(grdTextures.CurrentCell.Value);
                //numReplaceTo.Value = numReplace.Value;
                numRowIndex.Value = dgvTextures.CurrentCell.RowIndex;
                dpdTPage.Text = Convert.ToString(row.Cells[ColPage].Value);

                if (Settings.Default.OutputModelTextureInfo && dgvTextures.CurrentCell.Tag is string tags)
                {
                    Console.WriteLine($"Row {dgvTextures.CurrentCell.RowIndex} Tags: {string.Join(", ", tags)}");
                }
                dirty.Pop();
            }
        }

        private void dgvTextures_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex <= ColPage || e.ColumnIndex >= ColU1) e.Cancel = true;
            if (dgvTextures.Rows[e.RowIndex].Cells[ColIndex].Style == structColorStyle)
            {
                if (e.ColumnIndex >= ColClutX) e.Cancel = true;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
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

        private void dgvTexturesGetMaxValue(int rowIndex, int columnIndex, int newValue, out int minValue, out int maxValue, out bool isMaxCell)
        {
            isMaxCell = dgvTextures.Rows[rowIndex].Cells[columnIndex].Style == maxValueStyle;
            maxValue = 0; minValue = 0;
            // R, G, B
            if (columnIndex >= ColR && columnIndex <= ColB)
                maxValue = 255;
            // N
            else if (columnIndex == ColN)
                maxValue = 1;
            // ClutX
            else if (columnIndex == ColClutX)
                maxValue = 15;
            // ClutY
            else if (columnIndex == ColClutY)
                maxValue = 127;
            // X
            else if (columnIndex == ColX)
                maxValue = 31;
            // Y
            else if (columnIndex == ColY)
                maxValue = 31;
            // UV Index
            else if (columnIndex == ColUV)
                maxValue = 1023;
            // Segment
            else if (columnIndex == ColSegment)
                maxValue = 3;
            // Blend Mode
            else if (columnIndex == ColBlendMode)
                maxValue = 3;
            // Color Mode
            else if (columnIndex == ColColorMode)
                maxValue = 2;
        }

        private void dgvTextures_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (e.ColumnIndex <= ColPage || e.ColumnIndex >= ColU1) return;
            if (dgvTextures.Rows[e.RowIndex].Cells[ColIndex].Style == structColorStyle)
            {
                if (e.ColumnIndex >= ColClutX) return;
            }

            if (int.TryParse(e.FormattedValue.ToString(), out int newValue))
            {
                dgvTexturesGetMaxValue(e.RowIndex, e.ColumnIndex, newValue, out int minValue, out int maxValue, out bool isMaxCell);
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

        private void dgvTextures_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var row = dgvTextures.Rows[e.RowIndex];

            var selectedGroup = model.Structs[Convert.ToInt32(row.Cells[ColIndex].Value)];
            if (selectedGroup is OldModelTexture tex)
            {
                // Page
                if (e.ColumnIndex == ColPage)
                    tex.EID = Entry.ENameToEID(Convert.ToString(row.Cells[ColPage].Value));
                // R
                else if (e.ColumnIndex == ColR)
                    tex.R = Convert.ToByte(row.Cells[ColR].Value);
                // G
                else if (e.ColumnIndex == ColG)
                    tex.G = Convert.ToByte(row.Cells[ColG].Value);
                // B
                else if (e.ColumnIndex == ColB)
                    tex.B = Convert.ToByte(row.Cells[ColB].Value);
                // N
                else if (e.ColumnIndex == ColN)
                    tex.N = Convert.ToInt32(row.Cells[ColN].Value) == 0 ? false : true;
                // ClutX
                else if (e.ColumnIndex == ColClutX)
                    tex.ClutX = Convert.ToByte(row.Cells[ColClutX].Value);
                // ClutY
                else if (e.ColumnIndex == ColClutY)
                    tex.ClutY = Convert.ToByte(row.Cells[ColClutY].Value);
                // X
                else if (e.ColumnIndex == ColX)
                    tex.XOffU = Convert.ToByte(row.Cells[ColX].Value);
                // Y
                else if (e.ColumnIndex == ColY)
                    tex.YOffU = Convert.ToByte(row.Cells[ColY].Value);
                // Segment
                else if (e.ColumnIndex == ColSegment)
                    tex.Segment = Convert.ToByte(row.Cells[ColSegment].Value);
                // UV
                else if (e.ColumnIndex == ColUV)
                    tex.UVIndex = Convert.ToInt32(row.Cells[ColUV].Value);
                // Blend Mode
                else if (e.ColumnIndex == ColBlendMode)
                    tex.BlendMode = Convert.ToByte(row.Cells[ColBlendMode].Value);
                // Color Mode
                else if (e.ColumnIndex == ColColorMode)
                    tex.ColorMode = Convert.ToByte(row.Cells[ColColorMode].Value);

                RecalculateUVs(tex);
            }
            else if (selectedGroup is OldSceneryColor col)
            {
                // R
                if (e.ColumnIndex == ColR)
                    col.R = Convert.ToByte(row.Cells[ColR].Value);
                // G
                else if (e.ColumnIndex == ColG)
                    col.G = Convert.ToByte(row.Cells[ColG].Value);
                // B
                else if (e.ColumnIndex == ColB)
                    col.B = Convert.ToByte(row.Cells[ColB].Value);
                // N
                else if (e.ColumnIndex == ColN)
                    col.N = Convert.ToInt32(row.Cells[ColN].Value) == 0 ? false : true;
            }
        }

        private void RecalculateUVs(OldModelTexture tex)
        {
            int w = 4 << (tex.UVIndex % 5);
            int h = 4 << ((tex.UVIndex / 5) % 5);
            int xoff = ((64 << (2 - tex.ColorMode)) * tex.Segment) + ((2 << (2 - tex.ColorMode)) * tex.XOffU);
            int yoff = tex.YOffU * 4;
            int winding = tex.UVIndex / 25;
            tex.U1 = w * ((0x30FF0C >> winding) & 1) + xoff;
            tex.U2 = w * ((0x8799E1 >> winding) & 1) + xoff;
            tex.U3 = w * ((0x4B66D2 >> winding) & 1) + xoff;
            tex.V1 = h * ((0xF3CC30 >> winding) & 1) + yoff;
            tex.V2 = h * ((0x9E7186 >> winding) & 1) + yoff;
            tex.V3 = h * ((0x6DB249 >> winding) & 1) + yoff;
        }

        private void UpdatePicture()
        {
            if (!(dgvTextures.SelectedCells.Count > 0)) return;

            var row = dgvTextures.Rows[dgvTextures.SelectedCells[0].RowIndex];

            int _r = Convert.ToInt32(row.Cells[ColR].Value);
            int _g = Convert.ToInt32(row.Cells[ColG].Value);
            int _b = Convert.ToInt32(row.Cells[ColB].Value);
            Color texelColor = Color.FromArgb(_r, _g, _b);

            if (row.Cells[ColIndex].Style == structColorStyle)
            {
                pictureBox1.Image = null;
                pictureBox1.BackColor = texelColor;
                return;
            }

            string eid = row.Cells[ColPage].Value.ToString();
            chunk = controller.GetEntry<TextureChunk>(Entry.ENameToEID(eid));
            dpdTPage.SelectedItem = eid;

            int TexCX = Convert.ToInt32(row.Cells[ColClutX].Value);
            int TexCY = Convert.ToInt32(row.Cells[ColClutY].Value);
            int colormode = Convert.ToInt32(row.Cells[ColColorMode].Value);
            int blendmode = Convert.ToInt32(row.Cells[ColBlendMode].Value);

            int TexX, TexY, TexW, TexH;
            int XOffU = Convert.ToInt32(row.Cells[ColX].Value);
            int YOffU = Convert.ToInt32(row.Cells[ColY].Value);
            int segment = Convert.ToInt32(row.Cells[ColSegment].Value);
            int uvIndex = Convert.ToInt32(row.Cells[ColUV].Value);

            TexX = ((64 << (2 - colormode)) * segment) + ((2 << (2 - colormode)) * XOffU);
            TexY = YOffU * 4;
            TexW = 4 << (uvIndex % 5);
            TexH = 4 << ((uvIndex / 5) % 5);

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
                        Marshal.WriteInt32(bdata.Scan0, x * 4 + y * bdata.Stride, pixel);
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
            }

            Bitmap filteredBitmap = ApplyTexel(bitmap, texelColor);
            pictureBox1.Image = filteredBitmap;
            pictureBox1.BackColor = Color.Transparent;

            float zoom = trkPictureSize.Value / 100f;
            pictureBox1.Width = (int)(pictureBox1.Image.Width * zoom);
            pictureBox1.Height = (int)(pictureBox1.Image.Height * zoom);

            currentColorMode = colormode;
        }

        private Bitmap ApplyTexel(Bitmap vertexBitmap, Color texelColor)
        {
            Bitmap outputBitmap = new Bitmap(vertexBitmap.Width, vertexBitmap.Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new Rectangle(0, 0, vertexBitmap.Width, vertexBitmap.Height);

            BitmapData vertexData = vertexBitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData outputData = outputBitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            try
            {
                float rFactor = texelColor.R / 255f;
                float gFactor = texelColor.G / 255f;
                float bFactor = texelColor.B / 255f;
                float aFactor = texelColor.A / 255f;

                int bytes = Math.Abs(vertexData.Stride) * vertexBitmap.Height;
                byte[] vertexBytes = new byte[bytes];
                byte[] outputBytes = new byte[bytes];

                Marshal.Copy(vertexData.Scan0, vertexBytes, 0, bytes);

                for (int i = 0; i < vertexBytes.Length; i += 4)
                {
                    byte vr = vertexBytes[i + 2]; // R
                    byte vg = vertexBytes[i + 1]; // G
                    byte vb = vertexBytes[i];     // B
                    byte va = vertexBytes[i + 3]; // A

                    outputBytes[i + 2] = (byte)Math.Min(255, vr * rFactor * 2);
                    outputBytes[i + 1] = (byte)Math.Min(255, vg * gFactor * 2);
                    outputBytes[i] = (byte)Math.Min(255, vb * bFactor * 2);
                    outputBytes[i + 3] = (byte)Math.Min(255, va * aFactor * 2);
                }

                Marshal.Copy(outputBytes, 0, outputData.Scan0, bytes);
            }
            finally
            {
                vertexBitmap.UnlockBits(vertexData);
                outputBitmap.UnlockBits(outputData);
            }

            return outputBitmap;
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

        private void rbtReloadTPage_Click(object sender, EventArgs e)
        {
            if (dpdTPage.Items.Count > 0)
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
                if (dgvTextures.SelectedCells.Count > 0)
                {
                    int row = dgvTextures.SelectedCells[0].RowIndex;
                    dpdTPage.Text = dgvTextures.Rows[row].Cells[ColPage].Value.ToString();
                }
            }
            rbtReloadTPage.Checked = false;
        }
    }
}
