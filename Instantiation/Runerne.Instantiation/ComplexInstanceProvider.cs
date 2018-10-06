using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Runerne.Instantiation
{
    /// <summary>
    /// An instance provider, which can provide an instantiate of a class.
    /// </summary>
    public class ComplexInstanceProvider : IInstanceProvider
    {
        public Type DefinedType { get; }
        private readonly IEnumerable<IInstanceProvider> _constructorParameters;
        private readonly IDictionary<string, IInstanceProvider> _propertyInitiators;
        private readonly ConstructorInfo _constructor;
        private object _instance;

        /// <summary>
        /// Creates an instance of the class with the specified type of the class that it can provide an instance of.
        /// The constructor parameters and initial property values are specified.
        /// </summary>
        /// <param name="definedType">The type of the instance that an instance of this class can provide.</param>
        /// <param name="constructorParameters">The instance providers, which provides values for the constructor parameters.</param>
        /// <param name="propertyInitiators">The property names and associated instance providers, that will be used to initialize the 
        /// properties of the provided instance.</param>
        public ComplexInstanceProvider(Type definedType, IEnumerable<IInstanceProvider> constructorParameters, IDictionary<string, IInstanceProvider> propertyInitiators)
        {
            DefinedType = definedType;
            _constructorParameters = constructorParameters;
            _propertyInitiators = propertyInitiators;

            var constructorParametersTypes = new List<Type>();

            foreach (var instanceProvider in _constructorParameters)
                constructorParametersTypes.Add(instanceProvider.DefinedType);

            _constructor = FindConstructor(constructorParametersTypes.ToArray());
            if (_constructor == null)
                throw new NoMatchingConstructorException(this, constructorParametersTypes);

            // Validate Property Initiators
            foreach (var propertyInitiator in propertyInitiators)
            {
                var propertyName = propertyInitiator.Key;
                if (DefinedType.GetField(propertyName) != null)
                    continue;

                if (DefinedType.GetProperty(propertyName) != null)
                    continue;

                throw new NoMatchingPropertyException(this, propertyName);
            }
        }

        /// <summary>
        /// Implementation of the <see cref="IInstanceProvider"/> interface.
        /// </summary>
        /// <returns>The created instance.</returns>
        public object GetInstance()
        {
            if (_instance == null)
                CreateInstance();
            return _instance;
        }

        private ConstructorInfo FindConstructor(IEnumerable<Type> constructorArgumentTypes)
        {
            var argumentTypes = constructorArgumentTypes.ToArray();
            return DefinedType.GetConstructor(argumentTypes);
        }

        private void CreateInstance()
        {
            var constructorArguments = new List<object>();
            foreach (var instanceProvider in _constructorParameters)
                constructorArguments.Add(instanceProvider.GetInstance());

            _instance = _constructor.Invoke(constructorArguments.ToArray());

            foreach (var propertyName in _propertyInitiators.Keys)
            {
                var value = _propertyInitiators[propertyName].GetInstance();
                {
                    var field = _instance.GetType().GetField(propertyName);
                    if (field != null)
                    {
                        field.SetValue(_instance, value);
                        continue;
                    }
                }

                {
                    var property = _instance.GetType().GetProperty(propertyName);
                    if (property != null)
                    {
                        property.SetValue(_instance, value);
                        continue;
                    }
                }

                throw new RIException($"Unable to set the value into neither property or field named '{propertyName}'.");
            }
        }
    }
}
