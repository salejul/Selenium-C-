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
        public void Test1() {

            this.NavigateTo("https://www.google.com");

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
