// <copyright file="TestBase.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Ocaramba
{
    using System.Collections.Generic;
    using Ocaramba.Helpers;

    /// <summary>
    /// Class contains method for all tests, should be used in project test base.
    /// </summary>
    public class TestBase
    {
        /// <summary>
        /// Take screenshot if test failed and delete cached page objects.
        /// </summary>
        /// <param name="driverContext">The driver context.</param>
        /// <returns>The saved attachements, null if not found.</returns>
        public string[] SaveTestDetailsIfTestFailed(DriverContext driverContext)
        {
            if (driverContext.IsTestFailed)
            {
                var screenshots = driverContext.TakeAndSaveScreenshot();
                var pageSource = this.SavePageSource(driverContext);

                var returnList = new List<string>();
                returnList.AddRange(screenshots);
                if (pageSource != null)
                {
                    returnList.Add(pageSource);
                }

                return returnList.ToArray();
            }

            return null;
        }

        /// <summary>
        /// Save Page Source.
        /// </summary>
        /// <param name="driverContext">
        /// Driver context includes.
        /// </param>
        /// <returns>Path to the page source.</returns>
        public string SavePageSource(DriverContext driverContext)
        {
            return driverContext.SavePageSource(driverContext.TestTitle);
        }

        /// <summary>
        /// Fail Test If Verify Failed and clear verify messages.
        /// </summary>
        /// <param name="driverContext">Driver context includes.</param>
        /// <returns>True if test failed.</returns>
        public bool IsVerifyFailedAndClearMessages(DriverContext driverContext)
        {
            if (driverContext.VerifyMessages.Count.Equals(0))
            {
                return false;
            }

            driverContext.VerifyMessages.Clear();
            return true;
        }
    }
}