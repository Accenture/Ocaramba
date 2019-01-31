// <copyright file="CommonDriver.cs" company="Objectivity Bespoke Software Specialists">
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
    using NLog;
    using Objectivity.Test.Automation.Common.Helpers;
    using OpenQA.Selenium;

    /// <summary>
    /// Contains common fields, properties and methods for Selenium Driver
    /// </summary>
    public abstract class CommonDriver
    {
        /// <summary>
        /// Logger field.
        /// </summary>
        protected static NLog.Logger logger = LogManager.GetLogger("DRIVER");

        /// <summary>
        /// Occurs when [driver options set].
        /// </summary>
        public event EventHandler<DriverOptionsSetEventArgs> DriverOptionsSet;

        /// <summary>
        /// Gets Selenium WebDriver
        /// </summary>
        public abstract IWebDriver GetDriver { get; }

        /// <summary>
        /// Gets Selenium RemoteWebDriver
        /// </summary>
        public abstract IWebDriver GetRemoteDriver { get; }

        /// <summary>
        /// Gets Sets Folder name for Download
        /// </summary>
        public string DownloadFolder
        {
            get
            {
                return FilesHelper.GetFolder(BaseConfiguration.DownloadFolder, this.CurrentDirectory);
            }
        }

        /// <summary>
        /// Gets or sets directory where assembly files are located
        /// </summary>
        public string CurrentDirectory { get; set; }

        /// <summary>
        /// Gets or sets the Environment Browsers from App.config
        /// </summary>
        public BrowserType CrossBrowserEnvironment { get; set; }

        /// <summary>
        /// Gets or sets the driver capabilities collection.
        /// </summary>
        protected NameValueCollection DriverCapabilitiesConf => ConfigurationManager.GetSection("DriverCapabilities") as NameValueCollection;

        /// <summary>
        /// Gets or sets the environment settings collection.
        /// </summary>
        protected NameValueCollection Settings => ConfigurationManager.GetSection("environments/" + this.CrossBrowserEnvironment) as NameValueCollection;

        /// <summary>
        /// Sets the driver options.
        /// </summary>
        /// <typeparam name="T">The type of DriverOptions for the specific Browser</typeparam>
        /// <param name="options">The options.</param>
        /// <returns>
        /// The Driver Options
        /// </returns>
        protected T SetDriverOptions<T>(T options)
            where T : DriverOptions
        {
            this.DriverOptionsSet?.Invoke(this, new DriverOptionsSetEventArgs(options));
            return options;
        }

        /// <summary>
        /// Proxy configuration.
        /// </summary>
        /// <returns>Proxy settings.</returns>
        protected Proxy CurrentProxy()
        {
            Proxy proxy = new Proxy
            {
                HttpProxy = BaseConfiguration.Proxy,
                FtpProxy = BaseConfiguration.Proxy,
                SslProxy = BaseConfiguration.Proxy,
                SocksProxy = BaseConfiguration.Proxy
            };
            return proxy;
        }

        /// <summary>
        /// Saves the page source.
        /// </summary>
        /// <typeparam name="T">The type of Remote DriverOptions for the specific Browser</typeparam>
        /// <param name="driverCapabilitiesConf">Driver capabilities settings collection.</param>
        /// <param name="settings">Settings collection.</param>
        /// <param name="options">Remote WebDriver options.</param>
        /// <returns>The saved source file</returns>
        protected T SetRemoteDriverOptions<T>(NameValueCollection driverCapabilitiesConf, NameValueCollection settings, T options)
            where T : DriverOptions
        {
            // if there are any capability
            if (driverCapabilitiesConf != null)
            {
                // loop through all of them
                for (var i = 0; i < driverCapabilitiesConf.Count; i++)
                {
                    string value = driverCapabilitiesConf.GetValues(i)[0];
                    logger.Trace(CultureInfo.CurrentCulture, "Adding driver capability {0}", driverCapabilitiesConf.GetKey(i));
                    options.AddAdditionalCapability(driverCapabilitiesConf.GetKey(i), value);
                }
            }

            foreach (string key in settings.AllKeys)
            {
                logger.Trace(CultureInfo.CurrentCulture, "Adding driver capability {0} from {1}", key, this.CrossBrowserEnvironment);

                options.AddAdditionalCapability(key, settings[key]);
            }

            return options;
        }
    }
}
