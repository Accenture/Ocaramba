// <copyright file="CapabilitiesSetEventArgs.cs" company="Objectivity Bespoke Software Specialists">
// Copyright (c) Objectivity Bespoke Software Specialists. All rights reserved.
// </copyright>

namespace Objectivity.Test.Automation.Common
{
    using System;
    using OpenQA.Selenium.Remote;

    /// <summary>
    /// Before Capabilities Set Handler
    /// </summary>
    public class CapabilitiesSetEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CapabilitiesSetEventArgs"/> class.
        /// </summary>
        /// <param name="capabilities">The existing capabilities</param>
        public CapabilitiesSetEventArgs(DesiredCapabilities capabilities)
        {
            this.Capabilities = capabilities;
        }

        /// <summary>
        /// Gets the current capabilities
        /// </summary>
        public DesiredCapabilities Capabilities { get; }
    }
}