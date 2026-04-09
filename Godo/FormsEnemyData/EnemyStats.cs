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
    public partial class EnemyStats : Form
    {
        public EnemyStats()
        {
            InitializeComponent();
        }
        public bool[] enemyStatOptions = new bool[16];
        public int[] enemyStatParameters = new int[13];

        private bool[] OptionsArrayBuild()
        {
            if (chkLevel.Checked)
            {
                enemyStatOptions[0] = true;
            }
            if (chkStrength.Checked)
            {
                enemyStatOptions[1] = true;
            }
            if (chkVitality.Checked)
            {
                enemyStatOptions[2] = true;
            }
            if (chkMagic.Checked)
            {
                enemyStatOptions[3] = true;
            }
            if (chkSpirit.Checked)
            {
                enemyStatOptions[4] = true;
            }
            if (chkDexterity.Checked)
            {
                enemyStatOptions[5] = true;
            }
            if (chkLuck.Checked)
            {
                enemyStatOptions[6] = true;
            }
            if (chkHP.Checked)
            {
                enemyStatOptions[7] = true;
            }
            if (chkMP.Checked)
            {
                enemyStatOptions[8] = true;
            }
            if (chkEXP.Checked)
            {
                enemyStatOptions[9] = true;
            }
            if (chkGil.Checked)
            {
                enemyStatOptions[10] = true;
            }
            if (chkAP.Checked)
            {
                enemyStatOptions[11] = true;
            }
            if (chkEvade.Checked)
            {
                enemyStatOptions[12] = true;
            }
            if (chkEnemyNames.Checked)
            {
                enemyStatOptions[13] = true;
            }
            if (chkElemAffinity.Checked)
            {
                enemyStatOptions[14] = true;
            }
            if (chkStatusAffinity.Checked)
            {
                enemyStatOptions[15] = true;
            }
            return enemyStatOptions;
        }

        private int[] ParametersArrayBuild()
        {
            enemyStatParameters[0] = (int)numLevel.Value;
            enemyStatParameters[1] = (int)numStrength.Value;
            enemyStatParameters[2] = (int)numVitality.Value;
            enemyStatParameters[3] = (int)numMagic.Value;
            enemyStatParameters[4] = (int)numSpirit.Value;
            enemyStatParameters[5] = (int)numDexterity.Value;
            enemyStatParameters[6] = (int)numLuck.Value;
            enemyStatParameters[7] = (int)numHP.Value;
            enemyStatParameters[8] = (int)numMP.Value;
            enemyStatParameters[9] = (int)numEXP.Value;
            enemyStatParameters[10] = (int)numGil.Value;
            enemyStatParameters[11] = (int)numAP.Value;
            enemyStatParameters[12] = (int)numEvade.Value;
            return enemyStatParameters;
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            enemyStatOptions = OptionsArrayBuild();
            enemyStatParameters = ParametersArrayBuild();
        }
    }
}
