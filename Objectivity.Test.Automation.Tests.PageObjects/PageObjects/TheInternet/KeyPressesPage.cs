// <copyright file="KeyPressesPage.cs" company="Objectivity Bespoke Software Specialists">
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
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using NLog;
    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Types;
    using OpenQA.Selenium;

    public class KeyPressesPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator keyPressesPageHeader = new ElementLocator(Locator.XPath, "//h3[.='Key Presses']");

        private readonly ElementLocator resultTextLocator = new ElementLocator(Locator.Id, "result");

        public KeyPressesPage(DriverContext driverContext)
            : base(driverContext)
        {
            Logger.Info("Waiting for Key Process page to open");
            this.Driver.IsElementPresent(this.keyPressesPageHeader, BaseConfiguration.ShortTimeout);
        }

        public string ResultText
        {
            get
            {
                return this.Driver.GetElement(this.resultTextLocator).Text;
            }
        }

        public bool IsResultElementPresent
        {
            get
            {
                return this.Driver.IsElementPresent(this.resultTextLocator, BaseConfiguration.ShortTimeout);
            }
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Checking all keys")]
        public void SendKeyboardKey(string key)
        {
            switch (key.ToLower(CultureInfo.InvariantCulture))
            {
                case "esc":
                    this.Driver.Actions().SendKeys(Keys.Escape).Build().Perform();
                    break;
                case "f2":
                    this.Driver.Actions().SendKeys(Keys.F2).Build().Perform();
                    break;
                case "1":
                    this.Driver.Actions().SendKeys(Keys.NumberPad1).Build().Perform();
                    break;
                case "tab":
                    this.Driver.Actions().SendKeys(Keys.Tab).Build().Perform();
                    break;
                case "space":
                    this.Driver.Actions().SendKeys(Keys.Space).Build().Perform();
                    break;
                case "arrow down":
                    this.Driver.Actions().SendKeys(Keys.ArrowDown).Build().Perform();
                    break;
                case "arrow left":
                    this.Driver.Actions().SendKeys(Keys.ArrowLeft).Build().Perform();
                    break;
                case "alt":
                    this.Driver.Actions().SendKeys(Keys.Alt).Build().Perform();
                    break;
                case "shift":
                    this.Driver.Actions().SendKeys(Keys.Shift).Build().Perform();
                    break;
                case "page up":
                    this.Driver.Actions().SendKeys(Keys.PageUp).Build().Perform();
                    break;
                case "page down":
                    this.Driver.Actions().SendKeys(Keys.PageDown).Build().Perform();
                    break;
                case "delete":
                    this.Driver.Actions().SendKeys(Keys.Delete).Build().Perform();
                    break;
                case "multiply":
                    this.Driver.Actions().SendKeys(Keys.Multiply).Build().Perform();
                    break;
                case "subtract":
                    this.Driver.Actions().SendKeys(Keys.Subtract).Build().Perform();
                    break;
                case "":
                    break;
                default:
                    throw new ArgumentException("This keybord key is not supported: " + key);
            }
        }
    }
}
