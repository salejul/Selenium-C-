using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace MyFramework
{
    class Tests : SeleniumBaseClass
    {
        private readonly FileManagement log = new FileManagement();

        public Tests()
        {
            this.log.FileName = @"C:\Test\logger.txt";
        }

        [Test]
        [Category("First Class")]
        public void PerformGoogleSearch()
        {
            //this.log.Store("*** PerformGoogleSearch ***");
            this.NavigateTo("http://www.google.rs/");
            IWebElement el = this.FindElement(By.Name("q"));
            try
            {
                this.SendKeys("Selenium C# Helpers", true, el);
            }
            catch (Exception e)
            {
                this.log.Store($"Exception <{e.Message}>");
                Assert.Fail("Couldn't send keys, element not defined.");
                return;
            }
            Assert.Pass("Keys sent");
            this.DoWait(2);
        }

        [Test]
        [Category("First Class")]
        public void SelectHawaii()
        {
            string selectByText = "Havaji, SAD";
            //this.log.Store("*** SelectHawaii ***");
            this.NavigateTo("http://qa.todorowww.net/register");
            IWebElement el = this.FindElement(By.Name("summer"));
            var select = new SelectElement(el);
            select.SelectByText(selectByText);
            this.DoWait(2);
            string selectedItem = select.SelectedOption.Text;
            Assert.AreEqual(selectedItem, selectByText, "Selected item doesn't match required.");
        }

        [Test]
        [Category("First Class")]
        public void ClickOnRadioButtons()
        {
            //this.log.Store("*** ClickOnRadioButtons ***");
            this.NavigateTo("http://qa.todorowww.net/register");
            var elements = this.FindElements(By.XPath("//input[@type='radio']"));
            foreach (var el in elements)
            {
                el.Click();
                this.DoWait(1);
            }
            this.DoWait(1);
            IWebElement requiredElement = this.FindElement(By.XPath("//input[@type='radio' and @name='contactmodern' and @value='sms']"));
            string isChecked = requiredElement.GetAttribute("checked");
            if (isChecked == null)
            {
                Assert.Fail("Required radio button is not checked");
            }
            else
            {
                Assert.True(isChecked.ToLower().Equals("true"), "Something went wrong.");
            }
        }

        [Test]
        [Category("First Class")]
        public void CheckAllVolimCheckboxes()
        {
            //this.log.Store("*** CheckAllVolimCheckboxes ***");
            this.NavigateTo("http://qa.todorowww.net/register");
            var elements = this.FindElements(By.Name("volim[]"));
            foreach (var el in elements)
            {
                el.Click();
                this.DoWait(1);
            }
            this.DoWait(1);
            IWebElement requiredElement = this.FindElement(By.XPath("//input[@name='volim[]' and @value='paradajz']"));
            Assert.True(requiredElement.Selected, "Required checkbox is not checked");
            this.log.Store($"{requiredElement.Selected} is really selected");
        }

        [Test]
        [Category("First Class")]
        public void ExtractYearsFromTable()
        {
            this.log.Store("*** ExtractYearsFromTable ***");
            this.NavigateTo("https://www.toolsqa.com/automation-practice-table/");
            IWebElement table = this.FindElement(By.ClassName("tsc_table_s13"));
            var columns = FindElements(By.XPath("//tbody/tr/td[4]"), table);
            List<string> expectedYears = new List<string>(new string[] { "2010", "2012", "2004", "2008" });
            bool allPass = true;
            int counter = 0;
            foreach (var col in columns)
            {
                if (expectedYears[counter] != col.Text)
                {
                    this.log.Store($"expectedYears[{counter}] = {expectedYears[counter]}, got {col.Text}");
                    allPass = false;
                    break;
                }
                else
                {
                    this.log.Store("All good.");
                }
                counter++;
            }
            Assert.True(allPass, "Some of the values are not the same.");
        }

        [Test]
        [Category("Cas32")]
        public void TestDragAndDrop()
        {
            // Doesn't work
            this.NavigateTo("https://formy-project.herokuapp.com/dragdrop");
            IWebElement target = this.FindElement(By.CssSelector("#box"));
            IWebElement drop1 = this.FindElement(By.Id("image"));
            var action = new Actions(this.Driver);
            
            action.ClickAndHold(drop1);
            action.MoveToElement(target);
            action.MoveByOffset(-15, 274);
            action.Release(target);
            action.Build();
            action.Perform();


            //IWebElement image = this.FindElement(By.Id("image"));
            //IWebElement target = this.FindElement(By.Id("box"));
            //Actions builder1 = new Actions(this.Driver);
            //IAction dragAndDrop1 = builder1.ClickAndHold(image).MoveToElement(target).Release(target).Build();
            //dragAndDrop1.Perform();


            DoWait(2);
        }

        [Test]
        [Category("Cas33")]
        public void Test_TESTQA()
        {

            this.NavigateTo("http://test.qa.rs");
            var findBy = By.XPath("//a[contains(text(),'Kreiraj novog korisnika')]");
            var wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(20));
            wait.Until(drv => drv.FindElement(findBy));
            IWebElement link = this.FindElement(findBy);
            link.Click();

            IWebElement ime = this.FindElement(By.XPath("//input[@name='ime']"));
            ime.SendKeys("Marko");

            IWebElement prezime = this.FindElement(By.XPath("//input[@name='prezime']"));
            prezime.SendKeys("Markovic");

            IWebElement korisnicko = this.FindElement(By.XPath("//input[@name='korisnicko']"));
            korisnicko.SendKeys("MMarkovic");

            IWebElement email = this.FindElement(By.XPath("//input[@name='email']"));
            email.SendKeys("markovic@gmail.com");

            IWebElement telefon = this.FindElement(By.XPath("//input[@name='telefon']"));
            telefon.SendKeys("06111001100");

            IWebElement izaberiZemlju = this.FindElement(By.Name("zemlja"));
            var select = new SelectElement(izaberiZemlju);
            select.SelectByValue("srb");

            var findBy2 = By.XPath("//select[@id='grad']");
            var wait2 = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(20));
            wait2.Until(drv => drv.FindElement(findBy2));

            IWebElement izaberiGrad = this.FindElement(findBy2);
            var selectgrad = new SelectElement(izaberiGrad);
            selectgrad.SelectByValue("Niš");

            IWebElement ulica = this.FindElement(By.XPath("//form[1]/div[8]/div[2]/input[1]"));
            ulica.SendKeys("Neznanog Junaka 2");

            IWebElement pol = this.FindElement(By.XPath("//input[@id='pol_m']"));
            pol.Click();

            IWebElement cekbox1 = this.FindElement(By.Id("newsletter"));
            cekbox1.Click();

            IWebElement cekbox2 = this.FindElement(By.Id("promotions"));
            cekbox2.Click();

            IWebElement button = this.FindElement(By.XPath("//input[@name='register']"));
            button.Submit();

            IWebElement uspeh = this.FindElement(By.XPath("//div[@class='alert alert-success']"));
            Assert.True(uspeh.Displayed);
            this.DoWait(3);
        }

        [Test, Category("Cas32")]
        public void TestRightClickContextMenu()
        {
            this.NavigateTo("https://www.cssscript.com/demo/lightweight-context-menu-library-class2context-js/");
            var action = new Actions(this.Driver);
            IWebElement div = this.FindElement(By.XPath("//div[contains(@class, 'class1')]"));
            action.ContextClick(div).Perform();
            DoWait(1);
            IWebElement a1 = this.FindElement(By.XPath("//a[contains(., 'A1')]"));
            a1.Click();
            DoWait(2);
            try
            {
                var alert = this.Driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch (NoAlertPresentException e)
            {
                // Do something
            }
            DoWait(2);
        }

        [Test, Category("Cas33")]
        public void TestScrollTo()
        {
            // Works in Chrome, doesn't work in Firefox
            this.NavigateTo("https://www.toolsqa.com/automation-practice-form/");
            DoWait(2);
            IWebElement continents = this.FindElement(By.Id("continents"));
            var action = new Actions(this.Driver);
            action.MoveToElement(continents).Perform();
            DoWait(2);
        }

        [Test]
        [Category("Cas33")]

        public void TestVezba()
        {
            
            this.NavigateTo("http://test.qa.rs");
            var findBy = By.XPath("//a[contains(text(),'Kreiraj novog korisnika')]");
            var wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(20));
            wait.Until(drv => drv.FindElement(findBy));
            IWebElement link = this.FindElement(findBy);
            link.Click();

            IWebElement ime = this.FindElement(By.XPath("//input[@name='ime']"));
            ime.SendKeys("Marko");

            IWebElement prezime = this.FindElement(By.XPath("//input[@name='prezime']"));
            prezime.SendKeys("Markovic");

            IWebElement korisnicko = this.FindElement(By.XPath("//input[@name='korisnicko']"));
            korisnicko.SendKeys("MMarkovic");

            IWebElement email = this.FindElement(By.XPath("//input[@name='email']"));
            email.SendKeys("markovic@gmail.com");

            IWebElement telefon = this.FindElement(By.XPath("//input[@name='telefon']"));
            telefon.SendKeys("06111001100");

            IWebElement izaberiZemlju = this.FindElement(By.Name("zemlja"));
            var select = new SelectElement(izaberiZemlju);
            select.SelectByValue("srb");

            var findBy2 = By.XPath("//select[@id='grad']");
            var wait2 = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(20));
            wait2.Until(drv => drv.FindElement(findBy2));

            IWebElement izaberiGrad = this.FindElement(findBy2);
            var selectgrad = new SelectElement(izaberiGrad);
            selectgrad.SelectByValue("Niš");

            IWebElement ulica = this.FindElement(By.XPath("//form[1]/div[8]/div[2]/input[1]"));
            ulica.SendKeys("Neznanog Junaka 2");

            IWebElement pol = this.FindElement(By.XPath("//input[@id='pol_m']"));
            pol.Click();

            IWebElement cekbox1 = this.FindElement(By.Id("newsletter"));
            cekbox1.Click();

            IWebElement cekbox2 = this.FindElement(By.Id("promotions"));
            cekbox2.Click();

            IWebElement button = this.FindElement(By.XPath("//input[@name='register']"));
            button.Submit();


        }

        [Test]
        [Category("Cas33")]
        public void ShoppingCart()
        {

            this.NavigateTo("http://shop.qa.rs/");

            IWebElement vrednost = this.FindElement(By.CssSelector("div.container:nth-child(2) div.row:nth-child(8) div.col-sm-3.text-center:nth-child(4) div.panel.panel-info div.panel-body.text-justify form:nth-child(1) p.text-center:nth-child(4) > select:nth-child(2)"));
            var desetka = new SelectElement(vrednost);
            desetka.SelectByValue("10");

            IWebElement order = this.FindElement(By.CssSelector("div.container:nth-child(2) div.row:nth-child(8) div.col-sm-3.text-center:nth-child(4) div.panel.panel-info div.panel-body.text-justify form:nth-child(1) p.text-center.margin-top-20:nth-child(5) > input.btn.btn-primary"));
            order.Click();

            int quantity = Convert.ToInt32((this.FindElement(By.XPath("//tr[1]//td[2]")).Text));
            int pricePerItem = Convert.ToInt32(this.FindElement(By.XPath("//tr[1]//td[3]")).Text.Substring(1));


            Assert.AreEqual(quantity, 10, "Desired quantity doesn't match required citeria");
            Assert.AreEqual(pricePerItem, 800, "Desired price per item doesn't match required citeria");

            int totalItemPriceInt = quantity * pricePerItem;
            int totalPricePerItem = Convert.ToInt32(this.FindElement(By.XPath("//tr[3]//td[1]")).Text.Substring(8));

            DoWait(4);

            if (totalItemPriceInt != totalPricePerItem)
                Assert.Fail();
            else
                Assert.Pass();

        }

        [Test]
        [Category("Cas33")]
        public void PopupModal()
        {
            this.NavigateTo("https://www.seleniumeasy.com/test/window-popup-modal-demo.html");
            IWebElement button1 = this.FindElement(By.XPath("//div[@class='two-windows']/a"));
            //IWebElement button1 = this.FindElement(By.XPath("//a[contains(text(),'Follow Twitter & Facebook')]"));
            button1.Click();
            DoWait(1);
            var mainWindow = this.Driver.CurrentWindowHandle;

            foreach (var windowHandle in this.Driver.WindowHandles)
            {
                this.Driver.SwitchTo().Window(windowHandle);
                string title = this.Driver.Title;
                if (title.Contains("Twitter"))
                {
                    IWebElement username1 = this.FindElement(By.Id("username_or_email"));
                    username1.SendKeys("mail1@gmail.com");
                    DoWait(1);
                    IWebElement password1 = this.FindElement(By.XPath("//input[@id='password']"));
                    password1.SendKeys("sifra1");
                    DoWait(1);
                    this.Driver.Close();
                }
                if (title.Contains("Facebook"))
                {
                    IWebElement username2 = this.FindElement(By.CssSelector("#email"));
                    username2.SendKeys("mail2@gmail.com");
                    DoWait(1);
                    IWebElement password2 = this.FindElement(By.CssSelector("#pass"));
                    password2.SendKeys("sifra2");
                    DoWait(1);
                    this.Driver.Close();
                }
            }
            this.Driver.SwitchTo().Window(mainWindow);
        }


        [SetUp]
        public void SetUpTests()
        {
            //this.Driver = new FirefoxDriver();
            this.Driver = new ChromeDriver();
            this.Wait = 3;
        }

        [TearDown]
        public void TearDownTests()
        {
            this.Close();
        }
    }
}