using System;
using System.Globalization;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Objectivity.Test.Automation.Common.Logger
{
    /// <summary>
    /// Class for test logger
    /// </summary>
    public class TestLogger
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly NLog.Logger log = LogManager.GetLogger("TEST");

        /// <summary>
        /// The start test time
        /// </summary>
        private DateTime startTestTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestLogger"/> class.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        public TestLogger(string testName)
        {
            TestName = testName;
            TestFolder = TestFolderPattern(testName);
            SetLogFileLocalization(TestFolder, testName);
        }

        /// <summary>
        /// Gets the test folder.
        /// </summary>
        /// <value>
        /// The test folder.
        /// </value>
        public string TestFolder { get; set; }

        /// <summary>
        /// Gets the name of the test.
        /// </summary>
        /// <value>
        /// The name of the test.
        /// </value>
        private string TestName { get; set; }

        /// <summary>
        /// Tests the folder pattern.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <returns>Folder name</returns>
        private string TestFolderPattern(string testName)
        {
            var testFolder = AppDomain.CurrentDomain.BaseDirectory + BaseConfiguration.TestFolder;
            log.Info("TESTFOLDER:  " + testFolder);
            if (string.IsNullOrEmpty(testFolder))
            {
                return string.Empty;
            }

            return string.Format(CultureInfo.CurrentCulture, "{0}\\{1}_{2}", testFolder, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss", CultureInfo.CurrentCulture), testName);
        }

        /// <summary>
        /// Sets the log file localization.
        /// </summary>
        private void SetLogFileLocalization(string folder, string logName)
        {
            var pattern = string.IsNullOrEmpty(folder) ? "{1}.log" : "{0}\\{1}.log";
            FileTarget target = LogManager.Configuration.FindTargetByName("logfile") as FileTarget;
            var logfile = string.Format(CultureInfo.CurrentCulture, pattern, folder, logName);
            target.FileName = logfile;
        }

        /// <summary>
        /// Logs the test starting.
        /// </summary>
        public void LogTestStarting()
        {
            startTestTime = DateTime.Now;
            Info("START: {0} starts at {1}", TestName, startTestTime);
        }

        /// <summary>
        /// Logs the test ending.
        /// </summary>
        public void LogTestEnding()
        {
            var endTestTime = DateTime.Now;
            var timeInSec = (endTestTime - startTestTime).TotalMilliseconds / 1000d;
            Info("END: {0} ends at {1} after {2} sec.", TestName, endTestTime, timeInSec.ToString("##,###", CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// Information the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void Info(string message, params object[] args)
        {
            log.Info(CultureInfo.CurrentCulture, message, args);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void Warn(string message, params object[] args)
        {
            log.Warn(CultureInfo.CurrentCulture, message, args);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void Error(string message, params object[] args)
        {
            log.Error(CultureInfo.CurrentCulture, message, args);
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="e">The e.</param>
        public void LogError(Exception e)
        {
            Error("Error occurred: {0}", e);
            throw e;
        }
    }
}
