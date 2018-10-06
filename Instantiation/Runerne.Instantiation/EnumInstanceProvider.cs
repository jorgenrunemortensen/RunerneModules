using System;

namespace Runerne.Instantiation
{
    /// <summary>
    /// Can provide an instance of a specific enum type with a specific value.
    /// </summary>
    public class EnumInstanceProvider : IInstanceProvider
    {
        /// <summary>
        /// The type of the enum that can be provided.
        /// </summary>
        public Type DefinedType { get; }

        private readonly object _value;

        /// <summary>
        /// Creates an instance of this type with the specified initial value and type of the instance that can be provided.
        /// </summary>
        /// <param name="value">The initial value of the instance that can be provided.</param>
        /// <param name="enumType">The enum type of the instance that can be provided.</param>
        public EnumInstanceProvider(object value, Type enumType)
        {
            DefinedType = enumType;
            _value = value;
        }

        /// <summary>
        /// Provides the enum instance as specified.
        /// </summary>
        /// <returns>The enum instance.</returns>
        public object GetInstance()
        {
            return _value;
        }
    }
}
