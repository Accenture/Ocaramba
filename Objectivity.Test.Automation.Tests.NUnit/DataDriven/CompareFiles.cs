// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExcelFiles.cs" company="Objectivity Ltd">
//   Copyright (c) Objectivity Ltd. All rights reserved.
// </copyright>
// <author>jraczek</author>
// <date>27-10-2015</date>
// <summary>
//   DataDriven class for Excel Files
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Objectivity.Test.Automation.Tests.NUnit.DataDriven
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    using global::NUnit.Framework;

    using NLog;

    using Objectivity.Test.Automation.Common.Helpers;
    using Objectivity.Test.Automation.Tests.NUnit;

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
        /// The get files.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private static IEnumerable<TestCaseData> FindFiles(FileType type)
        {
            Logger.Info("Get Files {0}:", type);
            var liveFiles = FilesHelper.GetFilesOfGivenType(
                ProjectBaseConfiguration.DownloadFolderPath,
                type, 
                "live");

            if (liveFiles != null)
            {
                foreach (FileInfo liveFile in liveFiles)
                {
                    Logger.Trace("liveFile: {0}", liveFile);
                    string fileName =
                        liveFile.Name.Replace(
                            "live",
                            "branch");
                    string fileNameShort =
                        liveFile.Name.Replace(
                            "_" + "live",
                            string.Empty);
                    fileNameShort =
                        fileNameShort.Replace(
                            "_" + "branch",
                            string.Empty);
                    TestCaseData data = new TestCaseData(liveFile.Name, fileName);
                    data.SetName(fileNameShort.Replace(" ", "_").Replace(".", "_"));
                    Logger.Trace("file Name Short: {0}", fileNameShort);
                    yield return data;
                }
            }
        }
    }
}
