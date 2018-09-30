using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;

namespace Reports
{
    [TestFixture]
    public class BasicExtentReportExample
    {
        [Test]
        public void BasicReport()
        {
            // start reporters
            var htmlReporter = new ExtentHtmlReporter(TestContext.CurrentContext.Test.Name + "extent.html");

            // create ExtentReports and attach reporter(s)
            var extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            // creates a toggle for the given test, adds all log events under it    
            var test = extent.CreateTest("MyFirstTest", "Sample description");

            // log(Status, details)
            test.Log(Status.Info, "This step shows usage of log(status, details)");

            // info(details)
            test.Info("This step shows usage of info(details)");

            // log with snapshot
            test.Fail("details", MediaEntityBuilder.CreateScreenCaptureFromPath("screenshot.png").Build());

            // test with snapshot
            test.AddScreenCaptureFromPath("screenshot.png");

            // calling flush writes everything to the log file
            extent.Flush();
        }
    }
}
