using System;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
        const string SetttingsFileName = @"C:\\Users\\amazi\\OneDrive\\Desktop\\Students.dat";

        internal class Program
        {
            static void Main(string[] args)
            {
                if (File.Exists(SetttingsFileName))
                {
                    Student[] students = null;
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (var fs = new FileStream(SetttingsFileName, FileMode.OpenOrCreate))
                    {
                        students = (Student[])formatter.Deserialize(fs);
                        // Console.WriteLine($"{students.Name}{students.Group}{students.DateOfBirth}");

                    }

                    foreach (var student in students)
                    {
                        Console.WriteLine($"{student.Name}{student.Group}{student.DateOfBirth}");

                    }
                }
            }
        }
    }
}