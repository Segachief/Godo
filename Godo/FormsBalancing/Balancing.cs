using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo.FormsBalancing
{
    public partial class Balancing : Form
    {
        public Balancing()
        {
            InitializeComponent();
        }

        public bool[] balancingOptions = new bool[4];
        public int[] balancingParameters = new int[2];

        private bool[] OptionsArrayBuild()
        {
            if (chkStronger.Checked)
            {
                balancingOptions[0] = true;
            }
            if (chkWeaker.Checked)
            {
                balancingOptions[1] = true;
            }
            if (chkMaxDrop.Checked)
            {
                balancingOptions[2] = true;
            }
            if (chkMaxAP.Checked)
            {
                balancingOptions[3] = true;
            }
            return balancingOptions;
        }

        private int[] ParametersArrayBuild()
        {
            balancingParameters[0] = (int)numStronger.Value;
            balancingParameters[1] = (int)numWeaker.Value;
            return balancingParameters;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Hide();
            balancingOptions = OptionsArrayBuild();
            balancingParameters = ParametersArrayBuild();
        }
    }
}
