using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public class TextureUtils
    {
        public static Tuple<bool, ModelTexture?> ProcessTextureInfoC2(long currentFrame, int in_tex_id, bool animated, IList<ModelTexture> textures, IList<ModelExtendedTexture> animated_textures)
        {
            if (in_tex_id != 0 || animated)
            {
                ModelTexture? info_temp = null;
                int tex_id = in_tex_id - 1;
                if (animated)
                {
                    if (++tex_id >= animated_textures.Count)
                    {
                        return new(false, null);
                    }
                    var anim = animated_textures[tex_id];
                    // check if it's an untextured polygon
                    if (anim.Offset != 0)
                    {
                        tex_id = anim.Offset - 1;
                        if (anim.IsLOD)
                        {
                            tex_id += anim.LOD0; // we only render closest LOD for now
                        }
                        else
                        {
                            tex_id += (int)((currentFrame / 2 / (1 + anim.Latency) + anim.Delay) & anim.Mask);
                            if (anim.Leap)
                            {
                                anim = animated_textures[++tex_id];
                                tex_id = anim.Offset - 1 + anim.LOD0;
                            }
                        }
                        if (tex_id >= textures.Count)
                        {
                            return new(false, null);
                        }
                        info_temp = textures[tex_id];
                    }
                }
                else
                {
                    if (tex_id >= textures.Count)
                    {
                        return new(false, null);
                    }
                    info_temp = textures[tex_id];
                }
                return new(true, info_temp);
            }
            return new(true, null);
        }
    }

    public class TexInfoUnpacked(int color, int blend, int clutx, int cluty, int? face, int page, int left, int top, int width, int height, int delay)
    {
        public int Color = color;
        public int Blend = blend;
        public int ClutX = clutx;
        public int ClutY = cluty;
        public int? Face = face; // OldModelTexture 'N', no culling
        public int Page = page;
        public int Left = left;
        public int Top = top;
        public int Width = width;
        public int Height = height;
        public int Delay = delay;
    }
}