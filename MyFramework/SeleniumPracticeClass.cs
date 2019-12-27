using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyFramework
{
    [TestFixture]
    class SeleniumPracticeClass : SeleniumBaseClass
    {

        [Test]
        [Author("Aleksandar")]
        [Category("FirstFlight")]
        public void TodVachevDragAndDrop()
        {

            this.NavigateTo("http://testing.todvachev.com/draganddrop/draganddrop.html");
            IWebElement draggable = this.FindElement(By.CssSelector("#Drag1"));
            IWebElement droppable = this.FindElement(By.CssSelector("#Drag2"));

            Actions builder = new Actions(this.Driver);

            builder.ClickAndHold(draggable).MoveToElement(droppable).MoveByOffset(0, 10).Release().Build().Perform();


            DoWait(4);
        }



        [Test]
        [Author("Aleksandar")]
        [Category("FirstFlight")]
        public void HerokuAddRemoveElements()
        {
            this.NavigateTo("http://the-internet.herokuapp.com/add_remove_elements/");
            this.FindElement(By.XPath("//div[@class='example']/button[starts-with(text(), 'Add')]"))?.Click();
            this.FindElement(By.XPath("//div[@class='example']/button[starts-with(text(), 'Add')]"))?.Click();
            this.FindElement(By.XPath("//div[@class='example']/button[starts-with(text(), 'Add')]"))?.Click();
            var elementsToClick = this.FindElements(By.XPath("//div[@id='elements']//button"));
            foreach (var element in elementsToClick)
            {
                element.Click();
                //DoWait(2);
            }
            //DoWait(2);
        }

        [Test]
        [Author("Aleksandar")]
        [Category("FirstFlight")]
        public void HerokuAlerts() {

            this.NavigateTo("http://the-internet.herokuapp.com/javascript_alerts");

            //*************************
            //ClickForJsAlertButtonTest
            //*************************
            this.FindElement(By.XPath("//div[1]/ul[1]/li[1]/button[1]"))?.Click();
            IAlert alert1 = this.Driver.SwitchTo().Alert();
            DoWait(2);
            alert1.Accept();
            string alertResult = this.FindElement(By.XPath("//p[@id='result']")).Text;
            Assert.AreEqual(alertResult, "You successfuly clicked an alert", "Something went wrong!");

            //***********************
            //ClickForJsConfirmOKTest
            //***********************
            this.FindElement(By.XPath("//div[1]/ul[1]/li[2]/button[1]"))?.Click();
            IAlert alert2 = this.Driver.SwitchTo().Alert();
            DoWait(1);
            alert2.Accept();
            string confirmOKResult = this.FindElement(By.XPath("//p[@id='result']")).Text;
            Assert.AreEqual(confirmOKResult, "You clicked: Ok", "Something went wrong!");

            //***************************
            //ClickForJsConfirmCancelTest
            //***************************
            this.FindElement(By.XPath("//div[1]/ul[1]/li[2]/button[1]"))?.Click();
            IAlert alert3 = this.Driver.SwitchTo().Alert();
            DoWait(1);
            alert3.Dismiss();
            string confirmCancelResult = this.FindElement(By.XPath("//p[@id='result']")).Text;
            Assert.AreEqual(confirmCancelResult, "You clicked: Cancel", "Something went wrong!");

            //********************
            //ClickForJsPromptTest
            //********************
            this.FindElement(By.XPath("//div[1]/ul[1]/li[3]/button[1]"))?.Click();
            IAlert alert4 = this.Driver.SwitchTo().Alert();
            string keysToSend = "Hello there folks!";
            alert4.SendKeys(keysToSend);
            DoWait(4);
            alert4.Accept();
            string confirmKeysToSendResult = this.FindElement(By.XPath("//p[@id='result']")).Text;
            Assert.AreEqual(confirmKeysToSendResult, $"You entered: {keysToSend}", "Something went wrong!");
        }

        [Test]
        [Author("Aleksandar")]
        [Category("FirstFlight")]
        public void HerokuTypos()
        {
            this.NavigateTo("http://the-internet.herokuapp.com/typos");
            string paragraphToCkeck = this.FindElement(By.XPath("//div[1]/p[2]")).Text;

            if (paragraphToCkeck.Contains("won,t"))
                Assert.Fail();
            else
                Assert.Pass();
        }

        [Test]
        [Author("Aleksandar")]
        [Category("FirstFlight")]
        public void HerokuWindows() {

            this.NavigateTo("http://the-internet.herokuapp.com/windows");
            IWebElement dugmence = this.FindElement(By.XPath("//div[@class='example']/a"));
            //Console.WriteLine(dugmence.Displayed);
            //Console.WriteLine(dugmence.Enabled);
            //Console.WriteLine(dugmence.Size);
            //Console.WriteLine(dugmence.Text);
            //Console.WriteLine(dugmence.Location);
            //Console.WriteLine(dugmence.TagName);

            dugmence.Click();
            DoWait(1);
            var popup = this.Driver.WindowHandles[1];
            this.Driver.SwitchTo().Window(popup);
            DoWait(3);
            this.Driver.Close(); // Closes popup window
            DoWait(2);
            this.Driver.SwitchTo().Window(this.Driver.WindowHandles[0]);

        }

        [Test]
        [Author("Aleksandar")]
        [Category("FirstFlight")]
        public void SeleniumEasyMultiPopup()
        {

            this.NavigateTo("https://www.seleniumeasy.com/test/window-popup-modal-demo.html");
            IWebElement dugmence = this.FindElement(By.CssSelector("#followall"));
            dugmence.Click();
            DoWait(1);
            var popup3 = this.Driver.WindowHandles[3];
            this.Driver.SwitchTo().Window(popup3);
            DoWait(3);
            this.Driver.Close(); // Closes popup window
            DoWait(2);
            var popup2 = this.Driver.WindowHandles[2];
            this.Driver.SwitchTo().Window(popup2);
            DoWait(3);
            this.Driver.Close(); // Closes popup window
            DoWait(2);
            var popup1 = this.Driver.WindowHandles[1];
            this.Driver.SwitchTo().Window(popup1);
            DoWait(3);
            this.Driver.Close(); // Closes popup window
            this.Driver.SwitchTo().Window(this.Driver.WindowHandles[0]);

        }

        [Test]
        [Author("Aleksandar")]
        [Category("FirstFlight")]
        public void TwoInputFields() {

            this.NavigateTo("https://www.seleniumeasy.com/test/basic-first-form-demo.html");
            string keysToSend1 = "45";
            string keystoSend2 = "89";
            int result = Convert.ToInt32(keysToSend1) + Convert.ToInt32(keystoSend2);

            this.FindElement(By.CssSelector("#sum1"))?.SendKeys(keysToSend1);
            this.FindElement(By.CssSelector("#sum2"))?.SendKeys(keystoSend2);

            DoWait(4);

            //check if equal
            this.FindElement(By.XPath("//*[@id='gettotal']/button"))?.Click();
            DoWait(5);
            Assert.AreEqual($"Total a + b ={result}", $"Total a + b ={this.FindElement(By.CssSelector("#displayvalue")).Text}", "Something went wrong!");

        }

        [Test]
        [Author("Aleksandar")]
        [Category("FirstFlight")]
        public void RegistracijaQATodorovvv() {

            this.NavigateTo("http://qa.todorowww.net/register");
            this.FindElement(By.XPath("//input[@name='ime']"))?.SendKeys("Milovan");
            this.FindElement(By.XPath("//input[@name='prezime']"))?.SendKeys("Milovanovic");
            this.FindElement(By.XPath("//input[@name='korisnicko']"))?.SendKeys("MMTuts");
            this.FindElement(By.Id("password"))?.SendKeys("12345");
            this.FindElement(By.Id("passwordAgain"))?.SendKeys("12345");
            this.FindElement(By.XPath("//input[@id='pol_m']"))?.Click();
            var volim = this.FindElements(By.XPath("//input[@name='volim[]']"));
            foreach (var element in volim)
            {
                element.Click();
            }
            this.FindElement(By.XPath("//input[@id='passwordOptional']"))?.SendKeys("54321");
            this.FindElement(By.XPath("//input[@name='grad']"))?.SendKeys("NIS");
            IWebElement selektujZemlju = this.FindElement(By.Name("zemlja"));
            var select = new SelectElement(selektujZemlju);
            select.SelectByText("Makedonija");
            this.FindElement(By.CssSelector("#eyecolor"))?.SendKeys("plava");
            this.FindElement(By.CssSelector("#haircolor"))?.SendKeys("crna");
            IWebElement selektujVisinu = this.FindElement(By.Name("visina"));
            var select1 = new SelectElement(selektujVisinu);
            select1.SelectByValue("180-190");
            IWebElement selektujTezinu = this.FindElement(By.Name("tezina"));
            var select2 = new SelectElement(selektujTezinu);
            select2.SelectByValue("80-100");
            this.FindElement(By.XPath("//textarea[@id='intro']"))?.SendKeys("Ovo je kratak uvod o meni");
            this.FindElement(By.XPath("//textarea[@id='aboutme']"))?.SendKeys("Par recenica o meni.");
            this.FindElement(By.XPath("//input[@name='contactmodern' and @value='sms']"))?.Click();
            IWebElement selektujDestinaciju = this.FindElement(By.Name("summer"));
            var select3 = new SelectElement(selektujDestinaciju);
            select3.SelectByValue("usa-fl");
            DoWait(4);
            this.FindElement(By.XPath("//input[@name='register']"))?.Submit();

            string successMessage = this.FindElement(By.XPath("//div[@class='alert alert-success']")).Text;

            if (successMessage.Contains("Uspešno"))
                Assert.Pass("Success!");
            else
                Assert.Fail("Failure!!!!!");

        }

        [Test]
        [Category("Cas34")]
        [Author("Aleksandar")]
        public void EmmiTest() {

            this.NavigateTo("https://www.emmi.rs/");
            this.FindElement(By.XPath("//a[@title='Monitori']"))?.Click();
            IWebElement proizvodjac = this.FindElement(By.XPath("//select[@name='brandId']"));
            var selektujProizvodjaca = new SelectElement(proizvodjac);
            selektujProizvodjaca.SelectByValue("448");
            IWebElement tip = this.FindElement(By.XPath("//select[@name='tip']"));
            var selektujTip = new SelectElement(tip);
            selektujTip.SelectByValue("34878");
            this.FindElement(By.XPath("//input[@class='categoriesListButton']"))?.Click();

            this.FindElement(By.XPath("//div[5]/div[1]/div[2]/div[1]//a[contains(text(),'HP OMEN')]"))?.Click();

            IWebElement price = this.FindElement(By.XPath("//div[@class='price']"));

            Console.WriteLine(price.Text);

            string actualPrice = price.Text.Trim();

            Console.WriteLine(actualPrice);


            if (actualPrice == "29.990")
                Assert.Pass("Test Prosao");
            else
                Assert.Fail("Test nije prosao");

        }

        [Test]
        [Category("Cas34")]
        [Author("Aleksandar")]
        public void OrderYourBugsTodaySMALL() {

            //sad path test should fail
            this.NavigateTo("http://shop.qa.rs/");

            IWebElement order = this.FindElement(By.CssSelector("div.container:nth-child(2) div.row:nth-child(8) div.col-sm-3.text-center:nth-child(3) div.panel.panel-warning div.panel-body.text-justify form:nth-child(1) p.text-center:nth-child(4) > select:nth-child(2)"));
            var chooseQuantity = new SelectElement(order);
            chooseQuantity.SelectByValue("6");
            DoWait(2);
            int itemPrice = Convert.ToInt32(this.FindElement(By.XPath("//h3[contains(text(),'SMALL')]/parent::div/following-sibling::div[2]")).Text.Substring(1));
            this.FindElement(By.CssSelector("div.container:nth-child(2) div.row:nth-child(8) div.col-sm-3.text-center:nth-child(3) div.panel.panel-warning div.panel-body.text-justify form:nth-child(1) p.text-center.margin-top-20:nth-child(5) > input.btn.btn-primary"))?.Click();
            DoWait(5);

            int quantity = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[1]/td[2]")).Text);
            int pricePerItem = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[1]/td[3]")).Text.Substring(1));
            int totalItemPrice = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[1]/td[4]")).Text.Substring(1));
            int shipping = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[2]/td[3]")).Text.Trim().Substring(1));
            int total = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[3]/td[1]")).Text.Substring(8));

            int expectedValue = (quantity * itemPrice) + shipping;
            int actualValue = (quantity * pricePerItem) + shipping;

            if (expectedValue == total)
                Assert.Pass("Test Passed!");
            else
                Assert.Fail($"Test Failed! Value is {actualValue}$ but should be {expectedValue}$.");
             

            //Console.WriteLine(itemPrice);


        }

        [Test]
        [Category("Cas34")]
        [Author("Aleksandar")]
        public void OrderYourBugsTodayENTERPRISE()
        {

            //sad path test should fail
            this.NavigateTo("http://shop.qa.rs/");

            IWebElement order = this.FindElement(By.CssSelector("div.container:nth-child(2) div.row:nth-child(8) div.col-sm-3.text-center:nth-child(5) div.panel.panel-success div.panel-body.text-justify form:nth-child(1) p.text-center:nth-child(4) > select:nth-child(2)"));
            var chooseQuantity = new SelectElement(order);
            chooseQuantity.SelectByValue("6");
            DoWait(2);
            int itemPrice = Convert.ToInt32(this.FindElement(By.XPath("//h3[contains(text(),'ENTERPRISE')]/parent::div/following-sibling::div[2]")).Text.Remove(4,1));
            Console.WriteLine(itemPrice);
            this.FindElement(By.CssSelector("div.container:nth-child(2) div.row:nth-child(8) div.col-sm-3.text-center:nth-child(5) div.panel.panel-success div.panel-body.text-justify form:nth-child(1) p.text-center.margin-top-20:nth-child(5) > input.btn.btn-default"))?.Click();
            DoWait(5);

            //TrimEnd('','!').Trim()

            int quantity = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[1]/td[2]")).Text);
            int pricePerItem = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[1]/td[3]")).Text.Substring(1));
            int totalItemPrice = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[1]/td[4]")).Text.Substring(1));
            int shipping = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[2]/td[3]")).Text.Trim().Substring(1));
            int total = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[3]/td[1]")).Text.Substring(8));

            int expectedValue = (quantity * itemPrice) + shipping;
            int actualValue = (quantity * pricePerItem) + shipping;

            if (expectedValue == total)
                Assert.Pass("Test Passed!");
            else
                Assert.Fail($"Test Failed! Value is {actualValue}$ but should be {expectedValue}$.");

            //Console.WriteLine(itemPrice);
        }

        [SetUp]
        public void SetUpTests()
        {
            //this.Driver = new FirefoxDriver();
            this.Driver = new ChromeDriver();
            this.Driver.Manage().Window.Maximize();
            this.Wait = 3;
        }

        [TearDown]
        public void TearDownTests()
        {
            this.Close();
        }
    }
}
