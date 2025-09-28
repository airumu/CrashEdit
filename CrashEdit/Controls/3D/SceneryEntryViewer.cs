using CrashEdit.Crash;
using OpenTK.Mathematics;

namespace CrashEdit.CE
{
    public class SceneryEntryViewer : BaseSceneryEntryViewer<SceneryEntry>
    {
        private List<SLSTPolygonID>? sortlist;
        private readonly bool is_single_view = false;

        public SceneryEntryViewer(NSF nsf, int world) : base(nsf, world) { is_single_view = true; }

        public SceneryEntryViewer(NSF nsf, IEnumerable<int> worlds) : base(nsf, worlds) { is_single_view = false; }

        protected override void SetWorldOffset(SceneryEntry world)
        {
            if (world.IsSky)
            {
                world_offset = -render.Projection.Trans * GameScales.WorldC1;
                if (world.IsC3)
                    world_offset -= new Vector3(0x2000);
            }
            else
                world_offset = new Vector3(world.XOffset, world.YOffset, world.ZOffset);
        }

        protected void SetSortList(IEnumerable<SLSTPolygonID> sortlist)
        {
            if (sortlist != null)
                this.sortlist = new(sortlist);
            else
                this.sortlist = null;
        }

        protected override IEnumerable<IPosition> CorePositions
        {
            get
            {
                foreach (var world in GetWorlds())
                {
                    if (world == null)
                        continue;
                    Vector3 trans = new Vector3(world.XOffset, world.YOffset, world.ZOffset);
                    foreach (SceneryVertex vertex in world.Vertices)
                    {
                        Vector3 v_trans = (trans + new Vector3(vertex.X, vertex.Y, vertex.Z) * 16) / GameScales.WorldC1;
                        yield return new Position(v_trans.X, v_trans.Y, v_trans.Z);
                    }
                }
            }
        }

        protected override void CollectTPAGs()
        {
            foreach (var world in GetWorlds())
            {
                if (world == null)
                    continue;
                for (int i = 0, m = world.TPAGCount; i < m; ++i)
                {
                    tpages.AddTexturePage(world.GetTPAG(i));
                }
            }
        }

        public int PickVertex(int mouseX, int mouseY)
        {
            float threshold = 0.25f;

            int viewportWidth = this.Width;
            int viewportHeight = this.Height;

            Matrix4 projection = render.Projection.Perspective;
            Matrix4 view = render.Projection.View;

            // Convert screen coordinates to normalized device coordinates (-1 to 1)
            float ndcX = (2.0f * mouseX) / viewportWidth - 1.0f;
            float ndcY = 1.0f - (2.0f * mouseY) / viewportHeight;

            // Unproject to get the ray in world space
            Vector4 rayStartNDC = new Vector4(ndcX, ndcY, -1.0f, 1.0f);
            Vector4 rayEndNDC = new Vector4(ndcX, ndcY, 1.0f, 1.0f);

            Matrix4 invViewProj = Matrix4.Invert(view * projection);

            Vector4 rayStartWorld = Vector4.TransformRow(rayStartNDC, invViewProj);
            Vector4 rayEndWorld = Vector4.TransformRow(rayEndNDC, invViewProj);

            rayStartWorld /= rayStartWorld.W;
            rayEndWorld /= rayEndWorld.W;

            Vector3 rayOrigin = rayStartWorld.Xyz;
            Vector3 rayDirection = (rayEndWorld.Xyz - rayOrigin).Normalized();

            // only pick from the first world in single view mode
            var worlds = GetWorlds();
            var firstWorld = worlds?.FirstOrDefault();
            if (firstWorld == null)
                return -1;

            for (int i = 0; i < firstWorld.Vertices.Count; i++)
            {
                var vert = firstWorld.Vertices[i];
                Vector3 vertWorld = (new Vector3(vert.X, vert.Y, vert.Z) * 16 + world_offset) / GameScales.WorldC1;

                // check whether its behind the camera
                Vector3 toVertex = vertWorld - rayOrigin;
                float t = Vector3.Dot(toVertex, rayDirection);
                if (t < 0)
                    continue;

                Vector3 closestPoint = rayOrigin + rayDirection * t;
                float distance = (vertWorld - closestPoint).Length;

                if (distance <= threshold)                                    
                    return i;                
            }

            return -1;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!is_single_view)
                return;

            if (!render.ShowVertices)
                return;

            var worlds = GetWorlds();
            var firstWorld = worlds?.FirstOrDefault();
            if (firstWorld == null)
                return;

            int vertex = PickVertex(e.X, e.Y);
            if (vertex == -1)
                return;

            firstWorld.SelectedVertex = vertex;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!is_single_view)
                return;

            if (!render.ShowVertices)
                return;

            var worlds = GetWorlds();
            var firstWorld = worlds?.FirstOrDefault();
            if (firstWorld == null)
                return;

            firstWorld.HoveredVertex = PickVertex(e.X, e.Y);
        }

        protected override void RenderWorlds(bool sky)
        {
            // collect valid worlds
            var all_worlds = GetWorlds();
            _vao.TestReallocExtra(all_worlds.Sum(x => x?.IsSky == sky ? (x.Triangles.Count + x.Quads.Count * 2) * 3 : 0));
            _vao3.TestReallocExtra(all_worlds.Sum(x => x?.IsSky == sky ? (x.Triangles.Count + x.Quads.Count * 2) * 3 : 0));


            // render stuff
            if (sortlist == null)
            {
                foreach (var world in all_worlds)
                {
                    if (world == null || world.IsSky != sky)
                        continue;
                    RenderWorld(world);
                }
            }
            else
            {
                SceneryEntry lastworld = null;
                foreach (var poly_id in sortlist)
                {
                    if (poly_id.World >= all_worlds.Count)
                        continue;
                    var world = all_worlds[poly_id.World];
                    if (world == null || world.IsSky != sky)
                        continue;
                    if (world != lastworld)
                    {
                        SetWorldOffset(world);
                        lastworld = world;
                    }
                    if (poly_id.State == 0)
                        RenderTriangle(world, poly_id.ID);
                    else
                        RenderQuad(world, poly_id.ID, poly_id.State);
                }
            }

            RenderPasses();
        }

        protected override void RenderWorld(SceneryEntry world)
        {
            SetWorldOffset(world);
            for (int i = 0; i < world.Triangles.Count; ++i)
            {
                RenderTriangle(world, i);
            }
            for (int i = 0; i < world.Quads.Count; ++i)
            {
                RenderQuad(world, i, 3);
            }
            for (int i = 0; i < world.Vertices.Count; ++i)
            {
                RenderMarker(world, i);
            }
        }

        private void RenderMarker(SceneryEntry world, int index)
        {
            Rgba color = new(255, 255, 255, 128);
            float size = 6f;

            if (is_single_view)
            {
                if (world.HoveredVertex == index)
                {
                    color = new(255, 20, 100, 160);
                    size = 12f;
                }
                if (index == world.SelectedVertex)
                {
                    color = new(255, 0, 0, 255);
                    size = 16f;
                }                
            }

            SceneryVertex vert = world.Vertices[index];
            _vao3.Verts[_vao3.CurVert].trans = (new Vector3(vert.X, vert.Y, vert.Z) * 16 + world_offset) / GameScales.WorldC1;
            _vao3.Verts[_vao3.CurVert].rgba = color;
            _vao3.Verts[_vao3.CurVert].misc.X = size;
            _vao3.CurVert++;
        }

        private void RenderTriangle(SceneryEntry world, int index)
        {
            var tri = world.Triangles[index];
            if (tri.VertexA >= world.Vertices.Count || tri.VertexB >= world.Vertices.Count || tri.VertexC >= world.Vertices.Count)
                return;
            if (!ProcessTextureInfoC2(tri.Texture, tri.Animated, world.Textures, world.AnimatedTextures, out var polygon_texture_info))
                return;
            ref var a = ref _vao.Verts[_vao.CurVert + 0];
            ref var b = ref _vao.Verts[_vao.CurVert + 1];
            ref var c = ref _vao.Verts[_vao.CurVert + 2];
            VertexTexInfo tex = new(); // completely untextured
            if (polygon_texture_info != null)
            {
                var info = polygon_texture_info;
                tex = new(tpages[world.GetTPAG(info.Page)], color: info.ColorMode, blend: info.BlendMode, clutx: info.ClutX, cluty: info.ClutY);
                a.st = new(info.X2, info.Y2);
                b.st = new(info.X1, info.Y1);
                c.st = new(info.X3, info.Y3);

                _vao.BlendModes |= VertexTexInfo.GetBlendMode(info.BlendMode);
            }
            a.tex = tex;
            b.tex = tex;
            c.tex = tex;

            RenderVertex(world, tri.VertexA);
            RenderVertex(world, tri.VertexB);
            RenderVertex(world, tri.VertexC);
        }

        private void RenderQuad(SceneryEntry world, int index, int state)
        {
            var quad = world.Quads[index];
            if (quad.VertexA >= world.Vertices.Count || quad.VertexB >= world.Vertices.Count || quad.VertexC >= world.Vertices.Count || quad.VertexD >= world.Vertices.Count)
                return;
            if (!ProcessTextureInfoC2(quad.Texture, quad.Animated, world.Textures, world.AnimatedTextures, out var polygon_texture_info))
                return;
            ref var a = ref _vao.Verts[_vao.CurVert + 0];
            ref var b = ref _vao.Verts[_vao.CurVert + 1];
            ref var c = ref _vao.Verts[_vao.CurVert + 2];
            ref var d = ref _vao.Verts[_vao.CurVert + 3];
            ref var e = ref _vao.Verts[_vao.CurVert + 4];
            ref var f = ref _vao.Verts[_vao.CurVert + 5];
            VertexTexInfo tex = new(); // completely untextured
            if (polygon_texture_info != null)
            {
                var info = polygon_texture_info;
                tex = new(tpages[world.GetTPAG(info.Page)], color: info.ColorMode, blend: info.BlendMode, clutx: info.ClutX, cluty: info.ClutY);
                a.st = new(info.X2, info.Y2);
                b.st = new(info.X1, info.Y1);
                c.st = new(info.X3, info.Y3);
                d.st = new(info.X2, info.Y2);
                e.st = new(info.X4, info.Y4);
                f.st = new(info.X3, info.Y3);

                _vao.BlendModes |= VertexTexInfo.GetBlendMode(info.BlendMode);
            }
            a.tex = tex;
            b.tex = tex;
            c.tex = tex;
            d.tex = tex;
            e.tex = tex;
            f.tex = tex;

            if (state == 1)
            {
                RenderVertex(world, quad.VertexA);
                RenderVertex(world, quad.VertexB);
                RenderVertex(world, quad.VertexC);
            }
            else if (state == 2)
            {
                b = d;
                RenderVertex(world, quad.VertexA);
                RenderVertex(world, quad.VertexD);
                RenderVertex(world, quad.VertexC);
            }
            else if (state == 3)
            {
                RenderVertex(world, quad.VertexA);
                RenderVertex(world, quad.VertexB);
                RenderVertex(world, quad.VertexC);
                _vao.CopyAttrib(_vao.CurVert - 3); // copy A
                RenderVertex(world, quad.VertexD);
                _vao.CopyAttrib(_vao.CurVert - 3); // copy C
            }
        }

        private void RenderVertex(SceneryEntry world, int index)
        {
            SceneryVertex vert = world.Vertices[index];
            SceneryColor color = world.Colors[vert.Color];
            _vao.Verts[_vao.CurVert].trans = (new Vector3(vert.X, vert.Y, vert.Z) * 16 + world_offset) / GameScales.WorldC1;
            _vao.Verts[_vao.CurVert].rgba = new(color.Red, color.Green, color.Blue, 255);
            _vao.CurVert++;
        }
    }
}
