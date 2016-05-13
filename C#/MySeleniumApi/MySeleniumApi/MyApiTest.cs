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
    public class MyApiTests : IClassFixture<TestFixture>
    {
        // class declarations
        private readonly TestFixture _fixture;

        public MyApiTests(TestFixture fixture)
        {
            // set test fixture
            this._fixture = fixture;
        }

        [Fact]
        public void TestElementExistsMethods()
        {
            // arrange

            // act
            _fixture.SeleniumApi.ElementExistsById("ID");

            _fixture.SeleniumApi.ElementExistsByXpath("//a[@href=\"www.google.com\"]");

            _fixture.SeleniumApi.ElementExistsByName("Name");

            // assert
            _fixture.Dispose();
        }

        [Fact]
        public void TestScrollToElementMethod()
        {
            // arrange
            _fixture.Driver.Navigate().GoToUrl("https://frontendfront.com/");
            IWebElement builtByElement = _fixture.Driver.FindElement(By.ClassName("built-by"));

            // act
            _fixture.SeleniumApi.ScrollToElement(builtByElement);

            // assert
            _fixture.Dispose();
        }

    }
}
