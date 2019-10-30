using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;



namespace HomeTaskSeleniumProject.Pages.AdminPanel.LoginPage
{
    public class LoginPage
    {
        public IWebDriver Browser;

        public LoginPage(IWebDriver driver)
        {
            Browser = driver;
        }

        public void Login(string username, string password)
        {
            LoginPageMap _loginPageMap = new LoginPageMap(Browser);
            _loginPageMap.UserName.SendKeys(username);
            _loginPageMap.Password.SendKeys(password);
            _loginPageMap.btnLogin.Click();
        }

        
    }
}
