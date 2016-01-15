using System;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.Kendo;
using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;
using Objectivity.Test.Automation.Tests.XUnit;
using Xunit;


namespace Objectivity.Test.Automation.Tests.XUnit.Tests
{

    public class ExampleTest1 : ProjectTestBase
    {

        [Fact]
        public void TestMethod1()
        {
            const string pageTitle = "New Window";

            var newWindowPage = new InternetPage(DriverContext)
                .OpenHomePage()
                .GoToMultipleWindowsPage()
                .OpenNewWindowPage();

            Assert.True(newWindowPage.IsPageTile(pageTitle));
            Assert.True(newWindowPage.IsNewWindowH3TextVisible(pageTitle));
        }

        [Fact]
        public void ContextMenuTest()
        {
            const string h3Value = "Context Menu";
            var browser = BaseConfiguration.TestBrowser;
            if (browser.Equals(DriverContext.BrowserType.Firefox))
            {
                var contextMenuPage = new InternetPage(DriverContext)
                    .OpenHomePage()
                    .GoToContextMenuPage()
                    .SelectTheInternetOptionFromContextMenu();

                Assert.Equal("You selected a context menu", contextMenuPage.JavaScriptText);
                Assert.True(contextMenuPage
                    .ConfirmJavaScript()
                    .IsH3ElementEqualsToExpected(h3Value));
            }
        }
    }
}


