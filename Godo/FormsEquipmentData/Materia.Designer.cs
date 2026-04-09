namespace Godo.FormsEquipmentData
{
    partial class Materia
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
            this.numAP = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkAP = new System.Windows.Forms.CheckBox();
            this.chkStatChanges = new System.Windows.Forms.CheckBox();
            this.chkElement = new System.Windows.Forms.CheckBox();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numAP)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(149, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Modifier";
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
            // numAP
            // 
            this.numAP.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numAP.Location = new System.Drawing.Point(152, 26);
            this.numAP.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numAP.Name = "numAP";
            this.numAP.Size = new System.Drawing.Size(49, 20);
            this.numAP.TabIndex = 47;
            this.numAP.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chkAP);
            this.flowLayoutPanel2.Controls.Add(this.chkStatChanges);
            this.flowLayoutPanel2.Controls.Add(this.chkElement);
            this.flowLayoutPanel2.Controls.Add(this.chkStatus);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 23);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(134, 103);
            this.flowLayoutPanel2.TabIndex = 46;
            // 
            // chkAP
            // 
            this.chkAP.AutoSize = true;
            this.chkAP.Location = new System.Drawing.Point(3, 3);
            this.chkAP.Name = "chkAP";
            this.chkAP.Size = new System.Drawing.Size(40, 17);
            this.chkAP.TabIndex = 0;
            this.chkAP.Text = "AP";
            this.chkAP.UseVisualStyleBackColor = true;
            // 
            // chkStatChanges
            // 
            this.chkStatChanges.AutoSize = true;
            this.chkStatChanges.Location = new System.Drawing.Point(3, 26);
            this.chkStatChanges.Name = "chkStatChanges";
            this.chkStatChanges.Size = new System.Drawing.Size(126, 17);
            this.chkStatChanges.TabIndex = 4;
            this.chkStatChanges.Text = "Stat Bonus/Penalties";
            this.chkStatChanges.UseVisualStyleBackColor = true;
            // 
            // chkElement
            // 
            this.chkElement.AutoSize = true;
            this.chkElement.Location = new System.Drawing.Point(3, 49);
            this.chkElement.Name = "chkElement";
            this.chkElement.Size = new System.Drawing.Size(64, 17);
            this.chkElement.TabIndex = 1;
            this.chkElement.Text = "Element";
            this.chkElement.UseVisualStyleBackColor = true;
            // 
            // chkStatus
            // 
            this.chkStatus.AutoSize = true;
            this.chkStatus.Location = new System.Drawing.Point(3, 72);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(67, 17);
            this.chkStatus.TabIndex = 3;
            this.chkStatus.Text = "Statuses";
            this.chkStatus.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(12, 132);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 45;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click_1);
            // 
            // Materia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 169);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numAP);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.btnConfirm);
            this.Name = "Materia";
            this.Text = "Materia";
            ((System.ComponentModel.ISupportInitialize)(this.numAP)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numAP;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox chkAP;
        private System.Windows.Forms.CheckBox chkStatChanges;
        private System.Windows.Forms.CheckBox chkElement;
        private System.Windows.Forms.CheckBox chkStatus;
        private System.Windows.Forms.Button btnConfirm;
    }
}