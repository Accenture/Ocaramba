// <copyright file="Verify.cs" company="Objectivity Bespoke Software Specialists">
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
    using System;
    using System.Globalization;

    using NLog;

    using Ocaramba.Types;

    /// <summary>
    /// Class for assert without stop tests <see href="https://github.com/ObjectivityLtd/Ocaramba/wiki/Verify-asserts-without-stop-tests">More details on wiki</see>.
    /// </summary>
    public static class Verify
    {
#if net47 || net45
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
#endif
#if netcoreapp3_1
        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
#endif

        /// <summary>
        /// Verify group of assets.
        /// </summary>
        /// <param name="driverContext">Container for driver.</param>
        /// <param name="myAsserts">Group asserts.</param>
        /// <example>How to use it: <code>
        /// Verify.That(
        ///     this.DriverContext,
        ///     () => Assert.AreEqual(5 + 7 + 2, forgotPassword.EnterEmail(5, 7, 2)),
        ///     () => Assert.AreEqual("Your e-mail's been sent!", forgotPassword.ClickRetrievePassword));
        /// </code></example>
        public static void That(DriverContext driverContext, params Action[] myAsserts)
        {
            That(driverContext, false, false, myAsserts);
        }

        /// <summary>
        /// Verify group of assets.
        /// </summary>
        /// <param name="driverContext">Container for driver.</param>
        /// <param name="enableScreenShot">Enable screenshot.</param>
        /// <param name="enableSavePageSource">Enable save page source.</param>
        /// <param name="myAsserts">Group asserts.</param>
        /// <example>How to use it: <code>
        /// Verify.That(
        ///     this.DriverContext,
        ///     true,
        ///     false,
        ///     () => Assert.AreEqual(5 + 7 + 2, forgotPassword.EnterEmail(5, 7, 2)),
        ///     () => Assert.AreEqual("Your e-mail's been sent!", forgotPassword.ClickRetrievePassword));
        /// </code></example>
        public static void That(DriverContext driverContext, bool enableScreenShot, bool enableSavePageSource, params Action[] myAsserts)
        {
            foreach (var myAssert in myAsserts)
            {
                That(driverContext, myAssert, false, false);
            }

            if (!driverContext.VerifyMessages.Count.Equals(0) && enableScreenShot)
            {
                driverContext.TakeAndSaveScreenshot();
            }

            if (!driverContext.VerifyMessages.Count.Equals(0) && enableSavePageSource)
            {
                driverContext.SavePageSource(driverContext.TestTitle);
            }
        }

        /// <summary>
        /// Verify assert conditions.
        /// </summary>
        /// <param name="driverContext">Container for driver.</param>
        /// <param name="myAssert">Assert condition.</param>
        /// <param name="enableScreenShot">Enabling screenshot.</param>
        /// <param name="enableSavePageSource">Enable save page source.</param>
        /// <example>How to use it: <code>
        /// Verify.That(this.DriverContext, () => Assert.IsFalse(flag), true);
        /// </code></example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "removed ref to unit test")]
        public static void That(DriverContext driverContext, Action myAssert, bool enableScreenShot, bool enableSavePageSource)
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

                if (enableSavePageSource)
                {
                    driverContext.SavePageSource(driverContext.TestTitle);
                }

                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));

                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
            }
        }

        /// <summary>
        /// Verify assert conditions.
        /// </summary>
        /// <param name="driverContext">Container for driver.</param>
        /// <param name="myAssert">Assert condition.</param>
        /// <example>How to use it: <code>
        /// Verify.That(this.DriverContext, () => Assert.AreEqual(parameters["number"], links.CountLinks()));
        /// </code></example>
        public static void That(DriverContext driverContext, Action myAssert)
        {
            That(driverContext, myAssert, false, false);
        }
    }
}
