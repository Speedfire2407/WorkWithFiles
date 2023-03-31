using System;
using System.IO;
using System.Text;

class Test
{
    public static long Countsize(string path)
    {
        long size = 0;
        DirectoryInfo dir = new DirectoryInfo(path);
        if (Directory.Exists(path))
        {
            try
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo f in files)
                {
                    size = size + f.Length;
                }
           
                string[] dirs = Directory.GetDirectories(path);
                foreach (var d in dirs)
                {
                    size = size + Countsize(d);
                }
                return size;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }
    public static void Main()
        
    {
        string path = @"C:\prog\putty";
        try
        {
            Console.WriteLine("Укажите адрес папки в каталоге");
            path = Console.ReadLine();
            long size = 0;
            DriveInfo drive = new DriveInfo(@"C:\");
            size = Countsize(path);
            Console.WriteLine($"Общий размер каталога занимает {size} байт на диске из {drive.TotalSize}");
        }
        catch (Exception ex) 
        { 
            Console.WriteLine(ex.Message); 
        }
        
    }
}