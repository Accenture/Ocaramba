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

namespace Objectivity.Test.Automation.Features.StepDefinitions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Features.PageObjects;

    using TechTalk.SpecFlow;

    [Binding]
    public class CommonSteps
    {
        private readonly DriverContext driverContext;

        public CommonSteps()
        {
            this.driverContext = ScenarioContext.Current["DriverContext"] as DriverContext;
        }

        [Given(@"I log on and default page is opened")]
        public void GivenILogOnAndDefaultPageIsOpened()
        {
            new HomePage(this.driverContext).OpenHomePage();
        }

        [Given(@"I search for ""(.*)""")]
        public void GivenISearchFor(string searchedValue)
        {
            new HomePage(this.driverContext).Search(searchedValue);
        }

        [Then(@"I should be on ""(.*)"" page")]
        public void ThenIShouldBeOnPage(string pageName)
        {
            var searchResultsPage = new SearchResultsPage(this.driverContext);
            Assert.IsTrue(searchResultsPage.IsPageTitle(pageName));
        }
    }
}
