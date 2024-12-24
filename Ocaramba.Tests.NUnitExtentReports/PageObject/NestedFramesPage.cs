// <copyright file="NestedFramesPage.cs" company="Accenture">
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

namespace Ocaramba.Tests.NUnitExtentReports.PageObjects
{
    using Ocaramba;
    using Ocaramba.Extensions;
    using Ocaramba.Tests.NUnitExtentReports.ExtentLogger;
    using Ocaramba.Tests.PageObjects;
    using Ocaramba.Types;

    public class NestedFramesPage : ProjectPageBase
    {
        private readonly ElementLocator
            leftBody = new ElementLocator(Locator.CssSelector, "body"),
            middleBody = new ElementLocator(Locator.CssSelector, "div#content"),
            rightBody = new ElementLocator(Locator.CssSelector, "body"),
            bottomBody = new ElementLocator(Locator.CssSelector, "body");

        public NestedFramesPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public string LeftBody
        {
            get { return this.Driver.GetElement(this.leftBody).Text; }
        }

        public string MiddleBody
        {
            get { return this.Driver.GetElement(this.middleBody).Text; }
        }

        public string RightBody
        {
            get { return this.Driver.GetElement(this.rightBody).Text; }
        }

        public string BottomBody
        {
            get { return this.Driver.GetElement(this.bottomBody).Text; }
        }

        public NestedFramesPage SwitchToFrame(string frame)
        {
            ExtentTestLogger.Info("NestedFramesPage: Switching to frame: " + frame);
            this.Driver.SwitchTo().Frame(frame);
            return this;
        }

        public NestedFramesPage SwitchToParentFrame()
        {
            ExtentTestLogger.Info("NestedFramesPage: Switching to parent frame");
            this.Driver.SwitchTo().ParentFrame();
            return this;
        }

        public NestedFramesPage ReturnToDefaultContent()
        {
            ExtentTestLogger.Info("NestedFramesPage: Switching to default content");
            this.Driver.SwitchTo().DefaultContent();
            return this;
        }
    }
}
