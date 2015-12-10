/*
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

using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Common.WebElements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet
{
    public class DropdownPage : ProjectPageBase
    {
        private readonly ElementLocator dropDownLocator = new ElementLocator(
            Locator.Id, "dropdown");

        public DropdownPage(DriverContext driverContext) : base(driverContext)
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

        public void SelectByIndex(int index)
        {
            Select select = this.Driver.GetElement<Select>(this.dropDownLocator, 300);
            select.SelectByIndex(index, 300);
        }

        public void SelectByText(string text)
        {
            Select select = this.Driver.GetElement<Select>(this.dropDownLocator);

            if (select.IsSelectOptionAvailable(text) == false)
            {
                throw new NoSuchElementException("Option with text " +text +" is not present");
            }
            select.SelectByText(text);
        }
    }
}
