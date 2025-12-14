using NUnit.Framework;
using Ocaramba;
using OpenQA.Selenium;
using System;

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
            driver.Navigate().Back();
            page.ClickViews();
            page.ClickWebView();
            this.driverContext.SwitchToWebView();
            var webViewElement = this.DriverContext.Driver.FindElement(By.CssSelector("h1"));
            Assert.That(webViewElement.Text.Contains("This page is a Selenium sandbox"), Is.True);
            this.driverContext.SwitchToNativeApp();
            driver.Navigate().Back();
            driver.Navigate().Back();
            Assert.That(page.IsPreferencePresent(), Is.True);
            page.ClickPreference();
        }
    }
}
