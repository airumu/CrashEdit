using CrashEdit.CE;
using CrashEdit.Crash;
using OpenTK.Mathematics;

namespace CrashEdit.Exporters
{
    public static class FrameExtensions
    {
        /// <summary>
        /// Crash 1 OldModel
        /// </summary>
        public static void AddFrame_Old(this OBJExporter exporter, NSF nsf, OldFrame frame, bool isColored)
        {
            var model = nsf.GetEntry<OldModelEntry>(frame.ModelEID);
            var offset = new Vector3(frame.XOffset, frame.YOffset, frame.ZOffset) - new Vector3(GameScales.AnimC1);
            var scale = new Vector3(model.ScaleX, model.ScaleY, model.ScaleZ) / (GameScales.ModelC1 * GameScales.AnimC1);

            /*foreach (OldModelStruct str in model.Structs)
            {
                if (str is not OldModelTexture tex)
                    continue;

                if (textureEIDs.ContainsKey(tex.EID))
                    continue;

                textureEIDs[tex.EID] = textureEIDs.Count;
            }*/

            foreach (OldModelPolygon polygon in model.Polygons)
            {
                string material = null;
                Vector2? uv1 = null, uv2 = null, uv3 = null;
                Vector3 c1, c2, c3;
                OldModelStruct str = model.Structs[polygon.TexInfo & 0x7FFF];
                OldFrameVertex ov1 = frame.Vertices[polygon.VertexA / 6];
                OldFrameVertex ov2 = frame.Vertices[polygon.VertexB / 6];
                OldFrameVertex ov3 = frame.Vertices[polygon.VertexC / 6];
                Vector3 v1 = new(ov1.X, ov1.Y, ov1.Z);
                Vector3 v2 = new(ov2.X, ov2.Y, ov2.Z);
                Vector3 v3 = new(ov3.X, ov3.Y, ov3.Z);
                Vector3 color = Vector3.Zero;

                if (str is OldModelTexture t)
                {
                    material = exporter.AddTexture(nsf, model, t, t.EID, out color, out uv1, out uv2, out uv3);
                }
                else if (str is OldSceneryColor c)
                {
                    color = new Vector3(c.R, c.G, c.B) / 255F;
                }

                if (isColored)
                {
                    var vc1 = new Vector3(ov1.R, ov1.G, ov1.B) / 255F;
                    var vc2 = new Vector3(ov2.R, ov2.G, ov2.B) / 255F;
                    var vc3 = new Vector3(ov3.R, ov3.G, ov3.B) / 255F;

                    color *= 2F;

                    c1 = vc1 * color;
                    c2 = vc2 * color;
                    c3 = vc3 * color;
                }
                else
                {
                    c1 = color;
                    c2 = color;
                    c3 = color;
                }

                exporter.AddFace(
                    (v1 + offset) * scale,
                    (v2 + offset) * scale,
                    (v3 + offset) * scale,
                    c1, c2, c3,
                    material,
                    uv1, uv2, uv3
                );
            }
        }

        /// <summary>
        /// Crash 2/3 Model
        /// </summary>
        public static void AddFrame(this OBJExporter exporter, NSF nsf, Frame frame, AnimationEntry anim)
        {
            // TODO: SUPPORT CRASH2 AND CRASH3 PROPER SCALING
            // offset correction is 4f in Crash2, 32f in Crash3
            bool modelautocycle = false;
            int modelforceindex = 0; // TODO: Add an option for the user to choose which one to export?
            List<int> models = [];
            if (anim.IsNew)
            {
                // try to guess if this is a 'one model per frame' animation
                models = GetCrash3ModelList(nsf, anim);
                if (anim.Frames.Count > 1 && anim.Frames.Count == models.Count)
                    modelautocycle = true;
            }
            var model = nsf.GetEntry<ModelEntry>(GetModelEID(anim, frame, models, modelautocycle, modelforceindex));
            var vertices = frame.MakeVertices(model);
            var offset = new Vector3(frame.XOffset, frame.YOffset, frame.ZOffset) / 4F;
            var scale = new Vector3(model.ScaleX, model.ScaleY, model.ScaleZ) / GameScales.ModelC1 / GameScales.AnimC1;

            /*for (int i = 0; i < model.TPAGCount; i++)
            {
                int tpag_eid = model.GetTPAG(i);

                if (textureEIDs.ContainsKey(tpag_eid))
                    continue;

                textureEIDs.Add(tpag_eid, textureEIDs.Count);
            }*/

            // iterate all the triangles, get the texture modes and build information about those
            foreach (var tri in model.Triangles)
            {
                string material = exporter.AddTexture(nsf, tri, model, out var uv1, out var uv2, out var uv3, out _, out bool flip);

                SceneryColor fc1 = model.Colors[tri.Color[!flip ? 0 : 2]];
                SceneryColor fc2 = model.Colors[tri.Color[!flip ? 1 : 1]];
                SceneryColor fc3 = model.Colors[tri.Color[!flip ? 2 : 0]];
                Position fv1 = vertices[tri.Vertex[!flip ? 0 : 2] + frame.SpecialVertexCount];
                Position fv2 = vertices[tri.Vertex[!flip ? 1 : 1] + frame.SpecialVertexCount];
                Position fv3 = vertices[tri.Vertex[!flip ? 2 : 0] + frame.SpecialVertexCount];
                Vector3 v1 = new Vector3(fv1.X, fv1.Z, fv1.Y);
                Vector3 v2 = new Vector3(fv2.X, fv2.Z, fv2.Y);
                Vector3 v3 = new Vector3(fv3.X, fv3.Z, fv3.Y);
                Vector3 c1 = new Vector3(fc1.Red, fc1.Green, fc1.Blue) / 255f;
                Vector3 c2 = new Vector3(fc2.Red, fc2.Green, fc2.Blue) / 255f;
                Vector3 c3 = new Vector3(fc3.Red, fc3.Green, fc3.Blue) / 255f;

                exporter.AddFace(
                    (v1 + offset) * scale,
                    (v2 + offset) * scale,
                    (v3 + offset) * scale,
                    c1, c2, c3,
                    material,
                    uv1, uv2, uv3
                );
            }
        }

        private static List<int> GetCrash3ModelList(NSF nsf, AnimationEntry anim)
        {
            List<int> models = [];
            if (anim != null && anim.IsNew)
            {
                foreach (var gool in nsf.GetEntries<GOOLEntry>())
                {
                    foreach (var group in gool.FrameGroups)
                    {
                        if (group is VertexGroup3 vgroup)
                        {
                            if (anim.EID == vgroup.EID && !models.Contains(vgroup.ModelEID))
                            {
                                models.Add(vgroup.ModelEID);
                            }
                        }
                    }
                }
            }
            return models;
        }

        private static int GetModelEID(AnimationEntry anim, Frame frame, List<int> models, bool modelautocycle, int modelforceindex)
        {
            if (anim.IsNew)
            {
                if (models.Count == 0)
                    return Entry.NullEID;

                if (modelautocycle)
                    return models[anim.Frames.IndexOf(frame) % models.Count];
                else
                    return models[modelforceindex % models.Count];
            }
            return frame.ModelEID;
        }
    }
}