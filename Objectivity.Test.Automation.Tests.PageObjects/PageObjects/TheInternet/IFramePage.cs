// <copyright file="IFramePage.cs" company="Objectivity Bespoke Software Specialists">
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
    using System.Globalization;
    using Common;
    using Common.Extensions;
    using Common.Types;
    using NLog;
    using Objectivity.Test.Automation.Common.Helpers;

    public class IFramePage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator
            menu = new ElementLocator(Locator.Id, "mceu_14-body"),
            iframe = new ElementLocator(Locator.Id, "mce_0_ifr"),
            elelemtInIFrame = new ElementLocator(Locator.Id, "tinymce");

        public IFramePage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public string TakeScreenShotsOfTextInIFrame(string folder, string name)
        {
            Logger.Info(CultureInfo.CurrentCulture, "Take Screen Shots");
            var iFrame = this.Driver.GetElement(this.iframe);
            int x = iFrame.Location.X;
            int y = iFrame.Location.Y;
            this.Driver.SwitchTo().Frame(0);
            var el = this.Driver.GetElement(this.elelemtInIFrame);
            return TakeScreenShot.TakeScreenShotOfElement(x, y, el, folder, name);
        }

        public string TakeScreenShotsOfMenu(string folder, string name)
        {
            Logger.Info(CultureInfo.CurrentCulture, "Take Screen Shots");
            var el = this.Driver.GetElement(this.menu);
            return TakeScreenShot.TakeScreenShotOfElement(el, folder, name);
        }
    }
}
