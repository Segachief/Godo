namespace Godo.FormsEquipmentData
{
    partial class Weapons
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numBasePower = new System.Windows.Forms.NumericUpDown();
            this.numAccuracy = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkAccuracy = new System.Windows.Forms.CheckBox();
            this.chkBasePower = new System.Windows.Forms.CheckBox();
            this.chkCriticalHit = new System.Windows.Forms.CheckBox();
            this.chkStatA = new System.Windows.Forms.CheckBox();
            this.chkStatB = new System.Windows.Forms.CheckBox();
            this.chkStatC = new System.Windows.Forms.CheckBox();
            this.chkStatD = new System.Windows.Forms.CheckBox();
            this.chkSlots = new System.Windows.Forms.CheckBox();
            this.chkGrowth = new System.Windows.Forms.CheckBox();
            this.chkEquip = new System.Windows.Forms.CheckBox();
            this.chkElement = new System.Windows.Forms.CheckBox();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.chkDamageFormula = new System.Windows.Forms.CheckBox();
            this.chkProperties = new System.Windows.Forms.CheckBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.numCritical = new System.Windows.Forms.NumericUpDown();
            this.numStatA = new System.Windows.Forms.NumericUpDown();
            this.numStatB = new System.Windows.Forms.NumericUpDown();
            this.numStatC = new System.Windows.Forms.NumericUpDown();
            this.numStatD = new System.Windows.Forms.NumericUpDown();
            this.numSlots = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numBasePower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAccuracy)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCritical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStatA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStatB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStatC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStatD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSlots)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(132, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Stat Range";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Options";
            // 
            // numBasePower
            // 
            this.numBasePower.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBasePower.Location = new System.Drawing.Point(135, 44);
            this.numBasePower.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBasePower.Name = "numBasePower";
            this.numBasePower.Size = new System.Drawing.Size(49, 20);
            this.numBasePower.TabIndex = 23;
            this.numBasePower.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // numAccuracy
            // 
            this.numAccuracy.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numAccuracy.Location = new System.Drawing.Point(135, 21);
            this.numAccuracy.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numAccuracy.Name = "numAccuracy";
            this.numAccuracy.Size = new System.Drawing.Size(49, 20);
            this.numAccuracy.TabIndex = 21;
            this.numAccuracy.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chkCriticalHit);
            this.flowLayoutPanel2.Controls.Add(this.chkStatA);
            this.flowLayoutPanel2.Controls.Add(this.chkStatB);
            this.flowLayoutPanel2.Controls.Add(this.chkStatC);
            this.flowLayoutPanel2.Controls.Add(this.chkStatD);
            this.flowLayoutPanel2.Controls.Add(this.chkSlots);
            this.flowLayoutPanel2.Controls.Add(this.chkGrowth);
            this.flowLayoutPanel2.Controls.Add(this.chkEquip);
            this.flowLayoutPanel2.Controls.Add(this.chkElement);
            this.flowLayoutPanel2.Controls.Add(this.chkStatus);
            this.flowLayoutPanel2.Controls.Add(this.chkDamageFormula);
            this.flowLayoutPanel2.Controls.Add(this.chkProperties);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 89);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(119, 278);
            this.flowLayoutPanel2.TabIndex = 20;
            // 
            // chkAccuracy
            // 
            this.chkAccuracy.AutoSize = true;
            this.chkAccuracy.Location = new System.Drawing.Point(12, 21);
            this.chkAccuracy.Name = "chkAccuracy";
            this.chkAccuracy.Size = new System.Drawing.Size(82, 17);
            this.chkAccuracy.TabIndex = 0;
            this.chkAccuracy.Text = "Accuracy %";
            this.chkAccuracy.UseVisualStyleBackColor = true;
            // 
            // chkBasePower
            // 
            this.chkBasePower.AutoSize = true;
            this.chkBasePower.Location = new System.Drawing.Point(11, 44);
            this.chkBasePower.Name = "chkBasePower";
            this.chkBasePower.Size = new System.Drawing.Size(83, 17);
            this.chkBasePower.TabIndex = 4;
            this.chkBasePower.Text = "Base Power";
            this.chkBasePower.UseVisualStyleBackColor = true;
            // 
            // chkCriticalHit
            // 
            this.chkCriticalHit.AutoSize = true;
            this.chkCriticalHit.Location = new System.Drawing.Point(3, 3);
            this.chkCriticalHit.Name = "chkCriticalHit";
            this.chkCriticalHit.Size = new System.Drawing.Size(81, 17);
            this.chkCriticalHit.TabIndex = 1;
            this.chkCriticalHit.Text = "Critical Hit%";
            this.chkCriticalHit.UseVisualStyleBackColor = true;
            // 
            // chkStatA
            // 
            this.chkStatA.AutoSize = true;
            this.chkStatA.Location = new System.Drawing.Point(3, 26);
            this.chkStatA.Name = "chkStatA";
            this.chkStatA.Size = new System.Drawing.Size(88, 17);
            this.chkStatA.TabIndex = 2;
            this.chkStatA.Text = "Stat Bonus A";
            this.chkStatA.UseVisualStyleBackColor = true;
            // 
            // chkStatB
            // 
            this.chkStatB.AutoSize = true;
            this.chkStatB.Location = new System.Drawing.Point(3, 49);
            this.chkStatB.Name = "chkStatB";
            this.chkStatB.Size = new System.Drawing.Size(88, 17);
            this.chkStatB.TabIndex = 5;
            this.chkStatB.Text = "Stat Bonus B";
            this.chkStatB.UseVisualStyleBackColor = true;
            // 
            // chkStatC
            // 
            this.chkStatC.AutoSize = true;
            this.chkStatC.Location = new System.Drawing.Point(3, 72);
            this.chkStatC.Name = "chkStatC";
            this.chkStatC.Size = new System.Drawing.Size(88, 17);
            this.chkStatC.TabIndex = 6;
            this.chkStatC.Text = "Stat Bonus C";
            this.chkStatC.UseVisualStyleBackColor = true;
            // 
            // chkStatD
            // 
            this.chkStatD.AutoSize = true;
            this.chkStatD.Location = new System.Drawing.Point(3, 95);
            this.chkStatD.Name = "chkStatD";
            this.chkStatD.Size = new System.Drawing.Size(89, 17);
            this.chkStatD.TabIndex = 7;
            this.chkStatD.Text = "Stat Bonus D";
            this.chkStatD.UseVisualStyleBackColor = true;
            // 
            // chkSlots
            // 
            this.chkSlots.AutoSize = true;
            this.chkSlots.Location = new System.Drawing.Point(3, 118);
            this.chkSlots.Name = "chkSlots";
            this.chkSlots.Size = new System.Drawing.Size(87, 17);
            this.chkSlots.TabIndex = 10;
            this.chkSlots.Text = "Materia Slots";
            this.chkSlots.UseVisualStyleBackColor = true;
            // 
            // chkGrowth
            // 
            this.chkGrowth.AutoSize = true;
            this.chkGrowth.Location = new System.Drawing.Point(3, 141);
            this.chkGrowth.Name = "chkGrowth";
            this.chkGrowth.Size = new System.Drawing.Size(98, 17);
            this.chkGrowth.TabIndex = 9;
            this.chkGrowth.Text = "Materia Growth";
            this.chkGrowth.UseVisualStyleBackColor = true;
            // 
            // chkEquip
            // 
            this.chkEquip.AutoSize = true;
            this.chkEquip.Location = new System.Drawing.Point(3, 164);
            this.chkEquip.Name = "chkEquip";
            this.chkEquip.Size = new System.Drawing.Size(94, 17);
            this.chkEquip.TabIndex = 8;
            this.chkEquip.Text = "Equippable By";
            this.chkEquip.UseVisualStyleBackColor = true;
            // 
            // chkElement
            // 
            this.chkElement.AutoSize = true;
            this.chkElement.Location = new System.Drawing.Point(3, 187);
            this.chkElement.Name = "chkElement";
            this.chkElement.Size = new System.Drawing.Size(108, 17);
            this.chkElement.TabIndex = 11;
            this.chkElement.Text = "Weapon Element";
            this.chkElement.UseVisualStyleBackColor = true;
            // 
            // chkStatus
            // 
            this.chkStatus.AutoSize = true;
            this.chkStatus.Location = new System.Drawing.Point(3, 210);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(100, 17);
            this.chkStatus.TabIndex = 12;
            this.chkStatus.Text = "Weapon Status";
            this.chkStatus.UseVisualStyleBackColor = true;
            // 
            // chkDamageFormula
            // 
            this.chkDamageFormula.AutoSize = true;
            this.chkDamageFormula.Location = new System.Drawing.Point(3, 233);
            this.chkDamageFormula.Name = "chkDamageFormula";
            this.chkDamageFormula.Size = new System.Drawing.Size(106, 17);
            this.chkDamageFormula.TabIndex = 3;
            this.chkDamageFormula.Text = "Damage Formula";
            this.chkDamageFormula.UseVisualStyleBackColor = true;
            // 
            // chkProperties
            // 
            this.chkProperties.AutoSize = true;
            this.chkProperties.Location = new System.Drawing.Point(3, 256);
            this.chkProperties.Name = "chkProperties";
            this.chkProperties.Size = new System.Drawing.Size(111, 17);
            this.chkProperties.TabIndex = 13;
            this.chkProperties.Text = "Special Properties";
            this.chkProperties.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(12, 369);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 19;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click_1);
            // 
            // numCritical
            // 
            this.numCritical.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numCritical.Location = new System.Drawing.Point(135, 90);
            this.numCritical.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numCritical.Name = "numCritical";
            this.numCritical.Size = new System.Drawing.Size(49, 20);
            this.numCritical.TabIndex = 26;
            this.numCritical.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // numStatA
            // 
            this.numStatA.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numStatA.Location = new System.Drawing.Point(135, 112);
            this.numStatA.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numStatA.Name = "numStatA";
            this.numStatA.Size = new System.Drawing.Size(49, 20);
            this.numStatA.TabIndex = 27;
            this.numStatA.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // numStatB
            // 
            this.numStatB.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numStatB.Location = new System.Drawing.Point(135, 135);
            this.numStatB.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numStatB.Name = "numStatB";
            this.numStatB.Size = new System.Drawing.Size(49, 20);
            this.numStatB.TabIndex = 28;
            this.numStatB.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // numStatC
            // 
            this.numStatC.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numStatC.Location = new System.Drawing.Point(135, 158);
            this.numStatC.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numStatC.Name = "numStatC";
            this.numStatC.Size = new System.Drawing.Size(49, 20);
            this.numStatC.TabIndex = 29;
            this.numStatC.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // numStatD
            // 
            this.numStatD.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numStatD.Location = new System.Drawing.Point(135, 181);
            this.numStatD.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numStatD.Name = "numStatD";
            this.numStatD.Size = new System.Drawing.Size(49, 20);
            this.numStatD.TabIndex = 30;
            this.numStatD.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // numSlots
            // 
            this.numSlots.Location = new System.Drawing.Point(135, 203);
            this.numSlots.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numSlots.Name = "numSlots";
            this.numSlots.Size = new System.Drawing.Size(49, 20);
            this.numSlots.TabIndex = 31;
            this.numSlots.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(132, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "%-Modifier";
            // 
            // Weapons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 404);
            this.Controls.Add(this.chkBasePower);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkAccuracy);
            this.Controls.Add(this.numSlots);
            this.Controls.Add(this.numStatD);
            this.Controls.Add(this.numStatC);
            this.Controls.Add(this.numStatB);
            this.Controls.Add(this.numStatA);
            this.Controls.Add(this.numCritical);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numBasePower);
            this.Controls.Add(this.numAccuracy);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.btnConfirm);
            this.Name = "Weapons";
            this.Text = "Weapons";
            ((System.ComponentModel.ISupportInitialize)(this.numBasePower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAccuracy)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCritical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStatA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStatB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStatC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStatD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSlots)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numBasePower;
        private System.Windows.Forms.NumericUpDown numAccuracy;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox chkAccuracy;
        private System.Windows.Forms.CheckBox chkCriticalHit;
        private System.Windows.Forms.CheckBox chkBasePower;
        private System.Windows.Forms.CheckBox chkStatA;
        private System.Windows.Forms.CheckBox chkDamageFormula;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.CheckBox chkStatB;
        private System.Windows.Forms.CheckBox chkStatC;
        private System.Windows.Forms.CheckBox chkStatD;
        private System.Windows.Forms.CheckBox chkSlots;
        private System.Windows.Forms.CheckBox chkGrowth;
        private System.Windows.Forms.CheckBox chkEquip;
        private System.Windows.Forms.CheckBox chkElement;
        private System.Windows.Forms.CheckBox chkStatus;
        private System.Windows.Forms.CheckBox chkProperties;
        private System.Windows.Forms.NumericUpDown numCritical;
        private System.Windows.Forms.NumericUpDown numStatA;
        private System.Windows.Forms.NumericUpDown numStatB;
        private System.Windows.Forms.NumericUpDown numStatC;
        private System.Windows.Forms.NumericUpDown numStatD;
        private System.Windows.Forms.NumericUpDown numSlots;
        private System.Windows.Forms.Label label3;
    }
}