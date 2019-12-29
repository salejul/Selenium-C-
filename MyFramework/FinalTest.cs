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
    class FinalTest : SeleniumBaseClass
    {
        [Test]
        [Category("Final Test")]
        [Author("Aleksandar Julovski")]
        public void Zadatak1() {

            this.NavigateTo("http://test5.qa.rs");
            this.FindElement(By.XPath("//a[contains(text(),'Register')]"))?.Click();
            this.FindElement(By.XPath("//input[@name='ime']"))?.SendKeys("Aleksandar");
            this.FindElement(By.XPath("//input[@name='prezime']"))?.SendKeys("Julovski");
            this.FindElement(By.XPath("//input[@name='email']"))?.SendKeys("nekimail@example.com");
            this.FindElement(By.XPath("//input[@name='korisnicko']"))?.SendKeys("Sale");
            this.FindElement(By.XPath("//input[@id='password']"))?.SendKeys("12345");
            this.FindElement(By.XPath("//input[@id='passwordAgain']"))?.SendKeys("12345");
            this.FindElement(By.XPath("//input[@name='register']"))?.Click();
            IWebElement uspeh = this.FindElement(By.XPath("//div[@class='alert alert-success']"));
            Assert.True(uspeh.Displayed);
            DoWait(3);

        }

        [Test]
        [Category("Final Test")]
        [Author("Aleksandar Julovski")]
        public void Zadatak2()
        {
            this.NavigateTo("http://test5.qa.rs");
            this.FindElement(By.XPath("//a[contains(text(),'Login')]"))?.Click();
            DoWait(3);
            this.FindElement(By.CssSelector("body:nth-child(2) div.wrapper:nth-child(2) form.form-signin > input.form-control:nth-child(2)"))?.SendKeys("Sale");
            this.FindElement(By.CssSelector("body:nth-child(2) div.wrapper:nth-child(2) form.form-signin > input.form-control:nth-child(3)"))?.SendKeys("12345");
            this.FindElement(By.XPath("//input[@name='login']"))?.Click();
            DoWait(5);
            var greetMessage = this.FindElement(By.XPath("//div[3]/div[1]/h2[1]"));
            if (greetMessage.Text.Contains("Welcome"))
                Assert.Pass("You have successfully logged into a system.");
            else
                Assert.Fail("Error!");

        }
        [Test]
        [Category("Final Test")]
        [Author("Aleksandar Julovski")]
        public void Zadatak3()
        {
            this.NavigateTo("http://test5.qa.rs");
            this.Driver.Manage().Cookies.DeleteAllCookies();
            this.FindElement(By.XPath("//a[contains(text(),'Login')]"))?.Click();
            DoWait(3);
            this.FindElement(By.CssSelector("body:nth-child(2) div.wrapper:nth-child(2) form.form-signin > input.form-control:nth-child(2)"))?.SendKeys("Sale");
            this.FindElement(By.CssSelector("body:nth-child(2) div.wrapper:nth-child(2) form.form-signin > input.form-control:nth-child(3)"))?.SendKeys("12345");
            this.FindElement(By.XPath("//input[@name='login']"))?.Click();
            DoWait(1);

            IWebElement order1 = this.FindElement(By.XPath("//div[@class='panel panel-warning']//select[@name='quantity']"));
            var chooseFirstProduct = new SelectElement(order1);
            chooseFirstProduct.SelectByValue("6");
            //int itemPrice1 = Convert.ToInt32(this.FindElement(By.XPath("//h3[contains(text(),'small')]/parent::div/following-sibling::div[2]")).Text.Substring(1));
            //int itemPrice2 = Convert.ToInt32(this.FindElement(By.XPath("//h3[contains(text(),'small')]/parent::div/following-sibling::div[2]")).Text.Substring(1));
            this.FindElement(By.XPath("//h3[contains(text(),'small')]/parent::div/following-sibling::div[1]//input[@type='submit']"))?.Click();
            this.FindElement(By.XPath("//a[@class='btn btn-default']"))?.Click();
            DoWait(1);

            IWebElement order2 = this.FindElement(By.XPath("//div[@class='panel panel-info']//select[@name='quantity']"));
            var chooseSecondProduct = new SelectElement(order2);
            chooseSecondProduct.SelectByValue("5");
            this.FindElement(By.XPath("//h3[contains(text(),'pro')]/parent::div/following-sibling::div[1]//input[@type='submit']"))?.Click();

            DoWait(1);
            int quantity1 = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[1]/td[2]")).Text);
            int quantity2 = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[2]/td[2]")).Text);
            
            int pricePerItem1 = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[1]/td[3]")).Text.Substring(1));
            int pricePerItem2 = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[2]/td[3]")).Text.Substring(1));


            int totalItemPrice1 = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[1]/td[4]")).Text.Substring(1));
            int totalItemPrice2 = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[2]/td[4]")).Text.Substring(1));
            
            int total = Convert.ToInt32(this.FindElement(By.XPath("//table[1]/tbody[1]/tr[4]/td[1]")).Text.Trim().Substring(8));

            int cartSummary = (quantity1 * pricePerItem1) + (quantity2 * pricePerItem2);
            //Console.WriteLine(cartSummary);

            this.FindElement(By.XPath("//input[@name='checkout']"))?.Click();

            var poruka = this.FindElement(By.XPath("//div[1]/h3[1]"));

            DoWait(4);

            if (poruka.Text == $"Your credit card has been charged with the amount of ${total}" && total == cartSummary)
                Assert.Pass();
            else
                Assert.Fail("Error!");
                

        }



        [SetUp]
        public void SetUpTests()
        {
            this.Driver = new FirefoxDriver();
            //this.Driver = new ChromeDriver();
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
