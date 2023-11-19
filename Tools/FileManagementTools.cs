using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static PrimeNumberDownloader.Constants;
using System.Formats.Tar;

namespace PrimeNumberDownloader
{
    internal class FileManagementTools
    {
        public static bool CreateFile()
        {
            Directory.SetCurrentDirectory(".");
            if (!File.Exists(DEFAULT_FILE_PATH)) return false;
            else
            {
                File.Create(DEFAULT_FILE_PATH);
                return true;
            }
        }
    }
}
