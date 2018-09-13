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

namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet
{
    using Common;
    using Common.Extensions;
    using Common.Types;
    using Common.WebElements;
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

        public string SelectedText
        {
            get
            {
                var element = this.Driver.GetElement(this.dropDownLocator);
                var select = new SelectElement(element);
                return select.SelectedOption.Text;
            }
        }

        public bool IsOptionWithTextPresent(string optionText)
        {
            var isPresent = false;
            var element = this.Driver.GetElement(this.dropDownLocator);
            var select = new SelectElement(element);
            foreach (var option in select.Options)
            {
                if (optionText.Equals(option.Text))
                {
                    isPresent = true;
                }
            }

            return isPresent;
        }

        public void SelectByIndexWithCustomTimeout(int index, int timeout)
        {
            Select select = this.Driver.GetElement<Select>(this.dropDownLocator, timeout);
            select.SelectByIndex(index, timeout);
        }

        public void SelectByIndex(int index)
        {
            Select select = this.Driver.GetElement<Select>(this.dropDownLocator, 300);
            select.SelectByIndex(index);
        }

        public void SelectByIndex(int index, int timeout)
        {
            Select select = this.Driver.GetElement<Select>(this.dropDownLocator, 300);
            select.SelectByIndex(index, timeout);
        }

        public void SelectByValue(string value)
        {
            Select select = this.Driver.GetElement<Select>(this.dropDownLocator, 300);
            select.SelectByValue(value);
        }

        public void SelectByValueWithCustomTimeout(string value, int timeout)
        {
            Select select = this.Driver.GetElement<Select>(this.dropDownLocator, 300);
            select.SelectByValue(value, timeout);
        }

        public void SelectByText(string text)
        {
            Select select = this.Driver.GetElement<Select>(this.dropDownLocator);

            if (select.IsSelectOptionAvailable(text) == false)
            {
                throw new NoSuchElementException("Option with text " + text + " is not present");
            }

            select.SelectByText(text);
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
