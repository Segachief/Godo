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
    public partial class Accessories : Form
    {
        public Accessories()
        {
            InitializeComponent();
        }

        public bool[] accessoryOptions = new bool[6];
        public int[] accessoryParameters = new int[2];

        private bool[] OptionsArrayBuild()
        {
            if (chkStatA.Checked)
            {
                accessoryOptions[0] = true;
            }
            if (chkStatB.Checked)
            {
                accessoryOptions[1] = true;
            }
            if (chkEquip.Checked)
            {
                accessoryOptions[2] = true;
            }
            if (chkElement.Checked)
            {
                accessoryOptions[3] = true;
            }
            if (chkStatus.Checked)
            {
                accessoryOptions[4] = true;
            }
            if (chkSpecial.Checked)
            {
                accessoryOptions[5] = true;
            }
            return accessoryOptions;
        }

        private int[] ParametersArrayBuild()
        {
            accessoryParameters[0] = (int)numStatA.Value;
            accessoryParameters[1] = (int)numStatB.Value;
            return accessoryParameters;
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            accessoryOptions = OptionsArrayBuild();
            accessoryParameters = ParametersArrayBuild();
        }
    }
}
