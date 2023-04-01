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

    static void GetFiles(string dirName,ref long sizer, ref long counter)
    {
        
        string dateNow = DateTime.Now.ToString();

        if (Directory.Exists(dirName))
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(dirName);
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {

                    if (Directory.GetLastWriteTime(file.FullName) < DateTime.Now.AddMinutes(-30))
                    {
                        sizer = sizer + file.Length;
                        file.Delete();
                        counter++;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
        }    
    }

    static void GetCatalogs(string dirName,ref long size,ref long counter)
    {
        
        string dateNow = DateTime.Now.ToString();

        GetFiles(dirName, ref size, ref counter);

        if (Directory.Exists(dirName))
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(dirName);
                DirectoryInfo[] dirs = dir.GetDirectories();
                foreach (DirectoryInfo folder in dirs)
                {
                    if (Empty(folder))
                    {
                        if (File.GetLastWriteTime(folder.FullName) < DateTime.Now.AddMinutes(-30))
                        {                            
                            folder.Delete(true);
                        }
                    }
                    else
                    {
                        GetFiles(folder.FullName, ref size,ref counter);
                        GetCatalogs(folder.FullName, ref size, ref counter);
                        if (Empty(folder))
                        {
                            if (folder.LastAccessTime < DateTime.Now.AddMinutes(-30))
                            {
                                folder.Delete(true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        else
        {
            Console.WriteLine("Название каталога указано не верно");
            
        }
    }

    static bool Empty(DirectoryInfo directory)
    {
        DirectoryInfo[] dirs = directory.GetDirectories();
        FileInfo[] files = directory.GetFiles();
        if ((dirs.Length == 0) && (files.Length == 0))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public static void Main()

    {
        string path = @"C:\Task4"; 
        try
        {         
            long size = 0;
            Console.WriteLine("Укажите адрес папки в каталоге");
           // path = Console.ReadLine();
            DriveInfo drive = new DriveInfo(@"C:\");
            size = Countsize(path);
            Console.WriteLine($"Общий размер каталога занимает {size} байт на диске из {drive.TotalSize}");
            Console.WriteLine();
            long counter = 0;
            long sizer = 0;
            GetCatalogs(path, ref sizer, ref counter);
            Console.WriteLine("Готово");
            Console.WriteLine();
            size = 0;
            size = Countsize(path);
            Console.WriteLine($"Количество удаленных файлов {sizer} размер удаленных файлов {counter}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}