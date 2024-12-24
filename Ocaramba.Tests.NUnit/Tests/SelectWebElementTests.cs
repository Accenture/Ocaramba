// <copyright file="SelectWebElementTests.cs" company="Accenture">
// Copyright (c) Objectivity Bespoke Software Specialists. All rights reserved.
// </copyright>

namespace Ocaramba.Tests.NUnit.Tests
{
    using global::NUnit.Framework;
    using Ocaramba.Tests.PageObjects.PageObjects.TheInternet;

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

            Assert.That(dropdownPage.SelectedOption(), Is.EqualTo("Option 1"));
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