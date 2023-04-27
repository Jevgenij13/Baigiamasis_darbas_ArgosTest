using OpenQA.Selenium;
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
    }
}
