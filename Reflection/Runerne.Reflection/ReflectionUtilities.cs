using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Runerne.Reflection
{
    public static class ReflectionUtilities
    {
        public static IEnumerable<Type> GetImplementators(Type theInterface, params string[] assemblyFullPathes)
        {
            var interfaceName = theInterface.FullName;
            return GetImplementators(interfaceName, assemblyFullPathes);
        }

        public static IEnumerable<Type> GetImplementators(string fullInterfaceName, params string[] assemblyFullPathes)
        {
            var implementators = new List<Type>();

            foreach (var assemblyFullPath in assemblyFullPathes)
                CollectImplementators(fullInterfaceName, assemblyFullPath, implementators);

            return implementators;
        }

        public static ConstructorInfo GetBestMatchingConstructor(Type type, IEnumerable<Type> parameterTypes)
        {
            var parameterTypeList = parameterTypes.ToList();
            
            for(;;)
            {
                var constructorInfo = type.GetConstructor(parameterTypeList.ToArray());
                if(constructorInfo != null)
                    return constructorInfo;

                if (parameterTypeList.Count == 0)
                {
                    var parameterTypeNameList = new List<string>();
                    foreach(var parameterType in parameterTypes)
                        parameterTypeNameList.Add(parameterType.ToString());
                    throw new Exception($"No constructor found for {typeof (Type)} matching the following types: {string.Join(", ", parameterTypeNameList.ToArray())}");
                }
                parameterTypeList.RemoveAt(parameterTypeList.Count - 1);
            }            
        }

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
            foreach(var type in assembly.GetTypes())
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
            foreach(var implementedType in type.GetInterfaces())
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

        public static Type GetCommonClass(Type type1, params Type[] types)
        {
            var mostCommon = type1;
            foreach (var candidate in types)
                mostCommon = GetMostCommonClass(mostCommon, candidate);

            return mostCommon;
        }
    }
}
