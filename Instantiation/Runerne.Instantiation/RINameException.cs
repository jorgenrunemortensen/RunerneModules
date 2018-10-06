using System;
using System.Runtime.Serialization;

namespace Runerne.Instantiation
{
    /// <summary>
    /// An instance of this exception indicates that there was an exception when processing a RIName.
    /// </summary>
    [Serializable]
    public class RINameException : RIException
    {
        /// <summary>
        /// Creates an instance of this exception.
        /// </summary>
        /// <param name="message">The reason for this exception.</param>
        internal RINameException(string message) : base(message)
        {
        }

        protected RINameException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
