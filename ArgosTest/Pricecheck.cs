using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NUnit.Framework.Interfaces;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace ArgosTest
{
    internal class Pricecheck
    {
        static IWebDriver driver;

        [SetUp]
        public void SETUP()
        {
            var monitor = Screen.FromPoint(new Point(Screen.PrimaryScreen.Bounds.Right + 800, Screen.PrimaryScreen.Bounds.Top));
            driver = new ChromeDriver();
            driver.Manage().Window.Position = new Point(monitor.Bounds.X, monitor.Bounds.Y);
            driver.Manage().Window.Maximize();
            driver.Url = ("https://www.argos.co.uk/browse/technology/mobile-phones-and-accessories/sim-free-phones/c:30147/brands:apple?tag=ar:shop:apple-iphone:shop-all-footer");
            By cookieBtn = By.XPath("//*[@id='consent_prompt_submit']");
            driver.FindElement(cookieBtn).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }
        [TearDown]
        public void TearDown()
        {
            var countToLEave = 4;
            var filai = Directory.GetFiles("Screenshots").ToList();
            filai.Sort();
            if (filai.Count > countToLEave)
            {
                for (int i = 0; i < filai.Count - countToLEave; i++)
                {
                    File.Delete(filai[i]);
                }
            }
            

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

        //[Test]
        [TestCase("https://www.argos.co.uk/browse/technology/mobile-phones-and-accessories/sim-free-phones/c:30147/brands:apple?tag=ar:shop:apple-iphone:shop-all-footer")]

        public void CheckPriceAligment(string categoryURL)
        {

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            By dropdown = By.XPath("//div[@class='styles__SortSelectWrapper-sc-1hkcas-6 jfdbdO']");
            By LowToHigh = By.XPath("//select[@id='sort-select']//option[2]");

            driver.FindElement(dropdown).Click();
            driver.FindElement(LowToHigh).Click();






            By pricesBy = By.XPath("//*[@id='findability']/div[7]/div[1]/div[5]/div[5]/div[1]/div/div[1]/div/div[1]/div[3]/div[4]/div/div");
            List<double> prices = new List<double>();
            foreach (IWebElement el in driver.FindElements(pricesBy))
            {
                string onePrice = el.Text.Substring(1, el.Text.Length - 2).Replace(".", ",");
                double onePriceDouble = double.Parse(onePrice);
                prices.Add(onePriceDouble);
            }
            for (int i = 0; i < prices.Count - 1; i++)
            {
                Console.WriteLine(prices[i]);
                if (prices[i] > prices[i + 1])
                {
                    Assert.Fail("Failed in " + categoryURL + " " + prices[i] + " " + prices[i + 1]);
                }



            }
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 300);");
            Thread.Sleep(800);
            By FirstPhone = By.XPath("//img[contains(@alt,'- Black')]");
            driver.FindElement(FirstPhone).Click();

            driver.Navigate().Back();
            Thread.Sleep(500);
            pricesBy = By.XPath("//*[@id='findability']/div[7]/div[1]/div[5]/div[5]/div[1]/div/div[1]/div/div[1]/div[3]/div[4]/div/div");
            prices = new List<double>();
            foreach (IWebElement el in driver.FindElements(pricesBy))
            {
                string onePrice = el.Text.Substring(1, el.Text.Length - 2).Replace(".", ",");
                double onePriceDouble = double.Parse(onePrice);
                prices.Add(onePriceDouble);
            }
            for (int i = 0; i < prices.Count - 1; i++)
            {
                Console.WriteLine(prices[i]);
                if (prices[i] > prices[i + 1])
                {
                    Assert.Fail("Failed in " + categoryURL + " " + prices[i] + " " + prices[i + 1]);
                }
            }

            Assert.IsTrue(false);

        
        
            





            


        }
    }
}

