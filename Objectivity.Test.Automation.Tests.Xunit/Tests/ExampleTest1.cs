// <copyright file="ExampleTest1.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.Tests.Xunit.Tests
{
    using Common;
    using global::Xunit;
    using PageObjects.PageObjects.TheInternet;

    public class ExampleTest1 : ProjectTestBase
    {
        [Fact]
        public void TestMethod1()
        {
            const string PageTitle = "New Window";

            var newWindowPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToMultipleWindowsPage()
                .OpenNewWindowPage();

            Assert.True(newWindowPage.IsPageTile(PageTitle));
            Assert.True(newWindowPage.IsNewWindowH3TextVisible(PageTitle));
        }

        [Fact]
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

                Assert.Equal("You selected a context menu", contextMenuPage.JavaScriptText);
                Assert.True(contextMenuPage
                    .ConfirmJavaScript()
                    .IsH3ElementEqualsToExpected(H3Value));
            }
        }
    }
}
