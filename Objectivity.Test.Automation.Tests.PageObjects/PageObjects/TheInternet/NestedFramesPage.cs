using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
using Objectivity.Test.Automation.Common.Types;

namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet
{
    public class NestedFramesPage : ProjectPageBase
    {
        private readonly ElementLocator
            leftBody = new ElementLocator(Locator.CssSelector, "body"),
            middleBody = new ElementLocator(Locator.CssSelector, "div#content"),
            rightBody = new ElementLocator(Locator.CssSelector, "body"),
            bottomBody = new ElementLocator(Locator.CssSelector, "body");

        public NestedFramesPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public string LeftBody
        {
            get { return Driver.GetElement(leftBody).Text; }
        }

        public string MiddleBody
        {
            get { return Driver.GetElement(middleBody).Text; }
        }

        public string RightBody
        {
            get { return Driver.GetElement(rightBody).Text; }
        }

        public string BottomBody
        {
            get { return Driver.GetElement(bottomBody).Text; }
        }


        public NestedFramesPage SwitchToFrame(string frame)
        {
            Driver.SwitchTo().Frame(frame);
            return this;
        }

        public NestedFramesPage SwitchToParentFrame()
        {
            Driver.SwitchTo().ParentFrame();
            return this;
        }

        public NestedFramesPage ReturnToDefaultContent()
        {
            Driver.SwitchTo().DefaultContent();
            return this;
        }

    }
}
