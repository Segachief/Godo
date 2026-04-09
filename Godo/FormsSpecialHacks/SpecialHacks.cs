using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo.FormsSpecialHacks
{
    public partial class SpecialHacks : Form
    {
        public SpecialHacks()
        {
            InitializeComponent();
        }

        public bool[] specialHackOptions = new bool[5];
        public int[] specialHackParameters = new int[2];

        private bool[] OptionsArrayBuild()
        {
            // Challenges
            if (chkEnemyQuantity.Checked)
            {
                specialHackOptions[0] = true;
            }
            if (chkDisableEscape.Checked)
            {
                specialHackOptions[1] = true;
            }
            if (chkPovertyMode.Checked)
            {
                specialHackOptions[2] = true;
            }
            if (chkSpellspring.Checked)
            {
                specialHackOptions[3] = true;
            }
            if (chkBossSwarm.Checked)
            {
                specialHackOptions[4] = true;
            }
            return specialHackOptions;
        }

        private int[] ParametersArrayBuild()
        {
            specialHackParameters[0] = (int)numSwarm.Value;
            specialHackParameters[1] = (int)numBossSwarm.Value;
            return specialHackParameters;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Hide();
            specialHackOptions = OptionsArrayBuild();
            specialHackParameters = ParametersArrayBuild();
        }
    }
}
