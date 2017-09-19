using System;
using NUnit.Framework;
using Objectivity.Test.Automation.Common.Exceptions;
using Objectivity.Test.Automation.Common.Helpers;

namespace Objectivity.Test.Automation.UnitTests.Tests
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
            Assert.True(stop - start >= TimeSpan.FromSeconds(wait));
            Assert.False(stop - start < TimeSpan.FromSeconds(wait));
        }

        [Test()]
        public void WaitReturnFalseTest()
        {
            bool result = WaitHelper.Wait(() => SumNumber(1, 1) > 3, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(1));
            Assert.False(result);
        }

        [Test()]
        public void WaitReturnTrueTest()
        {
            bool result = WaitHelper.Wait(() => SumNumber(1, 1) > 1, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(1));
            Assert.True(result);
        }

        int SumNumber(int a, int b)
        {
            return a + b;
        }
    }
}