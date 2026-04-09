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
    public partial class EnemyItems : Form
    {
        public EnemyItems()
        {
            InitializeComponent();
        }

        public bool[] enemyItemOptions = new bool[6];

        private bool[] OptionsArrayBuild()
        {
            if (chkHeldItem.Checked)
            {
                enemyItemOptions[0] = true;
            }
            if (chkMorph.Checked)
            {
                enemyItemOptions[1] = true;
            }
            if (chkHeldWeapon.Checked)
            {
                enemyItemOptions[2] = true;
            }
            if (chkHeldArmour.Checked)
            {
                enemyItemOptions[3] = true;
            }
            if (chkHeldAccessory.Checked)
            {
                enemyItemOptions[4] = true;
            }
            if (chkRareItem.Checked)
            {
                enemyItemOptions[5] = true;
            }
            return enemyItemOptions;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Hide();
            enemyItemOptions = OptionsArrayBuild();
        }
    }
}
