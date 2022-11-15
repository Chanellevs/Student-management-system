using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System
{
    internal class FileHandeler
    {
        private List<AdminData> usersList = new List<AdminData>();

        //Creating a file Path
        public string pathConnection()
        {
            string directory = Environment.CurrentDirectory;
            string Newpath = Path.GetFileName(Path.Combine(directory, @"UserDetails.txt"));
            return Newpath;
        }
        public void fileWrite(string userName, string password)
        {
            string tempString = userName + "," + password;
            try
            {
                if (File.Exists(pathConnection())){
                    File.AppendAllText(pathConnection(), tempString);
                }
                else//if there is no file 
                {
                    File.CreateText(pathConnection());
                    File.WriteAllText(pathConnection(), tempString);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public List<AdminData> fileRead()
        {
            //Creating FilePath && Stream Reader
            FileStream fileStream = new FileStream(pathConnection(), FileMode.Open, FileAccess.Read);
            StreamReader readStream = new StreamReader(fileStream);
            try
            {
                    string line = readStream.ReadLine();
                    string[] userArr;

                    while (line != null)
                    {
                        userArr = line.Split(',');
                        usersList.Add(new AdminData(userArr[0], userArr[1]));
                        Console.WriteLine(userArr);
                        line = readStream.ReadLine();
                    }
                    return usersList;
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
                return usersList;
            }
            finally
            {
                using (readStream = new StreamReader(fileStream))
                {
                    if(readStream != null)
                        readStream.Close();
                }
            }
        }
    }
}

