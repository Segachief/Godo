namespace Godo.FormsInitialisationData
{
    partial class StartingEquipment
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
            this.numMateria = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkMateria = new System.Windows.Forms.CheckBox();
            this.chkWeapon = new System.Windows.Forms.CheckBox();
            this.chkArmour = new System.Windows.Forms.CheckBox();
            this.chkAccessory = new System.Windows.Forms.CheckBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.flowLayoutPanel10 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkCloud = new System.Windows.Forms.CheckBox();
            this.chkYuffie = new System.Windows.Forms.CheckBox();
            this.chkBarret = new System.Windows.Forms.CheckBox();
            this.chkYCloud = new System.Windows.Forms.CheckBox();
            this.chkTifa = new System.Windows.Forms.CheckBox();
            this.chkSeph = new System.Windows.Forms.CheckBox();
            this.chkAeristh = new System.Windows.Forms.CheckBox();
            this.chkCid = new System.Windows.Forms.CheckBox();
            this.chkRed = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numMateria)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // numMateria
            // 
            this.numMateria.Location = new System.Drawing.Point(129, 23);
            this.numMateria.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numMateria.Name = "numMateria";
            this.numMateria.Size = new System.Drawing.Size(49, 20);
            this.numMateria.TabIndex = 56;
            this.numMateria.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(125, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 54;
            this.label2.Text = "Quantity";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 53;
            this.label1.Text = "Equipment";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chkMateria);
            this.flowLayoutPanel2.Controls.Add(this.chkWeapon);
            this.flowLayoutPanel2.Controls.Add(this.chkArmour);
            this.flowLayoutPanel2.Controls.Add(this.chkAccessory);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 23);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(111, 101);
            this.flowLayoutPanel2.TabIndex = 50;
            // 
            // chkMateria
            // 
            this.chkMateria.AutoSize = true;
            this.chkMateria.Location = new System.Drawing.Point(3, 3);
            this.chkMateria.Name = "chkMateria";
            this.chkMateria.Size = new System.Drawing.Size(61, 17);
            this.chkMateria.TabIndex = 2;
            this.chkMateria.Text = "Materia";
            this.chkMateria.UseVisualStyleBackColor = true;
            // 
            // chkWeapon
            // 
            this.chkWeapon.AutoSize = true;
            this.chkWeapon.Location = new System.Drawing.Point(3, 26);
            this.chkWeapon.Name = "chkWeapon";
            this.chkWeapon.Size = new System.Drawing.Size(67, 17);
            this.chkWeapon.TabIndex = 0;
            this.chkWeapon.Text = "Weapon";
            this.chkWeapon.UseVisualStyleBackColor = true;
            // 
            // chkArmour
            // 
            this.chkArmour.AutoSize = true;
            this.chkArmour.Location = new System.Drawing.Point(3, 49);
            this.chkArmour.Name = "chkArmour";
            this.chkArmour.Size = new System.Drawing.Size(59, 17);
            this.chkArmour.TabIndex = 4;
            this.chkArmour.Text = "Armour";
            this.chkArmour.UseVisualStyleBackColor = true;
            // 
            // chkAccessory
            // 
            this.chkAccessory.AutoSize = true;
            this.chkAccessory.Location = new System.Drawing.Point(3, 72);
            this.chkAccessory.Name = "chkAccessory";
            this.chkAccessory.Size = new System.Drawing.Size(75, 17);
            this.chkAccessory.TabIndex = 1;
            this.chkAccessory.Text = "Accessory";
            this.chkAccessory.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(12, 184);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 49;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click_1);
            // 
            // flowLayoutPanel10
            // 
            this.flowLayoutPanel10.Controls.Add(this.chkCloud);
            this.flowLayoutPanel10.Controls.Add(this.chkYuffie);
            this.flowLayoutPanel10.Controls.Add(this.chkBarret);
            this.flowLayoutPanel10.Controls.Add(this.chkYCloud);
            this.flowLayoutPanel10.Controls.Add(this.chkTifa);
            this.flowLayoutPanel10.Controls.Add(this.chkSeph);
            this.flowLayoutPanel10.Controls.Add(this.chkAeristh);
            this.flowLayoutPanel10.Controls.Add(this.chkCid);
            this.flowLayoutPanel10.Controls.Add(this.chkRed);
            this.flowLayoutPanel10.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel10.Location = new System.Drawing.Point(12, 130);
            this.flowLayoutPanel10.Name = "flowLayoutPanel10";
            this.flowLayoutPanel10.Size = new System.Drawing.Size(340, 48);
            this.flowLayoutPanel10.TabIndex = 89;
            // 
            // chkCloud
            // 
            this.chkCloud.AutoSize = true;
            this.chkCloud.Location = new System.Drawing.Point(3, 3);
            this.chkCloud.Name = "chkCloud";
            this.chkCloud.Size = new System.Drawing.Size(53, 17);
            this.chkCloud.TabIndex = 0;
            this.chkCloud.Text = "Cloud";
            this.chkCloud.UseVisualStyleBackColor = true;
            // 
            // chkYuffie
            // 
            this.chkYuffie.AutoSize = true;
            this.chkYuffie.Location = new System.Drawing.Point(3, 26);
            this.chkYuffie.Name = "chkYuffie";
            this.chkYuffie.Size = new System.Drawing.Size(53, 17);
            this.chkYuffie.TabIndex = 4;
            this.chkYuffie.Text = "Yuffie";
            this.chkYuffie.UseVisualStyleBackColor = true;
            // 
            // chkBarret
            // 
            this.chkBarret.AutoSize = true;
            this.chkBarret.Location = new System.Drawing.Point(62, 3);
            this.chkBarret.Name = "chkBarret";
            this.chkBarret.Size = new System.Drawing.Size(54, 17);
            this.chkBarret.TabIndex = 1;
            this.chkBarret.Text = "Barret";
            this.chkBarret.UseVisualStyleBackColor = true;
            // 
            // chkYCloud
            // 
            this.chkYCloud.AutoSize = true;
            this.chkYCloud.Location = new System.Drawing.Point(62, 26);
            this.chkYCloud.Name = "chkYCloud";
            this.chkYCloud.Size = new System.Drawing.Size(63, 17);
            this.chkYCloud.TabIndex = 2;
            this.chkYCloud.Text = "Y.Cloud";
            this.chkYCloud.UseVisualStyleBackColor = true;
            // 
            // chkTifa
            // 
            this.chkTifa.AutoSize = true;
            this.chkTifa.Location = new System.Drawing.Point(131, 3);
            this.chkTifa.Name = "chkTifa";
            this.chkTifa.Size = new System.Drawing.Size(44, 17);
            this.chkTifa.TabIndex = 5;
            this.chkTifa.Text = "Tifa";
            this.chkTifa.UseVisualStyleBackColor = true;
            // 
            // chkSeph
            // 
            this.chkSeph.AutoSize = true;
            this.chkSeph.Location = new System.Drawing.Point(131, 26);
            this.chkSeph.Name = "chkSeph";
            this.chkSeph.Size = new System.Drawing.Size(71, 17);
            this.chkSeph.TabIndex = 6;
            this.chkSeph.Text = "Sephiroth";
            this.chkSeph.UseVisualStyleBackColor = true;
            // 
            // chkAeristh
            // 
            this.chkAeristh.AutoSize = true;
            this.chkAeristh.Location = new System.Drawing.Point(208, 3);
            this.chkAeristh.Name = "chkAeristh";
            this.chkAeristh.Size = new System.Drawing.Size(58, 17);
            this.chkAeristh.TabIndex = 7;
            this.chkAeristh.Text = "Aeristh";
            this.chkAeristh.UseVisualStyleBackColor = true;
            // 
            // chkCid
            // 
            this.chkCid.AutoSize = true;
            this.chkCid.Location = new System.Drawing.Point(208, 26);
            this.chkCid.Name = "chkCid";
            this.chkCid.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkCid.Size = new System.Drawing.Size(41, 17);
            this.chkCid.TabIndex = 10;
            this.chkCid.Text = "Cid";
            this.chkCid.UseVisualStyleBackColor = true;
            // 
            // chkRed
            // 
            this.chkRed.AutoSize = true;
            this.chkRed.Location = new System.Drawing.Point(272, 3);
            this.chkRed.Name = "chkRed";
            this.chkRed.Size = new System.Drawing.Size(65, 17);
            this.chkRed.TabIndex = 9;
            this.chkRed.Text = "Red XIII";
            this.chkRed.UseVisualStyleBackColor = true;
            // 
            // StartingEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 218);
            this.Controls.Add(this.flowLayoutPanel10);
            this.Controls.Add(this.numMateria);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.btnConfirm);
            this.Name = "StartingEquipment";
            this.Text = "StartingEquipment";
            ((System.ComponentModel.ISupportInitialize)(this.numMateria)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel10.ResumeLayout(false);
            this.flowLayoutPanel10.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numMateria;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox chkWeapon;
        private System.Windows.Forms.CheckBox chkArmour;
        private System.Windows.Forms.CheckBox chkAccessory;
        private System.Windows.Forms.CheckBox chkMateria;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel10;
        private System.Windows.Forms.CheckBox chkCloud;
        private System.Windows.Forms.CheckBox chkYuffie;
        private System.Windows.Forms.CheckBox chkBarret;
        private System.Windows.Forms.CheckBox chkYCloud;
        private System.Windows.Forms.CheckBox chkTifa;
        private System.Windows.Forms.CheckBox chkSeph;
        private System.Windows.Forms.CheckBox chkAeristh;
        private System.Windows.Forms.CheckBox chkCid;
        private System.Windows.Forms.CheckBox chkRed;
    }
}