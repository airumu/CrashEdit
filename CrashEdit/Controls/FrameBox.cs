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
        private bool isCompresed;
        private int vertexindex = 0;
        private int collisionindex = 0;

        private const int XOffset = 0;
        private const int YOffset = 1;
        private const int ZOffset = 2;

        public FrameBox(FrameController controller)
        {
            this.controller = controller;
            animationEntry = controller.AnimationEntryController.AnimationEntry;
            frame = controller.Frame;
            model = controller.GetEntry<ModelEntry>(frame.ModelEID);
            isCompresed = model != null && model.Positions != null;

            InitializeComponent();
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
                    SplitterDistance = 55,
                    IsSplitterFixed = true,
                    Dock = DockStyle.Fill
                };
                pnSplit.Panel1.Controls.Add(pnFrameBox);
                pnSplit.Panel2.Controls.Add(viewerbox);
                Controls.Add(pnSplit);
                MainInit();
            }
            else
            {
                TabPage viewertab = new TabPage("Viewer");
                viewertab.Controls.Add(viewerbox);
                TabPage edittab = new TabPage("Editor");
                edittab.Controls.Add(pnFrameBox);

                tbcTabs.TabPages.Add(viewertab);
                tbcTabs.TabPages.Add(edittab);

                EventHandler tabChangedHandler = null;
                tabChangedHandler = (sender, e) =>
                {
                    if (tbcTabs.SelectedTab == edittab)
                    {
                        MainInit();
                        tbcTabs.SelectedIndexChanged -= tabChangedHandler;
                    }
                };
                tbcTabs.SelectedIndexChanged += tabChangedHandler;

                tbcTabs.SelectedTab = viewertab;
                Controls.Add(tbcTabs);
            }
        }

        private void MainInit()
        {
            frame.MakeVertices(model);
            UpdateVertice();
            UpdateCollision();
            UpdateOffset();
            UpdateHeaderSize();
            UpdateSPVertex();
            UpdateModel();

            fraVertice.Text = isCompresed ? "Vertice(s) (read-only)" : "Vertice(s)";
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
            //frame.Vertices.Insert(vertexindex, frame.Vertices[vertexindex]);
            //UpdateVertice();
        }

        private void cmdRemoveVertice_Click(object sender, EventArgs e)
        {
            foreach (Frame frame in animationEntry.Frames)
            {
                vertexindex = frame.Vertices.Count;
                RemoveTemporals(frame);
                frame.Vertices.RemoveAt(vertexindex - 1);
                frame.Positions.RemoveAt(vertexindex - 1);
            }
            UpdateVertice();
        }

        private void cmdAppendVertice_Click(object sender, EventArgs e)
        {
            foreach (Frame frame in animationEntry.Frames)
            {
                vertexindex = frame.Vertices.Count;
                if (frame.Vertices.Count > 0)
                {
                    var vert = frame.Vertices[vertexindex - 1];
                    var pos = frame.Positions[vertexindex - 1];
                    AppendTemporals(frame, pos.X, pos.Y, pos.Z);
                    frame.Vertices.Add(vert);
                    frame.Positions.Add(pos);
                }
                else
                {
                    frame.Vertices[vertexindex] = new FrameVertex(0, 0, 0);
                    frame.Positions[vertexindex] = new Position(0, 0, 0);
                }
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

        public void AppendTemporals(Frame frame, float newX, float newY, float newZ)
        {
            byte[] convertTemporals = BoolArrayToByteArray(frame.Temporals);
            byte[] reversedTemporals = ReverseBytesIn4ByteBlocks(convertTemporals);

            int padding = frame.Vertices.Count % 4;
            if (padding == 0)
            {
                reversedTemporals = reversedTemporals.Concat(new byte[] { ReverseBits(Convert.ToByte(newX)), ReverseBits(Convert.ToByte(newY)), ReverseBits(Convert.ToByte(newZ)) }).ToArray();
                reversedTemporals = reversedTemporals.Concat(new byte[1]).ToArray();
            }
            else if (padding == 1)
            {
                reversedTemporals[reversedTemporals.Length - 1] = ReverseBits(Convert.ToByte(newX));
                reversedTemporals = reversedTemporals.Concat(new byte[] { ReverseBits(Convert.ToByte(newY)), ReverseBits(Convert.ToByte(newZ)) }).ToArray();
                reversedTemporals = reversedTemporals.Concat(new byte[2]).ToArray();
            }
            else if (padding == 2)
            {
                reversedTemporals[reversedTemporals.Length - 2] = ReverseBits(Convert.ToByte(newX));
                reversedTemporals[reversedTemporals.Length - 1] = ReverseBits(Convert.ToByte(newY));
                reversedTemporals = reversedTemporals.Concat(new byte[] { ReverseBits(Convert.ToByte(newZ)) }).ToArray();
                reversedTemporals = reversedTemporals.Concat(new byte[3]).ToArray();
            }
            else if (padding == 3)
            {
                reversedTemporals[reversedTemporals.Length - 3] = ReverseBits(Convert.ToByte(newX));
                reversedTemporals[reversedTemporals.Length - 2] = ReverseBits(Convert.ToByte(newY));
                reversedTemporals[reversedTemporals.Length - 1] = ReverseBits(Convert.ToByte(newZ));
            }

            byte[] convertBackTemporals = ReverseBytesIn4ByteBlocks(reversedTemporals);
            frame.Temporals = ByteArrayToBoolArray(convertBackTemporals);
        }

        public void RemoveTemporals(Frame frame)
        {
            byte[] convertTemporals = BoolArrayToByteArray(frame.Temporals);
            byte[] reversedTemporals = ReverseBytesIn4ByteBlocks(convertTemporals);

            int padding = frame.Vertices.Count % 4;
            if (padding == 0)
            {
                reversedTemporals = reversedTemporals.Take(reversedTemporals.Length - 3).ToArray();
                reversedTemporals = reversedTemporals.Concat(new byte[3]).ToArray();
            }
            else if (padding == 3)
            {
                reversedTemporals = reversedTemporals.Take(reversedTemporals.Length - 6).ToArray();
                reversedTemporals = reversedTemporals.Concat(new byte[2]).ToArray();
            }
            else if (padding == 2)
            {
                reversedTemporals = reversedTemporals.Take(reversedTemporals.Length - 5).ToArray();
                reversedTemporals = reversedTemporals.Concat(new byte[1]).ToArray();
            }
            else if (padding == 1)
            {
                reversedTemporals = reversedTemporals.Take(reversedTemporals.Length - 4).ToArray();
            }

            byte[] convertBackTemporals = ReverseBytesIn4ByteBlocks(reversedTemporals);
            frame.Temporals = ByteArrayToBoolArray(convertBackTemporals);
        }

        public void UpdateTemporals(Frame frame, int offset, float newV)
        {
            if (isCompresed) return;

            byte[] convertTemporals = BoolArrayToByteArray(frame.Temporals);
            byte[] reversedTemporals = ReverseBytesIn4ByteBlocks(convertTemporals);

            int byteIndex = vertexindex * 3 + offset;
            reversedTemporals[byteIndex] = ReverseBits(Convert.ToByte(newV));

            byte[] convertBackTemporals = ReverseBytesIn4ByteBlocks(reversedTemporals);
            frame.Temporals = ByteArrayToBoolArray(convertBackTemporals);
        }

        private byte ReverseBits(byte value)
        {
            byte reversed = 0;
            for (int i = 0; i < 8; i++)
            {
                reversed = (byte)((reversed << 1) | ((value >> i) & 0x1));
            }
            return reversed;
        }

        private byte[] BoolArrayToByteArray(bool[] boolArray)
        {
            int byteArrayLength = (boolArray.Length + 7) / 8;
            byte[] byteArray = new byte[byteArrayLength];
            for (int i = 0; i < boolArray.Length; i++)
            {
                if (boolArray[i])
                {
                    byteArray[i / 8] |= (byte)(1 << (i % 8));
                }
            }
            return byteArray;
        }

        private bool[] ByteArrayToBoolArray(byte[] byteArray)
        {
            int boolArrayLength = byteArray.Length * 8;
            bool[] boolArray = new bool[boolArrayLength];
            for (int i = 0; i < boolArrayLength; i++)
            {
                boolArray[i] = (byteArray[i / 8] & (1 << (i % 8))) != 0;
            }
            return boolArray;
        }

        private byte[] ReverseBytesIn4ByteBlocks(byte[] data)
        {
            int blockSize = 4;
            int numBlocks = data.Length / blockSize;
            byte[] reversedData = new byte[data.Length];

            for (int i = 0; i < numBlocks; i++)
            {
                byte[] block = new byte[blockSize];
                Array.Copy(data, i * blockSize, block, 0, blockSize);

                Array.Reverse(block);
                Array.Copy(block, 0, reversedData, i * blockSize, blockSize);
            }
            return reversedData;
        }

        private T ValidateValue<T>(T value, T dif) where T : struct, IComparable<T>
        {
            T result;
            try
            {
                long tempResult = Convert.ToInt64(value) + Convert.ToInt64(dif);

                T max = (T)typeof(T).GetField("MaxValue")?.GetValue(null);
                T min = (T)typeof(T).GetField("MinValue")?.GetValue(null);

                tempResult = Math.Clamp(tempResult, Convert.ToInt64(min), Convert.ToInt64(max));
                result = (T)Convert.ChangeType(tempResult, typeof(T));
            }
            catch (OverflowException)
            {
                T max = (T)typeof(T).GetField("MaxValue")?.GetValue(null);
                T min = (T)typeof(T).GetField("MinValue")?.GetValue(null);
                result = dif.CompareTo(default(T)) > 0 ? max : min;
            }

            return result;
        }

        private float ValidateVertexValue(float value, float dif)
        {
            float result = value + dif;
            if (result > byte.MaxValue)
                result = byte.MaxValue;
            else if (result < byte.MinValue)
                result = byte.MinValue;

            return result;
        }

        private void numX_ValueChanged(object sender, EventArgs e)
        {
            if (!vertexdirty)
            {
                //FrameVertex pos = frame.Vertices[vertexindex];
                //frame.Vertices[vertexindex] = new FrameVertex((byte)numX.Value, pos.Y, pos.Z);

                float oldV = frame.Positions[vertexindex].X;
                float newV = (float)numX.Value;
                float dif = newV - oldV;
                if (syncedit)
                {
                    foreach (Frame frame in animationEntry.Frames)
                    {
                        var f = frame.Positions[vertexindex];
                        float result = ValidateVertexValue(f.X, dif);
                        frame.Positions[vertexindex] = new Position(result, f.Y, f.Z);
                        UpdateTemporals(frame, XOffset, result);
                    }
                }
                else
                {
                    Position pos = frame.Positions[vertexindex];
                    frame.Positions[vertexindex] = new Position(newV, pos.Y, pos.Z);
                    UpdateTemporals(frame, XOffset, newV);
                }
            }
        }

        private void numY_ValueChanged(object sender, EventArgs e)
        {
            if (!vertexdirty)
            {
                //FrameVertex pos = frame.Vertices[vertexindex];
                //frame.Vertices[vertexindex] = new FrameVertex(pos.X, (byte)numY.Value, pos.Z);

                float oldV = frame.Positions[vertexindex].Y;
                float newV = (float)numY.Value;
                float dif = newV - oldV;
                if (syncedit)
                {
                    foreach (Frame frame in animationEntry.Frames)
                    {
                        var f = frame.Positions[vertexindex];
                        float result = ValidateVertexValue(f.Y, dif);
                        frame.Positions[vertexindex] = new Position(f.X, result, f.Z);
                        UpdateTemporals(frame, YOffset, result);
                    }
                }
                else
                {
                    Position pos = frame.Positions[vertexindex];
                    frame.Positions[vertexindex] = new Position(pos.X, newV, pos.Z);
                    UpdateTemporals(frame, YOffset, newV);
                }
            }
        }

        private void numZ_ValueChanged(object sender, EventArgs e)
        {
            if (!vertexdirty)
            {
                //FrameVertex pos = frame.Vertices[vertexindex];
                //frame.Vertices[vertexindex] = new FrameVertex(pos.X, pos.Y, (byte)numZ.Value);

                float oldV = frame.Positions[vertexindex].Z;
                float newV = (float)numZ.Value;
                float dif = newV - oldV;
                if (syncedit)
                {
                    foreach (Frame frame in animationEntry.Frames)
                    {
                        var f = frame.Positions[vertexindex];
                        float result = ValidateVertexValue(f.Z, dif);
                        frame.Positions[vertexindex] = new Position(f.X, f.Y, result);
                        UpdateTemporals(frame, ZOffset, result);
                    }
                }
                else
                {
                    Position pos = frame.Positions[vertexindex];
                    frame.Positions[vertexindex] = new Position(pos.X, pos.Y, newV);
                    UpdateTemporals(frame, ZOffset, newV);
                }
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
                {
                    short result = ValidateValue(frame.XOffset, dif);
                    frame.XOffset = result;
                }
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
                {
                    short result = ValidateValue(frame.YOffset, dif);
                    frame.YOffset = result;
                }
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
                {
                    short result = ValidateValue(frame.ZOffset, dif);
                    frame.ZOffset = result;
                }
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
                        int result = ValidateValue(f.X1, dif);
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset, f.YOffset, f.ZOffset, result, f.Y1, f.Z1, f.X2, f.Y2, f.Z2);
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
                        int result = ValidateValue(f.Y1, dif);
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset, f.YOffset, f.ZOffset, f.X1, result, f.Z1, f.X2, f.Y2, f.Z2);
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
                        int result = ValidateValue(f.Z1, dif);
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset, f.YOffset, f.ZOffset, f.X1, f.Y1, result, f.X2, f.Y2, f.Z2);
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
                        int result = ValidateValue(f.X2, dif);
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset, f.YOffset, f.ZOffset, f.X1, f.Y1, f.Z1, result, f.Y2, f.Z2);
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
                        int result = ValidateValue(f.Y2, dif);
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset, f.YOffset, f.ZOffset, f.X1, f.Y1, f.Z1, f.X2, result, f.Z2);
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
                        int result = ValidateValue(f.Z2, dif);
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset, f.YOffset, f.ZOffset, f.X1, f.Y1, f.Z1, f.X2, f.Y2, result);
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
                        int result = ValidateValue(f.XOffset, dif);
                        frame.Collision[collisionindex] = new FrameCollision(f.U, result, f.YOffset, f.ZOffset, f.X1, f.Y1, f.Z1, f.X2, f.Y2, f.Z2);
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
                        int result = ValidateValue(f.YOffset, dif);
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset, result, f.ZOffset, f.X1, f.Y1, f.Z1, f.X2, f.Y2, f.Z2);
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
                        int result = ValidateValue(f.ZOffset, dif);
                        frame.Collision[collisionindex] = new FrameCollision(f.U, f.XOffset, f.YOffset, result, f.X1, f.Y1, f.Z1, f.X2, f.Y2, f.Z2);
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