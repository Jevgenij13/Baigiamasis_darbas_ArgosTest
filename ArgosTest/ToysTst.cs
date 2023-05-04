using NUnit.Framework.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium.Interactions;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace ArgosTest
{
    internal class ToysTst
    {
        static IWebDriver driver;

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
        public void FindToys()
        {
            //patikrinti meniu kur issivinioja uzvaziuojant ant meniu pele, pasirinkti toys elementa ir patikrinti ar esam tam puslapi, Is toys pasirinkti lego ir patikrinti ar nuejome i ta puslapi
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(NotSupportedException));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

            By DropDownMenu = By.XPath("//a[@aria-label='To hear more Toys links, please hit the spacebar key to enter the menu. Or click here to go to all of Toys.']");
            By Legotoys = By.XPath("//a[@class='styles__CategoryLink-sc-8hzx8v-1 jtgtYs'][normalize-space()='LEGO']");
            IWebElement element = driver.FindElement(By.XPath("//span[@class='_13iYl'][normalize-space()='Shop']"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Perform();
            driver.FindElement(DropDownMenu).Click();

            Assert.AreEqual("Toys", driver.FindElement(By.XPath("//h1[@class='styles__CategoryName-sc-6xv5se-4 jxgscl']")).
            Text,"The expected text not present");
            Thread.Sleep(500);
            wait.Until(d => d.FindElement(Legotoys)).Click();
            Assert.AreEqual("LEGO", driver.FindElement(By.XPath("//h1[@class='styles__SearchTerm-sc-1haccah-1 eslAyR']")).
            Text, "The expected text not present");

        }
        [Test]
        public void CheckLegoCheckBoxes()
        {
            //Lego puslapi pasirinkti 2 checkboxus ir patikrinti ar pasirinkome 2 checkboxus is visu esamu 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(NotSupportedException));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

            driver.Url = ("https://www.argos.co.uk/browse/toys/lego/c:30379/");

            By LegoCity = By.XPath("//label[@id='filter-character-lego city-label']//div[@class='Checkboxstyles__CheckboxOption-b61uwr-3 cKQGxN']//*[name()='svg']");
            By LegoMarwel = By.XPath("//label[@id='filter-character-lego marvel-label']//div[@class='Checkboxstyles__CheckboxOption-b61uwr-3 cKQGxN']//*[name()='svg']");
            //By AllCheckBoxes = By.XPath("//input[@type='checbox']");

            wait.Until(d => d.FindElement(LegoCity)).Click();
            wait.Until(d => d.FindElement(LegoMarwel)).Click();
            ReadOnlyCollection<IWebElement>webElements =driver.FindElements(By.XPath("//input[@type='checbox']"));
            int checkedCount = 0;
            int uncheckedCount = 0;

            foreach (IWebElement element in webElements)
            {
                if(element.Selected==true) 
                    checkedCount++; 
                else
                    uncheckedCount++;
                
            }
            Console.WriteLine("Number of checked checboxes are" +checkedCount);
            Console.WriteLine("Number of unchecked checkboxes are" +uncheckedCount);
        }

    }

}
    

