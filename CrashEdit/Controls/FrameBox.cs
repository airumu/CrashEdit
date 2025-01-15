using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using MetroSet_UI.Controls;

namespace CrashEdit.CE
{
    public partial class FrameBox : UserControl
    {
        private FrameController controller;
        private AnimationEntry animationEntry;
        private Frame frame;
        private ModelEntry? model;

        SplitContainer pnSplit;

        private bool vertexdirty;
        private bool collisiondirty;
        private bool syncedit;
        private int vertexindex = 0;
        private int collisionindex = 0;

        public FrameBox(FrameController controller)
        {
            this.controller = controller;
            animationEntry = controller.AnimationEntryController.AnimationEntry;
            frame = controller.Frame;
            model = controller.GetEntry<ModelEntry>(frame.ModelEID);
            frame.MakeVertices(model);

            InitializeComponent();
            UpdateVertice();
            UpdateCollision();
            UpdateOffset();
            UpdateHeaderSize();
            UpdateSPVertex();
            UpdateModel();

            CreateTabs();
        }

        private void CreateTabs()
        {
            MetroSetTabControl tbcTabs = new MetroSetTabControl()
            {
                BackgroundColor = Color.FromArgb(31, 31, 32),
                Dock = DockStyle.Fill,
                IsDerivedStyle = false,
                ItemSize = new Size(100, 28),
                Style = MetroSet_UI.Enums.Style.Dark,
                TabStyle = MetroSet_UI.Enums.TabStyle.Style1
            };
            var viewerbox = new AnimationEntryViewer(controller.GetNSF(), animationEntry.EID, animationEntry.Frames.IndexOf(frame))
            {
                Dock = DockStyle.Fill
            };

            if (Settings.Default.SplitAnimViewerPanels)
            {
                pnSplit = new SplitContainer
                {
                    Orientation = Orientation.Horizontal,
                    SplitterDistance = 35,
                    IsSplitterFixed = true,
                    Dock = DockStyle.Fill
                };
                pnSplit.Panel1.Controls.Add(pnFrameBox);
                pnSplit.Panel2.Controls.Add(viewerbox);
                Controls.Add(pnSplit);
            }
            else
            {
                TabPage viewertab = new TabPage("Viewer");
                viewertab.Controls.Add(viewerbox);
                TabPage edittab = new TabPage("Editor");
                edittab.Controls.Add(pnFrameBox);

                tbcTabs.TabPages.Add(viewertab);
                tbcTabs.TabPages.Add(edittab);
                tbcTabs.SelectedTab = viewertab;
                Controls.Add(tbcTabs);
            }
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
                numX.Value = (decimal)frame.Positions[vertexindex].X;
                numY.Value = (decimal)frame.Positions[vertexindex].Y;
                numZ.Value = (decimal)frame.Positions[vertexindex].Z;
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

        private void AppendCollision(Frame _frame)
        {
            var frame = _frame;
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

        }

        private void cmdAppendCollision_Click(object sender, EventArgs e)
        {
            if (syncedit)
            {
                foreach (Frame frame in animationEntry.Frames)
                {
                    AppendCollision(frame);
                }
            }
            else
            {
                AppendCollision(frame);
            }
            UpdateCollision();
            UpdateHeaderSize();
        }

        private void cmdInsertCollision_Click(object sender, EventArgs e)
        {
            if (syncedit)
            {
                foreach (Frame frame in animationEntry.Frames)
                {
                    frame.Collision.Insert(collisionindex, frame.Collision[collisionindex]);
                    frame.HeaderSize = (int)numHeader.Value + 40;
                }
            }
            else
            {
                frame.Collision.Insert(collisionindex, frame.Collision[collisionindex]);
                frame.HeaderSize = (int)numHeader.Value + 40;
            }
            UpdateCollision();
            UpdateHeaderSize();
        }

        private void cmdRemoveCollision_Click(object sender, EventArgs e)
        {
            if (syncedit)
            {
                foreach (Frame frame in animationEntry.Frames)
                {
                    frame.Collision.RemoveAt(collisionindex);
                    frame.HeaderSize = (int)numHeader.Value - 40;
                }
            }
            else
            {
                frame.Collision.RemoveAt(collisionindex);
                frame.HeaderSize = (int)numHeader.Value - 40;
            }
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
                //FrameVertex pos = frame.Vertices[vertexindex];
                //frame.Vertices[vertexindex] = new FrameVertex((byte)numX.Value, pos.Y, pos.Z);
                Position pos = frame.Positions[vertexindex];
                frame.Positions[vertexindex] = new Position((float)numX.Value, pos.Y, pos.Z);
            }
        }

        private void numY_ValueChanged(object sender, EventArgs e)
        {
            if (!vertexdirty)
            {
                //FrameVertex pos = frame.Vertices[vertexindex];
                //frame.Vertices[vertexindex] = new FrameVertex(pos.X, (byte)numY.Value, pos.Z);
                Position pos = frame.Positions[vertexindex];
                frame.Positions[vertexindex] = new Position(pos.X, (float)numY.Value, pos.Z);
            }
        }

        private void numZ_ValueChanged(object sender, EventArgs e)
        {
            if (!vertexdirty)
            {
                //FrameVertex pos = frame.Vertices[vertexindex];
                //frame.Vertices[vertexindex] = new FrameVertex(pos.X, pos.Y, (byte)numZ.Value);
                Position pos = frame.Positions[vertexindex];
                frame.Positions[vertexindex] = new Position(pos.X, pos.Y, (float)numZ.Value);
            }
        }

        private void numXOffset_ValueChanged(object sender, EventArgs e)
        {
            short oldV = frame.XOffset;
            short newV = (short)numXOffset.Value;
            short dif = (short)(newV - oldV);
            if (syncedit)
            {
                foreach (Frame frame in animationEntry.Frames)
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
                foreach (Frame frame in animationEntry.Frames)
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
                foreach (Frame frame in animationEntry.Frames)
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
                    foreach (Frame frame in animationEntry.Frames)
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
                    foreach (Frame frame in animationEntry.Frames)
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
                    foreach (Frame frame in animationEntry.Frames)
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
                    foreach (Frame frame in animationEntry.Frames)
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
                    foreach (Frame frame in animationEntry.Frames)
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
                    foreach (Frame frame in animationEntry.Frames)
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
                    foreach (Frame frame in animationEntry.Frames)
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
                    foreach (Frame frame in animationEntry.Frames)
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
                    foreach (Frame frame in animationEntry.Frames)
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

        private void chkShowVertices_CheckedChanged(object sender, EventArgs e)
        {
            fraVertice.Visible = chkShowVertices.Checked;
            if (Settings.Default.SplitAnimViewerPanels && pnSplit != null)
            {
                pnSplit.SplitterDistance = chkShowVertices.Checked ? 480 : 300;
            }
        }

        private void cmdCopyCollision_Click(object sender, EventArgs e)
        {
            if (DarkMessageBox.ShowMessage("Are you sure you want to copy the current frame's collision values to other frames?", "Confirmation Prompt", DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                FrameCollision pos = frame.Collision[collisionindex];
                foreach (Frame frame in animationEntry.Frames)
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
                foreach (Frame frame in animationEntry.Frames)
                {
                    frame.XOffset = (short)numXOffset.Value;
                    frame.YOffset = (short)numYOffset.Value;
                    frame.ZOffset = (short)numZOffset.Value;
                }
            }
        }
    }
}