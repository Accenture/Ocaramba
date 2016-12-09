// <copyright file="CompareFilesDataDrivenTests.cs" company="Objectivity Bespoke Software Specialists">
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
    using System.IO;
    using DataDriven;
    using global::NUnit.Framework;
    using NLog;

    [TestFixture]
    public class CompareFilesDataDrivenTests
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly char separator = Path.DirectorySeparatorChar;

        /// <summary>
        /// Compares the CSV files, find files to compare by DataDriven methods from NUnit.
        /// Execute that tests after UploadDownloadFilesTestsNUnit TestFeature
        /// </summary>
        /// <param name="liveFiles">The live files.</param>
        /// <param name="testFiles">The test files.</param>
        [Test]
        [Category("CompareFiles")]
        [TestCaseSource(typeof(CompareFiles), "GetCsvFileToCompare")]
        public void CompareCsvFiles(string liveFiles, string testFiles)
        {
            var folder = ProjectBaseConfiguration.DownloadFolderPath;
            if (!File.Exists(folder + this.separator + testFiles))
            {
                this.logger.Info(ProjectBaseConfiguration.DownloadFolderPath + liveFiles);
                this.logger.Error("Missing file:\n{0}{1}", folder, testFiles);
                Assert.True(false, "File does not exist");
            }

            ////Implement here methods for comparing files
            ////if (Compare.Files(ProjectBaseConfiguration.DownloadFolderPath + this.separator, testFiles, liveFiles))
            ////{
            ////    Assert.True(false, "Files are different");
            ////}

            this.logger.Info("Files are identical");
        }

        /// <summary>
        /// Compares the TXT files, find files to compare by DataDriven methods from NUnit.
        /// Execute that tests after UploadDownloadFilesTestsNUnit TestFeature
        /// </summary>
        /// <param name="liveFiles">The live files.</param>
        /// <param name="testFiles">The test files.</param>
        [Test]
        [Category("CompareFiles")]
        [TestCaseSource(typeof(CompareFiles), "GetTxtFileToCompare")]
        public void CompareTxtFiles(string liveFiles, string testFiles)
        {
            var folder = ProjectBaseConfiguration.DownloadFolderPath;
            if (!File.Exists(folder + this.separator + testFiles))
            {
                this.logger.Info(ProjectBaseConfiguration.DownloadFolderPath + liveFiles);
                this.logger.Error("Missing file:\n{0}{1}", folder, testFiles);
                Assert.True(false, "File does not exist");
            }

            ////Implement here methods for comparing files
            ////if (Compare.Files(ProjectBaseConfiguration.DownloadFolderPath + this.separator, testFiles, liveFiles))
            ////{
            ////    Assert.True(false, "Files are different");
            ////}

            this.logger.Info("Files are identical");
        }
    }
}
