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

namespace Objectivity.Test.Automation.Common
{
    using System;
    using System.Globalization;

    using NLog;

    using Objectivity.Test.Automation.Common.Types;

    /// <summary>
    /// Class for assert without stop tests
    /// </summary>
    public static class Verify
    {
        private static readonly NLog.Logger Logger = LogManager.GetLogger("TEST");

        /// <summary>
        /// Verify group of assets
        /// </summary>
        /// <param name="driverContext">Container for driver</param>
        /// <param name="myAsserts">
        /// Group asserts
        /// </param>
        public static void That(DriverContext driverContext, params Action[] myAsserts)
        {
            foreach (var myAssert in myAsserts)
            {
                That(driverContext, myAssert, false);
            }

            if (!driverContext.VerifyMessages.Count.Equals(0))
            {
                driverContext.TakeAndSaveScreenshot();
            }
        }

        /// <summary>
        /// Verify assert conditions
        /// </summary>
        /// <param name="driverContext">Container for driver</param>
        /// <param name="myAssert">
        /// Assert condition
        /// </param>
        /// <param name="enableScreenShot">
        /// Enabling screenshot
        /// </param>
        public static void That(DriverContext driverContext, Action myAssert, bool enableScreenShot)
        {
            try
            {
                myAssert();
            }
            catch (Exception e)
            {
                if (enableScreenShot)
                {
                    driverContext.TakeAndSaveScreenshot();
                }

                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));

                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
            }
        }

        /// <summary>Verify assert conditions</summary>
        /// <param name="driverContext">Container for driver</param>
        /// <param name="myAssert">Assert condition</param>
        public static void That(DriverContext driverContext, Action myAssert)
        {
            That(driverContext, myAssert, true);
        }
    }
}
