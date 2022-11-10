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
            try
            {
                //Creating File path 
                FileStream fileStream = new FileStream(pathConnection(), FileMode.OpenOrCreate, FileAccess.Write);

                //Validation of streamwriter
                if (!File.Exists(pathConnection()))
                {
                    using (TextWriter riter = File.CreateText(pathConnection()))
                    {
                        riter.WriteLine(userName + "," + password);
                        Console.WriteLine("File Successfully written too");
                    }
                }
                else if (File.Exists(pathConnection()))
                {
                    using (TextWriter riter = File.AppendText(pathConnection()))
                    {
                        riter.WriteLine(userName + "," + password);
                        Console.WriteLine("File Successfully Written too");
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
                Console.WriteLine(e.Message);
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

