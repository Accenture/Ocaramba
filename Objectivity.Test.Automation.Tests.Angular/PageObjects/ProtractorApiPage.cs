using Ocaramba.Common;
using Ocaramba.Common.Extensions;
using Ocaramba.Common.Types;
using Ocaramba.Tests.PageObjects;

namespace Objectivity.Test.Automation.Tests.Angular.PageObjects
{
    using NLog;

    public class ProtractorApiPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator
            ElementToBeSelected = new ElementLocator(Locator.CssSelector, "a[href*='elementToBeSelected']"),
            ElementToBeSelectedHeader = new ElementLocator(Locator.XPath,"//h3[contains(text(),'ExpectedConditions.elementToBeSelected')]");

        public ProtractorApiPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public ProtractorApiPage ClickElementToBeSelected()
        {
            this.Driver.GetElement(this.ElementToBeSelected).Click();
            return new ProtractorApiPage(this.DriverContext);
        }

        public bool IsElementToBeSelectedHeaderDisplayed()
        {
            return this.Driver.GetElement(this.ElementToBeSelectedHeader).Displayed;
        }
    }
}
