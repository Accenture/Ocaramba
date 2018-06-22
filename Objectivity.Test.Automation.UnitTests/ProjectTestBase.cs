// <copyright file="ProjectTestBase.cs" company="Objectivity Bespoke Software Specialists">
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

using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Logger;

namespace Objectivity.Test.Automation.UnitTests
{
    /// <summary>
    /// The base class for all tests <see href="https://github.com/ObjectivityLtd/Test.Automation/wiki/ProjectTestBase-class">More details on wiki</see>
    /// </summary>
    public class ProjectTestBase : TestBase
    {
        private readonly DriverContext driverContext = new DriverContext();

        /// <summary>
        /// Gets or sets logger instance for driver
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
        /// Gets or Sets the driver context
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
        [OneTimeSetUp]
        public void BeforeClass()
        {
            this.DriverContext.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            this.DriverContext.Start();
            this.DriverContext.WindowMaximize();
            this.DriverContext.DeleteAllCookies();
        }

        /// <summary>
        /// After the class.
        /// </summary>
        [OneTimeTearDown]
        public void AfterClass()
        {
            this.DriverContext.Stop();
        }

        /// <summary>
        /// Before the test.
        /// </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.DriverContext.TestTitle = TestContext.CurrentContext.Test.Name;
            this.LogTest.LogTestStarting(this.driverContext);
        }

        /// <summary>
        /// After the test.
        /// </summary>
        [TearDown]
        public void AfterTest()
        {
            this.DriverContext.IsTestFailed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed || !this.driverContext.VerifyMessages.Count.Equals(0);
            var filePaths = this.SaveTestDetailsIfTestFailed(this.driverContext);
            this.SaveAttachmentsToTestContext(filePaths);
            if (this.IsVerifyFailedAndClearMessages(this.driverContext) && TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
            {
                Assert.Fail();
            }
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
