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

namespace PrimeNumberDownloader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<string> primesList = new List<string>();
            string htmlContent;
            string url = Constants.SOURCE_URL;
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(url);
            htmlContent = driver.PageSource;
            StringTools.ExtractPrimesFromPageContent(ref primesList, htmlContent);

            for (int i = 0; i < 10; i++)
            {
                IWebElement nextButton = driver.FindElement(By.Name("nextButton"));
                nextButton.Click();
                System.Threading.Thread.Sleep(Constants.DEFAULT_SLEEP);
                htmlContent = driver.PageSource;
                StringTools.ExtractPrimesFromPageContent(ref primesList, htmlContent);
            }
        }

        static async Task<string> DownloadHtmlAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
