// <copyright file="KendoSelect.cs" company="Objectivity Bespoke Software Specialists">
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
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;

    using Ocaramba.Extensions;
    using Ocaramba.Types;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    /// <summary>
    ///     Kendo Select element.
    /// </summary>
    public abstract class KendoSelect : RemoteWebElement
    {
        private readonly ElementLocator kendoSelect = new ElementLocator(
            Locator.XPath,
            "./ancestor-or-self::span[contains(@class, 'k-widget')]//*[@id]");

        private readonly IWebElement webElement;

        /// <summary>
        ///     Initializes a new instance of the <see cref="KendoSelect" /> class.
        /// </summary>
        /// <param name="webElement">The webElement.</param>
        protected KendoSelect(IWebElement webElement)
            : base(webElement.ToDriver() as RemoteWebDriver, null)
        {
            this.webElement = webElement;
            var id = this.webElement.GetElement(this.kendoSelect, e => e.Displayed == false).GetAttribute("id");
            this.ElementCssSelector = string.Format(CultureInfo.InvariantCulture, "#{0}", id);
        }

        /// <summary>
        ///     Gets the driver.
        /// </summary>
        public IWebDriver Driver
        {
            get
            {
                return this.webElement.ToDriver();
            }
        }

        /// <summary>Gets the unordered list.</summary>
        /// <value>The unordered list.</value>
        public IWebElement UnorderedList
        {
            get
            {
                var element =
                    (IWebElement)this.Driver.JavaScripts()
                        .ExecuteScript(
                            string.Format(
                                CultureInfo.InvariantCulture,
                                "return $('{0}').data('{1}').ul.toArray()[0];",
                                this.ElementCssSelector,
                                this.SelectType));

                return element;
            }
        }

        /// <summary>
        ///     Gets the options.
        /// </summary>
        public Collection<string> Options
        {
            get
            {
                var elements =
                    (ReadOnlyCollection<IWebElement>)this.Driver.JavaScripts()
                        .ExecuteScript(
                            string.Format(
                                CultureInfo.InvariantCulture,
                                "return $('{0}').data('{1}').ul.children().toArray();",
                                this.ElementCssSelector,
                                this.SelectType));
                var options = elements.Select(element => element.GetTextContent()).ToList();
                return new Collection<string>(options);
            }
        }

        /// <summary>Gets the selected option.</summary>
        /// <value>The selected option.</value>
        public string SelectedOption
        {
            get
            {
                var option =
                    (string)this.Driver.JavaScripts()
                        .ExecuteScript(
                            string.Format(
                                CultureInfo.InvariantCulture,
                                "return $('{0}').data('{1}').text();",
                                this.ElementCssSelector,
                                this.SelectType));
                return option;
            }
        }

        /// <summary>
        /// Gets or sets the element selector.
        /// </summary>
        protected string ElementCssSelector { get; set; }

        /// <summary>Gets the selector.</summary>
        /// <value>The selector.</value>
        protected abstract string SelectType { get; }

        /// <summary>Select by text.</summary>
        /// <param name="text">The text.</param>
        public void SelectByText(string text)
        {
            this.Driver.JavaScripts()
                .ExecuteScript(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "$('{0}').data('{1}').select(function(dataItem) {{return dataItem.text === '{2}';}});",
                        this.ElementCssSelector,
                        this.SelectType,
                        text));
        }

        /// <summary>Closes this object.</summary>
        public void Close()
        {
            this.Driver.JavaScripts()
                .ExecuteScript(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "$('{0}').data('{1}').close();",
                        this.ElementCssSelector,
                        this.SelectType));
        }

        /// <summary>Opens this object.</summary>
        public void Open()
        {
            this.Driver.JavaScripts()
                .ExecuteScript(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "$('{0}').data('{1}').open();",
                        this.ElementCssSelector,
                        this.SelectType));
        }
    }
}