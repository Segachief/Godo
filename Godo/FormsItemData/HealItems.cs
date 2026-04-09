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
    public partial class HealItems : Form
    {
        public HealItems()
        {
            InitializeComponent();
        }

        public bool[] healItemOptions = new bool[3];
        public int[] healItemParameters = new int[1];

        private bool[] OptionsArrayBuild()
        {
            if (chkBasePower.Checked)
            {
                healItemOptions[0] = true;
            }
            if (chkAnimation.Checked)
            {
                healItemOptions[1] = true;
            }
            if (chkDamageFormula.Checked)
            {
                healItemOptions[2] = true;
            }
            return healItemOptions;
        }

        private int[] ParametersArrayBuild()
        {
            healItemParameters[0] = (int)numBasePower.Value;
            return healItemParameters;
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            healItemOptions = OptionsArrayBuild();
            healItemParameters = ParametersArrayBuild();
        }
    }
}
