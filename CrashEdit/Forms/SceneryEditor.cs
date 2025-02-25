using System.Text.RegularExpressions;
using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.CE.Controls;
using CrashEdit.Crash;
using Cyotek.Windows.Forms;

namespace CrashEdit.CE
{
    public sealed class SceneryEditor : DarkForm
    {
        CheckBox chkLowestBrightness;
        DarkNumericUpDown numLowestBrightness;

        public SceneryEditor(NSF nsf)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            AutoSize = true;
            Text = "Scenery Editor";
            Icon = Embeds.GetIcon("Wrench");
            Width = 328;
            Height = 532;

            bool dialogResult = false;

            List<string> lstSceneries = new();
            List<string> lstIgnoreSceneries = new();

            Dictionary<SceneryEntry, List<SceneryColor>> sceneries = new();
            foreach (SceneryEntry scenery in nsf.GetEntries<SceneryEntry>())
            {
                lstSceneries.Add(scenery.EName);
                sceneries[scenery] = scenery.Colors.Select(c => new SceneryColor
                {
                    Red = c.Red,
                    Green = c.Green,
                    Blue = c.Blue,
                    Extra = c.Extra
                }).ToList();
            }
            lstSceneries.Sort();

            FlowLayoutPanel flowMain = new()
            {
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
            };

            FlowLayoutPanel flowBrightness = new()
            {
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
            };

            numLowestBrightness = new()
            {
                DecimalPlaces = 2,
                Enabled = false,
                Increment = new decimal(new int[] { 5, 0, 0, 131072 }),
                Maximum = new decimal(new int[] { 1, 0, 0, 0 }),
                Size = new Size(56, 23)
            };
            numLowestBrightness.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);
            chkLowestBrightness = new()
            {
                Text = "Ignore colors with brightness below:",
                AutoSize  = true,
                Padding = new Padding(9, 3, 3, 3)
            };
            chkLowestBrightness.CheckedChanged += (sender, e) =>
            {
                numLowestBrightness.Enabled = chkLowestBrightness.Checked;
            };

            ColorEditor editor = new()
            {
                Size = new Size(300, 96),
                Color = Color.FromArgb(0, 0, 0),
                ShowAlphaChannel = false,
                ShowColorSpaceLabels = false,
                ShowHex = false,
                ShowRgb = false,
                Padding = new Padding(12)
            };
            {
                var hslColor = editor.HslColor;
                hslColor.L = 0.5;
                hslColor.S = 0.5;
                editor.HslColor = hslColor;
            }
            editor.ColorChanged += (sender, e) =>
            {
                foreach (SceneryEntry scenery in nsf.GetEntries<SceneryEntry>())
                {
                    if (lstSceneries.Contains(scenery.EName))
                    {
                        if (sceneries.TryGetValue(scenery, out List<SceneryColor>? sceneryColors))
                        {
                            for (int i = 0; i < sceneryColors.Count; i++)
                            {
                                Color rgbColor = Color.FromArgb(sceneryColors[i].Red, sceneryColors[i].Green, sceneryColors[i].Blue);
                                HslColor hslColor = new HslColor(rgbColor);
                                if (chkLowestBrightness.Checked)
                                {
                                    if (hslColor.L <= (double)numLowestBrightness.Value)
                                        continue;
                                }

                                hslColor = ModelBox.ChangeHue(hslColor, editor.HslColor.H);
                                hslColor.S = Math.Clamp(hslColor.S + (editor.HslColor.S - 0.5), 0.0, 1.0);
                                hslColor.L = Math.Clamp(hslColor.L + (editor.HslColor.L - 0.5), 0.0, 1.0);

                                Color newColor = hslColor.ToRgbColor();

                                SceneryColor updatedColor = sceneryColors[i];
                                updatedColor.Red = newColor.R;
                                updatedColor.Green = newColor.G;
                                updatedColor.Blue = newColor.B;
                                scenery.Colors[i] = updatedColor;
                            }
                        }
                    }
                }
            };

            TableLayoutPanel panel = new()
            {
                AutoSize = true,
                Padding = new Padding(12)
            };

            Label lblApply = new()
            {
                ForeColor = SystemColors.MenuText
            };
            lblApply.Paint += (sender, e) =>
            {
                string text = "Applied List";
                Font font = lblApply.Font;
                Brush textBrush = Brushes.Gainsboro;

                Image icon = Embeds.GetIcon("Hint")!.ToBitmap();
                int padding = 4; 

                SizeF textSize = e.Graphics.MeasureString(text, font);
                PointF textPosition = new PointF(0, (lblApply.Height - textSize.Height) / 2);
                Point iconPosition = new Point((int)(textSize.Width + padding), (lblApply.Height - icon.Height) / 2);

                e.Graphics.DrawString(text, font, textBrush, textPosition);
                e.Graphics.DrawImage(icon, iconPosition);
            };

            DarkToolTip tipApply = new();
            tipApply.SetToolTip(lblApply, "List of scenery entries to apply changes.\nThe filter supports regex.");

            Label lblIgnore = new()
            {
                ForeColor = SystemColors.MenuText
            };
            lblIgnore.Paint += (sender, e) =>
            {
                string text = "Ignored List";
                Font font = lblApply.Font;
                Brush textBrush = Brushes.Gainsboro;

                Image icon = Embeds.GetIcon("Hint")!.ToBitmap();
                int padding = 4;

                SizeF textSize = e.Graphics.MeasureString(text, font);
                PointF textPosition = new PointF(0, (lblApply.Height - textSize.Height) / 2);
                Point iconPosition = new Point((int)(textSize.Width + padding), (lblApply.Height - icon.Height) / 2);

                e.Graphics.DrawString(text, font, textBrush, textPosition);
                e.Graphics.DrawImage(icon, iconPosition);
            };

            DarkToolTip tipIgnore = new();
            tipIgnore.SetToolTip(lblIgnore, "List of scenery entries to ignore changes.\nThe filter supports regex.");

            DarkListBox lstToApply = new()
            {
                SelectionMode = SelectionMode.MultiExtended,
                DrawMode = DrawMode.OwnerDrawFixed,
                ItemHeight = 16,
                Height = 200
            };
            lstToApply.Items.AddRange(lstSceneries.ToArray());

            DarkListBox lstToIgnore = new()
            {
                SelectionMode = SelectionMode.MultiExtended,
                DrawMode = DrawMode.OwnerDrawFixed,
                ItemHeight = 16,
                Height = 200
            };

            DarkTextBox txtFilterApply = new()
            {
                Width = lstToApply.Width
            };
            txtFilterApply.TextChanged += (sender, e) =>
            {
                string query = txtFilterApply.Text.Trim();

                // Reset the list.
                if (string.IsNullOrEmpty(query))
                {
                    lstToApply.Items.Clear();
                    lstToApply.Items.AddRange(lstSceneries.ToArray());
                    return;
                }

                try
                {
                    List<string> filteredItems = new();
                    Regex regex = new Regex(query, RegexOptions.IgnoreCase);
                    filteredItems = lstSceneries.Where(item => regex.IsMatch(item)).ToList();
                    filteredItems.Sort((a, b) => b.CompareTo(a));

                    foreach (string item in filteredItems)
                    {
                        lstToApply.Items.Remove(item);
                        lstToApply.Items.Insert(0, item);
                    }
                    lstToApply.Invalidate();
                }
                catch (RegexParseException)
                {
                    return;
                }
            };

            lstToApply.DrawItem += (sender, e) =>
            {
                if (e.Index < 0) return;

                ListBox lb = (ListBox)sender!;
                string itemText = lb.Items[e.Index].ToString()!;

                Font font = new Font("Segoe UI", 9);

                string query = txtFilterApply.Text.Trim();
                Regex regex = new Regex(query, RegexOptions.IgnoreCase);
                Color textColor = !string.IsNullOrEmpty(query) && regex.IsMatch(itemText) ?
                    (e.State & DrawItemState.Selected) != 0 ? Color.DarkTurquoise : Color.Turquoise :
                    Color.Gainsboro;

                e.DrawBackground();

                using (Brush brush = new SolidBrush(textColor))
                {
                    e.Graphics.DrawString(itemText, font, brush, e.Bounds);
                }

                e.DrawFocusRectangle();
            };

            DarkTextBox txtFilterIgnore = new()
            {
                Width = lstToIgnore.Width
            };
            txtFilterIgnore.TextChanged += (sender, e) =>
            {
                string query = txtFilterIgnore.Text.Trim();

                // Reset the list.
                if (string.IsNullOrEmpty(query))
                {
                    lstToIgnore.Items.Clear();
                    lstToIgnore.Items.AddRange(lstIgnoreSceneries.ToArray());
                    return;
                }

                try
                {
                    List<string> filteredItems = new();
                    Regex regex = new Regex(query, RegexOptions.IgnoreCase);
                    filteredItems = lstIgnoreSceneries.Where(item => regex.IsMatch(item)).ToList();
                    filteredItems.Sort((a, b) => b.CompareTo(a));

                    foreach (string item in filteredItems)
                    {
                        lstToIgnore.Items.Remove(item);
                        lstToIgnore.Items.Insert(0, item);
                    }
                    lstToIgnore.Invalidate();
                }
                catch (RegexParseException)
                {
                    return;
                }
            };

            lstToIgnore.DrawItem += (sender, e) =>
            {
                if (e.Index < 0) return;

                ListBox lb = (ListBox)sender!;
                string itemText = lb.Items[e.Index].ToString()!;

                Font font = new Font("Segoe UI", 9);

                string query = txtFilterIgnore.Text.Trim();
                Regex regex = new Regex(query, RegexOptions.IgnoreCase);
                Color textColor = !string.IsNullOrEmpty(query) && regex.IsMatch(itemText) ?
                    (e.State & DrawItemState.Selected) != 0 ? Color.DarkTurquoise : Color.Turquoise :
                    Color.Gainsboro;

                e.DrawBackground();

                // Draw selected items background.
                Brush backgroundBrush = (e.State & DrawItemState.Selected) != 0 ? new SolidBrush(Color.FromArgb(0, 120, 215)) : Brushes.Transparent;
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);

                using (Brush brush = new SolidBrush(textColor))
                {
                    e.Graphics.DrawString(itemText, font, brush, e.Bounds);
                }

                e.DrawFocusRectangle();
            };

            DarkButton cmdAdd = new()
            {
                Text = "->"
            };
            cmdAdd.Click += (sender, e) =>
            {
                if (lstToApply.SelectedItems.Count > 0)
                {
                    var itemsToMove = lstToApply.SelectedItems.Cast<object>().ToList();
                    foreach (var item in itemsToMove)
                    {
                        lstToIgnore.Items.Add(item);
                        lstToApply.Items.Remove(item);
                    }

                    List<string> temp = new();
                    foreach (var item in lstToIgnore.Items)
                    {
                        temp.Add(item.ToString());
                    }
                    temp.Sort();
                    lstToIgnore.Items.Clear();
                    lstToIgnore.Items.AddRange(temp.ToArray());

                    lstSceneries.Clear();
                    lstSceneries.AddRange(lstToApply.Items.Cast<string>().ToArray());
                    lstIgnoreSceneries.Clear();
                    lstIgnoreSceneries.AddRange(lstToIgnore.Items.Cast<string>().ToArray());

                    // Call the TextChanged event.
                    txtFilterIgnore.Text += " ";
                    txtFilterIgnore.Text = txtFilterIgnore.Text.Trim();

                    foreach (SceneryEntry scenery in nsf.GetEntries<SceneryEntry>())
                    {
                        if (lstIgnoreSceneries.Contains(scenery.EName))
                        {
                            if (sceneries.TryGetValue(scenery, out List<SceneryColor>? sceneryColors))
                            {
                                for (int i = 0; i < sceneryColors.Count; i++)
                                {
                                    scenery.Colors[i] = sceneryColors[i];
                                }
                            }
                        }
                    }
                }
            };

            DarkButton cmdRemove = new()
            {
                Text = "<-"
            };
            cmdRemove.Click += (sender, e) =>
            {
                if (lstToIgnore.SelectedItems.Count > 0)
                {
                    var itemsToMove = lstToIgnore.SelectedItems.Cast<object>().ToList();
                    foreach (var item in itemsToMove)
                    {
                        lstToApply.Items.Add(item);
                        lstToIgnore.Items.Remove(item);
                    }

                    List<string> temp = new();
                    foreach (var item in lstToApply.Items)
                    {
                        temp.Add(item.ToString());
                    }
                    temp.Sort();
                    lstToApply.Items.Clear();
                    lstToApply.Items.AddRange(temp.ToArray());

                    lstSceneries.Clear();
                    lstSceneries.AddRange(lstToApply.Items.Cast<string>().ToArray());
                    lstIgnoreSceneries.Clear();
                    lstIgnoreSceneries.AddRange(lstToIgnore.Items.Cast<string>().ToArray());
                    // Call the TextChanged event.
                    txtFilterApply.Text += " ";
                    txtFilterApply.Text = txtFilterApply.Text.Trim();

                    foreach (SceneryEntry scenery in nsf.GetEntries<SceneryEntry>())
                    {
                        if (lstSceneries.Contains(scenery.EName))
                        {
                            if (sceneries.TryGetValue(scenery, out List<SceneryColor>? sceneryColors))
                            {
                                for (int i = 0; i < sceneryColors.Count; i++)
                                {
                                    Color rgbColor = Color.FromArgb(sceneryColors[i].Red, sceneryColors[i].Green, sceneryColors[i].Blue);
                                    HslColor hslColor = new HslColor(rgbColor);

                                    hslColor = ModelBox.ChangeHue(hslColor, editor.HslColor.H);
                                    hslColor.S = Math.Clamp(hslColor.S + (editor.HslColor.S - 0.5), 0.0, 1.0);
                                    hslColor.L = Math.Clamp(hslColor.L + (editor.HslColor.L - 0.5), 0.0, 1.0);

                                    Color newColor = hslColor.ToRgbColor();

                                    SceneryColor updatedColor = sceneryColors[i];
                                    updatedColor.Red = newColor.R;
                                    updatedColor.Green = newColor.G;
                                    updatedColor.Blue = newColor.B;
                                    scenery.Colors[i] = updatedColor;
                                }
                            }
                        }
                    }
                }
            };

            DarkGroupBox frabuttons = new()
            {
                Text = "Apply Changes",
                Size = new Size(308, 80)
            };

            DarkButton cmdApply = new()
            {
                Text = "Apply",
                Size = new Size(80, 30),
                Location = new Point(10, 28)
            };
            cmdApply.Click += (sender, e) =>
            {
                dialogResult = true;
                Close();
            };

            DarkButton cmdCancel = new()
            {
                Text = "Cancel",
                Size = new Size(80, 30),
                Location = new Point(100, 28)
            };
            cmdCancel.Click += (sender, e) =>
            {
                Close();
            };

            panel.ColumnCount = 2;
            panel.RowCount = 4;
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            panel.Controls.Add(lblApply, 0, 0);
            panel.Controls.Add(lblIgnore, 1, 0);
            panel.Controls.Add(txtFilterApply, 0, 1);
            panel.Controls.Add(txtFilterIgnore, 1, 1);
            panel.Controls.Add(lstToApply, 0, 2);
            panel.Controls.Add(lstToIgnore, 1, 2);
            panel.Controls.Add(cmdAdd, 0, 3);
            panel.Controls.Add(cmdRemove, 1, 3);

            frabuttons.Controls.Add(cmdApply);
            frabuttons.Controls.Add(cmdCancel);

            flowBrightness.Controls.Add(chkLowestBrightness);
            flowBrightness.Controls.Add(numLowestBrightness);

            flowMain.Controls.Add(editor);
            flowMain.Controls.Add(flowBrightness);
            flowMain.Controls.Add(panel);
            flowMain.Controls.Add(frabuttons);

            Controls.Add(flowMain);

            AcceptButton = cmdApply;
            CancelButton = cmdCancel;

            FormClosed += (sender, e) =>
            {
                // Reset colors if the edit is cancelled.
                if (!dialogResult)
                {
                    foreach (SceneryEntry scenery in nsf.GetEntries<SceneryEntry>())
                    {
                        if (sceneries.TryGetValue(scenery, out List<SceneryColor>? sceneryColors))
                        {
                            for (int i = 0; i < sceneryColors.Count; i++)
                            {
                                scenery.Colors[i] = sceneryColors[i];
                            }
                        }
                    }
                }
            };
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
    }
}
