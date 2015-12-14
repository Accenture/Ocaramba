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

namespace Objectivity.Test.Automation.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    using NLog;

    using Objectivity.Test.Automation.Common;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Class for handling downloading files
    /// </summary>
    public static class FilesHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Directory separator
        /// </summary>
        public static readonly char Separator = Path.DirectorySeparatorChar;

        /// <summary>
        /// File type
        /// </summary>
        public enum FileType
        {
            /// <summary>
            /// File type not implemented
            /// </summary>
            None = 0,

            /// <summary>
            /// Portable document format files
            /// </summary>
            Pdf = 1,

            /// <summary>
            /// Excel files
            /// </summary>
            Excel = 2,

            /// <summary>
            /// Comma-separated values files
            /// </summary>
            Csv = 3,

            /// <summary>
            /// Text files
            /// </summary>
            Txt = 4
        }

        /// <summary>
        /// Gets the full path of folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns></returns>
        /// <value>Download folder path</value>
        public static string GetFolder(string folder)
        {
                if (BaseConfiguration.UseCurrentDirectory)
                {
                    return
                        System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + folder;
                }

                return folder;
        }

        /// <summary>
        /// Returns the file extension.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Files extension</returns>
        private static string ReturnFileExtension(FileType type)
        {
            switch (type)
            {
                case FileType.Pdf:
                    return ".pdf";
                case FileType.Excel:
                    return ".xls?";
                case FileType.Csv:
                    return ".csv";
                case FileType.Txt:
                    return ".txt";
                default:
                    return "none";
            }
        }

        /// <summary>
        /// Creates the folder if not exists.
        /// </summary>
        /// <param name="folder">The directory.</param>
        public static void CreateFolder(string folder)
        {
            if (!Directory.Exists(GetFolder(folder)))
            {
                Directory.CreateDirectory(GetFolder(folder));
            }
        }

        /// <summary>
        /// Gets the files of given type, use postfixFilesName in search pattern.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="type">The type of files.</param>
        /// <param name="postfixFilesName">Postfix name of files for search pattern.</param>
        /// <returns>Collection of files</returns>
        public static ICollection<FileInfo> GetFilesOfGivenType(string folder, FileType type, string postfixFilesName)
        {
            Logger.Debug("Get Files '{0}' from '{1}', postfixFilesName '{2}'", type, folder, postfixFilesName);
            CreateFolder(folder);
            ICollection<FileInfo> files =
                new DirectoryInfo(GetFolder(folder))
                    .GetFiles("*" + postfixFilesName + ReturnFileExtension(type)).OrderBy(f => f.Name).ToList();

            return files;
        }

        /// <summary>
        /// Get file by its name in given folder
        /// </summary>
        /// <param name="folder">The folder</param>
        /// <param name="fileName">The file name</param>
        /// <returns>FileInfo of file</returns>
        public static FileInfo GetFileByName(string folder, string fileName)
        {
            Logger.Debug("Get File '{0}' from '{1}'", fileName, GetFolder(folder));
            CreateFolder(folder);
            FileInfo file =
                new DirectoryInfo(GetFolder(folder))
                    .GetFiles(fileName).First();

            return file;
        }

        /// <summary>
        /// Gets the files of given type.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="type">The type.</param>
        /// <returns>Collection of files</returns>
        public static ICollection<FileInfo> GetFilesOfGivenType(string folder, FileType type)
         {
             string environment = string.Empty;
             return GetFilesOfGivenType(folder, type, environment);
         }

         /// <summary>
         /// Counts the files of given type.
         /// </summary>
        /// <param name="folder">The folder.</param>
         /// <param name="type">The type.</param>
         /// <returns>Number of files in subfolder</returns>
        public static int CountFiles(string folder, FileType type)
        {
            Logger.Debug(CultureInfo.CurrentCulture, "Count {0} Files in '{1}'", type, folder);
            var fileNumber = GetFilesOfGivenType(folder, type).Count;
            Logger.Debug(CultureInfo.CurrentCulture, "Number of files in '{0}': {1}", folder, fileNumber);
            return fileNumber;
        }

        /// <summary>
        /// Gets the last file of given type.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="type">The type of file.</param>
        /// <returns>Last file of given type</returns>
        public static FileInfo GetLastFile(string folder, FileType type)
        {
            Logger.Debug("Get Last File");
            CreateFolder(folder);
            var lastFile = new DirectoryInfo(GetFolder(folder)).GetFiles()
                .Where(f => f.Extension == ReturnFileExtension(type).Replace("?", string.Empty))
                .OrderByDescending(f => f.CreationTime)
                .First();
            Logger.Trace("Last File: {0}", lastFile);
            return lastFile;
        }

        /// <summary>
        /// Waits for file for given timeout till number of files increase in sub folder.
        /// </summary>
        /// <param name="type">The type of file.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="waitTime">Wait timeout</param>
        /// <param name="filesNumber">The initial files number.</param>
        /// <param name="folder">The folder.</param>
        public static void WaitForFile(FileType type, IWebDriver driver, double waitTime, int filesNumber, string folder)
        {
            Logger.Debug("Wait for file: {0}", type);
            CreateFolder(folder);
            IWait<IWebDriver> wait = new WebDriverWait(
                    driver,
                    TimeSpan.FromSeconds(waitTime));

            wait.Message = string.Format(CultureInfo.CurrentCulture, "Waiting for file number to increase in {0}", folder);
            wait.Until(x => CountFiles(folder, type) > filesNumber);
            Logger.Debug("Number of files increased, checking if size of last file > 0 bytes");
            wait.Message = "Checking if size of last file > 0 bytes";
            wait.Until(x => GetLastFile(folder, type).Length > 0);
        }

        /// <summary>
        /// Waits for file for LongTimeout till number of files increase in sub folder.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="filesNumber">The files number.</param>
        /// <param name="folder">The folder.</param>
        public static void WaitForFile(FileType type, IWebDriver driver, int filesNumber, string folder)
        {
            WaitForFile(type, driver, BaseConfiguration.LongTimeout, filesNumber, folder);
        }

        /// <summary>
        /// Waits for file with given name with given timeout.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="waitTime">Wait timeout</param>
        /// <param name="filesName">Name of the files.</param>
        /// <param name="folder">The folder.</param>
        public static void WaitForFile(IWebDriver driver, double waitTime, string filesName, string folder)
        {
            Logger.Debug(CultureInfo.CurrentCulture, "Wait for file: {0}", filesName);
            CreateFolder(folder);
            IWait<IWebDriver> wait = new WebDriverWait(
                    driver,
                    TimeSpan.FromSeconds(waitTime));

            wait.Message = string.Format(CultureInfo.CurrentCulture, "Waiting for file {0} in folder {1}", filesName, GetFolder(folder));
            wait.Until(x => File.Exists(GetFolder(folder) + Separator + filesName));

            Logger.Debug("File exists, checking if size of last file > 0 bytes");
            wait.Message = string.Format(CultureInfo.CurrentCulture, "Checking if size of file {0} > 0 bytes", filesName);
            wait.Until(x => GetFileByName(folder, filesName).Length > 0);
        }

        /// <summary>
        /// Waits for file with given name with LongTimeout
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="filesName">Name of the files.</param>
        /// <param name="folder">The folder.</param>
        public static void WaitForFile(IWebDriver driver, string filesName, string folder)
        {
            WaitForFile(driver, BaseConfiguration.LongTimeout, filesName, folder);
        }

        /// <summary>
        /// Rename the file.
        /// </summary>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        /// <param name="subFolder">The subFolder.</param>
        /// <param name="type">The type of file.</param>
        public static void RenameFile(string oldName, string newName, string subFolder, FileType type)
        {
            CreateFolder(subFolder);
            string folder = GetFolder(subFolder);
            newName = newName + ReturnFileExtension(type).Replace("?", string.Empty);

            Logger.Debug(CultureInfo.CurrentCulture, "new file name: {0}", newName);
            if (File.Exists(newName))
            {
                File.Delete(newName);
            }

            // Use ProcessStartInfo class   
            string command = "/c ren " + '\u0022' + oldName + '\u0022' + " " + '\u0022' + newName +
                             '\u0022';
            ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe")
                                         {
                                             WorkingDirectory = folder,
                                             Arguments = command
                                         };
            Thread.Sleep(1000);
            Process.Start(cmdsi);
        }
    }
}