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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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
            // Temporary test method, open files to check randomiser sections
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BinaryReader br = new BinaryReader(new MemoryStream(File.ReadAllBytes(openFileDialog1.FileName)));
                    lblFileName.Text = openFileDialog1.FileName;
                }
                catch
                {
                    MessageBox.Show("An error has occurred; please check that a valid file was loaded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnRandomise_Click(object sender, EventArgs e)
        {

        }
    }
}
