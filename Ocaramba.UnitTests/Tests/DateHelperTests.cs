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
            Assert.AreEqual(DateTime.Now.AddDays(1).ToString("ddMMyyyy", CultureInfo.CurrentCulture), DateHelper.TomorrowDate);
        }

        [Test()]
        public void CurrentDateTest()
        {
            Assert.AreEqual(DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.CurrentCulture), DateHelper.CurrentDate);
        }

        [Test()]
        public void CurrentTimeStampTest()
        {
            Assert.LessOrEqual(DateTime.ParseExact(DateHelper.CurrentTimeStamp, "ddMMyyyyHHmmss", null), DateTime.Now);
        }

        [Test()]
        public void GetFutureDateTest()
        {
            Assert.AreEqual(DateTime.Now.AddDays(3).ToString("ddMMyyyy", CultureInfo.CurrentCulture), DateHelper.GetFutureDate(3));
        }
    }
}