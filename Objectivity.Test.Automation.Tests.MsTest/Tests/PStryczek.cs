namespace Objectivity.Test.Automation.Tests.MSTest.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;

    /// <summary>
    /// Tests to verify checkboxes tick and Untick.
    /// </summary>
    [TestClass]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class PStryczek : ProjectTestBase
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
    }
}
