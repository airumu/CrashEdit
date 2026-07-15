using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using Cyotek.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Text.Json;


namespace CrashEdit.CE.Forms
{
    public partial class EnvironmentEditor : DarkForm
    {

        public bool UseFog => darkCheckBoxUseFog.Checked;
        public string ParticleEffect => dpdParticleEffect.SelectedItem?.ToString()!;
        public bool ParticleEffectIsActive => darkCheckBoxUseParticleEffect.Checked;
        public int FogValue => trackBarFog.Value;
        public bool UseRecolor => darkCheckBoxBackgroundTextureGapColor.Checked;
        public bool UseParticleOneColor => darkCheckBoxUseOnlyOneColor.Checked;
        public int ParticleAmountValue
        {

            get
            {

                return 0x8FF / 100 * trackBarParticleAmount.Value;
            }

        }

        public int ParticleVisibilityValue
        {

            get
            {

                switch(trackBarParticleVisibility.Value){ 

                    case 0: return 0x60;//not found better
                    case 1: return 0x80;
                    case 2: return 0x20;
                    default:return 0x20;

                }
            }

        }


        public uint BackgroundTextureGapColor
        {
            get
            {

                int argb = pictureBoxBackgroundTextureGapColor.BackColor.ToArgb();
                return ColorConverter(argb);

            }
        }


        public uint UpperParticleColor
        {
            get
            {

                int argb = pictureBoxUpperColor.BackColor.ToArgb();
                return ColorConverter(argb);

            }
        }


        public uint LowerParticleColor
        {
            get
            {

                int argb = pictureBoxLowerColor.BackColor.ToArgb();
                return ColorConverter(argb);

            }
        }

        public List<decimal> VelocityParticleValue
        {
            get
            {

                return [darkNumericUpDownParticleX.Value, darkNumericUpDownParticleY.Value, darkNumericUpDownParticleZ.Value];


            }
        }

        private List<ListItem> savedItems = new List<ListItem>();

        private const string FilePath = "CrashEdit.exe.savedenvironmentPreset.json";


        public class FieldData
        {
            public short ParticleAmount { get; set; }
            public short ParticleVisibility { get; set; }
            public short VelocityX { get; set; }
            public short VelocityY { get; set; }
            public short VelocityZ { get; set; }

            public uint UpperColor { get; set; }
            public uint LowerColor { get; set; }

        }

        public class ListItem
        {
            public required string Name { get; set; }
            public List<FieldData> Fields { get; set; } = new List<FieldData>();
        }

        public EnvironmentEditor()
        {
            Icon = Embeds.GetIcon("Wrench");

            InitializeComponent();

            darkCheckBoxUseFog.Checked = Settings.Default.DefaultFogIsActive;
            darkCheckBoxUseParticleEffect.Checked = Settings.Default.DefaultParticleIsActive;

            if (Settings.Default.DefaultpictureBoxBackgroundTextureGapColor != 0)
            {
                pictureBoxBackgroundTextureGapColor.BackColor = Color.FromArgb(Settings.Default.DefaultpictureBoxBackgroundTextureGapColor);
            }
            else
            {
                pictureBoxBackgroundTextureGapColor.BackColor = Color.FromArgb(255, 0, 0, 0);
            }



            dpdParticleEffect.Enabled = Settings.Default.DefaultParticleIsActive;
            trackBarFog.Value = Settings.Default.DefaultFogValue;
            trackBarFog.Enabled = Settings.Default.DefaultFogIsActive;
            txtTrackBarFogValue.Text = trackBarFog.Value.ToString();
            txtTrackBarFogValue.Visible = darkCheckBoxUseFog.Checked;
            darkCheckBoxBackgroundTextureGapColor.Checked = Settings.Default.DefaultRecolorGapIsActive;


        }

        private uint ColorConverter(int argb)
        {


            byte a = (byte)(argb >> 24);

            a = (byte)~a;
            byte r = (byte)(argb >> 16);
            byte g = (byte)(argb >> 8);
            byte b = (byte)argb;

            return (uint)((a << 24) | (b << 16) | (g << 8) | r);

        }

        private void CancelButtonClick(object sender, EventArgs e)
        {

            DialogResult = DialogResult.Cancel;

        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

        }

        private void UseFog_CheckedChanged(object sender, EventArgs e)
        {
            trackBarFog.Enabled = darkCheckBoxUseFog.Checked;
            txtTrackBarFogValue.Visible = darkCheckBoxUseFog.Checked;

        }

        private void TrackBarFog_ValueChanged(object sender, EventArgs e)
        {
            txtTrackBarFogValue.Text = trackBarFog.Value.ToString();

        }


        private void SaveAsDefaultButtonClick(object sender, EventArgs e)
        {

            if (darkCheckBoxUseParticleEffect.Checked)
            {

                if (dpdParticleEffect.SelectedItem == null)
                {

                    DarkMessageBox.ShowError("Please select a preset to save as default.", "Settings");
                    return;

                }

                SavePreset();

            }


            Settings.Default.DefaultFogValue = (byte)trackBarFog.Value;
            Settings.Default.DefaultFogIsActive = darkCheckBoxUseFog.Checked;
            Settings.Default.DefaultParticleIsActive = darkCheckBoxUseParticleEffect.Checked;

            if (dpdParticleEffect.SelectedItem != null)
            {
                Settings.Default.DefaultPreset = dpdParticleEffect.SelectedItem.ToString();
            }

            Settings.Default.DefaultpictureBoxBackgroundTextureGapColor = pictureBoxBackgroundTextureGapColor.BackColor.ToArgb();
            Settings.Default.DefaultRecolorGapIsActive = darkCheckBoxBackgroundTextureGapColor.Checked;

            Settings.Default.Save();

            DarkMessageBox.ShowInformation("Default values saved.", "Settings");


        }

        private void LoadItemsFromFile()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string jsonString = File.ReadAllText(FilePath);
                    savedItems = JsonSerializer.Deserialize<List<ListItem>>(jsonString) ?? new List<ListItem>();
                }

                    dpdParticleEffect.Items.Clear();
                    if (savedItems.Count == 0) { savedItems = GetBuiltInPresets(); }

                    foreach (var item in savedItems)
                    {
                        dpdParticleEffect.Items.Add(item.Name);
                    }

                
            }
            catch (Exception ex)
            {
                DarkMessageBox.ShowError($"Error loading Preset list: {ex.Message}", Resources.Title_Error);
            }
        }

        private void LoadItemsValueInForm()
        {

            //Console.WriteLine(savedItems);

            if (dpdParticleEffect.SelectedIndex < 0) return;



            var selectedIndex = dpdParticleEffect.SelectedIndex;
            var selectedPreset = savedItems[selectedIndex].Fields[0];


            trackBarParticleAmount.Value = selectedPreset.ParticleAmount;
            trackBarParticleVisibility.Value = selectedPreset.ParticleVisibility;
            darkNumericUpDownParticleX.Value = selectedPreset.VelocityX;
            darkNumericUpDownParticleY.Value = selectedPreset.VelocityY;
            darkNumericUpDownParticleZ.Value = selectedPreset.VelocityZ;
            pictureBoxUpperColor.BackColor = Color.FromArgb(unchecked((int)selectedPreset.UpperColor));
            pictureBoxLowerColor.BackColor = Color.FromArgb(unchecked((int)selectedPreset.LowerColor));

            if (pictureBoxUpperColor.BackColor == pictureBoxLowerColor.BackColor)
            {
                darkCheckBoxUseOnlyOneColor.Checked = true;
            }
            else
            {
                darkCheckBoxUseOnlyOneColor.Checked = false;
            }


        }

        private void AddSavedItem(string itemName, List<FieldData> fields)
        {
            var newItem = new ListItem
            {
                Name = itemName,
                Fields = fields
            };
            dpdParticleEffect.Items.Add(newItem.Name);
            savedItems.Add(newItem);
            try
            {
                SaveItemsToFile();
            }
            catch
            {
                DarkMessageBox.ShowError("Error saving Preset.", Resources.Title_Error);
            }
        }

        private void SaveItemsToFile()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(savedItems, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, jsonString);
                Console.WriteLine("Environment Preset list saved successfully.");
            }
            catch
            {
                DarkMessageBox.ShowError("Error saving Preset", Resources.Title_Error);
            }
        }

        private void DarkButtonSavePreset_Click(object sender, EventArgs e)
        {
            try
            {

                SavePreset();
                DarkMessageBox.ShowInformation("Preset updated successfully.", "Save Preset");


            }
            catch
            {

                DarkMessageBox.ShowError("Error saving Preset", Resources.Title_Error);

            }



        }

        private void SavePreset()
        {

            if (dpdParticleEffect.SelectedIndex < 0) return;


            try
            {

                List<FieldData> fields = CreatePresetFields();

                int selectedIndexdpd = dpdParticleEffect.SelectedIndex;
                savedItems[selectedIndexdpd].Fields = fields;
                try
                {
                    SaveItemsToFile();
                }
                catch
                {
                    DarkMessageBox.ShowError("Error updating Preset", Resources.Title_Error);
                }
            }
            catch
            {
                DarkMessageBox.ShowError("Error updating Preset", Resources.Title_Error);
            }

        }

        private void EnvironmentEditor_Load(object sender, EventArgs e)
        {
            LoadItemsFromFile();
            UpdatePercentTrackBarParticleAmount();
            EnableDisableSaveAndRemoveButton();
            InitializeDefaultPreset();
            EnableDisableParticleEffect();
            ShowHideRecolorBackgroundColor();
            ShowHideLowerColor();


        }

        private void InitializeDefaultPreset()
        {

            string selectedDefaultPreset = Settings.Default.DefaultPreset;

            if (selectedDefaultPreset == null && dpdParticleEffect.Items.Count > 0)
            {
                dpdParticleEffect.SelectedIndex = 0;
                return;
            }

            bool flagIsFound = false;



            for (int i = 0; i < savedItems.Count; i++)
            {
                if (savedItems[i].Name == selectedDefaultPreset)
                {
                    dpdParticleEffect.SelectedIndex = i;
                    flagIsFound = true;
                    break;
                }
            }


            if (!flagIsFound && dpdParticleEffect.Items.Count>0) {

                dpdParticleEffect.SelectedIndex = 0;
            }
        }

        private void UpdatePercentTrackBarParticleAmount()
        {

            ParticleAmountPercent.Text = trackBarParticleAmount.Value.ToString() + "%";

        }


        private void EnableDisableParticleEffect()
        {

            if (savedItems.Count == 1)
            {

                dpdParticleEffect.SelectedIndex = 0;

            }

            var enable = darkCheckBoxUseParticleEffect.Checked;

            darkGroupBoxParticleAmount.Enabled = enable;
            darkGroupBoxParticleVisibility.Enabled = enable;
            darkGroupBoxParticleVelocity.Enabled = enable;
            darkGroupBoxParticleColor.Enabled = enable;


            darkButtonSaveAsNewPreset.Enabled = enable;
            darkButtonRestoreDefaultPreset.Enabled = enable;


            if (dpdParticleEffect.SelectedIndex >= 0 && enable)
            {

                darkButtonSavePreset.Enabled = true;
                darkButtonRemovePreset.Enabled = true;

            }
            else {

                darkButtonSavePreset.Enabled = false;
                darkButtonRemovePreset.Enabled = false;
            }


            pictureBoxUpperColor.Visible = enable;
            pictureBoxLowerColor.Visible = enable;

            darkTitleLowerColor.Visible = enable;
            darkTitleUpperColor.Visible = enable;

            dpdParticleEffect.Enabled = enable;

            ParticleAmountPercent.Visible = enable;

        }

        private void DpdParticleEffect_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItemsValueInForm();
            UpdatePercentTrackBarParticleAmount();

        }

        private void TrackBarParticleAmount_Scroll(object sender, EventArgs e)
        {
            ParticleAmountPercent.Text = trackBarParticleAmount.Value.ToString() + "%";
        }

        private void ShowHideLowerColor()
        {

            if (darkCheckBoxUseParticleEffect.Checked == false) { return; }

            var hideLowerColor = darkCheckBoxUseOnlyOneColor.Checked;

            if (hideLowerColor)
            {

                darkTitleUpperColor.Text = "Particle Color";

            }
            else
            {

                darkTitleUpperColor.Text = "Upper Color";


            }

            darkTitleLowerColor.Visible = !hideLowerColor;
            pictureBoxLowerColor.Visible = !hideLowerColor;


        }

        private void DarkCheckBoxUseOnlyOneColor_CheckedChanged(object sender, EventArgs e)
        {
            ShowHideLowerColor();

        }

        private void DarkButtonSaveAsNewPreset_Click(object sender, EventArgs e)
        {
            try
            {

                using (InputWindow inputWindow = new InputWindow(Resources.EntityBox_CmdAdd, "Save As Preset", "Enter Preset Name:", string.Empty, -1))
                {

                    if (inputWindow.ShowDialog(this) == DialogResult.OK)
                    {
                        if (inputWindow.Input.Length == 0)
                        {
                            DarkMessageBox.ShowError("Please enter a preset name", "Error name empty");
                            return;
                        }

                        for (int name = 0; name < savedItems.Count; name++)
                        {

                            if (savedItems[name].Name == inputWindow.Input)
                            {
                                DarkMessageBox.ShowError("This name is already used", "Error name already used");
                                return;

                            }

                        }

                        string presetName = inputWindow.Input;
                        List<FieldData> fields = CreatePresetFields();



                        AddSavedItem(presetName, fields);

                    }
                    else { return; }

                }

                EnableDisableSaveAndRemoveButton();

                if (savedItems.Count == 1)
                {

                    dpdParticleEffect.SelectedIndex = 0;

                }


                DarkMessageBox.ShowInformation("Preset saved successfully.", "Save Preset");




            }
            catch
            {

                DarkMessageBox.ShowError("Error saving Preset", Resources.Title_Error);


            }




        }

        private List<FieldData> CreatePresetFields()
        {

            List<FieldData> list = new List<FieldData>();

            list.Add(new FieldData());
            list[0].ParticleAmount = (short)trackBarParticleAmount.Value;
            list[0].ParticleVisibility = (short)trackBarParticleVisibility.Value;
            list[0].VelocityX = (short)darkNumericUpDownParticleX.Value;
            list[0].VelocityY = (short)darkNumericUpDownParticleY.Value;
            list[0].VelocityZ = (short)darkNumericUpDownParticleZ.Value;
            list[0].UpperColor = (uint)pictureBoxUpperColor.BackColor.ToArgb();
            list[0].LowerColor = (uint)pictureBoxLowerColor.BackColor.ToArgb();


            if (darkCheckBoxUseOnlyOneColor.Checked)
            {
                list[0].LowerColor = (uint)pictureBoxUpperColor.BackColor.ToArgb();
            }

            return list;


        }

        private void DarkCheckBoxUseParticleEffect_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableParticleEffect();
        }

        private void PictureBoxUpperColor_Click(object sender, EventArgs e)
        {

            pictureBoxUpperColor.BackColor = ColorPicker(pictureBoxUpperColor.BackColor);


        }

        private Color ColorPicker(Color startColor)
        {


            var editor = new ColorEditor
            {
                Dock = DockStyle.Fill,
                Color = startColor,
                ShowAlphaChannel = true,
                ShowHsl = false,
                ShowColorSpaceLabels = false,
                Padding = new Padding(12)
            };

            var preview = new Panel { Dock = DockStyle.Top, Height = 32 };
            preview.Paint += (s, ev) =>
            {
                var r = preview.ClientRectangle;
                using (var bg = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.LightGray, Color.White))
                    ev.Graphics.FillRectangle(bg, r);
                using (var fg = new SolidBrush(editor.Color))
                    ev.Graphics.FillRectangle(fg, r);
            };
            editor.ColorChanged += (s, ev) => preview.Invalidate();

            var btnOk = new DarkButton
            {
                Text = "OK",
                Dock = DockStyle.Bottom,
                DialogResult = DialogResult.OK
            };

            using var form = new DarkForm
            {
                Text = "Pick color",
                ClientSize = new Size(320, 280),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false,
                AcceptButton = btnOk
            };

            form.Controls.Add(editor);
            form.Controls.Add(preview);
            form.Controls.Add(btnOk);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                return editor.Color;
            }

            return startColor;


        }

        private void PictureBoxLowerColor_Click(object sender, EventArgs e)
        {
            pictureBoxLowerColor.BackColor = ColorPicker(pictureBoxLowerColor.BackColor);
        }

        private void DarkButtonRemovePreset_Click(object sender, EventArgs e)
        {

            if (DarkMessageBox.ShowWarning("Delete this preset ?", "Confirmation", DarkDialogButton.YesNo) != DialogResult.Yes)
            {
                return;
            }


            try
            {
                int targetIndex = dpdParticleEffect.SelectedIndex;

                dpdParticleEffect.Items.RemoveAt(targetIndex);
                savedItems.RemoveAt(targetIndex);

                SaveItemsToFile();
                DarkMessageBox.ShowInformation("Preset removed successfully.", "Remove Preset");
            }
            catch
            {
                DarkMessageBox.ShowError("Error Removing Preset", Resources.Title_Error);
            }

            if (savedItems.Count > 0)
            {

                dpdParticleEffect.SelectedIndex = 0;

            }
            else
            {
                dpdParticleEffect.SelectedItem = null;
                dpdParticleEffect.Text = string.Empty;
                dpdParticleEffect.Refresh();
            }

            EnableDisableSaveAndRemoveButton();

        }

        private void EnableDisableSaveAndRemoveButton()
        {

            bool enable = true;

            if (savedItems.Count == 0)
            {
                enable = false;

            }
            dpdParticleEffect.Enabled = enable;
            darkButtonRemovePreset.Enabled = enable;

            darkButtonSavePreset.Enabled = enable;

        }

        public static List<ListItem> GetBuiltInPresets()
        {
            List<ListItem> defaultList = new List<ListItem>();

            defaultList.Add(new ListItem
            {
                Name = "Rain",
                Fields = new List<FieldData>
        {
            new FieldData
            {
                ParticleAmount = 10,
                ParticleVisibility = 0,
                VelocityX = 32,
                VelocityY = 255,
                VelocityZ = 0,
                UpperColor = 0xFF404040,
                LowerColor = 0xFFC0C0C0
            }
        }
            });

            defaultList.Add(new ListItem
            {
                Name = "Snow",
                Fields = new List<FieldData>
        {
            new FieldData
            {
                ParticleAmount = 10,
                ParticleVisibility = 2,
                VelocityX = 2,
                VelocityY = 16,
                VelocityZ = 0,
                UpperColor = 0xFF404040,
                LowerColor = 0xFF808080
            }
        }
            });

            defaultList.Add(new ListItem
            {
                Name = "Heavy rain",
                Fields = new List<FieldData>
        {
            new FieldData
            {
                ParticleAmount = 50,
                ParticleVisibility = 0,
                VelocityX = 32,
                VelocityY = 255,
                VelocityZ = 0,
                UpperColor = 0xFF404040,
                LowerColor = 0xFFC0C0C0
            }
        }
            });

            return defaultList;
        }


        private void DarkButtonRestoreDefaultPreset_Click(object sender, EventArgs e)
        {

            try
            {




                if (DarkMessageBox.ShowWarning("Reset all presets to default?", "Confirmation", DarkDialogButton.YesNo) != DialogResult.Yes)
                {
                    return;
                }

                savedItems.Clear();
                SaveItemsToFile();
                LoadItemsFromFile();
                UpdatePercentTrackBarParticleAmount();
                EnableDisableParticleEffect();
                EnableDisableSaveAndRemoveButton();
                InitializeDefaultPreset();

                DarkMessageBox.ShowInformation("Default presets restored", "Save Properties");




            }
            catch
            {

                DarkMessageBox.ShowError("Error Resetting presets.", "Save Properties");


            }


        }

        private void pictureBoxBackgroundTextureGapColor_Click(object sender, EventArgs e)
        {

            pictureBoxBackgroundTextureGapColor.BackColor = ColorPicker(pictureBoxBackgroundTextureGapColor.BackColor);


        }

        private void DarkCheckBoxBackgroundTextureGapColor_CheckedChanged(object sender, EventArgs e)
        {
            ShowHideRecolorBackgroundColor();

        }

        private void ShowHideRecolorBackgroundColor()
        {

            var enable = darkCheckBoxBackgroundTextureGapColor.Checked;

            pictureBoxBackgroundTextureGapColor.Visible = enable;
        }


    }
}
