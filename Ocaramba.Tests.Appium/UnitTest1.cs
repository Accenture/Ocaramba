using NUnit.Framework;
using Ocaramba;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using OpenQA.Selenium.Appium.Android;

namespace Ocaramba.Tests.Appium
{
    [TestFixture]
    public class AppiumAndroidTests : ProjectTestBase
    {

        [Test]
        public void SampleAppiumTest_ElementExists()
        {
            var page = new AppiumSamplePage(this.DriverContext);
            Assert.That(page.IsSomeElementPresent(), Is.True);
            Assert.That(page.IsPreferencePresent(), Is.True);
            page.ClickPreference();
            this.DriverContext.Driver.Navigate().Back();
            page.ClickViews();
            // Scroll until "WebView" is visible
            var androidDriver = (AppiumDriver)this.DriverContext.Driver;

            // Scroll inside the ListView until "WebView" is visible
            var element = androidDriver.FindElement(
                MobileBy.AndroidUIAutomator(
                    "new UiScrollable(new UiSelector().resourceId(\"android:id/list\"))" +
                    ".scrollIntoView(new UiSelector().text(\"WebView\"))"
                )
            );
            page.ClickWebView();
            this.DriverContext.SwitchToWebView();
            var webViewElementView = this.DriverContext.Driver.FindElement(By.CssSelector("h1"));
            Assert.That(webViewElementView.Text.Contains("This page is a Selenium sandbox"), Is.True);
            this.DriverContext.SwitchToNative();
            this.DriverContext.Driver.Navigate().Back();
            this.DriverContext.Driver.Navigate().Back();
            Assert.That(page.IsPreferencePresent(), Is.True);
            page.ClickPreference();
        }
    }
}
