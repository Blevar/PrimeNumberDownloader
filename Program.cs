using System;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using static System.Net.Mime.MediaTypeNames;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static PrimeNumberDownloader.WebManagementTools;

namespace PrimeNumberDownloader
{
    class Program
    {
        static async Task Main(string[] args)
        {            
            CreatePrimesList();
        }
    }
}
