using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(NSD))]
    public sealed class NSDController : LegacyController
    {
        public NSDController(NSD nsd, SubcontrollerGroup parentGroup) : base(parentGroup, nsd)
        {
            NSD = nsd;
            string nsfFilename = GetFileName();
            string nsdFilename = string.Empty;
            if (nsfFilename.EndsWith("F"))
            {
                nsdFilename = nsfFilename.Remove(nsfFilename.Length - 1);
                nsdFilename += "D";
            }
            else if (nsfFilename.EndsWith("f"))
            {
                nsdFilename = nsfFilename.Remove(nsfFilename.Length - 1);
                nsdFilename += "d";
            }
            NSDFileName = nsdFilename;
            AddMenu(Resources.ShowGOOLMap_Title, "ThingCode", Menu_ShowGOOLMap);
            AddMenu(Resources.GenerateSpawnPoint_Title, "Calculator", Menu_GenerateSpawnPoint);
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new NSDBox(this);
        }

        public NSD NSD { get; }

        public string NSDFileName { get; }

        private DarkForm? showGOOLMapForm { get; set; }

        public void Kill()
        {
            showGOOLMapForm?.Close();
            showGOOLMapForm?.Dispose();
            showGOOLMapForm = null;
        }

        private void Menu_ShowGOOLMap()
        {
            if (showGOOLMapForm != null)
            {
                showGOOLMapForm.Focus();
                return;
            }

            showGOOLMapForm = new DarkForm()
            {
                Text = $"GOOL Map ({NSDFileName})",
                BackColor = Color.FromArgb(31, 31, 32),
                MaximizeBox = false,
                MinimizeBox = false,
                Width = 584,
                Height = 380
            };
            ListView lst = new()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(31, 31, 32)
            };
            List<string> BaseGOOL = new() { "WillC", "WarpC", "FruiC", "DispC", "DoctC", "PartC", "ShadC", "BoxsC", "WEfOC", "EntNC" };
            for (int i = 0; i < NSD.GOOLMap.Length; ++i)
            {
                ListViewItem lsi = new();
                lsi.Text = $"{i:D2}: {Entry.EIDToEName(NSD.GOOLMap[i])}";
                lsi.ForeColor = lsi.Text.Contains(Entry.NullEName) ? SystemColors.ControlDarkDark :
                                BaseGOOL.Any(item => lsi.Text.Contains(item)) ? Color.Turquoise :
                                Color.Gainsboro;
                lst.Items.Add(lsi);
            }
            showGOOLMapForm.Controls.Add(lst);
            showGOOLMapForm.FormClosing += (object sender, FormClosingEventArgs e) =>
            {
                showGOOLMapForm = null;
            };
            showGOOLMapForm.Show();
        }

        private void Menu_GenerateSpawnPoint()
        {
            using (InputWindow inputWindow = new InputWindow(Resources.GenerateSpawnPoint_Title, "Calculator", "Enter entity ID:", string.Empty, -1))
            {
                if (inputWindow.ShowDialog() == DialogResult.OK)
                {
                    string input = inputWindow.Input;
                    if (string.IsNullOrEmpty(input)) return;

                    if (int.TryParse(input, out int targetID))
                    {
                        NSF nsf = GetNSF();
                        foreach (ZoneEntry entry in nsf.GetEntries<ZoneEntry>())
                        {
                            foreach (Entity entity in entry.Entities)
                            {
                                if (entity.ID == targetID)
                                {
                                    int zone = entry.EID;
                                    int cameraIdx = 0;
                                    string cameraIndex = string.Empty;
                                    if (entry.CameraCount > 3)
                                    {
                                        int cameraMaxIdx = (entry.CameraCount - 1) / 3;
                                        using (InputWindow inputWindows = new InputWindow(Resources.GenerateSpawnPoint_Title, "Calculator", $"Enter camera index [0-{cameraMaxIdx}]:", "0", 2))
                                        {
                                            if (inputWindows.ShowDialog() == DialogResult.OK)
                                            {
                                                bool valid = false;
                                                if (int.TryParse(inputWindows.Input, out cameraIdx))
                                                {
                                                    if (cameraIdx >= 0 && cameraIdx <= cameraMaxIdx)
                                                    {
                                                        valid = true;
                                                        cameraIndex = $" [Camera: {cameraIdx}]";
                                                    }
                                                }

                                                if (!valid)
                                                {
                                                    DarkMessageBox.ShowError("Invalid camera index.", Resources.GenerateSpawnPoint_Title);
                                                    return;
                                                }
                                            }
                                            else return;
                                        }
                                    }
                                    int x = (entry.X + 4 * entity.Positions[0].X) << 8;
                                    int y = (entry.Y + 4 * entity.Positions[0].Y) << 8;
                                    int z = (entry.Z + 4 * entity.Positions[0].Z) << 8;
                                    byte[] data = new byte[24];
                                    BitConv.ToInt32(data, 0, zone);
                                    BitConv.ToInt32(data, 4, cameraIdx);
                                    BitConv.ToInt32(data, 8, 0);
                                    BitConv.ToInt32(data, 12, x);
                                    BitConv.ToInt32(data, 16, y);
                                    BitConv.ToInt32(data, 20, z);
                                    string result = BitConverter.ToString(data).Replace("-", "");
                                    Console.WriteLine($"[{Entry.EIDToEName(zone)}]{cameraIndex} [ID: {entity.ID}]\n{result}");
                                    Clipboard.SetDataObject(result, true, 10, 100);
                                    Console.WriteLine("Copied to clipboard.");
                                    //DarkMessageBox.ShowInformation("Spawn point generated and output to the console.", Resources.GenerateSpawnPoint_Title);
                                    return;
                                }
                            }
                        }
                        DarkMessageBox.ShowError("Entity not found.", Resources.GenerateSpawnPoint_Title);
                        return;
                    }
                    else
                    {
                        DarkMessageBox.ShowError("Invalid entity ID.", Resources.GenerateSpawnPoint_Title);
                        return;
                    }
                }
            }
        }
    }
}
