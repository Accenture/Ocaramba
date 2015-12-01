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

namespace Objectivity.Test.Automation.NUnit
{
    using global::NUnit.Framework;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Helpers;
    using Objectivity.Test.Automation.Common.Logger;

    /// <summary>
    /// Class contains method for all tests, should be used in project test base
    /// </summary>
    public class TestBase
    {
        private readonly DriverContext driverContext = new DriverContext();

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
        /// Run before the class.
        /// </summary>
        protected static void StartPerformanceMeasure()
        {
            PerformanceHelper.Instance = new PerformanceHelper();
        }

        /// <summary>
        /// Run after the class.
        /// </summary>
        protected static void StopPerfromanceMeasure()
        {
            PerformanceHelper.Instance.PrintAveragePercentiles90DurationMilliseconds();
        }

        /// <summary>
        /// Take screenshot if test failed and delete cached page objects.
        /// </summary>
        protected void SaveTestDetailsIfTestFailed()
        {
            if (this.driverContext.IsTestFailed)
            {
                this.driverContext.TakeAndSaveScreenshot();
                this.SavePageSource();
            }
        }

        /// <summary>
        /// Take screenshot if test failed and delete cached page objects.
        /// </summary>
        public void SaveTestDetailsIfTestFailed(DriverContext externalDriverContext)
        {
            if (externalDriverContext.IsTestFailed)
            {
                externalDriverContext.TakeAndSaveScreenshot();
                this.SavePageSource(externalDriverContext);
            }
        }

        /// <summary>
        /// Save Page Source
        /// </summary>
        public void SavePageSource()
        {
            if (BaseConfiguration.GetPageSourceEnabled)
            {
                this.driverContext.SavePageSource(this.driverContext.LogTest.TestFolder, this.driverContext.TestTitle);
            }
        }

        /// <summary>
        /// Save Page Source
        /// </summary>
        /// <param name="externalDriverContext">
        /// Driver context includes
        /// </param>
        public void SavePageSource(DriverContext externalDriverContext)
        {
            if (BaseConfiguration.GetPageSourceEnabled)
            {
                externalDriverContext.SavePageSource(externalDriverContext.LogTest.TestFolder, externalDriverContext.TestTitle);
            }
        }

        /// <summary>
        /// Fails the test if verify failed.
        /// </summary>
        public void FailTestIfVerifyFailed()
        {
            if (!this.driverContext.VerifyMessages.Count.Equals(0) && !this.driverContext.IsTestFailed)
            {
                this.driverContext.VerifyMessages.Clear();
                Assert.Fail();
            }
        }

        /// <summary>
        /// Fail Test If Verify Failed
        /// </summary>
        /// <param name="externalDriverContext">
        /// Driver context includes
        /// </param>
        public void FailTestIfVerifyFailed(DriverContext externalDriverContext)
        {
            if (!externalDriverContext.VerifyMessages.Count.Equals(0) && !externalDriverContext.IsTestFailed)
            {
                externalDriverContext.VerifyMessages.Clear();
                Assert.Fail();
            }
        }
    }
}