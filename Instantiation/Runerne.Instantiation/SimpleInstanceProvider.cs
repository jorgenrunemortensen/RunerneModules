using System;

namespace Runerne.Instantiation
{
    /// <summary>
    /// An instance of this class can create a simple instance based in its configuration.
    /// The class is immutable.
    /// </summary>
    public class SimpleInstanceProvider : IInstanceProvider
    {
        /// <summary>
        /// The type of the instance that can be provided.
        /// </summary>
        public Type DefinedType => _instance.GetType();

        private readonly object _instance;

        /// <summary>
        /// Creates an instance of the simple instance provider. When requested, the instance provider provides the specified instance.
        /// </summary>
        /// <param name="instance">The instance that this instance provider can return.</param>
        public SimpleInstanceProvider(object instance)
        {
            _instance = instance;
        }

        /// <summary>
        /// Returns the instance, that was specified in the constructor (<see cref="SimpleInstanceProvider.SimpleInstanceProvider(object)"/>).
        /// </summary>
        /// <returns>The instance.</returns>
        public object GetInstance()
        {
            return _instance;
        }
    }
}
