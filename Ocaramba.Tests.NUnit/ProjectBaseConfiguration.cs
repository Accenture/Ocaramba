// <copyright file="ProjectBaseConfiguration.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Ocaramba.Tests.NUnit
{
    using System.Configuration;
    using System.IO;
    using global::NUnit.Framework;
    using Ocaramba;
    using Ocaramba.Helpers;
#if netcoreapp2_2
    using Microsoft.Extensions.Configuration;
#endif

    /// <summary>
    /// SeleniumConfiguration that consume app.config file
    /// </summary>
    public static class ProjectBaseConfiguration
    {
        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();

        /// <summary>
        /// Gets the data driven file.
        /// </summary>
        /// <value>
        /// The data driven file.
        /// </value>
        public static string DataDrivenFile
        {
            get
            {
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["DataDrivenFile"];
#endif
#if netcoreapp2_2
                setting = BaseConfiguration.Builder["appSettings:DataDrivenFile"];
#endif
                if (BaseConfiguration.UseCurrentDirectory)
                {
                    return Path.Combine(CurrentDirectory + setting);
                }

                return setting;
            }
        }

        /// <summary>
        /// Gets the Excel data driven file.
        /// </summary>
        /// <value>
        /// The Excel data driven file.
        /// </value>
        public static string DataDrivenFileXlsx
        {
            get
            {
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["DataDrivenFileXlsx"];
#endif
#if netcoreapp2_2
                setting = BaseConfiguration.Builder["appSettings:DataDrivenFileXlsx"];
#endif
                if (BaseConfiguration.UseCurrentDirectory)
                {
                    return Path.Combine(CurrentDirectory + setting);
                }

                return setting;
            }
        }

        /// <summary>
        /// Gets the Download Folder path
        /// </summary>
        public static string DownloadFolderPath
        {
            get
            {
                string setting = null;
#if net45
                setting = ConfigurationManager.AppSettings["DownloadFolder"];
#endif
#if netcoreapp2_2
                setting = BaseConfiguration.Builder["appSettings:DownloadFolder"];
#endif
                return FilesHelper.GetFolder(setting, CurrentDirectory);
            }
        }
    }
}
