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
    public partial class EnemySkills : Form
    {
        public EnemySkills()
        {
            InitializeComponent();
        }

        public bool[] enemySkillOptions = new bool[5];
        public int[] enemySkillParameters = new int[3];

        private bool[] OptionsArrayBuild()
        {
            // Enemy Skill Data
            if (chkAccuracy.Checked)
            {
                enemySkillOptions[0] = true;
            }
            if (chkMPCost.Checked)
            {
                enemySkillOptions[1] = true;
            }
            if (chkBasePower.Checked)
            {
                enemySkillOptions[2] = true;
            }
            if (chkAnimation.Checked)
            {
                enemySkillOptions[3] = true;
            }
            if (chkDamageFormula.Checked)
            {
                enemySkillOptions[4] = true;
            }
            return enemySkillOptions;
        }

        private int[] ParametersArrayBuild()
        {
            enemySkillParameters[0] = (int)numAccuracy.Value;
            enemySkillParameters[1] = (int)numMPCost.Value;
            enemySkillParameters[2] = (int)numBasePower.Value;
            return enemySkillParameters;
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            enemySkillOptions = OptionsArrayBuild();
            enemySkillParameters = ParametersArrayBuild();
        }
    }
}
