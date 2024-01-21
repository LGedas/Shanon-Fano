using System.Text;
using System.IO;
using System;

namespace Fano.Tests
{
    public class TestFileUtilities : IDisposable
    {
        public const string path = @"C:\Users\Gedas\Desktop\Shanon-Fano\TestingFiles\Temp_file.txt";
        public TestFileUtilities()
        {
            MakeFile("");
        }
        public void Dispose()
        {
            DeleteFile();
        }

        public static void MakeFile(string fileText)
        {
            FileStream testFile = File.Create(path);

            testFile.Write(Encoding.ASCII.GetBytes(fileText));

            testFile.Close();
        }

        public static void DeleteFile()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}