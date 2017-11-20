/**
 * RuDiCon Soft (c) 2017
 * 
 * Set User Form - form for set up UserName and password (keys for AES256 encrypt/decrypt)
 * 
 */

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
    public partial class SetUserForm : Form
    {

        public String userName = "USER NOT DEFINE";
        public String password = "";

        public SetUserForm()
        {
            InitializeComponent();
        }

        private void setUserForm_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Close();
        }

        private void btnSetUser_Click(object sender, EventArgs e)
        {
            if (tbUserName.Text.Length > 0 && tbPassword.Text.Length > 0)
            {
                if (tbPasswordRepeat.Text == tbPassword.Text)
                {
                    userName = tbUserName.Text;
                    password = tbPassword.Text;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrectly repeat password!!", "ERROR !!! Password repeat incorrect !!!!!");
                }
            }
            else
            {
                MessageBox.Show("User Name and Password must not be EMPTY !!", "ERROR !!! Empty User Name or Password !!!!!");
            }
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            validateLength();
        }
        private void tbPasswordRepeat_TextChanged(object sender, EventArgs e)
        {
            validateLength();
        }

        private void tbUserName_TextChanged(object sender, EventArgs e)
        {
            validateLength();
        }

        private void validateLength()
        {
            btnSetUser.Enabled = tbUserName.Text.Length != 0 && tbPassword.Text.Length > 5 && 
                                    tbPasswordRepeat.Text.Length > 5 && (tbPasswordRepeat.Text == tbPassword.Text);

            if (btnSetUser.Enabled)
            {
                labelImage.Image = Properties.Resources.green_valid1;
            }
            else
            {
                labelImage.Image = Properties.Resources.red_invalid1;
            }

        }

    }
}
