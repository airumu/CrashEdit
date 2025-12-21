using System.Windows.Forms;
using AltUI.Controls;

namespace CrashEdit.CE
{
    partial class MusicBox
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            KillForm();
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            lstMusic = new DoubleBufferedListView();
            txtMusic = new DarkTextBox();
            lbEIDError = new Label();
            fraVABLinks = new DarkGroupBox();
            cmdEditor = new DarkButton();
            fraPlayer = new DarkGroupBox();
            fraControls = new DarkGroupBox();
            numSeqSpeed = new DarkNumericUpDown();
            numSynthVolume = new DarkNumericUpDown();
            lbSeqSpeed = new Label();
            lbSynthVolume = new Label();
            cmdStop = new DarkButton();
            cmdPlay = new DarkButton();
            lbTimeInfo = new Label();
            cmdLoad = new DarkButton();
            trkSeekBar = new MetroSet_UI.Controls.MetroSetTrackBar();
            numSEQ = new DarkNumericUpDown();
            lbTracks = new Label();
            fraVABLinks.SuspendLayout();
            fraPlayer.SuspendLayout();
            fraControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSeqSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSynthVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSEQ).BeginInit();
            SuspendLayout();
            // 
            // lstMusic
            // 
            lstMusic.BackColor = SystemColors.Window;
            lstMusic.BorderStyle = BorderStyle.FixedSingle;
            lstMusic.FullRowSelect = true;
            lstMusic.Location = new Point(6, 22);
            lstMusic.Name = "lstMusic";
            lstMusic.Size = new Size(120, 200);
            lstMusic.TabIndex = 0;
            lstMusic.UseCompatibleStateImageBehavior = false;
            lstMusic.View = View.Details;
            lstMusic.Click += lstMusic_Click;
            // 
            // txtMusic
            // 
            txtMusic.BackColor = Color.FromArgb(26, 26, 28);
            txtMusic.BorderStyle = BorderStyle.FixedSingle;
            txtMusic.ForeColor = Color.FromArgb(213, 213, 213);
            txtMusic.Location = new Point(6, 228);
            txtMusic.MaxLength = 5;
            txtMusic.Name = "txtMusic";
            txtMusic.Size = new Size(120, 23);
            txtMusic.TabIndex = 1;
            txtMusic.TextChanged += txtMusic_TextChanged;
            txtMusic.KeyDown += txtMusic_KeyDown;
            txtMusic.LostFocus += txtMusic_LostFocus;
            // 
            // lbEIDError
            // 
            lbEIDError.AutoSize = true;
            lbEIDError.BackColor = Color.Transparent;
            lbEIDError.ForeColor = Color.Red;
            lbEIDError.Location = new Point(6, 254);
            lbEIDError.Name = "lbEIDError";
            lbEIDError.Size = new Size(63, 15);
            lbEIDError.TabIndex = 2;
            lbEIDError.Text = "EIDERROR!";
            // 
            // fraVABLinks
            // 
            fraVABLinks.AutoSize = true;
            fraVABLinks.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraVABLinks.BackColor = Color.Transparent;
            fraVABLinks.Controls.Add(lbEIDError);
            fraVABLinks.Controls.Add(lstMusic);
            fraVABLinks.Controls.Add(txtMusic);
            fraVABLinks.Controls.Add(cmdEditor);
            fraVABLinks.Location = new Point(3, 3);
            fraVABLinks.Name = "fraVABLinks";
            fraVABLinks.Size = new Size(224, 288);
            fraVABLinks.TabIndex = 3;
            fraVABLinks.TabStop = false;
            fraVABLinks.Text = "VAB Links";
            // 
            // cmdEditor
            // 
            cmdEditor.BorderColour = Color.Empty;
            cmdEditor.CustomColour = false;
            cmdEditor.FlatBottom = false;
            cmdEditor.FlatTop = false;
            cmdEditor.Location = new Point(132, 22);
            cmdEditor.Name = "cmdEditor";
            cmdEditor.Padding = new Padding(5);
            cmdEditor.Size = new Size(86, 32);
            cmdEditor.TabIndex = 3;
            cmdEditor.Text = "Open Editor";
            cmdEditor.Click += cmdEditor_Click;
            // 
            // fraPlayer
            // 
            fraPlayer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraPlayer.BackColor = Color.Transparent;
            fraPlayer.Controls.Add(fraControls);
            fraPlayer.Controls.Add(cmdStop);
            fraPlayer.Controls.Add(cmdPlay);
            fraPlayer.Controls.Add(lbTimeInfo);
            fraPlayer.Controls.Add(cmdLoad);
            fraPlayer.Controls.Add(trkSeekBar);
            fraPlayer.Controls.Add(numSEQ);
            fraPlayer.Controls.Add(lbTracks);
            fraPlayer.Location = new Point(3, 297);
            fraPlayer.Name = "fraPlayer";
            fraPlayer.Size = new Size(327, 222);
            fraPlayer.TabIndex = 4;
            fraPlayer.TabStop = false;
            fraPlayer.Text = "Player";
            // 
            // fraControls
            // 
            fraControls.BackColor = Color.Transparent;
            fraControls.Controls.Add(numSeqSpeed);
            fraControls.Controls.Add(numSynthVolume);
            fraControls.Controls.Add(lbSeqSpeed);
            fraControls.Controls.Add(lbSynthVolume);
            fraControls.Location = new Point(189, 19);
            fraControls.Name = "fraControls";
            fraControls.Size = new Size(132, 122);
            fraControls.TabIndex = 4;
            fraControls.TabStop = false;
            fraControls.Text = "Controls";
            // 
            // numSeqSpeed
            // 
            numSeqSpeed.DecimalPlaces = 2;
            numSeqSpeed.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numSeqSpeed.Location = new Point(6, 89);
            numSeqSpeed.Maximum = new decimal(new int[] { 20, 0, 0, 65536 });
            numSeqSpeed.Minimum = new decimal(new int[] { 5, 0, 0, 65536 });
            numSeqSpeed.Name = "numSeqSpeed";
            numSeqSpeed.Size = new Size(120, 23);
            numSeqSpeed.TabIndex = 1;
            numSeqSpeed.Value = new decimal(new int[] { 10, 0, 0, 65536 });
            numSeqSpeed.ValueChanged += numSeqSpeed_ValueChanged;
            // 
            // numSynthVolume
            // 
            numSynthVolume.DecimalPlaces = 1;
            numSynthVolume.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numSynthVolume.Location = new Point(6, 37);
            numSynthVolume.Maximum = new decimal(new int[] { 20, 0, 0, 65536 });
            numSynthVolume.Name = "numSynthVolume";
            numSynthVolume.Size = new Size(120, 23);
            numSynthVolume.TabIndex = 1;
            numSynthVolume.Value = new decimal(new int[] { 10, 0, 0, 65536 });
            numSynthVolume.ValueChanged += numSynthVolumee_ValueChanged;
            // 
            // lbSeqSpeed
            // 
            lbSeqSpeed.AutoSize = true;
            lbSeqSpeed.Location = new Point(6, 71);
            lbSeqSpeed.Name = "lbSeqSpeed";
            lbSeqSpeed.Size = new Size(39, 15);
            lbSeqSpeed.TabIndex = 0;
            lbSeqSpeed.Text = "Speed";
            // 
            // lbSynthVolume
            // 
            lbSynthVolume.AutoSize = true;
            lbSynthVolume.Location = new Point(6, 19);
            lbSynthVolume.Name = "lbSynthVolume";
            lbSynthVolume.Size = new Size(47, 15);
            lbSynthVolume.TabIndex = 0;
            lbSynthVolume.Text = "Volume";
            // 
            // cmdStop
            // 
            cmdStop.BorderColour = Color.Empty;
            cmdStop.CustomColour = false;
            cmdStop.FlatBottom = false;
            cmdStop.FlatTop = false;
            cmdStop.Location = new Point(6, 124);
            cmdStop.Name = "cmdStop";
            cmdStop.Padding = new Padding(5);
            cmdStop.Size = new Size(75, 23);
            cmdStop.TabIndex = 3;
            cmdStop.Text = "Stop";
            cmdStop.Click += cmdStop_Click;
            // 
            // cmdPlay
            // 
            cmdPlay.BorderColour = Color.Empty;
            cmdPlay.CustomColour = false;
            cmdPlay.FlatBottom = false;
            cmdPlay.FlatTop = false;
            cmdPlay.Location = new Point(6, 95);
            cmdPlay.Name = "cmdPlay";
            cmdPlay.Padding = new Padding(5);
            cmdPlay.Size = new Size(75, 23);
            cmdPlay.TabIndex = 3;
            cmdPlay.Text = "Play";
            cmdPlay.Click += cmdPlay_Click;
            // 
            // lbTimeInfo
            // 
            lbTimeInfo.AutoSize = true;
            lbTimeInfo.Location = new Point(6, 168);
            lbTimeInfo.Name = "lbTimeInfo";
            lbTimeInfo.Size = new Size(72, 15);
            lbTimeInfo.TabIndex = 0;
            lbTimeInfo.Text = "00:00 / 00:00";
            // 
            // cmdLoad
            // 
            cmdLoad.BorderColour = Color.Empty;
            cmdLoad.CustomColour = false;
            cmdLoad.FlatBottom = false;
            cmdLoad.FlatTop = false;
            cmdLoad.Location = new Point(6, 66);
            cmdLoad.Name = "cmdLoad";
            cmdLoad.Padding = new Padding(5);
            cmdLoad.Size = new Size(75, 23);
            cmdLoad.TabIndex = 3;
            cmdLoad.Text = "Load VAB";
            cmdLoad.Click += cmdLoad_Click;
            // 
            // trkSeekBar
            // 
            trkSeekBar.BackgroundColor = Color.FromArgb(90, 90, 90);
            trkSeekBar.DisabledBackColor = Color.FromArgb(80, 80, 80);
            trkSeekBar.DisabledBorderColor = Color.Empty;
            trkSeekBar.DisabledHandlerColor = Color.FromArgb(90, 90, 90);
            trkSeekBar.DisabledValueColor = Color.FromArgb(109, 109, 109);
            trkSeekBar.HandlerColor = Color.FromArgb(143, 143, 143);
            trkSeekBar.IsDerivedStyle = true;
            trkSeekBar.Location = new Point(6, 192);
            trkSeekBar.Maximum = 4096;
            trkSeekBar.Minimum = 0;
            trkSeekBar.Name = "trkSeekBar";
            trkSeekBar.Size = new Size(180, 16);
            trkSeekBar.Style = MetroSet_UI.Enums.Style.Dark;
            trkSeekBar.StyleManager = null;
            trkSeekBar.TabIndex = 2;
            trkSeekBar.Text = "SeekBar";
            trkSeekBar.ThemeAuthor = "Narwin";
            trkSeekBar.ThemeName = "MetroDark";
            trkSeekBar.TickFrequency = 64;
            trkSeekBar.Value = 0;
            trkSeekBar.ValueColor = Color.FromArgb(65, 177, 225);
            trkSeekBar.ValueChanged += trkSeekBar_ValueChanged;
            trkSeekBar.MouseDown += trkSeekBar_MouseDown;
            trkSeekBar.MouseUp += trkSeekBar_MouseUp;
            trkSeekBar.MouseWheel += trkSeekBar_MouseWheel;
            // 
            // numSEQ
            // 
            numSEQ.Location = new Point(6, 37);
            numSEQ.Maximum = new decimal(new int[] { 0, 0, 0, 0 });
            numSEQ.Name = "numSEQ";
            numSEQ.Size = new Size(57, 23);
            numSEQ.TabIndex = 1;
            numSEQ.ValueChanged += numSEQ_ValueChanged;
            // 
            // lbTracks
            // 
            lbTracks.AutoSize = true;
            lbTracks.Location = new Point(6, 19);
            lbTracks.Name = "lbTracks";
            lbTracks.Size = new Size(39, 15);
            lbTracks.TabIndex = 0;
            lbTracks.Text = "Tracks";
            // 
            // MusicBox
            // 
            BackColor = Color.FromArgb(31, 31, 32);
            Controls.Add(fraPlayer);
            Controls.Add(fraVABLinks);
            Name = "MusicBox";
            Size = new Size(520, 544);
            Leave += musicBox_Leave;
            fraVABLinks.ResumeLayout(false);
            fraVABLinks.PerformLayout();
            fraPlayer.ResumeLayout(false);
            fraPlayer.PerformLayout();
            fraControls.ResumeLayout(false);
            fraControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSeqSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSynthVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSEQ).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DoubleBufferedListView lstMusic;
        private DarkTextBox txtMusic;
        private Label lbEIDError;
        private DarkGroupBox fraVABLinks;
        private DarkGroupBox fraPlayer;
        private DarkNumericUpDown numSEQ;
        private Label lbTracks;
        private MetroSet_UI.Controls.MetroSetTrackBar trkSeekBar;
        private DarkButton cmdLoad;
        private DarkButton cmdStop;
        private DarkButton cmdPlay;
        private Label lbTimeInfo;
        private DarkGroupBox fraControls;
        private DarkNumericUpDown numSeqSpeed;
        private DarkNumericUpDown numSynthVolume;
        private Label lbSeqSpeed;
        private Label lbSynthVolume;
        private DarkButton cmdEditor;
    }
}
