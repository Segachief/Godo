namespace Godo.FormsEquipmentData
{
    partial class Accessories
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
            this.numStatB = new System.Windows.Forms.NumericUpDown();
            this.numStatA = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkStatA = new System.Windows.Forms.CheckBox();
            this.chkStatB = new System.Windows.Forms.CheckBox();
            this.chkEquip = new System.Windows.Forms.CheckBox();
            this.chkElement = new System.Windows.Forms.CheckBox();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.chkSpecial = new System.Windows.Forms.CheckBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numStatB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStatA)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // numStatB
            // 
            this.numStatB.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numStatB.Location = new System.Drawing.Point(124, 47);
            this.numStatB.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numStatB.Name = "numStatB";
            this.numStatB.Size = new System.Drawing.Size(49, 20);
            this.numStatB.TabIndex = 53;
            this.numStatB.Value = new decimal(new int[] {
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
            this.numStatA.Location = new System.Drawing.Point(124, 24);
            this.numStatA.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numStatA.Name = "numStatA";
            this.numStatA.Size = new System.Drawing.Size(49, 20);
            this.numStatA.TabIndex = 52;
            this.numStatA.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(121, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Stat Range";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "Options";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chkStatA);
            this.flowLayoutPanel2.Controls.Add(this.chkStatB);
            this.flowLayoutPanel2.Controls.Add(this.chkEquip);
            this.flowLayoutPanel2.Controls.Add(this.chkElement);
            this.flowLayoutPanel2.Controls.Add(this.chkStatus);
            this.flowLayoutPanel2.Controls.Add(this.chkSpecial);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 23);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(111, 143);
            this.flowLayoutPanel2.TabIndex = 46;
            // 
            // chkStatA
            // 
            this.chkStatA.AutoSize = true;
            this.chkStatA.Location = new System.Drawing.Point(3, 3);
            this.chkStatA.Name = "chkStatA";
            this.chkStatA.Size = new System.Drawing.Size(88, 17);
            this.chkStatA.TabIndex = 2;
            this.chkStatA.Text = "Stat Bonus A";
            this.chkStatA.UseVisualStyleBackColor = true;
            // 
            // chkStatB
            // 
            this.chkStatB.AutoSize = true;
            this.chkStatB.Location = new System.Drawing.Point(3, 26);
            this.chkStatB.Name = "chkStatB";
            this.chkStatB.Size = new System.Drawing.Size(88, 17);
            this.chkStatB.TabIndex = 5;
            this.chkStatB.Text = "Stat Bonus B";
            this.chkStatB.UseVisualStyleBackColor = true;
            // 
            // chkEquip
            // 
            this.chkEquip.AutoSize = true;
            this.chkEquip.Location = new System.Drawing.Point(3, 49);
            this.chkEquip.Name = "chkEquip";
            this.chkEquip.Size = new System.Drawing.Size(94, 17);
            this.chkEquip.TabIndex = 8;
            this.chkEquip.Text = "Equippable By";
            this.chkEquip.UseVisualStyleBackColor = true;
            // 
            // chkElement
            // 
            this.chkElement.AutoSize = true;
            this.chkElement.Location = new System.Drawing.Point(3, 72);
            this.chkElement.Name = "chkElement";
            this.chkElement.Size = new System.Drawing.Size(116, 17);
            this.chkElement.TabIndex = 11;
            this.chkElement.Text = "Accessory Element";
            this.chkElement.UseVisualStyleBackColor = true;
            // 
            // chkStatus
            // 
            this.chkStatus.AutoSize = true;
            this.chkStatus.Location = new System.Drawing.Point(3, 95);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(108, 17);
            this.chkStatus.TabIndex = 12;
            this.chkStatus.Text = "Accessory Status";
            this.chkStatus.UseVisualStyleBackColor = true;
            // 
            // chkSpecial
            // 
            this.chkSpecial.AutoSize = true;
            this.chkSpecial.Location = new System.Drawing.Point(3, 118);
            this.chkSpecial.Name = "chkSpecial";
            this.chkSpecial.Size = new System.Drawing.Size(92, 17);
            this.chkSpecial.TabIndex = 13;
            this.chkSpecial.Text = "Special Effect";
            this.chkSpecial.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(12, 172);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 45;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click_1);
            // 
            // Accessories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(201, 207);
            this.Controls.Add(this.numStatB);
            this.Controls.Add(this.numStatA);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.btnConfirm);
            this.Name = "Accessories";
            this.Text = "Accessories";
            ((System.ComponentModel.ISupportInitialize)(this.numStatB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStatA)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numStatB;
        private System.Windows.Forms.NumericUpDown numStatA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox chkStatA;
        private System.Windows.Forms.CheckBox chkStatB;
        private System.Windows.Forms.CheckBox chkEquip;
        private System.Windows.Forms.CheckBox chkElement;
        private System.Windows.Forms.CheckBox chkStatus;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.CheckBox chkSpecial;
    }
}