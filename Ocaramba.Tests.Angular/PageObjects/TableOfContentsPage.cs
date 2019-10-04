﻿using Ocaramba.Tests.PageObjects;

namespace Ocaramba.Tests.Angular.PageObjects
{
    using NLog;
    using Ocaramba.Types;
    using Ocaramba;
    using Ocaramba.Extensions;

    public class TableOfContentsPage : ProjectPageBase
    {
#if net47
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
#endif
#if netcoreapp2_2
        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
#endif

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator
            ProtractorApi = new ElementLocator(Locator.CssSelector, "ul[class='ng-scope']>li>a[href='#/api']");

        public TableOfContentsPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public ProtractorApiPage ClickProtractorApi()
        {
            this.Driver.GetElement(this.ProtractorApi).Click();
            return new ProtractorApiPage(this.DriverContext);
        }
    }
}
