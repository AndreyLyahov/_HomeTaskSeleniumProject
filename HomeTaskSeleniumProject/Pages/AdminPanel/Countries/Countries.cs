using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;


namespace HomeTaskSeleniumProject.Pages.AdminPanel.Countries
{
    class Countries
    {
        public IWebDriver Browser;

        public Countries(IWebDriver driver)
        {
            Browser = driver;
        }
    }
}
