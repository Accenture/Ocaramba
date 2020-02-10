// <copyright file="Locator.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Ocaramba
{
    /// <summary>
    /// The page element locator type. Needs to be translated to automation framework specific locators.
    /// </summary>
    public enum Locator
    {
        /// <summary>
        /// The Id selector
        /// </summary>
        Id,

        /// <summary>
        /// The class name selector
        /// </summary>
        ClassName,

        /// <summary>
        /// The CSS selector
        /// </summary>
        CssSelector,

        /// <summary>
        /// The link text selector
        /// </summary>
        LinkText,

        /// <summary>
        /// The name selector
        /// </summary>
        Name,

        /// <summary>
        /// The partial link text selector
        /// </summary>
        PartialLinkText,

        /// <summary>
        /// The tag name selector
        /// </summary>
        TagName,

        /// <summary>
        /// The XPath selector
        /// </summary>
        XPath,
    }
}
