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

        private byte[] Image;

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

            Student student = new Student(studentNumber, name, surname, Image, dateOfBirth, gender, phone, address, moduleCode);
            
            handle.AddStudent(student);
            dataGridView1.DataSource = handle.DisplayStudent();


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
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            if (open.ShowDialog() == DialogResult.OK)
            {
                FileStream stream = new FileStream(open.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);

                Image = reader.ReadBytes((int)reader.BaseStream.Length);

                reader.Close();
                stream.Close();

                pictureBox1.ImageLocation = open.FileName;
                
            }
        }

        private void BntOpenModule_Click(object sender, EventArgs e)
        {
            Module module = new Module();
            this.Hide();
            module.Show();
        }

        private void bntDelete_Click(object sender, EventArgs e)
        {
            string studentNumber = txtSearch.Text;
            Datahandler dt = new Datahandler();

            dt.DeleteStudent(studentNumber);
            dataGridView1.DataSource = dt.DisplayStudent();
        }

        private void BntSearch_Click(object sender, EventArgs e)
        {
            string studentNumber = txtSearch.Text;
            Datahandler dt = new Datahandler();

            Student student = dt.SearchStudent(studentNumber);

            if (student.StudentNumber != null)
            {
                txtStudentNumber.Text = student.StudentNumber;
                txtxName.Text = student.Name;
                txtSurname.Text = student.Surname;
                dateTimePicker1.Value = student.DateOfBirth;
                comboBox1.Text = student.Gender;
                txtPhone.Text = student.Phone;
                txtAdress.Text = student.Address;
                txtModuleCode.Text = student.ModuleCode;
                
            }
          
        }

        private void bntUpdate_Click(object sender, EventArgs e)
        {
            studentNumber = txtStudentNumber.Text;
            name = txtxName.Text;
            surname = txtSurname.Text;
            dateOfBirth = dateTimePicker1.Value;
            gender = comboBox1.Text;
            phone = txtPhone.Text;
            address = txtAdress.Text;
            moduleCode = txtModuleCode.Text;

            Student student = new Student(studentNumber, name, surname, Image, dateOfBirth, gender, phone, address, moduleCode);

            Datahandler dt = new Datahandler();
            dt.UpdateStudent(student);
            dataGridView1.DataSource = dt.DisplayStudent();

        }
    }
}
