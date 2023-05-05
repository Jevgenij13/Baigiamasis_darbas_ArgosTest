using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgosTest
{
    internal class GeneralMethods
    {
        IWebDriver driver;

        public GeneralMethods(IWebDriver driver)
        {
            this.driver = driver;
        }
        public static void CaptureScreenShot(IWebDriver driver, string fileName)
        {
            //argostest-bin-debug-screenshots(cia deda screenshotus)
            var screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            if (!Directory.Exists("Screenshots"))
            {
                Directory.CreateDirectory("Screenshots");
            }

            screenshot.SaveAsFile(
                $"Screenshots\\{fileName}.png",
                ScreenshotImageFormat.Png);
        }
        public static WebDriverWait GetWait(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(NotSupportedException));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            return wait;
        }

        public static void HoverOn(IWebDriver driver, string xpath)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(By.XPath(xpath))).Perform();
            
            
        }
    }
}
