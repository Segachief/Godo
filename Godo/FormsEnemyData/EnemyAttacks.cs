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
    public partial class EnemyAttacks : Form
    {
        public EnemyAttacks()
        {
            InitializeComponent();
        }

        public bool[] enemyAttackOptions = new bool[9];
        public int[] enemyAttackParameters = new int[3];

        private bool[] OptionsArrayBuild()
        {
            if (chkAccuracy.Checked)
            {
                enemyAttackOptions[0] = true;
            }
            if (chkMPCost.Checked)
            {
                enemyAttackOptions[1] = true;
            }
            if (chkBasePower.Checked)
            {
                enemyAttackOptions[2] = true;
            }
            if (chkAnimation.Checked)
            {
                enemyAttackOptions[3] = true;
            }
            if (chkDamageFormula.Checked)
            {
                enemyAttackOptions[4] = true;
            }
            if (chkAttackName.Checked)
            {
                enemyAttackOptions[5] = true;
            }
            if (chkElements.Checked)
            {
                enemyAttackOptions[6] = true;
            }
            if (chkAilmentsMild.Checked)
            {
                enemyAttackOptions[7] = true;
            }
            if (chkAilmentsAll.Checked)
            {
                enemyAttackOptions[8] = true;
            }
            return enemyAttackOptions;
        }

        private int[] ParametersArrayBuild()
        {
            enemyAttackParameters[0] = (int)numAccuracy.Value;
            enemyAttackParameters[1] = (int)numMPCost.Value;
            enemyAttackParameters[2] = (int)numBasePower.Value;
            return enemyAttackParameters;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Hide();
            enemyAttackOptions = OptionsArrayBuild();
            enemyAttackParameters = ParametersArrayBuild();
        }
    }
}
