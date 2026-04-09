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
    public partial class StatusItems : Form
    {
        public StatusItems()
        {
            InitializeComponent();
        }

        public bool[] statusItemOptions = new bool[2];

        private bool[] OptionsArrayBuild()
        {
            if (chkAnimation.Checked)
            {
                statusItemOptions[0] = true;
            }
            if (chkStatuses.Checked)
            {
                statusItemOptions[1] = true;
            }
            return statusItemOptions;
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            statusItemOptions = OptionsArrayBuild();
        }
    }
}
