using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using OpenTK.Mathematics;

namespace CrashEdit.CE
{
    public sealed class AnimationEntryViewer : BaseAnimationEntryViewer
    {
        private readonly AnimationRenderer animation_renderer;

        private bool _halfspeed = false;
        private bool _modelautocycle = false;
        private int _modelforceindex = 0;
        private bool _playAnimation = true;
        private int _frameIndex = 0;
        private long _tickCount = 0;

        public AnimationEntryViewer(NSF nsf, int anim_eid, int frame = -1) : base(nsf, anim_eid, frame)
        {
            animation_renderer = new() { TPages = tpages, Render = render };
        }

        protected override IEnumerable<IPosition> CorePositions
        {
            get
            {
                var anim = nsf.GetEntry<AnimationEntry>(animId);
                var frames = anim?.Frames;
                if (frames != null)
                {
                    // try to guess if this is a 'one model per frame' animation
                    if (anim.IsNew && frames.Count > 1 && frames.Count == GetCrash3ModelList(anim).Count)
                        _modelautocycle = true;
                    // guess if it's lerped
                    if (anim.IsLerped(nsf))
                        _halfspeed = true;

                    var usedframes = new List<Frame>();
                    if (animFrame != -1)
                        usedframes.Add(frames[animFrame]);
                    else
                        usedframes.AddRange(frames);

                    foreach (Frame frame in usedframes)
                    {
                        var model = nsf.GetEntry<ModelEntry>(GetModelEID(anim, frame));
                        float mx = 1 / 128f;
                        float my = 1 / 128f;
                        float mz = 1 / 128f;
                        if (model != null)
                        {
                            mx = model.ScaleX / GameScales.ModelC1 / GameScales.AnimC1;
                            my = model.ScaleY / GameScales.ModelC1 / GameScales.AnimC1;
                            mz = model.ScaleZ / GameScales.ModelC1 / GameScales.AnimC1;
                        }
                        var frame_offset = new Position(frame.XOffset / 4f, frame.YOffset / 4f, frame.ZOffset / 4f);
                        var scale = new Position(mx, my, mz);
                        foreach (var vert in frame.MakeVertices(model))
                        {
                            yield return (new Position(vert.X, vert.Z, vert.Y) + frame_offset) * scale;
                        }
                    }
                }
            }
        }

        private List<int> GetCrash3ModelList(AnimationEntry anim)
        {
            List<int> models = new();
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

        private int GetModelEID(AnimationEntry anim, Frame frame)
        {
            if (anim.IsNew)
            {
                var models = GetCrash3ModelList(anim);
                if (models.Count == 0)
                    return Entry.NullEID;

                if (_modelautocycle)
                    return models[anim.Frames.IndexOf(frame) % models.Count];
                else
                    return models[_modelforceindex % models.Count];
            }
            return frame.ModelEID;
        }

        private void RenderVertices()
        {
            var anim = nsf.GetEntry<AnimationEntry>(animId);
            if (anim == null)
                return;

            var uncompressed_verts = animation_renderer.GetUncompressedVerts();
            var all_verts = animation_renderer.GetAllVerts();
            var normalverts_start = 0;

            if (uncompressed_verts != null)
            {
                normalverts_start = uncompressed_verts.Length;
                for (int i = 0; i < uncompressed_verts.Length; ++i)
                {
                    var size = 0.24f;
                    var color = (Rgba)Color4.Magenta;

                    if (i == anim.HoveredVertex)
                    {
                        color = new(255, 20, 100, 160);
                        size = 0.36f;
                    }
                    if (i == anim.SelectedVertex)
                    {
                        color = new(255, 0, 0, 255);
                        size = 0.48f;
                    }

                    AddSprite(uncompressed_verts[i], new Vector2(size), color, OldResources.PointTexture);
                }
            }

            if (all_verts != null)
            {
                for (int i = normalverts_start; i < all_verts.Length; ++i)
                {
                    var size = 0.24f;
                    var color = (Rgba)Color4.White;

                    if (i == anim.HoveredVertex)
                    {
                        color = new(255, 20, 100, 160);
                        size = 0.36f;
                    }
                    if (i == anim.SelectedVertex)
                    {
                        color = new(255, 0, 0, 255);
                        size = 0.48f;
                    }

                    AddSprite(all_verts[i], new Vector2(size), color, OldResources.PointTexture);
                }
            }
        }

        protected override void Render()
        {
            base.Render();

            animation_renderer.Setup(_interpolate, _halfspeed);

            var anim = nsf.GetEntry<AnimationEntry>(animId);
            if (animation_renderer.RenderAnimFrame(new Vector3(0), vaoModel, anim,
                                                   animFrame != -1 ? animFrame * (_halfspeed ? 2 : 1) : render.FullCurrentFrame / 2,
                                                   _playAnimation, _frameIndex,
                                                   x => nsf.GetEntry<ModelEntry>(GetModelEID(anim, x))))
            {
                UploadTPAGs();

                if (render.ShowVertices)
                    RenderVertices();

                vaoModel[0].BlendModes |= animation_renderer.BlendMask;

                RenderPasses();

                if (_collision)
                {
                    foreach (var col in animation_renderer.BaseFrame.Collision)
                    {
                        var c1 = new Vector3(col.X1, col.Y1, col.Z1) / GameScales.CollisionC1;
                        var c2 = new Vector3(col.X2, col.Y2, col.Z2) / GameScales.CollisionC1;
                        var ct = new Vector3(col.XOffset, col.YOffset, col.ZOffset) / GameScales.CollisionC1;
                        var pos = c1 + ct;
                        var size = c2 - c1;
                        AddBox(pos, size, new Rgba(0, 255, 0, 255 / 5), false);
                        AddBox(pos, size, new Rgba(0, 255, 0, 255), true);
                    }
                }
            }
        }

        public int PickVertex(int mouseX, int mouseY)
        {
            int ret = -1;
            var vertices = animation_renderer.GetAllVerts();
            if (vertices == null)
                return ret;

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

            float smallest_dist = threshold;

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 vertWorld = vertices[i];

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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!render.ShowVertices)
                return;

            int vertex = PickVertex(e.X, e.Y);
            if (vertex == -1)
                return;

            var anim = nsf.GetEntry<AnimationEntry>(animId);
            if (anim != null)
                anim.SelectedVertex = vertex;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!render.ShowVertices)
                return;

            var anim = nsf.GetEntry<AnimationEntry>(animId);
            if (anim != null)
                anim.HoveredVertex = PickVertex(e.X, e.Y);
        }

        protected override void PrintDebug()
        {
            base.PrintDebug();

            var anim = nsf.GetEntry<AnimationEntry>(animId);
            if (anim != null)
            {
                var models = GetCrash3ModelList(anim);
                con_debug += $"auto? {_modelautocycle} force: {_modelforceindex}\n";
                for (int i = 0; i < models.Count; ++i)
                {
                    con_debug += $"{i}: {Entry.EIDToEName(models[i])}\n";
                }
            }
        }

        protected override void PrintHelp()
        {
            base.PrintHelp();
            con_help += KeyboardControls.ToggleSlowAnim.Print(OnOffName(_halfspeed));
            con_help += KeyboardControls.ToggleVerticesVisible.Print(OnOffName(render.ShowVertices));
            con_help += KeyboardControls.ToggleAnimation.Print(OnOffName(_playAnimation));

            var anim = nsf.GetEntry<AnimationEntry>(animId);
            if (anim != null)
            {
                if (render.ShowVertices)
                {
                    con_help += "\nLeft/Right to change highlighted vertex\n";
                    con_help += string.Format("Highlighted vertex: {0}", anim.SelectedVertex);
                }

                if (!_playAnimation)
                {
                    con_help += "\nF/G to change frame\n";
                    con_help += string.Format("Frame: {0} / {1}", _frameIndex, anim.Frames.Count - 1);
                }

                if (anim.IsNew)
                {
                    var models = GetCrash3ModelList(anim);
                    if (models.Count > 1)
                    {
                        if (render.ShowVertices || !_playAnimation)
                            con_help += "\n";
                        if (models.Count == anim.Frames.Count)
                            con_help += KeyboardControls.ToggleModelCycle.Print(OnOffName(_modelautocycle));
                        if (!_modelautocycle)
                            con_help += string.Format(Resources.ViewerControls_PickModel, Entry.EIDToEName(models[_modelforceindex % models.Count]));
                    }
                }
            }
        }

        protected override void RunLogic()
        {
            base.RunLogic();
            if (KPress(KeyboardControls.ToggleSlowAnim)) _halfspeed = !_halfspeed;
            if (KPress(KeyboardControls.ToggleAnimation)) _playAnimation = !_playAnimation;

            var anim = nsf.GetEntry<AnimationEntry>(animId);
            if (anim != null)
            {
                if (anim.IsNew)
                {
                    var models = GetCrash3ModelList(anim);
                    if (models.Count > 1)
                    {
                        if (models.Count == anim.Frames.Count)
                            if (KPress(KeyboardControls.ToggleModelCycle)) _modelautocycle = !_modelautocycle;
                        if (!_modelautocycle)
                        {
                            if (KPress(Keys.Left))
                                --_modelforceindex;
                            if (KPress(Keys.Right))
                                ++_modelforceindex;
                            while (_modelforceindex < 0)
                            {
                                _modelforceindex += models.Count;
                            }
                            _modelforceindex = _modelforceindex % models.Count;
                        }
                    }
                }

                if (render.ShowVertices)
                {
                    if (KDown(Keys.Left))
                    {
                        if (Environment.TickCount64 - _tickCount > 200)
                        {
                            anim.SelectedVertex = Math.Max(-1, anim.SelectedVertex - 1);
                            _tickCount = Environment.TickCount64;
                        }
                    }
                    else if (KDown(Keys.Right))
                    {
                        var vertices = animation_renderer.GetAllVerts();
                        if (vertices != null)
                        {
                            if (Environment.TickCount64 - _tickCount > 200)
                            {
                                anim.SelectedVertex = Math.Min(vertices.Length - 1, anim.SelectedVertex + 1);
                                _tickCount = Environment.TickCount64;
                            }
                        }
                    }
                }

                if (!_playAnimation)
                {
                    if (KDown(Keys.F))
                    {
                        if (Environment.TickCount64 - _tickCount > 200)
                        {
                            _frameIndex = Math.Max(0, _frameIndex - 1);
                            _tickCount = Environment.TickCount64;
                        }
                    }
                    else if (KDown(Keys.G))
                    {
                        if (Environment.TickCount64 - _tickCount > 200)
                        {
                            _frameIndex = Math.Min(anim.Frames.Count - 1, _frameIndex + 1);
                            _tickCount = Environment.TickCount64;
                        }
                    }
                }

            }
        }
    }
}
