using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using System.Linq;

namespace HomeTaskSeleniumProject.Tests
{
    
    public class BaseTest        
        
    {
        public IWebDriver Browser;
        public WebDriverWait Wait;

        [OneTimeSetUp]
        public void Start()
        {
            Browser = new ChromeDriver();
            Wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(10));
            Browser.Manage().Window.Maximize();
        }


        [OneTimeTearDown]
        public void TearDown()
        {
            Browser.Quit();
        }
    }
}
