﻿using Ocaramba.Extensions;
using Ocaramba.Types;
using Ocaramba.Tests.PageObjects;
using NLog;

namespace Ocaramba.Tests.Angular.PageObjects
{
    public class ProtractorApiPage : ProjectPageBase
    {


          private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();


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
