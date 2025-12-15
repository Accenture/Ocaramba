using Ocaramba;
using Ocaramba.Extensions;
using Ocaramba.Tests.PageObjects;
using Ocaramba.Types;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;


namespace Ocaramba.Tests.Appium
{
    public class AppiumSamplePage : ProjectPageBase
    {
        private readonly ElementLocator preferencesButtonAccId =
            new ElementLocator(Locator.AccessibilityId, "Preference");


        public AppiumSamplePage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public bool IsPreferencePresent()
        {
            try
            {
                return this.Driver.FindElement(this.preferencesButtonAccId.ToBy()) != null;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void ClickPreference()
        {
           this.Driver.FindElement(this.preferencesButtonAccId.ToBy()).Click();
        }

        public void ClickViews()
        {
            var viewsLocator = new ElementLocator(Locator.AccessibilityId, "Views");
            var wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(10));
            var viewsElement = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(viewsLocator.ToBy())
            );
            this.Driver.FindElement(viewsLocator.ToBy()).Click();
        }

        public void ClickWebView()
        {
            var webViewLocator = new ElementLocator(Locator.AccessibilityId, "WebView");
            this.Driver.FindElement(webViewLocator.ToBy()).Click();
        }

        public string GetElementinWebView()
        {
            var viewsLocator = new ElementLocator(Locator.CssSelector, "h1");
            return this.Driver.FindElement(viewsLocator.ToBy()).Text;
        }


    }
}