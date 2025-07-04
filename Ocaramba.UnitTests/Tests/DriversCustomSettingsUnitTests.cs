﻿// <copyright file="DriversCustomSettingsUnitTests.cs" company="Accenture">
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

using System.IO;
using NUnit.Framework;
using Ocaramba.Extensions;

namespace Ocaramba.UnitTests.Tests
{
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
    public class DriversCustomSettingsUnitTests
    {
        [Test]
        public void CheckSynchronizationWithAngularFuctionality()
        {

            string folder = TestContext.CurrentContext.TestDirectory;

            var driverContext = new Ocaramba.DriverContext {CurrentDirectory = folder };
            driverContext.Start();
            var Default_false = DriversCustomSettings.IsDriverSynchronizationWithAngular(driverContext.Driver);
            driverContext.Driver.SynchronizeWithAngular(true);
            var TurnOn_true = DriversCustomSettings.IsDriverSynchronizationWithAngular(driverContext.Driver);
            driverContext.Driver.SynchronizeWithAngular(false);
            var TurnOn_false = DriversCustomSettings.IsDriverSynchronizationWithAngular(driverContext.Driver);
            driverContext.Stop();
            Assert.That(false, Is.EqualTo(Default_false));
            Assert.That(true, Is.EqualTo(TurnOn_true));
            Assert.That(false, Is.EqualTo(TurnOn_false));
        }
    }
}
