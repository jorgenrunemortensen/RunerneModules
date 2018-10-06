using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Runerne.Instantiation.UnitTest")]
namespace Runerne.Instantiation
{
    /// <summary>
    /// In the context all instance providers are registered as well as the context controls which assemblies to load dynamically.
    /// </summary>
    public class RIContext : IContext
    {
        private readonly IDictionary<RIName, IInstanceProvider> _instanceProviderDictionary = new Dictionary<RIName, IInstanceProvider>();
        private readonly IDictionary<string, Assembly> _includedAssemblies = new Dictionary<string, Assembly>();

        /// <summary>
        /// Looks up all registered instance providers for one registered by the specified name. When found the instance provider is 
        /// requested to provide its instance, which hereafter is returned.
        /// If no such instance provider is found, then an <see cref="InstanceNotFoundException"/> is thrown.
        /// </summary>
        /// <param name="name">The name of the instance provider to look up.</param>
        /// <returns>The instance returned by the found instance provider.</returns>
        public object GetInstance(RIName name)
        {
            IInstanceProvider instanceProvider;
            if (!_instanceProviderDictionary.TryGetValue(name, out instanceProvider))
                throw new InstanceNotFoundException(this, name);
            return instanceProvider.GetInstance();
        }

        /// <summary>
        /// Looks up all registered instance providers for one registered by the specified name. When found the instance provider is 
        /// requested to provide its instance. Hereafter the method verifies whether the provided instance is a descendant of the specified
        /// type (checkType). If this is the case, then the instance is returned.
        /// If no such instance provider is found, then an <see cref= "InstanceNotFoundException"/> is thrown.
        /// If the instance was not a descendant of the specified type, then an <see cref="IncompatibleInstanceTypeException"/> is thrown.
        /// </summary>
        /// <param name="name">The name of the instance provider to look up.</param>
        /// <param name="checkType">The type against which the type of the provided instance is tested.</param>
        /// <returns>The provided instance.</returns>
        public object GetInstance(RIName name, Type checkType)
        {
            IInstanceProvider instanceProvider;
            if (!_instanceProviderDictionary.TryGetValue(name, out instanceProvider))
                throw new InstanceNotFoundException(this, name);

            var definedInstanceType = instanceProvider.DefinedType;
            if (!checkType.IsAssignableFrom(definedInstanceType))
                throw new IncompatibleInstanceTypeException(this, name, instanceProvider, $"The type {definedInstanceType.FullName} does not implement or inherit {checkType.FullName}.");

            return instanceProvider.GetInstance();
        }

        /// <summary>
        /// Looks up all registered instance providers for one registered by the specified name. When found the instance provider is 
        /// requested to provide its instance. Hereafter the method verifies whether the provided instance is a descendant of the specified
        /// type (T). If this is the case, then the instance is casted to the specified type and then returned.
        /// If no such instance provider is found, then an <see cref= "InstanceNotFoundException"/> is thrown.
        /// If the instance was not a descendant of the specified type, then an <see cref="IncompatibleInstanceTypeException"/> is thrown.
        /// </summary>
        /// <typeparam name="T">The type by which the instance is checked and casted before returned.</typeparam>
        /// <param name="instanceName">The name by which the instance provider is registered.</param>
        /// <returns>The provided instance.</returns>
        public T GetInstance<T>(RIName instanceName)
        {
            return (T)GetInstance(instanceName, typeof(T));
        }

        /// <summary>
        /// Retrieves the instance provider that is registered by the specified name. If no such instance provider is found then an
        /// <see cref="NoSuchInstanceProviderException"/> is thrown.
        /// </summary>
        /// <param name="name">The name of the instance provider that is retrieved.</param>
        /// <returns>The found sinstance provider.</returns>
        public IInstanceProvider GetInstanceProvider(RIName name)
        {
            if (!_instanceProviderDictionary.ContainsKey(name))
                throw new NoSuchInstanceProviderException(name);

            return _instanceProviderDictionary[name];
        }

        /// <summary>
        /// Registers an instance provider in the context by the specified name. If an instance provider by the same name already exists, 
        /// then a <see cref="DuplicateInstanceNameException"/> is thrown.
        /// </summary>
        /// <param name="name">The name by which the instance provider will be registered in the context.</param>
        /// <param name="instanceProvider">The instance provider that is registered.</param>
        public void AddNamedInstanceProvider(RIName name, IInstanceProvider instanceProvider)
        {
            if (_instanceProviderDictionary.ContainsKey(name))
                throw new DuplicateInstanceNameException(name);

            _instanceProviderDictionary[name] = instanceProvider;
        }

        /// <summary>
        /// Registers that the specified assembly shall be registered in the context.
        /// </summary>
        /// <param name="assemblyPath">The path to the assembly.</param>
        public void IncludeAssembly(string assemblyPath)
        {
            var assemblyFileInfo = new FileInfo(assemblyPath);
            var fullQualifiedAssemblyPath = assemblyFileInfo.FullName;
            if (_includedAssemblies.ContainsKey(fullQualifiedAssemblyPath))
                return;

            var assemblyName = AssemblyName.GetAssemblyName(fullQualifiedAssemblyPath);
            var assembly = Assembly.Load(assemblyName);
            _includedAssemblies[fullQualifiedAssemblyPath] = assembly;
        }

        /// <summary>
        /// Determines a type from a provided type name.
        /// The method maps the reserved words: “string” and “double” to their atomic types.All other types are determined by first looking up the types in the executing assembly and then in the current domain.Finally, the included assemblies are searched.
        /// If no matching type are found an exception is thrown.
        /// </summary>
        /// <param name="name">The name of the type, that the method searches for.</param>
        /// <returns>The type that has the provided name.</returns>
        public Type GetType(string name)
        {
            if (name == "string")
                return typeof(string);

            if (name == "double")
                return typeof(double);

            {
                var candidate = Assembly.GetExecutingAssembly().GetType(name);
                if (candidate != null)
                    return candidate;
            }

            {
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    var candidate = assembly.GetType(name);
                    if (candidate != null)
                        return candidate;
                }
            }

            {
                foreach (var assembly in _includedAssemblies.Values)
                {
                    var candidate = assembly.GetType(name);
                    if (candidate != null)
                        return candidate;
                }
            }
            throw new Exception($"No type was found by the name '{name}'.");
        }

        /// <summary>
        /// Returns a collection of all assemblies, which are registered by the context.
        /// </summary>
        /// <returns>The registered assemblies.</returns>
        public IEnumerable<Assembly> GetIncludedAssemblies()
        {
            return _includedAssemblies.Values;
        }
    }
}
