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
    public partial class Weapons : Form
    {
        public Weapons()
        {
            InitializeComponent();
        }

        public bool[] weaponOptions = new bool[14];
        public int[] weaponParameters = new int[8];

        private bool[] OptionsArrayBuild()
        {
            if (chkAccuracy.Checked)
            {
                weaponOptions[0] = true;
            }
            if (chkBasePower.Checked)
            {
                weaponOptions[1] = true;
            }
            if (chkCriticalHit.Checked)
            {
                weaponOptions[2] = true;
            }
            if (chkStatA.Checked)
            {
                weaponOptions[3] = true;
            }
            if (chkStatB.Checked)
            {
                weaponOptions[4] = true;
            }
            if (chkStatC.Checked)
            {
                weaponOptions[5] = true;
            }
            if (chkStatD.Checked)
            {
                weaponOptions[6] = true;
            }
            if (chkSlots.Checked)
            {
                weaponOptions[7] = true;
            }
            if (chkGrowth.Checked)
            {
                weaponOptions[8] = true;
            }
            if (chkEquip.Checked)
            {
                weaponOptions[9] = true;
            }
            if (chkElement.Checked)
            {
                weaponOptions[10] = true;
            }
            if (chkStatus.Checked)
            {
                weaponOptions[11] = true;
            }
            if (chkDamageFormula.Checked)
            {
                weaponOptions[12] = true;
            }
            if (chkProperties.Checked)
            {
                weaponOptions[13] = true;
            }
            return weaponOptions;
        }

        private int[] ParametersArrayBuild()
        {
            weaponParameters[0] = (int)numAccuracy.Value;
            weaponParameters[1] = (int)numBasePower.Value;
            weaponParameters[2] = (int)numCritical.Value;
            weaponParameters[3] = (int)numStatA.Value;
            weaponParameters[4] = (int)numStatB.Value;
            weaponParameters[5] = (int)numStatC.Value;
            weaponParameters[6] = (int)numStatD.Value;
            weaponParameters[7] = (int)numSlots.Value;
            return weaponParameters;
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            weaponOptions = OptionsArrayBuild();
            weaponParameters = ParametersArrayBuild();
        }
    }
}
