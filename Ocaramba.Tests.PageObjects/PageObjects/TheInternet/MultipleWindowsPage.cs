// <copyright file="MultipleWindowsPage.cs" company="Accenture">
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
    using NLog;
    using Ocaramba;
    using Ocaramba.Extensions;
    using Ocaramba.Helpers;
    using Ocaramba.Types;
    using System;

    public class MultipleWindowsPage : ProjectPageBase
    {

        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator
    clickHerePageLocator = new ElementLocator(Locator.CssSelector, "a[href='/windows/new']");

        public MultipleWindowsPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public NewWindowPage    OpenNewWindowPage()
        {
            Logger.Info("Opening New Window page from Multiple Windows page.");
            WaitHelper.Wait(() => this.Driver.GetElement(this.clickHerePageLocator).Enabled, TimeSpan.FromSeconds(BaseConfiguration.LongTimeout), "Timeout");
            Logger.Info("Clicking on 'Click Here' link to open new window.");
            this.Driver.GetElement(this.clickHerePageLocator).JavaScriptClick();
            Logger.Info("Switching to new window with URL 'https://the-internet.herokuapp.com/windows/new'.");
            this.Driver.SwitchToWindowUsingUrl(new Uri("https://the-internet.herokuapp.com/windows/new"), 20);
            return new NewWindowPage(this.DriverContext);
        }
    }
}
