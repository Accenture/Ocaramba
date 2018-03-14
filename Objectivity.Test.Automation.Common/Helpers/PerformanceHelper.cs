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

namespace Objectivity.Test.Automation.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;

    using NLog;

    using Objectivity.Test.Automation.Common.Types;

    /// <summary>
    /// Class which support performance tests. <see href="https://github.com/ObjectivityLtd/Test.Automation/wiki/Performance%20measures">More details on wiki</see>
    /// </summary>
    public class PerformanceHelper
    {
        private static readonly Logger Logger = LogManager.GetLogger("DRIVER");

        /// <summary>
        /// The timer
        /// </summary>
        private readonly Stopwatch timer;

        /// <summary>
        /// The scenario list
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
        /// Gets the scenario list
        /// </summary>
        public IList<SavedTimes> GetloadTimeList => this.loadTimeList;

        /// <summary>
        /// Gets or sets measured time.
        /// </summary>
        /// <value>Return last measured time.</value>
        private long MeasuredTime { get; set; }

        /// <summary>
        /// Prints the performance summary.
        /// </summary>
        /// <param name="measures">The instance of PerformanceHelper class.</param>
        /// TODO: Decide what parameters must be printed
        public static void PrintAveragePercentiles90DurationMilliseconds(PerformanceHelper measures)
        {
            var groupedDurations = AllGroupedDurationsMilliseconds(measures).Select(v =>
                "##teamcity[testStarted name='" + v.StepName + "." + v.Browser + ".Average']\n" +
                "##teamcity[testFinished name='" + v.StepName + "." + v.Browser + ".Average' duration='" + v.AverageDuration + "']\n" +
                "##teamcity[testStarted name='" + v.StepName + "." + v.Browser + ".Percentile90Line']\n" +
                "##teamcity[testFinished name='" + v.StepName + "." + v.Browser + ".Percentile90Line' duration='" + v.Percentile90 + "']\n" +
                v.StepName + " " + v.Browser + " Average: " + v.AverageDuration + "\n" +
                v.StepName + " " + v.Browser + " Percentile90Line: " + v.Percentile90).ToList().OrderBy(listElement => listElement);

            for (int i = 0; i < groupedDurations.Count(); i++)
            {
                Logger.Info(groupedDurations.ElementAt(i));
            }
        }

        /// <summary>
        /// All the durations milliseconds.
        /// </summary>
        /// <param name="measures">The instance of PerformanceHelper class.</param>
        /// <returns>Return average load times for particular scenarios and browsers.</returns>
        public static IEnumerable<AverageGroupedTimes> AllGroupedDurationsMilliseconds(PerformanceHelper measures)
        {
            var loadTimeList = measures.GetloadTimeList;
            var groupedList =
                loadTimeList.OrderBy(dur => dur.Duration).GroupBy(
                    st => new { st.Scenario, BName = st.BrowserName },
                    (key, g) =>
                    {
                        var savedTimeses = g as IList<SavedTimes> ?? g.ToList();
                        return new AverageGroupedTimes
                        {
                            StepName = key.Scenario,
                            Browser = key.BName,
                            AverageDuration = Math.Round(savedTimeses.Average(dur => dur.Duration)),
                            Percentile90 = savedTimeses[(int)(Math.Ceiling(savedTimeses.Count * 0.9) - 1)].Duration
                        };
                    }).ToList().OrderBy(listElement => listElement.StepName);
            return groupedList;
        }

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
