// <copyright file="MyEventFiringWebDriver.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.Common.Logger
{
    using System.Globalization;
    using NLog;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Events;

    /// <summary>
    /// Override selenium methods to add event logs
    /// </summary>
    public class MyEventFiringWebDriver : EventFiringWebDriver
    {
        private static readonly NLog.Logger Logger = LogManager.GetLogger("DRIVER");

        /// <summary>
        /// Initializes a new instance of the <see cref="MyEventFiringWebDriver"/> class.
        /// </summary>
        /// <param name="parentDriver">The parent driver.</param>
        public MyEventFiringWebDriver(IWebDriver parentDriver)
            : base(parentDriver)
        {
        }

        /// <summary>
        /// Raises the <see cref="E:Navigating" /> event.
        /// </summary>
        /// <param name="e">The <see cref="WebDriverNavigationEventArgs"/> instance containing the event data.</param>
        protected override void OnNavigating(WebDriverNavigationEventArgs e)
        {
            Logger.Trace(CultureInfo.CurrentCulture, "Navigating to: {0}", e.Url);
            base.OnNavigating(e);
        }

        /// <summary>
        /// Raises the <see cref="E:ElementClicking" /> event.
        /// </summary>
        /// <param name="e">The <see cref="WebElementEventArgs"/> instance containing the event data.</param>
        protected override void OnElementClicking(WebElementEventArgs e)
        {
            Logger.Trace(CultureInfo.CurrentCulture, "Clicking: {0}", ToStringElement(e));
            base.OnElementClicking(e);
        }

        /// <summary>
        /// Raises the <see cref="E:ElementValueChanging" /> event.
        /// </summary>
        /// <param name="e">The <see cref="WebElementEventArgs"/> instance containing the event data.</param>
        protected override void OnElementValueChanging(WebElementValueEventArgs e)
        {
            Logger.Trace(CultureInfo.CurrentCulture, "On Element Value Changing: {0}", ToStringElement(e));
            base.OnElementValueChanging(e);
        }

        /// <summary>
        /// Raises the <see cref="E:ElementValueChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="WebElementEventArgs"/> instance containing the event data.</param>
        protected override void OnElementValueChanged(WebElementValueEventArgs e)
        {
            Logger.Trace(CultureInfo.CurrentCulture, "On Element Value Changed: {0}", ToStringElement(e));
            base.OnElementValueChanging(e);
        }

        /// <summary>
        /// Raises the <see cref="E:FindingElement" /> event.
        /// </summary>
        /// <param name="e">The <see cref="FindElementEventArgs"/> instance containing the event data.</param>
        protected override void OnFindingElement(FindElementEventArgs e)
        {
            Logger.Trace(CultureInfo.CurrentCulture, "OnFindingElement: {0}", e.FindMethod);
            base.OnFindingElement(e);
        }

        /// <summary>
        /// Raises the <see cref="E:ScriptExecuting" /> event.
        /// </summary>
        /// <param name="e">The <see cref="WebDriverScriptEventArgs"/> instance containing the event data.</param>
        protected override void OnScriptExecuting(WebDriverScriptEventArgs e)
        {
            Logger.Trace(CultureInfo.CurrentCulture, "On Script Executing: {0}", e.Script);
            base.OnScriptExecuting(e);
        }

        /// <summary>
        /// Raises the <see cref="E:ScriptExecuted" /> event.
        /// </summary>
        /// <param name="e">The <see cref="WebDriverScriptEventArgs"/> instance containing the event data.</param>
        protected override void OnScriptExecuted(WebDriverScriptEventArgs e)
        {
            Logger.Trace(CultureInfo.CurrentCulture, "On Script Executed: {0}", e.Script);
            base.OnScriptExecuted(e);
        }

        /// <summary>
        /// To the string element.
        /// </summary>
        /// <param name="e">The <see cref="WebElementEventArgs"/> instance containing the event data.</param>
        /// <returns>Formated issue</returns>
        private static string ToStringElement(WebElementEventArgs e)
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                "{0}{{{1}{2}{3}{4}{5}{6}{7}{8}}}",
                e.Element.TagName,
                AppendAttribute(e, "id"),
                AppendAttribute(e, "name"),
                AppendAttribute(e, "value"),
                AppendAttribute(e, "class"),
                AppendAttribute(e, "type"),
                AppendAttribute(e, "role"),
                AppendAttribute(e, "text"),
                AppendAttribute(e, "href"));
        }

        /// <summary>
        /// Appends the attribute.
        /// </summary>
        /// <param name="e">The <see cref="WebElementEventArgs"/> instance containing the event data.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns>Atribute and value</returns>
        private static string AppendAttribute(WebElementEventArgs e, string attribute)
        {
            var attrValue = attribute == "text" ? e.Element.Text : e.Element.GetAttribute(attribute);
            return string.IsNullOrEmpty(attrValue) ? string.Empty : string.Format(CultureInfo.CurrentCulture, " {0}='{1}' ", attribute, attrValue);
        }
    }
}
