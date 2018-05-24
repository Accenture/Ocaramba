// <copyright file="WaitHelper.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Objectivity.Test.Automation.Common.Helpers
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;

    using Objectivity.Test.Automation.Common.Exceptions;

    /// <summary>
    /// Contains wait methods with timeouts
    /// </summary>
    public static class WaitHelper
    {
        /// <summary>
        /// Wait for a condition with given timeout.
        /// </summary>
        /// <param name="condition">The condition to be met.</param>
        /// <param name="timeout">The timeout value [seconds] indicating how long to wait for the condition.</param>
        /// <param name="message">The exception message</param>
        /// <exception cref="WaitTimeoutException">Timeout exception when condition is not met</exception>
        /// <example>How to use it: <code>
        /// WaitHelper.Wait(() => CountFiles(folder, type) > filesNumber, TimeSpan.FromSeconds(waitTime), timeoutMessage);
        /// </code></example>
        public static void Wait(Func<bool> condition, TimeSpan timeout, string message)
        {
            Wait(condition, timeout, TimeSpan.FromSeconds(1), message);
        }

        /// <summary>
        /// Wait for a condition with given timeout and timeInterval.
        /// </summary>
        /// <param name="condition">The condition to be met.</param>
        /// <param name="timeout">The timeout value [seconds] indicating how long to wait for the condition.</param>
        /// <param name="sleepInterval">The value [seconds] indicating how often to check for the condition to be true.</param>
        /// <param name="message">The exception message</param>
        /// <exception cref="WaitTimeoutException">Timeout exception when condition is not met</exception>
        /// <example>How to use it: <code>
        /// WaitHelper.Wait(() => CountFiles(folder, type) > filesNumber, TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);
        /// </code></example>
        public static void Wait(Func<bool> condition, TimeSpan timeout, TimeSpan sleepInterval, string message)
        {
            var result = Wait(condition, timeout, sleepInterval);

            if (!result)
            {
                throw new WaitTimeoutException(string.Format(CultureInfo.CurrentCulture, "Timeout after {0} second(s), {1}", timeout.TotalSeconds, message));
            }
        }

        /// <summary>
        /// Wait for a condition with given timeout and timeInterval.
        /// </summary>
        /// <param name="condition">The condition to be met.</param>
        /// <param name="timeout">The timeout value [seconds] indicating how long to wait for the condition.</param>
        /// <param name="sleepInterval">The value [seconds] indicating how often to check for the condition to be true.</param>
        /// <returns>
        /// True if condition is met in given timeout
        /// </returns>
        /// <example>How to use it: <code>
        /// bool result = WaitHelper.Wait(() => CountFiles(folder, type) > filesNumber, TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1));
        /// </code></example>
        public static bool Wait(Func<bool> condition, TimeSpan timeout, TimeSpan sleepInterval)
        {
            var result = false;
            var start = DateTime.Now;
            var canceller = new CancellationTokenSource();
            var task = Task.Factory.StartNew(condition, canceller.Token);

            while ((DateTime.Now - start).TotalSeconds < timeout.TotalSeconds)
            {
                if (task.IsCompleted)
                {
                    if (task.Result)
                    {
                        result = true;
                        canceller.Cancel();
                        break;
                    }

                    task = Task.Factory.StartNew(
                        () =>
                            {
                                using (canceller.Token.Register(Thread.CurrentThread.Abort))
                                {
                                    return condition();
                                }
                            },
                              canceller.Token);
                }

                Thread.Sleep(sleepInterval);
            }

            canceller.Cancel();
            return result;
        }
    }
}
