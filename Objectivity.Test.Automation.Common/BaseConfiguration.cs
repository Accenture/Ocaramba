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
    using System.Configuration;
    using System.Globalization;

    /// <summary>
    /// SeleniumConfiguration that consume app.config file
    /// </summary>
    public static class BaseConfiguration
    {
        /// <summary>
        /// Gets the browser.
        /// </summary>
        public static string TestBrowser
        {
            get { return ConfigurationManager.AppSettings["browser"]; }
        }

        /// <summary>
        /// Gets the application protocol (http or https).
        /// </summary>
        public static string Protocol
        {
            get { return ConfigurationManager.AppSettings["protocol"]; }
        }

        /// <summary>
        /// Gets the application host name.
        /// </summary>
        public static string Host
        {
            get { return ConfigurationManager.AppSettings["host"]; }
        }

        /// <summary>
        /// Gets the url.
        /// </summary>
        public static string Url
        {
            get { return ConfigurationManager.AppSettings["url"]; }
        }

        /// <summary>
        /// Gets the username.
        /// </summary>
        public static string Username
        {
            get { return ConfigurationManager.AppSettings["username"]; }
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        public static string Password
        {
            get { return ConfigurationManager.AppSettings["password"]; }
        }

        /// <summary>
        /// Gets the java script or ajax waiting time.
        /// </summary>
        public static double MediumTimeout
        {
            get { return Convert.ToDouble(ConfigurationManager.AppSettings["mediumTimeout"], CultureInfo.CurrentCulture); }
        }

        /// <summary>
        /// Gets the page load waiting time.
        /// </summary>
        public static double LongTimeout
        {
            get { return Convert.ToDouble(ConfigurationManager.AppSettings["longTimeout"], CultureInfo.CurrentCulture); }
        }

        /// <summary>
        /// Gets the assertion waiting time.
        /// </summary>
        public static double ShortTimeout
        {
            get { return Convert.ToDouble(ConfigurationManager.AppSettings["shortTimeout"], CultureInfo.CurrentCulture); }
        }

        /// <summary>
        /// Gets the firefox path
        /// </summary>
        public static string FirefoxPath
        {
            get
            {
                return ConfigurationManager.AppSettings["FireFoxPath"];
            }
        }

        /// <summary>
        /// Gets the data driven file.
        /// </summary>
        /// <value>
        /// The data driven file.
        /// </value>
        public static string DataDrivenFile
        {
            get
            {
                return ConfigurationManager.AppSettings["DataDrivenFile"];
            }
        }
    }
}
