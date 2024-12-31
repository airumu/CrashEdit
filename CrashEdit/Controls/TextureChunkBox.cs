using AltUI.Controls;
using CrashEdit.CE.Controls;
using CrashEdit.Crash;
using MetroSet_UI.Controls;
using System.Drawing.Imaging;
using System.Runtime;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace CrashEdit.CE
{
    public sealed class TextureChunkBox : UserControl
    {
        private MetroSetTabControl tbcTabs;

        private TextureViewer frmViewer = null;

        private TextureChunk texturechunk;

        private DarkToolTip tipClick;

        public TextureChunkBox(TextureChunk chunk)
        {
            texturechunk = chunk;
            tbcTabs = new MetroSetTabControl()
            {
                Dock = DockStyle.Fill,
                ItemSize = new Size(100, 28),
                Style = MetroSet_UI.Enums.Style.Dark,
                TabStyle = MetroSet_UI.Enums.TabStyle.Style1
            };
            {
                HexView hex = new HexView
                {
                    Data = chunk.Data,
                    DataChangeHandler = HexView_DataChangeHandler,
                    Dock = DockStyle.Fill,
                };
                TabPage page = new TabPage("Hex");
                page.Controls.Add(hex);
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
                PictureBox picture = new PictureBox();
                picture.Dock = DockStyle.Fill;
                picture.Image = bitmap;
                picture.Click += new EventHandler(OpenViewer);
                picture.Cursor = Cursors.Hand;
                tipClick = new DarkToolTip();
                tipClick.SetToolTip(picture, "Click to open the viewer");
                TabPage page = new TabPage("Monochrome 8");
                page.Controls.Add(picture);
                page.BackColor = Color.FromArgb(31, 31, 32);
                tbcTabs.TabPages.Add(page);
            }
            {
                Bitmap bitmap = new Bitmap(256, 128, PixelFormat.Format16bppArgb1555);
                Rectangle brect = new Rectangle(Point.Empty, bitmap.Size);
                BitmapData bdata = bitmap.LockBits(brect, ImageLockMode.WriteOnly, PixelFormat.Format16bppArgb1555);
                try
                {
                    for (int y = 0; y < 128; y++)
                    {
                        for (int x = 0; x < 256; x++)
                        {
                            short color = BitConv.FromInt16(chunk.Data, x * 2 + y * 512);
                            PixelConv.Unpack1555(color, out byte alpha, out byte blue, out byte green, out byte red);
                            color = PixelConv.Pack1555(1, red, green, blue);
                            System.Runtime.InteropServices.Marshal.WriteInt16(bdata.Scan0, x * 2 + y * bdata.Stride, color);
                        }
                    }
                }
                finally
                {
                    bitmap.UnlockBits(bdata);
                }
                PictureBox picture = new PictureBox();
                picture.Dock = DockStyle.Fill;
                picture.Image = bitmap;
                picture.Click += new EventHandler(OpenViewer);
                picture.Cursor = Cursors.Hand;
                tipClick = new DarkToolTip();
                tipClick.SetToolTip(picture, "Click to open the viewer");
                TabPage page = new TabPage("BGR555");
                page.Controls.Add(picture);
                page.BackColor = Color.FromArgb(31, 31, 32);
                tbcTabs.TabPages.Add(page);
                tbcTabs.SelectedTab = page;
            }
            {
                CLUTBox clut = new CLUTBox(chunk)
                {
                    Dock = DockStyle.Fill
                };
                TabPage page = new TabPage("CLUT");
                page.Controls.Add(clut);
                tbcTabs.TabPages.Add(page);
            }

            Controls.Add(tbcTabs);
            tbcTabs.SelectedIndex = 1;
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
    }
}
