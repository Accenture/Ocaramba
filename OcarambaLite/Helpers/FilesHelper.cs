// <copyright file="FilesHelper.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Ocaramba.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using NLog;

    /// <summary>
    /// Class for handling downloading files <see href="https://github.com/ObjectivityLtd/Ocaramba/wiki/Downloading%20files">More details on wiki</see>.
    /// </summary>
    public static class FilesHelper
    {
        /// <summary>
        /// Directory separator.
        /// </summary>
        public static readonly char Separator = Path.DirectorySeparatorChar;

#if net47 || net45
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
#endif
#if netcoreapp3_1
        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
#endif

        /// <summary>
        /// Returns the file extension.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// Files extension.
        /// </returns>
        /// <example>How to use it: <code>
        /// FilesHelper.ReturnFileExtension(FileType.Html);
        /// </code></example>
        public static string ReturnFileExtension(FileType type)
        {
            switch (type)
            {
                case FileType.Pdf:
                    return ".pdf";
                case FileType.Xls:
                    return ".xls";
                case FileType.Csv:
                    return ".csv";
                case FileType.Txt:
                    return ".txt";
                case FileType.Doc:
                    return ".doc";
                case FileType.Xlsx:
                    return ".xlsx";
                case FileType.Docx:
                    return ".docx";
                case FileType.Gif:
                    return ".gif";
                case FileType.Jpg:
                    return ".jpg";
                case FileType.Bmp:
                    return ".bmp";
                case FileType.Png:
                    return ".png";
                case FileType.Xml:
                    return ".xml";
                case FileType.Html:
                    return ".html";
                case FileType.Ppt:
                    return ".ppt";
                case FileType.Pptx:
                    return ".pptx";
                default:
                    return "none";
            }
        }

        /// <summary>
        /// Gets the files of given type.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        /// Collection of files.
        /// </returns>
        /// <example>How to use it: <code>
        /// var files = GetFilesOfGivenType(folder, FileType.Txt);
        /// </code></example>
        public static ICollection<FileInfo> GetFilesOfGivenType(string folder, FileType type)
        {
            return GetFilesOfGivenType(folder, type, string.Empty);
        }

        /// <summary>
        /// Gets the files of given type, use postfixFilesName in search pattern.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="type">The type of files.</param>
        /// <param name="postfixFilesName">Postfix name of files for search pattern.</param>
        /// <returns>
        /// Collection of files.
        /// </returns>
        /// <example>How to use it: <code>
        /// var files = GetFilesOfGivenType(folder, FileType.Txt, "live");
        /// </code></example>
        public static ICollection<FileInfo> GetFilesOfGivenType(string folder, FileType type, string postfixFilesName)
        {
            Logger.Debug("Get Files '{0}' from '{1}', postfixFilesName '{2}'", type, folder, postfixFilesName);
            ICollection<FileInfo> files =
                new DirectoryInfo(folder)
                    .GetFiles("*" + postfixFilesName + ReturnFileExtension(type)).OrderBy(f => f.Name).ToList();

            return files;
        }

        /// <summary>
        /// Gets the files of given type from all sub folders.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="type">The type of files.</param>
        /// <returns>
        /// Collection of files.
        /// </returns>
        /// <example>How to use it: <code>
        /// var files = GetFilesOfGivenTypeFromAllSubFolders(folder, FileType.Txt);
        /// </code></example>
        public static ICollection<FileInfo> GetFilesOfGivenTypeFromAllSubFolders(string folder, FileType type)
        {
            return GetFilesOfGivenTypeFromAllSubFolders(folder, type, string.Empty);
        }

        /// <summary>
        /// Gets the files of given type from all sub folders, use postfixFilesName in search pattern.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="type">The type of files.</param>
        /// <param name="postfixFilesName">Postfix name of files for search pattern.</param>
        /// <returns>
        /// Collection of files.
        /// </returns>
        /// <example>How to use it: <code>
        /// var files = GetFilesOfGivenTypeFromAllSubFolders(folder, FileType.Txt, "live");
        /// </code></example>
        public static ICollection<FileInfo> GetFilesOfGivenTypeFromAllSubFolders(string folder, FileType type, string postfixFilesName)
        {
            Logger.Debug("Get Files '{0}' from '{1}', postfixFilesName '{2}'", type, folder, postfixFilesName);
            List<FileInfo> files =
                new DirectoryInfo(folder)
                    .GetFiles("*" + postfixFilesName + ReturnFileExtension(type), SearchOption.AllDirectories).OrderBy(f => f.Name).ToList();

            return files;
        }

        /// <summary>
        /// Gets all files from folder, use postfixFilesName in search pattern.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="postfixFilesName">Postfix name of files for search pattern.</param>
        /// <returns>
        /// Collection of files.
        /// </returns>
        /// <example>How to use it: <code>
        /// var files = GetAllFiles(folder, "live");
        /// </code></example>
        public static ICollection<FileInfo> GetAllFiles(string folder, string postfixFilesName)
        {
            Logger.Debug("Get all files from '{0}', postfixFilesName '{1}'", folder, postfixFilesName);
            ICollection<FileInfo> files =
                new DirectoryInfo(folder)
                    .GetFiles("*" + postfixFilesName).OrderBy(f => f.Name).ToList();

            return files;
        }

        /// <summary>
        /// Gets all files from all sub folders, use postfixFilesName in search pattern.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="postfixFilesName">Postfix name of files for search pattern.</param>
        /// <returns>
        /// Collection of files.
        /// </returns>
        /// <example>How to use it: <code>
        /// var files = GetAllFilesFromAllSubFolders(folder, "live");
        /// </code></example>
        public static ICollection<FileInfo> GetAllFilesFromAllSubFolders(string folder, string postfixFilesName)
        {
            Logger.Debug("Get all files from '{0}', postfixFilesName '{1}'", folder, postfixFilesName);
            ICollection<FileInfo> files =
                new DirectoryInfo(folder)
                    .GetFiles("*" + postfixFilesName, SearchOption.AllDirectories).OrderBy(f => f.Name).ToList();

            return files;
        }

        /// <summary>
        /// Gets all files from all sub folders.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>
        /// Collection of files.
        /// </returns>
        /// <example>How to use it: <code>
        /// var files = GetAllFilesFromAllSubFolders(folder);
        /// </code></example>
        public static ICollection<FileInfo> GetAllFilesFromAllSubFolders(string folder)
        {
            return GetAllFilesFromAllSubFolders(folder, string.Empty);
        }

        /// <summary>
        /// Gets all files from folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>
        /// Collection of files.
        /// </returns>
        /// <example>How to use it: <code>
        /// var files = GetAllFiles(folder);
        /// </code></example>
        public static ICollection<FileInfo> GetAllFiles(string folder)
        {
            return GetAllFiles(folder, string.Empty);
        }

        /// <summary>
        /// Get file by its name in given folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="fileName">The file name.</param>
        /// <returns>FileInfo of file.</returns>
        public static FileInfo GetFileByName(string folder, string fileName)
        {
            Logger.Debug("Get File '{0}' from '{1}'", fileName, folder);
            FileInfo file =
                new DirectoryInfo(folder)
                    .GetFiles(fileName).First();

            return file;
        }

        /// <summary>
        /// Counts the files of given type.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        /// Number of files in subfolder.
        /// </returns>
        /// <example>How to use it: <code>
        /// var filesNumber = FilesHelper.CountFiles(this.DriverContext.DownloadFolder, FileType.Txt);
        /// </code></example>
        public static int CountFiles(string folder, FileType type)
        {
            Logger.Debug(CultureInfo.CurrentCulture, "Count {0} Files in '{1}'", type, folder);
            var fileNumber = GetFilesOfGivenType(folder, type).Count;
            Logger.Debug(CultureInfo.CurrentCulture, "Number of files in '{0}': {1}", folder, fileNumber);
            return fileNumber;
        }

        /// <summary>
        /// Counts the files.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>
        /// Number of files in subfolder.
        /// </returns>
        /// <example>How to use it: <code>
        /// var filesNumber = FilesHelper.CountFiles(this.DriverContext.DownloadFolder);
        /// </code></example>
        public static int CountFiles(string folder)
        {
            Logger.Debug(CultureInfo.CurrentCulture, "Count all Files in '{0}'", folder);
            var fileNumber = GetAllFiles(folder).Count;
            Logger.Debug(CultureInfo.CurrentCulture, "Number of files in '{0}': {1}", folder, fileNumber);
            return fileNumber;
        }

        /// <summary>
        /// Gets the last file of given type.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="type">The type of file.</param>
        /// <returns>Last file of given type.</returns>
        /// <example>How to use it: <code>
        /// FilesHelper.GetLastFile(this.DriverContext.ScreenShotFolder, FileType.Png);
        /// </code></example>
        public static FileInfo GetLastFile(string folder, FileType type)
        {
            Logger.Debug(CultureInfo.CurrentCulture, "Get Last File of given type {0}", type);
            var lastFile =
                new DirectoryInfo(folder).GetFiles()
                    .Where(f => f.Extension == ReturnFileExtension(type))
                    .OrderByDescending(f => f.LastWriteTime)
                    .First();
            Logger.Trace("Last File: {0}", lastFile);
            return lastFile;
        }

        /// <summary>
        /// Gets the last file.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>
        /// Last file of given type.
        /// </returns>
        /// <example>How to use it: <code>
        /// FilesHelper.GetLastFile(this.DriverContext.ScreenShotFolder);
        /// </code></example>
        public static FileInfo GetLastFile(string folder)
        {
            Logger.Debug("Get Last File");
            var lastFile = new DirectoryInfo(folder).GetFiles()
                .OrderByDescending(f => f.LastWriteTime)
                .First();
            Logger.Trace("Last File: {0}", lastFile);
            return lastFile;
        }

        /// <summary>
        /// Waits for file of given type for given timeout till number of files increase in sub folder,checks  the size of the current file.
        /// </summary>
        /// <param name="type">The type of file.</param>
        /// <param name="waitTime">Wait timeout.</param>
        /// <param name="filesNumber">The initial files number.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="checkSize">Check if  the size, in bytes, of the current file > 0.</param>
        /// <example>How to use it: <code>
        /// var filesNumber = FilesHelper.CountFiles(this.DriverContext.DownloadFolder, FileType.Txt);
        /// this.Driver.GetElement(this.fileLink.Format("some-file.txt")).Click();
        /// FilesHelper.WaitForFileOfGivenType(FileType.Txt, BaseConfiguration.LongTimeout, filesNumber, this.DriverContext.DownloadFolder);
        /// </code></example>
        public static void WaitForFileOfGivenType(FileType type, double waitTime, int filesNumber, string folder, bool checkSize)
        {
            Logger.Debug("Wait for file: {0}", type);
            var timeoutMessage = string.Format(CultureInfo.CurrentCulture, "Waiting for file number to increase in {0}", folder);
            WaitHelper.Wait(
                () => CountFiles(folder, type) > filesNumber, TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);

            Logger.Debug("Number of files increased");

            if (checkSize)
            {
                Logger.Debug("Checking if size of last file of given type {0} > 0 bytes", type);
                timeoutMessage = string.Format(CultureInfo.CurrentCulture, "Checking if size of last file of given type {0} > 0 bytes", type);

                WaitHelper.Wait(
                    () => GetLastFile(folder, type).Length > 0, TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);
            }
        }

        /// <summary>
        /// Waits for file of given type for LongTimeout till number of files increase in sub folder, checks  the size of the current file.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="filesNumber">The files number.</param>
        /// <param name="folder">The folder.</param>
        /// <example>How to use it: <code>
        /// var filesNumber = FilesHelper.CountFiles(this.DriverContext.DownloadFolder, FileType.Txt);
        /// this.Driver.GetElement(this.fileLink.Format("some-file.txt")).Click();
        /// FilesHelper.WaitForFileOfGivenType(FileType.Txt, filesNumber, this.DriverContext.DownloadFolder);
        /// </code></example>
        public static void WaitForFileOfGivenType(FileType type, int filesNumber, string folder)
        {
            WaitForFileOfGivenType(type, BaseConfiguration.LongTimeout, filesNumber, folder, true);
        }

        /// <summary>
        /// Waits for file with given name with given timeout, checks  the size of the current file.
        /// </summary>
        /// <param name="waitTime">Wait timeout.</param>
        /// <param name="filesName">Name of the files.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="checkSize">If true, check the size, in bytes, of the current file > 0.</param>
        /// <example>How to use it: <code>
        /// var file = "some-file.txt"
        /// this.Driver.GetElement(this.fileLink.Format(file), "Click on file").Click();
        /// FilesHelper.WaitForFileOfGivenName(BaseConfiguration.LongTimeout, file, this.DriverContext.DownloadFolder, true);
        /// </code></example>
        public static void WaitForFileOfGivenName(double waitTime, string filesName, string folder, bool checkSize)
        {
            Logger.Debug(CultureInfo.CurrentCulture, "Wait for file: {0}", filesName);
            var timeoutMessage = string.Format(CultureInfo.CurrentCulture, "Waiting for file {0} in folder {1}", filesName, folder);
            WaitHelper.Wait(
                () => File.Exists(folder + Separator + filesName), TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);

            Logger.Debug("File exists");
            if (checkSize)
            {
                Logger.Debug("Checking if size of last file > 0 bytes");
                timeoutMessage = string.Format(CultureInfo.CurrentCulture, "Checking if size of file {0} > 0 bytes", filesName);
                WaitHelper.Wait(
                    () => GetFileByName(folder, filesName).Length > 0, TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);
            }
        }

        /// <summary>
        /// Waits for file with given name with given timeout, checks  the size of the current file.
        /// </summary>
        /// <param name="waitTime">Wait timeout.</param>
        /// <param name="filesName">Name of the files.</param>
        /// <param name="folder">The folder.</param>
        /// <example>How to use it: <code>
        /// var file = "some-file.txt"
        /// this.Driver.GetElement(this.fileLink.Format(file), "Click on file").Click();
        /// FilesHelper.WaitForFileOfGivenName(BaseConfiguration.LongTimeout, file, this.DriverContext.DownloadFolder);
        /// </code></example>
        public static void WaitForFileOfGivenName(double waitTime, string filesName, string folder)
        {
            WaitForFileOfGivenName(waitTime, filesName, folder, true);
        }

        /// <summary>
        /// Waits for file of given name with LongTimeout, checks  the size of the current file.
        /// </summary>
        /// <param name="filesName">Name of the files.</param>
        /// <param name="folder">The folder.</param>
        /// <example>How to use it: <code>
        /// var file = "some-file.txt"
        /// this.Driver.GetElement(this.fileLink.Format(file), "Click on file").Click();
        /// FilesHelper.WaitForFileOfGivenName(file, this.DriverContext.DownloadFolder);
        /// </code></example>
        public static void WaitForFileOfGivenName(string filesName, string folder)
        {
            WaitForFileOfGivenName(BaseConfiguration.LongTimeout, filesName, folder);
        }

        /// <summary>
        /// Waits for file of given name with LongTimeout, checks  the size of the current file.
        /// </summary>
        /// <param name="filesName">Name of the files.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="checkSize">Check if  the size, in bytes, of the current file > 0.</param>
        /// <example>How to use it: <code>
        /// var file = "some-file.txt"
        /// this.Driver.GetElement(this.fileLink.Format(file), "Click on file").Click();
        /// FilesHelper.WaitForFileOfGivenName(file, this.DriverContext.DownloadFolder, true);
        /// </code></example>
        public static void WaitForFileOfGivenName(string filesName, string folder, bool checkSize)
        {
            WaitForFileOfGivenName(BaseConfiguration.LongTimeout, filesName, folder, checkSize);
        }

        /// <summary>
        /// Waits for file for given timeout till number of files increase in sub folder, checks  the size of the current file.
        /// </summary>
        /// <param name="waitTime">Wait timeout.</param>
        /// <param name="filesNumber">The initial files number.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="checkSize">Check if  the size, in bytes, of the current file > 0.</param>
        /// <example>How to use it:<code>
        /// var filesNumber = FilesHelper.CountFiles(this.DriverContext.DownloadFolder);
        /// this.Driver.GetElement(this.fileLink.Format("some-file.txt")).Click();
        /// FilesHelper.WaitForFile(BaseConfiguration.LongTimeout, filesNumber, this.DriverContext.DownloadFolder, true);
        /// </code></example>
        public static void WaitForFile(double waitTime, int filesNumber, string folder, bool checkSize)
        {
            Logger.Debug("Wait for file");
            var timeoutMessage = string.Format(CultureInfo.CurrentCulture, "Waiting for file number to increase in {0}", folder);
            WaitHelper.Wait(
                () => CountFiles(folder) > filesNumber, TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);

            Logger.Debug("Number of files increased");

            if (checkSize)
            {
                Logger.Debug("checking if size of last file > 0 bytes");
                timeoutMessage = "Checking if size of last file > 0 bytes";

                WaitHelper.Wait(
                    () => GetLastFile(folder).Length > 0, TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);
            }
        }

        /// <summary>
        /// Waits for file with LongTimeout till number of files increase in sub folder, checks  the size of the current file.
        /// </summary>
        /// <param name="filesNumber">The initial files number.</param>
        /// <param name="folder">The folder.</param>
        /// <example>How to use it: <code>
        /// var filesNumber = FilesHelper.CountFiles(this.DriverContext.DownloadFolder);
        /// this.Driver.GetElement(this.fileLink.Format("some-file.txt")).Click();
        /// FilesHelper.WaitForFile(filesNumber, this.DriverContext.DownloadFolder);
        /// </code></example>
        public static void WaitForFile(int filesNumber, string folder)
        {
            WaitForFile(BaseConfiguration.LongTimeout, filesNumber, folder, true);
        }

        /// <summary>
        /// Rename the file and check if file was renamed with given timeout, shorten the name of file if needed be removing "_".
        /// </summary>
        /// <param name="waitTime">Timeout for checking if file was removed.</param>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        /// <param name="subFolder">The subFolder.</param>
        /// <returns>The new name in case its shorten.</returns>
        /// <example>How to use it: <code>
        /// string newName = FilesHelper.RenameFile(BaseConfiguration.ShortTimeout, "filename.txt", "newname.txt", this.DriverContext.DownloadFolder);
        /// </code></example>
        public static string RenameFile(double waitTime, string oldName, string newName, string subFolder)
        {
            Logger.Debug(CultureInfo.CurrentCulture, "new file name: {0}", newName);

            newName = NameHelper.ShortenFileName(subFolder, newName, "_", 255);

            string fullPath = Path.Combine(subFolder, newName);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            CopyFile(waitTime, oldName, newName, subFolder);
            File.Delete(Path.Combine(subFolder, oldName));
            var timeoutMessage = string.Format(CultureInfo.CurrentCulture, "Waiting till file will be renamed {0}", subFolder);

            WaitHelper.Wait(() => File.Exists(subFolder + Separator + newName), TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);
            return newName;
        }

        /// <summary>
        /// Copy the file and check if file was copied with given timeout, shorten the name of file if needed be removing "_".
        /// </summary>
        /// <param name="waitTime">Timeout for checking if file was removed.</param>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        /// <param name="workingDirectory">The working folder.</param>
        /// <returns>The new name in case its shorten.</returns>
        /// <example>How to use it: <code>
        /// FilesHelper.CopyFile(BaseConfiguration.ShortTimeout, fileName, newName, this.DriverContext.DownloadFolder);
        /// </code></example>
        public static string CopyFile(double waitTime, string oldName, string newName, string workingDirectory)
        {
            return CopyFile(waitTime, oldName, newName, workingDirectory, workingDirectory);
        }

        /// <summary>
        /// Copy the file and check if file was copied with given timeout, shorten the name of file if needed be removing "_".
        /// </summary>
        /// <param name="waitTime">Timeout for checking if file was removed.</param>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        /// <param name="oldSubFolder">The old subFolder.</param>
        /// <param name="newSubFolder">The new subFolder.</param>
        /// <returns>The new name in case its shorten.</returns>
        /// <example>How to use it: <code>
        /// FilesHelper.CopyFile(BaseConfiguration.ShortTimeout, fileName, newName, this.DriverContext.DownloadFolder + "\\", this.DriverContext.DownloadFolder + "\\");
        /// </code></example>
        public static string CopyFile(double waitTime, string oldName, string newName, string oldSubFolder, string newSubFolder)
        {
            Logger.Debug(CultureInfo.CurrentCulture, "new file name: {0}\\{1}", newSubFolder, newName);

            newName = NameHelper.ShortenFileName(newSubFolder, newName, "_", 255);

            if (File.Exists(newSubFolder + Separator + newName))
            {
                File.Delete(newSubFolder + Separator + newName);
            }

            Thread.Sleep(1000);
            File.Copy(oldSubFolder + Separator + oldName, newSubFolder + Separator + newName);

            var timeoutMessage = string.Format(CultureInfo.CurrentCulture, "Waiting till file will be copied {0}", newSubFolder);
            WaitHelper.Wait(() => File.Exists(newSubFolder + Separator + newName), TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);
            return newName;
        }

        /// <summary>
        /// Delete file in given folder if exists.
        /// </summary>
        /// <param name="name">The name of file.</param>
        /// <param name="subFolder">The subFolder.</param>
        /// <example>How to use it: <code>
        /// FilesHelper.DeleteFile("filename.txt", this.DriverContext.DownloadFolder);
        /// </code></example>
        public static void DeleteFile(string name, string subFolder)
        {
            string fullPath = Path.Combine(subFolder, name);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        /// <summary>
        /// Rename the file of given type and check if file was renamed with ShortTimeout, shorten the name of file if needed be removing "_".
        /// </summary>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        /// <param name="subFolder">The subFolder.</param>
        /// <param name="type">The type of file.</param>
        /// <returns>The new name in case its shorten.</returns>
        /// <example>How to use it: <code>
        /// string newName = FilesHelper.RenameFile("filename.txt", "newname", this.DriverContext.DownloadFolder, FileType.Csv);
        /// </code></example>
        public static string RenameFile(string oldName, string newName, string subFolder, FileType type)
        {
            newName += ReturnFileExtension(type);
            return RenameFile(BaseConfiguration.ShortTimeout, oldName, newName, subFolder);
        }

        /// <summary>
        /// Gets the folder from app.config as value of given key.
        /// </summary>
        /// <param name="appConfigValue">The application configuration value.</param>
        /// <param name="currentFolder">Directory where assembly files are located.</param>
        /// <returns>
        /// The path to folder.
        /// </returns>
        /// <example>How to use it: <code>
        ///   FilesHelper.GetFolder(BaseConfiguration.DownloadFolder, this.CurrentDirectory);
        /// </code></example>
        public static string GetFolder(string appConfigValue, string currentFolder)
        {
            Logger.Trace(CultureInfo.CurrentCulture, "appConfigValue '{0}', currentFolder '{1}', UseCurrentDirectory '{2}'", appConfigValue, currentFolder, BaseConfiguration.UseCurrentDirectory);
            string folder;

            if (string.IsNullOrEmpty(appConfigValue))
            {
                folder = currentFolder;
            }
            else
            {
                if (BaseConfiguration.UseCurrentDirectory)
                {
                    Regex pattern = new Regex("[\\]|[\\\\]|[/]|[//]");
                    appConfigValue = pattern.Replace(appConfigValue, string.Empty);
                    folder = currentFolder + FilesHelper.Separator + appConfigValue;
                }
                else
                {
                    folder = appConfigValue;
                }

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }

            Logger.Trace(CultureInfo.CurrentCulture, "Returned folder '{0}'", folder);
            return folder;
        }
    }
}