## Test.Automation
<img align="left" src="/Objectivity.Test.Automation.Common.Documentation/icons/Objectivity_logo_avatar.png">

**C# Framework to automate tests using Selenium WebDriver**

[![Build status](https://ci.appveyor.com/api/projects/status/p8p3bndotyknv7rk/branch/master?svg=true)](https://ci.appveyor.com/project/ObjectivityAdminsTeam/test-automation/branch/master)
[![Build status](https://ci.appveyor.com/api/projects/status/p8p3bndotyknv7rk?svg=true)](https://ci.appveyor.com/project/ObjectivityAdminsTeam/test-automation)
[![Coverage Status](https://img.shields.io/coveralls/github/ObjectivityLtd/Test.Automation.svg)](https://coveralls.io/github/ObjectivityLtd/Test.Automation)
[![AppVeyor tests](https://img.shields.io/appveyor/tests/ObjectivityAdminsTeam/test-automation.svg)](https://ci.appveyor.com/project/ObjectivityAdminsTeam/test-automation/build/tests)
[![BrowserStack Status](https://www.browserstack.com/automate/badge.svg?badge_key=TmV0eWg4WElNTVBER2M2YWZSYVc2UjFCS2tJWjV4TUEwOFFpb0FXdGZVND0tLTJqRkVubVhnQWd2NHlISkFnMlBsM0E9PQ==--f3a8ace2e27c1ddf49487dd172e969dcacef037d)](https://www.browserstack.com/automate/public-build/TmV0eWg4WElNTVBER2M2YWZSYVc2UjFCS2tJWjV4TUEwOFFpb0FXdGZVND0tLTJqRkVubVhnQWd2NHlISkFnMlBsM0E9PQ==--f3a8ace2e27c1ddf49487dd172e969dcacef037d)

Test Framework was designed in Objectivity to propose common way how people should create Selenium WebDriver tests.

<img align="left" src="/Objectivity.Test.Automation.Common.Documentation/icons/Help.png">
Project API documentation can be found here: http://objectivityltd.github.io/Test.Automation<br /><br />

**It provides following features:**
- Ready for parallel tests execution, more details [here](https://github.com/ObjectivityLtd/Test.Automation/wiki/Selenium%20Parallel%20tests%20execution)
- Possibility to use MSTest, NUnit or xUNIT framework
- Specflow ready
- Written entirely in C#
- Contains example projects how to use it
- Allows to use Chrome, Firefox, Safari or Internet Explorer
- Overrides browser profile preferences, installs browser extensions, loading default firefox profile, more details [here](https://github.com/ObjectivityLtd/Test.Automation/wiki/Override%20browser%20profile%20preferences,%20install%20browser%20extensions)
- Extends Webdriver by additional methods like JavaScriptClick, WaitForAjax, WaitForAngular, etc., more details [here](http://objectivityltd.github.io/Test.Automation/html/0872cc4d-63cc-c3d2-30e5-1f8debf56860.htm)
- Automatically waits when locating element for specified time and conditions, more details [here](http://objectivityltd.github.io/Test.Automation/html/6b3a28a9-75c6-bda7-e44e-962f1e91c477.htm)
- Page Object Pattern
- Support for [SeleniumGrid](https://github.com/SeleniumHQ/selenium/wiki/Grid2), [SauceLab](https://saucelabs.com/) and [Browserstack](https://www.browserstack.com/) more details [here](https://github.com/ObjectivityLtd/Test.Automation/wiki/Selenium-Grid-support), Advanced-Browser-Capabilities more details [here](https://github.com/ObjectivityLtd/Test.Automation/wiki/Advanced-Browser-Capabilities)
- More common locators, e.g: ```"//*[@title='{0}' and @ms.title='{1}']"```, more details [here](https://github.com/ObjectivityLtd/Test.Automation/wiki/More%20common%20locators)
- Several methods to interact with kendo controls, more details [here](https://github.com/ObjectivityLtd/Test.Automation/wiki/Interact%20with%20kendo%20controls)
- Verify - asserts without stop tests, more details [here](https://github.com/ObjectivityLtd/Test.Automation/wiki/Verify-asserts-without-stop-tests)
- Measures average and 90 Percentile action times, more details [here](https://github.com/ObjectivityLtd/Test.Automation/wiki/Performance%20measures)
- DataDriven tests from Xml files for NUnit and MSTest with examples, more details [here](https://github.com/ObjectivityLtd/Test.Automation/wiki/DataDriven-tests-from-Xml-files)
- Possibility to take full desktop or browser screen shot, save page source, more details [here](https://github.com/ObjectivityLtd/Test.Automation/wiki/Screen-shots:-full-desktop,-selenium.-PageSource-saving)
- Logging with NLog, EventFiringWebDriver logs, more details [here](https://github.com/ObjectivityLtd/Test.Automation/wiki/Logging)
- Files downloading (Firefox, Chrome), more details [here](https://github.com/ObjectivityLtd/Test.Automation/wiki/Downloading%20files)
- Possibility to send [SQL](http://objectivityltd.github.io/Test.Automation/html/e339b346-66a4-70e6-4c54-f9c30cb3131a.htm) or [MDX](http://objectivityltd.github.io/Test.Automation/html/39ae874a-89a0-c435-c701-f20b26f1695e.htm) queries
- Possibility of debugging framework installed from nuget package, more details [here](https://github.com/ObjectivityLtd/Test.Automation/wiki/Debugging-Test.Automation-framework).
- AngularJS support, more details [here](https://github.com/ObjectivityLtd/Test.Automation/wiki/Angular-support).

For all documentation, visit the [Test.Automation Wiki](https://github.com/ObjectivityLtd/Test.Automation/wiki).

Projects examples of using Test Framework :
- Objectivity.Test.Automation.Tests.Angular for AngularJS
- Objectivity.Test.Automation.Tests.Features for Specflow
- Objectivity.Test.Automation.Tests.MsTest for MsTest
- Objectivity.Test.Automation.Tests.NUnit for NUnit
- Objectivity.Test.Automation.Tests.xUnit for xUnit
- Objectivity.Test.Automation.Tests.PageObjects for Page Object Pattern
- Objectivity.Test.Automation.Common.Documentation.shfbproj for building API documentation
- Objectivity.Test.Automation.UnitTests for unit test of framework

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
            this.Driver.GetElement(this.linkLocator.Format("javascript_alerts")).Click();
            return new JavaScriptAlertsPage(this.DriverContext);
        }
    }
}
```
		
#### Where to start?
-------------
- See [Getting started](https://github.com/ObjectivityLtd/Test.Automation/wiki/Getting%20started).

Checkout the code or get it from [nuget.org](https://www.nuget.org/packages?q=Objectivity.Test.Automation.Common)
- Objectivity.Test.Automation.Common.NUnit [![NuGet Badge](https://buildstats.info/nuget/Objectivity.Test.Automation.Common.NUnit)](https://www.nuget.org/packages/Objectivity.Test.Automation.Common.NUnit/)
- Objectivity.Test.Automation.Common.Features [![NuGet Badge](https://buildstats.info/nuget/Objectivity.Test.Automation.Common.Features)](https://www.nuget.org/packages/Objectivity.Test.Automation.Common.Features/)
- Objectivity.Test.Automation.Common.MsTest [![NuGet Badge](https://buildstats.info/nuget/Objectivity.Test.Automation.Common.MsTest)](https://www.nuget.org/packages/Objectivity.Test.Automation.Common.MsTest/)
- Objectivity.Test.Automation.Common.xUnit [![NuGet Badge](https://buildstats.info/nuget/Objectivity.Test.Automation.Common.xUnit)](https://www.nuget.org/packages/Objectivity.Test.Automation.Common.xUnit/)
