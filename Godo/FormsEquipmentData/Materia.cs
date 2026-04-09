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
    public partial class Materia : Form
    {
        public Materia()
        {
            InitializeComponent();
        }

        public bool[] materiaOptions = new bool[4];
        public int[] materiaParameters = new int[1];

        private bool[] OptionsArrayBuild()
        {
            if (chkAP.Checked)
            {
                materiaOptions[0] = true;
            }
            if (chkStatChanges.Checked)
            {
                materiaOptions[1] = true;
            }
            if (chkElement.Checked)
            {
                materiaOptions[2] = true;
            }
            if (chkStatus.Checked)
            {
                materiaOptions[3] = true;
            }
            return materiaOptions;
        }

        private int[] ParametersArrayBuild()
        {
            materiaParameters[0] = (int)numAP.Value;
            return materiaParameters;
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            materiaOptions = OptionsArrayBuild();
            materiaParameters = ParametersArrayBuild();
        }
    }
}
