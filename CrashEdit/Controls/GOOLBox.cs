using System.Text.RegularExpressions;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public sealed class GOOLBox : UserControl
    {
        private readonly DataGridView dgvCode;

        List<int> indentEndIndexes = new List<int>();
        List<int> processedRows = new List<int>();

        private int headerCount;
        private int titleCount;
        private int addressIndex;
        private string indent;
        private int lockindent;

        public GOOLBox(GOOLEntry goolentry)
        {
            BackColor = Color.FromArgb(31, 31, 32);

            dgvCode = new DataGridView
            {
                Dock = DockStyle.Fill,
                Font = new Font("Cascadia Code SemiLight", 8F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                ColumnHeadersVisible = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToOrderColumns = false,
                ShowCellToolTips = false
            };
            SetDarkTheme(dgvCode);
            EnableDoubleBuffering();
            dgvCode.RowTemplate.Height = 16;
            dgvCode.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvCode.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                HeaderText = "Description",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            dgvCode.CellPainting += dgvCode_CellPainting;
            dgvCode.CellClick += dgvCode_CellClick;

            headerCount = 0;
            addressIndex = 0;
            indent = string.Empty;
            indentEndIndexes = new List<int>();
            processedRows = new List<int>();
            lockindent = 1;

            PopulateData(goolentry);

            Controls.Add(dgvCode);
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
                headerCount ++;
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
                            rows.Add($"    Interrupt {i}: State_{goolentry.StateMap[i]}");
                        ++headerCount;
                    }
                }

                rows.Add($"Available Subtypes: {goolentry.StateMap.Length - goolentry.EventCount}");
                ++headerCount;

                // Process subtypes
                for (int i = goolentry.EventCount; i < goolentry.StateMap.Length; ++i)
                {
                    if (i > goolentry.EventCount && i + 1 == goolentry.StateMap.Length && goolentry.StateMap[i] == 0) continue;
                    rows.Add($"    Subtype {i - goolentry.EventCount}: {(goolentry.StateMap[i] == 255 ? "invalid" : $"State_{goolentry.StateMap[i]}")}");
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
                        rows.Add("      (no event hook)");
                    if (cpc != 0x3FFF)
                    {
                        bool isexternal = (goolentry.StateDescriptors[i].CodeHook & 0x4000) != 0;
                        addressIndex = rows.Add($"    Code: {cpc}" + (isexternal ? " (external)" : ""));
                        if (!isexternal)
                            dgvCode.Rows[addressIndex].Tag = cpc;
                        ++addressIndex;
                    }
                    else
                        rows.Add("      ERROR! No code thread! This state will not work.");
                    if (tpc != 0x3FFF)
                    {
                        bool isexternal = (goolentry.StateDescriptors[i].TransHook & 0x4000) != 0;
                        addressIndex = rows.Add($"    Trans: {tpc}" + (isexternal ? " (external)" : ""));
                        if (!isexternal)
                            dgvCode.Rows[addressIndex].Tag = tpc;
                        ++addressIndex;
                    }
                    else
                        rows.Add("      (no trans hook)");
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
            string str;
            // Process instructions
            for (int i = 0; i < goolentry.Instructions.Count; ++i)
            {
                bool hastitle = false;
                if (labels.ContainsKey(i))
                {
                    foreach (string label in labels[i])
                    {
                        rows.Add(label);
                        ++titleCount;
                    }
                    returned = false;
                    hastitle = true;
                }
                if (returned)
                {
                    rows.Add($"Sub_{i}:");
                    ++titleCount;
                    hastitle = true;
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

                int instIndex = dgvCode.Rows.Add($"{i,-6} {insName,-6} {ins.Arguments,-32} {(!string.IsNullOrWhiteSpace(ins.GetComment()) ? $"# {indent}{ins.GetComment()}" : "")}");
                dgvCode.Rows[instIndex].Tag = i;
                processedRows.Add(instIndex);
                ++instIndex;

                int number = 0;
                if (!string.IsNullOrWhiteSpace(ins.GetComment()))
                {
                    string insComment = ins.GetComment();
                    if (insComment.Contains("move"))
                    {
                        string pattern = @"[-+]?\d+";
                        Match match = Regex.Match(insComment, pattern);
                        if (match.Success)
                        {
                            number = int.Parse(match.Value);
                            int indentEndIndex = instIndex + number;
                            indentEndIndexes.Add(indentEndIndex);
                            indent += "  ";

                            // if the number is minus
                            if (indentEndIndex < instIndex)
                            {
                                int rowsToModify = Math.Abs(number) - 1;
                                for (int j = processedRows.Count - 1; j >= 0 && rowsToModify > 0; j--)
                                {
                                    int previousRowIndex = processedRows[j];
                                    if (previousRowIndex < instIndex - 1)
                                    {
                                        DataGridViewRow row = dgvCode.Rows[previousRowIndex];
                                        string lineText = row.Cells[0].Value.ToString();
                                        int hashIndex = lineText.IndexOf('#');
                                        if (hashIndex != -1)
                                        {
                                            lineText = lineText.Insert(hashIndex + 1, "  ");
                                        }
                                        row.Cells[0].Value = lineText;

                                        rowsToModify--;
                                    }
                                }
                                indent = indent.Substring(2);
                            }
                        }
                    }
                }

                foreach (int indentEndIndex in indentEndIndexes)
                {
                    if (instIndex == indentEndIndex)
                    {
                        indent = indent.Substring(2);
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

                //foreach (var row in rows)
                //{
                //    dgvCode.Rows.Add(row.Index, row.Description);
                //}
            }
        }

        private void dgvCode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0 && dgvCode.Rows[e.RowIndex].Tag != null)
            {
                if (e.RowIndex < headerCount)
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
                else
                {
                    string cellText = dgvCode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";

                    if (cellText.Contains("instructions"))
                    {
                        string target = "move";
                        string numberPattern = $@"(?<={Regex.Escape(target)}\s*)[-+]?\d+";
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
                        string numberPattern = $@"(?<={Regex.Escape(target)}\s*)[-+]?\d+";
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
                        string numberPattern = $@"(?<={Regex.Escape(target)}\s*)[-+]?\d+";
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
                                    dgvCode.CurrentCell = row.Cells[0];
                                }
                            }
                        }
                    }
                }
            }
        }

        private void dgvCode_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string cellText = dgvCode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";

                var targetWords = new Dictionary<string, Color>();

                Color title =      Color.FromArgb(93, 179, 149);  // teal
                Color keywords =   Color.FromArgb(114, 159, 255); // blue
                Color states =     Color.FromArgb(129, 198, 255); // sky blue
                Color logicals =   Color.FromArgb(255, 130, 130); // red
                Color numbers =    Color.FromArgb(240, 200, 87);  // orange
                Color classes =    Color.FromArgb(216, 160, 213); // pink
                Color names =      Color.FromArgb(91, 191, 139);  // green
                Color operators =  Color.FromArgb(255, 166, 77);  // red-orange

                var wordGroups = new Dictionary<Color, string[]>
                {
                    { Color.Gray, new[] { "#" } },
                    { keywords, new[] { "move", "go", "change", "call", "to", "at" } },
                    { states, new[] { "state", "instructions","subroutine" } },
                    { logicals, new[] { "true", "false", "accept", "reject" } },
                    { classes, new[] { "if", "else", "return" } },
                    //{ names, new[] { "sp", "[sp]" } },
                    { names, new[] { "play", "set", "spawn", "force", "send", "push", "pop" } },
                    { operators, new[] { "=", "==", "!", "!!", "!=", "|", "||", "|=", "&", "&&", "&=", "^", ">", ">>", ">=", "<", "<<", "<=", "+", "+=", "-", "-=", "*", "*=", "/", "/=", "%" } }
                };

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

                var numberPattern = @"^([-+]?(\(\s*[-+]?\d+(\.\d+)?\s*\)|\(\s*[-+]?(0x[0-9a-fA-F]+)\s*\)|\d+(\.\d+)?|0x[0-9a-fA-F]+))$";
                var statePattern = @"^(State_\d+_(event|code|trans):|Sub_\d+:)$";

                using (Brush defaultBrush = new SolidBrush(e.CellStyle.ForeColor))
                {
                    string[] words = cellText.Split(' ');

                    int charWidth = TextRenderer.MeasureText("A", e.CellStyle.Font).Width;
                    foreach (string word in words)
                    {
                        string cleanedWord = word.TrimEnd(',');
                        string displayWord = word + " ";
                        Color currentColor = e.CellStyle.ForeColor;

                        if (Regex.IsMatch(cleanedWord, statePattern))
                        {
                            currentColor = title;
                        }
                        else if (Regex.IsMatch(cleanedWord, numberPattern))
                        {
                            currentColor = numbers; // numbers
                        }
                        else if (targetWords.ContainsKey(cleanedWord))
                        {
                            currentColor = targetWords[cleanedWord];
                        }

                        using (Brush brush = new SolidBrush(currentColor))
                        {
                            e.Graphics.DrawString(displayWord, e.CellStyle.Font, brush, currentX, top);
                        }

                        currentX += charWidth / 2 * displayWord.Length;
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

        private void EnableDoubleBuffering()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, dgvCode, new object[] { true });
        }

        private void SetDarkTheme(DataGridView dataGridView)
        {
            // Background color of the entire grid
            dataGridView.BackgroundColor = Color.FromArgb(30, 30, 30);

            // Color of the grid lines
            dataGridView.GridColor = Color.FromArgb(30, 30, 30);

            // Default style for cells
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dataGridView.DefaultCellStyle.ForeColor = Color.White;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;

            // Style for column headers
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Style for row headers
            dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dataGridView.RowHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // Background color for odd and even rows
            dataGridView.RowsDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);

            // Row border style
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Header and gridline styles
            dataGridView.EnableHeadersVisualStyles = false;

            // Additional settings
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }
    }
}
