using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objectivity.Test.Automation.Tests.MSTest.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Assert.IsTrue(DriverContext.Driver.Url.EndsWith("#home", StringComparison.CurrentCulture), "URL does not end with #home - probably 'Home' floating menu button was not clicked properly");
        }
    }
}
