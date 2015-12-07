using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
using Objectivity.Test.Automation.Common.Types;

namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet
{
    public class MultipleWindowsPage : ProjectPageBase
    {
        private readonly ElementLocator
    clickHerePageLocator = new ElementLocator(Locator.CssSelector, "a[href='/windows/new']");


        
        public MultipleWindowsPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public NewWindowPage OpenNewWindowPage()
        {
            this.Driver.GetElement(this.clickHerePageLocator).Click();
            Driver.SwitchToWindowUsingUrl(new Uri("http://the-internet.herokuapp.com/windows/new"), 5);
            return new NewWindowPage(this.DriverContext);
        }
    }
}
