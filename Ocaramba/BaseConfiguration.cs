// <copyright file="BaseConfiguration.cs" company="Objectivity Bespoke Software Specialists">
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

using System.Collections.Generic;
using System.Collections.Specialized;
#if netcoreapp2_2
using Microsoft.Extensions.Configuration;
#endif
namespace Ocaramba
{
    using System;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Globalization;
    using NLog;
    using Ocaramba;

    /// <summary>
    /// SeleniumConfiguration that consume app.config file <see href="https://github.com/ObjectivityLtd/Ocaramba/wiki/Description%20of%20App.config%20file">More details on wiki</see>
    /// </summary>
    public static class BaseConfiguration
    {


        /// <summary>
        /// The logger
        /// </summary>
#if net45
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
#endif
#if netcoreapp2_2
        static string env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        public static readonly IConfigurationRoot Builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{env}.json", true, true)
            .Build();
        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
#endif

        /// <summary>
        /// Gets the Driver.
        /// </summary>
        /// <example>How to use it: <code>
        /// if (BaseConfiguration.TestBrowser == BrowserType.Firefox)
        ///     {
        ///     this.Driver.GetElement(this.fileLink.Format(fileName), "Click on file").Click();
        ///     };
        /// </code></example>
        public static BrowserType TestBrowser
        {
            get
            {
                BrowserType browserType;
                bool supportedBrowser = false;
                string setting = null;
#if netcoreapp2_2
                setting = Builder["appSettings:browser"];
#endif
#if net45
                setting = ConfigurationManager.AppSettings["browser"];

#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Browser value from AppConfig '{0}'", setting);
                supportedBrowser = Enum.TryParse(setting, out browserType);
                if (supportedBrowser)
                {
                    return browserType;
                }
                return BrowserType.None;
            }
        }

        /// <summary>
        /// Gets the Driver capabilities.
        /// </summary>
        public static BrowserType TestBrowserCapabilities
        {
            get
            {
                BrowserType browserType;
                bool supportedBrowser = false;
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["DriverCapabilities"];
#endif
#if netcoreapp2_2
                setting = Builder["appSettings:DriverCapabilities"];
#endif

                Logger.Trace(CultureInfo.CurrentCulture, "Driver Capabilities value from App.config '{0}'", setting);

                supportedBrowser = Enum.TryParse(setting, out browserType);
                if (supportedBrowser)
                {
                    return browserType;
                }

                return BrowserType.None;
            }
        }

        /// <summary>
        /// Gets the path to firefox profile.
        /// </summary>
        public static string PathToFirefoxProfile
        {
            get
            {
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["PathToFirefoxProfile"];
#endif
#if netcoreapp2_2
                setting = Builder["appSettings:DriverCapabilities"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Gets the path to firefox profile from App.config '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return string.Empty;
                }
                return setting;
            }
        }

        /// <summary>
        /// Gets the application protocol (http or https).
        /// </summary>
        public static string Protocol
        {
            get
            {
#if net45
                return ConfigurationManager.AppSettings["protocol"];
#endif
#if  netcoreapp2_2
                return Builder["appSettings:protocol"];          
#endif
            }
        }

        /// <summary>
        /// Gets the application host name.
        /// </summary>
        public static string Host
        {
            get
            {
#if net45
                return ConfigurationManager.AppSettings["host"];
#endif
#if  netcoreapp2_2
                return Builder["appSettings:host"];
#endif
            }
        }

        /// <summary>
        /// Gets the url.
        /// </summary>
        public static string Url
        {
            get
            {
#if net45
                return ConfigurationManager.AppSettings["url"];
#endif
#if netcoreapp2_2
                return Builder["appSettings:url"];
#endif
            }
        }

        /// <summary>
        /// Gets the browser proxy.
        /// </summary>
        public static string Proxy
        {
            get
            {
#if net45
                return ConfigurationManager.AppSettings["proxy"];
#endif
#if netcoreapp2_2
                return Builder["appSettings:proxy"];
#endif
            }
        }

        /// <summary>
        /// Gets the username.
        /// </summary>
        public static string Username
        {
            get
            {
#if net45
                return ConfigurationManager.AppSettings["username"];
#endif
#if netcoreapp2_2
                return Builder["appSettings:username"];
#endif
            }
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        public static string Password
        {
            get
            {
#if net45
                return ConfigurationManager.AppSettings["password"];
#endif
#if netcoreapp2_2
                return Builder["appSettings:password"];
#endif
            }
        }

        /// <summary>
        /// Gets the java script or ajax waiting time [seconds].
        /// </summary>
        /// <example>How to use it: <code>
        /// this.Driver.IsElementPresent(this.statusCodeHeader, BaseConfiguration.MediumTimeout);
        /// </code></example>
        public static double MediumTimeout
        {
            get
            {
#if net45
                return Convert.ToDouble(ConfigurationManager.AppSettings["mediumTimeout"], CultureInfo.CurrentCulture);
#endif
#if netcoreapp2_2
                return Convert.ToDouble(Builder["appSettings:mediumTimeout"]);
#endif
            }
        }

        /// <summary>
        /// Gets the page load waiting time [seconds].
        /// </summary>
        /// <example>How to use it: <code>
        /// element.GetElement(locator, BaseConfiguration.LongTimeout, e => e.Displayed, customMessage);
        /// </code></example>
        public static double LongTimeout
        {
            get
            {
#if net45
                return Convert.ToDouble(ConfigurationManager.AppSettings["longTimeout"], CultureInfo.CurrentCulture);
#endif
#if netcoreapp2_2
                return Convert.ToDouble(Builder["appSettings:longTimeout"]);
#endif
            }
        }

        /// <summary>
        /// Gets the assertion waiting time [seconds].
        /// </summary>
        /// <example>How to use it: <code>
        /// this.Driver.IsElementPresent(this.downloadPageHeader, BaseConfiguration.ShortTimeout);
        /// </code></example>
        public static double ShortTimeout
        {
            get
            {
#if net45
                return Convert.ToDouble(ConfigurationManager.AppSettings["shortTimeout"], CultureInfo.CurrentCulture);
#endif
#if netcoreapp2_2
                return Convert.ToDouble(Builder["appSettings:shortTimeout"]);
#endif
            }
        }

        /// <summary>
        /// Gets the Implicitly Wait time [milliseconds].
        /// </summary>
        public static double ImplicitlyWaitMilliseconds
        {
            get
            {
#if net45
                return Convert.ToDouble(ConfigurationManager.AppSettings["ImplicitlyWaitMilliseconds"], CultureInfo.CurrentCulture);
#endif
#if netcoreapp2_2
                return Convert.ToDouble(Builder["appSettings:ImplicitlyWaitMilliseconds"]);
#endif
            }
        }

        /// <summary>
        /// Gets the path and file name of the Firefox browser executable
        /// </summary>
        public static string FirefoxBrowserExecutableLocation
        {
            get
            {
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["FirefoxBrowserExecutableLocation"];
#endif
#if netcoreapp2_2
                setting = Builder["appSettings:FirefoxBrowserExecutableLocation"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Gets the path and file name of the Firefox browser executable from App.config '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return string.Empty;
                }

                return setting;
            }
        }

        /// <summary>
        /// Gets the path and file name of the Chrome browser executable
        /// </summary>
        public static string ChromeBrowserExecutableLocation
        {
            get
            {
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["ChromeBrowserExecutableLocation"];
#endif
#if netcoreapp2_2
                setting = Builder["appSettings:ChromeBrowserExecutableLocation"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Gets the path and file name of the Chrome browser executable from App.config '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return string.Empty;
                }

                return setting;
            }
        }

        /// <summary>
        /// Gets the Remote Web Driver hub url
        /// </summary>
        public static Uri RemoteWebDriverHub
        {
            get
            {
#if  net45
                return new Uri(ConfigurationManager.AppSettings["RemoteWebDriverHub"]);
#endif
#if netcoreapp2_2
                return new Uri(Builder["appSettings:RemoteWebDriverHub"]);
#endif
            }
        }

        /// <summary>
        /// Gets a value indicating whether enable full desktop screen shot. False by default.
        /// </summary>
        public static bool FullDesktopScreenShotEnabled
        {
            get
            {
#if net45
                Logger.Trace(CultureInfo.CurrentCulture, "Full Desktop Screen Shot Enabled value from App.config '{0}'", ConfigurationManager.AppSettings["FullDesktopScreenShotEnabled"]);
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["FullDesktopScreenShotEnabled"]))
                {
                    return false;
                }

                if (ConfigurationManager.AppSettings["FullDesktopScreenShotEnabled"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }
#endif
                return false;
            }
        }

        /// <summary>
        /// Gets specified path to the directory containing InternetExplorer Driver.
        /// </summary>
        public static string PathToInternetExplorerDriverDirectory
        {
            get
            {
#if net45
                Logger.Trace(CultureInfo.CurrentCulture, "Path to the directory containing Internet Explorer Driver from App.config '{0}'", ConfigurationManager.AppSettings["PathToInternetExplorerDriverDirectory"]);
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["PathToInternetExplorerDriverDirectory"]))
                {
                    return string.Empty;
                }

                return ConfigurationManager.AppSettings["PathToInternetExplorerDriverDirectory"];
#endif
#if netcoreapp2_2
                return Builder["appSettings:PathToInternetExplorerDriverDirectory"];
#endif
            }
        }

        /// <summary>
        /// Gets specified path to the directory containing Edge Driver.
        /// </summary>
        public static string PathToEdgeDriverDirectory
        {
            get
            {
#if net45
                Logger.Trace(CultureInfo.CurrentCulture, "Path to the directory containing Edge Driver from App.config '{0}'", ConfigurationManager.AppSettings["PathToEdgeDriverDirectory"]);
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["PathToEdgeDriverDirectory"]))
                {
                    return string.Empty;
                }

                return ConfigurationManager.AppSettings["PathToEdgeDriverDirectory"];
#endif
#if netcoreapp2_2
                return Builder["appSettings:PathToEdgeDriverDirectory"];
#endif
            }
        }

        /// <summary>
        /// Gets specified path to the directory containing ChromeDriver.
        /// </summary>
        public static string PathToChromeDriverDirectory
        {
            get
            {
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["PathToChromeDriverDirectory"];
#endif
#if netcoreapp2_2
                setting = Builder["appSettings:PathToChromeDriverDirectory"];
#endif

                Logger.Trace(CultureInfo.CurrentCulture, "Path to the directory containing Chrome Driver from App.config '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return string.Empty;
                }

                return setting;
            }
        }

        /// <summary>
        /// Gets specified path to the directory containing Firefox Driver.
        /// </summary>
        public static string PathToFirefoxDriverDirectory
        {
            get
            {
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["PathToFirefoxDriverDirectory"];
#endif
#if netcoreapp2_2
                setting = Builder["appSettings:PathToFirefoxDriverDirectory"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Path to the directory containing Firefox Driver from App.config '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return string.Empty;
                }

                return setting;
            }
        }

        /// <summary>
        /// Gets a value indicating whether logs JavaScript errors from a browser. False by default.
        /// </summary>
        public static bool JavaScriptErrorLogging
        {
            get
            {
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["JavaScriptErrorLogging"];
#endif
#if netcoreapp2_2
                setting = Builder["appSettings:JavaScriptErrorLogging"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "JavaScript error logging value from App.config '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return false;
                }

                if (setting.ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Gets JavaScript error types from a browser. "SyntaxError,EvalError,ReferenceError,RangeError,TypeError,URIError,Refused to display,Internal Server Error,Cannot read property" by default.
        /// </summary>
        public static Collection<string> JavaScriptErrorTypes
        {
            get
            {
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["JavaScriptErrorTypes"];
#endif
#if netcoreapp2_2
                setting = Builder["appSettings:JavaScriptErrorTypes"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "JavaScript error logging value from App.config '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return new Collection<string>
                    {
                        "SyntaxError",
                        "EvalError",
                        "ReferenceError",
                        "RangeError",
                        "TypeError",
                        "URIError",
                        "Refused to display",
                        "Internal Server Error",
                        "Cannot read property"
                    };
                }

                return new Collection<string>(setting.Split(new char[] { ',' }));
            }
        }

        /// <summary>
        /// Gets a value indicating whether enable legacy implementation for Firefox.
        /// </summary>
        public static bool FirefoxUseLegacyImplementation
        {
            get
            {
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["FirefoxUseLegacyImplementation"];
#endif
#if netcoreapp2_2
                setting = Builder["appSettings:FirefoxUseLegacyImplementation"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Firefox Use Legacy Implementation Enabled value from App.config '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return false;
                }

                if (setting.ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether enable full desktop screen shot. True by default.
        /// </summary>
        public static bool SeleniumScreenShotEnabled
        {
            get
            {
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["SeleniumScreenShotEnabled"];
#endif
#if netcoreapp2_2
                setting = Builder["appSettings:SeleniumScreenShotEnabled"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Selenium Screen Shot Enabled value from App.config '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return true;
                }

                if (setting.ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether enable EventFiringWebDriver.
        /// </summary>
        public static bool EnableEventFiringWebDriver
        {
            get
            {
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["EnableEventFiringWebDriver"];
#endif
#if netcoreapp2_2
                setting = Builder["appSettings:EnableEventFiringWebDriver"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Enable EventFiringWebDriver from App.config '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return false;
                }

                if (setting.ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether use CurrentDirectory for path where assembly files are located.
        /// </summary>
        public static bool UseCurrentDirectory
        {
            get
            {
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["UseCurrentDirectory"];
#endif
#if netcoreapp2_2
                setting = Builder["appSettings:UseCurrentDirectory"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Use Current Directory value from App.config '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return false;
                }

                if (setting.ToLower(CultureInfo.CurrentCulture).Equals("true"))
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
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["GetPageSourceEnabled"];
#endif
#if netcoreapp2_2
                setting = Builder["appSettings:GetPageSourceEnabled"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Get Page Source Enabled value from App.config '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return true;
                }

                if (setting.ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Gets the download folder key value
        /// </summary>
        public static string DownloadFolder
        {
            get
            {
#if net45
                return ConfigurationManager.AppSettings["DownloadFolder"];
#endif
#if netcoreapp2_2
                return Builder["appSettings:DownloadFolder"];
#endif
            }
        }

        /// <summary>
        /// Gets the screen shot folder key value
        /// </summary>
        public static string ScreenShotFolder
        {
            get
            {
#if net45
                return ConfigurationManager.AppSettings["ScreenShotFolder"];
#endif
#if netcoreapp2_2
                return Builder["appSettings:ScreenShotFolder"];
#endif
            }
        }

        /// <summary>
        /// Gets the page source folder key value
        /// </summary>
        public static string PageSourceFolder
        {
            get
            {
#if net45
                return ConfigurationManager.AppSettings["PageSourceFolder"];
#endif
#if netcoreapp2_2
                return Builder["appSettings:PageSourceFolder"];
#endif
            }
        }

        /// <summary>
        /// Gets the URL value 'Protocol://HostURL'.
        /// </summary>
        /// <example>How to use it: <code>
        /// var url = BaseConfiguration.GetUrlValue;
        /// </code></example>
        public static string GetUrlValue
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture, "{0}://{1}{2}", Protocol, Host, Url);
            }
        }

        /// <summary>
        /// Gets the URL value with user credentials 'Protocol://Username:Password@HostURL'.
        /// </summary>
        /// <example>How to use it: <code>
        /// var url = BaseConfiguration.GetUrlValueWithUserCredentials;
        /// </code></example>
        public static string GetUrlValueWithUserCredentials
        {
            get
            {
                return string.Format(
                    CultureInfo.CurrentCulture,
                    "{0}://{1}:{2}@{3}{4}",
                    Protocol,
                    Username,
                    Password,
                    Host,
                    Url);
            }
        }

        /// <summary>
        /// Gets a value indicating whether enable AngularJS synchronization. False by default.
        /// </summary>
        public static bool SynchronizationWithAngularEnabled
        {
            get
            {
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["SynchronizationWithAngularEnabled"];
#endif
#if  netcoreapp2_2
                setting = Builder["appSettings:SynchronizationWithAngularEnabled"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Angular synchronization Enabled value from App.config '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return false;
                }

                if (setting.ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }
                return false;
            }
        }
#if  netcoreapp2_2
        /// <summary>
        /// Converting settings from appsettings.json into the NameValueCollection, key - value pairs 
        /// </summary>
        /// <param name="preferences">Section name in appsettings.json file</param>
        /// <returns></returns>
        public static NameValueCollection GetNameValueCollectionFromAppsettings(string preferences)
        {
            NameValueCollection preferencesCollection = new NameValueCollection();
            var jsnonSettings = Builder.GetSection(preferences).Get<Dictionary<string, string>>();
            if (jsnonSettings == null)
            {
                return preferencesCollection;
            }

            foreach (var kvp in jsnonSettings)
            {
                string value = null;
                if (kvp.Value != null)
                    value = kvp.Value.ToString();

                preferencesCollection.Add(kvp.Key.ToString(), value);
            }

            return preferencesCollection;
        }
#endif
    }
}
