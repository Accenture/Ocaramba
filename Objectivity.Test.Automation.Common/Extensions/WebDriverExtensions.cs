/*
The MIT License (MIT)

Copyright (c) 2015 Objectivity Bespoke Software Specialists

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using Objectivity.Test.Automation.Common.Helpers;

namespace Objectivity.Test.Automation.Common.Extensions
{
    using System;
    using System.Globalization;

    using Objectivity.Test.Automation.Common.Types;

    using OpenQA.Selenium;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Extension methods for IWebDriver
    /// </summary>
    public static class WebDriverExtensions
    {
        /// <summary>
        /// Tables the specified web element.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <returns>
        /// Table element
        /// </returns>
        public static JavaScriptAlert JavaScriptAlert(this IWebDriver webDriver)
        {
            return new JavaScriptAlert(webDriver);
        }

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="url">The URL.</param>
        public static void NavigateTo(this IWebDriver webDriver, Uri url)
        {
            webDriver.Navigate().GoToUrl(url);

            ApproveCertificateForInternetExplorer(webDriver);
            ApproveCertificateForInternetExplorer(webDriver);
        }

        /// <summary>
        /// Navigates to given url.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="url">The URL.</param>
        public static void NavigateToAndMeasureTimeForAjaxFinished(this IWebDriver webDriver, Uri url)
        {
            PerformanceHelper.Instance.StartMeasure();
            webDriver.Navigate().GoToUrl(url);
            PerformanceHelper.Instance.StopMeasure(url.AbsolutePath);
        }

        /// <summary>
        /// Waits for all ajax actions to be completed.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        public static void WaitForAjax(this IWebDriver webDriver)
        {
            try
            {
                new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.AjaxWaitingTime)).Until(
                    driver =>
                    {
                        var javaScriptExecutor = driver as IJavaScriptExecutor;
                        return javaScriptExecutor != null
                               && (bool)javaScriptExecutor.ExecuteScript("return jQuery.active == 0");
                    });
            }
            catch (InvalidOperationException)
            {
            }
        }

        /// <summary>
        /// The element is present.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="customTimeout">The timeout.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public static bool IsElementPresent(this IWebDriver webDriver, ElementLocator locator, double customTimeout)
        {
            try
            {
                webDriver.GetDisplayedElement(locator, customTimeout);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns current page title.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <returns></returns>
        public static string GetPageTitle(this IWebDriver webDriver)
        {       
            return webDriver.Title.ToLower(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Waits the until element is no longer found.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="timeout">The timeout.</param>
        public static void WaitUntilElementIsNoLongerFound(this IWebDriver webDriver, ElementLocator locator, double timeout)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
            wait.Until(driver => webDriver.GetElements(locator).Count == 0);
        }

        /// <summary>
        /// The scroll into middle.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">The locator.</param>
        public static void ScrollIntoMiddle(this IWebDriver webDriver, ElementLocator locator)
        {
            var js = (IJavaScriptExecutor)webDriver;
            var element = webDriver.GetElement(locator);

            if (webDriver != null)
            {
                int height = webDriver.Manage().Window.Size.Height;

                var hoverItem = (ILocatable)element;
                var locationY = hoverItem.LocationOnScreenOnceScrolledIntoView.Y;
                js.ExecuteScript(string.Format(CultureInfo.InvariantCulture, "javascript:window.scrollBy(0,{0})", locationY - (height / 2)));
            }
        }

        /// <summary>
        /// The scroll into middle.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <returns>Return new Action handle</returns>
        public static Actions Actions(this IWebDriver webDriver)
        {
            return new Actions(webDriver);
        }

        /// <summary>An IWebDriver extension method that page source contains case.</summary>
        /// <param name="webDriver">The web webDriver.</param>
        /// <param name="text">The text.</param>
        /// <param name="timeoutInSeconds">The timeout in seconds.</param>
        /// <param name="isCaseSensitive">True if this object is case sensitive.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool PageSourceContainsCase(this IWebDriver webDriver, string text, double timeoutInSeconds, bool isCaseSensitive)
        {
            Func<IWebDriver, bool> condition;

            if (isCaseSensitive)
            {
                condition = drv => drv.PageSource.Contains(text);
            }
            else
            {
                condition = drv => drv.PageSource.ToUpperInvariant().Contains(text.ToUpperInvariant());
            }

            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(condition);
            }

            return condition.Invoke(webDriver);
        }

        /// <summary>An IWebDriver extension method that scripts the given webDriver.</summary>
        /// <param name="webDriver">The webDriver to act on.</param>
        /// <returns>An IJavaScriptExecutor.</returns>
        public static IJavaScriptExecutor Scripts(this IWebDriver webDriver)
        {
            return (IJavaScriptExecutor)webDriver;
        }

        /// <summary>
        /// Approves the trust certificate for internet explorer.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        private static void ApproveCertificateForInternetExplorer(this IWebDriver webDriver)
        {
            if (webDriver.GetType() == typeof(InternetExplorerDriver)
                && webDriver.Title.Contains("Certificate"))
            {
                webDriver.Navigate().GoToUrl(new Uri("javascript:document.FindElementById('overridelink').click()"));
            }
        }
    }
}
