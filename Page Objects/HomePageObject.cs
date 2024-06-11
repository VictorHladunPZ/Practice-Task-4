using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
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
            string xpath; // xpath stores the path to the element we will be interacting with NEXT

            // The sleep here is to prevent any sort of 'non-loaded page' issues. You can replace it with a 
            // more complex and elegent wait.Until solution, but due to XPath issues (?) , it doesn't work
            // everytime
            Thread.Sleep(3000);
            //Click on Compose Letter button
            xpath = "/html/body/div[7]/div[3]/div/div[2]/div[1]/div[1]/div/div";

            // The wait block. It works - sort of. However, it doesn't work EVERYtime, so be aware. 
            // Perhaps, some optimization is possible.
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            var element = wait.Until(SeleniumExtras.WaitHelpers.
                ExpectedConditions.ElementToBeClickable(By.XPath(xpath))); // ExpectedConditions is 3rd party selenium nuget package. Perhaps , this is the issue?
            
            ClickElement(driver, xpath);
            // Recipient field
            xpath = "//*[@id=\":b6\"]";

            InsertIntoTextField(driver, xpath,Recipient);
            // Theme field
            xpath = "//*[@id=\":7j\"]";

            InsertIntoTextField(driver,xpath,Theme);      

            // Message body field
            xpath = "//*[@id=\":f6\"]";
            InsertIntoTextField(driver,xpath ,Msg);
            
            // Send button
            xpath = "#\\:f6";

            ClickElement(driver, xpath);
            
            // The message box that appears when the message is empty or the recipient is empty
            xpath = "/html/body/div[52]/div[2]/div";
            // Used for unit tests, basically
            if(XPathElementExist(driver, xpath))
            {
                return false;
            }

            // Sent letters page
            xpath = "//*[@id=\":3a\"]";
            ClickElement(driver,xpath);

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
    }
}
