using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace MySeleniumApi.Api
{
    class SeleniumApi
    {
        // class fields
        private IWebDriver Driver;

        public SeleniumApi(IWebDriver driver)
        {
            this.Driver = driver;
        }

        /// <summary>
        /// Simple Boolean-returning function to both ensure
        /// an element exists in a test flow and catch
        /// the Selenium exception thrown in the event
        /// of that same element not existing
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boolean ElementExistsById(string id)
        {
            try
            {
                Driver.FindElement(By.Id(id));
            }
            catch (NoSuchElementException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Quick JavaScriptExecutor function to avoid
        /// test flow errors regarding web elements not
        /// being on screen, scrolling directly to the
        /// passed element
        /// </summary>
        /// <param name="element"></param>
        public void ScrollToElement(IWebElement element)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor) Driver;
            jse.ExecuteScript("arguments[0].scrollIntoView()", element);
        }

        static void Main(string[] args)
        {
        }
    }
}
