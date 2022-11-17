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
            string moduleCode = txtSearch.Text;

            Datahandler dh = new Datahandler();
            dh.DeleteModule(moduleCode);
            dataGridView1.DataSource = dh.DisplayModule();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string code = txtCode.Text;
            string name = txtName.Text;
            string description = txtDescription.Text;
            string links = txtLinks.Text;

            Modules module = new Modules(code, name, description, links);

            Datahandler dh = new Datahandler();
            dh.UpdateModule(module);

            dataGridView1.DataSource = dh.DisplayModule();
        }

        private void btnADD_Click_1(object sender, EventArgs e)
        {
            string code = txtCode.Text;
            string name = txtName.Text;
            string description = txtDescription.Text;
            string links = txtLinks.Text;

            Modules module = new Modules(code, name, description, links);

            Datahandler dh = new Datahandler();
            dh.AddModule(module);

            dataGridView1.DataSource = dh.DisplayModule();
        }

        private void Module_Load(object sender, EventArgs e)
        {
            Datahandler dh = new Datahandler();

            dataGridView1.DataSource = dh.DisplayModule();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string moduleCode = txtSearch.Text;

            Datahandler dh = new Datahandler();
            Modules module =dh.SearchModule(moduleCode);

            txtCode.Text = module.ModuleCode;
            txtName.Text = module.Name;
            txtDescription.Text = module.Description;
            txtLinks.Text = module.Links;
        }
    }
}
