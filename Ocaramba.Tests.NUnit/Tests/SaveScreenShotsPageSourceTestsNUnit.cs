// <copyright file="SaveScreenShotsPageSourceTestsNUnit.cs" company="Accenture">
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

namespace Ocaramba.Tests.NUnit.Tests
{
    using System.Drawing.Imaging;
    using System.Globalization;
    using global::NUnit.Framework;
    using Ocaramba.Helpers;
    using Ocaramba.Tests.PageObjects.PageObjects.TheInternet;

    [TestFixture]
    public class SaveScreenShotsPageSourceTestsNUnit : ProjectTestBase
    {
        [Test]
        [Category("TakingScreehShots")]
        [Category("NotImplementedInCoreOrUploadDownload")]
        public void SaveFullScreenShotTest()
        {
            var downloadPage = new InternetPage(this.DriverContext).OpenHomePage().GoToFileDownloader();
            var screenShotNumber = FilesHelper.CountFiles(this.DriverContext.ScreenShotFolder, FileType.Png);
#if net47
           // Assert.IsNotNull(TakeScreenShot.Save(TakeScreenShot.DoIt(), ImageFormat.Png, this.DriverContext.ScreenShotFolder, string.Format(CultureInfo.CurrentCulture, this.DriverContext.TestTitle + "_first")));
#endif
            var nameOfScreenShot = downloadPage.CheckIfScreenShotIsSaved(screenShotNumber);
            TestContext.AddTestAttachment(nameOfScreenShot);
            Assert.That(nameOfScreenShot.Contains(this.DriverContext.TestTitle), Is.True, "Name of screenshot doesn't contain Test Title");
            Assert.That(this.DriverContext.TakeAndSaveScreenshot(), Is.Not.Null);
        }

        [Test]
        public void SaveWebDriverScreenShotTest()
        {
            var downloadPage = new InternetPage(this.DriverContext).OpenHomePage().GoToFileDownloader();
            var screenShotNumber = FilesHelper.CountFiles(this.DriverContext.ScreenShotFolder, FileType.Png);
            Assert.That(downloadPage.SaveWebDriverScreenShot(), Is.Not.Null);
            var nameOfScreenShot = downloadPage.CheckIfScreenShotIsSaved(screenShotNumber);
            TestContext.AddTestAttachment(nameOfScreenShot);
            Assert.That(nameOfScreenShot.Contains(this.DriverContext.TestTitle), Is.True, "Name of screenshot doesn't contain Test Title");
        }

        [Test]
        [Category("NotImplementedInCoreOrUploadDownload")]
        public void SaveSourcePageTest()
        {
            var basicAuthPage = new InternetPage(this.DriverContext).OpenHomePageWithUserCredentials().GoToBasicAuthPage();
            var name = this.DriverContext.TestTitle + FilesHelper.ReturnFileExtension(FileType.Html);
            FilesHelper.DeleteFile(name, this.DriverContext.PageSourceFolder);
            var pageSourceNumber = FilesHelper.CountFiles(this.DriverContext.PageSourceFolder, FileType.Html);
            Assert.That(basicAuthPage.SaveSourcePage(), Is.Not.Null);
            basicAuthPage.CheckIfPageSourceSaved();
            Assert.That(pageSourceNumber < FilesHelper.CountFiles(this.DriverContext.PageSourceFolder, FileType.Html), "Number of html files did not increase");
        }
    }
}
