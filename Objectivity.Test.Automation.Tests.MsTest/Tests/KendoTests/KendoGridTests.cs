// <copyright file="KendoGridTests.cs" company="Objectivity Bespoke Software Specialists">
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
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.Kendo;

    [TestClass]
    public class KendoGridTests : ProjectTestBase
    {
        [TestMethod]
        public void KendoGridTotalPagesTest()
        {
            var totalPages = 5;
            var homePage = new KendoGridPage(this.DriverContext);
            homePage.Open();
            Assert.AreEqual(totalPages, homePage.GridTotalPages);
        }

        [TestMethod]
        public void KendoGridPageTest()
        {
            var currentPage = 1;
            var homePage = new KendoGridPage(this.DriverContext);
            homePage.Open();
            Assert.AreEqual(currentPage, homePage.GridPage);
        }

        [TestMethod]
        public void KendoGridSetPageTest()
        {
            var page = 3;
            var homePage = new KendoGridPage(this.DriverContext);
            homePage.Open().GoToGridPage(page);
            Assert.AreEqual(3, homePage.GridPage);
        }

        [TestMethod]
        public void KendoGridSearchRowWithTextTest()
        {
            var text = "Maurizio Moroni";
            var rowText = "Maurizio Moroni Sales Associate Reggiani Caseifici Italy";
            var homePage = new KendoGridPage(this.DriverContext);
            homePage.Open();
            Assert.AreEqual(rowText, homePage.SearchRowWithText(text));
        }
    }
}