using CrashEdit.CE;
using CrashEdit.Crash;
using OpenTK.Mathematics;

namespace CrashEdit.Exporters
{
    public static class MaterialExtensions
    {
        private static Vector2 GetUV(float u, int uo, int w, float v, int vo, int h)
        {
            // normalize uv
            return new((u - uo) / w, 1f - (v - vo) / h);
        }

        private static Vector2 AdjustUV(Vector2 uv, float invLen, float offset)
        {
            return new(uv.X * invLen + offset, uv.Y);
        }

        private static void CreateTexture(NSF nsf, dynamic tex, int textureEID, ModelExtendedTexture? animated, ref Dictionary<int, int> textureEIDs,
            out TexInfoUnpacked texinfo, out Bitmap texture)
        {
            int page = textureEIDs[textureEID];
            int? face = tex is OldModelTexture ? Convert.ToInt32(tex.N) : null;
            int delay = animated != null ? animated.Delay : 0;
            texinfo = new(
                true, tex.ColorMode, tex.BlendMode, tex.ClutX, tex.ClutY,
                face, page, tex.Left, tex.Top, tex.Width, tex.Height,
                delay
            );

            TextureChunk? tpage = nsf.GetEntry<TextureChunk>(textureEID);
            texture = TextureExporter.CreateTexture(tpage.Data, texinfo);
        }

        private static string CreateMaterial(this OBJExporter exporter, NSF nsf, dynamic model, dynamic tex, int textureEID, ModelExtendedTexture? animated, ref Dictionary<int, int> textureEIDs, ref Dictionary<string, TexInfoUnpacked> objTranslate)
        {
            string? material = null;
            int page = textureEIDs[textureEID];
            int delay = animated != null ? animated.Delay : 0;

            // add the texture to the list too
            material = objTranslate.FirstOrDefault(x =>
                x.Value.color == tex.ColorMode &&
                x.Value.blend == tex.BlendMode &&
                x.Value.clutx == tex.ClutX &&
                x.Value.cluty == tex.ClutY &&
                x.Value.page == page &&
                x.Value.left == tex.Left &&
                x.Value.top == tex.Top &&
                x.Value.width == tex.Width &&
                x.Value.height == tex.Height &&
                x.Value.delay == delay
            ).Key;

            // ignore the texinfo if there's already a texture with the exact same settings stored
            if (material is null)
            {
                CreateTexture(nsf, tex, textureEID, null, ref textureEIDs, out TexInfoUnpacked texinfo, out Bitmap texture);
                string name = $"{Entry.EIDToEName(textureEID)}x{texinfo.left}y{texinfo.top}cx{texinfo.clutx}cy{texinfo.cluty}c{texinfo.color}b{texinfo.blend}";

                if (tex.BlendMode != 3)
                    name += $"_m{tex.BlendMode}";

                // only process ModelEntry or SceneryEntry for now
                if (animated != null)
                {
                    List<Bitmap> textures = [];

                    if (animated.Leap)
                    {
                        for (int i = 0; i <= animated.Mask; i++)
                        {
                            ModelExtendedTexture animTex = model.AnimatedTextures[animated.Offset + i];
                            tex = model.Textures[animTex.Offset];

                            CreateTexture(nsf, tex, model.GetTPAG(tex.Page), animated, ref textureEIDs, out texinfo, out texture);
                            textures.Add(texture);
                        }
                    }
                    else
                    {
                        for (int i = animated.Offset; i <= animated.Offset + animated.Mask; i++)
                        {
                            tex = model.Textures[i - 1];

                            CreateTexture(nsf, tex, model.GetTPAG(tex.Page), animated, ref textureEIDs, out texinfo, out texture);
                            textures.Add(texture);
                        }
                    }

                    // TODO: Handle cases where animated textures have different sizes, the current method assumes they are the same size
                    texture = TextureExporter.CombineBitmaps(textures);

                    name +=
                        $"_a{animated.Mask + 1}x1" +
                        (animated.Latency > 0 ? $"_s{animated.Latency}" : "") +
                        (animated.Delay > 0 ? $"_d{animated.Delay}" : "");
                }

                // add it to the exporter's material list
                material = exporter.AddTexture(name, texture);

                // add it to the lookup table too
                objTranslate[material] = texinfo;
            }

            return material;
        }

        /// <summary>
        /// Crash 2/3 Model/Scenery
        /// </summary>
        public static string AddTexture(this OBJExporter exporter, NSF nsf, dynamic face, dynamic model,
            ref Dictionary<int, int> textureEIDs, ref Dictionary<string, TexInfoUnpacked> objTranslate,
            out Vector2? uv1, out Vector2? uv2, out Vector2? uv3, out Vector2? uv4, out bool flip)
        {
            string material = null;
            uv1 = uv2 = uv3 = uv4 = null;
            flip = false;

            var info = TextureUtils.ProcessTextureInfoC2(0, face.Texture, face.Animated, model.Textures, model.AnimatedTextures);
            if (info.Item1 && info.Item2 is not null)
            {
                ModelTexture tex = info.Item2;
                ModelExtendedTexture? animated = null;

                if (face.Animated)
                {
                    int animatedOffset = face.Texture;
                    animated = model.AnimatedTextures[animatedOffset];

                    // this is NOT an animated texture!
                    if (animated.IsLOD)
                        animated = null;
                }

                material = CreateMaterial(exporter, nsf, model, tex, model.GetTPAG(tex.Page), animated, ref textureEIDs, ref objTranslate);
                bool isQuad = false;

                if (face is ModelTransformedTriangle tri)
                {
                    bool nocull = tri.Subtype == 0 || tri.Subtype == 2;
                    flip = (tri.Type == 2 ^ tri.Subtype == 3) && !nocull;

                    uv2 = GetUV(tex.X2, tex.Left, tex.Width, tex.Y2, tex.Top, tex.Height);

                    if ((tri.Type != 2 && !flip) || (tri.Type == 2 && tri.Subtype == 1))
                    {
                        uv1 = GetUV(tex.X3, tex.Left, tex.Width, tex.Y3, tex.Top, tex.Height);
                        uv3 = GetUV(tex.X1, tex.Left, tex.Width, tex.Y1, tex.Top, tex.Height);
                    }
                    else
                    {
                        uv3 = GetUV(tex.X3, tex.Left, tex.Width, tex.Y3, tex.Top, tex.Height);
                        uv1 = GetUV(tex.X1, tex.Left, tex.Width, tex.Y1, tex.Top, tex.Height);
                    }
                }
                else if (face is SceneryTriangle)
                {
                    uv1 = GetUV(tex.X2, tex.Left, tex.Width, tex.Y2, tex.Top, tex.Height);
                    uv2 = GetUV(tex.X1, tex.Left, tex.Width, tex.Y1, tex.Top, tex.Height);
                    uv3 = GetUV(tex.X3, tex.Left, tex.Width, tex.Y3, tex.Top, tex.Height);
                }
                else if (face is SceneryQuad)
                {
                    uv1 = GetUV(tex.X2, tex.Left, tex.Width, tex.Y2, tex.Top, tex.Height);
                    uv2 = GetUV(tex.X1, tex.Left, tex.Width, tex.Y1, tex.Top, tex.Height);
                    uv3 = GetUV(tex.X3, tex.Left, tex.Width, tex.Y3, tex.Top, tex.Height);
                    uv4 = GetUV(tex.X4, tex.Left, tex.Width, tex.Y4, tex.Top, tex.Height);
                    isQuad = true;
                }

                // if animated, adjust UVs
                if (animated != null)
                {
                    int len = animated.Mask + 1;
                    float invLen = 1f / len;
                    float offset = invLen * animated.Delay;

                    uv1 = AdjustUV(uv1.Value, invLen, offset);
                    uv2 = AdjustUV(uv2.Value, invLen, offset);
                    uv3 = AdjustUV(uv3.Value, invLen, offset);

                    if (isQuad)
                        uv4 = AdjustUV(uv4.Value, invLen, offset);
                }
            }

            return material;
        }

        /// <summary>
        /// Crash 1 OldModel/OldScenery
        /// </summary>
        public static string AddTexture(this OBJExporter exporter, NSF nsf, dynamic model, dynamic tex, int textureEID,
            ref Dictionary<int, int> textureEIDs, ref Dictionary<string, TexInfoUnpacked> objTranslate,
            out Vector3 color, out Vector2? uv1, out Vector2? uv2, out Vector2? uv3)
        {
            string material = CreateMaterial(exporter, nsf, model, tex, textureEID, null, ref textureEIDs, ref objTranslate);
            color = new Vector3(tex.R, tex.G, tex.B) / 255F;
            uv1 = uv2 = uv3 = null;

            if (tex is OldModelTexture)
            {
                uv1 = GetUV(tex.U1, tex.Left, tex.Width, tex.V1, tex.Top, tex.Height);
                uv2 = GetUV(tex.U2, tex.Left, tex.Width, tex.V2, tex.Top, tex.Height);
                uv3 = GetUV(tex.U3, tex.Left, tex.Width, tex.V3, tex.Top, tex.Height);
            }
            else if (tex is OldSceneryTexture)
            {
                uv1 = GetUV(tex.U3, tex.Left, tex.Width, tex.V3, tex.Top, tex.Height);
                uv2 = GetUV(tex.U2, tex.Left, tex.Width, tex.V2, tex.Top, tex.Height);
                uv3 = GetUV(tex.U1, tex.Left, tex.Width, tex.V1, tex.Top, tex.Height);
            }

            return material;
        }
    }
}