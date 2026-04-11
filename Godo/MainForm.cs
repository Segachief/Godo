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
            InitializeComponent();
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

        public bool[] spellOptions { get; set; }
        public int[] spellParameters { get; set; }
        public bool[] summonOptions { get; set; }
        public int[] summonParameters { get; set; }
        public bool[] enemySkillOptions { get; set; }
        public int[] enemySkillParameters { get; set; }
        public bool[] attackItemOptions { get; set; }
        public int[] attackItemParameters { get; set; }
        public bool[] healItemOptions { get; set; }
        public int[] healItemParameters { get; set; }
        public bool[] statusItemOptions { get; set; }
        public int[] statusItemParameters { get; set; }
        public bool[] weaponOptions { get; set; }
        public int[] weaponParameters { get; set; }
        public bool[] armourOptions { get; set; }
        public int[] armourParameters { get; set; }
        public bool[] accessoryOptions { get; set; }
        public int[] accessoryParameters { get; set; }
        public bool[] materiaOptions { get; set; }
        public int[] materiaParameters { get; set; }
        public bool[] statOptions { get; set; }
        public bool[] characterSelectStats { get; set; }
        public int[] statParameters { get; set; }
        public bool[] limitOptions { get; set; }
        public bool[] characterSelectLimits { get; set; }
        public bool[] equipOptions { get; set; }
        public bool[] characterSelectEquip { get; set; }
        public int[] equipParameters { get; set; }
        public bool[] swapOptions { get; set; }
        public bool[] enemyStatOptions { get; set; }
        public int[] enemyStatParameters { get; set; }
        public bool[] enemyAttackOptions { get; set; }
        public int[] enemyAttackParameters { get; set; }
        public bool[] enemyItemOptions { get; set; }
        public bool[] formationOptions { get; set; }
        public bool[] balancingOptions { get; set; }
        public int[] balancingParameters { get; set; }
        public bool[] challengeOptions { get; set; }
        public bool[] specialHackOptions { get; set; }
        public int[] specialHackParameters { get; set; }

        readonly bool[] _languageOptions = new bool[5];
        readonly bool[] _rngOption = new bool[1];
        #endregion

        // Properties for file access & seed handling
        readonly string _directory = Directory.GetCurrentDirectory();
        readonly string _kernelStrings = Directory.GetCurrentDirectory() + "\\Kernel Strings";
        readonly string _kernel2Strings = Directory.GetCurrentDirectory() + "\\Kernel2 Strings";
        string _inputScene = Directory.GetCurrentDirectory() + "\\Default Files\\scene.bin";
        string _inputKernel = Directory.GetCurrentDirectory() + "\\Default Files\\kernel.bin";
        string _inputKernel2 = Directory.GetCurrentDirectory() + "\\Default Files\\kernel2.bin";
        readonly string _outputScene = Directory.GetCurrentDirectory() + "\\Output Files\\scene.bin";
        readonly string _outputKernel = Directory.GetCurrentDirectory() + "\\Output Files\\kernel.bin";
        readonly string _outputKernel2 = Directory.GetCurrentDirectory() + "\\Output Files\\kernel2.bin";
        readonly string _miscFile = Directory.GetCurrentDirectory() + "\\MiscInput\\FIELD.TDB";
        Random _rnd = new Random();
        int _seed;

        private void BtnRandoScene_Click(object sender, EventArgs e)
        {
            if (_directory != null)
            {
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

                    // Arrays instantiated if options weren't used
                    formationOptions = new bool[3];
                    swapOptions = new bool[4];

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
                    if (chkEnglish.Checked)
                    {
                        _languageOptions[0] = true;
                        _inputScene = Directory.GetCurrentDirectory() + "\\Default Files\\scene.bin";
                        _inputKernel = Directory.GetCurrentDirectory() + "\\Default Files\\kernel.bin";
                        _inputKernel2 = Directory.GetCurrentDirectory() + "\\Default Files\\kernel2.bin";
                    }
                    else if (chkFrench.Checked)
                    {
                        _languageOptions[1] = true;
                        _inputScene = Directory.GetCurrentDirectory() + "\\Default French Files\\scene.bin";
                        _inputKernel = Directory.GetCurrentDirectory() + "\\Default French Files\\kernel.bin";
                        _inputKernel2 = Directory.GetCurrentDirectory() + "\\Default French Files\\kernel2.bin";
                    }
                    else if (chkGerman.Checked)
                    {
                        _languageOptions[2] = true;
                        _inputScene = Directory.GetCurrentDirectory() + "\\Default German Files\\scene.bin";
                        _inputKernel = Directory.GetCurrentDirectory() + "\\Default German Files\\kernel.bin";
                        _inputKernel2 = Directory.GetCurrentDirectory() + "\\Default German Files\\kernel2.bin";
                    }
                    else if (chkSpanish.Checked)
                    {
                        _languageOptions[3] = true;
                        _inputScene = Directory.GetCurrentDirectory() + "\\Default Spanish Files\\scene.bin";
                        _inputKernel = Directory.GetCurrentDirectory() + "\\Default Spanish Files\\kernel.bin";
                        _inputKernel2 = Directory.GetCurrentDirectory() + "\\Default Spanish Files\\kernel2.bin";
                    }
                    else if (chkJapanese.Checked)
                    {
                        _languageOptions[4] = true;
                        _inputScene = Directory.GetCurrentDirectory() + "\\Default Japanese Files\\scene.bin";
                        _inputKernel = Directory.GetCurrentDirectory() + "\\Default Japanese Files\\kernel.bin";
                        _inputKernel2 = Directory.GetCurrentDirectory() + "\\Default Japanese Files\\kernel2.bin";
                    }
                    else
                    {
                        // Default to English if all options are unticked
                        _languageOptions[0] = true;
                        _inputScene = Directory.GetCurrentDirectory() + "\\Default Files\\scene.bin";
                        _inputKernel = Directory.GetCurrentDirectory() + "\\Default Files\\kernel.bin";
                        _inputKernel2 = Directory.GetCurrentDirectory() + "\\Default Files\\kernel2.bin";
                    }
                    #endregion

                    //Reset and cleanup for the new run
                    DirectoryInfo deleteKernelStrings = new DirectoryInfo(_kernelStrings);
                    foreach (FileInfo file in deleteKernelStrings.GetFiles())
                    {
                        file.Delete();
                    }
                    DirectoryInfo deleteKernel2Strings = new DirectoryInfo(_kernel2Strings);
                    foreach (FileInfo file in deleteKernel2Strings.GetFiles())
                    {
                        file.Delete();
                    }

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
                    MessageBox.Show("Rando Complete: seed = " + _seed);

                    string seedFile = _directory + "\\FF7RandomSeeds.txt";
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
                }
                catch
                {
                    MessageBox.Show("Error: Randomisation Failed - Check that valid/fresh files are in correct locations; if so, report the bug along with selected parameters and files used.");
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
            // Initialises Options data to be passed
            spellOptions = _spellsForm.spellOptions;
            spellParameters = _spellsForm.spellParameters;
            _spellsForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _spellsForm.VisibleChanged += (sender2, e2) =>
            {
                spellOptions = _spellsForm.spellOptions;
                spellParameters = _spellsForm.spellParameters;
            };
        }

        private void summonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            summonOptions = _summonsForm.summonOptions;
            summonParameters = _summonsForm.summonParameters;
            _summonsForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _summonsForm.VisibleChanged += (sender2, e2) =>
            {
                summonOptions = _summonsForm.summonOptions;
                summonParameters = _summonsForm.summonParameters;
            };
        }

        private void enemySkillsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            enemySkillOptions = _enemySkillsForm.enemySkillOptions;
            enemySkillParameters = _enemySkillsForm.enemySkillParameters;
            _enemySkillsForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _enemySkillsForm.VisibleChanged += (sender2, e2) =>
            {
                enemySkillOptions = _enemySkillsForm.enemySkillOptions;
                enemySkillParameters = _enemySkillsForm.enemySkillParameters;
            };
        }

        private void attackItemsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            attackItemOptions = _attackItemsForm.attackItemOptions;
            attackItemParameters = _attackItemsForm.attackItemParameters;
            _attackItemsForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _attackItemsForm.VisibleChanged += (sender2, e2) =>
            {
                attackItemOptions = _attackItemsForm.attackItemOptions;
                attackItemParameters = _attackItemsForm.attackItemParameters;
            };
        }

        private void healItemsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            healItemOptions = _healItemsForm.healItemOptions;
            healItemParameters = _healItemsForm.healItemParameters;
            _healItemsForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _healItemsForm.VisibleChanged += (sender2, e2) =>
            {
                healItemOptions = _healItemsForm.healItemOptions;
                healItemParameters = _healItemsForm.healItemParameters;
            };
        }

        private void statusItemsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            statusItemOptions = _statusItemsForm.statusItemOptions;
            _statusItemsForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _statusItemsForm.VisibleChanged += (sender2, e2) =>
            {
                statusItemOptions = _statusItemsForm.statusItemOptions;
            };
        }

        private void weaponsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            weaponOptions = _weaponsForm.weaponOptions;
            weaponParameters = _weaponsForm.weaponParameters;
            _weaponsForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _weaponsForm.VisibleChanged += (sender2, e2) =>
            {
                weaponOptions = _weaponsForm.weaponOptions;
                weaponParameters = _weaponsForm.weaponParameters;
            };
        }

        private void armourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            armourOptions = _armourForm.armourOptions;
            armourParameters = _armourForm.armourParameters;
            _armourForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _armourForm.VisibleChanged += (sender2, e2) =>
            {
                armourOptions = _armourForm.armourOptions;
                armourParameters = _armourForm.armourParameters;
            };
        }

        private void accessoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            accessoryOptions = _accessoriesForm.accessoryOptions;
            accessoryParameters = _accessoriesForm.accessoryParameters;
            _accessoriesForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _accessoriesForm.VisibleChanged += (sender2, e2) =>
            {
                accessoryOptions = _accessoriesForm.accessoryOptions;
                accessoryParameters = _accessoriesForm.accessoryParameters;
            };
        }

        private void materiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            materiaOptions = _materiaForm.materiaOptions;
            materiaParameters = _materiaForm.materiaParameters;
            _materiaForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _materiaForm.VisibleChanged += (sender2, e2) =>
            {
                materiaOptions = _materiaForm.materiaOptions;
                materiaParameters = _materiaForm.materiaParameters;
            };
        }

        private void characterStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            statOptions = _characterStatsForm.statOptions;
            statParameters = _characterStatsForm.statParameters;
            characterSelectStats = _characterStatsForm.characterSelectStats;
            _characterStatsForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _characterStatsForm.VisibleChanged += (sender2, e2) =>
            {
                statOptions = _characterStatsForm.statOptions;
                statParameters = _characterStatsForm.statParameters;
                characterSelectStats = _characterStatsForm.characterSelectStats;
            };
        }

        private void limitBreaksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            limitOptions = _limitBreaksForm.limitOptions;
            characterSelectLimits = _limitBreaksForm.characterSelectLimits;
            _limitBreaksForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _limitBreaksForm.VisibleChanged += (sender2, e2) =>
            {
                limitOptions = _limitBreaksForm.limitOptions;
                characterSelectLimits = _limitBreaksForm.characterSelectLimits;
            };
        }

        private void startingEquipmentToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            equipOptions = _startingEquipmentForm.equipOptions;
            equipParameters = _startingEquipmentForm.equipParameters;
            characterSelectEquip = _startingEquipmentForm.characterSelectEquip;
            _startingEquipmentForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _startingEquipmentForm.VisibleChanged += (sender2, e2) =>
            {
                equipOptions = _startingEquipmentForm.equipOptions;
                equipParameters = _startingEquipmentForm.equipParameters;
            };
        }

        private void modelSwapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            swapOptions = _swapForm.swapOptions;
            _swapForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _swapForm.VisibleChanged += (sender2, e2) =>
            {
                swapOptions = _swapForm.swapOptions;
            };
        }

        private void enemyStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            enemyStatOptions = _enemyStatsForm.enemyStatOptions;
            enemyStatParameters = _enemyStatsForm.enemyStatParameters;
            _enemyStatsForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _enemyStatsForm.VisibleChanged += (sender2, e2) =>
            {
                enemyStatOptions = _enemyStatsForm.enemyStatOptions;
                enemyStatParameters = _enemyStatsForm.enemyStatParameters;
            };
        }

        private void enemyAttacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            enemyAttackOptions = _enemyAttacksForm.enemyAttackOptions;
            enemyAttackParameters = _enemyAttacksForm.enemyAttackParameters;
            _enemyAttacksForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _enemyAttacksForm.VisibleChanged += (sender2, e2) =>
            {
                enemyAttackOptions = _enemyAttacksForm.enemyAttackOptions;
                enemyAttackParameters = _enemyAttacksForm.enemyAttackParameters;
            };
        }

        private void enemyItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            enemyItemOptions = _enemyItemsForm.enemyItemOptions;
            _enemyItemsForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _enemyItemsForm.VisibleChanged += (sender2, e2) =>
            {
                enemyItemOptions = _enemyItemsForm.enemyItemOptions;
            };
        }

        private void formationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            formationOptions = _formationsForm.formationOptions;
            _formationsForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _formationsForm.VisibleChanged += (sender2, e2) =>
            {
                formationOptions = _formationsForm.formationOptions;
            };
        }

        private void balanceAutoTuningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            balancingOptions = _balancingForm.balancingOptions;
            balancingParameters = _balancingForm.balancingParameters;
            _balancingForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _balancingForm.VisibleChanged += (sender2, e2) =>
            {
                balancingOptions = _balancingForm.balancingOptions;
                balancingParameters = _balancingForm.balancingParameters;
            };
        }

        private void restrictionRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            challengeOptions = _challengesForm.challengeOptions;
            _challengesForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _challengesForm.VisibleChanged += (sender2, e2) =>
            {
                challengeOptions = _challengesForm.challengeOptions;
            };
        }

        private void specialHacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialises Options data to be passed
            specialHackOptions = _specialHacksForm.specialHackOptions;
            specialHackParameters = _specialHacksForm.specialHackParameters;
            _specialHacksForm.ShowDialog();

            // Hook: When form is re-hidden, grab its options data
            _specialHacksForm.VisibleChanged += (sender2, e2) =>
            {
                specialHackOptions = _specialHacksForm.specialHackOptions;
                specialHackParameters = _specialHacksForm.specialHackParameters;
            };
        }

        private void chkEnglish_CheckedChanged(object sender, EventArgs e)
        {
            chkFrench.CheckState = CheckState.Unchecked;
            chkGerman.CheckState = CheckState.Unchecked;
            chkSpanish.CheckState = CheckState.Unchecked;
            chkJapanese.CheckState = CheckState.Unchecked;
        }

        private void chkFrench_CheckedChanged(object sender, EventArgs e)
        {
            chkEnglish.CheckState = CheckState.Unchecked;
            chkGerman.CheckState = CheckState.Unchecked;
            chkSpanish.CheckState = CheckState.Unchecked;
            chkJapanese.CheckState = CheckState.Unchecked;
        }

        private void chkGerman_CheckedChanged(object sender, EventArgs e)
        {
            chkEnglish.CheckState = CheckState.Unchecked;
            chkFrench.CheckState = CheckState.Unchecked;
            chkSpanish.CheckState = CheckState.Unchecked;
            chkJapanese.CheckState = CheckState.Unchecked;
        }

        private void chkSpanish_CheckedChanged(object sender, EventArgs e)
        {
            chkEnglish.CheckState = CheckState.Unchecked;
            chkFrench.CheckState = CheckState.Unchecked;
            chkGerman.CheckState = CheckState.Unchecked;
            chkJapanese.CheckState = CheckState.Unchecked;
        }
        private void chkJapanese_CheckedChanged_1(object sender, EventArgs e)
        {
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