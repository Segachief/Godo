using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo.FormsEnemyData
{
    public partial class ModelSwap : Form
    {
        public ModelSwap()
        {
            InitializeComponent();
        }

        public bool[] swapOptions = new bool[4];

        private bool[] OptionsArrayBuild()
        {
            if (chkSafeSwap.Checked)
            {
                swapOptions[0] = true;
            }
            if (chkRiskySwap.Checked)
            {
                swapOptions[1] = true;
            }
            if (chkCrashSwap.Checked)
            {
                swapOptions[2] = true;
            }
            if (chkBossSwap.Checked)
            {
                swapOptions[3] = true;
            }
            return swapOptions;
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            swapOptions = OptionsArrayBuild();
        }
    }
}
