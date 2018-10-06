using System;
using System.Runtime.Serialization;

namespace Runerne.Instantiation
{
    /// <summary>
    /// An instance of this exception indicates, that there was no instance provider registered in the context by the given name.
    /// </summary>
    [Serializable]
    public class NoSuchInstanceProviderException : RIException
    {
        /// <summary>
        /// The name by which no instance provider could be found.
        /// </summary>
        public RIName Name { get; }

        /// <summary>
        /// Creates an instance of this exception.
        /// </summary>
        /// <param name="name">The name by which no instance provider could be found.</param>
        internal NoSuchInstanceProviderException(RIName name)
            : base($"No instance provider found by the name '{name}'.")
        {
            Name = name;
        }

        protected NoSuchInstanceProviderException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
