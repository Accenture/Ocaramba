// <copyright file="TestLogger.cs" company="Accenture">
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

using System;
using System.Globalization;
using NLog;
using Ocaramba;

namespace OcarambaLite.Logger
{
    /// <summary>
    /// Class for test logger.
    /// </summary>
    public class TestLogger
    {
        /// <summary>
        /// The logger.
        /// </summary>

       private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        /// <summary>
        /// The start test time.
        /// </summary>
       private DateTime startTestTime;

        /// <summary>
        /// Logs the test starting.
        /// </summary>
        /// <param name="driverContext">The driver context.</param>
       public void LogTestStarting(DriverContext driverContext)
        {
            this.startTestTime = DateTime.Now;
            this.Info("*************************************************************************************");
            this.Info($"START: {driverContext.TestTitle} starts at {this.startTestTime}.");
        }

        /// <summary>
        /// Logs the test ending.
        /// </summary>
        /// <param name="driverContext">The driver context.</param>
        public void LogTestEnding(DriverContext driverContext)
        {
            var endTestTime = DateTime.Now;
            var timeInSec = (endTestTime - this.startTestTime).TotalSeconds;
            this.Info($"END: {driverContext.TestTitle} ends at {endTestTime} after {timeInSec.ToString("N2", CultureInfo.CurrentCulture)} sec.");
            this.Info("*************************************************************************************");
        }

        /// <summary>
        /// Information Info the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void Info(string message, params object[] args)
        {
            Logger.Info(CultureInfo.CurrentCulture, message, args);
        }

        /// <summary>
        /// Information Debug the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void Debug(string message, params object[] args)
        {
            Logger.Debug(CultureInfo.CurrentCulture, message, args);
        }

        /// <summary>
        /// Information Trace the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void Trace(string message, params object[] args)
        {
            Logger.Trace(CultureInfo.CurrentCulture, message, args);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void Warn(string message, params object[] args)
        {
            Logger.Warn(CultureInfo.CurrentCulture, message, args);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void Error(string message, params object[] args)
        {
            Logger.Error(CultureInfo.CurrentCulture, message, args);
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="e">The e.</param>
        public void LogError(Exception e)
        {
            this.Error($"Error occurred: {e}");
            throw e;
        }
    }
}
