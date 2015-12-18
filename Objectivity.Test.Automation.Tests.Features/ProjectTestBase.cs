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

namespace Objectivity.Test.Automation.Tests.Features
{
    using System.Configuration;
    using System.IO;
    using System.Reflection;

    using NUnit.Framework;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Logger;

    using TechTalk.SpecFlow;

    /// <summary>
    /// The base class for all tests
    /// </summary>
    [Binding]
    public class ProjectTestBase : TestBase
    {
        private readonly DriverContext driverContext = new DriverContext();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTestBase"/> class.
        /// </summary>
        public ProjectTestBase()
        {
            this.driverContext.DownloadFolder = this.GetFolder(ConfigurationManager.AppSettings["TestOutput"]);
            this.driverContext.ScreenShotFolder = this.GetFolder(ConfigurationManager.AppSettings["TestOutput"]);
            this.driverContext.PageSourceFolder = this.GetFolder(ConfigurationManager.AppSettings["TestOutput"]);
        }

        /// <summary>
        /// Logger instance for driver
        /// </summary>
        public TestLogger LogTest
        {
            get
            {
                return this.DriverContext.LogTest;
            }

            set
            {
                this.DriverContext.LogTest = value;
            }
        }

        /// <summary>
        /// The browser manager
        /// </summary>
        protected DriverContext DriverContext
        {
            get
            {
                return this.driverContext;
            }
        }

        /// <summary>
        /// Before the class.
        /// </summary>
        [BeforeFeature]
        public static void BeforeClass()
        {
            StartPerformanceMeasure();
        }

        /// <summary>
        /// After the class.
        /// </summary>
        [AfterFeature]
        public static void AfterClass()
        {
            StopPerfromanceMeasure();
        }

        /// <summary>
        /// Before the test.
        /// </summary>
        [Before]
        public void BeforeTest()
        {
            this.DriverContext.TestTitle = ScenarioContext.Current.ScenarioInfo.Title;
            this.LogTest.LogTestStarting(this.driverContext);
            this.DriverContext.Start();
            ScenarioContext.Current["DriverContext"] = this.DriverContext;
        }

        /// <summary>
        /// After the test.
        /// </summary>
        [After]
        public void AfterTest()
        {
            this.DriverContext.IsTestFailed = ScenarioContext.Current.TestError != null;
            this.SaveTestDetailsIfTestFailed(this.driverContext);
            this.DriverContext.Stop();
            this.LogTest.LogTestEnding(this.driverContext);
            if (this.IsVerifyFailed(this.driverContext))
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Gets the folder from app.config as value of given key.
        /// </summary>
        /// <param name="appConfigValue">The application configuration value.</param>
        /// <returns></returns>
        private string GetFolder(string appConfigValue)
        {
            string folder;

            if (string.IsNullOrEmpty(appConfigValue))
            {
                folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
            else
            {
                if (BaseConfiguration.UseCurrentDirectory)
                {
                    folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + appConfigValue;
                }
                else
                {
                    folder = appConfigValue;
                }

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }

            return folder;
        }
    }
}
