using NUnit.Framework;
namespace Objectivity.Test.Automation.Tests.Angular.Tests
{
    using Objectivity.Test.Automation.Tests.Angular.PageObjects;
    using Objectivity.Test.Automation.Tests.NUnit;

    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class AngularTestNunit : ProjectTestBase
    {
        [Test]
        public void AngularPageNavigationTest()
        {
            var protractorApiPage = new ProtractorHomePage(this.DriverContext)
                .OpenProtractorHomePage()
                .ClickQuickStart()
                .ClickTutorial()
                .ClickTableOfContents()
                .ClickProtractorApi()
                .ClickElementToBeSelected();

            Assert.True(protractorApiPage.IsElementToBeSelectedHeaderDisplayed(), "Header is not displayed.");
        }
    }
}
