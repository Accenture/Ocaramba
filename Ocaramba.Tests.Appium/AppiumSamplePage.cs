using Ocaramba;
using OpenQA.Selenium;
using Ocaramba.Tests.PageObjects;

namespace Ocaramba.Tests.Appium
{
    public class AppiumSamplePage : ProjectPageBase
    {
        private readonly By SomeElement = By.Id("com.google.android.youtube:id/search_edit_text");

        public AppiumSamplePage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public bool IsSomeElementPresent()
        {
            try
            {
                return this.Driver.FindElement(this.SomeElement) != null;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}