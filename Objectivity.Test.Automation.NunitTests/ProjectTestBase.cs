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

namespace Objectivity.Test.Automation.NunitTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using NUnit.Framework;

    using Objectivity.Test.Automation.Common;

    /// <summary>
    /// The base class for all tests
    /// </summary>
    public class ProjectTestBase : TestBase
    {
        /// <summary>
        /// Before the class.
        /// </summary>
        [TestFixtureSetUp]
        public void BeforeClass()
        {
            StartPerformanceMeasure();
        }

        /// <summary>
        /// After the class.
        /// </summary>
        [TestFixtureTearDown]
        public void AfterClass()
        {
            StopPerfromanceMeasure();
        }

        /// <summary>
        /// Before the test.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Exception")]
        [SetUp]
        public void BeforeTest()
        {
            TestTitle = TestContext.CurrentContext.Test.Name;
            LogTest.LogTestStarting();
            this.StartBrowser();
        }

        /// <summary>
        /// After the test.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Exception")]
        [TearDown]
        public void AfterTest()
        {
            IsTestFailed = TestContext.CurrentContext.Result.Status == TestStatus.Failed;
            this.FinalizeTest();
            StopBrowser();
            this.FailTestIfVerifyFailed();
            LogTest.LogTestEnding();
            LogTest = null;
        }
    }
}
