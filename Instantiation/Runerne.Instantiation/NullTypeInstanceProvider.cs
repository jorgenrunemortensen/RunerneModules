using System;

namespace Runerne.Instantiation
{
    /// <summary>
    /// An instance of this class is able to create a null instance (null) by a given type.
    /// </summary>
    public class NullTypeInstanceProvider : IInstanceProvider
    {
        /// <summary>
        /// The type of null instance, that the instance provider is known by.
        /// </summary>
        public Type DefinedType { get; }

        /// <summary>
        /// Creates an instance of the instance provider.
        /// </summary>
        /// <param name="definedType">The type of null instance, that the instance provider is known by.</param>
        public NullTypeInstanceProvider(Type definedType)
        {
            DefinedType = definedType;
        }

        /// <summary>
        /// Implementation of the <see cref="IInstanceProvider"/> interface. Creates the null-instance.
        /// </summary>
        /// <returns></returns>
        public object GetInstance()
        {
            return null;
        }
    }
}
