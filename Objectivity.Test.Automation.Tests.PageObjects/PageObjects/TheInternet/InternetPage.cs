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

using OpenQA.Selenium;

namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet
{
    using System;
    using System.Globalization;

    using NLog;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Types;
    using Objectivity.Test.Automation.Tests.PageObjects;

    public class InternetPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator
            linkLocator = new ElementLocator(Locator.CssSelector, "a[href='/{0}']");

        public InternetPage(DriverContext driverContext) : base(driverContext)
        {
        }

        /// <summary>
        /// Methods for this HomePage
        /// </summary>
        public InternetPage OpenHomePage()
        {
            var url = this.GetUrlValue();
            this.Driver.NavigateTo(new Uri(url));
            Logger.Info(CultureInfo.CurrentCulture, "Opening page {0}", url);
            return this;
        }

        public InternetPage OpenHomePageWithUserCredentials()
        {
            var url = this.GetUrlValueWithUserCredentials();
            this.Driver.NavigateTo(new Uri(url));
            Logger.Info(CultureInfo.CurrentCulture, "Opening page {0}", url);
            return this;
        }

        public JavaScriptAlertsPage GoToJavaScriptAlerts()
        {
            this.Driver.GetElement(this.linkLocator.Evaluate("javascript_alerts")).Click();
            return new JavaScriptAlertsPage(this.DriverContext);
        }

        public void GoToPage(string page)
        {
            this.Driver.GetElement(this.linkLocator.Evaluate(page)).Click();
        }

        public DownloadPage GoToFileDownloader()
        {
            this.Driver.GetElement(this.linkLocator.Evaluate("download")).Click();
            return new DownloadPage(this.DriverContext);
        }

        public MultipleWindowsPage GoToMultipleWindowsPage()
        {
             this.Driver.GetElement(this.linkLocator.Evaluate("windows")).Click();
             return new MultipleWindowsPage(this.DriverContext);
        }

        public BasicAuthPage GoToBasicAuthPage()
        {
            this.Driver.GetElement(this.linkLocator.Evaluate("basic_auth")).Click();
            return new BasicAuthPage(this.DriverContext);
        }

        public NestedFramesPage GoToNestedFramesPage()
        {
            Driver.GetElement(linkLocator.Evaluate("nested_frames")).Click();
            return new NestedFramesPage(DriverContext);
        }
        public CheckboxesPage GoToCheckboxesPage()
        {
            this.Driver.GetElement(this.linkLocator.Evaluate("checkboxes")).Click();
            return new CheckboxesPage(this.DriverContext);
        }

        public ContextMenuPage GoToContextMenuPage()
        {
            Driver.GetElement(linkLocator.Evaluate("context_menu")).Click();
            return new ContextMenuPage(DriverContext);
        }

        public FormAuthenticationPage GoToFormAuthenticationPage()
        {
            this.Driver.GetElement(this.linkLocator.Evaluate("login")).Click();
            return new FormAuthenticationPage(this.DriverContext);
        }

        public SecureFileDownloadPage GoToSecureFileDownloadPage()
        {
            this.Driver.GetElement(this.linkLocator.Evaluate("download_secure")).Click();
            return new SecureFileDownloadPage(this.DriverContext);
        }

        public ShiftingContentPage GoToShiftingContentPage()
        {
            this.Driver.GetElement(this.linkLocator.Evaluate("shifting_content")).Click();
            return new ShiftingContentPage(this.DriverContext);
        }

        public HoversPage GoToHoversPage()
        {
            this.Driver.GetElement(this.linkLocator.Evaluate("hovers")).Click();
            return new HoversPage(this.DriverContext);
        }

        public StatusCodesPage GoToStatusCodesPage()
        {
            this.Driver.GetElement(this.linkLocator.Evaluate("status_codes")).Click();
            return new StatusCodesPage(this.DriverContext);
        }

        public ForgotPasswordPage GoToForgotPasswordPage()
        {
            this.Driver.GetElement(this.linkLocator.Evaluate("forgot_password")).Click();
            return new ForgotPasswordPage(this.DriverContext);
        }

        public FloatingMenuPage GoToFloatingMenu()
        {
            this.Driver.GetElement(this.linkLocator.Evaluate("floating_menu")).Click();
            return new FloatingMenuPage(this.DriverContext);
        }

        private string GetUrlValue()
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                "{0}://{1}{2}",
                BaseConfiguration.Protocol,
                BaseConfiguration.Host,
                BaseConfiguration.Url);
        }



        private string GetUrlValueWithUserCredentials()
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                "{0}://{1}:{2}@{3}{4}",
                BaseConfiguration.Protocol,
                BaseConfiguration.Username,
                BaseConfiguration.Password,
                BaseConfiguration.Host,
                BaseConfiguration.Url);
    }


    }
}
