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

namespace Objectivity.Test.Automation.Common.WebElements
{
    using OpenQA.Selenium;

    /// <summary>
    /// Implementation for JavaScript Alert interface.
    /// </summary>
    public class JavaScriptAlert
    {
        /// <summary>
        /// The web driver
        /// </summary>
        private readonly IWebDriver webDriver;

        /// <summary>
        /// get Java script popup text
        /// </summary>
        public string JavaScriptText
        {
            get { return webDriver.SwitchTo().Alert().Text; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JavaScriptAlert"/> class.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        public JavaScriptAlert(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        /// <summary>
        /// Confirms the java script alert popup.
        /// </summary>
        public void ConfirmJavaScriptAlert()
        {
            this.webDriver.SwitchTo().Alert().Accept();
            this.webDriver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Dismisses the java script alert popup.
        /// </summary>
        public void DismissJavaScriptAlert()
        {
            this.webDriver.SwitchTo().Alert().Dismiss();
        }
    }
}
