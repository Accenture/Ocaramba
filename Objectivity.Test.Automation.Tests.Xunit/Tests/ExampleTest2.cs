using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.Kendo;
using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;
using Xunit;

namespace Objectivity.Test.Automation.Tests.xUnit.Tests
{
    public class ExampleTest2 : ProjectTestBase
    {
        [Fact]
        public void NestedFramesTest()
        {
            var nestedFramesPage = new InternetPage(DriverContext)
                .OpenHomePage()
                .GoToNestedFramesPage()
                .SwitchToFrame("frame-top");

            nestedFramesPage.SwitchToFrame("frame-left");
            Assert.Equal("LEFT", nestedFramesPage.LeftBody);

            nestedFramesPage.SwitchToParentFrame().SwitchToFrame("frame-middle");
            Assert.Equal("MIDDLE", nestedFramesPage.MiddleBody);

            nestedFramesPage.SwitchToParentFrame().SwitchToFrame("frame-right");
            Assert.Equal("RIGHT", nestedFramesPage.RightBody);

            nestedFramesPage.ReturnToDefaultContent().SwitchToFrame("frame-bottom");
            Assert.Equal("BOTTOM", nestedFramesPage.BottomBody);
        }

        [Fact]
        public void KendoOpenCloseComboboxTest()
        {
            var homePage = new KendoComboBoxPage(DriverContext);
            homePage.Open();

            homePage.OpenFabricComboBox();
            Assert.True(homePage.IsFabricComboBoxExpanded());

            homePage.CloseFabricComboBox();
            Assert.False(homePage.IsFabricComboBoxExpanded());
        }
    }
}
