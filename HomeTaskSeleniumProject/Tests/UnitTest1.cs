using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;


namespace HomeTaskSeleniumProject.Tests
{
    public class UnitTest1 : BaseTest 
    {
        [Test]
        public void TestMethod1()
        {
            Browser.Navigate().GoToUrl("http://localhost/litecart/admin/");

            IWebElement UserName = Browser.FindElement(By.XPath("//input[contains(@name,'username')]"));
            UserName.SendKeys("admin");

            IWebElement Password = Browser.FindElement(By.XPath("//input[contains(@name,'password')]"));
            Password.SendKeys("admin");

            IWebElement btnLogin = Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@type,'submit')]")));
            btnLogin.Click();

            Wait.Until(_ => Browser.FindElements(By.XPath("//ul[@id='box-apps-menu']/li/a/span[@class='name']")).Count > 0);

            List <IWebElement> MainMenuItems = Browser.FindElements(By.XPath("//ul[@id='box-apps-menu']/li/a/span[@class='name']")).ToList();

            for (int i = 0; i < MainMenuItems.Count; i++)
            {
                MainMenuItems = Browser.FindElements(By.XPath("//ul[@id='box-apps-menu']/li/a/span[@class='name']")).ToList();
                //if (MainMenuItems[i].Text.Equals("Settings") || MainMenuItems[i].Text.Equals("Modules"))  continue;
                MainMenuItems[i].Click();
                Wait.Until(_ => Browser.FindElement(By.XPath("//td[@id='content']/h1")).Displayed);
                List<IWebElement> ChildMenuItems = Browser.FindElements(By.XPath("//ul[@id='box-apps-menu']/li/ul[@class='docs']//span")).ToList();
                if (ChildMenuItems.Count > 0)
                {
                    List<string> ChildMenuItemsTexts = ChildMenuItems.Select(_ => _.Text).ToList();
                    for (int j = 0; j < ChildMenuItems.Count;  j++)
                    {
                        ChildMenuItems = Browser.FindElements(By.XPath("//ul[@id='box-apps-menu']/li/ul[@class='docs']//span")).ToList();
                        ChildMenuItems[j].Click();
                        string headerText = Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//td[@id='content']/h1"))).Text.Trim();
                        MainMenuItems = Browser.FindElements(By.XPath("//ul[@id='box-apps-menu']/li/a/span[@class='name']")).ToList();
                        if (MainMenuItems[i].Text.Equals("Settings") || MainMenuItems[i].Text.Equals("Modules"))
                            Assert.IsTrue(headerText.Contains(MainMenuItems[i].Text));
                        else Assert.IsTrue(headerText.Contains(ChildMenuItemsTexts[j]));
                    }
                }                
            }
            IWebElement LogOutBtn = Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//i[@class='fa fa-sign-out fa-lg']")));
            LogOutBtn.Click();      

        }


        [Test]
        public void TestMethod2()
        {
            Browser.Navigate().GoToUrl("http://localhost/litecart/");    

            Wait.Until(_ => Browser.FindElements(By.XPath("//li[@class='product column shadow hover-light']")).Count > 0);
            var Products = Browser.FindElements(By.XPath("//li[@class='product column shadow hover-light']")).ToList();
            for (int k = 0; k < Products.Count; k++)
            {
                var Sticker = Products[k].FindElement(By.XPath("//div[contains(@class,'sticker')]"));
                Assert.IsTrue(Sticker.Displayed);
            }           
        }

        [Test]
        public void TestMethod3()
        {
            Browser.Navigate().GoToUrl("http://localhost/litecart/admin/");
            //login
            IWebElement UserName = Browser.FindElement(By.XPath("//input[contains(@name,'username')]"));
            UserName.SendKeys("admin");
            IWebElement Password = Browser.FindElement(By.XPath("//input[contains(@name,'password')]"));
            Password.SendKeys("admin");
            //login
            //open Countries page
            IWebElement btnLogin = Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@type,'submit')]")));
            btnLogin.Click();
            IWebElement MenuItemCountries = Browser.FindElement(By.LinkText("Countries"));
            MenuItemCountries.Click();
            //open Countries page

            var rowNames = Browser.FindElements(By.XPath("//table[@class='dataTable']//th")).Select(el => el.Text);
            var colNameIndex = rowNames.ToList().IndexOf("Name");
            var rowsText = Browser.FindElements(By.XPath($"//table[@class='dataTable']//td[{colNameIndex + 1}]/a")).Select(el => el.Text).ToList();    
            var sortedCols = rowsText.OrderBy(t => t).ToList();
            Assert.IsTrue(sortedCols.SequenceEqual(rowsText), "Countries are not sorted alphabetically");
        }

        [Test]

        public void TestMethod4()
        {
            Browser.Navigate().GoToUrl("http://localhost/litecart/admin/");
            IWebElement UserName = Browser.FindElement(By.XPath("//input[contains(@name,'username')]"));
            UserName.SendKeys("admin");
            IWebElement Password = Browser.FindElement(By.XPath("//input[contains(@name,'password')]"));
            Password.SendKeys("admin");
            IWebElement btnLogin = Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@type,'submit')]")));
            btnLogin.Click();
            IWebElement MenuItemCountries = Browser.FindElement(By.LinkText("Countries"));
            MenuItemCountries.Click();

            var rowText = Browser.FindElements(By.XPath("//table[@class='dataTable']//th")).Select(el => el.Text);
            //var colZoneIndex = rowText.ToList().IndexOf("Zone");
            var zoneIndex = Browser.FindElements(By.XPath("//table[@class='dataTable']//td[6]")).Select(el => Convert.ToInt16(el.Text)).ToList();
            //var zoneIndexNotZero = zoneIndex.Select(el => !el.Equals("0");

            var zoneIndexNotZero = zoneIndex.Select((value, index) => new { value, index })
                      .Where(pair => pair.value > 0)
                      .Select(pair => pair.index + 1)
                      .ToList();


            foreach (var index in zoneIndexNotZero)
            {
                Browser.FindElement(By.XPath($"(//table[@class='dataTable']//td[5]/a)[{index}]")).Click();

                var rowNames = Browser.FindElements(By.XPath("//table[@class='dataTable']//th")).Select(el => el.Text);
                var colNameIndex = rowNames.ToList().IndexOf("Name");
                var rowsText = Browser.FindElements(By.XPath($"//table[@class='dataTable']//td[{colNameIndex + 1}]")).Select(el => el.Text).ToList();
                var sortedCols = rowsText.OrderBy(t => t).ToList();
                Assert.IsTrue(sortedCols.SequenceEqual(rowsText), "Zones are not sorted alphabetically");
                MenuItemCountries = Browser.FindElement(By.LinkText("Countries"));
                MenuItemCountries.Click();

            }
        }

        [Test]

        public void TestMethod5()
        {
            Browser.Navigate().GoToUrl("http://localhost/litecart/");
            Wait.Until(_ => Browser.FindElement(By.XPath("//*[@id='box-campaigns']//li[1]/a/div[2]")));
            IWebElement firstProduct = Browser.FindElement(By.XPath("//*[@id='box-campaigns']//li[1]/a/div[2]"));
            
            //Получение свойств цен на карточке товара
            IWebElement firstProductCard = Browser.FindElement(By.XPath("//*[@id='box-campaigns']//li[1]"));
            IWebElement campaignProductPriceCard = firstProductCard.FindElement(By.ClassName("campaign-price"));
            IWebElement regularProductPriceCard = firstProductCard.FindElement(By.ClassName("regular-price"));

            List<string> regularPriceStylesCard = new List<string>(3);
            regularPriceStylesCard.Insert(0, regularProductPriceCard.GetCssValue("color"));
            regularPriceStylesCard.Insert(1, regularProductPriceCard.GetCssValue("text-decoration-line"));
            regularPriceStylesCard.Insert(2, regularProductPriceCard.GetCssValue("font-weight"));

            List<string> campaignPriceStyles = new List<string>(2);
            campaignPriceStyles.Insert(0, campaignProductPriceCard.GetCssValue("color"));
            campaignPriceStyles.Insert(1, campaignProductPriceCard.GetCssValue("font-weight"));
            //Получение свойств цен на карточке товара
            string productLable = firstProduct.GetAttribute("textContent");
            IWebElement firstProductLink = Browser.FindElement(By.XPath("//*[@id='box-campaigns']//li[1]/a"));
            firstProductLink.Click();            
            IWebElement logoDuck = Browser.FindElement(By.TagName("H1"));
            string productLable1 = logoDuck.GetAttribute("innerText");

            //Получение свойств цен в товаре
            IWebElement campaignProductPrice = Browser.FindElement(By.XPath("//*[@id='box-product']//strong[contains(@class,'campaign-price')]"));
            IWebElement regularProductPrice = Browser.FindElement(By.XPath("//*[@id='box-product']//s[contains(@class,'regular-price')]"));

            List<string> regularPriceStylesIn = new List<string>(3);
            regularPriceStylesIn.Insert(0, regularProductPrice.GetCssValue("color"));
            regularPriceStylesIn.Insert(1, regularProductPrice.GetCssValue("text-decoration-line"));
            regularPriceStylesIn.Insert(2, regularProductPrice.GetCssValue("font-weight"));

            List<string> campaignPriceStylesIn = new List<string>(2);
            campaignPriceStylesIn.Insert(0, campaignProductPrice.GetCssValue("color"));
            campaignPriceStylesIn.Insert(1, campaignProductPrice.GetCssValue("font-weight"));
            //Получение свойств цен в товаре

            Assert.Multiple(() =>
            {
                CollectionAssert.AreEqual(regularPriceStylesCard, regularPriceStylesIn);
                CollectionAssert.AreEqual(campaignPriceStyles, campaignPriceStylesIn);
                Assert.AreEqual(productLable, productLable1);
            });
        }

        [Test]
        public void TestMethod6()
        {
            Browser.Navigate().GoToUrl("http://localhost/litecart/");
            IWebElement createNewCustomer = Browser.FindElement(By.LinkText("New customers click here"));
            createNewCustomer.Click();
            IWebElement firstNameField = Browser.FindElement(By.Name("firstname"));
            firstNameField.Click();
            firstNameField.SendKeys("Vasiliy");
            IWebElement lastNameField = Browser.FindElement(By.Name("lastname"));
            lastNameField.Click();
            lastNameField.SendKeys("Ivanov");
            IWebElement addressField = Browser.FindElement(By.Name("address1"));
            addressField.Click();
            addressField.SendKeys("Karla Marksa 35"); 
            IWebElement postcodeField = Browser.FindElement(By.Name("postcode"));
            postcodeField.Click();
            postcodeField.SendKeys("49000");
            IWebElement cityField = Browser.FindElement(By.Name("city"));
            cityField.Click();
            cityField.SendKeys("Dnipro");
            IWebElement countryField = Browser.FindElement(By.ClassName("select2-selection__arrow"));
            countryField.Click();
            IWebElement searchCountryfield = Browser.FindElement(By.ClassName("select2-search__field"));
            searchCountryfield.Click();
            searchCountryfield.SendKeys("Ukraine" + Keys.Enter);
            IWebElement registrEmail = Browser.FindElement(By.Name("email"));
            registrEmail.Click();
            registrEmail.SendKeys("asd@asd1.com");
            IWebElement phoneField = Browser.FindElement(By.Name("phone"));
            phoneField.Clear();
            phoneField.SendKeys("+380682936751");
            IWebElement passwordField = Browser.FindElement(By.Name("password"));
            passwordField.Click();
            passwordField.SendKeys("123123");
            IWebElement confirmPasswordField = Browser.FindElement(By.Name("confirmed_password"));
            confirmPasswordField.Click();
            confirmPasswordField.SendKeys("123123");

            /*
            //CAPTCHA

            var remElement = Browser.FindElement(By.Name("captcha_id"));
            Point location = remElement.Location;
            var screenshot = ((ITakesScreenshot)Browser).GetScreenshot();
            MemoryStream stream = new MemoryStream(screenshot.AsByteArray);
            Bitmap bitmap = new Bitmap(stream)
                {
                    RectangleF part = new RectangleF(location.X, location.Y, remElement.Size.Width, remElement.Size.Height);
                    using (Bitmap bn = bitmap.Clone(part, bitmap.PixelFormat))
                    {
                        bn.Save("c:\\tmp\\screenshot.png" + "CaptchImage.png", System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
            

            //reading text from images
            var engine = new TesseractEngine("tessdata path here", "eng", EngineMode.Default)
            {

                Page ocrPage = engine.Process(Pix.LoadFromFile(filePath + "CaptchImage.png"), PageSegMode.AutoOnly);
                var captchatext = ocrPage.GetText();
            }

            //CAPTCHA
            */
            
            IWebElement createButton = Browser.FindElement(By.Name("create_account"));
            createButton.Click();

            //logout

            void LogoutProfile()
            {
                IWebElement logout = Wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("logout")));
                logout.Click();
            }
            

            //login
            IWebElement loginEmail = Browser.FindElement(By.Name("email"));
            loginEmail.Click();
            loginEmail.SendKeys("asd@asd1.com");
            IWebElement loginPassword = Browser.FindElement(By.Name("password"));
            loginPassword.Click();
            loginPassword.SendKeys("123123");
            IWebElement loginButton = Browser.FindElement(By.XPath("//button[contains(@name,'login')]"));
            loginButton.Click();

            LogoutProfile();
        }

        [Test]

        public void TestMethod7()
        {
            Browser.Navigate().GoToUrl("http://localhost/litecart/admin/");
            IWebElement UserName = Browser.FindElement(By.XPath("//input[contains(@name,'username')]"));
            UserName.SendKeys("admin");
            IWebElement Password = Browser.FindElement(By.XPath("//input[contains(@name,'password')]"));
            Password.SendKeys("admin");
            IWebElement btnLogin = Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@type,'submit')]")));
            btnLogin.Click();
            IWebElement MenuItemCountries = Browser.FindElement(By.LinkText("Catalog"));
            MenuItemCountries.Click();
            IWebElement addNewProduct = Wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Add New Product")));
            addNewProduct.Click();
            IWebElement productNameField = Browser.FindElement(By.Name("name[en]"));
            productNameField.Click();
            productNameField.SendKeys("New Tet Duck");
            IWebElement productCodeField = Browser.FindElement(By.Name("code"));
            productCodeField.Click();
            productCodeField.SendKeys("04092019");
            IWebElement checkboxCategory = Browser.FindElement(By.XPath("//input[contains(@data-name,'Rubber Ducks')]"));
            checkboxCategory.Click();
            IWebElement checkboxGender = Browser.FindElement(By.XPath("//input[contains(@value,'1-3')]"));
            checkboxGender.Click();
            IWebElement quantityField = Browser.FindElement(By.Name("quantity"));
            quantityField.Click();
            quantityField.Clear();
            quantityField.SendKeys("10");

            IWebElement downloadImage = Browser.FindElement(By.Name("new_images[]"));
            //downloadImage.SendKeys("Trump-rubber-duck-Amsterdam-Duck-Store.jpg");
            // E:\Apriorit\Selenium\HomeTask\HomeTaskSeleniumProject - копия\Trump - rubber - duck - Amsterdam - Duck - Store.jpg
            
            IWebElement datePickerFrom = Browser.FindElement(By.Name("date_valid_from"));
            datePickerFrom.SendKeys("10092019");

            IWebElement datePickerTo = Browser.FindElement(By.Name("date_valid_to"));
            datePickerTo.SendKeys("11092020");

            IWebElement InformationTab = Browser.FindElement(By.XPath("//a[contains(@href,'#tab-information')]"));
            InformationTab.Click();

            IWebElement ManufactureDropdown = Browser.FindElement(By.XPath("//select[contains(@name,'manufacturer_id')]"));
            ManufactureDropdown.Click();

            IWebElement ManufacturerDropdownValue = ManufactureDropdown.FindElement(By.XPath("//option[contains(@value,'1')]"));
            ManufacturerDropdownValue.Click();

            IWebElement KeywordsField = Browser.FindElement(By.XPath("//input[contains(@name,'keywords')]"));
            KeywordsField.SendKeys("Duck Test");
            
            IWebElement ShortDescriptionField = Browser.FindElement(By.XPath("//input[contains(@name,'short_description[en]')]"));
            ShortDescriptionField.SendKeys("Description Duck Test");

            IWebElement DescriptionField = Browser.FindElement(By.XPath("//textarea[contains(@name,'description[en]')]"));
            DescriptionField.SendKeys("Description Duck Test");

            IWebElement HeadTitleField = Browser.FindElement(By.XPath("//input[contains(@name,'head_title[en]')]"));
            HeadTitleField.SendKeys("Duck Trump");

            IWebElement MetaDecriptionField = Browser.FindElement(By.XPath("//input[contains(@name,'meta_description[en]')]"));
            MetaDecriptionField.SendKeys("Duck Trump");

            IWebElement PricesTab = Browser.FindElement(By.XPath("//a[contains(@href,'#tab-prices')]"));
            PricesTab.Click();

            IWebElement PurchasePriceField = Browser.FindElement(By.XPath("//input[contains(@name,'meta_description[en]')]"));
            PurchasePriceField.Clear();
            PurchasePriceField.SendKeys("99,99");

            IWebElement CurrencyDropdown = Browser.FindElement(By.XPath("//option[contains(@value,'USD')]"));
            CurrencyDropdown.Click();

            IWebElement PriceUSDField = Browser.FindElement(By.XPath("//input[contains(@name,'prices[USD]')]"));
            PriceUSDField.Clear();
            PriceUSDField.SendKeys("10");

            IWebElement SaveButton = Browser.FindElement(By.XPath("//button[contains(@name,'save')]"));
            SaveButton.Click();

        }



         

    }
}
