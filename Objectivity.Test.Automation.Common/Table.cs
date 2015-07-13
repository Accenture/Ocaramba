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

namespace Objectivity.Test.Automation.Common
{
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Types;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    /// <summary>
    /// The table class contains actions on tables.
    /// </summary>
    public class Table : RemoteWebElement
    {
        /// <summary>
        /// The web element
        /// </summary>
        private readonly IWebElement webElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        /// <param name="webElement">The webElement.</param>
        public Table(IWebElement webElement)
            : base(webElement.ToDriver() as RemoteWebDriver, null)
        {
            this.webElement = webElement;
        }

        /// <summary>
        /// Returns a text representation of the grid or table html like element
        /// </summary>
        /// <param name="rowLocator">The row locator.</param>
        /// <param name="columnLocator">The column locator</param>
        /// <returns>
        /// Text representation of the grid or table html like element
        /// </returns>
        public string[][] GetTable(ElementLocator rowLocator, ElementLocator columnLocator)
        {
            var table = this.webElement;
            var rows = table.FindElements(rowLocator.ToBy());

            var result = new string[rows.Count][];
            var i = 0;

            foreach (var row in rows)
            {
                var cells = row.GetElements(columnLocator);
                result[i] = new string[cells.Count];

                var j = 0;
                foreach (var cell in cells)
                {
                    result[i][j++] = cell.Text;
                }
            }

            return result;
        }
    }
}
