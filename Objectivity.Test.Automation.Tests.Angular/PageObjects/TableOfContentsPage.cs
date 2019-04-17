using Ocaramba.Tests.PageObjects;

namespace Objectivity.Test.Automation.Tests.Angular.PageObjects
{
    using NLog;
    using Ocaramba.Common.Types;
    using Ocaramba.Common;
    using Ocaramba.Common.Extensions;

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
