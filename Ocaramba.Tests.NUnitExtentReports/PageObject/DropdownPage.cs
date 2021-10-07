// <copyright file="DropdownPage.cs" company="Objectivity Bespoke Software Specialists">
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
    using Ocaramba.Tests.PageObjects;
    using Ocaramba.Types;
    using Ocaramba.WebElements;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    public class DropdownPage : ProjectPageBase
    {
        private readonly ElementLocator dropDownLocator = new ElementLocator(
            Locator.Id, "dropdown");

        public DropdownPage(DriverContext driverContext)
            : base(driverContext)
        {
        }
        
        public void SelectByIndex(int index)
        {
            Select select = this.Driver.GetElement<Select>(this.dropDownLocator, 300);
            select.SelectByIndex(index);
        }

        public void SelectByValue(string value)
        {
            Select select = this.Driver.GetElement<Select>(this.dropDownLocator, 300);
            select.SelectByValue(value);
        }

        public void SelectByText(string text, int timeout)
        {
            Select select = this.Driver.GetElement<Select>(this.dropDownLocator);

            select.SelectByText(text, timeout);
        }

        public string SelectedOption()
        {
            Select select = this.Driver.GetElement<Select>(this.dropDownLocator);

            return select.SelectElement().SelectedOption.Text;
        }
    }
}
