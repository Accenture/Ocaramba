// <copyright file="Select.cs" company="Objectivity Bespoke Software Specialists">
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
    using System;
    using System.Globalization;
    using System.Linq;
    using NLog;
    using Ocaramba.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Select contains implementation for method that can be used on dropdown.
    /// </summary>
    public class Select : RemoteWebElement
    {
#if net47 || net45
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
#endif
#if netcoreapp3_1
        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
#endif

        /// <summary>
        /// The web element.
        /// </summary>
        private readonly IWebElement webElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="Select"/> class.
        /// </summary>
        /// <param name="webElement">The _webElement.</param>
        public Select(IWebElement webElement)
            : base(webElement.ToDriver() as RemoteWebDriver, null)
        {
            this.webElement = webElement;
        }

        /// <summary>
        /// Selenium SelectElement class.
        /// </summary>
        /// <example>Simple use of SelectElement: <code>
        /// this.Driver.GetElement&lt;Select&gt;(WebElement).SelectElement().SelectedOption;
        /// </code></example>
        /// <returns>Return new SelectElement handle.</returns>
        public SelectElement SelectElement()
        {
            return new SelectElement(this.webElement);
        }

        /// <summary>
        /// Select value in dropdown using text.
        /// </summary>
        /// <param name="selectValue">Text to be selected.</param>
        public void SelectByText(string selectValue)
        {
            this.SelectByText(selectValue, BaseConfiguration.MediumTimeout);
        }

        /// <summary>
        /// Select value in dropdown using text.
        /// </summary>
        /// <param name="selectValue">Text to be selected.</param>
        /// <param name="timeout">The timeout.</param>
        public void SelectByText(string selectValue, double timeout)
        {
            var element = this.WaitUntilDropdownIsPopulated(timeout);

            var selectElement = new SelectElement(element);

            try
            {
                selectElement.SelectByText(selectValue);
            }
            catch (NoSuchElementException e)
            {
                Logger.Error(CultureInfo.CurrentCulture, "unable to select given label: {0}", selectValue);
                Logger.Error(e.Message);
            }
        }

        /// <summary>
        /// Select value in dropdown using index.
        /// </summary>
        /// <param name="index">Index value to be selected.</param>
        public void SelectByIndex(int index)
        {
            this.SelectByIndex(index, BaseConfiguration.MediumTimeout);
        }

        /// <summary>
        /// Select value in dropdown using index.
        /// </summary>
        /// <param name="index">Index value to be selected.</param>
        /// <param name="timeout">The timeout.</param>
        public void SelectByIndex(int index, double timeout)
        {
            timeout = timeout.Equals(0) ? BaseConfiguration.MediumTimeout : timeout;

            var element = this.WaitUntilDropdownIsPopulated(timeout);

            var selectElement = new SelectElement(element);

            try
            {
                selectElement.SelectByIndex(index);
            }
            catch (NoSuchElementException e)
            {
                Logger.Error(CultureInfo.CurrentCulture, "unable to select given index: {0}", index);
                Logger.Error(e.Message);
            }
        }

        /// <summary>
        /// Select value in dropdown using value attribute.
        /// </summary>
        /// <param name="selectValue">Value to be selected.</param>
        public void SelectByValue(string selectValue)
        {
            this.SelectByValue(selectValue, BaseConfiguration.MediumTimeout);
        }

        /// <summary>
        /// Select value in dropdown using value attribute.
        /// </summary>
        /// <param name="selectValue">Value to be selected.</param>
        /// <param name="timeout">The timeout.</param>
        public void SelectByValue(string selectValue, double timeout)
        {
            var element = this.WaitUntilDropdownIsPopulated(timeout);

            var selectElement = new SelectElement(element);

            try
            {
                selectElement.SelectByValue(selectValue);
            }
            catch (NoSuchElementException e)
            {
                Logger.Error(CultureInfo.CurrentCulture, "unable to select given value: {0}", selectValue);
                Logger.Error(e.Message);
            }
        }

        /// <summary>
        /// Determines whether text is available in dropdown.
        /// </summary>
        /// <param name="option">The text.</param>
        /// <returns>
        /// True or False depends if text is available in dropdown.
        /// </returns>
        public bool IsSelectOptionAvailable(string option)
        {
            return this.IsSelectOptionAvailable(option, BaseConfiguration.MediumTimeout);
        }

        /// <summary>
        /// Determines whether text is available in dropdown.
        /// </summary>
        /// <param name="option">The text.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>
        /// True or False depends if text is available in dropdown.
        /// </returns>
        public bool IsSelectOptionAvailable(string option, double timeout)
        {
            var element = this.WaitUntilDropdownIsPopulated(timeout);
            var selectElement = new SelectElement(element);

            return selectElement.Options.Any(el => el.Text.Equals(option));
        }

        /// <summary>
        /// Waits the until dropdown is populated.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns>Web element when dropdown populated.</returns>
        private IWebElement WaitUntilDropdownIsPopulated(double timeout)
        {
            var selectElement = new SelectElement(this.webElement);
            var isPopulated = false;
            try
            {
                new WebDriverWait(this.webElement.ToDriver(), TimeSpan.FromSeconds(timeout)).Until(
                    x =>
                    {
                        var size = selectElement.Options.Count;
                        if (size > 1)
                        {
                            isPopulated = true;
                        }

                        return isPopulated;
                    });
            }
            catch (NoSuchElementException e)
            {
                Logger.Error(CultureInfo.CurrentCulture, "unable to select given index: {0}", selectElement);
                Logger.Error(e.Message);
            }

            return this.webElement;
        }
    }
}
