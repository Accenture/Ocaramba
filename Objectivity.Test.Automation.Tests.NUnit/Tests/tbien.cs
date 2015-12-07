using NUnit.Framework;
using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;

namespace Objectivity.Test.Automation.Tests.NUnit.Tests
{
    public class Tbien : ProjectTestBase
    {
        [Test]
        public void SwitchToWindowTest()
        {
            var newWindowPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToMultipleWindowsPage()
                .OpenNewWindowPage();

            Assert.True(newWindowPage.IsNewWindowH3TextVisible("New Window"), "text is not equal to {0}", "New Window");
        }
    }
}
