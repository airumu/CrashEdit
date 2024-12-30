using AltUI.Forms;
using CrashEdit.Crash;
using CrashEdit.Crash.GOOLIns;

namespace CrashEdit.CE
{
    public partial class FrameBox : UserControl
    {
        private FrameController controller;
        private Frame frame;
        private ModelEntry model;
        private AnimationEntry animation;

        private bool vertexdirty;
        private bool collisiondirty;
        private int vertexindex;
        private int collisionindex;
        private bool syncedit;

        public FrameBox(FrameController controller, AnimationEntry anim)
        {
            this.controller = controller;
            frame = controller.Frame;
            model = controller.Model;
            animation = anim;
            InitializeComponent();
            UpdateVertice();
            UpdateCollision();
            UpdateOffset();
            UpdateHeaderSize();
            UpdateSPVertex();
            UpdateModel();
            vertexindex = 0;
            collisionindex = 0;
        }

        private void UpdateVertice()
        {
            vertexdirty = true;
            if (vertexindex >= frame.Vertices.Count)
            {
                vertexindex = frame.Vertices.Count - 1;
            }
            // Do not make this else if,
            // sometimes both will run.
            // (this is intentional)
            if (vertexindex < 0)
            {
                vertexindex = 0;
            }
            // Do not remove this either
            if (vertexindex >= frame.Vertices.Count)
            {
                lblVerticeIndex.Text = "-- / --";
                cmdPreviousVertice.Enabled = false;
                cmdNextVertice.Enabled = false;
                cmdInsertVertice.Enabled = false;
                cmdRemoveVertice.Enabled = false;
                lblX.Enabled = false;
                lblY.Enabled = false;
                lblZ.Enabled = false;
                numX.Enabled = false;
                numY.Enabled = false;
                numZ.Enabled = false;
            }
            else
            {
                lblVerticeIndex.Text = string.Format("{0} / {1}", vertexindex + 1, frame.Vertices.Count);
                cmdInsertVertice.Enabled = true;
                cmdRemoveVertice.Enabled = (frame.Vertices.Count > 1);
                lblX.Enabled = true;
                lblY.Enabled = true;
                lblZ.Enabled = true;
                numX.Enabled = true;
                numY.Enabled = true;
                numZ.Enabled = true;
                //numX.Value = frame.Vertices[vertexindex].X;
                //numY.Value = frame.Vertices[vertexindex].Y;
                //numZ.Value = frame.Vertices[vertexindex].Z;
                var verts = frame.MakeVertices(model);
                numX.Value = (decimal)verts[vertexindex].X;
                numY.Value = (decimal)verts[vertexindex].Y;
                numZ.Value = (decimal)verts[vertexindex].Z;
                if (vertexindex <= frame.SpecialVertexCount - 1)
                {
                    lblVerticeIndex.ForeColor = Color.MediumTurquoise;
                    lblSPVertex.Visible = true;
                }
                else
                {
                    lblVerticeIndex.ForeColor = SystemColors.ControlText;
                    lblSPVertex.Visible = false;
                }
            }
            vertexdirty = false;
        }

        private void cmdPreviousVertice_Click(object sender, EventArgs e)
        {
            vertexindex--;
            UpdateVertice();
        }

        private void cmdNextVertice_Click(object sender, EventArgs e)
        {
            vertexindex++;
            UpdateVertice();
        }

        private void CmdPrevious10Vertice_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; ++i)
                vertexindex--;
            UpdateVertice();
        }

        private void CmdNext10Vertice_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; ++i)
                vertexindex++;
            UpdateVertice();
        }

        private void cmdFirstVertice_Click(object sender, EventArgs e)
        {
            vertexindex = 0;
            UpdateVertice();
        }

        private void cmdLastVertice_Click(object sender, EventArgs e)
        {
            vertexindex = frame.Vertices.Count;
            UpdateVertice();
        }

        private void cmdInsertVertice_Click(object sender, EventArgs e)
        {
            frame.Vertices.Insert(vertexindex, frame.Vertices[vertexindex]);
            UpdateVertice();
        }

        private void cmdRemoveVertice_Click(object sender, EventArgs e)
        {
            frame.Vertices.RemoveAt(vertexindex);
            UpdateVertice();
        }

        private void cmdAppendVertice_Click(object sender, EventArgs e)
        {
            vertexindex = frame.Vertices.Count;
            if (frame.Vertices.Count > 0)
            {
                frame.Vertices.Add(frame.Vertices[vertexindex - 1]);
            }
            else
            {
                frame.Vertices[vertexindex] = new FrameVertex(0, 0, 0);
            }
            UpdateVertice();
        }

        private void UpdateCollision()
        {
            collisiondirty = true;
            if (collisionindex >= frame.Collision.Count)
            {
                collisionindex = frame.Collision.Count - 1;
            }
            // Do not make this else if,
            // sometimes both will run.
            // (this is intentional)
            if (collisionindex < 0)
            {
                collisionindex = 0;
            }
            // Do not remove this either
            if (collisionindex >= frame.Collision.Count)
            {
                lblCollisionIndex.Text = "-- / --";
                cmdPreviousCollision.Enabled = false;
                cmdNextCollision.Enabled = false;
                cmdInsertCollision.Enabled = false;
                cmdRemoveCollision.Enabled = false;
                fraG1.Enabled = false;
                fraG2.Enabled = false;
                fraGG.Enabled = false;
            }
            else
            {
                lblCollisionIndex.Text = string.Format("{0} / {1}", collisionindex + 1, frame.Collision.Count);
                cmdPreviousCollision.Enabled = (collisionindex > 0);
                cmdNextCollision.Enabled = (collisionindex < frame.Collision.Count - 1);
                cmdInsertCollision.Enabled = true;
                cmdRemoveCollision.Enabled = true;
                fraG1.Enabled = true;
                numX1.Value = frame.Collision[collisionindex].X1;
                numY1.Value = frame.Collision[collisionindex].Y1;
                numZ1.Value = frame.Collision[collisionindex].Z1;
                fraG2.Enabled = true;
                numX2.Value = frame.Collision[collisionindex].X2;
                numY2.Value = frame.Collision[collisionindex].Y2;
                numZ2.Value = frame.Collision[collisionindex].Z2;
                fraGG.Enabled = true;
                numXG.Value = frame.Collision[collisionindex].XOffset;
                numYG.Value = frame.Collision[collisionindex].YOffset;
                numZG.Value = frame.Collision[collisionindex].ZOffset;
            }
            collisiondirty = false;
        }

        private void cmdPreviousCollision_Click(object sender, EventArgs e)
        {
            collisionindex--;
            UpdateCollision();
        }

        private void cmdNextCollision_Click(object sender, EventArgs e)
        {
            collisionindex++;
            UpdateCollision();
        }

        private void cmdAppendCollision_Click(object sender, EventArgs e)
        {
            collisionindex = frame.Collision.Count;
            if (frame.Collision.Count > 0)
            {
                frame.Collision.Add(frame.Collision[collisionindex - 1]);
                frame.HeaderSize = (int)numXOffset.Value;
                frame.HeaderSize = (int)numHeader.Value + 40;
            }
            else
            {
                frame.Collision.Add(new FrameCollision(0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
                frame.HeaderSize = (int)numXOffset.Value;
                frame.HeaderSize = (int)numHeader.Value + 40;
            }
            UpdateCollision();
            UpdateHeaderSize();
        }

        private void cmdInsertCollision_Click(object sender, EventArgs e)
        {
            frame.Collision.Insert(collisionindex, frame.Collision[collisionindex]);
            frame.HeaderSize = (int)numHeader.Value + 40;
            UpdateCollision();
            UpdateHeaderSize();
        }

        private void cmdRemoveCollision_Click(object sender, EventArgs e)
        {
            frame.Collision.RemoveAt(collisionindex);
            frame.HeaderSize = (int)numHeader.Value - 40;
            UpdateCollision();
            UpdateHeaderSize();
        }

        private void UpdateOffset()
        {
            numXOffset.Value = frame.XOffset;
            numYOffset.Value = frame.YOffset;
            numZOffset.Value = frame.ZOffset;
        }

        private void UpdateHeaderSize()
        {
            numHeader.Value = frame.HeaderSize;
        }

        private void UpdateSPVertex()
        {
            numSPVertex.Value = frame.SpecialVertexCount;
        }

        private void UpdateModel()
        {
            txtModel.Text = Entry.EIDToEName(frame.ModelEID);
        }

        private void numX_ValueChanged(object sender, EventArgs e)
        {
            if (!vertexdirty)
            {
                FrameVertex pos = frame.Vertices[vertexindex];
                frame.Vertices[vertexindex] = new FrameVertex((byte)numX.Value, pos.Y, pos.Z);
            }
        }

        private void numY_ValueChanged(object sender, EventArgs e)
        {
            if (!vertexdirty)
            {
                FrameVertex pos = frame.Vertices[vertexindex];
                frame.Vertices[vertexindex] = new FrameVertex(pos.X, (byte)numY.Value, pos.Z);
            }
        }

        private void numZ_ValueChanged(object sender, EventArgs e)
        {
            if (!vertexdirty)
            {
                FrameVertex pos = frame.Vertices[vertexindex];
                frame.Vertices[vertexindex] = new FrameVertex(pos.X, pos.Y, (byte)numZ.Value);
            }
        }

        private void numXOffset_ValueChanged(object sender, EventArgs e)
        {
            short oldV = frame.XOffset;
            short newV = (short)numXOffset.Value;
            short dif = (short)(newV - oldV);
            if (syncedit)
            {
                foreach (Frame frame in animation.Frames)
                    frame.XOffset += dif;
            }
            else
                frame.XOffset = newV;

        }

        private void numYOffset_ValueChanged(object sender, EventArgs e)
        {
            short oldV = frame.YOffset;
            short newV = (short)numYOffset.Value;
            short dif = (short)(newV - oldV);
            if (syncedit)
            {
                foreach (Frame frame in animation.Frames)
                    frame.YOffset += dif;
            }
            else
                frame.YOffset = newV;
        }

        private void numZOffset_ValueChanged(object sender, EventArgs e)
        {
            short oldV = frame.ZOffset;
            short newV = (short)numZOffset.Value;
            short dif = (short)(newV - oldV);
            if (syncedit)
            {
                foreach (Frame frame in animation.Frames)
                    frame.ZOffset += dif;
            }
            else
                frame.ZOffset = newV;
        }

        private void numX1_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                int oldV = frame.Collision[collisionindex].X1;
                int newV = (int)numX1.Value;
                int dif = newV - oldV;
                if (syncedit)
                {
                    foreach (Frame frame in animation.Frames)
                    {
                        var f = frame.Collision[collisionindex];
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset, f.YOffset, f.ZOffset, f.X1 + dif, f.Y1, f.Z1, f.X2, f.Y2, f.Z2);
                    }
                }
                else
                {
                    FrameCollision pos = frame.Collision[collisionindex];
                    frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XOffset, pos.YOffset, pos.ZOffset, newV, pos.Y1, pos.Z1, pos.X2, pos.Y2, pos.Z2);
                }
            }
        }

        private void numY1_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                int oldV = frame.Collision[collisionindex].Y1;
                int newV = (int)numY1.Value;
                int dif = newV - oldV;
                if (syncedit)
                {
                    foreach (Frame frame in animation.Frames)
                    {
                        var f = frame.Collision[collisionindex];
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset, f.YOffset, f.ZOffset, f.X1, f.Y1 + dif, f.Z1, f.X2, f.Y2, f.Z2);
                    }
                }
                else
                {
                    FrameCollision pos = frame.Collision[collisionindex];
                    frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XOffset, pos.YOffset, pos.ZOffset, pos.X1, newV, pos.Z1, pos.X2, pos.Y2, pos.Z2);
                }
            }
        }

        private void numZ1_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                int oldV = frame.Collision[collisionindex].Z1;
                int newV = (int)numZ1.Value;
                int dif = newV - oldV;
                if (syncedit)
                {
                    foreach (Frame frame in animation.Frames)
                    {
                        var f = frame.Collision[collisionindex];
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset, f.YOffset, f.ZOffset, f.X1, f.Y1, f.Z1 + dif, f.X2, f.Y2, f.Z2);
                    }
                }
                else
                {
                    FrameCollision pos = frame.Collision[collisionindex];
                    frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XOffset, pos.YOffset, pos.ZOffset, pos.X1, pos.Y1, newV, pos.X2, pos.Y2, pos.Z2);
                }
            }
        }

        private void numX2_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                int oldV = frame.Collision[collisionindex].X2;
                int newV = (int)numX2.Value;
                int dif = newV - oldV;
                if (syncedit)
                {
                    foreach (Frame frame in animation.Frames)
                    {
                        var f = frame.Collision[collisionindex];
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset, f.YOffset, f.ZOffset, f.X1, f.Y1, f.Z1, f.X2 + dif, f.Y2, f.Z2);
                    }
                }
                else
                {
                    FrameCollision pos = frame.Collision[collisionindex];
                    frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XOffset, pos.YOffset, pos.ZOffset, pos.X1, pos.Y1, pos.Z1, newV, pos.Y2, pos.Z2);
                }
            }
        }

        private void numY2_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                int oldV = frame.Collision[collisionindex].Y2;
                int newV = (int)numY2.Value;
                int dif = newV - oldV;
                if (syncedit)
                {
                    foreach (Frame frame in animation.Frames)
                    {
                        var f = frame.Collision[collisionindex];
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset, f.YOffset, f.ZOffset, f.X1, f.Y1, f.Z1, f.X2, f.Y2 + dif, f.Z2);
                    }
                }
                else
                {
                    FrameCollision pos = frame.Collision[collisionindex];
                    frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XOffset, pos.YOffset, pos.ZOffset, pos.X1, pos.Y1, pos.Z1, pos.X2, newV, pos.Z2);
                }
            }
        }

        private void numZ2_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                int oldV = frame.Collision[collisionindex].Z2;
                int newV = (int)numZ2.Value;
                int dif = newV - oldV;
                if (syncedit)
                {
                    foreach (Frame frame in animation.Frames)
                    {
                        var f = frame.Collision[collisionindex];
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset, f.YOffset, f.ZOffset, f.X1, f.Y1, f.Z1, f.X2, f.Y2, f.Z2 + dif);
                    }
                }
                else
                {
                    FrameCollision pos = frame.Collision[collisionindex];
                    frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XOffset, pos.YOffset, pos.ZOffset, pos.X1, pos.Y1, pos.Z1, pos.X2, pos.Y2, newV);
                }
            }
        }

        private void numXGlobal_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                int oldV = frame.Collision[collisionindex].XOffset;
                int newV = (int)numXG.Value;
                int dif = newV - oldV;
                if (syncedit)
                {
                    foreach (Frame frame in animation.Frames)
                    {
                        var f = frame.Collision[collisionindex];
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset + dif, f.YOffset, f.ZOffset, f.X1, f.Y1, f.Z1, f.X2, f.Y2, f.Z2);
                    }
                }
                else
                {
                    FrameCollision pos = frame.Collision[collisionindex];
                    frame.Collision[collisionindex] = new FrameCollision(pos.U, newV, pos.YOffset, pos.ZOffset, pos.X1, pos.Y1, pos.Z1, pos.X2, pos.Y2, pos.Z2);
                }
            }
        }

        private void numYGlobal_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                int oldV = frame.Collision[collisionindex].YOffset;
                int newV = (int)numYG.Value;
                int dif = newV - oldV;
                if (syncedit)
                {
                    foreach (Frame frame in animation.Frames)
                    {
                        var f = frame.Collision[collisionindex];
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset, f.YOffset + dif, f.ZOffset, f.X1, f.Y1, f.Z1, f.X2, f.Y2, f.Z2);
                    }
                }
                else
                {
                    FrameCollision pos = frame.Collision[collisionindex];
                    frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XOffset, newV, pos.ZOffset, pos.X1, pos.Y1, pos.Z1, pos.X2, pos.Y2, pos.Z2);
                }
            }
        }

        private void numZGlobal_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                int oldV = frame.Collision[collisionindex].ZOffset;
                int newV = (int)numZG.Value;
                int dif = newV - oldV;
                if (syncedit)
                {
                    foreach (Frame frame in animation.Frames)
                    {
                        var f = frame.Collision[collisionindex];
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset, f.YOffset, f.ZOffset + dif, f.X1, f.Y1, f.Z1, f.X2, f.Y2, f.Z2);
                    }
                }
                else
                {
                    FrameCollision pos = frame.Collision[collisionindex];
                    frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XOffset, pos.YOffset, newV, pos.X1, pos.Y1, pos.Z1, pos.X2, pos.Y2, pos.Z2);
                }
            }
        }

        private void txtModel_TextChanged(object sender, EventArgs e)
        {
            lblEIDError.Text = Entry.CheckEIDErrors(txtModel.Text, true);
            if (lblEIDError.Text != string.Empty) return;
            frame.ModelEID = Entry.ENameToEID(txtModel.Text);
        }

        private void chkSyncFrames_CheckedChanged(object sender, EventArgs e)
        {
            syncedit = chkSyncFrames.Checked;
        }

        private void cmdCopyCollision_Click(object sender, EventArgs e)
        {
            if (DarkMessageBox.ShowMessage("Are you sure you want to copy the current frame's collision values to other frames?", "Confirmation Prompt", DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                FrameCollision pos = frame.Collision[collisionindex];
                foreach (Frame frame in animation.Frames)
                {
                    if (frame.Collision.Count > 0)
                        frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XOffset, pos.YOffset, pos.ZOffset, pos.X1, pos.Y1, pos.Z1, pos.X2, pos.Y2, pos.Z2);
                }
            }
        }

        private void cmdCopyOffset_Click(object sender, EventArgs e)
        {
            if (DarkMessageBox.ShowMessage("Are you sure you want to copy the current frame's offset values to other frames?", "Confirmation Prompt", DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                foreach (Frame frame in animation.Frames)
                {
                    frame.XOffset = (short)numXOffset.Value;
                    frame.YOffset = (short)numYOffset.Value;
                    frame.ZOffset = (short)numZOffset.Value;
                }
            }
        }
    }
}