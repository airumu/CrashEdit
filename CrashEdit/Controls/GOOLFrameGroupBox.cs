using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public partial class GOOLFrameGroupBox : UserControl
    {
        private GOOLEntryController controller;
        private GOOLEntry goolentry;
        private TextureChunk chunk = null!;
        private List<object> frameGroups = new List<object>();
        private DataGridViewCell previousCell = null!;

        private DataGridViewCellStyle maxValueStyle = new DataGridViewCellStyle
        {
            ForeColor = Color.Turquoise
        };
        private DataGridViewCellStyle defaultValueStyle = new DataGridViewCellStyle
        {
            ForeColor = Color.Gainsboro
        };

        private int currentColorMode = 0;
        private bool simpleMode = false;
        private bool dirty = false;
        private bool tpagedirty = false;

        private readonly int ColIndex = 0;
        private readonly int ColIndexAlt = 1;
        private readonly int ColEID = 2;
        private readonly int ColFrameCount = 3;
        private readonly int ColInterpolated = 4;

        private int ColR = 0;
        private int ColG = 1;
        private int ColB = 2;
        private int ColClutX = 3;
        private int ColClutY = 4;
        private int ColLeft = 5;
        private int ColTop = 6;
        private int ColWidth = 7;
        private int ColHeight = 8;
        private int ColX1 = 9;
        private int ColX2 = 10;
        private int ColX3 = 11;
        private int ColX4 = 12;
        private int ColY1 = 13;
        private int ColY2 = 14;
        private int ColY3 = 15;
        private int ColY4 = 16;
        private int ColBlendMode = 17;
        private int ColColorMode = 18;

        private int ColX;
        private int ColY;
        private int ColUV;
        private int ColSegment;

        public GOOLFrameGroupBox(GOOLEntryController controller, GOOLEntry goolentry)
        {
            this.goolentry = goolentry;
            this.controller = controller;
        }

        public void OnTabSelected()
        {
            GOOLFrameGroupBox_Enter(this, EventArgs.Empty);
        }

        private void GOOLFrameGroupBox_Enter(object sender, EventArgs e)
        {
            InitializeComponent();

            DoubleBufferedDataGridView.Initialize(dgvFrameGroup);
            DoubleBufferedDataGridView.Initialize(dgvTexture);

            dgvFrameGroup.Visible = false;
            dgvFrameGroupCreateColumns();
            dgvTextureCreateColumns();

            if (goolentry.Version == GOOLVersion.Version1)
            {
                dgvFrameGroup.Columns[ColInterpolated].Visible = false;
                dgvFrameGroup.Width = 265;
                dgvTexture.Location = new Point(265, 3);
            }
            else
            {
                for (int i = ColX1; i <= ColY4; i++)
                    dgvTexture.Columns[i].Visible = false;
            }

            dgvFrameGroupCreateRows();

            AdjustColumnwidth(dgvFrameGroup);
            AdjustColumnwidth(dgvTexture);

            GetTpage();
            dgvFrameGroup.Visible = true;
        }

        private void GetTpage()
        {
            List<Chunk> chunks = controller.GetNSF().Chunks;
            foreach (Chunk chunk in chunks)
            {
                if (chunk is TextureChunk t)
                {
                    dpdTPages.Items.Add(Entry.EIDToEName(t.EID));
                }
            }
        }

        private void dgvFrameGroupCreateColumns()
        {
            dgvFrameGroup.Columns.Add("Index", "Index");
            dgvFrameGroup.Columns.Add("IndexAlt", "Index (Alt)");
            dgvFrameGroup.Columns.Add("EID", "EID\u3000\u3000\u3000\u3000");
            dgvFrameGroup.Columns.Add("FrameCount", "Frames");
            dgvFrameGroup.Columns.Add("Interpolated", "Interpolated");
        }

        private void dgvTextureCreateColumns()
        {
            if (goolentry.Version == GOOLVersion.Version1)
            {
                dgvTexture.Columns.Add("R", "R\u3000");
                dgvTexture.Columns.Add("G", "G\u3000");
                dgvTexture.Columns.Add("B", "B\u3000");
                dgvTexture.Columns.Add("ClutX", "Clut X");
                dgvTexture.Columns.Add("ClutY", "Clut Y");
                dgvTexture.Columns.Add("X", "X\u3000");
                dgvTexture.Columns.Add("Y", "Y\u3000");
                dgvTexture.Columns.Add("UV", "UV\u3000");
                dgvTexture.Columns.Add("Segment", "Segment");
                dgvTexture.Columns.Add("BlendMode", "Blend");
                dgvTexture.Columns.Add("ColorMode", "Color");

                ColX = 5;
                ColY = 6;
                ColUV = 7;
                ColSegment = 8;
                ColBlendMode = 9;
                ColColorMode = 10;
            }
            else
            {
                dgvTexture.Columns.Add("R", "R\u3000");
                dgvTexture.Columns.Add("G", "G\u3000");
                dgvTexture.Columns.Add("B", "B\u3000");
                dgvTexture.Columns.Add("ClutX", "Clut X");
                dgvTexture.Columns.Add("ClutY", "Clut Y");
                dgvTexture.Columns.Add("Left", "X\u3000");
                dgvTexture.Columns.Add("Top", "Y\u3000");
                dgvTexture.Columns.Add("Width", "Width");
                dgvTexture.Columns.Add("Height", "Height");
                dgvTexture.Columns.Add("X1", "X1");
                dgvTexture.Columns.Add("X2", "X2");
                dgvTexture.Columns.Add("X3", "X3");
                dgvTexture.Columns.Add("X4", "X4");
                dgvTexture.Columns.Add("Y1", "Y1");
                dgvTexture.Columns.Add("Y2", "Y2");
                dgvTexture.Columns.Add("Y3", "Y3");
                dgvTexture.Columns.Add("Y4", "Y4");
                dgvTexture.Columns.Add("BlendMode", "Blend");
                dgvTexture.Columns.Add("ColorMode", "Color");
            }
        }

        private void AdjustColumnwidth(DataGridView dataGridView)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void ToggleSimpleMode()
        {
            if (dgvTexture.IsCurrentCellInEditMode)
                dgvTexture.CancelEdit();
            dgvTexture.SuspendLayout();

            if (!simpleMode)
            {
                dgvTexture.Width = 446;
                for (int i = ColLeft; i <= ColHeight; i++)
                    dgvTexture.Columns[i].Visible = true;

                for (int i = ColX1; i <= ColY4; i++)
                    dgvTexture.Columns[i].Visible = false;
            }
            else
            {
                dgvTexture.Width = 498;
                for (int i = ColLeft; i <= ColHeight; i++)
                    dgvTexture.Columns[i].Visible = false;

                for (int i = ColX1; i <= ColY4; i++)
                    dgvTexture.Columns[i].Visible = true;
            }

            dgvTexture.ResumeLayout();
        }

        private void chkMaxValueFlag_Click(object sender, EventArgs e)
        {
            if (!(dgvTexture.SelectedCells.Count > 0)) return;

            foreach (DataGridViewCell cell in dgvTexture.SelectedCells)
            {
                if (cell.ColumnIndex >= ColX1 && cell.ColumnIndex <= ColY4)
                {
                    cell.Style = chkMaxValueFlag.Checked ? maxValueStyle : defaultValueStyle;
                }
            }
        }

        private void SetMaxValueStyle(int startCol, int endCol)
        {
            Parallel.For(0, dgvTexture.Rows.Count, rowIndex =>
            {
                var row = dgvTexture.Rows[rowIndex];
                int maxValue = int.MinValue;
                HashSet<int> keys = [];

                for (int col = startCol; col <= endCol; col++)
                {
                    int value = Convert.ToInt32(row.Cells[col].Value);
                    if (value > maxValue)
                    {
                        maxValue = value;
                    }
                    keys.Add(value);
                }
                // If all values are the same, set the default
                if (keys.Count == 1)
                {
                    if (startCol == ColX1)
                    {
                        row.Cells[ColX2].Style = maxValueStyle;
                        row.Cells[ColX4].Style = maxValueStyle;
                    }
                    else if (startCol == ColY1)
                    {
                        row.Cells[ColY3].Style = maxValueStyle;
                        row.Cells[ColY4].Style = maxValueStyle;
                    }
                    return;
                }

                for (int col = startCol; col <= endCol; col++)
                {
                    int value = Convert.ToInt32(row.Cells[col].Value);
                    if (value == maxValue)
                    {
                        row.Cells[col].Style = maxValueStyle;
                    }
                }
            });
        }

        private void GetIndex(int index, out string index1, out string index2)
        {
            index1 = string.Format("{0:X}", NumberExt.TransformedString(index));
            index2 = string.Format("0x{0:X}00", index.ToString("X"));
        }

        private void dgvFrameGroupCreateRows()
        {
            dgvFrameGroup.ScrollBars = ScrollBars.None;
            dgvFrameGroup.SuspendLayout();
            dirty = true;
            foreach (var group in goolentry.FrameGroups)
            {
                dynamic vgroup = null!;
                if (group is VertexGroup)
                {
                    vgroup = (VertexGroup)group;
                }
                else if (group is VertexGroup2)
                {
                    vgroup = (VertexGroup2)group;
                }
                else if (group is VertexGroup3to2)
                {
                    vgroup = (VertexGroup3to2)group;
                }
                else if (group is SpriteGroup)
                {
                    vgroup = (SpriteGroup)group;
                }
                else if (group is SpriteGroup2)
                {
                    vgroup = (SpriteGroup2)group;
                }
                else if (group is FontGroup)
                {
                    vgroup = (FontGroup)group;
                }
                else if (group is FontGroup2)
                {
                    vgroup = (FontGroup2)group;
                }
                else if (group is TextGroup)
                {
                    vgroup = (TextGroup)group;
                }
                else if (group is ImageGroup)
                {
                    vgroup = (ImageGroup)group;
                }
                else if (group is ImageGroup2)
                {
                    vgroup = (ImageGroup2)group;
                }
                else continue;

                frameGroups.Add(vgroup);
                GetIndex(vgroup.Index / 4, out string index1, out string index2);

                DataGridViewRow row = new DataGridViewRow();
                if (group is VertexGroup2 || group is VertexGroup3to2)
                {
                    row.CreateCells(dgvFrameGroup, index1, index2, Entry.EIDToEName(vgroup.EID), vgroup.FrameCount, vgroup.Interpolated);
                }
                else
                {
                    row.CreateCells(dgvFrameGroup, index1, index2, Entry.EIDToEName(vgroup.EID), vgroup.FrameCount,  "-");
                }
                row.Tag = vgroup.Index;
                dgvFrameGroup.Rows.Add(row);
            }
            dgvFrameGroup.ScrollBars = ScrollBars.Vertical;
            dgvFrameGroup.ResumeLayout();
            dirty = false;
        }

        private void dgvFrameGroup_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

            if (e.ColumnIndex >= ColIndex && e.ColumnIndex <= ColIndexAlt)
            {
                //DarkMessageBox.ShowError("This cell cannot be edited.", Resources.Title_InputError);
                e.Cancel = true;
            }

            if (dgvFrameGroup.SelectedCells.Count > 0)
            {
                object selectedGroup = frameGroups[e.RowIndex];
                if ((e.ColumnIndex == ColFrameCount && !(selectedGroup is VertexGroup2) && !(selectedGroup is VertexGroup3to2)) ||
                    (dgvFrameGroup.SelectedCells[0].Value.ToString() == "-")) // if interpolated is not set
                {
                    //DarkMessageBox.ShowError("This cell cannot be edited.", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
        }

        private void dgvFrameGroup_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex >= ColIndex && e.ColumnIndex <= ColIndexAlt) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !(dgvFrameGroup.SelectedCells.Count > 0)) return;

            string inputValue = e.FormattedValue.ToString();

            if (e.ColumnIndex == ColFrameCount)
            {
                if (int.TryParse(inputValue, out int newValue))
                {
                    try
                    {
                        int maxValue = 255, minValue = 0;
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
                    catch (Exception ex)
                    {
                        DarkMessageBox.ShowError($"Invalid value: {inputValue}\nError: {ex.Message}", Resources.Title_ValidationError);
                        e.Cancel = true;
                    }
                }
                else
                {
                    DarkMessageBox.ShowError($"Invalid input. Please enter an integer.", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
            else if (e.ColumnIndex == ColEID)
            {
                string input = Entry.CheckEIDErrors(inputValue, true);
                if (input != string.Empty)
                {
                    DarkMessageBox.ShowError($"Invalid EID string: {inputValue}", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
            else if (e.ColumnIndex == ColInterpolated)
            {
                if (dgvFrameGroup.SelectedCells[0].Value.ToString() == "-") return;

                if (!(inputValue == "True" || inputValue == "False" || inputValue == "true" || inputValue == "false"))
                {
                    DarkMessageBox.ShowError($"Invalid string: {inputValue}", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }

        }

        private void dgvFrameGroup_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !(dgvFrameGroup.SelectedCells.Count > 0)) return;

            var row = dgvFrameGroup.Rows[e.RowIndex];
            dynamic og = frameGroups[e.RowIndex];

            // FrameCount
            if (e.ColumnIndex == ColFrameCount)
            {
                og.FrameCount = Convert.ToByte(row.Cells[ColFrameCount].Value);
            }
            // EID
            else if (e.ColumnIndex == ColEID)
            {
                og.EID = Entry.ENameToEID(row.Cells[ColEID].Value.ToString());
            }
            // Interpolated
            else if (e.ColumnIndex == ColInterpolated)
            {
                if (bool.TryParse(row.Cells[ColInterpolated].Value.ToString(), out bool result))
                {
                    og.Interpolated = result;
                }
            }

            DebugOutput($"Row Index: {e.RowIndex}");
        }


        private void GetXOff(int colorMode, int value, out int xoffUnit, out int segment, out int xoff)
        {
            xoffUnit = (1 << (2 - colorMode)) * 64;
            segment = value / xoffUnit;
            xoff = xoffUnit * segment;
        }

        private void GetMaxValue(int rowIndex, int columnIndex, int newValue, out int minValue, out int maxValue, out bool isMaxCell)
        {
            isMaxCell = dgvTexture.Rows[rowIndex].Cells[columnIndex].Style == maxValueStyle;
            maxValue = 0; minValue = 0;
            // R, G, B
            if (columnIndex >= ColR && columnIndex <= ColB)
                maxValue = 255;
            // ClutX
            else if (columnIndex == ColClutX)
                maxValue = 15;
            // ClutY
            else if (columnIndex == ColClutY)
                maxValue = 127;
            // Blend Mode
            else if (columnIndex == ColBlendMode)
                maxValue = 3;
            // Color Mode
            else if (columnIndex == ColColorMode)
                maxValue = 2;

            if (goolentry.Version == GOOLVersion.Version1)
            {
                // X
                if (columnIndex == ColX)
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
            }
            else
            {
                // X (Left)
                if (columnIndex == ColLeft)
                {
                    int width = Convert.ToInt32(dgvTexture.Rows[rowIndex].Cells[ColWidth].Value);
                    GetXOff(currentColorMode, newValue, out int xoffUnit, out int segment, out int xoff);

                    int pw = 256 << (2 - currentColorMode);
                    if (newValue >= (isMaxCell ? pw + width : pw))
                    {
                        maxValue = isMaxCell ? pw : pw - width;
                        return;
                    }

                    int xoffEnd = xoff + xoffUnit;
                    maxValue = xoffEnd - width;
                    DebugOutput($"Segment {segment}, maxValue {maxValue}");
                }
                // Y (Top)
                else if (columnIndex == ColTop)
                    maxValue = 128 - Convert.ToInt32(dgvTexture.Rows[rowIndex].Cells[ColHeight].Value);
                // Width
                else if (columnIndex == ColWidth)
                {
                    int value = Convert.ToInt32(dgvTexture.Rows[rowIndex].Cells[ColLeft].Value);

                    GetXOff(currentColorMode, value, out int xoffUnit, out int segment, out int xoff);

                    maxValue = xoffUnit - (value - xoff);
                    DebugOutput($"Segment {segment}, maxValue {maxValue}");
                }
                // Height
                else if (columnIndex == ColHeight)
                {
                    maxValue = 128 - Convert.ToInt32(dgvTexture.Rows[rowIndex].Cells[ColTop].Value);
                }
            }
        }

        private void dgvTexture_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex >= ColX1 && e.ColumnIndex <= ColY4 && !(goolentry.Version == GOOLVersion.Version1))
            {
                //DarkMessageBox.ShowError("This cell cannot be edited.", Resources.Title_InputError);
                e.Cancel = true;
            }
        }

        private void dgvTexture_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex >= ColX1 && e.ColumnIndex <= ColY4) return;

            string inputValue = e.FormattedValue.ToString();

            if (int.TryParse(inputValue, out int newValue))
            {
                try
                {
                    GetMaxValue(e.RowIndex, e.ColumnIndex, newValue, out int minValue, out int maxValue, out bool isMaxCell);

                    if (newValue > maxValue)
                    {
                        if (e.ColumnIndex >= ColLeft && e.ColumnIndex <= ColHeight && !(goolentry.Version == GOOLVersion.Version1))
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
                catch (Exception ex)
                {
                    DarkMessageBox.ShowError($"Invalid value: {inputValue}\nError: {ex.Message}", Resources.Title_ValidationError);
                    e.Cancel = true;
                }
            }
            else
            {
                DarkMessageBox.ShowError($"Invalid input. Please enter an integer.", Resources.Title_InputError);
                e.Cancel = true;
            }
        }

        private void dgvTexture_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !(dgvFrameGroup.SelectedCells.Count > 0) || !(dgvTexture.SelectedCells.Count > 0)) return;

            int rowIndex = dgvFrameGroup.SelectedCells[0].RowIndex;
            var item = dgvTexture.Rows[e.RowIndex];
            int colorMode = Convert.ToInt32(item.Cells[ColColorMode].Value);

            if (goolentry.Version == GOOLVersion.Version1)
            {
                var selectedGroup = frameGroups[rowIndex];
                if (!(selectedGroup is SpriteGroup _spriteGroup)) return;

                var og = _spriteGroup.Frames[e.RowIndex];

                // R
                if (e.ColumnIndex == ColR)
                    og.R = Convert.ToByte(item.Cells[ColR].Value);
                // G
                else if (e.ColumnIndex == ColG)
                    og.G = Convert.ToByte(item.Cells[ColG].Value);
                // B
                else if (e.ColumnIndex == ColB)
                    og.B = Convert.ToByte(item.Cells[ColB].Value);
                // ClutX
                else if (e.ColumnIndex == ColClutX)
                    og.ClutX = Convert.ToByte(item.Cells[ColClutX].Value);
                // ClutY
                else if (e.ColumnIndex == ColClutY)
                    og.ClutY = Convert.ToByte(item.Cells[ColClutY].Value);
                // X
                else if (e.ColumnIndex == ColX)
                    og.X = Convert.ToByte(item.Cells[ColX].Value);
                // Y
                else if (e.ColumnIndex == ColY)
                    og.Y = Convert.ToByte(item.Cells[ColY].Value);
                // Segment
                else if (e.ColumnIndex == ColSegment)
                    og.Segment = Convert.ToByte(item.Cells[ColSegment].Value);
                // UV
                else if (e.ColumnIndex == ColUV)
                    og.UV = Convert.ToInt32(item.Cells[ColUV].Value);
                // Blend Mode
                else if (e.ColumnIndex == ColBlendMode)
                    og.BlendMode = Convert.ToByte(item.Cells[ColBlendMode].Value);
                // Color Mode
                else if (e.ColumnIndex == ColColorMode)
                    og.ColorMode = Convert.ToByte(item.Cells[ColColorMode].Value);
            }
            else
            {
                var selectedGroup = frameGroups[rowIndex];
                if (!(selectedGroup is SpriteGroup2 _spriteGroup2)) return;

                var og = _spriteGroup2.Frames[e.RowIndex];

                // R
                if (e.ColumnIndex == ColR)
                    og.R = Convert.ToByte(item.Cells[ColR].Value);
                // G
                else if (e.ColumnIndex == ColG)
                    og.G = Convert.ToByte(item.Cells[ColG].Value);
                // B
                else if (e.ColumnIndex == ColB)
                    og.B = Convert.ToByte(item.Cells[ColB].Value);
                // ClutX
                else if (e.ColumnIndex == ColClutX)
                    og.ClutX = Convert.ToByte(item.Cells[ColClutX].Value);
                // ClutY
                else if (e.ColumnIndex == ColClutY)
                    og.ClutY = Convert.ToByte(item.Cells[ColClutY].Value);
                // X (Left), Width
                else if (e.ColumnIndex == ColLeft || e.ColumnIndex == ColWidth)
                {
                    int left = Convert.ToInt32(item.Cells[ColLeft].Value);
                    int width = Convert.ToInt32(item.Cells[ColWidth].Value);
                    og.Left = left;
                    og.Width = width;

                    int x1 = og.X1, x2 = og.X2, x3 = og.X3, x4 = og.X4;
                    int minX = Math.Min(x1, Math.Min(x2, Math.Min(x3, x4)));
                    int maxX = Math.Max(x1, Math.Max(x2, Math.Max(x3, x4)));

                    GetXOff(colorMode, left, out int xoffUnit, out int segment, out int xoff);
                    int newMinU = left - xoff;
                    int newMaxU = newMinU + width - 1;

                    og.U1 = (item.Cells[ColX1].Style != maxValueStyle) ? newMinU : newMaxU;
                    og.U2 = (item.Cells[ColX2].Style != maxValueStyle) ? newMinU : newMaxU;
                    og.U3 = (item.Cells[ColX3].Style != maxValueStyle) ? newMinU : newMaxU;
                    og.U4 = (item.Cells[ColX4].Style != maxValueStyle) ? newMinU : newMaxU;
                    og.Segment = (byte)segment;

                    DebugOutput($"left: {left}, width: {width}, xoffUnit: {xoffUnit}, segment: {segment}, xoff: {xoff}\n" +
                        $" x1: {x1}, x2: {x2}. x3: {x3}, x4: {x4}, minX: {minX}, maxX: {maxX}\n" +
                        $" u1: {og.U1}, u2: {og.U2}, u3: {og.U3}, u4: {og.U4}, newMinU: {newMinU}, newMaxU: {newMaxU}");
                }
                // Y (Top), Height
                else if (e.ColumnIndex == ColTop || e.ColumnIndex == ColHeight)
                {
                    int top = Convert.ToInt32(item.Cells[ColTop].Value);
                    int height = Convert.ToInt32(item.Cells[ColHeight].Value);
                    og.Top = top;
                    og.Height = height;

                    int y1 = og.Y1, y2 = og.Y2, y3 = og.Y3, y4 = og.Y4;
                    int minY = Math.Min(y1, Math.Min(y2, Math.Min(y3, y4)));
                    int maxY = Math.Max(y1, Math.Max(y2, Math.Max(y3, y4)));

                    int newMinV = top;
                    int newMaxV = newMinV + height - 1;

                    og.V1 = (item.Cells[ColY1].Style != maxValueStyle) ? newMinV : newMaxV;
                    og.V2 = (item.Cells[ColY2].Style != maxValueStyle) ? newMinV : newMaxV;
                    og.V3 = (item.Cells[ColY3].Style != maxValueStyle) ? newMinV : newMaxV;
                    og.V4 = (item.Cells[ColY4].Style != maxValueStyle) ? newMinV : newMaxV;

                    DebugOutput($"top: {top}, height: {height}\n" +
                        $" y1: {y1}, y2: {y2}. y3: {y3}, y4: {y4}, minX: {minY}, maxY: {maxY}\n" +
                        $" v1: {og.V1}, v2: {og.V2}, v3: {og.V3}, v4: {og.V4}, newMinV: {newMinV}, newMaxV: {newMaxV}");
                }
                // Blend Mode
                else if (e.ColumnIndex == ColBlendMode)
                    og.BlendMode = Convert.ToByte(item.Cells[ColBlendMode].Value);
                // Color Mode
                else if (e.ColumnIndex == ColColorMode)
                {
                    int colormode = Convert.ToByte(item.Cells[ColColorMode].Value);
                    og.ColorMode = colormode;

                    int pw = 256 << (2 - colormode);
                    int left = Convert.ToInt32(item.Cells[ColLeft].Value);
                    int width = Convert.ToInt32(item.Cells[ColWidth].Value);
                    if (pw < left + width)
                    {
                        item.Cells[ColLeft].Value = pw - width;
                    }
                }
            }

            DebugOutput($"FrameGroup Index: {rowIndex}, Frame Index: {e.RowIndex}");
        }

        private void dpdTPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = dpdTPages.Text;
            int rowIndex = dgvFrameGroup.SelectedCells[0].RowIndex;

            object selectedGroup = frameGroups[rowIndex];
            if (!(selectedGroup is SpriteGroup) && !(selectedGroup is SpriteGroup2)) return;
            if (!(dgvFrameGroup.SelectedCells.Count > 0) || text == string.Empty) return;

            dgvFrameGroup.Rows[rowIndex].Cells[ColEID].Value = text;
            pictureBox1.Visible = true;
            UpdatePicture();
        }

        private void dgvFrameGroup_SelectionChanged(object sender, EventArgs e)
        {
            if (!(dgvFrameGroup.SelectedCells.Count > 0) || dirty) return;

            DataGridViewCell currentCell = dgvFrameGroup.CurrentCell;

            if (previousCell != null && previousCell.RowIndex == currentCell.RowIndex)
            {
                return;
            }

            int rowIndex = dgvFrameGroup.SelectedCells[0].RowIndex;
            var _row = dgvFrameGroup.Rows[rowIndex];

            object selectedGroup = frameGroups[rowIndex];
            if (selectedGroup is SpriteGroup)
            {
                foreach (var group in goolentry.FrameGroups)
                {
                    if (group is SpriteGroup)
                    {
                        var vgroup = (SpriteGroup)group;

                        int index = vgroup.Index / 4;
                        string _index = index.ToString("X");
                        //DebugOutput($"Current index: 0x{_index}");
                        if (vgroup.Index == Convert.ToInt32(_row.Tag))
                        {
                            dgvTexture.ScrollBars = ScrollBars.None;
                            dgvTexture.SuspendLayout();
                            dirty = true;
                            dgvTexture.Rows.Clear();
                            foreach (var frame in vgroup.Frames)
                            {
                                DataGridViewRow row = new DataGridViewRow();

                                row.CreateCells(dgvTexture, frame.R, frame.G, frame.B, frame.ClutX, frame.ClutY, frame.X, frame.Y, frame.UV, frame.Segment,
                                    frame.BlendMode, frame.ColorMode);
                                dgvTexture.Rows.Add(row);
                            }

                            dgvTexture.ScrollBars = ScrollBars.Vertical;
                            dgvTexture.ResumeLayout();
                            dirty = false;

                            dgvTexture.Visible =
                            pnTextureControls.Visible =
                            pnPicture.Visible = true;
                            tglSimpleMode.Visible =
                            lblSimpleMode.Visible =
                            chkMaxValueFlag.Visible = false;
                            UpdatePicture();
                            return;
                        }
                    }
                }
            }
            else if (selectedGroup is SpriteGroup2)
            {
                foreach (var group in goolentry.FrameGroups)
                {
                    if (group is SpriteGroup2)
                    {
                        var vgroup = (SpriteGroup2)group;

                        int index = vgroup.Index / 4;
                        string _index = index.ToString("X");
                        //DebugOutput($"Current index: 0x{_index}");
                        if (vgroup.Index == Convert.ToInt32(_row.Tag))
                        {
                            dgvTexture.ScrollBars = ScrollBars.None;
                            dgvTexture.SuspendLayout();
                            dirty = true;
                            dgvTexture.Rows.Clear();
                            foreach (var frame in vgroup.Frames)
                            {
                                DataGridViewRow row = new DataGridViewRow();

                                row.CreateCells(dgvTexture, frame.R, frame.G, frame.B, frame.ClutX, frame.ClutY, frame.Left, frame.Top, frame.Width, frame.Height,
                                    frame.X1, frame.X2, frame.X3, frame.X4, frame.Y1, frame.Y2, frame.Y3, frame.Y4, frame.BlendMode, frame.ColorMode);
                                dgvTexture.Rows.Add(row);
                            }

                            SetMaxValueStyle(ColX1, ColX4);
                            SetMaxValueStyle(ColY1, ColY4);

                            dgvTexture.ScrollBars = ScrollBars.Vertical;
                            dgvTexture.ResumeLayout();
                            dirty = false;

                            dgvTexture.Visible =
                            pnTextureControls.Visible =
                            pnPicture.Visible = true;
                            UpdatePicture();
                            return;
                        }
                    }
                }
            }
            else
            {
                dgvTexture.Visible =
                pnTextureControls.Visible =
                pnPicture.Visible =
                lblEIDError.Visible = false;
                dpdTPages.SelectedItem = null;

                if (selectedGroup is not VertexGroup && selectedGroup is not VertexGroup2 && selectedGroup is not VertexGroup3to2)
                {
                    string name = selectedGroup.GetType().Name;
                    Console.WriteLine($"{name} is not supported.");
                }
            }

            previousCell = currentCell;
        }

        private void dgvTexture_SelectionChanged(object sender, EventArgs e)
        {
            if (!(dgvTexture.SelectedCells.Count > 0)) return;

            var cell = dgvTexture.SelectedCells[0];
            chkMaxValueFlag.Enabled = (cell.ColumnIndex >= ColX1 && cell.ColumnIndex <= ColY4) ? true : false;
            chkMaxValueFlag.Checked = cell.Style == maxValueStyle ? true : false;
            UpdatePicture();
        }

        private void UpdatePicture()
        {
            if (!(dgvTexture.SelectedCells.Count > 0) || !(dgvFrameGroup.SelectedCells.Count > 0) || dirty) return;

            int rowIndex = dgvTexture.SelectedCells[0].RowIndex;
            var row = dgvTexture.Rows[rowIndex];
            int _r = Convert.ToInt32(row.Cells[ColR].Value);
            int _g = Convert.ToInt32(row.Cells[ColG].Value);
            int _b = Convert.ToInt32(row.Cells[ColB].Value);
            Color texelColor = Color.FromArgb(_r, _g, _b);

            int _rowIndex = dgvFrameGroup.SelectedCells[0].RowIndex;
            var _row = dgvFrameGroup.Rows[_rowIndex];
            string eid = _row.Cells[ColEID].Value.ToString();

            if (dpdTPages.Items.Contains(eid))
            {
                chunk = controller.GetEntry<TextureChunk>(Entry.ENameToEID(eid));
                lblEIDError.Visible = false;
                dpdTPages.SelectedItem = eid;
            }
            else
            {
                pictureBox1.Visible = false;
                lblEIDError.Visible = true;
                dpdTPages.SelectedItem = null;
                return;
            }

            int TexCX = Convert.ToInt32(row.Cells[ColClutX].Value);
            int TexCY = Convert.ToInt32(row.Cells[ColClutY].Value);
            int colormode = Convert.ToInt32(row.Cells[ColColorMode].Value);
            int blendmode = Convert.ToInt32(row.Cells[ColBlendMode].Value);

            int TexX, TexY, TexW, TexH;
            if (goolentry.Version == GOOLVersion.Version1)
            {
                int XOffU = Convert.ToInt32(row.Cells[ColX].Value);
                int YOffU = Convert.ToInt32(row.Cells[ColY].Value);
                int segment = Convert.ToInt32(row.Cells[ColSegment].Value);
                int uvIndex = Convert.ToInt32(row.Cells[ColUV].Value);

                TexX = ((64 << (2 - colormode)) * segment) + ((2 << (2 - colormode)) * XOffU);
                TexY = YOffU * 4;
                TexW = 4 << (uvIndex % 5);
                TexH = 4 << ((uvIndex / 5) % 5);
            }
            else
            {
                TexX = Convert.ToInt32(row.Cells[ColLeft].Value);
                TexY = Convert.ToInt32(row.Cells[ColTop].Value);
                TexW = Convert.ToInt32(row.Cells[ColWidth].Value);
                TexH = Convert.ToInt32(row.Cells[ColHeight].Value);
            }

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

            float zoom = trkPictureSize.Value / 100f;
            pictureBox1.Width = (int)(pictureBox1.Image.Width * zoom);
            pictureBox1.Height = (int)(pictureBox1.Image.Height * zoom);

            currentColorMode = colormode;
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

        private void dgvFrameGroup_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvFrameGroup.SelectedCells.Count > 0)
            {
                if (e.Control is TextBox textbox)
                {
                    if (dgvFrameGroup.SelectedCells[0].ColumnIndex == ColFrameCount)
                    {
                        textbox.KeyPress -= TextBox_KeyPress;
                        textbox.KeyPress += TextBox_KeyPress;
                    }
                    else
                    {
                        textbox.KeyPress -= TextBox_KeyPress;
                    }

                    if (dgvFrameGroup.SelectedCells[0].ColumnIndex == ColEID)
                    {
                        textbox.MaxLength = 5;
                    }
                }
            }
        }

        private void dgvTexture_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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

        private void tglSimpleMode_SwitchedChanged(object sender)
        {
            simpleMode = tglSimpleMode.Switched;
            ToggleSimpleMode();
        }

        private void DebugOutput(string line)
        {
            if (Settings.Default.OutputModelTextureInfo)
                Console.WriteLine(line);
        }
    }
}