// <copyright file="SaveScreenShotsPageSourceTestsNUnit.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.Tests.NUnit.Tests
{
    using System.Drawing.Imaging;
    using System.Globalization;
    using Automation.Tests.PageObjects.PageObjects.TheInternet;
    using Common.Helpers;
    using global::NUnit.Framework;

    [TestFixture]
    public class SaveScreenShotsPageSourceTestsNUnit : ProjectTestBase
    {
        [Test]
        public void SaveFullScreenShotTest()
        {
            var downloadPage = new InternetPage(this.DriverContext).OpenHomePage().GoToFileDownloader();
            var screenShotNumber = FilesHelper.CountFiles(this.DriverContext.ScreenShotFolder, FileType.Png);
            Assert.IsNotNull(TakeScreenShot.Save(TakeScreenShot.DoIt(), ImageFormat.Png, this.DriverContext.ScreenShotFolder, string.Format(CultureInfo.CurrentCulture, this.DriverContext.TestTitle + "_first")));
            var nameOfScreenShot = downloadPage.CheckIfScreenShotIsSaved(screenShotNumber);
            TestContext.AddTestAttachment(nameOfScreenShot);
            Assert.IsTrue(nameOfScreenShot.Contains(this.DriverContext.TestTitle), "Name of screenshot doesn't contain Test Title");
            Assert.IsNotNull(this.DriverContext.TakeAndSaveScreenshot());
        }

        [Test]
        public void SaveWebDriverScreenShotTest()
        {
            var downloadPage = new InternetPage(this.DriverContext).OpenHomePage().GoToFileDownloader();
            var screenShotNumber = FilesHelper.CountFiles(this.DriverContext.ScreenShotFolder, FileType.Png);
            Assert.IsNotNull(downloadPage.SaveWebDriverScreenShot());
            var nameOfScreenShot = downloadPage.CheckIfScreenShotIsSaved(screenShotNumber);
            TestContext.AddTestAttachment(nameOfScreenShot);
            Assert.IsTrue(nameOfScreenShot.Contains(this.DriverContext.TestTitle), "Name of screenshot doesn't contain Test Title");
        }

        [Test]
        public void SaveSourcePageTest()
        {
            var basicAuthPage = new InternetPage(this.DriverContext).OpenHomePageWithUserCredentials().GoToBasicAuthPage();
            var name = this.DriverContext.TestTitle + FilesHelper.ReturnFileExtension(FileType.Html);
            FilesHelper.DeleteFile(name, this.DriverContext.PageSourceFolder);
            var pageSourceNumber = FilesHelper.CountFiles(this.DriverContext.PageSourceFolder, FileType.Html);
            Assert.IsNotNull(basicAuthPage.SaveSourcePage());
            basicAuthPage.CheckIfPageSourceSaved();
            Assert.IsTrue(pageSourceNumber < FilesHelper.CountFiles(this.DriverContext.PageSourceFolder, FileType.Html), "Number of html files did not increase");
        }
    }
}
