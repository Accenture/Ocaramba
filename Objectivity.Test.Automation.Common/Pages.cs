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
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents base page object class.
    /// </summary>
    public static class Pages
    {
        /// <summary>
        /// The existing pages
        /// </summary>
        private static readonly Dictionary<Type, Page> ExistingPages = new Dictionary<Type, Page>();

        /// <summary>
        /// Deletes the cached pages.
        /// </summary>
        public static void DeleteCachedPages()
        {
            ExistingPages.Clear();
        }

        /// <summary>
        /// Creates instance of page object.
        /// </summary>
        /// <typeparam name="T">Type of page object.</typeparam>
        /// <returns>Instance of a page object.</returns>
        public static T Create<T>() where T : Page, new()
        {
            var page = new T { Browser = BrowserManager.Handle };
            return page;
        }
    }
}
