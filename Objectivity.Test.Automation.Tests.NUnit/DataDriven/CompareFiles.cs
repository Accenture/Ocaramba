// <copyright file="CompareFiles.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.Tests.NUnit.DataDriven
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using Common.Helpers;
    using global::NUnit.Framework;
    using NLog;

    /// <summary>
    /// DataDriven comparing files  methods for NUnit test framework <see href="https://github.com/ObjectivityLtd/Test.Automation/wiki/Comparing-files-by-NUnit-DataDriven-tests">More details on wiki</see>
    /// </summary>
    public static class CompareFiles
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets the comma-separated values file to compare.
        /// </summary>
        public static IEnumerable GetCsvFileToCompare
        {
            get
            {
                return FindFiles(FileType.Csv);
            }
        }

        /// <summary>
        /// Gets the txt file to compare.
        /// </summary>
        public static IEnumerable GetTxtFileToCompare
        {
            get
            {
                return FindFiles(FileType.Txt);
            }
        }

        /// <summary>
        /// Get files to compare.
        /// </summary>
        /// <param name="type">Files type</param>
        /// <returns>
        /// Pairs of files to compare <see cref="IEnumerable"/>.
        /// </returns>
        private static IEnumerable<TestCaseData> FindFiles(FileType type)
        {
            Logger.Info("Get Files {0}:", type);
            var liveFiles = FilesHelper.GetFilesOfGivenType(ProjectBaseConfiguration.DownloadFolderPath, type, "live");

            if (liveFiles != null)
            {
                foreach (FileInfo liveFile in liveFiles)
                {
                    Logger.Trace("liveFile: {0}", liveFile);

                    var fileNameBranch = liveFile.Name.Replace("live", "branch");
                    var testCaseName = liveFile.Name.Replace("_" + "live", string.Empty);

                    TestCaseData data = new TestCaseData(liveFile.Name, fileNameBranch);
                    data.SetName(Regex.Replace(testCaseName, @"[.]+|\s+", "_"));

                    Logger.Trace("file Name Short: {0}", testCaseName);

                    yield return data;
                }
            }
        }
    }
}
