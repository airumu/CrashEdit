using System.Data.Common;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public sealed class GOOLBox : UserControl
    {
        private readonly DataGridView dgvCode;

        private int headerCount;
        private int titleCount;
        private int addressIndex;

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
                //ColumnHeadersVisible = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToOrderColumns = false,

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
            dgvCode.CellClick += dgvCode_CellClick;

            headerCount = 0;
            addressIndex = 0;

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
                    
                    rows.Add($"State_{i} [{Entry.EIDToEName(stategooleid)}] (State Flags: {string.Format("0x{0:X}", goolentry.StateDescriptors[i].StateFlags)} | Block Flags: {string.Format("0x{0:X}", goolentry.StateDescriptors[i].BlockFlags)}))");
                    if (epc != 0x3FFF)
                    {
                        addressIndex = rows.Add($"    Event: {epc}" + ((goolentry.StateDescriptors[i].EventHook & 0x4000) != 0 ? " (external)" : ""));
                        dgvCode.Rows[addressIndex].Tag = epc;
                        ++addressIndex;
                    }
                    else
                        rows.Add("      (no event hook)");
                    if (cpc != 0x3FFF)
                    {
                        addressIndex = rows.Add($"    Code: {cpc}" + ((goolentry.StateDescriptors[i].CodeHook & 0x4000) != 0 ? " (external)" : ""));
                        dgvCode.Rows[addressIndex].Tag = cpc;
                        ++addressIndex;
                    }
                    else
                        rows.Add("      ERROR! No code thread! This state will not work.");
                    if (tpc != 0x3FFF)
                    {
                        addressIndex = rows.Add($"    Trans: {tpc}" + ((goolentry.StateDescriptors[i].TransHook & 0x4000) != 0 ? " (external)" : ""));
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

                int instIndex = dgvCode.Rows.Add($"{i,-6} {ins.GetName(),-6} {ins.Arguments,-28} {(!string.IsNullOrWhiteSpace(ins.GetComment()) ? $"# {ins.GetComment()}" : "")}");
                dgvCode.Rows[instIndex].Tag = i;
                ++instIndex;
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

        private void AddHookInfo(List<(string Index, string Description)> rows, string type, int value)
        {
            if ((value & 0x3FFF) != 0x3FFF)
                rows.Add(("", $"    {type}: {value & 0x3FFF}" + ((value & 0x4000) != 0 ? " (external)" : "")));
            else
                rows.Add(("", $"      (no {type.ToLower()} hook)"));
        }

        private void AddLabel(Dictionary<int, List<string>> labels, int hook, string label)
        {
            if ((hook & 0x3FFF) != 0x3FFF)
            {
                if (!labels.ContainsKey(hook))
                    labels[hook] = new List<string>();
                labels[hook].Add(label);
            }
        }

        private void dgvCode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0 && e.RowIndex < headerCount && dgvCode.Rows[e.RowIndex].Tag != null)
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

        private string ExtractNumbers(string input)
        {
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(input);

            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                return string.Empty;
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
            dataGridView.GridColor = Color.FromArgb(40, 40, 40);

            // Default style for cells
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dataGridView.DefaultCellStyle.ForeColor = Color.White;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 70, 70);
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
