using System;
using System.Runtime.Serialization;

namespace Runerne.Instantiation
{
    /// <summary>
    /// An instance of this exception indicates, that the class of the requested instance did not have any any property that matches the 
    /// specified name.
    /// </summary>
    [Serializable]
    public class NoMatchingPropertyException : RIException
    {
        /// <summary>
        /// The complex instance provider, that was attempting to create an instance.</param>
        /// </summary>
        public ComplexInstanceProvider ComplexInstanceProvider { get; }

        /// <summary>
        /// The name of the property that could not be found.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Creates an instance of this exception.
        /// </summary>
        /// <param name="complexInstanceProvider">The complex instance provider, that was attempting to create an instance.</param>
        /// <param name="propertyName">The name of the property that could not be found.</param>
        internal NoMatchingPropertyException(ComplexInstanceProvider complexInstanceProvider, string propertyName) :
            base($"No property was found by the name: '{propertyName}' in Class Instance Provider '{complexInstanceProvider.DefinedType}'.")
        {
            ComplexInstanceProvider = complexInstanceProvider;
            PropertyName = propertyName;
        }

        protected NoMatchingPropertyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
