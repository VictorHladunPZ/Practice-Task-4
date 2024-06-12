using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Task_4.Page_Objects
{
    public class LoginPageObject : BasePageObject
    {
        protected WebDriver driver;
        private readonly string Email; // The login
        private readonly string psw; // The password
        private const string testUrl = "https://mail.google.com/";

        public LoginPageObject(WebDriver driver, string Email, string psw)
        {
            this.driver = driver;
            this.Email = Email;
            this.psw = psw;
        }
        public bool Login()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            // Login input
            var xpath = "//*[@id=\"identifierId\"]";
            driver.Navigate().GoToUrl(testUrl);
            try
            {
                InsertIntoTextField(driver, xpath, Email);

                // Next button
                xpath = "//*[@id=\"identifierNext\"]";

                ClickElement(driver, xpath);
                
                // The 'empty login' field. Is not re-checked for password, because I am lazy. You may want to fix this.
                xpath = "/html/body/div[1]/div[1]/div[2]/c-wiz/div/div[2]/div/div/div[1]/form/span/section/div/div/div[1]/div/div[2]/div[2]/div";

                if (XPathElementExist(driver, xpath))
                {
                    return false;
                }
               
                // Password input field
                xpath = "/html/body/div[1]/div[1]/div[2]/c-wiz/div/div[2]/div/div/div/form/span/section[2]/div/div/div[1]/div[1]/div/div/div/div/div[1]/div/div[1]/input";
                
                // Here we wait for the password input to be interactable
                var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
                var element = wait.Until(SeleniumExtras.WaitHelpers.
                    ExpectedConditions.ElementToBeClickable(By.XPath(xpath)));

                InsertIntoTextField(driver, xpath, psw);
                // Next , and essentially login, button
                // Be aware that sometimes it bugs out and is non-interactable or stale. I do not know what causes the issue.
                xpath = "/html/body/div[1]/div[1]/div[2]/c-wiz/div/div[3]/div/div[1]/div/div/button/span";
                ClickElement(driver, xpath);

                
                return driver.PageSource.Contains("Inbox");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}
