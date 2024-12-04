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
                    Assert.That(basicAuthPage.GetCongratulationsInfo, Is.EqualTo(ExpectedCongratulationsInfo)));
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
            Assert.That(newWindowPage.IsPageTile(PageTitle), Is.True, "wrong page title, should be {0}", PageTitle);

            test.Info("Verifying H3 header text displayd on te page, expected: " + PageTitle);
            Assert.That(newWindowPage.IsNewWindowH3TextVisible(PageTitle), Is.True, "text is not equal to {0}", PageTitle);
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
            Assert.That(nestedFramesPage.LeftBody, Is.EqualTo(ExpectedLeftFrameText));

            nestedFramesPage.SwitchToParentFrame().SwitchToFrame("frame-middle");

            test.Info("Verifying text displayed in middle frame, expected: " + ExpectedMiddleFrameText);
            Assert.That(nestedFramesPage.MiddleBody, Is.EqualTo(ExpectedMiddleFrameText));

            nestedFramesPage.SwitchToParentFrame().SwitchToFrame("frame-right");

            test.Info("Verifying text displayed in right frame, expected: " + ExpectedRightFrameText);
            Assert.That(nestedFramesPage.RightBody, Is.EqualTo(ExpectedRightFrameText));

            nestedFramesPage.ReturnToDefaultContent().SwitchToFrame("frame-bottom");

            test.Info("Verifying text displayed in bottom frame, expected: " + ExpectedBottomFrameText);
            Assert.That(nestedFramesPage.BottomBody, Is.EqualTo(ExpectedBottomFrameText));
        }

        [Test]
        public void SetAttributeTest()
        {
            const string PageHeader = "Broken Images";

            var internetPage = new InternetPage(this.DriverContext)
                .OpenHomePage();

            internetPage.ChangeBasicAuthLink("/broken_images");
            internetPage.BasicAuthLinkClick();

            var brokenImagesPage = new BrokenImagesPage(this.DriverContext);

            test.Info("Verifying page header, expected: " + PageHeader);
            Assert.That(brokenImagesPage.IsPageHeaderElementEqualsToExpected(PageHeader), 
                Is.True, "Page header element is not equal to expected " + PageHeader);
        }

        [Test]
        public void TablesTest()
        {
            const string ExpectedSurname = "Smith";
            const string ExpectedActionLinks = "edit delete";

            var tableElements = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToTablesPage();
            var table = tableElements.GetTableElements();

            test.Info("Verifying surname displayed in the table, expected: " + ExpectedSurname);
            Assert.That(table[0][0], Is.EqualTo(ExpectedSurname));

            test.Info("Verifying action links displayed in the table, expected: " + ExpectedActionLinks);
            Assert.That(table[3][5], Is.EqualTo(ExpectedActionLinks));
        }

        [Test]
        public void DragAndDropTest()
        {
            var dragAndDrop = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToDragAndDropPage()
                .MoveElementAtoElementB();

            test.Info("Verifying element A was moved to element B");
            Assert.That(dragAndDrop.IsElementAMovedToB(), Is.True, "Element is not moved.");
        }

        [Test]
        public void ReportDemoFailingTest()
        {
            const string ExpectedLeftFrameText = "LEFT";
            const string ExpectedMiddleFrameText = "CENTER";

            var nestedFramesPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToNestedFramesPage()
                .SwitchToFrame("frame-top");

            nestedFramesPage.SwitchToFrame("frame-left");

            test.Info("Verifying text displayed in left frame, expected: " + ExpectedLeftFrameText);
            Assert.That(nestedFramesPage.LeftBody, Is.EqualTo(ExpectedLeftFrameText));

            nestedFramesPage.SwitchToParentFrame().SwitchToFrame("frame-middle");

            test.Info("Verifying text displayed in middle frame, expected: " + ExpectedMiddleFrameText);
            Assert.That(nestedFramesPage.MiddleBody, Is.EqualTo(ExpectedMiddleFrameText));
        }
    }
}
