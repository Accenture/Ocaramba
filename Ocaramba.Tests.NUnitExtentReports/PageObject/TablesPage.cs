﻿// <copyright file="TablesPage.cs" company="Accenture">
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

namespace Ocaramba.Tests.NUnitExtentReports.PageObjects
{
    using Ocaramba;
    using Ocaramba.Extensions;
    using Ocaramba.Tests.NUnitExtentReports.ExtentLogger;
    using Ocaramba.Tests.PageObjects;
    using Ocaramba.Types;
    using Ocaramba.WebElements;

    public class TablesPage : ProjectPageBase
    {
        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator
            tableLocator = new ElementLocator(Locator.ClassName, "tablesorter"),
            column = new ElementLocator(Locator.CssSelector, "tr td"),
            row = new ElementLocator(Locator.CssSelector, "tbody tr"),
            tagNameLocator = new ElementLocator(Locator.TagName, "th"),
            xPathLocator = new ElementLocator(Locator.XPath, "//span");

        public TablesPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public string[][] GetTableElements()
        {
            ExtentTestLogger.Info("TablesPage: Getting table elements");
            return this.Driver.GetElement<Table>(this.tableLocator).GetTable(this.row, this.column);
        }
    }
}
