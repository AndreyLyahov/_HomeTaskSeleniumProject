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




namespace HomeTaskSeleniumProject.Pages.AdminPanel.LoginPage
{
    public class LoginPageMap
    {
        private IWebDriver Browser;
        public LoginPageMap(IWebDriver driver)
        {
            Browser = driver;
        }
        
        
        public IWebElement UserName => Browser.FindElement(By.XPath("//input[contains(@name,'username')]"));
        public IWebElement Password => Browser.FindElement(By.XPath("//input[contains(@name,'password')]"));
        public IWebElement btnLogin => Browser.FindElement(By.XPath("//button[contains(@type,'submit')]"));
    }    
}
