﻿// <copyright file="HttpCode200Page.cs" company="Accenture">
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

namespace Ocaramba.Tests.PageObjects.PageObjects.TheInternet
{
    using Ocaramba;
    using Ocaramba.Extensions;
    using Ocaramba.Types;

    public class HttpCode200Page : ProjectPageBase
    {
        private readonly ElementLocator
            httpCode200Header = new ElementLocator(Locator.XPath, "//*[contains(text(),'This page returned a 200 status code.')]");

        public HttpCode200Page(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public bool IsHTTPCode200PageIsDisplayed()
        {
            return this.Driver.IsElementPresent(this.httpCode200Header, BaseConfiguration.MediumTimeout);
        }
    }
}
