// <copyright file="WebDriverExtensions.cs" company="Objectivity Bespoke Software Specialists">
// Copyright (c) Objectivity Bespoke Software Specialists. All rights reserved.
// </copyright>
// <license>
//     The MIT License (MIT)
//     Permission is hereby granted, free of charge, to any person obtaining a copy
//     of this software and associated documentation files (the "Software"), to deal
//     in the Software without restriction, including without limitation the rights
//     to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//     copies of the Software, and to permit persons to whom the Software is
//     furnished to do so, subject to the following conditions:
//     The above copyright notice and this permission notice shall be included in all
//     copies or substantial portions of the Software.
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//     LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//     OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//     SOFTWARE.
// </license>

namespace Ocaramba.Extensions
{
    using System;
    using System.Globalization;
    using NLog;
    using Ocaramba;
    using Ocaramba.Types;
    using Ocaramba.WebElements;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Extension methods for IWebDriver.
    /// </summary>
    public static class WebDriverExtensions
    {
#if net47 || net45
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
#endif
#if netcoreapp3_1
        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
#endif

        /// <summary>
        /// Handler for simple use JavaScriptAlert.
        /// </summary>
        /// <example>Sample confirmation for java script alert: <code>
        /// this.Driver.JavaScriptAlert().ConfirmJavaScriptAlert();
        /// </code></example>
        /// <param name="webDriver">The web driver.</param>
        /// <returns>JavaScriptAlert Handle.</returns>
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
        }

        /// <summary>
        /// Waits for all ajax actions to be completed.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        public static void WaitForAjax(this IWebDriver webDriver)
        {
            WaitForAjax(webDriver, BaseConfiguration.MediumTimeout);
        }

        /// <summary>
        /// Waits for all ajax actions to be completed.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="timeout">The timeout.</param>
        public static void WaitForAjax(this IWebDriver webDriver, double timeout)
        {
            try
            {
                new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout)).Until(
                    driver =>
                    {
                        var javaScriptExecutor = driver as IJavaScriptExecutor;
                        return javaScriptExecutor != null
                               && (bool)javaScriptExecutor.ExecuteScript("return jQuery.active == 0");
                    });
            }
            catch (InvalidOperationException)
            {
                Logger.Error(CultureInfo.CurrentCulture, "Invalid Operation Exception");
            }
        }

        /// <summary>
        /// Wait for element to be displayed for specified time.
        /// </summary>
        /// <example>Example code to wait for login Button: <code>
        /// this.Driver.IsElementPresent(this.loginButton, BaseConfiguration.ShortTimeout);
        /// </code></example>
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
                webDriver.GetElement(locator, customTimeout, e => e.Displayed);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether [is page title] equals [the specified page title].
        /// </summary>
        /// <example>Sample code to check page title: <code>
        /// this.Driver.IsPageTitle(expectedPageTitle, BaseConfiguration.MediumTimeout);
        /// </code></example>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="pageTitle">The page title.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>
        /// Returns title of page.
        /// </returns>
        public static bool IsPageTitle(this IWebDriver webDriver, string pageTitle, double timeout)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));

            try
            {
                wait.Until(d => d.Title.ToLower(CultureInfo.CurrentCulture) == pageTitle.ToLower(CultureInfo.CurrentCulture));
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Error(CultureInfo.CurrentCulture, "Actual page title is {0};", webDriver.Title);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Waits the until element is no longer found.
        /// </summary>
        /// <example>Sample code to check page title: <code>
        /// this.Driver.WaitUntilElementIsNoLongerFound(dissapearingInfo, BaseConfiguration.ShortTimeout);
        /// </code></example>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="timeout">The timeout.</param>
        public static void WaitUntilElementIsNoLongerFound(this IWebDriver webDriver, ElementLocator locator, double timeout)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));
            wait.Until(driver => webDriver.GetElements(locator).Count == 0);
        }

        /// <summary>
        /// Switch to existing window using url.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="url">The url.</param>
        /// <param name="timeout">The timeout.</param>
        public static void SwitchToWindowUsingUrl(this IWebDriver webDriver, Uri url, double timeout)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
            wait.Until(
                driver =>
                {
                    foreach (var handle in webDriver.WindowHandles)
                    {
                        webDriver.SwitchTo().Window(handle);
                        if (driver.Url.Equals(url.ToString()))
                        {
                            return true;
                        }
                    }

                    return false;
                });
        }

        /// <summary>
        /// The scroll into middle.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">The locator.</param>
        public static void ScrollIntoMiddle(this IWebDriver webDriver, ElementLocator locator)
        {
            var js = (IJavaScriptExecutor)webDriver;
            var element = webDriver.ToDriver().GetElement(locator);

            var height = webDriver.Manage().Window.Size.Height;

            var hoverItem = (ILocatable)element;
            var locationY = hoverItem.LocationOnScreenOnceScrolledIntoView.Y;
            js.ExecuteScript(string.Format(CultureInfo.InvariantCulture, "javascript:window.scrollBy(0,{0})", locationY - (height / 2)));
        }

        /// <summary>
        /// Selenium Actions.
        /// </summary>
        /// <example>Simple use of Actions: <code>
        /// this.Driver.Actions().SendKeys(Keys.Return).Perform();
        /// </code></example>
        /// <param name="webDriver">The web driver.</param>
        /// <returns>Return new Action handle.</returns>
        public static Actions Actions(this IWebDriver webDriver)
        {
            return new Actions(webDriver);
        }

        /// <summary>Checks that page source contains text for specified time.</summary>
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

        /// <summary>Easy use for java scripts.</summary>
        /// <example>Sample use of java scripts: <code>
        /// this.Driver.JavaScripts().ExecuteScript("return document.getElementById("demo").innerHTML");
        /// </code></example>
        /// <param name="webDriver">The webDriver to act on.</param>
        /// <returns>An IJavaScriptExecutor Handle.</returns>
        public static IJavaScriptExecutor JavaScripts(this IWebDriver webDriver)
        {
            return (IJavaScriptExecutor)webDriver;
        }

        /// <summary>
        /// Waits for all angular actions to be completed.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        public static void WaitForAngular(this IWebDriver webDriver)
        {
            WaitForAngular(webDriver, BaseConfiguration.MediumTimeout);
        }

        /// <summary>
        /// Waits for all angular actions to be completed.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="timeout">The timeout.</param>
        public static void WaitForAngular(this IWebDriver webDriver, double timeout)
        {
            try
            {
                new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout)).Until(
                    driver =>
                    {
                        var javaScriptExecutor = driver as IJavaScriptExecutor;
                        return javaScriptExecutor != null
                               &&
                               (bool)javaScriptExecutor.ExecuteScript(
                                   "return window.angular != undefined && window.angular.element(document.body).injector().get('$http').pendingRequests.length == 0");
                    });
            }
            catch (InvalidOperationException)
            {
                Logger.Info("Wait for angular invalid operation exception.");
            }
        }

        /// <summary>
        /// Enable synchronization with angular.
        /// </summary>
        /// <param name="webDriver">The WebDriver.</param>
        /// <param name="enable">Enable or disable synchronization.</param>
        public static void SynchronizeWithAngular(this IWebDriver webDriver, bool enable)
        {
            DriversCustomSettings.SetAngularSynchronizationForDriver(webDriver, enable);
        }

        /// <summary>
        /// Javascript drag and drop function.
        /// </summary>
        /// <param name="webDriver">The WebDriver.</param>
        /// <param name="source">Source element.</param>
        /// <param name="destination">Destination element.</param>
        public static void DragAndDropJs(this IWebDriver webDriver, IWebElement source, IWebElement destination)
        {
            var script =
                "function createEvent(typeOfEvent) { " +
                   "var event = document.createEvent(\"CustomEvent\"); " +
                   "event.initCustomEvent(typeOfEvent, true, true, null); " +
                   "event.dataTransfer = { " +
                            "data: { }, " +
                        "setData: function(key, value) { " +
                                "this.data[key] = value; " +
                            "}, " +
                        "getData: function(key) { " +
                               "return this.data[key]; " +
                            "} " +
                        "}; " +
                    "return event; " +
                        "} " +
                        "function dispatchEvent(element, event, transferData) { " +
                            "if (transferData !== undefined)" +
                            "{" +
                        "event.dataTransfer = transferData;" +
                        "}" +
                    "if (element.dispatchEvent) {" +
                        "element.dispatchEvent(event);" +
                        "} else if (element.fireEvent) {" +
                        "element.fireEvent(\"on\" + event.type, event);" +
                        "}" +
                    "}" +
                    "function simulateHTML5DragAndDrop(element, target)" +
                    "{" +
                        "var dragStartEvent = createEvent('dragstart');" +
                        "dispatchEvent(element, dragStartEvent);" +
                        "var dropEvent = createEvent('drop');" +
                        "dispatchEvent(target, dropEvent, dragStartEvent.dataTransfer);" +
                        "var dragEndEvent = createEvent('dragend');" +
                        "dispatchEvent(element, dragEndEvent, dropEvent.dataTransfer);" +
                    "} simulateHTML5DragAndDrop(arguments[0], arguments[1])";

            ((IJavaScriptExecutor)webDriver).ExecuteScript(script, source, destination);
        }

        /// <summary>
        /// Approves the trust certificate for internet explorer.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        private static void ApproveCertificateForInternetExplorer(this IWebDriver webDriver)
        {
            if ((BaseConfiguration.TestBrowser.Equals(BrowserType.InternetExplorer) || BaseConfiguration.TestBrowser.Equals(BrowserType.IE)) && webDriver.Title.Contains("Certificate"))
            {
                webDriver.FindElement(By.Id("overridelink")).JavaScriptClick();
            }
        }
    }
}
