using Ocaramba.Tests.PageObjects;

namespace Ocaramba.Tests.Angular.PageObjects
{
    using NLog;
    using Ocaramba.Types;
    using Ocaramba;
    using Ocaramba.Extensions;

    public class TutorialPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

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
