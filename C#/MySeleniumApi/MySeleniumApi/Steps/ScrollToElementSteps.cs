using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySeleniumApi.Api;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace MySeleniumApi.Tests.Steps
{
    [Binding]
    public class ScrollToElementSteps
    {
        private IWebDriver _driver;

        private IWebElement _builtByElement;

        private SeleniumApi _seleniumApi;

        [Given(@"I am on the stated application screen")]
        public void GivenIAmOnTheStatedApplicationScreen()
        {
            _driver = new FirefoxDriver();

            _driver.Manage().Window.Maximize();

            _driver.Navigate().GoToUrl("https://frontendfront.com/");
        }

        [Given(@"I call my scrolling function on the element name (.*) used by the test")]
        public void GivenICallMyScrollingFunctionOnTheElementUsedByTheTest(string elementName)
        {
            _seleniumApi = new SeleniumApi(_driver);

            _builtByElement = _driver.FindElement(By.ClassName(elementName));

            _seleniumApi.ScrollToElement(_builtByElement);
        }

        [Then(@"I should be able to utilize the element without an exception")]
        public void ThenIShouldBeAbleToFindTheElementWithoutAnException()
        {
            Assert.AreEqual("built by Stelian & Sergiu", _builtByElement.Text);
        }

        [AfterScenario]
        public void DisposeWebDriver()
        {
            _driver.Dispose();
        }
    }
}
