/*
The MIT License (MIT)

Copyright (c) 2015 Objectivity Bespoke Software Specialists

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace Objectivity.Test.Automation.Common.Logger
{
    using System;
    using System.Globalization;

    using NLog;
    using NLog.Targets;

    /// <summary>
    /// Class for test logger
    /// </summary>
    public class TestLogger
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly Logger log = LogManager.GetLogger("TEST");

        /// <summary>
        /// The start test time
        /// </summary>
        private DateTime startTestTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestLogger"/> class.
        /// </summary>
        /// <param name="testFolder">Name of the folder</param>
        /// <param name="testName">Name of the test.</param>
        public TestLogger(string testFolder, string testName)
        {
            this.TestName = testName;        
            if (string.IsNullOrEmpty(testFolder))
            {
                this.TestFolder = this.TestFolderPattern(testName);
            }
            else
            {
                this.TestFolder = testFolder;
            }

            this.SetLogFileLocalization(this.TestFolder, testName);
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
            var testFolder = BaseConfiguration.TestOutput;
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
            this.startTestTime = DateTime.Now;
            this.Info("START: {0} starts at {1}", this.TestName, this.startTestTime);
        }

        /// <summary>
        /// Logs the test ending.
        /// </summary>
        public void LogTestEnding()
        {
            var endTestTime = DateTime.Now;
            var timeInSec = (endTestTime - this.startTestTime).TotalMilliseconds / 1000d;
            this.Info("END: {0} ends at {1} after {2} sec.", this.TestName, endTestTime, timeInSec.ToString("##,###", CultureInfo.CurrentCulture));
        }

        /// TODO: does it required?
        /// <summary>
        /// Information the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void Info(string message, params object[] args)
        {
            this.log.Info(CultureInfo.CurrentCulture, message, args);
        }

        /// TODO: does it required?
        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void Warn(string message, params object[] args)
        {
            this.log.Warn(CultureInfo.CurrentCulture, message, args);
        }

        /// TODO: does it required?
        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void Error(string message, params object[] args)
        {
            this.log.Error(CultureInfo.CurrentCulture, message, args);
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="e">The e.</param>
        public void LogError(Exception e)
        {
            this.Error("Error occurred: {0}", e);
            throw e;
        }
    }
}
