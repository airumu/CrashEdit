using System.Collections.Concurrent;
using System.Diagnostics;
using AltUI.Controls;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public partial class OldModelBox : UserControl
    {
        DataGridViewCellStyle maxValueStyle = new DataGridViewCellStyle
        {
            ForeColor = Color.Turquoise
        }; DataGridViewCellStyle defaultValueStyle = new DataGridViewCellStyle
        {
            ForeColor = Color.Gainsboro
        };

        private OldModelEntryController controller;
        private OldModelEntry model;

        private DarkToolTip tipReloadTPage;

        private bool simpleMode;
        private bool BGRAMode;
        private bool replaceCLUT;
        private int selectedRegionX;
        private int selectedRegionY;
        private int currentColorMode;

        private int ColR = 0;
        private int ColG = 1;
        private int ColB = 2;
        private int ColClutX = 3;
        private int ColClutY = 4;
        private int ColU1 = 5;
        private int ColU2 = 6;
        private int ColU3 = 7;
        private int ColV1 = 8;
        private int ColV2 = 9;
        private int ColV3 = 10;
        private int ColX = 11;
        private int ColY = 12;
        private int ColUV = 13;
        private int ColSegment = 14;
        private int ColBlendMode = 15;
        private int ColColorMode = 16;

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
            DoubleBufferedDataGridView.Initialize(grdTextures);
            CreateTextureListColumns();
            //UpdateTPageList();
            await UpdateTextureListAsync(true);
            if (grdTextures.Rows.Count > 0)
            {
                //UpdateTPageButtons();
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

        private void SetMaxValueTag(int start, int end)
        {
            int startColumnIndex = start;
            int endColumnIndex = end;

            Parallel.For(0, grdTextures.Rows.Count, rowIndex =>
            {
                var row = grdTextures.Rows[rowIndex];
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
            grdTextures.Columns.Add("R", "R\u3000");
            grdTextures.Columns.Add("G", "G\u3000");
            grdTextures.Columns.Add("B", "B\u3000");
            grdTextures.Columns.Add("ClutX", "Clut X");
            grdTextures.Columns.Add("ClutY", "Clut Y");
            grdTextures.Columns.Add("U1", "U1");
            grdTextures.Columns.Add("U2", "U2");
            grdTextures.Columns.Add("U3", "U3");
            grdTextures.Columns.Add("V1", "V1");
            grdTextures.Columns.Add("V2", "V2");
            grdTextures.Columns.Add("V3", "V3");
            grdTextures.Columns.Add("X", "X\u3000");
            grdTextures.Columns.Add("Y", "Y\u3000");
            grdTextures.Columns.Add("UV", "UV\u3000");
            grdTextures.Columns.Add("Segment", "Segment");
            grdTextures.Columns.Add("BlendMode", "Blend");
            grdTextures.Columns.Add("ColorMode", "Color");

            foreach (DataGridViewColumn column in grdTextures.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 44;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private async Task UpdateTextureListAsync(bool setMaxTags)
        {
            grdTextures.SuspendLayout();
            grdTextures.ScrollBars = ScrollBars.None;
            Stopwatch stopwatch = Stopwatch.StartNew();
            var seenTags = new ConcurrentDictionary<string, bool>();

            var rows = await Task.Run(() =>
            {
                var rowsToAdd = new ConcurrentBag<(int Index, DataGridViewRow Row)>();

                Parallel.ForEach(Enumerable.Range(0, (int)model.Structs.Count), (int i) =>
                {
                    if (model.Structs[i] is OldModelTexture item)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(grdTextures, item.R, item.G, item.B, item.ClutX, item.ClutY, item.U1, item.U2, item.U3, item.V1, item.V2, item.V3, item.XOffU, item.YOffU, item.UVIndex, item.Segment, item.BlendMode, item.ColorMode);
                        var tagValue = $"{item.ClutX}, {item.ClutY}, {item.XOffU}, {item.YOffU}";
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            cell.Tag = tagValue;
                        }
                        rowsToAdd.Add((i, row));
                    }
                });
                return rowsToAdd.OrderBy(pair => pair.Index).Select(pair => pair.Row).ToList();
            });
            grdTextures.Rows.Clear();

            int visibleRowIndex = 0;
            foreach (var row in rows)
            {
                grdTextures.Rows.Add(row);
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
            grdTextures.ScrollBars = ScrollBars.Vertical;
            grdTextures.ResumeLayout();
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
    }
}
