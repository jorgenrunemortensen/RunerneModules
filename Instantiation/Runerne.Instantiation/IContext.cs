using System;

namespace Runerne.Instantiation
{
    /// <summary>
    /// The implementation(s) of this interface provides access to the context of the Runerne Instantiation Framework. The interface 
    /// defines read-only access only.
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// Requests an instance registered in the framework and verifies that the instance is of a specific type or a descendant of the 
        /// type.
        /// </summary>
        /// <param name="instanceName">The name of the registered instance.</param>
        /// <param name="checkType">The type that the instance is verified against.</param>
        /// <returns>The instance.</returns>
        object GetInstance(RIName instanceName, Type checkType);

        /// <summary>
        /// Requests an instance registered in the framework and verifies that the instance is of a specific type (generic argument) or a 
        /// descendant of the type.
        /// </summary>
        /// <typeparam name="T">The type that the instance is verified against and to which the instance is casted.</typeparam>
        /// <param name="instanceName">The name of the registered instance.</param>
        /// <returns>The instance.</returns>
        T GetInstance<T>(RIName instanceName);

        /// <summary>
        /// Requests an instance registered in the framework.
        /// </summary>
        /// <param name="instanceName">The name of the registered instance.</param>
        /// <returns>The instance.</returns>
        object GetInstance(RIName instanceName);

        /// <summary>
        /// Retrieves the instance provider by the specified name.
        /// </summary>
        /// <param name="instanceName">The name of the instance provider.</param>
        /// <returns>The instance provider.</returns>
        IInstanceProvider GetInstanceProvider(RIName instanceName);
    }
}
