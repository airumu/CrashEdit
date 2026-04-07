using CrashEdit.Crash;
using OpenTK.Mathematics;
using System.Media;
using System.Text.Json;
using System.Text.RegularExpressions;
using static CrashEdit.CE.TriangleStripBuilder;
using static CrashEdit.CE.TextureAtlasPacker;

namespace CrashEdit.CE
{
    public static class BaseScales
    {
        public const float ModelScaleFactor = 255.0f;
        public const float ModelScale = 0x646;

        public const float CollisionScaleFactor = 127.0f;
        public const float CollisionScale = 0xC700;
    }

    public class Tri
    {
        public int v0, v1, v2;
        public bool used = false;
        public List<int> adj = []; // adjacent tris
        public int degree;
    }

    public readonly struct TriangleKey(int material, byte[] uv0, byte[] uv1, byte[] uv2)
    {
        public readonly int Material = material;
        public readonly byte U0 = uv0[0], V0 = uv0[1];
        public readonly byte U1 = uv1[0], V1 = uv1[1];
        public readonly byte U2 = uv2[0], V2 = uv2[1];
    }

    public readonly struct StructureInfo(byte textureIndex, byte faceOrientation, bool isCC)
    {
        public readonly byte TextureIndex = textureIndex;
        public readonly byte FaceOrientation = faceOrientation;
        public readonly bool IsCC = isCC;
    }

    public readonly struct PackedTexture(int index, string name, string filePath, int bpp, int clutX, int clutY, int destX, int destY, int w, int h, int tpage, MaterialInfo info)
    {
        public readonly int Index = index;
        public readonly string Name = name;
        public readonly string FilePath = filePath;
        public readonly int Bpp = bpp;
        public readonly int ClutX = clutX, ClutY = clutY;
        public readonly int DestX = destX, DestY = destY;
        public readonly int Width = w;
        public readonly int Height = h;
        public readonly int TPage = tpage;
        public readonly MaterialInfo Info = info;
    }

    public readonly struct MaterialInfo(int face, int blend, int offset, int count, int speed, int delay, int repeat, int repeats)
    {
        public readonly int FaceOrientation = face;
        public readonly int BlendMode = blend;
        public readonly int AnimOffset = offset; // split texture offset
        public readonly int AnimCount = count;
        public readonly int AnimSpeed = speed;
        public readonly int AnimDelay = delay;
        public readonly int AnimRepeat = repeat;
        public readonly int TotalAnimRepeats = repeats;
    }

    public readonly struct ModelMaterial(string name, MaterialInfo info, int aniTexIdx, List<ModelTexture> texture)
    {
        public readonly string Name = name;
        public readonly MaterialInfo Info = info;
        public readonly int AnimatedTextureIndex = aniTexIdx;
        public readonly List<ModelTexture> Texture = texture;
    }

    public static class TriangleStripBuilder
    {
        public static (int, int) Edge(int x, int y)
                   => x < y ? (x, y) : (y, x);

        static int FindBestNextTri(List<Tri> tris, int currentTriIndex, int v0, int v1)
        {
            // find adjacent triangle that shares the edge (v0, v1)
            foreach (var adjIdx in tris[currentTriIndex].adj)
            {
                var t = tris[adjIdx];
                if (t.used) continue;

                int match = 0;
                if (t.v0 == v0 || t.v0 == v1) match++;
                if (t.v1 == v0 || t.v1 == v1) match++;
                if (t.v2 == v0 || t.v2 == v1) match++;

                if (match >= 2)
                    return adjIdx; // found
            }

            return -1;
        }

        private static List<int> BuildStrip(List<Tri> tris, int start)
        {
            tris[start].used = true;
            var t0 = tris[start];

            var strip = new List<int> { t0.v0, t0.v1, t0.v2 };

            Grow(tris, strip, forward: true, start);
            Grow(tris, strip, forward: false, start);

            return strip;
        }

        public static List<List<int>> BuildAllStripsBestOfAttempts(List<Tri> trisTemplate, ModelSettings settings, bool output)
        {
            var bestStrips = new List<List<int>>();
            int bestScore = int.MaxValue;

            if (output)
                Console.WriteLine();

            BuildAdjacency(trisTemplate, output);
            UnifyWinding(trisTemplate);

            // strategy 1: degree descending
            {
                var tris = CopyTris(trisTemplate);
                var strips = BuildAllStripsWithStrategy(tris, settings, 1);
                int score = EvaluateStrips(strips, settings);
                if (score < bestScore)
                {
                    bestScore = score;
                    bestStrips = strips;
                }
                if (output)
                    Console.WriteLine($"    Strategy 1: {strips.Count} strips, score: {score}");
            }

            // strategy 2: degree ascending
            {
                var tris = CopyTris(trisTemplate);
                var strips = BuildAllStripsWithStrategy(tris, settings, 2);
                int score = EvaluateStrips(strips, settings);
                if (score < bestScore)
                {
                    bestScore = score;
                    bestStrips = strips;
                }
                if (output)
                    Console.WriteLine($"    Strategy 2: {strips.Count} strips, score: {score}");
            }

            //// strategy 3: random
            //{
            //    var random = new Random(123456);
            //    Console.WriteLine($"Strategy 3: Random attempts (seed: {random.GetHashCode()}); first 10 attempts:");
            //    for (int iteration = 0; iteration < settings.MaxIterations; iteration++)
            //    {
            //        var tris = CopyTris(trisTemplate);
            //        var strips = BuildAllStripsWithStrategy(tris, settings, 3, random);
            //        int score = EvaluateStrips(strips, settings);
            //        if (score < bestScore)
            //        {
            //            bestScore = score;
            //            bestStrips = strips;
            //        }
            //        if (iteration < 10)
            //            Console.WriteLine($"    Attempt {iteration}: {strips.Count} strips, score: {score}");
            //    }
            //}

            if (output)
                Console.WriteLine($"    [Strip Generation] Best: {bestStrips.Count} strips with score {bestScore}");
            return bestStrips;
        }

        private static int EvaluateStrips(List<List<int>> strips, ModelSettings settings)
        {
            var currentLive = new HashSet<int>();
            var globalRemaining = new Dictionary<int, int>();

            foreach (var s in strips)
                foreach (var v in s)
                    globalRemaining[v] = globalRemaining.TryGetValue(v, out int cnt) ? cnt + 1 : 1;

            int maxLive = 0;
            int totalLive = 0;
            int sampleCount = 0;

            foreach (var strip in strips)
            {
                foreach (var v in strip)
                {
                    if (!currentLive.Contains(v))
                        currentLive.Add(v);
                    globalRemaining[v]--;
                    if (globalRemaining[v] == 0)
                        currentLive.Remove(v);

                    totalLive += currentLive.Count;
                    sampleCount++;
                    if (currentLive.Count > maxLive)
                        maxLive = currentLive.Count;
                }
            }

            int avgLive = sampleCount > 0 ? totalLive / sampleCount : 0;

            int score = (int)(maxLive * settings.MaxKeysPenalty
                      + avgLive * settings.AvgKeysPenalty
                      + strips.Count * settings.StripCountPenalty);

            return score;
        }

        public static List<List<int>> ReorderStripsGreedy(IList<List<int>> strips, int keyOffset, bool output)
        {
            if (strips == null) return new List<List<int>>();
            var remaining = new List<List<int>>(strips);
            var result = new List<List<int>>(strips.Count);

            var keyMap = new Dictionary<int, int>();
            var freeKeys = new SortedSet<int>();
            int nextKey = keyOffset;
            int currentMaxKey = keyOffset - 1;

            var globalRemaining = new Dictionary<int, int>();
            foreach (var s in strips)
                foreach (var v in s)
                    globalRemaining[v] = globalRemaining.TryGetValue(v, out var cnt) ? cnt + 1 : 1;

            var vertexRemaining = new Dictionary<int, int>(globalRemaining);

            int GetOrCreateKeySimulated(int vertexIndex)
            {
                if (keyMap.TryGetValue(vertexIndex, out int existingKey))
                {
                    vertexRemaining[vertexIndex]--;
                    if (vertexRemaining[vertexIndex] == 0)
                    {
                        keyMap.Remove(vertexIndex);
                        freeKeys.Add(existingKey);
                    }
                    return existingKey;
                }

                int assigned;
                if (freeKeys.Count > 0)
                {
                    assigned = freeKeys.Min;
                    freeKeys.Remove(assigned);
                }
                else
                {
                    while (nextKey == ModelTriangle.NullPtr) nextKey++;
                    assigned = nextKey++;
                }

                keyMap[vertexIndex] = assigned;

                if (assigned > currentMaxKey)
                    currentMaxKey = assigned;

                vertexRemaining[vertexIndex]--;
                if (vertexRemaining[vertexIndex] == 0)
                {
                    keyMap.Remove(vertexIndex);
                    freeKeys.Add(assigned);
                }

                return assigned;
            }

            while (remaining.Count > 0)
            {
                int bestIdx = -1;
                int bestPeakKey = int.MaxValue;
                int bestNew = int.MaxValue;
                int bestReuse = -1;
                int bestScoreLen = -1;

                for (int i = 0; i < remaining.Count; i++)
                {
                    var s = remaining[i];
                    int newVerts = 0;
                    int reusedVerts = 0;
                    var seen = new HashSet<int>();

                    var tempKeyMap = new Dictionary<int, int>(keyMap);
                    var tempFreeKeys = new SortedSet<int>(freeKeys);
                    var tempVertexRemaining = new Dictionary<int, int>(vertexRemaining);
                    int tempNextKey = nextKey;
                    int peakKey = currentMaxKey;

                    foreach (var v in s)
                    {
                        if (seen.Add(v))
                        {
                            if (!keyMap.ContainsKey(v))
                                newVerts++;
                            else
                                reusedVerts++;

                            int assignedKey;
                            if (tempKeyMap.TryGetValue(v, out int existingKey))
                            {
                                assignedKey = existingKey;
                                tempVertexRemaining[v]--;
                                if (tempVertexRemaining[v] == 0)
                                {
                                    tempKeyMap.Remove(v);
                                    tempFreeKeys.Add(existingKey);
                                }
                            }
                            else
                            {
                                if (tempFreeKeys.Count > 0)
                                {
                                    assignedKey = tempFreeKeys.Min;
                                    tempFreeKeys.Remove(assignedKey);
                                }
                                else
                                {
                                    while (tempNextKey == ModelTriangle.NullPtr) tempNextKey++;
                                    assignedKey = tempNextKey++;
                                }

                                tempKeyMap[v] = assignedKey;

                                if (assignedKey > peakKey)
                                    peakKey = assignedKey;

                                tempVertexRemaining[v]--;
                                if (tempVertexRemaining[v] == 0)
                                {
                                    tempKeyMap.Remove(v);
                                    tempFreeKeys.Add(assignedKey);
                                }
                            }
                        }
                    }

                    bool withinLimit = peakKey < ModelTriangle.NullPtr;
                    bool currentBestWithinLimit = bestPeakKey < ModelTriangle.NullPtr;

                    bool isBetter = false;

                    if (withinLimit && !currentBestWithinLimit)
                    {
                        isBetter = true;
                    }
                    else if (withinLimit == currentBestWithinLimit)
                    {
                        if (peakKey < bestPeakKey ||
                            (peakKey == bestPeakKey && newVerts < bestNew) ||
                            (peakKey == bestPeakKey && newVerts == bestNew && reusedVerts > bestReuse) ||
                            (peakKey == bestPeakKey && newVerts == bestNew && reusedVerts == bestReuse && s.Count > bestScoreLen))
                        {
                            isBetter = true;
                        }
                    }

                    if (isBetter)
                    {
                        bestPeakKey = peakKey;
                        bestNew = newVerts;
                        bestReuse = reusedVerts;
                        bestIdx = i;
                        bestScoreLen = s.Count;
                    }
                }

                var pick = remaining[bestIdx];
                remaining.RemoveAt(bestIdx);
                result.Add(pick);

                foreach (var v in pick)
                {
                    GetOrCreateKeySimulated(v);
                }
            }

            //if (output)
            //    Console.WriteLine($"[Reorder] Peak key number: {currentMaxKey} (offset: {keyOffset}, max allowed: {ModelTriangle.NullPtr - 1})");

            return result;
        }

        private static List<Tri> CopyTris(List<Tri> trisTemplate)
        {
            var tris = new List<Tri>(trisTemplate.Count);
            for (int i = 0; i < trisTemplate.Count; i++)
            {
                var t = trisTemplate[i];
                tris.Add(new Tri
                {
                    v0 = t.v0,
                    v1 = t.v1,
                    v2 = t.v2,
                    used = false,
                    adj = new List<int>(t.adj),
                    degree = t.degree
                });
            }
            return tris;
        }

        private static List<List<int>> BuildAllStripsWithStrategy(List<Tri> tris, ModelSettings settings, int strategy, Random random = null)
        {
            var strips = new List<List<int>>();

            List<int> order;
            switch (strategy)
            {
                case 1: // boundary priority, high degree first
                    order = tris
                        .Select((t, i) => new { Index = i, IsBoundary = t.adj.Count < 3, Degree = t.adj.Count })
                        .OrderByDescending(x => x.IsBoundary)
                        .ThenByDescending(x => x.Degree)
                        .Select(x => x.Index)
                        .ToList();
                    break;

                case 2: // boundary priority, low degree first
                    order = tris
                        .Select((t, i) => new { Index = i, IsBoundary = t.adj.Count < 3, Degree = t.adj.Count })
                        .OrderByDescending(x => x.IsBoundary)
                        .ThenBy(x => x.Degree)
                        .Select(x => x.Index)
                        .ToList();
                    break;

                case 3: // smart random - randomize within degree groups
                    var grouped = tris
                        .Select((t, i) => new { Index = i, IsBoundary = t.adj.Count < 3, Degree = t.adj.Count })
                        .GroupBy(x => x.Degree)
                        .OrderByDescending(g => g.Key) // high degree first
                        .ToList();

                    order = [];
                    foreach (var group in grouped)
                    {
                        var shuffled = group.OrderBy(x => random.Next()).Select(x => x.Index).ToList();
                        order.AddRange(shuffled);
                    }
                    break;

                default:
                    order = Enumerable.Range(0, tris.Count).ToList();
                    break;
            }

            foreach (var idx in order)
            {
                if (tris[idx].used) continue;
                var strip = BuildStrip(tris, idx);
                if (strip.Count >= 3)
                {
                    strips.Add(strip);
                }
            }

            // remaining tris (should not happen)
            for (int i = 0; i < tris.Count; i++)
            {
                if (!tris[i].used)
                {
                    var strip = new List<int> { tris[i].v0, tris[i].v1, tris[i].v2 };
                    tris[i].used = true;
                    strips.Add(strip);
                }
            }

            return strips;
        }

        private static void Grow(List<Tri> tris, List<int> strip, bool forward, int currentTriIndex)
        {
            while (true)
            {
                int v0, v1;
                if (forward)
                {
                    v0 = strip[^2];
                    v1 = strip[^1];
                }
                else
                {
                    v0 = strip[1];
                    v1 = strip[0];
                }

                int next = FindBestNextTri(tris, currentTriIndex, v0, v1);
                if (next == -1) break;

                var nt = tris[next];
                var (a, b, c) = OrientToEdge(nt, v0, v1);

                // add vertex c to the strip if edge direction matches,
                // end the strip if direction does not match (do not use degenerate triangle)
                if (forward)
                {

                    if (a == v0 && b == v1)
                        strip.Add(c);
                    else
                        break;
                }
                else
                {
                    if (a == v1 && b == v0)
                        strip.Insert(0, c);
                    else
                        break;
                }

                nt.used = true;
                currentTriIndex = next;
            }
        }

        private static void BuildAdjacency(List<Tri> tris, bool output)
        {
            foreach (var t in tris)
            {
                t.adj.Clear();
            }

            var map = new Dictionary<(int, int), List<int>>();

            for (int i = 0; i < tris.Count; i++)
            {
                var t = tris[i];
                foreach (var e in new[] { Edge(t.v0, t.v1), Edge(t.v1, t.v2), Edge(t.v2, t.v0) })
                {
                    if (!map.TryGetValue(e, out var list))
                        map[e] = list = [];
                    list.Add(i);
                }
            }

            foreach (var kv in map)
            {
                var list = kv.Value;
                if (list.Count == 2)
                {
                    tris[list[0]].adj.Add(list[1]);
                    tris[list[1]].adj.Add(list[0]);
                }
            }

            foreach (var t in tris)
                t.degree = t.adj.Count;

            int sharedEdges = map.Count(kv => kv.Value.Count == 2);
            int isolated = tris.Count(t => t.adj.Count == 0);
            if (output)
                Console.WriteLine($"    [Adjacency] Shared edges: {sharedEdges}, Isolated tris: {isolated}");
        }

        private static (int a, int b, int c) OrientToEdge(Tri t, int e0, int e1)
        {
            int[] v = { t.v0, t.v1, t.v2 };
            for (int i = 0; i < 3; i++)
            {
                int x = v[i], y = v[(i + 1) % 3], z = v[(i + 2) % 3];
                if (x == e0 && y == e1) return (x, y, z);
                if (x == e1 && y == e0) return (y, x, z);
            }
            return (t.v0, t.v1, t.v2);
        }

        private static void UnifyWinding(List<Tri> tris)
        {
            var visited = new bool[tris.Count];
            var stack = new Stack<int>();
            stack.Push(0);
            visited[0] = true;

            while (stack.Count > 0)
            {
                int i = stack.Pop();
                var t = tris[i];

                foreach (var j in t.adj)
                {
                    if (visited[j]) continue;

                    if (!SameEdgeDirection(t, tris[j]))
                    {
                        Swap(tris[j]); // swap v1 and v2
                    }

                    visited[j] = true;
                    stack.Push(j);
                }
            }
        }

        private static bool SameEdgeDirection(Tri a, Tri b)
        {
            int[] av = { a.v0, a.v1, a.v2 };
            int[] bv = { b.v0, b.v1, b.v2 };

            // look for each edge of a
            for (int i = 0; i < 3; i++)
            {
                int a0 = av[i];
                int a1 = av[(i + 1) % 3];

                // whether b has the same edge
                for (int j = 0; j < 3; j++)
                {
                    int b0 = bv[j];
                    int b1 = bv[(j + 1) % 3];

                    if (a0 == b0 && a1 == b1)
                        return true;   // same direction (abnormal)

                    if (a0 == b1 && a1 == b0)
                        return false;  // reverse direction (normal)
                }
            }

            return false; // no shared edges (usually not)
        }

        private static void Swap(Tri t)
        {
            (t.v1, t.v2) = (t.v2, t.v1);
        }

        public static void RemapJsonToOutputOrder(C2Json json, Dictionary<int, int> originalVertexToOutputIndex)
        {
            int oldCount = json.vertices.Count;
            int assignedCount = originalVertexToOutputIndex.Count;

            int[] oldToNew = Enumerable.Repeat(-1, oldCount).ToArray();

            foreach (var kv in originalVertexToOutputIndex)
                oldToNew[kv.Key] = kv.Value;

            // assign unassigned old vertices sequentially to the end
            int nextIdx = assignedCount;
            for (int i = 0; i < oldCount; i++)
            {
                if (oldToNew[i] == -1)
                    oldToNew[i] = nextIdx++;
            }

            int newCount = nextIdx;

            // reconstruct the vertex array
            var newVertices = new List<float[]>(newCount);
            for (int i = 0; i < newCount; i++) newVertices.Add(null!);
            for (int old = 0; old < oldCount; old++)
                newVertices[oldToNew[old]] = json.vertices[old];
            json.vertices = newVertices;

            // if colors exist for each vertex, sort them
            if (json.colors != null && json.colors.Count == oldCount)
            {
                var newColors = new List<int[]>(newCount);
                for (int i = 0; i < newCount; i++) newColors.Add(null!);
                for (int old = 0; old < oldCount; old++)
                    newColors[oldToNew[old]] = json.colors[old];
                json.colors = newColors;
            }

            // frames: sort the vertex lists within each frame
            if (json.frames != null)
            {
                for (int fi = 0; fi < json.frames.Count; fi++)
                {
                    var oldFrame = json.frames[fi];
                    if (oldFrame == null) continue;
                    var newFrame = new List<float[]>(newCount);
                    for (int i = 0; i < newCount; i++) newFrame.Add(null!);
                    for (int old = 0; old < oldFrame.Count; old++)
                        newFrame[oldToNew[old]] = oldFrame[old];
                    json.frames[fi] = newFrame;
                }
            }

            // triangles: replace the indexes from old to new
            foreach (var tri in json.triangles)
            {
                for (int k = 0; k < tri.v.Length; k++)
                {
                    int oldIdx = tri.v[k];
                    tri.v[k] = oldToNew[oldIdx];
                }
            }
        }
    }

    public static class ModelConverter
    {
        public static List<C2Json> LoadModelJson(string path)
        {
            WaitForStableFile(path);

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    using var fs = new FileStream(
                        path,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.ReadWrite);

                    using var sr = new StreamReader(fs);
                    string json = sr.ReadToEnd();

                    return JsonSerializer.Deserialize<List<C2Json>>(json)!;
                }
                catch (IOException)
                {
                    Thread.Sleep(100);
                }
                catch (JsonException)
                {
                    Thread.Sleep(100);
                }
            }

            throw new Exception("JSON did not stabilize.");
        }

        private static void WaitForStableFile(string path)
        {
            long lastSize = -1;
            int stableCount = 0;

            while (stableCount < 5)
            {
                long size = new FileInfo(path).Length;

                if (size == lastSize)
                    stableCount++;
                else
                    stableCount = 0;

                lastSize = size;
                Thread.Sleep(100);
            }
        }

        //
        // model
        //
        private static (uint[], Dictionary<TriangleKey, StructureInfo>, int) BuildPolyDataFromStrip
            (C2Json json, Dictionary<SceneryColor, int> colors, Dictionary<TriangleKey, ModelMaterial> materials, bool compressed, int spVcount, ModelSettings settings, Debug debug, bool skipOutput)
        {
            bool output = debug.DebugModels && !skipOutput;

            //if (output)
            //{
            //    Console.WriteLine();
            //    Console.WriteLine("    [Strips]");
            //}

            // TODO: verify
            byte header = (byte)colors.Count;
            int keyOffset = (header + 1) / 2;
            int maxAllowedKeys = ModelTriangle.NullPtr - keyOffset;

            // build initial tris
            List<Tri> tris = [];
            for (int i = 0; i < json.triangles.Count; i++)
            {
                var t = json.triangles[i];
                tris.Add(new Tri { v0 = t.v[0], v1 = t.v[1], v2 = t.v[2] });
            }

            // tris index map (tris -> json)
            Dictionary<(int, int, int), int> triMap = [];
            for (int i = 0; i < json.triangles.Count; i++)
            {
                var t = json.triangles[i];
                int a = t.v[0],
                    b = t.v[1],
                    c = t.v[2];
                var key = (a, b, c);
                if (!triMap.ContainsKey(key))
                    triMap[key] = i;
            }

            var strips = BuildAllStripsBestOfAttempts(tris, settings, false);
            strips = ReorderStripsGreedy(strips, keyOffset, false);

            //if (debug)
            //{
            //    for (int i = 0; i < strips.Count; i++)
            //        Console.WriteLine($"Strip [{i}]: {string.Join(", ", strips[i])}");
            //    Console.WriteLine();

            //    var edgeUse = new Dictionary<(int, int), int>();
            //    foreach (var t in tris)
            //    {
            //        foreach (var e in new[] { Edge(t.v0, t.v1), Edge(t.v1, t.v2), Edge(t.v2, t.v0) })
            //        {
            //            edgeUse.TryAdd(e, 0);
            //            edgeUse[e]++;
            //        }
            //    }
            //    int border = edgeUse.Count(e => e.Value == 1);
            //    int manifold = edgeUse.Count(e => e.Value == 2);
            //    int broken = edgeUse.Count(e => e.Value > 2);
            //    Console.WriteLine($"border:{border}  manifold:{manifold}  broken:{broken}");
            //}

            // pre-pass: determine originalVertexToOutputIndex (performed before creating ModelTriangle)
            Dictionary<int, int> keyMapTemp = [];
            byte nextKeyTemp = 0;
            int GetOrCreateKeyTemp(int vertexIndex, out ModelTriangle.IndexType idxType)
            {
                if (keyMapTemp.TryGetValue(vertexIndex, out int existing))
                {
                    idxType = ModelTriangle.IndexType.Duplicate;
                    return existing;
                }
                while (nextKeyTemp == ModelTriangle.NullPtr) nextKeyTemp++;
                int assigned = nextKeyTemp++;
                keyMapTemp[vertexIndex] = assigned;
                idxType = ModelTriangle.IndexType.Original;
                return assigned;
            }

            Dictionary<int, int> originalVertexToOutputIndex = [];
            int nextOutputIndex = 0;

            // scan strips first to determine the “first appearance order”
            foreach (var strip in strips)
            {
                for (int j = 0; j < strip.Count; j++)
                {
                    int v = strip[j];
                    var _ = GetOrCreateKeyTemp(v, out ModelTriangle.IndexType idt);
                    if (idt == ModelTriangle.IndexType.Original)
                    {
                        if (!originalVertexToOutputIndex.ContainsKey(v))
                            originalVertexToOutputIndex[v] = nextOutputIndex++;
                    }
                }
            }

            // sort JSON by output order (destructive)
            RemapJsonToOutputOrder(json, originalVertexToOutputIndex);

            // rebuild tris/trimap/strips after sorting (with new indexes)
            tris.Clear();
            for (int i = 0; i < json.triangles.Count; i++)
            {
                var t = json.triangles[i];
                tris.Add(new Tri { v0 = t.v[0], v1 = t.v[1], v2 = t.v[2] });
            }

            triMap.Clear();
            for (int i = 0; i < json.triangles.Count; i++)
            {
                var t = json.triangles[i];
                int[] verts = [t.v[0], t.v[1], t.v[2]];
                Array.Sort(verts);
                var key = (verts[0], verts[1], verts[2]);
                if (!triMap.ContainsKey(key))
                    triMap[key] = i;
            }

            strips = BuildAllStripsBestOfAttempts(tris, settings, output);
            strips = ReorderStripsGreedy(strips, keyOffset, true);
            //if (debug)
            //{
            //    Console.WriteLine();
            //    Console.WriteLine("[Remapped Strips]");
            //    for (int i = 0; i < strips.Count; i++)
            //        Console.WriteLine($"Strip [{i}]: {string.Join(", ", strips[i])}");
            //}

            var vertexRemaining = new Dictionary<int, int>();
            foreach (var strip in strips)
            {
                foreach (var v in strip)
                {
                    if (!vertexRemaining.TryGetValue(v, out int cnt)) cnt = 0;
                    vertexRemaining[v] = cnt + 1;
                }
            }

            // PositionKey management — prioritize reuse of the smallest available key
            var keyMap = new Dictionary<int, int>(); // vertex -> key
            var freeKeys = new SortedSet<int>();
            int nextKey = keyOffset;

            int AllocateNewKey()
            {
                while (nextKey == ModelTriangle.NullPtr) nextKey++;
                return nextKey++;
            }

            int GetOrCreateKey(int vertexIndex, out ModelTriangle.IndexType idxType)
            {
                // if already have the key -> Duplicate
                if (keyMap.TryGetValue(vertexIndex, out int existingKey))
                {
                    idxType = ModelTriangle.IndexType.Duplicate;

                    if (vertexRemaining.TryGetValue(vertexIndex, out int rem))
                    {
                        rem--;
                        vertexRemaining[vertexIndex] = rem;
                        if (rem == 0)
                        {
                            // release the key to make it reusable
                            keyMap.Remove(vertexIndex);
                            freeKeys.Add(existingKey);
                        }
                    }

                    return existingKey;
                }

                // use the smallest available key first
                int assigned;
                if (freeKeys.Count > 0)
                {
                    assigned = freeKeys.Min;
                    freeKeys.Remove(assigned);
                }
                else
                {
                    assigned = AllocateNewKey();
                }

                keyMap[vertexIndex] = assigned;
                idxType = ModelTriangle.IndexType.Original;

                if (vertexRemaining.TryGetValue(vertexIndex, out int remainingNow))
                {
                    remainingNow--;
                    vertexRemaining[vertexIndex] = remainingNow;
                    if (remainingNow == 0)
                    {
                        // if it won't be used in the future, release it immediately and make it reusable
                        keyMap.Remove(vertexIndex);
                        freeKeys.Add(assigned);
                        assigned = ModelTriangle.NullPtr;
                    }
                }

                return assigned;
            }

            var structureInfo = new Dictionary<TriangleKey, StructureInfo>();
            byte texIdx = 1;
            byte animTexIdx = 0;

            var tempEntries = new List<(bool isColor, int Color1, int Color2, int Key, int Vertex, ModelTriangle.IndexType IdxType, int ColorIndex, byte TriangleType, byte TriangleSubtype, byte TextureIndex, bool Animated)>();
            var entries = new List<(bool isColor, int Color1, int Color2, int Key, int Vertex, ModelTriangle.IndexType IdxType, int ColorIndex, byte TriangleType, byte TriangleSubtype, byte TextureIndex, bool Animated)>();
            var data = new List<uint>();

            // header
            ModelColor initColor = new() { Color1 = header };
            data.Add(initColor.SaveHeader());

            HashSet<int> actualUsedKeys = [];
            List<int> lastColors = [0, 0];

            // if compressed, create deg tris for sp verts
            if (compressed)
            {
                for (int spV = 0; spV < spVcount; spV++)
                {
                    var idt = ModelTriangle.IndexType.Original;
                    tempEntries.Add((false, -1, -1, Key: keyOffset, Vertex: spV, IdxType: idt, ColorIndex: 0, TriangleType: 2, TriangleSubtype: 3, TextureIndex: 0, Animated: false));
                    if (spV < spVcount - 1)
                    {
                        // og-og-dupl (a-b-b)
                        spV++;
                        tempEntries.Add((false, -1, -1, Key: keyOffset, Vertex: spV, IdxType: idt, ColorIndex: 0, TriangleType: 2, TriangleSubtype: 3, TextureIndex: 0, Animated: false));
                        idt = ModelTriangle.IndexType.Duplicate;
                        tempEntries.Add((false, -1, -1, Key: keyOffset, Vertex: spV, IdxType: idt, ColorIndex: 0, TriangleType: 2, TriangleSubtype: 3, TextureIndex: 0, Animated: false));
                    }
                    else
                    {
                        // og-dupl-dupl (a-a-a)
                        idt = ModelTriangle.IndexType.Duplicate;
                        tempEntries.Add((false, -1, -1, Key: keyOffset, Vertex: spV, IdxType: idt, ColorIndex: 0, TriangleType: 2, TriangleSubtype: 3, TextureIndex: 0, Animated: false));
                        tempEntries.Add((false, -1, -1, Key: keyOffset, Vertex: spV, IdxType: idt, ColorIndex: 0, TriangleType: 2, TriangleSubtype: 3, TextureIndex: 0, Animated: false));
                    }
                }
                entries.AddRange(from e in tempEntries
                                 select e);
            }

            for (int si = 0; si < strips.Count; si++)
            {
                //DebugLog($"Strip [{si}]", true, debug);

                var strip = strips[si];
                tempEntries.Clear();
                int offset = 0;

                // create each vertex entry (textures, etc. are undetermined)
                for (int j = 0; j < strip.Count; j++)
                {
                    int v = strip[j];
                    int assigned = GetOrCreateKey(v, out ModelTriangle.IndexType idt);

                    //if (idt == ModelTriangle.IndexType.Duplicate)
                    //    actualUsedKeys.Add(assigned);
                    if (assigned != ModelTriangle.NullPtr)
                        actualUsedKeys.Add(assigned);

                    int colorIndex = 0; // temp

                    byte triType = (byte)(j < 3 ? 2 : 0); // first 3 tris are CC(2)
                    byte triSubtype = 0; // temp

                    tempEntries.Add((false, -1, -1, Key: assigned, Vertex: v, IdxType: idt, ColorIndex: colorIndex, TriangleType: triType, triSubtype, TextureIndex: 0, Animated: false));
                }

                // determine color/texture/animated/triSubtype
                for (int j = 2; j < strip.Count; j++)
                {
                    int a, b, c;

                    bool isCC = j == 2;
                    if (isCC)
                    {
                        // CC
                        a = strip[j - 2];
                        b = strip[j - 1];
                        c = strip[j];
                    }
                    else
                    {
                        // AA
                        a = strip[j];
                        b = strip[j - 1];
                        c = strip[j - 2];
                    }

                    byte color0 = 0;
                    byte color1 = 0;
                    byte color2 = 0;
                    byte textureIndex = 0;
                    bool animated = false;
                    byte triSubtype = 0; // temp default
                    bool overrideSubtype = false;

                    int[] verts = [a, b, c];
                    Array.Sort(verts);
                    var key = (verts[0], verts[1], verts[2]);

                    if (triMap.TryGetValue(key, out int triIndex))
                    {
                        var srcTri = json.triangles[triIndex];

                        // get index (0-2) in the triangle
                        int ia = Array.IndexOf(srcTri.v, a);
                        int ib = Array.IndexOf(srcTri.v, b);
                        int ic = Array.IndexOf(srcTri.v, c);

                        if (ia >= 0 && ib >= 0 && ic >= 0)
                        {
                            color0 = (byte)srcTri.c[ia];
                            color1 = (byte)srcTri.c[ib];
                            color2 = (byte)srcTri.c[ic];

                            // TODO
                            if (isCC)
                            {
                                // CC[0]-CC[1]-CC[2]
                                lastColors[0] = color2; // CC[2]
                                lastColors[1] = color1; // CC[1]
                            }
                            else
                            {
                                if (lastColors[0] != color1 || lastColors[1] != color2)
                                {
                                    tempEntries.Insert(j + offset, (isColor: true, color1, color2, 0, 0, 0, 0, 0, 0, 0, false)); // ModelColor
                                    offset++;
                                    lastColors[0] = color1;
                                    lastColors[1] = color2;
                                }
                            }

                            // if not CC, swap uv0 and uv2
                            byte[] uv0 = !isCC ? ToUVByte(srcTri.uv[ic]) : ToUVByte(srcTri.uv[ia]);
                            byte[] uv1 = ToUVByte(srcTri.uv[ib]);
                            byte[] uv2 = !isCC ? ToUVByte(srcTri.uv[ia]) : ToUVByte(srcTri.uv[ic]);

                            // flip v
                            byte v0 = uv0[1], v1 = uv1[1], v2 = uv2[1];
                            byte minV = Math.Min(v0, Math.Min(v1, v2));
                            byte maxV = Math.Max(v0, Math.Max(v1, v2));
                            uv0[1] = v0 == minV ? maxV : minV;
                            uv1[1] = v1 == minV ? maxV : minV;
                            uv2[1] = v2 == minV ? maxV : minV;

                            if (srcTri.material >= 0)
                            {
                                TriangleKey tkey = new(srcTri.material, uv0, uv1, uv2);
                                if (!structureInfo.ContainsKey(tkey))
                                {
                                    textureIndex = texIdx;

                                    // search for if animated
                                    foreach (var oldKvp in materials)
                                    {
                                        TriangleKey oldKey = oldKvp.Key;
                                        if (tkey.Material == oldKey.Material)
                                        {
                                            ModelMaterial mat = oldKvp.Value;

                                            int fo = mat.Info.FaceOrientation;
                                            if (fo > 0 && fo <= 3)
                                            {
                                                triSubtype = (byte)fo;
                                                overrideSubtype = true;
                                            }

                                            // if animated
                                            if (mat.Info.AnimCount > 0)
                                            {
                                                animated = true;
                                                textureIndex = animTexIdx;
                                                animTexIdx++;
                                            }
                                            else
                                            {
                                                texIdx++;
                                            }

                                            break;
                                        }
                                    }

                                    structureInfo.Add(tkey, new StructureInfo(textureIndex, triSubtype, isCC));

                                    //DebugLog($"        Created new texture: {textureIndex}", true, debug);
                                }
                                else if (structureInfo.TryGetValue(tkey, out StructureInfo str))
                                {
                                    textureIndex = str.TextureIndex;

                                    foreach (var oldKvp in materials)
                                    {
                                        TriangleKey oldKey = oldKvp.Key;
                                        if (tkey.Material == oldKey.Material)
                                        {
                                            ModelMaterial mat = oldKvp.Value;
                                            if (mat.Info.AnimCount > 0)
                                                animated = true;

                                            int fo = str.FaceOrientation;
                                            if (fo > 0 && fo <= 3)
                                            {
                                                triSubtype = (byte)fo;
                                                overrideSubtype = true;
                                            }
                                            break;
                                        }
                                    }
                                    //DebugLog($"        Found exist texture: {textureIndex}", true, debug);
                                }
                                else
                                {
                                    //DebugLog($"        Could not find texture.", true, debug);
                                }
                            }

                            // determine the subtype from the winding (using the remapped vertices)
                            if (!overrideSubtype)
                            {
                                try
                                {
                                    Vector3 va = new(json.vertices[a][0], json.vertices[a][1], json.vertices[a][2]);
                                    Vector3 vb = new(json.vertices[b][0], json.vertices[b][1], json.vertices[b][2]);
                                    Vector3 vc = new(json.vertices[c][0], json.vertices[c][1], json.vertices[c][2]);

                                    Vector3 normalFromBlender = srcTri != null && srcTri.normal != null && srcTri.normal.Length >= 3
                                        ? new Vector3(srcTri.normal[0], srcTri.normal[1], srcTri.normal[2])
                                        : Vector3.Zero;

                                    // true -> orientation matches (considered CCW） => subtype = 1
                                    // false -> reversed orientation (CW) => subtype = 3
                                    bool correct = IsWindingCorrect(va, vb, vc, normalFromBlender);
                                    if (isCC)
                                        triSubtype = correct ? (byte)3 : (byte)1;
                                    else
                                        triSubtype = correct ? (byte)1 : (byte)3;
                                }
                                catch
                                {
                                    Console.WriteLine($"    [Warning] Failed to determine triangle subtype for tri with vertices {a}, {b}, {c}.");
                                    triSubtype = 0;
                                }
                            }
                        }
                    }

                    int i0 = j + offset - 2;

                    // if CC-CC-CC
                    if (i0 == 0)
                    {
                        tempEntries[i0] = (false, -1, -1, tempEntries[i0].Key, tempEntries[i0].Vertex, tempEntries[i0].IdxType, color0, tempEntries[i0].TriangleType, triSubtype, textureIndex, animated);
                        tempEntries[i0 + 1] = (false, -1, -1, tempEntries[i0 + 1].Key, tempEntries[i0 + 1].Vertex, tempEntries[i0 + 1].IdxType, color1, tempEntries[i0 + 1].TriangleType, triSubtype, textureIndex, animated);
                        tempEntries[i0 + 2] = (false, -1, -1, tempEntries[i0 + 2].Key, tempEntries[i0 + 2].Vertex, tempEntries[i0 + 2].IdxType, color2, tempEntries[i0 + 2].TriangleType, triSubtype, textureIndex, animated);
                    }
                    else
                    {
                        if (!tempEntries[i0 + 2].isColor)
                            tempEntries[i0 + 2] = (false, -1, -1, tempEntries[i0 + 2].Key, tempEntries[i0 + 2].Vertex, tempEntries[i0 + 2].IdxType, color0, tempEntries[i0 + 2].TriangleType, triSubtype, textureIndex, animated);
                    }

                }

                entries.AddRange(from e in tempEntries
                                 select e);
            }

            int maxKey = actualUsedKeys.Count > 0 ? actualUsedKeys.Max() : keyOffset - 1;
            int uniqueKeys = actualUsedKeys.Count;

            if (output)
            {
                Console.WriteLine();
                Console.WriteLine("    [Result]");
                Console.WriteLine($"    Strips: {strips.Count}");
            }

            if (!skipOutput)
            {
                if (!output)
                    Console.WriteLine();
                Console.WriteLine($"    Max key number: {maxKey} (offset: {keyOffset}, unique: {uniqueKeys})");
            }
            else
            {
                Console.WriteLine("  Skipped.");
            }

            if (maxKey > ModelTriangle.NullPtr)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"    The keys exceeded {ModelTriangle.NullPtr} (NullPtr). Exceeded by {maxKey - ModelTriangle.NullPtr}.");
                Console.ForegroundColor = ConsoleColor.White;
                throw new InvalidOperationException("Exceeded maximum key limit. Consider reducing the number of vertices or colors.");
            }

            //int row = 0;
            foreach (var e in entries)
            {
                if (e.isColor)
                {
                    var mc = new ModelColor
                    {
                        Color1 = (byte)e.Color1,
                        Color2 = (byte)e.Color2
                    };

                    data.Add(mc.Save());
                    //DebugLog($"{row}: color1={e.Color1} color2={e.Color2}", true, debug);
                }
                else
                {
                    //byte posKey = keyMap[e.Vertex];
                    var mt = new ModelTriangle(
                        texture: e.TextureIndex,
                        animated: e.Animated,
                        color: (byte)e.ColorIndex,
                        key: (byte)e.Key,
                        unknown: 0,
                        type: (byte)e.IdxType,
                        flag: true,
                        tritype: (byte)((e.TriangleSubtype & 0x3) | ((e.TriangleType & 0x3) << 2))
                    );

                    data.Add(mt.Save());
                    //DebugLog($"{row}: v{e.Vertex} posKey=0x{e.Key:X3} idxType={e.IdxType} triType={e.TriangleType} triSub={e.TriangleSubtype} tex={e.TextureIndex} col={e.ColorIndex}", true, debug);
                }
                //row++;
            }

            // footer
            data.Add(0xFFFFFFFF);
            return (data.ToArray(), structureInfo, strips.Count);
        }

        private static (List<ModelTexture>, List<ModelExtendedTexture>) BuildTexture(Dictionary<TriangleKey, StructureInfo> structureInfo, Dictionary<TriangleKey, ModelMaterial> materials)
        {
            List<ModelTexture> textures = [];
            List<ModelExtendedTexture> animatedtextures = [];

            // split animated and normal textures to add normal textures first
            var normalTextures = new List<ModelTexture>();
            var animatedTextureData = new List<(List<ModelTexture> textures, MaterialInfo info)>();

            foreach (var kvp in structureInfo)
            {
                TriangleKey key = kvp.Key;
                foreach (var oldKvp in materials)
                {
                    TriangleKey oldKey = oldKvp.Key;
                    if (key.Material == oldKey.Material)
                    {
                        var mat = oldKvp.Value;
                        List<ModelTexture> modelTextures = mat.Texture;

                        if (mat.Info.AnimCount > 0)
                        {
                            // animated texture: create ModelTexture list and store with anim info to add later
                            var texList = new List<ModelTexture>();
                            foreach (ModelTexture tex in modelTextures)
                            {
                                texList.Add(CreateModelTexture(tex, key));
                            }
                            animatedTextureData.Add((texList, mat.Info));
                        }
                        else
                        {
                            // normal texture
                            foreach (ModelTexture tex in modelTextures)
                            {
                                normalTextures.Add(CreateModelTexture(tex, key));
                            }
                        }
                        break;
                    }
                }
            }

            // add normal textures first
            textures.AddRange(normalTextures);

            // add animated textures
            foreach (var (texList, info) in animatedTextureData)
            {
                int startOffset = textures.Count + 1;
                textures.AddRange(texList);

                animatedtextures.Add(new ModelExtendedTexture(0)
                {
                    Offset = startOffset,
                    Mask = texList.Count - 1,
                    Delay = info.AnimDelay,
                    Latency = info.AnimSpeed
                });
            }

            return (textures, animatedtextures);
        }

        private static ModelTexture CreateModelTexture(ModelTexture tex, TriangleKey key)
        {
            int blendMode = tex.BlendMode;
            int colorMode = tex.ColorMode;
            int clutY2 = tex.ClutY >> 2;
            int clutY1 = (tex.ClutY & 0x3) << 2;
            int clutX = tex.ClutX;

            int DestX = Math.Min(tex.U1, Math.Min(tex.U2, tex.U3));
            int DestY = Math.Min(tex.V1, Math.Min(tex.V2, tex.V3));
            int Width = Math.Max(tex.U1, Math.Max(tex.U2, tex.U3)) - DestX;
            int Height = Math.Max(tex.V1, Math.Max(tex.V2, tex.V3)) - DestY;

            // UVs must be 0 or 1.
            int u1 = key.U0 != 0 ? DestX + Width : DestX;
            int v1 = key.V0 != 0 ? DestY + Height : DestY;
            int u2 = key.U1 != 0 ? DestX + Width : DestX;
            int v2 = key.V1 != 0 ? DestY + Height : DestY;
            int u3 = key.U2 != 0 ? DestX + Width : DestX;
            int v3 = key.V2 != 0 ? DestY + Height : DestY;

            int tpage = tex.Page;

            return new ModelTexture(
                u1: (byte)u1,
                v1: (byte)v1,
                cluty1: (byte)clutY1,
                clutx: (byte)clutX,
                cluty2: (byte)clutY2,
                u2: (byte)u2,
                v2: (byte)v2,
                colormode: (byte)colorMode,
                blendmode: (byte)blendMode,
                segment: tex.Segment,
                textureoffset: (byte)tpage,
                u3: (byte)u3,
                v3: (byte)v3,
                u4: 0,
                v4: 0
            );
        }

        private static ModelEntry BuildModelEntry(C2Json json, Dictionary<TriangleKey, ModelMaterial> materials, int eid, List<string> tpageNames, int[] modelScales, bool compressed, int spVcount, ModelSettings settings, Debug debug, bool output)
        {
            // colors
            Dictionary<SceneryColor, int> colors = [];
            int colorIndex = 0;
            foreach (var color in json.colors)
            {
                SceneryColor col = new()
                {
                    Red = (byte)color[0],
                    Green = (byte)color[1],
                    Blue = (byte)color[2],
                    Extra = 0
                };
                if (colors.TryAdd(col, colorIndex))
                {
                    colorIndex++;
                }
            }

            if (colorIndex > 127)
                throw new Exception($"Too many colors in model ({colorIndex} colors, must be <= 127)");

            var colorsList = colors.Keys.ToList();

            // poly
            var polys = BuildPolyDataFromStrip(json, colors, materials, compressed, spVcount, settings, debug, output);
            uint[] poly = polys.Item1;
            var structureInfo = polys.Item2;
            int stripCount = polys.Item3;

            // textures
            var textureSet = BuildTexture(structureInfo, materials);
            List<ModelTexture> textures = textureSet.Item1;
            List<ModelExtendedTexture> animatedtextures = textureSet.Item2;

            // info
            byte[] info = new byte[0x50];

            int spVertCount = spVcount; // for compressed model
            int polyCount = poly.Length;
            int textureCount = textures.Count;
            int vertexCount = json.vertices.Count + (spVertCount * 2);
            int colorCount = colorsList.Count;
            int triCount = json.triangles.Count;
            int animTexCount = animatedtextures.Count;

            int tpageCount = tpageNames.Count;

            if (tpageCount > 8)
                throw new Exception($"Too many TPages in model ({tpageCount} TPages, must be <= 8)");

            if (tpageCount > 0)
            {
                // set first TPage
                int tpage1 = Entry.ENameToEID(tpageNames[0]);

                BitConv.ToInt32(info, 0xC, tpage1);

                // set remaining TPages if exist
                for (int i = 1; i < tpageNames.Count && i < 8; i++)
                {
                    int tpageEID = Entry.ENameToEID(tpageNames[i]);
                    BitConv.ToInt32(info, 0x10 + (i - 1) * 4, tpageEID);
                }
            }

            BitConv.ToInt32(info, 0x0, modelScales[0]);  // scaleX
            BitConv.ToInt32(info, 0x4, modelScales[1]);  // scaleY
            BitConv.ToInt32(info, 0x8, modelScales[2]);  // scaleZ
            BitConv.ToInt32(info, 0x2C, polyCount);      // ModelStructCount
            BitConv.ToInt32(info, 0x30, stripCount);     // StripCount ?
            BitConv.ToInt32(info, 0x34, textureCount);   // TextureCount
            BitConv.ToInt32(info, 0x38, vertexCount);    // VertexCount
            BitConv.ToInt32(info, 0x3C, colorCount);     // ColorCount
            BitConv.ToInt32(info, 0x40, tpageCount);     // TPageCount
            BitConv.ToInt32(info, 0x44, triCount);       // PolyCount
            BitConv.ToInt32(info, 0x48, animTexCount);   // AnimatedTextureCount
            BitConv.ToInt32(info, 0x4C, spVertCount);    // SpecialVertexCount

            return new ModelEntry(
                info,
                poly,
                colorsList,
                textures,
                animatedtextures,
                positions: compressed ? new List<ModelPosition>() : null!,
                eid
            );
        }

        //
        // anim
        //
        private static Frame BuildFrame(C2Json json, int frameIndex, int eid, float[] scaleFactors, int[] modelScales, bool compressed, bool debug)
        {
            //DebugLog($"[Frame {frameIndex}]", true, debug);

            // special vertex
            List<float[]> spVerts = [];
            foreach (var marker in json.markers[frameIndex])
                spVerts.Add(marker.pos);
            foreach (var marker in json.groups[frameIndex])
                spVerts.Add(marker.pos);

            int spVertCount = spVerts.Count;

            // vertices
            List<float[]> frameVerts = spVerts;
            frameVerts.AddRange(json.frames[frameIndex]);

            // vertices
            // only the vertex count is matters ?
            FrameVertex[] vertices = new FrameVertex[frameVerts.Count];

            float scaleX = 1.0f / scaleFactors[0];
            float scaleY = 1.0f / scaleFactors[1];
            float scaleZ = 1.0f / scaleFactors[2];

            int count = frameVerts.Count;
            var rawX = new int[count];
            var rawY = new int[count];
            var rawZ = new int[count];

            int minRawX = int.MaxValue, maxRawX = int.MinValue;
            int minRawY = int.MaxValue, maxRawY = int.MinValue;
            int minRawZ = int.MaxValue, maxRawZ = int.MinValue;

            for (int i = 0; i < count; i++)
            {
                var v = frameVerts[i];
                // change axis order (x, y, z) -> (x, z, -y)
                int rx = (int)Math.Round(v[0] / scaleX);
                int ry = (int)Math.Round(v[2] / scaleY);
                int rz = (int)Math.Round(-v[1] / scaleZ);

                rawX[i] = rx;
                rawY[i] = ry;
                rawZ[i] = rz;

                if (rx < minRawX) minRawX = rx;
                if (rx > maxRawX) maxRawX = rx;
                if (ry < minRawY) minRawY = ry;
                if (ry > maxRawY) maxRawY = ry;
                if (rz < minRawZ) minRawZ = rz;
                if (rz > maxRawZ) maxRawZ = rz;
            }

            // choose offsets so that local = raw - offset >= 0
            int frameOffsetRawX = minRawX;
            int frameOffsetRawY = minRawY;
            int frameOffsetRawZ = minRawZ;

            short frameOffsetX = (short)(frameOffsetRawX * 4);
            short frameOffsetY = (short)(frameOffsetRawY * 4);
            short frameOffsetZ = (short)(frameOffsetRawZ * 4);

            List<byte> lstVerts = new();
            List<int> overflowedVerts = new();
            for (int i = 0; i < count; i++)
            {
                int localX = rawX[i] - frameOffsetRawX;
                int localY = rawY[i] - frameOffsetRawY;
                int localZ = rawZ[i] - frameOffsetRawZ;

                byte x = ToByte(localX, out bool ox);
                byte y = ToByte(localY, out bool oy);
                byte z = ToByte(localZ, out bool oz);

                // swap Y and Z
                lstVerts.Add(x);
                lstVerts.Add(z);
                lstVerts.Add(y);

                if (ox || oy || oz)
                    overflowedVerts.Add(i);
            }

            if (overflowedVerts.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"    [Frame {frameIndex}] Warning: Vertices overflowed ({string.Join(", ", overflowedVerts)})");
                Console.ForegroundColor = ConsoleColor.White;
            }

            int byteCount = frameVerts.Count * 3;
            byte[] verts = new byte[(byteCount + 3) / 4 * 4]; // align to 4 bytes
            Array.Copy(lstVerts.ToArray(), verts, lstVerts.ToArray().Length);

            bool[] temporals = new bool[verts.Length / 4 * 32];
            for (int i = 0; i < verts.Length / 4; i++)
            {
                int val = BitConv.FromInt32(verts, i * 4);
                for (int j = 0; j < 32; j++) // reverse endianness for decompression
                {
                    temporals[i * 32 + j] = (val >> (31 - j) & 0x1) == 1;
                }
            }

            // collisions
            const float CollisionProduct = BaseScales.CollisionScaleFactor * BaseScales.ModelScale;
            float[] collScales =
            [
                scaleFactors[0] * modelScales[0] / CollisionProduct,
                scaleFactors[1] * modelScales[1] / CollisionProduct,
                scaleFactors[2] * modelScales[2] / CollisionProduct,
            ];
            List<FrameCollision> lstColl = [];
            foreach (var coll in json.collisions[frameIndex])
            {
                lstColl.Add(BuildCollision(coll.min, coll.max, collScales));
            }
            FrameCollision[] collisions = lstColl.ToArray();

            // HeaderSize
            int headersize = 0x18 + (collisions.Length * 0x28) + (spVertCount * 3);

            return new Frame(
                xoffset: frameOffsetX,
                yoffset: frameOffsetY,
                zoffset: frameOffsetZ,
                unknown: 0,
                modeleid: eid,
                headersize: headersize,
                collision: collisions,
                vertices: vertices,
                specialvertexcount: spVertCount,
                temporals: temporals,
                isnew: false
            );
        }

        private static IList<Position> GetVertices(Frame frame)
        {
            IList<Position> verts = new Position[frame.Vertices.Count];

            // uncompressed frame
            bool[] uncompressedbitstream = new bool[frame.Temporals.Length];
            for (int i = 0; i < uncompressedbitstream.Length / 32; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    for (int k = 0; k < 8; ++k)
                    {
                        uncompressedbitstream[32 * i + 24 - j * 8 + k] = frame.Temporals[32 * i + j * 8 + k]; // replace this with a cool formula one day
                    }
                }
            }
            int bi = 0;
            for (int i = 0; i < frame.Vertices.Count; ++i)
            {
                byte x = 0;
                for (int j = 0; j < 8; ++j)
                {
                    x |= (byte)(Convert.ToByte(uncompressedbitstream[bi++]) << (7 - j));
                }

                byte y = 0;
                for (int j = 0; j < 8; ++j)
                {
                    y |= (byte)(Convert.ToByte(uncompressedbitstream[bi++]) << (7 - j));
                }

                byte z = 0;
                for (int j = 0; j < 8; ++j)
                {
                    z |= (byte)(Convert.ToByte(uncompressedbitstream[bi++]) << (7 - j));
                }

                verts[i] = new Position(x, y, z);
            }

            return verts;
        }

        private static void BuildOptimalSharedPositions(List<Frame> frames, List<ModelPosition> sharedPos, int method)
        {
            int spVcount = frames[0].SpecialVertexCount;
            int vcount = frames[0].Vertices.Count;

            for (int i = 0; i < vcount; i++)
            {
                var diffsX = new List<int>();
                var diffsY = new List<int>();
                var diffsZ = new List<int>();
                int prevX = 0, prevY = 0, prevZ = 0;

                foreach (var frame in frames)
                {
                    var v = GetVertices(frame)[i];

                    int tx = (int)v.X;
                    int ty = (int)v.Y;
                    int tz = (int)v.Z;

                    diffsX.Add(WrapDiff(tx, prevX));
                    diffsY.Add(WrapDiff(ty, prevY));
                    diffsZ.Add(WrapDiff(tz, prevZ));

                    prevX = tx;
                    prevY = ty;
                    prevZ = tz;
                }

                int x, y, z;

                switch (method)
                {
                    case 1: // average
                        x = (int)diffsX.Average();
                        y = (int)diffsY.Average();
                        z = (int)diffsZ.Average();
                        break;
                    case 2: // all 0
                        x = 0;
                        y = 0;
                        z = 0;
                        break;
                    default: // median
                        x = Median(diffsX);
                        y = Median(diffsY);
                        z = Median(diffsZ);
                        break;
                }
                ;

                sharedPos[i].X = (byte)(x >> 1); // X is scaled to twice
                sharedPos[i].Y = (byte)y;
                sharedPos[i].Z = (byte)z;
            }
        }

        public static (List<Frame>, List<ModelPosition>) CompressFrames(List<Frame> frames, int method)
        {
            if (frames == null || frames.Count == 0)
                return (frames, new List<ModelPosition>());

            int spVcount = frames[0].SpecialVertexCount;
            int vcount = frames[0].Vertices.Count;

            List<ModelPosition> sharedPos = [];
            for (int i = 0; i < vcount; i++)
                sharedPos.Add(new ModelPosition(0));

            BuildOptimalSharedPositions(frames, sharedPos, method);

            var maxXBits = new int[vcount];
            var maxYBits = new int[vcount];
            var maxZBits = new int[vcount];

            foreach (var frame in frames)
            {
                int x_acc = 0, y_acc = 0, z_acc = 0;
                var verts = GetVertices(frame);

                for (int i = 0; i < vcount; i++)
                {
                    var p = sharedPos[i];
                    var v = verts[i];

                    int targetX, targetY, targetZ;

                    targetX = (int)v.X;
                    targetY = (int)v.Y;
                    targetZ = (int)v.Z;

                    int predX = (x_acc + ((p.XBits == 7) ? 0 : (p.X << 1))) & 0xFF;
                    int predY = (y_acc + ((p.YBits == 7) ? 0 : p.Y)) & 0xFF;
                    int predZ = (z_acc + ((p.ZBits == 7) ? 0 : p.Z)) & 0xFF;

                    int diffX = WrapDiff(targetX, predX);
                    int diffY = WrapDiff(targetY, predY);
                    int diffZ = WrapDiff(targetZ, predZ);

                    int xBits = BitsNeededSigned(diffX);
                    int yBits = BitsNeededSigned(diffY);
                    int zBits = BitsNeededSigned(diffZ);

                    if (xBits > maxXBits[i]) maxXBits[i] = xBits;
                    if (yBits > maxYBits[i]) maxYBits[i] = yBits;
                    if (zBits > maxZBits[i]) maxZBits[i] = zBits;

                    x_acc = targetX;
                    y_acc = targetY;
                    z_acc = targetZ;
                }
            }

            for (int i = 0; i < vcount; i++)
            {
                if (i == 0)
                {
                    // the first one must always be reset
                    sharedPos[i].X = 0;
                    sharedPos[i].Y = 0;
                    sharedPos[i].Z = 0;
                    sharedPos[i].XBits = 7;
                    sharedPos[i].YBits = 7;
                    sharedPos[i].ZBits = 7;
                }
                else
                {
                    sharedPos[i].XBits = (byte)Math.Clamp(maxXBits[i], 0, 7);
                    sharedPos[i].YBits = (byte)Math.Clamp(maxYBits[i], 0, 7);
                    sharedPos[i].ZBits = (byte)Math.Clamp(maxZBits[i], 0, 7);
                }
            }

            for (int f = 0; f < frames.Count; f++)
            {
                //Console.WriteLine($"Frame [{f}]");

                var temporals = new List<bool>();
                int x_acc = 0, y_acc = 0, z_acc = 0;

                var frame = frames[f];
                var verts = GetVertices(frame);

                for (int s = 0; s < spVcount; s++)
                {
                    // these vertices are NEVER compressed
                    WriteSignedBits(frame.Vertices[s].X, 7, temporals);
                    WriteSignedBits(frame.Vertices[s].Y, 7, temporals);
                    WriteSignedBits(frame.Vertices[s].Z, 7, temporals);
                }

                for (int i = 0; i < vcount; i++)
                {
                    //Console.WriteLine($"Vertex [{i}]");
                    var p = sharedPos[i];
                    var v = verts[i];

                    int targetX = (int)v.X;
                    int targetY = (int)v.Y;
                    int targetZ = (int)v.Z;

                    int predX = (x_acc + ((p.XBits == 7) ? 0 : (p.X << 1))) & 0xFF;
                    int predY = (y_acc + ((p.YBits == 7) ? 0 : p.Y)) & 0xFF;
                    int predZ = (z_acc + ((p.ZBits == 7) ? 0 : p.Z)) & 0xFF;

                    int diffX = WrapDiff(targetX, predX);
                    int diffY = WrapDiff(targetY, predY);
                    int diffZ = WrapDiff(targetZ, predZ);

                    if (p.XBits == 7)
                    {
                        sharedPos[i].X = 0;
                        diffX = targetX;
                    }
                    if (p.YBits == 7)
                    {
                        sharedPos[i].Y = 0;
                        diffY = targetY;
                    }
                    if (p.ZBits == 7)
                    {
                        sharedPos[i].Z = 0;
                        diffZ = targetZ;
                    }

                    if (i == 0)
                    {
                        WriteSignedBits(targetX, 7, temporals);
                        WriteSignedBits(targetZ, 7, temporals);
                        WriteSignedBits(targetY, 7, temporals);
                    }
                    else
                    {
                        WriteSignedBits(diffX, p.XBits, temporals);
                        WriteSignedBits(diffZ, p.ZBits, temporals);
                        WriteSignedBits(diffY, p.YBits, temporals);
                    }

                    x_acc = targetX;
                    y_acc = targetY;
                    z_acc = targetZ;
                    //Console.WriteLine($"x_acc={x_acc}, y_acc={y_acc}, z_acc={z_acc}");
                    //Console.WriteLine($"dx={diffX}, XBits={p.XBits}\ndy={diffY}, YBits={p.YBits}\ndz={diffZ}, ZBits={p.ZBits}\n");
                }

                while (temporals.Count % 32 != 0)
                    temporals.Add(false);

                frame.Temporals = temporals.ToArray();
                frames[f] = frame;
            }

            return (frames, sharedPos);
        }

        private static void WriteSignedBits(int value, int bits, List<bool> temporals)
        {
            if (bits == 7)
            {
                byte raw = (byte)value;
                for (int b = 7; b >= 0; b--)
                    temporals.Add(((raw >> b) & 1) != 0);
                return;
            }

            bool sign = value < 0;
            temporals.Add(sign);
            int mag = sign ? value + (1 << bits) : value;

            for (int b = bits - 1; b >= 0; b--)
                temporals.Add(((mag >> b) & 1) != 0);
        }

        private static int Median(List<int> v)
        {
            v.Sort();
            return v[v.Count / 2];
        }

        private static int WrapDiff(int a, int b)
        {
            int d = (a - b) & 0xFF;
            if (d >= 128) d -= 256;
            return d;
        }

        private static int BitsNeededSigned(int v)
        {
            for (int bits = 0; bits <= 6; bits++)
            {
                int min = -(1 << bits);
                int max = (1 << bits) - 1;
                if (v >= min && v <= max)
                    return bits;
            }
            return 7;
        }

        private static byte ToByte(int v, out bool overflow)
        {
            overflow = v < 0 || v > 255;
            return (byte)Math.Clamp(v, 0, 255);
        }

        private static bool IsWindingCorrect(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 normalFromBlender)
        {
            Vector3 e1 = v1 - v0;
            Vector3 e2 = v2 - v0;

            Vector3 geomNormal = Vector3.Cross(e1, e2);
            float dot = Vector3.Dot(geomNormal, normalFromBlender);
            return dot >= 0f; // true = Orientation matches, false = Orientation is reversed
        }

        private static FrameCollision BuildCollision(float[] min, float[] max, float[] collScales)
        {
            const float scale = BaseScales.CollisionScale;
            float sx = scale * collScales[0];
            float sy = scale * collScales[1];
            float sz = scale * collScales[2];

            // change axis order (x, y, z) -> (x, z, -y)
            float cx = (min[0] + max[0]) * 0.5f;
            float cy = (min[2] + max[2]) * 0.5f;
            float cz = (-min[1] + -max[1]) * 0.5f;

            float ex = (max[0] - min[0]) * 0.5f;
            float ey = (max[2] - min[2]) * 0.5f;
            float ez = (-max[1] - -min[1]) * 0.5f;

            return new FrameCollision
            {
                U = 0, // ?
                XOffset = ClampToInt32(cx * sx),
                YOffset = ClampToInt32(cy * sy),
                ZOffset = ClampToInt32(cz * sz),
                X1 = ClampToInt32(-ex * sx),
                Y1 = ClampToInt32(-ey * sy),
                Z1 = ClampToInt32(ez * sz),
                X2 = ClampToInt32(ex * sx),
                Y2 = ClampToInt32(ey * sy),
                Z2 = ClampToInt32(-ez * sz)
            };
        }

        private static int ClampToInt32(float v)
        {
            int i = (int)Math.Round(v);
            Math.Clamp(i, int.MinValue, int.MaxValue);
            return i;
        }

        private static List<Frame> BuildFrames(C2Json json, int modelEID, float[] scaleFactors, int[] modelScales, bool compressed, ModelSettings settings, Debug debug)
        {
            List<Frame> frames = [];
            bool skipOdd = settings.SkipOddFrames;
            int i = 0;
            foreach (var f in json.frames)
            {
                if (!skipOdd || (skipOdd && (i & 1) == 0))
                    frames.Add(BuildFrame(json, i, modelEID, scaleFactors, modelScales, compressed, debug.DebugMode));
                i++;
            }

            return frames;
        }

        private static AnimationEntry BuildAnimationEntry(C2Json json, int modelEID, int animEID, float[] scaleFactors, int[] modelScales, bool compressed, ModelSettings settings, Debug debug)
        {
            int frameCount;
            string info;
            if (settings.SkipOddFrames)
            {
                frameCount = (json.frames.Count + 1) / 2;
                info = $"    Building animation with {frameCount} frames (skipping odd frames)...";
            }
            else
            {
                frameCount = json.frames.Count;
                info = $"    Building animation with {frameCount} frames...";
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.WriteLine();
            Console.WriteLine(info);
            Console.ForegroundColor = ConsoleColor.White;

            //float scaleX = 1.0f / scaleFactors[0];
            //float scaleY = 1.0f / scaleFactors[1];
            //float scaleZ = 1.0f / scaleFactors[2];
            //Console.WriteLine($"Scale: X={scaleX}, Y={scaleY}, Z={scaleZ}");

            List<Frame> frames = BuildFrames(json, modelEID, scaleFactors, modelScales, compressed, settings, debug);

            // non-compressed
            return new AnimationEntry(frames, false, animEID);
        }

        //
        // materials
        //
        private static (Dictionary<TriangleKey, ModelMaterial>, List<string>) BuildMaterials(C2Json json, List<TextureChunk> tpages, List<PackedTexture> packedTextures, bool debug)
        {
            if (debug)
                Console.WriteLine();

            // create a set of texture paths used by this model's materials
            var usedMaterialPaths = new HashSet<string>();
            foreach (var mat in json.materials)
            {
                if (mat.texture != null)
                    usedMaterialPaths.Add(mat.texture);
            }

            List<PackedTexture> modelPackedTextures = [];

            var materialIndexToPackedTextures = new Dictionary<int, List<PackedTexture>>();

            for (int matIdx = 0; matIdx < json.materials.Count; matIdx++)
            {
                var mat = json.materials[matIdx];
                if (mat.texture == null)
                    continue;

                string matName = mat.name;
                string searchName = Regex.Replace(matName, @"_r=[^_]+", "").TrimEnd('_');

                // search for packed textures that match both the material's texture path and the base name (without parameters)
                var matchingTextures = packedTextures.Where(pt =>
                    pt.FilePath == mat.texture &&
                    pt.Name == searchName
                ).ToList();

                // if no exact matches found, try matching by base name only (ignoring parameters)
                if (matchingTextures.Count == 0)
                {
                    string baseMatName = Regex.Replace(searchName, @"_([dsmf])(\d+)", "");
                    baseMatName = baseMatName.TrimEnd('_');

                    matchingTextures = packedTextures.Where(pt =>
                        pt.FilePath == mat.texture &&
                        (pt.Name == baseMatName || Regex.Replace(pt.Name, @"_([dsmf])(\d+)", "").TrimEnd('_') == baseMatName)
                    ).ToList();

                    if (matchingTextures.Count > 0)
                    {
                        Console.WriteLine($"    Material [{matIdx}] '{matName}' matched to base '{baseMatName}' -> {matchingTextures.Count} packed textures");
                    }
                }

                if (matchingTextures.Count > 0)
                {
                    // if the material name contains an animation delay parameter, apply it to the matched textures
                    var delayMatch = Regex.Match(matName, @"_d(\d+)");
                    if (delayMatch.Success)
                    {
                        int animDelay = int.Parse(delayMatch.Groups[1].Value);

                        var delayedTextures = new List<PackedTexture>();
                        foreach (var pt in matchingTextures)
                        {
                            var delayedInfo = new MaterialInfo(
                                pt.Info.FaceOrientation,
                                pt.Info.BlendMode,
                                pt.Info.AnimOffset,
                                pt.Info.AnimCount,
                                pt.Info.AnimSpeed,
                                animDelay,
                                pt.Info.AnimRepeat,
                                pt.Info.TotalAnimRepeats
                            );

                            delayedTextures.Add(new PackedTexture(
                                matIdx, // use current material index for the model
                                matName,
                                pt.FilePath,
                                pt.Bpp,
                                pt.ClutX,
                                pt.ClutY,
                                pt.DestX,
                                pt.DestY,
                                pt.Width,
                                pt.Height,
                                pt.TPage,
                                delayedInfo
                            ));
                        }

                        materialIndexToPackedTextures[matIdx] = delayedTextures;
                        modelPackedTextures.AddRange(delayedTextures);
                    }
                    else
                    {
                        // remap the matched textures to use the current material index for this model
                        var remappedTextures = matchingTextures.Select(pt => new PackedTexture(
                            matIdx,  // use current material index for the model
                            pt.Name,
                            pt.FilePath,
                            pt.Bpp,
                            pt.ClutX,
                            pt.ClutY,
                            pt.DestX,
                            pt.DestY,
                            pt.Width,
                            pt.Height,
                            pt.TPage,
                            pt.Info
                        )).ToList();

                        materialIndexToPackedTextures[matIdx] = remappedTextures;
                        modelPackedTextures.AddRange(remappedTextures);
                    }

                    if (debug)
                        Console.WriteLine($"    Material [{matIdx}] '{mat.name}' -> {matchingTextures.Count} packed textures");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"    Warning: Material [{matIdx}] '{mat.name}' (search: '{searchName}') has no matching packed textures");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            var usedTPageIndices = modelPackedTextures.Select(pt => pt.TPage).Distinct().OrderBy(x => x).ToList();
            List<string> tpageNames = usedTPageIndices.Select(idx => tpages[idx].EName).ToList();

            // create a mapping from global texture page index to local index
            var globalToLocalPageMap = new Dictionary<int, int>();
            for (int localIdx = 0; localIdx < usedTPageIndices.Count; localIdx++)
            {
                globalToLocalPageMap[usedTPageIndices[localIdx]] = localIdx;
            }

            if (debug)
            {
                //Console.WriteLine($"Texture page mapping for model '{modelName}':");
                foreach (var kvp in globalToLocalPageMap)
                    Console.WriteLine($"    Global page {kvp.Key} ({tpages[kvp.Key].EName}) -> Local index {kvp.Value}");
            }

            // build materials for this model using the assigned packed textures and the page mapping
            Dictionary<TriangleKey, ModelMaterial> materials = BuildMaterialsFromPacked(
                json,
                materialIndexToPackedTextures,
                globalToLocalPageMap);

            return (materials, tpageNames);
        }

        private static void GetXOff(int colorMode, int value, out int segment, out int xoff)
        {
            int xoffUnit = (1 << (2 - colorMode)) * 64;
            segment = value / xoffUnit;
            xoff = xoffUnit * segment;
        }

        private static byte ClampToByte(float v)
        {
            int i = (int)Math.Round(v);
            return (byte)Math.Clamp(i, 0, 255);
        }

        private static byte[] ToUVByte(float[] uv)
        {
            byte u = ClampToByte(uv[0]);
            byte v = ClampToByte(uv[1]);
            return [u, v];
        }

        private static Dictionary<TriangleKey, ModelMaterial> BuildMaterialsFromPacked(
            C2Json json,
            Dictionary<int, List<PackedTexture>> materialIndexToPackedTextures,
            Dictionary<int, int> globalToLocalPageMap)
        {
            //Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.WriteLine();
            //Console.WriteLine($"Building materials from packed textures...");
            //Console.ForegroundColor = ConsoleColor.White;

            Dictionary<TriangleKey, ModelMaterial> materials = [];

            var normalMaterials = new List<(TriangleKey key, ModelMaterial material)>();
            var animatedMaterials = new List<(TriangleKey key, ModelMaterial material)>();

            var processedKeys = new HashSet<TriangleKey>();

            foreach (C2Triangle tri in json.triangles)
            {
                int materialIndex = tri.material;

                // skip invalid material indices (notex)
                if (materialIndex < 0 || materialIndex >= json.materials.Count)
                {
                    continue;
                }

                TriangleKey key = new(
                    materialIndex,
                    ToUVByte(tri.uv[0]),
                    ToUVByte(tri.uv[1]),
                    ToUVByte(tri.uv[2])
                );

                if (processedKeys.Contains(key))
                    continue;

                processedKeys.Add(key);

                // get the packed textures for this material index, if any
                if (!materialIndexToPackedTextures.TryGetValue(materialIndex, out var matchingPacked))
                {
                    string matName = json.materials[materialIndex].name;

                    if (!matName.Contains("_r="))
                    {
                        string baseName = Regex.Replace(matName, @"_([dsmf])(\d+)", "");
                        baseName = baseName.TrimEnd('_');

                        bool foundByBaseName = false;
                        for (int i = 0; i < json.materials.Count; i++)
                        {
                            string otherMatName = json.materials[i].name;
                            string otherBaseName = Regex.Replace(otherMatName, @"_([dsmf])(\d+)", "");
                            otherBaseName = otherBaseName.TrimEnd('_');

                            if (otherBaseName == baseName && materialIndexToPackedTextures.TryGetValue(i, out matchingPacked))
                            {
                                foundByBaseName = true;
                                //Console.WriteLine($"  Material[{materialIndex}] '{matName}' matched to base material '{otherMatName}' (index {i})");
                                break;
                            }
                        }

                        if (!foundByBaseName)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"  Warning: No packed textures found for material index {materialIndex} ('{matName}', base: '{baseName}')");
                            Console.ForegroundColor = ConsoleColor.White;
                            continue;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"  Warning: No packed textures found for material index {materialIndex} ('{matName}')");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                }

                PackedTexture firstPacked = matchingPacked[0];
                List<ModelTexture> tex = [];

                if (firstPacked.Info.AnimCount > 0)
                {
                    //Console.Write($"  Material[{materialIndex}] Packing animated texture '{firstPacked.Name}'...");
                    int count = 0;
                    foreach (PackedTexture p in matchingPacked)
                    {
                        if (p.Name == firstPacked.Name)
                        {
                            tex.Add(BuildModelTexture(tri, p, globalToLocalPageMap));
                            count++;
                        }
                    }
                    //Console.WriteLine($" {count} frames");

                    animatedMaterials.Add((key, new ModelMaterial(firstPacked.Name, firstPacked.Info, 0, tex)));
                }
                else
                {
                    //Console.WriteLine($"  Material[{materialIndex}] Normal texture '{firstPacked.Name}'");
                    tex.Add(BuildModelTexture(tri, firstPacked, globalToLocalPageMap));
                    normalMaterials.Add((key, new ModelMaterial(firstPacked.Name, firstPacked.Info, 0, tex)));
                }
            }

            // assign texture indices for normal materials first, then animated materials
            int textureIndex = 1;
            foreach (var (key, material) in normalMaterials)
            {
                materials.Add(key, new ModelMaterial(
                    material.Name,
                    material.Info,
                    textureIndex,
                    material.Texture
                ));
                //Console.WriteLine($"    Assigned texture index {textureIndex} to material '{material.Name}'");
                textureIndex++;
            }

            int animatedTextureIndex = 0;
            foreach (var (key, material) in animatedMaterials)
            {
                materials.Add(key, new ModelMaterial(
                    material.Name,
                    material.Info,
                    animatedTextureIndex,
                    material.Texture
                ));
                //Console.WriteLine($"    Assigned animated texture index {animatedTextureIndex} to material '{material.Name}'");
                animatedTextureIndex++;
            }

            return materials;
        }

        private static ModelTexture BuildModelTexture(
            C2Triangle tri,
            PackedTexture packed,
            Dictionary<int, int> globalToLocalPageMap)
        {
            int blendMode = packed.Info.BlendMode;
            int colorMode = packed.Bpp == 4 ? 0 : 1;
            int clutY2 = packed.ClutY >> 2;
            int clutY1 = (packed.ClutY & 0x3) << 2;
            int clutX = packed.ClutX;

            int u1 = Math.Round(tri.uv[0][0]) > 0 ? packed.DestX + packed.Width - 1 : packed.DestX;
            int v1 = Math.Round(tri.uv[0][1]) > 0 ? packed.DestY + packed.Height - 1 : packed.DestY;
            int u2 = Math.Round(tri.uv[1][0]) > 0 ? packed.DestX + packed.Width - 1 : packed.DestX;
            int v2 = Math.Round(tri.uv[1][1]) > 0 ? packed.DestY + packed.Height - 1 : packed.DestY;
            int u3 = Math.Round(tri.uv[2][0]) > 0 ? packed.DestX + packed.Width - 1 : packed.DestX;
            int v3 = Math.Round(tri.uv[2][1]) > 0 ? packed.DestY + packed.Height - 1 : packed.DestY;

            // get x offset adjustments
            GetXOff(colorMode, u1, out int segment, out int xoff);
            u1 -= xoff;
            GetXOff(colorMode, u2, out _, out xoff);
            u2 -= xoff;
            GetXOff(colorMode, u3, out _, out xoff);
            u3 -= xoff;

            // flip y
            var minV = Math.Min(v1, Math.Min(v2, v3));
            var maxV = Math.Max(v1, Math.Max(v2, v3));
            v1 = v1 == minV ? maxV : minV;
            v2 = v2 == minV ? maxV : minV;
            v3 = v3 == minV ? maxV : minV;

            // map global texture page index to local index for this model
            if (!globalToLocalPageMap.TryGetValue(packed.TPage, out int localTPage))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Error] Global page {packed.TPage} not found in mapping for texture '{packed.Name}'");
                Console.ForegroundColor = ConsoleColor.White;
                localTPage = packed.TPage; // fallback
            }

            //Console.WriteLine($"    Texture '{packed.Name}': Global page {packed.TPage} -> Local page {localTPage}, " +
            //    $"UV=({u1},{v1},{u2},{v2},{u3},{v3}), Seg={segment}, CLUT=({clutX},{packed.ClutY}), " +
            //    $"Pos=({packed.DestX},{packed.DestY}), Size=({packed.Width}x{packed.Height})");

            return new ModelTexture(
                u1: (byte)u1,
                v1: (byte)v1,
                cluty1: (byte)clutY1,
                clutx: (byte)clutX,
                cluty2: (byte)clutY2,
                u2: (byte)u2,
                v2: (byte)v2,
                colormode: (byte)colorMode,
                blendmode: (byte)blendMode,
                segment: (byte)segment,
                textureoffset: (byte)localTPage,
                u3: (byte)u3,
                v3: (byte)v3,
                u4: 0,
                v4: 0
            );
        }

        //
        // tpages
        //
        private static List<TextureEntry> BuildTPages(List<C2Json> jsons)
        {
            // collect all textures first to build atlases and assign texture coordinates
            List<TextureEntry> allTextures = [];
            Dictionary<(string filePath, string matName), int> textureIndexMap = [];

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("Collecting textures from all models...");
            Console.ForegroundColor = ConsoleColor.White;

            int globalTextureIndex = 0;
            for (int p = 0; p < jsons.Count; p++)
            {
                C2Json json = jsons[p];

                for (int i = 0; i < json.materials.Count; i++)
                {
                    var mat = json.materials[i];
                    string filePath = mat.texture;
                    string name = mat.name;

                    if (filePath == null)
                        continue;

                    var textureKey = (filePath, name);
                    if (textureIndexMap.ContainsKey(textureKey))
                        continue;

                    try
                    {
                        var (rawImageData, palette, width, height) = TextureConv.ProcessPng(
                            null,
                            filePath,
                            isBGRA: true,
                            oldBpp: -1,
                            quantize: true
                        );

                        int bpp = palette.Length <= 0x40 ? 4 : 8;

                        int faceOrientation = -1;
                        int blendMode = 3;
                        int animCount = 0;
                        (int, int) grid = (0, 0);
                        int animDelay = 0;
                        int animSpeed = 0;
                        List<(int index, int repeat)> animSequence = [];

                        // parse material name for parameters
                        var gridMatch = Regex.Match(mat.name, @"_a(\d+)x(\d+)");
                        if (gridMatch.Success)
                        {
                            int cols = int.Parse(gridMatch.Groups[1].Value);
                            int rows = int.Parse(gridMatch.Groups[2].Value);
                            grid = (cols, rows);
                            animCount = cols * rows;
                        }

                        var paramMatches = Regex.Matches(mat.name, @"_([sdrmf])(\d+|=[^_]+)");
                        foreach (Match m in paramMatches)
                        {
                            string type = m.Groups[1].Value;
                            string valueStr = m.Groups[2].Value;

                            switch (type)
                            {
                                case "d":
                                    animDelay = int.Parse(valueStr);
                                    break;
                                case "s":
                                    animSpeed = int.Parse(valueStr);
                                    break;
                                case "r":
                                    if (valueStr.StartsWith('='))
                                    {
                                        var sequenceStr = valueStr[1..];
                                        var parts = sequenceStr.Split(',');

                                        foreach (var part in parts)
                                        {
                                            if (part.Length >= 2)
                                            {
                                                char indexChar = part[0];
                                                string repeatStr = part[1..];

                                                int index = char.IsUpper(indexChar)
                                                    ? indexChar - 'A'
                                                    : indexChar - 'a';

                                                if (int.TryParse(repeatStr, out int repeat))
                                                {
                                                    animSequence.Add((index, repeat));
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case "m":
                                    blendMode = int.Parse(valueStr);
                                    break;
                                case "f":
                                    faceOrientation = int.Parse(valueStr);
                                    break;
                            }
                        }

                        // use material name without parameters as base name
                        string baseName = Regex.Replace(mat.name, @"_r=[^_]+", "").TrimEnd('_');

                        Console.WriteLine($"Loaded texture: {filePath} ({name}), {width,3:d}x{height,2:d}, {bpp} bpp");

                        if (animCount > 0)
                        {
                            if (animSequence.Count == 0)
                            {
                                for (int j = 0; j < animCount; j++)
                                    animSequence.Add((j, 1));
                            }

                            List<Bitmap> splitTex = SplitPng(filePath, grid.Item1, grid.Item2);

                            int sequenceOffset = 0;
                            foreach (var (texIndex, repeat) in animSequence)
                            {
                                if (texIndex >= splitTex.Count)
                                {
                                    Console.WriteLine($"Warning: Texture index {texIndex} out of range for '{name}'");
                                    continue;
                                }

                                var image = TextureConv.ProcessPng(
                                    splitTex[texIndex],
                                    null,
                                    isBGRA: true,
                                    oldBpp: -1,
                                    quantize: true
                                );

                                int w = bpp == 4 ? image.width : image.width * 2;
                                int h = image.height;

                                allTextures.Add(new TextureEntry
                                {
                                    Index = i,
                                    Name = baseName,
                                    FilePath = filePath,
                                    Data = image.rawImageData,
                                    Palette = image.palette,
                                    Bpp = bpp,
                                    Width = w,
                                    Height = h,
                                    Info = new MaterialInfo(
                                        faceOrientation,
                                        blendMode,
                                        texIndex,
                                        animCount,
                                        animSpeed,
                                        animDelay,
                                        repeat,
                                        animSequence.Sum(s => s.repeat)
                                    )
                                });

                                Console.WriteLine($"    Sequence[{sequenceOffset}]: texture[{texIndex}] × {repeat} frames (name: '{baseName}')");
                                sequenceOffset++;
                            }
                        }
                        else
                        {
                            allTextures.Add(new TextureEntry
                            {
                                Index = i,
                                Name = baseName,
                                FilePath = filePath,
                                Data = rawImageData,
                                Palette = palette,
                                Bpp = bpp,
                                Width = bpp == 4 ? width : width * 2,
                                Height = height,
                                Info = new MaterialInfo(faceOrientation, blendMode, 0, 0, 0, 0, 1, 1)
                            });
                        }

                        textureIndexMap[textureKey] = globalTextureIndex;
                        globalTextureIndex++;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Failed to load texture '{filePath}': {ex.Message}");
                    }
                }
            }

            return allTextures;
        }

        private static List<Bitmap> SplitPng(string path, int cols, int rows)
        {
            Bitmap src = new(path);

            if (src.Width % cols != 0 || src.Height % rows != 0)
                throw new Exception("Image size not divisible by grid.");

            int frameWidth = src.Width / cols;
            int frameHeight = src.Height / rows;

            List<Bitmap> frames = new(cols * rows);

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    Rectangle rect = new(
                        x * frameWidth,
                        y * frameHeight,
                        frameWidth,
                        frameHeight
                    );

                    Bitmap frame = src.Clone(rect, src.PixelFormat);
                    frame.Palette = src.Palette;
                    frames.Add(frame);
                }
            }

            src.Dispose();
            return frames;
        }

        //
        // run
        //
        public static void ConvertModel(string path, ModelSettings settings, Debug debug)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("================================");
            Console.WriteLine("Starting conversion...");
            Console.WriteLine("================================");
            Console.ForegroundColor = ConsoleColor.White;

            List<C2Json> jsons = LoadModelJson(path);

            string saveDirectory = settings.ExportPath;
            string fileName = Path.GetFileNameWithoutExtension(path);

            Dictionary<string, List<Frame>> allFrames = [];
            Dictionary<string, ModelEntry> modelsToSave = [];
            List<(string, AnimationEntry)> animationsToSave = [];
            Dictionary<int, TextureChunk> texturesToSave = [];

            var allTextures = BuildTPages(jsons);
            var (tpages, packedTextures) = AllocateTextureAtlas(allTextures, settings.BaseTPageName, debug.DebugTextures);

            foreach (TextureChunk tpage in tpages)
                _ = texturesToSave.TryAdd(tpage.EID, tpage);

            byte[] fileBytes;
            string savePath;
            string modelName, animName;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine($"Building models...");
            Console.ForegroundColor = ConsoleColor.White;

            // build models
            for (int p = 0; p < jsons.Count; p++)
            {
                C2Json json = jsons[p];
                ModelObject obj = settings.ModelObjects[p];
                modelName = obj.ModelEID;

                ModelItem item = settings.ModelItems.FirstOrDefault(m => m.ModelEID == modelName)
                    ?? throw new InvalidOperationException("Model item not found for current model EID.");

                int modelEID = Entry.ENameToEID(modelName);
                int spVcount = json.markers[0].Count + json.groups[0].Count;
                int[] modelScales = item.ModelScales;
                bool compressed = item.CompressionMethod >= 0;

                bool skipOutput = modelsToSave.ContainsKey(modelName);

                Console.Write($"Processing '{modelName}'...");

                var (materials, tpageNames) = BuildMaterials(json, tpages, packedTextures, debug.DebugMaterials);
                ModelEntry model = BuildModelEntry(json, materials, modelEID, tpageNames, modelScales, compressed, spVcount, settings, debug, skipOutput);

                if (!skipOutput)
                    modelsToSave.Add(modelName, model);
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine($"Building animations...");
            Console.ForegroundColor = ConsoleColor.White;

            // build animations
            for (int p = 0; p < jsons.Count; p++)
            {
                C2Json json = jsons[p];
                ModelObject obj = settings.ModelObjects[p];
                animName = obj.AnimEID;
                modelName = obj.ModelEID;

                ModelItem item = settings.ModelItems.FirstOrDefault(m => m.ModelEID == modelName)
                    ?? throw new InvalidOperationException("Model item not found for current model EID.");

                int modelEID = Entry.ENameToEID(modelName);
                int animEID = Entry.ENameToEID(animName);
                int[] modelScales = item.ModelScales;
                float[] scaleFactors = item.ScaleFactor;
                bool compressed = item.CompressionMethod >= 0;

                Console.WriteLine($"=== Object [{p}] ({animName}) ===");

                AnimationEntry animation = BuildAnimationEntry(json, modelEID, animEID, scaleFactors, modelScales, compressed, settings, debug);

                if (allFrames.TryAdd(modelName, new List<Frame>(animation.Frames)))
                {
                    // first time seeing this model, added new entry
                    Console.WriteLine($"    Added {animation.Frames.Count} frames for model '{modelName}'");
                }
                else
                {
                    allFrames[modelName].AddRange(animation.Frames);
                    Console.WriteLine($"    Appended {animation.Frames.Count} frames to model '{modelName}' (total now {allFrames[modelName].Count} frames)");
                }
                Console.WriteLine();

                animationsToSave.Add((modelName, animation));
            }

            // compress frames
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Compressing frames...");
            Console.ForegroundColor = ConsoleColor.White;

            Dictionary<string, List<Frame>>? allCompressedFrames = [];
            Dictionary<string, List<ModelPosition>>? allPositions = [];

            foreach (var kvp in allFrames)
            {
                modelName = kvp.Key;
                List<Frame> frames = kvp.Value;

                // get compression method for this model
                ModelItem? modelItem = settings.ModelItems.FirstOrDefault(m => m.ModelEID == modelName);
                if (modelItem == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Warning: Model item not found for '{modelName}', skipping compression");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                int method = modelItem.CompressionMethod;
                if (method >= 0)
                {
                    (List<Frame>, List<ModelPosition>) compressedItem;

                    if (debug.TestCompression)
                    {
                        int bestLength = int.MaxValue;
                        int bestMethod = 0;
                        (List<Frame>, List<ModelPosition>) bestCompressed = ([], []);

                        Console.WriteLine($"Testing compression methods for '{modelName}':");
                        for (int m = 0; m < 3; m++)
                        {
                            List<Frame> copy = frames
                                .Select(x => new Frame(x.XOffset, x.YOffset, x.ZOffset, x.Unknown, x.ModelEID, x.HeaderSize, x.Collision, x.Vertices, x.SpecialVertexCount, x.Temporals, x.IsNew))
                                .ToList();
                            var comp = CompressFrames(copy, m);

                            int newLength = comp.Item1.Count > 0 ? comp.Item1[0].Temporals.Length : 0;

                            if (newLength < bestLength)
                            {
                                bestLength = newLength;
                                bestMethod = m;
                                bestCompressed = comp;
                            }
                            Console.WriteLine($"    Method {m}: {newLength / 8} bytes");
                        }
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"    Best method: {bestMethod} ({bestLength / 8} bytes)");
                        Console.ForegroundColor = ConsoleColor.White;
                        compressedItem = bestCompressed;
                    }
                    else
                    {
                        Console.WriteLine($"Processing '{modelName}'...");
                        compressedItem = CompressFrames(frames, method);
                        int compressedLength = compressedItem.Item1.Count > 0 ? compressedItem.Item1[0].Temporals.Length : 0;
                        Console.WriteLine($"    Compressed using method {method}: {compressedLength / 8} bytes");
                    }

                    allCompressedFrames.Add(modelName, compressedItem.Item1);
                    allPositions.Add(modelName, compressedItem.Item2);
                }
            }

            if (allCompressedFrames.Count == 0)
                Console.WriteLine("No models had compression enabled, skipping compression step.");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("Saving...");
            Console.ForegroundColor = ConsoleColor.White;

            // save models
            foreach (var kvp in modelsToSave)
            {
                ModelEntry model = kvp.Value;
                modelName = kvp.Key;

                ModelItem modelItem = settings.ModelItems.FirstOrDefault(m => m.ModelEID == modelName)
                    ?? throw new InvalidOperationException("Model item not found for current model EID.");
                if (modelItem.CompressionMethod >= 0)
                {
                    List<ModelPosition> positions = allPositions[modelName];
                    foreach (var pos in positions)
                        model.Positions.Add(pos);
                }

                fileBytes = model.Save();
                savePath = Path.Combine(saveDirectory, $"{fileName}_Model_{modelName}.nsentry");
                File.WriteAllBytes(savePath, fileBytes);
                //Console.WriteLine($"    Saved model entry: {savePath}");
            }

            // save animations, making sure to assign the correct compressed frames if compression is enabled
            Dictionary<string, int> modelOffsets = [];
            foreach (var item in animationsToSave)
            {
                AnimationEntry anim = item.Item2;
                string name = Entry.EIDToEName(anim.EID);
                modelName = item.Item1;

                if (!modelOffsets.ContainsKey(modelName))
                    modelOffsets[modelName] = 0;

                ModelItem modelItem = settings.ModelItems.FirstOrDefault(m => m.ModelEID == modelName)
                    ?? throw new InvalidOperationException("Model item not found for current model EID.");
                if (modelItem.CompressionMethod >= 0)
                {
                    List<Frame> frames = allCompressedFrames[modelName];
                    int frameCount = anim.Frames.Count;
                    int currentOffset = modelOffsets[modelName];

                    for (int i = 0; i < frameCount; i++)
                    {
                        anim.Frames[i] = frames[i + currentOffset];
                    }

                    modelOffsets[modelName] += frameCount;
                }

                fileBytes = anim.Save();
                savePath = Path.Combine(saveDirectory, $"{fileName}_Anim_{name}.nsentry");
                File.WriteAllBytes(savePath, fileBytes);
                //Console.WriteLine($"    Saved animation entry: {savePath}");
            }

            // save tpages
            foreach (var kvp in texturesToSave)
            {
                TextureChunk tpage = kvp.Value;
                fileBytes = tpage.Save();
                savePath = Path.Combine(saveDirectory, $"{fileName}_Tpage_{tpage.EName}.nschunk");
                File.WriteAllBytes(savePath, fileBytes);
                //Console.WriteLine($"    Saved texture page:    {savePath}");
            }

            Console.WriteLine($"Total: {modelsToSave.Count} models, {animationsToSave.Count} animations, {texturesToSave.Count} texture pages");
            Console.WriteLine($"Saved to '{saveDirectory}'.");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("Conversion completed!");
            Console.ForegroundColor = ConsoleColor.White;
            SystemSounds.Asterisk.Play();
        }
    }
}
