﻿// <copyright file="SecureFileDownloadPage.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Ocaramba.Tests.PageObjects.PageObjects.TheInternet
{
    using System.Globalization;
    using System.IO;
    using NLog;
    using Ocaramba;
    using Ocaramba.Extensions;
    using Ocaramba.Helpers;
    using Ocaramba.Types;

    public class SecureFileDownloadPage : ProjectPageBase
    {
#if net47
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
#endif
#if netcoreapp2_2
        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
#endif

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator downloadPageHeader = new ElementLocator(Locator.XPath, "//h3[.='Secure File Downloader']"),
                                        fileLink = new ElementLocator(Locator.CssSelector, "a[href='download_secure/{0}']");

        public SecureFileDownloadPage(DriverContext driverContext)
            : base(driverContext)
        {
            Logger.Info("Waiting for File Download page to open");
            this.Driver.IsElementPresent(this.downloadPageHeader, BaseConfiguration.ShortTimeout);
        }

        public SecureFileDownloadPage SaveFile(string fileName, string newName)
        {
            if (BaseConfiguration.TestBrowser == BrowserType.Firefox
                || BaseConfiguration.TestBrowser == BrowserType.Chrome)
            {
            this.Driver.GetElement(this.fileLink.Format(fileName)).Click();
            FilesHelper.WaitForFileOfGivenName(5, fileName, this.DriverContext.DownloadFolder);
            FileInfo file = FilesHelper.GetLastFile(this.DriverContext.DownloadFolder, FileType.Txt);
            FilesHelper.RenameFile(file.Name, newName, this.DriverContext.DownloadFolder, FileType.Csv);
            }
            else
            {
                Logger.Info(CultureInfo.CurrentCulture, "Downloading files in browser {0} is not supported", BaseConfiguration.TestBrowser);
            }

            return this;
        }
    }
}
