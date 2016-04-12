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

namespace Objectivity.Test.Automation.Common.Helpers
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;
    using NLog;
    /// <summary>
    /// Contains useful actions connected with test data
    /// </summary>
    public static class NameHelper
    {
        /// <summary>
        /// NLog logger handle
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Create random name.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>Random name</returns>
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
        /// Replaces the special characters.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>String without special characters</returns>
        public static string ReplaceSpecialCharacters(string text)
        {
            Logger.Trace(CultureInfo.CurrentCulture, "Removing all special characters");
            text = Regex.Replace(text, @"[^0-9a-zA-Z._]+", "_");
            return text;
        }


        /// <summary>
        /// Shortens the text.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="text">The text.</param>
        /// <returns>String with removed all '_'</returns>
        public static string ShortenText(string folder, string text)
        {
            if ((folder + text).Length > 255)
            {
                text = text.Replace("_", string.Empty);
                Logger.Info(CultureInfo.CurrentCulture, "Lengt of the file fullname is over 255 characters, removing all '_'");
            }

            if ((folder + text).Length > 255)
            {
                Logger.Error(CultureInfo.CurrentCulture, "Lengt of the file fullname is over 255 characters, try to shorten the name of tests");
            }
            return text;
        }
    }
}