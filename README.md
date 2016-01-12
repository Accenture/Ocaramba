# Test.Automation
Framework to automate tests using Selenium WebDriver

Test Framework was designed in Objectivity to propose common way how people should create Selenium WebDriver tests:

It provides following features:
- Possibility to Use MSTest or NUnit framework
- Specflow ready
- Written entirely in C#
- Contains example projects how to use it
- Allows to use Chrome, Firefox or Internet Explorer
- Extends Webdriver by additional methods like JavaScriptClick, WaitForAjax, etc.
- Automatically waits when locating element for specified time and conditions
- Page Object Pattern
- More common locators, e.g: "//*[@title='{0}' and @ms.title='{1}']"
- Several methods to interact with kendo controls
- Verify along with Assert
- Measures average and 90 Percentile action times
- DataDriven for NUnit and MSTest with examples 
- Possibility to take full desktop screen shot
- Logging with NLog
- Files downloading (Firefox, Chrome)
- Ready for parallel tests execution
- Possibility to send SQL or MDX queries

Projects examples of using Test Framework :
- Objectivity.Test.Automation.Tests.Features for Specflow
- Objectivity.Test.Automation.Tests.MsTest for MsTest
- Objectivity.Test.Automation.Tests.NUnit for NUnit
- Objectivity.Test.Automation.Tests.PageObjects for Page Object Pattern

In case of problems with running tests in your internet browser remember to update version of Selenium WebDriver from Nuget packages.
To run NUnit tests from Visual Studio remember to install NUnit TestAdapter.


NUnit Example Test:

```csharp
        [Test]
        public void SendKeysAndClickTest()
        {
            var loginPage = new HomePage(this.DriverContext)
                                 .OpenHomePage();

            var searchResultsPage = loginPage.Search("objectivity");
            searchResultsPage.MarkStackOverFlowFilter();

            var expectedPageTitle = searchResultsPage.GetPageTitle("Search Results");
            Assert.IsTrue(searchResultsPage.IsPageTitle(expectedPageTitle), "Search results page is not displayed");
        }
```

NUnit Example Page Object:

```csharp
        public class SearchResultsPage : ProjectPageBase
		{
			/// <summary>
			/// Locators for elements
			/// </summary>
			private readonly ElementLocator stackOverFlowCheckbox = new ElementLocator(Locator.Id, "500");

			public SearchResultsPage(DriverContext driverContext)
				: base(driverContext)
			{
			}

			/// <summary>
			/// Marks the stack over flow filter.
			/// </summary>
			public SearchResultsPage MarkStackOverFlowFilter()
			{
				var checkbox = this.Driver.GetElement<Checkbox>(this.stackOverFlowCheckbox);
				checkbox.TickCheckbox();
				return this;
			}
		}
```
		
#### Where to start?
-------------
- See [Getting started](https://github.com/ObjectivityBSS/Test.Automation/wiki/Getting%20started).

Checkout the code or get it from [nuget.org](https://www.nuget.org/packages?q=Objectivity.Test.Automation.Common+by?tfajks)
- Objectivity.Test.Automation.Common.NUnit [nuget](https://www.nuget.org/packages/Objectivity.Test.Automation.Common.NUnit/)
- Objectivity.Test.Automation.Common.Features [nuget](https://www.nuget.org/packages/Objectivity.Test.Automation.Common.Features/)
- Objectivity.Test.Automation.Common.MsTest [nuget](https://www.nuget.org/packages/Objectivity.Test.Automation.Common.MsTest/)

