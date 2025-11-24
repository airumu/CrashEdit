using System.Drawing.Imaging;
using AltUI.Controls;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public partial class T21EntryBox : UserControl
    {
        private T21EntryController controller;
        private DarkToolTip tipViewer = new();

        private byte[] Header = [];
        private List<(int Index, byte[] Data)> Palettes = [];
        private List<(int Index, byte[] Data)> Images = [];

        internal Stack<bool> dirty = new Stack<bool>();
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        public T21EntryBox(T21EntryController controller)
        {
            this.controller = controller;
            InitializeComponent();
            MainInit();
        }

        private void MainInit()
        {
            dirty.Push(true);

            int index = 0;
            int paletteCount = 0;
            int imageCount = 0;
            foreach (var bytes in controller.T21Entry.Items)
            {
                byte first = bytes[0];
                if (index == 0)
                {
                    Header = bytes;
                    index++;
                }
                else if ((first & 0xF0) == 0x40)
                {
                    int headerLength = 6;
                    var trimmed = new byte[512];
                    Array.Copy(bytes, headerLength, trimmed, 0, trimmed.Length);
                    Palettes.Add((index, trimmed));
                    cmbPalette.Items.Add(paletteCount);
                    index++;
                    paletteCount++;
                }
                else if ((first & 0xF0) == 0x80)
                {
                    Images.Add((index, bytes));
                    cmbImage.Items.Add(imageCount);
                    index++;
                    imageCount++;
                }
                else
                {
                    index++;
                }
            }
            if (Images.Count > 0 && Palettes.Count > 0)
            {
                if (Images.Count != Palettes.Count)
                {
                    chkSync.Enabled = false;
                    chkSync.Checked = false;
                }
                cmbPalette.SelectedIndex = 0;
                cmbImage.SelectedIndex = 0;
                UpdatePicture();
            }
            else
            {
                fraPicture.Enabled = false;
                picImage.Image = null;
            }
            lblInfo.Text = $"Palettes: {Palettes.Count}\r\nImages: {Images.Count}";

            picImage.MouseClick += delegate (object? sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right && picImage.Image != null && picImage.Image is Bitmap bmp)
                {
                    using (MemoryStream w = new MemoryStream())
                    {
                        Rectangle region = new Rectangle(0, 0, bmp.Width, bmp.Height);
                        bmp.Clone(region, PixelFormat.Format32bppArgb).Save(w, ImageFormat.Png);
                        FileUtil.SaveFile($"T21_{cmbPalette.Text}_{cmbImage.Text}", w.ToArray(), FileFilters.PNG);
                    }
                }
            };
            tipViewer.SetToolTip(picImage, Resources.T21_tipViewer);
            numW.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);
            numH.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);

            dirty.Pop();
        }

        private void UpdatePicture()
        {
            Bitmap bmp = DecodeToBitmap(Images[cmbImage.SelectedIndex].Data, (int)numW.Value, (int)numH.Value, Palettes[cmbPalette.SelectedIndex].Data);
            picImage.Image = bmp;
            if (Images[cmbImage.SelectedIndex].Data[0] == 0x82)
            {
                lblUnknown.Visible = false;
            }
            else
            {
                lblUnknown.Visible = true;
            }
        }

        private Bitmap DecodeToBitmap(byte[] data, int width, int height, byte[] paletteData)
        {
            int[] _palette = new int[256];
            int blendmode = 3;
            for (int i = 0; i < 256; ++i)
            {
                _palette[i] = PixelConv.Convert5551_8888(BitConv.FromInt16(paletteData, i * 2), blendmode);
            }
            Color[] palette = _palette.Select(c => Color.FromArgb(c)).ToArray();

            if (Settings.Default.OutputModelTextureInfo)
                Console.WriteLine($"\r\nDecoding T21 image with palette_{cmbPalette.Text}, image_{cmbImage.Text}, {width}x{height}.");

            int totalPixels = width * height;
            var pixels = new List<byte>(totalPixels);

            int cur = 0;
            int nextPos = 0;
            int headerLen;

            // Check for the first compressed segment
            if (data[0] == 0x82)
            {
                byte b3 = data[1];
                if ((b3 & 0x80) != 0)
                {
                    if (b3 == 0x80)
                    {
                        byte b4 = data[2];
                        byte b5 = data[3];
                        headerLen = 4;
                        nextPos = (b4 << 8) | b5;
                    }
                    else
                    {
                        headerLen = 2;
                        nextPos = 0x80 - (b3 & 0x7F);
                    }
                }
                else
                {
                    headerLen = 1;
                }
                nextPos += headerLen;
                cur += headerLen;
                if (Settings.Default.OutputModelTextureInfo)
                    Console.WriteLine($"    nextPos=0x{nextPos:X3}");
            }

            while (cur < data.Length)
            {
                if (cur == nextPos && data[0] == 0x82)
                {
                    byte count = data[cur];
                    byte colorIndex = data[cur + 1];
                    byte b3 = data[cur + 2];
                    if (count == 0 && b3 == 0)
                    {
                        if (Settings.Default.OutputModelTextureInfo)
                            Console.WriteLine($"    End of image reached at 0x{cur:X3}.");
                        break;
                    }
                    else if ((b3 & 0x80) != 0)
                    {
                        if (b3 == 0x80)
                        {
                            byte b4 = data[cur + 3];
                            byte b5 = data[cur + 4];
                            headerLen = 5;
                            nextPos = cur + ((b4 << 8) | b5);
                        }
                        else
                        {
                            headerLen = 3;
                            nextPos = cur + (0x80 - (b3 & 0x7F));
                        }
                    }
                    else
                    {
                        headerLen = 2;
                    }
                    nextPos += headerLen;
                    cur += headerLen;

                    for (int i = 0; i <= count; i++)
                    {
                        pixels.Add(colorIndex);
                    }
                    if (Settings.Default.OutputModelTextureInfo)
                        Console.WriteLine($"    cur=0x{cur - headerLen:X3}, count=0x{count:X2}, colorIndex=0x{colorIndex:X2}, headerLen={headerLen:X}, nextPos=0x{nextPos:X3}");
                }
                else
                {
                    pixels.Add(data[cur]);
                    cur++;
                }
            }

            // Pad/trim
            if (pixels.Count < totalPixels)
            {
                pixels.AddRange(Enumerable.Repeat((byte)0, totalPixels - pixels.Count));
            }
            else if (pixels.Count > totalPixels)
            {
                pixels.RemoveRange(totalPixels, pixels.Count - totalPixels);
            }

            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            for (int y = 0; y < height; y++)
            {
                int rowBase = y * width;
                for (int x = 0; x < width; x++)
                {
                    Color c = palette[pixels[rowBase + x]];
                    bmp.SetPixel(x, y, c);
                }
            }
            return bmp;
        }


        private void ScrollHandlerFunction(object sender, MouseEventArgs e)
        {
            if (sender is NumericUpDown numericUpDown)
            {
                HandledMouseEventArgs? handledArgs = e as HandledMouseEventArgs;
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

        private void cmbPalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                if (chkSync.Checked)
                {
                    dirty.Push(true);
                    cmbImage.SelectedIndex = cmbPalette.SelectedIndex;
                    UpdatePicture();
                    dirty.Pop();
                }
                else
                {
                    UpdatePicture();
                }
            }
        }

        private void cmbImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                if (chkSync.Checked)
                {
                    dirty.Push(true);
                    cmbPalette.SelectedIndex = cmbImage.SelectedIndex;
                    UpdatePicture();
                    dirty.Pop();
                }
                else
                {
                    UpdatePicture();
                }
            }
        }

        private void cmdReplacePalette_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All Files|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] data = File.ReadAllBytes(openFileDialog.FileName);

                    dirty.Push(true);
                    var paletteTuple = Palettes[cmbPalette.SelectedIndex];
                    paletteTuple.Data = data;
                    Palettes[cmbPalette.SelectedIndex] = paletteTuple;
                    controller.T21Entry.Items[Palettes[cmbPalette.SelectedIndex].Index] = data;
                    UpdatePicture();
                    dirty.Pop();
                }
            }
        }

        private void cmdReplaceImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All Files|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] data = File.ReadAllBytes(openFileDialog.FileName);

                    dirty.Push(true);
                    var paletteTuple = Images[cmbImage.SelectedIndex];
                    paletteTuple.Data = data;
                    Images[cmbImage.SelectedIndex] = paletteTuple;
                    controller.T21Entry.Items[Images[cmbImage.SelectedIndex].Index] = data;
                    UpdatePicture();
                    dirty.Pop();
                }
            }
        }

        private void numW_ValueChanged(object sender, EventArgs e)
        {
            UpdatePicture();
        }

        private void numH_ValueChanged(object sender, EventArgs e)
        {
            UpdatePicture();
        }
    }
}
