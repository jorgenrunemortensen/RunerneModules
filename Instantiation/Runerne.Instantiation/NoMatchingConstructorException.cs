using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Runerne.Instantiation
{
    /// <summary>
    /// An instance of this exception indicates, that the class of the requested instance did not have any constructor that matches the 
    /// provided instance providers defined types. This exception can be thrown when attempting to instantiate a
    /// <see cref="ComplexInstanceProvider"/>.
    /// </summary>
    [Serializable]
    public class NoMatchingConstructorException : RIException
    {
        private ComplexInstanceProvider _complexInstanceProvider;
        private IEnumerable<Type> _constructorArgumentTypes;

        /// <summary>
        /// Creates an instance of this exception.
        /// </summary>
        /// <param name="complexInstanceProvider">The complex instance provider, that was attempting to create an instance.</param>
        /// <param name="constructorArgumentTypes">The types of instances, which should be used for creating the instance.</param>
        internal NoMatchingConstructorException(ComplexInstanceProvider complexInstanceProvider, IEnumerable<Type> constructorArgumentTypes)
            : base($"No matching constructor could be found for Class Instance Provider '{complexInstanceProvider.DefinedType}' taking following argument types:{Environment.NewLine}  {ToString(constructorArgumentTypes)}.{Environment.NewLine}The following constructors are available:{Environment.NewLine}{GetConstructorSignatures(complexInstanceProvider.DefinedType)}")
        {
            _complexInstanceProvider = complexInstanceProvider;
            _constructorArgumentTypes = constructorArgumentTypes;
        }

        protected NoMatchingConstructorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string GetConstructorSignatures(Type type)
        {
            var s = "";
            foreach (var constructorInfo in type.GetConstructors())
            {
                if (!constructorInfo.IsPublic)
                    continue;

                if (s.Length > 0)
                    s += Environment.NewLine;

                s += "  (" + GetConstructorSignature(constructorInfo) + ")";
            }
            return s;
        }

        private static string GetConstructorSignature(ConstructorInfo constructorInfo)
        {
            var arguments = new List<string>();
            foreach (var parameterInfo in constructorInfo.GetParameters())
                arguments.Add(parameterInfo.ParameterType.FullName);

            return string.Join(", ", arguments);
        }

        private static string ToString(IEnumerable<Type> argumentTypes)
        {
            var typeNames = new List<string>();
            foreach (var argumentType in argumentTypes)
                typeNames.Add(argumentType.FullName);

            return string.Join(", ", typeNames);
        }
    }
}
