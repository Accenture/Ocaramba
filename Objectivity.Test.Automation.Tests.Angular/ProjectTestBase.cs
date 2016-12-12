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

namespace Objectivity.Test.Automation.Tests.Angular
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Logger;

    /// <summary>
    /// The base class for all tests
    /// </summary>
    [TestClass]
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
        /// Gets or sets the microsoft test context.
        /// </summary>
        /// <value>
        /// The microsoft test context.
        /// </value>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Gets the driver context
        /// </summary>
        protected DriverContext DriverContext
        {
            get
            {
                return this.driverContext;
            }
        }

        /// <summary>
        /// Before the test.
        /// </summary>
        [TestInitialize]
        public void BeforeTest()
        {
            this.DriverContext.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            this.DriverContext.TestTitle = this.TestContext.TestName;
            this.LogTest.LogTestStarting(this.driverContext);
            this.DriverContext.Start();
        }

        /// <summary>
        /// After the test.
        /// </summary>
        [TestCleanup]
        public void AfterTest()
        {
            this.DriverContext.IsTestFailed = this.TestContext.CurrentTestOutcome == UnitTestOutcome.Failed || !this.driverContext.VerifyMessages.Count.Equals(0);
            this.SaveTestDetailsIfTestFailed(this.driverContext);
            this.DriverContext.Stop();
            this.LogTest.LogTestEnding(this.driverContext);
            if (this.IsVerifyFailedAndClearMessages(this.driverContext) && this.TestContext.CurrentTestOutcome != UnitTestOutcome.Failed)
            {
                Assert.Fail("Look at stack trace logs for more details");
            }
        }
    }
}
