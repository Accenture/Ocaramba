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

using System.Collections.Generic;

namespace Objectivity.Test.Automation.NunitTests.Tests
{
    using System.Globalization;

    using NUnit.Framework;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.NunitTests.DataDriven;
    using Objectivity.Test.Automation.NunitTests.PageObjects;

    /// <summary>
    /// Tests to test framework
    /// </summary>
    public class SmokeTests : ProjectTestBase
    {
        [Test]
        public void SendKeysAndClickTest()
        {
            var loginPage = Pages.Create<HomePage>()
                                 .OpenHomePage();

            var searchResultsPage = loginPage.Search("objectivity");
            searchResultsPage.MarkStackOverFlowFilter();

            var pageTitle = searchResultsPage.GetPageTitle("Search Results");
            Assert.AreEqual(pageTitle["expected"], pageTitle["actual"], "Search results page is not displayed");
        }

        [Test]
        [TestCaseSource(typeof(TestData), "WordsToSearch")]
        public void SendKeysAndClickDataDrivenSetTest(IDictionary<string, string> parameters)
        {
            var loginPage = Pages.Create<HomePage>()
                                 .OpenHomePage();

            var searchResultsPage = loginPage.Search(parameters["word"]);

            Assert.AreEqual(parameters["expected_title"].ToLower(CultureInfo.CurrentCulture), searchResultsPage.GetPageTitleDataDriven(parameters["word"]), "Search results page is not displayed");
        }

        [Test]
        [TestCaseSource(typeof(TestData), "WordsToSearchSetTestName")]
        public void SendKeysAndClickDataDrivenSetNameOfTest(IDictionary<string, string> parameters)
        {
            var loginPage = Pages.Create<HomePage>()
                                 .OpenHomePage();

            var searchResultsPage = loginPage.Search(parameters["word"]);

            Assert.AreEqual(parameters["expected_title"].ToLower(CultureInfo.CurrentCulture), searchResultsPage.GetPageTitleDataDriven(parameters["expected_title"]), "Search results page is not displayed");
        }

        [Test]
        public void EvaluateLocatorTest()
        {
            var loginPage = Pages.Create<HomePage>()
                                 .OpenHomePage();

            Assert.AreEqual("Technologies", loginPage.GetLinkText("Technologies", "Technologies"));
        }

        [Test]
        public void FindElementsTest()
        {
            var loginPage = Pages.Create<HomePage>()
                                 .OpenHomePage();

            Assert.AreEqual(6, loginPage.CountAllLinks());
        }

        [Test]
        public void FindChildElementsTest()
        {
            var loginPage = Pages.Create<HomePage>()
                                 .OpenHomePage();

            Assert.AreEqual(5, loginPage.CountAllTechnologiesSubLinks());
        }

        [Test]
        public void FindChildElementTest()
        {
            var loginPage = Pages.Create<HomePage>()
                                 .OpenHomePage();

            var technologiesBusinessPage = loginPage.ClickTechnologiesWebLink();
            var pageTitle = technologiesBusinessPage.GetPageTitle("Technologies Web");
            Assert.AreEqual(pageTitle["expected"], pageTitle["actual"], "Wrong title of the page");
        }

        [Test]
        public void JavaScriptClickTest()
        {
            var loginPage = Pages.Create<HomePage>()
                                 .OpenHomePage();

            var technologiesBusinessPage = loginPage.JavaScriptClickTechnologiesWebLink();

            var pageTitle = technologiesBusinessPage.GetPageTitle("Technologies Web");
            Assert.AreEqual(pageTitle["expected"], pageTitle["actual"], "Wrong title of the page");
        }

        [Test]
        public void VerifyTest()
        {
            this.Verify(() => Assert.AreEqual(1, 1));
            this.Verify(() => Assert.IsTrue(true));
        }
    }
}
