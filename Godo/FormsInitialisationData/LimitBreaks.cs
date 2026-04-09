using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo.FormsInitialisationData
{
    public partial class LimitBreaks : Form
    {
        public LimitBreaks()
        {
            InitializeComponent();
        }

        public bool[] limitOptions = new bool[21];
        public bool[] characterSelectLimits = new bool[9];

        private bool[] OptionsArrayBuild()
        {
            if (chkID11.Checked)
            {
                limitOptions[0] = true;
            }
            if (chkID12.Checked)
            {
                limitOptions[1] = true;
            }
            if (chkID13.Checked)
            {
                limitOptions[2] = true;
            }
            if (chkID21.Checked)
            {
                limitOptions[3] = true;
            }
            if (chkID22.Checked)
            {
                limitOptions[4] = true;
            }
            if (chkID23.Checked)
            {
                limitOptions[5] = true;
            }
            if (chkID31.Checked)
            {
                limitOptions[6] = true;
            }
            if (chkID32.Checked)
            {
                limitOptions[7] = true;
            }
            if (chkID33.Checked)
            {
                limitOptions[8] = true;
            }
            if (chkUses12.Checked)
            {
                limitOptions[9] = true;
            }
            if (chkUses13.Checked)
            {
                limitOptions[10] = true;
            }
            if (chkUses22.Checked)
            {
                limitOptions[11] = true;
            }
            if (chkUses23.Checked)
            {
                limitOptions[12] = true;
            }
            if (chkUses32.Checked)
            {
                limitOptions[13] = true;
            }
            if (chkUses33.Checked)
            {
                limitOptions[14] = true;
            }
            if (chkKills2.Checked)
            {
                limitOptions[15] = true;
            }
            if (chkKills3.Checked)
            {
                limitOptions[16] = true;
            }
            if (chkGauge1.Checked)
            {
                limitOptions[17] = true;
            }
            if (chkGauge2.Checked)
            {
                limitOptions[18] = true;
            }
            if (chkGauge3.Checked)
            {
                limitOptions[19] = true;
            }
            if (chkGauge4.Checked)
            {
                limitOptions[20] = true;
            }
            return limitOptions;
        }

        private bool[] CharacterSelectArrayBuild()
        {
            if (chkCloud.Checked)
            {
                characterSelectLimits[0] = true;
            }
            if (chkBarret.Checked)
            {
                characterSelectLimits[1] = true;
            }
            if (chkTifa.Checked)
            {
                characterSelectLimits[2] = true;
            }
            if (chkAeristh.Checked)
            {
                characterSelectLimits[3] = true;
            }
            if (chkRed.Checked)
            {
                characterSelectLimits[4] = true;
            }
            if (chkYuffie.Checked)
            {
                characterSelectLimits[5] = true;
            }
            if (chkYCloud.Checked)
            {
                characterSelectLimits[6] = true;
            }
            if (chkSeph.Checked)
            {
                characterSelectLimits[7] = true;
            }
            if (chkCid.Checked)
            {
                characterSelectLimits[8] = true;
            }
            return characterSelectLimits;
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            limitOptions = OptionsArrayBuild();
            characterSelectLimits = CharacterSelectArrayBuild();
        }
    }
}
