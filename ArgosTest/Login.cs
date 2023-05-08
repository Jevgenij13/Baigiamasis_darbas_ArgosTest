using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace ArgosTest
{

    [TestFixture]
    public class ArgosLoginTests
    {
        private IWebDriver driver;

        [SetUp]
        public void SETUP()
        {
            var monitor = Screen.FromPoint(new Point(Screen.PrimaryScreen.Bounds.Right + 800, Screen.PrimaryScreen.Bounds.Top));
            driver = new ChromeDriver();
            driver.Manage().Window.Position = new Point(monitor.Bounds.X, monitor.Bounds.Y);
            driver.Manage().Window.Maximize();
            driver.Url = ("https://www.argos.co.uk");
            By cookieBtn = By.XPath("//*[@id='consent_prompt_submit']");
            driver.FindElement(cookieBtn).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }
        [TearDown]
        public void TearDown()
        {
            //trinti screenshot failus folderije
            var countToLEave = 10;
            var filai = Directory.GetFiles("Screenshots").ToList();
            filai.Sort();
            if (filai.Count > countToLEave)
            {
                for (int i = 0; i < filai.Count - countToLEave; i++)
                {
                    File.Delete(filai[i]);
                }
            }

            //padaryti screenshotus 
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var name =
                    $"{TestContext.CurrentContext.Test.MethodName}" +
                    $" Error at " +
                    $"{DateTime.Now.ToString().Replace(":", "_")}";

                GeneralMethods.CaptureScreenShot(driver, name);

                File.WriteAllText(
                    $"Screenshots\\{name}.txt",
                    TestContext.CurrentContext.Result.Message);
            }
            driver.Close();
            driver.Quit();
        }

        [Test]

        //patikrinti ar prisijungia prie paskyros
        public void LoginAndLogout()
        {
            WebDriverWait wait = GeneralMethods.GetWait(driver);
            driver.Navigate().GoToUrl("https://www.argos.co.uk/");
            By SignOutBTn = By.XPath("('//a[@class='Bh-zw']')");

            IWebElement signInLink = driver.FindElement(By.XPath("//span[normalize-space()='Account']"));
            signInLink.Click();

            driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(10);

            //panaudojam is POM
            var loginPage = new LoginPage(driver);
            loginPage.EnterEmail("testlpbpdc@gmail.com");
            loginPage.EnterPassword("Baltic11");
            
            Thread.Sleep(1000);
            driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(10);
            IWebElement signInButton = driver.FindElement(By.XPath("//button[normalize-space()='Sign in securely']"));
            signInButton.Click();
            //Thread.Sleep(600);
            //patikrinam ar prisijungia ir mato teksta Hi...
            Assert.AreEqual("Hi, Jevgenij Volynec", driver.FindElement(By.XPath("//span[normalize-space()='Hi,']")).
            Text, "The expected text not present");

            wait.Until(d => d.FindElement(SignOutBTn)).Click();

            Assert.AreNotEqual("Hi, Jevgenij Volynec", driver.FindElement(By.XPath("//div[@class='m-jtR']")).
            Text, "The expected text not present");

        }
          
    }
}

