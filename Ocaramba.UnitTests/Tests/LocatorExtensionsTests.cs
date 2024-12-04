using NUnit.Framework;
using Ocaramba.Tests.PageObjects.PageObjects.TheInternet;

namespace Ocaramba.UnitTests.Tests
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

            Assert.That(columnAText, Is.EqualTo("A"));
        }

        [Test]
        public void ClassNameLocatorTest()
        {
            var titleByClassName = new InternetPage(DriverContext)
                .OpenHomePage()
                .GoToDragAndDropPage().GetByClassName;
#if net8_0
            if (BaseConfiguration.Env == "Linux")
            {
                Assert.That(titleByClassName, Is.EqualTo("Drag and Drop\nA\nB"));
            }
            else
            {
                Assert.That(titleByClassName, Is.EqualTo("Drag and Drop\r\nA\r\nB"));
            }
#endif
#if net47
             Assert.That(titleByClassName, Is.EqualTo("Drag and Drop\r\nA\r\nB"));
#endif
        }

        [Test]
        public void CssSelectorLocatorTest()
        {
            var titleByCssSelector = new InternetPage(DriverContext)
                .OpenHomePage()
                .GoToDragAndDropPage().GetByCssSelectorLocator;
#if net8_0
            if (BaseConfiguration.Env == "Linux")
            {
                Assert.That(titleByCssSelector, Is.EqualTo("Drag and Drop\nA\nB"));
            }
            else
            {
                Assert.That(titleByCssSelector, Is.EqualTo("Drag and Drop\r\nA\r\nB"));
            }
#endif
#if net47
            Assert.That(titleByCssSelector, Is.EqualTo("Drag and Drop\r\nA\r\nB"));
#endif
        }

        [Test]
        public void LinkTextLocatorTest()
        {
            var selectedOption = new InternetPage(DriverContext)
                .OpenHomePage()
                .GoToDropdownPageByLinkText().SelectedText;

            Assert.That(selectedOption, Is.EqualTo("Please select an option"));
        }

        [Test]
        public void NameLocatorTest()
        {
            var columnA = new InternetPage(DriverContext)
                .OpenHomePage()
                .GoToFormAuthenticationPage()
                .GetUsernameByNameLocator;
#if net8_0
            if (BaseConfiguration.Env == "Linux")
            {
                Assert.That(columnA, Is.EqualTo("Username\nPassword\nLogin"));
            }
            else
            {
                Assert.That(columnA, Is.EqualTo("Username\r\nPassword\r\nLogin"));
            }
#endif
#if net47
            Assert.That(columnA, Is.EqualTo("Username\r\nPassword\r\nLogin"));
#endif
        }


        [Test]
        public void PartialLinkTextLocatorTest()
        {
            var titleBypartialLinkText = new InternetPage(DriverContext)
                .OpenHomePage().GetDragAndDropLinkByPartialLinkText;
            Assert.That(titleBypartialLinkText, Is.EqualTo("Drag and Drop"));
        }

        [Test]
        public void TagNameLocatorTest()
        {
            var titleByTagName = new InternetPage(DriverContext)
                .OpenHomePage()
                .GoToTablesPage().GetByTagNameLocator;

            Assert.That(titleByTagName, Is.EqualTo("Last Name"));
        }

        [Test]
        public void XPathLocatorTest()
        {
            var linkByXPath = new InternetPage(DriverContext)
                .OpenHomePage()
                .GoToTablesPage().GetByXpathLocator;

            Assert.That(linkByXPath, Is.EqualTo("Last Name"));
        }
    }
}