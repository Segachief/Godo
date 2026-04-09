namespace Godo.FormsEnemyData
{
    partial class Formations
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
            this.chkCameraStandard = new System.Windows.Forms.CheckBox();
            this.chkFirstPerson = new System.Windows.Forms.CheckBox();
            this.chkBG = new System.Windows.Forms.CheckBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Options";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chkCameraStandard);
            this.flowLayoutPanel2.Controls.Add(this.chkFirstPerson);
            this.flowLayoutPanel2.Controls.Add(this.chkBG);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 21);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(157, 70);
            this.flowLayoutPanel2.TabIndex = 33;
            // 
            // chkCameraStandard
            // 
            this.chkCameraStandard.AutoSize = true;
            this.chkCameraStandard.Location = new System.Drawing.Point(3, 3);
            this.chkCameraStandard.Name = "chkCameraStandard";
            this.chkCameraStandard.Size = new System.Drawing.Size(127, 17);
            this.chkCameraStandard.TabIndex = 0;
            this.chkCameraStandard.Text = "Standardised Camera";
            this.chkCameraStandard.UseVisualStyleBackColor = true;
            // 
            // chkFirstPerson
            // 
            this.chkFirstPerson.AutoSize = true;
            this.chkFirstPerson.Location = new System.Drawing.Point(3, 26);
            this.chkFirstPerson.Name = "chkFirstPerson";
            this.chkFirstPerson.Size = new System.Drawing.Size(151, 17);
            this.chkFirstPerson.TabIndex = 2;
            this.chkFirstPerson.Text = "First-Person Camera (Beta)";
            this.chkFirstPerson.UseVisualStyleBackColor = true;
            // 
            // chkBG
            // 
            this.chkBG.AutoSize = true;
            this.chkBG.Location = new System.Drawing.Point(3, 49);
            this.chkBG.Name = "chkBG";
            this.chkBG.Size = new System.Drawing.Size(114, 17);
            this.chkBG.TabIndex = 1;
            this.chkBG.Text = "Battle Background";
            this.chkBG.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(12, 97);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 32;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // Formations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(176, 123);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.btnConfirm);
            this.Name = "Formations";
            this.Text = "Formations";
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox chkCameraStandard;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.CheckBox chkBG;
        private System.Windows.Forms.CheckBox chkFirstPerson;
    }
}