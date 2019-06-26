// <copyright file="TestData.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Ocaramba.Tests.NUnit.DataDriven
{
    using System.Collections;
    using System.Globalization;
    using global::NUnit.Framework;
    using Ocaramba.Tests.NUnit.DataDriven;

    /// <summary>
    /// DataDriven methods for NUnit test framework
    /// </summary>
    public static class TestData
    {
        public static IEnumerable Credentials
        {
            get { return DataDrivenHelper.ReadDataDriveFile(ProjectBaseConfiguration.DataDrivenFile, "credential", new[] { "user", "password" }, "credential"); }
        }

        public static IEnumerable CredentialsExcel
        {
            get { return DataDrivenHelper.ReadXlsxDataDriveFile(ProjectBaseConfiguration.DataDrivenFileXlsx, "credential", new[] { "user", "password" }, "credentialExcel"); }
        }

        public static IEnumerable LinksSetTestName
        {
            get { return DataDrivenHelper.ReadDataDriveFile(ProjectBaseConfiguration.DataDrivenFile, "links", new[] { "number" }, "Count_links"); }
        }

        public static IEnumerable Links
        {
            get { return DataDrivenHelper.ReadDataDriveFile(ProjectBaseConfiguration.DataDrivenFile, "links"); }
        }

        public static IEnumerable LinksExcel()
        {
            return DataDrivenHelper.ReadXlsxDataDriveFile(ProjectBaseConfiguration.DataDrivenFileXlsx, "links");
        }

        public static IEnumerable CredentialsCSV()
        {
            var path = TestContext.CurrentContext.TestDirectory;
            path = string.Format(CultureInfo.CurrentCulture, "{0}{1}", path, @"\DataDriven\TestDataCsv.csv");
            return DataDrivenHelper.ReadDataDriveFileCsv(path, new[] { "user", "password" }, "credentialCsv");
        }
    }
}
