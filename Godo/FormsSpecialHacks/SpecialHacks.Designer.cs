namespace Godo.FormsSpecialHacks
{
    partial class SpecialHacks
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
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkEnemyQuantity = new System.Windows.Forms.CheckBox();
            this.chkDisableEscape = new System.Windows.Forms.CheckBox();
            this.chkPovertyMode = new System.Windows.Forms.CheckBox();
            this.chkSpellspring = new System.Windows.Forms.CheckBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numSwarm = new System.Windows.Forms.NumericUpDown();
            this.chkBossSwarm = new System.Windows.Forms.CheckBox();
            this.numBossSwarm = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSwarm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBossSwarm)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.chkEnemyQuantity);
            this.flowLayoutPanel5.Controls.Add(this.chkDisableEscape);
            this.flowLayoutPanel5.Controls.Add(this.chkPovertyMode);
            this.flowLayoutPanel5.Controls.Add(this.chkSpellspring);
            this.flowLayoutPanel5.Controls.Add(this.chkBossSwarm);
            this.flowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(12, 25);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(108, 126);
            this.flowLayoutPanel5.TabIndex = 18;
            // 
            // chkEnemyQuantity
            // 
            this.chkEnemyQuantity.AutoSize = true;
            this.chkEnemyQuantity.Location = new System.Drawing.Point(3, 3);
            this.chkEnemyQuantity.Name = "chkEnemyQuantity";
            this.chkEnemyQuantity.Size = new System.Drawing.Size(93, 17);
            this.chkEnemyQuantity.TabIndex = 5;
            this.chkEnemyQuantity.Text = "Enemy Swarm";
            this.chkEnemyQuantity.UseVisualStyleBackColor = true;
            // 
            // chkDisableEscape
            // 
            this.chkDisableEscape.AutoSize = true;
            this.chkDisableEscape.Location = new System.Drawing.Point(3, 26);
            this.chkDisableEscape.Name = "chkDisableEscape";
            this.chkDisableEscape.Size = new System.Drawing.Size(100, 17);
            this.chkDisableEscape.TabIndex = 2;
            this.chkDisableEscape.Text = "Disable Escape";
            this.chkDisableEscape.UseVisualStyleBackColor = true;
            // 
            // chkPovertyMode
            // 
            this.chkPovertyMode.AutoSize = true;
            this.chkPovertyMode.Location = new System.Drawing.Point(3, 49);
            this.chkPovertyMode.Name = "chkPovertyMode";
            this.chkPovertyMode.Size = new System.Drawing.Size(92, 17);
            this.chkPovertyMode.TabIndex = 6;
            this.chkPovertyMode.Text = "Poverty Mode";
            this.chkPovertyMode.UseVisualStyleBackColor = true;
            // 
            // chkSpellspring
            // 
            this.chkSpellspring.AutoSize = true;
            this.chkSpellspring.Location = new System.Drawing.Point(3, 72);
            this.chkSpellspring.Name = "chkSpellspring";
            this.chkSpellspring.Size = new System.Drawing.Size(77, 17);
            this.chkSpellspring.TabIndex = 7;
            this.chkSpellspring.Text = "Spellspring";
            this.chkSpellspring.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(12, 157);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 20;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(119, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Quantity";
            // 
            // numSwarm
            // 
            this.numSwarm.Location = new System.Drawing.Point(122, 25);
            this.numSwarm.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numSwarm.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSwarm.Name = "numSwarm";
            this.numSwarm.Size = new System.Drawing.Size(49, 20);
            this.numSwarm.TabIndex = 26;
            this.numSwarm.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // chkBossSwarm
            // 
            this.chkBossSwarm.AutoSize = true;
            this.chkBossSwarm.Location = new System.Drawing.Point(3, 95);
            this.chkBossSwarm.Name = "chkBossSwarm";
            this.chkBossSwarm.Size = new System.Drawing.Size(84, 17);
            this.chkBossSwarm.TabIndex = 8;
            this.chkBossSwarm.Text = "Boss Swarm";
            this.chkBossSwarm.UseVisualStyleBackColor = true;
            // 
            // numBossSwarm
            // 
            this.numBossSwarm.Location = new System.Drawing.Point(122, 120);
            this.numBossSwarm.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numBossSwarm.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBossSwarm.Name = "numBossSwarm";
            this.numBossSwarm.Size = new System.Drawing.Size(49, 20);
            this.numBossSwarm.TabIndex = 28;
            this.numBossSwarm.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // SpecialHacks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(185, 235);
            this.Controls.Add(this.numBossSwarm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numSwarm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.flowLayoutPanel5);
            this.Name = "SpecialHacks";
            this.Text = "SpecialHacks";
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSwarm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBossSwarm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.CheckBox chkEnemyQuantity;
        private System.Windows.Forms.CheckBox chkDisableEscape;
        private System.Windows.Forms.CheckBox chkPovertyMode;
        private System.Windows.Forms.CheckBox chkSpellspring;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numSwarm;
        private System.Windows.Forms.CheckBox chkBossSwarm;
        private System.Windows.Forms.NumericUpDown numBossSwarm;
    }
}