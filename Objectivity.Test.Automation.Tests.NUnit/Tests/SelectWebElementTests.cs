// <copyright file="SelectWebElementTests.cs" company="Objectivity Bespoke Software Specialists">
// Copyright (c) Objectivity Bespoke Software Specialists. All rights reserved.
// </copyright>

namespace Objectivity.Test.Automation.Tests.NUnit.Tests
{
    using global::NUnit.Framework;
    using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.TheInternet;

    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class SelectWebElementTests : ProjectTestBase
    {
        [Test]
        public void SelectByIndexTest()
        {
            var dropdownPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToDropdownPage();

            dropdownPage.SelectByIndex(1);

            Assert.AreEqual(dropdownPage.SelectedOption(), "Option 1");
        }

        [Test]
        public void NoSuchElementExceptionByTextTest()
        {
            var dropdownPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToDropdownPage();

            Assert.That(() => dropdownPage.SelectByText("Qwerty.", 10), Throws.Nothing);
        }

        [Test]
        public void NoSuchElementExceptionByIndexTest()
        {
            var dropdownPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToDropdownPage();

            Assert.That(() => dropdownPage.SelectByIndex(7), Throws.Nothing);
        }

        [Test]
        public void NoSuchElementExceptionByValueTest()
        {
            var dropdownPage = new InternetPage(this.DriverContext)
                .OpenHomePage()
                .GoToDropdownPage();

            Assert.That(() => dropdownPage.SelectByValue("qwerty"), Throws.Nothing);
        }
    }
}