namespace Objectivity.Test.Automation.Tests.Angular.PageObjects
{
    using System.Globalization;
    using NLog;
    using Objectivity.Test.Automation.Common.Types;
    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Tests.PageObjects;
    using System;

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
