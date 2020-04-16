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

namespace Ocaramba
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
#if netcoreapp3_1
    using Microsoft.Extensions.Configuration;
#endif
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
        /// <summary>
        /// The logger.
        /// </summary>
#if net47 || net45
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
#endif
#if netcoreapp3_1
        public static readonly string Env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        /// <summary>
        /// Getting appsettings.json file.
        /// </summary>
        public static readonly IConfigurationRoot Builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{Env}.json", true, true)
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
                bool supportedBrowser = false;
                string setting = null;
#if netcoreapp3_1
                setting = Builder["appSettings:browser"];
#endif
#if net47 || net45
                setting = ConfigurationManager.AppSettings["browser"];

#endif
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["DriverCapabilities"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:DriverCapabilities"];
#endif

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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["PathToFirefoxProfile"];
#elif netcoreapp3_1
                setting = Builder["appSettings:PathToFirefoxProfile"];
#endif
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["protocol"];
#endif
#if  netcoreapp3_1
                setting = Builder["appSettings:protocol"];
#endif
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["host"];
#endif
#if  netcoreapp3_1
                setting = Builder["appSettings:host"];
#endif
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["url"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:url"];
#endif
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["proxy"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:proxy"];
#endif
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["httpProxy"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:httpProxy"];
#endif
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["ftpProxy"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:ftpProxy"];
#endif
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["sslProxy"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:sslProxy"];
#endif
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["socksproxy"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:socksproxy"];
#endif
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
                int setting = 0;
#if net47 || net45
                setting = int.Parse(ConfigurationManager.AppSettings["remoteTimeout"], CultureInfo.InvariantCulture);
#endif
#if netcoreapp3_1
                setting = int.Parse(Builder["appSettings:remoteTimeout"], CultureInfo.InvariantCulture);
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Gets the remote timeout from settings file '{0}'", setting);
                return new TimeSpan(0, 0, setting);
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["username"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:username"];
#endif
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["password"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:password"];
#endif
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
#if net47 || net45
                setting = Convert.ToDouble(ConfigurationManager.AppSettings["mediumTimeout"], CultureInfo.CurrentCulture);
#endif
#if netcoreapp3_1
                setting = Convert.ToDouble(Builder["appSettings:mediumTimeout"]);
#endif
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
#if net47 || net45
                setting = Convert.ToDouble(ConfigurationManager.AppSettings["longTimeout"], CultureInfo.CurrentCulture);
#endif
#if netcoreapp3_1
                setting = Convert.ToDouble(Builder["appSettings:longTimeout"]);
#endif
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
#if net47 || net45
                setting = Convert.ToDouble(ConfigurationManager.AppSettings["shortTimeout"], CultureInfo.CurrentCulture);
#endif
#if netcoreapp3_1
                setting = Convert.ToDouble(Builder["appSettings:shortTimeout"]);
#endif
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
#if net47 || net45
                setting = Convert.ToDouble(ConfigurationManager.AppSettings["ImplicitlyWaitMilliseconds"], CultureInfo.CurrentCulture);
#endif
#if netcoreapp3_1
                setting = Convert.ToDouble(Builder["appSettings:ImplicitlyWaitMilliseconds"]);
#endif
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["FirefoxBrowserExecutableLocation"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:FirefoxBrowserExecutableLocation"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Gets the path and file name of the Firefox browser executable from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return string.Empty;
                }

                return setting;
            }
        }

        /// <summary>
        /// Gets the path and file name of the Edge Chrominium browser executable. Default value "C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe".
        /// </summary>
        public static string EdgeChrominiumBrowserExecutableLocation
        {
            get
            {
                string setting = null;
#if net47 || net45
                setting = ConfigurationManager.AppSettings["EdgeChrominiumBrowserExecutableLocation"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:EdgeChrominiumBrowserExecutableLocation"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Gets the path and file name of the Edge Chrominium browser executable from settings file '{0}'", setting);
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["ChromeBrowserExecutableLocation"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:ChromeBrowserExecutableLocation"];
#endif
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

#if net47 || net45
                setting = ConfigurationManager.AppSettings["RemoteWebDriverHub"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:RemoteWebDriverHub"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "RemoteWebDriverHub from settings file '{0}'", setting);
                return new Uri(setting);
            }
        }

        /// <summary>
        /// Gets a value indicating whether enable full desktop screen shot. False by default.
        /// </summary>
        public static bool FullDesktopScreenShotEnabled
        {
            get
            {
#if net47 || net45
                Logger.Trace(
                    CultureInfo.CurrentCulture,
                    "Full Desktop Screen Shot Enabled value from App.config '{0}'",
                    ConfigurationManager.AppSettings["FullDesktopScreenShotEnabled"]);
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["FullDesktopScreenShotEnabled"]))
                {
                    return false;
                }

                if (ConfigurationManager.AppSettings["FullDesktopScreenShotEnabled"].ToLower(CultureInfo.CurrentCulture)
                    .Equals("true"))
                {
                    return true;
                }
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Full Desktop Screen Shot not supported in .NET Core 'false'");
                return false;
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["PathToChromeDriverLog"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:PathToChromeDriverLog"];
#endif
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["EnableVerboseLoggingChrome"];
#endif
#if  netcoreapp3_1
                setting = Builder["appSettings:EnableVerboseLoggingChrome"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Verbose logging for Chrome value from settings file '{0}'", setting);
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
        /// Gets specified path to the directory containing InternetExplorer Driver.
        /// </summary>
        public static string PathToInternetExplorerDriverDirectory
        {
            get
            {
                string setting = null;
#if net47 || net45
                setting = ConfigurationManager.AppSettings["PathToInternetExplorerDriverDirectory"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:PathToInternetExplorerDriverDirectory"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Gets the PathToInternetExplorerDriverDirectory from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return string.Empty;
                }

                return setting;
            }
        }

        /// <summary>
        /// Gets specified path to the directory containing Edge Chrominum Driver, name of driver msedgedriver.exe. Default value "C:\Temp\Drivers".
        /// </summary>
        public static string PathToEdgeChrominumDriverDirectory
        {
            get
            {
                string setting = null;
#if net47 || net45
                setting = ConfigurationManager.AppSettings["PathToEdgeChrominumDriverDirectory"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:PathToEdgeChrominumDriverDirectory"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Gets the PathToEdgeChrominumDriverDirectory from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return @"C:\Temp\Drivers";
                }

                return setting;
            }
        }

        /// <summary>
        /// Gets specified path to the directory containing Edge Driver.
        /// </summary>
        public static string PathToEdgeDriverDirectory
        {
            get
            {
                string setting = null;
#if net47 || net45
                setting = ConfigurationManager.AppSettings["PathToEdgeDriverDirectory"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:PathToEdgeDriverDirectory"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Gets the PathToEdgeDriverDirectory from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return string.Empty;
                }

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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["PathToChromeDriverDirectory"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:PathToChromeDriverDirectory"];
#endif

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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["PathToFirefoxDriverDirectory"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:PathToFirefoxDriverDirectory"];
#endif
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["JavaScriptErrorLogging"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:JavaScriptErrorLogging"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "JavaScript error logging value from settings file '{0}'", setting);
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["JavaScriptErrorTypes"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:JavaScriptErrorTypes"];
#endif
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
        /// Gets a value indicating whether enable legacy implementation for Firefox.
        /// </summary>
        public static bool FirefoxUseLegacyImplementation
        {
            get
            {
                string setting = null;
#if net47 || net45
                setting = ConfigurationManager.AppSettings["FirefoxUseLegacyImplementation"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:FirefoxUseLegacyImplementation"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Firefox Use Legacy Implementation Enabled value from settings file '{0}'", setting);
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["SeleniumScreenShotEnabled"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:SeleniumScreenShotEnabled"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Selenium Screen Shot Enabled value from settings file '{0}'", setting);
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["EnableEventFiringWebDriver"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:EnableEventFiringWebDriver"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Enable EventFiringWebDriver from settings file '{0}'", setting);
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["UseCurrentDirectory"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:UseCurrentDirectory"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Use Current Directory value from settings file '{0}'", setting);
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["GetPageSourceEnabled"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:GetPageSourceEnabled"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Get Page Source Enabled value from settings file '{0}'", setting);
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
        /// Gets the download folder key value.
        /// </summary>
        public static string DownloadFolder
        {
            get
            {
                string setting = null;
#if net47 || net45
                setting = ConfigurationManager.AppSettings["DownloadFolder"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:DownloadFolder"];
#endif
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["ScreenShotFolder"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:ScreenShotFolder"];
#endif
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["PageSourceFolder"];
#endif
#if netcoreapp3_1
                setting = Builder["appSettings:PageSourceFolder"];
#endif
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
#if net47 || net45
                setting = ConfigurationManager.AppSettings["SynchronizationWithAngularEnabled"];
#endif
#if  netcoreapp3_1
                setting = Builder["appSettings:SynchronizationWithAngularEnabled"];
#endif
                Logger.Trace(CultureInfo.CurrentCulture, "Angular synchronization Enabled value from settings file '{0}'", setting);
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
#if  netcoreapp3_1

        /// <summary>
        /// Converting settings from appsettings.json into the NameValueCollection, key - value pairs.
        /// </summary>
        /// <param name="preferences">Section name in appsettings.json file.</param>
        /// <returns>Settings.</returns>
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
                {
                    value = kvp.Value.ToString();
                }

                preferencesCollection.Add(kvp.Key.ToString(), value);
            }

            return preferencesCollection;
        }
#endif
    }
}