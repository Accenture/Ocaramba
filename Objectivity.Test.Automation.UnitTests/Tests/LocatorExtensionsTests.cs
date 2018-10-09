using NUnit.Framework;
using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;

namespace Objectivity.Test.Automation.UnitTests.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]

    public class LocatorExtensionsTests : ProjectTestBase
    {
        [Test]
        public void IdLocatorTest()
        {
            var columnAText = new InternetPage(DriverContext)
                .OpenHomePage()
                .GoToDragAndDropPage()
                .GetByIdLocator;

            Assert.AreEqual("A", columnAText);
        }

        [Test]
        public void ClassNameLocatorTest()
        {
            var titleByClassName = new InternetPage(DriverContext)
                .OpenHomePage()
                .GoToDragAndDropPage().GetByClassName;

            Assert.AreEqual("Drag and Drop\r\nA\r\nB", titleByClassName);
        }

        [Test]
        public void CssSelectorLocatorTest()
        {
            var titleByCssSelector = new InternetPage(DriverContext)
                .OpenHomePage()
                .GoToDragAndDropPage().GetByCssSelectorLocator;
            
            Assert.AreEqual("Drag and Drop\r\nA\r\nB", titleByCssSelector);
        }

        [Test]
        public void LinkTextLocatorTest()
        {
            var selectedOption = new InternetPage(DriverContext)
                .OpenHomePage()
                .GoToDropdownPageByLinkText().SelectedText;

            Assert.AreEqual("Please select an option", selectedOption);
        }

        [Test]
        public void NameLocatorTest()
        {
            var columnA = new InternetPage(DriverContext)
                .OpenHomePage()
                .GoToFormAuthenticationPage()
                .GetUsernameByNameLocator;

            Assert.AreEqual("Username\r\nPassword\r\nLogin", columnA);
        }


        [Test]
        public void PartialLinkTextLocatorTest()
        {
            var titleBypartialLinkText = new InternetPage(DriverContext)
                .OpenHomePage().GetDragAndDropLinkByPartialLinkText;
            Assert.AreEqual("Drag and Drop", titleBypartialLinkText);
        }

        [Test]
        public void TagNameLocatorTest()
        {
            var titleByTagName = new InternetPage(DriverContext)
                .OpenHomePage()
                .GoToTablesPage().GetByTagNameLocator;

            Assert.AreEqual("Last Name", titleByTagName);
        }

        [Test]
        public void XPathLocatorTest()
        {
            var linkByXPath = new InternetPage(DriverContext)
                .OpenHomePage()
                .GoToTablesPage().GetByXpathLocator;

            Assert.AreEqual("Last Name", linkByXPath);
        }
    }
}