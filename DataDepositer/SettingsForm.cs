using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDepositer
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCreateINIFile_Click(object sender, EventArgs e)
        {
            IniFile ini = new IniFile("config.ini");
            DialogResult res = DialogResult.No;

            if (ini.Exists())
            {
                res = MessageBox.Show("CONFIG.INI already exists !!\n Do You want rewrite ?", "Warning !", MessageBoxButtons.YesNo);
            }

            if (res == DialogResult.OK)
            {
                // create new ini file
                Config config = new Config();
                config.ToINI(ini);

            }



            //config.ToINI("config.ini");
        }
    }
}
