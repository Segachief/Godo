namespace Godo
{
    partial class Spells
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
            this.btnConfirm = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkAccuracy = new System.Windows.Forms.CheckBox();
            this.chkMPCost = new System.Windows.Forms.CheckBox();
            this.chkBasePower = new System.Windows.Forms.CheckBox();
            this.chkAnimation = new System.Windows.Forms.CheckBox();
            this.chkDamageFormula = new System.Windows.Forms.CheckBox();
            this.numAccuracy = new System.Windows.Forms.NumericUpDown();
            this.numBasePower = new System.Windows.Forms.NumericUpDown();
            this.numMPCost = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAccuracy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBasePower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMPCost)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(6, 151);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chkAccuracy);
            this.flowLayoutPanel2.Controls.Add(this.chkMPCost);
            this.flowLayoutPanel2.Controls.Add(this.chkBasePower);
            this.flowLayoutPanel2.Controls.Add(this.chkAnimation);
            this.flowLayoutPanel2.Controls.Add(this.chkDamageFormula);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 33);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(111, 117);
            this.flowLayoutPanel2.TabIndex = 11;
            // 
            // chkAccuracy
            // 
            this.chkAccuracy.AutoSize = true;
            this.chkAccuracy.Location = new System.Drawing.Point(3, 3);
            this.chkAccuracy.Name = "chkAccuracy";
            this.chkAccuracy.Size = new System.Drawing.Size(82, 17);
            this.chkAccuracy.TabIndex = 0;
            this.chkAccuracy.Text = "Accuracy %";
            this.chkAccuracy.UseVisualStyleBackColor = true;
            // 
            // chkMPCost
            // 
            this.chkMPCost.AutoSize = true;
            this.chkMPCost.Location = new System.Drawing.Point(3, 26);
            this.chkMPCost.Name = "chkMPCost";
            this.chkMPCost.Size = new System.Drawing.Size(66, 17);
            this.chkMPCost.TabIndex = 1;
            this.chkMPCost.Text = "MP Cost";
            this.chkMPCost.UseVisualStyleBackColor = true;
            // 
            // chkBasePower
            // 
            this.chkBasePower.AutoSize = true;
            this.chkBasePower.Location = new System.Drawing.Point(3, 49);
            this.chkBasePower.Name = "chkBasePower";
            this.chkBasePower.Size = new System.Drawing.Size(83, 17);
            this.chkBasePower.TabIndex = 4;
            this.chkBasePower.Text = "Base Power";
            this.chkBasePower.UseVisualStyleBackColor = true;
            // 
            // chkAnimation
            // 
            this.chkAnimation.AutoSize = true;
            this.chkAnimation.Location = new System.Drawing.Point(3, 72);
            this.chkAnimation.Name = "chkAnimation";
            this.chkAnimation.Size = new System.Drawing.Size(72, 17);
            this.chkAnimation.TabIndex = 2;
            this.chkAnimation.Text = "Animation";
            this.chkAnimation.UseVisualStyleBackColor = true;
            // 
            // chkDamageFormula
            // 
            this.chkDamageFormula.AutoSize = true;
            this.chkDamageFormula.Location = new System.Drawing.Point(3, 95);
            this.chkDamageFormula.Name = "chkDamageFormula";
            this.chkDamageFormula.Size = new System.Drawing.Size(106, 17);
            this.chkDamageFormula.TabIndex = 3;
            this.chkDamageFormula.Text = "Damage Formula";
            this.chkDamageFormula.UseVisualStyleBackColor = true;
            // 
            // numAccuracy
            // 
            this.numAccuracy.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numAccuracy.Location = new System.Drawing.Point(124, 36);
            this.numAccuracy.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numAccuracy.Name = "numAccuracy";
            this.numAccuracy.Size = new System.Drawing.Size(49, 20);
            this.numAccuracy.TabIndex = 14;
            this.numAccuracy.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // numBasePower
            // 
            this.numBasePower.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBasePower.Location = new System.Drawing.Point(124, 82);
            this.numBasePower.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBasePower.Name = "numBasePower";
            this.numBasePower.Size = new System.Drawing.Size(49, 20);
            this.numBasePower.TabIndex = 15;
            this.numBasePower.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // numMPCost
            // 
            this.numMPCost.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numMPCost.Location = new System.Drawing.Point(124, 59);
            this.numMPCost.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numMPCost.Name = "numMPCost";
            this.numMPCost.Size = new System.Drawing.Size(49, 20);
            this.numMPCost.TabIndex = 16;
            this.numMPCost.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(121, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "%-Modifier";
            // 
            // Spells
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 194);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numMPCost);
            this.Controls.Add(this.numBasePower);
            this.Controls.Add(this.numAccuracy);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.btnConfirm);
            this.Name = "Spells";
            this.Text = "Spells";
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAccuracy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBasePower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMPCost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox chkAccuracy;
        private System.Windows.Forms.CheckBox chkMPCost;
        private System.Windows.Forms.CheckBox chkAnimation;
        private System.Windows.Forms.CheckBox chkDamageFormula;
        private System.Windows.Forms.CheckBox chkBasePower;
        private System.Windows.Forms.NumericUpDown numAccuracy;
        private System.Windows.Forms.NumericUpDown numBasePower;
        private System.Windows.Forms.NumericUpDown numMPCost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}