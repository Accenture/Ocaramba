// <copyright file="SavedTimes.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Ocaramba.Types
{
    using Ocaramba;

    /// <summary>
    /// SavedTimes class.
    /// </summary>
    public class SavedTimes
    {
        /// <summary>
        /// The scenario.
        /// </summary>
        private readonly string scenario;

        /// <summary>
        /// The browser name.
        /// </summary>
        private readonly string browserName;

        /// <summary>
        /// The duration.
        /// </summary>
        private long duration;

        /// <summary>
        /// Initializes a new instance of the <see cref="SavedTimes" /> class.
        /// </summary>
        /// <param name="title">The title.</param>
        public SavedTimes(string title)
        {
            this.scenario = title;
            this.browserName = BaseConfiguration.TestBrowser.ToString();
        }

        /// <summary>
        /// Gets the scenario.
        /// </summary>
        /// <value>
        /// The scenario.
        /// </value>
        public string Scenario
        {
            get { return this.scenario; }
        }

        /// <summary>
        /// Gets the name of the Driver.
        /// </summary>
        /// <value>
        /// The name of the Driver.
        /// </value>
        public string BrowserName
        {
            get { return this.browserName; }
        }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public long Duration
        {
            get { return this.duration; }
        }

        /// <summary>
        /// Sets the duration.
        /// </summary>
        /// <param name="loadTime">The load time.</param>
        public void SetDuration(long loadTime)
        {
            this.duration = loadTime;
        }
    }
}