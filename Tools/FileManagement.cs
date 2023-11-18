using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PrimeNumberDownloader
{
    internal class FileManagement
    {
        public static void CreateFile()
        {
            using (FileStream fs = File.Open(Constants.DEFAULT_FILE_PATH, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");                
                fs.Write(info, 0, info.Length);
            }
        }

    }
}
