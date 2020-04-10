// <copyright file="PerformanceHelper.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Ocaramba.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using NLog;
    using Ocaramba.Types;

    /// <summary>
    /// Class which support performance tests. <see href="https://github.com/ObjectivityLtd/Ocaramba/wiki/Performance%20measures">More details on wiki</see>.
    /// </summary>
    public class PerformanceHelper
    {
#if net47 || net45
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
#endif
#if netcoreapp3_1
        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
#endif

        /// <summary>
        /// The timer.
        /// </summary>
        private readonly Stopwatch timer;

        /// <summary>
        /// The scenario list.
        /// </summary>
        private readonly List<SavedTimes> loadTimeList;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceHelper"/> class.
        /// </summary>
        public PerformanceHelper()
        {
            this.loadTimeList = new List<SavedTimes>();
            this.timer = new Stopwatch();
        }

        /// <summary>
        /// Gets the scenario list.
        /// </summary>
        public IList<SavedTimes> GetloadTimeList => this.loadTimeList;

        /// <summary>
        /// Gets all the durations milliseconds.
        /// </summary>
        /// <returns>Return average load times for particular scenarios and browsers.</returns>
        public IEnumerable<AverageGroupedTimes> AllGroupedDurationsMilliseconds
        {
            get
            {
                var groupedList =
                    this.loadTimeList.OrderBy(dur => dur.Duration).GroupBy(
                        st => new { st.Scenario, BName = st.BrowserName },
                        (key, g) =>
                        {
                            var savedTimeses = g as IList<SavedTimes> ?? g.ToList();
                            return new AverageGroupedTimes
                            {
                                StepName = key.Scenario,
                                Browser = key.BName,
                                AverageDuration = Math.Round(savedTimeses.Average(dur => dur.Duration)),
                                Percentile90 = savedTimeses[(int)(Math.Ceiling(savedTimeses.Count * 0.9) - 1)].Duration,
                            };
                        }).ToList().OrderBy(listElement => listElement.StepName);
                return groupedList;
            }
        }

        /// <summary>
        /// Gets or sets measured time.
        /// </summary>
        /// <value>Return last measured time.</value>
        private long MeasuredTime { get; set; }

        /// <summary>
        /// Starts the measure.
        /// </summary>
        public void StartMeasure()
        {
            this.timer.Reset();
            this.timer.Start();
        }

        /// <summary>
        /// Stops the measure.
        /// </summary>
        /// <param name="title">The title.</param>
        public void StopMeasure(string title)
        {
            this.timer.Stop();

            var savedTimes = new SavedTimes(title);
            this.MeasuredTime = this.timer.ElapsedMilliseconds;
            savedTimes.SetDuration(this.MeasuredTime);

            Logger.Info(CultureInfo.CurrentCulture, "Load Time {0}", this.MeasuredTime);

            this.loadTimeList.Add(savedTimes);
        }
    }
}
