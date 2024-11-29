using System;
using NUnit.Framework;
using Ocaramba.Exceptions;
using Ocaramba.Helpers;

namespace Ocaramba.UnitTests.Tests
{
    [TestFixture()]
    [TestFixture, Parallelizable(ParallelScope.Self)]
    public class WaitHelperTests
    {
        [Test()]
        public void WaitTimeoutExceptionTest()
        {
            var start = DateTime.Now;
            int wait = 3;
            Assert.Throws<WaitTimeoutException>(() => WaitHelper.Wait(() => SumNumber(1,1) > 3, TimeSpan.FromSeconds(wait), TimeSpan.FromSeconds(1), "Timeout"));
            var stop = DateTime.Now;
            Assert.That(stop - start, Is.GreaterThanOrEqualTo(TimeSpan.FromSeconds(wait)));
            Assert.That(stop - start, Is.Not.LessThan(TimeSpan.FromSeconds(wait)));
        }

        [Test()]
        public void WaitReturnFalseTest()
        {
            bool result = WaitHelper.Wait(() => SumNumber(1, 1) > 3, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(1));
            Assert.That(result, Is.False);
        }

        [Test()]
        public void WaitReturnTrueTest()
        {
            bool result = WaitHelper.Wait(() => SumNumber(1, 1) > 1, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(1));
            Assert.That(result, Is.True);
        }

        int SumNumber(int a, int b)
        {
            return a + b;
        }
    }
}