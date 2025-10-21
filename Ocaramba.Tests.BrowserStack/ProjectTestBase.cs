// <copyright file="ProjectTestBase.cs" company="Accenture">
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

using OcarambaLite.Logger;
using System.IO;

namespace Ocaramba.Tests.BrowserStack
{
    using global::NUnit.Framework;
    using global::NUnit.Framework.Interfaces;
    using NLog;
    using Ocaramba;
    using Ocaramba.Logger;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Globalization;
    using System.Runtime.Intrinsics.X86;
    using System.Text.Json.Nodes;

    /// <summary>
    /// The base class for all tests <see href="https://github.com/ObjectivityLtd/Ocaramba/wiki/ProjectTestBase-class">More details on wiki</see>
    /// </summary>
    public class ProjectTestBase : TestBase
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly DriverContext driverContext = new DriverContext();

        public ProjectTestBase()
        {
            
        }

        /// <summary>
        /// Gets or sets logger instance for driver
        /// </summary>
        public TestLogger LogTest
        {
            get { return this.driverContext.LogTest; }
            set { this.driverContext.LogTest = value; }
        }

        /// <summary>
        /// Gets or Sets the driver context
        /// </summary>
        protected DriverContext DriverContext
        {
            get { return this.driverContext; }
        }

        /// <summary>
        /// Before the class.
        /// </summary>
        [OneTimeSetUp]
        public void BeforeClass()
        {
            this.driverContext.CurrentDirectory = Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// After the class.
        /// </summary>
        [OneTimeTearDown]
        public void AfterClass()
        {
        }

        /// <summary>
        /// Before the test.
        /// </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.driverContext.TestTitle = TestContext.CurrentContext.Test.Name;
            this.driverContext.Start();
            this.LogTest.LogTestStarting(this.driverContext);
            JsonObject executorObject = new JsonObject();
            JsonObject argumentsObject = new JsonObject();
            argumentsObject.Add("name", this.driverContext.TestTitle);
            executorObject.Add("action", "setSessionName");
            executorObject.Add("arguments", argumentsObject);
            ((IJavaScriptExecutor)this.driverContext.Driver).ExecuteScript("browserstack_executor: " + executorObject.ToString());

        }

        /// <summary>
        /// After the test.
        /// </summary>
        [TearDown]
        public void AfterTest()
        {
            this.driverContext.IsTestFailed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed || this.driverContext.VerifyMessages.Count != 0;
            var filePaths = this.SaveTestDetailsIfTestFailed(this.driverContext);
            this.SaveAttachmentsToTestContext(filePaths);
            this.LogTest.LogTestEnding(this.driverContext);
            JsonObject executorObject = new JsonObject();
            JsonObject argumentsObject = new JsonObject();
            
            executorObject.Add("action", "setSessionStatus");
            

            if (this.driverContext.IsTestFailed)
            {
                argumentsObject.Add("status", "failed");
                argumentsObject.Add("reason", "Test failed");
            } else
            {
                argumentsObject.Add("status", "passed");
                argumentsObject.Add("reason", "Test Passed");
            }

            executorObject.Add("arguments", argumentsObject);
            ((IJavaScriptExecutor)this.driverContext.Driver).ExecuteScript("browserstack_executor: " + executorObject.ToString());

            if (this.IsVerifyFailedAndClearMessages(this.driverContext) && TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
            {
                Assert.Fail();
            }

            this.driverContext.Stop();
        }

        private void SaveAttachmentsToTestContext(string[] filePaths)
        {
            if (filePaths != null)
            {
                foreach (var filePath in filePaths)
                {
                    this.LogTest.Info("Uploading file [{0}] to test context", filePath);
                    TestContext.AddTestAttachment(filePath);
                }
            }
        }
    }
}
