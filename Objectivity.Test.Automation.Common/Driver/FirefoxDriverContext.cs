// <copyright file="FirefoxDriverContext.cs" company="Objectivity Bespoke Software Specialists">
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
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Remote;

    /// <summary>
    /// Contains Firefox WebDriver implementation.
    /// </summary>
    public class FirefoxDriverContext : CommonDriver
    {
        /// <summary>
        /// Gets Selenium WebDriver for Firefox
        /// </summary>
        public override IWebDriver GetDriver
        {
            get
            {
                return new FirefoxDriver(this.SetDriverOptions(this.FirefoxOptions));
            }
        }

        /// <summary>
        /// Gets Selenium RemoteWebDriver for Firefox
        /// </summary>
        public override IWebDriver GetRemoteDriver
        {
            get
            {
                FirefoxOptions firefoxOptions = new FirefoxOptions();
                this.SetRemoteDriverFireFoxOptions(this.DriverCapabilitiesConf, this.Settings, firefoxOptions);
                return new RemoteWebDriver(BaseConfiguration.RemoteWebDriverHub, this.SetDriverOptions(firefoxOptions).ToCapabilities());
            }
        }

        /// <summary>
        /// Gets Firefox options.
        /// </summary>
        protected FirefoxOptions FirefoxOptions
        {
            get
            {
                FirefoxOptions options = new FirefoxOptions();

                if (BaseConfiguration.UseDefaultFirefoxProfile)
                {
                    try
                    {
                        var pathToCurrentUserProfiles = BaseConfiguration.PathToFirefoxProfile; // Path to profile
                        var pathsToProfiles = Directory.GetDirectories(pathToCurrentUserProfiles, "*.default", SearchOption.TopDirectoryOnly);

                        options.Profile = new FirefoxProfile(pathsToProfiles[0]);
                    }
                    catch (DirectoryNotFoundException e)
                    {
                        logger.Info(CultureInfo.CurrentCulture, "problem with loading firefox profile {0}", e.Message);
                    }
                }

                options.SetPreference("toolkit.startup.max_resumed_crashes", "999999");
                options.SetPreference("network.automatic-ntlm-auth.trusted-uris", BaseConfiguration.Host ?? string.Empty);

                // retrieving settings from config file
                var firefoxPreferences = ConfigurationManager.GetSection("FirefoxPreferences") as NameValueCollection;
                var firefoxExtensions = ConfigurationManager.GetSection("FirefoxExtensions") as NameValueCollection;

                // preference for downloading files
                options.SetPreference("browser.download.dir", this.DownloadFolder);
                options.SetPreference("browser.download.folderList", 2);
                options.SetPreference("browser.download.managershowWhenStarting", false);
                options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/vnd.ms-excel, application/x-msexcel, application/pdf, text/csv, text/html, application/octet-stream");

                // disable Firefox's built-in PDF viewer
                options.SetPreference("pdfjs.disabled", true);

                // disable Adobe Acrobat PDF preview plugin
                options.SetPreference("plugin.scan.Acrobat", "99.0");
                options.SetPreference("plugin.scan.plid.all", false);

                options.UseLegacyImplementation = BaseConfiguration.FirefoxUseLegacyImplementation;

                // set browser proxy for Firefox
                if (!string.IsNullOrEmpty(BaseConfiguration.Proxy))
                {
                    options.Proxy = this.CurrentProxy();
                }

                // if there are any extensions
                if (firefoxExtensions != null)
                {
                    // loop through all of them
                    for (var i = 0; i < firefoxExtensions.Count; i++)
                    {
                        logger.Trace(CultureInfo.CurrentCulture, "Installing extension {0}", firefoxExtensions.GetKey(i));
                        options.Profile.AddExtension(firefoxExtensions.GetKey(i));
                    }
                }

                // custom preferences
                // if there are any settings
                if (firefoxPreferences == null)
                {
                    return options;
                }

                // loop through all of them
                for (var i = 0; i < firefoxPreferences.Count; i++)
                {
                    logger.Trace(CultureInfo.CurrentCulture, "Set custom preference '{0},{1}'", firefoxPreferences.GetKey(i), firefoxPreferences[i]);

                    // and verify all of them
                    switch (firefoxPreferences[i])
                    {
                        // if current settings value is "true"
                        case "true":
                            options.SetPreference(firefoxPreferences.GetKey(i), true);
                            break;

                        // if "false"
                        case "false":
                            options.SetPreference(firefoxPreferences.GetKey(i), false);
                            break;

                        // otherwise
                        default:
                            int temp;

                            // an attempt to parse current settings value to an integer. Method TryParse returns True if the attempt is successful (the string is integer) or return False (if the string is just a string and cannot be cast to a number)
                            if (int.TryParse(firefoxPreferences.Get(i), out temp))
                            {
                                options.SetPreference(firefoxPreferences.GetKey(i), temp);
                            }
                            else
                            {
                                options.SetPreference(firefoxPreferences.GetKey(i), firefoxPreferences[i]);
                            }

                            break;
                    }
                }

                return options;
            }
        }

        private void SetRemoteDriverFireFoxOptions(NameValueCollection driverCapabilitiesConf, NameValueCollection settings, FirefoxOptions firefoxOptions)
        {
            // if there are any capability
            if (driverCapabilitiesConf != null)
            {
                // loop through all of them
                for (var i = 0; i < driverCapabilitiesConf.Count; i++)
                {
                    string value = driverCapabilitiesConf.GetValues(i)[0];
                    logger.Trace(CultureInfo.CurrentCulture, "Adding driver capability {0}", driverCapabilitiesConf.GetKey(i));
                    firefoxOptions.AddAdditionalCapability(driverCapabilitiesConf.GetKey(i), value, true);
                }
            }

            foreach (string key in settings.AllKeys)
            {
                logger.Trace(CultureInfo.CurrentCulture, "Adding driver capability {0} from {1}", key, this.CrossBrowserEnvironment);

                firefoxOptions.AddAdditionalCapability(key, settings[key], true);
            }
        }
    }
}
