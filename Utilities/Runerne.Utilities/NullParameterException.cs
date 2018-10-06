using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

[assembly: InternalsVisibleTo("Runerne.Utilities.UnitTest")]
namespace Runerne.Utilities
{
    /// <summary>
    /// Indicates that a parameter to a method was null and that the method do not allow that.
    /// </summary>
    [Serializable]
    public class NullParameterException : ValueException
    {
        /// <summary>
        /// The name of the parameter, which was null. This value is provided by the constructor.
        /// </summary>
        public string ParameterName { get; }

        /// <summary>
        /// Creates an instance of this class, with the name of the parameter, which was null.
        /// </summary>
        /// <param name="parameterName">The name of the parameter, which was null.</param>
        internal NullParameterException(string parameterName)
            : base($"The parameter '{parameterName}' cannot be null.")
        {
            ParameterName = parameterName;
        }

        public NullParameterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
