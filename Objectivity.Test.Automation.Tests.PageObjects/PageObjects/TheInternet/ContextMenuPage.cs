// <copyright file="ContextMenuPage.cs" company="Objectivity Bespoke Software Specialists">
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
    using OpenQA.Selenium.Interactions;

    public class ContextMenuPage : ProjectPageBase
    {
        private readonly ElementLocator
            hotSpot = new ElementLocator(Locator.CssSelector, "#hot-spot"),
            h3Element = new ElementLocator(Locator.CssSelector, "h3");

        public ContextMenuPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public string JavaScriptText
        {
            get { return new JavaScriptAlert(this.Driver).JavaScriptText; }
        }

        public ContextMenuPage SelectTheInternetOptionFromContextMenu()
        {
            new Actions(this.Driver)
                .ContextClick(this.Driver.GetElement(this.hotSpot))
                .SendKeys(Keys.ArrowDown)
                .SendKeys(Keys.ArrowDown)
                .SendKeys(Keys.ArrowDown)
                .SendKeys(Keys.ArrowDown)
                .SendKeys(Keys.ArrowDown)
                .SendKeys(Keys.Enter)
                .Perform();
            return this;
        }

        public ContextMenuPage ConfirmJavaScript()
        {
            new JavaScriptAlert(this.Driver).ConfirmJavaScriptAlert();
            return this;
        }

        public bool IsH3ElementEqualsToExpected(string text)
        {
            return this.Driver.GetElement(this.h3Element).IsElementTextEqualsToExpected(text);
        }
    }
}
