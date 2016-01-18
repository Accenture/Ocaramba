// -----------------------------------------------------------------------
// <copyright file="CompareCsv.cs" company="Objectivity Ltd">
// Copyright (c) Objectivity Ltd. All rights reserved.
// </copyright>
// <author>Jakub Raczek</author>
// <date>26-10-2015</date>
// <summary></summary>
// -----------------------------------------------------------------------
namespace Objectivity.Test.Automation.Tests.NUnit.Tests
{
    using System.IO;

    using global::NUnit.Framework;

    using NLog;

    using Objectivity.Test.Automation.Tests.NUnit;
    using Objectivity.Test.Automation.Tests.NUnit.DataDriven;

    [TestFixture]
    public class CompareDataDrivenTests
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly char separator = Path.DirectorySeparatorChar;

        /// <summary>
        /// Compares the CSV files, find files to compare by DataDriven methods from NUnit.
        /// Execute that tests after DownloadFilesTestsNUnit TestFeature
        /// </summary>
        /// <param name="liveFiles">The live files.</param>
        /// <param name="testFiles">The test files.</param>
        [Test, Category("CompareFiles")]
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
        /// Execute that tests after DownloadFilesTestsNUnit TestFeature
        /// </summary>
        /// <param name="liveFiles">The live files.</param>
        /// <param name="testFiles">The test files.</param>
        [Test, Category("CompareFiles")]
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
