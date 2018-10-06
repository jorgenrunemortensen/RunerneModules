using System;
using System.Runtime.Serialization;

namespace Runerne.Instantiation
{
    /// <summary>
    /// An instance of this exception indicates that an instance was not found in context by the specified name.
    /// </summary>
    [Serializable]
    public class InstanceNotFoundException : RIException
    {
        /// <summary>
        /// The context in which the instance was requested.
        /// </summary>
        public IContext Context { get; }

        /// <summary>
        /// The name of the instance that was requested.
        /// </summary>
        public RIName InstanceName { get; }

        /// <summary>
        /// Create an instance of this exception.
        /// </summary>
        /// <param name="context">The context in which the instance was requested.</param>
        /// <param name="instanceName">The name of the instance that was requested.</param>
        internal InstanceNotFoundException(IContext context, RIName instanceName) : base($"No instance found in context by the name '{instanceName}'.")
        {
            Context = context;
            InstanceName = instanceName;
        }

        protected InstanceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}