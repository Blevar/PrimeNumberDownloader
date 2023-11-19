using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrimeNumberDownloader.Constants;
using static PrimeNumberDownloader.FileManagementTools;

namespace PrimeNumberDownloader
{
    internal class WebManagementTools
    {
        static public void CreatePrimesList()
        {
            List<string> primesList = new List<string>();

            CreateFile();

            string htmlContent;
            string url = SOURCE_URL;
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(url);
            htmlContent = driver.PageSource;
            StringTools.ExtractPrimesFromPageContent(ref primesList, htmlContent);

            for (int i = 0; i < 10; i++)
            {
                IWebElement nextButton = driver.FindElement(By.Name("nextButton"));
                nextButton.Click();
                System.Threading.Thread.Sleep(DEFAULT_SLEEP);
                htmlContent = driver.PageSource;
                StringTools.ExtractPrimesFromPageContent(ref primesList, htmlContent);
            }
        }

        static public async Task<string> DownloadHtmlAsync(string url)
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
