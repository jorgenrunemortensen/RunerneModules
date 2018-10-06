using System;
using System.Runtime.Serialization;

namespace Runerne.Instantiation
{
    /// <summary>
    /// An instance of this exception indicates that the requested instance did not descend from the requested type.
    /// </summary>
    [Serializable]
    public class IncompatibleInstanceTypeException : RIException
    {
        /// <summary>
        /// The context in which the instance was requested.
        /// </summary>
        public IContext Context { get; }

        /// <summary>
        /// The name of the instance that was requested.
        /// </summary>
        public RIName Name { get; }

        /// <summary>
        /// The instance provider, which was unable to provide an instance of the specified type.
        /// </summary>
        public IInstanceProvider InstanceProvider { get; }

        /// <summary>
        /// Creates an instance of this exception.
        /// </summary>
        /// <param name="context">The context in which the instance was requested.</param>
        /// <param name="name">The name of the instance that was requested.</param>
        /// <param name="instanceProvider">The instance provider, which was unable to provide an instance of the specified type.</param>
        /// <param name="reason"></param>
        internal IncompatibleInstanceTypeException(IContext context, RIName name, IInstanceProvider instanceProvider, string reason)
            : base($"The instance by the name '{name}' cannot be instantiated. {reason}")
        {
            Context = context;
            Name = name;
            InstanceProvider = instanceProvider;
        }

        protected IncompatibleInstanceTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

    }
}
