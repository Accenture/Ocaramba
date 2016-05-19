// <copyright file="StatusCodesPage.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet
{
    using NLog;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Types;

    /// <summary>
    /// The status codes page object.
    /// </summary>
    public class StatusCodesPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator
            statusCodeHeader = new ElementLocator(Locator.XPath, "//h3[text()='Status Codes']"),
            code200 = new ElementLocator(Locator.CssSelector, "a[href='status_codes/200']");

        public StatusCodesPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public bool IsStatusCodesPageDisplayed()
        {
            Logger.Info("Check if Status Codes page is displayed.");
            return this.Driver.IsElementPresent(this.statusCodeHeader, BaseConfiguration.MediumTimeout);
        }

        public bool IsTextExistedInPageSource(string text)
        {
            return this.Driver.PageSourceContainsCase(text, BaseConfiguration.MediumTimeout, false);
        }

        public HttpCode200Page Click200()
        {
            this.Driver.GetElement(this.code200).JavaScriptClick();
            return new HttpCode200Page(this.DriverContext);
        }
    }
}
