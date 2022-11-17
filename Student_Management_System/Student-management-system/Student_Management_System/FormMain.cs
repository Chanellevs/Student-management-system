using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Student_Management_System
{
    public partial class FormMain : Form
    {
        Datahandler handle = new Datahandler();
        private string studentNumber;
        private string name;
        private string surname;
        private byte[] image;
        private DateTime dateOfBirth;
        private string gender;
        private string phone;
        private string address;
        private string moduleCode;
        public FormMain()
        {
            InitializeComponent();
        }

        private void bntADD_Click(object sender, EventArgs e)
        {
            Datahandler handle = new Datahandler();
            
            
            studentNumber = txtStudentNumber.Text;
            name=txtxName.Text;
            surname= txtSurname.Text;

            dateOfBirth = dateTimePicker1.Value;
            gender = comboBox1.Text;
            phone = txtPhone.Text;
            address = txtAdress.Text;
            moduleCode = txtModuleCode.Text;

            Student student = new Student(studentNumber, name, surname, image, dateOfBirth, gender, phone, address, moduleCode);

            handle.AddStudent(student);


        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Datahandler handle = new Datahandler();

            dataGridView1.DataSource = handle.DisplayStudent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void BntOpenModule_Click(object sender, EventArgs e)
        {
            Module module = new Module();
            this.Hide();
            module.Show();
        }
    }
}
