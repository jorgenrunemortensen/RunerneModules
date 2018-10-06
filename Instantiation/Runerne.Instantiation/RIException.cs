using System;
using System.Runtime.Serialization;

namespace Runerne.Instantiation
{
    /// <summary>
    /// Base exception for all exceptions defined in this assembly.
    /// </summary>
    [Serializable]
    public class RIException : Exception
    {
        /// <summary>
        /// Creates an instance of this exception.
        /// </summary>
        /// <param name="message">The message which informs the reason for the exception.</param>
        internal RIException(string message)
            : base(message)
        {
        }

        protected RIException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
