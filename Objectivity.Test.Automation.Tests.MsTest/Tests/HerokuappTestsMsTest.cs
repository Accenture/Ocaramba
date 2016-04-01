namespace Objectivity.Test.Automation.Tests.MsTest.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;

    /// <summary>
    /// Tests to verify checkboxes tick and Untick.
    /// </summary>
    [TestClass]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class HerokuappTestsMsTest : ProjectTestBase
    {
        [TestMethod]
        public void TickCheckboxTest()
        {
            var checkboxes = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToCheckboxesPage()
                .TickCheckboxOne();

            Assert.IsTrue(checkboxes.IsCheckmarkOneTicked, "Checkbox1 is unticked!");
        }

        [TestMethod]
        public void UnTickCheckboxTest()
        {
            var checkboxes = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToCheckboxesPage()
                .UnTickCheckboxTwo();

            Assert.IsFalse(checkboxes.IsCheckmarkTwoTicked, "Checkbox2 is ticked!");
        }

        [TestMethod]
        public void PageSourceContainsCaseTest()
        {
            const string ExpectedText = "HTTP status codes are a standard set of numbers used to communicate from a web server";
            var statusCodes = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToStatusCodesPage();

            Assert.IsTrue(statusCodes.IsTextExistedInPageSource(ExpectedText), "Text is not present!");
        }

        [TestMethod]
        public void NavigateToUrlTest()
        {
            this.DriverContext.Driver.NavigateTo(new Uri("http://the-internet.herokuapp.com/status_codes"));
            var statusCodes = new StatusCodesPage(this.DriverContext);

            Assert.IsTrue(statusCodes.IsStatusCodesPageDisplayed(), "Status codes page is not displayed.");
        }

        [TestMethod]
        public void JavaScriptClickTest()
        {
            HTTPCode200Page httpCode200 = new InternetPage(this.DriverContext)
               .OpenHomePage()
               .GoToStatusCodesPage()
               .Click200();

            Assert.IsTrue(httpCode200.IsHTTPCode200PageIsDisplayed(), "Code 200 was not clicked or page is not displayed.");
        }

        [TestMethod]
        public void ClickFloatingMenuTest()
        {
            new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToFloatingMenu()
                .ClickFloatingMenuButton();
            Assert.IsTrue(this.DriverContext.Driver.Url.EndsWith("#home", StringComparison.CurrentCulture), "URL does not end with #home - probably 'Home' floating menu button was not clicked properly");
        }

        [DeploymentItem("Objectivity.Test.Automation.MsTests\\DDT.xml"),
           DeploymentItem("Objectivity.Test.Automation.MsTests\\IEDriverServer.exe"),
           DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
           "|DataDirectory|\\DDT.xml", "credential",
           DataAccessMethod.Sequential), TestMethod]
        public void FormAuthenticationPageTest()
        {
            var formFormAuthentication = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToFormAuthenticationPage();

            formFormAuthentication.EnterUserName((string)this.TestContext.DataRow["user"]);
            formFormAuthentication.EnterPassword((string)this.TestContext.DataRow["password"]);
            formFormAuthentication.LogOn();
            Verify.That(
                this.DriverContext,
                () => Assert.AreEqual((string)this.TestContext.DataRow["message"], formFormAuthentication.GetMessage));
        }

        [TestMethod]
        public void VerifyTest()
        {
            Verify.That(this.DriverContext, () => Assert.AreEqual(1, 1), () => Assert.AreEqual(2, 2), () => Assert.AreEqual(3, 3));
            Verify.That(this.DriverContext, () => Assert.IsFalse(false), enableScreenShot: true, enableSavePageSource: true);
            Verify.That(this.DriverContext, () => Assert.IsTrue(true));
        }

        [TestMethod]
        public void ToByTest()
        {
            string expectedDescription = @"Also known as split testing. This is a way in which businesses are able to simultaneously test and learn different versions of a page to see which text and/or functionality works best towards a desired outcome (e.g. a user action such as a click-through).";

            new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToPage("abtest");

            var abeTestingPage = new AbTestingPage(this.DriverContext);
            Assert.AreEqual(expectedDescription, abeTestingPage.GetDescriptionUsingBy);
        }

        [TestMethod]
        public void GetElementTest()
        {
            string expectedDescription = @"Also known as split testing. This is a way in which businesses are able to simultaneously test and learn different versions of a page to see which text and/or functionality works best towards a desired outcome (e.g. a user action such as a click-through).";

            new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToPage("abtest");

            var abeTestingPage = new AbTestingPage(this.DriverContext);
            Assert.AreEqual(expectedDescription, abeTestingPage.GetDescription);
        }

        [TestMethod]
        public void GetElementTestWithCustomTimeoutTest()
        {
            string expectedDescription = @"Also known as split testing. This is a way in which businesses are able to simultaneously test and learn different versions of a page to see which text and/or functionality works best towards a desired outcome (e.g. a user action such as a click-through).";

            new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToPage("abtest");

            var abeTestingPage = new AbTestingPage(this.DriverContext);
            Assert.AreEqual(expectedDescription, abeTestingPage.GetDescriptionWithCustomTimeout);
        }

        [TestMethod]
        public void WaitElementDissapearsTest()
        {
            new InternetPage(this.DriverContext)
                 .OpenHomePage()
                 .GoToPage("disappearing_elements");

            var disappearingElements = new DisappearingElementsPage(this.DriverContext);
            disappearingElements.RefreshAndWaitLinkNotVisible("NotExistingLink");
        }

        [TestMethod]
        public void GetElementWithCustomConditionTest()
        {
            new InternetPage(this.DriverContext)
                 .OpenHomePage()
                 .GoToPage("disappearing_elements");

            var disappearingElementsPage = new DisappearingElementsPage(this.DriverContext);
            var currentTagName = disappearingElementsPage.GetLinkTitleTagName("Home");
            Assert.AreEqual("a", currentTagName);
        }

        [TestMethod]
        public void GetElementWithCustomTimeoutAndConditionTest()
        {
            new InternetPage(this.DriverContext)
                 .OpenHomePage()
                 .GoToPage("disappearing_elements");

            var disappearingElementsPage = new DisappearingElementsPage(this.DriverContext);
            var currentIsSelected = disappearingElementsPage.IsLinkSelected("Home");
            Assert.AreEqual(false, currentIsSelected);
        }
    }
}
