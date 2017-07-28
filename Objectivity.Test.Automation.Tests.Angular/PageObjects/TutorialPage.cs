namespace Objectivity.Test.Automation.Tests.Angular.PageObjects
{
    using System.Globalization;
    using NLog;
    using Objectivity.Test.Automation.Common.Types;
    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Tests.PageObjects;
    using System;

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
