// <copyright file="KendoGrid.cs" company="Objectivity Bespoke Software Specialists">
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
    using System;
    using System.Globalization;

    using Ocaramba.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Kendo Grid element.
    /// </summary>
    public class KendoGrid : RemoteWebElement
    {
        private readonly IWebElement webElement;

        private readonly string kendoGrid;

        /// <summary>
        /// Initializes a new instance of the <see cref="KendoGrid"/> class.
        /// </summary>
        /// <param name="webElement">The webElement.</param>
        public KendoGrid(IWebElement webElement)
            : base(webElement.ToDriver() as RemoteWebDriver, null)
        {
            this.webElement = webElement;
            var id = webElement.GetAttribute("id");
            this.kendoGrid = string.Format(CultureInfo.InvariantCulture, "$('#{0}').data('kendoGrid')", id);
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
        /// Gets the page.
        /// </summary>
        public long Page
        {
            get
            {
                return
                    (long)this.Driver.JavaScripts()
                        .ExecuteScript(
                            string.Format(CultureInfo.InvariantCulture, "return {0}.pager.page();", this.kendoGrid));
            }
        }

        /// <summary>
        /// Gets the total pages.
        /// </summary>
        public long TotalPages
        {
            get
            {
                return
                    (long)this.Driver.JavaScripts()
                        .ExecuteScript(
                            string.Format(
                                CultureInfo.InvariantCulture,
                                "return {0}.pager.totalPages();",
                                this.kendoGrid));
            }
        }

        /// <summary>
        /// The set page.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        public void SetPage(int page)
        {
            this.Driver.JavaScripts()
                .ExecuteScript(
                    string.Format(CultureInfo.InvariantCulture, "{0}.pager.page({1});", this.kendoGrid, page));
            this.Driver.WaitForAjax();
        }

        /// <summary>
        /// The search row with text.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The <see cref="IWebElement"/>.
        /// </returns>
        public IWebElement SearchRowWithText(string text)
        {
            var row = this.GetRowWithText(text);
            if (row != null)
            {
                return row;
            }

            for (var i = 1; i < this.TotalPages + 1; i++)
            {
                this.SetPage(i);
                row = this.GetRowWithText(text);
                if (row != null)
                {
                    return row;
                }
            }

            return null;
        }

        /// <summary>
        /// The search row with text.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="timeoutInSeconds">
        /// The timeout in seconds.
        /// </param>
        /// <returns>
        /// The <see cref="IWebElement"/>.
        /// </returns>
        /// <exception cref="NotFoundException">
        /// When row with text was not found in specific time.
        /// </exception>
        public IWebElement SearchRowWithText(string text, double timeoutInSeconds)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.Until(x => this.SearchRowWithText(text) != null);
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }

            return this.SearchRowWithText(text);
        }

        /// <summary>
        /// The get row with text.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The <see cref="IWebElement"/>.
        /// </returns>
        private IWebElement GetRowWithText(string text)
        {
            return
                (IWebElement)this.Driver.JavaScripts()
                    .ExecuteScript(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "return {0}.tbody.find('tr:contains(\"{1}\")').get(0);",
                            this.kendoGrid,
                            text));
        }
    }
}