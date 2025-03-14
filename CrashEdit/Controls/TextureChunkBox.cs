using AltUI.Controls;
using CrashEdit.CE.Controls;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using MetroSet_UI.Controls;
using System.Drawing.Imaging;

namespace CrashEdit.CE
{
    public sealed class TextureChunkBox : UserControl
    {
        private MetroSetTabControl tbcTabs;

        private TextureViewer? frmViewer = null;

        private TextureChunk texturechunk;

        private DarkToolTip tipClick;

        private PictureBox pictureBox;

        public TextureChunkBox(TextureChunk chunk)
        {
            texturechunk = chunk;
            tbcTabs = new MetroSetTabControl()
            {
                BackgroundColor = Color.FromArgb(31, 31, 32),
                Dock = DockStyle.Fill,
                IsDerivedStyle = false,
                ItemSize = new Size(100, 28),
                Style = MetroSet_UI.Enums.Style.Dark,
                TabStyle = MetroSet_UI.Enums.TabStyle.Style1
            };
            tbcTabs.KeyDown += (sender, e) =>
            {
                if (e.Control && e.KeyCode == Keys.R)
                {
                    UpdatePicture(chunk.Data);
                }
            };
            {
                TabPage page = new TabPage("Hex");
                page.Controls.Add(new HexView(this, chunk.Data, HexView_DataChangeHandler) { Dock = DockStyle.Fill });
                tbcTabs.TabPages.Add(page);
            }
            {
                Bitmap bitmap = new Bitmap(512, 128, PixelFormat.Format16bppArgb1555);
                Rectangle brect = new Rectangle(Point.Empty, bitmap.Size);
                BitmapData bdata = bitmap.LockBits(brect, ImageLockMode.WriteOnly, PixelFormat.Format16bppArgb1555);
                try
                {
                    for (int y = 0; y < 128; y++)
                    {
                        for (int x = 0; x < 512; x++)
                        {
                            byte color = chunk.Data[x + y * 512];
                            color >>= 3;
                            short color16 = PixelConv.Pack1555(1, color, color, color);
                            System.Runtime.InteropServices.Marshal.WriteInt16(bdata.Scan0, x * 2 + y * bdata.Stride, color16);
                        }
                    }
                }
                finally
                {
                    bitmap.UnlockBits(bdata);
                }

                FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel()
                {
                    AutoSize = true,
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false
                };

                pictureBox = new PictureBox
                {
                    Image = bitmap,
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Cursor = Cursors.Hand
                };
                pictureBox.Click += new EventHandler(OpenViewer);
                tipClick = new DarkToolTip();
                tipClick.SetToolTip(pictureBox, Resources.TextureChunkBox_TipText);

                DarkButton cmdReload = new DarkButton()
                {
                    Text = "Reload",
                    Size = new Size(78, 28)
                };
                cmdReload.Click += (sender, e) =>
                {
                    UpdatePicture(chunk.Data);
                };

                //TabPage page = new TabPage("Monochrome 8");
                TabPage page = new TabPage("Viewer")
                {
                    BackColor = Color.FromArgb(31, 31, 32)
                };
                flowLayoutPanel.Controls.Add(pictureBox);
                flowLayoutPanel.Controls.Add(cmdReload);
                page.Controls.Add(flowLayoutPanel);
                tbcTabs.TabPages.Add(page);
            }
            {
            //    Bitmap bitmap = new Bitmap(256, 128, PixelFormat.Format16bppArgb1555);
            //    Rectangle brect = new Rectangle(Point.Empty, bitmap.Size);
            //    BitmapData bdata = bitmap.LockBits(brect, ImageLockMode.WriteOnly, PixelFormat.Format16bppArgb1555);
            //    try
            //    {
            //        for (int y = 0; y < 128; y++)
            //        {
            //            for (int x = 0; x < 256; x++)
            //            {
            //                short color = BitConv.FromInt16(chunk.Data, x * 2 + y * 512);
            //                PixelConv.Unpack1555(color, out byte alpha, out byte blue, out byte green, out byte red);
            //                color = PixelConv.Pack1555(1, red, green, blue);
            //                System.Runtime.InteropServices.Marshal.WriteInt16(bdata.Scan0, x * 2 + y * bdata.Stride, color);
            //            }
            //        }
            //    }
            //    finally
            //    {
            //        bitmap.UnlockBits(bdata);
            //    }
            //    PictureBox picture = new PictureBox
            //    {
            //        Dock = DockStyle.Fill,
            //        Image = bitmap,
            //        Cursor = Cursors.Hand
            //    };
            //    picture.Click += new EventHandler(OpenViewer);
            //    tipClick = new DarkToolTip();
            //    tipClick.SetToolTip(picture, Resources.TextureChunkBox_TipText);
            //    TabPage page = new TabPage("BGR555");
            //    page.Controls.Add(picture);
            //    page.BackColor = Color.FromArgb(31, 31, 32);
            //    tbcTabs.TabPages.Add(page);
            }
            {
                CLUTBox clut = new CLUTBox(chunk)
                {
                    Dock = DockStyle.Fill
                };
                TabPage page = new TabPage("CLUT");
                page.Controls.Add(clut);
               
                EventHandler tabChangedHandler = null;
                tabChangedHandler = (sender, e) =>
                {
                    if (tbcTabs.SelectedTab == page)
                    {
                        clut.OnTabSelected();
                        tbcTabs.SelectedIndexChanged -= tabChangedHandler;
                    }
                };
                tbcTabs.SelectedIndexChanged += tabChangedHandler;

                tbcTabs.TabPages.Add(page);
            }

            tbcTabs.SelectedIndex = 1;
            Controls.Add(tbcTabs);
        }

        private void UpdatePicture(byte[] newChunkData)
        {
            Bitmap newBitmap = new Bitmap(512, 128, PixelFormat.Format16bppArgb1555);
            Rectangle brect = new Rectangle(Point.Empty, newBitmap.Size);
            BitmapData bdata = newBitmap.LockBits(brect, ImageLockMode.WriteOnly, PixelFormat.Format16bppArgb1555);
            try
            {
                for (int y = 0; y < 128; y++)
                {
                    for (int x = 0; x < 512; x++)
                    {
                        byte color = newChunkData[x + y * 512];
                        color >>= 3;
                        short color16 = PixelConv.Pack1555(1, color, color, color);
                        System.Runtime.InteropServices.Marshal.WriteInt16(bdata.Scan0, x * 2 + y * bdata.Stride, color16);
                    }
                }
            }
            finally
            {
                newBitmap.UnlockBits(bdata);
            }
            pictureBox.Image = newBitmap;
        }

        private void OpenViewer(object sender, EventArgs e)
        {
            if (frmViewer == null)
            {
                frmViewer = new TextureViewer(texturechunk);
                frmViewer.FormClosing += delegate (object sender2, FormClosingEventArgs e2)
                {
                    frmViewer = null;
                };
                frmViewer.Show(this);
            }
            else
                frmViewer.Select();
        }

        private bool HexView_DataChangeHandler(int destOffset, int destLength, byte[] source)
        {
            var data = texturechunk.Data;

            if (destLength != source.Length)
                throw new ArgumentException();
            if (destOffset < 0 || destOffset >= data.Length)
                throw new ArgumentException();

            Array.Copy(source, 0, data, destOffset, destLength);
            return true;
        }

        public void ReplaceData(byte[] source)
        {
            texturechunk.Data = source;
            UpdatePicture(source);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (frmViewer != null)
                frmViewer.Dispose();
        }
    }
}
