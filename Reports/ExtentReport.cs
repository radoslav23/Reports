using System;
using System.Net;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;

namespace Reports
{
    [TestFixture]
    public class ExtentReport
    {
        [Test]
        public void ExtentTest()
        {
            var dir = TestContext.CurrentContext.TestDirectory + "\\";
            var htmlReporter = new ExtentHtmlReporter(dir + "testreport.html");
            var extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            string hostName = Dns.GetHostName();
            OperatingSystem os = Environment.OSVersion;

            extent.AddSystemInfo("OS:", os.ToString());
            extent.AddSystemInfo("Host name:", hostName);
            extent.AddSystemInfo("Browser:", "Chrome");

            var test = extent.CreateTest("ExtentTest");
            test.Log(Status.Info, "Step 1: Test case starts");
            test.Log(Status.Pass, "Step 2: Test case pass");
            test.Log(Status.Fail, "Step 3: Test case fail");
            test.Pass("Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath("screenshot.png").Build());
            test.Pass("Screenshot").AddScreenCaptureFromPath("screenshot.png");
            extent.Flush();
        }
    }
}
