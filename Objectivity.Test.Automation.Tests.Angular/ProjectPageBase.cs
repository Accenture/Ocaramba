namespace Objectivity.Test.Automation.Tests.Angular
{
    using Common;
    using Protractor;
    public partial class ProjectPageBase
    {
        public ProjectPageBase(DriverContext driverContext)
        {
            this.DriverContext = driverContext;
            this.Driver = driverContext.Driver;
        }

        protected NgWebDriver Driver { get; set; }

        protected DriverContext DriverContext { get; set; }
    }
}
