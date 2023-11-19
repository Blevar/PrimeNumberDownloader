using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrimeNumberDownloader.Constants;
using static PrimeNumberDownloader.FileManagementTools;
using static PrimeNumberDownloader.StringTools;

namespace PrimeNumberDownloader
{
    internal class WebManagementTools
    {
        static public void CreatePrimesList()
        {
            string htmlContent;
            string lastValue;
            IWebDriver website = new ChromeDriver();            

            website.Navigate().GoToUrl(SOURCE_URL);
            ExtendListToMaxSize(website);
            lastValue = GetLastValueFromFile();
            if (lastValue != FIRST_PRIME) EnterLastStoredValue(website, lastValue);

            while (StringToLong(lastValue) <= LAST_PRIME_TOO_LOOK_FOR)
            {
                htmlContent = website.PageSource;
                ExtractPrimesFromPageContent(htmlContent);
                IWebElement nextButton = website.FindElement(By.Name("nextButton"));
                nextButton.Click();
                lastValue = GetLastValueFromFile();
                System.Threading.Thread.Sleep(DEFAULT_SLEEP);                
            }

            website.Quit();
        }

        // Find the list for the prime numbers list in a page
        static public void ExtendListToMaxSize(IWebDriver website)
        {            
            IWebElement primePerPageDropdown = website.FindElement(By.Name("primePageInput"));
            primePerPageDropdown.Click(); // To open the list
            IWebElement option600 = website.FindElement(By.XPath("//option[@value='600']"));
            option600.Click();

            ClickSearchButton(website);
            System.Threading.Thread.Sleep(DEFAULT_SLEEP);
        }

        // Find "Number" and enter stored value
        static public void EnterLastStoredValue(IWebDriver website, string value)
        {            
            IWebElement numberInput = website.FindElement(By.Name("numberInput"));
            numberInput.Clear();
            numberInput.SendKeys(value);

            ClickSearchButton(website);
            System.Threading.Thread.Sleep(DEFAULT_SLEEP);
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
