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
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    using Objectivity.Test.Automation.Common.Helpers;
    using Objectivity.Test.Automation.Common.Logger;
    using Objectivity.Test.Automation.Common.Types;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;

    /// <summary>
    /// Contains handle to driver and methods for web browser
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Driver is disposed on test end")]
    public class DriverContext
    {
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
        /// Supported browsers
        /// </summary>
        public enum BrowserType
        {
            /// <summary>
            /// Firefox browser
            /// </summary>
            Firefox,

            /// <summary>
            /// Firefox portable
            /// </summary>
            FirefoxPortable,

            /// <summary>
            /// InternetExplorer browser
            /// </summary>
            InternetExplorer,

            /// <summary>
            /// Chrome browser
            /// </summary>
            Chrome,

            /// <summary>
            /// Not supported browser
            /// </summary>
            None
        }

        /// <summary>
        /// Gets or sets the test title.
        /// </summary>
        /// <value>
        /// The test title.
        /// </value>
        public string TestTitle { get; set; }

        /// <summary>
        /// Gets Folder name
        /// </summary>
        public string TestFolder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [test failed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [test failed]; otherwise, <c>false</c>.
        /// </value>
        public bool IsTestFailed { get; set; }

        /// <summary>
        /// Test logger
        /// </summary>
        public TestLogger LogTest
        {
            get
            {
                return this.logTest ?? (this.logTest = new TestLogger(this.TestFolder, this.TestTitle));
            }

            set
            {
                this.logTest = value;
            }
        }

        /// <summary>
        /// Driver Handle
        /// </summary>
        public IWebDriver Driver
        {
            get
            {
                return this.driver;
            }
        }

        /// <summary>
        /// Held all verify messages
        /// </summary>
        public Collection<ErrorDetail> VerifyMessages
        {
            get
            {
                return this.verifyMessages;
            }
        }

        private FirefoxProfile FirefoxProfile
        {
            get
            {
                var profile = new FirefoxProfile();

                // set browser proxy for firefox
                if (!string.IsNullOrEmpty(BaseConfiguration.Proxy))
                {
                    profile.SetProxyPreferences(this.CurrentProxy());
                }

                profile.SetPreference("toolkit.startup.max_resumed_crashes", "999999");
                profile.SetPreference("network.automatic-ntlm-auth.trusted-uris", BaseConfiguration.Host ?? string.Empty);

                // preference for downloading files
                profile.SetPreference("browser.download.dir", FilesHelper.GetFolder(BaseConfiguration.DownloadFolder));
                profile.SetPreference("browser.download.folderList", 2);
                profile.SetPreference("browser.download.managershowWhenStarting", false);
                profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/vnd.ms-excel, application/x-msexcel, application/pdf, text/csv, text/html, application/octet-stream");
                
                // disable Firefox's built-in PDF viewer
                profile.SetPreference("pdfjs.disabled", true);

                // disable Adobe Acrobat PDF preview plugin
                profile.SetPreference("plugin.scan.Acrobat", "99.0");
                profile.SetPreference("plugin.scan.plid.all", false);

                return profile;
            }
        }

        private ChromeOptions ChromeProfile
        {
            get
            {
                ChromeOptions options = new ChromeOptions();
                options.AddUserProfilePreference("profile.default_content_settings.popups", 0);
                options.AddUserProfilePreference("download.default_directory", FilesHelper.GetFolder(BaseConfiguration.DownloadFolder));
                options.AddUserProfilePreference("download.prompt_for_download", false);

                // set browser proxy for chrome
                if (!string.IsNullOrEmpty(BaseConfiguration.Proxy))
                {
                    options.Proxy = this.CurrentProxy();
                }
                
                return options;
            }
        }

        private InternetExplorerOptions InternetExplorerProfile
        {
            get
            {
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

                return options;
            }
        }

        private Proxy CurrentProxy()
        {
            Proxy proxy = new Proxy();
            proxy.HttpProxy = BaseConfiguration.Proxy;
            proxy.FtpProxy = BaseConfiguration.Proxy;
            proxy.SslProxy = BaseConfiguration.Proxy;
            proxy.SocksProxy = BaseConfiguration.Proxy;
            return proxy;
        }

        /// <summary>
        /// Starts the specified Driver.
        /// </summary>
        /// <exception cref="NotSupportedException">When driver not supported</exception>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Driver disposed later in stop method")]
        public void Start()
        {
            IWebDriver chosenDriver;

            switch (BaseConfiguration.TestBrowser)
            {
                case BrowserType.Firefox:
                    chosenDriver = new FirefoxDriver(this.FirefoxProfile);
                    break;
                case BrowserType.FirefoxPortable:
                    var profile = this.FirefoxProfile;
                    var firefoxBinary = new FirefoxBinary(BaseConfiguration.FirefoxPath);
                    chosenDriver = new FirefoxDriver(firefoxBinary, profile);
                    break;
                case BrowserType.InternetExplorer:
                    chosenDriver = new InternetExplorerDriver(this.InternetExplorerProfile);
                    break;
                case BrowserType.Chrome:
                    chosenDriver = new ChromeDriver(this.ChromeProfile);
                    break;
                default:
                    throw new NotSupportedException(
                        string.Format(CultureInfo.CurrentCulture, "Driver {0} is not supported", BaseConfiguration.TestBrowser));
            }

            chosenDriver.Manage().Window.Maximize();
            this.driver = new MyEventFiringWebDriver(chosenDriver);
        }

        /// <summary>
        /// Stop browser instance.
        /// </summary>
        public void Stop()
        {
            this.driver.Quit();
        }

        /// <summary>
        /// Takes the screenshot.
        /// </summary>
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
                Console.Error.WriteLine("Test failed but was unable to get screenshot.");
            }

            return null;
        }

        /// <summary>
        /// Saves the screenshot.
        /// </summary>
        /// <param name="errorDetail">The error detail.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="title">The title.</param>
        public void SaveScreenshot(ErrorDetail errorDetail, string folder, string title)
        {
            var fileName = string.Format(CultureInfo.CurrentCulture, "{0}_{1}.png", title, errorDetail.DateTime.ToString("yyyy-MM-dd HH-mm-ss-fff", CultureInfo.CurrentCulture));
            var correctFileName = Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(CultureInfo.CurrentCulture), string.Empty));
            var filePath = Path.Combine(Environment.CurrentDirectory, folder, correctFileName);

            errorDetail.Screenshot.SaveAsFile(filePath, ImageFormat.Png);

            Console.Error.WriteLine("Test failed: screenshot saved to {0}.", filePath);
        }

        /// <summary>
        /// Saves the page source.
        /// </summary>
        /// <param name="testFolder">The test folder.</param>
        /// <param name="fileName">Name of the file.</param>
        public void SavePageSource(string testFolder, string fileName)
        {
            var path = Path.Combine(Environment.CurrentDirectory, testFolder, string.Format(CultureInfo.CurrentCulture, "{0}{1}", fileName, ".html"));
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            var pageSource = this.driver.PageSource;
            pageSource = pageSource.Replace("<head>", string.Format(CultureInfo.CurrentCulture, "<head><base href=\"http://{0}\" target=\"_blank\">", BaseConfiguration.Host));
            File.WriteAllText(path, pageSource);
        }

        /// <summary>
        /// Takes and saves screen shot
        /// </summary>
        public void TakeAndSaveScreenshot()
        {
            if (BaseConfiguration.FullDesktopScreenShotEnabled)
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains("TestFolder"))
                {
                    TakeScreenShot.Save(TakeScreenShot.DoIt(), ImageFormat.Png, this.LogTest.TestFolder, this.TestTitle);
                }
                else
                {
                    TakeScreenShot.Save(TakeScreenShot.DoIt(), ImageFormat.Png, BaseConfiguration.ScreenShotFolder, this.TestTitle);
                }
            }

            if (BaseConfiguration.SeleniumScreenShotEnabled)
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains("TestFolder"))
                {
                    this.SaveScreenshot(new ErrorDetail(this.TakeScreenshot(), DateTime.Now, null), this.LogTest.TestFolder, this.TestTitle);
                }
                else
                {
                    this.SaveScreenshot(new ErrorDetail(this.TakeScreenshot(), DateTime.Now, null), BaseConfiguration.ScreenShotFolder, this.TestTitle);
                }
            }
        }  
    }
}