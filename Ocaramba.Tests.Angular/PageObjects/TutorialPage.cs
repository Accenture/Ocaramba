using Ocaramba.Tests.PageObjects;

namespace Ocaramba.Tests.Angular.PageObjects
{
    using NLog;
    using Ocaramba.Types;
    using Ocaramba;
    using Ocaramba.Extensions;

    public class TutorialPage : ProjectPageBase
    {
#if net47
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
#endif
#if netcoreapp2_2
        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
#endif

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator
            TableOfContents = new ElementLocator(Locator.CssSelector, "a[href*='toc']");

        public TutorialPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public TableOfContentsPage ClickTableOfContents()
        {
            this.Driver.GetElement(this.TableOfContents).Click();
            return new TableOfContentsPage(this.DriverContext);
        }
    }
}
