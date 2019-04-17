﻿// <copyright file="KendoTreeViewTests.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Ocaramba.Tests.MsTest.Tests.KendoTests
{
    using System.Collections.ObjectModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ocaramba.Tests.PageObjects.PageObjects.Kendo;

    [TestClass]
    public class KendoTreeViewTests : ProjectTestBase
    {
        [TestMethod]
        public void KendoTreeViewSelectTest()
        {
            var text = "logo.png";
            var homePage = new KendoTreeViewPage(this.DriverContext);
            homePage.Open().CollapseTreeView().SelectElementByText(text);
            Assert.AreEqual(text, homePage.SelectedOption);
        }

        [TestMethod]
        public void KendoTreeViewFindElementsByTextTest()
        {
            var text = "logo.png";
            var elements = new Collection<string> { "logo.png" };
            var homePage = new KendoTreeViewPage(this.DriverContext);
            homePage.Open();
            CollectionAssert.AreEqual(elements, homePage.FindElementsByText(text));
        }
    }
}