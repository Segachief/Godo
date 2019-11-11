using System;
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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        string directory;
        bool[][] options = new bool[4][];
        Random rnd = new Random();
        int seed;
        int newSeed;

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
            //openFileDialog1.InitialDirectory = "D:\\Steam\\steamapps\\common\\FINAL FANTASY VII\\data";
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    directory = fbd.SelectedPath;
                }
            }
        }

        private void BtnRandoScene_Click(object sender, EventArgs e)
        {
            if (directory != null)
            {
                try
                {
                    if (txtSeed != null)
                    {
                        seed = int.Parse(txtSeed.Text);
                        rnd = new Random(seed);
                    }
                    else
                    {
                        seed = Environment.TickCount;
                        rnd = new Random(seed);
                        //rnd = new Random(Guid.NewGuid().GetHashCode());
                    }



                    string fileName = lblFileName.Text;
                    byte[] kernelLookup = GZipper.PrepareScene(directory, options, rnd);
                    GZipper.PrepareKernel(directory, kernelLookup, options, rnd);
                    MessageBox.Show("Rando Complete: seed = " + seed);
                }
                catch
                {
                    MessageBox.Show("Randomisation failed");
                }
            }
            else
            {
                MessageBox.Show("Error: Valid directory required");
            }
        }
    }
}
