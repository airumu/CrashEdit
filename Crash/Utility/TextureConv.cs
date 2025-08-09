using System.Drawing.Imaging;
using System.Drawing;
using CrashEdit.CE.Properties;
using AltUI.Forms;
using System.Windows.Forms;

namespace CrashEdit.Crash
{
    public class TextureConv
    {
        const int VRAMWidth = 512;
        const int VRAMHeight = 128;
        private static byte[] vram = new byte[VRAMWidth * VRAMHeight];

        private const string TitleTextureReplacement = "Texture Replacement";
        private const string TitleCLUTReplacement = "CLUT Replacement";

        public static byte[] ReplaceTextureFromFile(string filePath, string extension, bool isBGRA, byte[] currentData, int destX, int destY, bool replaceCLUT, int oldBpp, int clutX, int clutY)
        {
            byte[] newData = currentData;
            bool failed = false;
            switch (extension)
            {
                case ".bmp":
                    try
                    {
                        var result = ProcessBmp(filePath);
                        newData = ReplaceTextureFromViewer(currentData, result.rawImageData, result.palette, result.width, result.height, destX, destY, replaceCLUT, oldBpp, clutX, clutY);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;
                case ".png":
                    try
                    {
                        var result = ProcessPng(filePath, isBGRA, oldBpp);
                        newData = ReplaceTextureFromViewer(currentData, result.rawImageData, result.palette, result.width, result.height, destX, destY, replaceCLUT, oldBpp, clutX, clutY);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;
                default:
                    Console.WriteLine("The selected file is not a supported image format.");
                    failed = true;
                    break;
            }

            if (failed)
            {
                Console.WriteLine("Failed to process the image file.");
            }

            return newData;
        }

        private static byte[] ReplaceTextureFromViewer(byte[] currentData, byte[] rawImageData, byte[] palette, int width, int height, int destX, int destY, bool replaceCLUT, int oldBpp, int clutX, int clutY)
        {
            byte[] rgba5551List = ConvertPaletteToRGBA5551(palette);
            int paletteCount = rgba5551List.Length / 2;
            int bpp = (paletteCount <= 16) ? 4 : 8;

            if (bpp != oldBpp)
            {
                DarkMessageBox.ShowError("The color depth of the selected image differs from the current one.", TitleTextureReplacement);
                return currentData;
            }

            byte[] newTextureData = ReplaceTexture(rawImageData, currentData, width, height, bpp, 0, 0, width, height, destX / (bpp == 8 ? 2 : 1), destY, true);
            byte[] newTPage = newTextureData;

            WriteResult(rgba5551List, rawImageData, paletteCount, bpp, width, height);

            if (replaceCLUT)
            {
                newTPage = ReplaceClut(rgba5551List, newTPage, bpp, oldBpp, clutX, clutY);
            }

            BitConv.ToInt32(newTPage, 12, Chunk.CalculateChecksum(newTPage));

            return newTPage;
        }

        private static void WriteResult(byte[] rgba5551List, byte[] rawImageData, int paletteCount, int bpp, int width, int height)
        {
            Console.WriteLine("========================================");
            Console.WriteLine($"Raw Image Data Length: {rawImageData.Length}");
            Console.WriteLine($"Image Size: {width} x {height}");
            Console.WriteLine($"Palette Count: {paletteCount}, {bpp}bpp");
            string hexString = BitConverter.ToString(rgba5551List).Replace("-", "");
            Console.WriteLine($"Palette (RGBA5551 format):\r\n{hexString}");
        }

        public static byte[] ReplaceClut(byte[] rgba5551List, byte[] newTPage, int bpp, int oldBpp, int clutX, int clutY)
        {
            bool doProcess = true;
            if (bpp != oldBpp)
            {
                if (DarkMessageBox.ShowWarning("The color depth of the selected image differs from the current one. Do you want to process anyway?", TitleCLUTReplacement, DarkDialogButton.YesNo) != DialogResult.Yes)
                    doProcess = false;
            }

            if ((clutX == 0 || bpp == 8) && clutY == 0)
            {
                DarkMessageBox.ShowError("CLUT cannot be replaced on the header.", TitleCLUTReplacement);
                doProcess = false;
            }

            if (doProcess)
            {
                int offset = (bpp == 8) ? clutY * 0x200 : clutX * 0x20 + clutY * 0x200;
                Array.Copy(rgba5551List, 0, newTPage, offset, rgba5551List.Length);
                Console.WriteLine("CLUT replacement done.");
            }
            else
            {
                Console.WriteLine("CLUT replacement cancelled.");
            }

            return newTPage;
        }

        public static byte[] ReplaceTexture(byte[] srcTexture, byte[] destTexture, int textureWidth, int textureHeight, int bpp, int srcX, int srcY, int width, int height, int destX, int destY, bool isFromFile)
        {
            bool is8bpp = (bpp == 8);

            //if (textureWidth != VRAMWidth * (is8bpp ? 1 : 2))
            //    CreateBufferFromTexture(srcTexture, textureWidth, textureHeight, is8bpp);
            //else
            //    vram = srcTexture;
            Settings.Default.Reload();
            CreateBuffer(srcTexture, textureWidth, textureHeight, is8bpp, isFromFile, 0, 0);

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
                Console.WriteLine("Unsupported color depth.");
            }

            return destTexture;
        }

        public static (byte[] tempTexture, int tempWidth, int tempHeight, int tempBpp) CopyTexture(byte[] srcTexture, int bpp, int srcX, int srcY, int width, int height)
        {
            bool is8bpp = (bpp == 8);
            int destX = 0;
            int destY = 0;
            byte[] tempTexture = new byte[VRAMWidth * VRAMHeight];

            Settings.Default.Reload();
            CreateBuffer(srcTexture, width, height, is8bpp, false, srcX, srcY);

            if (bpp == 4)
            {
                for (int i = 0; i < height; i++)
                {
                    int srcOffset = i * 0x200;
                    int destOffset = (i + destY) * 0x200 + destX / 2;
                    if (Settings.Default.OutputCopyTextureResult)
                        Console.WriteLine($"i: {i:D2} sourceOffset: {srcOffset:D5}, destinationOffset: {destOffset:D5}");

                    int byteCount = (width + 1) / 2;
                    for (int j = 0; j < byteCount; j++)
                    {
                        byte srcByte = vram[srcOffset + j];
                        byte reversedByte = (byte)(((srcByte & 0xF) << 4) | ((srcByte & 0xF0) >> 4));
                        tempTexture[destOffset + j] = reversedByte;
                    }
                }
            }
            else if (bpp == 8)
            {
                for (int i = 0; i < height; i++)
                {
                    int srcOffset = i * 0x200;
                    int destOffset = (i + destY) * 0x200 + destX;
                    if (Settings.Default.OutputCopyTextureResult)
                        Console.WriteLine($"i: {i:D2} sourceOffset: {srcOffset:D5}, destinationOffset: {destOffset:D5}");

                    Array.Copy(vram, srcOffset, tempTexture, destOffset, width);
                }
            }
            else
            {
                Console.WriteLine("Unsupported color depth.");
            }

            return (tempTexture, width, height, bpp);
        }

        private static void CreateBuffer(byte[] srcTexture, int textureWidth, int textureHeight, bool is8bpp, bool isFromFile, int srcX, int srcY)
        {
            if (Settings.Default.OutputCopyTextureResult)
            {
                Console.WriteLine();
                Console.WriteLine($"srcX: {srcX}, srcY: {srcY}");
            }

            Array.Clear(vram, 0, vram.Length);

            int bytesPerPixel = is8bpp ? 1 : 2;
            int rowBytes = is8bpp ? textureWidth : (textureWidth + 1) / 2;

            for (int i = 0; i < textureHeight; i++)
            {
                int sourceOffset;
                if (isFromFile)
                    sourceOffset = i * rowBytes;
                else
                    sourceOffset = (i + srcY) * 0x200 + srcX / (is8bpp ? 1 : 2);
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
            Console.WriteLine();
            byte[] bmpData = File.ReadAllBytes(filePath);
            int width = BitConverter.ToInt32(bmpData, 18);
            int height = BitConverter.ToInt32(bmpData, 22);
            int offset = BitConverter.ToInt32(bmpData, 10);
            int bitsPerPixel = BitConverter.ToInt16(bmpData, 28);

            if (bitsPerPixel != 4 && bitsPerPixel != 8)
            {
                throw new InvalidOperationException($"Unsupported bpp: {bitsPerPixel}");
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

        public static ushort ConvertToRGBA5551(byte r, byte g, byte b, byte a)
        {
            ushort rgba5551 = 0;

            rgba5551 |= (ushort)((r >> 3) << 10);  // Red: 5 bits
            rgba5551 |= (ushort)((g >> 3) << 5);   // Green: 5 bits
            rgba5551 |= (ushort)((b >> 3));        // Blue: 5 bits
            rgba5551 |= (ushort)((a >= 128 ? 0 : 1) << 15);  // Alpha: 1 bit

            return rgba5551;
        }

        public static (byte[] rawImageData, byte[] palette, int width, int height) ProcessPng(string filePath, bool isBGRA, int oldBpp)
        {
            Console.WriteLine();
            Bitmap bitmap = new Bitmap(filePath);
            if (bitmap.PixelFormat != PixelFormat.Format4bppIndexed &&
                bitmap.PixelFormat != PixelFormat.Format8bppIndexed)
            {
                Console.WriteLine($"Input pixel format: {bitmap.PixelFormat}; start quantization...");
                if (oldBpp == 4)
                {
                    OctreeQuantizer quantizer = new OctreeQuantizer(16);
                    bitmap = quantizer.Quantize4bpp(bitmap);
                }
                else if (oldBpp == 8)
                {
                    OctreeQuantizer quantizer = new OctreeQuantizer(256);
                    bitmap = quantizer.Quantize8bpp(bitmap);
                }
                //throw new InvalidOperationException($"Unsupported pixel format: {bitmap.PixelFormat}");
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

    public class OctreeQuantizer
    {
        private OctreeNode root;
        private int maxColors;
        private int leafCount;
        private List<OctreeNode>[] levels;

        public OctreeQuantizer(int maxColors)
        {
            this.maxColors = maxColors;

            levels = new List<OctreeNode>[9];
            for (int i = 0; i < 9; i++)
            {
                levels[i] = new List<OctreeNode>();
            }

            root = new OctreeNode(0, this);
            leafCount = 0;
        }

        public void AddColor(Color color)
        {
            root.AddColor(color, 0, this);
            while (leafCount > maxColors)
            {
                Reduce();
            }
        }

        public void IncrementLeafCount()
        {
            leafCount++;
        }

        public void DecrementLeafCount(int count)
        {
            leafCount -= count;
        }

        public void AddLevelNode(int level, OctreeNode node)
        {
            if (level < 8)
                levels[level].Add(node);
        }

        private void Reduce()
        {
            int level = 7;
            while (level >= 0 && levels[level].Count == 0)
                level--;

            if (level < 0)
                return;

            OctreeNode node = levels[level][0];
            levels[level].RemoveAt(0);
            int reducedLeaves = node.Reduce();
            DecrementLeafCount(reducedLeaves);
        }

        public Color[] GetPalette()
        {
            Color[] palette = new Color[leafCount];
            int index = 0;
            root.ConstructPalette(ref palette, ref index);
            return palette;
        }

        public Bitmap Quantize4bpp(Bitmap source)
        {
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color color = source.GetPixel(x, y);
                    AddColor(color);
                }
            }

            Color[] palette = GetPalette();

            Bitmap result = new Bitmap(source.Width, source.Height, PixelFormat.Format4bppIndexed);
            ColorPalette bmpPalette = result.Palette;
            for (int i = 0; i < bmpPalette.Entries.Length; i++)
            {
                if (i < palette.Length)
                    bmpPalette.Entries[i] = palette[i];
                else
                    bmpPalette.Entries[i] = Color.Black;
            }
            result.Palette = bmpPalette;

            Rectangle rect = new Rectangle(0, 0, result.Width, result.Height);
            BitmapData data = result.LockBits(rect, ImageLockMode.WriteOnly, result.PixelFormat);
            int stride = data.Stride;
            IntPtr scan0 = data.Scan0;
            byte[] pixelIndices = new byte[stride * result.Height];

            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    Color color = source.GetPixel(x, y);
                    int paletteIndex = root.GetPaletteIndex(color, 0);
                    if (paletteIndex < 0 || paletteIndex > 15)
                        paletteIndex = 0;

                    int byteIndex = y * stride + (x / 2);
                    if (x % 2 == 0)
                    {
                        pixelIndices[byteIndex] = (byte)(((paletteIndex & 0x0F) << 4) | (pixelIndices[byteIndex] & 0x0F));
                    }
                    else
                    {
                        pixelIndices[byteIndex] = (byte)((pixelIndices[byteIndex] & 0xF0) | (paletteIndex & 0x0F));
                    }
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(pixelIndices, 0, scan0, pixelIndices.Length);
            result.UnlockBits(data);

            return result;
        }

        public Bitmap Quantize8bpp(Bitmap source)
        {
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color color = source.GetPixel(x, y);
                    AddColor(color);
                }
            }

            Color[] palette = GetPalette();

            Bitmap result = new Bitmap(source.Width, source.Height, PixelFormat.Format8bppIndexed);
            ColorPalette bmpPalette = result.Palette;
            for (int i = 0; i < palette.Length && i < bmpPalette.Entries.Length; i++)
            {
                bmpPalette.Entries[i] = palette[i];
            }
            result.Palette = bmpPalette;

            Rectangle rect = new Rectangle(0, 0, result.Width, result.Height);
            BitmapData data = result.LockBits(rect, ImageLockMode.WriteOnly, result.PixelFormat);
            int stride = data.Stride;
            IntPtr scan0 = data.Scan0;
            byte[] pixelIndices = new byte[stride * result.Height];

            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    Color color = source.GetPixel(x, y);
                    int paletteIndex = root.GetPaletteIndex(color, 0);
                    int offset = y * stride + x;
                    if (offset < pixelIndices.Length)
                        pixelIndices[offset] = (byte)paletteIndex;
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(pixelIndices, 0, scan0, pixelIndices.Length);
            result.UnlockBits(data);

            return result;
        }
    }

    public class OctreeNode
    {
        private const int ChildCount = 8;
        private bool isLeaf;
        private int pixelCount;
        private int red;
        private int green;
        private int blue;
        private OctreeNode[] children;
        private int paletteIndex;
        private int level;

        public OctreeNode(int level, OctreeQuantizer quantizer)
        {
            this.level = level;
            isLeaf = (level == 8);
            if (isLeaf)
            {
                quantizer.IncrementLeafCount();
            }
            else
            {
                children = new OctreeNode[ChildCount];
                quantizer.AddLevelNode(level, this);
            }
        }

        public void AddColor(Color color, int currentLevel, OctreeQuantizer quantizer)
        {
            if (isLeaf)
            {
                pixelCount++;
                red += color.R;
                green += color.G;
                blue += color.B;
            }
            else
            {
                int index = GetColorIndex(color, currentLevel);
                if (children[index] == null)
                {
                    children[index] = new OctreeNode(currentLevel + 1, quantizer);
                }
                children[index].AddColor(color, currentLevel + 1, quantizer);
            }
        }

        public int Reduce()
        {
            int reducedLeaves = 0;
            int rSum = 0, gSum = 0, bSum = 0, countSum = 0;
            for (int i = 0; i < ChildCount; i++)
            {
                if (children[i] != null)
                {
                    rSum += children[i].red;
                    gSum += children[i].green;
                    bSum += children[i].blue;
                    countSum += children[i].pixelCount;
                    reducedLeaves++;
                    children[i] = null;
                }
            }
            isLeaf = true;
            red += rSum;
            green += gSum;
            blue += bSum;
            pixelCount += countSum;
            return reducedLeaves - 1;
        }

        public void ConstructPalette(ref Color[] palette, ref int index)
        {
            if (isLeaf)
            {
                paletteIndex = index;
                int r = (pixelCount == 0) ? 0 : (red / pixelCount);
                int g = (pixelCount == 0) ? 0 : (green / pixelCount);
                int b = (pixelCount == 0) ? 0 : (blue / pixelCount);
                palette[index++] = Color.FromArgb(r, g, b);
            }
            else
            {
                for (int i = 0; i < ChildCount; i++)
                {
                    if (children[i] != null)
                    {
                        children[i].ConstructPalette(ref palette, ref index);
                    }
                }
            }
        }

        public int GetPaletteIndex(Color color, int currentLevel)
        {
            if (isLeaf)
            {
                return paletteIndex;
            }
            int index = GetColorIndex(color, currentLevel);
            if (children[index] != null)
                return children[index].GetPaletteIndex(color, currentLevel + 1);
            else
            {
                for (int i = 0; i < ChildCount; i++)
                {
                    if (children[i] != null)
                        return children[i].GetPaletteIndex(color, currentLevel + 1);
                }
                return paletteIndex;
            }
        }

        private int GetColorIndex(Color color, int currentLevel)
        {
            int shift = 7 - currentLevel;
            int r = (color.R >> shift) & 1;
            int g = (color.G >> shift) & 1;
            int b = (color.B >> shift) & 1;
            return (r << 2) | (g << 1) | b;
        }
    }

}
