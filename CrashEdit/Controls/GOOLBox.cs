using System;
using System.Net;
using System.Text.RegularExpressions;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using MetroSet_UI.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CrashEdit.CE
{
    public sealed class GOOLBox : UserControl
    {
        private GOOLEntryController controller;
        private GOOLEntry goolentry;

        private readonly DataGridView dgvCode;

        List<int> indents;
        List<int> processedRows;

        private int headerCount;
        private int addressIndex;

        public GOOLBox(GOOLEntryController controller, GOOLEntry goolentry)
        {
            this.controller = controller;
            this.goolentry = goolentry;

            BackColor = Color.FromArgb(31, 31, 32);

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
            CreateTabs();
        }

        private void CreateTabs()
        {
            if (!(goolentry.Version == GOOLVersion.Version0))
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
                TabPage tab1 = new TabPage("Code");
                tab1.Controls.Add(dgvCode);
                TabPage tab2 = new TabPage("Frame Groups");
                var goolFrameGroupBox = new GOOLFrameGroupBox(controller, goolentry)
                {
                    Dock = DockStyle.Fill
                };
                tab2.Controls.Add(goolFrameGroupBox);

                tbcTabs.TabPages.Add(tab1);
                tbcTabs.TabPages.Add(tab2);

                EventHandler tabChangedHandler = null;
                tabChangedHandler = (sender, e) =>
                {
                    if (tbcTabs.SelectedTab == tab2)
                    {
                        goolFrameGroupBox.OnTabSelected();
                        tbcTabs.SelectedIndexChanged -= tabChangedHandler;
                    }
                };
                tbcTabs.SelectedIndexChanged += tabChangedHandler;

                tbcTabs.SelectedTab = tab1;
                Controls.Add(tbcTabs);
            }
            else
            {
                Controls.Add(dgvCode);
            }

        }

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
    }
}
