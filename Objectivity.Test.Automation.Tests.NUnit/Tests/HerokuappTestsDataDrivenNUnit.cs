// <copyright file="HerokuappTestsDataDrivenNUnit.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.Tests.NUnit.Tests
{
    using System.Collections.Generic;
    using Automation.Tests.PageObjects.PageObjects.TheInternet;
    using Common;
    using DataDriven;
    using global::NUnit.Framework;

    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class HerokuappTestsDataDrivenNUnit : ProjectTestBase
    {
        [Test]
        [TestCaseSource(typeof(TestData), "Credentials")]
        public void FormAuthenticationPageTest(IDictionary<string, string> parameters)
        {
            new InternetPage(this.DriverContext).OpenHomePage().GoToFormAuthenticationPage();

            var formFormAuthentication = new FormAuthenticationPage(this.DriverContext);
            formFormAuthentication.EnterUserName(parameters["user"]);
            formFormAuthentication.EnterPassword(parameters["password"]);
            formFormAuthentication.LogOn();
            Verify.That(
                this.DriverContext,
                () => Assert.AreEqual(parameters["message"], formFormAuthentication.GetMessage));
        }

        [Test]
        [TestCaseSource(typeof(TestData), "CredentialsExcel")]
        public void FormAuthenticationPageExcelTest(IDictionary<string, string> parameters)
        {
            new InternetPage(this.DriverContext).OpenHomePage().GoToFormAuthenticationPage();

            var formFormAuthentication = new FormAuthenticationPage(this.DriverContext);
            formFormAuthentication.EnterUserName(parameters["user"]);
            formFormAuthentication.EnterPassword(parameters["password"]);
            formFormAuthentication.LogOn();
            Verify.That(
                this.DriverContext,
                () => Assert.AreEqual(parameters["message"], formFormAuthentication.GetMessage));
        }

        [Test]
        [TestCaseSource(typeof(TestData), "CredentialsCSV")]
        public void CSVTest(IDictionary<string, string> parameters)
        {
            new InternetPage(this.DriverContext).OpenHomePage().GoToFormAuthenticationPage();

            var formFormAuthentication = new FormAuthenticationPage(this.DriverContext);
            formFormAuthentication.EnterUserName(parameters["user"]);
            formFormAuthentication.EnterPassword(parameters["password"]);
            formFormAuthentication.LogOn();
            Verify.That(
                this.DriverContext,
                () => Assert.AreEqual(parameters["message"], formFormAuthentication.GetMessage));
        }

        [Test]
        [TestCaseSource(typeof(TestData), "LinksSetTestName")]
        public void CountLinksAndSetTestNameAtShiftingContentTest(IDictionary<string, string> parameters)
        {
            new InternetPage(this.DriverContext).OpenHomePage().GoToShiftingContentPage();

            var links = new ShiftingContentPage(this.DriverContext);
            Verify.That(this.DriverContext, () => Assert.AreEqual(parameters["number"], links.CountLinks()));
        }

        [Test]
        [TestCaseSource(typeof(TestData), "LinksExcel")]
        public void CountLinksAndSetTestNameAtShiftingContentExcelTest(IDictionary<string, string> parameters)
        {
            new InternetPage(this.DriverContext).OpenHomePage().GoToShiftingContentPage();

            var links = new ShiftingContentPage(this.DriverContext);
            Verify.That(this.DriverContext, () => Assert.AreEqual(parameters["number"], links.CountLinksGetElementsBasic()));
        }

        [Test]
        [TestCaseSource(typeof(TestData), "Links")]
        public void CountLinksAtShiftingContentTest(IDictionary<string, string> parameters)
        {
            new InternetPage(this.DriverContext).OpenHomePage().GoToShiftingContentPage();

            var links = new ShiftingContentPage(this.DriverContext);
            Verify.That(this.DriverContext, () => Assert.AreEqual(parameters["number"], links.CountLinks()));
        }
    }
}
