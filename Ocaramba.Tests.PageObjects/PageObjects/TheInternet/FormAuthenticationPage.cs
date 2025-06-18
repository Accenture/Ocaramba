// <copyright file="FormAuthenticationPage.cs" company="Accenture">
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
    using NLog;
    using Ocaramba;
    using Ocaramba.Extensions;
    using Ocaramba.Helpers;
    using Ocaramba.Types;
    using OpenQA.Selenium.Interactions;
    using System;
    using System.Globalization;

    public class FormAuthenticationPage : ProjectPageBase
    {

        


        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator pageHeader = new ElementLocator(Locator.XPath, "//h2[.='Login Page']"),
                                        userNameForm = new ElementLocator(Locator.CssSelector, "Input[id=username]"),
                                        passwordForm = new ElementLocator(Locator.CssSelector, "Input[id=password]"),
                                        loginButton = new ElementLocator(Locator.XPath, "//form[@id='login']/button"),
                                        message = new ElementLocator(Locator.XPath, "//a[@class='close']/.."),
                                        nameLocator = new ElementLocator(Locator.Name, "login");

        public FormAuthenticationPage(DriverContext driverContext)
            : base(driverContext)
        {
            Logger.Info("Waiting for page to open 'Login Page'");
            this.Driver.IsElementPresent(this.pageHeader, BaseConfiguration.MediumTimeout);
			WaitHelper.Wait(() => this.Driver.GetElement(this.loginButton).Displayed,TimeSpan.FromSeconds(BaseConfiguration.LongTimeout), "Timeout while waiting for login button");
		}

        public string GetMessage
        {
            get
            {
                Logger.Info("Try to get message");
                var text = this.Driver.GetElement(this.message, BaseConfiguration.MediumTimeout, 0.1, e => e.Displayed && e.Enabled, "Tying to get welcome message every 0.1 s").Text;
                var index = text.IndexOf("!", StringComparison.Ordinal);
                text = text.Remove(index + 1);
                Logger.Info(CultureInfo.CurrentCulture, "Message '{0}'", text);
                return text;
            }
        }

        public string GetUsernameByNameLocator => this.Driver.GetElement(this.nameLocator).Text;

        public void EnterPassword(string password)
        {
            Logger.Info(CultureInfo.CurrentCulture, "Password '{0}'", new string('*', password.Length));
            Actions actions = new Actions(this.Driver);
            var element = this.Driver.GetElement(this.passwordForm);
            actions.MoveToElement(element)
             .Build()
             .Perform();

            this.Driver.GetElement(this.passwordForm).JavaScriptClick();
            this.Driver.GetElement(this.passwordForm).SendKeys(password);
            this.Driver.WaitForAjax();
        }

        public void EnterUserName(string userName)
        {
            Logger.Info(CultureInfo.CurrentCulture, "User name '{0}'", userName);
            Actions actions = new Actions(this.Driver);
            var element = this.Driver.GetElement(this.userNameForm);
            actions.MoveToElement(element)
             .Build()
             .Perform();

            this.Driver.GetElement(this.userNameForm).JavaScriptClick();
            this.Driver.GetElement(this.userNameForm).SendKeys(userName);
        }

        public void LogOn()
        {
            Logger.Info(CultureInfo.CurrentCulture, "Click on Login Button");

            this.Driver.GetElement(this.loginButton).JavaScriptClick();
        }
    }
}
