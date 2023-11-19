using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Formats.Tar;
using static PrimeNumberDownloader.Constants;
using static PrimeNumberDownloader.StringTools;

namespace PrimeNumberDownloader
{
    internal class FileManagementTools
    {
        public static string GetLastValueFromFile()
        {
            string lastValue;

            if (File.Exists(DEFAULT_FILE_PATH))
            {
                if (VerifyFileContents())
                {
                    return LastValueInFile();
                }
                else
                {
                    File.Delete(DEFAULT_FILE_PATH);
                    return FIRST_PRIME;
                }
            }
            else
            {
                CreateFile();
                return FIRST_PRIME;
            }                
        }

        public static void CreateFile()
        {
            try
            {
                using (StreamWriter file = new StreamWriter(DEFAULT_FILE_PATH));
            }
            catch (Exception e) { };
        }

        // Checks if the file contents are digits only
        public static bool VerifyFileContents()
        {            
            using (StreamReader file = new StreamReader(DEFAULT_FILE_PATH))
            {
                string line;
                bool ans = true;

                while ((line = file.ReadLine()) != null)
                {
                    ans = IsDigit(line);
                    if (!ans) { break; }
                }
                return ans;
            }
        }


        // Returns the last stored value in the file        
        public static string LastValueInFile()
        {
            using (StreamReader file = new StreamReader(DEFAULT_FILE_PATH))
            {
                string line;
                string lastLine = null;

                while ((line = file.ReadLine()) != null)
                {
                    lastLine = line;
                }

                if (lastLine != null)
                {
                    return lastLine;
                }
                else
                {
                    return FIRST_PRIME;
                }
            }
        }
    }
}
