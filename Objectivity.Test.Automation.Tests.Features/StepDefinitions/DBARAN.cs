namespace Objectivity.Test.Automation.Tests.Features.StepDefinitions
{
    using System;
    using System.Globalization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;

    using TechTalk.SpecFlow;

    [Binding]
    public class DBARAN
    {
        private readonly DriverContext driverContext;
        private readonly ScenarioContext scenarioContext;

        public DBARAN(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null) throw new ArgumentNullException("scenarioContext");
            this.scenarioContext = scenarioContext;

            this.driverContext = this.scenarioContext["DriverContext"] as DriverContext;
        }

        [When(@"I press ""(.*)""")]
        public void WhenIPress(string key)
        {
            new KeyPressesPage(this.driverContext).SendKeyboardKey(key);
        }

        [Then(@"Valid ""(.*)"" is displayed")]
        public void ThenValidIsDisplayed(string expectedMessage)
        {
            var isElementPresent = new KeyPressesPage(this.driverContext).IsResultElementPresent;
            Verify.That(this.driverContext, () => Assert.IsTrue(isElementPresent, "Results element does not exist for unclicked key"), false);

            expectedMessage = string.Format(CultureInfo.CurrentCulture, "You entered: {0}", expectedMessage);
            var resultText = new KeyPressesPage(this.driverContext).ResultText;
            Verify.That(this.driverContext, () => Assert.AreEqual(resultText, expectedMessage), false);
        }

        [Then(@"Results element is not displayed")]
        public void ThenResultsElementIsNotDisplayed()
        {
            var isElementPresent = new KeyPressesPage(this.driverContext).IsResultElementPresent;
            Verify.That(this.driverContext, () => Assert.IsFalse(isElementPresent, "Results element exists for unclicked key"), false);
        }
    }
}
