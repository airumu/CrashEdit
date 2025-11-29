using System.Text.RegularExpressions;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public partial class GOOLBox : UserControl
    {
        private GOOLEntryController controller;
        private GOOLEntry goolentry;

        private readonly DataGridView dgvCode;

        List<int> indents;
        List<int> processedRows;

        private int headerCount;
        private int addressIndex;

        private bool poolShowAsEID => chkPoolToggleEID.Checked;

        private readonly int ColState = 0;
        private readonly int ColStateFlags = 1;
        private readonly int ColBlockFlags = 2;
        private readonly int ColEventBlock = 3;
        private readonly int ColCodeBlock = 4;
        private readonly int ColTransBlock = 5;
        private readonly int ColExternal = 6;

        internal Stack<bool> dirty = new Stack<bool>();
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        public GOOLBox(GOOLEntryController controller, GOOLEntry goolentry)
        {
            this.controller = controller;
            this.goolentry = goolentry;

            InitializeComponent();

            dgvCode = new DataGridView
            {
                Dock = DockStyle.Fill,
                Font = new Font("Cascadia Code SemiLight", 8F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToOrderColumns = false,
                RowHeadersVisible = false,
                ColumnHeadersVisible = false,
                ShowCellToolTips = false,
                ReadOnly = true
            };
            DoubleBufferedDataGridView.Initialize_NoAltColor(dgvCode);
            dgvCode.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCode.RowTemplate.Height = 16;
            dgvCode.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                HeaderText = "Description",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            dgvCode.CellPainting += dgvCode_CellPainting;
            dgvCode.CellDoubleClick += dgvCode_CellDoubleClick;
            dgvCode.MouseDown += dgvCode_MouseDown;
            dgvCode.KeyDown += dgvCode_KeyDown;
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("Copy Offset as hex", null, CopyOffsetToClipboard);
            contextMenuStrip.Opening += ContextMenuStrip_Opening;
            dgvCode.ContextMenuStrip = contextMenuStrip;

            processedRows = new();
            headerCount = 0;
            addressIndex = 0;

            PopulateData(goolentry);
            TabsInit();
        }

        private void TabsInit()
        {
            tbpCode.Controls.Add(dgvCode);
            tbcTabs.SelectedTab = tbpCode;

            if (goolentry.Version != GOOLVersion.Version0 && goolentry.Format == 1)
            {
                TabPage tabFrames = new("Frame Groups");
                var goolFrameGroupBox = new GOOLFrameGroupBox(controller, goolentry)
                {
                    Dock = DockStyle.Fill
                };
                tabFrames.Controls.Add(goolFrameGroupBox);
                tbcTabs.TabPages.Add(tabFrames);

                EventHandler tabChangedHandler = null;
                tabChangedHandler = (sender, e) =>
                {
                    if (tbcTabs.SelectedTab == tabFrames)
                    {
                        goolFrameGroupBox.OnTabSelected();
                        tbcTabs.SelectedIndexChanged -= tabChangedHandler;
                    }
                };
                tbcTabs.SelectedIndexChanged += tabChangedHandler;
            }
            else
            {
                // Remove tabs not applicable to this GOOL version/format
                tbcTabs.Controls.Remove(tbpDataPool);
                tbpDataPool.Dispose();
                tbcTabs.Controls.Remove(tbpStateMap);
                tbpStateMap.Dispose();
                tbcTabs.Controls.Remove(tbpStateDescriptors);
                tbpStateDescriptors.Dispose();
            }
        }

        #region General
        private void tbpGeneral_Enter(object sender, EventArgs e)
        {
            if (goolentry.Version == GOOLVersion.Version3)
            {
                numGoolType.Maximum = 127;
            }

            dirty.Push(true);
            numGoolType.Value = goolentry.ID;
            numGoolClass.Value = goolentry.Class >> 8;
            dirty.Pop();

            numGoolType.MouseWheel += ScrollHandlerFunction;
            numGoolClass.MouseWheel += ScrollHandlerFunction;

            tbpGeneral.Enter -= tbpGeneral_Enter;
        }

        private void numGoolType_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            goolentry.ID = (int)numGoolType.Value;
        }

        private void numGoolClass_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            goolentry.Class = (int)numGoolClass.Value << 8;
        }

        private void ScrollHandlerFunction(object? sender, MouseEventArgs e)
        {
            if (sender is NumericUpDown numericUpDown)
            {
                HandledMouseEventArgs handledArgs = e as HandledMouseEventArgs;
                if (handledArgs != null)
                    handledArgs.Handled = true;

                decimal newValue = numericUpDown.Value;
                if (e.Delta > 0 && newValue < numericUpDown.Maximum)
                    newValue += numericUpDown.Increment;

                else if (e.Delta < 0 && newValue > numericUpDown.Minimum)
                    newValue -= numericUpDown.Increment;

                numericUpDown.Value = newValue;
            }
        }

        #endregion

        #region Code

        private void PopulateData(GOOLEntry goolentry)
        {
            // Data container
            //var rows = new List<(string Index, string Description)>();
            var rows = dgvCode.Rows;

            // Add header information
            rows.Add($"Type {goolentry.ID}");
            rows.Add($"Class {goolentry.Class / 0x100}");
            rows.Add($"Format {goolentry.Format}");
            rows.Add($"Heap Base {(ObjectFields)goolentry.HeapBase} ({(goolentry.HeapBase * 4 + GOOLInterpreter.GetProcessOff(goolentry.Version)).TransformedString()})");
            rows.Add($"Interrupt Count {goolentry.EventCount}");
            rows.Add($"Entry Count {goolentry.EntryCount}");
            headerCount += 6;

            var labels = new Dictionary<int, List<string>>();

            if (goolentry.Format == 1)
            {
                rows.Add("");
                headerCount++;
                bool addedInterrupts = false;

                // Process interrupts
                for (int i = 0; i < goolentry.EventCount; ++i)
                {
                    if (goolentry.StateMap[i] == 255)
                        continue;
                    else
                    {
                        if (!addedInterrupts)
                        {
                            rows.Add("Interrupts:");
                            ++headerCount;
                            addedInterrupts = true;
                        }
                        if ((goolentry.StateMap[i] & 0x8000) != 0)
                        {
                            int offset = goolentry.StateMap[i] & 0x3FFF;
                            addressIndex = rows.Add($"    Interrupt {i}: Sub_{offset}");
                            dgvCode.Rows[addressIndex].Tag = offset;
                            ++addressIndex;
                        }
                        else
                        {
                            addressIndex = rows.Add($"    Interrupt {i}: State_{goolentry.StateMap[i]}");
                            dgvCode.Rows[addressIndex].Tag = $"State_{goolentry.StateMap[i]}";
                            ++addressIndex;
                        }

                        ++headerCount;
                    }
                }

                rows.Add($"Available Subtypes: {goolentry.StateMap.Length - goolentry.EventCount}");
                ++headerCount;

                // Process subtypes
                for (int i = goolentry.EventCount; i < goolentry.StateMap.Length; ++i)
                {
                    if (i > goolentry.EventCount && i + 1 == goolentry.StateMap.Length && goolentry.StateMap[i] == 0) continue;
                    addressIndex = rows.Add($"    Subtype {i - goolentry.EventCount}: {(goolentry.StateMap[i] == 255 ? "invalid" : $"State_{goolentry.StateMap[i]}")}");
                    dgvCode.Rows[addressIndex].Tag = $"State_{goolentry.StateMap[i]}";
                    ++addressIndex;
                    ++headerCount;
                }

                rows.Add("");
                ++headerCount;

                // Process states
                for (int i = 0; i < goolentry.StateDescriptors.Count; ++i)
                {
                    short epc = (short)(goolentry.StateDescriptors[i].EventHook & 0x3FFF);
                    short tpc = (short)(goolentry.StateDescriptors[i].TransHook & 0x3FFF);
                    short cpc = (short)(goolentry.StateDescriptors[i].CodeHook & 0x3FFF);
                    int stategooleid = goolentry.Data[goolentry.StateDescriptors[i].GOOLIndex];

                    rows.Add($"State_{i} [{Entry.EIDToEName(stategooleid)}] State Flags: {string.Format("0x{0:X}", goolentry.StateDescriptors[i].StateFlags)} | Block Flags: {string.Format("0x{0:X}", goolentry.StateDescriptors[i].BlockFlags)}");
                    if (epc != 0x3FFF)
                    {
                        bool isexternal = (goolentry.StateDescriptors[i].EventHook & 0x4000) != 0;
                        addressIndex = rows.Add($"    Event: {epc}" + (isexternal ? " (external)" : ""));
                        if (!isexternal)
                            dgvCode.Rows[addressIndex].Tag = epc;
                        ++addressIndex;
                    }
                    else
                        rows.Add("    (no\u00A0event\u00A0hook)");
                    if (cpc != 0x3FFF)
                    {
                        bool isexternal = (goolentry.StateDescriptors[i].CodeHook & 0x4000) != 0;
                        addressIndex = rows.Add($"    Code: {cpc}" + (isexternal ? " (external)" : ""));
                        if (!isexternal)
                            dgvCode.Rows[addressIndex].Tag = cpc;
                        ++addressIndex;
                    }
                    else
                        rows.Add("    ERROR! No code thread! This state will not work.");
                    if (tpc != 0x3FFF)
                    {
                        bool isexternal = (goolentry.StateDescriptors[i].TransHook & 0x4000) != 0;
                        addressIndex = rows.Add($"    Trans: {tpc}" + (isexternal ? " (external)" : ""));
                        if (!isexternal)
                            dgvCode.Rows[addressIndex].Tag = tpc;
                        ++addressIndex;
                    }
                    else
                        rows.Add("    (no\u00A0trans\u00A0hook)");
                    headerCount += 4;

                    if (stategooleid == goolentry.EID)
                    {
                        if (cpc != 0x3FFF)
                        {
                            if (!labels.ContainsKey(cpc))
                                labels.Add(cpc, new());
                            labels[cpc].Add($"State_{i}_code:");
                        }
                        if (epc != 0x3FFF)
                        {
                            if (!labels.ContainsKey(epc))
                                labels.Add(epc, new());
                            labels[epc].Add($"State_{i}_event:");
                        }
                        if (tpc != 0x3FFF)
                        {
                            if (!labels.ContainsKey(tpc))
                                labels.Add(tpc, new());
                            labels[tpc].Add($"State_{i}_trans:");
                        }
                    }
                }
            }

            rows.Add("");
            ++headerCount;
            bool returned = true;
            int mipscount = 0;
            int goolcount = 0;

            indents = new List<int>();
            indents.Capacity = goolentry.Instructions.Count;
            for (int i = 0; i < goolentry.Instructions.Count; ++i)
                indents.Add(0);

            for (int i = 0; i < goolentry.Instructions.Count; ++i)
            {
                GOOLInstruction ins = goolentry.Instructions[i];
                string insComment = ins.GetComment();
                int instIndex = i;

                if (!string.IsNullOrWhiteSpace(ins.GetComment()))
                {

                    if (insComment.Contains("if") && insComment.Contains("move"))
                    {
                        string pattern = @"-?\d+";
                        Match match = Regex.Match(insComment, pattern);
                        if (match.Success)
                        {
                            int number = int.Parse(match.Value);
                            int indentEndIndex = instIndex + number;
                            if (indentEndIndex < instIndex)
                            {
                                var t = instIndex;
                                instIndex = indentEndIndex;
                                indentEndIndex = t; // swap values
                            }

                            for (int j = instIndex; j < indentEndIndex; j++)
                            {
                                if (j >= -1)
                                    indents[j + 1]++;
                            }
                        }
                    }
                }
            }

            // Process instructions
            for (int i = 0; i < goolentry.Instructions.Count; ++i)
            {
                if (labels.ContainsKey(i))
                {
                    foreach (string label in labels[i])
                    {
                        rows.Add(label);
                    }
                    returned = false;
                }
                if (returned)
                {
                    rows.Add($"Sub_{i}:");
                }
                GOOLInstruction ins = goolentry.Instructions[i];
                if (ins is MIPSInstruction)
                {
                    returned = goolentry.Instructions[i - 1].Value == 0x03E00008 && goolentry.Instructions[i - 1] is MIPSInstruction;
                    ++mipscount;
                }
                else
                {
                    returned = GOOLInterpreter.IsReturnInstruction(ins);
                    if (ins is not GOOLUnknownInstruction)
                        ++goolcount;
                }

                string insName = ins.GetName();

                int instIndex = dgvCode.Rows.Add($"{i,-6} {insName,-6} {ins.Arguments,-32} {(!string.IsNullOrWhiteSpace(ins.GetComment()) ? $"# {ins.GetComment()}" : "")}");
                dgvCode.Rows[instIndex].Tag = i;
                processedRows.Add(instIndex);

                if (indents[i] > 0)
                {
                    string indent_str = " ";
                    for (int j = 0; j < indents[i]; j++)
                        indent_str += "Ͱ";

                    DataGridViewRow row = dgvCode.Rows[instIndex];
                    string lineText = row.Cells[0].Value.ToString();
                    int hashIndex = lineText.IndexOf('#');
                    if (hashIndex != -1)
                    {
                        lineText = lineText.Insert(hashIndex + 1, indent_str);
                        row.Cells[0].Value = lineText;
                    }
                }
            }

            // Add statistics
            if (goolcount != goolentry.Instructions.Count)
            {
                rows.Add("");
                string gool = $"Instructions: {(float)goolcount / goolentry.Instructions.Count:P} GOOL";
                string mips = string.Empty;
                string invalid = string.Empty;

                if (mipscount > 0)
                    mips = $", {(float)mipscount / goolentry.Instructions.Count:P} MIPS";
                if (goolentry.Instructions.Count - mipscount - goolcount > 0)
                    invalid = $"{(float)(goolentry.Instructions.Count - mipscount - goolcount) / goolentry.Instructions.Count:P} invalid";

                rows.Add(gool + mips + invalid);
            }
        }

        private void dgvCode_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dgvCode.HitTest(e.X, e.Y);
                if (hitTest.Type == DataGridViewHitTestType.Cell)
                {
                    dgvCode.CurrentCell = dgvCode[hitTest.ColumnIndex, hitTest.RowIndex];
                }
            }
        }

        private void CopyOffsetToClipboard(object sender, EventArgs e)
        {
            var cellValue = dgvCode.CurrentCell?.Value?.ToString();

            if (!string.IsNullOrEmpty(cellValue))
            {
                var match = Regex.Match(cellValue, @"\d+");
                if (match.Success)
                {
                    int number = Convert.ToInt32(match.Value);
                    string offset = (number * 4).ToString("X");
                    Clipboard.SetDataObject(offset, true, 10, 100);
                }
            }
        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var cell = dgvCode.CurrentCell;
            if (cell != null)
            {
                ToolStripMenuItem copyItem = (ToolStripMenuItem)dgvCode.ContextMenuStrip.Items[0]; // "Copy Offset as hex"
                copyItem.Enabled = dgvCode.Rows[cell.RowIndex].Tag != null;
            }
        }

        private void dgvCode_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0 && dgvCode.Rows[e.RowIndex].Tag != null)
            {
                // header
                if (e.RowIndex < headerCount)
                {
                    string tag = dgvCode.Rows[e.RowIndex].Tag.ToString();
                    if (tag.Contains("State"))
                    {
                        string pattern = $@"^{Regex.Escape(tag)}\b";
                        foreach (DataGridViewRow row in dgvCode.Rows)
                        {
                            string targetCellText = row.Cells[0].Value?.ToString() ?? "";
                            if (Regex.IsMatch(targetCellText, pattern))
                            {
                                int targetRowIndex = row.Index;
                                dgvCode.FirstDisplayedScrollingRowIndex = targetRowIndex;
                                dgvCode.ClearSelection();
                                dgvCode.Rows[targetRowIndex].Selected = true;
                            }
                        }
                    }
                    else
                    {
                        int targetIndex = Convert.ToInt32(dgvCode.Rows[e.RowIndex].Tag);

                        for (int i = headerCount; i < dgvCode.Rows.Count; i++)
                        {
                            if (dgvCode.Rows[i].Tag != null)
                            {
                                int targetTagValue = (int)dgvCode.Rows[i].Tag;
                                if (targetIndex == targetTagValue)
                                {
                                    int targetRowIndex = dgvCode.Rows[i].Index;
                                    dgvCode.FirstDisplayedScrollingRowIndex = targetRowIndex;
                                    dgvCode.ClearSelection();
                                    dgvCode.Rows[targetRowIndex].Selected = true;
                                }
                            }
                        }
                    }
                }
                // body
                else
                {
                    string cellText = dgvCode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";

                    if (cellText.Contains("instructions"))
                    {
                        string target = "move";
                        string numberPattern = $@"(?<={Regex.Escape(target)}\s*)-?\d+";
                        var match = Regex.Match(cellText, numberPattern);

                        if (match.Success)
                        {
                            int number = int.Parse(match.Value);
                            int moveAmount = number + 1;
                            int targetRowIndex = e.RowIndex + moveAmount;

                            if (targetRowIndex >= 0 && targetRowIndex < dgvCode.RowCount)
                            {
                                dgvCode.CurrentCell = dgvCode.Rows[targetRowIndex].Cells[0];
                            }
                        }
                    }
                    else if (cellText.Contains("subroutine"))
                    {
                        string target = "at";
                        string numberPattern = $@"(?<={Regex.Escape(target)}\s*)\d+";
                        var match = Regex.Match(cellText, numberPattern);

                        if (match.Success)
                        {
                            int number = int.Parse(match.Value);
                            for (int i = headerCount; i < dgvCode.Rows.Count; i++)
                            {
                                if (dgvCode.Rows[i].Tag != null)
                                {
                                    int targetTagValue = (int)dgvCode.Rows[i].Tag;
                                    if (number == targetTagValue)
                                    {
                                        int targetRowIndex = dgvCode.Rows[i].Index;
                                        dgvCode.FirstDisplayedScrollingRowIndex = targetRowIndex;
                                        dgvCode.ClearSelection();
                                        dgvCode.Rows[targetRowIndex].Selected = true;
                                    }
                                }

                            }
                        }
                    }
                    else if (cellText.Contains("state"))
                    {
                        string target = "state";
                        string numberPattern = $@"(?<={Regex.Escape(target)}\s*)\d+";
                        var match = Regex.Match(cellText, numberPattern);

                        if (match.Success)
                        {
                            int number = int.Parse(match.Value);
                            string pattern = $@"State_{number}_code:";

                            foreach (DataGridViewRow row in dgvCode.Rows)
                            {
                                string targetCellText = row.Cells[0].Value?.ToString() ?? "";

                                if (Regex.IsMatch(targetCellText, pattern))
                                {
                                    int targetRowIndex = row.Index;
                                    dgvCode.FirstDisplayedScrollingRowIndex = targetRowIndex;
                                    dgvCode.ClearSelection();
                                    dgvCode.Rows[targetRowIndex].Selected = true;
                                }
                            }
                        }
                    }
                    else if (cellText.Contains("ins"))
                    {
                        string target = "ins";
                        string hexPattern = $@"{Regex.Escape(target)}\[\s*(0x[0-9a-fA-F]+|\d+)\s*\]";
                        var match = Regex.Match(cellText, hexPattern);

                        if (match.Success)
                        {
                            string value = match.Groups[1].Value;
                            int number;
                            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                                number = Convert.ToInt32(value, 16);
                            else
                                number = int.Parse(value);
                            for (int i = headerCount; i < dgvCode.Rows.Count; i++)
                            {
                                if (dgvCode.Rows[i].Tag != null)
                                {
                                    int targetTagValue = (int)dgvCode.Rows[i].Tag;
                                    if (number == targetTagValue)
                                    {
                                        int targetRowIndex = dgvCode.Rows[i].Index;
                                        dgvCode.FirstDisplayedScrollingRowIndex = targetRowIndex;
                                        dgvCode.ClearSelection();
                                        dgvCode.Rows[targetRowIndex].Selected = true;
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }

        private void dgvCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.G && e.Modifiers == Keys.Control)
            {
                using (InputWindow inputWindow = new(Resources.GOOLBox_Goto, "Arrow", "Enter a line address or state code:",
                                                     string.Empty, -1,
                                                     "Jump to a state by typing “s<number>”.\r\nUse “c”, “t”, or “e” to jump to code, trans, or event.\r\nExample: s0, s0c, s0t, s0e"))
                {
                    if (inputWindow.ShowDialog() == DialogResult.OK)
                    {
                        string input = inputWindow.Input;

                        // Serach for State_xxx_c/t/e
                        if (input.StartsWith("s", StringComparison.OrdinalIgnoreCase))
                        {
                            var m = Regex.Match(input, @"^s(?<num>\d+)(?<type>[cte]?)$", RegexOptions.IgnoreCase);

                            if (m.Success)
                            {
                                int number = int.Parse(m.Groups["num"].Value);
                                string type = m.Groups["type"].Value.ToLower();

                                string suffix = type switch
                                {
                                    "t" => "trans",
                                    "e" => "event",
                                    _ => "code"
                                };

                                string pattern = $"State_{number}_{suffix}";

                                foreach (DataGridViewRow row in dgvCode.Rows)
                                {
                                    string text = row.Cells[0].Value?.ToString() ?? "";
                                    if (text.Contains(pattern))
                                    {
                                        int idx = row.Index;
                                        dgvCode.FirstDisplayedScrollingRowIndex = idx;
                                        dgvCode.ClearSelection();
                                        dgvCode.Rows[idx].Selected = true;
                                        return;
                                    }
                                }

                                DarkMessageBox.ShowError("State not found.", Resources.GOOLBox_Goto);
                                return;
                            }
                        }
                        // Search for line index
                        else
                        {
                            if (int.TryParse(input, out int targetIndex))
                            {
                                for (int i = headerCount; i < dgvCode.Rows.Count; i++)
                                {
                                    if (dgvCode.Rows[i].Tag != null)
                                    {
                                        int targetTagValue = (int)dgvCode.Rows[i].Tag;
                                        if (targetIndex == targetTagValue)
                                        {
                                            int targetRowIndex = dgvCode.Rows[i].Index;
                                            dgvCode.FirstDisplayedScrollingRowIndex = targetRowIndex;
                                            dgvCode.ClearSelection();
                                            dgvCode.Rows[targetRowIndex].Selected = true;
                                            return;
                                        }
                                    }
                                }
                                DarkMessageBox.ShowError("Line address out of range.", Resources.GOOLBox_Goto);
                                return;
                            }
                        }

                        DarkMessageBox.ShowError("Invalid input.", Resources.GOOLBox_Goto);
                    }
                }
            }
        }

        private static readonly Color comments = Color.FromArgb(84, 84, 109);   // gray
        private static readonly Color commands = Color.FromArgb(126, 156, 216); // blue
        private static readonly Color states = Color.FromArgb(127, 180, 201); // light blue
        private static readonly Color logicals = Color.FromArgb(228, 104, 118); // red
        private static readonly Color statements = Color.FromArgb(149, 127, 184); // purple
        private static readonly Color actions = Color.FromArgb(122, 167, 159); // green
        private static readonly Color operators = Color.FromArgb(209, 126, 153); // red-orange

        private static readonly Color titles = Color.FromArgb(152, 187, 108); // light green
        private static readonly Color numbers = Color.FromArgb(230, 194, 132); // orange
        private static readonly Color globals = Color.FromArgb(220, 215, 186); // light yellow

        private readonly string numberPattern = @"^(-?(?:\(\s*-?\d+(?:\.\d+)?\s*\)|\(\s*-?0x[0-9a-fA-F]+\s*\)|-?\d+(?:\.\d+)?|-?0x[0-9a-fA-F]+))$";
        private readonly string statePattern = @"^(State_\d+_.*:|Sub_\d+:)$";
        private readonly string disabledPattern = @"(\(no\s*.*\s*hook\)|Ͱ)";
        private readonly string insPattern = @"(ins|ext)\[[^\]]+\]";
        private readonly string animPattern = @"(&anim\[\(?0x[0-9A-Fa-f]+\)?\]|&\d+)";
        private readonly string EIDPattern = @"\((?!(0x))[a-zA-Z0-9_!]{4}(G|V|T|A|O|I)\)";
        private readonly string goolEIDPattern = @"\[[a-zA-Z0-9]{4}C\]";
        private readonly string globalPattern = @"^(<[A-Z0-9]+>|global\[0x[0-9]+\])$";
        private readonly string extraPattern = @"(rand|VEL|degdiff)\(.*\)";
        private readonly string extra2Pattern = @"(seek|degseek|loop)\(.*\)";
        private readonly string extraNumPattern = @"^(\(?-?[0-9A-F]+\)?)|(\(?-?0x?[0-9A-F]+\)?)";

        private readonly Dictionary<Color, string[]> wordGroups = new()
        {
            { comments,   new[] { "#", "b" } },
            { commands,   new[] { "move", "go", "change", "call", "to", "at" } },
            { states,     new[] { "state", "instructions","subroutine" } },
            { logicals,   new[] { "true", "false", "accept", "reject", "invalid" } },
            { statements, new[] { "if", "else", "return" } },
            //{ stacks,     new[] { "sp", "[sp]" } },
            { actions,    new[] { "play", "set", "spawn", "force", "send", "cascade", "push", "pop" } },
            { operators,  new[] { "=", "==", "!", "!=", "|", "||", "|=", "&", "&&", "&=", "^", ">", ">>", ">=", "<", "<<", "<=", "+", "+=", "-", "-=", "*", "*=", "/", "/=", "%" } }
        };
        private Dictionary<string, Color> targetWords = new();

        private void dgvCode_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string cellText = dgvCode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";

                foreach (var group in wordGroups)
                {
                    foreach (var word in group.Value)
                    {
                        targetWords[word] = group.Key;
                    }
                }

                e.PaintBackground(e.CellBounds, true);

                float currentX = e.CellBounds.Left;
                float top = e.CellBounds.Top + (e.CellBounds.Height - e.Graphics.MeasureString(cellText, e.CellStyle.Font).Height) / 2;

                using (Brush defaultBrush = new SolidBrush(e.CellStyle.ForeColor))
                {
                    string[] words = cellText.Split(' ');

                    int charWidth = TextRenderer.MeasureText("A", e.CellStyle.Font).Width;
                    foreach (string word in words)
                    {
                        string cleanedWord = word.TrimEnd(',');
                        string displayWord = word + " ";
                        int exOffset = 0;
                        Color currentColor = e.CellStyle.ForeColor;

                        if (Regex.IsMatch(cleanedWord, statePattern)) // State_{<number>}_<number>, Sub_<number>
                        {
                            currentColor = titles;
                        }
                        else if (Regex.IsMatch(cleanedWord, disabledPattern)) // (no_xxxx_hook)
                        {
                            currentColor = comments;
                        }
                        else if (Regex.IsMatch(cleanedWord, insPattern)) // ins[<number>]
                        {
                            currentColor = states;
                        }
                        else if (Regex.IsMatch(cleanedWord, animPattern)) // &anim[<hex>]
                        {
                            currentColor = titles;
                        }
                        else if (Regex.IsMatch(cleanedWord, EIDPattern)) // (<EID>) *end with '(G|V|T|A|I)'
                        {
                            currentColor = titles;
                        }
                        else if (Regex.IsMatch(cleanedWord, goolEIDPattern)) // [<EID>] *end with 'C'
                        {
                            currentColor = titles;
                        }
                        else if (Regex.IsMatch(cleanedWord, globalPattern)) // <GLOBAL>
                        {
                            currentColor = globals;
                        }
                        else if (Regex.IsMatch(cleanedWord, extraPattern) || Regex.IsMatch(cleanedWord, extra2Pattern)) // arg(x, y) or arg(x, y, z)
                        {
                            exOffset = charWidth / 4;

                            int openParenIndex = word.IndexOf('(');
                            string prefix = word.Substring(0, openParenIndex + 1);  // "arg("
                            string args = word.Substring(openParenIndex + 1); // "x, y)" or "x, y, z)"

                            openParenIndex = args.IndexOf(',');
                            string arg1 = args.Substring(0, openParenIndex + 1);  // "x,"
                            string arg2 = args.Substring(openParenIndex + 1); // "y)" or "y, z)"

                            string arg3 = string.Empty;

                            if (Regex.IsMatch(cleanedWord, extra2Pattern))
                            {
                                string oldArgs = arg2;
                                openParenIndex = oldArgs.IndexOf(',');
                                arg2 = oldArgs.Substring(0, openParenIndex + 1);  // "y,"
                                arg3 = oldArgs.Substring(openParenIndex + 1); // "z)"

                                arg3 = arg3.Substring(0, arg3.Length - 1); // "z"
                            }
                            else
                            {
                                arg2 = arg2.Substring(0, arg2.Length - 1); // "y"
                            }

                            // "arg("
                            using (Brush brush = new SolidBrush(e.CellStyle.ForeColor))
                            {
                                e.Graphics.DrawString(prefix, e.CellStyle.Font, brush, currentX, top);
                            }
                            currentX += (charWidth / 2 * prefix.Length) + exOffset;

                            // "x,"
                            currentColor = Regex.IsMatch(arg1, extraNumPattern) ? numbers : e.CellStyle.ForeColor;
                            using (Brush brush = new SolidBrush(currentColor))
                            {
                                e.Graphics.DrawString(arg1, e.CellStyle.Font, brush, currentX, top);
                            }
                            currentX += (charWidth / 2 * arg1.Length) + exOffset;

                            // "y" or "y,"
                            currentColor = Regex.IsMatch(arg2, extraNumPattern) ? numbers : e.CellStyle.ForeColor;
                            using (Brush brush = new SolidBrush(currentColor))
                            {
                                e.Graphics.DrawString(arg2, e.CellStyle.Font, brush, currentX, top);
                            }
                            currentX += (charWidth / 2 * arg2.Length) + exOffset;

                            // "z"
                            if (arg3 != string.Empty)
                            {
                                currentColor = Regex.IsMatch(arg3, extraNumPattern) ? numbers : e.CellStyle.ForeColor;
                                using (Brush brush = new SolidBrush(currentColor))
                                {
                                    e.Graphics.DrawString(arg3, e.CellStyle.Font, brush, currentX, top);
                                }
                                currentX += (charWidth / 2 * arg3.Length) + exOffset;
                            }

                            // ")"
                            using (Brush brush = new SolidBrush(e.CellStyle.ForeColor))
                            {
                                e.Graphics.DrawString(")", e.CellStyle.Font, brush, currentX, top);
                            }
                            currentX += (charWidth / 2) + exOffset;

                            continue;
                        }
                        else if (Regex.IsMatch(cleanedWord, numberPattern)) // numbers
                        {
                            currentColor = numbers;
                        }
                        else if (wordGroups.ContainsKey(operators) && wordGroups[operators].Contains(cleanedWord))
                        {
                            exOffset = charWidth / 4;
                            currentColor = targetWords[cleanedWord];
                        }
                        else if (targetWords.ContainsKey(cleanedWord))
                        {
                            currentColor = targetWords[cleanedWord];
                        }

                        using (Brush brush = new SolidBrush(currentColor))
                        {
                            e.Graphics.DrawString(displayWord, e.CellStyle.Font, brush, currentX + exOffset, top);
                        }
                        currentX += (charWidth / 2 * displayWord.Length) + exOffset;
                    }
                }

                e.Handled = true;
            }
            else
            {
                e.PaintBackground(e.CellBounds, true);
                e.PaintContent(e.CellBounds);
            }
        }

        #endregion

        #region Data Pool

        private void tbpDataPool_Enter(object sender, EventArgs e)
        {
            DoubleBufferedDataGridView.Initialize(dgvPool);
            CreatePoolColumns();
            dgvPool.RowTemplate.Height = 28;
            dgvPool.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvPool.SelectionMode = DataGridViewSelectionMode.CellSelect;

            dirty.Push(true);

            int colCount = 4;
            int rowIndex = -1;
            for (int i = 0; i < goolentry.Data.Length; i++)
            {
                int off = i & 0x3FF;
                int cval = goolentry.Data[off];
                string ename = Entry.EIDToEName(cval);

                int col = i % colCount;
                if (col == 0)
                {
                    rowIndex = dgvPool.Rows.Add();
                }
                dgvPool.Rows[rowIndex].Cells[col].Value = ename;
                var tagValue = $"{cval}, {ename}";
                dgvPool.Rows[rowIndex].Cells[col].Tag = tagValue;
            }

            dirty.Pop();

            tbpDataPool.Enter -= tbpDataPool_Enter;
        }

        private void CreatePoolColumns()
        {
            dgvPool.Columns.Add("", "");
            dgvPool.Columns.Add("", "");
            dgvPool.Columns.Add("", "");
            dgvPool.Columns.Add("", "");

            foreach (DataGridViewColumn column in dgvPool.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 64;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void dgvPool_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox tb)
            {
                if (poolShowAsEID)
                    tb.MaxLength = 5;
                else
                    tb.MaxLength = 8;
            }
        }

        private void dgvPool_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var cell = dgvPool.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.Value == null)
                e.Cancel = true;
        }

        private void dgvPool_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var cell = dgvPool.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Tag == null) return;

            string inputValue = e.FormattedValue.ToString();
            if (!string.IsNullOrEmpty(inputValue))
            {
                if (poolShowAsEID)
                {
                    string input = Entry.CheckEIDErrors(inputValue, true);
                    if (input != string.Empty)
                    {
                        DarkMessageBox.ShowError($"Invalid EID string.", Resources.Title_InputError);
                        e.Cancel = true;
                    }
                }
            }
            else
            {
                DarkMessageBox.ShowError("Input cannot be empty.", Resources.Title_InputError);
                e.Cancel = true;
            }
        }

        private void dgvPool_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Dirty) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !(dgvPool.SelectedCells.Count > 0)) return;

            var cell = dgvPool.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value != null)
            {
                string ename = cell.Value.ToString();
                int poolIndex = e.RowIndex * 4 + e.ColumnIndex;
                if (poolIndex < goolentry.Data.Length)
                {
                    int eid = Entry.ENameToEID(ename);
                    goolentry.Data[poolIndex] = eid;
                }
            }
        }

        private void dgvPool_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || !poolShowAsEID) return;

            string text = e.Value.ToString();

            if (text.EndsWith("A")) // sound
                e.CellStyle.ForeColor = states;
            else if (text.EndsWith("G")) // model
                e.CellStyle.ForeColor = logicals;
            else if (text.EndsWith("V")) // animation
                e.CellStyle.ForeColor = titles;
            else if (text.EndsWith("C")) // GOOL entry
                e.CellStyle.ForeColor = Color.MediumSeaGreen;
            else if (text.EndsWith("T")) // texture
                e.CellStyle.ForeColor = numbers;
            else if (text.EndsWith("O")) // voice
                e.CellStyle.ForeColor = Color.SeaShell;
            else if (text.EndsWith("D")) // image
                e.CellStyle.ForeColor = globals;
            else if (text.EndsWith("I")) // unassigned
                e.CellStyle.ForeColor = statements;
            else
                e.CellStyle.ForeColor = comments;
        }

        private void chkPoolToggleEID_CheckedChanged(object sender, EventArgs e)
        {
            dirty.Push(true);

            if (chkPoolToggleEID.Checked)
            {
                foreach (DataGridViewRow row in dgvPool.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value == null) continue;

                        string tag = cell.Tag.ToString();
                        string[] parts = tag.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 2)
                        {
                            string ename = parts[1];
                            cell.Value = ename;
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgvPool.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value == null) continue;

                        string tag = cell.Tag.ToString();
                        string[] parts = tag.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 2)
                        {
                            string eidn = parts[0];
                            int eid = Convert.ToInt32(eidn);
                            cell.Value = eid.ToString("X8");
                        }
                    }
                }
            }

            dirty.Pop();
        }

        #endregion

        #region State Map

        private void tbpStateMap_Enter(object sender, EventArgs e)
        {
            DoubleBufferedDataGridView.Initialize(dgvStateMap);
            CreateStateMapColumns();
            dgvStateMap.RowTemplate.Height = 24;
            dgvStateMap.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvStateMap.SelectionMode = DataGridViewSelectionMode.CellSelect;

            dirty.Push(true);

            var rows = dgvStateMap.Rows;
            for (int i = 0; i < goolentry.EventCount; ++i)
            {
                GetEventName(i, out string eventName);
                if (goolentry.StateMap[i] == 0xFF)
                {
                    rows.Add(eventName, i, "-");
                }
                else
                {
                    if ((goolentry.StateMap[i] & 0x8000) != 0)
                    {
                        int offset = goolentry.StateMap[i] & 0x3FFF;
                        rows.Add(eventName, i, $"sub {offset}");
                    }
                    else
                    {
                        rows.Add(eventName, i, $"state {goolentry.StateMap[i]}");
                    }
                }
            }

            dirty.Pop();

            tbpStateMap.Enter -= tbpStateMap_Enter;
        }

        private void GetEventName(int eventIndex, out string eventName)
        {
            eventName = $"Event_{eventIndex}";
            switch (eventIndex)
            {
                case 0: eventName = "EventJumpedOn"; break;
                case 2: eventName = "EventAddFruit"; break;
                case 3: eventName = "EventHit"; break;
                case 5: eventName = "EventAddLives"; break;
                case 6: eventName = "EventAddDoctor"; break;
                case 7: eventName = "EventHit"; break; // other EventHit
                case 4: eventName = "EventSpinHit"; break;
                case 8: eventName = "EventTriggered"; break;
                case 9: eventName = "EventFallKill"; break; // also: EventBoxStackBreak
                case 10: eventName = "EventHitInvincible"; break;
                case 12: eventName = "EventInstantKill"; break;
                case 14: eventName = "EventSquash"; break; // other EventSquash
                case 15: eventName = "EventStatus"; break;
                case 16: eventName = "EventCombo"; break;
                case 17: eventName = "EventAddLives"; break;
                case 18: eventName = "EventWade"; break; // WilaC
                case 19: eventName = "EventRespawn"; break;
                case 20: eventName = "EventEat"; break;
                case 21: eventName = "EventBounce"; break; // also: EventPushPlayerBack
                case 22: eventName = "EventWarp"; break;
                case 24: eventName = "EventHit2"; break;// EventHit, but undodgeable by spin
                case 25: eventName = "EventSquash"; break;
                case 26: eventName = "EventDespawn"; break;
                case 29: eventName = "EventFling"; break;
                case 30: eventName = "EventExplode"; break;
                case 31: eventName = "EventBurn"; break;
                case 32: eventName = "EventPush"; break;
                case 33: eventName = "EventDrown"; break;
                case 34: eventName = "EventStuck"; break;
                case 35: eventName = "EventShock"; break;
                case 36: eventName = "EventAddFruit"; break;
                case 37: eventName = "EventSquash"; break; // other EventSquash
                case 38: eventName = "EventHit3"; break; // EventHit, but ignores masks
                case 39: eventName = "EventGetInvincible"; break;
                case 40: eventName = "EventAddDoctor"; break;
                case 42: eventName = "EventAddFruit"; break;
                case 44: eventName = "EventShock2"; break; // EventShock, but undodgeable by spin
                case 45: eventName = "EventSlideHit"; break;
                case 46: eventName = "EventSlamHit"; break;
                case 47: eventName = "EventTransElevator"; break;
                case 50: eventName = "EventTransWarp"; break;
                case 51: eventName = "EventHang"; break; // WildC
                case 53: eventName = "EventStatus2"; break; // WildC
                case 54: eventName = "EventStatus2"; break; // WildC
                case 55: eventName = "EventStatus2"; break; // WildC
                case 57: eventName = "EventSlip"; break;
                case 58: eventName = "EventTriggerJetpack"; break; // WiliC
                case 59: eventName = "EventMinesExploded"; break;
                case 60: eventName = "EventAccelerate"; break;
                case 61: eventName = "EventSmash"; break;
                case 62: eventName = "EventExplosiveSeedDeath"; break;
                case 63: eventName = "EventDidgeridoo"; break;
                case 65: eventName = "EventTriggerBear"; break; // WiltC
                case 66: eventName = "EventTriggerJetboard"; break; // WilkC
                case 70: eventName = "EventTransWarpBonus"; break;
                case 71: eventName = "EventTriggerBonus"; break;
                case 72: eventName = "EventBoxBreak"; break;
                case 74: eventName = "EventBeeDeath"; break;
                case 76: eventName = "EventWarpRoomLookUp"; break; // Wil2C
                case 77: eventName = "EventWarpRoomOnElevator"; break; // Wil2C
                case 78: eventName = "EventWarpRoomWarpIn"; break; // Wil2C
                default: break;
            }
        }

        private void CreateStateMapColumns()
        {
            dgvStateMap.Columns.Add("Event Name", "Event Name");
            dgvStateMap.Columns.Add("Interrupt", "Interrupt");
            dgvStateMap.Columns.Add("State/Sub", "State/Sub");

            foreach (DataGridViewColumn column in dgvStateMap.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            dgvStateMap.Columns[0].Width = 160;
            dgvStateMap.Columns[1].Width = 80;
            dgvStateMap.Columns[2].Width = 100;
        }

        private void dgvStateMap_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex < 2)
                e.Cancel = true;
        }

        private void dgvStateMap_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex < 2) return;

            string inputValue = e.FormattedValue.ToString();
            if (!string.IsNullOrEmpty(inputValue))
            {
                if (inputValue == "-") return;

                var pattern = @"^(state|sub)\s+\d+$";
                var match = Regex.Match(inputValue, pattern, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    DarkMessageBox.ShowError($"Invalid state/sub string. Input must be one of the following:\r\n“state <number>”\r\n“sub <number>”\r\n“-”", Resources.Title_InputError);
                    e.Cancel = true;
                }
            }
            else
            {
                DarkMessageBox.ShowError("Input cannot be empty.", Resources.Title_InputError);
                e.Cancel = true;
            }
        }

        private void dgvStateMap_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Dirty) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !(dgvStateMap.SelectedCells.Count > 0)) return;

            var cell = dgvStateMap.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value != null)
            {
                string text = cell.Value.ToString();

                if (text == "-")
                {
                    goolentry.StateMap[e.RowIndex] = 0xFF;
                    return;
                }

                var parts = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    string type = parts[0].ToLower();
                    int number = int.Parse(parts[1]);
                    if (type == "state")
                    {
                        goolentry.StateMap[e.RowIndex] = (short)(number & 0x3FFF);
                    }
                    else if (type == "sub")
                    {
                        goolentry.StateMap[e.RowIndex] = (short)((number & 0x3FFF) | 0x8000);
                    }
                }
            }
        }

        private void dgvStateMap_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null) return;

            string text = e.Value.ToString();

            if (text.StartsWith("sub"))
                e.CellStyle.ForeColor = Color.Turquoise;
        }

        #endregion

        #region State Descriptors

        private void tbpStateDescriptors_Enter(object sender, EventArgs e)
        {
            DoubleBufferedDataGridView.Initialize(dgvStateDescriptors);
            CreateStateDescriptorsColumns();
            dgvStateDescriptors.RowTemplate.Height = 24;
            dgvStateDescriptors.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvStateDescriptors.SelectionMode = DataGridViewSelectionMode.CellSelect;

            dirty.Push(true);

            var rows = dgvStateDescriptors.Rows;
            for (int i = 0; i < goolentry.StateDescriptors.Count; ++i)
            {
                int stateFlags = goolentry.StateDescriptors[i].StateFlags;
                int blockFlags = goolentry.StateDescriptors[i].BlockFlags;
                short epc = (short)(goolentry.StateDescriptors[i].EventHook & 0x3FFF);
                short cpc = (short)(goolentry.StateDescriptors[i].CodeHook & 0x3FFF);
                short tpc = (short)(goolentry.StateDescriptors[i].TransHook & 0x3FFF);
                int externalIndex = goolentry.StateDescriptors[i].GOOLIndex;
                int externalEID = goolentry.Data[externalIndex];

                int rowIndex = rows.Add(i, stateFlags, blockFlags, epc, cpc, tpc, Entry.EIDToEName(externalEID));
                var row = rows[rowIndex];

                MarkExternal(row, ColEventBlock, (goolentry.StateDescriptors[i].EventHook & 0x4000) != 0);
                MarkExternal(row, ColCodeBlock, (goolentry.StateDescriptors[i].CodeHook & 0x4000) != 0);
                MarkExternal(row, ColTransBlock, (goolentry.StateDescriptors[i].TransHook & 0x4000) != 0);
                MarkNull(row, ColEventBlock, epc == 0x3FFF);
                MarkNull(row, ColCodeBlock, cpc == 0x3FFF);
                MarkNull(row, ColTransBlock, tpc == 0x3FFF);
                row.Cells[ColExternal].Tag = externalIndex;
            }

            numExternalIndex.Maximum = goolentry.Data.Length - 1;
            if (rows.Count > 0)
            {
                numExternalIndex.Value = rows[0].Cells[ColExternal].Tag != null ?
                    Convert.ToInt32(rows[0].Cells[ColExternal].Tag) : 0;
            }

            dirty.Pop();

            numExternalIndex.MouseWheel += ScrollHandlerFunction;

            tbpStateDescriptors.Enter -= tbpStateDescriptors_Enter;
        }

        private void MarkExternal(DataGridViewRow row, int col, bool isexternal)
        {
            if (!isexternal) return;
            row.Cells[col].Tag = "external";
        }

        private void MarkNull(DataGridViewRow row, int col, bool isnull)
        {
            if (!isnull) return;
            row.Cells[col].Tag = "null";
            row.Cells[col].Value = "-";
        }

        private void CreateStateDescriptorsColumns()
        {
            dgvStateDescriptors.Columns.Add("State", "State");
            dgvStateDescriptors.Columns.Add("State Flags", "State Flags");
            dgvStateDescriptors.Columns.Add("Block Flags", "Block Flags");
            dgvStateDescriptors.Columns.Add("Event", "Event");
            dgvStateDescriptors.Columns.Add("Code", "Code");
            dgvStateDescriptors.Columns.Add("Trans", "Trans");
            dgvStateDescriptors.Columns.Add("External EID", "External EID");

            foreach (DataGridViewColumn column in dgvStateDescriptors.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 80;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            dgvStateDescriptors.Columns[ColState].Width = 60;
            dgvStateDescriptors.Columns[ColStateFlags].Width = 90;
            dgvStateDescriptors.Columns[ColBlockFlags].Width = 90;
        }

        private void dgvStateDescriptors_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == ColState || e.ColumnIndex == ColExternal)
                e.Cancel = true;
        }

        private void dgvStateDescriptors_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == ColState) return;

            string inputValue = e.FormattedValue.ToString();
            if (!string.IsNullOrEmpty(inputValue))
            {
                if (e.ColumnIndex == ColStateFlags || e.ColumnIndex == ColBlockFlags)
                {
                    string text = inputValue.Trim();
                    if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                        text = text.Substring(2);
                    if (!long.TryParse(text,
                        System.Globalization.NumberStyles.HexNumber,
                        null,
                        out long hexValue)
                        || hexValue < 0 || hexValue > 0xFFFFFFFF)
                    {
                        DarkMessageBox.ShowError($"Invalid input. It must be between 0x0 and 0xFFFFFFFF.", Resources.Title_InputError);
                        e.Cancel = true;
                    }
                }
                else if (e.ColumnIndex == ColEventBlock || e.ColumnIndex == ColCodeBlock || e.ColumnIndex == ColTransBlock)
                {
                    string text = inputValue.Trim();
                    if (text != "-")
                    {
                        bool failed = false;
                        if (!int.TryParse(text, out int intValue) || intValue < 0 || intValue > 0x3FFE || text.Contains('-', StringComparison.OrdinalIgnoreCase))
                        {
                            DarkMessageBox.ShowError($"Invalid input. It must be between 0 and 16382, or “-”.", Resources.Title_InputError);
                            e.Cancel = true;
                        }
                    }
                }
            }
            else
            {
                DarkMessageBox.ShowError("Input cannot be empty.", Resources.Title_InputError);
                e.Cancel = true;
            }
        }

        private void dgvStateDescriptors_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Dirty) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !(dgvStateDescriptors.SelectedCells.Count > 0)) return;

            var row = dgvStateDescriptors.Rows[e.RowIndex];
            var cell = row.Cells[e.ColumnIndex];

            if (e.ColumnIndex == ColStateFlags || e.ColumnIndex == ColBlockFlags)
            {
                int intValue = Convert.ToInt32(cell.Value);

                UpdateStateDescriptor(e.RowIndex, e.ColumnIndex, intValue);
            }
            else if (e.ColumnIndex == ColEventBlock || e.ColumnIndex == ColCodeBlock || e.ColumnIndex == ColTransBlock)
            {
                short hookValue = 0;
                string text = cell.Value.ToString().Trim();
                if (text == "-")
                {
                    hookValue = 0x3FFF;
                    cell.Tag = "null";
                }
                else
                {
                    int intValue = int.Parse(text);
                    hookValue = (short)(intValue & 0x3FFF);
                    if (cell.Tag != null && cell.Tag.ToString() == "external")
                    {
                        hookValue |= 0x4000;
                    }
                    else
                    {
                        cell.Tag = null;
                    }
                }
                
                UpdateStateDescriptor(e.RowIndex, e.ColumnIndex, hookValue);
            }
        }

        private void UpdateStateDescriptor(int row, int col, object value)
        {
            GOOLStateDescriptor descriptor = goolentry.StateDescriptors[row];
            if (col == ColStateFlags)
                descriptor.StateFlags = Convert.ToInt32(value);
            else if (col == ColBlockFlags)
                descriptor.BlockFlags = Convert.ToInt32(value);
            else if (col == ColEventBlock)
                descriptor.EventHook = Convert.ToInt16(value);
            else if (col == ColCodeBlock)
                descriptor.CodeHook = Convert.ToInt16(value);
            else if (col == ColTransBlock)
                descriptor.TransHook = Convert.ToInt16(value);
            else if (col == ColExternal)
                descriptor.GOOLIndex = Convert.ToInt16(value);
            goolentry.StateDescriptors[row] = descriptor;
        }

        private void dgvStateDescriptors_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (e.Value == null) return;

            // Parse hex values for State Flags and Block Flags
            if (e.ColumnIndex == ColStateFlags || e.ColumnIndex == ColBlockFlags)
            {
                string text = e.Value.ToString().Trim();

                if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    text = text.Substring(2);

                if (int.TryParse(text,
                    System.Globalization.NumberStyles.HexNumber,
                    null,
                    out int hexValue))
                {
                    e.Value = hexValue;
                    e.ParsingApplied = true;
                }
                else
                {
                    e.ParsingApplied = false;
                }
            }
        }

        private void dgvStateDescriptors_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var row = dgvStateDescriptors.Rows[e.RowIndex];

            // Format State Flags and Block Flags as hex
            if (e.ColumnIndex == ColStateFlags || e.ColumnIndex == ColBlockFlags)
            {
                if (e.Value is int intValue)
                {
                    e.Value = $"0x{intValue:X}";
                    e.FormattingApplied = true;
                }
            }

            if (row.Cells[e.ColumnIndex].Tag != null)
            {
                if (e.ColumnIndex == ColExternal)
                {
                    int externalIndex = Convert.ToInt32(row.Cells[ColExternal].Tag);
                    if (externalIndex != 0)
                    {
                        e.CellStyle.ForeColor = Color.MediumSeaGreen;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = comments;
                    }
                }
                else // Event, Code, Trans
                {
                    if (row.Cells[e.ColumnIndex].Tag.ToString() == "external")
                    {
                        e.CellStyle.ForeColor = Color.Turquoise;
                    }
                    else if (row.Cells[e.ColumnIndex].Tag.ToString() == "null")
                    {
                        e.CellStyle.ForeColor = comments;
                    }
                }
            }
        }

        private void dgvStateDescriptors_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStateDescriptors.SelectedCells.Count == 0) return;

            dirty.Push(true);

            var cell = dgvStateDescriptors.SelectedCells[0];
            var row = dgvStateDescriptors.Rows[cell.RowIndex];

            lblExternalEID.Text = string.Empty;
            numExternalIndex.Value = row.Cells[ColExternal].Tag != null ?
                Convert.ToInt32(row.Cells[ColExternal].Tag) : 0;

            if (cell.ColumnIndex == ColState ||
                cell.ColumnIndex == ColStateFlags ||
                cell.ColumnIndex == ColBlockFlags ||
                cell.ColumnIndex == ColExternal)
            {
                chkIsExternal.Enabled = false;
                chkIsExternal.Checked = false;
            }
            else
            {
                // Disable checkbox if cell tag is null
                if (cell.Tag != null && cell.Tag.ToString() == "null")
                {
                    chkIsExternal.Enabled = false;
                    chkIsExternal.Checked = false;
                }
                else
                {
                    chkIsExternal.Enabled = true;

                    // Set checkbox and label based on cell tag 
                    if (cell.Tag != null && cell.Tag.ToString() == "external")
                    {
                        chkIsExternal.Checked = true;
                        lblExternalEID.Text = dgvStateDescriptors.Rows[cell.RowIndex].Cells[6].Value.ToString();
                    }
                    else
                    {
                        chkIsExternal.Checked = false;
                    }
                }
            }

            dirty.Pop();
        }


        private void chkIsExternal_CheckedChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            if (dgvStateDescriptors.SelectedCells.Count == 0) return;

            var cell = dgvStateDescriptors.SelectedCells[0];
            short value = Convert.ToInt16(cell.Value);

            if (chkIsExternal.Checked)
            {
                cell.Tag = "external";
                lblExternalEID.Text = dgvStateDescriptors.Rows[cell.RowIndex].Cells[ColExternal].Value.ToString();
                value |= 0x4000;
            }
            else
            {
                cell.Tag = null;
                lblExternalEID.Text = string.Empty;
            }

            UpdateStateDescriptor(cell.RowIndex, cell.ColumnIndex, value);
        }

        private void numExternalIndex_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            if (dgvStateDescriptors.SelectedCells.Count == 0) return;

            var cell = dgvStateDescriptors.SelectedCells[0];
            int externalIndex = Convert.ToInt32(numExternalIndex.Value);
            int externalEID = goolentry.Data[externalIndex];
            dgvStateDescriptors.Rows[cell.RowIndex].Cells[ColExternal].Value = Entry.EIDToEName(externalEID);
            dgvStateDescriptors.Rows[cell.RowIndex].Cells[ColExternal].Tag = externalIndex;
            if (chkIsExternal.Checked)
            {
                lblExternalEID.Text = Entry.EIDToEName(externalEID);
            }
            UpdateStateDescriptor(cell.RowIndex, ColExternal, externalIndex);
        }

        #endregion
    }
}
