// <copyright file="HerokuappTestsNUnit.cs" company="Accenture">
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

using System;

namespace Ocaramba.Tests.BrowserStack.Tests
{
    using NUnit.Framework;
    using Ocaramba.Tests.PageObjects.PageObjects.TheInternet;


    [Parallelizable(ParallelScope.Fixtures)]
    public class HerokuappTestsNUnit : ProjectTestBase
    {
        public HerokuappTestsNUnit()
            : base()
        {
        }

        [Test]
        public void TablesTest()
        {
            var tableElements = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToTablesPage();
            var table = tableElements.GetTableElements();

            Assert.That(table[0][0], Is.EqualTo("Smith"));
            Assert.That(table[3][5].Trim().Replace("\r", string.Empty).Replace("         ", string.Empty).Replace("\n", string.Empty), Is.EqualTo("edit delete"));
        }

        [Test]
        public void DynamicallyLoadedPageElementsTest()
        {
            var page = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToDynamicLoading()
                .ClickOnExample2();

            page.ClickStart();
            Assert.That(page.Text, Is.EqualTo("Hello World!"));
        }
    }
}
