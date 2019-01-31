// <copyright file="ChromeDriverContext.cs" company="Objectivity Bespoke Software Specialists">
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
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Remote;

    /// <summary>
    /// Contains Chrome WebDriver implementation.
    /// </summary>
    public class ChromeDriverContext : CommonDriver
    {
        /// <summary>
        /// Gets Selenium WebDriver for Chrome
        /// </summary>
        public override IWebDriver GetDriver
        {
            get
            {
                return new ChromeDriver(@"C:\Users\amucha\Desktop\ObjectivityFramework\Objectivity.Test.Automation.Common", this.SetDriverOptions(this.ChromeOptions));
            }
        }

        /// <summary>
        /// Gets Selenium RemoteWebDriver for Chrome
        /// </summary>
        public override IWebDriver GetRemoteDriver
        {
            get
            {
                ChromeOptions chromeOptions = new ChromeOptions();
                this.SetRemoteDriverChromeOptions(this.DriverCapabilitiesConf, this.Settings, chromeOptions);
                return new RemoteWebDriver(BaseConfiguration.RemoteWebDriverHub, this.SetDriverOptions(chromeOptions).ToCapabilities());
            }
        }

        private ChromeOptions ChromeOptions
        {
            get
            {
                ChromeOptions options = new ChromeOptions();

                // retrieving settings from config file
                var chromePreferences = ConfigurationManager.GetSection("ChromePreferences") as NameValueCollection;
                var chromeExtensions = ConfigurationManager.GetSection("ChromeExtensions") as NameValueCollection;
                var chromeArguments = ConfigurationManager.GetSection("ChromeArguments") as NameValueCollection;

                options.AddUserProfilePreference("profile.default_content_settings.popups", 0);
                options.AddUserProfilePreference("download.default_directory", this.DownloadFolder);
                options.AddUserProfilePreference("download.prompt_for_download", false);

                // set browser proxy for chrome
                if (!string.IsNullOrEmpty(BaseConfiguration.Proxy))
                {
                    options.Proxy = this.CurrentProxy();
                }

                // if there are any extensions
                if (chromeExtensions != null)
                {
                    // loop through all of them
                    for (var i = 0; i < chromeExtensions.Count; i++)
                    {
                        logger.Trace(CultureInfo.CurrentCulture, "Installing extension {0}", chromeExtensions.GetKey(i));
                        options.AddExtension(chromeExtensions.GetKey(i));
                    }
                }

                if (BaseConfiguration.ChromePath != null)
                {
                    logger.Trace(CultureInfo.CurrentCulture, "Setting Chrome Path {0}", BaseConfiguration.ChromePath);
                    options.BinaryLocation = BaseConfiguration.ChromePath;
                }

                // if there are any arguments
                if (chromeArguments != null)
                {
                    // loop through all of them
                    for (var i = 0; i < chromeArguments.Count; i++)
                    {
                        logger.Trace(CultureInfo.CurrentCulture, "Setting Chrome Arguments {0}", chromeArguments.GetKey(i));
                        options.AddArgument(chromeArguments.GetKey(i));
                    }
                }

                // custom preferences
                // if there are any settings
                if (chromePreferences == null)
                {
                    return options;
                }

                // loop through all of them
                for (var i = 0; i < chromePreferences.Count; i++)
                {
                    logger.Trace(CultureInfo.CurrentCulture, "Set custom preference '{0},{1}'", chromePreferences.GetKey(i), chromePreferences[i]);

                    // and verify all of them
                    switch (chromePreferences[i])
                    {
                        // if current settings value is "true"
                        case "true":
                            options.AddUserProfilePreference(chromePreferences.GetKey(i), true);
                            break;

                        // if "false"
                        case "false":
                            options.AddUserProfilePreference(chromePreferences.GetKey(i), false);
                            break;

                        // otherwise
                        default:
                            int temp;

                            // an attempt to parse current settings value to an integer. Method TryParse returns True if the attempt is successful (the string is integer) or return False (if the string is just a string and cannot be cast to a number)
                            if (int.TryParse(chromePreferences.Get(i), out temp))
                            {
                                options.AddUserProfilePreference(chromePreferences.GetKey(i), temp);
                            }
                            else
                            {
                                options.AddUserProfilePreference(chromePreferences.GetKey(i), chromePreferences[i]);
                            }

                            break;
                    }
                }

                return options;
            }
        }

        private void SetRemoteDriverChromeOptions(NameValueCollection driverCapabilitiesConf, NameValueCollection settings, ChromeOptions chromeOptions)
        {
            // if there are any capability
            if (driverCapabilitiesConf != null)
            {
                // loop through all of them
                for (var i = 0; i < driverCapabilitiesConf.Count; i++)
                {
                    string value = driverCapabilitiesConf.GetValues(i)[0];
                    logger.Trace(CultureInfo.CurrentCulture, "Adding driver capability {0}", driverCapabilitiesConf.GetKey(i));
                    chromeOptions.AddAdditionalCapability(driverCapabilitiesConf.GetKey(i), value, true);
                }
            }

            foreach (string key in settings.AllKeys)
            {
                logger.Trace(CultureInfo.CurrentCulture, "Adding driver capability {0} from {1}", key, this.CrossBrowserEnvironment);

                chromeOptions.AddAdditionalCapability(key, settings[key], true);
            }
        }
    }
}
