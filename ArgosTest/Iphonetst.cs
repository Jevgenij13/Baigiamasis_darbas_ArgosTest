using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ArgosTest
{
    public class Iphonetst
    {
        static IWebDriver driver;

        [SetUp]
        public void SETUP()

        {
            var monitor = Screen.FromPoint(new Point(Screen.PrimaryScreen.Bounds.Right + 800, Screen.PrimaryScreen.Bounds.Top));
            driver = new ChromeDriver();
            driver.Manage().Window.Position = new Point(monitor.Bounds.X, monitor.Bounds.Y);
            driver.Manage().Window.Maximize();
            driver.Url = ("https://www.argos.co.uk/");
            By cookieBtn = By.XPath("//*[@id='consent_prompt_submit']");
            driver.FindElement(cookieBtn).Click();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
        [Test]
        public void FindIphoneBadge()
        {
            By InputField = By.XPath("//input[@id='searchTerm'] ");
            By SearchBtn = By.XPath("//span[@class='_1gqeQ']");

            driver.FindElement(InputField).SendKeys("Iphone");
            driver.FindElement(SearchBtn).Click();
            Thread.Sleep(500);


            IWebElement element = driver.FindElement(By.XPath("//a[@title='Shop all iPhone at Argos.']"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            element.Click();

            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 500);");

            Assert.IsTrue(driver.FindElement(By.XPath("//a[@id='product-title-2207344']")).
            Text.Equals("SIM Free iPhone 14 5G 128GB Mobile Phone - Yellow"),
            "The expected new model was not desplayed");


        }
        [Test]
        public void AddToCart()

        {
            
            driver.Url = ("https://www.argos.co.uk/browse/technology/mobile-phones-and-accessories/sim-free-phones/c:30147/brands:apple?tag=ar:shop:apple-iphone:shop-all-footer");
            
            //Thread.Sleep(800);

            By ChooseOptionsBtn = By.XPath("//*[@id='findability']/div[7]/div[1]/div[5]/div[5]/div[1]/div/div[1]/div/div[1]/div[2]/a/div[2]/picture/img");
            By AddToCartBtn = By.XPath("//button[@data-test='add-to-trolley-button-button']");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3); 
            driver.FindElement(ChooseOptionsBtn).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(AddToCartBtn).Click();
                

        }
        

        



    }
}