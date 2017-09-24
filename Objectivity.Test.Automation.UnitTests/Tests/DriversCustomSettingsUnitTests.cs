// <copyright file="DriversCustomSettingsUnitTests.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.UnitTests.Tests
{
    using NUnit.Framework;
    using Common;
    using Common.Extensions;

    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
    public class DriversCustomSettingsUnitTests
    {
        [Test]
        public void CheckSynchronizationWithAngularFuctionality()
        {
            var driverContext = new DriverContext {CurrentDirectory = TestContext.CurrentContext.TestDirectory};
            driverContext.Start();
            var Default_false = DriversCustomSettings.IsDriverSynchronizationWithAngular(driverContext.Driver);
            driverContext.Driver.SynchronizeWithAngular(true);
            var TurnOn_true = DriversCustomSettings.IsDriverSynchronizationWithAngular(driverContext.Driver);
            driverContext.Driver.SynchronizeWithAngular(false);
            var TurnOn_false = DriversCustomSettings.IsDriverSynchronizationWithAngular(driverContext.Driver);
            driverContext.Stop();
            Assert.False(Default_false, "Default setting is not false");
            Assert.True(TurnOn_true, "Setting is not true");
            Assert.False(TurnOn_false, "Setting is not false");
        }
    }
}
