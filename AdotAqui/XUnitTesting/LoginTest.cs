using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace SeleniumTests
{
    public class UntitledTestCase
    {
        private IWebDriver driver;
        [Fact]
        public void LogIn()
        {
#pragma warning restore xUnit1013 // Public method should be marked as test


            driver = new FirefoxDriver(Path.GetDirectoryName
                               (Assembly.GetExecutingAssembly().Location));
                driver.Navigate().GoToUrl
                       (@"https://eswapp.azurewebsites.net/Identity/Account/Login?ReturnUrl=%2FIdentity%2FAccount%2FLogOff%3FreturnUrl%3D%252F");
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Email")));

                driver.FindElement(By.Id("Email")).Click();
                driver.FindElement(By.Id("Email")).Clear();
                driver.FindElement(By.Id("Email")).SendKeys("170221081@estudantes.ips.pt");
                driver.FindElement(By.Id("Password")).Click();
                driver.FindElement(By.Id("Password")).Clear();
                driver.FindElement(By.Id("Password")).SendKeys("Password1!");
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='AdotAqui'])[2]/following::button[1]")).Click();


                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.LinkText("David")), "David"));

            }
    }
}