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
    public partial class Formations : Form
    {
        public Formations()
        {
            InitializeComponent();
        }
        public bool[] formationOptions = new bool[3];

        private bool[] OptionsArrayBuild()
        {
            if (chkCameraStandard.Checked)
            {
                formationOptions[0] = true;
            }
            if (chkFirstPerson.Checked)
            {
                formationOptions[1] = true;
            }
            if (chkBG.Checked)
            {
                formationOptions[2] = true;
            }
            return formationOptions;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Hide();
            formationOptions = OptionsArrayBuild();
        }
    }
}
