namespace Objectivity.Test.Automation.Common
{
    using OpenQA.Selenium.Remote;

    /// <summary>
    /// BeforeCapabilitiesSetHandler delegate
    /// </summary>
    /// <param name="sender">sender</param>
    /// <param name="args"><see cref="BeforeCapabilitiesSetHandlerArgs"/></param>
    public delegate void BeforeCapabilitiesSetHandler(object sender, BeforeCapabilitiesSetHandlerArgs args);

    /// <summary>
    /// Before Capabilities Set Handker
    /// </summary>
    public class BeforeCapabilitiesSetHandlerArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BeforeCapabilitiesSetHandlerArgs"/> class.
        /// </summary>
        /// <param name="capabilities">The existing capabilities</param>
        public BeforeCapabilitiesSetHandlerArgs(DesiredCapabilities capabilities)
        {
            this.Capabilities = capabilities;
        }

        /// <summary>
        /// Gets the current capabilities
        /// </summary>
        public DesiredCapabilities Capabilities { get; }
    }
}