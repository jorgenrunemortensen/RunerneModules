using System;
using System.Runtime.Serialization;

namespace Runerne.Utilities
{
    /// <summary>
    /// Common exception class from which exceptions specific for the <see cref="ValueUtilities"/> are thrown.
    /// </summary>
    [Serializable]
    public class ValueException : Exception
    {
        /// <summary>
        /// Creates an instance of this exception with the specified message.
        /// </summary>
        /// <param name="message">The message contained in the exception.</param>
        internal ValueException(string message)
            : base(message)
        {
        }

        public ValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
