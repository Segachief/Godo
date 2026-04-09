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
    public partial class CharacterStats : Form
    {
        public CharacterStats()
        {
            InitializeComponent();
        }

        public bool[] statOptions = new bool[18];
        public bool[] characterSelectStats = new bool[9];
        public int[] statParameters = new int[9];

        private bool[] OptionsArrayBuild()
        {
            if (chkLevel.Checked)
            {
                statOptions[0] = true;
            }
            if (chkStrength.Checked)
            {
                statOptions[1] = true;
            }
            if (chkVitality.Checked)
            {
                statOptions[2] = true;
            }
            if (chkMagic.Checked)
            {
                statOptions[3] = true;
            }
            if (chkSpirit.Checked)
            {
                statOptions[4] = true;
            }
            if (chkDexterity.Checked)
            {
                statOptions[5] = true;
            }
            if (chkLuck.Checked)
            {
                statOptions[6] = true;
            }
            if (chkHP.Checked)
            {
                statOptions[7] = true;
            }
            if (chkMP.Checked)
            {
                statOptions[8] = true;
            }
            if (chkStrGrow.Checked)
            {
                statOptions[9] = true;
            }
            if (chkVitGrow.Checked)
            {
                statOptions[10] = true;
            }
            if (chkMagGrow.Checked)
            {
                statOptions[11] = true;
            }
            if (chkSprGrow.Checked)
            {
                statOptions[12] = true;
            }
            if (chkDexGrow.Checked)
            {
                statOptions[13] = true;
            }
            if (chkLckGrow.Checked)
            {
                statOptions[14] = true;
            }
            if (chkHPGrow.Checked)
            {
                statOptions[15] = true;
            }
            if (chkMPGrow.Checked)
            {
                statOptions[16] = true;
            }
            if (chkEXPGrow.Checked)
            {
                statOptions[17] = true;
            }
            return statOptions;
        }

        private int[] ParametersArrayBuild()
        {
            statParameters[0] = (int)numLevel.Value;
            statParameters[1] = (int)numStrength.Value;
            statParameters[2] = (int)numVitality.Value;
            statParameters[3] = (int)numMagic.Value;
            statParameters[4] = (int)numSpirit.Value;
            statParameters[5] = (int)numDexterity.Value;
            statParameters[6] = (int)numLuck.Value;
            statParameters[7] = (int)numHP.Value;
            statParameters[8] = (int)numMP.Value;
            return statParameters;
        }

        private bool[] CharacterSelectArrayBuild()
        {
            if (chkCloud.Checked)
            {
                characterSelectStats[0] = true;
            }
            if (chkBarret.Checked)
            {
                characterSelectStats[1] = true;
            }
            if (chkTifa.Checked)
            {
                characterSelectStats[2] = true;
            }
            if (chkAeristh.Checked)
            {
                characterSelectStats[3] = true;
            }
            if (chkRed.Checked)
            {
                characterSelectStats[4] = true;
            }
            if (chkYuffie.Checked)
            {
                characterSelectStats[5] = true;
            }
            if (chkYCloud.Checked)
            {
                characterSelectStats[6] = true;
            }
            if (chkSeph.Checked)
            {
                characterSelectStats[7] = true;
            }
            if (chkCid.Checked)
            {
                characterSelectStats[8] = true;
            }
            return characterSelectStats;
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            statOptions = OptionsArrayBuild();
            statParameters = ParametersArrayBuild();
            characterSelectStats = CharacterSelectArrayBuild();
        }
    }
}
