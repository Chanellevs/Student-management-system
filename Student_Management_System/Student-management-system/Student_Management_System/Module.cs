using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System
{
    public partial class Module : Form
    {
        private string moduleCode;
        private string name;
        private string description;
        private string links;
        public Module()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void bntStudent_Click(object sender, EventArgs e)
        {
            FormMain frmMain = new FormMain();
            this.Hide();
            frmMain.ShowDialog();
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            moduleCode = txtCode.Text;
            name = txtName.Text;
            description = txtDescription.Text;
            links = txtLinks.Text;

            Datahandler handle = new Datahandler();
            Modules module = new Modules(moduleCode,name,description,links);
            

            handle.AddModule(module);



    }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
