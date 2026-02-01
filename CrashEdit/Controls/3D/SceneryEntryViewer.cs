using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace CrashEdit.CE
{
    public class SceneryEntryViewer : BaseSceneryEntryViewer<SceneryEntry>
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        // VK_CONTROL, for cases when ctrl-clicking with the window not being focused
        public bool CtrlHeld()
        {
            return KDown(Keys.Control) || (GetAsyncKeyState(0x11) & 0x8000) != 0;
        }

        private List<SLSTPolygonID>? sortlist;
        private readonly bool is_single_view = false;
        private Point _altDragStart = Point.Empty;
        private Point _altDragCurrent = Point.Empty;
        private bool _isAltDragging = false;

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

            int ret = -1;
            float smallest_dist = threshold;
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

                if (distance <= smallest_dist)
                {
                    smallest_dist = distance;
                    ret = i;
                }
            }

            return ret;
        }

        private List<int> PickVerticesInBox(int startX, int startY, int endX, int endY)
        {
            var result = new List<int>();

            int minX = Math.Min(startX, endX);
            int maxX = Math.Max(startX, endX);
            int minY = Math.Min(startY, endY);
            int maxY = Math.Max(startY, endY);

            int viewportWidth = this.Width;
            int viewportHeight = this.Height;

            Matrix4 projection = render.Projection.Perspective;
            Matrix4 view = render.Projection.View;
            Matrix4 invViewProj = Matrix4.Invert(view * projection);

            var worlds = GetWorlds();
            var firstWorld = worlds?.FirstOrDefault();
            if (firstWorld == null)
                return result;

            for (int i = 0; i < firstWorld.Vertices.Count; i++)
            {
                var vert = firstWorld.Vertices[i];
                Vector3 vertWorld = (new Vector3(vert.X, vert.Y, vert.Z) * 16 + world_offset) / GameScales.WorldC1;

                // Project vertex to screen space
                Vector4 vertClip = Vector4.TransformRow(new Vector4(vertWorld, 1.0f), view * projection);
                Vector3 vertNDC = vertClip.Xyz / vertClip.W;

                // Convert NDC to screen coordinates
                int screenX = (int)((vertNDC.X + 1.0f) * 0.5f * viewportWidth);
                int screenY = (int)((1.0f - vertNDC.Y) * 0.5f * viewportHeight);

                // Check if vertex is within box and in front of camera
                if (screenX >= minX && screenX <= maxX && screenY >= minY && screenY <= maxY && vertNDC.Z >= -1.0f && vertNDC.Z <= 1.0f)
                {
                    result.Add(i);
                }
            }

            return result;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (KPress(Keys.Alt))
            {
                if (is_single_view && render.ShowVertices)
                {
                    _isAltDragging = true;
                    _altDragStart = new Point(e.X, e.Y);
                    _altDragCurrent = _altDragStart;
                    return;
                }
            }

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

            void apply_all(int idx, bool add)
            {
                if (add)
                    firstWorld.MassSelectVertices.Add(idx);
                else
                    firstWorld.MassSelectVertices.Add(idx);

                if (!firstWorld.AddColocatedToMult)
                    return;

                Vector3 _pos = new(firstWorld.Vertices[idx].X,
                                   firstWorld.Vertices[idx].Y,
                                   firstWorld.Vertices[idx].Z);

                for (int i = 0; i < firstWorld.Vertices.Count; i++)
                {
                    Vector3 vec_pos = new(firstWorld.Vertices[i].X,
                                          firstWorld.Vertices[i].Y,
                                          firstWorld.Vertices[i].Z);

                    if (vec_pos != _pos)
                        continue;

                    if (add)
                        firstWorld.MassSelectVertices.Add(i);
                    else
                        firstWorld.MassSelectVertices.Remove(i);
                }
            }

            if (CtrlHeld())
            {
                if (firstWorld.MassSelectVertices.Contains(vertex))
                {
                    apply_all(vertex, false);
                    firstWorld.SelectedVertex = firstWorld.MassSelectVertices.ElementAtOrDefault(0);
                }
                else
                {
                    apply_all(vertex, true);
                    firstWorld.SelectedVertex = vertex;
                }
            }
            else
            {
                firstWorld.SelectedVertex = vertex;
                firstWorld.MassSelectVertices.Clear();
                apply_all(vertex, true);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_isAltDragging)
            {
                _altDragCurrent.X = e.X;
                _altDragCurrent.Y = e.Y;
                return;
            }

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

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_isAltDragging)
            {
                _isAltDragging = false;

                if (!is_single_view || !render.ShowVertices)
                    return;

                var worlds = GetWorlds();
                var firstWorld = worlds?.FirstOrDefault();
                if (firstWorld == null)
                    return;

                var selectedVertices = PickVerticesInBox(_altDragStart.X, _altDragStart.Y, e.X, e.Y);
                if (selectedVertices.Count > 0)
                {
                    firstWorld.SelectedVertex = selectedVertices[0];
                    if (!CtrlHeld())
                        firstWorld.MassSelectVertices.Clear();

                    foreach (var x in selectedVertices)
                        firstWorld.MassSelectVertices.Add(x);
                }

                _altDragStart = Point.Empty;
                return;
            }

            base.OnMouseUp(e);
        }

        void RenderAltSelectBox()
        {
            int minX = Math.Min(_altDragStart.X, _altDragCurrent.X);
            int maxX = Math.Max(_altDragStart.X, _altDragCurrent.X);
            int minY = Math.Min(_altDragStart.Y, _altDragCurrent.Y);
            int maxY = Math.Max(_altDragStart.Y, _altDragCurrent.Y);

            int viewportWidth = Math.Max(1, this.Width);
            int viewportHeight = Math.Max(1, this.Height);
            minX = Math.Clamp(minX, 0, viewportWidth);
            maxX = Math.Clamp(maxX, 0, viewportWidth);
            minY = Math.Clamp(minY, 0, viewportHeight);
            maxY = Math.Clamp(maxY, 0, viewportHeight);

            if (maxX > minX && maxY > minY)
            {
                Matrix4 projection = render.Projection.Perspective;
                Matrix4 view = render.Projection.View;
                Matrix4 invViewProj = Matrix4.Invert(view * projection);

                int clear_rgb = Settings.Default.ClearColorRGB;
                double lum = 0.299 * ((clear_rgb >> 16) & 255) + 0.587 * ((clear_rgb >> 8) & 255) + 0.114 * (clear_rgb & 255);
                Rgba lineColor = lum > 186 ? new(0, 0, 0, 200) : new(255, 255, 255, 200);

                // local helper
                static bool TryUnproject(int sx, int sy, int vw, int vh, Matrix4 invVP, float ndcDepth, out Vector3 wp)
                {
                    float ndcX = (2.0f * sx) / vw - 1.0f;
                    float ndcY = 1.0f - (2.0f * sy) / vh;
                    Vector4 clip = new Vector4(ndcX, ndcY, ndcDepth, 1.0f);
                    Vector4 world = Vector4.TransformRow(clip, invVP);
                    if (Math.Abs(world.W) < float.Epsilon) { wp = default; return false; }
                    world /= world.W;
                    wp = world.Xyz;
                    return true;
                }

                if (TryUnproject(minX, minY, viewportWidth, viewportHeight, invViewProj, 0, out var p00) &&
                    TryUnproject(maxX, minY, viewportWidth, viewportHeight, invViewProj, 0, out var p10) &&
                    TryUnproject(maxX, maxY, viewportWidth, viewportHeight, invViewProj, 0, out var p11) &&
                    TryUnproject(minX, maxY, viewportWidth, viewportHeight, invViewProj, 0, out var p01))
                {
                    // top, right, bottom, left
                    vaoLines.PushAttrib(trans: p00, rgba: lineColor);
                    vaoLines.PushAttrib(trans: p10, rgba: lineColor);

                    vaoLines.PushAttrib(trans: p10, rgba: lineColor);
                    vaoLines.PushAttrib(trans: p11, rgba: lineColor);

                    vaoLines.PushAttrib(trans: p11, rgba: lineColor);
                    vaoLines.PushAttrib(trans: p01, rgba: lineColor);

                    vaoLines.PushAttrib(trans: p01, rgba: lineColor);
                    vaoLines.PushAttrib(trans: p00, rgba: lineColor);
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            // ctrl-a
            if (is_single_view && render.ShowVertices && e.KeyCode == Keys.A && CtrlHeld())
            {
                var worlds = GetWorlds();
                var firstWorld = worlds?.FirstOrDefault();
                if (firstWorld == null)
                    return;
                firstWorld.MassSelectVertices.Clear();
                for (int i = 0; i < firstWorld.Vertices.Count; i++)
                    firstWorld.MassSelectVertices.Add(i);
            }
        }

        protected override void RenderWorlds(bool sky)
        {
            // collect valid worlds
            var all_worlds = GetWorlds();
            _vao.TestReallocExtra(all_worlds.Sum(x => x?.IsSky == sky ? (x.Triangles.Count + x.Quads.Count * 2) * 3 : 0));

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

            if (_isAltDragging && is_single_view && render.ShowVertices)
            {
                RenderAltSelectBox();
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
            if (!render.ShowVertices)
                return;

            Rgba color = new(255, 255, 255, 128);
            float size = 0.666f;

            if (is_single_view)
            {
                if (world.MassSelectVertices.Contains(index))
                {
                    color = new(0, 255, 255, 200);
                    size = 0.8f;
                }
                if (index == world.HoveredVertex)
                {
                    color = new(255, 20, 100, 160);
                    size = 1f;
                }
                if (index == world.SelectedVertex)
                {
                    color = new(255, 0, 0, 255);
                    size = 1.333f;
                }
            }

            SceneryVertex vert = world.Vertices[index];
            var trans = (new Vector3(vert.X, vert.Y, vert.Z) * 16 + world_offset) / GameScales.WorldC1;
            AddSprite(trans, new Vector2(size), color, OldResources.PointTexture);
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
