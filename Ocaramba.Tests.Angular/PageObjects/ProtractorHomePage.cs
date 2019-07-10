using Ocaramba;
using Ocaramba.Extensions;
using Ocaramba.Types;
using Ocaramba.Tests.PageObjects;

namespace Ocaramba.Tests.Angular.PageObjects
{
    using System.Globalization;
    using NLog;
    using System;

    public class ProtractorHomePage : ProjectPageBase
    {
#if net45
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
#endif
#if netcoreapp2_2
        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
#endif

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator
            QuickStart = new ElementLocator(Locator.Id, "drop1"),
            Tutorial = new ElementLocator(Locator.CssSelector, "li[class*='open']>ul>li>a[href*='tutorial']");

        public ProtractorHomePage OpenProtractorHomePage()
        {
            var url = BaseConfiguration.GetUrlValue;
            this.Driver.SynchronizeWithAngular(true);
            this.Driver.NavigateTo(new Uri(url));
            Logger.Info(CultureInfo.CurrentCulture, "Opening page {0}", url);
            return this;
        }

        public ProtractorHomePage(DriverContext driverContext) : base(driverContext)
        {
        }

        public ProtractorHomePage ClickQuickStart()
        {
            this.Driver.GetElement(this.QuickStart).JavaScriptClick();
            return this;
        }

        public TutorialPage ClickTutorial()
        {
            this.Driver.GetElement(this.Tutorial).Click();
            return new TutorialPage(this.DriverContext);
        }
    }
}
