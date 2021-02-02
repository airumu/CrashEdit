using Crash;
using System;
using System.Windows.Forms;

namespace CrashEdit
{
    public partial class FrameBox : UserControl
    {
        private FrameController controller;
        private Frame frame;

        private bool vertexdirty;
        private bool collisiondirty;
        private int vertexindex;
        private int collisionindex;

        public FrameBox(FrameController controller)
        {
            this.controller = controller;
            frame = controller.Frame;
            InitializeComponent();
            UpdateVertice();
            UpdateCollision();
            UpdateOffset();
            UpdateHeaderSize();
            vertexindex = 0;
            collisionindex = 0;
        }

        private void InvalidateNodes()
        {
            controller.InvalidateNode();
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
                lblVerticeIndex.Text = string.Format("{0} / {1}",vertexindex + 1,frame.Vertices.Count);
                cmdPreviousVertice.Enabled = (vertexindex > 0);
                cmdNextVertice.Enabled = (vertexindex < frame.Vertices.Count - 1);
                cmdInsertVertice.Enabled = true;
                cmdRemoveVertice.Enabled = (frame.Vertices.Count > 1);
                lblX.Enabled = true;
                lblY.Enabled = true;
                lblZ.Enabled = true;
                numX.Enabled = true;
                numY.Enabled = true;
                numZ.Enabled = true;
                numX.Value = frame.Vertices[vertexindex].X;
                numY.Value = frame.Vertices[vertexindex].Y;
                numZ.Value = frame.Vertices[vertexindex].Z;
            }
            vertexdirty = false;
        }

        private void cmdPreviousVertice_Click(object sender,EventArgs e)
        {
            vertexindex--;
            UpdateVertice();
        }

        private void cmdNextVertice_Click(object sender,EventArgs e)
        {
            vertexindex++;
            UpdateVertice();
        }

        private void cmdNextAndRemoveVertice_Click(object sender,EventArgs e)
        {
            vertexindex++;
            frame.Vertices.RemoveAt(vertexindex);
            InvalidateNodes();
            UpdateVertice();
        }

        private void cmdInsertVertice_Click(object sender,EventArgs e)
        {
            frame.Vertices.Insert(vertexindex, frame.Vertices[vertexindex]);
            InvalidateNodes();
            UpdateVertice();
        }

        private void cmdRemoveVertice_Click(object sender,EventArgs e)
        {
            frame.Vertices.RemoveAt(vertexindex);
            InvalidateNodes();
            UpdateVertice();
        }

        private void cmdAppendVertice_Click(object sender,EventArgs e)
        {
            vertexindex = frame.Vertices.Count;
            if (frame.Vertices.Count > 0)
            {
                frame.Vertices.Add(frame.Vertices[vertexindex - 1]);
            }
            else
            {
                frame.Vertices[vertexindex] = new FrameVertex(0,0,0);
            }
            InvalidateNodes();
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
                numXG.Value = frame.Collision[collisionindex].XO;
                numYG.Value = frame.Collision[collisionindex].YO;
                numZG.Value = frame.Collision[collisionindex].ZO;
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
            InvalidateNodes();
            UpdateCollision();
            UpdateHeaderSize();
        }

        private void cmdInsertCollision_Click(object sender, EventArgs e)
        {
            frame.Collision.Insert(collisionindex, frame.Collision[collisionindex]);
            frame.HeaderSize = (int)numHeader.Value + 40;
            InvalidateNodes();
            UpdateCollision();
            UpdateHeaderSize();
        }

        private void cmdRemoveCollision_Click(object sender, EventArgs e)
        {
            frame.Collision.RemoveAt(collisionindex);
            frame.HeaderSize = (int)numHeader.Value - 40;
            InvalidateNodes();
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

        private void numX_ValueChanged(object sender,EventArgs e)
        {
            if (!vertexdirty)
            {
                FrameVertex pos = frame.Vertices[vertexindex];
                frame.Vertices[vertexindex] = new FrameVertex((byte)numX.Value, pos.Y, pos.Z);
            }
        }

        private void numY_ValueChanged(object sender,EventArgs e)
        {
            if (!vertexdirty)
            {
                FrameVertex pos = frame.Vertices[vertexindex];
                frame.Vertices[vertexindex] = new FrameVertex(pos.X, (byte)numY.Value, pos.Z);
            }
        }

        private void numZ_ValueChanged(object sender,EventArgs e)
        {
            if (!vertexdirty)
            {
                FrameVertex pos = frame.Vertices[vertexindex];
                frame.Vertices[vertexindex] = new FrameVertex(pos.X, pos.Y, (byte)numZ.Value);
            }
        }

        private void numXOffset_ValueChanged(object sender, EventArgs e)
        {
            frame.XOffset = (short)numXOffset.Value;
        }

        private void numYOffset_ValueChanged(object sender, EventArgs e)
        {
            frame.YOffset = (short)numYOffset.Value;
        }

        private void numZOffset_ValueChanged(object sender, EventArgs e)
        {
            frame.ZOffset = (short)numZOffset.Value;
        }

        private void numX1_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                FrameCollision pos = frame.Collision[collisionindex];
                frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XO, pos.YO, pos.ZO, (int)numX1.Value, pos.Y1, pos.Z1, pos.X2, pos.Y2, pos.Z2);
            }
        }

        private void numY1_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                FrameCollision pos = frame.Collision[collisionindex];
                frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XO, pos.YO, pos.ZO, pos.X1, (int)numY1.Value, pos.Z1, pos.X2, pos.Y2, pos.Z2);
            }
        }

        private void numZ1_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                FrameCollision pos = frame.Collision[collisionindex];
                frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XO, pos.YO, pos.ZO, pos.X1, pos.Y1, (int)numZ1.Value, pos.X2, pos.Y2, pos.Z2);
            }

        }

        private void numX2_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                FrameCollision pos = frame.Collision[collisionindex];
                frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XO, pos.YO, pos.ZO, pos.X1, pos.Y1, pos.Z1, (int)numX2.Value, pos.Y2, pos.Z2);
            }
        }

        private void numY2_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                FrameCollision pos = frame.Collision[collisionindex];
                frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XO, pos.YO, pos.ZO, pos.X1, pos.Y1, pos.Z1, pos.X2, (int)numY2.Value, pos.Z2);
            }
        }

        private void numZ2_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                FrameCollision pos = frame.Collision[collisionindex];
                frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XO, pos.YO, pos.ZO, pos.X1, pos.Y1, pos.Z1, pos.X2, pos.Y2, (int)numZ2.Value);
            }
        }

        private void numXGlobal_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                FrameCollision pos = frame.Collision[collisionindex];
                frame.Collision[collisionindex] = new FrameCollision(pos.U, (int)numXG.Value, pos.YO, pos.ZO, pos.X1, pos.Y1, pos.Z1, pos.X2, pos.Y2, pos.Z2);
            }
        }

        private void numYGlobal_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                FrameCollision pos = frame.Collision[collisionindex];
                frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XO, (int)numYG.Value, pos.ZO, pos.X1, pos.Y1, pos.Z1, pos.X2, pos.Y2, pos.Z2);
            }
        }

        private void numZGlobal_ValueChanged(object sender, EventArgs e)
        {
            if (!collisiondirty)
            {
                FrameCollision pos = frame.Collision[collisionindex];
                frame.Collision[collisionindex] = new FrameCollision(pos.U, pos.XO, pos.YO, (int)numZG.Value, pos.X1, pos.Y1, pos.Z1, pos.X2, pos.Y2, pos.Z2);
            }
        }
    }
}
