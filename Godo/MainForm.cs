using Godo.FormsBalancing;
using Godo.FormsChallenges;
using Godo.FormsEnemyData;
using Godo.FormsEquipmentData;
using Godo.FormsInitialisationData;
using Godo.FormsItemData;
using Godo.FormsSpecialHacks;
using Godo.Helper;
using Godo.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            _directory = ResolveRuntimeDirectory();
            Directory.SetCurrentDirectory(_directory);

            _inputScene = Path.Combine(_directory, "Default Files", "scene.bin");
            _inputKernel = Path.Combine(_directory, "Default Files", "kernel.bin");
            _inputKernel2 = Path.Combine(_directory, "Default Files", "kernel2.bin");
            _outputScene = Path.Combine(_directory, "Output Files", "scene.bin");
            _outputKernel = Path.Combine(_directory, "Output Files", "kernel.bin");
            _outputKernel2 = Path.Combine(_directory, "Output Files", "kernel2.bin");
            _miscFile = Path.Combine(_directory, "MiscInput", "FIELD.TDB");

            InitializeComponent();
            InitializeOptionState();
        }

        // Initialises forms for options selection

        #region Forms & Options Arrays
        readonly Spells _spellsForm = new Spells();
        readonly Summons _summonsForm = new Summons();
        readonly EnemySkills _enemySkillsForm = new EnemySkills();
        readonly AttackItems _attackItemsForm = new AttackItems();
        readonly HealItems _healItemsForm = new HealItems();
        readonly StatusItems _statusItemsForm = new StatusItems();
        readonly Weapons _weaponsForm = new Weapons();
        readonly Armour _armourForm = new Armour();
        readonly Accessories _accessoriesForm = new Accessories();
        readonly Materia _materiaForm = new Materia();
        readonly LimitBreaks _limitBreaksForm = new LimitBreaks();
        readonly CharacterStats _characterStatsForm = new CharacterStats();
        readonly StartingEquipment _startingEquipmentForm = new StartingEquipment();
        readonly ModelSwap _swapForm = new ModelSwap();
        readonly EnemyStats _enemyStatsForm = new EnemyStats();
        readonly EnemyAttacks _enemyAttacksForm = new EnemyAttacks();
        readonly EnemyItems _enemyItemsForm = new EnemyItems();
        readonly Formations _formationsForm = new Formations();
        readonly Balancing _balancingForm = new Balancing();
        readonly Challenges _challengesForm = new Challenges();
        readonly SpecialHacks _specialHacksForm = new SpecialHacks();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] spellOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] spellParameters { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] summonOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] summonParameters { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] enemySkillOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] enemySkillParameters { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] attackItemOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] attackItemParameters { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] healItemOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] healItemParameters { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] statusItemOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] statusItemParameters { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] weaponOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] weaponParameters { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] armourOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] armourParameters { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] accessoryOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] accessoryParameters { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] materiaOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] materiaParameters { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] statOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] characterSelectStats { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] statParameters { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] limitOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] characterSelectLimits { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] equipOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] characterSelectEquip { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] equipParameters { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] swapOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] enemyStatOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] enemyStatParameters { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] enemyAttackOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] enemyAttackParameters { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] enemyItemOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] formationOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] balancingOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] balancingParameters { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] challengeOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool[] specialHackOptions { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] specialHackParameters { get; set; }

        readonly bool[] _languageOptions = new bool[5];
        readonly bool[] _rngOption = new bool[1];
        #endregion

        // Properties for file access & seed handling
        readonly string _directory;
        string _inputScene;
        string _inputKernel;
        string _inputKernel2;
        readonly string _outputScene;
        readonly string _outputKernel;
        readonly string _outputKernel2;
        readonly string _miscFile;
        Random _rnd = new Random();
        int _seed;

        private static string ResolveRuntimeDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string applicationDirectory = AppContext.BaseDirectory;
            string applicationParent = Directory.GetParent(applicationDirectory)?.FullName;

            string[] candidates =
            {
                currentDirectory,
                applicationDirectory,
                applicationParent,
                Path.Combine(currentDirectory, "bin", "Debug"),
                Path.Combine(currentDirectory, "bin", "Release")
            };

            string[] dataDirectories =
            {
                "Default Files",
                "Default French Files",
                "Default German Files",
                "Default Spanish Files",
                "Default Japanese Files"
            };

            foreach (string candidate in candidates)
            {
                if (string.IsNullOrEmpty(candidate))
                {
                    continue;
                }

                foreach (string dataDirectory in dataDirectories)
                {
                    if (Directory.Exists(Path.Combine(candidate, dataDirectory)))
                    {
                        return Path.GetFullPath(candidate);
                    }
                }
            }

            return currentDirectory;
        }

        private void InitializeOptionState()
        {
            spellOptions = _spellsForm.spellOptions;
            spellParameters = _spellsForm.spellParameters;
            summonOptions = _summonsForm.summonOptions;
            summonParameters = _summonsForm.summonParameters;
            enemySkillOptions = _enemySkillsForm.enemySkillOptions;
            enemySkillParameters = _enemySkillsForm.enemySkillParameters;
            attackItemOptions = _attackItemsForm.attackItemOptions;
            attackItemParameters = _attackItemsForm.attackItemParameters;
            healItemOptions = _healItemsForm.healItemOptions;
            healItemParameters = _healItemsForm.healItemParameters;
            statusItemOptions = _statusItemsForm.statusItemOptions;
            statusItemParameters = Array.Empty<int>();
            weaponOptions = _weaponsForm.weaponOptions;
            weaponParameters = _weaponsForm.weaponParameters;
            armourOptions = _armourForm.armourOptions;
            armourParameters = _armourForm.armourParameters;
            accessoryOptions = _accessoriesForm.accessoryOptions;
            accessoryParameters = _accessoriesForm.accessoryParameters;
            materiaOptions = _materiaForm.materiaOptions;
            materiaParameters = _materiaForm.materiaParameters;
            statOptions = _characterStatsForm.statOptions;
            statParameters = _characterStatsForm.statParameters;
            characterSelectStats = _characterStatsForm.characterSelectStats;
            limitOptions = _limitBreaksForm.limitOptions;
            characterSelectLimits = _limitBreaksForm.characterSelectLimits;
            equipOptions = _startingEquipmentForm.equipOptions;
            equipParameters = _startingEquipmentForm.equipParameters;
            characterSelectEquip = _startingEquipmentForm.characterSelectEquip;
            swapOptions = _swapForm.swapOptions;
            enemyStatOptions = _enemyStatsForm.enemyStatOptions;
            enemyStatParameters = _enemyStatsForm.enemyStatParameters;
            enemyAttackOptions = _enemyAttacksForm.enemyAttackOptions;
            enemyAttackParameters = _enemyAttacksForm.enemyAttackParameters;
            enemyItemOptions = _enemyItemsForm.enemyItemOptions;
            formationOptions = _formationsForm.formationOptions;
            balancingOptions = _balancingForm.balancingOptions;
            balancingParameters = _balancingForm.balancingParameters;
            challengeOptions = _challengesForm.challengeOptions;
            specialHackOptions = _specialHacksForm.specialHackOptions;
            specialHackParameters = _specialHacksForm.specialHackParameters;
        }

        private void BtnRandoScene_Click(object sender, EventArgs e)
        {
            if (_directory != null)
            {
                RunWorkspace workspace = new RunWorkspace(_directory);
                bool workspacePrepared = false;
                bool runSucceeded = false;
                ScratchCleanupException finalCleanupFailure = null;

                try
                {
                    if (txtSeed.Text != "")
                    {
                        _seed = int.Parse(txtSeed.Text);
                        _rnd = new Random(_seed);
                    }
                    else
                    {
                        _seed = Environment.TickCount;
                        _rnd = new Random(_seed);
                    }

                    bool[] interimOptions = new bool[7];

                    #region Quick Options

                    if (chkWeaponData.Checked)
                    {
                        interimOptions[0] = true;
                    }

                    if (chkArmourData.Checked)
                    {
                        interimOptions[1] = true;
                    }

                    if (chkAccessoryData.Checked)
                    {
                        interimOptions[2] = true;
                    }

                    if(chkCharacterStats.Checked)
                    {
                        interimOptions[3] = true;
                    }

                    if(chkStartingMateria.Checked)
                    {
                        interimOptions[4] = true;
                    }

                    if (chkEnemyStats.Checked)
                    {
                        interimOptions[5] = true;
                    }

                    if(chkEnemyItems.Checked)
                    {
                        interimOptions[6] = true;
                    }
                    #endregion

                    #region Languages
                    Array.Clear(_languageOptions, 0, _languageOptions.Length);

                    if (chkEnglish.Checked)
                    {
                        _languageOptions[0] = true;
                        _inputScene = Path.Combine(_directory, "Default Files", "scene.bin");
                        _inputKernel = Path.Combine(_directory, "Default Files", "kernel.bin");
                        _inputKernel2 = Path.Combine(_directory, "Default Files", "kernel2.bin");
                    }
                    else if (chkFrench.Checked)
                    {
                        _languageOptions[1] = true;
                        _inputScene = Path.Combine(_directory, "Default French Files", "scene.bin");
                        _inputKernel = Path.Combine(_directory, "Default French Files", "kernel.bin");
                        _inputKernel2 = Path.Combine(_directory, "Default French Files", "kernel2.bin");
                    }
                    else if (chkGerman.Checked)
                    {
                        _languageOptions[2] = true;
                        _inputScene = Path.Combine(_directory, "Default German Files", "scene.bin");
                        _inputKernel = Path.Combine(_directory, "Default German Files", "kernel.bin");
                        _inputKernel2 = Path.Combine(_directory, "Default German Files", "kernel2.bin");
                    }
                    else if (chkSpanish.Checked)
                    {
                        _languageOptions[3] = true;
                        _inputScene = Path.Combine(_directory, "Default Spanish Files", "scene.bin");
                        _inputKernel = Path.Combine(_directory, "Default Spanish Files", "kernel.bin");
                        _inputKernel2 = Path.Combine(_directory, "Default Spanish Files", "kernel2.bin");
                    }
                    else if (chkJapanese.Checked)
                    {
                        _languageOptions[4] = true;
                        _inputScene = Path.Combine(_directory, "Default Japanese Files", "scene.bin");
                        _inputKernel = Path.Combine(_directory, "Default Japanese Files", "kernel.bin");
                        _inputKernel2 = Path.Combine(_directory, "Default Japanese Files", "kernel2.bin");
                    }
                    else
                    {
                        // Default to English if all options are unticked
                        _languageOptions[0] = true;
                        _inputScene = Path.Combine(_directory, "Default Files", "scene.bin");
                        _inputKernel = Path.Combine(_directory, "Default Files", "kernel.bin");
                        _inputKernel2 = Path.Combine(_directory, "Default Files", "kernel2.bin");
                    }
                    #endregion

                    // Reset and prepare generated scratch data for the new run.
                    workspace.Prepare();
                    workspacePrepared = true;

                    byte[] kernelLookup = GZipper.PrepareScene(_inputScene, _outputScene,
                        swapOptions,
                        enemyStatOptions, enemyStatParameters,
                        enemyAttackOptions, enemyAttackParameters,
                        enemyItemOptions,
                        formationOptions,
                        balancingOptions, balancingParameters,
                        challengeOptions,
                        specialHackOptions, specialHackParameters,
                        interimOptions,
                        _rnd);
                    GZipper.PrepareKernel(
                        _inputKernel, _inputKernel2,
                        _outputKernel, _outputKernel2, kernelLookup,
                        spellOptions, spellParameters,
                        summonOptions, summonParameters,
                        enemySkillOptions, enemySkillParameters,
                        attackItemOptions, attackItemParameters,
                        healItemOptions, healItemParameters,
                        statusItemOptions, statusItemParameters,
                        weaponOptions, weaponParameters,
                        armourOptions, armourParameters,
                        accessoryOptions, accessoryParameters,
                        materiaOptions, materiaParameters,
                        statOptions, statParameters, characterSelectStats,
                        limitOptions, characterSelectLimits,
                        equipOptions, equipParameters, characterSelectEquip,
                        challengeOptions,
                        specialHackOptions, specialHackParameters,
                        interimOptions,
                        _languageOptions,
                        _rngOption,
                        _rnd);
                    string seedFile = Path.Combine(_directory, "FF7RandomSeeds.txt");
                    if (!File.Exists(seedFile))
                    {
                        using (FileStream fs = File.Create(seedFile))
                        {
                            Byte[] title = new UTF8Encoding(true).GetBytes("Random Seed History");
                            fs.Write(title, 0, title.Length);
                        }
                    }

                    using (StreamWriter w = File.AppendText(seedFile))
                    {
                        Misc.Log(_seed, w);
                    }

                    using (StreamReader r = File.OpenText(seedFile))
                    {
                        Misc.DumpLog(r);
                    }

                    runSucceeded = true;
                }
                catch (ScratchCleanupException ex)
                {
                    MessageBox.Show(
                        "Error: Unable to prepare the randomisation workspace.\n\n" +
                        "File: " + ex.FilePath + "\n" +
                        (ex.InnerException?.Message ?? ex.Message));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Randomisation Failed - Check that valid/fresh files are in correct locations; if so, report the bug along with selected parameters and files used.\n\n" + ex.Message);
                }
                finally
                {
                    if (workspacePrepared)
                    {
                        try
                        {
                            workspace.CleanupScratchFiles();
                        }
                        catch (ScratchCleanupException ex)
                        {
                            finalCleanupFailure = ex;
                        }
                    }
                }

                if (finalCleanupFailure != null)
                {
                    MessageBox.Show(
                        "Randomisation ended, but temporary files could not be cleaned up.\n\n" +
                        "File: " + finalCleanupFailure.FilePath + "\n" +
                        (finalCleanupFailure.InnerException?.Message ?? finalCleanupFailure.Message));
                }
                else if (runSucceeded)
                {
                    MessageBox.Show("Rando Complete: seed = " + _seed);
                }
            }
            else
            {
                MessageBox.Show("Error: Valid directory required");
            }
        }
        #region Form Handling
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Terminate program if main form is closed.
            Application.Exit();
        }

        private void spellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _spellsForm.ShowDialog();
            spellOptions = _spellsForm.spellOptions;
            spellParameters = _spellsForm.spellParameters;
        }

        private void summonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _summonsForm.ShowDialog();
            summonOptions = _summonsForm.summonOptions;
            summonParameters = _summonsForm.summonParameters;
        }

        private void enemySkillsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _enemySkillsForm.ShowDialog();
            enemySkillOptions = _enemySkillsForm.enemySkillOptions;
            enemySkillParameters = _enemySkillsForm.enemySkillParameters;
        }

        private void attackItemsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            _attackItemsForm.ShowDialog();
            attackItemOptions = _attackItemsForm.attackItemOptions;
            attackItemParameters = _attackItemsForm.attackItemParameters;
        }

        private void healItemsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            _healItemsForm.ShowDialog();
            healItemOptions = _healItemsForm.healItemOptions;
            healItemParameters = _healItemsForm.healItemParameters;
        }

        private void statusItemsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            _statusItemsForm.ShowDialog();
            statusItemOptions = _statusItemsForm.statusItemOptions;
        }

        private void weaponsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _weaponsForm.ShowDialog();
            weaponOptions = _weaponsForm.weaponOptions;
            weaponParameters = _weaponsForm.weaponParameters;
        }

        private void armourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _armourForm.ShowDialog();
            armourOptions = _armourForm.armourOptions;
            armourParameters = _armourForm.armourParameters;
        }

        private void accessoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _accessoriesForm.ShowDialog();
            accessoryOptions = _accessoriesForm.accessoryOptions;
            accessoryParameters = _accessoriesForm.accessoryParameters;
        }

        private void materiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _materiaForm.ShowDialog();
            materiaOptions = _materiaForm.materiaOptions;
            materiaParameters = _materiaForm.materiaParameters;
        }

        private void characterStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _characterStatsForm.ShowDialog();
            statOptions = _characterStatsForm.statOptions;
            statParameters = _characterStatsForm.statParameters;
            characterSelectStats = _characterStatsForm.characterSelectStats;
        }

        private void limitBreaksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _limitBreaksForm.ShowDialog();
            limitOptions = _limitBreaksForm.limitOptions;
            characterSelectLimits = _limitBreaksForm.characterSelectLimits;
        }

        private void startingEquipmentToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            _startingEquipmentForm.ShowDialog();
            equipOptions = _startingEquipmentForm.equipOptions;
            equipParameters = _startingEquipmentForm.equipParameters;
            characterSelectEquip = _startingEquipmentForm.characterSelectEquip;
        }

        private void modelSwapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _swapForm.ShowDialog();
            swapOptions = _swapForm.swapOptions;
        }

        private void enemyStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _enemyStatsForm.ShowDialog();
            enemyStatOptions = _enemyStatsForm.enemyStatOptions;
            enemyStatParameters = _enemyStatsForm.enemyStatParameters;
        }

        private void enemyAttacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _enemyAttacksForm.ShowDialog();
            enemyAttackOptions = _enemyAttacksForm.enemyAttackOptions;
            enemyAttackParameters = _enemyAttacksForm.enemyAttackParameters;
        }

        private void enemyItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _enemyItemsForm.ShowDialog();
            enemyItemOptions = _enemyItemsForm.enemyItemOptions;
        }

        private void formationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _formationsForm.ShowDialog();
            formationOptions = _formationsForm.formationOptions;
        }

        private void balanceAutoTuningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _balancingForm.ShowDialog();
            balancingOptions = _balancingForm.balancingOptions;
            balancingParameters = _balancingForm.balancingParameters;
        }

        private void restrictionRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _challengesForm.ShowDialog();
            challengeOptions = _challengesForm.challengeOptions;
        }

        private void specialHacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _specialHacksForm.ShowDialog();
            specialHackOptions = _specialHacksForm.specialHackOptions;
            specialHackParameters = _specialHacksForm.specialHackParameters;
        }

        private void chkEnglish_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkEnglish.Checked)
            {
                return;
            }

            chkFrench.CheckState = CheckState.Unchecked;
            chkGerman.CheckState = CheckState.Unchecked;
            chkSpanish.CheckState = CheckState.Unchecked;
            chkJapanese.CheckState = CheckState.Unchecked;
        }

        private void chkFrench_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFrench.Checked)
            {
                return;
            }

            chkEnglish.CheckState = CheckState.Unchecked;
            chkGerman.CheckState = CheckState.Unchecked;
            chkSpanish.CheckState = CheckState.Unchecked;
            chkJapanese.CheckState = CheckState.Unchecked;
        }

        private void chkGerman_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkGerman.Checked)
            {
                return;
            }

            chkEnglish.CheckState = CheckState.Unchecked;
            chkFrench.CheckState = CheckState.Unchecked;
            chkSpanish.CheckState = CheckState.Unchecked;
            chkJapanese.CheckState = CheckState.Unchecked;
        }

        private void chkSpanish_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSpanish.Checked)
            {
                return;
            }

            chkEnglish.CheckState = CheckState.Unchecked;
            chkFrench.CheckState = CheckState.Unchecked;
            chkGerman.CheckState = CheckState.Unchecked;
            chkJapanese.CheckState = CheckState.Unchecked;
        }
        private void chkJapanese_CheckedChanged_1(object sender, EventArgs e)
        {
            if (!chkJapanese.Checked)
            {
                return;
            }

            chkEnglish.CheckState = CheckState.Unchecked;
            chkFrench.CheckState = CheckState.Unchecked;
            chkGerman.CheckState = CheckState.Unchecked;
            chkSpanish.CheckState = CheckState.Unchecked;
        }
        #endregion

        // This was added to extract textures from a PS1 file for unused character expressions from FIELD.TDB
        private void btnMiscFileDecompress_Click(object sender, EventArgs e)
        {
            // This is for personal use to make use of LZS methods to decompress misc files
            using (BinaryReader ker = new BinaryReader(new FileStream(_miscFile, FileMode.Open)))
            {
                // Retrieves and reads the misc file into memory
                FileInfo miscFileInfo = new FileInfo(_miscFile);
                byte[] compressedMiscFile = new byte[miscFileInfo.Length];
                ker.Read(compressedMiscFile, 0, (int)miscFileInfo.Length);
                Kernel2TextCompressor.MiscDecompress(compressedMiscFile);
            }
        }


    }
}
