using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Godo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        string directory = Directory.GetCurrentDirectory();
        bool[] options = new bool[61];
        Random rnd = new Random();
        int seed;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Formatting
            this.Text = "Godo";
            this.Location = new Point(300, 300);
            this.MaximizeBox = false;

            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;

            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            ////openFileDialog1.InitialDirectory = "D:\\Steam\\steamapps\\common\\FINAL FANTASY VII\\data";
            //using (var fbd = new FolderBrowserDialog())
            //{
            //    DialogResult result = fbd.ShowDialog();

            //    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            //    {
            //        directory = fbd.SelectedPath;
            //    }
            //}
        }

        private void BtnRandoScene_Click(object sender, EventArgs e)
        {
            if (directory != null)
            {
                try
                {
                    if (txtSeed.Text != "")
                    {
                        seed = int.Parse(txtSeed.Text);
                        rnd = new Random(seed);
                    }
                    else
                    {
                        seed = Environment.TickCount;
                        rnd = new Random(seed);
                        //rnd = new Random(Guid.NewGuid().GetHashCode());
                    }
                    options = OptionsArrayBuild();
                    //string fileName = lblFileName.Text;
                    byte[] kernelLookup = GZipper.PrepareScene(directory, options, rnd, seed);
                    GZipper.PrepareKernel(directory, kernelLookup, options, rnd, seed);
                    MessageBox.Show("Rando Complete: seed = " + seed);

                    string seedFile = directory + "\\FF7RandomSeeds.txt";
                    if (!File.Exists(seedFile))
                    {
                        using (FileStream fs = File.Create(seedFile))
                        {
                            Byte[] title = new UTF8Encoding(true).GetBytes("Random Seed History");
                            fs.Write(title, 0, title.Length);
                        }
                    }

                    using (StreamWriter w = File.AppendText(seedFile))
                    {
                        AllMethods.Log(seed, w);
                    }

                    using (StreamReader r = File.OpenText(seedFile))
                    {
                        AllMethods.DumpLog(r);
                    }
                }
                catch
                {
                    MessageBox.Show("Error: Randomisation Failed - Check that valid files are in correct locations; if so, report the bug along with selected parameters and files used.");
                }
            }
            else
            {
                MessageBox.Show("Error: Valid directory required");
            }
        }

        private bool[] OptionsArrayBuild()
        {
            // it has no style
            // it has no grace
            // this if-else chain
            // is a huge disgrace

            // Character Data
            if (chkStatCurves.Checked)
            {
                options[0] = true;
            }
            if (chkLimitIDs.Checked)
            {
                options[1] = true;
            }
            if (chkLimitKillUse.Checked)
            {
                options[2] = true;
            }
            if (chkLimitGauge.Checked)
            {
                options[3] = true;
            }
            if (chkLevelUpBonus.Checked)
            {
                options[4] = true;
            }
            if (chkStatCurveData.Checked)
            {
                options[5] = true;
            }
            if (chkCharacterAI.Checked)
            {
                options[6] = true;
            }
            if (chkRandomLookup.Checked)
            {
                options[7] = true;
            }


            // Player Data
            if (chkAttackData.Checked)
            {
                options[8] = true;
            }
            if (chkItemData.Checked)
            {
                options[9] = true;
            }
            if (chkWeaponData.Checked)
            {
                options[10] = true;
            }
            if (chkArmourData.Checked)
            {
                options[11] = true;
            }
            if (chkAccessoryData.Checked)
            {
                options[12] = true;
            }
            if (chkMateriaData.Checked)
            {
                options[13] = true;
            }


            // Initialisation Data
            if (chkCharacterID.Checked)
            {
                options[14] = true;
            }
            if (chkCharacterStats.Checked)
            {
                options[15] = true;
            }
            if (chkCharacterName.Checked)
            {
                options[16] = true;
            }
            if (chkEquippedWeapon.Checked)
            {
                options[17] = true;
            }
            if (chkEquippedArmour.Checked)
            {
                options[18] = true;
            }
            if (chkEquippedAccessory.Checked)
            {
                options[19] = true;
            }
            if (chkCharacterHP.Checked)
            {
                options[20] = true;
            }
            if (chkCharacterMP.Checked)
            {
                options[21] = true;
            }
            if (chkEquippedMateria.Checked)
            {
                options[22] = true;
            }
            if (chkStartParty.Checked)
            {
                options[23] = true;
            }


            // Enemy Data
            if (chkEnemyModels.Checked)
            {
                options[24] = true;
            }
            if (chkBattleBG.Checked)
            {
                options[25] = true;
            }
            if (chkDisableEscape.Checked)
            {
                options[26] = true;
            }
            if (chkCamera.Checked)
            {
                options[27] = true;
            }
            if (chkEnemyPlacement.Checked)
            {
                options[28] = true;
            }
            if (chkEnemyQuantity.Checked)
            {
                options[29] = true;
            }
            if (chkEnemyName.Checked)
            {
                options[30] = true;
            }
            if (chkEnemyStats.Checked)
            {
                options[31] = true;
            }
            if (chkElementalAffinity.Checked)
            {
                options[32] = true;
            }
            if (chkHeldItems.Checked)
            {
                options[33] = true;
            }
            if (chkEnemyMP.Checked)
            {
                options[34] = true;
            }
            if (chkEnemyAP.Checked)
            {
                options[35] = true;
            }
            if (chkEnemyHP.Checked)
            {
                options[36] = true;
            }
            if (chkEnemyEXP.Checked)
            {
                options[37] = true;
            }
            if (chkEnemyGil.Checked)
            {
                options[38] = true;
            }
            if (chkStatusImmunities.Checked)
            {
                options[39] = true;
            }
            if (chkEnemyAttacks.Checked)
            {
                options[40] = true;
            }
            if (chkStatusSafe.Checked)
            {
                options[41] = true;
            }
            if (chkStatusUnsafe.Checked)
            {
                options[42] = true;
            }
            if (chkRandomElements.Checked)
            {
                options[43] = true;
            }
            if (chkAttackNames.Checked)
            {
                options[44] = true;
            }
            if (chkEnemyAI.Checked)
            {
                options[45] = true;
            }

            // Tuning & Hacks - Some used to be in Enemy Data and are higher up in the list
            if(chkPovertyMode.Checked)
            {
                options[46] = true;
            }
            if (chkStrongerEnemies.Checked)
            {
                options[47] = true;
            }
            if (chkWeakerEnemies.Checked)
            {
                options[48] = true;
            }
            if (chkMaxDropRates.Checked)
            {
                options[49] = true;
            }
            if (chkSpellspring.Checked)
            {
                options[50] = true;
            }

            // Restriction Rules
            if (chkNoPhysicals.Checked)
            {
                options[51] = true;
            }
            if (chkNoLimits.Checked)
            {
                options[52] = true;
            }
            if (chkNoSpells.Checked)
            {
                options[53] = true;
            }
            if (chkNoSummons.Checked)
            {
                options[54] = true;
            }
            if (chkNoItems.Checked)
            {
                options[55] = true;
            }
            if (chkNoMateria.Checked)
            {
                options[56] = true;
            }
            if (chkNoExp.Checked)
            {
                options[57] = true;
            }
            if (chkNoGil.Checked)
            {
                options[58] = true;
            }
            if (chkNoAP.Checked)
            {
                options[59] = true;
            }
            if (chkInitialEquip.Checked)
            {
                options[60] = true;
            }
            return options;
        }

        //private void MainForm_Load(object sender, EventArgs e)
        //{

        //}
    }
}