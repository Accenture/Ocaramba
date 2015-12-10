/*
The MIT License (MIT)

Copyright (c) 2015 Objectivity Bespoke Software Specialists

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
using Objectivity.Test.Automation.Common.Types;
using NLog;

namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet
{
    public class FloatingMenuPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator floatingMenuPageText = new ElementLocator(
            Locator.CssSelector,
            "div.scroll");

        private readonly ElementLocator floatingHomeButton = new ElementLocator(
            Locator.CssSelector,
            "a[href='#home']");

        public FloatingMenuPage(DriverContext driverContext)
            : base(driverContext)
        {
            Logger.Info("Waiting for File Download page to open");
           // this.Driver.IsElementPresent(this.floatingMenuPageText, BaseConfiguration.ShortTimeout);
        }

        public FloatingMenuPage ClickFloatingMenuButton()
        {
           this.Driver.ScrollIntoMiddle(this.floatingMenuPageText);
            this.Driver.GetElement(this.floatingHomeButton).Click();        

            return this;
        }
    }
}
