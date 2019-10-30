using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace HomeTaskSeleniumProject.Pages.AdminPanel.Countries
{
    class CountriesMap
    {
        public IWebDriver Browser;

        public CountriesMap(IWebDriver driver)
        {
            Browser = driver;
        }
        public List<string> rowNames => Browser.FindElements(By.XPath("//table[@class='dataTable']//th")).Select(el => el.Text).ToList();

        //var rowNames = Browser.FindElements(By.XPath("//table[@class='dataTable']//th")).Select(el => el.Text);
        public IWebElement colNameIndex = rowNames.ToList().IndexOf("Name");

        var rowsText = Browser.FindElements(By.XPath($"//table[@class='dataTable']//td[{colNameIndex + 1}]/a")).Select(el => el.Text).ToList();
        var sortedCols = rowsText.OrderBy(t => t).ToList();
    }
}
