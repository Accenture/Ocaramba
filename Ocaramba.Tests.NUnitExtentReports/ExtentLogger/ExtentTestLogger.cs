using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;

namespace Ocaramba.Tests.NUnitExtentReports.ExtentLogger
{
    class ExtentTestLogger : ProjectTestBase
    {

        public static void Info(string message)
        {
            test.Info(message);
        }

        public static void Debug(string message)
        {
            test.Debug(message);
        }

        public static void Warning(string message)
        {
            test.Warning(message);
        }

        public static void Pass(string message)
        {
            test.Pass(message);
        }

        public static void Fail(TestStatus status, string errorMessage)
        {
            test.Fail(status +": " + errorMessage);
        }
    }
}
