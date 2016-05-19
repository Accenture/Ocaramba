// <copyright file="TakeScreenShot.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.Common.Helpers
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

    /// <summary>
    /// Custom screenshot solution
    /// </summary>
    public static class TakeScreenShot
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Takes screen shot.
        /// </summary>
        /// <returns>Image contains desktop screenshot</returns>
        public static Bitmap DoIt()
        {
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
        public static void Save(Bitmap bitmap, ImageFormat format, string folder, string title)
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
                Logger.Info(CultureInfo.CurrentCulture, "##teamcity[publishArtifacts '{0}']", filePath);
            }
        }
    }
}
