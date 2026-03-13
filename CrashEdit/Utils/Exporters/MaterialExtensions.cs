using CrashEdit.CE;
using CrashEdit.Crash;
using OpenTK.Mathematics;

namespace CrashEdit.Exporters
{
    // TODO: ALL VERTEX/QUAD CLASSES/STRUCTS SHOULD HAVE A BASE INTERFACE WITH DATA IN COMMON
    // TODO: THAT WOULD MAKE WORKING WITH THEM EASIER FOR THINGS LIKE THESE WHERE YOU ONLY NEED THE DATA
    // TODO: THEY HAVE IN COMMON, BUT CHANGING THAT IS OUT OF THE SCOPE OF THESE COMMITS
    // TODO: BUT THAT WOULD CUT DOWN THE METHODS HERE TO JUST ONE OR TWO
    public static class MaterialExtensions
    {
        private static string GetTexName(int textureEID, TexInfoUnpacked texinfo)
        {
            //return $"{textureEID}-{texinfo.GetHashCode().ToString("X8")}c{texinfo.color}b{texinfo.blend}");
            return $"{Entry.EIDToEName(textureEID)}x{texinfo.left}y{texinfo.top}cx{texinfo.clutx}cy{texinfo.cluty}c{texinfo.color}b{texinfo.blend}";
        }

        /// <summary>
        /// Crash 2/3 Model
        /// </summary>
        public static string AddTexture(this OBJExporter exporter, NSF nsf, ModelTransformedTriangle tri, ModelEntry model, ref Dictionary<int, int> textureEIDs, ref Dictionary<string, TexInfoUnpacked> objTranslate, out Vector2? uv1, out Vector2? uv2, out Vector2? uv3, out bool flip)
        {
            var info = TextureUtils.ProcessTextureInfoC2(0, tri.Texture, tri.Animated, model.Textures, model.AnimatedTextures);
            string material = null;
            uv1 = uv2 = uv3 = null;
            bool nocull = tri.Subtype == 0 || tri.Subtype == 2;
            flip = (tri.Type == 2 ^ tri.Subtype == 3) && !nocull;

            // parse the texture and add it to the exporter
            if (info.Item1 && info.Item2 is not null)
            {
                var value = info.Item2;
                int textureEID = model.GetTPAG(value.Page);
                int page = textureEIDs[textureEID];

                material = objTranslate.FirstOrDefault(x =>
                    x.Value.color == value.ColorMode &&
                    x.Value.blend == value.BlendMode &&
                    x.Value.clutx == value.ClutX &&
                    x.Value.cluty == value.ClutY &&
                    x.Value.page == page
                ).Key;

                // ignore the texinfo if there's already a texture with the exact same settings stored
                if (material is null)
                {
                    var texinfo = new TexInfoUnpacked(
                        true, color: value.ColorMode, blend: value.BlendMode, clutx: value.ClutX, cluty: value.ClutY,
                        page: textureEIDs[textureEID],
                        value.Left, value.Top, value.Width, value.Height
                    );

                    var tpag = nsf.GetEntry<TextureChunk>(textureEIDs.First(x => x.Key == textureEID).Key);

                    Bitmap texture = TextureExporter.CreateTexture(tpag.Data, texinfo);

                    // the material name changes
                    material = exporter.AddTexture(GetTexName(textureEID, texinfo), texture);

                    // add it to the lookup table too
                    objTranslate[material] = texinfo;
                }

                // normalize UVs
                uv2 = new Vector2((value.X2 - value.Left) / value.Width, 1f - (value.Y2 - value.Top) / value.Height);

                if ((tri.Type != 2 && !flip) || (tri.Type == 2 && tri.Subtype == 1))
                {
                    uv1 = new Vector2((value.X3 - value.Left) / value.Width, 1f - (value.Y3 - value.Top) / value.Height);
                    uv3 = new Vector2((value.X1 - value.Left) / value.Width, 1f - (value.Y1 - value.Top) / value.Height);
                }
                else
                {
                    uv3 = new Vector2((value.X3 - value.Left) / value.Width, 1f - (value.Y3 - value.Top) / value.Height);
                    uv1 = new Vector2((value.X1 - value.Left) / value.Width, 1f - (value.Y1 - value.Top) / value.Height);
                }
            }

            return material;
        }

        /// <summary>
        /// Crash 2/3 Scenery (triangle)
        /// </summary>
        public static string AddTexture(this OBJExporter exporter, NSF nsf, SceneryTriangle tri, SceneryEntry scenery, ref Dictionary<int, int> textureEIDs, ref Dictionary<string, TexInfoUnpacked> objTranslate, out Vector2? uv1, out Vector2? uv2, out Vector2? uv3)
        {
            var info = TextureUtils.ProcessTextureInfoC2(0, tri.Texture, tri.Animated, scenery.Textures, scenery.AnimatedTextures);
            string material = null;
            uv1 = uv2 = uv3 = null;

            if (info.Item1 && info.Item2 is not null)
            {
                var value = info.Item2;
                int textureEID = scenery.GetTPAG(value.Page);
                int page = textureEIDs[textureEID];

                material = objTranslate.FirstOrDefault(x =>
                    x.Value.color == value.ColorMode &&
                    x.Value.blend == value.BlendMode &&
                    x.Value.clutx == value.ClutX &&
                    x.Value.cluty == value.ClutY &&
                    x.Value.page == page
                ).Key;

                // ignore the texinfo if there's already a texture with the exact same settings stored
                if (material is null)
                {
                    var texinfo = new TexInfoUnpacked(
                        true, color: value.ColorMode, blend: value.BlendMode, clutx: value.ClutX, cluty: value.ClutY,
                        page: textureEIDs[textureEID],
                        value.Left, value.Top, value.Width, value.Height
                    );

                    var tpag = nsf.GetEntry<TextureChunk>(textureEIDs.First(x => x.Key == textureEID).Key);

                    Bitmap texture = TextureExporter.CreateTexture(tpag.Data, texinfo);

                    // the material name changes
                    material = exporter.AddTexture(GetTexName(textureEID, texinfo), texture);

                    // add it to the lookup table too
                    objTranslate[material] = texinfo;
                }

                // normalize UVs
                uv1 = new Vector2((value.X1 - value.Left) / value.Width, 1f - (value.Y1 - value.Top) / value.Height);
                uv2 = new Vector2((value.X2 - value.Left) / value.Width, 1f - (value.Y2 - value.Top) / value.Height);
                uv3 = new Vector2((value.X3 - value.Left) / value.Width, 1f - (value.Y3 - value.Top) / value.Height);
            }

            return material;
        }

        /// <summary>
        /// Crash 2/3 Scenery (quad)
        /// </summary>
        public static string AddTexture(this OBJExporter exporter, NSF nsf, SceneryQuad quad, SceneryEntry scenery, ref Dictionary<int, int> textureEIDs, ref Dictionary<string, TexInfoUnpacked> objTranslate, out Vector2? uv1, out Vector2? uv2, out Vector2? uv3, out Vector2? uv4)
        {
            var info = TextureUtils.ProcessTextureInfoC2(0, quad.Texture, quad.Animated, scenery.Textures, scenery.AnimatedTextures);
            string material = null;
            uv1 = uv2 = uv3 = uv4 = null;

            if (info.Item1 && info.Item2 is not null)
            {
                var value = info.Item2;
                int textureEID = scenery.GetTPAG(value.Page);
                int page = textureEIDs[textureEID];

                material = objTranslate.FirstOrDefault(x =>
                    x.Value.color == value.ColorMode &&
                    x.Value.blend == value.BlendMode &&
                    x.Value.clutx == value.ClutX &&
                    x.Value.cluty == value.ClutY &&
                    x.Value.page == page
                ).Key;

                // ignore the texinfo if there's already a texture with the exact same settings stored
                if (material is null)
                {
                    var texinfo = new TexInfoUnpacked(
                        true, color: value.ColorMode, blend: value.BlendMode, clutx: value.ClutX, cluty: value.ClutY,
                        page: textureEIDs[textureEID],
                        value.Left, value.Top, value.Width, value.Height
                    );

                    var tpag = nsf.GetEntry<TextureChunk>(textureEIDs.First(x => x.Key == textureEID).Key);

                    Bitmap texture = TextureExporter.CreateTexture(tpag.Data, texinfo);

                    // the material name changes
                    material = exporter.AddTexture(GetTexName(textureEID, texinfo), texture);

                    // add it to the lookup table too
                    objTranslate[material] = texinfo;
                }

                // normalize UVs
                uv1 = new Vector2((value.X2 - value.Left) / value.Width, 1f - (value.Y2 - value.Top) / value.Height);
                uv2 = new Vector2((value.X1 - value.Left) / value.Width, 1f - (value.Y1 - value.Top) / value.Height);
                uv3 = new Vector2((value.X3 - value.Left) / value.Width, 1f - (value.Y3 - value.Top) / value.Height);
                uv4 = new Vector2((value.X4 - value.Left) / value.Width, 1f - (value.Y4 - value.Top) / value.Height);
            }

            return material;
        }

        /// <summary>
        /// Crash 1 OldScenery
        /// </summary>
        public static string AddTexture(this OBJExporter exporter, NSF nsf, OldSceneryTexture t, int textureEID, ref Dictionary<int, int> textureEIDs, ref Dictionary<string, TexInfoUnpacked> objTranslate, out Vector3 color, out Vector2? uv1, out Vector2? uv2, out Vector2? uv3)
        {
            string material = null;
            int page = textureEIDs[textureEID];
            color = new Vector3(t.R, t.G, t.B) / 255F;

            int left = Math.Min(t.U1, Math.Min(t.U2, t.U3));
            int top = Math.Min(t.V1, Math.Min(t.V2, t.V3));
            int width = Math.Max(t.U1, Math.Max(t.U2, t.U3)) - left;
            int height = Math.Max(t.V1, Math.Max(t.V2, t.V3)) - top;

            // add the texture to the list too
            material = objTranslate.FirstOrDefault(x =>
                x.Value.color == t.ColorMode &&
                x.Value.blend == t.BlendMode &&
                x.Value.clutx == t.ClutX &&
                x.Value.cluty == t.ClutY &&
                x.Value.page == page &&
                x.Value.left == left &&
                x.Value.top == top &&
                x.Value.width == width &&
                x.Value.height == height
            ).Key;

            if (material is null)
            {
                var texinfo = new TexInfoUnpacked(
                    true, color: t.ColorMode, blend: t.BlendMode,
                    clutx: t.ClutX, cluty: t.ClutY,
                    page: textureEIDs[textureEID],
                    left, top, width, height
                );

                var tpag = nsf.GetEntry<TextureChunk>(textureEID);
                Bitmap texture = TextureExporter.CreateTexture(tpag.Data, texinfo);

                // the material name changes
                material = exporter.AddTexture(GetTexName(textureEID, texinfo), texture);

                // add it to the lookup table too
                objTranslate[material] = texinfo;
            }

            // normalize UVs
            uv3 = new Vector2((t.U1 - left) / width, 1f - (t.V1 - top) / height);
            uv2 = new Vector2((t.U2 - left) / width, 1f - (t.V2 - top) / height);
            uv1 = new Vector2((t.U3 - left) / width, 1f - (t.V3 - top) / height);

            return material;
        }

        /// <summary>
        /// Crash 1 OldModel
        /// </summary>
        public static string AddTexture(this OBJExporter exporter, NSF nsf, OldModelTexture t, ref Dictionary<int, int> textureEIDs, ref Dictionary<string, TexInfoUnpacked> objTranslate, out Vector3 color, out Vector2? uv1, out Vector2? uv2, out Vector2? uv3)
        {
            string material = null;
            int page = textureEIDs[t.EID];
            color = new Vector3(t.R, t.G, t.B) / 255F;

            int left = Math.Min(t.U1, Math.Min(t.U2, t.U3));
            int top = Math.Min(t.V1, Math.Min(t.V2, t.V3));
            int width = Math.Max(t.U1, Math.Max(t.U2, t.U3)) - left;
            int height = Math.Max(t.V1, Math.Max(t.V2, t.V3)) - top;

            // add the texture to the list too
            material = objTranslate.FirstOrDefault(x =>
                x.Value.color == t.ColorMode &&
                x.Value.blend == t.BlendMode &&
                x.Value.clutx == t.ClutX &&
                x.Value.cluty == t.ClutY &&
                x.Value.page == page &&
                x.Value.left == left &&
                x.Value.top == top &&
                x.Value.width == width &&
                x.Value.height == height
            ).Key;

            if (material is null)
            {
                var texinfo = new TexInfoUnpacked(
                    true, color: t.ColorMode, blend: t.BlendMode,
                    clutx: t.ClutX, cluty: t.ClutY,
                    face: Convert.ToInt32(t.N),
                    page: textureEIDs[t.EID],
                    left, top, width, height
                );

                var tpag = nsf.GetEntry<TextureChunk>(t.EID);
                Bitmap texture = TextureExporter.CreateTexture(tpag.Data, texinfo);

                // the material name changes
                material = exporter.AddTexture(GetTexName(t.EID, texinfo), texture);

                // add it to the lookup table too
                objTranslate[material] = texinfo;
            }

            // normalize UVs
            uv3 = new Vector2((t.U3 - left) / width, 1f - (t.V3 - top) / height);
            uv2 = new Vector2((t.U2 - left) / width, 1f - (t.V2 - top) / height);
            uv1 = new Vector2((t.U1 - left) / width, 1f - (t.V1 - top) / height);

            return material;
        }
    }
}