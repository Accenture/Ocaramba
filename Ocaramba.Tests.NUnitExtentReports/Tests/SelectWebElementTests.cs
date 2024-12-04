// <copyright file="SelectWebElementTests.cs" company="Objectivity Bespoke Software Specialists">
// Copyright (c) Objectivity Bespoke Software Specialists. All rights reserved.
// </copyright>

namespace Ocaramba.Tests.NUnitExtentReports.Tests
{
    using global::NUnit.Framework;
    using Ocaramba.Tests.NUnitExtentReports;
    using Ocaramba.Tests.NUnitExtentReports.PageObjects;

    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class SelectWebElementTests : ProjectTestBase
    {
        [Test]
        public void SelectByIndexTest()
        {
            const string ExpectedOption = "Option 1";

            var dropdownPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToDropdownPage();

            dropdownPage.SelectByIndex(1);

            Assert.That(dropdownPage.SelectedOption(), Is.EqualTo(ExpectedOption));
            test.Info("Verifying selected option, expected: " + ExpectedOption);
        }

        [Test]
        public void NoSuchElementExceptionByTextTest()
        {
            const string ElementText = "Qwerty.";

            var dropdownPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToDropdownPage();

            Assert.That(() => dropdownPage.SelectByText(ElementText, 10), Throws.Nothing);
            test.Info("Verifying it's possible to select element on dropdown by text: " + ElementText);
        }

        [Test]
        public void NoSuchElementExceptionByIndexTest()
        {
            const int ElementIndex = 7;

            var dropdownPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToDropdownPage();

            Assert.That(() => dropdownPage.SelectByIndex(ElementIndex), Throws.Nothing);
            test.Info("Verifying it's possible to select element on dropdown by element index: " + ElementIndex.ToString());
        }

        [Test]
        public void NoSuchElementExceptionByValueTest()
        {
            const string ElementValue = "qwerty";

            var dropdownPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToDropdownPage();

            Assert.That(() => dropdownPage.SelectByValue(ElementValue), Throws.Nothing);
            test.Info("Verifying it's possible to select element on dropdown by value: " + ElementValue);
        }
    }
}