using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fano
{
    public class FileReader : IDisposable
    {
        private FileStream readableFile;
        private const int bufferSize = 1024;
        private byte[] buffer = new byte[bufferSize];
        private int bytesRead;

        public FileReader(string path)
        {
            try
            {
                readableFile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error opening file: {exception.Message}");
            }
        }

        public byte[] Read()
        {
            try
            {
                bytesRead = readableFile.Read(buffer, 0, buffer.Length);
                return buffer.Take(bytesRead).ToArray();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error reading from file: {exception.Message}");
                return null;
            }
        }

        public void Dispose() 
        {
            if (readableFile != null)
            {
                readableFile.Dispose();
                readableFile = null;
            }
        }
    }
}
