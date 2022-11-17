using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using System.Net;

namespace Student_Management_System
{
    internal class Datahandler
    {
        SqlConnection cnn;
        SqlCommand cmd;
        SqlDataReader reader;
        SqlDataAdapter adapter;

        public string DatabaseCon()
        {
            string directory = Environment.CurrentDirectory;
            string path = Path.GetFullPath(Path.Combine(directory, @"StudentManagements.mdf"));
            //return @"Data Source=(LocalDB)\MSSQLLOCALDB;AttachDbFilename=" + path + @";Integrated Security=True; connect Timeout =30";
            return @"Data Source=(local); Initial Catalog=StudentManagements; Integrated Security=SSPI";
        }

        public void AddStudent(Student student)
        {
            try
            {
                cnn = new SqlConnection(DatabaseCon());
                cnn.Open();
                string sql = $"INSERT INTO StudentDetails VALUES ('{student.StudentNumber}','{student.Name}','{student.Surname}',@image,'{student.DateOfBirth}','{student.Gender}','{student.Phone}','{student.Address}','{student.ModuleCode}')";

                cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.Add("@image", SqlDbType.Image, student.Image.Length).Value = student.Image;

                //cmd = new SqlCommand($"INSERT INTO StudentDetails(StudentNumber,StudentName,StudentSurname,DateOfBirth,Gender,Phone,Adress,ModuleCode) VALUES ('{student.StudentNumber}','{student.Name}','{student.Surname}','{student.DateOfBirth}','{student.Gender}','{student.Phone}','{student.Address}','{student.ModuleCode}')", cnn);

                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Student could not be added");
            }
            finally
            {
                cnn.Close();
            }
            
        }

        public DataTable DisplayStudent()
        {
            DataTable dt = new DataTable();

            try
            {
                cnn = new SqlConnection(DatabaseCon());
                cnn.Open();

                adapter = new SqlDataAdapter("SELECT * FROM StudentDetails", cnn);
                adapter.Fill(dt);
            }
            catch
            {
                MessageBox.Show("An error occured when loading the database");
            }
            finally
            {
                cnn.Close();
            }
            return dt;
        }

        public void UpdateStudent(Student student)
        {
            try
            {
                cnn = new SqlConnection(DatabaseCon());
                cnn.Open();

                cmd = new SqlCommand($"UPDATE StudentDetails SET StudentName='{student.Name}', StudentSurname='{student.Surname}',StudentImage=@image , DateOfBirth='{student.DateOfBirth}', Gender='{student.Gender}', Phone={student.Phone}, Adress='{student.Address}', ModuleCode='{student.ModuleCode}' WHERE StudentNumber='{student.StudentNumber}'", cnn);
                cmd.Parameters.Add("@image", SqlDbType.Image, student.Image.Length).Value = student.Image;
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Student could not be updated.");
            }
            finally
            {
                cnn.Close();
            }

        }

        public void DeleteStudent(string studentNumber)
        {
           try
            {
                cnn = new SqlConnection(DatabaseCon());
                cnn.Open();

                cmd = new SqlCommand($"DELETE FROM StudentDetails WHERE StudentNumber='{studentNumber}'", cnn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Student could not be deleted.");
            }
            finally
            {
                cnn.Close();
            }
        }

        public Student SearchStudent(string studentNumber)
        {
            Student student = new Student();

            cnn = new SqlConnection(DatabaseCon());
            cnn.Open();

            cmd = new SqlCommand($"SELECT * FROM StudentDetails WHERE StudentNumber='{studentNumber}'", cnn);
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                student.Name = reader[1].ToString();
                student.Surname = reader[2].ToString();
                //student.Image = reader.GetBytes(3);
                student.DateOfBirth = reader.GetDateTime(4);
                student.Gender = reader[5].ToString();
                student.Phone = reader[6].ToString();
                student.Address = reader[7].ToString();
                student.ModuleCode = reader[8].ToString();
                student.StudentNumber = studentNumber;

                MessageBox.Show("Student found.");
            }
            else
            {
                MessageBox.Show("Student could not be found.");
            }

            return student;
        }

        public void AddModule(Modules module)
        {
            
            try
            {
                cnn = new SqlConnection(DatabaseCon());
                cnn.Open();

                cmd = new SqlCommand($"INSERT INTO StudentModules VALUES ('{module.ModuleCode}','{module.Name}','{module.Description}','{module.Links}')", cnn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Module could not be added.");
            }
            finally
            {
                cnn.Close();
            }

        }

        public DataTable DisplayModule()
        {
            DataTable dt = new DataTable();

            try
            {
                cnn = new SqlConnection(DatabaseCon());
                cnn.Open();

                adapter = new SqlDataAdapter("SELECT * FROM StudentModules", cnn);
                adapter.Fill(dt);
            }
            catch
            {
                MessageBox.Show("An error occured when loading the database");
            }
            return dt;
        }

        public void UpdateModule(Modules module)
        {
            cnn = new SqlConnection(DatabaseCon());
            cnn.Open();

            cmd = new SqlCommand($"UPDATE StudentModules SET ModuleName='{module.Name}', ModuleDescription='{module.Description}', Links='{module.Links}' WHERE ModuleCode={module.ModuleCode}", cnn);
            cmd.ExecuteNonQuery();

            cnn.Close();
        }

        public void DeleteModule(string moduleCode)
        {
            cnn = new SqlConnection(DatabaseCon());
            cnn.Open();

            cmd = new SqlCommand($"DELETE FROM StudentModules WHERE ModuleCode='{moduleCode}'", cnn);
            cmd.ExecuteNonQuery();

            cnn.Close();
        }

        public Modules SearchModule(string moduleCode)
        {
            Modules module = new Modules();

            cnn = new SqlConnection(DatabaseCon());
            cnn.Open();

            cmd = new SqlCommand($"SELECT * FROM StudentModules WHERE ModuleCode='{moduleCode}'", cnn);
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                module.Name = reader[1].ToString();
                module.Description = reader[2].ToString();
                module.Links = reader[3].ToString();
                module.ModuleCode = moduleCode;

                MessageBox.Show("Module found.");
            }
            else
            {
                MessageBox.Show("Module could not be found.");
            }

            return module;
        }

    }
    class Student
    {
        private string studentNumber;
        private string name;
        private string surname;
        private byte[] image;
        private DateTime dateOfBirth;
        private string gender;
        private string phone;
        private string address;
        private string moduleCode;
        private string imageFile;

        public Student(string studentNumber, string name, string surname, byte[] image, DateTime dateOfBirth, string gender, string phone, string address, string moduleCode)
        {
            this.studentNumber = studentNumber;
            this.name = name;
            this.surname = surname;
            this.image = image;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.phone = phone;
            this.address = address;
            this.moduleCode = moduleCode;
        }

        public Student() { }

        public string StudentNumber { get { return studentNumber; } set { studentNumber = value; } }
        public string Name { get { return name; } set { name = value; } }
        public byte[] Image { get { return image; } set { image = value; } }
        public DateTime DateOfBirth { get { return dateOfBirth; } set { dateOfBirth = value; } }
        public string Gender { get { return gender; } set { gender = value; } }
        public string Phone { get { return phone; } set { phone = value; } }
        public string Address { get { return address; } set { address = value; } }
        public string Surname { get { return surname; } set { surname = value; } }
        public string ModuleCode { get { return moduleCode; } set { moduleCode = value; } }
        public string ImageFile { get { return imageFile; } set { imageFile = value; } }
    }

    class Modules
    {
        private string moduleCode;
        private string name;
        private string description;
        private string links;

        public string ModuleCode { get { return moduleCode; } set { moduleCode = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Description { get { return description; } set { description = value; } }
        public string Links { get { return links; } set { links = value; } }

        public Modules(string moduleCode, string name, string description, string links)
        {
            ModuleCode = moduleCode;
            Name = name;
            Description = description;
            Links = links;
        }

        public Modules() { }
    }
}

