﻿// <copyright file="HerokuappTestsNUnit.cs" company="Accenture">
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
    using Ocaramba;
    using Ocaramba.Tests.PageObjects.PageObjects.TheInternet;
    using OpenQA.Selenium;

    [TestFixture]
    [Category("BasicNUnit")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class HerokuappTestsNUnit : ProjectTestBase
    {
        [Test]
        [Category("Grid")]
        public void BasicAuthTest()
        {
            var basicAuthPage =
                new InternetPage(this.DriverContext).OpenHomePageWithUserCredentials().GoToBasicAuthPage();

            Verify.That(
                this.DriverContext,
                () => Assert.That(basicAuthPage.GetCongratulationsInfo, Is.EqualTo("Congratulations! You must have the proper credentials."))
            );
        }

        [Test]
        [Ignore("ForgotPasswordPage doesn't work")]
        public void ForgotPasswordTest()
        {
            new InternetPage(this.DriverContext).OpenHomePage().GoToForgotPasswordPage();

            var forgotPassword = new ForgotPasswordPage(this.DriverContext);

            Verify.That(
                this.DriverContext,
                () => Assert.That(forgotPassword.EnterEmail(5, 7, 2), Is.EqualTo(5 + 7 + 2)),
                () => Assert.That(forgotPassword.ClickRetrievePassword, Is.EqualTo("Your e-mail's been sent!"))
            );
        }

        [Test]
        public void MultipleWindowsTest()
        {
            const string PageTitle = "New Window";

            var newWindowPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToMultipleWindowsPage()
                .OpenNewWindowPage();

            Assert.That(newWindowPage.IsPageTile(PageTitle), Is.True, "wrong page title, should be {0}", PageTitle);
            Assert.That(newWindowPage.IsNewWindowH3TextVisible(PageTitle), Is.True, "text is not equal to {0}", PageTitle);
        }

        [Test]
        [Category("Grid")]
        public void NestedFramesTest()
        {
            var nestedFramesPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToNestedFramesPage()
                .SwitchToFrame("frame-top");

            nestedFramesPage.SwitchToFrame("frame-left");
            Assert.That(nestedFramesPage.LeftBody, Is.EqualTo("LEFT"));

            nestedFramesPage.SwitchToParentFrame().SwitchToFrame("frame-middle");
            Assert.That(nestedFramesPage.MiddleBody, Is.EqualTo("MIDDLE"));

            nestedFramesPage.SwitchToParentFrame().SwitchToFrame("frame-right");
            Assert.That(nestedFramesPage.RightBody, Is.EqualTo("RIGHT"));

            nestedFramesPage.ReturnToDefaultContent().SwitchToFrame("frame-bottom");
            Assert.That(nestedFramesPage.BottomBody, Is.EqualTo("BOTTOM"));
        }

        [Test]
        [Category("Grid")]
        public void ContextMenuTest()
        {
            const string H3Value = "Context Menu";
            var browser = BaseConfiguration.TestBrowser;
            if (browser.Equals(BrowserType.Firefox))
            {
                var contextMenuPage = new InternetPage(this.DriverContext)
                    .OpenHomePage()
                    .GoToContextMenuPage()
                    .SelectTheInternetOptionFromContextMenu();

                Assert.That(contextMenuPage.JavaScriptText, Is.EqualTo("You selected a context menu"));
                Assert.That(contextMenuPage.ConfirmJavaScript().IsH3ElementEqualsToExpected(H3Value), Is.True, $"h3 element is not equal to expected {H3Value}");

            }
        }

        [Test]
        [Category("Grid")]
        public void HoversTest()
        {
            var expected = new[] { "name: user1", "name: user2", "name: user3" };

            var homePage = new InternetPage(this.DriverContext)
                .OpenHomePageWithUserCredentials()
                .GoToHoversPage();

            var text1Before = homePage.GetHoverText(1);
            this.LogTest.Info("Text before: '{0}'", text1Before);
            homePage.MouseHoverAt(1);
            var text1After = homePage.GetHoverText(1);
            this.LogTest.Info("Text after: '{0}'", text1After);

            var text2Before = homePage.GetHoverText(2);
            this.LogTest.Info("Text before: '{0}'", text2Before);
            homePage.MouseHoverAt(2);
            var text2After = homePage.GetHoverText(2);
            this.LogTest.Info("Text after: '{0}'", text2After);

            var text3Before = homePage.GetHoverText(3);
            this.LogTest.Info("Text before: '{0}'", text3Before);
            homePage.MouseHoverAt(3);
            var text3After = homePage.GetHoverText(3);
            this.LogTest.Info("Text after: '{0}'", text3After);

            Assert.That(text1Before, Is.EqualTo(string.Empty));
            Assert.That(text2Before, Is.EqualTo(string.Empty));
            Assert.That(text3Before, Is.EqualTo(string.Empty));

            Assert.That(text1After, Is.EqualTo(expected[0]));
            Assert.That(text2After, Is.EqualTo(expected[1]));
            Assert.That(text3After, Is.EqualTo(expected[2]));
        }

        [Test]
        [Category("Grid")]
        public void SetAttributeTest()
        {
            var internetPage = new InternetPage(this.DriverContext)
                .OpenHomePage();

            internetPage.ChangeBasicAuthLink("/broken_images");
            internetPage.BasicAuthLinkClick();

            var brokenImagesPage = new BrokenImagesPage(this.DriverContext);

            Assert.That(brokenImagesPage.IsPageHeaderElementEqualsToExpected("Broken Images"), Is.True, "Page header element is not equal to expected 'Broken Images'");
        }

        [Test]
        [Category("Grid")]
        public void SlowResourcesTest()
        {
            int timeout = 35;
            new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToSlowResources()
                .WaitForIt(timeout);
        }

        [Test]
        [Category("Grid")]
        public void TablesTest()
        {
            var tableElements = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToTablesPage();
            var table = tableElements.GetTableElements();

            Assert.That(table[0][0], Is.EqualTo("Smith"));
            Assert.That(table[3][5], Is.EqualTo("edit delete"));
        }

        [Test]
        [Category("Grid")]
        public void DragAndDropTest()
        {
            var dragAndDrop = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToDragAndDropPage()
                .MoveElementAtoElementB();

            Assert.That(dragAndDrop.IsElementAMovedToB(), Is.True, "Element is not moved.");
        }

        [Test]
        [Category("Grid")]
        public void DynamicallyLoadedPageElementsTest()
        {
            var page = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToDynamicLoading()
                .ClickOnExample2();

            this.DriverContext.PerformanceMeasures.StartMeasure();
            page.ClickStart();
            Assert.That("Hello World!", Is.EqualTo(page.Text));
            this.DriverContext.PerformanceMeasures.StopMeasure(TestContext.CurrentContext.Test.Name + "WaitForTest");
        }

        [Test]
        [Category("Grid")]
        public void DynamicallyLoadedPageElementsTimeOutTest()
        {
            var page = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToDynamicLoading()
                .ClickOnExample2();

            page.ClickStart();
            Assert.Throws<WebDriverTimeoutException>(() => page.ShortTimeoutText());
        }
    }
}
