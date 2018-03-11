// <copyright file="KendoComboBoxPage.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.Kendo
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using NLog;
    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Helpers;
    using Objectivity.Test.Automation.Common.Types;
    using Objectivity.Test.Automation.Common.WebElements.Kendo;

    public class KendoComboBoxPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Locators for elements
        /// </summary>
        private readonly ElementLocator tshirtFabricComboBoxLocator = new ElementLocator(Locator.CssSelector, "input[aria-owns=fabric_listbox]");

        private readonly Uri url = new Uri("http://demos.telerik.com/jsp-ui/combobox/index");

        public KendoComboBoxPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        protected KendoComboBox FabricKendoComboBox
        {
            get
            {
                var element = this.Driver.GetElement<KendoComboBox>(this.tshirtFabricComboBoxLocator);
                return element;
            }
        }

        public Collection<string> FabricOptions(int number)
        {
            WaitHelper.Wait(() => this.FabricKendoComboBox.Options.Count <= number, TimeSpan.FromSeconds(3), "Check number of options");
            var options = this.FabricKendoComboBox.Options;
            foreach (var option in options)
            {
                Logger.Info(CultureInfo.CurrentCulture, "Option: {0}", option);
            }

            return options;
        }

        public void SearchFabricOptions(string text)
        {
            Logger.Info(CultureInfo.CurrentCulture, "Typing text {0}", text);
            this.Driver.ScrollIntoMiddle(this.tshirtFabricComboBoxLocator);
            this.Driver.GetElement(this.tshirtFabricComboBoxLocator).JavaScriptClick();
            this.FabricKendoComboBox.SendKeys(text);
            this.Driver.WaitForAjax(BaseConfiguration.ShortTimeout);
        }

        public KendoComboBoxPage Open()
        {
            this.Driver.NavigateTo(this.url);
            return this;
        }

        public void OpenFabricComboBox()
        {
           this.FabricKendoComboBox.Open();
        }

        public void CloseFabricComboBox()
        {
            this.FabricKendoComboBox.Close();
        }

        public bool IsFabricComboBoxExpanded()
        {
            var combobox = this.Driver.GetElement(this.tshirtFabricComboBoxLocator);
            var attribute = combobox.GetAttribute("aria-expanded");
            return bool.Parse(attribute);
        }
    }
}