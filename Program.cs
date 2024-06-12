using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Practice_Task_4.Page_Objects;
using System;

namespace selenium
{
    internal static class Program 
    {
        // Ideally, you shouldn't be keeping code here
        // Write it in unit tests if you wish to have it executed
        // Write it in Page Objects if you want to make methods interacting with pages

        // REMEMBER TO REPLACE THE CREDENTIALS IN UNIT TESTS !!!!!

        static void Main(string[] args) 
        {
            WebDriver driver = new ChromeDriver();
            //Remember to replace the email
            LoginPageObject loginPage = new LoginPageObject(driver, "seleniumtestacc496@gmail.com", "qwe123zx");
            loginPage.Login();
            //Remember to replace the email
            HomePageObject homePage = new HomePageObject(driver, "victorhladun@gmail.com", "Selenium Test");
            homePage.WriteLetter();
            driver.Quit();
            
        }
        

    }
}