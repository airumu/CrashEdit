using AltUI.Controls;
using AltUI.Forms;
using System.Diagnostics;
using System.Text;

namespace CrashEdit.CE.Forms
{
    public class RebuildConfig : DarkForm
    {
        public bool Cancelled = false;

        // Form layout constants
        private const int MIN_FORM_WIDTH = 850;
        private const int MIN_FORM_HEIGHT = 575;
        private const int PADDING = 12;
        private const int PADDING_LBL = 16;
        private const int ROW_HEIGHT = 25;
        private static readonly Color HIGHLIGHT_LABEL_COLOR = Color.DarkCyan;
        private static readonly Color NORMAL_LABEL_COLOR = SystemColors.ControlText;

        private const int MAX_ITER_COUNT = 1000000;
        private const int MAX_LL_DISTANCE = 100000;
        private const int MAX_DL_DISTANCE = 100000;
        private const float MAX_BACKW_PENTALTY = 0.5f;
        private const int MAX_PAYLOAD_ALLOWED = 22;
        private const int MAX_LEVEL_ID = 0x3F;
        private const int MAX_SPAWN_INDEX = 50;
        private const int MAX_THREAD_COUNT = 128;
        private const int MAX_BOOST_RATIO = 25;
        private const int MAX_TRANS_PREL_TYPE = 3;
        private const int MAX_ANGLE_3D = 180;
        private const int MAX_MERGE_TYPE = 6;
        private const float MAX_RANDOM_MULT = 5.0f;
        private const int MAX_RANDOM_SEED = 1000000000;

        private Label lblType;
        private DarkComboBox cmbType;
        private Label lblPathNSF;
        private DarkTextBox txtPathNSF;
        private DarkButton btnBrowseNSF;
        private Label lblId;
        private DarkTextBox txtId;
        private Label lblAltSave;
        private DarkButton btnBrowseAltFolder;
        private DarkTextBox txtAltSave;
        private Label lblRemakeLL;
        private DarkComboBox cmbRemakeLL;
        private Label lblMergeType;
        private DarkComboBox cmbMergeType;
        private Label lblSpawnIndex;
        private DarkTextBox txtSpawnIndex;
        private Label lblPathPerma;
        private DarkTextBox txtPathPerma;
        private DarkButton btnBrowsePerma;
        private Label lblPathDeps;
        private DarkTextBox txtPathDeps;
        private DarkButton btnBrowseDeps;
        private Label lblPathCollDeps;
        private DarkTextBox txtPathCollDeps;
        private DarkButton btnBrowseCollDeps;
        private Label lblPathMusicDeps;
        private DarkTextBox txtPathMusicDeps;
        private DarkButton btnBrowseMusicDeps;

        private Label lblDL_Dist_XDist;
        private DarkTextBox txtDL_Dist_XDist;
        private Label lblDL_Dist_YDist;
        private DarkTextBox txtDL_Dist_YDist;
        private Label lblDL_Dist_XZDist;
        private DarkTextBox txtDL_Dist_XZDist;
        private Label lblDL_Dist_Angle3D;
        private DarkTextBox txtDL_Dist_Angle3D;

        private Label lblLL_Dist_SLST;
        private DarkTextBox txtLL_Dist_SLST;
        private Label lblLL_Dist_Neigh;
        private DarkTextBox txtLL_Dist_Neigh;
        private Label lblLL_Dist_Draw;
        private DarkTextBox txtLL_Dist_Draw;
        private Label lblLL_TransPreload;
        private DarkComboBox cmbLL_TransPreload;
        private Label lblBackwPenalty;
        private DarkTextBox txtBackWPenalty;
        private Label lblOmitUnused;
        private DarkComboBox cmbOmitUnused;

        private Label lblMaxPayload;
        private DarkTextBox txtMaxPayload;
        private Label lblIterCount;
        private DarkTextBox txtIterCount;
        private Label lblRandomMult;
        private DarkTextBox txtRandomMult;
        private Label lblRandomSeed;
        private DarkTextBox txtRandomSeed;
        private Label lblThreadCount;
        private DarkTextBox txtThreadCount;
        private Label lblHotBoostRatio;
        private DarkTextBox txtHotBoostRatio;

        private ToolTip tooltip;
        private Panel separatorLine;
        private Panel separatorLineH;
        private Panel separatorLineH2;
        private Panel separatorLineH3;
        private DarkTextBox outputTextArea;
        private CheckBox chkHighlightLabels;
        private DarkButton btnSaveNew;
        private DarkButton btnSaveOver;
        private DarkButton btnCancel;

        private DarkButton btnOpenPathPerma;
        private DarkButton btnOpenPathDeps;
        private DarkButton btnOpenPathCollDeps;
        private DarkButton btnOpenPathMusicDeps;

        private bool PathHadQuotesNSF = false;
        private bool PathHadQuotesAltSave = false;
        private bool PathHadQuotesPerma = false;
        private bool PathHadQuotesDeps = false;
        private bool PathHadQuotesCollDeps = false;
        private bool PathHadQuotesMusicDeps = false;
        private bool KeepPathQuotes = false;

        private RebuildForm rbldForm;
        private bool IsNewConfig = true;
        private string OriginalConfigPath { get; set; }
        private string OriginalContent { get; set; }
        private string ConfigContent { get; set; }

        public RebuildConfig(RebuildForm par, string configFilePath)
        {
            rbldForm = par;
            OriginalConfigPath = configFilePath;
            if (!string.IsNullOrEmpty(configFilePath) && File.Exists(configFilePath))
            {
                OriginalContent = File.ReadAllText(configFilePath);
                IsNewConfig = false;
            }

            ConfigContent = string.Empty;
            InitializeComponent();
        }

        private void AddTooltip(Control lbl, string txt)
        {
            lbl.MouseHover += (s, e) =>
            {
                tooltip.SetToolTip(lbl, txt);
            };
            lbl.MouseLeave += (s, e) =>
            {
                tooltip.Hide(lbl);
            };
        }

        private void IntegerOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (e.g., backspace), and digits only
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void CheckExistingConfig()
        {
            if (IsNewConfig)
                return;

            KeepPathQuotes = true;

            // Load existing config content            
            // remove empty lines and lines that are just 'wipe' or 'kill'
            var lines = OriginalContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            lines = lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
            lines = lines.Where(line => !line.Trim().Equals("wipe", StringComparison.OrdinalIgnoreCase) &&

                                        !line.Trim().Equals("kill", StringComparison.OrdinalIgnoreCase)).ToArray();

            try
            {
                int curr_idx = 0;
                var rbld_type = lines[curr_idx++].ToLower();
                if (rbld_type == "rebuild")
                    cmbType.SelectedIndex = 0;
                else if (rbld_type == "rebuild_dl")
                    cmbType.SelectedIndex = 1;
                else
                    Console.WriteLine($"Invalid rebuild type: {rbld_type}");

                // check for " on start/end, remove if yes
                var path_fix_rem_quotes = (string path, out bool had_quotes) =>
                {
                    had_quotes = false;
                    if (path.StartsWith("\"") && path.EndsWith("\""))
                    {
                        had_quotes = true;
                        path = path[1..^1].Trim();
                    }

                    return path;
                };

                var nsf_path = lines[curr_idx++].Trim();
                txtPathNSF.Text = path_fix_rem_quotes(nsf_path, out PathHadQuotesNSF);

                var levelID = lines[curr_idx++].Trim();
                if (int.TryParse(levelID, System.Globalization.NumberStyles.HexNumber, null, out int idValue) && idValue >= 0 && idValue <= MAX_LEVEL_ID)
                    txtId.Text = idValue.ToString("X");
                else
                    Console.WriteLine($"Invalid level ID in config: {levelID}");

                var alt_save_path = lines[curr_idx++].Trim();
                txtAltSave.Text = path_fix_rem_quotes(alt_save_path, out PathHadQuotesAltSave);

                var remake_ll = lines[curr_idx++].Trim();
                if (remake_ll == "0" || remake_ll == "1" || remake_ll == "2")
                    cmbRemakeLL.SelectedIndex = remake_ll[0] - '0';
                else
                    Console.WriteLine($"Invalid remake LL value in config: {remake_ll}");

                var merge_type = lines[curr_idx++].Trim();
                if (int.TryParse(merge_type, out int mergeIndex) && mergeIndex >= 0 && mergeIndex <= MAX_MERGE_TYPE)
                    cmbMergeType.SelectedIndex = mergeIndex;
                else
                    Console.WriteLine($"Invalid merge type in config: {merge_type}");

                var spawn_index = lines[curr_idx++].Trim();
                if (int.TryParse(spawn_index, out int spawnIndex) && spawnIndex >= 1 && spawnIndex <= MAX_SPAWN_INDEX)
                    txtSpawnIndex.Text = spawnIndex.ToString();
                else
                    Console.WriteLine("$Invalid spawn index in config: {spawn_index}");

                var path_perma = lines[curr_idx++].Trim();
                txtPathPerma.Text = path_fix_rem_quotes(path_perma, out PathHadQuotesPerma);

                if (cmbRemakeLL.SelectedIndex >= 1)
                {
                    var path_deps = lines[curr_idx++].Trim();
                    txtPathDeps.Text = path_fix_rem_quotes(path_deps, out PathHadQuotesDeps);

                    var path_coll_deps = lines[curr_idx++].Trim();
                    txtPathCollDeps.Text = path_fix_rem_quotes(path_coll_deps, out PathHadQuotesCollDeps);

                    var path_music_deps = lines[curr_idx++].Trim();
                    txtPathMusicDeps.Text = path_fix_rem_quotes(path_music_deps, out PathHadQuotesMusicDeps);

                    // rebuild_dl
                    if (cmbType.SelectedIndex == 1)
                    {
                        var dl_dist_x = lines[curr_idx++].Trim();
                        txtDL_Dist_XDist.Text = dl_dist_x;
                        var dl_dist_y = lines[curr_idx++].Trim();
                        txtDL_Dist_YDist.Text = dl_dist_y;
                        var dl_dist_xz = lines[curr_idx++].Trim();
                        txtDL_Dist_XZDist.Text = dl_dist_xz;
                        var dl_angle_3d = lines[curr_idx++].Trim();
                        txtDL_Dist_Angle3D.Text = dl_angle_3d;
                    }

                    var slst_dist = lines[curr_idx++].Trim();
                    if (int.TryParse(slst_dist, out int slstValue) && slstValue >= 0 && slstValue <= MAX_LL_DISTANCE)
                        txtLL_Dist_SLST.Text = slstValue.ToString();
                    else
                        Console.WriteLine($"Invalid LL SLST Dist in config: {slst_dist}");

                    var neigh_dist = lines[curr_idx++].Trim();
                    if (int.TryParse(neigh_dist, out int neighValue) && neighValue >= 0 && neighValue <= MAX_LL_DISTANCE)
                        txtLL_Dist_Neigh.Text = neighValue.ToString();
                    else
                        Console.WriteLine($"Invalid LL Neighbour Dist in config: {neigh_dist}");

                    var draw_dist = lines[curr_idx++].Trim();
                    if (int.TryParse(draw_dist, out int drawValue) && drawValue >= 0 && drawValue <= MAX_LL_DISTANCE)
                        txtLL_Dist_Draw.Text = drawValue.ToString();
                    else
                        Console.WriteLine($"Invalid LL DrawList Dist in config: {draw_dist}");

                    var trans_preload = lines[curr_idx++].Trim();
                    if (int.TryParse(trans_preload, out int trnsLoadValue) && trnsLoadValue >= 0 && trnsLoadValue <= MAX_TRANS_PREL_TYPE)
                        cmbLL_TransPreload.SelectedIndex = trnsLoadValue;
                    else
                        Console.WriteLine($"Invalid LL Trans Preloading in config: {trans_preload}");

                    var backw_penalty = lines[curr_idx++].Trim();
                    if (float.TryParse(backw_penalty, out float backwPenValue) && backwPenValue >= 0 && backwPenValue <= MAX_BACKW_PENTALTY)
                        txtBackWPenalty.Text = backw_penalty;
                    else
                        Console.WriteLine($"Invalid backwards penalty: {backw_penalty}");
                }

                var omit_entries = lines[curr_idx++].Trim();
                if (omit_entries == "0" || omit_entries == "1")
                    cmbOmitUnused.SelectedIndex = omit_entries == "1" ? 1 : 0;
                else
                    Console.WriteLine($"Invalid omit unused entries in config: {omit_entries}");

                var max_payload_limit = lines[curr_idx++].Trim();
                if (int.TryParse(max_payload_limit, out int maxPayload) && maxPayload >= 0 && maxPayload <= MAX_PAYLOAD_ALLOWED)
                    txtMaxPayload.Text = maxPayload.ToString();
                else
                    Console.WriteLine($"Invalid max payload limit in config: {max_payload_limit}");

                var iteration_count = lines[curr_idx++].Trim();
                if (int.TryParse(iteration_count, out int iterCount) && iterCount >= 0 && iterCount <= 1000000)
                    txtIterCount.Text = iterCount.ToString();
                else
                    Console.WriteLine($"Invalid iteration count in config: {iteration_count}");

                var random_mult = lines[curr_idx++].Trim();
                if (float.TryParse(random_mult, out float randMult) && randMult >= 1.0f && randMult <= MAX_RANDOM_MULT)
                    txtRandomMult.Text = random_mult;
                else
                    Console.WriteLine($"Invalid random multiplier in config: {random_mult}");

                var random_seed = lines[curr_idx++].Trim();
                if (int.TryParse(random_seed, out int randSeed) && randSeed >= 0 && randSeed <= MAX_RANDOM_SEED)
                    txtRandomSeed.Text = randSeed.ToString();
                else
                    Console.WriteLine($"Invalid random seed in config: {random_seed}");

                // thread count (method 5)
                var thread_count = lines[curr_idx++].Trim();
                if (int.TryParse(thread_count, out int threadCount) && threadCount >= 1 && threadCount <= MAX_THREAD_COUNT)
                    txtThreadCount.Text = threadCount.ToString();
                else
                    Console.WriteLine($"Invalid thread count in config: {thread_count}");

                var hotspot_boost_ratio = lines[curr_idx++].Trim();
                if (int.TryParse(hotspot_boost_ratio, out int boostRatioInt) && boostRatioInt >= 0 && boostRatioInt <= MAX_BOOST_RATIO)
                    txtHotBoostRatio.Text = boostRatioInt.ToString();
                else
                    Console.WriteLine($"Invalid hotspot boost ratio in config: {hotspot_boost_ratio}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception {ex.Message}");
            }

            UpdateOutputConfig();
            KeepPathQuotes = false;
            string original_content;
            original_content = File.ReadAllText(OriginalConfigPath);

            if (original_content != ConfigContent)
            {
                Cancelled = true;
                var diffForm = new DarkForm
                {
                    Text = $"⚠ Config Mismatch {OriginalConfigPath}",
                    StartPosition = FormStartPosition.CenterParent,
                    Size = new Size(800, 600),
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false,
                };

                var txtOriginal = new DarkTextBox
                {
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    ReadOnly = true,
                    WordWrap = false,
                    Location = new Point(12, 30),
                    Size = new Size(376, 460),
                    Text = original_content
                };
                var txtParsed = new DarkTextBox
                {
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    ReadOnly = true,
                    WordWrap = false,
                    Location = new Point(400, 30),
                    Size = new Size(376, 460),
                    Text = ConfigContent
                };

                var lblOriginal = new Label
                {
                    Text = "Original Content",
                    AutoSize = true,
                    Location = new Point(12, 12)
                };
                var lblParsed = new Label
                {
                    Text = "Parsed Content",
                    AutoSize = true,
                    Location = new Point(400, 12)
                };
                var lblWarning = new Label
                {
                    Text = "The parsed args differ from the original. Please check the changes. See console for potential errors.",
                    ForeColor = Color.Red,
                    AutoSize = true,
                    Location = new Point(12, 530)
                };

                var btnOk = new DarkButton
                {
                    Text = "Continue",
                    DialogResult = DialogResult.OK,
                    Size = new Size(80, 29),
                    Location = new Point(580, 520)
                };
                var btnCancel = new DarkButton
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Size = new Size(88, 29),
                    Location = new Point(680, 520)
                };

                btnOk.Click += (s, e) =>
                {
                    Cancelled = false;
                };

                btnCancel.Click += (s, e) =>
                {
                    diffForm.Close();
                };

                diffForm.Controls.AddRange(new Control[] {
                    lblOriginal,
                    lblParsed,
                    txtOriginal,
                    txtParsed,
                    lblWarning,
                    btnOk,
                    btnCancel
                });

                diffForm.ActiveControl = btnOk;
                diffForm.ShowDialog();
            }
        }

        private void InitializeComponent()
        {
            chkHighlightLabels = new CheckBox();
            btnSaveNew = new DarkButton();
            btnSaveOver = new DarkButton();
            btnCancel = new DarkButton();
            lblType = new Label();
            cmbType = new DarkComboBox();
            outputTextArea = new DarkTextBox();
            separatorLine = new Panel();
            separatorLineH = new Panel();
            separatorLineH2 = new Panel();
            separatorLineH3 = new Panel();
            tooltip = new ToolTip();
            lblPathNSF = new Label();
            txtPathNSF = new DarkTextBox();
            btnBrowseNSF = new DarkButton();
            lblId = new Label();
            txtId = new DarkTextBox();
            lblAltSave = new Label();
            btnBrowseAltFolder = new DarkButton();
            txtAltSave = new DarkTextBox();
            lblRemakeLL = new Label();
            cmbRemakeLL = new DarkComboBox();
            lblMergeType = new Label();
            cmbMergeType = new DarkComboBox();
            lblSpawnIndex = new Label();
            txtSpawnIndex = new DarkTextBox();
            lblPathPerma = new Label();
            txtPathPerma = new DarkTextBox();
            btnBrowsePerma = new DarkButton();
            lblPathDeps = new Label();
            txtPathDeps = new DarkTextBox();
            btnBrowseDeps = new DarkButton();
            lblPathCollDeps = new Label();
            txtPathCollDeps = new DarkTextBox();
            btnBrowseCollDeps = new DarkButton();
            lblPathMusicDeps = new Label();
            txtPathMusicDeps = new DarkTextBox();
            btnBrowseMusicDeps = new DarkButton();
            lblDL_Dist_XDist = new Label();
            txtDL_Dist_XDist = new DarkTextBox();
            lblDL_Dist_YDist = new Label();
            txtDL_Dist_YDist = new DarkTextBox();
            lblDL_Dist_XZDist = new Label();
            txtDL_Dist_XZDist = new DarkTextBox();
            lblDL_Dist_Angle3D = new Label();
            txtDL_Dist_Angle3D = new DarkTextBox();
            lblLL_Dist_SLST = new Label();
            txtLL_Dist_SLST = new DarkTextBox();
            lblLL_Dist_Neigh = new Label();
            txtLL_Dist_Neigh = new DarkTextBox();
            lblLL_Dist_Draw = new Label();
            txtLL_Dist_Draw = new DarkTextBox();
            lblLL_TransPreload = new Label();
            cmbLL_TransPreload = new DarkComboBox();
            lblBackwPenalty = new Label();
            txtBackWPenalty = new DarkTextBox();
            lblOmitUnused = new Label();
            cmbOmitUnused = new DarkComboBox();
            lblMaxPayload = new Label();
            txtMaxPayload = new DarkTextBox();
            lblIterCount = new Label();
            txtIterCount = new DarkTextBox();
            lblRandomMult = new Label();
            txtRandomMult = new DarkTextBox();
            lblRandomSeed = new Label();
            txtRandomSeed = new DarkTextBox();
            lblThreadCount = new Label();
            txtThreadCount = new DarkTextBox();
            lblHotBoostRatio = new Label();
            txtHotBoostRatio = new DarkTextBox();
            btnOpenPathPerma = new DarkButton();
            btnOpenPathDeps = new DarkButton();
            btnOpenPathCollDeps = new DarkButton();
            btnOpenPathMusicDeps = new DarkButton();
            SuspendLayout();

            // Form properties
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;
            MinimumSize = new Size(MIN_FORM_WIDTH, MIN_FORM_HEIGHT + 25);
            ClientSize = new Size(MIN_FORM_WIDTH, MIN_FORM_HEIGHT);
            StartPosition = FormStartPosition.CenterScreen;
            Text = IsNewConfig ? "Create new rebuild args file" : $"Edit rebuild args file - {OriginalConfigPath}";
            Resize += (s, e) => { UpdatePositions(); };

            // Left side controls
            lblType.AutoSize = true;
            lblType.Text = "Rebuild type:";
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.Items.AddRange(["rebuild", "rebuild_dl"]);
            cmbType.SelectedIndex = 0;
            cmbType.SelectedIndexChanged += (s, e) => UpdateOutputConfig();

            // path selector
            lblPathNSF.AutoSize = true;
            lblPathNSF.Text = "NSF path:";
            btnBrowseNSF.Text = "...";
            btnBrowseNSF.Click += (e, a) => btnBrowseNSF_Click();
            txtPathNSF.Text = "level.NSF";
            txtPathNSF.TextChanged += (s, e) => UpdateOutputConfig();

            // ID selector
            lblId.AutoSize = true;
            lblId.Text = "Level ID (hex):";

            txtId.MaxLength = 2;
            txtId.Text = "0";
            txtId.TextChanged += (s, e) => levelID_Changed();
            txtId.Leave += (s, e) =>
            {
                if (txtId.Text.Trim().Length == 0)
                    txtId.Text = "0";
                UpdateOutputConfig();
            };

            // alt save            
            lblAltSave.AutoSize = true;
            lblAltSave.Text = "Alt. save path:";
            btnBrowseAltFolder.Text = "...";
            btnBrowseAltFolder.Click += (e, a) => btnBrowseAlt_Click();
            txtAltSave.Text = "-";
            txtAltSave.TextChanged += (s, e) => UpdateOutputConfig();
            txtAltSave.Leave += (s, e) => { if (txtAltSave.Text.Trim().Length <= 1) txtAltSave.Text = "-"; };

            // remake load lists
            lblRemakeLL.AutoSize = true;
            lblRemakeLL.Text = "Remake load lists:";
            cmbRemakeLL.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRemakeLL.Items.AddRange(["[0] No", "[1] Yes", "[2] Yes + log"]);
            cmbRemakeLL.SelectedIndex = 1;
            cmbRemakeLL.Enabled = true;
            cmbRemakeLL.SelectedIndexChanged += (s, e) => UpdateOutputConfig();

            // Spawn index            
            lblSpawnIndex.AutoSize = true;
            lblSpawnIndex.Text = "Spawn Index:";
            txtSpawnIndex.MaxLength = 3;
            txtSpawnIndex.Text = "1";
            txtSpawnIndex.KeyPress += IntegerOnly_KeyPress;
            txtSpawnIndex.TextChanged += (s, e) => intField_Changed(txtSpawnIndex, 1, MAX_SPAWN_INDEX);
            txtSpawnIndex.Leave += (s, e) => intField_Leave(txtSpawnIndex, "1");

            // Merge type            
            lblMergeType.AutoSize = true;
            lblMergeType.Text = "Rebuild type:";
            cmbMergeType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMergeType.Items.AddRange([
                "[0] BAD occurence count matrix (abs)",
                "[1] BAD occurence count matrix (rel)",
                "[2] BAD relatives & payload",
                "[3] BAD state set graph search based (A*DFS)",
                "[4] occ. count (abs, rand)",
                "[5] occ. count (abs, rand, threaded)",
                "[6] occ. count (abs, rand, threaded, hot-boost)",
                ]);
            cmbMergeType.SelectedIndex = 4;
            cmbMergeType.SelectedIndexChanged += (s, e) =>
            {
                if (cmbMergeType.SelectedIndex < 4)
                    cmbMergeType.SelectedIndex = 4;
                UpdateOutputConfig();
            };

            // permaload file            
            lblPathPerma.AutoSize = true;
            lblPathPerma.Text = "Perma file:";
            btnBrowsePerma.Text = "...";
            btnBrowsePerma.Click += (e, a) => btnBrowsePerma_Click();
            txtPathPerma.Text = "permalist.txt";
            txtPathPerma.TextChanged += (s, e) => UpdateOutputConfig();
            btnOpenPathPerma.Size = new Size(20, 20);
            btnOpenPathPerma.Image = Embeds.Bitmaps["Folder"];
            btnOpenPathPerma.Click += (e, a) => OpenTxtFileDefault(txtPathPerma.Text);

            // dependency file            
            lblPathDeps.AutoSize = true;
            lblPathDeps.Text = "Ent.deps. file:";
            btnBrowseDeps.Text = "...";
            btnBrowseDeps.Click += (e, a) => btnBrowseDeps_Click();
            txtPathDeps.Text = "entity dependencies.txt";
            txtPathDeps.TextChanged += (s, e) => UpdateOutputConfig();
            btnOpenPathDeps.Size = new Size(20, 20);
            btnOpenPathDeps.Image = Embeds.Bitmaps["Folder"];
            btnOpenPathDeps.Click += (e, a) => OpenTxtFileDefault(txtPathDeps.Text);

            // collision dependency file            
            lblPathCollDeps.AutoSize = true;
            lblPathCollDeps.Text = "Coll.deps. file:";
            btnBrowseCollDeps.Text = "...";
            btnBrowseCollDeps.Click += (e, a) => btnBrowseCollDeps_Click();
            txtPathCollDeps.Text = "-";
            txtPathCollDeps.Leave += (s, e) => { if (txtPathCollDeps.Text.Trim().Length <= 1) txtPathCollDeps.Text = "-"; };
            txtPathCollDeps.TextChanged += (s, e) => UpdateOutputConfig();
            btnOpenPathCollDeps.Size = new Size(20, 20);
            btnOpenPathCollDeps.Image = Embeds.Bitmaps["Folder"];
            btnOpenPathCollDeps.Click += (e, a) => OpenTxtFileDefault(txtPathCollDeps.Text);

            // music dependency file            
            lblPathMusicDeps.AutoSize = true;
            lblPathMusicDeps.Text = "Mus.deps. file:";
            btnBrowseMusicDeps.Text = "...";
            btnBrowseMusicDeps.Click += (e, a) => btnBrowseMusicDeps_Click();
            txtPathMusicDeps.Text = "-";
            txtPathMusicDeps.Leave += (s, e) => { if (txtPathMusicDeps.Text.Trim().Length <= 1) txtPathMusicDeps.Text = "-"; };
            txtPathMusicDeps.TextChanged += (s, e) => UpdateOutputConfig();
            btnOpenPathMusicDeps.Size = new Size(20, 20);
            btnOpenPathMusicDeps.Image = Embeds.Bitmaps["Folder"];
            btnOpenPathMusicDeps.Click += (e, a) => OpenTxtFileDefault(txtPathMusicDeps.Text);

            // Distance settings
            lblDL_Dist_XDist.AutoSize = true;
            lblDL_Dist_XDist.Text = "DL DistCap X:";
            txtDL_Dist_XDist.Text = "0";
            txtDL_Dist_XDist.KeyPress += IntegerOnly_KeyPress;
            txtDL_Dist_XDist.TextChanged += (s, e) => intField_Changed(txtDL_Dist_XDist, 0, MAX_DL_DISTANCE);
            txtDL_Dist_XDist.Leave += (s, e) => intField_Leave(txtDL_Dist_XDist, "0");

            lblDL_Dist_YDist.AutoSize = true;
            lblDL_Dist_YDist.Text = "DL DistCap Y:";
            txtDL_Dist_YDist.Text = "0";
            txtDL_Dist_YDist.KeyPress += IntegerOnly_KeyPress;
            txtDL_Dist_YDist.TextChanged += (s, e) => intField_Changed(txtDL_Dist_YDist, 0, MAX_DL_DISTANCE);
            txtDL_Dist_YDist.Leave += (s, e) => intField_Leave(txtDL_Dist_YDist, "0");

            lblDL_Dist_XZDist.AutoSize = true;
            lblDL_Dist_XZDist.Text = "DL DistCap XZ:";
            txtDL_Dist_XZDist.Text = "0";
            txtDL_Dist_XZDist.KeyPress += IntegerOnly_KeyPress;
            txtDL_Dist_XZDist.TextChanged += (s, e) => intField_Changed(txtDL_Dist_XZDist, 0, MAX_DL_DISTANCE);
            txtDL_Dist_XZDist.Leave += (s, e) => intField_Leave(txtDL_Dist_XZDist, "0");

            lblDL_Dist_Angle3D.AutoSize = true;
            lblDL_Dist_Angle3D.Text = "DL AngleCap3D:";
            txtDL_Dist_Angle3D.Text = "0";
            txtDL_Dist_Angle3D.KeyPress += IntegerOnly_KeyPress;
            txtDL_Dist_Angle3D.TextChanged += (s, e) => intField_Changed(txtDL_Dist_Angle3D, 0, MAX_ANGLE_3D);
            txtDL_Dist_Angle3D.Leave += (s, e) => intField_Leave(txtDL_Dist_Angle3D, "0");

            // LL distance settings
            lblLL_Dist_SLST.AutoSize = true;
            lblLL_Dist_SLST.Text = "LL Dist SLST:";
            txtLL_Dist_SLST.Text = "7000";
            txtLL_Dist_SLST.KeyPress += IntegerOnly_KeyPress;
            txtLL_Dist_SLST.TextChanged += (s, e) => intField_Changed(txtLL_Dist_SLST, 0, MAX_LL_DISTANCE);
            txtLL_Dist_SLST.Leave += (s, e) => intField_Leave(txtLL_Dist_SLST, "0");

            lblLL_Dist_Neigh.AutoSize = true;
            lblLL_Dist_Neigh.Text = "LL Dist Neigh:";
            txtLL_Dist_Neigh.Text = "7000";
            txtLL_Dist_Neigh.KeyPress += IntegerOnly_KeyPress;
            txtLL_Dist_Neigh.TextChanged += (s, e) => intField_Changed(txtLL_Dist_Neigh, 0, MAX_LL_DISTANCE);
            txtLL_Dist_Neigh.Leave += (s, e) => intField_Leave(txtLL_Dist_Neigh, "0");

            lblLL_Dist_Draw.AutoSize = true;
            lblLL_Dist_Draw.Text = "LL Dist Draw:";
            txtLL_Dist_Draw.Text = "7000";
            txtLL_Dist_Draw.KeyPress += IntegerOnly_KeyPress;
            txtLL_Dist_Draw.TextChanged += (s, e) => intField_Changed(txtLL_Dist_Draw, 0, MAX_LL_DISTANCE);
            txtLL_Dist_Draw.Leave += (s, e) => intField_Leave(txtLL_Dist_Draw, "0");

            lblLL_TransPreload.AutoSize = true;
            lblLL_TransPreload.Text = "Trans Load. :";
            cmbLL_TransPreload.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLL_TransPreload.Items.AddRange([
                "[0] none",
                "[1] textures",
                "[2] normal entries",
                "[3] all",
                ]);
            cmbLL_TransPreload.SelectedIndex = 3;
            cmbLL_TransPreload.SelectedIndexChanged += (s, e) => UpdateOutputConfig();

            // misc stuff
            lblBackwPenalty.AutoSize = true;
            lblBackwPenalty.Text = "Backw. penalty:";
            lblBackwPenalty.Font = new Font(lblBackwPenalty.Font, FontStyle.Strikeout);
            txtBackWPenalty.Text = "0";
            txtBackWPenalty.KeyPress += (s, e) => floatField_keyPress(txtBackWPenalty, e);
            txtBackWPenalty.TextChanged += (s, e) => UpdateOutputConfig();
            txtBackWPenalty.Leave += (s, e) => floatField_Leave(txtBackWPenalty, "0", 0f, MAX_BACKW_PENTALTY);

            lblOmitUnused.AutoSize = true;
            lblOmitUnused.Text = "Omit unused:";
            lblOmitUnused.Font = new Font(lblOmitUnused.Font, FontStyle.Strikeout);
            cmbOmitUnused.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOmitUnused.Items.AddRange([
                "[0] no",
                "[1] yes",
                ]);
            cmbOmitUnused.SelectedIndex = 0;
            cmbOmitUnused.SelectedIndexChanged += (s, e) => UpdateOutputConfig();

            // rebuild limit stuff
            lblMaxPayload.AutoSize = true;
            lblMaxPayload.Text = "Max payload:";
            txtMaxPayload.Text = "20";
            txtMaxPayload.KeyPress += IntegerOnly_KeyPress;
            txtMaxPayload.TextChanged += (s, e) => intField_Changed(txtMaxPayload, 0, MAX_PAYLOAD_ALLOWED);
            txtMaxPayload.Leave += (s, e) => intField_Leave(txtMaxPayload, "20");

            lblIterCount.AutoSize = true;
            lblIterCount.Text = "Max iterations:";
            txtIterCount.Text = "2000";
            txtIterCount.KeyPress += IntegerOnly_KeyPress;
            txtIterCount.TextChanged += (s, e) => intField_Changed(txtIterCount, 0, MAX_ITER_COUNT);
            txtIterCount.Leave += (s, e) => intField_Leave(txtIterCount, "2000");

            // rebuild random stuff (methods 4/5)            
            lblRandomMult.AutoSize = true;
            lblRandomMult.Text = "Random mult:";
            txtRandomMult.Text = "1.5";
            txtRandomMult.KeyPress += (s, e) => floatField_keyPress(txtRandomMult, e);
            txtRandomMult.TextChanged += (s, e) => UpdateOutputConfig();
            txtRandomMult.Leave += (s, e) => floatField_Leave(txtRandomMult, "1.5", 1.0f, MAX_RANDOM_MULT);

            lblRandomSeed.AutoSize = true;
            lblRandomSeed.Text = "Random seed:";
            txtRandomSeed.Text = "0";
            txtRandomSeed.KeyPress += IntegerOnly_KeyPress;
            txtRandomSeed.TextChanged += (s, e) => intField_Changed(txtRandomSeed, 0, MAX_RANDOM_SEED);
            txtRandomSeed.Leave += (s, e) => intField_Leave(txtRandomSeed, "0");

            // thread count (method 5)
            lblThreadCount.AutoSize = true;
            lblThreadCount.Text = "Thread count:";
            txtThreadCount.Text = "4";
            txtThreadCount.KeyPress += IntegerOnly_KeyPress;
            txtThreadCount.TextChanged += (s, e) => intField_Changed(txtThreadCount, 1, MAX_THREAD_COUNT);
            txtThreadCount.Leave += (s, e) => intField_Leave(txtThreadCount, "4");

            // hotspot boost ratio (method 6)
            lblHotBoostRatio.AutoSize = true;
            lblHotBoostRatio.Text = "Hotspot boost:";
            txtHotBoostRatio.Text = "10";
            txtHotBoostRatio.KeyPress += IntegerOnly_KeyPress;
            txtHotBoostRatio.TextChanged += (s, e) => intField_Changed(txtHotBoostRatio, 0, 200);
            txtHotBoostRatio.Leave += (s, e) => intField_Leave(txtHotBoostRatio, "10");

            // ---------------------------------------------------------------
            // Horizontal separator
            separatorLineH.BackColor = Color.FromArgb(100, 100, 100);
            separatorLineH2.BackColor = Color.FromArgb(100, 100, 100);
            separatorLineH3.BackColor = Color.FromArgb(100, 100, 100);

            // Vertical separator
            separatorLine.BackColor = Color.FromArgb(100, 100, 100);

            // Preview textbox
            outputTextArea.Multiline = true;
            outputTextArea.ScrollBars = ScrollBars.Vertical;
            outputTextArea.ReadOnly = true;
            outputTextArea.WordWrap = false;

            chkHighlightLabels = new CheckBox();
            chkHighlightLabels.Text = "Highlight most important fields";
            chkHighlightLabels.AutoSize = true;
            chkHighlightLabels.Checked = false;
            chkHighlightLabels.CheckedChanged += (s, e) => ToggleHighlightLabels();

            // Save button            
            btnSaveNew.Text = "Save new";
            btnSaveNew.Visible = IsNewConfig;
            btnSaveNew.Click += (e, a) => btnSaveNew_Click();

            btnSaveOver.Text = "Overwrite";
            btnSaveOver.Visible = !IsNewConfig;
            btnSaveOver.Click += (e, a) => btnSaveOver_Click();

            // Cancel button            
            btnCancel.Text = "Cancel";
            btnCancel.Click += (e, a) => BtnCancel_Click();

            // tooltips
            AddTooltip(lblType, "Select whether to do regular rebuild, or rebuild with draw lists");
            AddTooltip(lblPathNSF, "Path to the input (source) NSF file");
            AddTooltip(lblId, "Hexadecimal level ID (0-3F) - can overwrite existing files!");
            AddTooltip(lblAltSave, "[Optional] alternate secondary save path (dir) for the rebuilt NSF");
            AddTooltip(lblRemakeLL, "Select whether to regenerate load lists or keep the existing ones");
            AddTooltip(lblMergeType, "Algorithm to use for merging entries into chunks\nObsolete methods 0-3 are disabled");
            AddTooltip(lblSpawnIndex, "Starting spawn point index (starting from 1)");
            AddTooltip(lblPathPerma, "Path to file containing entries that should always be loaded");
            AddTooltip(lblPathDeps, "Path to file containing entity type/subtype dependencies");
            AddTooltip(lblPathCollDeps, "[Optional] Path to file containing collision dependencies");
            AddTooltip(lblPathMusicDeps, "[Optional] Path to file containing music dependencies");

            AddTooltip(lblDL_Dist_XDist, "Draw list gen - Maximum X distance between camera and object");
            AddTooltip(lblDL_Dist_YDist, "Draw list gen - Maximum Y distance between camera and object");
            AddTooltip(lblDL_Dist_XZDist, "Draw list gen - Maximum XZ distance between camera and object");
            AddTooltip(lblDL_Dist_Angle3D, "Draw list gen, 3D only \nMaximum angle (deg.) difference between camera dir. and object");

            AddTooltip(lblLL_Dist_SLST, "How far in advance to load SLST entries in load lists");
            AddTooltip(lblLL_Dist_Neigh, "How far in advance to load neighbour entries in load lists");
            AddTooltip(lblLL_Dist_Draw, "How far in advance to load drawn entity dependencies in load lists");
            AddTooltip(lblLL_TransPreload, "Type of entries to preload during segment transitions");
            AddTooltip(lblBackwPenalty, "[Not recommended] Penalty multiplier for loading things 'backwards' (0.0-0.5, 0 means no effect)");
            AddTooltip(lblOmitUnused, "[Not recommended] Select whether to omit unused entries from the final NSF\nRecommended option: no");

            AddTooltip(lblMaxPayload, "Maximal normal chunk payload allowed (how many normal chunks at most can be loaded)");
            AddTooltip(lblIterCount, "Maximum number of iterations for merge algorithm (set to 0 for limitless)");
            AddTooltip(lblRandomMult, "Random multiplier for merge algorithm costs (use 1.5 if unsure)\nDefines how 'chaotic' merging is");
            AddTooltip(lblRandomSeed, "Random seed for merge algorithm (use 0 for random seed)");
            AddTooltip(lblThreadCount, "[Merge method 5] Number of parallel threads");
            AddTooltip(lblHotBoostRatio, "[Merge method 6] Boost ratio for pairs loaded in hotspots");

            AddTooltip(btnOpenPathPerma, "Open the perma file in the default text editor");
            AddTooltip(btnOpenPathDeps, "Open the entity dependencies file in the default text editor");
            AddTooltip(btnOpenPathCollDeps, "Open the collision dependencies file in the default text editor");
            AddTooltip(btnOpenPathMusicDeps, "Open the music dependencies file in the default text editor");

            // Add controls to form
            Controls.AddRange([
                lblType,
                cmbType,
                lblPathNSF,
                btnBrowseNSF,
                txtPathNSF,
                lblId,
                txtId,
                lblAltSave,
                btnBrowseAltFolder,
                txtAltSave,
                lblRemakeLL,
                cmbRemakeLL,
                lblMergeType,
                cmbMergeType,
                lblSpawnIndex,
                txtSpawnIndex,
                lblPathPerma,
                btnBrowsePerma,
                txtPathPerma,
                btnOpenPathPerma,
                lblPathDeps,
                btnBrowseDeps,
                txtPathDeps,
                btnOpenPathDeps,
                lblPathCollDeps,
                btnBrowseCollDeps,
                txtPathCollDeps,
                btnOpenPathCollDeps,
                lblPathMusicDeps,
                btnBrowseMusicDeps,
                txtPathMusicDeps,
                btnOpenPathMusicDeps,
                lblDL_Dist_XDist,
                txtDL_Dist_XDist,
                lblDL_Dist_YDist,
                txtDL_Dist_YDist,
                lblDL_Dist_XZDist,
                txtDL_Dist_XZDist,
                lblDL_Dist_Angle3D,
                txtDL_Dist_Angle3D,
                lblLL_Dist_SLST,
                txtLL_Dist_SLST,
                lblLL_Dist_Neigh,
                txtLL_Dist_Neigh,
                lblLL_Dist_Draw,
                txtLL_Dist_Draw,
                lblLL_TransPreload,
                cmbLL_TransPreload,
                lblBackwPenalty,
                txtBackWPenalty,
                lblOmitUnused,
                cmbOmitUnused,
                lblMaxPayload,
                txtMaxPayload,
                lblIterCount,
                txtIterCount,
                lblRandomMult,
                txtRandomMult,
                lblRandomSeed,
                txtRandomSeed,
                lblThreadCount,
                txtThreadCount,
                lblHotBoostRatio,
                txtHotBoostRatio,

                separatorLineH,
                separatorLineH2,
                separatorLineH3,
                separatorLine,
                outputTextArea,
                chkHighlightLabels,
                btnSaveNew,
                btnSaveOver,
                btnCancel,
            ]);

            ResumeLayout(false);
            CheckExistingConfig();
            UpdateOutputConfig();
            UpdatePositions();
        }

        private void UpdateOutputConfig()
        {
            bool is_rebuild_dl = (cmbType.SelectedIndex == 1);
            lblDL_Dist_XDist.Enabled = is_rebuild_dl;
            txtDL_Dist_XDist.Enabled = is_rebuild_dl;
            lblDL_Dist_YDist.Enabled = is_rebuild_dl;
            txtDL_Dist_YDist.Enabled = is_rebuild_dl;
            lblDL_Dist_XZDist.Enabled = is_rebuild_dl;
            txtDL_Dist_XZDist.Enabled = is_rebuild_dl;
            lblDL_Dist_Angle3D.Enabled = is_rebuild_dl;
            txtDL_Dist_Angle3D.Enabled = is_rebuild_dl;

            bool is_threaded = (cmbMergeType.SelectedIndex >= 5);
            txtThreadCount.Enabled = is_threaded;
            lblThreadCount.Enabled = is_threaded;
            bool is_hotboost = (cmbMergeType.SelectedIndex >= 6);
            txtHotBoostRatio.Enabled = is_hotboost;
            lblHotBoostRatio.Enabled = is_hotboost;

            bool is_remake_load_lists = (cmbRemakeLL.SelectedIndex >= 1);
            lblPathDeps.Enabled = is_remake_load_lists;
            txtPathDeps.Enabled = is_remake_load_lists;
            lblPathCollDeps.Enabled = is_remake_load_lists;
            txtPathCollDeps.Enabled = is_remake_load_lists;
            lblPathMusicDeps.Enabled = is_remake_load_lists;
            txtPathMusicDeps.Enabled = is_remake_load_lists;
            lblLL_Dist_Draw.Enabled = is_remake_load_lists;
            txtLL_Dist_Draw.Enabled = is_remake_load_lists;
            lblLL_Dist_Neigh.Enabled = is_remake_load_lists;
            txtLL_Dist_Neigh.Enabled = is_remake_load_lists;
            lblLL_Dist_SLST.Enabled = is_remake_load_lists;
            txtLL_Dist_SLST.Enabled = is_remake_load_lists;
            lblLL_TransPreload.Enabled = is_remake_load_lists;
            cmbLL_TransPreload.Enabled = is_remake_load_lists;
            lblBackwPenalty.Enabled = is_remake_load_lists;
            txtBackWPenalty.Enabled = is_remake_load_lists;

            // maybe handle case without remaking load lists?
            string cfgText = "";
            cfgText += cmbType.SelectedItem + "\r\n";

            if (!KeepPathQuotes)
            {
                PathHadQuotesNSF = false;
                PathHadQuotesAltSave = false;
                PathHadQuotesPerma = false;
                PathHadQuotesDeps = false;
                PathHadQuotesCollDeps = false;
                PathHadQuotesMusicDeps = false;
            }

            var path_fix_add_quote = (string path, bool had_quotes) =>
            {
                path = path.Trim();
                if (path.Contains(" ") || had_quotes)
                    return "\"" + path + "\"";
                return path;
            };

            cfgText += path_fix_add_quote(txtPathNSF.Text, PathHadQuotesNSF) + "\r\n";
            cfgText += txtId.Text + "\r\n";
            cfgText += path_fix_add_quote(txtAltSave.Text, PathHadQuotesAltSave) + "\r\n";
            cfgText += cmbRemakeLL.SelectedIndex + "\r\n";
            cfgText += cmbMergeType.SelectedIndex + "\r\n";
            cfgText += txtSpawnIndex.Text + "\r\n";
            cfgText += path_fix_add_quote(txtPathPerma.Text, PathHadQuotesPerma) + "\r\n";
            if (is_remake_load_lists)
            {
                cfgText += path_fix_add_quote(txtPathDeps.Text, PathHadQuotesDeps) + "\r\n";
                cfgText += path_fix_add_quote(txtPathCollDeps.Text, PathHadQuotesCollDeps) + "\r\n";
                cfgText += path_fix_add_quote(txtPathMusicDeps.Text, PathHadQuotesMusicDeps) + "\r\n";
                if (is_rebuild_dl)
                {
                    cfgText += txtDL_Dist_XDist.Text + "\r\n";
                    cfgText += txtDL_Dist_YDist.Text + "\r\n";
                    cfgText += txtDL_Dist_XZDist.Text + "\r\n";
                    cfgText += txtDL_Dist_Angle3D.Text + "\r\n";
                }
                cfgText += txtLL_Dist_SLST.Text + "\r\n";
                cfgText += txtLL_Dist_Neigh.Text + "\r\n";
                cfgText += txtLL_Dist_Draw.Text + "\r\n";
                cfgText += cmbLL_TransPreload.SelectedIndex + "\r\n";
                cfgText += txtBackWPenalty.Text + "\r\n";
            }
            cfgText += cmbOmitUnused.SelectedIndex + "\r\n";
            cfgText += txtMaxPayload.Text + "\r\n";
            cfgText += txtIterCount.Text + "\r\n";
            cfgText += txtRandomMult.Text + "\r\n";
            cfgText += txtRandomSeed.Text + "\r\n";

            if (is_threaded)
                cfgText += txtThreadCount.Text + "\r\n";

            if (is_hotboost)
                cfgText += txtHotBoostRatio.Text + "\r\n";

            outputTextArea.Text = cfgText;
            ConfigContent = cfgText;
        }

        private void ToggleHighlightLabels()
        {
            var color = chkHighlightLabels.Checked ? HIGHLIGHT_LABEL_COLOR : NORMAL_LABEL_COLOR;
            lblType.ForeColor = color;
            lblPathNSF.ForeColor = color;
            lblId.ForeColor = color;
            lblSpawnIndex.ForeColor = color;
            lblPathPerma.ForeColor = color;
            lblPathDeps.ForeColor = color;
        }

        private void btnSaveOver_Click()
        {
            File.WriteAllText(OriginalConfigPath, ConfigContent);
            Close();
        }

        private void btnSaveNew_Click()
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Config files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.Title = "Save Config File";
                saveFileDialog.FileName = "level rebuild args.txt";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.AddExtension = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, ConfigContent);
                        rbldForm.UpdateConfigPath(saveFileDialog.FileName);
                        Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnCancel_Click()
        {
            Close();
        }

        private void OpenTxtFileDefault(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            if (!Path.IsPathFullyQualified(path))
            {
                path = Path.Combine(rbldForm.GetWD(), path);
            }

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return;

            using Process fileopener = new Process();
            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + path + "\"";
            fileopener.Start();
        }

        private void btnBrowseNSF_Click()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "NSF files (*.NSF)|*.NSF|All files (*.*)|*.*";
                openFileDialog.Title = "Select Input File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPathNSF.Text = openFileDialog.FileName;
                    UpdateOutputConfig();
                }
            }
        }

        private void btnBrowseAlt_Click()
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select Alternate Save Folder";
                folderBrowserDialog.ShowNewFolderButton = true;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtAltSave.Text = folderBrowserDialog.SelectedPath;
                    UpdateOutputConfig();
                }
            }
        }

        private void levelID_Changed()
        {
            // Validate hex input
            if (txtId.Text.Length > 0)
            {
                var save_sel = txtId.SelectionStart;
                bool isValid = int.TryParse(txtId.Text,
                    System.Globalization.NumberStyles.HexNumber,
                    null, out int value);
                if (!isValid || value < 0 || value > MAX_LEVEL_ID)
                {
                    txtId.Text = txtId.Text.Length > 1 ? txtId.Text[..^1] : "";
                }
                txtId.Text = txtId.Text.ToUpper();
                txtId.SelectionStart = save_sel;
            }
            UpdateOutputConfig();
        }

        private void intField_Leave(DarkTextBox txtBox, string def)
        {
            bool isValid = int.TryParse(txtBox.Text, out int value);
            if (txtBox.Text.Trim().Length == 1)
                txtBox.Text = def;
            txtBox.Text = value.ToString();
            UpdateOutputConfig();
        }

        private void intField_Changed(DarkTextBox txtBox, int min, int max)
        {
            if (txtBox.Text.Length > 0)
            {
                var save_sel = txtBox.SelectionStart;
                bool isValid = int.TryParse(txtBox.Text, out int value);
                if (!isValid)
                    value = min;
                value = int.Clamp(value, min, max);
                txtBox.Text = value.ToString();
                txtBox.SelectionStart = save_sel;
            }
            UpdateOutputConfig();
        }

        private void floatField_keyPress(DarkTextBox txtBox, KeyPressEventArgs e)
        {
            if (txtBox.Text.Length > 0)
            {
                {
                    // Allow control keys and digits only
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                        e.Handled = true;
                }
            }
        }

        private void floatField_Leave(DarkTextBox txtBox, string def, float min, float max)
        {
            if (float.TryParse(txtRandomMult.Text, out float value))
            {
                value = float.Clamp(value, min, max);
                txtRandomMult.Text = value.ToString("F2");
            }
            else
            {
                txtRandomMult.Text = def;
            }
            if (txtRandomMult.Text.Trim().Length == 0)
                txtRandomMult.Text = def;
            UpdateOutputConfig();
        }

        private void btnBrowsePerma_Click()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.Title = "Select Permaload File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPathPerma.Text = openFileDialog.FileName;
                    UpdateOutputConfig();
                }
            }
        }
        private void btnBrowseDeps_Click()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.Title = "Select Dependencies File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPathDeps.Text = openFileDialog.FileName;
                    UpdateOutputConfig();
                }
            }
        }

        private void btnBrowseCollDeps_Click()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.Title = "Select Collision Dependencies File";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPathCollDeps.Text = openFileDialog.FileName;
                    UpdateOutputConfig();
                }
            }
        }

        private void btnBrowseMusicDeps_Click()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.Title = "Select Music Dependencies File";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPathMusicDeps.Text = openFileDialog.FileName;
                    UpdateOutputConfig();
                }
            }
        }

        private void UpdatePositions()
        {
            separatorLine.Size = new Size(1, ClientSize.Height - PADDING * 4);
            separatorLine.Location = new Point(ClientSize.Width / 2, PADDING);

            outputTextArea.Location = new Point(ClientSize.Width / 2 + PADDING, PADDING);
            outputTextArea.Size = new Size(ClientSize.Width / 2 - PADDING * 2, ClientSize.Height - PADDING * 5);

            chkHighlightLabels.Location = new Point(PADDING, ClientSize.Height - PADDING * 2);
            btnSaveOver.Size = new Size(88, 29);
            btnSaveOver.Location = new Point(ClientSize.Width - PADDING * 16, ClientSize.Height - PADDING * 3);
            btnSaveNew.Size = new Size(88, 29);
            btnSaveNew.Location = new Point(ClientSize.Width - PADDING * 16, ClientSize.Height - PADDING * 3);
            btnCancel.Size = new Size(88, 29);
            btnCancel.Location = new Point(ClientSize.Width - PADDING * 8, ClientSize.Height - PADDING * 3);

            lblType.Location = new Point(PADDING, PADDING_LBL);
            cmbType.Size = new Size(ClientSize.Width / 2 - 125 - PADDING, 23);
            cmbType.Location = new Point(125, PADDING);

            lblPathNSF.Location = new Point(PADDING, PADDING_LBL + ROW_HEIGHT);
            btnBrowseNSF.Size = new Size(32, 23);
            btnBrowseNSF.Location = new Point(90, PADDING + ROW_HEIGHT);
            txtPathNSF.Size = new Size(ClientSize.Width / 2 - 125 - PADDING, 23);
            txtPathNSF.Location = new Point(125, PADDING + ROW_HEIGHT);

            lblId.Location = new Point(PADDING, PADDING_LBL + ROW_HEIGHT * 2);
            txtId.Size = new Size(32, 23);
            txtId.Location = new Point(ClientSize.Width / 2 - 32 - PADDING, PADDING + ROW_HEIGHT * 2);

            lblAltSave.Location = new Point(PADDING, PADDING_LBL + ROW_HEIGHT * 3);
            btnBrowseAltFolder.Size = new Size(32, 23);
            btnBrowseAltFolder.Location = new Point(90, PADDING + ROW_HEIGHT * 3);
            txtAltSave.Size = new Size(ClientSize.Width / 2 - PADDING - 125, 23);
            txtAltSave.Location = new Point(125, PADDING + ROW_HEIGHT * 3);

            lblRemakeLL.Location = new Point(PADDING, PADDING_LBL + ROW_HEIGHT * 4);
            cmbRemakeLL.Size = new Size(ClientSize.Width / 2 - 125 - PADDING, 23);
            cmbRemakeLL.Location = new Point(125, PADDING + ROW_HEIGHT * 4);

            lblMergeType.Location = new Point(PADDING, PADDING_LBL + ROW_HEIGHT * 5);
            cmbMergeType.Size = new Size(ClientSize.Width / 2 - 125 - PADDING, 23);
            cmbMergeType.Location = new Point(125, PADDING + ROW_HEIGHT * 5);

            lblSpawnIndex.Location = new Point(PADDING, PADDING_LBL + ROW_HEIGHT * 6);
            txtSpawnIndex.Size = new Size(32, 23);
            txtSpawnIndex.Location = new Point(ClientSize.Width / 2 - 32 - PADDING, PADDING + ROW_HEIGHT * 6);

            // Horizontal separator
            separatorLineH.Size = new Size(ClientSize.Width / 2 - PADDING * 2, 1);
            separatorLineH.Location = new Point(PADDING, 4 + PADDING + ROW_HEIGHT * 7);

            lblPathPerma.Location = new Point(PADDING, ROW_HEIGHT / 2 + PADDING_LBL + ROW_HEIGHT * 7);
            btnBrowsePerma.Size = new Size(32, 23);
            btnBrowsePerma.Location = new Point(90, ROW_HEIGHT / 2 + PADDING + ROW_HEIGHT * 7);
            txtPathPerma.Size = new Size(ClientSize.Width / 2 - 150 - PADDING, 23);
            txtPathPerma.Location = new Point(125, ROW_HEIGHT / 2 + PADDING + ROW_HEIGHT * 7);
            btnOpenPathPerma.Location = new Point(ClientSize.Width / 2 - 20 - PADDING, ROW_HEIGHT / 2 + PADDING + ROW_HEIGHT * 7);

            lblPathDeps.Location = new Point(PADDING, ROW_HEIGHT / 2 + PADDING_LBL + ROW_HEIGHT * 8);
            btnBrowseDeps.Size = new Size(32, 23);
            btnBrowseDeps.Location = new Point(90, ROW_HEIGHT / 2 + PADDING + ROW_HEIGHT * 8);
            txtPathDeps.Size = new Size(ClientSize.Width / 2 - 150 - PADDING, 23);
            txtPathDeps.Location = new Point(125, ROW_HEIGHT / 2 + PADDING + ROW_HEIGHT * 8);
            btnOpenPathDeps.Location = new Point(ClientSize.Width / 2 - 20 - PADDING, ROW_HEIGHT / 2 + PADDING + ROW_HEIGHT * 8);

            lblPathCollDeps.Location = new Point(PADDING, ROW_HEIGHT / 2 + PADDING_LBL + ROW_HEIGHT * 9);
            btnBrowseCollDeps.Size = new Size(32, 23);
            btnBrowseCollDeps.Location = new Point(90, ROW_HEIGHT / 2 + PADDING + ROW_HEIGHT * 9);
            txtPathCollDeps.Size = new Size(ClientSize.Width / 2 - 150 - PADDING, 23);
            txtPathCollDeps.Location = new Point(125, ROW_HEIGHT / 2 + PADDING + ROW_HEIGHT * 9);
            btnOpenPathCollDeps.Location = new Point(ClientSize.Width / 2 - 20 - PADDING, ROW_HEIGHT / 2 + PADDING + ROW_HEIGHT * 9);

            lblPathMusicDeps.Location = new Point(PADDING, ROW_HEIGHT / 2 + PADDING_LBL + ROW_HEIGHT * 10);
            btnBrowseMusicDeps.Size = new Size(32, 23);
            btnBrowseMusicDeps.Location = new Point(90, ROW_HEIGHT / 2 + PADDING + ROW_HEIGHT * 10);
            txtPathMusicDeps.Size = new Size(ClientSize.Width / 2 - 150 - PADDING, 23);
            txtPathMusicDeps.Location = new Point(125, ROW_HEIGHT / 2 + PADDING + ROW_HEIGHT * 10);
            btnOpenPathMusicDeps.Location = new Point(ClientSize.Width / 2 - 20 - PADDING, ROW_HEIGHT / 2 + PADDING + ROW_HEIGHT * 10);

            // Horizontal separator 2
            separatorLineH2.Size = new Size(ClientSize.Width / 2 - PADDING * 2, 1);
            separatorLineH2.Location = new Point(PADDING, PADDING_LBL + PADDING + ROW_HEIGHT * 11);

            lblDL_Dist_XDist.Location = new Point(PADDING, PADDING_LBL + ROW_HEIGHT * 12);
            txtDL_Dist_XDist.Size = new Size(80, 23);
            txtDL_Dist_XDist.Location = new Point(125, PADDING + ROW_HEIGHT * 12);

            lblDL_Dist_YDist.Location = new Point(ClientSize.Width / 2 - PADDING - 200, PADDING_LBL + ROW_HEIGHT * 12);
            txtDL_Dist_YDist.Size = new Size(80, 23);
            txtDL_Dist_YDist.Location = new Point(ClientSize.Width / 2 - PADDING - 80, PADDING + ROW_HEIGHT * 12);

            lblDL_Dist_XZDist.Location = new Point(PADDING, PADDING_LBL + ROW_HEIGHT * 13);
            txtDL_Dist_XZDist.Size = new Size(80, 23);
            txtDL_Dist_XZDist.Location = new Point(125, PADDING + ROW_HEIGHT * 13);

            lblDL_Dist_Angle3D.Location = new Point(ClientSize.Width / 2 - PADDING - 200, PADDING_LBL + ROW_HEIGHT * 13);
            txtDL_Dist_Angle3D.Size = new Size(80, 23);
            txtDL_Dist_Angle3D.Location = new Point(ClientSize.Width / 2 - PADDING - 80, PADDING + ROW_HEIGHT * 13);

            // Horizontal separator 3
            separatorLineH3.Size = new Size(ClientSize.Width / 2 - PADDING * 2, 1);
            separatorLineH3.Location = new Point(PADDING, PADDING_LBL / 2 + PADDING + ROW_HEIGHT * 14);

            lblLL_Dist_SLST.Location = new Point(PADDING, PADDING_LBL + PADDING_LBL + ROW_HEIGHT * 14);
            txtLL_Dist_SLST.Size = new Size(80, 23);
            txtLL_Dist_SLST.Location = new Point(125, PADDING_LBL + PADDING + ROW_HEIGHT * 14);

            lblLL_Dist_Neigh.Location = new Point(ClientSize.Width / 2 - PADDING - 200, PADDING_LBL + PADDING_LBL + ROW_HEIGHT * 14);
            txtLL_Dist_Neigh.Size = new Size(80, 23);
            txtLL_Dist_Neigh.Location = new Point(ClientSize.Width / 2 - PADDING - 80, PADDING_LBL + PADDING + ROW_HEIGHT * 14);

            lblLL_Dist_Draw.Location = new Point(PADDING, PADDING_LBL + PADDING_LBL + ROW_HEIGHT * 15);
            txtLL_Dist_Draw.Size = new Size(80, 23);
            txtLL_Dist_Draw.Location = new Point(125, PADDING_LBL + PADDING + ROW_HEIGHT * 15);

            lblLL_TransPreload.Location = new Point(ClientSize.Width / 2 - PADDING - 200, PADDING_LBL + PADDING_LBL + ROW_HEIGHT * 15);
            cmbLL_TransPreload.Size = new Size(80, 23);
            cmbLL_TransPreload.Location = new Point(ClientSize.Width / 2 - PADDING - 80, PADDING_LBL + PADDING + ROW_HEIGHT * 15);

            lblBackwPenalty.Location = new Point(PADDING, PADDING_LBL + PADDING_LBL + ROW_HEIGHT * 16);
            txtBackWPenalty.Size = new Size(80, 23);
            txtBackWPenalty.Location = new Point(125, PADDING_LBL + PADDING + ROW_HEIGHT * 16);

            lblOmitUnused.Location = new Point(ClientSize.Width / 2 - PADDING - 200, PADDING_LBL + PADDING_LBL + ROW_HEIGHT * 16);
            cmbOmitUnused.Size = new Size(80, 23);
            cmbOmitUnused.Location = new Point(ClientSize.Width / 2 - PADDING - 80, PADDING_LBL + PADDING + ROW_HEIGHT * 16);

            lblMaxPayload.Location = new Point(PADDING, PADDING_LBL + PADDING_LBL + ROW_HEIGHT * 17);
            txtMaxPayload.Size = new Size(80, 23);
            txtMaxPayload.Location = new Point(125, PADDING_LBL + PADDING + ROW_HEIGHT * 17);

            lblIterCount.Location = new Point(ClientSize.Width / 2 - PADDING - 200, PADDING_LBL + PADDING_LBL + ROW_HEIGHT * 17);
            txtIterCount.Size = new Size(80, 23);
            txtIterCount.Location = new Point(ClientSize.Width / 2 - PADDING - 80, PADDING_LBL + PADDING + ROW_HEIGHT * 17);

            lblRandomMult.Location = new Point(PADDING, PADDING_LBL + PADDING_LBL + ROW_HEIGHT * 18);
            txtRandomMult.Size = new Size(80, 23);
            txtRandomMult.Location = new Point(125, PADDING_LBL + PADDING + ROW_HEIGHT * 18);

            lblRandomSeed.Location = new Point(ClientSize.Width / 2 - PADDING - 200, PADDING_LBL + PADDING_LBL + ROW_HEIGHT * 18);
            txtRandomSeed.Size = new Size(80, 23);
            txtRandomSeed.Location = new Point(ClientSize.Width / 2 - PADDING - 80, PADDING_LBL + PADDING + ROW_HEIGHT * 18);

            lblThreadCount.Location = new Point(PADDING, PADDING_LBL + PADDING_LBL + ROW_HEIGHT * 19);
            txtThreadCount.Size = new Size(80, 23);
            txtThreadCount.Location = new Point(125, PADDING_LBL + PADDING + ROW_HEIGHT * 19);

            lblHotBoostRatio.Location = new Point(ClientSize.Width / 2 - PADDING - 200, PADDING_LBL + PADDING_LBL + ROW_HEIGHT * 19);
            txtHotBoostRatio.Size = new Size(80, 23);
            txtHotBoostRatio.Location = new Point(ClientSize.Width / 2 - PADDING - 80, PADDING_LBL + PADDING + ROW_HEIGHT * 19);
        }
    }
}