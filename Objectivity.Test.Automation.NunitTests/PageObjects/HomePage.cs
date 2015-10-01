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

namespace Objectivity.Test.Automation.NunitTests.PageObjects
{
    using System;
    using System.Globalization;

    using NLog;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Types;

    public class HomePage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator
            searchTextbox = new ElementLocator(Locator.Id, "HeaderSearchTextBox"),
            searchButton = new ElementLocator(Locator.Id, "FakeHeaderSearchButton"),
            menuLink = new ElementLocator(Locator.XPath, "//*[@title='{0}' and @ms.title='{1}']"),
            menuAllLinks = new ElementLocator(Locator.CssSelector, "ul.navL1>li>a"),
            technologiesMenuLink = new ElementLocator(Locator.XPath, "//a[@title='Technologies']"),
            objectivityMenuLink = new ElementLocator(Locator.XPath, "//a[@title='Objectivity']");

        public HomePage(DriverContext driverContext): base(driverContext)
        {
        }

        /// <summary>
        /// Methods for this HomePage
        /// </summary>
        public HomePage OpenHomePage()
        {
            var url = this.GetUrlValue();
            this.Driver.NavigateTo(new Uri(url));
            Logger.Info(CultureInfo.CurrentCulture, "Opening page {0}", url);
            return this;
        }

        /// <summary>
        /// Methods for this HomePage
        /// </summary>
        public HomePage OpenHomePageAndMeasureTime()
        {
            var url = this.GetUrlValue();
            this.Driver.NavigateToAndMeasureTimeForAjaxFinished(new Uri(url));
            Logger.Info(CultureInfo.CurrentCulture, "Opening page {0}", url);
            return this;
        }

        public SearchResultsPage Search(string value)
        {
            this.Driver.GetElement(this.searchButton).Click();
            this.Driver.GetElement(this.searchTextbox).SendKeys(value);
            this.Driver.GetElement(this.searchButton).Click();
            
            return new SearchResultsPage(DriverContext);
        }

        public SearchResultsPage SearchUsingActions(string value)
        {
            var searchButtonElement = this.Driver.GetElement(this.searchButton);
            this.Driver.Actions().Click(searchButtonElement).Build().Perform();

            var searchTextboxElement = this.Driver.GetElement(this.searchTextbox);
            this.Driver.Actions().SendKeys(searchTextboxElement, value).Build().Perform();

            this.Driver.Actions().Click(searchButtonElement).Build().Perform();

            return new SearchResultsPage(this.DriverContext);
        }

        public string GetLinkText(params object[] parameters)
        {
            var element = this.Driver.GetElement(this.menuLink.Evaluate(parameters));
            return element.Text;
        }

        public int CountAllLinks()
        {
            var elements = this.Driver.GetElements(this.menuAllLinks);
            return elements.Count;
        }

        public int CountAllTechnologiesSubLinks()
        {
            this.Driver.GetElement(this.technologiesMenuLink).Click();

            var elements = this.Driver.GetElement(this.technologiesMenuLink);
            return elements.GetElements(new ElementLocator(Locator.XPath, "./../ul/li")).Count;
        }

        public TechnologiesBusinessPage ClickTechnologiesWebLink()
        {
            this.Driver.GetElement(this.technologiesMenuLink).Click();

            var elements = this.Driver.GetElement(this.technologiesMenuLink);
            elements.GetElement(new ElementLocator(Locator.XPath, "./../ul/li/a[@title='Web']")).Click();
            return new TechnologiesBusinessPage(this.DriverContext);
        }

        public bool CheckIfObjectivityLinkNotExistsonMsdnPage()
        {
            return this.Driver.IsElementPresent(this.objectivityMenuLink, BaseConfiguration.ShortTimeout);
        }

        public TechnologiesBusinessPage JavaScriptClickTechnologiesWebLink()
        {
            this.Driver.GetElement(this.technologiesMenuLink).JavaScriptClick();

            var elements = this.Driver.GetElement(this.technologiesMenuLink);
            elements.GetElement(new ElementLocator(Locator.XPath, "./../ul/li/a[@title='Web']")).JavaScriptClick();
            return new TechnologiesBusinessPage(this.DriverContext);
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
    }
}
