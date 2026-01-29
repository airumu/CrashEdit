using AltUI.Forms;
using CrashEdit.CE.Controls;
using CrashEdit.Crash;
using MetroSet_UI.Animates;
using OpenTK.Windowing.Common.Input;
using System;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Media;
using System.Numerics;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Media.TextFormatting;
using System.Xml.Linq;
using static CrashEdit.CE.TextureAtlasPacker;

namespace CrashEdit.CE
{
    public partial class ModelConverterForm : DarkForm
    {
        private static readonly string Version = "1.0";

        public bool DebugMode;

        private ModelSettings modelSettings;
        private string settingsPath;

        public ModelConverterForm()
        {
            InitializeComponent();
            Icon = Embeds.GetIcon("Wrench");

            DebugMode = chkDebug.Checked;

            lblPath.Text = "";
            lblExportPath.Text = "";
            cmdSetExportPath.Image = new Bitmap(Embeds.Bitmaps["FolderOpen"], new Size(16, 16));
            numScaleX.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);
            numScaleY.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);
            numScaleZ.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);
            numScaleFX.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);
            numScaleFY.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);
            numScaleFZ.MouseWheel += new MouseEventHandler(ScrollHandlerFunction2);
        }

        private void cmdOpen_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new();
            ofd.Filter = FileFilters.JSON;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path = ofd.FileName;
                string saveDirectory = Path.GetDirectoryName(path)!;
                string fileName = Path.GetFileNameWithoutExtension(path);

                Console.WriteLine();
                Console.WriteLine("Selected file: " + path);
                // try load settings file
                try
                {
                    modelSettings = ModelSettingsIO.Load(path);
                    settingsPath = path;
                    LoadSettings();
                    Console.WriteLine("Loaded settings.");
                }
                // invalid settings file, try load model json
                catch (Exception)
                {
                    try
                    {
                        Console.WriteLine("Could not load settings, trying to load model JSON file...");
                        var json = BlenderModelConverter.LoadJson(path);

                        settingsPath = Path.Combine(saveDirectory, $"{fileName}_settings.json");

                        // load existing settings
                        if (File.Exists(settingsPath))
                        {
                            modelSettings = ModelSettingsIO.Load(settingsPath);
                            LoadSettings();
                            Console.WriteLine("Found and loaded existing settings.");
                        }
                        else
                        {
                            // use default settings
                            txtModelEID.Text = "0000G";
                            txtAnimEID.Text = "0000V";
                            txtTpageEID.Text = "Z000T";
                            numScaleX.Value = 0x646;
                            numScaleY.Value = 0x646;
                            numScaleZ.Value = 0x646;
                            numScaleFX.Value = 127.0M;
                            numScaleFY.Value = 127.0M;
                            numScaleFZ.Value = 127.0M;

                            lblPath.Text = path;
                            lblExportPath.Text = saveDirectory;
                            modelSettings = new();
                            SaveSettings();

                            Console.WriteLine("No existing settings found, created new default settings.");
                        }
                    }
                    catch (Exception ex)
                    {
                        DarkMessageBox.ShowError($"The selected file is not a valid model JSON file.\n\nDetails: {ex.Message}", "Invalid File");
                        return;
                    }
                }

                chkDebug.Enabled =
                cmdConvert.Enabled =
                fraSettings.Enabled = true;
            }
        }

        private void LoadSettings()
        {
            lblPath.Text = modelSettings.ModelJsonPath;
            lblExportPath.Text = modelSettings.ExportPath;
            txtModelEID.Text = modelSettings.ModelEID;
            txtAnimEID.Text = modelSettings.AnimationEID;
            txtTpageEID.Text = modelSettings.TpageEID;
            numScaleX.Value = modelSettings.ModelScales[0];
            numScaleY.Value = modelSettings.ModelScales[1];
            numScaleZ.Value = modelSettings.ModelScales[2];
            numScaleFX.Value = (decimal)modelSettings.ScaleFactor[0];
            numScaleFY.Value = (decimal)modelSettings.ScaleFactor[1];
            numScaleFZ.Value = (decimal)modelSettings.ScaleFactor[2];
        }

        private void cmdSaveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void cmdConvert_Click(object sender, EventArgs e)
        {
            SaveSettings();
            BlenderModelConverter.ConvertModel(modelSettings, DebugMode);
        }

        private void SaveSettings()
        {
            modelSettings.Version = Version;
            modelSettings.ModelJsonPath = lblPath.Text;
            modelSettings.ExportPath = lblExportPath.Text;
            modelSettings.ModelEID = txtModelEID.Text;
            modelSettings.AnimationEID = txtAnimEID.Text;
            modelSettings.TpageEID = txtTpageEID.Text;
            modelSettings.ModelScales =
            [
                (int)numScaleX.Value,
                (int)numScaleY.Value,
                (int)numScaleZ.Value
            ];
            modelSettings.ScaleFactor =
            [
                (float)numScaleFX.Value,
                (float)numScaleFY.Value,
                (float)numScaleFZ.Value
            ];

            ModelSettingsIO.Save(settingsPath, modelSettings);
            Console.WriteLine("Settings saved.");
        }

        private void EID_Validating(object sender, CancelEventArgs e)
        {
            TextBox txtBox = sender as TextBox ?? throw new InvalidOperationException("Sender is not a TextBox");
            string error = Entry.CheckEIDErrors(txtBox.Text, true);
            if (error != string.Empty)
            {
                e.Cancel = true;
                DarkMessageBox.ShowError(error, "EID Error");
            }
        }

        private void cmdSetExportPath_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog fbd = new();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                lblExportPath.Text = fbd.SelectedPath;
            }
        }

        private void chkHex_CheckedChanged(object sender, EventArgs e)
        {
            numScaleX.Hexadecimal =
            numScaleY.Hexadecimal =
            numScaleZ.Hexadecimal = chkHex.Checked;
        }

        private void ScrollHandlerFunction2(object sender, MouseEventArgs e)
        {
            if (sender is NumericUpDown numericUpDown)
            {
                HandledMouseEventArgs handledArgs = e as HandledMouseEventArgs;
                if (handledArgs != null) handledArgs.Handled = true;

                decimal newValue = numericUpDown.Value;
                if (e.Delta > 0 && newValue + 4 < numericUpDown.Maximum)
                    newValue += 4;

                else if (e.Delta < 0 && newValue - 4 >= numericUpDown.Minimum)
                    newValue -= 4;

                numericUpDown.Value = newValue;
            }
        }

        private void chkDebug_CheckedChanged(object sender, EventArgs e)
        {
            DebugMode = chkDebug.Checked;
        }
    }

    public static class ModelSettingsIO
    {
        private static readonly JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        public static void Save(string settingsPath, ModelSettings settings)
        {
            string json = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(settingsPath, json);
        }

        public static ModelSettings Load(string path)
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<ModelSettings>(json)!;
        }
    }

    public class ModelSettings
    {
        public string Version { get; set; }
        public string ModelJsonPath { get; set; }
        public string ExportPath { get; set; }
        public string ModelEID { get; set; }
        public string AnimationEID { get; set; }
        public string TpageEID { get; set; }
        public int[] ModelScales { get; set; }
        public float[] ScaleFactor { get; set; }
    }

    public class Crash2Triangle
    {
        public int[] v { get; set; }
        public float[] normal { get; set; }
        public float[][] uv { get; set; } 
        public int material { get; set; } // TextureIndex
    }

    public class Crash2Material
    {
        public string name { get; set; }
        public string texture { get; set; }
    }

    public class Crash2Collision
    {
        public float[] min { get; set; }
        public float[] max { get; set; }
    }

    public class Crash2Json
    {
        public List<float[]> vertices { get; set; }
        public List<Crash2Triangle> triangles { get; set; }
        public List<List<float[]>> frames { get; set; }
        public List<int[]> colors { get; set; }
        public List<Crash2Material> materials { get; set; }
        public List<List<Crash2Collision>> collisions { get; set; }
    }

    public readonly struct TriangleKey(int material, byte[] uv0, byte[] uv1, byte[] uv2)
    {
        public readonly int Material = material;
        public readonly byte U0 = uv0[0], V0 = uv0[1];
        public readonly byte U1 = uv1[0], V1 = uv1[1];
        public readonly byte U2 = uv2[0], V2 = uv2[1];
    }

    public readonly struct PackedTexture(int index, string name, string filePath, int bpp, int clutX, int clutY, int destX, int destY, int w, int h, int tpage, TextureInfo info)
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
        public readonly TextureInfo Info = info;
    }

    public struct TextureInfo
    {
        public int BlendMode;
        public int AnimOffset; // split texture offset
        public int AnimCount;
        public int AnimSpeed;
        public int AnimDelay;
    }

    public struct ModelMaterial
    {
        public string Name;
        public TextureInfo Info;
        public int TextureIndex;
        public int AnimatedTextureIndex;
        public List<ModelTexture> Texture;
    }

    public static class BlenderModelConverter
    {
        public static Crash2Json LoadJson(string path)
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Crash2Json>(json)!;
        }

        //
        // model
        //
        private static uint[] BuildPolyData(Crash2Json json, Dictionary<SceneryColor, int> colors, Dictionary<TriangleKey, ModelMaterial> materials, bool debug)
        {
            //Console.WriteLine();
            //Console.WriteLine($"[PolyData]");
            List<uint> poly = [];

            // init
            var mc = new ModelColor(
                color1: 0,
                color2: 0
            );
            poly.Add(mc.Save());

            foreach (var tri in json.triangles)
            {
                for (int i = 0; i < 3; i++)
                {
                    SceneryColor color = new()
                    {
                        Red = (byte)json.colors[tri.v[i]][0],
                        Green = (byte)json.colors[tri.v[i]][1],
                        Blue = (byte)json.colors[tri.v[i]][2],
                        Extra = 0
                    };
                    int colorIndex = 0;
                    if (colors.ContainsKey(color))
                    {
                        colorIndex = colors.TryGetValue(color, out int idx) ? idx : 0;
                    }

                    byte textureIndex = 0;
                    TriangleKey key = new(
                        tri.material,
                        ToUVByte(tri.uv[0]),
                        ToUVByte(tri.uv[1]),
                        ToUVByte(tri.uv[2])
                    );

                    bool animated = false;

                    if (materials.TryGetValue(key, out ModelMaterial mat))
                    {
                        textureIndex = (byte)mat.TextureIndex;

                        var info = mat.Info;
                        if (info.AnimCount > 0)
                        {
                            animated = true;
                            //DebugLog($"    Count: {info.AnimCount}, Delay: {info.AnimDelay}, Speed: {info.AnimSpeed}", true, debug);

                            string baseName = Regex.Replace(mat.Name, @"_[d]\d+", "");
                            int delay = 0;
                            var match = Regex.Match(mat.Name, @"_d(\d+)");
                            if (match.Success)
                                delay = int.Parse(match.Groups[1].Value);

                            foreach (var kvp in materials)
                            {
                                ModelMaterial _mat = kvp.Value;
                                if (_mat.Name == mat.Name &&
                                    _mat.Info.AnimDelay == delay &&
                                    kvp.Key.U0 == key.U0 && kvp.Key.V0 == key.V0 &&
                                    kvp.Key.U1 == key.U1 && kvp.Key.V1 == key.V1 &&
                                    kvp.Key.U2 == key.U2 && kvp.Key.V2 == key.V2)
                                {
                                    textureIndex = (byte)_mat.AnimatedTextureIndex;
                                    break;
                                }
                            }
                        }
                    }

                    // TriangleSubtype (face orientation)
                    // 0 = double sided?
                    // 1 = backward
                    // 2 = double sided
                    // 3 = forward
                    byte triangleSubtype = 3; // temp
                    byte triangleType = 2; // temp

                    ModelTriangle mt = new(
                        texture: textureIndex,
                        animated: animated,
                        color: (byte)colorIndex,
                        key: 0x57, // temp NullPtr
                        unknown: 0, // temp, can be used for some vfx stuff
                        type: (byte)ModelTriangle.IndexType.Original, // temp
                        flag: true, // temp ?
                        tritype: (byte)((triangleSubtype & 0x3) | ((triangleType & 0x3) << 2))
                    );

                    poly.Add(mt.Save());
                }
            }

            poly.Add(0xFFFFFFFF); // footer

            return poly.ToArray();
        }

        private static ModelEntry BuildModelEntry(Crash2Json json, Dictionary<TriangleKey, ModelMaterial> materials, int eid, string tpageName, int[] modelScales, bool debug)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine($"Building model...");
            Console.ForegroundColor = ConsoleColor.White;

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

            // textures
            List<ModelTexture> textureSet = [];
            List<ModelExtendedTexture> animatedtextures = [];

            List<ModelMaterial> materialList = materials.Values.ToList();

            int textureOffset = 0;

            for (int idx = 0; idx < materialList.Count; idx++)
            {
                ModelMaterial mat = materialList[idx];
                TextureInfo texInfo = mat.Info;

                textureSet.Add(mat.Texture[0]);

                // update texture index
                mat.TextureIndex += textureOffset;
                materialList[idx] = mat;

                // if animated texture
                if (texInfo.AnimCount > 0)
                {
                    // update animated texture index
                    mat.AnimatedTextureIndex = animatedtextures.Count;
                    materialList[idx] = mat;

                    int index = mat.TextureIndex;

                    Console.WriteLine($"Created animated texture for {mat.Name}, Index: {animatedtextures.Count}, Offset: {index}");
                    animatedtextures.Add(new ModelExtendedTexture(0)
                    {
                        Offset = index,
                        Mask = texInfo.AnimCount - 1,
                        Delay = texInfo.AnimDelay,
                        Latency = texInfo.AnimSpeed
                    });

                    for (int i = 1; i < texInfo.AnimCount; i++)
                    {
                        textureSet.Add(mat.Texture[i]);
                        textureOffset++;
                    }
                }

            }

            List<ModelTexture> textures = textureSet.ToList();

            var keys = materials.Keys.ToList();
            for (int i = 0; i < keys.Count; i++)
                materials[keys[i]] = materialList[i];

            // poly
            var poly = BuildPolyData(json, colors, materials, debug);

            // info
            byte[] info = new byte[0x50];

            int polyCount = poly.Length;
            int textureCount = textures.Count;
            int vertexCount = json.triangles.Count * 3;
            int colorCount = colorsList.Count;
            int triCount = json.triangles.Count;
            int animTexCount = animatedtextures.Count;

            int tpageCount = 1; // temp
            int tpage1 = Entry.ENameToEID(tpageName);

            // TODO: calculate correct scale values
            BitConv.ToInt32(info, 0x0, modelScales[0]);  // scaleX
            BitConv.ToInt32(info, 0x4, modelScales[1]);  // scaleY
            BitConv.ToInt32(info, 0x8, modelScales[2]);  // scaleZ
            BitConv.ToInt32(info, 0xC, tpage1);          // TPage1            temp
            BitConv.ToInt32(info, 0x2C, polyCount);      // ModelStructCount
            BitConv.ToInt32(info, 0x34, textureCount);   // TextureCount
            BitConv.ToInt32(info, 0x38, vertexCount);    // VertexCount
            BitConv.ToInt32(info, 0x3C, colorCount);     // ColorCount
            BitConv.ToInt32(info, 0x40, tpageCount);     // TPageCount
            BitConv.ToInt32(info, 0x44, triCount);       // PolyCount
            BitConv.ToInt32(info, 0x48, animTexCount);   // AnimatedTextureCount

            return new ModelEntry(
                info,
                poly,
                colorsList,
                textures,
                animatedtextures,
                positions: null!,
                eid
            );
        }

        //
        // anim
        //
        private static void DebugLog(string message, bool addLine, bool debug)
        {
            if (debug)
            {
                if (addLine)
                    Console.WriteLine(message);
                else
                    Console.Write(message);
            }
        }

        private static Frame BuildFrame(Crash2Json json, int frameIndex, int eid, float[] scaleFactors, bool debug)
        {
            DebugLog($"[Frame {frameIndex}]", true, debug);

            List<float[]> frameVerts = json.frames[frameIndex];
            List<Crash2Triangle> triangles = json.triangles;

            // Vertices
            // only the vertex count is matters ?
            FrameVertex[] vertices = new FrameVertex[triangles.Count * 3];

            // Temporals
            float minX = float.MaxValue, minY = float.MaxValue, minZ = float.MaxValue;
            float maxX = float.MinValue, maxY = float.MinValue, maxZ = float.MinValue;

            foreach (var v in frameVerts)
            {
                // change axis order (x, y, z) -> (x, z, -y)
                minX = Math.Min(minX, v[0]);
                minY = Math.Min(minY, v[2]);
                minZ = Math.Min(minZ, -v[1]);
                maxX = Math.Max(maxX, v[0]);
                maxY = Math.Max(maxY, v[2]);
                maxZ = Math.Max(maxZ, -v[1]);
            }

            float scaleX = 1.0f / scaleFactors[0];
            float scaleY = 1.0f / scaleFactors[1];
            float scaleZ = 1.0f / scaleFactors[2];

            // TODO: verify?
            short frameOffsetX = (short)Math.Floor(minX / scaleX);
            short frameOffsetY = (short)Math.Floor(minY / scaleY);
            short frameOffsetZ = (short)Math.Floor(minZ / scaleZ);

            List<byte> lstVerts = [];
            List<int> overflowedVerts = [];
            for (int i = 0; i < triangles.Count; i++)
            {
                Crash2Triangle tri = triangles[i];
                int[] triVertIdx = tri.v;
                
                // test
                float[] f0 = frameVerts[triVertIdx[0]];
                float[] f1 = frameVerts[triVertIdx[1]];
                float[] f2 = frameVerts[triVertIdx[2]];
                bool correct = IsWindingCorrect(
                    new(f0[0], f0[1], f0[2]),
                    new(f1[0], f1[1], f1[2]),
                    new(f2[0], f2[1], f2[2]),
                    new(tri.normal[0], tri.normal[1], tri.normal[2]));
                string winding = $", WindingCorrect: {correct}";
                //

                DebugLog($"    Triangle {i}: Verts [{triVertIdx[0]}, {triVertIdx[1]}, {triVertIdx[2]}]" + winding, true, debug);

                for (int j = 0; j < 3; j++)
                {
                    int vtxIndex = triVertIdx[j];
                    float[] v = frameVerts[vtxIndex];

                    // change axis order (x, y, z) -> (x, z, -y)
                    int localX = (int)(Math.Round(v[0] / scaleX) - frameOffsetX);
                    int localY = (int)(Math.Round(v[2] / scaleY) - frameOffsetY);
                    int localZ = (int)(Math.Round(-v[1] / scaleZ) - frameOffsetZ);
                    byte x = ToByte(localX, out bool ox);
                    byte y = ToByte(localY, out bool oy);
                    byte z = ToByte(localZ, out bool oz);

                    // swap Y and Z
                    lstVerts.Add(x);
                    lstVerts.Add(z);
                    lstVerts.Add(y);
                    vertices[i * 3 + j] = new FrameVertex(x, y, z);

                    DebugLog($"        [{vtxIndex}] {v[0]}, {v[1]}, {v[2]} -> {x}, {z}, {y}", false, debug);
                    if (ox || oy || oz)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        DebugLog($"    Warning: Vertex overflowed!", true, debug);
                        Console.ForegroundColor = ConsoleColor.White;
                        overflowedVerts.Add(vtxIndex);
                    }
                    else
                    {
                        DebugLog("", true, debug);
                    }
                }
            }

            if (overflowedVerts.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                DebugLog("", true, debug);
                Console.WriteLine($"[Frame {frameIndex}] Warning: Some vertices overflowed.");
                Console.ForegroundColor = ConsoleColor.White;
            }

            // adjust frame offsets
            frameOffsetX *= 4;
            frameOffsetY *= 4;
            frameOffsetZ *= 4;

            int byteCount = triangles.Count * 3 * 3;
            byte[] verts = new byte[(byteCount + 3) / 4 * 4]; // align to 4 bytes
            Array.Copy(lstVerts.ToArray(), verts, lstVerts.ToArray().Length);

            // convert to temporals bit array
            bool[] temporals = new bool[verts.Length / 4 * 32];
            for (int i = 0; i < verts.Length / 4; i++)
            {
                int val = BitConv.FromInt32(verts, i * 4);
                for (int j = 0; j < 32; j++) // reverse endianness for decompression
                {
                    temporals[i * 32 + j] = (val >> (31 - j) & 0x1) == 1;
                }
            }

            // Collision
            List<FrameCollision> lstColl = [];
            foreach (var col in json.collisions[frameIndex])
            {
                lstColl.Add(BuildCollision(col.min, col.max));
            }
            FrameCollision[] collisions = lstColl.ToArray();

            // HeaderSize
            int headersize = 0x18 + (collisions.Length * 0x28);

            return new Frame(
                xoffset: frameOffsetX,
                yoffset: frameOffsetY,
                zoffset: frameOffsetZ,
                unknown: 0,
                modeleid: eid,
                headersize: headersize,
                collision: collisions,
                vertices: vertices,
                specialvertexcount: 0,
                temporals: temporals,
                isnew: false
            );
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

            Vector3 geomNormal = Vector3.Cross(e1, e2); // normals calculated in vertex order

            float dot = Vector3.Dot(geomNormal, normalFromBlender);

            return dot >= 0f; // true = Orientation matches, false = Orientation is reversed
        }

        private static FrameCollision BuildCollision(float[] min, float[] max)
        {
            // TODO: verify?
            const float scale = 50944.0f; // 0xC700

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
                XOffset = ClampToInt32(cx * scale),
                YOffset = ClampToInt32(cy * scale),
                ZOffset = ClampToInt32(cz * scale),
                X1 = ClampToInt32(-ex * scale),
                Y1 = ClampToInt32(-ey * scale),
                Z1 = ClampToInt32(-ez * scale),
                X2 = ClampToInt32(ex * scale),
                Y2 = ClampToInt32(ey * scale),
                Z2 = ClampToInt32(ez * scale)
            };
        }

        private static int ClampToInt32(float v)
        {
            int i = (int)Math.Round(v);
            Math.Clamp(i, int.MinValue, int.MaxValue);
            return i;
        }

        private static AnimationEntry BuildAnimationEntry(Crash2Json json, int modelEID, int animEID, float[] scaleFactors, bool debug)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine($"Building animation with {json.frames.Count} frames...");
            Console.ForegroundColor = ConsoleColor.White;

            // just for logging
            float scaleX = 1.0f / scaleFactors[0];
            float scaleY = 1.0f / scaleFactors[1];
            float scaleZ = 1.0f / scaleFactors[2];
            Console.WriteLine($"Scale: X={scaleX}, Y={scaleY}, Z={scaleZ}");

            List<Frame> frames = [];
            int i = 0;
            foreach (var f in json.frames)
            {
                frames.Add(BuildFrame(json, i, modelEID, scaleFactors, debug));
                i++;
            }
            return new AnimationEntry(frames, false, animEID);
        }

        //
        // materials
        //
        private static void GetXOff(int colorMode, int value, out int segment, out int xoff)
        {
            int xoffUnit = (1 << (2 - colorMode)) * 64;
            segment = value / xoffUnit;
            xoff = xoffUnit * segment;
        }

        private static ModelTexture BuildModelTexture(Crash2Triangle tri, PackedTexture packed)
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

            int tpage = packed.TPage;

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
                textureoffset: (byte)tpage,
                u3: (byte)u3,
                v3: (byte)v3,
                u4: 0,
                v4: 0
            );
        }

        private static Dictionary<TriangleKey, ModelMaterial> BuildMaterials(Crash2Json json, List<PackedTexture> packedTextures)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine($"Building materials...");
            Console.ForegroundColor = ConsoleColor.White;

            Dictionary<TriangleKey, ModelMaterial> materials = [];
            int textureIndex = 1; // start from 1, 0 is reserved for null texture

            foreach (Crash2Triangle tri in json.triangles)
            {
                int materialIndex = tri.material;

                TriangleKey key = new(
                    materialIndex,
                    ToUVByte(tri.uv[0]),
                    ToUVByte(tri.uv[1]),
                    ToUVByte(tri.uv[2])
                );

                foreach (PackedTexture packed in packedTextures)
                {
                    if (packed.Index == materialIndex)
                    {
                        if (!materials.ContainsKey(key))
                        {
                            List<ModelTexture> tex = [];
                            
                            // if animated, add split textures too
                            if (packed.Info.AnimCount > 0)
                            {
                                Console.Write($"Packing split textures for '{packed.Name}'...");
                                int count = 0;
                                foreach (PackedTexture p in packedTextures)
                                {
                                    if (p.Name == packed.Name)
                                    {
                                        tex.Add(BuildModelTexture(tri, p));
                                        count++;
                                    }
                                }
                                Console.WriteLine($"    Done. Total: {count}");
                            }
                            else
                            {
                                tex.Add(BuildModelTexture(tri, packed));
                            }

                            materials.Add(key, new ModelMaterial()
                            { 
                                Name = packed.Name,
                                Info = packed.Info,
                                TextureIndex = textureIndex,
                                Texture = tex
                            });
                            textureIndex++;
                        }
                    }
                }
            }

            return materials;
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

        //
        // tpages
        //
        private static (List<TextureChunk>, List<PackedTexture>) BuildTPages(Crash2Json json, string tpageName)
        {
            List<TextureEntry> textures = [];
            List<List<Bitmap>> animTextures = [];

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("Loading textures...");
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < json.materials.Count; i++)
            {
                var mat = json.materials[i];
                string filePath = mat.texture;
                string name = mat.name;

                // skip null textures
                if (filePath == null)
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

                    int blendMode = 3; // default
                    int animCount = 0;
                    int animSpeed = 0;
                    int animDelay = 0;

                    var matches = Regex.Matches(mat.name, @"_([asdm])(\d+)"); // a = anim count, s = speed, d = delay, m = blend mode
                    foreach (Match m in matches)
                    {
                        string type = m.Groups[1].Value;
                        int value = int.Parse(m.Groups[2].Value);

                        switch (type)
                        {
                            case "a":
                                animCount = value;
                                break;
                            case "s":
                                animSpeed = value;
                                break;
                            case "d":
                                animDelay = value;
                                break;
                            case "m":
                                blendMode = value;
                                break;
                        }
                    }

                    Console.WriteLine($"Loaded texture: {filePath} ({name}), {width,3:d}x{height,2:d}, {bpp} bpp");

                    if (animCount > 0)
                    {
                        List<Bitmap> splitTex = SplitPng(filePath, animCount);

                        for (int j = 0; j < splitTex.Count; j++)
                        {
                            var image = TextureConv.ProcessPng(
                                splitTex[j],
                                null,
                                isBGRA: true,
                                oldBpp: -1,
                                quantize: true
                            );

                            int w = bpp == 4 ? image.width : image.width * 2;
                            int h = image.height;

                            textures.Add(new TextureEntry
                            {
                                Index = i,
                                Name = mat.name,
                                FilePath = filePath,
                                Data = image.rawImageData,
                                Palette = image.palette,
                                Bpp = bpp,
                                Width = w,
                                Height = h,
                                Info = new TextureInfo
                                {
                                    BlendMode = blendMode,
                                    AnimOffset = j,
                                    AnimCount = animCount,
                                    AnimSpeed = animSpeed,
                                    AnimDelay = animDelay
                                }
                            });
                            Console.WriteLine($"    Split texture [{j}]: {w}x{h}");
                        }
                    }
                    else
                    {
                        textures.Add(new TextureEntry
                        {
                            Index = i,
                            Name = mat.name,
                            FilePath = filePath,
                            Data = rawImageData,
                            Palette = palette,
                            Bpp = bpp,
                            Width = bpp == 4 ? width : width * 2,
                            Height = height,
                            Info = new TextureInfo
                            {
                                BlendMode = blendMode,
                                AnimOffset = 0,
                                AnimCount = 0,
                                AnimSpeed = 0,
                                AnimDelay = 0
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to load texture '{filePath}': {ex.Message}");
                }
            }

            var tex = AllocateTextureAtlas(textures, tpageName);
            return (tex.Item1, tex.Item2);
        }

        private static List<Bitmap> SplitPng(string path, int animCount)
        {
            Bitmap src = new(path);

            if (src.Width % animCount != 0)
                throw new Exception("Width not divisible by animCount");

            int frameWidth = src.Width / animCount;
            int height = src.Height;

            List<Bitmap> frames = new(animCount);

            for (int i = 0; i < animCount; i++)
            {
                Rectangle rect = new(i * frameWidth, 0, frameWidth, height);

                Bitmap frame = src.Clone(rect, src.PixelFormat);
                frame.Palette = src.Palette;
                frames.Add(frame);
            }

            src.Dispose();
            return frames;
        }

        //
        // run
        //
        public static void ConvertModel(ModelSettings settings, bool debug)
        {
            string path = settings.ModelJsonPath;
            var json = LoadJson(path);
            string saveDirectory = settings.ExportPath;
            string fileName = Path.GetFileNameWithoutExtension(path);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("================================");
            Console.WriteLine("Starting conversion...");
            Console.WriteLine("================================");
            Console.ForegroundColor = ConsoleColor.White;

            string modelName = settings.ModelEID;
            string animName = settings.AnimationEID;
            string tpageName = settings.TpageEID;
            int[] modelScales = settings.ModelScales;
            float[] scaleFactors = settings.ScaleFactor;

            var tex = BuildTPages(json, tpageName);
            List<TextureChunk> tpages = tex.Item1;
            List<PackedTexture> packedTextures = tex.Item2;
            Dictionary<TriangleKey, ModelMaterial> materials = BuildMaterials(json, packedTextures);

            int modelEID = Entry.ENameToEID(modelName);
            int animEID = Entry.ENameToEID(animName);
            ModelEntry model = BuildModelEntry(json, materials, modelEID, tpageName, modelScales, debug);
            AnimationEntry animation = BuildAnimationEntry(json, modelEID, animEID, scaleFactors, debug);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine($"Saving...");
            Console.ForegroundColor = ConsoleColor.White;

            // save model
            byte[] fileBytes = model.Save();
            string savePath = Path.Combine(saveDirectory, $"{fileName}_{modelName}.nsentry");
            File.WriteAllBytes(savePath, fileBytes);
            Console.WriteLine($"    Saved model entry:     {savePath}");

            // save animation
            fileBytes = animation.Save();
            savePath = Path.Combine(saveDirectory, $"{fileName}_{animName}.nsentry");
            File.WriteAllBytes(savePath, fileBytes);
            Console.WriteLine($"    Saved animation entry: {savePath}");

            // save tpages
            for (int i = 0; i < tpages.Count; i++)
            {
                var tpage = tpages[i];
                fileBytes = tpage.Save();
                savePath = Path.Combine(saveDirectory, $"{fileName}_{tpage.EName}.nschunk");
                File.WriteAllBytes(savePath, fileBytes);
                Console.WriteLine($"    Saved texture page:    {savePath}");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("Conversion completed!");
            Console.ForegroundColor = ConsoleColor.White;
            SystemSounds.Asterisk.Play();
        }
    }

    public class TextureAtlasPacker
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
            public TextureInfo Info;
        }

        private struct SkylineNode
        {
            public int X;
            public int Y;
            public int Width;
        }

        private static Dictionary<int, List<SkylineNode>> Skylines = [];

        private static readonly Point AtlasSize = new()
        {
            X = 1024,
            Y = 128
        };
        private static readonly int SegmentWidth = 256;

        private static bool TryPlaceInSegment(
         int seg,
         int w, int h,
         int usableHeight,
         out int localX, out int localY)
        {
            localX = localY = 0;
            var skyline = Skylines[seg];

            int bestY = int.MaxValue;
            int bestX = 0;
            int bestIndex = -1;

            for (int i = 0; i < skyline.Count; i++)
            {
                var n = skyline[i];
                if (w > n.Width) continue;

                int x = n.X;
                int y = n.Y;

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
            AddSkyline(seg, bestIndex, localX, localY + h, w);
            return true;
        }

        private static void AddSkyline(int seg, int index, int x, int y, int width)
        {
            var skyline = Skylines[seg];
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

        public static (List<TextureChunk>, List<PackedTexture>) AllocateTextureAtlas(List<TextureEntry> textures, string tpageName)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("Packing textures into atlas...");
            Console.ForegroundColor = ConsoleColor.White;

            List<TextureChunk> tpages = [];
            List<PackedTexture> packedTextures = [];

            Skylines = [];
            int segmentCount = AtlasSize.X / SegmentWidth;
            for (int i = 0; i < segmentCount; i++)
            {
                Skylines[i] =
                [
                    new SkylineNode { X = 0, Y = 0, Width = SegmentWidth }
                ];
            }

            // sort textures by size
            List<int> sorted = [];
            for (int i = 0; i < textures.Count; i++)
                sorted.Add(i);

            sorted = sorted
                .OrderBy(i => textures[i].Info.AnimCount > 1) // non-animated > animated
                .ThenByDescending(i => textures[i].Width * textures[i].Height) // descending order by area
                .ToList();

            int eid = Entry.ENameToEID(tpageName);
            byte[] header = {
                0x34, 0x12, 0x01, 0x00,
                (byte)(eid & 0xFF), (byte)((eid >> 8) & 0xFF), (byte)((eid >> 16) & 0xFF), (byte)((eid >> 24) & 0xFF),
                0x05, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
            };

            byte[] newchunk = new byte[0x10000];
            Array.Copy(header, 0, newchunk, 0, header.Length);
            TextureChunk tpage = new(newchunk);
            int tpageIndex = 0; // temp

            int _4bppCount = 1; // reserve 1 for tpage header
            int _8bppCount = 0;
            HashSet<string> unique = [];
            foreach (var tex in textures)
            {
                if (unique.Contains(tex.FilePath))
                    continue;
                unique.Add(tex.FilePath);

                if (tex.Bpp == 4)
                    _4bppCount++;
                else if (tex.Bpp == 8)
                    _8bppCount++;
            }

            int curClutX = 1,
                curClutY = 0,
                oldClutX = -1,
                oldClutY = -1;
            int clutYRow = (_4bppCount + 15) / 16;
            int ClutHeight = clutYRow + _8bppCount;
            int usableHeight = AtlasSize.Y - ClutHeight;

            if (textures.Count > 0)
            {
                Console.WriteLine($"4bpp textures: {_4bppCount - 1}, 8bpp textures: {_8bppCount}");
                Console.WriteLine($"CLUT Height: {ClutHeight} rows");
            }

            foreach (int i in sorted)
            {
                var tex = textures[i];
                var info = tex.Info;
                if (info.AnimDelay > 0)
                {
                    // if animated texture with delay, search for a non-delayed texture
                    string baseName = Regex.Replace(tex.Name, @"_[d]\d+", "");
                    bool found = false;
                    foreach (var pt in packedTextures.ToList())
                    {
                        if (pt.Name == baseName &&
                            pt.Info.AnimOffset == info.AnimOffset)
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
                            break;
                        }
                    }

                    if (!found)
                        throw new Exception($"Could not find a non-delayed counterpart for animated texture '{tex.FilePath}'.");

                    continue;
                }

                Point result = new();
                bool placed = false;
                for (int seg = 0; seg < segmentCount && !placed; seg++)
                {
                    if (TryPlaceInSegment(seg, tex.Width, tex.Height, usableHeight, out int lx, out int ly))
                    {
                        int worldX = seg * SegmentWidth + lx;
                        int topY = AtlasSize.Y - (ly + tex.Height);

                        result.X = worldX;
                        result.Y = topY;

                        placed = true;
                    }
                }
                if (!placed)
                    throw new Exception("Atlas overflow (all segments full)");

                // place texture into tpage
                string filePath = tex.FilePath;
                string extension = Path.GetExtension(filePath).ToLower();

                int bpp = tex.Bpp;
                bool addNewClut = true;
                if (info.AnimCount > 0)
                {
                    if (info.AnimOffset < info.AnimCount - 1)
                        addNewClut = false;
                }

                if (bpp == 8)
                {
                    if (oldClutX == -1 || oldClutY == -1)
                    {
                        // save CLUT position
                        oldClutX = curClutX;
                        oldClutY = curClutY;
                    }
                    curClutX = 0;
                    curClutY = clutYRow;
                }
                else
                {
                    if (oldClutX != -1 || oldClutY != -1)
                    {
                        // restore CLUT position
                        curClutX = oldClutX;
                        curClutY = oldClutY;
                        oldClutX = -1;
                        oldClutY = -1;
                    }
                }

                int width = bpp == 8 ? tex.Width / 2 : tex.Width;
                int height = tex.Height;

                int destX = result.X;
                int destY = result.Y;

                tpage.Data = TextureConv.ReplaceTextureFromViewer(tpage.Data, tex.Data, tex.Palette, width, height, destX, destY, addNewClut, bpp, curClutX, curClutY);

                packedTextures.Add(new PackedTexture(
                    index: tex.Index,
                    name: tex.Name,
                    filePath: filePath,
                    bpp: bpp,
                    clutX: curClutX,
                    clutY: curClutY,
                    destX: bpp == 8 ? result.X / 2 : result.X, // restore correct pos
                    destY: destY,
                    w: width,
                    h: height,
                    tpage: tpageIndex,
                    info: info
                ));

                Console.WriteLine($"Allocated texture {Path.GetFileName(tex.FilePath)} ({tex.Name}) at ({result.X,4:d}, {result.Y,3:d}), {width,3:d}x{height,3:d}, CLUT: Y{curClutY,2:d} - X{curClutX,2:d}");

                if (addNewClut)
                {
                    if (bpp == 8)
                    {
                        clutYRow += 1;
                    }
                    else
                    {
                        curClutX += 1;
                        if (curClutX > 15)
                        {
                            curClutX = 0;
                            curClutY += 1;
                        }
                    }
                }
            }

            tpages.Add(tpage);
            return (tpages, packedTextures);
        }
    }
}
