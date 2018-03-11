// <copyright file="KendoDropDownListTests.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.Tests.MsTest.Tests.KendoTests
{
    using System.Collections.ObjectModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.Kendo;

    [TestClass]
    public class KendoDropDownListTests : ProjectTestBase
    {
        [TestMethod]
        public void KendoDropDownListOptionsTest()
        {
            var options = new Collection<string> { "Black", "Orange", "Grey" };
            var homePage = new KendoDropDownListPage(this.DriverContext);
            homePage.Open();
            CollectionAssert.AreEqual(options, homePage.CapColorOptions);
        }

        [TestMethod]
        public void KendoDropDownSizeListOptionsTest()
        {
            var options = new Collection<string> { "S - 6 3/4\"", "M - 7 1/4\"", "L - 7 1/8\"", "XL - 7 5/8\"" };
            var homePage = new KendoDropDownListPage(this.DriverContext);
            homePage.Open();
            CollectionAssert.AreEqual(options, homePage.CapSizeOptions);
        }

        [TestMethod]
        public void KendoDropDownListSelectedOptionTest()
        {
            var option = "Black";
            var homePage = new KendoDropDownListPage(this.DriverContext);
            homePage.Open();
            Assert.AreEqual(option, homePage.CapColorSelectedOption);
        }

        [TestMethod]
        public void KendoDropDownListSelectByTextTest()
        {
            var option = "Orange";
            var homePage = new KendoDropDownListPage(this.DriverContext);
            homePage.Open();
            homePage.SelectCapColor(option);
            Assert.AreEqual(option, homePage.CapColorSelectedOption);
        }

        [TestMethod]
        public void KendoDropDownSizeListSelectByTextTest()
        {
            var option = "XL - 7 5/8\"";
            var homePage = new KendoDropDownListPage(this.DriverContext);
            homePage.Open();
            homePage.SelectCapSize(option);
            Assert.AreEqual(option, homePage.CapSizeSelectedOption);
        }
    }
}