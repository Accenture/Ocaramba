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

using System.Reflection;

namespace Objectivity.Test.Automation.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;

    using Objectivity.Test.Automation.Common.Types;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Internal;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Extensions methods for both IWebDriver and IWebElement
    /// </summary>
    public static class SearchContextExtensions
    {
        /// <summary>
        /// Finds and waits for an element that is visible and displayed for long timeout.
        /// </summary>
        /// <example>How to use it: <code>
        /// this.Driver.GetElement(this.loginButton);
        /// </code></example>
        /// <param name="element">The element.</param>
        /// <param name="locator">The locator.</param>
        /// <returns>Found element</returns>
        public static IWebElement GetElement(this ISearchContext element, ElementLocator locator)
        {
            return element.GetElement(locator, BaseConfiguration.LongTimeout, e => e.Displayed & e.Enabled);
        }

        /// <summary>
        /// Finds and waits for an element that is visible and displayed at specified time.
        /// </summary>
        /// <example>How to use it: <code>
        /// this.Driver.GetElement(this.loginButton, timeout);
        /// </code></example>
        /// <param name="element">The element.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="timeout">Specified time to wait.</param>
        /// <returns>Found element</returns>
        public static IWebElement GetElement(this ISearchContext element, ElementLocator locator, double timeout)
        {
            return element.GetElement(locator, timeout, e => e.Displayed & e.Enabled);
        }

        /// <summary>
        /// Finds and waits for an element that meets specified conditions for long timeout.
        /// </summary>
        /// <example>How to use it: <code>
        /// this.Driver.GetElement(this.loginButton, e => e.Displayed);
        /// </code></example>
        /// <param name="element">The element.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="condition">Wait until condition met.</param>
        /// <returns>Found element</returns>
        public static IWebElement GetElement(this ISearchContext element, ElementLocator locator, Func<IWebElement, bool> condition)
        {
            return element.GetElement(locator, BaseConfiguration.LongTimeout, condition);
        }

        /// <summary>
        /// Finds and waits for an element that meets specified conditions at specified time.
        /// </summary>
        /// <example>How to use it: <code>
        /// this.Driver.GetElement(this.loginButton, timeout, e => e.Displayed);
        /// </code></example>
        /// <param name="element">The element.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="condition">The condition to be met.</param>
        /// <returns>
        /// Return found element
        /// </returns>
        public static IWebElement GetElement(this ISearchContext element, ElementLocator locator, double timeout, Func<IWebElement, bool> condition)
        {
            var by = locator.ToBy();

            var wait = new WebDriverWait(element.ToDriver(), TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

            wait.Until(
                    drv =>
                    {
                        var ele = element.FindElement(@by);
                        return condition(ele);
                    });

            return element.FindElement(@by);
        }

        /// <summary>
        /// Finds and waits for an element that is visible and displayed for long timeout.
        /// </summary>
        /// <example>How to specify element type to get additional actions for it: <code>
        /// var checkbox = this.Driver.GetElement&lt;Checkbox&gt;(this.stackOverFlowCheckbox);
        /// checkbox.TickCheckbox();
        /// </code></example>
        /// <typeparam name="T">IWebComponent like ICheckbox, ISelect, etc.</typeparam>
        /// <param name="searchContext">The search context.</param>
        /// <param name="locator">The locator.</param>
        /// <returns>
        /// Located and displayed element
        /// </returns>
        public static T GetElement<T>(this ISearchContext searchContext, ElementLocator locator) where T : class, IWebElement
        {
            IWebElement webElemement = searchContext.GetElement(locator);
            return webElemement.As<T>();
        }

        /// <summary>
        /// Finds and waits for an element that is visible and displayed at specified time.
        /// </summary>
        /// <example>How to specify element type to get additional actions for it: <code>
        /// var checkbox = this.Driver.GetElement&lt;Checkbox&gt;(this.stackOverFlowCheckbox, timeout);
        /// checkbox.TickCheckbox();
        /// </code></example>
        /// <typeparam name="T">IWebComponent like ICheckbox, ISelect, etc.</typeparam>
        /// <param name="searchContext">The search context.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="timeout">Specified time to wait.</param>
        /// <returns>
        /// Located and displayed element
        /// </returns>
        public static T GetElement<T>(this ISearchContext searchContext, ElementLocator locator, double timeout) where T : class, IWebElement
        {
            IWebElement webElemement = searchContext.GetElement(locator, timeout);
            return webElemement.As<T>();
        }

        /// <summary>
        /// Finds and waits for an element that meets specified conditions for long timeout.
        /// </summary>
        /// <example>How to find hidden element, specify element type to get additional actions for it and specify condition : <code>
        /// var checkbox = this.Driver.GetElement&lt;Checkbox&gt;(this.stackOverFlowCheckbox, e => e.Displayed == false);
        /// checkbox.TickCheckbox();
        /// </code></example>
        /// <typeparam name="T">IWebComponent like ICheckbox, ISelect, etc.</typeparam>
        /// <param name="searchContext">The search context.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="condition">The condition to be met.</param>
        /// <returns>
        /// Located and displayed element
        /// </returns>
        public static T GetElement<T>(this ISearchContext searchContext, ElementLocator locator, Func<IWebElement, bool> condition) where T : class, IWebElement
        {
            IWebElement webElemement = searchContext.GetElement(locator, condition);
            return webElemement.As<T>();
        }

        /// <summary>
        /// Finds and waits for an element that meets specified conditions at specified time.
        /// </summary>
        /// <example>How to specify element type to get additional actions for it and specify time and condition to find this element: <code>
        /// var checkbox = this.Driver.GetElement&lt;Checkbox&gt;(this.stackOverFlowCheckbox, timeout, e => e.Displayed);
        /// checkbox.TickCheckbox();
        /// </code></example>
        /// <typeparam name="T">IWebComponent like ICheckbox, ISelect, etc.</typeparam>
        /// <param name="searchContext">The search context.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="timeout">Specified time to wait.</param>
        /// <param name="condition">The condition to be met.</param>
        /// <returns>
        /// Located and displayed element
        /// </returns>
        public static T GetElement<T>(this ISearchContext searchContext, ElementLocator locator, double timeout, Func<IWebElement, bool> condition) where T : class, IWebElement
        {
            IWebElement webElemement = searchContext.GetElement(locator, timeout, condition);
            return webElemement.As<T>();
        }

        /// <summary>
        /// Finds elements that are visible and displayed.
        /// </summary>
        /// <example>How to find elements : <code>
        /// var checkboxes = this.Driver.GetElements(this.stackOverFlowCheckbox);
        /// </code></example>
        /// <param name="element">The element.</param>
        /// <param name="locator">The locator.</param>
        /// <returns>Return all found and displayed and enabled elements</returns>
        public static IList<IWebElement> GetElements(this ISearchContext element, ElementLocator locator)
        {
            return element.GetElements(locator, e => e.Displayed && e.Enabled).ToList();
        }

        /// <summary>
        /// Finds elements that meet specified conditions.
        /// </summary>
        /// <example>How to find disabled elements : <code>
        /// var checkboxes = this.Driver.GetElements(this.stackOverFlowCheckbox, e => e.Enabled == false);
        /// </code></example>
        /// <param name="element">The element.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="condition">Condition to be fulfilled by elements</param>
        /// <returns>Return all found elements for specified conditions</returns>
        public static IList<IWebElement> GetElements(this ISearchContext element, ElementLocator locator, Func<IWebElement, bool> condition)
        {
            return element.FindElements(locator.ToBy()).Where(condition).ToList();
        }

        /// <summary>
        /// Finds elements that are visible and displayed.
        /// </summary>
        /// <example>How to find elements and specify element type to get additional actions for them : <code>
        /// var checkboxes = this.Driver.GetElements&lt;Checkbox&gt;(this.stackOverFlowCheckbox);
        /// checkboxes[0].TickCheckbox();
        /// </code></example>
        /// <typeparam name="T">IWebComponent like ICheckbox, ISelect, etc.</typeparam>
        /// <param name="searchContext">The search context.</param>
        /// <param name="locator">The locator.</param>
        /// <returns>Located elements</returns>
        public static IList<T> GetElements<T>(this ISearchContext searchContext, ElementLocator locator) where T : class, IWebElement
        {
            var webElements = searchContext.GetElements(locator);
            return
                new ReadOnlyCollection<T>(
                    webElements.Select(e => e.As<T>()).ToList());
        }

        /// <summary>
        /// Finds elements that meet specified conditions.
        /// </summary>
        /// <example>How to find displayed elements and specify element type to get additional actions for them : <code>
        /// var checkboxes = this.Driver.GetElements&lt;Checkbox&gt;(this.stackOverFlowCheckbox, e => e.Displayed);
        /// checkboxes[0].TickCheckbox();
        /// </code></example>
        /// <typeparam name="T">IWebComponent like Checkbox, Select, etc.</typeparam>
        /// <param name="searchContext">The search context.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="condition">The condition to be met.</param>
        /// <returns>Located elements</returns>
        public static IList<T> GetElements<T>(this ISearchContext searchContext, ElementLocator locator, Func<IWebElement, bool> condition) where T : class, IWebElement
        {
            var webElements = searchContext.GetElements(locator, condition);
            return
                new ReadOnlyCollection<T>(
                    webElements.Select(e => e.As<T>()).ToList());
        }

        /// <summary>
        /// To the driver.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        /// <returns>Driver from element</returns>
        /// <exception cref="System.ArgumentException">Element must wrap a web driver;webElement</exception>
        public static IWebDriver ToDriver(this ISearchContext webElement)
        {
            var wrappedElement = webElement as IWrapsDriver;
            if (wrappedElement == null)
            {
                FieldInfo fieldInfo = webElement.GetType().GetField("underlyingElement", BindingFlags.NonPublic | BindingFlags.Instance);
                if (fieldInfo != null)
                {
                    wrappedElement = fieldInfo.GetValue(webElement) as IWrapsDriver;
                    if (wrappedElement == null)
                    {
                        return (IWebDriver)webElement;
                    }
                }
            }

            return wrappedElement.WrappedDriver;
        }

        /// <summary>
        /// Converts generic IWebElement into specified web element (Checkbox, Table, etc.) .
        /// </summary>
        /// <typeparam name="T">Specified web element class</typeparam>
        /// <param name="webElement">Generic IWebElement.</param>
        /// <returns>Specified web element (Checkbox, Table, etc.)</returns>
        /// <exception cref="System.NullReferenceException">When constructor is null.</exception>
        private static T As<T>(this IWebElement webElement) where T : class, IWebElement
        {
            var constructor = typeof(T).GetConstructor(new[] { typeof(IWebElement) });

            if (constructor != null)
            {
                return constructor.Invoke(new object[] { webElement }) as T;
            }

            throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, "Constructor for type {0} is null.", typeof(T)));
        }
    }
}
