using System;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Student(string name, string group, DateTime dateOfBirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateOfBirth;
        }

    }
    class BinaryFile
    {
        const string SetttingsFileName = @"C:\\Students.dat";

        internal class Program
        {
            static void Main(string[] args)
            {
                Student[] students = null;

                if (File.Exists(SetttingsFileName))
                {                   
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (var fs = new FileStream(SetttingsFileName, FileMode.OpenOrCreate))
                    {
                        students = (Student[])formatter.Deserialize(fs);
                    }

                    string path = @"C:\\Task4\\";
                    DirectoryInfo GroupStudent = new DirectoryInfo(path);

                    GroupStudent.Create();
                    try
                    {
                        
                        for (int i = 0; i < students.Length; i++) {
                            string newpath = path + students[i].Group +".txt";
                            if (!File.Exists(newpath)) 
                            {
                                using (StreamWriter line = File.CreateText(newpath))
                                    {
                                        line.WriteLine(students[i].Name + " " + students[i].DateOfBirth);
                                    }
                                
                            }
                            else 
                            {     
                                using (StreamWriter line = File.AppendText(newpath))
                                {
                                    line.WriteLine(students[i].Name + " " + students[i].DateOfBirth);
                                }

                            }
                        }                    
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                Console.WriteLine("Готово");
            }
        }
    }
}