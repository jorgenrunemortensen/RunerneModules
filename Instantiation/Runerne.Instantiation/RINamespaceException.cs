using System;
using System.Runtime.Serialization;

namespace Runerne.Instantiation
{
    [Serializable]
    internal class RINamespaceException : RIException
    {
        /// <summary>
        /// Creates an instance of this excetion.
        /// </summary>
        /// <param name="message">The reason for the exception.</param>
        internal RINamespaceException(string message) 
            : base(message)
        {
        }

        protected RINamespaceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}