using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using System.Drawing;
using System.Drawing.Imaging;

namespace CrashEdit.CE
{
    public partial class TextureViewer : DarkForm
    {
        internal enum TextureType
        {
            Crash1,
            Crash2
        }

        private TextureChunk chunk;
        private TextureType textype;
        private Rectangle selectedregion;

        private bool isDragging = false;
        private Point dragStartPoint;
        private Point initialSelectedRegionPosition;
        private int selectionSize;

        public TextureViewer(TextureChunk texturechunk)
        {
            chunk = texturechunk;
            textype = TextureType.Crash2;

            Icon = Embeds.GetIcon("Painting");
            Text = string.Format("Texture Viewer [{0}] - Right-click to save texture region to file", texturechunk.EName);

            InitializeComponent();

            tabC1.Enter += delegate (object? sender, EventArgs e)
            {
                textype = TextureType.Crash1;
                UpdatePicture();
            };
            tabC2.Enter += delegate (object? sender, EventArgs e)
            {
                textype = TextureType.Crash2;
                UpdatePicture();
            };
            tabControl1.SelectedTab = tabC2;
            C1dpdW.SelectedIndex = 0;
            C1dpdH.SelectedIndex = 0;
            C1dpdColor.SelectedIndex = 0;
            C1dpdBlend.SelectedIndex = 3;
            C2dpdColor.SelectedIndex = 0;
            C2dpdBlend.SelectedIndex = 3;
            selectionSize = 32;

            pictureBox1.MouseClick += delegate (object? sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right && pictureBox1.Image != null && pictureBox1.Image is Bitmap bmp)
                {
                    using (MemoryStream w = new MemoryStream())
                    {
                        bmp.Clone(selectedregion, PixelFormat.Format32bppArgb).Save(w, ImageFormat.Png);
                        FileUtil.SaveFile($"{chunk.EName}_{TexCY}_{TexCX}", w.ToArray(), FileFilters.PNG);
                    }
                }
            };

            pictureBox1.MouseDown += (sender, e) =>
            {
                if (textype == TextureType.Crash1) return;
                if (e.Button == MouseButtons.Left)
                {
                    selectedregion.Width = selectionSize;
                    selectedregion.Height = selectionSize;
                    C2numW.Value = selectionSize;
                    C2numH.Value = selectionSize;

                    dragStartPoint = e.Location;
                    //initialSelectedRegionPosition = selectedregion.Location;
                    selectedregion = new Rectangle(dragStartPoint.X, dragStartPoint.Y, TexW, TexH);
                    isDragging = true;

                    int clickX = e.X;
                    int clickY = e.Y;
                    int offsetX = clickX / TexW * TexW;
                    int offsetY = clickY / TexH * TexH;
                    selectedregion.X = offsetX;
                    selectedregion.Y = offsetY;
                    C2numX.Value = offsetX;
                    C2numY.Value = offsetY;
                    UpdatePicture();
                }
            };

            pictureBox1.MouseMove += (sender, e) =>
            {
                if (textype == TextureType.Crash1) return;
                if (isDragging)
                {
                    int deltaX = e.X - dragStartPoint.X + selectionSize;
                    int deltaY = e.Y - dragStartPoint.Y + selectionSize;
                    selectedregion.Width = deltaX;
                    selectedregion.Height = deltaY;
                    selectedregion.Width = (selectedregion.Width / selectionSize) * selectionSize;
                    selectedregion.Height = (selectedregion.Height / selectionSize) * selectionSize;

                    if (selectedregion.Width < selectionSize)
                        selectedregion.Width = selectionSize;
                    else if (selectedregion.Width > 1024)
                        selectedregion.Width = 1024;

                    if (selectedregion.Height < selectionSize)
                        selectedregion.Height = selectionSize;
                    else if (selectedregion.Height > 128)
                        selectedregion.Height = 128;

                    C2numW.Value = selectedregion.Width;
                    C2numH.Value = selectedregion.Height;
                    UpdatePicture();
                }
                //if (isDragging)
                //{
                //    int deltaX = e.X - dragStartPoint.X;
                //    int deltaY = e.Y - dragStartPoint.Y;
                //    int newX = initialSelectedRegionPosition.X + deltaX;
                //    int newY = initialSelectedRegionPosition.Y + deltaY;
                //    newX = (newX / TexW) * TexW;
                //    newY = (newY / TexH) * TexH;
                //    C2numX.Value = newX;
                //    C2numY.Value = newY;
                //    selectedregion.X = newX;
                //    selectedregion.Y = newY;
                //    UpdatePicture();
                //}
            };

            pictureBox1.MouseUp += (sender, e) =>
            {
                if (textype == TextureType.Crash1) return;
                if (e.Button == MouseButtons.Left)
                {
                    isDragging = false;
                }
            };

            foreach (ComboBox dpd in this.GetAll(typeof(ComboBox)))
            {
                dpd.SelectedIndex = 0;
            }

            C1dpdColor.SelectedIndexChanged += new EventHandler(Control_UpdatePicture);
            C1dpdBlend.SelectedIndexChanged += new EventHandler(Control_UpdatePicture);
            C1dpdW.SelectedIndexChanged += new EventHandler(Control_UpdatePicture);
            C1dpdH.SelectedIndexChanged += new EventHandler(Control_UpdatePicture);
            C2dpdColor.SelectedIndexChanged += new EventHandler(Control_UpdatePicture);
            C2dpdBlend.SelectedIndexChanged += new EventHandler(Control_UpdatePicture);
            C1numX.ValueChanged += new EventHandler(Control_UpdatePicture);
            C1numY.ValueChanged += new EventHandler(Control_UpdatePicture);
            C1numCX.ValueChanged += new EventHandler(Control_UpdatePicture);
            C1numCY.ValueChanged += new EventHandler(Control_UpdatePicture);
            C2numX.ValueChanged += new EventHandler(Control_UpdatePicture_1);
            C2numY.ValueChanged += new EventHandler(Control_UpdatePicture_1);
            C2numX2.ValueChanged += new EventHandler(Control_UpdatePicture_2);
            C2numY2.ValueChanged += new EventHandler(Control_UpdatePicture_2);
            C2numCX.ValueChanged += new EventHandler(Control_UpdatePicture_3);
            C2numCY.ValueChanged += new EventHandler(Control_UpdatePicture_3);
            C2numW.ValueChanged += new EventHandler(Control_UpdatePicture);
            C2numH.ValueChanged += new EventHandler(Control_UpdatePicture);

            UpdatePicture();

            groupBox1.Text = Properties.Resources.TextureViewer_groupBox1;
            groupBox2.Text = Properties.Resources.TextureViewer_groupBox2;
            groupBox4.Text = Properties.Resources.TextureViewer_groupBox4;
            groupBox5.Text = Properties.Resources.TextureViewer_groupBox5;
            groupBox6.Text = Properties.Resources.TextureViewer_groupBox5;
            groupBox7.Text = Properties.Resources.TextureViewer_groupBox4;
            groupBox9.Text = Properties.Resources.TextureViewer_groupBox2;
            groupBox10.Text = Properties.Resources.TextureViewer_groupBox1;

            MakeArgAsText();
        }

        internal int TexColorMode => textype == TextureType.Crash1 ? C1dpdColor.SelectedIndex : C2dpdColor.SelectedIndex;
        internal int TexBlendMode => textype == TextureType.Crash1 ? C1dpdBlend.SelectedIndex : C2dpdBlend.SelectedIndex;
        internal int TexX => textype == TextureType.Crash1 ? (2 << (2 - C1dpdColor.SelectedIndex)) * (int)C1numX.Value : (int)C2numX.Value;
        internal int TexY => textype == TextureType.Crash1 ? (int)C1numY.Value * 4 : (int)C2numY.Value;
        internal int TexW => textype == TextureType.Crash1 ? 4 << C1dpdW.SelectedIndex : (int)C2numW.Value;
        internal int TexH => textype == TextureType.Crash1 ? 4 << C1dpdH.SelectedIndex : (int)C2numH.Value;
        internal int TexCX => textype == TextureType.Crash1 ? (int)C1numCX.Value : (int)C2numCX.Value;
        internal int TexCY => textype == TextureType.Crash1 ? (int)C1numCY.Value : (int)C2numCY.Value;

        private void MakeArgAsText()
        {
            int clut_offset = ((int)C2numCX.Value * 0x20) + ((int)C2numCY.Value * 0x200);
            int clut_val = (int)C2numCX.Value + ((int)C2numCY.Value * 0x40);
            string clut_offset_hex = clut_offset.ToString("X");
            string clut_val_hex = clut_val.ToString("X");
            lblCLUT.Text = string.Format("Hex    0x{0}\r\nOffset 0x{1}", clut_val_hex, clut_offset_hex);
        }

        private void Control_UpdatePicture(object sender, EventArgs e)
        {
            UpdatePicture();
        }

        private void Control_UpdatePicture_1(object sender, EventArgs e)
        {
            C2numY2.Value = (int)C2numY.Value;
            C2numX2.Value = (int)C2numX.Value;
            UpdatePicture();
        }

        private void Control_UpdatePicture_2(object sender, EventArgs e)
        {
            C2numY.Value = (int)C2numY2.Value;
            C2numX.Value = (int)C2numX2.Value;
            UpdatePicture();
        }

        private void Control_UpdatePicture_3(object sender, EventArgs e)
        {
            MakeArgAsText();
            UpdatePicture();
        }

        private void UpdatePicture()
        {
            int pw = 256 << (2 - TexColorMode);
            int ph = 128;
            // Bitmap bitmap = new Bitmap(pw + 64, ph + 64, PixelFormat.Format32bppArgb); // we give the image some buffer space for the selection graphic
            Bitmap bitmap = new Bitmap(pw + 2, ph + 2, PixelFormat.Format32bppArgb);
            Rectangle brect = new Rectangle(Point.Empty, bitmap.Size);
            BitmapData bdata = bitmap.LockBits(brect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int[] palette = null;
            int colormode = TexColorMode;
            int blendmode = TexBlendMode;
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
                    new Rectangle(x + w, y, Math.Max(pw - (x + w), 0), minh),
                    new Rectangle(0, y + h, pw, Math.Max(ph - (y + h), 0))
                    });

                    if (textype == TextureType.Crash1)
                    {
                        g.DrawRectangles(pen, new Rectangle[2]
                        {
                            new Rectangle(x-1,y-1,w+1,h+1),
                            new Rectangle(x-3,y-3,w+5,h+5)
                        });
                        pen.Color = Color.White;
                        g.DrawRectangle(pen, new Rectangle(x - 2, y - 2, w + 3, h + 3));
                    }
                    else
                    {
                        g.DrawRectangles(pen, new Rectangle[2]
                        {
                            new Rectangle(selectedregion.X - 1, selectedregion.Y - 1, selectedregion.Width + 1, selectedregion.Height + 1),
                            new Rectangle(selectedregion.X - 3, selectedregion.Y - 3, selectedregion.Width + 5, selectedregion.Height + 5)
                        });
                        pen.Color = Color.White;
                        g.DrawRectangle(pen, new Rectangle(selectedregion.X - 2, selectedregion.Y - 2, selectedregion.Width + 3, selectedregion.Height + 3));
                    }
                }
                selectedregion.X = x;
                selectedregion.Y = y;
                selectedregion.Width = w;
                selectedregion.Height = h;
            }

            pictureBox1.Image = bitmap;
            pictureBox1.Size = bitmap.Size;
            Width = 1024 + 32;
        }

        private void C2Size16_Click(object sender, EventArgs e)
        {
            C2numW.Value = 16;
            C2numH.Value = 16;
            selectionSize = 16;
            UpdatePicture();
        }

        private void C2Size32_Click(object sender, EventArgs e)
        {
            C2numW.Value = 32;
            C2numH.Value = 32;
            selectionSize = 32;
            UpdatePicture();
        }

        private void C2Size64_Click(object sender, EventArgs e)
        {
            C2numW.Value = 64;
            C2numH.Value = 64;
            selectionSize = 64;
            UpdatePicture();
        }

        private void C2SizeMax_Click(object sender, EventArgs e)
        {
            C2numW.Value = 1024;
            C2numH.Value = 128;
            UpdatePicture();
        }

        private void C1Size16_Click(object sender, EventArgs e)
        {
            C1dpdW.SelectedItem = "16";
            C1dpdH.SelectedItem = "16";
            UpdatePicture();
        }

        private void C1Size32_Click(object sender, EventArgs e)
        {
            C1dpdW.SelectedItem = "32";
            C1dpdH.SelectedItem = "32";
            UpdatePicture();
        }

        private void C1Size64_Click(object sender, EventArgs e)
        {
            C1dpdW.SelectedItem = "64";
            C1dpdH.SelectedItem = "64";
            UpdatePicture();
        }

        private void C2btnMoveX1_Click(object sender, EventArgs e)
        {
            int arg = (int)C2numShiftX.Value;
            if ((int)C2numX.Value > arg)
                C2numX.Value -= arg;
            else
                C2numX.Value = 0;
        }

        private void C2btnMoveX2_Click(object sender, EventArgs e)
        {
            int arg = (int)C2numShiftX.Value;
            if ((int)C2numX.Value < 1023 - arg)
                C2numX.Value += arg;
            /*            else
                            C2numX.Value = 1023;*/
        }

        private void C2btnMoveY1_Click(object sender, EventArgs e)
        {
            int arg = (int)C2numShiftY.Value;
            if ((int)C2numY.Value > arg)
                C2numY.Value -= arg;
            else
                C2numY.Value = 0;
        }

        private void C2btnMoveY2_Click(object sender, EventArgs e)
        {
            int arg = (int)C2numShiftY.Value;
            if ((int)C2numY.Value < 127 - arg)
                C2numY.Value += arg;
            /*            else
                            C2numY.Value = 127;*/
        }

        private void splitContainer1_GotFocus(object sender, EventArgs e)
        {
            tabControl1.Focus();
        }
    }
}
