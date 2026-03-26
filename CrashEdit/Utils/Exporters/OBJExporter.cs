using CrashEdit.CE;
using CrashEdit.Crash;
using OpenTK.Mathematics;
using System.Globalization;

namespace CrashEdit.Exporters
{
    public class OBJExporter
    {
        private const string DEFAULT_MATERIAL = "notex";

        public class Material
        {
            public Vector3 ambient;
            public Vector3 diffuse;
            public Vector3 specular;
            public float highlight;
            public Bitmap? texture;
        }

        public class Face
        {
            public int V1;
            public int V2;
            public int V3;
            public int? V4;
            public string? material;
            public int? UV1;
            public int? UV2;
            public int? UV3;
            public int? UV4;
        }

        public class Vertex
        {
            public Vector3 position;
            public Vector3 color;
        }

        public class ConvObject
        {
            public readonly List<Vertex> vertices = [];
            public readonly List<Face> faces = [];
            public readonly List<Vector2> uvs = [];
        }

        private readonly Dictionary<string, Material> materials = [];

        private readonly List<ConvObject> convObjects = [];
        private int currentIdx;

        public Dictionary<string, Material> Materials => materials;

        public OBJExporter()
        {
            // create a default material for everything that is not textured
            materials[DEFAULT_MATERIAL] = new Material
            {
                ambient = Vector3.One,
                diffuse = Vector3.One,
                highlight = 0.0f,
                specular = Vector3.Zero,
                texture = null
            };
        }

        /// <summary>
        /// Adds an object
        /// </summary>
        public void AddObject()
        {
            convObjects.Add(new());
            currentIdx = convObjects.Count - 1;
        }

        /// <summary>
        /// Adds a material with the given name to the obj
        /// </summary>
        public void AddMaterial(string name, Bitmap texture)
        {
            Material mat = new()
            {
                ambient = Vector3.One,
                diffuse = Vector3.One,
                highlight = 0f,
                specular = Vector3.Zero,
                texture = texture
            };

            materials.TryAdd(name, mat);
        }

        /// <summary>
        /// Adds a new vertex to the output
        /// </summary>
        public void AddVertex(Vector3 position, Vector3 color)
        {
            convObjects[currentIdx].vertices.Add(
                new Vertex
                {
                    position = position,
                    color = color
                }
            );
        }

        /// <summary>
        /// Adds a simple face using the given vertices
        /// </summary>
        public void AddFace(int v1, int v2, int v3, string material = null, Vector2? uv1 = null, Vector2? uv2 = null, Vector2? uv3 = null)
        {
            // add uv coordinates to the lists first
            int? uv1id = null;
            int? uv2id = null;
            int? uv3id = null;

            if (uv1 != uv2 || uv1 != uv3)
                throw new InvalidDataException("UVs must all be null or all have values");

            if (uv1 is not null)
            {
                uv1id = convObjects[currentIdx].uvs.Count;
                uv2id = convObjects[currentIdx].uvs.Count + 1;
                uv3id = convObjects[currentIdx].uvs.Count + 2;

                convObjects[currentIdx].uvs.Add(uv1.Value);
                convObjects[currentIdx].uvs.Add(uv2.Value);
                convObjects[currentIdx].uvs.Add(uv3.Value);
            }

            convObjects[currentIdx].faces.Add(
                new Face
                {
                    material = material ?? DEFAULT_MATERIAL,
                    V1 = v1,
                    V2 = v2,
                    V3 = v3,
                    UV1 = uv1id,
                    UV2 = uv2id,
                    UV3 = uv3id
                }
            );
        }

        /// <summary>
        /// Adds a simple face using the given vertices
        /// </summary>
        public void AddFace(int v1, int v2, int v3, int v4, string material = null, Vector2? uv1 = null, Vector2? uv2 = null, Vector2? uv3 = null, Vector2? uv4 = null)
        {
            // add uv coordinates to the lists first
            int? uv1id = null;
            int? uv2id = null;
            int? uv3id = null;
            int? uv4id = null;

            if (
                (uv1 is null && (uv2 is not null || uv3 is not null || uv4 is not null)) ||
                (uv2 is null && (uv1 is not null || uv3 is not null || uv4 is not null)) ||
                (uv3 is null && (uv1 is not null || uv2 is not null || uv4 is not null)) ||
                (uv4 is null && (uv1 is not null || uv2 is not null || uv3 is not null))
            )
                throw new InvalidDataException("UVs must all be null or all have values");

            if (uv1 is not null)
            {
                uv1id = convObjects[currentIdx].uvs.Count;
                uv2id = convObjects[currentIdx].uvs.Count + 1;
                uv3id = convObjects[currentIdx].uvs.Count + 2;
                uv4id = convObjects[currentIdx].uvs.Count + 3;

                convObjects[currentIdx].uvs.Add(uv1.Value);
                convObjects[currentIdx].uvs.Add(uv2.Value);
                convObjects[currentIdx].uvs.Add(uv3.Value);
                convObjects[currentIdx].uvs.Add(uv4.Value);
            }

            convObjects[currentIdx].faces.Add(
                new Face
                {
                    material = material ?? DEFAULT_MATERIAL,
                    V1 = v1,
                    V2 = v2,
                    V3 = v3,
                    V4 = v4,
                    UV1 = uv1id,
                    UV2 = uv2id,
                    UV3 = uv3id,
                    UV4 = uv4id
                }
            );
        }

        /// <summary>
        /// Creates a new face with it's own vertices and uv coordinates
        /// </summary>
        public void AddFace(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 c1, Vector3 c2, Vector3 c3, string material = null, Vector2? uv1 = null, Vector2? uv2 = null, Vector2? uv3 = null)
        {
            int v1id = convObjects[currentIdx].vertices.Count;
            int v2id = convObjects[currentIdx].vertices.Count + 1;
            int v3id = convObjects[currentIdx].vertices.Count + 2;
            int? uv1id = null;
            int? uv2id = null;
            int? uv3id = null;

            if (
                (uv1 is null && (uv2 is not null || uv3 is not null)) ||
                (uv2 is null && (uv1 is not null || uv3 is not null)) ||
                (uv3 is null && (uv1 is not null || uv2 is not null))
            )
                throw new InvalidDataException("UVs must all be null or all have values");

            if (uv1 is not null)
            {
                uv1id = convObjects[currentIdx].uvs.Count;
                uv2id = convObjects[currentIdx].uvs.Count + 1;
                uv3id = convObjects[currentIdx].uvs.Count + 2;

                convObjects[currentIdx].uvs.Add(uv1.Value);
                convObjects[currentIdx].uvs.Add(uv2.Value);
                convObjects[currentIdx].uvs.Add(uv3.Value);
            }

            convObjects[currentIdx].vertices.Add(
                new Vertex
                {
                    position = v1,
                    color = c1
                }
            );
            convObjects[currentIdx].vertices.Add(
                new Vertex
                {
                    position = v2,
                    color = c2
                }
            );
            convObjects[currentIdx].vertices.Add(
                new Vertex
                {
                    position = v3,
                    color = c3
                }
            );

            convObjects[currentIdx].faces.Add(
                new Face
                {
                    material = material,
                    V1 = v1id,
                    V2 = v2id,
                    V3 = v3id,
                    UV1 = uv1id,
                    UV2 = uv2id,
                    UV3 = uv3id
                }
            );
        }

        /// <summary>
        /// Creates a new face with it's own vertices and uv coordinates
        /// </summary>
        public void AddFace(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Vector3 c1, Vector3 c2, Vector3 c3, Vector3 c4, string material = null, Vector2? uv1 = null, Vector2? uv2 = null, Vector2? uv3 = null, Vector2? uv4 = null)
        {
            int v1id = convObjects[currentIdx].vertices.Count;
            int v2id = convObjects[currentIdx].vertices.Count + 1;
            int v3id = convObjects[currentIdx].vertices.Count + 2;
            int v4id = convObjects[currentIdx].vertices.Count + 3;
            int? uv1id = null;
            int? uv2id = null;
            int? uv3id = null;
            int? uv4id = null;

            if (
                (uv1 is null && (uv2 is not null || uv3 is not null || uv4 is not null)) ||
                (uv2 is null && (uv1 is not null || uv3 is not null || uv4 is not null)) ||
                (uv3 is null && (uv1 is not null || uv2 is not null || uv4 is not null)) ||
                (uv4 is null && (uv1 is not null || uv2 is not null || uv3 is not null))
            )
                throw new InvalidDataException("UVs must all be null or all have values");

            if (uv1 is not null)
            {
                uv1id = convObjects[currentIdx].uvs.Count;
                uv2id = convObjects[currentIdx].uvs.Count + 1;
                uv3id = convObjects[currentIdx].uvs.Count + 2;
                uv4id = convObjects[currentIdx].uvs.Count + 3;

                convObjects[currentIdx].uvs.Add(uv1.Value);
                convObjects[currentIdx].uvs.Add(uv2.Value);
                convObjects[currentIdx].uvs.Add(uv3.Value);
                convObjects[currentIdx].uvs.Add(uv4.Value);
            }

            convObjects[currentIdx].vertices.Add(
                new Vertex
                {
                    position = v1,
                    color = c1
                }
            );
            convObjects[currentIdx].vertices.Add(
                new Vertex
                {
                    position = v2,
                    color = c2
                }
            );
            convObjects[currentIdx].vertices.Add(
                new Vertex
                {
                    position = v3,
                    color = c3
                }
            );
            convObjects[currentIdx].vertices.Add(
                new Vertex
                {
                    position = v4,
                    color = c4
                }
            );

            convObjects[currentIdx].faces.Add(
                new Face
                {
                    material = material,
                    V1 = v1id,
                    V2 = v2id,
                    V3 = v3id,
                    V4 = v4id,
                    UV1 = uv1id,
                    UV2 = uv2id,
                    UV3 = uv3id,
                    UV4 = uv4id
                }
            );
        }

        private void ExportMaterials(string path, string filename)
        {
            // first write all the textures to disk
            // then write the mtl file
            using MemoryStream stream = new();
            using StreamWriter writer = new(stream);

            writer.WriteLine("# CrashEdit exported material");

            // write all the materials
            foreach (KeyValuePair<string, Material> material in materials)
            {
                //string name = Regex.Replace(material.Key, "_d\\d+$", "");
                string name = material.Key;

                writer.WriteLine("newmtl {0}", name);
                writer.WriteLine(
                    "Ka {0} {1} {2}",
                    material.Value.ambient.X.ToString(CultureInfo.InvariantCulture),
                    material.Value.ambient.Y.ToString(CultureInfo.InvariantCulture),
                    material.Value.ambient.Z.ToString(CultureInfo.InvariantCulture)
                );
                writer.WriteLine(
                    "Kd {0} {1} {2}",
                    material.Value.diffuse.X.ToString(CultureInfo.InvariantCulture),
                    material.Value.diffuse.Y.ToString(CultureInfo.InvariantCulture),
                    material.Value.diffuse.Z.ToString(CultureInfo.InvariantCulture)
                );
                writer.WriteLine(
                    "Ks {0} {1} {2}",
                    material.Value.specular.X.ToString(CultureInfo.InvariantCulture),
                    material.Value.specular.Y.ToString(CultureInfo.InvariantCulture),
                    material.Value.specular.Z.ToString(CultureInfo.InvariantCulture)
                );
                writer.WriteLine(
                    "Ns {0}",
                    material.Value.highlight.ToString(CultureInfo.InvariantCulture)
                );

                if (material.Value.texture is null)
                    continue;

                writer.WriteLine("map_Kd {0}.png", name);

                // write the bitmap to a file too
                material.Value.texture.Save(Path.Combine(path, name + ".png"));
            }

            writer.Flush();

            // material file finally written, save it to disk too
            File.WriteAllBytes(Path.Combine(path, filename + ".mtl"), stream.ToArray());
        }

        public void Export(string path, string filename, bool appendIndex)
        {
            // first write the material file
            ExportMaterials(path, filename);

            int count = convObjects.Count.ToString().Length;

            for (int i = 0; i < convObjects.Count; i++)
            {
                using MemoryStream stream = new();
                using StreamWriter writer = new(stream);

                string modelname = filename;
                if (appendIndex)
                    modelname = $"{filename}_{i.ToString($"D{count}")}";
                ConvObject obj = convObjects[i];

                writer.WriteLine("# CrashEdit exported model");
                writer.WriteLine("mtllib {0}.mtl", filename);
                writer.WriteLine();
                writer.WriteLine("# Vertices");

                foreach (Vertex vertex in obj.vertices)
                {
                    writer.WriteLine(
                        "v {0} {1} {2} {3} {4} {5}",
                        vertex.position.X.ToString(CultureInfo.InvariantCulture),
                        vertex.position.Y.ToString(CultureInfo.InvariantCulture),
                        vertex.position.Z.ToString(CultureInfo.InvariantCulture),
                        vertex.color.X.ToString(CultureInfo.InvariantCulture),
                        vertex.color.Y.ToString(CultureInfo.InvariantCulture),
                        vertex.color.Z.ToString(CultureInfo.InvariantCulture)
                    );
                }

                // write any uvs we have
                writer.WriteLine();
                writer.WriteLine("# UVs");

                foreach (Vector2 uv in obj.uvs)
                {
                    writer.WriteLine(
                        "vt {0} {1}",
                        uv.X.ToString(CultureInfo.InvariantCulture), uv.Y.ToString(CultureInfo.InvariantCulture)
                    );
                }

                // finally write the faces
                writer.WriteLine();
                writer.WriteLine("# Faces with textures");

                string lastmaterial = null;

                // by default use the default material
                writer.WriteLine("usemtl {0}", DEFAULT_MATERIAL);

                foreach (Face face in obj.faces.OrderBy(x => x.material))
                {
                    if (lastmaterial != face.material)
                    {
                        writer.WriteLine("usemtl {0}", face.material);

                        lastmaterial = face.material;
                    }

                    // write face information, UVs must all be null or have value
                    // at the same time, so this check is safe
                    if (face.UV1 is null)
                    {
                        if (face.V4 is null)
                        {
                            writer.WriteLine(
                                "f {0} {1} {2}",
                                face.V1 + 1,
                                face.V2 + 1,
                                face.V3 + 1
                            );
                        }
                        else
                        {
                            writer.WriteLine(
                                "f {0} {1} {2} {3}",
                                face.V1 + 1,
                                face.V2 + 1,
                                face.V3 + 1,
                                face.V4 + 1
                            );
                        }
                    }
                    else
                    {
                        if (face.V4 is null)
                        {
                            writer.WriteLine(
                                "f {0}/{3} {1}/{4} {2}/{5}",
                                face.V1 + 1,
                                face.V2 + 1,
                                face.V3 + 1,
                                face.UV1 + 1,
                                face.UV2 + 1,
                                face.UV3 + 1
                            );
                        }
                        else
                        {
                            writer.WriteLine(
                                "f {0}/{4} {1}/{5} {2}/{6} {3}/{7}",
                                face.V1 + 1,
                                face.V2 + 1,
                                face.V3 + 1,
                                face.V4 + 1,
                                face.UV1 + 1,
                                face.UV2 + 1,
                                face.UV3 + 1,
                                face.UV4 + 1
                            );
                        }
                    }
                }

                writer.Flush();

                // obj file ready, write to the destination
                File.WriteAllBytes(Path.Combine(path,  $"{modelname}.obj"), stream.ToArray());
            }
        }
    }

    public class ZoneExporter
    {
        public class CameraInfo
        {
            public Vector3 Pos;
            public (Vector3 Ang1, Vector3 Ang2) Angles;
        }

        public class ZoneInfo
        {
            public Vector3 Min;
            public Vector3 Max;
            public List<List<CameraInfo>> Cameras = [];
        }

        public Dictionary<string, ZoneInfo> Zones = [];

        public ZoneExporter()
        {
        }

        public void AddZone(ZoneEntry zone)
        {
            Vector3 min = new Vector3(zone.X, zone.Y, zone.Z) / GameScales.ZoneC1;
            Vector3 max = new Vector3(zone.Width, zone.Height, zone.Depth) / GameScales.ZoneC1 + min;
            ZoneInfo zoneinfo = new()
            {
                Min = min,
                Max = max
            };

            List<List<CameraInfo>> cameraList = [];
            for (int i = 0; i < zone.CameraCount; i++)
            {
                List<CameraInfo> cameras = [];
                for (int e = 0; e < zone.Entities.Count; e++)
                {
                    Entity entity = zone.Entities[e];
                    if (entity.CameraIndex == i && entity.CameraSubIndex == 0)
                    {
                        Entity entity1 = zone.Entities[e + 1];
                        for (int j = 0; j < entity.Positions.Count; j++)
                        {
                            Vector3 pos = new(entity.Positions[j].X, entity.Positions[j].Y, entity.Positions[j].Z);
                            pos = pos / GameScales.ZoneCameraC1 + min; // min == zonetrans
                            Vector3 angle1 = new(entity1.Positions[j * 2].X, entity1.Positions[j * 2].Y, entity1.Positions[j * 2].Z);
                            Vector3 angle2 = new(entity1.Positions[j * 2 + 1].X, entity1.Positions[j * 2 + 1].Y, entity1.Positions[j * 2 + 1].Z);
                            CameraInfo camerainfo = new()
                            {
                                Pos = pos,
                                Angles = (angle1, angle2)
                            };
                            cameras.Add(camerainfo);
                        }
                    }
                }

                cameraList.Add(cameras);
            }

            zoneinfo.Cameras = cameraList;

            Zones.TryAdd(zone.EName, zoneinfo);
        }

        public void ExportZones(string path, string filename)
        {
            using MemoryStream stream = new();
            using MemoryStream streamCam = new();
            using var writer = new StreamWriter(stream);
            using var writerCam = new StreamWriter(streamCam);
            int vertexOffset = 0;
            int camVertexOffset = 0;

            foreach (KeyValuePair<string, ZoneInfo> zoneinfo in Zones)
            {
                writer.WriteLine($"o {zoneinfo.Key}");

                var zone = zoneinfo.Value;
                var min = zone.Min;
                var max = zone.Max;

                Vector3[] v =
                [
                    new Vector3(min.X, min.Y, min.Z),
                    new Vector3(max.X, min.Y, min.Z),
                    new Vector3(max.X, max.Y, min.Z),
                    new Vector3(min.X, max.Y, min.Z),

                    new Vector3(min.X, min.Y, max.Z),
                    new Vector3(max.X, min.Y, max.Z),
                    new Vector3(max.X, max.Y, max.Z),
                    new Vector3(min.X, max.Y, max.Z),
                ];

                foreach (var p in v)
                {
                    writer.WriteLine($"v {p.X} {p.Y} {p.Z}");
                }

                int[,] edges =
                {
                    {1,2},{2,3},{3,4},{4,1},
                    {5,6},{6,7},{7,8},{8,5},
                    {1,5},{2,6},{3,7},{4,8}
                };

                for (int i = 0; i < edges.GetLength(0); i++)
                {
                    int a = edges[i, 0] + vertexOffset;
                    int b = edges[i, 1] + vertexOffset;
                    writer.WriteLine($"l {a} {b}");
                }

                for (int i = 0; i < zone.Cameras.Count; i++)
                {
                    ExportCameraPath2(writerCam, zone.Cameras[i], zoneinfo.Key, i, ref camVertexOffset);
                }

                vertexOffset += 8;
            }

            writer.Flush();
            writerCam.Flush();

            File.WriteAllBytes(Path.Combine(path, filename + ".obj"), stream.ToArray());
            File.WriteAllBytes(Path.Combine(path, filename + "_Cameras.obj"), streamCam.ToArray());
        }

        private const float ANG2RAD = MathF.PI / 2048f;

        public static void ExportCameraPath(StreamWriter writer, List<CameraInfo> cameras, string zoneName, int index, ref int vertexOffset)
        {
            writer.WriteLine($"o {zoneName}_Camera_{index}");

            for (int i = 0; i < cameras.Count; i++)
            {
                var trans = cameras[i].Pos;
                var angles = cameras[i].Angles;

                // dir 1
                var dir1 = GetDir(angles.Ang1, ANG2RAD);
                var tip1 = trans + dir1;

                writer.WriteLine($"v {trans.X} {trans.Y} {trans.Z}");
                writer.WriteLine($"v {tip1.X} {tip1.Y} {tip1.Z}");

                int baseIndex = vertexOffset + 1;
                writer.WriteLine($"l {baseIndex} {baseIndex + 1}");

                vertexOffset += 2;
            }
        }

        public static void ExportCameraPath2(StreamWriter writer, List<CameraInfo> cameras, string zoneName, int index, ref int vertexOffset)
        {
            writer.WriteLine($"o {zoneName}_Camera_{index}");

            for (int i = 0; i < cameras.Count; i++)
            {
                var trans = cameras[i].Pos;
                var angles = cameras[i].Angles;

                // dir 1
                var dir1 = GetDir(angles.Ang1, ANG2RAD);
                var tip1 = trans + dir1;

                // dir 2
                var dir2 = GetDir(angles.Ang2, ANG2RAD);
                var tip2 = trans + dir2;

                writer.WriteLine($"v {trans.X} {trans.Y} {trans.Z}");
                writer.WriteLine($"v {tip1.X} {tip1.Y} {tip1.Z}");
                writer.WriteLine($"v {tip2.X} {tip2.Y} {tip2.Z}");

                int baseIndex = vertexOffset + 1;
                writer.WriteLine($"l {baseIndex} {baseIndex + 1}");
                writer.WriteLine($"l {baseIndex} {baseIndex + 2}");

                vertexOffset += 3;
            }
        }

        public static void ExportCameraObjects(StreamWriter writer, List<CameraInfo> cameras, string zoneName, int index, ref int vertexOffset)
        {
            for (int i = 0; i < cameras.Count; i++)
            {
                var trans = cameras[i].Pos;
                var angles = cameras[i].Angles;

                // dir 1
                var dir = GetDir(angles.Ang1, ANG2RAD);
                var tip = trans + dir;

                writer.WriteLine($"o {zoneName}_Camera_{index}_{i}");

                writer.WriteLine($"v {trans.X} {trans.Y} {trans.Z}");
                writer.WriteLine($"v {tip.X} {tip.Y} {tip.Z}");

                writer.WriteLine($"l {vertexOffset + 1} {vertexOffset + 2}");

                vertexOffset += 2;
            }
        }

        private static Vector3 GetDir(Vector3 ang, float ang2rad)
        {
            var quat = Quaternion.FromEulerAngles(
                -ang.X * ang2rad,
                -ang.Y * ang2rad,
                -ang.Z * ang2rad
            );

            var mat = Matrix4.CreateFromQuaternion(quat);
            var forward = (mat * new Vector4(0, 0, -1, 1)).Xyz;
            return Vector3.Normalize(forward) * 0.5f;
        }

    }
}