using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgosTest
{
    internal class LoginPage
    {
        private IWebDriver driver;

        // Locators
        private readonly By emailInput = By.Id("email-address");
        private readonly By passwordInput = By.XPath("//input[@id='current-password']");
        private readonly By signInButton = By.XPath("//button[normalize-space()='Sign in securely']");
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void EnterEmail(string email)
        {
            driver.FindElement(emailInput).SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            driver.FindElement(passwordInput).SendKeys(password);
        }

        public void ClickLoginButton()
        {
            driver.FindElement(signInButton).Click();
        }







    }
    

}

