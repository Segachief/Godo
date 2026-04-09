namespace Godo.FormsItemData
{
    partial class HealItems
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
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkBasePower = new System.Windows.Forms.CheckBox();
            this.chkAnimation = new System.Windows.Forms.CheckBox();
            this.chkDamageFormula = new System.Windows.Forms.CheckBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numBasePower)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(121, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "%-Modifier";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Options";
            // 
            // numBasePower
            // 
            this.numBasePower.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBasePower.Location = new System.Drawing.Point(124, 27);
            this.numBasePower.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBasePower.Name = "numBasePower";
            this.numBasePower.Size = new System.Drawing.Size(49, 20);
            this.numBasePower.TabIndex = 36;
            this.numBasePower.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chkBasePower);
            this.flowLayoutPanel2.Controls.Add(this.chkAnimation);
            this.flowLayoutPanel2.Controls.Add(this.chkDamageFormula);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 24);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(111, 74);
            this.flowLayoutPanel2.TabIndex = 35;
            // 
            // chkBasePower
            // 
            this.chkBasePower.AutoSize = true;
            this.chkBasePower.Location = new System.Drawing.Point(3, 3);
            this.chkBasePower.Name = "chkBasePower";
            this.chkBasePower.Size = new System.Drawing.Size(89, 17);
            this.chkBasePower.TabIndex = 4;
            this.chkBasePower.Text = "Item Strength";
            this.chkBasePower.UseVisualStyleBackColor = true;
            // 
            // chkAnimation
            // 
            this.chkAnimation.AutoSize = true;
            this.chkAnimation.Location = new System.Drawing.Point(3, 26);
            this.chkAnimation.Name = "chkAnimation";
            this.chkAnimation.Size = new System.Drawing.Size(72, 17);
            this.chkAnimation.TabIndex = 2;
            this.chkAnimation.Text = "Animation";
            this.chkAnimation.UseVisualStyleBackColor = true;
            // 
            // chkDamageFormula
            // 
            this.chkDamageFormula.AutoSize = true;
            this.chkDamageFormula.Location = new System.Drawing.Point(3, 49);
            this.chkDamageFormula.Name = "chkDamageFormula";
            this.chkDamageFormula.Size = new System.Drawing.Size(106, 17);
            this.chkDamageFormula.TabIndex = 3;
            this.chkDamageFormula.Text = "Damage Formula";
            this.chkDamageFormula.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(12, 97);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 34;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click_1);
            // 
            // HealItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(198, 124);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numBasePower);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.btnConfirm);
            this.Name = "HealItems";
            this.Text = "HealItems";
            ((System.ComponentModel.ISupportInitialize)(this.numBasePower)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numBasePower;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox chkBasePower;
        private System.Windows.Forms.CheckBox chkAnimation;
        private System.Windows.Forms.CheckBox chkDamageFormula;
        private System.Windows.Forms.Button btnConfirm;
    }
}