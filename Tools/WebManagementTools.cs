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
            string htmlContent;
            string lastValue;
            IWebDriver website = new ChromeDriver();            

            website.Navigate().GoToUrl(SOURCE_URL);
            ExtendListToMaxSize(website);
            lastValue = GetLastValueFromFile();
            if (lastValue != FIRST_PRIME) EnterLastStoredValue(website, lastValue);

            //-----Working area

            htmlContent = website.PageSource;
            StringTools.ExtractPrimesFromPageContent(ref primesList, htmlContent);

            for (int i = 0; i < 10; i++)
            {
                IWebElement nextButton = website.FindElement(By.Name("nextButton"));
                nextButton.Click();
                System.Threading.Thread.Sleep(DEFAULT_SLEEP);
                htmlContent = website.PageSource;
                StringTools.ExtractPrimesFromPageContent(ref primesList, htmlContent);
            }

            //-----EOWA

            website.Quit();
        }

        static public void ExtendListToMaxSize(IWebDriver website)
        {
            // Find the list for the prime numbers list in a page
            IWebElement primePerPageDropdown = website.FindElement(By.Name("primePageInput"));
            primePerPageDropdown.Click(); // To open the list
            IWebElement option600 = website.FindElement(By.XPath("//option[@value='600']"));
            option600.Click();

            ClickSearchButton(website);
        }

        static public void EnterLastStoredValue(IWebDriver website, string value)
        {
            // Find "Number" and enter stored value
            IWebElement numberInput = website.FindElement(By.Name("numberInput"));
            numberInput.Clear();
            numberInput.SendKeys(value);

            ClickSearchButton(website);
        }

        static public void ClickSearchButton(IWebDriver website)
        {
            IWebElement searchButton = website.FindElement(By.Name("firstButton"));
            searchButton.Click();
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
