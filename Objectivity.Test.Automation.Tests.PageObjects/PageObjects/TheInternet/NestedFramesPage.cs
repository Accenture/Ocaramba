// <copyright file="NestedFramesPage.cs" company="Objectivity Bespoke Software Specialists">
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
    using Common;
    using Common.Extensions;
    using Common.Types;

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
            this.Driver.SwitchTo().Frame(frame);
            return this;
        }

        public NestedFramesPage SwitchToParentFrame()
        {
            this.Driver.SwitchTo().ParentFrame();
            return this;
        }

        public NestedFramesPage ReturnToDefaultContent()
        {
            this.Driver.SwitchTo().DefaultContent();
            return this;
        }
    }
}
