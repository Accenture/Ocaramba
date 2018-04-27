// <copyright file="DriversCustomSettings.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.Common
{
    using System.Collections.Generic;
    using OpenQA.Selenium;

    /// <summary>
    /// To keep drivers custom setting. <see href="https://github.com/ObjectivityLtd/Test.Automation/wiki/Angular-support">More details on wiki</see>
    /// </summary>
    public static class DriversCustomSettings
    {
        private static Dictionary<IWebDriver, bool> driversAngularSynchronizationEnable =
            new Dictionary<IWebDriver, bool>();

        /// <summary>
        /// Method return true or false is driver is synchronized with angular.
        /// </summary>
        /// <param name="driver">Provide driver.</param>
        /// <returns>If driver is synchornized with angular return true if not return false.</returns>
        public static bool IsDriverSynchronizationWithAngular(IWebDriver driver)
        {
            return driversAngularSynchronizationEnable.ContainsKey(driver) && driversAngularSynchronizationEnable[driver];
        }

        /// <summary>
        /// Set angular synchronization for driver.
        /// </summary>
        /// <param name="driver">Provide driver.</param>
        /// <param name="enable">Set true to enable.</param>
        public static void SetAngularSynchronizationForDriver(IWebDriver driver, bool enable)
        {
            if (!enable && driversAngularSynchronizationEnable.ContainsKey(driver))
            {
                driversAngularSynchronizationEnable.Remove(driver);
            }

            if (enable && !driversAngularSynchronizationEnable.ContainsKey(driver))
            {
                driversAngularSynchronizationEnable.Add(driver, true);
            }

            if (enable && driversAngularSynchronizationEnable.ContainsKey(driver))
            {
                driversAngularSynchronizationEnable[driver] = true;
            }
        }
    }
}
