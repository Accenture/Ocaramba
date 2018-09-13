// <copyright file="DragAndDropPage.cs" company="Objectivity Bespoke Software Specialists">
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
    using Objectivity.Test.Automation.Common.Types;

    public class DragAndDropPage : ProjectPageBase
    {
        private readonly ElementLocator boxA = new ElementLocator(Locator.Id, "column-a");
        private readonly ElementLocator boxB = new ElementLocator(Locator.Id, "column-b");
        private readonly ElementLocator boxBtext = new ElementLocator(Locator.XPath, "//div[@id='column-b']/header");
        private readonly ElementLocator classNameLocator = new ElementLocator(Locator.ClassName, "example");

        private readonly ElementLocator cssSelectorLocator = new ElementLocator(Locator.CssSelector, "[class='example']");

        public DragAndDropPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public string GetByIdLocator => this.Driver.GetElement(this.boxA).Text;

        public string GetByClassName => this.Driver.GetElement(this.classNameLocator).Text;

        public string GetByCssSelectorLocator => this.Driver.GetElement(this.cssSelectorLocator).Text;

        public DragAndDropPage MoveElementAtoElementB()
        {
           this.Driver.DragAndDropJs(this.Driver.GetElement(this.boxA), this.Driver.GetElement(this.boxB));
           return this;
        }

        public bool IsElementAMovedToB()
        {
            return this.Driver.GetElement(this.boxBtext).Text.Equals("A");
        }
    }
}
