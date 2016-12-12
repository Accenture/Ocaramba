namespace Objectivity.Test.Automation.Tests.Angular.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics.CodeAnalysis;
    using Objectivity.Test.Automation.Tests.Angular.PageObjects;

    /// <summary>
    /// Tests to verify checkboxes tick and Untick.
    /// </summary>
    [TestClass]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class AngularMsTest : ProjectTestBase
    {
        [TestMethod]
        public void AngularTest()
        {
            new AngularStartPage(this.DriverContext)
                .OpenHomePage();
        }
    }
}
