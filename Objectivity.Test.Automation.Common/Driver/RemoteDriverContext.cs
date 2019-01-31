// <copyright file="RemoteDriverContext.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.Common.Driver
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using OpenQA.Selenium;

    /// <summary>
    /// Contains Remote WebDriver implementation.
    /// </summary>
    public class RemoteDriverContext
    {
        private readonly FirefoxDriverContext ffContext = new FirefoxDriverContext();
        private readonly FirefoxPortableDriverContext ffpContext = new FirefoxPortableDriverContext();
        private readonly ChromeDriverContext chromeContext = new ChromeDriverContext();
        private readonly SafariDriverContext safariContext = new SafariDriverContext();
        private readonly IEDriverContext ieDriverContext = new IEDriverContext();
        private readonly EdgeDriverContext edgeContext = new EdgeDriverContext();

        /// <summary>
        /// Gets Selenium WebDriver for Remote WebDriver
        /// </summary>
        [SuppressMessage("Microsoft.Reliability", "CA1024:RemoteDriverContext.GetDriver()' to a property if appropriate", Justification = "Driver disposed later in stop method")]
        public IWebDriver GetDriver
        {
            get
            {
                BrowserType browserType = BrowserType.Chrome;

                try
                {
                    return this.CreateRemoteDriverDictionary()[browserType];
                }
                catch
                {
                    throw new NotSupportedException(
                            string.Format(CultureInfo.CurrentCulture, "Driver {0} is not supported", BaseConfiguration.TestBrowser));
                }
            }
        }

        /// <summary>
        /// Dictionary that contains references to all remote web driver types.
        /// </summary>
        /// <returns>Dictionary with Selenium Remote WebDriver reference.</returns>
        public Dictionary<BrowserType, IWebDriver> CreateRemoteDriverDictionary()
        {
            var dictionary = new Dictionary<BrowserType, IWebDriver>
                {
                    { BrowserType.Firefox, this.ffContext.GetRemoteDriver },
                    { BrowserType.FirefoxPortable, this.ffpContext.GetRemoteDriver },
                    { BrowserType.Chrome, this.chromeContext.GetRemoteDriver },
                    { BrowserType.IE, this.ieDriverContext.GetRemoteDriver },
                    { BrowserType.InternetExplorer, this.ieDriverContext.GetRemoteDriver },
                    { BrowserType.Edge, this.edgeContext.GetRemoteDriver },
                    { BrowserType.Safari, this.safariContext.GetRemoteDriver }
            };

            return dictionary;
        }
    }
}
