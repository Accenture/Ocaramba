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

namespace Objectivity.Test.Automation.NunitTests.Tests
{
    using System.Collections.Generic;
    using System.Globalization;

    using NUnit.Framework;

    using Objectivity.Test.Automation.Common;
   // using Objectivity.Test.Automation.NunitTests.DataDriven;
    using Objectivity.Test.Automation.NunitTests.PageObjects;

    /// <summary>
    /// Tests to test framework
    /// </summary>
    public class SmokeTests : ProjectTestBase
    {
        [Test]
        public void SendKeysAndClickTest()
        {
            var loginPage = new HomePage(this.DriverContext)
                                 .OpenHomePage();

            var searchResultsPage = loginPage.Search("objectivity");
            searchResultsPage.MarkStackOverFlowFilter();

            var expectedPageTitle = searchResultsPage.GetPageTitle("Search Results");
            Assert.IsTrue(searchResultsPage.IsPageTitle(expectedPageTitle), "Search results page is not displayed");
        }

        //[Test]
        //[TestCaseSource(typeof(TestData), "WordsToSearch")]
        //public void SendKeysAndClickDataDrivenSetTest(IDictionary<string, string> parameters)
        //{
        //    var loginPage = new HomePage(this.DriverContext)
        //                         .OpenHomePage();

        //    var searchResultsPage = loginPage.Search(parameters["word"]);
        //    var expectedPageTitle = parameters["expected_title"].ToLower(CultureInfo.CurrentCulture);

        //    Assert.IsTrue(searchResultsPage.IsPageTitle(expectedPageTitle), string.Format(CultureInfo.CurrentCulture, "{0} page is not displayed", expectedPageTitle));
        //}

        //[Test]
        //[TestCaseSource(typeof(TestData), "WordsToSearchSetTestName")]
        //public void SendKeysAndClickDataDrivenSetNameOfTest(IDictionary<string, string> parameters)
        //{
        //    var loginPage = new HomePage(this.DriverContext)
        //                         .OpenHomePage();

        //    var searchResultsPage = loginPage.Search(parameters["word"]);
        //    var expectedPageTitle = parameters["expected_title"].ToLower(CultureInfo.CurrentCulture);

        //    Assert.IsTrue(searchResultsPage.IsPageTitle(expectedPageTitle), string.Format(CultureInfo.CurrentCulture, "{0} page is not displayed", expectedPageTitle));
        //}

        [Test]
        public void EvaluateLocatorTest()
        {
            var loginPage = new HomePage(this.DriverContext)
                                 .OpenHomePage();

            Assert.AreEqual("Technologies", loginPage.GetLinkText("Technologies", "Technologies"));
        }

        [Test]
        public void FindElementsTest()
        {
            var loginPage = new HomePage(this.DriverContext)
                                 .OpenHomePage();

            Assert.AreEqual(6, loginPage.CountAllLinks());
        }

        [Test]
        public void FindChildElementsTest()
        {
            var loginPage = new HomePage(this.DriverContext)
                                 .OpenHomePage();

            Assert.AreEqual(5, loginPage.CountAllTechnologiesSubLinks());
        }

        [Test]
        public void FindChildElementTest()
        {
            var loginPage = new HomePage(this.DriverContext).OpenHomePage();

            var technologiesBusinessPage = loginPage.ClickTechnologiesWebLink();

            var expectedPageTitle = technologiesBusinessPage.GetPageTitle("Technologies Web");
            Assert.IsTrue(technologiesBusinessPage.IsPageTitle(expectedPageTitle), "Technologies Web page is not displayed");
        }

        [Test]
        public void JavaScriptClickTest()
        {
            var loginPage = new HomePage(this.DriverContext).OpenHomePage();

            var technologiesBusinessPage = loginPage.JavaScriptClickTechnologiesWebLink();

            var expectedPageTitle = technologiesBusinessPage.GetPageTitle("Technologies Web");
            Assert.IsTrue(technologiesBusinessPage.IsPageTitle(expectedPageTitle), "Technologies Web page is not displayed");
        }

        //[Test]
        //public void VerifyTest()
        //{
        //    Verify.That(this.DriverContext, () => Assert.AreEqual(1, 1), () => Assert.AreEqual(2, 2), () => Assert.AreEqual(3, 3));
        //    Verify.That(this.DriverContext, () => Assert.IsFalse(false), enableScreenShot:true);
        //    Verify.That(this.DriverContext, () => Assert.IsTrue(true));
        //}

        [Test]
        public void ElementNotPresent()
        {
            var homepage = new HomePage(this.DriverContext)
                                 .OpenHomePage();

            Assert.IsFalse(homepage.CheckIfObjectivityLinkNotExistsonMsdnPage());
        }
    }
}
