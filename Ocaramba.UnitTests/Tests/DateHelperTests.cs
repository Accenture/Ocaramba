using System;
using System.Globalization;
using NUnit.Framework;
using Ocaramba.Helpers;

namespace Ocaramba.UnitTests.Tests
{
    [TestFixture()]
    [TestFixture, Parallelizable(ParallelScope.Self)]
    public class DateHelperTests
    {
        [Test()]
        public void TomorrowDateTest()
        {
            Assert.That(DateHelper.TomorrowDate, Is.EqualTo(DateTime.Now.AddDays(1).ToString("ddMMyyyy", CultureInfo.CurrentCulture)));
        }

        [Test()]
        public void CurrentDateTest()
        {
            Assert.That(DateHelper.CurrentDate, Is.EqualTo(DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.CurrentCulture)));
        }

        [Test()]
        public void CurrentTimeStampTest()
        {
            var expectedDateTime = DateTime.ParseExact(DateHelper.CurrentTimeStamp, "ddMMyyyyHHmmss", null);
            var actualDateTime = DateTime.Now;
            Assert.That(actualDateTime.ToString("ddMMyyyyHHmmss"), Is.EqualTo(expectedDateTime.ToString("ddMMyyyyHHmmss")));
        }

        [Test()]
        public void GetFutureDateTest()
        {
            Assert.That(DateHelper.GetFutureDate(3), Is.EqualTo(DateTime.Now.AddDays(3).ToString("ddMMyyyy", CultureInfo.CurrentCulture)));
        }
    }
}