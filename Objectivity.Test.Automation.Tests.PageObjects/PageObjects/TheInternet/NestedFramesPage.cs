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
            get { return this.Driver.GetElement(this.leftBody).Text; }
        }

        public string MiddleBody
        {
            get { return this.Driver.GetElement(this.middleBody).Text; }
        }

        public string RightBody
        {
            get { return this.Driver.GetElement(this.rightBody).Text; }
        }

        public string BottomBody
        {
            get { return this.Driver.GetElement(this.bottomBody).Text; }
        }

        public NestedFramesPage SwitchToFrame(string frame)
        {
            this.Driver.SwitchTo().Frame(frame);
            return this;
        }

        public NestedFramesPage SwitchToParentFrame()
        {
            this.Driver.SwitchTo().ParentFrame();
            return this;
        }

        public NestedFramesPage ReturnToDefaultContent()
        {
            this.Driver.SwitchTo().DefaultContent();
            return this;
        }
    }
}
