using CrashEdit.Crash;
using System.Text.RegularExpressions;
using static CrashEdit.CE.ModelConverterForm;

namespace CrashEdit.CE
{
    public static class TextureAtlasPacker
    {
        public struct TextureEntry
        {
            public int Index;
            public string Name;
            public string FilePath;
            public byte[] Data;
            public byte[] Palette;
            public int Bpp;
            public int Width;
            public int Height;
            public MaterialInfo Info;
        }

        private struct SkylineNode
        {
            public int X;
            public int Y;
            public int Width;
        }

        private static readonly Point AtlasSize = new()
        {
            X = 1024,
            Y = 128
        };

        private static readonly int SegmentWidth = 256;
        private static readonly int MaxClutHeight = 16;
        private static readonly int TextureStartY = 16;

        private static bool TryPlaceInSegment(
            Dictionary<int, List<SkylineNode>> skylines,
            int seg,
            int w, int h,
            int usableHeight,
            out int localX, out int localY)
        {
            localX = localY = 0;
            var skyline = skylines[seg];

            int bestY = int.MaxValue;
            int bestX = 0;
            int bestIndex = -1;

            for (int i = 0; i < skyline.Count; i++)
            {
                var n = skyline[i];
                if (w > n.Width) continue;

                int x = n.X;
                int y = n.Y;

                // check if it fits within the usable texture area vertically
                if (y + h > usableHeight) continue;

                if (y < bestY || (y == bestY && x < bestX))
                {
                    bestY = y;
                    bestX = x;
                    bestIndex = i;
                }
            }

            if (bestIndex == -1)
                return false;

            localX = bestX;
            localY = bestY;
            AddSkyline(skylines, seg, bestIndex, localX, localY + h, w);
            return true;
        }

        public static (List<TextureChunk>, List<PackedTexture>) AllocateTextureAtlas(List<TextureEntry> textures, string tpageName, bool debug)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("Packing textures into atlas...");
            //Console.WriteLine($"CLUT area: Y=0-{MaxClutHeight - 1}, Texture area: Y={TextureStartY}-{AtlasSize.Y - 1}");
            Console.ForegroundColor = ConsoleColor.White;

            List<int> sorted = [];
            for (int i = 0; i < textures.Count; i++)
                sorted.Add(i);

            sorted = sorted
                .OrderBy(i => textures[i].Info.AnimCount > 1)
                .ThenByDescending(i => textures[i].Width * textures[i].Height)
                .ToList();

            // Pass 1: Simulate texture placement to determine page assignments and CLUT requirements without actually modifying the skyline data structures or creating texture chunks yet
            if (debug)
                Console.WriteLine("=== Pass 1: Simulating texture placement ===");

            var pageCLUTRequirements = new Dictionary<int, (int _4bpp, int _8bpp)>();
            var texturePlacementPlan = new List<(int texIdx, int pageIdx, bool needsNewClut, int destX, int destY)>();
            var physicalTextureMap = new Dictionary<(int Index, int AnimOffset, string FilePath), (int texIdx, int pageIdx)>();
            var simulatedSkylines = new Dictionary<int, Dictionary<int, List<SkylineNode>>>();

            int currentPage = 0;
            pageCLUTRequirements[0] = (0, 0);

            void InitSimulatedSkyline(int pageIdx)
            {
                var skylines = new Dictionary<int, List<SkylineNode>>();
                for (int i = 0; i < AtlasSize.X / SegmentWidth; i++)
                {
                    skylines[i] = [new SkylineNode { X = 0, Y = 0, Width = SegmentWidth }];
                }
                simulatedSkylines[pageIdx] = skylines;
            }

            InitSimulatedSkyline(0);

            int usableTextureHeight = AtlasSize.Y - TextureStartY;

            foreach (int i in sorted)
            {
                var tex = textures[i];
                var info = tex.Info;

                if (info.AnimDelay > 0) continue;

                var physicalKey = (tex.Index, info.AnimOffset, tex.FilePath);

                if (physicalTextureMap.TryGetValue(physicalKey, out var existing))
                {
                    continue;
                }

                bool needsNewClut = !(info.AnimCount > 0 && info.AnimOffset < info.AnimCount - 1);
                bool assigned = false;
                int assignedDestX = 0, assignedDestY = 0;

                for (int pageIdx = 0; pageIdx <= currentPage && !assigned; pageIdx++)
                {
                    var (count4bpp, count8bpp) = pageCLUTRequirements[pageIdx];

                    int test4bpp = count4bpp;
                    int test8bpp = count8bpp;
                    if (needsNewClut)
                    {
                        if (tex.Bpp == 4) test4bpp++;
                        else if (tex.Bpp == 8) test8bpp++;
                    }

                    int clutYRow = Math.Max(1, (test4bpp + 15) / 16);
                    int clutHeight = clutYRow + test8bpp;

                    if (clutHeight > MaxClutHeight)
                    {
                        if (debug)
                            Console.WriteLine($"    Page {pageIdx}: CLUT height {clutHeight} exceeds max {MaxClutHeight}, skipping");
                        continue;
                    }

                    if (tex.Height > usableTextureHeight)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"    Texture '{tex.Name}' height {tex.Height} exceeds available texture area {usableTextureHeight}");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }

                    var skylines = simulatedSkylines[pageIdx];
                    bool canPlace = false;

                    for (int seg = 0; seg < AtlasSize.X / SegmentWidth && !canPlace; seg++)
                    {
                        if (TryPlaceInSegment(skylines, seg, tex.Width, tex.Height, usableTextureHeight, out int lx, out int ly))
                        {
                            int destX = seg * SegmentWidth + lx;
                            int destY = AtlasSize.Y - (ly + tex.Height);
                            int textureTopY = destY;
                            int textureBottomY = destY + tex.Height;

                            if (textureTopY >= TextureStartY && textureBottomY <= AtlasSize.Y)
                            {
                                canPlace = true;
                                assigned = true;
                                assignedDestX = destX;
                                assignedDestY = destY;

                                texturePlacementPlan.Add((i, pageIdx, needsNewClut, destX, destY));
                                physicalTextureMap[physicalKey] = (i, pageIdx);

                                if (needsNewClut)
                                {
                                    pageCLUTRequirements[pageIdx] = (test4bpp, test8bpp);
                                }

                                if (debug)
                                    Console.WriteLine($"    Texture '{tex.Name}' -> page {pageIdx} at ({destX},{destY}), size {tex.Width}x{tex.Height}, Y range: {textureTopY}-{textureBottomY - 1}");
                                break;
                            }
                        }
                    }
                }

                if (!assigned)
                {
                    currentPage++;
                    InitSimulatedSkyline(currentPage);

                    int count4bpp = 0;
                    int count8bpp = 0;

                    if (needsNewClut)
                    {
                        if (tex.Bpp == 4) count4bpp = 1;
                        else if (tex.Bpp == 8) count8bpp = 1;
                    }

                    int clutYRow = Math.Max(1, (count4bpp + 15) / 16);
                    int clutHeight = clutYRow + count8bpp;

                    if (clutHeight > MaxClutHeight)
                    {
                        throw new Exception($"Texture '{tex.Name}' requires CLUT height {clutHeight} which exceeds max {MaxClutHeight}");
                    }

                    pageCLUTRequirements[currentPage] = (count4bpp, count8bpp);

                    var skylines = simulatedSkylines[currentPage];
                    bool placed = false;

                    for (int seg = 0; seg < AtlasSize.X / SegmentWidth && !placed; seg++)
                    {
                        if (TryPlaceInSegment(skylines, seg, tex.Width, tex.Height, usableTextureHeight, out int lx, out int ly))
                        {
                            int destX = seg * SegmentWidth + lx;
                            int destY = AtlasSize.Y - (ly + tex.Height);
                            int textureTopY = destY;
                            int textureBottomY = destY + tex.Height;

                            if (textureTopY >= TextureStartY && textureBottomY <= AtlasSize.Y)
                            {
                                placed = true;
                                assignedDestX = destX;
                                assignedDestY = destY;

                                texturePlacementPlan.Add((i, currentPage, needsNewClut, destX, destY));
                                physicalTextureMap[physicalKey] = (i, currentPage);

                                if (debug)
                                    Console.WriteLine($"    Texture '{tex.Name}' -> NEW page {currentPage} at ({destX},{destY}), Y range: {textureTopY}-{textureBottomY - 1}");
                                break;
                            }
                        }
                    }

                    if (!placed)
                    {
                        throw new Exception($"Failed to place texture '{tex.FilePath}' during Pass 1 simulation.");
                    }
                }
            }

            if (debug)
            {
                Console.WriteLine();
                Console.WriteLine("=== Pass 1 Summary ===");
                foreach (var kvp in pageCLUTRequirements.OrderBy(k => k.Key))
                {
                    var (count4bpp, count8bpp) = kvp.Value;
                    int clutYRow = Math.Max(1, (count4bpp + 15) / 16);
                    int clutHeight = clutYRow + count8bpp;
                    int texCount = texturePlacementPlan.Count(t => t.pageIdx == kvp.Key);
                    Console.WriteLine($"    Page {kvp.Key}: {texCount} textures, 4bpp={count4bpp}, 8bpp={count8bpp}, CLUT area (Y: 0-{MaxClutHeight - 1}), texture area (Y: {TextureStartY}-{AtlasSize.Y - 1})");
                }
            }

            // Pass 2: Create actual texture chunks based on the placement plan
            if (debug)
            {
                Console.WriteLine();
                Console.WriteLine("=== Pass 2: Placing textures ===");
            }

            List<TextureChunk> tpages = [];
            List<PackedTexture> packedTextures = [];
            var pageClutInfo = new Dictionary<int, (int _4bppCount, int _8bppCount)>();
            var clutPositions = new Dictionary<int, (int curClutX, int curClutY, int oldClutX, int oldClutY)>();
            // number of 4bpp rows reserved per page (computed from Pass 1 results)
            var pageReserved4bppRows = new Dictionary<int, int>();
            var physicalTextureResults = new Dictionary<(int Index, int AnimOffset, string FilePath), PackedTexture>();

            for (int pageIdx = 0; pageIdx <= currentPage; pageIdx++)
            {
                string pageName = tpageName.Replace('_', Convert62(pageIdx + 1));
                int eid = Entry.ENameToEID(pageName);

                byte[] header = {
                    0x34, 0x12, 0x01, 0x00,
                    (byte)(eid & 0xFF), (byte)((eid >> 8) & 0xFF), (byte)((eid >> 16) & 0xFF), (byte)((eid >> 24) & 0xFF),
                    0x05, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00,
                };

                byte[] chunk = new byte[0x10000];
                Array.Copy(header, 0, chunk, 0, header.Length);
                tpages.Add(new TextureChunk(chunk));

                pageClutInfo[pageIdx] = (0, 0);
                clutPositions[pageIdx] = (1, 0, -1, -1);
                var (finalCount4bpp, finalCount8bpp) = pageCLUTRequirements[pageIdx];
                int finalClutYRow = Math.Max(1, (finalCount4bpp + 15) / 16);
                int finalClutHeight = finalClutYRow + finalCount8bpp;
                // record reserved 4bpp rows so 8bpp placements can start after them
                pageReserved4bppRows[pageIdx] = finalClutYRow;
                Console.WriteLine($"    Created page {pageIdx} ({pageName}): CLUT height={finalClutHeight} (Y: 0-{finalClutHeight - 1})");
            }

            foreach (int texIdx in sorted)
            {
                var tex = textures[texIdx];
                var info = tex.Info;

                if (info.AnimDelay > 0) continue; // later handle delayed animated textures 

                var physicalKey = (tex.Index, info.AnimOffset, tex.FilePath);

                // if this physical texture (index + anim offset + file path) has already been placed, reuse the same placement
                if (physicalTextureResults.TryGetValue(physicalKey, out var existingPacked))
                {
                    for (int r = 0; r < info.AnimRepeat; r++)
                    {
                        packedTextures.Add(existingPacked);
                    }
                    if (debug)
                        Console.WriteLine($"    Reused '{tex.Name}' (AnimOffset={info.AnimOffset}) × {info.AnimRepeat}");
                    continue;
                }

                // find the placement for this texture from the plan created in Pass 1
                var placement = texturePlacementPlan.FirstOrDefault(p => p.texIdx == texIdx);
                if (placement == default)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"    [Error] No placement found for texture '{tex.Name}' (index {texIdx})");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                int pageIdx = placement.pageIdx;
                bool needsNewClut = placement.needsNewClut;
                int destX = placement.destX;
                int destY = placement.destY;

                var (curClutX, curClutY, oldClutX, oldClutY) = clutPositions[pageIdx];
                int actualClutX = curClutX;
                int actualClutY = curClutY;

                var pageInfo = pageClutInfo[pageIdx];
                int reserved4bppRows = pageReserved4bppRows.ContainsKey(pageIdx) ? pageReserved4bppRows[pageIdx] : Math.Max(1, (pageInfo._4bppCount + 15) / 16);

                if (tex.Bpp == 8)
                {
                    // ensure we place 8bpp CLUTs after all reserved 4bpp rows
                    if (oldClutX == -1 || oldClutY == -1)
                    {
                        oldClutX = curClutX;
                        oldClutY = curClutY;
                    }
                    actualClutX = 0;
                    actualClutY = reserved4bppRows + pageInfo._8bppCount;

                    if (actualClutY >= MaxClutHeight)
                    {
                        throw new Exception($"CLUT Y position {actualClutY} exceeds max {MaxClutHeight - 1} for texture '{tex.Name}'");
                    }
                }
                else // 4bpp
                {
                    // prefer to reuse old CLUT position if available
                    if (oldClutX != -1 && oldClutY != -1)
                    {
                        actualClutX = oldClutX;
                        actualClutY = oldClutY;
                        oldClutX = -1;
                        oldClutY = -1;
                    }

                    if (actualClutY == 0 && actualClutX == 0)
                    {
                        actualClutX = 1;
                    }

                    // ensure 4bpp stays within its reserved rows
                    if (actualClutY >= reserved4bppRows)
                    {
                        throw new Exception($"CLUT Y position {actualClutY} exceeds reserved 4bpp rows ({reserved4bppRows - 1}) for texture '{tex.Name}'");
                    }
                }

                int width = tex.Bpp == 8 ? tex.Width / 2 : tex.Width;
                int height = tex.Height;

                tpages[pageIdx].Data = TextureConv.ReplaceTextureFromViewer(
                    tpages[pageIdx].Data,
                    tex.Data,
                    tex.Palette,
                    width,
                    height,
                    destX,
                    destY,
                    needsNewClut,
                    tex.Bpp,
                    actualClutX,
                    actualClutY
                );

                var packedTex = new PackedTexture(
                    index: tex.Index,
                    name: tex.Name,
                    filePath: tex.FilePath,
                    bpp: tex.Bpp,
                    clutX: actualClutX,
                    clutY: actualClutY,
                    destX: tex.Bpp == 8 ? destX / 2 : destX,
                    destY: destY,
                    w: width,
                    h: height,
                    tpage: pageIdx,
                    info: info
                );

                physicalTextureResults[physicalKey] = packedTex;

                for (int r = 0; r < info.AnimRepeat; r++)
                {
                    packedTextures.Add(packedTex);
                }

                int textureTopY = destY;
                int textureBottomY = destY + tex.Height;
                if (debug)
                    Console.WriteLine($"    Placed '{tex.Name}' (AnimOffset={info.AnimOffset}) at ({destX},{destY}), size {tex.Width}x{tex.Height}, Y range: {textureTopY}-{textureBottomY - 1}, CLUT({actualClutX},{actualClutY}) × {info.AnimRepeat}");

                if (needsNewClut)
                {
                    if (tex.Bpp == 8)
                    {
                        // 8bpp uses its own rows after reserved 4bpp rows
                        // do not modify the 4bpp curClutX/curClutY here to avoid overlap with 4bpp placement
                    }
                    else // 4bpp
                    {
                        curClutX = actualClutX + 1;

                        if (curClutY == 0 && curClutX == 0)
                        {
                            curClutX = 1;
                        }

                        if (curClutX > 15)
                        {
                            curClutX = 0;
                            curClutY += 1;

                            if (curClutY == 0)
                            {
                                curClutX = 1;
                            }
                        }
                    }

                    clutPositions[pageIdx] = (curClutX, curClutY, oldClutX, oldClutY);

                    // finally, update the page CLUT counters
                    if (tex.Bpp == 8)
                    {
                        pageInfo._8bppCount++;
                    }
                    else
                    {
                        pageInfo._4bppCount++;
                    }
                    pageClutInfo[pageIdx] = pageInfo;
                }
            }

            // handle delayed animation textures by finding their base texture placements

            if (debug)
            {
                Console.WriteLine();
                Console.WriteLine("Processing delayed animation textures...");
            }
            foreach (int i in sorted)
            {
                var tex = textures[i];
                var info = tex.Info;

                if (info.AnimDelay > 0)
                {
                    string baseName = Regex.Replace(tex.Name, @"_[d]\d+", "");
                    bool found = false;

                    foreach (var pt in packedTextures)
                    {
                        if (pt.Name == baseName && pt.Info.AnimOffset == info.AnimOffset)
                        {
                            packedTextures.Add(new PackedTexture(
                                index: tex.Index,
                                name: tex.Name,
                                filePath: pt.FilePath,
                                bpp: pt.Bpp,
                                clutX: pt.ClutX,
                                clutY: pt.ClutY,
                                destX: pt.DestX,
                                destY: pt.DestY,
                                w: pt.Width,
                                h: pt.Height,
                                tpage: pt.TPage,
                                info: info
                            ));
                            found = true;
                            if (debug)
                                Console.WriteLine($"    Delayed '{tex.Name}' reuses '{pt.Name}' (offset={info.AnimOffset})");
                            break;
                        }
                    }

                    if (!found)
                    {
                        foreach (var pt in packedTextures)
                        {
                            string ptBaseName = Regex.Replace(pt.Name, @"_[d]\d+", "");
                            if (ptBaseName == baseName &&
                                pt.FilePath == tex.FilePath &&
                                pt.Info.AnimCount > 0)
                            {
                                var targetPacked = packedTextures
                                    .Where(p => Regex.Replace(p.Name, @"_[d]\d+", "") == baseName &&
                                               p.FilePath == tex.FilePath &&
                                               p.Info.AnimOffset == info.AnimOffset)
                                    .FirstOrDefault();

                                if (targetPacked.Equals(default(PackedTexture)))
                                {
                                    targetPacked = pt;
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine($"    Warning: Could not find frame at offset {info.AnimOffset} for delayed '{tex.Name}', using first frame");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }

                                packedTextures.Add(new PackedTexture(
                                    index: tex.Index,
                                    name: tex.Name,
                                    filePath: targetPacked.FilePath,
                                    bpp: targetPacked.Bpp,
                                    clutX: targetPacked.ClutX,
                                    clutY: targetPacked.ClutY,
                                    destX: targetPacked.DestX,
                                    destY: targetPacked.DestY,
                                    w: targetPacked.Width,
                                    h: targetPacked.Height,
                                    tpage: targetPacked.TPage,
                                    info: info
                                ));
                                found = true;
                                if (debug)
                                    Console.WriteLine($"    Delayed '{tex.Name}' reuses '{targetPacked.Name}'");
                                break;
                            }
                        }
                    }

                    if (!found)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"    [Error] Could not find base texture for delayed animation '{tex.Name}' (base='{baseName}', offset={info.AnimOffset})");
                        Console.ForegroundColor = ConsoleColor.White;
                        throw new Exception($"Could not find base texture for delayed animation '{tex.FilePath}'.");
                    }
                }
            }

            foreach (var tpage in tpages)
            {
                int checksum = Chunk.CalculateChecksum(tpage.Data);
                BitConv.ToInt32(tpage.Data, 12, checksum);
            }

            Console.WriteLine();
            Console.WriteLine($"=== Texture packing complete: {tpages.Count} pages, {packedTextures.Count} packed textures ===");

            return (tpages, packedTextures);
        }

        private static void AddSkyline(Dictionary<int, List<SkylineNode>> skylines, int seg, int index, int x, int y, int width)
        {
            var skyline = skylines[seg];
            var node = skyline[index];

            skyline[index] = new SkylineNode
            {
                X = node.X + width,
                Y = node.Y,
                Width = node.Width - width
            };

            skyline.Insert(index, new SkylineNode
            {
                X = x,
                Y = y,
                Width = width
            });

            skyline.RemoveAll(n => n.Width <= 0);

            for (int i = 0; i < skyline.Count - 1; i++)
            {
                var a = skyline[i];
                var b = skyline[i + 1];
                if (a.Y == b.Y && a.X + a.Width == b.X)
                {
                    skyline[i] = new SkylineNode
                    {
                        X = a.X,
                        Y = a.Y,
                        Width = a.Width + b.Width
                    };
                    skyline.RemoveAt(i + 1);
                    i--;
                }
            }
        }
    }
}
