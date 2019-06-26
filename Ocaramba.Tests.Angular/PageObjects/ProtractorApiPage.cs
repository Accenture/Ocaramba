using Ocaramba;
using Ocaramba.Extensions;
using Ocaramba.Types;
using Ocaramba.Tests.PageObjects;

namespace Ocaramba.Tests.Angular.PageObjects
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
            ElementToBeSelectedHeader = new ElementLocator(Locator.XPath, "//h3[@class='api-title ng-binding'][contains(text(),'ExpectedConditions.elementToBeSelected')]");

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
