// <copyright file="KendoComboBox.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Ocaramba.WebElements.Kendo
{
    using System.Globalization;

    using Ocaramba.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    ///     Kendo Combo Box element.
    /// </summary>
    public class KendoComboBox : KendoSelect
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="KendoComboBox" /> class.
        /// </summary>
        /// <param name="webElement">The webElement.</param>
        public KendoComboBox(IWebElement webElement)
            : base(webElement)
        {
        }

        /// <summary>
        ///     Gets web element of the visible input element, where the user types.
        /// </summary>
        public IWebElement Input
        {
            get
            {
                var element =
                    (IWebElement)this.Driver.JavaScripts()
                        .ExecuteScript(
                            string.Format(
                                CultureInfo.InvariantCulture,
                                "return $('{0}').data('{1}').input.toArray()[0];",
                                this.ElementCssSelector,
                                this.SelectType));
                return element;
            }
        }

        /// <summary>Gets the selector.</summary>
        /// <value>The selector.</value>
        protected override string SelectType
        {
            get
            {
                return "kendoComboBox";
            }
        }

        /// <summary>
        /// Types text into KendoComboBox input.
        /// </summary>
        /// <param name="text">Text to type.</param>
        public new void SendKeys(string text)
        {
            this.Input.Clear();
            this.Input.SendKeys(text);
        }
    }
}