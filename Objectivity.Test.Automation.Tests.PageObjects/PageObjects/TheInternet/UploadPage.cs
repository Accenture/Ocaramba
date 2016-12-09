// <copyright file="UploadPage.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet
{
    using System;
    using System.Globalization;
    using System.IO;

    using NLog;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Helpers;
    using Objectivity.Test.Automation.Common.Types;
    using Objectivity.Test.Automation.Tests.PageObjects;

    public class UploadPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator uploadPageHeader = new ElementLocator(Locator.XPath, "//h3[.='File Uploader']"),
                                        fileUpload = new ElementLocator(Locator.Id, "file-upload"),
                                        fileSumbit = new ElementLocator(Locator.Id, "file-submit"),
                                        fileUploadedPageHeader = new ElementLocator(Locator.XPath, "//h3[.='File Uploaded!']");

        public UploadPage(DriverContext driverContext)
            : base(driverContext)
        {
            Logger.Info("Waiting for File Upload page to open");
            this.Driver.IsElementPresent(this.uploadPageHeader, BaseConfiguration.ShortTimeout);
        }

        public UploadPage UploadFile(string newName)
        {
            if (BaseConfiguration.TestBrowser == BrowserType.Firefox
                || BaseConfiguration.TestBrowser == BrowserType.Chrome
                || BaseConfiguration.TestBrowser == BrowserType.RemoteWebDriver)
            {
                newName = FilesHelper.CopyFile(BaseConfiguration.ShortTimeout, "filetocompare_branch.txt", newName, this.DriverContext.DownloadFolder);
                this.Driver.GetElement(this.fileUpload).SendKeys(this.DriverContext.DownloadFolder + "\\" + newName);
                this.Driver.GetElement(this.fileSumbit).Click();
                this.Driver.IsElementPresent(this.fileUploadedPageHeader, BaseConfiguration.ShortTimeout);
            }
            else
            {
               Logger.Info(CultureInfo.CurrentCulture, "Uploading files in browser {0} is not supported", BaseConfiguration.TestBrowser);
            }

            return this;
        }
    }
}
