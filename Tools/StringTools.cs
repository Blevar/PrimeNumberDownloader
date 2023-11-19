using OpenQA.Selenium.DevTools.V117.Debugger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrimeNumberDownloader.Constants;
using static System.Net.Mime.MediaTypeNames;

namespace PrimeNumberDownloader
{
    internal class StringTools
    {
        static public void ExtractPrimesFromPageContent(string pageContent)
        {
            if (pageContent != "ERROR")
            {
                //Remove all the chatter from the beggining html code which doesnt interest us.
                pageContent = pageContent.Substring(pageContent.IndexOf("<table width=\"100%\" style=\"font-size:8pt;text-align:center\">"));

                //Removest unnececary lines from the begining of the table
                pageContent = pageContent.Substring(pageContent.IndexOf("<td>"));

                //Removes unnececary lines from the end of the html code
                pageContent = pageContent.Substring(0, pageContent.IndexOf("</table>"));

                //Removes last line contens
                pageContent = pageContent.Substring(0, pageContent.IndexOf("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td><"));

                //Removes the first number (which is a repeat of the last one from previous page)
                if (pageContent.Contains("</u>")) pageContent = pageContent.Substring(pageContent.IndexOf("</u>"));

                //Removing table lines markers <tr> and </tr>
                pageContent = pageContent.Replace("<tr>", "");
                pageContent = pageContent.Replace("</tr>", "");
                pageContent = pageContent.Replace("</u>", "");

                //Splitting html table to individual primes
                pageContent = pageContent.Replace("</td>", "\n");
                pageContent = pageContent.Replace("<td>", "");
                string[] numbers = pageContent.Split('\n');

                using (StreamWriter file = new StreamWriter(DEFAULT_FILE_PATH, true))
                {
                    //Adding numbers to the list
                    foreach (string number in numbers)
                    {
                        if (IsDigit(number))
                        {
                            file.WriteLine(number);
                        }
                    }
                }
            }
        }

        static public bool IsDigit(string number)
        {
            //Sanity check
            if (number.Length <= 0) return false;

            bool check = true;
            foreach (char letter in number)
            {
                switch (letter)
                {
                    case '0':
                        check = true;
                        break;
                    case '1':
                        check = true;
                        break;
                    case '2':
                        check = true;
                        break;
                    case '3':
                        check = true;
                        break;
                    case '4':
                        check = true;
                        break;
                    case '5':
                        check = true;
                        break;
                    case '6':
                        check = true;
                        break;
                    case '7':
                        check = true;
                        break;
                    case '8':
                        check = true;
                        break;
                    case '9':
                        check = true;
                        break;
                    default:
                        check = false;
                        break;
                }
                if (check == false) return false;
            }
            return check;
        }

        static public long StringToLong(string number)
        {
            try
            {
                return long.Parse(number);
            }
            catch (Exception)
            {
                return LAST_PRIME_TOO_LOOK_FOR + 1;
            }
        }
    }
}
