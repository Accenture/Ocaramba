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

using System.Text;
using Objectivity.Test.Automation.Common.Logger;

namespace Objectivity.Test.Automation.Common
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using Objectivity.Test.Automation.Common.Types;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;

    /// <summary>
    /// Contains handle to driver and methods for web browser
    /// </summary>
    public class BrowserManager
    {
        /// <summary>
        /// Gets or sets the handle to current driver.
        /// </summary>
        /// <value>
        /// The handle to driver.
        /// </value>
        private static readonly Dictionary<int, IWebDriver> CurrentDrivers = new Dictionary<int, IWebDriver>();

        internal static IWebDriver Handle
        {
            get
            {
                return CurrentDrivers[Thread.CurrentThread.ManagedThreadId];
            }
        }

        private FirefoxProfile FirefoxProfile
        {
            get
            {
                var profile = new FirefoxProfile();
                profile.SetPreference("toolkit.startup.max_resumed_crashes", "999999");
                profile.SetPreference("network.automatic-ntlm-auth.trusted-uris", BaseConfiguration.Host ?? string.Empty);
                return profile;
            }
        }

        /// <summary>
        /// Starts the specified browser.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <exception cref="NotSupportedException">When driver not supported</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Driver disposed later in stop method")]
        public void Start(string browser)
        {
            IWebDriver driver;

            switch (browser)
            {
                case "Firefox":
                    driver = new FirefoxDriver(this.FirefoxProfile);
                    break;
                case "FirefoxPortable":
                    var profile = this.FirefoxProfile;
                    var firefoxBinary = new FirefoxBinary(BaseConfiguration.FirefoxPath);
                    driver = new FirefoxDriver(firefoxBinary, profile);
                    break;
                case "InternetExplorer":
                    var options = new InternetExplorerOptions
                    {
                        EnsureCleanSession = true,
                        IgnoreZoomLevel = true,                        
                    };
                    driver = new InternetExplorerDriver(BaseConfiguration.InternetExplorerPath, options);
                    break;
                case "Chrome":
                    driver = new ChromeDriver(@"Drivers\");
                    break;
                default:
                    throw new NotSupportedException(
                        string.Format(CultureInfo.CurrentCulture, "Driver {0} is not supported", browser));
            }

            driver.Manage().Window.Maximize();
            var driverEventListener = new MyEventFiringWebDriver(driver);
            CurrentDrivers.Add(Thread.CurrentThread.ManagedThreadId, driverEventListener);
        }

        /// <summary>
        /// Stop browser instance.
        /// </summary>
        public void Stop()
        {
            if (!CurrentDrivers.ContainsKey(Thread.CurrentThread.ManagedThreadId))
            {
                return;
            }

            CurrentDrivers[Thread.CurrentThread.ManagedThreadId].Dispose();
            CurrentDrivers.Remove(Thread.CurrentThread.ManagedThreadId);
        }

        /// <summary>
        /// Takes the screenshot.
        /// </summary>
        public Screenshot TakeScreenshot()
        {
            try
            {
                var screenshotDriver = (ITakesScreenshot)Handle;
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

            var pageSource = Handle.PageSource;
            pageSource = pageSource.Replace("<head>", string.Format(CultureInfo.CurrentCulture, "<head><base href=\"http://{0}\" target=\"_blank\">", BaseConfiguration.Host));
            File.WriteAllText(path, pageSource);
        }
    }
}