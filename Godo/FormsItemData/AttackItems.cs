using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo.FormsItemData
{
    public partial class AttackItems : Form
    {
        public AttackItems()
        {
            InitializeComponent();
        }

        public bool[] attackItemOptions = new bool[3];
        public int[] attackItemParameters = new int[1];

        private bool[] OptionsArrayBuild()
        {
            if (chkBasePower.Checked)
            {
                attackItemOptions[0] = true;
            }
            if (chkAnimation.Checked)
            {
                attackItemOptions[1] = true;
            }
            if (chkDamageFormula.Checked)
            {
                attackItemOptions[2] = true;
            }
            return attackItemOptions;
        }

        private int[] ParametersArrayBuild()
        {
            attackItemParameters[0] = (int)numBasePower.Value;
            return attackItemParameters;
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            attackItemOptions = OptionsArrayBuild();
            attackItemParameters = ParametersArrayBuild();
        }
    }
}
