namespace Godo
{
    partial class MainForm
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
            this.btnRandoScene = new System.Windows.Forms.Button();
            this.txtSeed = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkCharacterStats = new System.Windows.Forms.CheckBox();
            this.chkStartingMateria = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkWeaponData = new System.Windows.Forms.CheckBox();
            this.chkArmourData = new System.Windows.Forms.CheckBox();
            this.chkAccessoryData = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkEnemyStats = new System.Windows.Forms.CheckBox();
            this.chkEnemyItems = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.spellsEquipmentDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spellsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.summonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enemySkillsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attackItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.healItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.weaponsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.armourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accessoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.characterInitialisationDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.characterStatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limitBreaksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startingEquipmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rNGTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enemyFormationDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelSwapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enemyStatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enemyAttacksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enemyItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.balanceAutoTuningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restrictionRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specialHacksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkEnglish = new System.Windows.Forms.CheckBox();
            this.chkFrench = new System.Windows.Forms.CheckBox();
            this.chkGerman = new System.Windows.Forms.CheckBox();
            this.chkSpanish = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnMiscFileDecompress = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRandoScene
            // 
            this.btnRandoScene.Location = new System.Drawing.Point(15, 184);
            this.btnRandoScene.Name = "btnRandoScene";
            this.btnRandoScene.Size = new System.Drawing.Size(100, 23);
            this.btnRandoScene.TabIndex = 3;
            this.btnRandoScene.Text = "Randomise";
            this.btnRandoScene.UseVisualStyleBackColor = true;
            this.btnRandoScene.Click += new System.EventHandler(this.BtnRandoScene_Click);
            // 
            // txtSeed
            // 
            this.txtSeed.Location = new System.Drawing.Point(15, 158);
            this.txtSeed.Name = "txtSeed";
            this.txtSeed.Size = new System.Drawing.Size(100, 20);
            this.txtSeed.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Seed";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.chkCharacterStats);
            this.flowLayoutPanel1.Controls.Add(this.chkStartingMateria);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(548, 68);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(115, 144);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // chkCharacterStats
            // 
            this.chkCharacterStats.AutoSize = true;
            this.chkCharacterStats.Checked = true;
            this.chkCharacterStats.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCharacterStats.Location = new System.Drawing.Point(3, 3);
            this.chkCharacterStats.Name = "chkCharacterStats";
            this.chkCharacterStats.Size = new System.Drawing.Size(99, 17);
            this.chkCharacterStats.TabIndex = 0;
            this.chkCharacterStats.Text = "Character Stats";
            this.chkCharacterStats.UseVisualStyleBackColor = true;
            // 
            // chkStartingMateria
            // 
            this.chkStartingMateria.AutoSize = true;
            this.chkStartingMateria.Checked = true;
            this.chkStartingMateria.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStartingMateria.Location = new System.Drawing.Point(3, 26);
            this.chkStartingMateria.Name = "chkStartingMateria";
            this.chkStartingMateria.Size = new System.Drawing.Size(100, 17);
            this.chkStartingMateria.TabIndex = 1;
            this.chkStartingMateria.Text = "Starting Materia";
            this.chkStartingMateria.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chkWeaponData);
            this.flowLayoutPanel2.Controls.Add(this.chkArmourData);
            this.flowLayoutPanel2.Controls.Add(this.chkAccessoryData);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(444, 68);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(99, 144);
            this.flowLayoutPanel2.TabIndex = 10;
            // 
            // chkWeaponData
            // 
            this.chkWeaponData.AutoSize = true;
            this.chkWeaponData.Checked = true;
            this.chkWeaponData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWeaponData.Location = new System.Drawing.Point(3, 3);
            this.chkWeaponData.Name = "chkWeaponData";
            this.chkWeaponData.Size = new System.Drawing.Size(72, 17);
            this.chkWeaponData.TabIndex = 2;
            this.chkWeaponData.Text = "Weapons";
            this.chkWeaponData.UseVisualStyleBackColor = true;
            // 
            // chkArmourData
            // 
            this.chkArmourData.AutoSize = true;
            this.chkArmourData.Checked = true;
            this.chkArmourData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkArmourData.Location = new System.Drawing.Point(3, 26);
            this.chkArmourData.Name = "chkArmourData";
            this.chkArmourData.Size = new System.Drawing.Size(59, 17);
            this.chkArmourData.TabIndex = 3;
            this.chkArmourData.Text = "Armour";
            this.chkArmourData.UseVisualStyleBackColor = true;
            // 
            // chkAccessoryData
            // 
            this.chkAccessoryData.AutoSize = true;
            this.chkAccessoryData.Checked = true;
            this.chkAccessoryData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAccessoryData.Location = new System.Drawing.Point(3, 49);
            this.chkAccessoryData.Name = "chkAccessoryData";
            this.chkAccessoryData.Size = new System.Drawing.Size(83, 17);
            this.chkAccessoryData.TabIndex = 4;
            this.chkAccessoryData.Text = "Accessories";
            this.chkAccessoryData.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.chkEnemyStats);
            this.flowLayoutPanel4.Controls.Add(this.chkEnemyItems);
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(666, 68);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(126, 144);
            this.flowLayoutPanel4.TabIndex = 12;
            // 
            // chkEnemyStats
            // 
            this.chkEnemyStats.AutoSize = true;
            this.chkEnemyStats.Checked = true;
            this.chkEnemyStats.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnemyStats.Location = new System.Drawing.Point(3, 3);
            this.chkEnemyStats.Name = "chkEnemyStats";
            this.chkEnemyStats.Size = new System.Drawing.Size(114, 17);
            this.chkEnemyStats.TabIndex = 22;
            this.chkEnemyStats.Text = "Enemy Parameters";
            this.chkEnemyStats.UseVisualStyleBackColor = true;
            // 
            // chkEnemyItems
            // 
            this.chkEnemyItems.AutoSize = true;
            this.chkEnemyItems.Checked = true;
            this.chkEnemyItems.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnemyItems.Location = new System.Drawing.Point(3, 26);
            this.chkEnemyItems.Name = "chkEnemyItems";
            this.chkEnemyItems.Size = new System.Drawing.Size(86, 17);
            this.chkEnemyItems.TabIndex = 9;
            this.chkEnemyItems.Text = "Enemy Items";
            this.chkEnemyItems.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(441, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Equipment";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(545, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Character Data";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(663, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Enemy Data";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(433, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(174, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Quick Randomisation Options";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spellsEquipmentDataToolStripMenuItem,
            this.characterInitialisationDataToolStripMenuItem,
            this.enemyFormationDataToolStripMenuItem,
            this.balanceAutoTuningToolStripMenuItem,
            this.restrictionRulesToolStripMenuItem,
            this.specialHacksToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(817, 24);
            this.menuStrip1.TabIndex = 28;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // spellsEquipmentDataToolStripMenuItem
            // 
            this.spellsEquipmentDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spellsToolStripMenuItem,
            this.summonsToolStripMenuItem,
            this.enemySkillsToolStripMenuItem,
            this.itemsToolStripMenuItem,
            this.weaponsToolStripMenuItem,
            this.armourToolStripMenuItem,
            this.accessoriesToolStripMenuItem,
            this.materiaToolStripMenuItem});
            this.spellsEquipmentDataToolStripMenuItem.Name = "spellsEquipmentDataToolStripMenuItem";
            this.spellsEquipmentDataToolStripMenuItem.Size = new System.Drawing.Size(139, 20);
            this.spellsEquipmentDataToolStripMenuItem.Text = "Spells/Equipment Data";
            this.spellsEquipmentDataToolStripMenuItem.Visible = false;
            // 
            // spellsToolStripMenuItem
            // 
            this.spellsToolStripMenuItem.Name = "spellsToolStripMenuItem";
            this.spellsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.spellsToolStripMenuItem.Text = "Spells";
            this.spellsToolStripMenuItem.Click += new System.EventHandler(this.spellsToolStripMenuItem_Click);
            // 
            // summonsToolStripMenuItem
            // 
            this.summonsToolStripMenuItem.Name = "summonsToolStripMenuItem";
            this.summonsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.summonsToolStripMenuItem.Text = "Summons";
            this.summonsToolStripMenuItem.Click += new System.EventHandler(this.summonsToolStripMenuItem_Click);
            // 
            // enemySkillsToolStripMenuItem
            // 
            this.enemySkillsToolStripMenuItem.Name = "enemySkillsToolStripMenuItem";
            this.enemySkillsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.enemySkillsToolStripMenuItem.Text = "Enemy Skills";
            this.enemySkillsToolStripMenuItem.Click += new System.EventHandler(this.enemySkillsToolStripMenuItem_Click);
            // 
            // itemsToolStripMenuItem
            // 
            this.itemsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attackItemsToolStripMenuItem,
            this.healItemsToolStripMenuItem,
            this.statusItemsToolStripMenuItem});
            this.itemsToolStripMenuItem.Name = "itemsToolStripMenuItem";
            this.itemsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.itemsToolStripMenuItem.Text = "Items";
            // 
            // attackItemsToolStripMenuItem
            // 
            this.attackItemsToolStripMenuItem.Name = "attackItemsToolStripMenuItem";
            this.attackItemsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.attackItemsToolStripMenuItem.Text = "Attack Items";
            this.attackItemsToolStripMenuItem.Click += new System.EventHandler(this.attackItemsToolStripMenuItem_Click_1);
            // 
            // healItemsToolStripMenuItem
            // 
            this.healItemsToolStripMenuItem.Name = "healItemsToolStripMenuItem";
            this.healItemsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.healItemsToolStripMenuItem.Text = "Heal Items";
            this.healItemsToolStripMenuItem.Click += new System.EventHandler(this.healItemsToolStripMenuItem_Click_1);
            // 
            // statusItemsToolStripMenuItem
            // 
            this.statusItemsToolStripMenuItem.Name = "statusItemsToolStripMenuItem";
            this.statusItemsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.statusItemsToolStripMenuItem.Text = "Status Items";
            this.statusItemsToolStripMenuItem.Click += new System.EventHandler(this.statusItemsToolStripMenuItem_Click_1);
            // 
            // weaponsToolStripMenuItem
            // 
            this.weaponsToolStripMenuItem.Name = "weaponsToolStripMenuItem";
            this.weaponsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.weaponsToolStripMenuItem.Text = "Weapons";
            this.weaponsToolStripMenuItem.Click += new System.EventHandler(this.weaponsToolStripMenuItem_Click);
            // 
            // armourToolStripMenuItem
            // 
            this.armourToolStripMenuItem.Name = "armourToolStripMenuItem";
            this.armourToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.armourToolStripMenuItem.Text = "Armour";
            this.armourToolStripMenuItem.Click += new System.EventHandler(this.armourToolStripMenuItem_Click);
            // 
            // accessoriesToolStripMenuItem
            // 
            this.accessoriesToolStripMenuItem.Name = "accessoriesToolStripMenuItem";
            this.accessoriesToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.accessoriesToolStripMenuItem.Text = "Accessories";
            this.accessoriesToolStripMenuItem.Click += new System.EventHandler(this.accessoriesToolStripMenuItem_Click);
            // 
            // materiaToolStripMenuItem
            // 
            this.materiaToolStripMenuItem.Name = "materiaToolStripMenuItem";
            this.materiaToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.materiaToolStripMenuItem.Text = "Materia";
            this.materiaToolStripMenuItem.Click += new System.EventHandler(this.materiaToolStripMenuItem_Click);
            // 
            // characterInitialisationDataToolStripMenuItem
            // 
            this.characterInitialisationDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.characterStatsToolStripMenuItem,
            this.limitBreaksToolStripMenuItem,
            this.startingEquipmentToolStripMenuItem,
            this.rNGTableToolStripMenuItem});
            this.characterInitialisationDataToolStripMenuItem.Name = "characterInitialisationDataToolStripMenuItem";
            this.characterInitialisationDataToolStripMenuItem.Size = new System.Drawing.Size(166, 20);
            this.characterInitialisationDataToolStripMenuItem.Text = "Character/Initialisation Data";
            this.characterInitialisationDataToolStripMenuItem.Visible = false;
            // 
            // characterStatsToolStripMenuItem
            // 
            this.characterStatsToolStripMenuItem.Name = "characterStatsToolStripMenuItem";
            this.characterStatsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.characterStatsToolStripMenuItem.Text = "Character Stats";
            this.characterStatsToolStripMenuItem.Click += new System.EventHandler(this.characterStatsToolStripMenuItem_Click);
            // 
            // limitBreaksToolStripMenuItem
            // 
            this.limitBreaksToolStripMenuItem.Name = "limitBreaksToolStripMenuItem";
            this.limitBreaksToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.limitBreaksToolStripMenuItem.Text = "Limit Breaks";
            this.limitBreaksToolStripMenuItem.Click += new System.EventHandler(this.limitBreaksToolStripMenuItem_Click);
            // 
            // startingEquipmentToolStripMenuItem
            // 
            this.startingEquipmentToolStripMenuItem.Name = "startingEquipmentToolStripMenuItem";
            this.startingEquipmentToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.startingEquipmentToolStripMenuItem.Text = "Starting Equipment";
            this.startingEquipmentToolStripMenuItem.Click += new System.EventHandler(this.startingEquipmentToolStripMenuItem_Click_1);
            // 
            // rNGTableToolStripMenuItem
            // 
            this.rNGTableToolStripMenuItem.Name = "rNGTableToolStripMenuItem";
            this.rNGTableToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.rNGTableToolStripMenuItem.Text = "RNG Table";
            // 
            // enemyFormationDataToolStripMenuItem
            // 
            this.enemyFormationDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelSwapsToolStripMenuItem,
            this.enemyStatsToolStripMenuItem,
            this.enemyAttacksToolStripMenuItem,
            this.enemyItemsToolStripMenuItem,
            this.formationsToolStripMenuItem});
            this.enemyFormationDataToolStripMenuItem.Name = "enemyFormationDataToolStripMenuItem";
            this.enemyFormationDataToolStripMenuItem.Size = new System.Drawing.Size(142, 20);
            this.enemyFormationDataToolStripMenuItem.Text = "Enemy/Formation Data";
            this.enemyFormationDataToolStripMenuItem.Visible = false;
            // 
            // modelSwapsToolStripMenuItem
            // 
            this.modelSwapsToolStripMenuItem.Name = "modelSwapsToolStripMenuItem";
            this.modelSwapsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.modelSwapsToolStripMenuItem.Text = "Model Swaps";
            this.modelSwapsToolStripMenuItem.Click += new System.EventHandler(this.modelSwapsToolStripMenuItem_Click);
            // 
            // enemyStatsToolStripMenuItem
            // 
            this.enemyStatsToolStripMenuItem.Name = "enemyStatsToolStripMenuItem";
            this.enemyStatsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.enemyStatsToolStripMenuItem.Text = "Enemy Data";
            this.enemyStatsToolStripMenuItem.Click += new System.EventHandler(this.enemyStatsToolStripMenuItem_Click);
            // 
            // enemyAttacksToolStripMenuItem
            // 
            this.enemyAttacksToolStripMenuItem.Name = "enemyAttacksToolStripMenuItem";
            this.enemyAttacksToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.enemyAttacksToolStripMenuItem.Text = "Enemy Attacks";
            this.enemyAttacksToolStripMenuItem.Click += new System.EventHandler(this.enemyAttacksToolStripMenuItem_Click);
            // 
            // enemyItemsToolStripMenuItem
            // 
            this.enemyItemsToolStripMenuItem.Name = "enemyItemsToolStripMenuItem";
            this.enemyItemsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.enemyItemsToolStripMenuItem.Text = "Enemy Items";
            this.enemyItemsToolStripMenuItem.Click += new System.EventHandler(this.enemyItemsToolStripMenuItem_Click);
            // 
            // formationsToolStripMenuItem
            // 
            this.formationsToolStripMenuItem.Name = "formationsToolStripMenuItem";
            this.formationsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.formationsToolStripMenuItem.Text = "Formations";
            this.formationsToolStripMenuItem.Click += new System.EventHandler(this.formationsToolStripMenuItem_Click);
            // 
            // balanceAutoTuningToolStripMenuItem
            // 
            this.balanceAutoTuningToolStripMenuItem.Name = "balanceAutoTuningToolStripMenuItem";
            this.balanceAutoTuningToolStripMenuItem.Size = new System.Drawing.Size(131, 20);
            this.balanceAutoTuningToolStripMenuItem.Text = "Balance Auto-Tuning";
            this.balanceAutoTuningToolStripMenuItem.Visible = false;
            this.balanceAutoTuningToolStripMenuItem.Click += new System.EventHandler(this.balanceAutoTuningToolStripMenuItem_Click);
            // 
            // restrictionRulesToolStripMenuItem
            // 
            this.restrictionRulesToolStripMenuItem.Name = "restrictionRulesToolStripMenuItem";
            this.restrictionRulesToolStripMenuItem.Size = new System.Drawing.Size(136, 20);
            this.restrictionRulesToolStripMenuItem.Text = "Challenge Restrictions";
            this.restrictionRulesToolStripMenuItem.Visible = false;
            this.restrictionRulesToolStripMenuItem.Click += new System.EventHandler(this.restrictionRulesToolStripMenuItem_Click);
            // 
            // specialHacksToolStripMenuItem
            // 
            this.specialHacksToolStripMenuItem.Name = "specialHacksToolStripMenuItem";
            this.specialHacksToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.specialHacksToolStripMenuItem.Text = "Special Hacks";
            this.specialHacksToolStripMenuItem.Visible = false;
            this.specialHacksToolStripMenuItem.Click += new System.EventHandler(this.specialHacksToolStripMenuItem_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.chkEnglish);
            this.flowLayoutPanel3.Controls.Add(this.chkFrench);
            this.flowLayoutPanel3.Controls.Add(this.chkGerman);
            this.flowLayoutPanel3.Controls.Add(this.chkSpanish);
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(11, 37);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(115, 94);
            this.flowLayoutPanel3.TabIndex = 32;
            // 
            // chkEnglish
            // 
            this.chkEnglish.AutoSize = true;
            this.chkEnglish.Checked = true;
            this.chkEnglish.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnglish.Location = new System.Drawing.Point(3, 3);
            this.chkEnglish.Name = "chkEnglish";
            this.chkEnglish.Size = new System.Drawing.Size(60, 17);
            this.chkEnglish.TabIndex = 0;
            this.chkEnglish.Text = "English";
            this.chkEnglish.UseVisualStyleBackColor = true;
            this.chkEnglish.CheckedChanged += new System.EventHandler(this.chkEnglish_CheckedChanged);
            // 
            // chkFrench
            // 
            this.chkFrench.AutoSize = true;
            this.chkFrench.Location = new System.Drawing.Point(3, 26);
            this.chkFrench.Name = "chkFrench";
            this.chkFrench.Size = new System.Drawing.Size(104, 17);
            this.chkFrench.TabIndex = 1;
            this.chkFrench.Text = "French/Français";
            this.chkFrench.UseVisualStyleBackColor = true;
            this.chkFrench.CheckedChanged += new System.EventHandler(this.chkFrench_CheckedChanged);
            // 
            // chkGerman
            // 
            this.chkGerman.AutoSize = true;
            this.chkGerman.Location = new System.Drawing.Point(3, 49);
            this.chkGerman.Name = "chkGerman";
            this.chkGerman.Size = new System.Drawing.Size(108, 17);
            this.chkGerman.TabIndex = 2;
            this.chkGerman.Text = "German/Deutsch";
            this.chkGerman.UseVisualStyleBackColor = true;
            this.chkGerman.CheckedChanged += new System.EventHandler(this.chkGerman_CheckedChanged);
            // 
            // chkSpanish
            // 
            this.chkSpanish.AutoSize = true;
            this.chkSpanish.Location = new System.Drawing.Point(3, 72);
            this.chkSpanish.Name = "chkSpanish";
            this.chkSpanish.Size = new System.Drawing.Size(107, 17);
            this.chkSpanish.TabIndex = 3;
            this.chkSpanish.Text = "Spanish/Español";
            this.chkSpanish.UseVisualStyleBackColor = true;
            this.chkSpanish.CheckedChanged += new System.EventHandler(this.chkSpanish_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "Language";
            this.label7.Visible = false;
            // 
            // btnMiscFileDecompress
            // 
            this.btnMiscFileDecompress.Location = new System.Drawing.Point(121, 184);
            this.btnMiscFileDecompress.Name = "btnMiscFileDecompress";
            this.btnMiscFileDecompress.Size = new System.Drawing.Size(122, 23);
            this.btnMiscFileDecompress.TabIndex = 33;
            this.btnMiscFileDecompress.Text = "MiscFile Decompress";
            this.btnMiscFileDecompress.UseVisualStyleBackColor = true;
            this.btnMiscFileDecompress.Visible = false;
            this.btnMiscFileDecompress.Click += new System.EventHandler(this.btnMiscFileDecompress_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(817, 231);
            this.Controls.Add(this.btnMiscFileDecompress);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.flowLayoutPanel4);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSeed);
            this.Controls.Add(this.btnRandoScene);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Godo: FF7 Randomiser/Tuner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRandoScene;
        private System.Windows.Forms.TextBox txtSeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox chkCharacterStats;
        private System.Windows.Forms.CheckBox chkStartingMateria;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox chkWeaponData;
        private System.Windows.Forms.CheckBox chkArmourData;
        private System.Windows.Forms.CheckBox chkAccessoryData;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.CheckBox chkEnemyItems;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkEnemyStats;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem spellsEquipmentDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spellsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem summonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enemySkillsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem weaponsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem armourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accessoriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem materiaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem characterInitialisationDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem characterStatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem limitBreaksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startingEquipmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rNGTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enemyFormationDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelSwapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enemyStatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enemyAttacksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem balanceAutoTuningToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restrictionRulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem specialHacksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem attackItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem healItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enemyItemsToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.CheckBox chkEnglish;
        private System.Windows.Forms.CheckBox chkFrench;
        private System.Windows.Forms.CheckBox chkGerman;
        private System.Windows.Forms.CheckBox chkSpanish;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnMiscFileDecompress;
    }
}

