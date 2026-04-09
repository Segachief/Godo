namespace Godo.FormsEnemyData
{
    partial class EnemyItems
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
            this.chkHeldItem = new System.Windows.Forms.CheckBox();
            this.chkMorph = new System.Windows.Forms.CheckBox();
            this.chkHeldWeapon = new System.Windows.Forms.CheckBox();
            this.chkHeldArmour = new System.Windows.Forms.CheckBox();
            this.chkHeldAccessory = new System.Windows.Forms.CheckBox();
            this.chkRareItem = new System.Windows.Forms.CheckBox();
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
            this.label1.TabIndex = 31;
            this.label1.Text = "Options";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chkHeldItem);
            this.flowLayoutPanel2.Controls.Add(this.chkMorph);
            this.flowLayoutPanel2.Controls.Add(this.chkHeldWeapon);
            this.flowLayoutPanel2.Controls.Add(this.chkHeldArmour);
            this.flowLayoutPanel2.Controls.Add(this.chkHeldAccessory);
            this.flowLayoutPanel2.Controls.Add(this.chkRareItem);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 23);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(140, 144);
            this.flowLayoutPanel2.TabIndex = 27;
            // 
            // chkHeldItem
            // 
            this.chkHeldItem.AutoSize = true;
            this.chkHeldItem.Location = new System.Drawing.Point(3, 3);
            this.chkHeldItem.Name = "chkHeldItem";
            this.chkHeldItem.Size = new System.Drawing.Size(126, 17);
            this.chkHeldItem.TabIndex = 0;
            this.chkHeldItem.Text = "Change Enemy Items";
            this.chkHeldItem.UseVisualStyleBackColor = true;
            // 
            // chkMorph
            // 
            this.chkMorph.AutoSize = true;
            this.chkMorph.Location = new System.Drawing.Point(3, 26);
            this.chkMorph.Name = "chkMorph";
            this.chkMorph.Size = new System.Drawing.Size(136, 17);
            this.chkMorph.TabIndex = 1;
            this.chkMorph.Text = "Change Enemy Morphs";
            this.chkMorph.UseVisualStyleBackColor = true;
            // 
            // chkHeldWeapon
            // 
            this.chkHeldWeapon.AutoSize = true;
            this.chkHeldWeapon.Location = new System.Drawing.Point(3, 49);
            this.chkHeldWeapon.Name = "chkHeldWeapon";
            this.chkHeldWeapon.Size = new System.Drawing.Size(110, 17);
            this.chkHeldWeapon.TabIndex = 4;
            this.chkHeldWeapon.Text = "Include Weapons";
            this.chkHeldWeapon.UseVisualStyleBackColor = true;
            // 
            // chkHeldArmour
            // 
            this.chkHeldArmour.AutoSize = true;
            this.chkHeldArmour.Location = new System.Drawing.Point(3, 72);
            this.chkHeldArmour.Name = "chkHeldArmour";
            this.chkHeldArmour.Size = new System.Drawing.Size(97, 17);
            this.chkHeldArmour.TabIndex = 5;
            this.chkHeldArmour.Text = "Include Armour";
            this.chkHeldArmour.UseVisualStyleBackColor = true;
            // 
            // chkHeldAccessory
            // 
            this.chkHeldAccessory.AutoSize = true;
            this.chkHeldAccessory.Location = new System.Drawing.Point(3, 95);
            this.chkHeldAccessory.Name = "chkHeldAccessory";
            this.chkHeldAccessory.Size = new System.Drawing.Size(121, 17);
            this.chkHeldAccessory.TabIndex = 6;
            this.chkHeldAccessory.Text = "Include Accessories";
            this.chkHeldAccessory.UseVisualStyleBackColor = true;
            // 
            // chkRareItem
            // 
            this.chkRareItem.AutoSize = true;
            this.chkRareItem.Location = new System.Drawing.Point(3, 118);
            this.chkRareItem.Name = "chkRareItem";
            this.chkRareItem.Size = new System.Drawing.Size(115, 17);
            this.chkRareItem.TabIndex = 7;
            this.chkRareItem.Text = "Include Rare Items";
            this.chkRareItem.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(12, 173);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 26;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // EnemyItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(163, 204);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.btnConfirm);
            this.Name = "EnemyItems";
            this.Text = "EnemyItems";
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox chkHeldItem;
        private System.Windows.Forms.CheckBox chkMorph;
        private System.Windows.Forms.CheckBox chkHeldWeapon;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.CheckBox chkHeldArmour;
        private System.Windows.Forms.CheckBox chkHeldAccessory;
        private System.Windows.Forms.CheckBox chkRareItem;
    }
}