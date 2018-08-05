using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace University_Management_System
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
            this.ActiveControl = txtUsername;
            txtUsername.Focus();

            txtPassword.PasswordChar = '*';
            txtPassword.MaxLength = 10;
            Binding binding = new Binding("Text", txtUsername, "Text", true, DataSourceUpdateMode.OnValidation);
            txtUsername.DataBindings.Add(binding);
            Binding binding1 = new Binding("Text", txtPassword, "Text", true, DataSourceUpdateMode.OnValidation);
            txtPassword.DataBindings.Add(binding1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Toha")
            {
                if (txtPassword.Text == "admin")
                {
                    new university_management().Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Password");
                    txtUsername.Text = "Toha";
                    txtPassword.Focus();
                    // txtUsername.Text = "Habib";
                    //txtPassword.ResetText();
                    //txtUsername.ResetText();
                }
            }
            else
            {
                MessageBox.Show("Wrong Username");
                //txtPassword.ResetText();
                // txtUsername.ResetText();
            }
        }
        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
    }
}
