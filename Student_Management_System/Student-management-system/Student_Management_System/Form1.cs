using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Student_Management_System
{
    public partial class FormLogin : Form
    {
        private string username;
        private string password;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            username = txtUsername.Text;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            password = txtPassword.Text;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            FileHandeler handler = new FileHandeler();
            List<AdminData> userList = new List<AdminData>();

            //Get Values from the text file 
            userList = handler.fileRead();

            //each value must now be compared
            foreach (AdminData user in userList)
            {
                if (username == user.Username && password == user.Password)
                {
                    FormMain formMain = new FormMain();
                    this.Hide();
                    formMain.Show();
                    Console.WriteLine("Build Successfull");
                }
                else
                {
                    Console.WriteLine("Incorrect pass");
                }
            }
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            FileHandeler handler = new FileHandeler();
            handler.fileWrite(username, password);
            txtPassword.Clear();
            txtUsername.Clear();
            MessageBox.Show("User has been created!");
        }
    }
}
