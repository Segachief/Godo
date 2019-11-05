﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Current TODO
        /*
         * 1) Set up iteration that can unpack and repack entire kernel, all sections, and final file be opened with wall market without issue.
         * 
         * 2) Set up the randomise kernel button as a method that is called during the unpacking, targeting required sections appropriately.
         * 
         * 3) Have everything happen in-memory without files, or at least clean up leftover files (especially if error is encountered).
         */
        string directory;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Formatting
            this.Text = "Godo";
            this.BackColor = Color.DarkRed;
            this.Location = new Point(300, 300);
            this.MaximizeBox = false;

            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;

            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "D:\\Steam\\steamapps\\common\\FINAL FANTASY VII\\data";
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    directory = fbd.SelectedPath;
                }
            }

            if (directory != null)
            {
                try
                {
                    lblFileName.Text = openFileDialog1.FileName;
                    string fileName = lblFileName.Text;
                    GZipper.PrepareScene(directory);
                    MessageBox.Show("Scene Prep Complete: DEBUG");
                }
                catch
                {
                    MessageBox.Show("Error: File failed to open");
                }
            }
            else
            {

            }
        }

        private void BtnRandoScene_Click(object sender, EventArgs e)
        {

        }

        private void BtnRandoKernel_Click(object sender, EventArgs e)
        {
            
        }
    }
}
