using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Task_4.Page_Objects
{
    /// <summary>
    ///  Class inherited by all other Page Objects. Provides basic functions for interaction with the page
    /// </summary>
    public class BasePageObject
    {
        protected BasePageObject()
        {

        }
        /// <summary>
        ///  Attempts to find an element by its XPath, and then, if successfull, type value into it.Throws a "NoSuchElementException" if didn't find it.
        /// </summary>
        /// <param name="XPath">The element you want to find</param>
        /// <param name="value">The input text</param>
        protected static void InsertIntoTextField(IWebDriver driver, string XPath, string value)
        {
            IWebElement currWebElement;
            if (XPathElementExist(driver, XPath))
            {
                currWebElement = driver.FindElement(By.XPath(XPath));
                currWebElement.SendKeys(value);

            }
            else
            {
                throw new NoSuchElementException("Did not find element by XPath " + XPath);
            }
        }
        /// <summary>
        ///  Attempts to find an element by its XPath, and then, if successfull, click it. Throws a "NoSuchElementException" if didn't find it.
        /// </summary>
        /// <param name="XPath">The element you want to find</param>
        protected static void ClickElement(IWebDriver driver, string XPath)
        {
            IWebElement currWebElement;
            if (XPathElementExist(driver, XPath))
            {
                currWebElement = driver.FindElement(By.XPath(XPath));
                currWebElement.Click();
            }
            else
            {
                throw new NoSuchElementException("Did not find element by XPath " + XPath);
            }
        }
        /// <summary>
        ///  Attempts to find an element by its XPath, and then, if successfull, return true.
        /// </summary>
        protected static bool XPathElementExist(IWebDriver driver, string path)
        {
            try
            {
                driver.FindElement(By.XPath(path));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
