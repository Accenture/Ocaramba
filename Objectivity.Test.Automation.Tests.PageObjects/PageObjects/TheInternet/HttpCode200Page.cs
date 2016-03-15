namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet
{
    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Types;

    public class HTTPCode200Page : ProjectPageBase
    {
        private readonly ElementLocator
            httpCode200Header = new ElementLocator(Locator.XPath, "//*[contains(text(),'This page returned a 200 status code.')]");

        public HTTPCode200Page(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public bool IsHTTPCode200PageIsDisplayed()
        {
            return this.Driver.IsElementPresent(this.httpCode200Header, BaseConfiguration.MediumTimeout);
        }
    }
}
