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
        private static Logger logger = LogManager.GetCurrentClassLogger();

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

        /// <summary>
        /// Methods for this HomePage
        /// </summary>
        public HomePage OpenHomePage()
        {
            var url = this.GetUrlValue();
            this.Browser.NavigateTo(new Uri(url));
            logger.Info(CultureInfo.CurrentCulture, "Opening page {0}", url);
            return this;
        }

        /// <summary>
        /// Methods for this HomePage
        /// </summary>
        public HomePage OpenHomePageAndMeasureTime()
        {
            var url = this.GetUrlValue();
            this.Browser.NavigateToAndMeasureTimeForAjaxFinished(new Uri(url));
            logger.Info(CultureInfo.CurrentCulture, "Opening page {0}", url);
            return this;
        }

        public SearchResultsPage Search(string value)
        {
            this.Browser.GetElement(this.searchButton).Click();
            this.Browser.GetElement(this.searchTextbox).SendKeys(value);
            this.Browser.GetElement(this.searchButton).Click();
            
            return Pages.Create<SearchResultsPage>();
        }

        public SearchResultsPage SearchUsingActions(string value)
        {
            var searchButtonElement = this.Browser.GetElement(this.searchButton);
            this.Browser.Actions().Click(searchButtonElement).Build().Perform();

            var searchTextboxElement = this.Browser.GetElement(this.searchTextbox);
            this.Browser.Actions().SendKeys(searchTextboxElement, value).Build().Perform();

            this.Browser.Actions().Click(searchButtonElement).Build().Perform();

            return Pages.Create<SearchResultsPage>();
        }

        public string GetLinkText(params object[] parameters)
        {
            var element = this.Browser.GetElement(this.menuLink.Evaluate(parameters));
            return element.Text;
        }

        public int CountAllLinks()
        {
            var elements = this.Browser.GetElements(this.menuAllLinks);
            return elements.Count;
        }

        public int CountAllTechnologiesSubLinks()
        {
            this.Browser.GetElement(this.technologiesMenuLink).Click();

            var elements = this.Browser.GetElement(this.technologiesMenuLink);
            return elements.GetElements(new ElementLocator(Locator.XPath, "./../ul/li")).Count;
        }

        public TechnologiesBusinessPage ClickTechnologiesWebLink()
        {
            this.Browser.GetElement(this.technologiesMenuLink).Click();

            var elements = this.Browser.GetElement(this.technologiesMenuLink);
            elements.GetElement(new ElementLocator(Locator.XPath, "./../ul/li/a[@title='Web']")).Click();
            return Pages.Create<TechnologiesBusinessPage>();
        }


        public bool CheckIfObjectivityLinkNotExistsonMSDNPage()
        {
            return this.Browser.IsElementPresent(this.objectivityMenuLink, 2);
        }

        public TechnologiesBusinessPage JavaScriptClickTechnologiesWebLink()
        {
            this.Browser.GetElement(this.technologiesMenuLink).JavaScriptClick();

            var elements = this.Browser.GetElement(this.technologiesMenuLink);
            elements.GetElement(new ElementLocator(Locator.XPath, "./../ul/li/a[@title='Web']")).JavaScriptClick();
            return Pages.Create<TechnologiesBusinessPage>();
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
