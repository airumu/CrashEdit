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

        public readonly Dictionary<string, Material> materials = [];

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

        private void ExportMaterials(string path, string modelname)
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
                material.Value.texture.Save(path + Path.DirectorySeparatorChar + name + ".png");
            }

            writer.Flush();

            // material file finally written, save it to disk too
            File.WriteAllBytes(path + Path.DirectorySeparatorChar + modelname + ".mtl", stream.ToArray());
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

                string modelname = appendIndex ? filename + "_" + i.ToString($"D{count}") : filename;
                var modelObj = convObjects[i];

                writer.WriteLine("# CrashEdit exported model");
                writer.WriteLine("mtllib {0}.mtl", filename);
                writer.WriteLine();
                writer.WriteLine("# Vertices");

                foreach (Vertex vertex in modelObj.vertices)
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

                foreach (Vector2 uv in modelObj.uvs)
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

                foreach (Face face in modelObj.faces.OrderBy(x => x.material))
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
                File.WriteAllBytes(path + Path.DirectorySeparatorChar + modelname + ".obj", stream.ToArray());
            }
        }
    }
}