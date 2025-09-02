// <copyright file="IFramePage.cs" company="Accenture">
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
    using System;
    using System.Globalization;
    using NLog;
    using Ocaramba;
    using Ocaramba.Extensions;
    using Ocaramba.Helpers;
    using Ocaramba.Types;

    public class IFramePage : ProjectPageBase
    {

        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator
            menu = new ElementLocator(Locator.CssSelector, "div[role=menubar]"),
            iframe = new ElementLocator(Locator.Id, "mce_0_ifr"),
            elelemtInIFrame = new ElementLocator(Locator.Id, "tinymce"),
            toClose = new ElementLocator(Locator.CssSelector, "button div[aria-label]");

        public IFramePage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public string TakeScreenShotsOfTextInIFrame(string folder, string name)
        {
            Logger.Info(CultureInfo.CurrentCulture, "Take Screen Shots");

            bool flag = WaitHelper.Wait(() => this.Driver.GetElement(this.toClose).Displayed, TimeSpan.FromSeconds(BaseConfiguration.ShortTimeout), TimeSpan.FromSeconds(1));
            if (flag)
            {
                this.Driver.GetElement(this.toClose).Click();
            }

            var iFrame = this.Driver.GetElement(this.iframe);
            this.Driver.SwitchTo().Frame(0);

            var el = this.Driver.GetElement(this.elelemtInIFrame);
            return TakeScreenShot.TakeScreenShotOfElement(el, folder, name);
        }

        public string TakeScreenShotsOfMenu(string folder, string name)
        {
            Logger.Info(CultureInfo.CurrentCulture, "Take Screen Shots");
            var el = this.Driver.GetElement(this.menu);
            return TakeScreenShot.TakeScreenShotOfElement(el, folder, name);
        }
    }
}
