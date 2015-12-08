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

using NUnit.Framework;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.Kendo;
using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;
using OpenQA.Selenium;

namespace Objectivity.Test.Automation.Tests.NUnit.Tests
{
    public class MBahr : ProjectTestBase
    {
        [Test]
        public void KendoOpenCloseComboboxTest()
        {
            var homePage = new KendoComboBoxPage(this.DriverContext);
            homePage.Open();

            homePage.OpenFabricComboBox();           
            Assert.IsTrue(homePage.IsFabricComboBoxExpanded());

            homePage.CloseFabricComboBox();
            Assert.IsFalse(homePage.IsFabricComboBoxExpanded());
        }

        [Test]
        public void KendoOpenCloseDropDownListTest()
        {
            var homePage = new KendoDropDownListPage(this.DriverContext);
            homePage.Open();

            homePage.OpenColorDropDownList();
            Assert.IsTrue(homePage.IsColorDropDownListExpanded());

            homePage.CloseColorDropDownList();
            Assert.IsFalse(homePage.IsColorDropDownListExpanded());
        }

        [Test]
        public void KendoUnorderedDropDownListTest()
        {
            var numberExpected = 3;
            var homePage = new KendoDropDownListPage(this.DriverContext);
            homePage.Open();

            var number = homePage.GetNumberOfOptions(homePage.GetWebElementColorListBox);
            Assert.AreEqual(numberExpected, number);
        }

        [Test]
        public void HoversTest()
        {
            var expected = new string[3] { "name: user1", "name: user2", "name: user3" };

            var homePage = new InternetPage(this.DriverContext)
                .OpenHomePageWithUserCredentials()
                .GoToHoversPage();

            var text1before = homePage.GetHoverText(1);
            LogTest.Info("Text before: '{0}'", text1before);
            homePage.MouseHoverAt(1);
            var text1after = homePage.GetHoverText(1);
            LogTest.Info("Text after: '{0}'", text1after);

            var text2before = homePage.GetHoverText(2);
            LogTest.Info("Text before: '{0}'", text2before);
            homePage.MouseHoverAt(2);
            var text2after = homePage.GetHoverText(2);
            LogTest.Info("Text after: '{0}'", text2after);

            var text3before = homePage.GetHoverText(3);
            LogTest.Info("Text before: '{0}'", text3before);
            homePage.MouseHoverAt(3);
            var text3after = homePage.GetHoverText(3);
            LogTest.Info("Text after: '{0}'", text3after);

            Assert.AreEqual(string.Empty, text1before);
            Assert.AreEqual(string.Empty, text2before);
            Assert.AreEqual(string.Empty, text3before);

            Assert.AreEqual(expected[0], text1after);
            Assert.AreEqual(expected[1], text2after);
            Assert.AreEqual(expected[2], text3after);
        }
    }
}

