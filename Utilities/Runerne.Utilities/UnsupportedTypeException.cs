using System;
using System.Runtime.Serialization;

namespace Runerne.Utilities
{
    /// <summary>
    /// Indicates that the specified value could not be boxed in a variable of the specified type.
    /// </summary>
    [Serializable]
    public class UnsupportedTypeException : ParseException
    {
        /// <summary>
        /// The value that could not be boxed.
        /// </summary>
        public new string Value { get; }

        /// <summary>
        /// The name of the type into which the value was attempted boxed.
        /// </summary>
        public string TypeName { get; }

        /// <summary>
        /// Creates an instance of this class, with the value which was attempted boxed and the name of the type into which it was
        /// attempted boxed.
        /// </summary>
        /// <param name="value">The value that was attempted boxed.</param>
        /// <param name="typeName">The type of the variable into which the value was attempted boxed.</param>
        internal UnsupportedTypeException(string value, string typeName)
            : base(value, $"The type '{typeName}' is not supported.")
        {
            Value = value;
            TypeName = typeName;
        }

        public UnsupportedTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
