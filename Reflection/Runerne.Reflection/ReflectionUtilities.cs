using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Runerne.Reflection
{
    /// <summary>
    /// Utility that supports determining types based in specific parameters.
    /// </summary>
    public static class ReflectionUtilities
    {
        /// <summary>
        /// Find the types which implements a given interface.
        /// </summary>
        /// <param name="theInterface">The interface that the returned types implements.</param>
        /// <param name="assemblyFullPathes">The pathes to the assemblies, which types are searched.</param>
        /// <returns>The types contained in the specified assemblies and which implements the specified interface.</returns>
        public static IEnumerable<Type> GetImplementators(Type theInterface, params string[] assemblyFullPathes)
        {
            var interfaceName = theInterface.FullName;
            return GetImplementators(interfaceName, assemblyFullPathes);
        }

        /// <summary>
        /// Find the types which implements a given interface.
        /// </summary>
        /// <param name="fullInterfaceName">The fully qualified name of the interface that the returned types implements.</param>
        /// <param name="assemblyFullPathes">The pathes to the assemblies, which types are searched.</param>
        /// <returns>The types contained in the specified assemblies and which implements the specified interface.</returns>
        public static IEnumerable<Type> GetImplementators(string fullInterfaceName, params string[] assemblyFullPathes)
        {
            var implementators = new List<Type>();

            foreach (var assemblyFullPath in assemblyFullPathes)
                CollectImplementators(fullInterfaceName, assemblyFullPath, implementators);

            return implementators;
        }

        /// <summary>
        /// Determines the constructor of a class that best matches the specified parameter types.
        /// </summary>
        /// <param name="type">The class.</param>
        /// <param name="parameterTypes">The constructor parameter types.</param>
        /// <returns>The best matching constructor.</returns>
        public static ConstructorInfo GetBestMatchingConstructor(Type type, IEnumerable<Type> parameterTypes)
        {
            var parameterTypeList = parameterTypes.ToList(); // A list of parameter types, that the method can manipulate.

            for (; ; )
            {
                var constructorInfo = type.GetConstructor(parameterTypeList.ToArray());
                if (constructorInfo != null)
                    return constructorInfo; // Found a match

                if (parameterTypeList.Count == 0)
                {
                    // Nothing found -> Build and throw an exception.
                    var parameterTypeNameList = new List<string>();
                    foreach (var parameterType in parameterTypes)
                        parameterTypeNameList.Add(parameterType.ToString());

                    throw new Exception($"No constructor found for {typeof(Type)} matching the following types: {string.Join(", ", parameterTypeNameList.ToArray())}");
                }

                // Let us remove the last parameter type.
                parameterTypeList.RemoveAt(parameterTypeList.Count - 1);
            }
        }

        /// <summary>
        /// Creates an instance of a type by using the constructor and parameters provided.
        /// </summary>
        /// <param name="constructorInfo">The constructor used for creating the instance.</param>
        /// <param name="parameters">The parameters for the constructor.</param>
        /// <returns>The created instance.</returns>
        public static object CreateInstance(ConstructorInfo constructorInfo, params object[] parameters)
        {
            var instance = constructorInfo.Invoke(parameters);
            return instance;
        }

        /// <summary>
        /// Instantiates a class by finding the constructor, which matches the provided parameters best.
        /// The strategy of how to find the best matching constructor is to find the one, which consumes as many of 
        /// the supplied parameters as possible.
        /// </summary>
        /// <param name="type">The class, which is to be instantiated.</param>
        /// <param name="parameters">Parameters, which can be parsed to the constructor.</param>
        /// <returns>The craeted instance.</returns>
        public static object CreateInstance(Type type, params object[] parameters)
        {
            // Make a list of parameters types to the constructor.
            var constructorParameterTypes = new List<Type>();

            foreach (var providedParameter in parameters)
                if (providedParameter != null)
                    constructorParameterTypes.Add(providedParameter.GetType());

            // Ask the reflection utility to find the constructor of the command, which has the best match.
            var constructor = GetBestMatchingConstructor(type, constructorParameterTypes);

            // Credate the parameter list for the found constructor.
            var acceptedParameters = SetupParameters(constructor.GetParameters(), parameters);

            // Create and return the command.
            var instance = constructor.Invoke(acceptedParameters.ToArray());
            return instance;
        }

        private static IEnumerable<object> SetupParameters(IEnumerable<ParameterInfo> parameterInfos, params object[] values)
        {
            var parameters = new List<object>();

            foreach (var parameterInfo in parameterInfos)
            {
                var parameterType = parameterInfo.ParameterType;
                var value = GetFirstTypeMatchingValue(parameterType, values);
                parameters.Add(value);
            }

            return parameters;
        }

        private static object GetFirstTypeMatchingValue(Type type, object[] values)
        {
            foreach (var value in values)
            {
                var valueType = value.GetType();
                if (type.IsAssignableFrom(valueType))
                    return value;
            }
            return null;
        }

        private static void CollectImplementators(string fullInterfaceName, string assemblyFullPath, IList<Type> implementators)
        {
            var assembly = Assembly.LoadFrom(assemblyFullPath);
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsPublic)
                    continue;

                if (!type.IsClass)
                    continue;

                if (!TypeImplementsInterface(type, fullInterfaceName))
                    continue;

                implementators.Add(type);
            }
        }

        private static bool TypeImplementsInterface(Type type, string fullInterfaceName)
        {
            foreach (var implementedType in type.GetInterfaces())
            {
                var fullImplementedTypeName = implementedType.FullName;
                if (fullImplementedTypeName != fullInterfaceName)
                    continue;

                return true;
            }
            return false;
        }

        private static Type GetMostCommonClass(Type t1, Type t2)
        {
            if (t1.GetType() == typeof(object))
                return typeof(object);

            if (t2.GetType() == typeof(object))
                return typeof(object);

            if (t1.IsAssignableFrom(t2))
                return t1;

            if (t2.IsAssignableFrom(t1))
                return t2;

            var t1Parent = t1.BaseType;
            return GetMostCommonClass(t1Parent, t2);
        }

        /// <summary>
        /// Finds the most detailed but common class of the specified types.
        /// </summary>
        /// <param name="type1">The first specified type.</param>
        /// <param name="types">THe second, third and so forward types.</param>
        /// <returns>The most detailed but common class of the specified types.</returns>
        public static Type GetCommonClass(Type type1, params Type[] types)
        {
            var mostCommon = type1;
            foreach (var candidate in types)
                mostCommon = GetMostCommonClass(mostCommon, candidate);

            return mostCommon;
        }
    }
}
