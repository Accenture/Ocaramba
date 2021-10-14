// <copyright file="TestExecutionManager.cs" company="Objectivity Bespoke Software Specialists">
// Copyright (c) Objectivity Bespoke Software Specialists. All rights reserved.
// </copyright>
// <license>
//     The MIT License (MIT)
//     Permission is hereby granted, free of charge, to any person obtaining a copy
//     of this software and associated documentation files (the "Software"), to deal
//     in the Software without restriction, including without limitation the rights
//     to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//     copies of the Software, and to permit persons to whom the Software is
//     furnished to do so, subject to the following conditions:
//     The above copyright notice and this permission notice shall be included in all
//     copies or substantial portions of the Software.
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//     LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//     OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//     SOFTWARE.
// </license>
using AventStack.ExtentReports;
using NUnit.Framework;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System.IO;

namespace Ocaramba.Tests.NUnitExtentReports
{
    /// <summary>
    /// Class containing setup and teardown definitons which are executed only once before and after entire test suite is executed
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
            var reporter = new ExtentHtmlReporter(this.DriverContext.CurrentDirectory + "\\TestOutput\\index.html");
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