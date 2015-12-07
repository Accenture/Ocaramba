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

namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet
{
    using System.Globalization;
    using NLog;
    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Types;
    using OpenQA.Selenium;

    public class KeyPressesPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator keyPressesPageHeader = new ElementLocator(Locator.XPath, "//h3[.='File Downloader']");

        public void PressDownKey(string key)
        {
            switch (key.ToLower(CultureInfo.InvariantCulture))
            {
                case "esc":
                    this.Driver.Actions().KeyDown(Keys.Escape).KeyUp(Keys.Escape).Perform();
                    break;
                case "f2":
                    this.Driver.Actions().KeyDown(Keys.F2);
                    break;
                case "1":
                    this.Driver.Actions().KeyDown(Keys.NumberPad1);
                    break;
                case "tab":
                    this.Driver.Actions().KeyDown(Keys.Tab);
                    break;
            }
            this.Driver.Actions().KeyDown(Keys.Escape);
        }

        public KeyPressesPage(DriverContext driverContext) : base(driverContext)
        {
            Logger.Info("Waiting for Key Process page to open");
            this.Driver.IsElementPresent(this.keyPressesPageHeader, BaseConfiguration.ShortTimeout);
        }
    }
}
