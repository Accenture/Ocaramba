// <copyright file="ForgotPasswordPage.cs" company="Objectivity Bespoke Software Specialists">
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

    using NLog;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Helpers;
    using Objectivity.Test.Automation.Common.Types;
    using Objectivity.Test.Automation.Tests.PageObjects;

    public class ForgotPasswordPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator pageHeader = new ElementLocator(Locator.XPath, "//h3[.='Forgot Password']"),
                                        emailTextBox = new ElementLocator(Locator.Id, "email"),
                                        emailLabel = new ElementLocator(Locator.XPath, "//input[@id='email']/preceding-sibling::label"),
                                        retrievePassword = new ElementLocator(Locator.Id, "form_submit"),
                                        message = new ElementLocator(Locator.Id, "content");

        public ForgotPasswordPage(DriverContext driverContext)
            : base(driverContext)
        {
            Logger.Info("Waiting for page to open");
            this.Driver.IsElementPresent(this.pageHeader, BaseConfiguration.ShortTimeout);
        }

        public string GetEmailLabel
        {
            get
            {
                var text = this.Driver.GetElement(this.emailLabel).Text;
                Logger.Info(CultureInfo.CurrentCulture, "Email label '{0}'", text);
                return text;
            }
        }

        public string ClickRetrievePassword
        {
            get
            {
                this.Driver.GetElement(this.retrievePassword).Click();
                var text = this.Driver.GetElement(this.message).Text.Trim();
                Logger.Info(CultureInfo.CurrentCulture, "Message '{0}'", text);
                return text;
            }
        }

        public int EnterEmail(int name, int server, int country)
        {
            var email = string.Format(CultureInfo.CurrentCulture, "{0}{1}{2}{3}{4}", NameHelper.RandomName(name), "@", NameHelper.RandomName(server), ".", NameHelper.RandomName(country));
            Logger.Info(CultureInfo.CurrentCulture, "Random generated Email'{0}'", email);
            this.Driver.GetElement(this.emailTextBox).SendKeys(email);
            return email.Length - 2;
        }
    }
}
