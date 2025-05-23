﻿// <copyright file="JavaScriptAlertsTestsNUnit.cs" company="Accenture">
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

namespace Ocaramba.Tests.NUnit.Tests
{
    using global::NUnit.Framework;
    using Ocaramba.Tests.PageObjects.PageObjects.TheInternet;

    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class JavaScriptAlertsTestsNUnit : ProjectTestBase
    {
        [Test]
        public void ClickJsAlertTest()
        {
            var internetPage = new InternetPage(this.DriverContext).OpenHomePage();
            var jsAlertsPage = internetPage.GoToJavaScriptAlerts();
            jsAlertsPage.OpenJsAlert();
            jsAlertsPage.AcceptAlert();
            Assert.That(jsAlertsPage.ResultText, Is.EqualTo("You successfully clicked an alert"));
        }

        [Test]
        public void AcceptJsConfirmTest()
        {
            var internetPage = new InternetPage(this.DriverContext).OpenHomePage();
            var jsAlertsPage = internetPage.GoToJavaScriptAlerts();
            jsAlertsPage.OpenJsConfirm();
            jsAlertsPage.AcceptAlert();
            Assert.That(jsAlertsPage.ResultText, Is.EqualTo("You clicked: Ok"));

        }

        [Test]
        public void DismissJsConfirmTest()
        {
            var internetPage = new InternetPage(this.DriverContext).OpenHomePage();
            var jsAlertsPage = internetPage.GoToJavaScriptAlerts();
            jsAlertsPage.OpenJsConfirm();
            jsAlertsPage.DismissAlert();
            Assert.That(jsAlertsPage.ResultText, Is.EqualTo("You clicked: Cancel"));
        }

        [Test]
        public void TypeTextAndAcceptJsPromptTest()
        {
            var text = "Sample text";
            var internetPage = new InternetPage(this.DriverContext).OpenHomePage();
            var jsAlertsPage = internetPage.GoToJavaScriptAlerts();
            jsAlertsPage.OpenJsPrompt();
            jsAlertsPage.TypeTextOnAlert(text);
            jsAlertsPage.AcceptAlert();
           Assert.That(jsAlertsPage.ResultText, Is.EqualTo("You entered: " + text));
        }

        [Test]
        public void DismissJsPromptTest()
        {
            var internetPage = new InternetPage(this.DriverContext).OpenHomePage();
            var jsAlertsPage = internetPage.GoToJavaScriptAlerts();
            jsAlertsPage.OpenJsPrompt();
            jsAlertsPage.DismissAlert();
            Assert.That(jsAlertsPage.ResultText, Is.EqualTo("You entered: null"));
        }
    }
}
