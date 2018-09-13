// <copyright file="DynamicLoadingPage.cs" company="Objectivity Bespoke Software Specialists">
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
    using Common.Types;
    using NLog;
    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Tests.PageObjects;

    public class DynamicLoadingPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator exampleLink1 = new ElementLocator(Locator.CssSelector, "a[href='/dynamic_loading/2'"),
        startButton = new ElementLocator(Locator.XPath, "//button[.='Start']"),
        text = new ElementLocator(Locator.XPath, "//div[@id='finish']/h4");

        public DynamicLoadingPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public string Text
        {
            get
            {
                var returnText = this.Driver.GetElement(this.text).Text;
                Logger.Debug(returnText);
                return returnText;
            }
        }

        public void ShortTimeoutText()
        {
          var returnText = this.Driver.GetElement(this.text, BaseConfiguration.ShortTimeout).Text;
          Logger.Debug(returnText);
        }

        public DynamicLoadingPage ClickOnExample2()
        {
            this.Driver.GetElement(this.exampleLink1).Click();
            return this;
        }

        public DynamicLoadingPage ClickStart()
        {
            this.Driver.GetElement(this.startButton).Click();
            return this;
        }
    }
}
