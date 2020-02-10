// <copyright file="ElementLocator.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Ocaramba.Types
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Class that helps to define Kind and value for html elements.
    /// </summary>
    public class ElementLocator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementLocator"/> class.
        /// </summary>
        /// <example>How we define locator: <code>
        /// private readonly ElementLocator searchTextbox = new ElementLocator(Locator.Id, "SearchTextBoxId");
        /// </code> </example>
        /// <param name="kind">The locator type.</param>
        /// <param name="value">The locator value.</param>
        public ElementLocator(Locator kind, string value)
        {
            this.Kind = kind;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the kind of element locator.
        /// </summary>
        /// <value>
        /// Kind of element locator (Id, Xpath, ...).
        /// </value>
        public Locator Kind { get; set; }

        /// <summary>
        /// Gets or sets the element locator value.
        /// </summary>
        /// <value>
        /// The the element locator value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Formats the generic element locator definition and create the instance.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// New element locator with value changed by injected parameters.
        /// </returns>
        /// <example>How we can replace parts of defined locator: <code>
        /// private readonly ElementLocator menuLink = new ElementLocator(Locator.XPath, "//*[@title='{0}' and @ms.title='{1}']");
        /// var element = this.Driver.GetElement(this.menuLink.Format("info","news"));
        /// </code></example>
        public ElementLocator Format(params object[] parameters)
        {
            return new ElementLocator(this.Kind, string.Format(CultureInfo.CurrentCulture, this.Value, parameters));
        }
    }
}