namespace Godo.FormsBalancing
{
    partial class Balancing
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
            this.numWeaker = new System.Windows.Forms.NumericUpDown();
            this.numStronger = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkStronger = new System.Windows.Forms.CheckBox();
            this.chkWeaker = new System.Windows.Forms.CheckBox();
            this.chkMaxDrop = new System.Windows.Forms.CheckBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.chkMaxAP = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numWeaker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStronger)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(121, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "%-Modifier";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Options";
            // 
            // numWeaker
            // 
            this.numWeaker.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numWeaker.Location = new System.Drawing.Point(124, 49);
            this.numWeaker.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numWeaker.Name = "numWeaker";
            this.numWeaker.Size = new System.Drawing.Size(49, 20);
            this.numWeaker.TabIndex = 23;
            this.numWeaker.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // numStronger
            // 
            this.numStronger.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numStronger.Location = new System.Drawing.Point(124, 26);
            this.numStronger.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numStronger.Name = "numStronger";
            this.numStronger.Size = new System.Drawing.Size(49, 20);
            this.numStronger.TabIndex = 21;
            this.numStronger.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chkStronger);
            this.flowLayoutPanel2.Controls.Add(this.chkWeaker);
            this.flowLayoutPanel2.Controls.Add(this.chkMaxDrop);
            this.flowLayoutPanel2.Controls.Add(this.chkMaxAP);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 23);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(111, 94);
            this.flowLayoutPanel2.TabIndex = 20;
            // 
            // chkStronger
            // 
            this.chkStronger.AutoSize = true;
            this.chkStronger.Location = new System.Drawing.Point(3, 3);
            this.chkStronger.Name = "chkStronger";
            this.chkStronger.Size = new System.Drawing.Size(109, 17);
            this.chkStronger.TabIndex = 0;
            this.chkStronger.Text = "Stronger Enemies";
            this.chkStronger.UseVisualStyleBackColor = true;
            // 
            // chkWeaker
            // 
            this.chkWeaker.AutoSize = true;
            this.chkWeaker.Location = new System.Drawing.Point(3, 26);
            this.chkWeaker.Name = "chkWeaker";
            this.chkWeaker.Size = new System.Drawing.Size(107, 17);
            this.chkWeaker.TabIndex = 1;
            this.chkWeaker.Text = "Weaker Enemies";
            this.chkWeaker.UseVisualStyleBackColor = true;
            // 
            // chkMaxDrop
            // 
            this.chkMaxDrop.AutoSize = true;
            this.chkMaxDrop.Location = new System.Drawing.Point(3, 49);
            this.chkMaxDrop.Name = "chkMaxDrop";
            this.chkMaxDrop.Size = new System.Drawing.Size(101, 17);
            this.chkMaxDrop.TabIndex = 4;
            this.chkMaxDrop.Text = "Max Drop/Steal";
            this.chkMaxDrop.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(12, 123);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 19;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // chkMaxAP
            // 
            this.chkMaxAP.AutoSize = true;
            this.chkMaxAP.Location = new System.Drawing.Point(3, 72);
            this.chkMaxAP.Name = "chkMaxAP";
            this.chkMaxAP.Size = new System.Drawing.Size(63, 17);
            this.chkMaxAP.TabIndex = 5;
            this.chkMaxAP.Text = "Max AP";
            this.chkMaxAP.UseVisualStyleBackColor = true;
            // 
            // Balancing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(197, 154);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numWeaker);
            this.Controls.Add(this.numStronger);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.btnConfirm);
            this.Name = "Balancing";
            this.Text = "Balancing";
            ((System.ComponentModel.ISupportInitialize)(this.numWeaker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStronger)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numWeaker;
        private System.Windows.Forms.NumericUpDown numStronger;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox chkStronger;
        private System.Windows.Forms.CheckBox chkWeaker;
        private System.Windows.Forms.CheckBox chkMaxDrop;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.CheckBox chkMaxAP;
    }
}