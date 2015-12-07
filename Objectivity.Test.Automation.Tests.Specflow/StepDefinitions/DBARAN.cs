using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;

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
        public void WhenIPress(string keybord)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"Valid ""(.*)"" is displayed")]
        public void ThenValidIsDisplayed(string p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
