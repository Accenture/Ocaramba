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

namespace Ocaramba.Tests.NUnitExtentReports.PageObjects
{
    using System;
    using System.Globalization;
    using NLog;
    using Ocaramba;
    using Ocaramba.Extensions;
    using Ocaramba.Tests.NUnitExtentReports.ExtentLogger;
    using Ocaramba.Tests.PageObjects;
    using Ocaramba.Types;

    public class InternetPage : ProjectPageBase
    {
        [ThreadStatic]
        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

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
            ExtentTestLogger.Debug("InternetPage: Opening page: " + url);
            return this;
        }

        public DropdownPage GoToDropdownPage()
        {
            ExtentTestLogger.Debug("InternetPage: Opening Dropdown page");
            this.Driver.GetElement(this.linkLocator.Format("dropdown")).Click();
            return new DropdownPage(this.DriverContext);
        }

        public MultipleWindowsPage GoToMultipleWindowsPage()
        {
            ExtentTestLogger.Debug("InternetPage: Opening Multiple Windows page");
            this.Driver.GetElement(this.linkLocator.Format("windows")).Click();
            return new MultipleWindowsPage(this.DriverContext);
        }

        public BasicAuthPage GoToBasicAuthPage()
        {
            ExtentTestLogger.Debug("InternetPage: Opening Basic Auth Page");
            this.Driver.GetElement(this.linkLocator.Format("basic_auth")).Click();
            return new BasicAuthPage(this.DriverContext);
        }

        public NestedFramesPage GoToNestedFramesPage()
        {
            ExtentTestLogger.Debug("InternetPage: Opening Nested Frames page");
            this.Driver.GetElement(this.linkLocator.Format("nested_frames")).Click();
            return new NestedFramesPage(this.DriverContext);
        }

        public TablesPage GoToTablesPage()
        {
            ExtentTestLogger.Debug("InternetPage: Opening Tables page");
            this.Driver.GetElement(this.linkLocator.Format("tables")).Click();
            return new TablesPage(this.DriverContext);
        }

        public DragAndDropPage GoToDragAndDropPage()
        {
            ExtentTestLogger.Debug("InternetPage: Opening Drag And Drop page");
            this.Driver.GetElement(this.linkLocator.Format("drag_and_drop")).Click();
            return new DragAndDropPage(this.DriverContext);
        }

        public void ChangeBasicAuthLink(string newAttributeValue)
        {
            ExtentTestLogger.Debug("InternetPage: Changing BasicAuthLink to: " + newAttributeValue);
            var element = this.Driver.GetElement(this.basicAuthLink);
            element.SetAttribute("href", newAttributeValue);
        }

        public void BasicAuthLinkClick()
        {
            ExtentTestLogger.Debug("InternetPage: Clicking BasicAuthLink");
            var element = this.Driver.GetElement(this.basicAuthLink);
            element.Click();
        }

    }
}
