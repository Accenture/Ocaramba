// <copyright file="IEDriverContext.cs" company="Objectivity Bespoke Software Specialists">
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
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Globalization;
    using OpenQA.Selenium;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Remote;

    /// <summary>
    /// Contains IE WebDriver implementation.
    /// </summary>
    public class IEDriverContext : CommonDriver
    {
        /// <summary>
        /// Gets Selenium WebDriver for IE
        /// </summary>
        public override IWebDriver GetDriver
        {
            get
            {
                return new InternetExplorerDriver(this.SetDriverOptions(this.InternetExplorerOptions));
            }
        }

        /// <summary>
        /// Gets Selenium RemoteWebDriver for IE
        /// </summary>
        public override IWebDriver GetRemoteDriver
        {
            get
            {
                InternetExplorerOptions internetExplorerOptions = new InternetExplorerOptions();
                this.SetRemoteDriverOptions(this.DriverCapabilitiesConf, this.Settings, internetExplorerOptions);
                return new RemoteWebDriver(BaseConfiguration.RemoteWebDriverHub, this.SetDriverOptions(internetExplorerOptions).ToCapabilities());
            }
        }

        private InternetExplorerOptions InternetExplorerOptions
        {
            get
            {
                // retrieving settings from config file
                var internetExplorerPreferences = ConfigurationManager.GetSection("InternetExplorerPreferences") as NameValueCollection;
                var options = new InternetExplorerOptions
                {
                    EnsureCleanSession = true,
                    IgnoreZoomLevel = true,
                };

                // set browser proxy for IE
                if (!string.IsNullOrEmpty(BaseConfiguration.Proxy))
                {
                    options.Proxy = this.CurrentProxy();
                }

                // custom preferences
                // if there are any settings
                if (internetExplorerPreferences == null)
                {
                    return options;
                }

                // loop through all of them
                for (var i = 0; i < internetExplorerPreferences.Count; i++)
                {
                    logger.Trace(CultureInfo.CurrentCulture, "Set custom preference '{0},{1}'", internetExplorerPreferences.GetKey(i), internetExplorerPreferences[i]);

                    // and verify all of them
                    switch (internetExplorerPreferences.GetKey(i))
                    {
                        case "EnsureCleanSession":
                            options.EnsureCleanSession = Convert.ToBoolean(internetExplorerPreferences[i], CultureInfo.CurrentCulture);
                            break;

                        case "IgnoreZoomLevel":
                            options.IgnoreZoomLevel = Convert.ToBoolean(internetExplorerPreferences[i], CultureInfo.CurrentCulture);
                            break;
                    }
                }

                return options;
            }
        }
    }
}
