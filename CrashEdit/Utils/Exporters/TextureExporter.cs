using CrashEdit.CE;
using CrashEdit.Crash;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CrashEdit.Exporters
{
    public class TextureExporter
    {
        public static Bitmap CreateTexture(byte[] data, TexInfoUnpacked info)
        {
            Bitmap bmp = new(info.Width, info.Height);
            Rectangle brect = new(Point.Empty, bmp.Size);
            BitmapData bdata = bmp.LockBits(brect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int[] palette = [];
            int colormode = info.Color;

            if (colormode == 0)
            {
                int clutx = info.ClutX;
                int cluty = info.ClutY;
                palette = new int[16];
                for (int x = 0; x < 16; ++x)
                    palette[x] = PixelConv.Convert5551_8888(BitConv.FromInt16(data, cluty * 512 + (clutx * 16 + x) * 2), info.Blend);
            }
            else if (colormode == 1)
            {
                int cluty = info.ClutY;
                palette = new int[256];
                for (int x = 0; x < 256; ++x)
                    palette[x] = PixelConv.Convert5551_8888(BitConv.FromInt16(data, cluty * 512 + x * 2), info.Blend);
            }
            else if (colormode != 2)
                throw new Exception("invalid colormode");

            try
            {
                for (int y = 0; y < info.Height; y++)
                {
                    int sy = info.Top + y;

                    for (int x = 0; x < info.Width; x++)
                    {
                        int sx = info.Left + x;

                        int pixel =
                            colormode == 0 ? palette[data[sx / 2 + sy * 512] >> ((sx & 1) == 0 ? 0 : 4) & 0xF] :
                            colormode == 1 ? palette[data[sx + sy * 512]] :
                            PixelConv.Convert5551_8888(BitConv.FromInt16(data, sx * 2 + sy * 512), info.Blend);

                        Marshal.WriteInt32(bdata.Scan0, x * 4 + y * bdata.Stride, pixel);
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(bdata);
            }

            return bmp;
        }

        /// <summary>
        /// Resizes bitmaps to normalize them.
        /// </summary>
        public static List<Bitmap> NormalizeBitmaps(List<Bitmap> bitmaps)
        {
            int targetWidth = bitmaps.Max(b => b.Width);
            int targetHeight = bitmaps.Max(b => b.Height);

            return bitmaps.Select(b =>
            {
                if (b.Width == targetWidth && b.Height == targetHeight)
                    return b;

                Bitmap resized = new(targetWidth, targetHeight);

                using (Graphics g = Graphics.FromImage(resized))
                {
                    g.Clear(Color.Transparent);

                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;

                    g.DrawImage(b, 0, 0, targetWidth, targetHeight);
                }

                return resized;
            }).ToList();
        }

        /// <summary>
        /// Combines bitmaps horizontally.
        /// </summary>
        public static Bitmap CombineBitmaps(List<Bitmap> bitmaps)
        {
            int width = bitmaps.Sum(b => b.Width);
            int height = bitmaps.Max(b => b.Height);

            Bitmap result = new(width, height);

            using (Graphics g = Graphics.FromImage(result))
            {
                int offsetX = 0;

                foreach (var bmp in bitmaps)
                {
                    g.DrawImage(bmp, offsetX, 0);
                    offsetX += bmp.Width;
                }
            }

            return result;
        }
    }
}