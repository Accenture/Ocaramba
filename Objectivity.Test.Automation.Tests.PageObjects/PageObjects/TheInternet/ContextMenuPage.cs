using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Common.WebElements;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet
{
    public class ContextMenuPage : ProjectPageBase
    {
        private readonly ElementLocator
            hotSpot = new ElementLocator(Locator.CssSelector, "#hot-spot");


        public ContextMenuPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public ContextMenuPage SelectTheInternetOptionFromContextMenu()
        {
            new Actions(Driver)
                .ContextClick(Driver.GetElement(hotSpot))
                .SendKeys(Keys.ArrowDown)
                .SendKeys(Keys.ArrowDown)
                .SendKeys(Keys.ArrowDown)
                .SendKeys(Keys.ArrowDown)
                .SendKeys(Keys.ArrowDown)
                .SendKeys(Keys.Enter)
                .Perform();
            return this;
        }

        public string JavaScriptText
        {
            get { return Driver.SwitchTo().Alert().Text; }
        }

        public ContextMenuPage ConfirmJavaScript()
        {
            Driver.SwitchTo().Alert().Accept();
            return this;
        }
    }
}
