namespace Objectivity.Test.Automation.Tests.Angular.PageObjects
{
    using System.Globalization;
    using NLog;
    using Objectivity.Test.Automation.Common.Types;
    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Tests.PageObjects;
    using System;

    public class TableOfContentsPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator
            ProtractorApi = new ElementLocator(Locator.CssSelector, "ul[class='ng-scope']>li>a[href='#/api']");

        public TableOfContentsPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public ProtractorApiPage ClickProtractorApi()
        {
            this.Driver.GetElement(this.ProtractorApi).Click();
            return new ProtractorApiPage(this.DriverContext);
        }
    }
}
