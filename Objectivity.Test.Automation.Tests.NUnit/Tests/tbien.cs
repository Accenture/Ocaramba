using NUnit.Framework;
using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;

namespace Objectivity.Test.Automation.Tests.NUnit.Tests
{
    public class Tbien : ProjectTestBase
    {
        [Test]
        public void MultipleWindowsTest()
        {
            const string pageTitle = "New Window";

            var newWindowPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToMultipleWindowsPage()
                .OpenNewWindowPage();

            Assert.True(newWindowPage.IsPageTile(pageTitle), "wrong page title, should be {0}", pageTitle);
            Assert.True(newWindowPage.IsNewWindowH3TextVisible(pageTitle), "text is not equal to {0}", pageTitle);
        }

        [Test]
        public void NestedFramesTest()
        {
            var nestedFramesPage = new InternetPage(DriverContext)
                .OpenHomePage()
                .GoToNestedFramesPage()
                .SwitchToFrame("frame-top");

            nestedFramesPage.SwitchToFrame("frame-left");
            Assert.AreEqual("LEFT", nestedFramesPage.LeftBody);

            nestedFramesPage.SwitchToParentFrame().SwitchToFrame("frame-middle");
            Assert.AreEqual("MIDDLE", nestedFramesPage.MiddleBody);

            nestedFramesPage.SwitchToParentFrame().SwitchToFrame("frame-right");
            Assert.AreEqual("RIGHT", nestedFramesPage.RightBody);

            nestedFramesPage.ReturnToDefaultContent().SwitchToFrame("frame-bottom");
            Assert.AreEqual("BOTTOM", nestedFramesPage.BottomBody);
        }
    }
}
