// <copyright file="JavaScriptAlertsPage.cs" company="Accenture">
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

namespace Ocaramba.Tests.PageObjects.PageObjects.TheInternet
{
    using Ocaramba;
    using Ocaramba.Extensions;
    using Ocaramba.Types;
    using Ocaramba.WebElements;

    public class JavaScriptAlertsPage : ProjectPageBase
    {
        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator
            jsAlertButtonLocator = new ElementLocator(Locator.CssSelector, "button[onclick='jsAlert()']"),
            jsConfirmButtonLocator = new ElementLocator(Locator.CssSelector, "button[onclick='jsConfirm()']"),
            jsPromptButtonLocator = new ElementLocator(Locator.CssSelector, "button[onclick='jsPrompt()']"),
            resultTextLocator = new ElementLocator(Locator.Id, "result");

        public JavaScriptAlertsPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public string ResultText
        {
            get
            {
                var result = this.Driver.GetElement(this.resultTextLocator).Text;
                return result;
            }
        }

        public void OpenJsAlert()
        {
            this.Driver.GetElement(this.jsAlertButtonLocator).Click();
        }

        public void OpenJsConfirm()
        {
            this.Driver.GetElement(this.jsConfirmButtonLocator).Click();
        }

        public void OpenJsPrompt()
        {
            this.Driver.GetElement(this.jsPromptButtonLocator).Click();
        }

        public void AcceptAlert()
        {
            new JavaScriptAlert(this.Driver).ConfirmJavaScriptAlert();
        }

        public void DismissAlert()
        {
            new JavaScriptAlert(this.Driver).DismissJavaScriptAlert();
        }

        public void TypeTextOnAlert(string text)
        {
            new JavaScriptAlert(this.Driver).SendTextToJavaScript(text);
        }
    }
}
