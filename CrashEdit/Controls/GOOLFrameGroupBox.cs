using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using Chunk = CrashEdit.Crash.Chunk;

namespace CrashEdit.CE
{
    public partial class GOOLFrameGroupBox : UserControl
    {
        GOOLEntryController controller;

        GOOLEntry goolentry;

        private TextureChunk chunk { get; set; }
        private Rectangle selectedregion;
        private bool dirty;
        private bool simpleMode;
        private bool BGRAMode;
        private bool replaceCLUT;
        private int selectedRegionX;
        private int selectedRegionY;
        private int currentColorMode;
        private int oldRowIndex;

        private int ColPage = 0;
        private int ColR = 1;
        private int ColG = 2;
        private int ColB = 3;
        private int ColClutX = 4;
        private int ColClutY = 5;
        private int ColLeft = 6;
        private int ColTop = 7;
        private int ColWidth = 8;
        private int ColHeight = 9;
        private int ColBlendMode = 10;
        private int ColColorMode = 11;

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

            //dgvFrameGroup.CellEndEdit += dgvFrameGroup_CellEndEdit;
            //dgvFrameGroup.CellValidating += dgvFrameGroup_CellValidating;
            //dgvFrameGroup.CellValueChanged += dgvFrameGroup_CellValueChanged;
            //dgvFrameGroup.EditingControlShowing += dgvFrameGroup_EditingControlShowing;
            dgvFrameGroup.SelectionChanged += dgvFrameGroup_SelectionChanged;
            dgvTexture.SelectionChanged += dgvTexture_SelectionChanged;

            dgvFrameGroupCreateColumns();
            dgvTextureCreateColumns();

            dgvFrameGroupCreateRows();
            dgvTextureCreateRows();

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
            dgvFrameGroup.Columns.Add("EID", "EID");
            dgvFrameGroup.Columns.Add("Interpolated", "Interpolated");
        }

        private void dgvTextureCreateColumns()
        {
            dgvTexture.Columns.Add("Page", "Page");
            dgvTexture.Columns.Add("R", "R");
            dgvTexture.Columns.Add("G", "G");
            dgvTexture.Columns.Add("B", "B");
            dgvTexture.Columns.Add("ClutX", "Clut X");
            dgvTexture.Columns.Add("ClutY", "Clut Y");
            dgvTexture.Columns.Add("Left", "X  ");
            dgvTexture.Columns.Add("Top", "Y  ");
            dgvTexture.Columns.Add("Width", "Width");
            dgvTexture.Columns.Add("Height", "Height");
            dgvTexture.Columns.Add("BlendMode", "Blend");
            dgvTexture.Columns.Add("ColorMode", "Color");

            dgvTexture.Columns[ColPage].Visible = false;
        }

        private void AdjustColumnwidth(DataGridView dataGridView)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //column.Width = 80;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void GetIndex(int index,  out string index1, out string index2)
        {
            index1 = string.Format("{0:X}", NumberExt.TransformedString(index));
            index2 = string.Format("0x{0:X}00", index.ToString("X"));
        }

        private void dgvFrameGroupCreateRows()
        {
            dgvFrameGroup.SuspendLayout();
            dgvFrameGroup.ScrollBars = ScrollBars.None;
            dirty = true;
            foreach (var group in goolentry.FrameGroups)
            {
                if (group is VertexGroup2)
                {
                    var vgroup = (VertexGroup2)group;
                    DataGridViewRow row = new DataGridViewRow();

                    GetIndex(vgroup.Index / 4, out string index1, out string index2);

                    row.CreateCells(dgvFrameGroup, index1, index2, vgroup.FrameCount, Entry.EIDToEName(vgroup.EID), vgroup.Interpolated);
                    dgvFrameGroup.Rows.Add(row);
                }
                else if (group is VertexGroup3to2)
                {
                    var vgroup = (VertexGroup3to2)group;
                    DataGridViewRow row = new DataGridViewRow();

                    GetIndex(vgroup.Index / 4, out string index1, out string index2);

                    row.CreateCells(dgvFrameGroup, index1, index2, vgroup.FrameCount, Entry.EIDToEName(vgroup.EID), vgroup.Interpolated);
                    dgvFrameGroup.Rows.Add(row);
                }
                else if (group is SpriteGroup2)
                {
                    var vgroup = (SpriteGroup2)group;
                    DataGridViewRow row = new DataGridViewRow();

                    GetIndex(vgroup.Index / 4, out string index1, out string index2);

                    row.CreateCells(dgvFrameGroup, index1, index2, vgroup.FrameCount, Entry.EIDToEName(vgroup.EID), "-");
                    row.Tag = vgroup.Index;
                    dgvFrameGroup.Rows.Add(row);
                }
            }
            dgvFrameGroup.ResumeLayout();
            dgvFrameGroup.ScrollBars = ScrollBars.Both;
            dirty = false;
        }

        private void dgvTextureCreateRows()
        {
               
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
                    if (Settings.Default.OutputModelTextureInfo)
                    {
                        int index = vgroup.Index / 4;
                        Console.WriteLine(index.ToString("X"));
                    }
                    if (vgroup.Index == Convert.ToInt32(_row.Tag) && _row.Tag != null)
                    {
                        dgvTexture.SuspendLayout();
                        dgvTexture.ScrollBars = ScrollBars.None;
                        dirty = true;
                        dgvTexture.Rows.Clear();
                        foreach (var frame in vgroup.Frames)
                        {
                            DataGridViewRow row = new DataGridViewRow();

                            SpriteTexture2 tex = SpriteTexture2.Load(frame.PackedValue1, frame.PackedValue2, frame.PackedValue3, frame.PackedValue4);
                            row.CreateCells(dgvTexture, Entry.EIDToEName(vgroup.EID), tex.R, tex.G, tex.B, tex.ClutX, tex.ClutY, tex.Left, tex.Top, tex.Width, tex.Height, tex.BlendMode, tex.ColorMode);
                            dgvTexture.Rows.Add(row);
                        }
                        dgvTexture.ResumeLayout();
                        dgvTexture.ScrollBars = ScrollBars.Both;
                        dirty = false;

                        dgvTexture.Visible =
                        pictureBox1.Visible = true;
                        UpdatePicture();
                        return;
                    }
                }
            }
            dgvTexture.Visible =
            pictureBox1.Visible =
            lblTPageError.Visible = false;
            dpdTPages.SelectedItem = null;
        }

        private void dgvTexture_SelectionChanged(object sender, EventArgs e)
        {
            UpdatePicture();
        }

        private void UpdatePicture()
        {
            if (!(dgvTexture.SelectedCells.Count > 0) || dirty) return;

            int rowIndex = dgvTexture.SelectedCells[0].RowIndex;
            var row = dgvTexture.Rows[rowIndex];
            int _r = Convert.ToInt32(row.Cells[ColR].Value);
            int _g = Convert.ToInt32(row.Cells[ColG].Value);
            int _b = Convert.ToInt32(row.Cells[ColB].Value);
            Color texelColor = Color.FromArgb(_r, _g, _b);

            string eid = row.Cells[ColPage].Value.ToString();
            if (dpdTPages.Items.Contains(eid))
            {
                chunk = controller.GetEntry<TextureChunk>(Entry.ENameToEID(eid));
                lblTPageError.Visible = false;
                dpdTPages.SelectedItem = eid;
            }
            else
            {
                pictureBox1.Visible = false;
                lblTPageError.Visible = true;
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
                selectedregion.X = x;
                selectedregion.Y = y;
                selectedregion.Width = w;
                selectedregion.Height = h;
                selectedRegionX = x;
                selectedRegionY = y;
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
                    byte va = vertexBytes[i + 3]; // Al

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