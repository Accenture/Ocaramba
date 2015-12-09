namespace Objectivity.Test.Automation.Tests.Features.StepDefinitions
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class MNOWIK
    {
        [When(@"I get selected option")]
        public void WhenIGetSelectedOption()
        {
            var dropDownPage = ScenarioContext.Current.Get<DropdownPage>("DropdownPage");
            var selectedText = dropDownPage.SelectedText;
            ScenarioContext.Current.Set(selectedText, "SelectedText");
        }

        [When(@"I select option with text ""(.*)""")]
        public void WhenISelectOptionWithText(string text)
        {
            var dropDownPage = ScenarioContext.Current.Get<DropdownPage>("DropdownPage");
            dropDownPage.SelectByText(text);
        }

        [When(@"I select option with index '(.*)'")]
        public void WhenISelectOptionWithIndex(int index)
        {
            var dropDownPage = ScenarioContext.Current.Get<DropdownPage>("DropdownPage");
            dropDownPage.SelectByIndex(index);
        }

        [Then(@"Option with text ""(.*)"" is selected")]
        public void ThenOptionWithTextIsSelected(string expectedText)
        {
            var currentText = ScenarioContext.Current.Get<string>("SelectedText");
            Console.Out.WriteLine(currentText);
            var driverContext = ScenarioContext.Current["DriverContext"] as DriverContext;
            Verify.That(driverContext, () => Assert.AreEqual(currentText, expectedText), false);
        }
    }
}
