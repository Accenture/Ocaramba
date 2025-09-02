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
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using NLog;
    using Ocaramba.Extensions;
    using Ocaramba.Helpers;
    using OpenQA.Selenium;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Png;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;

    /// <summary>
    /// Custom screenshot solution.
    /// </summary>
    public static class TakeScreenShot
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Takes screen shot of specific element.
        /// </summary>
        /// <param name="element">Element to take screenshot.</param>
        /// <param name="folder">Folder to save screenshot.</param>
        /// <param name="screenshotName">Name of screenshot.</param>
        /// <returns>Full path to taken screenshot.</returns>
        /// <example>How to use it: <code>
        /// var el = this.Driver.GetElement(this.menu);
        /// var fullPath = TakeScreenShot.TakeScreenShotOfElement(el, Directory.GetCurrentDirectory() + BaseConfiguration.ScreenShotFolder, "MenuOutSideTheIFrame");
        /// </code></example>
        public static string TakeScreenShotOfElement(IWebElement element, string folder, string screenshotName)
        {
            Logger.Debug("Taking screenhot of element not within iframe");
            return TakeScreenShotOfElement(0, 0, element, folder, screenshotName);
        }

        /// <summary>
        /// Takes screen shot of specific element within iframe.
        /// </summary>
        /// <param name="iframeLocationX">X coordinate of iframe.</param>
        /// <param name="iframeLocationY">Y coordinate of iframe.</param>
        /// <param name="element">Element to take screenshot.</param>
        /// <param name="folder">Folder to save screenshot.</param>
        /// <param name="screenshotName">Name of screenshot.</param>
        /// <returns>Full path to taken screenshot.</returns>
        /// <example>How to use it: <code>
        /// var iFrame = this.Driver.GetElement(this.iframe);
        /// int x = iFrame.Location.X;
        /// int y = iFrame.Location.Y;
        /// this.Driver.SwitchTo().Frame(0);
        /// var el = this.Driver.GetElement(this.elelemtInIFrame);
        /// var fullPath = TakeScreenShot.TakeScreenShotOfElement(x, y, el, Directory.GetCurrentDirectory() + BaseConfiguration.ScreenShotFolder, "MenuOutSideTheIFrame");
        /// </code></example>
        public static string TakeScreenShotOfElement(int iframeLocationX, int iframeLocationY, IWebElement element, string folder, string screenshotName)
        {
            Logger.Debug("Taking screenhot of iframe LocationX:{0} LocationY:{1}", iframeLocationX, iframeLocationY);
            var locationX = iframeLocationX;
            var locationY = iframeLocationY;

            var driver = element.ToDriver();

            var screenshotDriver = (ITakesScreenshot)driver;
            var screenshot = screenshotDriver.GetScreenshot();
            var filePath = Path.Combine(folder, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff", CultureInfo.CurrentCulture) + "temporary_fullscreen.png");
            Logger.Debug(CultureInfo.CurrentCulture, "Taking full screenshot {0}", filePath);
            screenshot.SaveAsFile(filePath);
            var js = (IJavaScriptExecutor)driver;
            var rect = (Dictionary<string, object>)js.ExecuteScript(
                "var r = arguments[0].getBoundingClientRect(); return {x: r.left + window.scrollX, y: r.top + window.scrollY};", element);

            if (BaseConfiguration.TestBrowser == BrowserType.Chrome)
            {
                locationX = Convert.ToInt32(rect["x"], CultureInfo.InvariantCulture) + locationX;
                locationY = Convert.ToInt32(rect["y"], CultureInfo.InvariantCulture) + locationY;
            }
            else
            {
                locationX = Convert.ToInt32(rect["x"], CultureInfo.InvariantCulture);
                locationY = Convert.ToInt32(rect["y"], CultureInfo.InvariantCulture);
            }

            var elementWidth = element.Size.Width;
            var elementHeight = element.Size.Height;

            return CutOutScreenShot(folder, screenshotName, locationX, locationY, elementWidth, elementHeight, filePath);
        }

        /// <summary>
        /// Cut out the screen shot by giving locationX, locationY, elementWidth, elementHeight.
        /// </summary>
        /// <param name="folder">Folder to save new screenshot.</param>
        /// <param name="newScreenShotName">Name of new screenshot.</param>
        /// <param name="locationX">The x-coordinate of the upper-left corner of the new rectangle.</param>
        /// <param name="locationY">The y-coordinate of the upper-left corner of the new rectangle.</param>
        /// <param name="elementWidth">The width of the new rectangle.</param>
        /// <param name="elementHeight">The height of the new rectangle.</param>
        /// <param name="fullPathToScreenShotToCutOut">Full path to the screenshot to be cut out.</param>
        /// <returns>Full path of cutted out screenshot.</returns>
        /// <example>How to use it: <code>
        /// var fullPath = CutOutScreenShot(folder, screenshotName, locationX, locationY, elementWidth, elementHeight, fullPathToScreenShotToCutOut);
        /// </code></example>
        public static string CutOutScreenShot(string folder, string newScreenShotName, int locationX, int locationY, int elementWidth, int elementHeight, string fullPathToScreenShotToCutOut)
        {
            Logger.Debug(CultureInfo.CurrentCulture, "Trying to cut out screenshot locationX:{0} locationY:{1} elementWidth:{2} elementHeight:{3}", locationX, locationY, elementWidth, elementHeight);

            string newFilePath = Path.Combine(folder, newScreenShotName + ".png");

            try
            {
                using (var image = Image.Load<Rgba32>(fullPathToScreenShotToCutOut))
                {
                    // Ensure the crop rectangle is within the image bounds
                    if (locationY > image.Height || locationX > image.Width)
                    {
                        Logger.Error(CultureInfo.CurrentCulture, "Cutting out screenshot locationX:{0} locationY:{1} elementWidth:{2} elementHeight:{3} is not possible", locationX, locationY, elementWidth, elementHeight);
                        return null;
                    }

                    if (image.Height - locationY < elementHeight)
                    {
                        elementHeight = image.Height - locationY;
                    }

                    if (image.Width - locationX < elementWidth)
                    {
                        elementWidth = image.Width - locationX;
                    }

                    Logger.Debug(CultureInfo.CurrentCulture, "Cutting out screenshot locationX:{0} locationY:{1} elementWidth:{2} elementHeight:{3}", locationX, locationY, elementWidth, elementHeight);

                    var cropRectangle = new Rectangle(locationX, locationY, elementWidth, elementHeight);
                    image.Mutate(x => x.Crop(cropRectangle));
                    image.Save(newFilePath, new PngEncoder());
                }

                File.Delete(fullPathToScreenShotToCutOut);
            }
            catch (SixLabors.ImageSharp.ImageFormatException ex)
            {
                Logger.Error(CultureInfo.CurrentCulture, "Error during screenshot cropping: {0}", ex);
                return null;
            }

            Logger.Debug(CultureInfo.CurrentCulture, "Saving new screenshot {0}", newFilePath);
            return newFilePath;
        }
    }
}