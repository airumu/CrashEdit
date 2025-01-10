using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public partial class GOOLFrameGroupBox : UserControl
    {
        GOOLEntryController controller;
        GOOLEntry goolentry;
        List<VertexGroup2> vertexGroup2;
        List<VertexGroup3to2> vertexGroup3to2;
        List<SpriteGroup2> spriteGroup2;
        DataGridViewCellStyle maxValueStyle = new DataGridViewCellStyle
        {
            ForeColor = Color.Turquoise
        };
        DataGridViewCellStyle defaultValueStyle = new DataGridViewCellStyle
        {
            ForeColor = Color.Gainsboro
        };

        private TextureChunk chunk { get; set; }

        private int currentColorMode = 0;
        private bool simpleMode = false;
        private bool dirty = false;
        private bool tpagedirty = false;

        private readonly int ColIndex = 0;
        private readonly int ColIndexAlt = 1;
        private readonly int ColFrameCount = 2;
        private readonly int ColEID = 3;
        private readonly int ColInterpolated = 4;

        private readonly int ColR = 0;
        private readonly int ColG = 1;
        private readonly int ColB = 2;
        private readonly int ColClutX = 3;
        private readonly int ColClutY = 4;
        private readonly int ColLeft = 5;
        private readonly int ColTop = 6;
        private readonly int ColWidth = 7;
        private readonly int ColHeight = 8;
        private readonly int ColX1 = 9;
        private readonly int ColX2 = 10;
        private readonly int ColX3 = 11;
        private readonly int ColX4 = 12;
        private readonly int ColY1 = 13;
        private readonly int ColY2 = 14;
        private readonly int ColY3 = 15;
        private readonly int ColY4 = 16;
        private readonly int ColBlendMode = 17;
        private readonly int ColColorMode = 18;

        private readonly int typeVertex2 = 0;
        private readonly int typeVertex3to2 = 1;
        private readonly int typeSprite2 = 2;

        private readonly string titleInputError = "Input Error";
        private readonly string titleValidationError = "Validation Error";

        public GOOLFrameGroupBox(GOOLEntryController controller)
        {
            this.controller = controller;
            goolentry = controller.GOOLEntry;
            InitializeComponent();
        }

        public void OnTabSelected()
        {
            GOOLFrameGroupBox_Enter(this, EventArgs.Empty);
        }

        private void GOOLFrameGroupBox_Enter(object sender, EventArgs e)
        {
            EnableDoubleBuffering();
            SetDarkTheme(dgvFrameGroup);
            SetDarkTheme(dgvTexture);

            dgvFrameGroupCreateColumns();
            dgvTextureCreateColumns();

            dgvFrameGroupCreateRows();

            AdjustColumnwidth(dgvFrameGroup);
            AdjustColumnwidth(dgvTexture);

            GetTpage();
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
            dgvFrameGroup.Columns.Add("FrameCount", "Frames");
            dgvFrameGroup.Columns.Add("EID", "EID\u3000\u3000");
            dgvFrameGroup.Columns.Add("Interpolated", "Interpolated");
        }

        private void dgvTextureCreateColumns()
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

            for (int i = ColX1; i <= ColY4; i++)
                dgvTexture.Columns[i].Visible = false;
        }

        private void AdjustColumnwidth(DataGridView dataGridView)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                //column.Width = 80;
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
            chkMaxValueFlag.Visible = simpleMode;

            dgvTexture.ResumeLayout();
        }

        private void chkMaxFlag_CheckedChanged(object sender, EventArgs e)
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

        private void SetMaxValueTag(int start, int end)
        {
            int startColumnIndex = start;
            int endColumnIndex = end;

            Parallel.For(0, dgvTexture.Rows.Count, rowIndex =>
            {
                var row = dgvTexture.Rows[rowIndex];
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
                            row.Cells[col].Style = maxValueStyle;
                        }
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
            // Todo something better
            vertexGroup2 = new List<VertexGroup2>();
            vertexGroup3to2 = new List<VertexGroup3to2>();
            spriteGroup2 = new List<SpriteGroup2>();

            dgvFrameGroup.SuspendLayout();
            dgvFrameGroup.ScrollBars = ScrollBars.None;
            dirty = true;
            foreach (var group in goolentry.FrameGroups)
            {
                if (group is VertexGroup2)
                {
                    var vgroup = (VertexGroup2)group;
                    vertexGroup2.Add(vgroup);
                    vertexGroup3to2.Add(null);
                    spriteGroup2.Add(null);
                    DataGridViewRow row = new DataGridViewRow();

                    GetIndex(vgroup.Index / 4, out string index1, out string index2);

                    row.CreateCells(dgvFrameGroup, index1, index2, vgroup.FrameCount, Entry.EIDToEName(vgroup.EID), vgroup.Interpolated);
                    row.Tag = (typeVertex2, 0);
                    dgvFrameGroup.Rows.Add(row);
                }
                else if (group is VertexGroup3to2)
                {
                    var vgroup = (VertexGroup3to2)group;
                    vertexGroup2.Add(null);
                    vertexGroup3to2.Add(vgroup);
                    spriteGroup2.Add(null);
                    DataGridViewRow row = new DataGridViewRow();

                    GetIndex(vgroup.Index / 4, out string index1, out string index2);

                    row.CreateCells(dgvFrameGroup, index1, index2, vgroup.FrameCount, Entry.EIDToEName(vgroup.EID), vgroup.Interpolated);
                    row.Tag = (typeVertex3to2, 0);
                    dgvFrameGroup.Rows.Add(row);
                }
                else if (group is SpriteGroup2)
                {
                    var vgroup = (SpriteGroup2)group;
                    vertexGroup2.Add(null);
                    vertexGroup3to2.Add(null);
                    spriteGroup2.Add(vgroup);
                    DataGridViewRow row = new DataGridViewRow();

                    GetIndex(vgroup.Index / 4, out string index1, out string index2);

                    row.CreateCells(dgvFrameGroup, index1, index2, vgroup.FrameCount, Entry.EIDToEName(vgroup.EID), "-");
                    row.Tag = (typeSprite2, vgroup.Index);
                    dgvFrameGroup.Rows.Add(row);
                }
            }
            dgvFrameGroup.ResumeLayout();
            dgvFrameGroup.ScrollBars = ScrollBars.Both;
            dirty = false;
        }

        private void dgvFrameGroup_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

            if (e.ColumnIndex >= ColIndex && e.ColumnIndex <= ColIndexAlt)
            {
                DarkMessageBox.ShowError("This cell cannot be edited.", titleInputError);
                e.Cancel = true;
            }

            if (dgvFrameGroup.SelectedCells.Count > 0)
            {
                var row = dgvFrameGroup.Rows[e.RowIndex];
                if (row.Tag is ValueTuple<int, int> tag)
                {
                    int type = tag.Item1;
                    if ((e.ColumnIndex == ColFrameCount && type == typeSprite2) || // SpriteGroup2 FrameCount
                    (dgvFrameGroup.SelectedCells[0].Value.ToString() == "-")) // SpriteGroup2 Interpolated
                    {
                        DarkMessageBox.ShowError("This cell cannot be edited.", titleInputError);
                        e.Cancel = true;
                    }
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
                            DarkMessageBox.ShowError($"The value must be less than or equal to {maxValue}.", titleInputError);
                            e.Cancel = true;
                        }
                        else if (newValue < minValue)
                        {
                            DarkMessageBox.ShowError($"The value must be greater than or equal to {minValue}.", titleInputError);
                            e.Cancel = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        DarkMessageBox.ShowError($"Invalid value: {inputValue}\nError: {ex.Message}", titleValidationError);
                        e.Cancel = true;
                    }
                }
                else
                {
                    DarkMessageBox.ShowError($"Invalid input. Please enter an integer.", titleInputError);
                    e.Cancel = true;
                }
            }
            else if (e.ColumnIndex == ColEID)
            {
                string input = Entry.CheckEIDErrors(inputValue, true);
                if (input != string.Empty)
                {
                    DarkMessageBox.ShowError($"Invalid EID string: {inputValue}", titleInputError);
                    e.Cancel = true;
                }
            }
            else if (e.ColumnIndex == ColInterpolated)
            {
                if (dgvFrameGroup.SelectedCells[0].Value.ToString() == "-") return;

                if (!(inputValue == "True" || inputValue == "False" || inputValue == "true" || inputValue == "false"))
                {
                    DarkMessageBox.ShowError($"Invalid string: {inputValue}", titleInputError);
                    e.Cancel = true;
                }
            }

        }

        private void dgvFrameGroup_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !(dgvFrameGroup.SelectedCells.Count > 0)) return;

            var row = dgvFrameGroup.Rows[e.RowIndex];
            if (row.Tag is ValueTuple<int, int> tag)
            {
                int type = tag.Item1;

                object selectedGroup =
                    type == typeVertex2 ? vertexGroup2[e.RowIndex] :
                    type == typeVertex3to2 ? vertexGroup3to2[e.RowIndex] :
                    spriteGroup2[e.RowIndex];
                if (selectedGroup == null) return;

                if (!(selectedGroup is VertexGroup2 vertexGroup) && !(selectedGroup is VertexGroup3to2 vertexGroup3) && !(selectedGroup is SpriteGroup2 spriteGroup)) return;
                dynamic og = selectedGroup;

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

                DebugOutput($"FrameGroup Index: {e.RowIndex}");
            }
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
            // X (Left)
            else if (columnIndex == ColLeft)
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
                minValue = 4;
                DebugOutput($"Segment {segment}, maxValue {maxValue}");
            }
            // Height
            else if (columnIndex == ColHeight)
            {
                maxValue = 128 - Convert.ToInt32(dgvTexture.Rows[rowIndex].Cells[ColTop].Value);
                minValue = 4;
            }
            // Blend Mode
            else if (columnIndex == ColBlendMode)
                maxValue = 3;
            // Color Mode
            else if (columnIndex == ColColorMode)
                maxValue = 2;
        }

        private void dgvTexture_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex >= ColX1 && e.ColumnIndex <= ColY4)
            {
                DarkMessageBox.ShowError("This cell cannot be edited.", titleInputError);
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
                        if (e.ColumnIndex >= ColLeft && e.ColumnIndex <= ColHeight)
                            DarkMessageBox.ShowError($"The UV does not fit within the segment. The value must be less than or equal to {maxValue}.", titleInputError);
                        else
                            DarkMessageBox.ShowError($"The value must be less than or equal to {maxValue}.", titleInputError);
                        e.Cancel = true;
                    }
                    else if (newValue < minValue)
                    {
                        DarkMessageBox.ShowError($"The value must be greater than or equal to {minValue}.", titleInputError);
                        e.Cancel = true;
                    }
                }
                catch (Exception ex)
                {
                    DarkMessageBox.ShowError($"Invalid value: {inputValue}\nError: {ex.Message}", titleValidationError);
                    e.Cancel = true;
                }
            }
            else
            {
                DarkMessageBox.ShowError($"Invalid input. Please enter an integer.", titleInputError);
                e.Cancel = true;
            }
        }

        private void dgvTexture_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !(dgvFrameGroup.SelectedCells.Count > 0) || !(dgvTexture.SelectedCells.Count > 0)) return;

            int rowIndex = dgvFrameGroup.SelectedCells[0].RowIndex;

            var selectedGroup = spriteGroup2[rowIndex];
            if (selectedGroup == null || !(selectedGroup is SpriteGroup2 spriteGroup)) return;

            var og = spriteGroup.Frames[e.RowIndex];
            var item = dgvTexture.Rows[e.RowIndex];
            int colorMode = Convert.ToInt32(item.Cells[ColColorMode].Value);

            DebugOutput($"FrameGroup Index: {rowIndex}, Frame Index: {e.RowIndex}");

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

        private void dpdTPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = dpdTPages.Text;
            int rowIndex = dgvFrameGroup.SelectedCells[0].RowIndex;
            var row = dgvFrameGroup.Rows[rowIndex];
            if (row.Tag is ValueTuple<int, int> tag)
            {
                int type = tag.Item1;
                if (!(dgvFrameGroup.SelectedCells.Count > 0) || text == string.Empty || type != typeSprite2) return;

                dgvFrameGroup.Rows[rowIndex].Cells[ColEID].Value = text;
                pictureBox1.Visible = true;
                UpdatePicture();
            }
        }

        private void dgvFrameGroup_SelectionChanged(object sender, EventArgs e)
        {
            if (!(dgvFrameGroup.SelectedCells.Count > 0) || dirty) return;
            int rowIndex = dgvFrameGroup.SelectedCells[0].RowIndex;
            var _row = dgvFrameGroup.Rows[rowIndex];

            foreach (var group in goolentry.FrameGroups)
            {
                if (group is SpriteGroup2)
                {
                    var vgroup = (SpriteGroup2)group;

                    int index = vgroup.Index / 4;
                    string _index = index.ToString("X");
                    DebugOutput($"Current index: 0x{_index}");
                    if (_row.Tag is ValueTuple<int, int> tag)
                    {
                        int type = tag.Item1;
                        int index2 = tag.Item2;
                        if (vgroup.Index == index2 && type == typeSprite2)
                        {
                            dgvTexture.SuspendLayout();
                            dgvTexture.ScrollBars = ScrollBars.None;
                            dirty = true;
                            dgvTexture.Rows.Clear();
                            foreach (var frame in vgroup.Frames)
                            {
                                DataGridViewRow row = new DataGridViewRow();

                                row.CreateCells(dgvTexture, frame.R, frame.G, frame.B, frame.ClutX, frame.ClutY, frame.Left, frame.Top, frame.Width, frame.Height,
                                    frame.X1, frame.X2, frame.X3, frame.X4, frame.Y1, frame.Y2, frame.Y3, frame.Y4, frame.BlendMode, frame.ColorMode);
                                dgvTexture.Rows.Add(row);
                            }

                            SetMaxValueTag(ColX1, ColX4);
                            SetMaxValueTag(ColY1, ColY4);

                            dgvTexture.ResumeLayout();
                            dgvTexture.ScrollBars = ScrollBars.Both;
                            dirty = false;

                            dgvTexture.Visible =
                            pnTextureControls.Visible =
                            pictureBox1.Visible = true;
                            UpdatePicture();
                            return;
                        }
                    }
                }
            }
            dgvTexture.Visible =
            pnTextureControls.Visible =
            pictureBox1.Visible =
            lblEIDError.Visible = false;
            dpdTPages.SelectedItem = null;
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
            int TexX = Convert.ToInt32(row.Cells[ColLeft].Value);
            int TexY = Convert.ToInt32(row.Cells[ColTop].Value);
            int TexW = Convert.ToInt32(row.Cells[ColWidth].Value);
            int TexH = Convert.ToInt32(row.Cells[ColHeight].Value);

            int colormode = Convert.ToInt32(row.Cells[ColColorMode].Value);
            int blendmode = Convert.ToInt32(row.Cells[ColBlendMode].Value);

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


        private void EnableDoubleBuffering()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, dgvFrameGroup, new object[] { true });
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