using AventStack.ExtentReports;
using NUnit.Framework;
using System.IO;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;

namespace Ocaramba.Tests.NUnit
{
    [SetUpFixture]
    class TestExecutionManager : TestBase
    {
        protected readonly DriverContext driverContext = new DriverContext();
        public static ExtentReports extent;

        protected DriverContext DriverContext
        {
            get
            {
                return this.driverContext;
            }
        }

        /// <summary>
        /// Before suite
        /// </summary>
        [OneTimeSetUp]
        public void BeforeSuite()
        {
            this.DriverContext.CurrentDirectory = Directory.GetCurrentDirectory();
            extent = new ExtentReports();
            var reporter = new ExtentHtmlReporter(this.DriverContext.CurrentDirectory + "\\TestOutput\\");
            reporter.Config.ReportName = "BullSearch UITests Report - " + BaseConfiguration.TestBrowser;
            reporter.Config.Theme = Theme.Standard;
            extent.AttachReporter(reporter);
        }

        /// <summary>
        /// After suite.
        /// </summary>
        [OneTimeTearDown]
        public void AfterSuite()
        {
            extent.Flush();
        }
    }
}