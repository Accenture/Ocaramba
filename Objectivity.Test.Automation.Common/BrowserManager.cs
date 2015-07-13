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
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Linq;

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
        internal static IWebDriver Handle { get; set; }

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
        /// <exception cref="System.NotSupportedException">When driver not supported</exception>
        public void Start(string browser)
        {
            switch (browser)
            {
                case "Firefox":
                    Handle = new FirefoxDriver(this.FirefoxProfile);
                    break;
                case "FirefoxPortable":

                    var profile = this.FirefoxProfile;
                    var firefoxBinary = new FirefoxBinary(BaseConfiguration.FirefoxPath);
                    Handle = new FirefoxDriver(firefoxBinary, profile);
                    break;
                case "InternetExplorer":
                    var options = new InternetExplorerOptions
                    {
                        EnsureCleanSession = true,
                    };
                    Handle = new InternetExplorerDriver(@"Drivers\", options);
                    break;
                case "Chrome":
                    Handle = new ChromeDriver(@"Drivers\");
                    break;
                default:
                    throw new NotSupportedException(
                        string.Format(CultureInfo.CurrentCulture, "Driver {0} is not supported", browser));
            }

            Handle.Manage().Window.Maximize();
        }

        /// <summary>
        /// Stop browser instance.
        /// </summary>
        public void Stop()
        {
            Handle.Quit();
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
        /// Takes the screenshot.
        /// </summary>
        /// <param name="errorDetail">The error detail.</param>
        /// <param name="title">The title.</param>
        public void SaveScreenshot(ErrorDetail errorDetail, string title)
        {
            var fileName = string.Format(CultureInfo.CurrentCulture, "{0}_{1}.png", title, errorDetail.DateTime.ToString("yyyy-MM-dd HH-mm-ss-fff", CultureInfo.CurrentCulture));
            var correctFileName = Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(CultureInfo.CurrentCulture), string.Empty));
            var filePath = Path.Combine(Environment.CurrentDirectory, correctFileName);

            errorDetail.Screenshot.SaveAsFile(filePath, ImageFormat.Png);
            
            Console.Error.WriteLine("Test failed: screenshot saved to {0}.", filePath);
        }
    }
}