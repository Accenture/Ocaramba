// <copyright file="CommonSteps.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.Tests.Features.StepDefinitions
{
    using System;
    using System.Globalization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;
    using TechTalk.SpecFlow;

    [Binding]
    public class CommonSteps
    {
        private readonly DriverContext driverContext;
        private readonly ScenarioContext scenarioContext;

        public CommonSteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null)
            {
                throw new ArgumentNullException("scenarioContext");
            }

            this.scenarioContext = scenarioContext;

            this.driverContext = this.scenarioContext["DriverContext"] as DriverContext;
        }

        [Given(@"Default page is opened")]
        public void GivenDefaultPageIsOpened()
        {
            new InternetPage(this.driverContext).OpenHomePage();
        }

        [When(@"I click ""(.*)"" link")]
        public void WhenIClickLink(string nameOfThePage)
        {
            new InternetPage(this.driverContext).GoToPage(nameOfThePage);
        }

        [When(@"I see page Dropdown List")]
        public void WhenISeePageDropdownList()
        {
            var page = new DropdownPage(this.driverContext);
            this.scenarioContext.Set(page, "DropdownPage");
        }

        [When(@"I check selected option")]
        public void WhenICheckSelectedOption()
        {
            var dropDownPage = this.scenarioContext.Get<DropdownPage>("DropdownPage");
            var selectedText = dropDownPage.SelectedText;
            this.scenarioContext.Set(selectedText, "SelectedText");
        }

        [When(@"I press ""(.*)""")]
        public void WhenIPress(string key)
        {
            new KeyPressesPage(this.driverContext).SendKeyboardKey(key);
        }

        [When(@"I select option with text ""(.*)""")]
        public void WhenISelectOptionWithText(string text)
        {
            var dropDownPage = this.scenarioContext.Get<DropdownPage>("DropdownPage");
            dropDownPage.SelectByText(text);
        }

        [When(@"I select option with custom timeout '(.*)' with index '(.*)'")]
        public void WhenISelectOptionWithIndex(int timeout, int index)
        {
            var dropDownPage = this.scenarioContext.Get<DropdownPage>("DropdownPage");
            dropDownPage.SelectByIndexWithCustomTimeout(index, timeout);
        }

        [When(@"I select option with index '(.*)'")]
        public void WhenISelectOptionWithIndex(int index)
        {
            var dropDownPage = this.scenarioContext.Get<DropdownPage>("DropdownPage");
            dropDownPage.SelectByIndex(index);
        }

        [When(@"I select option with value '(.*)'")]
        public void WhenISelectOptionWithValue(string value)
        {
            var dropDownPage = this.scenarioContext.Get<DropdownPage>("DropdownPage");
            dropDownPage.SelectByValue(value);
        }

        [When(@"I select option with custom timeout '(.*)' with value '(.*)'")]
        public void WhenISelectOptionWithValue(int timeout, string value)
        {
            var dropDownPage = this.scenarioContext.Get<DropdownPage>("DropdownPage");
            dropDownPage.SelectByValueWithCustomTimeout(value, timeout);
        }

        [Then(@"Option with text ""(.*)"" is selected")]
        public void ThenOptionWithTextIsSelected(string expectedText)
        {
            var currentText = this.scenarioContext.Get<string>("SelectedText");
            Console.Out.WriteLine(currentText);
            Verify.That(this.driverContext, () => Assert.AreEqual(currentText, expectedText), false, false);
        }

        [Then(@"Valid ""(.*)"" is displayed")]
        public void ThenValidIsDisplayed(string expectedMessage)
        {
            var isElementPresent = new KeyPressesPage(this.driverContext).IsResultElementPresent;
            Verify.That(this.driverContext, () => Assert.IsTrue(isElementPresent, "Results element does not exist for unclicked key"), false, false);

            expectedMessage = string.Format(CultureInfo.CurrentCulture, "You entered: {0}", expectedMessage);
            var resultText = new KeyPressesPage(this.driverContext).ResultText;
            Verify.That(this.driverContext, () => Assert.AreEqual(resultText, expectedMessage), false, false);
        }

        [Then(@"Results element is not displayed")]
        public void ThenResultsElementIsNotDisplayed()
        {
            var isElementPresent = new KeyPressesPage(this.driverContext).IsResultElementPresent;
            Verify.That(this.driverContext, () => Assert.IsFalse(isElementPresent, "Results element exists for unclicked key"), false, false);
        }
    }
}
