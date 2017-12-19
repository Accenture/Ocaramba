// <copyright file="UploadDownloadFilesTestsNUnit.cs" company="Objectivity Bespoke Software Specialists">
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
    using Automation.Tests.PageObjects.PageObjects.TheInternet;
    using global::NUnit.Framework;

    /// <summary>
    /// Tests to test framework, downloading files, must be exexuted after UploadFilesTestsNUnit.
    /// </summary>
    [TestFixture]
    [Category("herokuapp")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class UploadDownloadFilesTestsNUnit : ProjectTestBase
    {
        [Test]
        public void FirstUploadFileTest()
        {
            new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToFileUploader()
                .UploadFile("ObjectivityTestAutomationCSHarpFramework.txt");
        }

        [Test]
        public void SecondDownloadFileByNameTest()
        {
            new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToFileDownloader()
                .SaveFile("ObjectivityTestAutomationCSHarpFramework.txt", "name_of_file_branch");
        }

        [Test]
        public void DownloadFileByNameTest()
        {
            new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToFileDownloader()
                .SaveFile("some-file.txt", "new_file");
        }

        [Test]
        public void SecondDownloadGivenTypeFileByCountTest()
        {
            new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToFileDownloader()
                .SaveFile("name_of_file_live");
        }

        [Test]
        public void SecondDownloadAnyFileByCountTest()
        {
            new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToFileDownloader()
                .SaveFile("name_of_file_branch");
        }

        [Test]
        public void SecondSecureDownloadFileByNameTest()
        {
            new InternetPage(this.DriverContext)
                .OpenHomePageWithUserCredentials()
                .GoToSecureFileDownloadPage()
                .SaveFile("ObjectivityTestAutomationCSHarpFramework.txt", "name_of_file_live");
        }
    }
}
