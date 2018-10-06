using System;
using System.Runtime.Serialization;

namespace Runerne.Utilities
{
    /// <summary>
    /// Indicates that the parsing of a value failed.
    /// </summary>
    [Serializable]
    public class ParseException : ValueException
    {
        /// <summary>
        /// The value which could not be parsed.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// The reason why the parsing failed.
        /// </summary>
        public string Reason { get; }

        /// <summary>
        /// Creates an instance of this class, with the value that could not be parsed and the reason.
        /// </summary>
        /// <param name="value">The value that could not be parsed.</param>
        /// <param name="reason">The reason why the value could not be parsed.</param>
        internal ParseException(string value, string reason)
            : base($"Unable to parse '{value}'. {reason}")
        {
            Value = value;
            Reason = reason;
        }

        public ParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
