// <copyright file="KendoTreeViewPage.cs" company="Objectivity Bespoke Software Specialists">
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
    using System.Collections.ObjectModel;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Types;
    using Objectivity.Test.Automation.Common.WebElements.Kendo;

    public class KendoTreeViewPage : ProjectPageBase
    {
        /// <summary>
        ///     Locators for elements
        /// </summary>
        private readonly ElementLocator kendoTreeViewLocator = new ElementLocator(Locator.Id, "treeview");

        private readonly Uri url = new Uri("http://demos.telerik.com/kendo-ui/treeview/index");

        public KendoTreeViewPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public KendoTreeView KendoTreeView
        {
            get
            {
                var kendoTreeView = this.Driver.GetElement<KendoTreeView>(this.kendoTreeViewLocator);
                return kendoTreeView;
            }
        }

        public string SelectedOption
        {
            get
            {
                var selectedOption = this.KendoTreeView.SelectedOption;
                return selectedOption != null ? selectedOption.GetTextContent() : string.Empty;
            }
        }

        public KendoTreeViewPage SelectElementByText(string text)
        {
            this.KendoTreeView.SelectByText(text);
            return this;
        }

        public Collection<string> FindElementsByText(string text)
        {
            return this.KendoTreeView.FindByText(text);
        }

        public KendoTreeViewPage CollapseTreeView()
        {
            this.KendoTreeView.Collapse();
            return this;
        }

        public KendoTreeViewPage ExpandTreeView()
        {
            this.KendoTreeView.Expand();
            return this;
        }

        public KendoTreeViewPage Open()
        {
            this.Driver.NavigateTo(this.url);
            return this;
        }
    }
}