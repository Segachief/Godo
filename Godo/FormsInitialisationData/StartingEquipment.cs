using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo.FormsInitialisationData
{
    public partial class StartingEquipment : Form
    {
        public StartingEquipment()
        {
            InitializeComponent();
        }

        public bool[] equipOptions = new bool[4];
        public bool[] characterSelectEquip = new bool[9];
        public int[] equipParameters = new int[1];

        

        private bool[] OptionsArrayBuild()
        {
            if (chkMateria.Checked)
            {
                equipOptions[0] = true;
            }
            if (chkWeapon.Checked)
            {
                equipOptions[1] = true;
            }
            if (chkArmour.Checked)
            {
                equipOptions[2] = true;
            }
            if (chkAccessory.Checked)
            {
                equipOptions[3] = true;
            }
            return equipOptions;
        }

        private int[] ParametersArrayBuild()
        {
            equipParameters[0] = (int)numMateria.Value;
            return equipParameters;
        }

        private bool[] CharacterSelectArrayBuild()
        {
            if (chkCloud.Checked)
            {
                characterSelectEquip[0] = true;
            }
            if (chkBarret.Checked)
            {
                characterSelectEquip[1] = true;
            }
            if (chkTifa.Checked)
            {
                characterSelectEquip[2] = true;
            }
            if (chkAeristh.Checked)
            {
                characterSelectEquip[3] = true;
            }
            if (chkRed.Checked)
            {
                characterSelectEquip[4] = true;
            }
            if (chkYuffie.Checked)
            {
                characterSelectEquip[5] = true;
            }
            if (chkYCloud.Checked)
            {
                characterSelectEquip[6] = true;
            }
            if (chkSeph.Checked)
            {
                characterSelectEquip[7] = true;
            }
            if (chkCid.Checked)
            {
                characterSelectEquip[8] = true;
            }
            return characterSelectEquip;
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            equipOptions = OptionsArrayBuild();
            characterSelectEquip = CharacterSelectArrayBuild();
            equipParameters = ParametersArrayBuild();
        }
    }
}
