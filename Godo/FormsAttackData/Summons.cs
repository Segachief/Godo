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
    public partial class Summons : Form
    {
        public Summons()
        {
            InitializeComponent();
        }

        public bool[] summonOptions = new bool[5];
        public int[] summonParameters = new int[3];

        private bool[] OptionsArrayBuild()
        {
            // Spell Data
            if (chkAccuracy.Checked)
            {
                summonOptions[0] = true;
            }
            if (chkMPCost.Checked)
            {
                summonOptions[1] = true;
            }
            if (chkBasePower.Checked)
            {
                summonOptions[2] = true;
            }
            if (chkAnimation.Checked)
            {
                summonOptions[3] = true;
            }
            if (chkDamageFormula.Checked)
            {
                summonOptions[4] = true;
            }
            return summonOptions;
        }

        private int[] ParametersArrayBuild()
        {
            summonParameters[0] = (int)numAccuracy.Value;
            summonParameters[1] = (int)numMPCost.Value;
            summonParameters[2] = (int)numBasePower.Value;
            return summonParameters;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Hide();
            summonOptions = OptionsArrayBuild();
            summonParameters = ParametersArrayBuild();
        }
    }
}
