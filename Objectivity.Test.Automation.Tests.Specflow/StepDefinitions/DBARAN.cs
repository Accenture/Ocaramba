using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;

namespace Objectivity.Test.Automation.Tests.Specflow.StepDefinitions
{
    using TechTalk.SpecFlow;

    [Binding]
    public class DBARAN
    {
        private readonly DriverContext driverContext;

        public DBARAN()
        {
            this.driverContext = ScenarioContext.Current["DriverContext"] as DriverContext;
        }

        [When(@"I press ""(.*)""")]
        public void WhenIPress(string key)
        {
            new KeyPressesPage(driverContext).PressDownKey(key);
        }

        [Given(@"I have page key page initialized")]
        public void GivenIHavePageKeyPageInitialized()
        {
            ScenarioContext.Current.Pending();
        }

        // [Then(@"Valid ""(.*)"" is displayed")]
        // public void ThenValidIsDisplayed(string validText)
        // {

        // ScenarioContext.Current.Pending();
        // }
    }
}
