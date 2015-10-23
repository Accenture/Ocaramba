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

namespace Objectivity.Test.Automation.Common.Helpers.NUnit
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Xml.Linq;

    using global::NUnit.Framework;

    using Objectivity.Test.Automation.Common;

    using OpenQA.Selenium;

    /// <summary>
    /// DataDriven methods for NUnit test framework
    /// </summary>
    public class DataDrivenHelper
    {
        private static readonly string Path = BaseConfiguration.DataDrivenFile;

        /// <summary>
        /// Reads the data drive file and set test name.
        /// </summary>
        /// <param name="testData">Name of the child element in xml file.</param>
        /// <param name="diffParam">The difference parameter, will be used in test case name.</param>
        /// <param name="testName">Name of the test.</param>
        /// <returns></returns>
        protected IEnumerable<TestCaseData> ReadDataDriveFile(string testData, string[] diffParam, [Optional] string testName)
        {
            var doc = XDocument.Load(Path);

            if (!doc.Descendants(testData).Any())
            {
                throw new Exception(string.Format(" Exception while reading Data Driven file\n row '{0}' not found \n in file '{1}'", testData, Path));
            }

            foreach (XElement element in doc.Descendants(testData))
            {
                var testParams = element.Attributes().ToDictionary(k => k.Name.ToString(), v => v.Value);

                var testCaseName = string.IsNullOrEmpty(testName) ? testData : testName;
                if (diffParam != null && diffParam.Any())
                {
                    foreach (var p in diffParam)
                    {
                        string keyValue;
                        bool keyFlag = testParams.TryGetValue(p, out keyValue);

                        if (keyFlag)
                        {
                            if (!string.IsNullOrEmpty(keyValue))
                            {
                                testCaseName += "_" + keyValue.Replace(" ", "_").Replace(".", "_");
                            }
                        }
                        else
                        {
                            throw new Exception(string.Format(" Exception while reading Data Driven file\n test data '{0}' \n test name '{1}' \n searched key '{2}' not found in row \n '{3}'  \n in file '{4}'", testData, testName, p, element, Path));
                        }
                    }
                }

                var data = new TestCaseData(testParams);
                data.SetName(testCaseName);
                yield return data;
            }
        }

        /// <summary>
        /// Reads the data drive file without setting test name.
        /// </summary>
        /// <param name="testData">The test data.</param>
        /// <returns></returns>
        protected IEnumerable<TestCaseData> ReadDataDriveFile(string testData)
        {
            var doc = XDocument.Load(Path);
            if (!doc.Descendants(testData).Any())
            {
                throw new KeyNotFoundException(string.Format(CultureInfo.CurrentCulture, "Exception while reading Data Driven file\n row '{0}' not found \n in file '{1}'", testData, Path));
            }

            return doc.Descendants(testData).Select(element => element.Attributes().ToDictionary(k => k.Name.ToString(), v => v.Value)).Select(testParams => new TestCaseData(testParams));
        }
    }
}
