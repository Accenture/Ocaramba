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

namespace Objectivity.Test.Automation.Common.Extensions
{
    using Objectivity.Test.Automation.Common.Types;

    using OpenQA.Selenium;

    /// <summary>
    /// Locator extensions methods for selenium
    /// </summary>
    public static class LocatorExtensions
    {
        /// <summary>
        /// From the locator to selenium by converter.
        /// </summary>
        /// <param name="locator">The locator value.</param>
        /// <returns>The Selenium By</returns>
        internal static By ToBy(this ElementLocator locator)
        {
            return locator.Kind.ToBy(locator.Value);
        }

        /// <summary>
        /// From the locator to selenium by converter.
        /// </summary>
        /// <param name="locatorType">GetType of the locator.</param>
        /// <param name="locator">The locator value.</param>
        /// <returns>The Selenium By</returns>
        public static By ToBy(this Locator locatorType, string locator)
        {
            By by;
            switch (locatorType)
            {
                case Locator.Id:
                    by = By.Id(locator);
                    break;
                case Locator.ClassName:
                    by = By.ClassName(locator);
                    break;
                case Locator.CssSelector:
                    by = By.CssSelector(locator);
                    break;
                case Locator.LinkText:
                    by = By.LinkText(locator);
                    break;
                case Locator.Name:
                    by = By.Name(locator);
                    break;
                case Locator.PartialLinkText:
                    by = By.PartialLinkText(locator);
                    break;
                case Locator.TagName:
                    by = By.TagName(locator);
                    break;
                case Locator.XPath:
                    by = By.XPath(locator);
                    break;
                default:
                    by = By.Id(locator);
                    break;
            }

            return by;
        }
    }
}
