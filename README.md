## Test.Automation
**C# Framework to automate tests using Selenium WebDriver**

Test Framework was designed in Objectivity to propose common way how people should create Selenium WebDriver tests.

Project API documentation can be found here: http://objectivitybss.github.io/Test.Automation

It provides following features:
- Possibility to use MSTest, NUnit or xUNIT framework
- Specflow ready
- Written entirely in C#
- Contains example projects how to use it
- Allows to use Chrome, Firefox or Internet Explorer
- Overrides browser profile preferences, installs browser extensions, more details [here](https://github.com/ObjectivityBSS/Test.Automation/wiki/Override%20browser%20profile%20preferences,%20install%20browser%20extensions)
- Extends Webdriver by additional methods like JavaScriptClick, WaitForAjax, WaitForAngular, etc.
- Automatically waits when locating element for specified time and conditions
- Page Object Pattern
- More common locators, e.g: ```"//*[@title='{0}' and @ms.title='{1}']"```
- Several methods to interact with kendo controls
- Verify - asserts without stop tests, more details [here](https://github.com/ObjectivityBSS/Test.Automation/wiki/Verify-asserts-without-stop-tests)
- Measures average and 90 Percentile action times
- DataDriven tests from Xml files for NUnit and MSTest with examples, more details [here](https://github.com/ObjectivityBSS/Test.Automation/wiki/DataDriven-tests-from-Xml-files)
- Possibility to take full desktop or browser screen shot, save page source, , more details [here](https://github.com/ObjectivityBSS/Test.Automation/wiki/Screen-shots:-full-desktop,-selenium.-PageSource-saving)
- Logging with NLog, more details [here](https://github.com/ObjectivityBSS/Test.Automation/wiki/Logging)
- Files downloading (Firefox, Chrome), more details [here](https://github.com/ObjectivityBSS/Test.Automation/wiki/Downloading%20files)
- Ready for parallel tests execution, more details [here](https://github.com/ObjectivityBSS/Test.Automation/wiki/Selenium%20Parallel%20tests%20execution)
- Possibility to send SQL or MDX queries
- Possibility of debugging framework installed from nuget package, more details [here](https://github.com/ObjectivityBSS/Test.Automation/wiki/Debugging-Test.Automation-framework).

Projects examples of using Test Framework :
- Objectivity.Test.Automation.Tests.Features for Specflow
- Objectivity.Test.Automation.Tests.MsTest for MsTest
- Objectivity.Test.Automation.Tests.NUnit for NUnit
- Objectivity.Test.Automation.Tests.xUnit for xUnit
- Objectivity.Test.Automation.Tests.PageObjects for Page Object Pattern
- Objectivity.Test.Automation.Common.Documentation.shfbproj for building API documentation

NUnit Example Test:

```csharp
namespace Objectivity.Test.Automation.Tests.NUnit.Tests
{
    using global::NUnit.Framework;

    using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;

    [Parallelizable(ParallelScope.Fixtures)]
    public class JavaScriptAlertsTestsNUnit : ProjectTestBase
    {
        [Test]
        public void ClickJsAlertTest()
        {
            var internetPage = new InternetPage(this.DriverContext).OpenHomePage();
            var jsAlertsPage = internetPage.GoToJavaScriptAlerts();
            jsAlertsPage.OpenJsAlert();
            jsAlertsPage.AcceptAlert();
            Assert.AreEqual("You successfuly clicked an alert", jsAlertsPage.ResultText);
        }
    }
}

```

NUnit Example Page Object:

```csharp
namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet
{
    using System;
    using System.Globalization;

    using NLog;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Types;
    using Objectivity.Test.Automation.Tests.PageObjects;

    public class InternetPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator
            linkLocator = new ElementLocator(Locator.CssSelector, "a[href='/{0}']");

        public InternetPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public JavaScriptAlertsPage GoToJavaScriptAlerts()
        {
            this.Driver.GetElement(this.linkLocator.Evaluate("javascript_alerts")).Click();
            return new JavaScriptAlertsPage(this.DriverContext);
        }
    }
}
```
		
#### Where to start?
-------------
- See [Getting started](https://github.com/ObjectivityBSS/Test.Automation/wiki/Getting%20started).

Checkout the code or get it from [nuget.org](https://www.nuget.org/packages?q=Objectivity.Test.Automation.Common)
- Objectivity.Test.Automation.Common.NUnit [nuget](https://www.nuget.org/packages/Objectivity.Test.Automation.Common.NUnit/)
- Objectivity.Test.Automation.Common.Features [nuget](https://www.nuget.org/packages/Objectivity.Test.Automation.Common.Features/)
- Objectivity.Test.Automation.Common.MsTest [nuget](https://www.nuget.org/packages/Objectivity.Test.Automation.Common.MsTest/)
- Objectivity.Test.Automation.Common.xUnit [nuget](https://www.nuget.org/packages/Objectivity.Test.Automation.Common.xUnit/)
