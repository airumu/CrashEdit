namespace CrashEdit.CE.Forms
{
    partial class EnvironmentEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            darkCheckBoxUseFog = new AltUI.Controls.DarkCheckBox();
            trackBarFog = new TrackBar();
            darkTitle1 = new AltUI.Controls.DarkTitle();
            darkTitle2 = new AltUI.Controls.DarkTitle();
            darkTitle3 = new AltUI.Controls.DarkTitle();
            darkButton1 = new AltUI.Controls.DarkButton();
            darkButton2 = new AltUI.Controls.DarkButton();
            txtTrackBarFogValue = new AltUI.Controls.DarkTitle();
            darkButton3 = new AltUI.Controls.DarkButton();
            darkTitle5 = new AltUI.Controls.DarkTitle();
            fogSettingsBox = new AltUI.Controls.DarkGroupBox();
            darkCheckBoxBackgroundTextureGapColor = new AltUI.Controls.DarkCheckBox();
            pictureBoxBackgroundTextureGapColor = new PictureBox();
            darkGroupBoxParticleSettings = new AltUI.Controls.DarkGroupBox();
            darkGroupBoxParticleVisibility = new AltUI.Controls.DarkGroupBox();
            darkTitle10 = new AltUI.Controls.DarkTitle();
            trackBarParticleVisibility = new TrackBar();
            darkTitle14 = new AltUI.Controls.DarkTitle();
            darkTitle16 = new AltUI.Controls.DarkTitle();
            darkButtonRestoreDefaultPreset = new AltUI.Controls.DarkButton();
            darkCheckBoxUseParticleEffect = new AltUI.Controls.DarkCheckBox();
            darkGroupBoxParticleAmount = new AltUI.Controls.DarkGroupBox();
            ParticleAmountPercent = new AltUI.Controls.DarkTitle();
            trackBarParticleAmount = new TrackBar();
            darkTitle8 = new AltUI.Controls.DarkTitle();
            darkTitle7 = new AltUI.Controls.DarkTitle();
            darkTitle6 = new AltUI.Controls.DarkTitle();
            darkGroupBoxParticleColor = new AltUI.Controls.DarkGroupBox();
            darkCheckBoxUseOnlyOneColor = new AltUI.Controls.DarkCheckBox();
            darkTitleLowerColor = new AltUI.Controls.DarkTitle();
            pictureBoxLowerColor = new PictureBox();
            darkTitleUpperColor = new AltUI.Controls.DarkTitle();
            pictureBoxUpperColor = new PictureBox();
            darkGroupBoxParticleVelocity = new AltUI.Controls.DarkGroupBox();
            darkNumericUpDownParticleZ = new AltUI.Controls.DarkNumericUpDown();
            darkNumericUpDownParticleY = new AltUI.Controls.DarkNumericUpDown();
            darkTitle13 = new AltUI.Controls.DarkTitle();
            darkTitle9 = new AltUI.Controls.DarkTitle();
            darkNumericUpDownParticleX = new AltUI.Controls.DarkNumericUpDown();
            darkTitle12 = new AltUI.Controls.DarkTitle();
            darkTitle11 = new AltUI.Controls.DarkTitle();
            darkButtonSaveAsNewPreset = new AltUI.Controls.DarkButton();
            darkButtonRemovePreset = new AltUI.Controls.DarkButton();
            darkButtonSavePreset = new AltUI.Controls.DarkButton();
            dpdParticleEffect = new AltUI.Controls.DarkComboBox();
            ((System.ComponentModel.ISupportInitialize)trackBarFog).BeginInit();
            fogSettingsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBackgroundTextureGapColor).BeginInit();
            darkGroupBoxParticleSettings.SuspendLayout();
            darkGroupBoxParticleVisibility.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarParticleVisibility).BeginInit();
            darkGroupBoxParticleAmount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarParticleAmount).BeginInit();
            darkGroupBoxParticleColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLowerColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUpperColor).BeginInit();
            darkGroupBoxParticleVelocity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)darkNumericUpDownParticleZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)darkNumericUpDownParticleY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)darkNumericUpDownParticleX).BeginInit();
            SuspendLayout();
            // 
            // darkCheckBox1
            // 
            darkCheckBoxUseFog.AutoSize = true;
            darkCheckBoxUseFog.Location = new Point(26, 76);
            darkCheckBoxUseFog.Name = "darkCheckBox1";
            darkCheckBoxUseFog.Offset = 1;
            darkCheckBoxUseFog.Size = new Size(103, 29);
            darkCheckBoxUseFog.TabIndex = 1;
            darkCheckBoxUseFog.Text = "Use Fog";
            darkCheckBoxUseFog.CheckedChanged += UseFog_CheckedChanged;
            // 
            // trackBarFog
            // 
            trackBarFog.Location = new Point(275, 76);
            trackBarFog.Margin = new Padding(5);
            trackBarFog.Maximum = 64;
            trackBarFog.Name = "trackBarFog";
            trackBarFog.Size = new Size(370, 69);
            trackBarFog.TabIndex = 6;
            trackBarFog.Tag = "";
            trackBarFog.ValueChanged += TrackBarFog_ValueChanged;
            // 
            // darkTitle1
            // 
            darkTitle1.Location = new Point(338, 25);
            darkTitle1.Margin = new Padding(3, 0, 3, 15);
            darkTitle1.Name = "darkTitle1";
            darkTitle1.Size = new Size(115, 30);
            darkTitle1.TabIndex = 7;
            darkTitle1.Text = "Fog Distance";
            // 
            // darkTitle2
            // 
            darkTitle2.AutoSize = true;
            darkTitle2.Location = new Point(184, 80);
            darkTitle2.Name = "darkTitle2";
            darkTitle2.Size = new Size(94, 25);
            darkTitle2.TabIndex = 8;
            darkTitle2.Text = "Very Close";
            // 
            // darkTitle3
            // 
            darkTitle3.AutoSize = true;
            darkTitle3.Location = new Point(651, 76);
            darkTitle3.Name = "darkTitle3";
            darkTitle3.Size = new Size(74, 25);
            darkTitle3.TabIndex = 9;
            darkTitle3.Text = "Very Far";
            // 
            // darkButton1
            // 
            darkButton1.BorderColour = Color.Empty;
            darkButton1.CustomColour = false;
            darkButton1.FlatBottom = false;
            darkButton1.FlatTop = false;
            darkButton1.Location = new Point(538, 983);
            darkButton1.Name = "darkButton1";
            darkButton1.Padding = new Padding(5);
            darkButton1.Size = new Size(112, 34);
            darkButton1.TabIndex = 10;
            darkButton1.Text = "Ok";
            darkButton1.Click += OkButtonClick;
            // 
            // darkButton2
            // 
            darkButton2.BorderColour = Color.Empty;
            darkButton2.CustomColour = false;
            darkButton2.FlatBottom = false;
            darkButton2.FlatTop = false;
            darkButton2.Location = new Point(689, 983);
            darkButton2.Name = "darkButton2";
            darkButton2.Padding = new Padding(5);
            darkButton2.Size = new Size(112, 34);
            darkButton2.TabIndex = 11;
            darkButton2.Text = "Cancel";
            darkButton2.Click += CancelButtonClick;
            // 
            // darkTitle4
            // 
            txtTrackBarFogValue.AutoSize = true;
            txtTrackBarFogValue.Location = new Point(459, 25);
            txtTrackBarFogValue.Margin = new Padding(3, 0, 3, 15);
            txtTrackBarFogValue.Name = "darkTitle4";
            txtTrackBarFogValue.Size = new Size(115, 25);
            txtTrackBarFogValue.TabIndex = 12;
            txtTrackBarFogValue.Text = "Fog Distance";
            // 
            // darkButton3
            // 
            darkButton3.BorderColour = Color.Empty;
            darkButton3.CustomColour = false;
            darkButton3.FlatBottom = false;
            darkButton3.FlatTop = false;
            darkButton3.Location = new Point(38, 983);
            darkButton3.Name = "darkButton3";
            darkButton3.Padding = new Padding(5);
            darkButton3.Size = new Size(163, 34);
            darkButton3.TabIndex = 13;
            darkButton3.Text = "Save As Default";
            darkButton3.Click += SaveAsDefaultButtonClick;
            // 
            // darkTitle5
            // 
            darkTitle5.AutoSize = true;
            darkTitle5.Location = new Point(209, 37);
            darkTitle5.Margin = new Padding(3, 0, 3, 15);
            darkTitle5.Name = "darkTitle5";
            darkTitle5.Size = new Size(169, 25);
            darkTitle5.TabIndex = 15;
            darkTitle5.Text = "Particle Effect Preset";
            // 
            // fogSettingsBox
            // 
            fogSettingsBox.AccessibleName = "Fog Settings";
            fogSettingsBox.Controls.Add(darkCheckBoxBackgroundTextureGapColor);
            fogSettingsBox.Controls.Add(pictureBoxBackgroundTextureGapColor);
            fogSettingsBox.Controls.Add(darkCheckBoxUseFog);
            fogSettingsBox.Controls.Add(trackBarFog);
            fogSettingsBox.Controls.Add(darkTitle2);
            fogSettingsBox.Controls.Add(darkTitle3);
            fogSettingsBox.Controls.Add(darkTitle1);
            fogSettingsBox.Controls.Add(txtTrackBarFogValue);
            fogSettingsBox.Location = new Point(38, 12);
            fogSettingsBox.Name = "fogSettingsBox";
            fogSettingsBox.Size = new Size(763, 214);
            fogSettingsBox.TabIndex = 16;
            fogSettingsBox.TabStop = false;
            fogSettingsBox.Text = "Fog Settings";
            // 
            // darkCheckBoxBackgroundTextureGapColor
            // 
            darkCheckBoxBackgroundTextureGapColor.AutoSize = true;
            darkCheckBoxBackgroundTextureGapColor.Location = new Point(26, 161);
            darkCheckBoxBackgroundTextureGapColor.Name = "darkCheckBoxBackgroundTextureGapColor";
            darkCheckBoxBackgroundTextureGapColor.Offset = 1;
            darkCheckBoxBackgroundTextureGapColor.Size = new Size(342, 29);
            darkCheckBoxBackgroundTextureGapColor.TabIndex = 36;
            darkCheckBoxBackgroundTextureGapColor.Text = "Recolor Background Texture Gap Color";
            darkCheckBoxBackgroundTextureGapColor.CheckedChanged += DarkCheckBoxBackgroundTextureGapColor_CheckedChanged;
            // 
            // pictureBoxBackgroundTextureGapColor
            // 
            pictureBoxBackgroundTextureGapColor.BackColor = Color.Black;
            pictureBoxBackgroundTextureGapColor.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxBackgroundTextureGapColor.Location = new Point(374, 157);
            pictureBoxBackgroundTextureGapColor.Name = "pictureBoxBackgroundTextureGapColor";
            pictureBoxBackgroundTextureGapColor.Size = new Size(36, 39);
            pictureBoxBackgroundTextureGapColor.TabIndex = 35;
            pictureBoxBackgroundTextureGapColor.TabStop = false;
            pictureBoxBackgroundTextureGapColor.Click += pictureBoxBackgroundTextureGapColor_Click;
            // 
            // darkGroupBoxParticleSettings
            // 
            darkGroupBoxParticleSettings.Controls.Add(darkGroupBoxParticleVisibility);
            darkGroupBoxParticleSettings.Controls.Add(darkButtonRestoreDefaultPreset);
            darkGroupBoxParticleSettings.Controls.Add(darkCheckBoxUseParticleEffect);
            darkGroupBoxParticleSettings.Controls.Add(darkGroupBoxParticleAmount);
            darkGroupBoxParticleSettings.Controls.Add(darkGroupBoxParticleColor);
            darkGroupBoxParticleSettings.Controls.Add(darkGroupBoxParticleVelocity);
            darkGroupBoxParticleSettings.Controls.Add(darkButtonSaveAsNewPreset);
            darkGroupBoxParticleSettings.Controls.Add(darkButtonRemovePreset);
            darkGroupBoxParticleSettings.Controls.Add(darkButtonSavePreset);
            darkGroupBoxParticleSettings.Controls.Add(dpdParticleEffect);
            darkGroupBoxParticleSettings.Controls.Add(darkTitle5);
            darkGroupBoxParticleSettings.Location = new Point(38, 243);
            darkGroupBoxParticleSettings.Name = "darkGroupBoxParticleSettings";
            darkGroupBoxParticleSettings.Size = new Size(763, 708);
            darkGroupBoxParticleSettings.TabIndex = 17;
            darkGroupBoxParticleSettings.TabStop = false;
            darkGroupBoxParticleSettings.Text = "Particle Settings";
            // 
            // darkGroupBoxParticleVisibility
            // 
            darkGroupBoxParticleVisibility.Controls.Add(darkTitle10);
            darkGroupBoxParticleVisibility.Controls.Add(trackBarParticleVisibility);
            darkGroupBoxParticleVisibility.Controls.Add(darkTitle14);
            darkGroupBoxParticleVisibility.Controls.Add(darkTitle16);
            darkGroupBoxParticleVisibility.Location = new Point(20, 305);
            darkGroupBoxParticleVisibility.Name = "darkGroupBoxParticleVisibility";
            darkGroupBoxParticleVisibility.Size = new Size(719, 130);
            darkGroupBoxParticleVisibility.TabIndex = 28;
            darkGroupBoxParticleVisibility.TabStop = false;
            darkGroupBoxParticleVisibility.Text = "Particle Visibility";
            // 
            // darkTitle10
            // 
            darkTitle10.AutoSize = true;
            darkTitle10.Location = new Point(618, 53);
            darkTitle10.Name = "darkTitle10";
            darkTitle10.Size = new Size(39, 25);
            darkTitle10.TabIndex = 37;
            darkTitle10.Text = "Full";
            // 
            // trackBarParticleVisibility
            // 
            trackBarParticleVisibility.LargeChange = 1;
            trackBarParticleVisibility.Location = new Point(238, 53);
            trackBarParticleVisibility.Margin = new Padding(5);
            trackBarParticleVisibility.Maximum = 2;
            trackBarParticleVisibility.Name = "trackBarParticleVisibility";
            trackBarParticleVisibility.Size = new Size(370, 69);
            trackBarParticleVisibility.TabIndex = 13;
            trackBarParticleVisibility.Tag = "";
            // 
            // darkTitle14
            // 
            darkTitle14.Location = new Point(45, 53);
            darkTitle14.Margin = new Padding(3, 0, 3, 15);
            darkTitle14.Name = "darkTitle14";
            darkTitle14.Size = new Size(70, 30);
            darkTitle14.TabIndex = 14;
            darkTitle14.Text = "Visibility";
            // 
            // darkTitle16
            // 
            darkTitle16.AutoSize = true;
            darkTitle16.Location = new Point(193, 53);
            darkTitle16.Name = "darkTitle16";
            darkTitle16.Size = new Size(44, 25);
            darkTitle16.TabIndex = 15;
            darkTitle16.Text = "Low";
            // 
            // darkButtonRestoreDefaultPreset
            // 
            darkButtonRestoreDefaultPreset.BorderColour = Color.Empty;
            darkButtonRestoreDefaultPreset.CustomColour = false;
            darkButtonRestoreDefaultPreset.FlatBottom = false;
            darkButtonRestoreDefaultPreset.FlatTop = false;
            darkButtonRestoreDefaultPreset.Location = new Point(477, 107);
            darkButtonRestoreDefaultPreset.Name = "darkButtonRestoreDefaultPreset";
            darkButtonRestoreDefaultPreset.Padding = new Padding(5);
            darkButtonRestoreDefaultPreset.Size = new Size(203, 34);
            darkButtonRestoreDefaultPreset.TabIndex = 30;
            darkButtonRestoreDefaultPreset.Text = "Restore Default Preset";
            darkButtonRestoreDefaultPreset.Click += DarkButtonRestoreDefaultPreset_Click;
            // 
            // darkCheckBoxUseParticleEffect
            // 
            darkCheckBoxUseParticleEffect.AutoSize = true;
            darkCheckBoxUseParticleEffect.Location = new Point(26, 77);
            darkCheckBoxUseParticleEffect.Name = "darkCheckBoxUseParticleEffect";
            darkCheckBoxUseParticleEffect.Offset = 1;
            darkCheckBoxUseParticleEffect.Size = new Size(127, 29);
            darkCheckBoxUseParticleEffect.TabIndex = 13;
            darkCheckBoxUseParticleEffect.Text = "Use Particle";
            darkCheckBoxUseParticleEffect.CheckedChanged += DarkCheckBoxUseParticleEffect_CheckedChanged;
            // 
            // darkGroupBoxParticleAmount
            // 
            darkGroupBoxParticleAmount.Controls.Add(ParticleAmountPercent);
            darkGroupBoxParticleAmount.Controls.Add(trackBarParticleAmount);
            darkGroupBoxParticleAmount.Controls.Add(darkTitle8);
            darkGroupBoxParticleAmount.Controls.Add(darkTitle7);
            darkGroupBoxParticleAmount.Controls.Add(darkTitle6);
            darkGroupBoxParticleAmount.Location = new Point(20, 156);
            darkGroupBoxParticleAmount.Name = "darkGroupBoxParticleAmount";
            darkGroupBoxParticleAmount.Size = new Size(719, 140);
            darkGroupBoxParticleAmount.TabIndex = 27;
            darkGroupBoxParticleAmount.TabStop = false;
            darkGroupBoxParticleAmount.Text = "Particle Amount";
            // 
            // ParticleAmountPourcent
            // 
            ParticleAmountPercent.AutoSize = true;
            ParticleAmountPercent.Location = new Point(393, 21);
            ParticleAmountPercent.Margin = new Padding(3, 0, 3, 15);
            ParticleAmountPercent.Name = "ParticleAmountPourcent";
            ParticleAmountPercent.Size = new Size(137, 25);
            ParticleAmountPercent.TabIndex = 13;
            ParticleAmountPercent.Text = "Particle Amount";
            // 
            // trackBarParticleAmount
            // 
            trackBarParticleAmount.Location = new Point(238, 53);
            trackBarParticleAmount.Margin = new Padding(5);
            trackBarParticleAmount.Maximum = 100;
            trackBarParticleAmount.Name = "trackBarParticleAmount";
            trackBarParticleAmount.Size = new Size(370, 69);
            trackBarParticleAmount.TabIndex = 13;
            trackBarParticleAmount.Tag = "";
            trackBarParticleAmount.Scroll += TrackBarParticleAmount_Scroll;
            // 
            // darkTitle8
            // 
            darkTitle8.Location = new Point(45, 63);
            darkTitle8.Margin = new Padding(3, 0, 3, 15);
            darkTitle8.Name = "darkTitle8";
            darkTitle8.Size = new Size(104, 30);
            darkTitle8.TabIndex = 14;
            darkTitle8.Text = "Amount (%)";
            // 
            // darkTitle7
            // 
            darkTitle7.AutoSize = true;
            darkTitle7.Location = new Point(614, 53);
            darkTitle7.Name = "darkTitle7";
            darkTitle7.Size = new Size(57, 25);
            darkTitle7.TabIndex = 16;
            darkTitle7.Text = "100%";
            // 
            // darkTitle6
            // 
            darkTitle6.AutoSize = true;
            darkTitle6.Location = new Point(193, 53);
            darkTitle6.Name = "darkTitle6";
            darkTitle6.Size = new Size(37, 25);
            darkTitle6.TabIndex = 15;
            darkTitle6.Text = "0%";
            // 
            // darkGroupBoxParticleColor
            // 
            darkGroupBoxParticleColor.Controls.Add(darkCheckBoxUseOnlyOneColor);
            darkGroupBoxParticleColor.Controls.Add(darkTitleLowerColor);
            darkGroupBoxParticleColor.Controls.Add(pictureBoxLowerColor);
            darkGroupBoxParticleColor.Controls.Add(darkTitleUpperColor);
            darkGroupBoxParticleColor.Controls.Add(pictureBoxUpperColor);
            darkGroupBoxParticleColor.Location = new Point(20, 585);
            darkGroupBoxParticleColor.Name = "darkGroupBoxParticleColor";
            darkGroupBoxParticleColor.Size = new Size(719, 99);
            darkGroupBoxParticleColor.TabIndex = 29;
            darkGroupBoxParticleColor.TabStop = false;
            darkGroupBoxParticleColor.Text = "Particle Color";
            // 
            // darkCheckBoxUseOnlyOneColor
            // 
            darkCheckBoxUseOnlyOneColor.AutoSize = true;
            darkCheckBoxUseOnlyOneColor.Location = new Point(45, 40);
            darkCheckBoxUseOnlyOneColor.Name = "darkCheckBoxUseOnlyOneColor";
            darkCheckBoxUseOnlyOneColor.Offset = 1;
            darkCheckBoxUseOnlyOneColor.Size = new Size(195, 29);
            darkCheckBoxUseOnlyOneColor.TabIndex = 34;
            darkCheckBoxUseOnlyOneColor.Text = "Use Only One Color";
            darkCheckBoxUseOnlyOneColor.CheckedChanged += DarkCheckBoxUseOnlyOneColor_CheckedChanged;
            // 
            // darkTitleLowerColor
            // 
            darkTitleLowerColor.Location = new Point(536, 39);
            darkTitleLowerColor.Margin = new Padding(3, 0, 3, 15);
            darkTitleLowerColor.Name = "darkTitleLowerColor";
            darkTitleLowerColor.Size = new Size(113, 30);
            darkTitleLowerColor.TabIndex = 33;
            darkTitleLowerColor.Text = "Lower Color ";
            // 
            // pictureBoxLowerColor
            // 
            pictureBoxLowerColor.BackColor = Color.White;
            pictureBoxLowerColor.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxLowerColor.Location = new Point(658, 34);
            pictureBoxLowerColor.Name = "pictureBoxLowerColor";
            pictureBoxLowerColor.Size = new Size(36, 39);
            pictureBoxLowerColor.TabIndex = 32;
            pictureBoxLowerColor.TabStop = false;
            pictureBoxLowerColor.Click += PictureBoxLowerColor_Click;
            // 
            // darkTitleUpperColor
            // 
            darkTitleUpperColor.Location = new Point(359, 39);
            darkTitleUpperColor.Margin = new Padding(3, 0, 3, 15);
            darkTitleUpperColor.Name = "darkTitleUpperColor";
            darkTitleUpperColor.Size = new Size(113, 30);
            darkTitleUpperColor.TabIndex = 31;
            darkTitleUpperColor.Text = "Upper Color ";
            // 
            // pictureBoxUpperColor
            // 
            pictureBoxUpperColor.BackColor = Color.White;
            pictureBoxUpperColor.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxUpperColor.Location = new Point(478, 34);
            pictureBoxUpperColor.Name = "pictureBoxUpperColor";
            pictureBoxUpperColor.Size = new Size(36, 39);
            pictureBoxUpperColor.TabIndex = 0;
            pictureBoxUpperColor.TabStop = false;
            pictureBoxUpperColor.Click += PictureBoxUpperColor_Click;
            // 
            // darkGroupBoxParticleVelocity
            // 
            darkGroupBoxParticleVelocity.Controls.Add(darkNumericUpDownParticleZ);
            darkGroupBoxParticleVelocity.Controls.Add(darkNumericUpDownParticleY);
            darkGroupBoxParticleVelocity.Controls.Add(darkTitle13);
            darkGroupBoxParticleVelocity.Controls.Add(darkTitle9);
            darkGroupBoxParticleVelocity.Controls.Add(darkNumericUpDownParticleX);
            darkGroupBoxParticleVelocity.Controls.Add(darkTitle12);
            darkGroupBoxParticleVelocity.Controls.Add(darkTitle11);
            darkGroupBoxParticleVelocity.Location = new Point(20, 462);
            darkGroupBoxParticleVelocity.Name = "darkGroupBoxParticleVelocity";
            darkGroupBoxParticleVelocity.Size = new Size(722, 107);
            darkGroupBoxParticleVelocity.TabIndex = 28;
            darkGroupBoxParticleVelocity.TabStop = false;
            darkGroupBoxParticleVelocity.Text = "Particle Velocity";
            // 
            // darkNumericUpDownParticleZ
            // 
            darkNumericUpDownParticleZ.Location = new Point(514, 42);
            darkNumericUpDownParticleZ.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            darkNumericUpDownParticleZ.Minimum = new decimal(new int[] { 255, 0, 0, int.MinValue });
            darkNumericUpDownParticleZ.Name = "darkNumericUpDownParticleZ";
            darkNumericUpDownParticleZ.Size = new Size(72, 31);
            darkNumericUpDownParticleZ.TabIndex = 30;
            // 
            // darkNumericUpDownParticleY
            // 
            darkNumericUpDownParticleY.Location = new Point(403, 42);
            darkNumericUpDownParticleY.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            darkNumericUpDownParticleY.Minimum = new decimal(new int[] { 255, 0, 0, int.MinValue });
            darkNumericUpDownParticleY.Name = "darkNumericUpDownParticleY";
            darkNumericUpDownParticleY.Size = new Size(72, 31);
            darkNumericUpDownParticleY.TabIndex = 29;
            // 
            // darkTitle13
            // 
            darkTitle13.Location = new Point(45, 42);
            darkTitle13.Margin = new Padding(3, 0, 3, 15);
            darkTitle13.Name = "darkTitle13";
            darkTitle13.Size = new Size(172, 30);
            darkTitle13.TabIndex = 28;
            darkTitle13.Text = "(min: -255, max: 255)";
            // 
            // darkTitle9
            // 
            darkTitle9.Location = new Point(258, 44);
            darkTitle9.Margin = new Padding(3, 0, 3, 15);
            darkTitle9.Name = "darkTitle9";
            darkTitle9.Size = new Size(22, 30);
            darkTitle9.TabIndex = 27;
            darkTitle9.Text = "X:";
            // 
            // darkNumericUpDownParticleX
            // 
            darkNumericUpDownParticleX.Location = new Point(286, 42);
            darkNumericUpDownParticleX.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            darkNumericUpDownParticleX.Minimum = new decimal(new int[] { 255, 0, 0, int.MinValue });
            darkNumericUpDownParticleX.Name = "darkNumericUpDownParticleX";
            darkNumericUpDownParticleX.Size = new Size(72, 31);
            darkNumericUpDownParticleX.TabIndex = 23;
            // 
            // darkTitle12
            // 
            darkTitle12.Location = new Point(486, 44);
            darkTitle12.Margin = new Padding(3, 0, 3, 15);
            darkTitle12.Name = "darkTitle12";
            darkTitle12.Size = new Size(22, 30);
            darkTitle12.TabIndex = 26;
            darkTitle12.Text = "Z:";
            // 
            // darkTitle11
            // 
            darkTitle11.Location = new Point(373, 42);
            darkTitle11.Margin = new Padding(3, 0, 3, 15);
            darkTitle11.Name = "darkTitle11";
            darkTitle11.Size = new Size(22, 30);
            darkTitle11.TabIndex = 25;
            darkTitle11.Text = "Y:";
            // 
            // darkButtonSaveAsNewPreset
            // 
            darkButtonSaveAsNewPreset.BorderColour = Color.Empty;
            darkButtonSaveAsNewPreset.CustomColour = false;
            darkButtonSaveAsNewPreset.FlatBottom = false;
            darkButtonSaveAsNewPreset.FlatTop = false;
            darkButtonSaveAsNewPreset.Location = new Point(477, 37);
            darkButtonSaveAsNewPreset.Name = "darkButtonSaveAsNewPreset";
            darkButtonSaveAsNewPreset.Padding = new Padding(5);
            darkButtonSaveAsNewPreset.Size = new Size(203, 34);
            darkButtonSaveAsNewPreset.TabIndex = 20;
            darkButtonSaveAsNewPreset.Text = "Save As New Preset";
            darkButtonSaveAsNewPreset.Click += DarkButtonSaveAsNewPreset_Click;
            // 
            // darkButtonRemovePreset
            // 
            darkButtonRemovePreset.BorderColour = Color.Empty;
            darkButtonRemovePreset.CustomColour = false;
            darkButtonRemovePreset.FlatBottom = false;
            darkButtonRemovePreset.FlatTop = false;
            darkButtonRemovePreset.Location = new Point(574, 72);
            darkButtonRemovePreset.Name = "darkButtonRemovePreset";
            darkButtonRemovePreset.Padding = new Padding(5);
            darkButtonRemovePreset.Size = new Size(163, 34);
            darkButtonRemovePreset.TabIndex = 19;
            darkButtonRemovePreset.Text = "Remove Preset";
            darkButtonRemovePreset.Click += DarkButtonRemovePreset_Click;
            // 
            // darkButtonSavePreset
            // 
            darkButtonSavePreset.BorderColour = Color.Empty;
            darkButtonSavePreset.CustomColour = false;
            darkButtonSavePreset.FlatBottom = false;
            darkButtonSavePreset.FlatTop = false;
            darkButtonSavePreset.Location = new Point(411, 72);
            darkButtonSavePreset.Name = "darkButtonSavePreset";
            darkButtonSavePreset.Padding = new Padding(5);
            darkButtonSavePreset.Size = new Size(163, 34);
            darkButtonSavePreset.TabIndex = 18;
            darkButtonSavePreset.Text = "Save Preset";
            darkButtonSavePreset.Click += DarkButtonSavePreset_Click;
            // 
            // dpdParticuleEffect
            // 
            dpdParticleEffect.DrawMode = DrawMode.OwnerDrawVariable;
            dpdParticleEffect.FormattingEnabled = true;
            dpdParticleEffect.Location = new Point(196, 86);
            dpdParticleEffect.Name = "dpdParticuleEffect";
            dpdParticleEffect.Size = new Size(182, 32);
            dpdParticleEffect.TabIndex = 16;
            dpdParticleEffect.SelectedIndexChanged += DpdParticleEffect_SelectedIndexChanged;
            // 
            // EnvironmentEditor
            // 
            AcceptButton = darkButton1;
            AccessibleName = "EnvironmentEditorWindow";
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = darkButton2;
            ClientSize = new Size(822, 1077);
            Controls.Add(darkGroupBoxParticleSettings);
            Controls.Add(fogSettingsBox);
            Controls.Add(darkButton3);
            Controls.Add(darkButton2);
            Controls.Add(darkButton1);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EnvironmentEditor";
            Text = " Environment Editor";
            Load += EnvironmentEditor_Load;
            ((System.ComponentModel.ISupportInitialize)trackBarFog).EndInit();
            fogSettingsBox.ResumeLayout(false);
            fogSettingsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBackgroundTextureGapColor).EndInit();
            darkGroupBoxParticleSettings.ResumeLayout(false);
            darkGroupBoxParticleSettings.PerformLayout();
            darkGroupBoxParticleVisibility.ResumeLayout(false);
            darkGroupBoxParticleVisibility.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarParticleVisibility).EndInit();
            darkGroupBoxParticleAmount.ResumeLayout(false);
            darkGroupBoxParticleAmount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarParticleAmount).EndInit();
            darkGroupBoxParticleColor.ResumeLayout(false);
            darkGroupBoxParticleColor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLowerColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUpperColor).EndInit();
            darkGroupBoxParticleVelocity.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)darkNumericUpDownParticleZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)darkNumericUpDownParticleY).EndInit();
            ((System.ComponentModel.ISupportInitialize)darkNumericUpDownParticleX).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private MetroSet_UI.Controls.MetroSetCheckBox metroSetCheckBox1;
        private AltUI.Controls.DarkCheckBox darkCheckBoxUseFog;
        private TrackBar trackBarFog;
        private AltUI.Controls.DarkTitle darkTitle1;
        private AltUI.Controls.DarkTitle darkTitle2;
        private AltUI.Controls.DarkTitle darkTitle3;
        private AltUI.Controls.DarkButton darkButton1;
        private AltUI.Controls.DarkButton darkButton2;
        private AltUI.Controls.DarkTitle txtTrackBarFogValue;
        private AltUI.Controls.DarkButton darkButton3;
        private AltUI.Controls.DarkTitle darkTitle5;
        private AltUI.Controls.DarkGroupBox fogSettingsBox;
        private AltUI.Controls.DarkGroupBox darkGroupBoxParticleSettings;
        private AltUI.Controls.DarkButton darkButtonRemovePreset;
        private AltUI.Controls.DarkButton darkButtonSavePreset;
        private AltUI.Controls.DarkComboBox dpdParticleEffect;
        private TrackBar trackBarParticleAmount;
        private AltUI.Controls.DarkTitle darkTitle6;
        private AltUI.Controls.DarkTitle darkTitle7;
        private AltUI.Controls.DarkTitle darkTitle8;
        private AltUI.Controls.DarkButton darkButtonSaveAsNewPreset;
        private AltUI.Controls.DarkNumericUpDown darkNumericUpDownParticleX;
        private AltUI.Controls.DarkGroupBox darkGroupBoxParticleAmount;
        private AltUI.Controls.DarkTitle darkTitle12;
        private AltUI.Controls.DarkTitle darkTitle11;
        private AltUI.Controls.DarkGroupBox darkGroupBoxParticleVelocity;
        private AltUI.Controls.DarkTitle darkTitle13;
        private AltUI.Controls.DarkTitle darkTitle9;
        private AltUI.Controls.DarkNumericUpDown darkNumericUpDownParticleZ;
        private AltUI.Controls.DarkNumericUpDown darkNumericUpDownParticleY;
        private AltUI.Controls.DarkGroupBox darkGroupBoxParticleColor;
        private Cyotek.Windows.Forms.ColorWheel colorWheel2;
        private Cyotek.Windows.Forms.ColorWheel colorWheel1;
        private PictureBox pictureBoxUpperColor;
        private AltUI.Controls.DarkTitle darkTitleUpperColor;
        private AltUI.Controls.DarkTitle darkTitleLowerColor;
        private PictureBox pictureBoxLowerColor;
        private AltUI.Controls.DarkCheckBox darkCheckBoxUseOnlyOneColor;
        private AltUI.Controls.DarkTitle ParticleAmountPercent;
        private AltUI.Controls.DarkCheckBox darkCheckBoxUseParticleEffect;
        private AltUI.Controls.DarkButton darkButtonRestoreDefaultPreset;
        private PictureBox pictureBox1;
        private PictureBox pictureBoxBackgroundTextureGapColor;
        private AltUI.Controls.DarkCheckBox darkCheckBoxBackgroundTextureGapColor;
        private AltUI.Controls.DarkGroupBox darkGroupBoxParticleVisibility;
        private TrackBar trackBar1;
        private AltUI.Controls.DarkTitle darkTitle14;
        private AltUI.Controls.DarkTitle darkTitle16;
        private TrackBar trackBarParticleVisibility;
        private AltUI.Controls.DarkTitle darkTitle10;
    }
}