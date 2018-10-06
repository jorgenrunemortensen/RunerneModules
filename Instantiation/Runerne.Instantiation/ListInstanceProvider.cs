using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Runerne.Reflection;
using Runerne.Utilities;

namespace Runerne.Instantiation
{
    /// <summary>
    /// An instance of this class can provide a collection instance, which can be either a list or an array of elements.
    /// Instances of this class is immutable.
    /// </summary>
    public class ListInstanceProvider : IInstanceProvider
    {
        /// <summary>
        /// The supported collection types.
        /// </summary>
        public enum CollectionType
        {
            /// <summary>
            /// A list (List<>) of elements.
            /// </summary>
            List,

            /// <summary>
            /// An array of elements.
            /// </summary>
            Array,
        };

        /// <summary>
        /// The actual type of collection that the instance provider can provide.
        /// </summary>
        public Type DefinedType => GetDefinedType();

        private Type _elementType;
        private readonly IEnumerable<IInstanceProvider> _instanceProviders;
        private object _instance;
        private CollectionType _collectionType = CollectionType.List;

        /// <summary>
        /// Creates an instance of the instance provider.
        /// </summary>
        /// <param name="instanceProviders">Instance providers, which are used for creating the elements for the collection.</param>
        /// <param name="elementType">The type of the elements, which shall be contained in the collection.</param>
        /// <param name="collectionType">The type of collection that this instance provider shall provide. By default a list is used.</param>
        public ListInstanceProvider(IEnumerable<IInstanceProvider> instanceProviders, Type elementType, CollectionType collectionType = CollectionType.List)
        {
            _instanceProviders = instanceProviders;
            _elementType = elementType;
            _collectionType = collectionType;
        }

        /// <summary>
        /// Creates an instance of the instance provider. The element type is determined as being the most concrete type from which all elements inherit. 
        /// </summary>
        /// <param name="instanceProviders">Instance providers, which are used for creating the elements for the collection.</param>
        /// <param name="collectionType">The type of collection that this instance provider shall provide. By default a list is used.</param>
        public ListInstanceProvider(IEnumerable<IInstanceProvider> instanceProviders, CollectionType collectionType = CollectionType.List)
        {
            _instanceProviders = instanceProviders;
            _elementType = null;
            _collectionType = collectionType;
        }

        public object GetInstance()
        {
            return _instance ?? (_instance = CreateInstance());
        }

        private Type GetDefinedType()
        {
            switch (_collectionType)
            {
                case CollectionType.List:
                    return typeof(List<>).MakeGenericType(GetElementType());

                case CollectionType.Array:
                    return GetElementType().MakeArrayType();

                default:
                    throw new RIException($"Undefined collection type {_collectionType}.");
            }
        }

        private Type GetElementType()
        {
            if (_elementType != null)
                return _elementType;

            _elementType = DetermineCommonType(_instanceProviders);
            return _elementType;
        }

        /// <summary>
        /// Creates an instance of the collection according to the values specified in the constructor.
        /// </summary>
        /// <returns>The collection as an object (implementation of <see cref="IInstanceProvider"/>).</returns>
        private object CreateInstance()
        {
            var elementType = GetElementType();
            var list = CreateList(elementType);

            switch (_collectionType)
            {
                case CollectionType.List:
                    return list;

                case CollectionType.Array:
                    // Create an array and copy all the values from the list into the array.
                    var array = Array.CreateInstance(elementType, _instanceProviders.Count());
                    var i = 0;
                    foreach (var value in list)
                        array.SetValue(value, i++);
                    return array;

                default:
                    throw new RIException($"Undefined collection type {_collectionType}.");
            }
        }

        /// <summary>
        /// Creates a list of the specified element type and fills in the elements into the list.
        /// </summary>
        /// <param name="elementType">The element type of the list.</param>
        /// <returns>The list.</returns>
        private IList CreateList(Type elementType)
        {
            var genericType = typeof(List<>).MakeGenericType(elementType);
            var list = (IList)Activator.CreateInstance(genericType);
            foreach (var instanceProvider in _instanceProviders)
            {
                var instance = instanceProvider.GetInstance();
                var castedInstance = ValueUtilities.CastTo(instance, elementType);
                list.Add(castedInstance);
            }
            return list;
        }

        private static Type DetermineCommonType(IEnumerable<IInstanceProvider> instanceProviders)
        {
            Type mostCommonType = null;

            foreach (var instanceProvider in instanceProviders)
            {
                if (mostCommonType == null)
                {
                    mostCommonType = instanceProvider.DefinedType;
                    continue;
                }

                var definedType = instanceProvider.DefinedType;
                mostCommonType = ReflectionUtilities.GetCommonClass(mostCommonType, definedType);
            }

            return mostCommonType;
        }
    }
}
