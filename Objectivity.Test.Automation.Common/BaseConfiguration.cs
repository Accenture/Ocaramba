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
        /// Gets the Driver.
        /// </summary>
        public static DriverContext.BrowserType TestBrowser
        {
            get
            {
                DriverContext.BrowserType browserType;
                bool supportedBrowser = Enum.TryParse(ConfigurationManager.AppSettings["browser"], out browserType);

                if (supportedBrowser)
                {
                    return browserType;
                }

                return DriverContext.BrowserType.None;
            }
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

        /// <summary>
        /// Enable full desktop screen shot. False by default.
        /// </summary>
        public static bool FullDesktopScreenShotEnabled
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["FullDesktopScreenShotEnabled"]))
                {
                    return false;
                }

                if (ConfigurationManager.AppSettings["FullDesktopScreenShotEnabled"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Enable full desktop screen shot. True by default.
        /// </summary>
        public static bool SeleniumScreenShotEnabled
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SeleniumScreenShotEnabled"]))
                {
                    return true;
                }

                if (ConfigurationManager.AppSettings["SeleniumScreenShotEnabled"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the test folder.
        /// </summary>
        /// <value>
        /// The test folder.
        /// </value>
        public static string TestFolder
        {
            get
            {
                return ConfigurationManager.AppSettings["TestFolder"];
            }
        }

        /// <summary>
        /// Gets the test folder.
        /// </summary>
        /// <value>
        /// The test folder.
        /// </value>
        public static string ScreenShotFolder
        {
            get
            {
                return string.IsNullOrEmpty(ConfigurationManager.AppSettings["ScreenShotFolder"]) ? string.Empty : ConfigurationManager.AppSettings["ScreenShotFolder"];
            }
        }

        /// <summary>
        /// Gets the download folder.
        /// </summary>
        /// <value>
        /// The download folder.
        /// </value>
        public static string DownloadFolder
        {
            get
            {
                return string.IsNullOrEmpty(ConfigurationManager.AppSettings["DownloadFolder"]) ? string.Empty : ConfigurationManager.AppSettings["DownloadFolder"];
            }
        }

        /// <summary>
        /// Use CurrentDirectory and DownloadFolder as download path.
        /// </summary>
        public static bool UseCurrentDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["UseCurrentDirectory"]))
                {
                    return false;
                }

                if (ConfigurationManager.AppSettings["UseCurrentDirectory"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [get page source enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [get page source enabled]; otherwise, <c>false</c>.
        /// </value>
        public static bool GetPageSourceEnabled
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["GetPageSourceEnabled"]))
                {
                    return true;
                }

                if (ConfigurationManager.AppSettings["GetPageSourceEnabled"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }

                return false;
            }
        }
    }
}
