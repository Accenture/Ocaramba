﻿// <copyright file="JavaScriptAlertsTestsNUnit.cs" company="Accenture">
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

using System.IO;
using ImageMagick;
using NUnit.Framework;
using Ocaramba.Helpers;
using Ocaramba.Tests.PageObjects.PageObjects.TheInternet;

namespace Ocaramba.UnitTests.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    [Category("TakingScreehShots")] 
    [Category("NotImplementedInCoreOrUploadDownload")]
    public class TakingScreehShotsOfElementsTests : ProjectTestBase
    {

        string folder = TestContext.CurrentContext.TestDirectory;


        [Test]
        public void TakingScreehShotsOfElementInIFrameTest()
        {
            var internetPage = new InternetPage(this.DriverContext).OpenHomePage();
            internetPage.GoToIFramePage();
            IFramePage page = new IFramePage(this.DriverContext);
            var path = page.TakeScreenShotsOfTextInIFrame(folder + FilesHelper.Separator + BaseConfiguration.ScreenShotFolder, "TextWithinIFrame" + BaseConfiguration.TestBrowser);
            var path2 = folder + FilesHelper.Separator + BaseConfiguration.ScreenShotFolder + FilesHelper.Separator + "TextWithinIFrameChromeError.png";
            bool flag = true;
            using (var img1 = new MagickImage(path))
            {
                using (var img2 = new MagickImage(path2))
                {
                    using (var imgDiff = new MagickImage())
                    {
                        img1.Compose = CompositeOperator.Src;
                        img1.Compare(img2, ErrorMetric.MeanAbsolute, Channels.RGB);
                        flag = img1.Equals(img2);
                        imgDiff.Write(folder + FilesHelper.Separator + BaseConfiguration.ScreenShotFolder + FilesHelper.Separator + BaseConfiguration.TestBrowser + "TextWithinIFrameDIFF.png");
                    }
                }
            }




            Assert.That(flag, Is.False);
        }

        [Test]
        public void TakingScreenShotsOfElementTest()
        {
            var internetPage = new InternetPage(this.DriverContext).OpenHomePage();
            internetPage.GoToIFramePage();

            IFramePage page = new IFramePage(this.DriverContext);
            page.TakeScreenShotsOfMenu(folder + FilesHelper.Separator + BaseConfiguration.ScreenShotFolder, "MenuOutSideTheIFrame" + BaseConfiguration.TestBrowser);
        }
    }
}
