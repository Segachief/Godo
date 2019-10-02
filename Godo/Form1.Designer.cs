namespace Godo
{
    partial class Form1
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
            this.btnRandoKernel = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.btnRandoScene = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRandoKernel
            // 
            this.btnRandoKernel.Location = new System.Drawing.Point(37, 42);
            this.btnRandoKernel.Name = "btnRandoKernel";
            this.btnRandoKernel.Size = new System.Drawing.Size(75, 23);
            this.btnRandoKernel.TabIndex = 0;
            this.btnRandoKernel.Text = "Test Kernel";
            this.btnRandoKernel.UseVisualStyleBackColor = true;
            this.btnRandoKernel.Click += new System.EventHandler(this.BtnRandoKernel_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(37, 13);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open File";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFileName.Location = new System.Drawing.Point(119, 22);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(2, 15);
            this.lblFileName.TabIndex = 2;
            // 
            // btnRandoScene
            // 
            this.btnRandoScene.Location = new System.Drawing.Point(37, 72);
            this.btnRandoScene.Name = "btnRandoScene";
            this.btnRandoScene.Size = new System.Drawing.Size(75, 23);
            this.btnRandoScene.TabIndex = 3;
            this.btnRandoScene.Text = "Test Scene";
            this.btnRandoScene.UseVisualStyleBackColor = true;
            this.btnRandoScene.Click += new System.EventHandler(this.BtnRandoScene_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRandoScene);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnRandoKernel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRandoKernel;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Button btnRandoScene;
    }
}

