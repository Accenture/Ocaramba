// <copyright file="DriverContext.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.Common
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using NLog;
    using Objectivity.Test.Automation.Common.Helpers;
    using Objectivity.Test.Automation.Common.Logger;
    using Objectivity.Test.Automation.Common.Types;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Edge;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Safari;

    /// <summary>
    /// Contains handle to driver and methods for web browser
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Driver is disposed on test end")]
    public class DriverContext
    {
        private static readonly NLog.Logger Logger = LogManager.GetLogger("DRIVER");
        private readonly Collection<ErrorDetail> verifyMessages = new Collection<ErrorDetail>();

        /// <summary>
        /// Gets or sets the handle to current driver.
        /// </summary>
        /// <value>
        /// The handle to driver.
        /// </value>
        private IWebDriver driver;

        private TestLogger logTest;

        /// <summary>
        /// Occurs when [driver options set].
        /// </summary>
        public event EventHandler<DriverOptionsSetEventArgs> DriverOptionsSet;

        /// <summary>
        /// Gets instance of Performance PerformanceMeasures class
        /// </summary>
        public PerformanceHelper PerformanceMeasures { get; } = new PerformanceHelper();

        /// <summary>
        /// Gets or sets the test title.
        /// </summary>
        /// <value>
        /// The test title.
        /// </value>
        public string TestTitle { get; set; }

        /// <summary>
        /// Gets or sets the CrossBrowserProfile from App.config
        /// </summary>
        public string CrossBrowserProfile { get; set; }

        /// <summary>
        /// Gets or sets the Environment Browsers from App.config
        /// </summary>
        public string CrossBrowserEnvironment { get; set; }

        /// <summary>
        /// Gets Sets Folder name for ScreenShot
        /// </summary>
        public string ScreenShotFolder
        {
            get
            {
                return FilesHelper.GetFolder(BaseConfiguration.ScreenShotFolder, this.CurrentDirectory);
            }
        }

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
        /// Gets Sets Folder name for PageSource
        /// </summary>
        public string PageSourceFolder
        {
            get
            {
                return FilesHelper.GetFolder(BaseConfiguration.PageSourceFolder, this.CurrentDirectory);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [test failed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [test failed]; otherwise, <c>false</c>.
        /// </value>
        public bool IsTestFailed { get; set; }

        /// <summary>
        /// Gets or sets test logger
        /// </summary>
        public TestLogger LogTest
        {
            get
            {
                return this.logTest ?? (this.logTest = new TestLogger());
            }

            set
            {
                this.logTest = value;
            }
        }

        /// <summary>
        /// Gets driver Handle
        /// </summary>
        public IWebDriver Driver
        {
            get
            {
                return this.driver;
            }
        }

        /// <summary>
        /// Gets all verify messages
        /// </summary>
        public Collection<ErrorDetail> VerifyMessages
        {
            get
            {
                return this.verifyMessages;
            }
        }

        /// <summary>
        /// Gets or sets directory where assembly files are located
        /// </summary>
        public string CurrentDirectory { get; set; }

        private FirefoxOptions FirefoxOptions
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
                        Logger.Info(CultureInfo.CurrentCulture, "problem with loading firefox profile {0}", e.Message);
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
                        Logger.Trace(CultureInfo.CurrentCulture, "Installing extension {0}", firefoxExtensions.GetKey(i));
                        try
                        {
                            options.Profile.AddExtension(firefoxExtensions.GetKey(i));
                        }
                        catch (FileNotFoundException)
                        {
                            Logger.Trace(CultureInfo.CurrentCulture, "Installing extension {0}", this.CurrentDirectory + FilesHelper.Separator + firefoxExtensions.GetKey(i));
                            options.Profile.AddExtension(this.CurrentDirectory + FilesHelper.Separator + firefoxExtensions.GetKey(i));
                        }
                    }
                }

                options = this.AddFirefoxArguments(options);

                // custom preferences
                // if there are any settings
                if (firefoxPreferences == null)
                {
                    return options;
                }

                // loop through all of them
                for (var i = 0; i < firefoxPreferences.Count; i++)
                {
                    Logger.Trace(CultureInfo.CurrentCulture, "Set custom preference '{0},{1}'", firefoxPreferences.GetKey(i), firefoxPreferences[i]);

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
                        Logger.Trace(CultureInfo.CurrentCulture, "Installing extension {0}", chromeExtensions.GetKey(i));
                        try
                        {
                            options.AddExtension(chromeExtensions.GetKey(i));
                        }
                        catch (FileNotFoundException)
                        {
                            Logger.Trace(CultureInfo.CurrentCulture, "Installing extension {0}", this.CurrentDirectory + FilesHelper.Separator + chromeExtensions.GetKey(i));
                            options.AddExtension(this.CurrentDirectory + FilesHelper.Separator + chromeExtensions.GetKey(i));
                        }
                    }
                }

                if (BaseConfiguration.ChromePath != null)
                {
                    Logger.Trace(CultureInfo.CurrentCulture, "Setting Chrome Path {0}", BaseConfiguration.ChromePath);
                    options.BinaryLocation = BaseConfiguration.ChromePath;
                }

                // if there are any arguments
                if (chromeArguments != null)
                {
                    // loop through all of them
                    for (var i = 0; i < chromeArguments.Count; i++)
                    {
                        Logger.Trace(CultureInfo.CurrentCulture, "Setting Chrome Arguments {0}", chromeArguments.GetKey(i));
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
                    Logger.Trace(CultureInfo.CurrentCulture, "Set custom preference '{0},{1}'", chromePreferences.GetKey(i), chromePreferences[i]);

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
                    Logger.Trace(CultureInfo.CurrentCulture, "Set custom preference '{0},{1}'", internetExplorerPreferences.GetKey(i), internetExplorerPreferences[i]);

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

        private EdgeOptions EdgeOptions
        {
            get
            {
                var options = new EdgeOptions();

                // set browser proxy for Edge
                if (!string.IsNullOrEmpty(BaseConfiguration.Proxy))
                {
                    options.Proxy = this.CurrentProxy();
                }

                return options;
            }
        }

        private SafariOptions SafariOptions
        {
            get
            {
                var options = new SafariOptions();
                options.AddAdditionalCapability("cleanSession", true);

                return options;
            }
        }

        /// <summary>
        /// Takes the screenshot.
        /// </summary>
        /// <returns>An image of the page currently loaded in the browser.</returns>
        public Screenshot TakeScreenshot()
        {
            try
            {
                var screenshotDriver = (ITakesScreenshot)this.driver;
                var screenshot = screenshotDriver.GetScreenshot();
                return screenshot;
            }
            catch (NullReferenceException)
            {
                Logger.Error("Test failed but was unable to get webdriver screenshot.");
            }
            catch (UnhandledAlertException)
            {
                Logger.Error("Test failed but was unable to get webdriver screenshot.");
            }

            return null;
        }

        /// <summary>
        /// Starts the specified Driver.
        /// </summary>
        /// <exception cref="NotSupportedException">When driver not supported</exception>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Driver disposed later in stop method")]
        public void Start()
        {
            switch (BaseConfiguration.TestBrowser)
            {
                case BrowserType.Firefox:
                    this.driver = new FirefoxDriver(this.SetDriverOptions(this.FirefoxOptions));
                    break;
                case BrowserType.FirefoxPortable:
                    this.FirefoxOptions.BrowserExecutableLocation = BaseConfiguration.FirefoxPath;
                    this.driver = new FirefoxDriver(this.SetDriverOptions(this.FirefoxOptions));
                    break;
                case BrowserType.InternetExplorer:
                case BrowserType.IE:
                    this.driver = new InternetExplorerDriver(this.SetDriverOptions(this.InternetExplorerOptions));
                    break;
                case BrowserType.Chrome:
                    this.driver = new ChromeDriver(this.SetDriverOptions(this.ChromeOptions));
                    break;
                case BrowserType.Safari:
                    this.driver = new SafariDriver(this.SetDriverOptions(this.SafariOptions));
                    this.CheckIfProxySetForSafari();
                    break;
                case BrowserType.RemoteWebDriver:
                    var driverCapabilitiesConf = ConfigurationManager.GetSection("DriverCapabilities") as NameValueCollection;
                    NameValueCollection settings = ConfigurationManager.GetSection("environments/" + this.CrossBrowserEnvironment) as NameValueCollection;
                    var browserType = this.GetBrowserTypeForRemoteDriver(settings);

                    switch (browserType)
                    {
                        case BrowserType.Firefox:
                            FirefoxOptions firefoxOptions = new FirefoxOptions();
                            this.SetRemoteDriverBrowserOptions(driverCapabilitiesConf, settings, firefoxOptions);
                            this.driver = new RemoteWebDriver(BaseConfiguration.RemoteWebDriverHub, this.SetDriverOptions(firefoxOptions).ToCapabilities());
                            break;
                        case BrowserType.Android:
                        case BrowserType.Chrome:
                            ChromeOptions chromeOptions = new ChromeOptions();
                            this.SetRemoteDriverBrowserOptions(driverCapabilitiesConf, settings, chromeOptions);
                            this.driver = new RemoteWebDriver(BaseConfiguration.RemoteWebDriverHub, this.SetDriverOptions(chromeOptions).ToCapabilities());
                            break;
                        case BrowserType.Iphone:
                        case BrowserType.Safari:
                            SafariOptions safariOptions = new SafariOptions();
                            this.SetRemoteDriverOptions(driverCapabilitiesConf, settings, safariOptions);
                            this.driver = new RemoteWebDriver(BaseConfiguration.RemoteWebDriverHub, this.SetDriverOptions(safariOptions).ToCapabilities());
                            break;
                        case BrowserType.Edge:
                            EdgeOptions egEdgeOptions = new EdgeOptions();
                            this.SetRemoteDriverOptions(driverCapabilitiesConf, settings, egEdgeOptions);
                            this.driver = new RemoteWebDriver(BaseConfiguration.RemoteWebDriverHub, this.SetDriverOptions(egEdgeOptions).ToCapabilities());
                            break;
                        case BrowserType.IE:
                        case BrowserType.InternetExplorer:
                            InternetExplorerOptions internetExplorerOptions = new InternetExplorerOptions();
                            this.SetRemoteDriverBrowserOptions(driverCapabilitiesConf, settings, internetExplorerOptions);
                            this.driver = new RemoteWebDriver(BaseConfiguration.RemoteWebDriverHub, this.SetDriverOptions(internetExplorerOptions).ToCapabilities());
                            break;
                        default:
                            throw new NotSupportedException(
                                string.Format(CultureInfo.CurrentCulture, "Driver {0} is not supported", this.CrossBrowserEnvironment));
                    }

                    break;
                case BrowserType.Edge:
                    this.driver = new EdgeDriver(this.SetDriverOptions(this.EdgeOptions));
                    break;
                default:
                    throw new NotSupportedException(
                        string.Format(CultureInfo.CurrentCulture, "Driver {0} is not supported", BaseConfiguration.TestBrowser));
            }

            if (BaseConfiguration.EnableEventFiringWebDriver)
            {
                this.driver = new MyEventFiringWebDriver(this.driver);
            }
        }

        /// <summary>
        /// Maximizes the current window if it is not already maximized.
        /// </summary>
        public void WindowMaximize()
        {
            this.driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Deletes all cookies from the page.
        /// </summary>
        public void DeleteAllCookies()
        {
            this.driver.Manage().Cookies.DeleteAllCookies();
        }

        /// <summary>
        /// Stop browser instance.
        /// </summary>
        public void Stop()
        {
            if (this.driver != null)
            {
                this.driver.Quit();
            }
        }

        /// <summary>
        /// Saves the screenshot.
        /// </summary>
        /// <param name="errorDetail">The error detail.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="title">The title.</param>
        /// <returns>Path to the screenshot</returns>
        public string SaveScreenshot(ErrorDetail errorDetail, string folder, string title)
        {
            var fileName = string.Format(CultureInfo.CurrentCulture, "{0}_{1}_{2}.png", title, errorDetail.DateTime.ToString("yyyy-MM-dd HH-mm-ss-fff", CultureInfo.CurrentCulture), "browser");
            var correctFileName = Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(CultureInfo.CurrentCulture), string.Empty));
            correctFileName = Regex.Replace(correctFileName, "[^0-9a-zA-Z._]+", "_");
            correctFileName = NameHelper.ShortenFileName(folder, correctFileName, "_", 255);

            var filePath = Path.Combine(folder, correctFileName);

            try
            {
                errorDetail.Screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
                FilesHelper.WaitForFileOfGivenName(BaseConfiguration.ShortTimeout, correctFileName, folder);
                Logger.Error(CultureInfo.CurrentCulture, "Test failed: screenshot saved to {0}.", filePath);
                Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "##teamcity[publishArtifacts '{0}']", filePath));
                return filePath;
            }
            catch (NullReferenceException)
            {
                Logger.Error("Test failed but was unable to get webdriver screenshot.");
            }

            return null;
        }

        /// <summary>
        /// Saves the page source.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The saved source file</returns>
        public string SavePageSource(string fileName)
        {
            if (BaseConfiguration.GetPageSourceEnabled)
            {
                var fileNameShort = Regex.Replace(fileName, "[^0-9a-zA-Z._]+", "_");
                fileNameShort = NameHelper.ShortenFileName(this.PageSourceFolder, fileNameShort, "_", 255);
                var fileNameWithExtension = string.Format(CultureInfo.CurrentCulture, "{0}{1}", fileNameShort, ".html");
                var path = Path.Combine(this.PageSourceFolder, fileNameWithExtension);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                var pageSource = this.driver.PageSource;
                pageSource = pageSource.Replace("<head>", string.Format(CultureInfo.CurrentCulture, "<head><base href=\"http://{0}\" target=\"_blank\">", BaseConfiguration.Host));
                File.WriteAllText(path, pageSource);
                FilesHelper.WaitForFileOfGivenName(BaseConfiguration.LongTimeout, fileNameWithExtension, this.PageSourceFolder);
                Logger.Error(CultureInfo.CurrentCulture, "Test failed: page Source saved to {0}.", path);
                Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "##teamcity[publishArtifacts '{0}']", path));
                return path;
            }

            return null;
        }

        /// <summary>
        /// Takes and saves screen shot
        /// </summary>
        /// <returns>Array of filepaths</returns>
        public string[] TakeAndSaveScreenshot()
        {
            List<string> filePaths = new List<string>();
            if (BaseConfiguration.FullDesktopScreenShotEnabled)
            {
                filePaths.Add(TakeScreenShot.Save(TakeScreenShot.DoIt(), ImageFormat.Png, this.ScreenShotFolder, this.TestTitle));
            }

            if (BaseConfiguration.SeleniumScreenShotEnabled)
            {
                filePaths.Add(this.SaveScreenshot(new ErrorDetail(this.TakeScreenshot(), DateTime.Now, null), this.ScreenShotFolder, this.TestTitle));
            }

            return filePaths.ToArray();
        }

        /// <summary>
        /// Logs JavaScript errors
        /// </summary>
        /// <returns>True if JavaScript errors found</returns>
        public bool LogJavaScriptErrors()
        {
            IEnumerable<LogEntry> jsErrors = null;
            bool javScriptErrors = false;

            // Check JavaScript browser logs for errors.
            if (BaseConfiguration.JavaScriptErrorLogging)
            {
                Logger.Debug(CultureInfo.CurrentCulture, "Checking JavaScript error(s) in browser");
                try
                {
                    jsErrors =
                        this.driver.Manage()
                            .Logs.GetLog(LogType.Browser)
                            .Where(x => BaseConfiguration.JavaScriptErrorTypes.Any(e => x.Message.Contains(e)));
                }
                catch (NullReferenceException)
                {
                    Logger.Error(CultureInfo.CurrentCulture, "NullReferenceException while trying to read JavaScript errors from browser.");
                    return false;
                }

                if (jsErrors.Any())
                {
                    // Show JavaScript errors if there are any
                    Logger.Error(CultureInfo.CurrentCulture, "JavaScript error(s): {0}", Environment.NewLine + jsErrors.Aggregate(string.Empty, (s, entry) => s + entry.Message + Environment.NewLine));
                    javScriptErrors = true;
                }
            }

            return javScriptErrors;
        }

        /// <summary>
        /// Sets the driver options.
        /// </summary>
        /// <typeparam name="T">The type of DriverOptions for the specific Browser</typeparam>
        /// <param name="options">The options.</param>
        /// <returns>
        /// The Driver Options
        /// </returns>
        private T SetDriverOptions<T>(T options)
            where T : DriverOptions
        {
            this.DriverOptionsSet?.Invoke(this, new DriverOptionsSetEventArgs(options));
            return options;
        }

        private Proxy CurrentProxy()
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

        private void CheckIfProxySetForSafari()
        {
            // set browser proxy for Safari
            if (!string.IsNullOrEmpty(BaseConfiguration.Proxy))
            {
                throw new NotSupportedException("Use command line to setup proxy");
            }
        }

        private FirefoxOptions AddFirefoxArguments(FirefoxOptions option)
        {
            var firefoxArguments = ConfigurationManager.GetSection("FirefoxArguments") as NameValueCollection;

            // if there are any arguments
            if (firefoxArguments != null)
            {
                // loop through all of them
                for (var i = 0; i < firefoxArguments.Count; i++)
                {
                    Logger.Trace(CultureInfo.CurrentCulture, "Setting FireFox Arguments {0}", firefoxArguments.GetKey(i));
                    option.AddArgument(firefoxArguments.GetKey(i));
                }
            }

            return option;
        }

        private T SetRemoteDriverOptions<T>(NameValueCollection driverCapabilitiesConf, NameValueCollection settings, T options)
            where T : DriverOptions
        {
            // if there are any capability
            if (driverCapabilitiesConf != null)
            {
                // loop through all of them
                for (var i = 0; i < driverCapabilitiesConf.Count; i++)
                {
                    string value = driverCapabilitiesConf.GetValues(i)[0];
                    Logger.Trace(CultureInfo.CurrentCulture, "Adding driver capability {0}", driverCapabilitiesConf.GetKey(i));
                    options.AddAdditionalCapability(driverCapabilitiesConf.GetKey(i), value);
                }
            }

            // if there are any capability
            if (settings != null)
            {
                foreach (string key in settings.AllKeys)
                {
                    Logger.Trace(CultureInfo.CurrentCulture, "Adding driver capability {0} from {1}", key, this.CrossBrowserEnvironment);

                    options.AddAdditionalCapability(key, settings[key]);
                }
            }

            return options;
        }

        private BrowserType GetBrowserTypeForRemoteDriver(NameValueCollection settings)
        {
            if (BaseConfiguration.TestBrowserCapabilities != BrowserType.CloudProvider)
            {
                return BaseConfiguration.TestBrowserCapabilities;
            }

            BrowserType browserType = BrowserType.None;
            bool supportedBrowser = false;
            if (settings != null)
            {
                string browser = settings.GetValues("browser")?[0];
                supportedBrowser = Enum.TryParse(browser, true, out browserType);
                Logger.Info(CultureInfo.CurrentCulture, "supportedBrowser {0} : {1}", supportedBrowser, browserType);
            }

            if (!supportedBrowser)
            {
                if (this.CrossBrowserEnvironment.ToLower(CultureInfo.CurrentCulture).Contains(BrowserType.Android.ToString().ToLower(CultureInfo.CurrentCulture)))
                {
                    browserType = BrowserType.Chrome;
                }
                else if (this.CrossBrowserEnvironment.ToLower(CultureInfo.CurrentCulture).Contains(BrowserType.Iphone.ToString().ToLower(CultureInfo.CurrentCulture)))
                {
                    browserType = BrowserType.Safari;
                }
            }

            return browserType;
        }

        private void SetRemoteDriverBrowserOptions(NameValueCollection driverCapabilitiesConf, NameValueCollection settings, dynamic browserOptions)
        {
            // if there are any capability
            if (driverCapabilitiesConf != null)
            {
                // loop through all of them
                for (var i = 0; i < driverCapabilitiesConf.Count; i++)
                {
                    string value = driverCapabilitiesConf.GetValues(i)[0];
                    Logger.Trace(CultureInfo.CurrentCulture, "Adding driver capability {0}", driverCapabilitiesConf.GetKey(i));
                    browserOptions.AddAdditionalCapability(driverCapabilitiesConf.GetKey(i), value, true);
                }
            }

            // if there are any capability
            if (settings != null)
            {
                foreach (string key in settings.AllKeys)
                {
                    Logger.Trace(CultureInfo.CurrentCulture, "Adding driver capability {0} from {1}", key, this.CrossBrowserEnvironment);
                    browserOptions.AddAdditionalCapability(key, settings[key], true);
                }
            }
        }
    }
}
