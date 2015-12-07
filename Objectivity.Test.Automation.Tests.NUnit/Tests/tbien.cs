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
            Assert.AreEqual("New Window", newWindowPage.NewWindowH3Text);
        }
    }
}
