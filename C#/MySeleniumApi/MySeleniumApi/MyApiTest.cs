using System;
using MySeleniumApi.Api;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace MySeleniumApi
{
    /// <summary>
    /// Xunit class fixture, implementing disposable interface for
    /// webdriver cleanup
    /// </summary>
    public class TestFixture : IDisposable
    {
        // class fields
        public IWebDriver Driver;
        public SeleniumApi SeleniumApi;

        /// <summary>
        /// Class constructor
        /// </summary>
        public TestFixture()
        {
            Driver = new FirefoxDriver();
            Driver.Manage().Window.Maximize();

            SeleniumApi = new SeleniumApi(Driver);
        }

        /// <summary>
        /// Quit and Dispose of WebDriver resources at the end of a test run
        /// </summary>
        public void Dispose()
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }

    /// <summary>
    /// Test class
    /// </summary>
    public class MyApiTests : TestFixture
    {
        [Fact]
        public void TestElementExistsMethods()
        {
            // arrange

            // act
            SeleniumApi.ElementExistsById("ID");

            SeleniumApi.ElementExistsByXpath("//a[@href=\"www.google.com\"]");

            SeleniumApi.ElementExistsByName("Name");

            // assert
            Dispose();
        }

        [Fact]
        public void TestScrollToElementMethod()
        {
            // arrange
            Driver.Navigate().GoToUrl("https://frontendfront.com/");
            IWebElement builtByElement = Driver.FindElement(By.ClassName("built-by"));

            // act
            SeleniumApi.ScrollToElement(builtByElement);

            // assert
            Dispose();
        }

    }
}
