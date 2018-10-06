using System;

namespace Runerne.Instantiation
{
    /// <summary>
    /// An instance of this class is an instance provider that references another instance provider. The instance provider works like a 
    /// proxy by, when requested, returns the instance of the referenced instance provider.
    /// The class is immutable.
    /// </summary>
    public class ReferenceProvider : IInstanceProvider
    {
        /// <summary>
        /// The name of the referenced instance provider.
        /// </summary>
        public RIName Reference { get; }

        /// <summary>
        /// The type defined by the referenced instance provider.
        /// </summary>
        public Type DefinedType => GetInstanceProvider().DefinedType;

        private IContext _context;
        private IInstanceProvider _instanceProvider = null;

        /// <summary>
        /// Creates an instance of the reference instance provider.
        /// </summary>
        /// <param name="reference">The name by which the referenced instance provider is known.</param>
        /// <param name="context">The context in which the referenced instance provider is searched.</param>
        public ReferenceProvider(string reference, IContext context)
        {
            Reference = reference;
            _context = context;
        }

        /// <summary>
        /// Retrieves the referenced instance provider and requests it to provide an instance.
        /// </summary>
        /// <returns>The instance provided by the referenced instance provider.</returns>
        public object GetInstance()
        {
            return GetInstanceProvider().GetInstance();
        }

        private IInstanceProvider GetInstanceProvider()
        {
            if (_instanceProvider == null)
                _instanceProvider = _context.GetInstanceProvider(Reference);
            return _instanceProvider;
        }
    }
}
