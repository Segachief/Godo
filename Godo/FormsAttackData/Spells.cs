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
    public partial class Spells : Form
    {
        public Spells()
        {
            InitializeComponent();
        }

        public bool[] spellOptions = new bool[5];
        public int[] spellParameters = new int[3];

        private bool[] OptionsArrayBuild()
        {
            if (chkAccuracy.Checked)
            {
                spellOptions[0] = true;
            }
            if (chkMPCost.Checked)
            {
                spellOptions[1] = true;
            }
            if (chkBasePower.Checked)
            {
                spellOptions[2] = true;
            }
            if (chkAnimation.Checked)
            {
                spellOptions[3] = true;
            }
            if (chkDamageFormula.Checked)
            {
                spellOptions[4] = true;
            }
            return spellOptions;
        }

        private int[] ParametersArrayBuild()
        {
            spellParameters[0] = (int)numAccuracy.Value;
            spellParameters[1] = (int)numMPCost.Value;
            spellParameters[2] = (int)numBasePower.Value;        
            return spellParameters;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Hide();
            spellOptions = OptionsArrayBuild();
            spellParameters = ParametersArrayBuild();
        }
    }
}
