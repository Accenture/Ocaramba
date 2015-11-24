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

namespace Objectivity.Test.Automation.Common.WebElements.Kendo
{
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;

    using Objectivity.Test.Automation.Common.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    /// <summary>
    /// Kendo Tree View element
    /// </summary>
    public class KendoTreeView : RemoteWebElement
    {
        private readonly IWebElement webElement;

        private readonly string kendoTreeView;

        /// <summary>
        /// Initializes a new instance of the <see cref="KendoTreeView"/> class.
        /// </summary>
        /// <param name="webElement">The webElement</param>
        public KendoTreeView(IWebElement webElement)
            : base(webElement.ToDriver() as RemoteWebDriver, null)
        {
            this.webElement = webElement;
            var id = webElement.GetAttribute("id");
            this.kendoTreeView = string.Format(CultureInfo.InvariantCulture, "$('#{0}').data('kendoTreeView')", id);
        }

        /// <summary>
        /// Gets the driver.
        /// </summary>
        public IWebDriver Driver
        {
            get
            {
                return this.webElement.ToDriver();
            }
        }

        /// <summary>
        /// The expand.
        /// </summary>
        public void Expand()
        {
            this.Driver.JavaScripts()
                .ExecuteScript(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "var treeView = {0}.expand('.k-item'); ",
                        this.kendoTreeView));
            ////this.Driver.WaitForSourceChanged(1);
        }

        /// <summary>
        /// The select by text.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        public void SelectByText(string text)
        {
            this.Driver.JavaScripts()
                .ExecuteScript(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "var treeView = {0}; var element = treeView.findByText('{1}'); treeView.select(element); treeView.trigger('select',{{node:element}});",
                        this.kendoTreeView,
                    text));
            ////this.Driver.WaitForSourceChanged(1);
        }

        /// <summary>Searches for the first text.</summary>
        /// <param name="text">The text.</param>
        /// <returns>The found text.</returns>
        public Collection<string> FindByText(string text)
        {
            object elements = this.Driver.JavaScripts()
                .ExecuteScript(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "var treeView = {0}; return treeView.findByText('{1}').toArray();",
                        this.kendoTreeView,
                        text));

            var webElements = elements as ReadOnlyCollection<IWebElement>;
            if (webElements != null)
            {
                return
                    (Collection<string>)webElements.Select(
                        element =>
                        (string)this.Driver.JavaScripts().ExecuteScript("return arguments[0].textContent", element));
            }

            return new Collection<string>();
        }
    }
}
