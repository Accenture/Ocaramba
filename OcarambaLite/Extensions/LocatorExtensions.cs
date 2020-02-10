// <copyright file="LocatorExtensions.cs" company="Objectivity Bespoke Software Specialists">
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
    using Ocaramba.Types;
    using OpenQA.Selenium;

    /// <summary>
    /// Locator extensions methods for selenium.
    /// </summary>
    public static class LocatorExtensions
    {
        /// <summary>
        /// From the locator to selenium by converter.
        /// </summary>
        /// <example>Using standard method FindElement, even we have locator as ElementLocator: <code>
        /// private readonly ElementLocator searchTextbox = new ElementLocator(Locator.Id, "SearchTextBoxId");
        /// this.Driver.FindElement(searchTextbox.ToBy());
        /// </code> </example>
        /// <param name="locator">The element locator.</param>
        /// <returns>The Selenium By.</returns>
        public static By ToBy(this ElementLocator locator)
        {
            By by;
            switch (locator.Kind)
            {
                case Locator.Id:
                    by = By.Id(locator.Value);
                    break;
                case Locator.ClassName:
                    by = By.ClassName(locator.Value);
                    break;
                case Locator.CssSelector:
                    by = By.CssSelector(locator.Value);
                    break;
                case Locator.LinkText:
                    by = By.LinkText(locator.Value);
                    break;
                case Locator.Name:
                    by = By.Name(locator.Value);
                    break;
                case Locator.PartialLinkText:
                    by = By.PartialLinkText(locator.Value);
                    break;
                case Locator.TagName:
                    by = By.TagName(locator.Value);
                    break;
                case Locator.XPath:
                    by = By.XPath(locator.Value);
                    break;
                default:
                    by = By.Id(locator.Value);
                    break;
            }

            return by;
        }
    }
}
