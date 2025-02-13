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
        public SceneryEditor(NSF nsf)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            AutoSize = false;
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

            ColorEditor editor = new()
            {
                Size = new Size(320, 96),
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
                Dock = DockStyle.Fill,
                AutoSize = false,
                ColumnCount = 2,
                RowCount = 8,
                Padding = new Padding(12)
            };

            Panel panelFake = new()
            {
                Height = editor.Height - 6
            };

            Label lblApply = new()
            {
                ForeColor = SystemColors.MenuText,
                Text = "Apply List"
            };

            Label lblIgnore = new()
            {
                ForeColor = SystemColors.MenuText,
                Text = "Ignore List"
            };

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

                // Draw selected items background.
                Brush backgroundBrush = (e.State & DrawItemState.Selected) != 0 ? new SolidBrush(Color.FromArgb(0, 120, 215)) : Brushes.Transparent;
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);

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
                Text = "Add"
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
                Text = "Remove"
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

            Label lblSeparator = new()
            {
                Text = ""
            };

            DarkGroupBox frabuttons = new()
            {
                Dock = DockStyle.Bottom,
                Text = "Actions",
                Size = new Size(280, 80)
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

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            panel.Controls.Add(panelFake, 0, 0);
            panel.Controls.Add(lblApply, 0, 1);
            panel.Controls.Add(lblIgnore, 1, 1);
            panel.Controls.Add(txtFilterApply, 0, 2);
            panel.Controls.Add(txtFilterIgnore, 1, 2);
            panel.Controls.Add(lstToApply, 0, 3);
            panel.Controls.Add(lstToIgnore, 1, 3);
            panel.Controls.Add(cmdAdd, 0, 4);
            panel.Controls.Add(cmdRemove, 1, 4);
            panel.Controls.Add(lblSeparator, 0, 5);

            frabuttons.Controls.Add(cmdApply);
            frabuttons.Controls.Add(cmdCancel);

            Controls.Add(editor);
            Controls.Add(panel);
            Controls.Add(frabuttons);

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
    }
}
