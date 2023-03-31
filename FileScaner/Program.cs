using System.Drawing;

class Program
{
    public static string sizeConver(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            FileInfo info = new FileInfo(filePath);
            long size = info.Length;
            string[] sizeletters = new string[] { "bytes", "KB", "MB", "GB", "TB" };
            for (int i = 0; i < 5; i++)
            {
                if (size < 1024)
                {
                    string fileSize = size.ToString() + sizeletters[i];
                    return fileSize;
                }
                size /= 1024;
            }
        }
        return "";
    }
    static void Main()
    {
        Console.WriteLine(sizeConver(@"C:\\prog\\putty\\putty.exe"));
    
    }
}