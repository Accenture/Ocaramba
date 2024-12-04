using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using NUnit.Framework;

namespace Ocaramba.Tests.NUnitExtentReports
{
    /// <summary>
    /// Class containing setup and teardown definitions which are executed only once before and after entire test suite is executed
    /// </summary>
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
        /// Method executed once only before all the tests are started
        /// </summary>
        [OneTimeSetUp]
        public void BeforeSuite()
        {
            this.DriverContext.CurrentDirectory = Directory.GetCurrentDirectory();
            extent = new ExtentReports();
            var reporter = new ExtentSparkReporter(this.DriverContext.CurrentDirectory + "\\TestOutput\\index.html");
            reporter.Config.ReportName = "Ocaramba UITests Report - " + BaseConfiguration.TestBrowser;
            reporter.Config.Theme = Theme.Standard;
            extent.AttachReporter(reporter);
        }

        /// <summary>
        /// Method executed once only after all the tests are finished
        /// </summary>
        [OneTimeTearDown]
        public void AfterSuite()
        {
            extent.Flush();
        }
    }
}