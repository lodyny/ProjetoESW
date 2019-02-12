using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Xunit;
using XUnitTesting;

namespace SeleniumTests
{
    public class BrowserTestCase
    {
        private IWebDriver driver;


        [Fact]
        public void CheckEmail()
        {
            driver = new FirefoxDriver(Path.GetDirectoryName
                               (Assembly.GetExecutingAssembly().Location));

            driver.Navigate().GoToUrl
                   (@"http://eswapp.azurewebsites.net/Identity/Account/Register");
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));

            driver.FindElement(By.Id("Email")).Click();
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys("teste");
            driver.FindElement(By.Id("Password")).Click();
            var expectFail = driver.FindElement(By.ClassName("result_mail")).Text;

            driver.FindElement(By.Id("Email")).Click();
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys("teste@gmail.com");
            driver.FindElement(By.Id("Password")).Click();
            var expectPass = driver.FindElement(By.ClassName("result_mail")).Text;

            Assert.Matches(expectFail, "✘");
            Assert.Matches(expectPass, "✔");

        }

    [Fact]
        public void CheckAdmin()
        {
            driver = new FirefoxDriver(Path.GetDirectoryName
                               (Assembly.GetExecutingAssembly().Location));

            WebHelper.LogIn(driver);

            IWebElement adminBtn = driver.FindElement(By.XPath("(.//a[@href = '/Users' and ((text() = ' Administração' or . = ' Administração') or (text() = ' Administration' or . = ' Administration'))])"));

            ExpectedConditions.ElementExists(By.XPath("(.//a[@href = '/Users' and ((text() = ' Administração' or . = ' Administração') or (text() = ' Administration or . = ' Administration'))])"));

        }
    }
}