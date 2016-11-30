// <copyright file="ExampleTest2.cs" company="Objectivity Bespoke Software Specialists">
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
    using global::Xunit;
    using PageObjects.PageObjects.Kendo;
    using PageObjects.PageObjects.TheInternet;

    public class ExampleTest2 : ProjectTestBase
    {
        [Fact]
        public void NestedFramesTest()
        {
            var nestedFramesPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToNestedFramesPage()
                .SwitchToFrame("frame-top");

            nestedFramesPage.SwitchToFrame("frame-left");
            Assert.Equal("LEFT", nestedFramesPage.LeftBody);

            nestedFramesPage.SwitchToParentFrame().SwitchToFrame("frame-middle");
            Assert.Equal("MIDDLE", nestedFramesPage.MiddleBody);

            nestedFramesPage.SwitchToParentFrame().SwitchToFrame("frame-right");
            Assert.Equal("RIGHT", nestedFramesPage.RightBody);

            nestedFramesPage.ReturnToDefaultContent().SwitchToFrame("frame-bottom");
            Assert.Equal("BOTTOM", nestedFramesPage.BottomBody);
        }

        [Fact]
        public void KendoOpenCloseComboboxTest()
        {
            var homePage = new KendoComboBoxPage(this.DriverContext);
            homePage.Open();

            homePage.OpenFabricComboBox();
            Assert.True(homePage.IsFabricComboBoxExpanded());

            homePage.CloseFabricComboBox();
            Assert.False(homePage.IsFabricComboBoxExpanded());
        }
    }
}
