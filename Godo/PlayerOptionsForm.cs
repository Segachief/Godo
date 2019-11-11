using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo
{
    public partial class PlayerOptionsForm : Form
    {
        public int[][] Options;

        public PlayerOptionsForm()
        {
            InitializeComponent();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            int test;
            if (chkTest.Checked)
            {
                test = 1;
            }
            else
            {
                test = 0;
            }
            Options[0] = new int[] { test };
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
