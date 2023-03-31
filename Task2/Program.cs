using System;
using System.IO;
using System.Text;

class Test
{
    public static void Main()
    {
        string path = @"C:\Users\amazi\OneDrive\Desktop\Task1\";

        try
        {
            // Create the file, or overwrite if the file exists.
            string add = path + "123.txt";
            using (FileStream fs = File.Create(add))
            {
                byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }

            // Open the stream and read it back.
            using (StreamReader sr = File.OpenText(add))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}