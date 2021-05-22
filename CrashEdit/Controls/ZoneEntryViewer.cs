using Crash;
using CrashEdit.Properties;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CrashEdit
{
    public sealed class ZoneEntryViewer : SceneryEntryViewer
    {
        private static byte[] stipplea;
        private static byte[] stippleb;

        static ZoneEntryViewer()
        {
            stipplea = new byte[128];
            stippleb = new byte[128];
            for (int i = 0; i < 128; i += 8)
            {
                stipplea[i + 0] = 0x55;
                stipplea[i + 1] = 0x55;
                stipplea[i + 2] = 0x55;
                stipplea[i + 3] = 0x55;
                stipplea[i + 4] = 0xAA;
                stipplea[i + 5] = 0xAA;
                stipplea[i + 6] = 0xAA;
                stipplea[i + 7] = 0xAA;
                stippleb[i + 0] = 0xAA;
                stippleb[i + 1] = 0xAA;
                stippleb[i + 2] = 0xAA;
                stippleb[i + 3] = 0xAA;
                stippleb[i + 4] = 0x55;
                stippleb[i + 5] = 0x55;
                stippleb[i + 6] = 0x55;
                stippleb[i + 7] = 0x55;
            }
        }

        private ZoneEntry entry;
        private ZoneEntry[] linkedentries;
        private CommonZoneEntryViewer common;
        private bool timetrialmode;

        public string EntryName => entry.EName;

        public ZoneEntryViewer(ZoneEntry entry, SceneryEntry[] linkedsceneryentries, TextureChunk[][] texturechunks, ZoneEntry[] linkedentries)
            : base(linkedsceneryentries, texturechunks)
        {
            this.entry = entry;
            this.linkedentries = linkedentries;
            common = new CommonZoneEntryViewer(linkedentries.Length + 1);
            timetrialmode = false;
        }

        protected override IEnumerable<IPosition> CorePositions
        {
            get
            {
                int xoffset = BitConv.FromInt32(entry.Layout, 0);
                int yoffset = BitConv.FromInt32(entry.Layout, 4);
                int zoffset = BitConv.FromInt32(entry.Layout, 8);
                yield return new Position(xoffset, yoffset, zoffset);
                int x2 = BitConv.FromInt32(entry.Layout, 12);
                int y2 = BitConv.FromInt32(entry.Layout, 16);
                int z2 = BitConv.FromInt32(entry.Layout, 20);
                yield return new Position(x2 + xoffset, y2 + yoffset, z2 + zoffset);
                foreach (Entity entity in entry.Entities)
                {
                    if (entry.Entities.IndexOf(entity) % 3 == 0 || entity.ID != null)
                    {
                        foreach (EntityPosition position in entity.Positions)
                        {
                            int x = position.X + xoffset;
                            int y = position.Y + yoffset;
                            int z = position.Z + zoffset;
                            yield return new Position(x, y, z);
                        }
                    }
                }
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            bool? settingsinput = common.IsInputKey(keyData);
            if (settingsinput != null)
                return settingsinput.Value;
            switch (keyData)
            {
                case Keys.O:
                    return true;
                default:
                    return base.IsInputKey(keyData);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            common.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.O:
                    timetrialmode = !timetrialmode;
                    break;
            }
        }

        protected override void RenderObjects()
        {
            GL.Disable(EnableCap.Texture2D);
            GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.RgbScale, 1.0f);
            RenderEntry(entry, ref common.OctreeDisplayLists[0]);
            GL.Enable(EnableCap.PolygonStipple);
            for (int i = 0; i < linkedentries.Length; i++)
            {
                ZoneEntry linkedentry = linkedentries[i];
                if (linkedentry == entry)
                    continue;
                if (linkedentry == null)
                    continue;
                RenderLinkedEntry(linkedentry, ref common.OctreeDisplayLists[i + 1]);
            }
            GL.Disable(EnableCap.PolygonStipple);
            if (common.DeleteLists)
                common.DeleteLists = false;
            GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.RgbScale, 2.0f);
            GL.Enable(EnableCap.Texture2D);
            base.RenderObjects();
        }

        private void RenderEntry(ZoneEntry entry, ref int octreedisplaylist)
        {
            common.CurrentEntry = entry;
            int xoffset = BitConv.FromInt32(entry.Layout, 0);
            int yoffset = BitConv.FromInt32(entry.Layout, 4);
            int zoffset = BitConv.FromInt32(entry.Layout, 8);
            int x2 = BitConv.FromInt32(entry.Layout, 12);
            int y2 = BitConv.FromInt32(entry.Layout, 16);
            int z2 = BitConv.FromInt32(entry.Layout, 20);
            GL.PushMatrix();
            GL.Translate(xoffset, yoffset, zoffset);
            if (common.DeleteLists)
            {
                GL.DeleteLists(octreedisplaylist, 1);
                octreedisplaylist = -1;
            }
            if (common.EnableOctree)
            {
                if (!common.PolygonMode)
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                if (octreedisplaylist == -1)
                {
                    octreedisplaylist = GL.GenLists(1);
                    GL.NewList(octreedisplaylist, ListMode.CompileAndExecute);
                    GL.PushMatrix();
                    int xmax = (ushort)BitConv.FromInt16(entry.Layout, 0x1E);
                    int ymax = (ushort)BitConv.FromInt16(entry.Layout, 0x20);
                    int zmax = (ushort)BitConv.FromInt16(entry.Layout, 0x22);
                    common.RenderOctree(entry.Layout, 0x1C, 0, 0, 0, x2, y2, z2, xmax, ymax, zmax);
                    GL.PopMatrix();
                    GL.EndList();
                }
                else
                {
                    GL.CallList(octreedisplaylist);
                }
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            }
            GL.Color3(Color.White);
            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(x2, 0, 0);
            GL.Vertex3(x2, y2, 0);
            GL.Vertex3(0, y2, 0);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, z2);
            GL.Vertex3(x2, 0, z2);
            GL.Vertex3(x2, y2, z2);
            GL.Vertex3(0, y2, z2);
            GL.Vertex3(0, 0, z2);
            GL.Vertex3(x2, 0, z2);
            GL.Vertex3(x2, 0, 0);
            GL.Vertex3(x2, y2, 0);
            GL.Vertex3(x2, y2, z2);
            GL.Vertex3(0, y2, z2);
            GL.Vertex3(0, y2, 0);
            GL.End();
            GL.Scale(4, 4, 4);
            foreach (Entity entity in entry.Entities)
            {
                if (entity.ID != null)
                {
                    RenderEntity(entity, false);
                }
                else if (entry.Entities.IndexOf(entity) % 3 == 0)
                {
                    RenderEntity(entity, true);
                }
            }
            GL.PopMatrix();
        }

        private void RenderLinkedEntry(ZoneEntry entry, ref int octreedisplaylist)
        {
            common.CurrentEntry = entry;
            int xoffset = BitConv.FromInt32(entry.Layout, 0);
            int yoffset = BitConv.FromInt32(entry.Layout, 4);
            int zoffset = BitConv.FromInt32(entry.Layout, 8);
            int x2 = BitConv.FromInt32(entry.Layout, 12);
            int y2 = BitConv.FromInt32(entry.Layout, 16);
            int z2 = BitConv.FromInt32(entry.Layout, 20);
            GL.PushMatrix();
            GL.Translate(xoffset, yoffset, zoffset);
            if (common.AllEntries)
            {
                if (common.DeleteLists)
                {
                    GL.DeleteLists(octreedisplaylist, 1);
                    octreedisplaylist = -1;
                }
                if (common.EnableOctree)
                {
                    GL.Disable(EnableCap.PolygonStipple);
                    if (!common.PolygonMode)
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                    if (octreedisplaylist == -1)
                    {
                        octreedisplaylist = GL.GenLists(1);
                        GL.NewList(octreedisplaylist, ListMode.CompileAndExecute);
                        GL.PushMatrix();
                        int xmax = (ushort)BitConv.FromInt16(entry.Layout, 0x1E);
                        int ymax = (ushort)BitConv.FromInt16(entry.Layout, 0x20);
                        int zmax = (ushort)BitConv.FromInt16(entry.Layout, 0x22);
                        common.RenderOctree(entry.Layout, 0x1C, 0, 0, 0, x2, y2, z2, xmax, ymax, zmax);
                        GL.PopMatrix();
                        GL.EndList();
                    }
                    else
                    {
                        GL.CallList(octreedisplaylist);
                    }
                    GL.Color3(Color.Cyan);
                    GL.Begin(PrimitiveType.LineStrip);
                    GL.Vertex3(0, 0, 0);
                    GL.Vertex3(x2, 0, 0);
                    GL.Vertex3(x2, y2, 0);
                    GL.Vertex3(0, y2, 0);
                    GL.Vertex3(0, 0, 0);
                    GL.Vertex3(0, 0, z2);
                    GL.Vertex3(x2, 0, z2);
                    GL.Vertex3(x2, y2, z2);
                    GL.Vertex3(0, y2, z2);
                    GL.Vertex3(0, 0, z2);
                    GL.Vertex3(x2, 0, z2);
                    GL.Vertex3(x2, 0, 0);
                    GL.Vertex3(x2, y2, 0);
                    GL.Vertex3(x2, y2, z2);
                    GL.Vertex3(0, y2, z2);
                    GL.Vertex3(0, y2, 0);
                    GL.End();
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                    GL.Enable(EnableCap.PolygonStipple);
                }
            }
            GL.Scale(4, 4, 4);
            foreach (Entity entity in entry.Entities)
            {
                if (entity.ID != null)
                {
                    RenderEntity(entity, false);
                }
                else if (entry.Entities.IndexOf(entity) % 3 == 0)
                {
                    RenderEntity(entity, true);
                }
            }
            GL.PopMatrix();
        }

        private void RenderEntity(Entity entity, bool camera)
        {
            if (camera)
                GL.PolygonStipple(stippleb);
            else
                GL.PolygonStipple(stipplea);
            if (entity.Positions.Count == 1)
            {
                EntityPosition position = entity.Positions[0];
                GL.PushMatrix();
                if (camera)
                    GL.Scale(0.25F, 0.25F, 0.25F);
                GL.Translate(position.X, position.Y, position.Z);
                if (camera)
                    GL.Scale(4, 4, 4);
                switch (entity.Type)
                {
                    case 0x3:
                        if (entity.Subtype.HasValue)
                        {
                            RenderPickup(entity.Subtype.Value);
                        }
                        break;
                    case 0x22:
                        if (entity.Subtype.HasValue)
                        {
                            RenderBox(entity.Subtype.Value, entity.TTC2Type.HasValue ? entity.TTC2Type.Value >> 8 : 0);
                        }
                        break;
                    default:
                        if (camera)
                            GL.Color3(Color.Yellow);
                        else
                            GL.Color3(Color.White);
                        LoadTexture(OldResources.PointTexture);
                        RenderSprite();
                        break;
                }
                GL.PopMatrix();
            }
            else
            {
                if (camera)
                    GL.Color3(Color.Green);
                else
                    GL.Color3(Color.Blue);
                GL.PushMatrix();
                if (camera)
                    GL.Scale(0.25F, 0.25F, 0.25F);
                GL.Begin(PrimitiveType.LineStrip);
                foreach (EntityPosition position in entity.Positions)
                {
                    GL.Vertex3(position.X, position.Y, position.Z);
                }
                GL.End();
                if (camera)
                    GL.Color3(Color.Yellow);
                else
                    GL.Color3(Color.Red);
                LoadTexture(OldResources.PointTexture);
                foreach (EntityPosition position in entity.Positions)
                {
                    GL.PushMatrix();
                    GL.Translate(position.X, position.Y, position.Z);
                    if (camera)
                        GL.Scale(4, 4, 4);
                    RenderSprite();
                    GL.PopMatrix();
                }
                GL.PopMatrix();
            }
        }

        private void RenderSprite()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.PushMatrix();
            GL.Rotate(-rotx,0,1,0);
            GL.Rotate(-roty,1,0,0);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0,0);
            GL.Vertex2(-50,+50);
            GL.TexCoord2(1,0);
            GL.Vertex2(+50,+50);
            GL.TexCoord2(1,1);
            GL.Vertex2(+50,-50);
            GL.TexCoord2(0,1);
            GL.Vertex2(-50,-50);
            GL.End();
            GL.PopMatrix();
            GL.Disable(EnableCap.Texture2D);
        }

        private void RenderPickup(int subtype)
        {
            GL.Translate(0,50,0);
            GL.Color3(Color.White);
            LoadPickupTexture(subtype);
            GL.Enable(EnableCap.Texture2D);
            GL.PushMatrix();
            GL.Rotate(-rotx,0,1,0);
            GL.Rotate(-roty,1,0,0);
            ScalePickup(subtype);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0,0);
            GL.Vertex2(-50,+50);
            GL.TexCoord2(1,0);
            GL.Vertex2(+50,+50);
            GL.TexCoord2(1,1);
            GL.Vertex2(+50,-50);
            GL.TexCoord2(0,1);
            GL.Vertex2(-50,-50);
            GL.End();
            GL.PopMatrix();
            GL.Disable(EnableCap.Texture2D);
        }

        private void RenderBox(int subtype, int timetrialreward)
        {
            GL.Translate(0,50,0);
            GL.Enable(EnableCap.Texture2D);
            GL.Color3(Color.White);
            LoadBoxSideTexture(subtype, timetrialreward);
            GL.PushMatrix();
            GL.Color3(93/128F,93/128F,93/128F);
            RenderBoxFace();
            GL.Rotate(90,0,1,0);
            GL.Color3(51/128F,51/128F,76/128F);
            RenderBoxFace();
            GL.Rotate(90,0,1,0);
            RenderBoxFace();
            GL.Rotate(90,0,1,0);
            GL.Color3(115/128F,115/128F,92/128F);
            RenderBoxFace();
            GL.PopMatrix();
            LoadBoxTopTexture(subtype, timetrialreward);
            GL.PushMatrix();
            GL.Rotate(90,1,0,0);
            GL.Color3(33/128F,33/128F,59/128F);
            RenderBoxFace();
            GL.Rotate(180,1,0,0);
            GL.Color3(115/128F,115/128F,92/128F);
            RenderBoxFace();
            GL.PopMatrix();
            GL.Disable(EnableCap.Texture2D);
        }

        private void RenderBoxFace()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0,0);
            GL.Vertex3(-50,+50,50);
            GL.TexCoord2(1,0);
            GL.Vertex3(+50,+50,50);
            GL.TexCoord2(1,1);
            GL.Vertex3(+50,-50,50);
            GL.TexCoord2(0,1);
            GL.Vertex3(-50,-50,50);
            GL.End();
        }

        private void LoadPickupTexture(int subtype)
        {
            switch (subtype)
            {
                case 5: // Life
                    LoadTexture(OldResources.LifeTexture);
                    break;
                case 6: // Mask
                    LoadTexture(OldResources.MaskTexture);
                    break;
                case 16: // Apple
                    LoadTexture(OldResources.AppleTexture);
                    break;
                default:
                    LoadTexture(OldResources.UnknownPickupTexture);
                    break;
            }
        }

        private void ScalePickup(int subtype)
        {
            switch (subtype)
            {
                case 5: // Life
                case 6: // Mask
                    GL.Scale(1.8f,1.125f,1);
                    break;
                case 16: // Apple
                    GL.Scale(0.675f,0.84375f,1);
                    break;
            }
        }

        private void LoadBoxTopTexture(int subtype, int timetrialreward)
        {
            switch (subtype)
            {
                case 0: // TNT
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxTopTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.TNTBoxTopTexture);
                    break;
                case 2: // Empty
                case 3: // Spring
                case 4: // Continue
                case 6: // Fruit
                case 8: // Life
                case 9: // Doctor
                case 10: // Pickup
                    if (timetrialmode)
                        LoadBoxTopTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.EmptyBoxTexture);
                    break;
                case 5: // Iron
                case 7: // Action
                case 15: // Iron Spring
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxTopTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.IronBoxTexture);
                    break;
                case 18: // Nitro
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxTopTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.NitroBoxTopTexture);
                    break;
                case 23: // Steel
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxTopTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.SteelBoxTexture);
                    break;
                case 24: // Action Nitro
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxTopTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.ActionNitroBoxTopTexture);
                    break;
                default:
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxTopTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.UnknownBoxTopTexture);
                    break;
            }
            if (Settings.Default.UseCustomCrates)
            {
                switch (subtype)
                {
                    case 11: // POW
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxTopTextureTimeTrial(timetrialreward);
                        else
                            LoadTexture(OldResources.POWBoxTopTexture);
                        break;
                    case 12: // Purple
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxTopTextureTimeTrial(timetrialreward);
                        else
                            LoadTexture(OldResources.PurpleBoxTopTexture);
                        break;
                    case 27: // Iron Continue
                    case 28: // Switch OFF
                    case 29: // Switch ON
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxTopTextureTimeTrial(timetrialreward);
                        else
                            LoadTexture(OldResources.IronBoxTexture);
                        break;
                    case 25: // Steel Pickup
                    case 26: // Steel Fruit
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxTopTextureTimeTrial(timetrialreward);
                        else
                            LoadTexture(OldResources.SteelBoxTexture);
                        break;
                    case 30: // Switch Ghost to Red
                    case 32: // Switch Ghost to Green
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxTopTextureTimeTrial(timetrialreward);
                        else
                            LoadTexture(OldResources.SwitchGhostTexture);
                        break;
                    case 31: // Switch Green
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxTopTextureTimeTrial(timetrialreward);
                        else
                            LoadTexture(OldResources.SwitchGreenBoxTexture);
                        break;
                    case 33: // Switch Red
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxTopTextureTimeTrial(timetrialreward);
                        else
                            LoadTexture(OldResources.SwitchRedBoxTexture);
                        break;
                }
            }
        }

        private void LoadBoxTopTextureTimeTrial(int timetrialreward)
        {
            switch (timetrialreward)
            {
                case 1: // Time 1
                    LoadTexture(OldResources.TimeBoxTopTexture);
                    break;
                case 2: // Time 2
                    LoadTexture(OldResources.TimeBoxTopTexture);
                    break;
                case 3: // Time 3
                    LoadTexture(OldResources.TimeBoxTopTexture);
                    break;
                case 5: // TNT
                    LoadTexture(OldResources.TNTBoxTopTexture);
                    break;
                case 6: // Nitro
                    LoadTexture(OldResources.NitroBoxTopTexture);
                    break;
                case 7: // POW
                    LoadTexture(OldResources.POWBoxTopTexture);
                    break;
                case 9: // Action
                case 10: // Iron
                case 11: // Iron arrow
                    LoadTexture(OldResources.IronBoxTexture);
                    break;
                default: // Empty
                    LoadTexture(OldResources.EmptyBoxTexture);
                    break;
            }
        }

        private void LoadBoxSideTexture(int subtype, int timetrialreward)
        {
            switch (subtype)
            {
                case 0: // TNT
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxSideTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.TNTBoxTexture);
                    break;
                case 2: // Empty
                    if (timetrialmode)
                        LoadBoxSideTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.EmptyBoxTexture);
                    break;
                case 3: // Spring
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxSideTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.SpringBoxTexture);
                    break;
                case 4: // Continue
                    if (timetrialmode)
                        LoadBoxSideTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.ContinueBoxTexture);
                    break;
                case 5: // Iron
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxSideTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.IronBoxTexture);
                    break;
                case 6: // Fruit
                    if (timetrialmode)
                        LoadBoxSideTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.FruitBoxTexture);
                    break;
                case 7: // Action
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxSideTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.ActionBoxTexture);
                    break;
                case 8: // Life
                    if (timetrialmode)
                        LoadBoxSideTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.LifeBoxTexture);
                    break;
                case 9: // Doctor
                    if (timetrialmode)
                        LoadBoxSideTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.DoctorBoxTexture);
                    break;
                case 10: // Pickup
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxSideTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.PickupBoxTexture);
                    break;
                case 13: // Ghost
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxSideTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.UnknownBoxTopTexture);
                    break;
                case 15: // Iron Spring
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxSideTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.IronSpringBoxTexture);
                    break;
                case 18: // Nitro
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxSideTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.NitroBoxTexture);
                    break;
                case 23: // Steel
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxSideTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.SteelBoxTexture);
                    break;
                case 24: // Action Nitro
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxSideTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.ActionNitroBoxTexture);
                    break;
                default:
                    if (timetrialmode && timetrialreward != 0)
                        LoadBoxSideTextureTimeTrial(timetrialreward);
                    else
                        LoadTexture(OldResources.UnknownBoxTexture);
                    break;
            }
            if (Settings.Default.UseCustomCrates)
            {
                switch (subtype)
                {
                    case 11: // POW
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxSideTextureTimeTrial(timetrialreward);
                        else
                            LoadTexture(OldResources.POWBoxTexture);
                        break;
                    case 12: // Purple
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxSideTextureTimeTrial(timetrialreward);
                        else
                            LoadTexture(OldResources.PurpleBoxTexture);
                        break;
                    case 25: // Steel Pickup
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxSideTextureTimeTrial(timetrialreward);
                        else
                            LoadTexture(OldResources.SteelPickupBoxTexture);
                        break;
                    case 26: // Steel Fruit
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxSideTextureTimeTrial(timetrialreward);
                        else if (timetrialmode)
                            LoadTexture(OldResources.SteelBoxTexture);
                        else
                            LoadTexture(OldResources.SteelFruitBoxTexture);
                        break;
                    case 27: // Iron Continue
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxSideTextureTimeTrial(timetrialreward);
                        else if (timetrialmode)
                            LoadTexture(OldResources.IronBoxTexture);
                        else
                            LoadTexture(OldResources.IronContinueBoxTexture);
                        break;
                    case 28: // Switch OFF
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxSideTextureTimeTrial(timetrialreward);
                        else
                            LoadTexture(OldResources.SwitchOFFBoxTexture);
                        break;
                    case 29: // Switch ON
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxSideTextureTimeTrial(timetrialreward);
                        else
                            LoadTexture(OldResources.SwitchONBoxTexture);
                        break;
                    case 30: // Switch Ghost to Red
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxSideTextureTimeTrial(timetrialreward);
                        else
                            LoadTexture(OldResources.SwitchGhostToRedBoxTexture);
                        break;
                    case 31: // Switch Green
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxSideTextureTimeTrial(timetrialreward);
                        else
                            LoadTexture(OldResources.SwitchGreenBoxTexture);
                        break;
                    case 32: // Switch Ghost to Green
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxSideTextureTimeTrial(timetrialreward);
                        else
                            LoadTexture(OldResources.SwitchGhostToGreenBoxTexture);
                        break;
                    case 33: // Switch Red
                        if (timetrialmode && timetrialreward != 0)
                            LoadBoxSideTextureTimeTrial(timetrialreward);
                        else
                            LoadTexture(OldResources.SwitchRedBoxTexture);
                        break;
                }
            }
        }

        private void LoadBoxSideTextureTimeTrial(int timetrialreward)
        {
            switch (timetrialreward)
            {
                case 4: // Doctor
                    LoadTexture(OldResources.DoctorBoxTexture);
                    break;
                case 1: // Time 1
                    LoadTexture(OldResources.Time1BoxTexture);
                    break;
                case 2: // Time 2
                    LoadTexture(OldResources.Time2BoxTexture);
                    break;
                case 3: // Time 3
                    LoadTexture(OldResources.Time3BoxTexture);
                    break;
                case 5: // TNT
                    LoadTexture(OldResources.TNTBoxTexture);
                    break;
                case 6: // Nitro
                    LoadTexture(OldResources.NitroBoxTexture);
                    break;
                case 7: // POW
                    LoadTexture(OldResources.POWBoxTexture);
                    break;
                case 9: // Action
                    LoadTexture(OldResources.ActionBoxTexture);
                    break;
                case 10: // Iron
                    LoadTexture(OldResources.IronBoxTexture);
                    break;
                case 11: // Iron arrow
                    LoadTexture(OldResources.IronSpringBoxTexture);
                    break;
                default: // Empty
                    LoadTexture(OldResources.EmptyBoxTexture);
                    break;
            }
        }
    }
}
