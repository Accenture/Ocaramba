// <copyright file="JavaScriptAlert.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Ocaramba.WebElements
{
    using OpenQA.Selenium;

    /// <summary>
    /// Implementation for JavaScript Alert interface.
    /// </summary>
    public class JavaScriptAlert
    {
        /// <summary>
        /// The web driver.
        /// </summary>
        private readonly IWebDriver webDriver;

        /// <summary>
        /// Initializes a new instance of the <see cref="JavaScriptAlert"/> class.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        public JavaScriptAlert(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        /// <summary>
        /// Gets java script popup text.
        /// </summary>
        public string JavaScriptText
        {
            get { return this.webDriver.SwitchTo().Alert().Text; }
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
            this.webDriver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Method sends text to Java Script Alert.
        /// </summary>
        /// <param name="text">Text to be sent.</param>
        public void SendTextToJavaScript(string text)
        {
            this.webDriver.SwitchTo().Alert().SendKeys(text);
        }
    }
}
