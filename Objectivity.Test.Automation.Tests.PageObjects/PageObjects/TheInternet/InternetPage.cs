// <copyright file="InternetPage.cs" company="Objectivity Bespoke Software Specialists">
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
            linkLocator = new ElementLocator(Locator.CssSelector, "a[href='/{0}']"),
            basicAuthLink = new ElementLocator(Locator.XPath, "//a[contains(text(),'Auth')]"),
            dropdownPageByLinkTextLocator = new ElementLocator(Locator.LinkText, "Dropdown"),
            partialLinkTextLocator = new ElementLocator(Locator.PartialLinkText, "Drag");

        public InternetPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public string GetDragAndDropLinkByPartialLinkText => this.Driver.GetElement(this.partialLinkTextLocator).Text;

        /// <summary>
        /// Methods for this HomePage
        /// </summary>
        /// <returns>Returns HomePage</returns>
        public InternetPage OpenHomePage()
        {
            var url = BaseConfiguration.GetUrlValue;
            this.Driver.NavigateTo(new Uri(url));
            Logger.Info(CultureInfo.CurrentCulture, "Opening page {0}", url);
            return this;
        }

        public InternetPage OpenHomePageWithUserCredentials()
        {
            var url = BaseConfiguration.GetUrlValueWithUserCredentials;
            this.Driver.NavigateTo(new Uri(url));
            Logger.Info(CultureInfo.CurrentCulture, "Opening page {0}", url);
            return this;
        }

        public DynamicControlsPage GoToDynamicControls()
        {
            this.Driver.GetElement(this.linkLocator.Format("dynamic_controls")).Click();
            return new DynamicControlsPage(this.DriverContext);
        }

        public DynamicLoadingPage GoToDynamicLoading()
        {
            this.Driver.GetElement(this.linkLocator.Format("dynamic_loading")).Click();
            return new DynamicLoadingPage(this.DriverContext);
        }

        public SlowResourcesPage GoToSlowResources()
        {
            this.Driver.GetElement(this.linkLocator.Format("slow")).Click();
            return new SlowResourcesPage(this.DriverContext);
        }

        public JavaScriptAlertsPage GoToJavaScriptAlerts()
        {
            this.Driver.GetElement(this.linkLocator.Format("javascript_alerts")).Click();
            return new JavaScriptAlertsPage(this.DriverContext);
        }

        public JavaScriptOnLoadPage GoToJavaScriptOnLoad()
        {
            this.Driver.GetElement(this.linkLocator.Format("javascript_error")).Click();
            return new JavaScriptOnLoadPage(this.DriverContext);
        }

        public void GoToPage(string page)
        {
            this.Driver.GetElement(this.linkLocator.Format(page)).Click();
        }

        public DownloadPage GoToFileDownloader()
        {
            this.Driver.GetElement(this.linkLocator.Format("download")).Click();
            return new DownloadPage(this.DriverContext);
        }

        public DropdownPage GoToDropdownPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("dropdown")).Click();
            return new DropdownPage(this.DriverContext);
        }

        public UploadPage GoToFileUploader()
        {
            this.Driver.GetElement(this.linkLocator.Format("upload")).Click();
            return new UploadPage(this.DriverContext);
        }

        public MultipleWindowsPage GoToMultipleWindowsPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("windows")).Click();
            return new MultipleWindowsPage(this.DriverContext);
        }

        public BasicAuthPage GoToBasicAuthPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("basic_auth")).Click();
            return new BasicAuthPage(this.DriverContext);
        }

        public NestedFramesPage GoToNestedFramesPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("nested_frames")).Click();
            return new NestedFramesPage(this.DriverContext);
        }

        public IFramePage GoToIFramePage()
        {
            this.Driver.GetElement(this.linkLocator.Format("frames")).Click();
            this.Driver.GetElement(this.linkLocator.Format("iframe")).Click();
            return new IFramePage(this.DriverContext);
        }

        public TablesPage GoToTablesPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("tables")).Click();
            return new TablesPage(this.DriverContext);
        }

        public CheckboxesPage GoToCheckboxesPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("checkboxes")).Click();
            return new CheckboxesPage(this.DriverContext);
        }

        public ContextMenuPage GoToContextMenuPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("context_menu")).Click();
            return new ContextMenuPage(this.DriverContext);
        }

        public FormAuthenticationPage GoToFormAuthenticationPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("login")).Click();
            return new FormAuthenticationPage(this.DriverContext);
        }

        public SecureFileDownloadPage GoToSecureFileDownloadPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("download_secure")).Click();
            return new SecureFileDownloadPage(this.DriverContext);
        }

        public ShiftingContentPage GoToShiftingContentPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("shifting_content")).Click();
            return new ShiftingContentPage(this.DriverContext);
        }

        public HoversPage GoToHoversPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("hovers")).Click();
            return new HoversPage(this.DriverContext);
        }

        public StatusCodesPage GoToStatusCodesPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("status_codes")).Click();
            return new StatusCodesPage(this.DriverContext);
        }

        public ForgotPasswordPage GoToForgotPasswordPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("forgot_password")).Click();
            return new ForgotPasswordPage(this.DriverContext);
        }

        public FloatingMenuPage GoToFloatingMenu()
        {
            this.Driver.GetElement(this.linkLocator.Format("floating_menu")).Click();
            return new FloatingMenuPage(this.DriverContext);
        }

        public DragAndDropPage GoToDragAndDropPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("drag_and_drop")).Click();
            return new DragAndDropPage(this.DriverContext);
        }

        public void ChangeBasicAuthLink(string newAttributeValue)
        {
            var element = this.Driver.GetElement(this.basicAuthLink);
            element.SetAttribute("href", newAttributeValue);
        }

        public void BasicAuthLinkClick()
        {
            var element = this.Driver.GetElement(this.basicAuthLink);
            element.Click();
        }

        public DropdownPage GoToDropdownPageByLinkText()
        {
            this.Driver.GetElement(this.dropdownPageByLinkTextLocator).Click();
            return new DropdownPage(this.DriverContext);
        }
    }
}
