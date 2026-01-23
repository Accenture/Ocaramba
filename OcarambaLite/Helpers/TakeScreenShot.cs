// <copyright file="TakeScreenShot.cs" company="Accenture">
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

namespace Ocaramba.Helpers
{
    using System.Globalization;
    using System.IO;
    using NLog;
    using OpenQA.Selenium;

    /// <summary>
    /// Custom screenshot solution.
    /// </summary>
    public static class TakeScreenShot
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Takes screen shot of specific element .
        /// </summary>
        /// <param name="element">Element to take screenshot.</param>
        /// <param name="folder">Folder to save screenshot.</param>
        /// <param name="screenshotName">Name of screenshot.</param>
        /// <returns>Full path to taken screenshot.</returns>
        /// <example>How to use it: <code>
        /// var iFrame = this.Driver.GetElement(this.iframe);
        /// this.Driver.SwitchTo().Frame(0);
        /// var el = this.Driver.GetElement(this.elementInIFrame);
        /// var fullPath = TakeScreenShot.TakeScreenShotOfElement(el, Directory.GetCurrentDirectory() + BaseConfiguration.ScreenShotFolder, "MenuOutSideTheIFrame");
        /// </code></example>
        public static string TakeScreenShotOfElement(IWebElement element, string folder, string screenshotName)
        {
            Logger.Debug("Taking screenshot of element");

            var screenshot = ((ITakesScreenshot)element).GetScreenshot();
            var filePath = Path.Combine(folder, screenshotName);
            Logger.Debug(CultureInfo.CurrentCulture, "Taking full screenshot {0}", filePath);
            screenshot.SaveAsFile(filePath);
            return filePath;
        }
    }
}