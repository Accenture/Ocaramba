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

namespace Objectivity.Test.Automation.Tests.MsTest
{
    using System.Configuration;
    using System.IO;
    using System.Reflection;

    using Objectivity.Test.Automation.Common;

    public static class ProjectBaseConfiguration
    {
        public static string DownloadFolder
        {
            get { return GetFolder(ConfigurationManager.AppSettings["TestOutput"]); }
        }

        public static string ScreenShotFolder
        {
            get { return GetFolder(ConfigurationManager.AppSettings["TestOutput"]); }
        }

        public static string PageSourceFolder
        {
            get { return GetFolder(ConfigurationManager.AppSettings["TestOutput"]); }
        }

        /// <summary>
        /// Gets the folder from app.config as value of given key.
        /// </summary>
        /// <param name="appConfigValue">The application configuration value.</param>
        /// <returns></returns>
        private static string GetFolder(string appConfigValue)
        {
            string folder;

            if (string.IsNullOrEmpty(appConfigValue))
            {
                folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
            else
            {
                if (BaseConfiguration.UseCurrentDirectory)
                {
                    folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + appConfigValue;
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

            return folder;
        }
    }
}
