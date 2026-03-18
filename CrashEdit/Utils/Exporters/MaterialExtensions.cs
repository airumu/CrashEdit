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

        private static TexInfoUnpacked GetTexInfo(dynamic tex, int textureEID, ModelExtendedTexture? animated)
        {
            int? face = tex is OldModelTexture ? Convert.ToInt32(tex.N) : null;
            int delay = animated != null ? animated.Delay : 0;
            return new(
                tex.ColorMode, tex.BlendMode, tex.ClutX, tex.ClutY,
                face, textureEID, tex.Left, tex.Top, tex.Width, tex.Height,
                delay
            );
        }

        private static Bitmap CreateTexture(TextureChunk? tpage, TexInfoUnpacked texinfo)
        {
            return TextureExporter.CreateTexture(tpage.Data, texinfo);
        }

        private static string CreateMaterial(this OBJExporter exporter, NSF nsf, dynamic model, dynamic tex, int textureEID, ModelExtendedTexture? animated)
        {
            TexInfoUnpacked texinfo = GetTexInfo(tex, textureEID, null);
            string material = $"tex{Entry.EIDToEName(textureEID)}_x{texinfo.Left}y{texinfo.Top}_w{texinfo.Width}h{texinfo.Height}_cx{texinfo.ClutX}cy{texinfo.ClutY}_c{texinfo.Color}b{texinfo.Blend}";
            if (tex.BlendMode != 3)
            {
                material += $"_m{tex.BlendMode}";
            }
            if (animated != null)
            {
                material +=
                    $"_a{animated.Mask + 1}x1" +
                    (animated.Latency > 0 ? $"_s{animated.Latency}" : "") +
                    (animated.Delay > 0 ? $"_d{animated.Delay}" : "");
            }

            // ignore the texinfo if there's already a texture with the exact same settings stored
            if (!exporter.Materials.ContainsKey(material))
            {
                Bitmap texture;

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

                            textureEID = model.GetTPAG(tex.Page);
                            textures.Add(CreateTexture(nsf.GetEntry<TextureChunk>(textureEID), GetTexInfo(tex, textureEID, animated)));
                        }
                    }
                    else
                    {
                        for (int i = animated.Offset; i <= animated.Offset + animated.Mask; i++)
                        {
                            tex = model.Textures[i - 1];

                            textureEID = model.GetTPAG(tex.Page);
                            textures.Add(CreateTexture(nsf.GetEntry<TextureChunk>(textureEID), GetTexInfo(tex, textureEID, animated)));
                        }
                    }

                    texture = TextureExporter.CombineBitmaps(TextureExporter.NormalizeBitmaps(textures));
                }
                else
                {
                    texture = CreateTexture(nsf.GetEntry<TextureChunk>(textureEID), texinfo);
                }

                // add it to the exporter's material list
                exporter.AddMaterial(material, texture);
            }

            return material;
        }

        /// <summary>
        /// Crash 2/3 Model/Scenery
        /// </summary>
        public static string AddTexture(this OBJExporter exporter, NSF nsf, dynamic face, dynamic model,
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

                material = CreateMaterial(exporter, nsf, model, tex, model.GetTPAG(tex.Page), animated);

                bool isQuad = face is SceneryQuad;
                Vector2 _uv1 = GetUV(tex.X1, tex.Left, tex.Width, tex.Y1, tex.Top, tex.Height);
                Vector2 _uv2 = GetUV(tex.X2, tex.Left, tex.Width, tex.Y2, tex.Top, tex.Height);
                Vector2 _uv3 = GetUV(tex.X3, tex.Left, tex.Width, tex.Y3, tex.Top, tex.Height);
                Vector2? _uv4 = isQuad ? GetUV(tex.X4, tex.Left, tex.Width, tex.Y4, tex.Top, tex.Height) : null;

                if (face is ModelTransformedTriangle tri)
                {
                    bool nocull = tri.Subtype == 0 || tri.Subtype == 2;
                    flip = (tri.Type == 2 ^ tri.Subtype == 3) && !nocull;

                    if ((tri.Type != 2 && !flip) || (tri.Type == 2 && tri.Subtype == 1))
                    {
                        uv1 = _uv3;
                        uv2 = _uv2;
                        uv3 = _uv1;
                    }
                    else
                    {
                        uv1 = _uv1;
                        uv2 = _uv2;
                        uv3 = _uv3;
                    }
                }
                else
                {
                    // face is SceneryTriangle or SceneryQuad
                    uv1 = _uv2;
                    uv2 = _uv1;
                    uv3 = _uv3;
                    if (isQuad)
                        uv4 = _uv4;
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
            out Vector3 color, out Vector2? uv1, out Vector2? uv2, out Vector2? uv3)
        {
            string material = CreateMaterial(exporter, nsf, model, tex, textureEID, null);
            color = new Vector3(tex.R, tex.G, tex.B) / 255F;
            uv1 = uv2 = uv3 = null;
            Vector2 _uv1 = GetUV(tex.U1, tex.Left, tex.Width, tex.V1, tex.Top, tex.Height);
            Vector2 _uv2 = GetUV(tex.U2, tex.Left, tex.Width, tex.V2, tex.Top, tex.Height);
            Vector2 _uv3 = GetUV(tex.U3, tex.Left, tex.Width, tex.V3, tex.Top, tex.Height);

            if (tex is OldModelTexture)
            {
                uv1 = _uv1;
                uv2 = _uv2;
                uv3 = _uv3;
            }
            else if (tex is OldSceneryTexture)
            {
                uv1 = _uv3;
                uv2 = _uv2;
                uv3 = _uv1;
            }

            return material;
        }
    }
}