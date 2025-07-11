﻿// <copyright file="BaseConfiguration.cs" company="Accenture">
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
    using System.Collections.Generic;
    using System.Collections.Specialized;

    using Microsoft.Extensions.Configuration;

    using System;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Globalization;
    using NLog;
    using Ocaramba;

    /// <summary>
    /// SeleniumConfiguration that consume settings file file <see href="https://github.com/ObjectivityLtd/Ocaramba/wiki/Description%20of%20settings file%20file">More details on wiki</see>.
    /// </summary>
    public static class BaseConfiguration
    {
        public static readonly string Env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        /// <summary>
        /// Getting appsettings.json file.
        /// </summary>
        public static readonly IConfigurationRoot Builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{Env}.json", true, true)
            .Build();

        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();


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
                bool supportedBrowser = false;
                string setting = null;

                setting = Builder["appSettings:browser"];

                Logger.Trace(CultureInfo.CurrentCulture, "Browser value from settings file '{0}'", setting);
                supportedBrowser = Enum.TryParse(setting, out BrowserType browserType);
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
                bool supportedBrowser = false;
                string setting = null;
                setting = Builder["appSettings:DriverCapabilities"];


                Logger.Trace(CultureInfo.CurrentCulture, "Driver Capabilities value from settings file '{0}'", setting);

                supportedBrowser = Enum.TryParse(setting, out BrowserType browserType);
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

                setting = Builder["appSettings:PathToFirefoxProfile"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the path to firefox profile from settings file '{0}'", setting);
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
                string setting = null;

                setting = Builder["appSettings:protocol"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the protocol from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the application host name.
        /// </summary>
        public static string Host
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:host"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the protocol from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the url.
        /// </summary>
        public static string Url
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:url"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the url from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the browser proxy.
        /// </summary>
        public static string Proxy
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:proxy"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the url from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the http proxy.
        /// </summary>
        public static string HttpProxy
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:httpProxy"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the httpProxy from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the ftp proxy.
        /// </summary>
        public static string FtpProxy
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:ftpProxy"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the ftpProxy from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the ssl proxy.
        /// </summary>
        public static string SslProxy
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:sslProxy"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the sslProxy from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the socket proxy.
        /// </summary>
        public static string SocksProxy
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:socksproxy"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the socksproxy from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets time used by remote web driver to wait for connection.
        /// </summary>
        public static TimeSpan RemoteWebDriverTimeout
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:remoteTimeout"];

                if (string.IsNullOrEmpty(setting))
                {
                    setting = "60";
                }

                var timeout = int.Parse(setting, CultureInfo.InvariantCulture);

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the remote timeout from settings file '{0}'", timeout);
                return new TimeSpan(0, 0, timeout);
            }
        }

        /// <summary>
        /// Gets the username.
        /// </summary>
        public static string Username
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:username"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the username from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        public static string Password
        {
            get
            {
                string setting = null;


                setting = Builder["appSettings:password"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the password from settings file '{0}'", setting);
                return setting;
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
                double setting;

                setting = Convert.ToDouble(Builder["appSettings:mediumTimeout"], CultureInfo.InvariantCulture);

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the mediumTimeout from settings file '{0}'", setting);
                return setting;
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
                double setting;

                setting = Convert.ToDouble(Builder["appSettings:longTimeout"], CultureInfo.InvariantCulture);

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the longTimeout from settings file '{0}'", setting);
                return setting;
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
                double setting;

                setting = Convert.ToDouble(Builder["appSettings:shortTimeout"], CultureInfo.InvariantCulture);

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the shortTimeout from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the Implicitly Wait time [milliseconds].
        /// </summary>
        public static double ImplicitlyWaitMilliseconds
        {
            get
            {
                double setting;

                setting = Convert.ToDouble(Builder["appSettings:ImplicitlyWaitMilliseconds"], CultureInfo.InvariantCulture);

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the ImplicitlyWaitMilliseconds from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the path and file name of the Firefox browser executable.
        /// </summary>
        public static string FirefoxBrowserExecutableLocation
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:FirefoxBrowserExecutableLocation"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the path and file name of the Firefox browser executable from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return string.Empty;
                }

                return setting;
            }
        }

        /// <summary>
        /// Gets the path and file name of the Edge Chromium browser executable. Default value "C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe".
        /// </summary>
        public static string EdgeChromiumBrowserExecutableLocation
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:EdgeChromiumBrowserExecutableLocation"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the path and file name of the Edge Chromium browser executable from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
                }

                return setting;
            }
        }

        /// <summary>
        /// Gets the path and file name of the Chrome browser executable.
        /// </summary>
        public static string ChromeBrowserExecutableLocation
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:ChromeBrowserExecutableLocation"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the path and file name of the Chrome browser executable from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return string.Empty;
                }

                return setting;
            }
        }

        /// <summary>
        /// Gets the Remote Web Driver hub url.
        /// </summary>
        public static Uri RemoteWebDriverHub
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:RemoteWebDriverHub"];

                Logger.Trace(CultureInfo.CurrentCulture, "RemoteWebDriverHub from settings file '{0}'", setting);
                return new Uri(setting);
            }
        }

        /// <summary>
        /// Gets specified path to the file of Chrome driver log. "C:\\Temp\\chromedriver.log" by Default.
        /// </summary>
        public static string PathToChromeDriverLog
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:PathToChromeDriverLog"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the PathToChromeDriverLog from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return "C:\\Temp\\chromedriver.log";
                }

                return setting;
            }
        }

        /// <summary>
        /// Gets a value indicating whether enable verbose logging for Chrome. False by default.
        /// </summary>
        public static bool EnableVerboseLoggingChrome
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:EnableVerboseLoggingChrome"];

                Logger.Trace(CultureInfo.CurrentCulture, "Verbose logging for Chrome value from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return false;
                }

                if (setting.ToLower(CultureInfo.CurrentCulture).Equals("true", StringComparison.Ordinal))
                {
                    return true;
                }

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
                string setting = null;

                setting = Builder["appSettings:PathToInternetExplorerDriverDirectory"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the PathToInternetExplorerDriverDirectory from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return string.Empty;
                }

                return setting;
            }
        }

        /// <summary>
        /// Gets specified path to the directory containing Edge Chromium Driver, name of driver msedgedriver.exe. Default value "C:\Temp\Drivers".
        /// </summary>
        public static string PathToEdgeChromiumDriverDirectory
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:PathToEdgeChromiumDriverDirectory"];

                Logger.Trace(CultureInfo.CurrentCulture, "Gets the PathToEdgeChromiumDriverDirectory from settings file '{0}'", setting);
                return setting;
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

                setting = Builder["appSettings:PathToChromeDriverDirectory"];


                Logger.Trace(CultureInfo.CurrentCulture, "Path to the directory containing Chrome Driver from settings file '{0}'", setting);
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

                setting = Builder["appSettings:PathToFirefoxDriverDirectory"];

                Logger.Trace(CultureInfo.CurrentCulture, "Path to the directory containing Firefox Driver from settings file '{0}'", setting);
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

                setting = Builder["appSettings:JavaScriptErrorLogging"];

                Logger.Trace(CultureInfo.CurrentCulture, "JavaScript error logging value from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return false;
                }

                if (setting.ToLower(CultureInfo.CurrentCulture).Equals("true", StringComparison.Ordinal))
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

                setting = Builder["appSettings:JavaScriptErrorTypes"];

                Logger.Trace(CultureInfo.CurrentCulture, "JavaScript error logging value from settings file '{0}'", setting);
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
                        "Cannot read property",
                    };
                }

                return new Collection<string>(setting.Split(new char[] { ',' }));
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

                setting = Builder["appSettings:SeleniumScreenShotEnabled"];

                Logger.Trace(CultureInfo.CurrentCulture, "Selenium Screen Shot Enabled value from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return true;
                }

                if (setting.ToLower(CultureInfo.CurrentCulture).Equals("true", StringComparison.Ordinal))
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

                setting = Builder["appSettings:EnableEventFiringWebDriver"];

                Logger.Trace(CultureInfo.CurrentCulture, "Enable EventFiringWebDriver from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return false;
                }

                if (setting.ToLower(CultureInfo.CurrentCulture).Equals("true", StringComparison.Ordinal))
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

                setting = Builder["appSettings:UseCurrentDirectory"];

                Logger.Trace(CultureInfo.CurrentCulture, "Use Current Directory value from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return false;
                }

                if (setting.ToLower(CultureInfo.CurrentCulture).Equals("true", StringComparison.Ordinal))
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


                setting = Builder["appSettings:GetPageSourceEnabled"];

                Logger.Trace(CultureInfo.CurrentCulture, "Get Page Source Enabled value from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return true;
                }

                return setting.ToLower(CultureInfo.CurrentCulture).Equals("true", StringComparison.Ordinal);
            }
        }

        /// <summary>
        /// Gets the download folder key value.
        /// </summary>
        public static string DownloadFolder
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:DownloadFolder"];

                Logger.Trace(CultureInfo.CurrentCulture, "Get DownloadFolder value from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the screen shot folder key value.
        /// </summary>
        public static string ScreenShotFolder
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:ScreenShotFolder"];

                Logger.Trace(CultureInfo.CurrentCulture, "Get ScreenShotFolder value from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the page source folder key value.
        /// </summary>
        public static string PageSourceFolder
        {
            get
            {
                string setting = null;

                setting = Builder["appSettings:PageSourceFolder"];

                Logger.Trace(CultureInfo.CurrentCulture, "Get PageSourceFolder value from settings file '{0}'", setting);
                return setting;
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
                Logger.Trace(CultureInfo.CurrentCulture, "Get Url value from settings file '{0}://{1}{2}'", Protocol, Host, Url);
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
                Logger.Trace(CultureInfo.CurrentCulture, "Get UrlWithUserCredentials value from settings file '{0}://{1}:{2}@{3}{4}'", Protocol, Username, Password, Host, Url);
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

                setting = Builder["appSettings:SynchronizationWithAngularEnabled"];

                Logger.Trace(CultureInfo.CurrentCulture, "Angular synchronization Enabled value from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return false;
                }

                if (setting.ToLower(CultureInfo.CurrentCulture).Equals("true", StringComparison.Ordinal))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Converting settings from appsettings.json into the NameValueCollection, key - value pairs.
        /// </summary>
        /// <param name="preferences">Section name in appsettings.json file.</param>
        /// <returns>Settings.</returns>
        public static NameValueCollection GetNameValueCollectionFromAppsettings(string preferences)
        {
            NameValueCollection preferencesCollection = new NameValueCollection();
            var jsonSettings = Builder.GetSection(preferences).Get<Dictionary<string, object>>();
            if (jsonSettings == null)
            {
                return preferencesCollection;
            }

            foreach (var kvp in jsonSettings)
            {
                string value = null;
                if (kvp.Value != null)
                {
                    value = kvp.Value.ToString();
                }

                preferencesCollection.Add(kvp.Key.ToString(), value);
            }

            return preferencesCollection;
        }

        /// <summary>
        /// Gets Appium App Package from configuration.
        /// </summary>
        public static string AppiumAppPackage
        {
            get
            {
                string setting = null;
                setting = Builder["appSettings:AppiumAppPackage"];
                Logger.Trace(CultureInfo.CurrentCulture, "Gets the AppiumAppPackage from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets Appium App Activity from configuration.
        /// </summary>
        public static string AppiumAppActivity
        {
            get
            {
                string setting = null;
                setting = Builder["appSettings:AppiumAppActivity"];
                Logger.Trace(CultureInfo.CurrentCulture, "Gets the AppiumAppActivity from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the Appium platform name (e.g., Android, iOS).
        /// </summary>
        public static string AppiumPlatformName
        {
            get
            {
                string setting = null;
                setting = Builder["appSettings:AppiumPlatformName"];
                Logger.Trace(CultureInfo.CurrentCulture, "Gets the AppiumPlatformName from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the Appium AutomationName.
        /// </summary>
        public static string AppiumAutomationName
        {
            get
            {
                string setting = null;
                setting = Builder["appSettings:AppiumAutomationName"];
                Logger.Trace(CultureInfo.CurrentCulture, "Gets the AppiumAutomationName from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the Appium device name.
        /// </summary>
        public static string AppiumDeviceName
        {
            get
            {
                string setting = null;
                setting = Builder["appSettings:AppiumDeviceName"];
                Logger.Trace(CultureInfo.CurrentCulture, "Gets the AppiumDeviceName from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the Appium app path.
        /// </summary>
        public static string AppiumAppPath
        {
            get
            {
                string setting = null;
                setting = Builder["appSettings:AppiumAppPath"];
                Logger.Trace(CultureInfo.CurrentCulture, "Gets the AppiumAppPath from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the Appium server URL.
        /// </summary>
        public static string AppiumServerUrl
        {
            get
            {
                string setting = null;
                setting = Builder["appSettings:AppiumServerUrl"];
                Logger.Trace(CultureInfo.CurrentCulture, "Gets the AppiumServerUrl from settings file '{0}'", setting);
                return setting;
            }
        }

    }
}