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

namespace Ocaramba.Tests.NUnitExtentReports.Tests
{
    using global::NUnit.Framework;
    using Ocaramba;
    using Ocaramba.Tests.NUnitExtentReports;
    using Ocaramba.Tests.NUnitExtentReports.PageObjects;

    [TestFixture]
    [Category("BasicNUnit")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class HerokuappTestsNUnit : ProjectTestBase
    {

        [Test]
        public void BasicAuthTest()
        {
            const string ExpectedCongratulationsInfo = "Congratulations! You must have the proper credentials.";

            var basicAuthPage =
                new InternetPage(this.DriverContext).OpenHomePageWithUserCredentials().GoToBasicAuthPage();

            test.Info("Verifying congratulations info, expected: " + ExpectedCongratulationsInfo);
            Verify.That(
                this.DriverContext,
                () =>
                Assert.AreEqual(
                    ExpectedCongratulationsInfo,
                    basicAuthPage.GetCongratulationsInfo));
        }

        [Test]
        public void MultipleWindowsTest()
        {
            const string PageTitle = "New Window";

            var newWindowPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToMultipleWindowsPage()
                .OpenNewWindowPage();

            test.Info("Verifying page title, expected: " + PageTitle);
            Assert.True(newWindowPage.IsPageTile(PageTitle), "wrong page title, should be {0}", PageTitle);

            test.Info("Verifying H3 header text displayd on te page, expected: " + PageTitle);
            Assert.True(newWindowPage.IsNewWindowH3TextVisible(PageTitle), "text is not equal to {0}", PageTitle);
        }

        [Test]
        public void NestedFramesTest()
        {
            const string ExpectedLeftFrameText = "LEFT";
            const string ExpectedMiddleFrameText = "MIDDLE";
            const string ExpectedRightFrameText = "RIGHT";
            const string ExpectedBottomFrameText = "BOTTOM";

            var nestedFramesPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToNestedFramesPage()
                .SwitchToFrame("frame-top");

            nestedFramesPage.SwitchToFrame("frame-left");

            test.Info("Verifying text displayed in left frame, expected: " + ExpectedLeftFrameText);
            Assert.AreEqual(ExpectedLeftFrameText, nestedFramesPage.LeftBody);

            nestedFramesPage.SwitchToParentFrame().SwitchToFrame("frame-middle");

            test.Info("Verifying text displayed in middle frame, expected: " + ExpectedMiddleFrameText);
            Assert.AreEqual(ExpectedMiddleFrameText, nestedFramesPage.MiddleBody);

            nestedFramesPage.SwitchToParentFrame().SwitchToFrame("frame-right");

            test.Info("Verifying text displayed in right frame, expected: " + ExpectedRightFrameText);
            Assert.AreEqual(ExpectedRightFrameText, nestedFramesPage.RightBody);

            nestedFramesPage.ReturnToDefaultContent().SwitchToFrame("frame-bottom");

            test.Info("Verifying text displayed in bottom frame, expected: " + ExpectedBottomFrameText);
            Assert.AreEqual(ExpectedBottomFrameText, nestedFramesPage.BottomBody);
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
    }
}
