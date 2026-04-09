using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo.FormsEquipmentData
{
    public partial class Armour : Form
    {
        public Armour()
        {
            InitializeComponent();
        }

        public bool[] armourOptions = new bool[13];
        public int[] armourParameters = new int[9];

        private bool[] OptionsArrayBuild()
        {
            if (chkDefence.Checked)
            {
                armourOptions[0] = true;
            }
            if (chkMagicDefence.Checked)
            {
                armourOptions[1] = true;
            }
            if (chkEvasion.Checked)
            {
                armourOptions[2] = true;
            }
            if (chkMagicEvasion.Checked)
            {
                armourOptions[3] = true;
            }
            if (chkStatA.Checked)
            {
                armourOptions[4] = true;
            }
            if (chkStatB.Checked)
            {
                armourOptions[5] = true;
            }
            if (chkStatC.Checked)
            {
                armourOptions[6] = true;
            }
            if (chkStatD.Checked)
            {
                armourOptions[7] = true;
            }
            if (chkSlots.Checked)
            {
                armourOptions[8] = true;
            }
            if (chkGrowth.Checked)
            {
                armourOptions[9] = true;
            }
            if (chkEquip.Checked)
            {
                armourOptions[10] = true;
            }
            if (chkElement.Checked)
            {
                armourOptions[11] = true;
            }
            if (chkStatus.Checked)
            {
                armourOptions[12] = true;
            }
            return armourOptions;
        }

        private int[] ParametersArrayBuild()
        {
            armourParameters[0] = (int)numDefence.Value;
            armourParameters[1] = (int)numMagicDefence.Value;
            armourParameters[2] = (int)numEvasion.Value;
            armourParameters[3] = (int)numMagicEvasion.Value;
            armourParameters[4] = (int)numStatA.Value;
            armourParameters[5] = (int)numStatB.Value;
            armourParameters[6] = (int)numStatC.Value;
            armourParameters[7] = (int)numStatD.Value;
            armourParameters[8] = (int)numSlots.Value;
            return armourParameters;
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            armourOptions = OptionsArrayBuild();
            armourParameters = ParametersArrayBuild();
        }
    }
}
