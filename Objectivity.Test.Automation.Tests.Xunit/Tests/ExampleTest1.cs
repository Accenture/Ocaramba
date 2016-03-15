using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;
using Xunit;

namespace Objectivity.Test.Automation.Tests.xUnit.Tests
{
    public class ExampleTest1 : ProjectTestBase
    {
        [Fact]
        public void TestMethod1()
        {
            const string PageTitle = "New Window";

            var newWindowPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToMultipleWindowsPage()
                .OpenNewWindowPage();

            Assert.True(newWindowPage.IsPageTile(PageTitle));
            Assert.True(newWindowPage.IsNewWindowH3TextVisible(PageTitle));
        }

        [Fact]
        public void ContextMenuTest()
        {
            const string H3Value = "Context Menu";
            var browser = BaseConfiguration.TestBrowser;
            if (browser.Equals(BrowserType.Firefox))
            {
                var contextMenuPage = new InternetPage(this.DriverContext)
                    .OpenHomePage()
                    .GoToContextMenuPage()
                    .SelectTheInternetOptionFromContextMenu();

                Assert.Equal("You selected a context menu", contextMenuPage.JavaScriptText);
                Assert.True(contextMenuPage
                    .ConfirmJavaScript()
                    .IsH3ElementEqualsToExpected(H3Value));
            }
        }
    }
}
