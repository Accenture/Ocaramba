// <copyright file="KendoGridPage.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.Kendo
{
    using System;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Types;
    using Objectivity.Test.Automation.Common.WebElements.Kendo;

    public class KendoGridPage : ProjectPageBase
    {
        /// <summary>
        ///     Locators for elements
        /// </summary>
        private readonly ElementLocator kendoGridLocator = new ElementLocator(Locator.Id, "grid");

        private readonly Uri url = new Uri("http://demos.telerik.com/kendo-ui/grid/index");

        public KendoGridPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public long GridTotalPages
        {
            get
            {
                var pages = this.Grid.TotalPages;
                return pages;
            }
        }

        public long GridPage
        {
            get
            {
                var page = this.Grid.Page;
                return page;
            }
        }

        protected KendoGrid Grid
        {
            get
            {
                var grid = this.Driver.GetElement<KendoGrid>(this.kendoGridLocator);
                return grid;
            }
        }

        public KendoGridPage GoToGridPage(int page)
        {
            this.Grid.SetPage(page);
            return this;
        }

        public string SearchRowWithText(string text)
        {
            var rowText = this.Grid.SearchRowWithText(text, BaseConfiguration.ShortTimeout).Text;
            return rowText;
        }

        public KendoGridPage Open()
        {
            this.Driver.NavigateTo(this.url);
            return this;
        }
    }
}