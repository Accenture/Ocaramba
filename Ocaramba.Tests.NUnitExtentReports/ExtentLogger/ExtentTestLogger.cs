using NUnit.Framework.Interfaces;

namespace Ocaramba.Tests.NUnitExtentReports.ExtentLogger
{
    /// <summary>
    /// Class containing methods enabling writing messages to Extent Report HTML file
    /// </summary>
    class ExtentTestLogger : ProjectTestBase
    {
        /// <summary>
        /// Log Info entry in HTML file
        /// </summary>
        /// <param name="message">The message</param>
        public static void Info(string message)
        {
            test.Info(message);
        }

        /// <summary>
        /// Log Debug entry in HTML file
        /// </summary>
        /// <param name="message">The message</param>
        public static void Debug(string message)
        {
            test.Debug(message);
        }

        /// <summary>
        /// Log Warning entry in HTML file
        /// </summary>
        /// <param name="message">The message</param>
        public static void Warning(string message)
        {
            test.Warning(message);
        }

        /// <summary>
        /// Log Test Pass status in HTML file
        /// </summary>
        /// <param name="message">The message</param>
        public static void Pass(string message)
        {
            test.Pass(message);
        }

        /// <summary>
        /// Log Test Failed status in HTML file
        /// </summary>
        /// <param name="status">Test status</param>
        /// <param name="errorMessage">Error message</param>
        public static void Fail(TestStatus status, string errorMessage)
        {
            test.Fail(status +": " + errorMessage);
        }
    }
}
