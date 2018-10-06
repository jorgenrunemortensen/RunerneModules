using System;
using System.Runtime.Serialization;

namespace Runerne.Instantiation
{
    /// <summary>
    /// An instance of this exception indicates an attempt to add an instance by an already existing name.
    /// </summary>
    [Serializable]
    public class DuplicateInstanceNameException : RIException
    {
        /// <summary>
        /// The name of the instance that is duplicated.
        /// </summary>
        public RIName Name { get; }

        /// <summary>
        /// Creates an instance of this exception.
        /// </summary>
        /// <param name="name">The name that is duplicated.</param>
        public DuplicateInstanceNameException(RIName name)
            : base($"An instance by the name '{name}' is already contained in the context.")
        {
            Name = name;
        }

        protected DuplicateInstanceNameException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
