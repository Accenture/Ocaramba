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

namespace Objectivity.Test.Automation.MsTests.Tests
{
    using System;
    using System.Globalization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Objectivity.Test.Automation.MsTests.PageObjects;

    /// <summary>
    /// Tests to test framework
    /// </summary>
    [TestClass]
    public class PerformanceTests : ProjectTestBase
    {
        /// <summary>
        /// Before the class.
        /// </summary>
        [ClassInitialize]
        public static void BeforeClass(TestContext testContext)
        {
            StartPerformanceMeasure();
        }

        /// <summary>
        /// After the class.
        /// </summary>
        [ClassCleanup]
        public static void AfterClass()
        {
            StopPerfromanceMeasure();
        }

        [DeploymentItem("Objectivity.Test.Automation.MsTests\\DDT.xml"),
            DeploymentItem("Objectivity.Test.Automation.MsTests\\IEDriverServer.exe"),
            DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\DDT.xml", "Links",
            DataAccessMethod.Sequential), TestMethod]
        public void FindChildElementsTest()
        {
            this.LogTest.Info("I go to HomePage");
            var loginPage = new HomePage(this.DriverContext)
                                 .OpenHomePage();
            var numOfSubLinks = loginPage.CountAllTechnologiesSubLinks();
            this.LogTest.Info("Number of links: {0}", numOfSubLinks);
            Assert.AreEqual(Convert.ToInt16((string)this.TestContext.DataRow["number"], CultureInfo.CurrentCulture), numOfSubLinks);
        }

        [DeploymentItem("Objectivity.Test.Automation.MsTests\\DDT.xml"),
            DeploymentItem("Objectivity.Test.Automation.MsTests\\IEDriverServer.exe"),
            DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\DDT.xml", "WordsToSearch",
            DataAccessMethod.Sequential), TestMethod]
        public void SendKeysAndClickDataDrivenTest()
        {
            var loginPage = new HomePage(this.DriverContext)
                                 .OpenHomePage();

            var searchResultsPage = loginPage.Search((string)this.TestContext.DataRow["word"]);
            var expectedPagetitle = this.TestContext.DataRow["expected_title"].ToString().ToLower(CultureInfo.CurrentCulture);

            Assert.IsTrue(searchResultsPage.IsPageTitle(expectedPagetitle),  "Search results page is not displayed");
        }
    }
}
