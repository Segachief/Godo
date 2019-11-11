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
            this.btnOpen = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.btnRandoScene = new System.Windows.Forms.Button();
            this.txtSeed = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkStatCurves = new System.Windows.Forms.CheckBox();
            this.chkLimitIDs = new System.Windows.Forms.CheckBox();
            this.chkLimitKillUse = new System.Windows.Forms.CheckBox();
            this.chkLimitGauge = new System.Windows.Forms.CheckBox();
            this.chkLevelUpBonus = new System.Windows.Forms.CheckBox();
            this.chkStatCurveData = new System.Windows.Forms.CheckBox();
            this.chkCharacterAI = new System.Windows.Forms.CheckBox();
            this.chkRandomLookup = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkAttackData = new System.Windows.Forms.CheckBox();
            this.chkItemData = new System.Windows.Forms.CheckBox();
            this.chkWeaponData = new System.Windows.Forms.CheckBox();
            this.chkArmourData = new System.Windows.Forms.CheckBox();
            this.chkAccessoryData = new System.Windows.Forms.CheckBox();
            this.chkMateriaData = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkCharacterID = new System.Windows.Forms.CheckBox();
            this.chkCharacterStats = new System.Windows.Forms.CheckBox();
            this.chkCharacterName = new System.Windows.Forms.CheckBox();
            this.chkEquippedWeapon = new System.Windows.Forms.CheckBox();
            this.chkEquippedArmour = new System.Windows.Forms.CheckBox();
            this.chkEquippedAccessory = new System.Windows.Forms.CheckBox();
            this.chkCharacterHP = new System.Windows.Forms.CheckBox();
            this.chkCharacterMP = new System.Windows.Forms.CheckBox();
            this.chkEquippedMateria = new System.Windows.Forms.CheckBox();
            this.chkStartParty = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkEnemyModels = new System.Windows.Forms.CheckBox();
            this.chkBattleBG = new System.Windows.Forms.CheckBox();
            this.chkDisableEscape = new System.Windows.Forms.CheckBox();
            this.chkCamera = new System.Windows.Forms.CheckBox();
            this.chkEnemyPlacement = new System.Windows.Forms.CheckBox();
            this.chkEnemyQuantity = new System.Windows.Forms.CheckBox();
            this.chkEnemyName = new System.Windows.Forms.CheckBox();
            this.chkEnemyStats = new System.Windows.Forms.CheckBox();
            this.chkElementalAffinity = new System.Windows.Forms.CheckBox();
            this.chkHeldItems = new System.Windows.Forms.CheckBox();
            this.chkEnemyMP = new System.Windows.Forms.CheckBox();
            this.chkEnemyAP = new System.Windows.Forms.CheckBox();
            this.chkEnemyHP = new System.Windows.Forms.CheckBox();
            this.chkEnemyEXP = new System.Windows.Forms.CheckBox();
            this.chkEnemyGil = new System.Windows.Forms.CheckBox();
            this.chkStatusImmunities = new System.Windows.Forms.CheckBox();
            this.chkEnemyAttacks = new System.Windows.Forms.CheckBox();
            this.chkStatusSafe = new System.Windows.Forms.CheckBox();
            this.chkStatusUnsafe = new System.Windows.Forms.CheckBox();
            this.chkRandomElements = new System.Windows.Forms.CheckBox();
            this.chkAttackNames = new System.Windows.Forms.CheckBox();
            this.chkEnemyAI = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(34, 13);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(100, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open Directory";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFileName.Location = new System.Drawing.Point(143, 18);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(2, 15);
            this.lblFileName.TabIndex = 2;
            // 
            // btnRandoScene
            // 
            this.btnRandoScene.Location = new System.Drawing.Point(143, 62);
            this.btnRandoScene.Name = "btnRandoScene";
            this.btnRandoScene.Size = new System.Drawing.Size(100, 23);
            this.btnRandoScene.TabIndex = 3;
            this.btnRandoScene.Text = "Randomise";
            this.btnRandoScene.UseVisualStyleBackColor = true;
            this.btnRandoScene.Click += new System.EventHandler(this.BtnRandoScene_Click);
            // 
            // txtSeed
            // 
            this.txtSeed.Location = new System.Drawing.Point(33, 63);
            this.txtSeed.Name = "txtSeed";
            this.txtSeed.Size = new System.Drawing.Size(100, 20);
            this.txtSeed.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Seed";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.chkStatCurves);
            this.flowLayoutPanel1.Controls.Add(this.chkLimitIDs);
            this.flowLayoutPanel1.Controls.Add(this.chkLimitKillUse);
            this.flowLayoutPanel1.Controls.Add(this.chkLimitGauge);
            this.flowLayoutPanel1.Controls.Add(this.chkLevelUpBonus);
            this.flowLayoutPanel1.Controls.Add(this.chkStatCurveData);
            this.flowLayoutPanel1.Controls.Add(this.chkCharacterAI);
            this.flowLayoutPanel1.Controls.Add(this.chkRandomLookup);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(153, 168);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(143, 189);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // chkStatCurves
            // 
            this.chkStatCurves.AutoSize = true;
            this.chkStatCurves.Location = new System.Drawing.Point(3, 3);
            this.chkStatCurves.Name = "chkStatCurves";
            this.chkStatCurves.Size = new System.Drawing.Size(81, 17);
            this.chkStatCurves.TabIndex = 0;
            this.chkStatCurves.Text = "Stat Curves";
            this.chkStatCurves.UseVisualStyleBackColor = true;
            // 
            // chkLimitIDs
            // 
            this.chkLimitIDs.AutoSize = true;
            this.chkLimitIDs.Location = new System.Drawing.Point(3, 26);
            this.chkLimitIDs.Name = "chkLimitIDs";
            this.chkLimitIDs.Size = new System.Drawing.Size(66, 17);
            this.chkLimitIDs.TabIndex = 1;
            this.chkLimitIDs.Text = "Limit IDs";
            this.chkLimitIDs.UseVisualStyleBackColor = true;
            // 
            // chkLimitKillUse
            // 
            this.chkLimitKillUse.AutoSize = true;
            this.chkLimitKillUse.Location = new System.Drawing.Point(3, 49);
            this.chkLimitKillUse.Name = "chkLimitKillUse";
            this.chkLimitKillUse.Size = new System.Drawing.Size(97, 17);
            this.chkLimitKillUse.TabIndex = 2;
            this.chkLimitKillUse.Text = "Limit Kills/Uses";
            this.chkLimitKillUse.UseVisualStyleBackColor = true;
            // 
            // chkLimitGauge
            // 
            this.chkLimitGauge.AutoSize = true;
            this.chkLimitGauge.Location = new System.Drawing.Point(3, 72);
            this.chkLimitGauge.Name = "chkLimitGauge";
            this.chkLimitGauge.Size = new System.Drawing.Size(117, 17);
            this.chkLimitGauge.TabIndex = 3;
            this.chkLimitGauge.Text = "Limit Gauge Divisor";
            this.chkLimitGauge.UseVisualStyleBackColor = true;
            // 
            // chkLevelUpBonus
            // 
            this.chkLevelUpBonus.AutoSize = true;
            this.chkLevelUpBonus.Location = new System.Drawing.Point(3, 95);
            this.chkLevelUpBonus.Name = "chkLevelUpBonus";
            this.chkLevelUpBonus.Size = new System.Drawing.Size(113, 17);
            this.chkLevelUpBonus.TabIndex = 4;
            this.chkLevelUpBonus.Text = "Level Up Bonuses";
            this.chkLevelUpBonus.UseVisualStyleBackColor = true;
            // 
            // chkStatCurveData
            // 
            this.chkStatCurveData.AutoSize = true;
            this.chkStatCurveData.Location = new System.Drawing.Point(3, 118);
            this.chkStatCurveData.Name = "chkStatCurveData";
            this.chkStatCurveData.Size = new System.Drawing.Size(102, 17);
            this.chkStatCurveData.TabIndex = 5;
            this.chkStatCurveData.Text = "Stat Curve Data";
            this.chkStatCurveData.UseVisualStyleBackColor = true;
            // 
            // chkCharacterAI
            // 
            this.chkCharacterAI.AutoSize = true;
            this.chkCharacterAI.Location = new System.Drawing.Point(3, 141);
            this.chkCharacterAI.Name = "chkCharacterAI";
            this.chkCharacterAI.Size = new System.Drawing.Size(85, 17);
            this.chkCharacterAI.TabIndex = 6;
            this.chkCharacterAI.Text = "Character AI";
            this.chkCharacterAI.UseVisualStyleBackColor = true;
            // 
            // chkRandomLookup
            // 
            this.chkRandomLookup.AutoSize = true;
            this.chkRandomLookup.Location = new System.Drawing.Point(3, 164);
            this.chkRandomLookup.Name = "chkRandomLookup";
            this.chkRandomLookup.Size = new System.Drawing.Size(135, 17);
            this.chkRandomLookup.TabIndex = 7;
            this.chkRandomLookup.Text = "Random Lookup Table";
            this.chkRandomLookup.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chkAttackData);
            this.flowLayoutPanel2.Controls.Add(this.chkItemData);
            this.flowLayoutPanel2.Controls.Add(this.chkWeaponData);
            this.flowLayoutPanel2.Controls.Add(this.chkArmourData);
            this.flowLayoutPanel2.Controls.Add(this.chkAccessoryData);
            this.flowLayoutPanel2.Controls.Add(this.chkMateriaData);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(31, 168);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(105, 139);
            this.flowLayoutPanel2.TabIndex = 10;
            // 
            // chkAttackData
            // 
            this.chkAttackData.AutoSize = true;
            this.chkAttackData.Location = new System.Drawing.Point(3, 3);
            this.chkAttackData.Name = "chkAttackData";
            this.chkAttackData.Size = new System.Drawing.Size(83, 17);
            this.chkAttackData.TabIndex = 0;
            this.chkAttackData.Text = "Attack Data";
            this.chkAttackData.UseVisualStyleBackColor = true;
            // 
            // chkItemData
            // 
            this.chkItemData.AutoSize = true;
            this.chkItemData.Location = new System.Drawing.Point(3, 26);
            this.chkItemData.Name = "chkItemData";
            this.chkItemData.Size = new System.Drawing.Size(72, 17);
            this.chkItemData.TabIndex = 1;
            this.chkItemData.Text = "Item Data";
            this.chkItemData.UseVisualStyleBackColor = true;
            // 
            // chkWeaponData
            // 
            this.chkWeaponData.AutoSize = true;
            this.chkWeaponData.Location = new System.Drawing.Point(3, 49);
            this.chkWeaponData.Name = "chkWeaponData";
            this.chkWeaponData.Size = new System.Drawing.Size(93, 17);
            this.chkWeaponData.TabIndex = 2;
            this.chkWeaponData.Text = "Weapon Data";
            this.chkWeaponData.UseVisualStyleBackColor = true;
            // 
            // chkArmourData
            // 
            this.chkArmourData.AutoSize = true;
            this.chkArmourData.Location = new System.Drawing.Point(3, 72);
            this.chkArmourData.Name = "chkArmourData";
            this.chkArmourData.Size = new System.Drawing.Size(85, 17);
            this.chkArmourData.TabIndex = 3;
            this.chkArmourData.Text = "Armour Data";
            this.chkArmourData.UseVisualStyleBackColor = true;
            // 
            // chkAccessoryData
            // 
            this.chkAccessoryData.AutoSize = true;
            this.chkAccessoryData.Location = new System.Drawing.Point(3, 95);
            this.chkAccessoryData.Name = "chkAccessoryData";
            this.chkAccessoryData.Size = new System.Drawing.Size(101, 17);
            this.chkAccessoryData.TabIndex = 4;
            this.chkAccessoryData.Text = "Accessory Data";
            this.chkAccessoryData.UseVisualStyleBackColor = true;
            // 
            // chkMateriaData
            // 
            this.chkMateriaData.AutoSize = true;
            this.chkMateriaData.Location = new System.Drawing.Point(3, 118);
            this.chkMateriaData.Name = "chkMateriaData";
            this.chkMateriaData.Size = new System.Drawing.Size(87, 17);
            this.chkMateriaData.TabIndex = 5;
            this.chkMateriaData.Text = "Materia Data";
            this.chkMateriaData.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.chkCharacterID);
            this.flowLayoutPanel3.Controls.Add(this.chkCharacterStats);
            this.flowLayoutPanel3.Controls.Add(this.chkCharacterName);
            this.flowLayoutPanel3.Controls.Add(this.chkEquippedWeapon);
            this.flowLayoutPanel3.Controls.Add(this.chkEquippedArmour);
            this.flowLayoutPanel3.Controls.Add(this.chkEquippedAccessory);
            this.flowLayoutPanel3.Controls.Add(this.chkCharacterHP);
            this.flowLayoutPanel3.Controls.Add(this.chkCharacterMP);
            this.flowLayoutPanel3.Controls.Add(this.chkEquippedMateria);
            this.flowLayoutPanel3.Controls.Add(this.chkStartParty);
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(312, 167);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(128, 239);
            this.flowLayoutPanel3.TabIndex = 11;
            // 
            // chkCharacterID
            // 
            this.chkCharacterID.AutoSize = true;
            this.chkCharacterID.Location = new System.Drawing.Point(3, 3);
            this.chkCharacterID.Name = "chkCharacterID";
            this.chkCharacterID.Size = new System.Drawing.Size(91, 17);
            this.chkCharacterID.TabIndex = 0;
            this.chkCharacterID.Text = "Character IDs";
            this.chkCharacterID.UseVisualStyleBackColor = true;
            // 
            // chkCharacterStats
            // 
            this.chkCharacterStats.AutoSize = true;
            this.chkCharacterStats.Location = new System.Drawing.Point(3, 26);
            this.chkCharacterStats.Name = "chkCharacterStats";
            this.chkCharacterStats.Size = new System.Drawing.Size(89, 17);
            this.chkCharacterStats.TabIndex = 1;
            this.chkCharacterStats.Text = "Starting Stats";
            this.chkCharacterStats.UseVisualStyleBackColor = true;
            // 
            // chkCharacterName
            // 
            this.chkCharacterName.AutoSize = true;
            this.chkCharacterName.Location = new System.Drawing.Point(3, 49);
            this.chkCharacterName.Name = "chkCharacterName";
            this.chkCharacterName.Size = new System.Drawing.Size(103, 17);
            this.chkCharacterName.TabIndex = 2;
            this.chkCharacterName.Text = "Character Name";
            this.chkCharacterName.UseVisualStyleBackColor = true;
            // 
            // chkEquippedWeapon
            // 
            this.chkEquippedWeapon.AutoSize = true;
            this.chkEquippedWeapon.Location = new System.Drawing.Point(3, 72);
            this.chkEquippedWeapon.Name = "chkEquippedWeapon";
            this.chkEquippedWeapon.Size = new System.Drawing.Size(115, 17);
            this.chkEquippedWeapon.TabIndex = 3;
            this.chkEquippedWeapon.Text = "Equipped Weapon";
            this.chkEquippedWeapon.UseVisualStyleBackColor = true;
            // 
            // chkEquippedArmour
            // 
            this.chkEquippedArmour.AutoSize = true;
            this.chkEquippedArmour.Location = new System.Drawing.Point(3, 95);
            this.chkEquippedArmour.Name = "chkEquippedArmour";
            this.chkEquippedArmour.Size = new System.Drawing.Size(107, 17);
            this.chkEquippedArmour.TabIndex = 4;
            this.chkEquippedArmour.Text = "Equipped Armour";
            this.chkEquippedArmour.UseVisualStyleBackColor = true;
            // 
            // chkEquippedAccessory
            // 
            this.chkEquippedAccessory.AutoSize = true;
            this.chkEquippedAccessory.Location = new System.Drawing.Point(3, 118);
            this.chkEquippedAccessory.Name = "chkEquippedAccessory";
            this.chkEquippedAccessory.Size = new System.Drawing.Size(123, 17);
            this.chkEquippedAccessory.TabIndex = 5;
            this.chkEquippedAccessory.Text = "Equipped Accessory";
            this.chkEquippedAccessory.UseVisualStyleBackColor = true;
            // 
            // chkCharacterHP
            // 
            this.chkCharacterHP.AutoSize = true;
            this.chkCharacterHP.Location = new System.Drawing.Point(3, 141);
            this.chkCharacterHP.Name = "chkCharacterHP";
            this.chkCharacterHP.Size = new System.Drawing.Size(90, 17);
            this.chkCharacterHP.TabIndex = 6;
            this.chkCharacterHP.Text = "Character HP";
            this.chkCharacterHP.UseVisualStyleBackColor = true;
            // 
            // chkCharacterMP
            // 
            this.chkCharacterMP.AutoSize = true;
            this.chkCharacterMP.Location = new System.Drawing.Point(3, 164);
            this.chkCharacterMP.Name = "chkCharacterMP";
            this.chkCharacterMP.Size = new System.Drawing.Size(91, 17);
            this.chkCharacterMP.TabIndex = 7;
            this.chkCharacterMP.Text = "Character MP";
            this.chkCharacterMP.UseVisualStyleBackColor = true;
            // 
            // chkEquippedMateria
            // 
            this.chkEquippedMateria.AutoSize = true;
            this.chkEquippedMateria.Location = new System.Drawing.Point(3, 187);
            this.chkEquippedMateria.Name = "chkEquippedMateria";
            this.chkEquippedMateria.Size = new System.Drawing.Size(109, 17);
            this.chkEquippedMateria.TabIndex = 8;
            this.chkEquippedMateria.Text = "Equipped Materia";
            this.chkEquippedMateria.UseVisualStyleBackColor = true;
            // 
            // chkStartParty
            // 
            this.chkStartParty.AutoSize = true;
            this.chkStartParty.Location = new System.Drawing.Point(3, 210);
            this.chkStartParty.Name = "chkStartParty";
            this.chkStartParty.Size = new System.Drawing.Size(89, 17);
            this.chkStartParty.TabIndex = 9;
            this.chkStartParty.Text = "Starting Party";
            this.chkStartParty.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.chkEnemyModels);
            this.flowLayoutPanel4.Controls.Add(this.chkBattleBG);
            this.flowLayoutPanel4.Controls.Add(this.chkDisableEscape);
            this.flowLayoutPanel4.Controls.Add(this.chkCamera);
            this.flowLayoutPanel4.Controls.Add(this.chkEnemyPlacement);
            this.flowLayoutPanel4.Controls.Add(this.chkEnemyQuantity);
            this.flowLayoutPanel4.Controls.Add(this.chkEnemyName);
            this.flowLayoutPanel4.Controls.Add(this.chkEnemyStats);
            this.flowLayoutPanel4.Controls.Add(this.chkElementalAffinity);
            this.flowLayoutPanel4.Controls.Add(this.chkHeldItems);
            this.flowLayoutPanel4.Controls.Add(this.chkEnemyMP);
            this.flowLayoutPanel4.Controls.Add(this.chkEnemyAP);
            this.flowLayoutPanel4.Controls.Add(this.chkEnemyHP);
            this.flowLayoutPanel4.Controls.Add(this.chkEnemyEXP);
            this.flowLayoutPanel4.Controls.Add(this.chkEnemyGil);
            this.flowLayoutPanel4.Controls.Add(this.chkStatusImmunities);
            this.flowLayoutPanel4.Controls.Add(this.chkEnemyAttacks);
            this.flowLayoutPanel4.Controls.Add(this.chkStatusSafe);
            this.flowLayoutPanel4.Controls.Add(this.chkStatusUnsafe);
            this.flowLayoutPanel4.Controls.Add(this.chkRandomElements);
            this.flowLayoutPanel4.Controls.Add(this.chkAttackNames);
            this.flowLayoutPanel4.Controls.Add(this.chkEnemyAI);
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(454, 167);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(334, 271);
            this.flowLayoutPanel4.TabIndex = 12;
            // 
            // chkEnemyModels
            // 
            this.chkEnemyModels.AutoSize = true;
            this.chkEnemyModels.Location = new System.Drawing.Point(3, 3);
            this.chkEnemyModels.Name = "chkEnemyModels";
            this.chkEnemyModels.Size = new System.Drawing.Size(95, 17);
            this.chkEnemyModels.TabIndex = 0;
            this.chkEnemyModels.Text = "Enemy Models";
            this.chkEnemyModels.UseVisualStyleBackColor = true;
            // 
            // chkBattleBG
            // 
            this.chkBattleBG.AutoSize = true;
            this.chkBattleBG.Location = new System.Drawing.Point(3, 26);
            this.chkBattleBG.Name = "chkBattleBG";
            this.chkBattleBG.Size = new System.Drawing.Size(71, 17);
            this.chkBattleBG.TabIndex = 1;
            this.chkBattleBG.Text = "Battle BG";
            this.chkBattleBG.UseVisualStyleBackColor = true;
            // 
            // chkDisableEscape
            // 
            this.chkDisableEscape.AutoSize = true;
            this.chkDisableEscape.Location = new System.Drawing.Point(3, 49);
            this.chkDisableEscape.Name = "chkDisableEscape";
            this.chkDisableEscape.Size = new System.Drawing.Size(100, 17);
            this.chkDisableEscape.TabIndex = 2;
            this.chkDisableEscape.Text = "Disable Escape";
            this.chkDisableEscape.UseVisualStyleBackColor = true;
            // 
            // chkCamera
            // 
            this.chkCamera.AutoSize = true;
            this.chkCamera.Location = new System.Drawing.Point(3, 72);
            this.chkCamera.Name = "chkCamera";
            this.chkCamera.Size = new System.Drawing.Size(62, 17);
            this.chkCamera.TabIndex = 3;
            this.chkCamera.Text = "Camera";
            this.chkCamera.UseVisualStyleBackColor = true;
            // 
            // chkEnemyPlacement
            // 
            this.chkEnemyPlacement.AutoSize = true;
            this.chkEnemyPlacement.Location = new System.Drawing.Point(3, 95);
            this.chkEnemyPlacement.Name = "chkEnemyPlacement";
            this.chkEnemyPlacement.Size = new System.Drawing.Size(111, 17);
            this.chkEnemyPlacement.TabIndex = 4;
            this.chkEnemyPlacement.Text = "Enemy Placement";
            this.chkEnemyPlacement.UseVisualStyleBackColor = true;
            // 
            // chkEnemyQuantity
            // 
            this.chkEnemyQuantity.AutoSize = true;
            this.chkEnemyQuantity.Location = new System.Drawing.Point(3, 118);
            this.chkEnemyQuantity.Name = "chkEnemyQuantity";
            this.chkEnemyQuantity.Size = new System.Drawing.Size(100, 17);
            this.chkEnemyQuantity.TabIndex = 5;
            this.chkEnemyQuantity.Text = "Enemy Quantity";
            this.chkEnemyQuantity.UseVisualStyleBackColor = true;
            // 
            // chkEnemyName
            // 
            this.chkEnemyName.AutoSize = true;
            this.chkEnemyName.Location = new System.Drawing.Point(3, 141);
            this.chkEnemyName.Name = "chkEnemyName";
            this.chkEnemyName.Size = new System.Drawing.Size(89, 17);
            this.chkEnemyName.TabIndex = 6;
            this.chkEnemyName.Text = "Enemy Name";
            this.chkEnemyName.UseVisualStyleBackColor = true;
            // 
            // chkEnemyStats
            // 
            this.chkEnemyStats.AutoSize = true;
            this.chkEnemyStats.Location = new System.Drawing.Point(3, 164);
            this.chkEnemyStats.Name = "chkEnemyStats";
            this.chkEnemyStats.Size = new System.Drawing.Size(85, 17);
            this.chkEnemyStats.TabIndex = 7;
            this.chkEnemyStats.Text = "Enemy Stats";
            this.chkEnemyStats.UseVisualStyleBackColor = true;
            // 
            // chkElementalAffinity
            // 
            this.chkElementalAffinity.AutoSize = true;
            this.chkElementalAffinity.Location = new System.Drawing.Point(3, 187);
            this.chkElementalAffinity.Name = "chkElementalAffinity";
            this.chkElementalAffinity.Size = new System.Drawing.Size(106, 17);
            this.chkElementalAffinity.TabIndex = 8;
            this.chkElementalAffinity.Text = "Elemental Affinity";
            this.chkElementalAffinity.UseVisualStyleBackColor = true;
            // 
            // chkHeldItems
            // 
            this.chkHeldItems.AutoSize = true;
            this.chkHeldItems.Location = new System.Drawing.Point(3, 210);
            this.chkHeldItems.Name = "chkHeldItems";
            this.chkHeldItems.Size = new System.Drawing.Size(76, 17);
            this.chkHeldItems.TabIndex = 9;
            this.chkHeldItems.Text = "Held Items";
            this.chkHeldItems.UseVisualStyleBackColor = true;
            // 
            // chkEnemyMP
            // 
            this.chkEnemyMP.AutoSize = true;
            this.chkEnemyMP.Location = new System.Drawing.Point(3, 233);
            this.chkEnemyMP.Name = "chkEnemyMP";
            this.chkEnemyMP.Size = new System.Drawing.Size(77, 17);
            this.chkEnemyMP.TabIndex = 10;
            this.chkEnemyMP.Text = "Enemy MP";
            this.chkEnemyMP.UseVisualStyleBackColor = true;
            // 
            // chkEnemyAP
            // 
            this.chkEnemyAP.AutoSize = true;
            this.chkEnemyAP.Location = new System.Drawing.Point(120, 3);
            this.chkEnemyAP.Name = "chkEnemyAP";
            this.chkEnemyAP.Size = new System.Drawing.Size(75, 17);
            this.chkEnemyAP.TabIndex = 11;
            this.chkEnemyAP.Text = "Enemy AP";
            this.chkEnemyAP.UseVisualStyleBackColor = true;
            // 
            // chkEnemyHP
            // 
            this.chkEnemyHP.AutoSize = true;
            this.chkEnemyHP.Location = new System.Drawing.Point(120, 26);
            this.chkEnemyHP.Name = "chkEnemyHP";
            this.chkEnemyHP.Size = new System.Drawing.Size(76, 17);
            this.chkEnemyHP.TabIndex = 12;
            this.chkEnemyHP.Text = "Enemy HP";
            this.chkEnemyHP.UseVisualStyleBackColor = true;
            // 
            // chkEnemyEXP
            // 
            this.chkEnemyEXP.AutoSize = true;
            this.chkEnemyEXP.Location = new System.Drawing.Point(120, 49);
            this.chkEnemyEXP.Name = "chkEnemyEXP";
            this.chkEnemyEXP.Size = new System.Drawing.Size(82, 17);
            this.chkEnemyEXP.TabIndex = 13;
            this.chkEnemyEXP.Text = "Enemy EXP";
            this.chkEnemyEXP.UseVisualStyleBackColor = true;
            // 
            // chkEnemyGil
            // 
            this.chkEnemyGil.AutoSize = true;
            this.chkEnemyGil.Location = new System.Drawing.Point(120, 72);
            this.chkEnemyGil.Name = "chkEnemyGil";
            this.chkEnemyGil.Size = new System.Drawing.Size(73, 17);
            this.chkEnemyGil.TabIndex = 14;
            this.chkEnemyGil.Text = "Enemy Gil";
            this.chkEnemyGil.UseVisualStyleBackColor = true;
            // 
            // chkStatusImmunities
            // 
            this.chkStatusImmunities.AutoSize = true;
            this.chkStatusImmunities.Location = new System.Drawing.Point(120, 95);
            this.chkStatusImmunities.Name = "chkStatusImmunities";
            this.chkStatusImmunities.Size = new System.Drawing.Size(108, 17);
            this.chkStatusImmunities.TabIndex = 15;
            this.chkStatusImmunities.Text = "Status Immunities";
            this.chkStatusImmunities.UseVisualStyleBackColor = true;
            // 
            // chkEnemyAttacks
            // 
            this.chkEnemyAttacks.AutoSize = true;
            this.chkEnemyAttacks.Location = new System.Drawing.Point(120, 118);
            this.chkEnemyAttacks.Name = "chkEnemyAttacks";
            this.chkEnemyAttacks.Size = new System.Drawing.Size(97, 17);
            this.chkEnemyAttacks.TabIndex = 16;
            this.chkEnemyAttacks.Text = "Enemy Attacks";
            this.chkEnemyAttacks.UseVisualStyleBackColor = true;
            // 
            // chkStatusSafe
            // 
            this.chkStatusSafe.AutoSize = true;
            this.chkStatusSafe.Location = new System.Drawing.Point(120, 141);
            this.chkStatusSafe.Name = "chkStatusSafe";
            this.chkStatusSafe.Size = new System.Drawing.Size(196, 17);
            this.chkStatusSafe.TabIndex = 17;
            this.chkStatusSafe.Text = "Attacks w/ Random Statuses (Safe)";
            this.chkStatusSafe.UseVisualStyleBackColor = true;
            // 
            // chkStatusUnsafe
            // 
            this.chkStatusUnsafe.AutoSize = true;
            this.chkStatusUnsafe.Location = new System.Drawing.Point(120, 164);
            this.chkStatusUnsafe.Name = "chkStatusUnsafe";
            this.chkStatusUnsafe.Size = new System.Drawing.Size(208, 17);
            this.chkStatusUnsafe.TabIndex = 18;
            this.chkStatusUnsafe.Text = "Attacks w/ Random Statuses (Unsafe)";
            this.chkStatusUnsafe.UseVisualStyleBackColor = true;
            // 
            // chkRandomElements
            // 
            this.chkRandomElements.AutoSize = true;
            this.chkRandomElements.Location = new System.Drawing.Point(120, 187);
            this.chkRandomElements.Name = "chkRandomElements";
            this.chkRandomElements.Size = new System.Drawing.Size(167, 17);
            this.chkRandomElements.TabIndex = 21;
            this.chkRandomElements.Text = "Attacks w/ Random Elements";
            this.chkRandomElements.UseVisualStyleBackColor = true;
            // 
            // chkAttackNames
            // 
            this.chkAttackNames.AutoSize = true;
            this.chkAttackNames.Location = new System.Drawing.Point(120, 210);
            this.chkAttackNames.Name = "chkAttackNames";
            this.chkAttackNames.Size = new System.Drawing.Size(93, 17);
            this.chkAttackNames.TabIndex = 19;
            this.chkAttackNames.Text = "Attack Names";
            this.chkAttackNames.UseVisualStyleBackColor = true;
            // 
            // chkEnemyAI
            // 
            this.chkEnemyAI.AutoSize = true;
            this.chkEnemyAI.Location = new System.Drawing.Point(120, 233);
            this.chkEnemyAI.Name = "chkEnemyAI";
            this.chkEnemyAI.Size = new System.Drawing.Size(71, 17);
            this.chkEnemyAI.TabIndex = 20;
            this.chkEnemyAI.Text = "Enemy AI";
            this.chkEnemyAI.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Spells/Equipment";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Character Data";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(308, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Initialisation Data";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(451, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Enemy Data";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.flowLayoutPanel4);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSeed);
            this.Controls.Add(this.btnRandoScene);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.btnOpen);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Button btnRandoScene;
        private System.Windows.Forms.TextBox txtSeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox chkStatCurves;
        private System.Windows.Forms.CheckBox chkLimitIDs;
        private System.Windows.Forms.CheckBox chkLimitKillUse;
        private System.Windows.Forms.CheckBox chkLimitGauge;
        private System.Windows.Forms.CheckBox chkLevelUpBonus;
        private System.Windows.Forms.CheckBox chkStatCurveData;
        private System.Windows.Forms.CheckBox chkCharacterAI;
        private System.Windows.Forms.CheckBox chkRandomLookup;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox chkAttackData;
        private System.Windows.Forms.CheckBox chkItemData;
        private System.Windows.Forms.CheckBox chkWeaponData;
        private System.Windows.Forms.CheckBox chkArmourData;
        private System.Windows.Forms.CheckBox chkAccessoryData;
        private System.Windows.Forms.CheckBox chkMateriaData;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.CheckBox chkCharacterID;
        private System.Windows.Forms.CheckBox chkCharacterStats;
        private System.Windows.Forms.CheckBox chkCharacterName;
        private System.Windows.Forms.CheckBox chkEquippedWeapon;
        private System.Windows.Forms.CheckBox chkEquippedArmour;
        private System.Windows.Forms.CheckBox chkEquippedAccessory;
        private System.Windows.Forms.CheckBox chkCharacterHP;
        private System.Windows.Forms.CheckBox chkCharacterMP;
        private System.Windows.Forms.CheckBox chkEquippedMateria;
        private System.Windows.Forms.CheckBox chkStartParty;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.CheckBox chkEnemyModels;
        private System.Windows.Forms.CheckBox chkBattleBG;
        private System.Windows.Forms.CheckBox chkDisableEscape;
        private System.Windows.Forms.CheckBox chkCamera;
        private System.Windows.Forms.CheckBox chkEnemyPlacement;
        private System.Windows.Forms.CheckBox chkEnemyQuantity;
        private System.Windows.Forms.CheckBox chkEnemyName;
        private System.Windows.Forms.CheckBox chkEnemyStats;
        private System.Windows.Forms.CheckBox chkElementalAffinity;
        private System.Windows.Forms.CheckBox chkHeldItems;
        private System.Windows.Forms.CheckBox chkEnemyMP;
        private System.Windows.Forms.CheckBox chkEnemyAP;
        private System.Windows.Forms.CheckBox chkEnemyHP;
        private System.Windows.Forms.CheckBox chkEnemyEXP;
        private System.Windows.Forms.CheckBox chkEnemyGil;
        private System.Windows.Forms.CheckBox chkStatusImmunities;
        private System.Windows.Forms.CheckBox chkEnemyAttacks;
        private System.Windows.Forms.CheckBox chkStatusSafe;
        private System.Windows.Forms.CheckBox chkStatusUnsafe;
        private System.Windows.Forms.CheckBox chkRandomElements;
        private System.Windows.Forms.CheckBox chkAttackNames;
        private System.Windows.Forms.CheckBox chkEnemyAI;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

