using System;

namespace Runerne.Instantiation
{
    /// <summary>
    /// Common interface for providers, which can provides an instance of a specific type.
    /// </summary>
    public interface IInstanceProvider
    {
        /// <summary>
        /// The type of the instance which is provided by the implementation of the provider.
        /// </summary>
        /// <returns>The type of the instance.</returns>
        Type DefinedType { get; }

        /// <summary>
        /// Implementations of this method shall provide an instance of the type, which can be retrieved by calling the function <see cref="GetDefinedType"/>.
        /// Whether the method shall contain the instance as a singleton is up to the implementation.
        /// </summary>
        /// <returns>An instance of the defined type.</returns>
        object GetInstance();
    }
}
