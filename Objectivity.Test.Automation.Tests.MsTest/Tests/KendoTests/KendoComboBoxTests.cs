// <copyright file="KendoComboBoxTests.cs" company="Objectivity Bespoke Software Specialists">
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
    public class KendoComboBoxTests : ProjectTestBase
    {
        [TestMethod]
        public void KendoComboBoxOptionsTest()
        {
            var options = new Collection<string> { "Cotton", "Polyester", "Cotton/Polyester", "Rib Knit" };
            var homePage = new KendoComboBoxPage(this.DriverContext);
            homePage.Open();
            CollectionAssert.AreEqual(options, homePage.FabricOptions(options.Count));
        }

        [TestMethod]
        public void KendoComboBoxSearchOptionsTest()
        {
            var searchString = "cotton";
            var options = new Collection<string> { "Cotton", "Cotton/Polyester" };
            var homePage = new KendoComboBoxPage(this.DriverContext);
            homePage.Open();
            homePage.SearchFabricOptions(searchString);
            CollectionAssert.AreEqual(options, homePage.FabricOptions(options.Count));
        }
    }
}