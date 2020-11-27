// <copyright file="BrowserType.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Ocaramba
{
    /// <summary>
    /// Supported browsers.
    /// </summary>
    public enum BrowserType
    {
        /// <summary>
        /// Firefox browser
        /// </summary>
        Firefox,

        /// <summary>
        /// InternetExplorer browser
        /// </summary>
        InternetExplorer,

        /// <summary>
        /// InternetExplorer browser
        /// </summary>
        IE,

        /// <summary>
        /// Chrome browser
        /// </summary>
        Chrome,

        /// <summary>
        /// Safari browser
        /// </summary>
        Safari,

        /// <summary>
        /// Remote Web driver
        /// </summary>
        RemoteWebDriver,

        /// <summary>
        /// Edge driver
        /// </summary>
        Edge,

        /// <summary>
        /// Edge Chrominium driver
        /// </summary>
        EdgeChrominium,

        /// <summary>
        /// CloudProvider parallel cross browsers testing
        /// </summary>
        CloudProvider,

        /// <summary>
        /// Browser type to run test on Android with CloudProviders
        /// </summary>
        Android,

        /// <summary>
        /// Browser type to run test on Iphone with CloudProviders
        /// </summary>
        Iphone,

        /// <summary>
        /// Not supported browser
        /// </summary>
        None,
    }
}
