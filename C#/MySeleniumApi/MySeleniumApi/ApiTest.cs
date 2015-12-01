using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using Shouldly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySeleniumApi.Api;

namespace MySeleniumApi
{
    [TestFixture]
    public class ApiTest
    {
        // fields and driver init
        private IWebDriver Driver;
        private string driverResourceString = "C:\\Users\\Public\\Documents\\chromedriver_win32";

        // Api class declaration
        public SeleniumApi SeleniumApi;

        [TestFixtureSetUp]
        public void Init()
        {
            Driver = new ChromeDriver(driverResourceString);

            // Api class init
            this.SeleniumApi = new SeleniumApi(Driver);
        }

        /// <summary>
        /// Basic test to ensure working state of the Webdriver
        /// </summary>
        [Test]
        public void BasicDriverTest()
        {
            // arrange 
            Driver.Navigate().GoToUrl("http://www.google.com");

            var inputBox = Driver.FindElement(By.Id("lst-ib"));

            // act
            inputBox.Click();
            inputBox.SendKeys("Google");
            inputBox.SendKeys(Keys.Enter);

            // assert
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
