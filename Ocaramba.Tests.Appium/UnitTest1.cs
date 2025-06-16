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
            Assert.That(page.IsSomeElementPresent(), Is.False);
        }
    }
}
