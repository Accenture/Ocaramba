// <copyright file="WebElementExtensions.cs" company="Objectivity Bespoke Software Specialists">
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
namespace Ocaramba.Extensions
{
    using System;

    using OpenQA.Selenium;

    /// <summary>
    /// Extension methods for IWebElement.
    /// </summary>
    public static class WebElementExtensions
    {
        /// <summary>
        /// Verify if actual element text equals to expected.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        /// <param name="text">The text.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public static bool IsElementTextEqualsToExpected(this IWebElement webElement, string text)
        {
            return webElement.Text.Equals(text);
        }

        /// <summary>
        /// Set element attribute using java script.
        /// </summary>
        /// <example>Sample code to check page title: <code>
        /// this.Driver.SetAttribute(this.username, "attr", "10");
        /// </code></example>
        /// <param name="webElement">The web element.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="attributeValue">The attribute value.</param>
        /// <exception cref="System.ArgumentException">Element must wrap a web driver
        /// or
        /// Element must wrap a web driver that supports java script execution.</exception>
        public static void SetAttribute(this IWebElement webElement, string attribute, string attributeValue)
        {
            var javascript = webElement.ToDriver() as IJavaScriptExecutor;
            if (javascript == null)
            {
                throw new ArgumentException("Element must wrap a web driver that supports javascript execution");
            }

            javascript.ExecuteScript(
                "arguments[0].setAttribute(arguments[1], arguments[2])",
                webElement,
                attribute,
                attributeValue);
        }

        /// <summary>
        /// Click on element using java script.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        public static void JavaScriptClick(this IWebElement webElement)
        {
            var javascript = webElement.ToDriver() as IJavaScriptExecutor;
            if (javascript == null)
            {
                throw new ArgumentException("Element must wrap a web driver that supports javascript execution");
            }

            javascript.ExecuteScript("arguments[0].click();", webElement);
        }

        /// <summary>
        /// Returns the textual content of the specified node, and all its descendants regardless element is visible or not.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        /// <returns>The attribute.</returns>
        /// <exception cref="ArgumentException">Element must wrap a web driver
        /// or
        /// Element must wrap a web driver that supports java script execution.</exception>
        public static string GetTextContent(this IWebElement webElement)
        {
            var javascript = webElement.ToDriver() as IJavaScriptExecutor;
            if (javascript == null)
            {
                throw new ArgumentException("Element must wrap a web driver that supports javascript execution");
            }

            var textContent = (string)javascript.ExecuteScript("return arguments[0].textContent", webElement);
            return textContent;
        }
    }
}
