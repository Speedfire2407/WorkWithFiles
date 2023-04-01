using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Укажите путь папки для отчистки");
        string path = Console.ReadLine();
        GetCatalogs(path);
        Console.WriteLine("Готово");
    }

    static void GetFiles(string dirName)
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
                        file.Delete();
                        
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

        }
    }

    static void GetCatalogs(string dirName)
    {

        string dateNow = DateTime.Now.ToString();

        GetFiles(dirName);

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
                        GetFiles(folder.FullName);
                        GetCatalogs(folder.FullName);
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

    static bool Empty (DirectoryInfo directory)
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
}