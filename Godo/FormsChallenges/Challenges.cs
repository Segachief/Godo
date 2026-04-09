using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo.FormsChallenges
{
    public partial class Challenges : Form
    {
        public Challenges()
        {
            InitializeComponent();
        }

        public bool[] challengeOptions = new bool[10];

        private bool[] OptionsArrayBuild()
        {
            // Challenges
            if (chkNoPhysicals.Checked)
            {
                challengeOptions[0] = true;
            }
            if (chkNoLimits.Checked)
            {
                challengeOptions[1] = true;
            }
            if (chkNoSpells.Checked)
            {
                challengeOptions[2] = true;
            }
            if (chkNoSummons.Checked)
            {
                challengeOptions[3] = true;
            }
            if (chkNoItems.Checked)
            {
                challengeOptions[4] = true;
            }
            if (chkNoMateria.Checked)
            {
                challengeOptions[5] = true;
            }
            if (chkNoExp.Checked)
            {
                challengeOptions[6] = true;
            }
            if (chkNoGil.Checked)
            {
                challengeOptions[7] = true;
            }
            if (chkNoAP.Checked)
            {
                challengeOptions[8] = true;
            }
            if (chkInitialEquip.Checked)
            {
                challengeOptions[9] = true;
            }
            return challengeOptions;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Hide();
            challengeOptions = OptionsArrayBuild();
        }
    }
}
