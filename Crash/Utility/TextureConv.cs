using System.Drawing.Imaging;
using System.Drawing;
using CrashEdit.CE.Properties;

namespace CrashEdit.Crash
{
    public static class TextureConv
    {
        const int VRAMWidth = 512;
        const int VRAMHeight = 128;
        private static byte[] vram = new byte[VRAMWidth * VRAMHeight];

        public static byte[] ReplaceTexture(byte[] srcTexture, byte[] destTexture, int textureWidth, int textureHeight, int bpp, int srcX, int srcY, int width, int height, int destX, int destY)
        {
            bool is8bpp = (bpp == 8);

            //if (textureWidth != VRAMWidth * (is8bpp ? 1 : 2))
            //    CreateBufferFromTexture(srcTexture, textureWidth, textureHeight, is8bpp);
            //else
            //    vram = srcTexture;
            Settings.Default.Reload();
            CreateBuffer(srcTexture, textureWidth, textureHeight, is8bpp);

            if (bpp == 4)
            {
                for (int i = 0; i < height; i++)
                {
                    int srcOffset = (i + srcY) * 0x200 + srcX / 2;
                    int destOffset = (i + destY) * 0x200 + destX / 2;
                    if (Settings.Default.OutputCopyTextureResult)
                        Console.WriteLine($"i: {i:D2} sourceOffset: {srcOffset:D5}, destinationOffset: {destOffset:D5}");

                    int byteCount = (width + 1) / 2;
                    for (int j = 0; j < byteCount; j++)
                    {
                        byte srcByte = vram[srcOffset + j];
                        byte reversedByte = (byte)(((srcByte & 0xF) << 4) | ((srcByte & 0xF0) >> 4));
                        destTexture[destOffset + j] = reversedByte;
                    }
                }
            }
            else if (bpp == 8)
            {
                for (int i = 0; i < height; i++)
                {
                    int srcOffset = (i + srcY) * 0x200 + srcX;
                    int destOffset = (i + destY) * 0x200 + destX;
                    if (Settings.Default.OutputCopyTextureResult)
                        Console.WriteLine($"i: {i:D2} sourceOffset: {srcOffset:D5}, destinationOffset: {destOffset:D5}");

                    Array.Copy(vram, srcOffset, destTexture, destOffset, width);
                }
            }
            else
            {
                Console.WriteLine("Unsupported bpp value.");
            }

            return destTexture;
        }

        public static void CreateBuffer(byte[] srcTexture, int texture1Width, int texture1Height, bool is8bpp)
        {
            Console.WriteLine();
            Array.Clear(vram, 0, vram.Length);

            int bytesPerPixel = is8bpp ? 1 : 2;
            int rowBytes = is8bpp ? texture1Width : (texture1Width + 1) / 2;

            for (int i = 0; i < texture1Height; i++)
            {
                int sourceOffset = i * rowBytes;
                int destinationOffset = i * VRAMWidth;

                if (sourceOffset + rowBytes <= srcTexture.Length && destinationOffset + rowBytes <= vram.Length)
                {
                    if (Settings.Default.OutputCopyTextureResult)
                        Console.WriteLine($"Buffer_i: {i:D2} sourceOffset: {sourceOffset:D5}, destinationOffset: {destinationOffset:D5}");

                    Array.Copy(srcTexture, sourceOffset, vram, destinationOffset, rowBytes);
                }
                else
                {
                    Console.WriteLine($"Error: Out of bounds copy. sourceOffset: {sourceOffset}, destinationOffset: {destinationOffset}");
                }
                //File.WriteAllBytes("raw_vram.bin", vram); // debug
            }
        }

        public static (byte[] rawImageData, byte[] palette, int width, int height) ProcessBmp(string filePath)
        {
            byte[] bmpData = File.ReadAllBytes(filePath);
            int width = BitConverter.ToInt32(bmpData, 18);
            int height = BitConverter.ToInt32(bmpData, 22);
            int offset = BitConverter.ToInt32(bmpData, 10);
            int bitsPerPixel = BitConverter.ToInt16(bmpData, 28);

            if (bitsPerPixel != 4 && bitsPerPixel != 8)
            {
                throw new InvalidOperationException("The loaded image is not 4bpp or 8bpp.");
            }

            int rowSize, paletteSize;
            if (bitsPerPixel == 4)
            {
                rowSize = (width + 1) / 2;
                paletteSize = 16 * 4;
            }
            else
            {
                rowSize = width;
                paletteSize = 256 * 4;
            }
            int pixelDataOffset = BitConverter.ToInt32(bmpData, 10);
            int pixelDataSize = rowSize * height;

            if (bmpData.Length < 54 + paletteSize)
            {
                throw new InvalidOperationException("The BMP file is corrupted or incomplete.");
            }
            byte[] paletteData = new byte[paletteSize];
            Array.Copy(bmpData, 54, paletteData, 0, paletteSize);

            byte[] pixelData = new byte[pixelDataSize];
            Array.Copy(bmpData, offset, pixelData, 0, pixelDataSize);

            byte[] rawImageData = new byte[pixelDataSize];

            for (int y = 0; y < height; y++)
            {
                int flippedY = height - 1 - y;
                int srcOffset = y * rowSize;
                int destOffset = flippedY * rowSize;

                Array.Copy(pixelData, srcOffset, rawImageData, destOffset, rowSize);
            }

            return (rawImageData, paletteData, width, height);
        }

        public static byte[] ConvertPaletteToRGBA5551(byte[] palette)
        {
            int paletteSize = palette.Length / 4;
            byte[] convertedPalette = new byte[paletteSize * 2];

            for (int i = 0; i < paletteSize; i++)
            {
                byte r = palette[i * 4];
                byte g = palette[i * 4 + 1];
                byte b = palette[i * 4 + 2];
                byte a = palette[i * 4 + 3];

                ushort rgba5551 = ConvertToRGBA5551(r, g, b, a);
                convertedPalette[i * 2] = (byte)(rgba5551 & 0xFF);
                convertedPalette[i * 2 + 1] = (byte)((rgba5551 >> 8) & 0xFF);
            }

            return convertedPalette;
        }

        private static ushort ConvertToRGBA5551(byte r, byte g, byte b, byte a)
        {
            ushort rgba5551 = 0;

            rgba5551 |= (ushort)((r >> 3) << 10);  // Red: 5 bits
            rgba5551 |= (ushort)((g >> 3) << 5);   // Green: 5 bits
            rgba5551 |= (ushort)((b >> 3));        // Blue: 5 bits
            rgba5551 |= (ushort)((a > 0 ? 1 : 0) << 15);  // Alpha: 1 bit (opaque)

            return rgba5551;
        }

        public static (byte[] rawImageData, byte[] palette, int width, int height) ProcessPng(string filePath, bool isBGRA)
        {
            using (Bitmap bitmap = new Bitmap(filePath))
            {
                if (bitmap.PixelFormat != PixelFormat.Format4bppIndexed &&
                    bitmap.PixelFormat != PixelFormat.Format8bppIndexed)
                {
                    throw new InvalidOperationException($"Unsupported pixel format: {bitmap.PixelFormat}");
                }

                if (bitmap.Width <= 0 || bitmap.Height <= 0)
                {
                    throw new InvalidOperationException("Invalid image dimensions.");
                }

                ColorPalette palette = bitmap.Palette;
                Color[] paletteColors = new Color[palette.Entries.Length];
                if (isBGRA)
                {
                    for (int i = 0; i < palette.Entries.Length; i++)
                    {
                        Color color = palette.Entries[i];
                        paletteColors[i] = Color.FromArgb(color.A, color.B, color.G, color.R);
                    }
                }
                else
                {
                    paletteColors = palette.Entries;
                }

                byte[] paletteData = new byte[palette.Entries.Length * 4];
                int index = 0;
                foreach (Color color in paletteColors)
                {
                    paletteData[index++] = color.R;
                    paletteData[index++] = color.G;
                    paletteData[index++] = color.B;
                    paletteData[index++] = color.A;
                }

                byte[] rawImageData = ExtractRawImageData(bitmap);
                return (rawImageData, paletteData, bitmap.Width, bitmap.Height);
            }
        }

        private static byte[] ExtractRawImageData(Bitmap bitmap)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);

            int bitsPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat);
            int bytesPerPixel = bitsPerPixel / 8;
            int byteWidth = (bitmap.Width * bitsPerPixel + 7) / 8;

            if (byteWidth <= 0 || bitmap.Height <= 0)
            {
                throw new InvalidOperationException("Invalid image dimensions.");
            }

            byte[] rawImageData = new byte[byteWidth * bitmap.Height];

            for (int y = 0; y < bitmap.Height; y++)
            {
                IntPtr rowPtr = bitmapData.Scan0 + y * bitmapData.Stride;
                System.Runtime.InteropServices.Marshal.Copy(rowPtr, rawImageData, y * byteWidth, byteWidth);
            }

            bitmap.UnlockBits(bitmapData);
            return rawImageData;
        }

    }
}
