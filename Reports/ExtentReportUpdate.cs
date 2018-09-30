using System;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestContext = NUnit.Framework.TestContext;

namespace Reports
{
    [TestClass]
    public class ExtentReportUpdate
    {
        static ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter("myReport.html");
        static ExtentReports extent = new ExtentReports();
        private IWebDriver driver;

        [TestInitialize]
        public void SetUp()
        {
            extent.AttachReporter(htmlReporter);
        }

        public static string Capture(IWebDriver driver, string screenshotName)
        {
            ITakesScreenshot takeScreenshot = (ITakesScreenshot) driver;
            Screenshot screenshot = takeScreenshot.GetScreenshot();
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string binPath = path.Substring(0, path.LastIndexOf("bin"))
                             + "Screenshots\\" + TestContext.CurrentContext.Test.Name + ".png";
            string localPath = new Uri(binPath).LocalPath;
            screenshot.SaveAsFile(localPath, ScreenshotImageFormat.Png);

            return localPath;
        }

        [TestMethod]
        public void EnumVariables()
        {
            extent.AddSystemInfo("OS:", "Windows 10");
            extent.AddSystemInfo("Host name: ", "Selenium");
            extent.AddSystemInfo("Browser: ", "Chrome");
        }

        //create test methods for all situations
        [TestMethod]
        public void PassedTestMethod()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://sqa.stackexchange.com");
            driver.Manage().Window.Maximize();

            //screenshot
            string screenshotPath = Capture(driver, "name");
            var test = extent.CreateTest("Passed test method", "This test method gets passed");
            test.Log(Status.Info, "First step of passed test");
            test.Pass("Passed test method gets completed");
            test.AddScreenCaptureFromPath(screenshotPath);
            driver.Quit();
        }

        [TestMethod]
        public void FailedTestMethod()
        {
            var test = extent.CreateTest("Failed test method", "This test method gets failed");
            test.Log(Status.Info, "First step of failed test");
            test.Fail("Failed test method gets completed");
            driver.Quit();
        }

        [TestMethod]
        public void SkippedTestMethod()
        {
            var test = extent.CreateTest("Skipped test method", "This test method gets skipped");
            test.Log(Status.Info, "First step of skipped test");
            test.Skip("Skipped test method gets completed");
            driver.Quit();
        }

        [TestMethod]
        public void WarningTestMethod()
        {
            var test = extent.CreateTest("Warning test method", "This test method gets warning");
            test.Log(Status.Info, "First step of skipped test");
            test.Warning("Warning test method gets completed");
            driver.Quit();
        }

        [TestMethod]
        public void TestScreenshot()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://sqa.stackexchange.com");
            driver.Manage().Window.Maximize();

            string screenshotPath = Capture(driver, TestContext.CurrentContext.Test.Name);
            var test = extent.CreateTest("name", "name2");
            test.Log(Status.Info, "");
            test.Info("Info");
            test.AddScreenCaptureFromPath(screenshotPath);
            driver.Quit();
        }

        //simple calculation test
        [TestMethod]
        public void IntegerCalculation()
        {
            var test = extent.CreateTest("Calculation", "Calculate sum");
            test.Log(Status.Info, "First step");

            try
            {
                int a = 6;
                int b = 3;
                Assert.AreEqual(a + b, 98);
                test.Pass("Calculation method passed");
            }
            catch (Exception e)
            {
                test.Fail("Calculation method failed");
                Console.WriteLine(e);
                throw;
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            extent.Flush();
        }
    }
}
