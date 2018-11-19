// <copyright file="HerokuappTestsNUnit.cs" company="Objectivity Bespoke Software Specialists">
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
    using OpenQA.Selenium;

    [TestFixture]
    [Category("BasicNUnit")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class HerokuappTestsNUnit : ProjectTestBase
    {
        [Test]
        public void BasicAuthTest()
        {
            var basicAuthPage =
                new InternetPage(this.DriverContext).OpenHomePageWithUserCredentials().GoToBasicAuthPage();

            Verify.That(
                this.DriverContext,
                () =>
                Assert.AreEqual(
                    "Congratulations! You must have the proper credentials.",
                    basicAuthPage.GetCongratulationsInfo));
        }

        [Test]
        public void ForgotPasswordTest()
        {
            new InternetPage(this.DriverContext).OpenHomePage().GoToForgotPasswordPage();

            var forgotPassword = new ForgotPasswordPage(this.DriverContext);

            Verify.That(
                this.DriverContext,
                () => Assert.AreEqual(5 + 7 + 2, forgotPassword.EnterEmail(5, 7, 2)),
                () => Assert.AreEqual("Your e-mail's been sent!", forgotPassword.ClickRetrievePassword));
        }

        [Test]
        public void MultipleWindowsTest()
        {
            const string PageTitle = "New Window";

            var newWindowPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToMultipleWindowsPage()
                .OpenNewWindowPage();

            Assert.True(newWindowPage.IsPageTile(PageTitle), "wrong page title, should be {0}", PageTitle);
            Assert.True(newWindowPage.IsNewWindowH3TextVisible(PageTitle), "text is not equal to {0}", PageTitle);
        }

        [Test]
        public void NestedFramesTest()
        {
            var nestedFramesPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToNestedFramesPage()
                .SwitchToFrame("frame-top");

            nestedFramesPage.SwitchToFrame("frame-left");
            Assert.AreEqual("LEFT", nestedFramesPage.LeftBody);

            nestedFramesPage.SwitchToParentFrame().SwitchToFrame("frame-middle");
            Assert.AreEqual("MIDDLE", nestedFramesPage.MiddleBody);

            nestedFramesPage.SwitchToParentFrame().SwitchToFrame("frame-right");
            Assert.AreEqual("RIGHT", nestedFramesPage.RightBody);

            nestedFramesPage.ReturnToDefaultContent().SwitchToFrame("frame-bottom");
            Assert.AreEqual("BOTTOM", nestedFramesPage.BottomBody);
        }

        [Test]
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

                Assert.AreEqual("You selected a context menu", contextMenuPage.JavaScriptText);
                Assert.True(contextMenuPage.ConfirmJavaScript().IsH3ElementEqualsToExpected(H3Value), "h3 element is not equal to expected {0}", H3Value);
            }
        }

        [Test]
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

            Assert.AreEqual(string.Empty, text1Before);
            Assert.AreEqual(string.Empty, text2Before);
            Assert.AreEqual(string.Empty, text3Before);

            Assert.AreEqual(expected[0], text1After);
            Assert.AreEqual(expected[1], text2After);
            Assert.AreEqual(expected[2], text3After);
        }

        [Test]
        public void SetAttributeTest()
        {
            var internetPage = new InternetPage(this.DriverContext)
                .OpenHomePage();

            internetPage.ChangeBasicAuthLink("/broken_images");
            internetPage.BasicAuthLinkClick();

            var brokenImagesPage = new BrokenImagesPage(this.DriverContext);

            Assert.True(brokenImagesPage.IsPageHeaderElementEqualsToExpected("Broken Images"), "Page header element is not equal to expected 'Broken Images'");
        }

        [Test]
        public void SlowResourcesTest()
        {
            int timeout = 35;
            new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToSlowResources()
                .WaitForIt(timeout);
        }

        [Test]
        public void TablesTest()
        {
            var tableElements = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToTablesPage();
            var table = tableElements.GetTableElements();

            Assert.AreEqual("Smith", table[0][0]);
            Assert.AreEqual("edit delete", table[3][5]);
        }

        [Test]
        public void DragAndDropTest()
        {
            var dragAndDrop = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToDragAndDropPage()
                .MoveElementAtoElementB();

            Assert.IsTrue(dragAndDrop.IsElementAMovedToB(), "Element is not moved.");
        }

        [Test]
        public void DynamicallyLoadedPageElementsTest()
        {
            var page = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToDynamicLoading()
                .ClickOnExample2();

            this.DriverContext.PerformanceMeasures.StartMeasure();
            page.ClickStart();
            Assert.AreEqual(page.Text, "Hello World!");
            this.DriverContext.PerformanceMeasures.StopMeasure(TestContext.CurrentContext.Test.Name + "WaitForTest");
        }

        [Test]
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
