using System;

namespace Objectivity.Test.Automation.Tests.NUnit.Helpers
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception to throw when problem with setting the test case name from parameters
    /// </summary>
    public class DataDrivenReadException : Exception
    {
        public DataDrivenReadException() { }

        public DataDrivenReadException(string message) : base(message) { }

        public DataDrivenReadException(string message, Exception innerException) : base(message, innerException) { }

        protected DataDrivenReadException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
