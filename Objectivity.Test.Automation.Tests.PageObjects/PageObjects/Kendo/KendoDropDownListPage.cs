// <copyright file="KendoDropDownListPage.cs" company="Objectivity Bespoke Software Specialists">
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
    using OpenQA.Selenium;

    public class KendoDropDownListPage : ProjectPageBase
    {
        /// <summary>
        ///     Locators for elements
        /// </summary>
        private readonly ElementLocator
            capColorKendoDropDownListLocator = new ElementLocator(Locator.CssSelector, "span[aria-owns='color_listbox']"),
            capSizeKendoDropDownListLocator = new ElementLocator(Locator.CssSelector, "span[aria-owns='size_listbox']");

        private readonly Uri url = new Uri("http://demos.telerik.com/jsp-ui/dropdownlist/index");

        public KendoDropDownListPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public Collection<string> CapColorOptions
        {
            get
            {
                var options = this.CapColorKendoDropDownList.Options;
                return options;
            }
        }

        public Collection<string> CapSizeOptions
        {
            get
            {
                var options = this.CapSizeKendoDropDownList.Options;
                return options;
            }
        }

        public string CapColorSelectedOption
        {
            get
            {
                var option = this.CapColorKendoDropDownList.SelectedOption;
                return option;
            }
        }

        public string CapSizeSelectedOption
        {
            get
            {
                var option = this.CapSizeKendoDropDownList.SelectedOption;
                return option;
            }
        }

        public IWebElement GetWebElementColorListBox
        {
            get
            {
                return this.CapColorKendoDropDownList.UnorderedList;
            }
        }

        protected KendoDropDownList CapColorKendoDropDownList
        {
            get
            {
                var element = this.Driver.GetElement<KendoDropDownList>(this.capColorKendoDropDownListLocator);
                return element;
            }
        }

        protected KendoDropDownList CapSizeKendoDropDownList
        {
            get
            {
                var element = this.Driver.GetElement<KendoDropDownList>(this.capSizeKendoDropDownListLocator);
                return element;
            }
        }

        public void SelectCapColor(string text)
        {
            this.CapColorKendoDropDownList.SelectByText(text);
        }

        public void SelectCapSize(string text)
        {
            this.CapSizeKendoDropDownList.SelectByText(text);
        }

        public KendoDropDownListPage Open()
        {
            this.Driver.NavigateTo(this.url);
            return this;
        }

        public void OpenColorDropDownList()
        {
            this.CapColorKendoDropDownList.Open();
        }

        public void CloseColorDropDownList()
        {
            this.CapColorKendoDropDownList.Close();
        }

        public bool IsColorDropDownListExpanded()
        {
            var element = this.Driver.GetElement(this.capColorKendoDropDownListLocator);
            var attribute = element.GetAttribute("aria-expanded");
            return bool.Parse(attribute);
        }

        public int GetNumberOfOptions(IWebElement webElement)
        {
            return webElement.FindElements(By.TagName("li")).Count;
        }
    }
}