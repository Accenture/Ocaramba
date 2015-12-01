﻿/*
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

namespace Objectivity.Test.Automation.MsTest.Tests.Tests.KendoTests
{
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Objectivity.Test.Automation.MsTest.Tests.PageObjects;

    [TestClass]
    public class KendoTreeViewTests : ProjectTestBase
    {
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "png",
                Justification = "png is valid picture file extension")]
        [TestMethod]
        public void KendoTreeViewSelectTest()
        {
            var text = "logo.png";
            var homePage = new KendoTreeViewPage(this.DriverContext);
            homePage.Open().CollapseTreeView().SelectElementByText(text);
            Assert.AreEqual(text, homePage.SelectedOption);
        }

        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "png",
                Justification = "png is valid picture file extension")]
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