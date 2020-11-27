// <copyright file="PrintPerformanceResultsHelper.cs" company="Objectivity Bespoke Software Specialists">
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
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
#if netcoreapp3_1
    using System.Management.Automation;
#endif
    using NLog;

    /// <summary>
    /// Class which support displaying performance test results. <see href="https://github.com/ObjectivityLtd/Ocaramba/wiki/Performance%20measures">More details on wiki</see>.
    /// </summary>
    public static class PrintPerformanceResultsHelper
    {
#if net47 || net45
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
#endif
#if netcoreapp3_1
        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
#endif

        /// <summary>
        /// Prints the performance summary of percentiles 90 duration in millisecond in Teamcity.
        /// </summary>
        /// <param name="measures">The instance of PerformanceHelper class.</param>
        public static void PrintPercentiles90DurationMillisecondsinTeamcity(PerformanceHelper measures)
        {
            var groupedPercentiles90Durations = measures.AllGroupedDurationsMilliseconds.Select(v =>
                "##teamcity[testStarted name='" + v.StepName + "." + v.Browser + ".Percentile90Line']\n" +
                "##teamcity[testFinished name='" + v.StepName + "." + v.Browser + ".Percentile90Line' duration='" + v.Percentile90 + "']\n" +
                v.StepName + " " + v.Browser + " Percentile90Line: " + v.Percentile90).ToList().OrderBy(listElement => listElement);

            for (int i = 0; i < groupedPercentiles90Durations.Count(); i++)
            {
                Logger.Info(groupedPercentiles90Durations.ElementAt(i));
            }
        }

        /// <summary>
        /// Prints the performance summary of average duration in millisecond in TeamCity.
        /// </summary>
        /// <param name="measures">The instance of PerformanceHelper class.</param>
        public static void PrintAverageDurationMillisecondsInTeamcity(PerformanceHelper measures)
        {
            var groupedAverageDurations = measures.AllGroupedDurationsMilliseconds.Select(v =>
                "\n##teamcity[testStarted name='" + v.StepName + "." + v.Browser + ".Average']" +
                "\n##teamcity[testFinished name='" + v.StepName + "." + v.Browser + ".Average' duration='" + v.AverageDuration + "']" +
                "\n" + v.StepName + " " + v.Browser + " Average: " + v.AverageDuration + "\n").ToList().OrderBy(listElement => listElement);

            for (int i = 0; i < groupedAverageDurations.Count(); i++)
            {
                Logger.Info(groupedAverageDurations.ElementAt(i));
            }
        }

        /// <summary>
        /// Prints the performance summary of percentiles 90 duration in millisecond in AppVeyor.
        /// </summary>
        /// <param name="measures">The instance of PerformanceHelper class.</param>
        public static void PrintPercentiles90DurationMillisecondsInAppVeyor(PerformanceHelper measures)
        {
            var groupedDurationsAppVeyor = measures.AllGroupedDurationsMilliseconds.Select(v =>
                v.StepName + "." + v.Browser +
                ".Percentile90Line -Framework NUnit -Filename PerformanceResults -Outcome Passed -Duration " + v.Percentile90)
                .ToList()
                .OrderBy(listElement => listElement);

            PrintResultsInAppVeyor(groupedDurationsAppVeyor);
        }

        /// <summary>
        /// Prints the performance summary of average duration in millisecond in AppVeyor.
        /// </summary>
        /// <param name="measures">The instance of PerformanceHelper class.</param>
        public static void PrintAverageDurationMillisecondsInAppVeyor(PerformanceHelper measures)
        {
            var groupedDurationsAppVeyor = measures.AllGroupedDurationsMilliseconds.Select(v =>
                v.StepName + "." + v.Browser +
                ".Average -Framework NUnit -Filename PerformanceResults -Outcome Passed -Duration " + v.AverageDuration)
                .ToList()
                .OrderBy(listElement => listElement);

            PrintResultsInAppVeyor(groupedDurationsAppVeyor);
        }

        /// <summary>
        /// Prints test results in AppVeyor.
        /// </summary>
        /// <param name="measuresToPrint">Average load times for particular scenarios and browsers.</param>
        public static void PrintResultsInAppVeyor(IOrderedEnumerable<string> measuresToPrint)
        {
#if net47 || net45
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = "appveyor",
                WindowStyle = ProcessWindowStyle.Hidden,
            };

#endif

            for (int i = 0; i < measuresToPrint.Count(); i++)
            {
                var text = "AddTest " + measuresToPrint.ElementAt(i);

#if net47 || net45
                startInfo.Arguments = text;

                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                try
                {
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        if (exeProcess != null)
                        {
                            exeProcess.WaitForExit();
                        }
                    }
                }
                catch (Win32Exception)
                {
                    Logger.Info("AppVeyor app not found");
                    break;
                }
#endif

#if netcoreapp3_1
                text = "Add-AppveyorTest -Name " + measuresToPrint.ElementAt(i);
                using (var ps = PowerShell.Create())
                {
                    var results = ps.AddScript(text).Invoke();
                    foreach (var result in results)
                    {
                        Debug.Write(result.ToString());
                    }
                }
#endif
            }
        }
    }
}
