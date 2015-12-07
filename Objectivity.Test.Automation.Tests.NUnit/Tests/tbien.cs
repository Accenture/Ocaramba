using NUnit.Framework;
using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;

namespace Objectivity.Test.Automation.Tests.NUnit.Tests
{
    public class Tbien : ProjectTestBase
    {
        [Test]
        public void SwitchToWindowTest()
        {
            const string pageTitle = "New Window";

            var newWindowPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToMultipleWindowsPage()
                .OpenNewWindowPage();

            Assert.True(newWindowPage.IsPageTile(pageTitle), "wrong page title, should be {0}", pageTitle);
            Assert.True(newWindowPage.IsNewWindowH3TextVisible(pageTitle), "text is not equal to {0}", pageTitle);
        }
    }
}
