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
    using Objectivity.Test.Automation.Common.Driver;
    using Objectivity.Test.Automation.Common.Helpers;
    using Objectivity.Test.Automation.Common.Logger;
    using Objectivity.Test.Automation.Common.Types;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;

    /// <summary>
    /// Contains handle to driver and methods for web browser
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Driver is disposed on test end")]
    public class DriverContext
    {
        private static readonly NLog.Logger Logger = LogManager.GetLogger("DRIVER");
        private readonly Collection<ErrorDetail> verifyMessages = new Collection<ErrorDetail>();
        private readonly FirefoxDriverContext ffContext = new FirefoxDriverContext();
        private readonly FirefoxPortableDriverContext ffpContext = new FirefoxPortableDriverContext();
        private readonly ChromeDriverContext chromeContext = new ChromeDriverContext();
        private readonly SafariDriverContext safariContext = new SafariDriverContext();
        private readonly IEDriverContext ieDriverContext = new IEDriverContext();
        private readonly EdgeDriverContext edgeContext = new EdgeDriverContext();
        private readonly RemoteDriverContext remoteDriverContext = new RemoteDriverContext();

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
        public BrowserType CrossBrowserEnvironment { get; set; }

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
        /// Dictionary that contains references to all driver types.
        /// </summary>
        /// <returns>Dictionary with Selenium WebDriver reference.</returns>
        public Dictionary<BrowserType, IWebDriver> CreateDictionary()
        {
            var dictionary = new Dictionary<BrowserType, IWebDriver>
                {
                    { BrowserType.Firefox, this.ffContext.GetDriver },
                    { BrowserType.FirefoxPortable, this.ffpContext.GetDriver },
                    { BrowserType.Chrome, this.chromeContext.GetDriver },
                    { BrowserType.IE, this.ieDriverContext.GetDriver },
                    { BrowserType.InternetExplorer, this.ieDriverContext.GetDriver },
                    { BrowserType.Edge, this.edgeContext.GetDriver },
                    { BrowserType.Safari, this.safariContext.GetDriver },
                    { BrowserType.RemoteWebDriver, this.remoteDriverContext.GetDriver }
                };

            return dictionary;
        }

        /// <summary>
        /// Handle choosing correct Selenium WebDriver.
        /// </summary>
        /// <param name="browserType">Browser type name.</param>
        /// <returns>Selenium WebDriver instance.</returns>
        public IWebDriver ReturnCurrentDriver(BrowserType browserType)
        {
            try
            {
                return this.CreateDictionary()[browserType];
            }
            catch
            {
                throw new NotSupportedException(
                        string.Format(CultureInfo.CurrentCulture, "Driver {0} is not supported", BaseConfiguration.TestBrowser));
            }
        }

        /// <summary>
        /// Starts the specified Driver.
        /// </summary>
        /// <exception cref="NotSupportedException">When driver not supported</exception>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Driver disposed later in stop method")]
        public void Start()
        {
            this.driver = this.ReturnCurrentDriver(BaseConfiguration.TestBrowser);

            this.driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(BaseConfiguration.LongTimeout);
            this.driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(BaseConfiguration.ShortTimeout);
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(BaseConfiguration.ImplicitlyWaitMilliseconds);

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
    }
}
