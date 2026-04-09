namespace Godo.FormsEnemyData
{
    partial class ModelSwap
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
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkSafeSwap = new System.Windows.Forms.CheckBox();
            this.chkRiskySwap = new System.Windows.Forms.CheckBox();
            this.chkCrashSwap = new System.Windows.Forms.CheckBox();
            this.chkBossSwap = new System.Windows.Forms.CheckBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Options";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chkSafeSwap);
            this.flowLayoutPanel2.Controls.Add(this.chkRiskySwap);
            this.flowLayoutPanel2.Controls.Add(this.chkCrashSwap);
            this.flowLayoutPanel2.Controls.Add(this.chkBossSwap);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 23);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(196, 100);
            this.flowLayoutPanel2.TabIndex = 28;
            // 
            // chkSafeSwap
            // 
            this.chkSafeSwap.AutoSize = true;
            this.chkSafeSwap.Location = new System.Drawing.Point(3, 3);
            this.chkSafeSwap.Name = "chkSafeSwap";
            this.chkSafeSwap.Size = new System.Drawing.Size(127, 17);
            this.chkSafeSwap.TabIndex = 0;
            this.chkSafeSwap.Text = "Enemy Swap (Safest)";
            this.chkSafeSwap.UseVisualStyleBackColor = true;
            // 
            // chkRiskySwap
            // 
            this.chkRiskySwap.AutoSize = true;
            this.chkRiskySwap.Location = new System.Drawing.Point(3, 26);
            this.chkRiskySwap.Name = "chkRiskySwap";
            this.chkRiskySwap.Size = new System.Drawing.Size(123, 17);
            this.chkRiskySwap.TabIndex = 4;
            this.chkRiskySwap.Text = "Enemy Swap (Risky)";
            this.chkRiskySwap.UseVisualStyleBackColor = true;
            this.chkRiskySwap.Visible = false;
            // 
            // chkCrashSwap
            // 
            this.chkCrashSwap.AutoSize = true;
            this.chkCrashSwap.Location = new System.Drawing.Point(3, 49);
            this.chkCrashSwap.Name = "chkCrashSwap";
            this.chkCrashSwap.Size = new System.Drawing.Size(134, 17);
            this.chkCrashSwap.TabIndex = 6;
            this.chkCrashSwap.Text = "Enemy Swap (Riskiest)";
            this.chkCrashSwap.UseVisualStyleBackColor = true;
            this.chkCrashSwap.Visible = false;
            // 
            // chkBossSwap
            // 
            this.chkBossSwap.AutoSize = true;
            this.chkBossSwap.Location = new System.Drawing.Point(3, 72);
            this.chkBossSwap.Name = "chkBossSwap";
            this.chkBossSwap.Size = new System.Drawing.Size(115, 17);
            this.chkBossSwap.TabIndex = 1;
            this.chkBossSwap.Text = "Enable Boss Swap";
            this.chkBossSwap.UseVisualStyleBackColor = true;
            this.chkBossSwap.Visible = false;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(12, 129);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 27;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click_1);
            // 
            // ModelSwap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 166);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.btnConfirm);
            this.Name = "ModelSwap";
            this.Text = "ModelSwap";
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox chkSafeSwap;
        private System.Windows.Forms.CheckBox chkRiskySwap;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.CheckBox chkBossSwap;
        private System.Windows.Forms.CheckBox chkCrashSwap;
    }
}