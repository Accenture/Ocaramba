﻿// <copyright file="TakeScreenShot.cs" company="Objectivity Bespoke Software Specialists">
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

#if net47
namespace Ocaramba.Helpers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using NLog;
    using Ocaramba.Extensions;
    using Ocaramba.Helpers;
    using OpenQA.Selenium;

    /// <summary>
    /// Custom screenshot solution.
    /// </summary>
    public static class TakeScreenShot
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Takes screen shot.
        /// </summary>
        /// <returns>Image contains desktop screenshot.</returns>
        public static Bitmap DoIt()
        {
            Logger.Info("****************************Taking*Screenshot***************************************");
            var screen = Screen.PrimaryScreen;
            using (var bitmap = new Bitmap(screen.Bounds.Width, screen.Bounds.Height))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    try
                    {
                        graphics.CopyFromScreen(0, 0, 0, 0, screen.Bounds.Size);
                    }
                    catch (Win32Exception)
                    {
                        Logger.Error("Win32Exception Exception, user is locked out with no access to windows desktop");
                        return null;
                    }

                    Logger.Error("Screenshot taken.");
                }

                return (Bitmap)bitmap.Clone();
            }
        }

        /// <summary>
        /// Saves the specified bitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="format">The format.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="title">The title.</param>
        /// <returns>The path to the saved bitmap, null if not saved.</returns>
        public static string Save(Bitmap bitmap, ImageFormat format, string folder, string title)
        {
            var fileName = string.Format(CultureInfo.CurrentCulture, "{0}_{1}_{2}.png", title, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff", CultureInfo.CurrentCulture), "fullscreen");
            fileName = Regex.Replace(fileName, "[^0-9a-zA-Z._]+", "_");
            fileName = NameHelper.ShortenFileName(folder, fileName, "_", 255);
            var filePath = Path.Combine(folder, fileName);

            if (bitmap == null)
            {
                Logger.Error("Full screenshot is not saved");
            }
            else
            {
                bitmap.Save(filePath, format);
                bitmap.Dispose();
                Logger.Error(CultureInfo.CurrentCulture, "Test failed: full screenshot saved to {0}.", filePath);
                FilesHelper.WaitForFileOfGivenName(BaseConfiguration.ShortTimeout, fileName, folder);
                Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "##teamcity[publishArtifacts '{0}']", filePath));
                return filePath;
            }

            return null;
        }

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
            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);

            if (BaseConfiguration.TestBrowser == BrowserType.Chrome)
            {
                locationX = element.Location.X + locationX;
                locationY = element.Location.Y + locationY;
            }
            else
            {
                locationX = element.Location.X;
                locationY = element.Location.Y;
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

            string newFilePath;
            Bitmap importFile = null;
            Bitmap cloneFile;
            try
            {
                importFile = new Bitmap(fullPathToScreenShotToCutOut);
                Logger.Debug(CultureInfo.CurrentCulture, "Size of imported screenshot Width:{0} Height:{1}", importFile.Size.Width, importFile.Size.Height);
                ////Check if new size of image is not bigger than imported.
                if (locationY > importFile.Size.Height || locationX > importFile.Size.Width)
                {
                    Logger.Error(CultureInfo.CurrentCulture, "Cutting out screenshot locationX:{0} locationY:{1} elementWidth:{2} elementHeight:{3} is not possible", locationX, locationY, elementWidth, elementHeight);
                    return null;
                }

                if (importFile.Size.Height - locationY < elementHeight)
                {
                    elementHeight = importFile.Size.Height - locationY;
                }

                if (importFile.Size.Width - locationX < elementWidth)
                {
                    elementWidth = importFile.Size.Width - locationX;
                }

                Logger.Debug(CultureInfo.CurrentCulture, "Cutting out screenshot locationX:{0} locationY:{1} elementWidth:{2} elementHeight:{3}", locationX, locationY, elementWidth, elementHeight);
                var image = new Rectangle(locationX, locationY, elementWidth, elementHeight);
                newFilePath = Path.Combine(folder, newScreenShotName + ".png");
                cloneFile = (Bitmap)importFile.Clone(image, importFile.PixelFormat);
            }
            finally
            {
                importFile?.Dispose();
            }

            Logger.Debug(CultureInfo.CurrentCulture, "Saving new screenshot {0}", newFilePath);
            cloneFile.Save(newFilePath);
            File.Delete(fullPathToScreenShotToCutOut);
            return newFilePath;
        }
    }
}
#endif