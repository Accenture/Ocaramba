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

namespace Objectivity.Test.Automation.Tests.NUnit.DataDriven
{
    using System.Collections;
    using System.IO;
    using System.Reflection;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Tests.NUnit.Helpers;

    /// <summary>
    /// DataDriven methods for NUnit test framework
    /// </summary>
    public static class TestData 
    {
        /// <summary>
        /// Get current folder of Assembly
        /// </summary>
        /// <returns>Path to Folder</returns>
        public static string GetFolder
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + BaseConfiguration.DataDrivenFile;
            }

        }

        public static IEnumerable Credentials
        {
            get { return DataDrivenHelper.ReadDataDriveFile(GetFolder, "credential", new[] { "user", "password" }, "credential"); }
        }
    }
}
