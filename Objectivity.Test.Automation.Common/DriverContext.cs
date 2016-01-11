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
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    using NLog;

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
        /// Gets Sets Folder name for ScreenShot
        /// </summary>
        public string ScreenShotFolder
        {
            get
            {
                return this.GetFolder(BaseConfiguration.ScreenShotFolder);
            }
        }

        /// <summary>
        /// Gets Sets Folder name for Download
        /// </summary>
        public string DownloadFolder
        {
            get
            {
                return this.GetFolder(BaseConfiguration.DownloadFolder);
            }
        }

        /// <summary>
        /// Gets Sets Folder name for PageSource
        /// </summary>
        public string PageSourceFolder
        {
            get
            {
                return this.GetFolder(BaseConfiguration.PageSourceFolder);
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
        /// Test logger
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

                // predefined preferences
                // set browser proxy for firefox
                if (!string.IsNullOrEmpty(BaseConfiguration.Proxy))
                {
                    profile.SetProxyPreferences(this.CurrentProxy());
                }

                profile.SetPreference("toolkit.startup.max_resumed_crashes", "999999");
                profile.SetPreference("network.automatic-ntlm-auth.trusted-uris", BaseConfiguration.Host ?? string.Empty);

                // retrieving settings from config file
                var firefoxPreferences = ConfigurationManager.GetSection("FirefoxPreferences") as NameValueCollection;
                var firefoxExtensions = ConfigurationManager.GetSection("FirefoxExtensions") as NameValueCollection;

                // preference for downloading files
                profile.SetPreference("browser.download.dir", this.DownloadFolder);
                profile.SetPreference("browser.download.folderList", 2);
                profile.SetPreference("browser.download.managershowWhenStarting", false);
                profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/vnd.ms-excel, application/x-msexcel, application/pdf, text/csv, text/html, application/octet-stream");
                
                // disable Firefox's built-in PDF viewer
                profile.SetPreference("pdfjs.disabled", true);

                // disable Adobe Acrobat PDF preview plugin
                profile.SetPreference("plugin.scan.Acrobat", "99.0");
                profile.SetPreference("plugin.scan.plid.all", false);

                // custom preferences
                // if there are any settings
                if (firefoxPreferences != null)
                {
                    // loop through all of them
                    for (var i = 0; i < firefoxPreferences.Count; i++)
                    {
                        // and verify all of them
                        switch (firefoxPreferences[i])
                        {
                            // if current settings value is "true"
                            case "true":
                                profile.SetPreference(firefoxPreferences.GetKey(i), true);
                                break;

                            // if "false"
                            case "false":
                                profile.SetPreference(firefoxPreferences.GetKey(i), false);
                                break;

                            // otherwise
                            default:
                                int temp;

                                // an attempt to parse current settings value to an integer. Method TryParse returns True if the attempt is successful (the string is integer) or return False (if the string is just a string and cannot be cast to a number)
                                if (int.TryParse(firefoxPreferences.Get(i), out temp))
                                {
                                    profile.SetPreference(firefoxPreferences.GetKey(i), temp);
                                }
                                else profile.SetPreference(firefoxPreferences.GetKey(i), firefoxPreferences[i]);
                                break;
                        }
                    }
                }

                // if there are any extensions
                if (firefoxExtensions != null)
                {
                    // loop through all of them
                    for (var i = 0; i < firefoxExtensions.Count; i++)
                    {
                        profile.AddExtension(firefoxExtensions.GetKey(i));
                    }
                }

                return profile;
            }
        }

        private ChromeOptions ChromeProfile
        {
            get
            {
                ChromeOptions options = new ChromeOptions();
                options.AddUserProfilePreference("profile.default_content_settings.popups", 0);
                options.AddUserProfilePreference("download.default_directory", this.DownloadFolder);
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

        /// <summary>
        /// Directory where assembly files are located
        /// </summary>
        public string CurrentDirectory { get; set; }

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
            switch (BaseConfiguration.TestBrowser)
            {
                case BrowserType.Firefox:
                    this.driver = new FirefoxDriver(this.FirefoxProfile);
                    break;
                case BrowserType.FirefoxPortable:
                    var profile = this.FirefoxProfile;
                    var firefoxBinary = new FirefoxBinary(BaseConfiguration.FirefoxPath);
                    this.driver = new FirefoxDriver(firefoxBinary, profile);
                    break;
                case BrowserType.InternetExplorer:
                    this.driver = new InternetExplorerDriver(this.InternetExplorerProfile);
                    break;
                case BrowserType.Chrome:
                    this.driver = new ChromeDriver(this.ChromeProfile);
                    break;
                default:
                    throw new NotSupportedException(
                        string.Format(CultureInfo.CurrentCulture, "Driver {0} is not supported", BaseConfiguration.TestBrowser));
            }

            this.driver.Manage().Window.Maximize();
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
                Logger.Error("Test failed but was unable to get screenshot.");
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
            var filePath = Path.Combine(folder, correctFileName);

            errorDetail.Screenshot.SaveAsFile(filePath, ImageFormat.Png);

            Logger.Error(CultureInfo.CurrentCulture, "Test failed: screenshot saved to {0}.", filePath);
        }

        /// <summary>
        /// Saves the page source.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void SavePageSource(string fileName)
        {
            if (BaseConfiguration.GetPageSourceEnabled)
            {
                var path = Path.Combine(this.PageSourceFolder, string.Format(CultureInfo.CurrentCulture, "{0}{1}", fileName, ".html"));
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                var pageSource = this.driver.PageSource;
                pageSource = pageSource.Replace("<head>", string.Format(CultureInfo.CurrentCulture, "<head><base href=\"http://{0}\" target=\"_blank\">", BaseConfiguration.Host));
                File.WriteAllText(path, pageSource);
           }
        }

        /// <summary>
        /// Takes and saves screen shot
        /// </summary>
        public void TakeAndSaveScreenshot()
        {
            if (BaseConfiguration.FullDesktopScreenShotEnabled)
            {
                TakeScreenShot.Save(TakeScreenShot.DoIt(), ImageFormat.Png, this.ScreenShotFolder, this.TestTitle);
            }

            if (BaseConfiguration.SeleniumScreenShotEnabled)
            {
                this.SaveScreenshot(new ErrorDetail(this.TakeScreenshot(), DateTime.Now, null), this.ScreenShotFolder, this.TestTitle);
            }
        }

        /// <summary>
        /// Gets the folder from app.config as value of given key.
        /// </summary>
        /// <param name="appConfigValue">The application configuration value.</param>
        /// <returns></returns>
        private string GetFolder(string appConfigValue)
        {
            string folder;

            if (string.IsNullOrEmpty(appConfigValue))
            {
                folder = this.CurrentDirectory;
            }
            else
            {
                if (BaseConfiguration.UseCurrentDirectory)
                {
                    folder = this.CurrentDirectory + appConfigValue;
                }
                else
                {
                    folder = appConfigValue;
                }

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }

            Logger.Trace(CultureInfo.CurrentCulture, "Folder '{0}'", folder);
            return folder;
        }
    }
}