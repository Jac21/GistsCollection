using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using Shouldly;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MySeleniumApi
{
    [TestFixture]
    public class ApiTest
    {
        // fields and driver init
        private IWebDriver Driver;
        private string driverResourceString = "C:\\Users\\Jeremy_Cantu\\Documents\\Selenium";

        [TestFixtureSetUp]
        public void Init()
        {
            Driver = new ChromeDriver(driverResourceString);
        }

        [Test]
        public void BasicDriverTest()
        {
            // arrange 
            Driver.Navigate().GoToUrl("http://www.google.com");

            var inputBox = Driver.FindElement(By.Id("lst-ib"));

            // act
            inputBox.SendKeys("Google");
            inputBox.SendKeys(Keys.Enter);

            // assert
            var infoPaneHeader = Driver.FindElement(By.XPath("//*[@id=\"rhs_block\"]/ol/li/div[1]/div/div[1]/ol/div[2]/div/div[1]"));
            infoPaneHeader.Text.ShouldBe("Google");
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
