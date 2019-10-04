﻿// <copyright file="IFramePage.cs" company="Objectivity Bespoke Software Specialists">
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
    using System.Globalization;
    using NLog;
    using Ocaramba;
    using Ocaramba.Extensions;
    using Ocaramba.Helpers;
    using Ocaramba.Types;

    public class IFramePage : ProjectPageBase
    {
#if net47
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
#endif
#if netcoreapp2_2
        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
#endif

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
#if net47
            return TakeScreenShot.TakeScreenShotOfElement(x, y, el, folder, name);
#endif
#if netcoreapp2_2
            return "to_be_implemented_in_netcore";
#endif
        }

        public string TakeScreenShotsOfMenu(string folder, string name)
        {
            Logger.Info(CultureInfo.CurrentCulture, "Take Screen Shots");
            var el = this.Driver.GetElement(this.menu);
            //return TakeScreenShot.TakeScreenShotOfElement(el, folder, name);
            return "";
        }
    }
}
