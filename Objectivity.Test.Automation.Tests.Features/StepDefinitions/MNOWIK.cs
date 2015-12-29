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
        private readonly DriverContext driverContext;
        private readonly ScenarioContext scenarioContext;

        public MNOWIK(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null) throw new ArgumentNullException("scenarioContext");
            this.scenarioContext = scenarioContext;

            this.driverContext = this.scenarioContext["DriverContext"] as DriverContext;
        }

        [When(@"I get selected option")]
        public void WhenIGetSelectedOption()
        {
            var dropDownPage = this.scenarioContext.Get<DropdownPage>("DropdownPage");
            var selectedText = dropDownPage.SelectedText;
            this.scenarioContext.Set(selectedText, "SelectedText");
        }

        [When(@"I select option with text ""(.*)""")]
        public void WhenISelectOptionWithText(string text)
        {
            var dropDownPage = this.scenarioContext.Get<DropdownPage>("DropdownPage");
            dropDownPage.SelectByText(text);
        }

        [When(@"I select option with index '(.*)'")]
        public void WhenISelectOptionWithIndex(int index)
        {
            var dropDownPage = this.scenarioContext.Get<DropdownPage>("DropdownPage");
            dropDownPage.SelectByIndex(index);
        }

        [Then(@"Option with text ""(.*)"" is selected")]
        public void ThenOptionWithTextIsSelected(string expectedText)
        {
            var currentText = this.scenarioContext.Get<string>("SelectedText");
            Console.Out.WriteLine(currentText);
            Verify.That(this.driverContext, () => Assert.AreEqual(currentText, expectedText), false);
        }
    }
}
