using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Task_4.Page_Objects
{
    public class HomePageObject : BasePageObject
    {
        protected WebDriver driver;
        protected string Recipient; // To whom you write the letter
        protected string Theme; // The Theme
        protected string Msg; // The text
        public HomePageObject(HomePageObject homePageObject, string sender)
        {
            this.Recipient = sender;
            this.Theme = homePageObject.Theme;
            this.Msg = homePageObject.Msg;
            this.driver = homePageObject.driver;
        }
        public HomePageObject(WebDriver driver, string recipient, string theme)
        {
            this.driver = driver;
            Recipient = recipient;
            Theme = theme;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            //Randomly generated text as per task 
            Msg = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            // 'Unique' theme allows you to quickly identify if the letter has been sent
            Theme += new string(Enumerable.Repeat(chars, 2)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        //The supression is only cosmetic.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1854:Unused assignments should be removed", Justification = "<Pending>")]
        public bool WriteLetter()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            try
            {
                ClickWriteNew();
                WriteLetterContent();
                // The message box that appears when the message is empty or the recipient is empty
                var xpath = "/html/body/div[52]/div[2]/div";
                // Used for unit tests, basically
                if (XPathElementExist(driver, xpath))
                {
                    return false;
                }

                // Sent letters page
                xpath = "//*[@id=\":3x\"]";
                ClickElement(driver, xpath);

                // Loading 
                Thread.Sleep(1500);

                // Check if letter is sent
                // Currently, it utilizes the semi-unique Theme field. You may want to change this
                if (driver.PageSource.Contains(Theme))
                {
                    // Console output is only for debugging purposes
                    Console.WriteLine($"Text \"{Theme}\" is present on the screen. Letter has been sent successfully");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Text \"{Theme}\" is NOT present on the screen. Letter has NOT been sent.");
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        private void ClickWriteNew()
        {
            string composePath = "/html/body/div[7]/div[3]/div/div[2]/div[1]/div[1]/div/div";
            ClickElement(driver, composePath);
        }

        private void WriteLetterContent()
        {
            // New letter form
            string newLetterPath = "//*[@class='AD']";
            var newLetter = driver.FindElement(By.XPath(newLetterPath));

            // Container for better access
            string subjectRecipientContainerPath = "./div/div/div[3]/div/div/div[4]/table/tbody/tr/td[2]/form";
            var subjectRecipientContainer = newLetter.FindElement(By.XPath(subjectRecipientContainerPath));

            // Recipient
            string recipientPath = "./div[1]/table/tbody/tr[1]/td[2]/div/div/div[1]/div/div[3]/div/div/div/div/div/input";
            var recipient = subjectRecipientContainer.FindElement(By.XPath(recipientPath));
            recipient.SendKeys(Recipient);

            // Theme
            string subjectPath = "./div[3]/input";
            var subject = subjectRecipientContainer.FindElement(By.XPath(subjectPath));
            subject.SendKeys(Theme);

            // Content
            string letterContentPath = "./div/div/div[3]/div/div/div[4]/table/tbody/tr/td[2]/table/tbody/tr[1]/td/div/div[1]/div[2]/div[3]/div/table/tbody/tr/td[2]/div[2]/div/div[1]";
            var letterContent = newLetter.FindElement(By.XPath(letterContentPath));
            letterContent.SendKeys(Msg);

            // Send button
            string sendPath = "./div/div/div[3]/div/div/div[4]/table/tbody/tr/td[2]/table/tbody/tr[2]/td/div/div/div[4]/table/tbody/tr/td[1]/div/div[2]/div[1]";
            newLetter.FindElement(By.XPath(sendPath)).Click();
        }
    }
}
