using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System
{
    internal class FileHandeler
    {
        private string filePath = "D:\\Student_Project\\Student-management-system";
        List<AdminData> usersList = new List<AdminData>();
        public void fileWrite(string userName, string password)
        {
            try
            {
                //Creating File path 
                FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);

                //Validation of streamwriter
                if (!File.Exists(filePath))
                {
                    using (TextWriter riter = File.CreateText(filePath))
                    {
                        riter.WriteLine(userName + "," + password);
                        Console.WriteLine("File Successfully written too");
                    }
                }
                else if (File.Exists(filePath))
                {
                    using (TextWriter riter = File.AppendText(filePath))
                    {
                        riter.WriteLine(userName + "," + password);
                        Console.WriteLine("File Successfully Written too");
                        riter.Close();
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("File Write Not SuccessFull");
            }
        }

        public List<AdminData> fileRead()
        {
            try
            {
                //Creating File path 
                FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
                using (StreamReader readStream = new StreamReader(fileStream))
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
                    readStream.Close();
                    return usersList;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return usersList;
            }
        }
    }
}

