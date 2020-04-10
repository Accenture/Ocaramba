// <copyright file="NameHelper.cs" company="Objectivity Bespoke Software Specialists">
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
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;
    using NLog;

    /// <summary>
    /// Contains useful actions connected with test data.
    /// </summary>
    public static class NameHelper
    {
        /// <summary>
        /// NLog logger handle.
        /// </summary>
#if net47 || net45
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
#endif
#if netcoreapp3_1
        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
#endif

        /// <summary>
        /// Create random name.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>Random name.</returns>
        public static string RandomName(int length)
        {
            const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var randomString = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                randomString.Append(Chars[random.Next(Chars.Length)]);
            }

            return randomString.ToString();
        }

        /// <summary>
        /// Shortens the file name by removing occurrences of given pattern till length of folder + filename will be shorten than max Length.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="fileName">The fileName.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="maxLength">Max length.</param>
        /// <returns>String with removed all patterns.</returns>
        /// <example>How to use it: <code>
        /// NameHelper.ShortenFileName(folder, correctFileName, "_", 255);
        /// </code></example>
        public static string ShortenFileName(string folder, string fileName, string pattern, int maxLength)
        {
            Logger.Debug(CultureInfo.CurrentCulture, "Length of the file full name is {0} characters", (folder + fileName).Length);

            while (((folder + fileName).Length > maxLength) && fileName.Contains(pattern))
            {
                Logger.Trace(CultureInfo.CurrentCulture, "Length of the file full name is over {0} characters removing first occurrence of {1}", maxLength, pattern);
                Regex rgx = new Regex(pattern);
                fileName = rgx.Replace(fileName, string.Empty, 1);
                Logger.Trace(CultureInfo.CurrentCulture, "File full name: {0}", folder + fileName);
            }

            if ((folder + fileName).Length > 255)
            {
                Logger.Error(CultureInfo.CurrentCulture, "Length of the file full name is over {0} characters, try to shorten the name of tests", maxLength);
            }

            return fileName;
        }

        /// <summary>
        /// Remove all special characters except digit and letters.
        /// </summary>
        /// <param name="name">The string to remove special chracters.</param>
        /// <returns>String with removed all special chracters.</returns>
        /// <example>How to use it: <code>
        /// var name = NameHelper.RemoveSpecialCharacters("country/region");
        /// </code></example>
        public static string RemoveSpecialCharacters(string name)
        {
            Logger.Debug(CultureInfo.CurrentCulture, "Removing all special characters except digit and letters from '{0}'", name);
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            name = rgx.Replace(name, string.Empty);
            Logger.Debug(CultureInfo.CurrentCulture, "name without special characters: '{0}'", name);

            return name;
        }
    }
}