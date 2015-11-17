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

namespace Objectivity.Test.Automation.NunitTests.PageObjects
{
    using System;
    using System.Globalization;
    using System.IO;

    using NLog;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Helpers;
    using Objectivity.Test.Automation.Common.Types;

    public class InternetDownloadPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator downloadPageHeader = new ElementLocator(Locator.XPath, "//h3[.='File Downloader']"),
                                        fileLink = new ElementLocator(Locator.CssSelector, ".example>a:nth-of-type({0})");

        public InternetDownloadPage(DriverContext driverContext)
            : base(driverContext)
        {
            Logger.Info("Waiting for File Download page to open");
            this.Driver.WaitForElement(this.downloadPageHeader, BaseConfiguration.ShortTimeout);
        }

        public InternetDownloadPage SaveFile(string fileName)
        {
            this.Driver.GetElement(this.fileLink.Evaluate(1)).Click();
            FilesHelper.WaitForFile(this.Driver, fileName, BaseConfiguration.DownloadFolder);
            return this;
        }

        public InternetDownloadPage SaveFile()
        {
            var filesNumber = FilesHelper.CountFiles(BaseConfiguration.DownloadFolder, FilesHelper.FileType.Txt);
            this.Driver.GetElement(this.fileLink.Evaluate(1)).Click();
            FilesHelper.WaitForFile(FilesHelper.FileType.Txt, this.Driver,filesNumber, BaseConfiguration.DownloadFolder);
            FileInfo file = FilesHelper.GetLastFile(BaseConfiguration.DownloadFolder, FilesHelper.FileType.Txt);
            FilesHelper.RenameFile(file.Name, "new_name_of_file", BaseConfiguration.DownloadFolder, FilesHelper.FileType.Txt);
            return this;
        }
    }
}
