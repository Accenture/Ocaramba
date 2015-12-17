namespace Objectivity.Test.Automation.Tests.MsTest.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;

    [TestClass]
    public class rszkudlarek : ProjectTestBase
    {
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
            new InternetPage(this.DriverContext).OpenHomePage().GoToFormAuthenticationPage();

            var formFormAuthentication = new FormAuthenticationPage(this.DriverContext);
            formFormAuthentication.EnterUserName((string)this.TestContext.DataRow["user"]);
            formFormAuthentication.EnterPassword((string)this.TestContext.DataRow["password"]);
            formFormAuthentication.LogOn();
            Verify.That(
                this.DriverContext,
                () => Assert.AreEqual((string)this.TestContext.DataRow["message"], formFormAuthentication.GetMessage));
        }
    }
}
