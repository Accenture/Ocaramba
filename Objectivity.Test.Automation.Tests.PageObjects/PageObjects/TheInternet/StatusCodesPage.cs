namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet
{
    using NLog;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Types;

    /// <summary>
    /// The status codes page object.
    /// </summary>
    public class StatusCodesPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator
            statusCodeHeader = new ElementLocator(Locator.XPath, "//h3[text()='Status Codes']");

        public StatusCodesPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public bool IsStatusCodesPageDisplayed()
        {
            Logger.Info("Check if Status Codes page is displayed.");
            return this.Driver.IsElementPresent(this.statusCodeHeader, BaseConfiguration.MediumTimeout);
        }

        public bool IsTextExistedInPageSource(string text)
        {
            return this.Driver.PageSourceContainsCase(text, BaseConfiguration.MediumTimeout, false);
        }
    }
}
