using System;
using System.Collections.Generic;

namespace Runerne.Utilities
{
    /// <summary>
    /// General methods for working with enum values and converting between enum values to and texts.
    /// </summary>
    public static class EnumUtilities
    {
        /// <summary>
        /// Convert an enumeration value to string by use of the built-in ToString() function.
        /// </summary>
        /// <typeparam name="TEnumType">The enumeration type of the parameter value</typeparam>
        /// <param name="value">The enumeration value to convert</param>
        /// <param name="forceUppercase">If true, convert the result to uppercase</param>
        /// <returns>The corresponding string representation</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public static string ToText<TEnumType>(TEnumType value, bool forceUppercase = false)
        {
            if (forceUppercase)
            {
                return value.ToString().ToUpperInvariant();
            }
            else
            {
                return value.ToString();
            }
        }

        /// <summary>
        /// Converts a string to an enumeration value.
        /// If the conversion fails, an exception in throw with a detailed description of the reason.
        /// </summary>
        /// <typeparam name="T">The enum-type into which the name is to be converted.</typeparam>
        /// <param name="name">The name of the enum value.</param>
        /// <param name="ignoreCase">Indicates whether the conversion should ignoreCases. (By default the parameter is false).</param>
        /// <returns>The converted enumeration value.</returns>
        public static T ToValue<T>(string name, bool ignoreCase = true)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), name, ignoreCase);
            }
            catch (ArgumentException)
            {
                var t = typeof(T);
                var options = string.Join(", ", Enum.GetNames(t));
                throw new Exception($"Unable to convert \"{name}\" into a {t.FullName}. \"{name}\" is not a member of the enumeration. The following options are available: {options}.");
            }
        }

        /// <summary>
        /// Maps the provided name into an enumeration value by the use of a dictionary. If no mapping can be made an exception is thrown.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="name">The name that is mapped into an enumeration.</param>
        /// <param name="map">The dictionary that maps the name into an enumeration.</param>
        /// <returns>The result of the mapping.</returns>
        public static T ToValue<T>(string name, IDictionary<string, T> map)
        {
            if (!map.ContainsKey(name))
            {
                Type valueType = map.GetType().GetGenericArguments()[1];
                var options = string.Join(", ", map.Keys);
                throw new Exception($"Unable to map \"{name}\" into a {valueType.FullName}. Please use one of the following options: {options}.");
            }

            return map[name];
        }

        /// <summary>
        /// Converts a string to a nullable enumeration value. If the input string is null, then null is returned.
        /// If the conversion fails, an exception in throw with a detailed description of the reason.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="name"></param>
        /// <param name="ignoreCase">Indicates whether the conversion should ignoreCases. (By default the parameter is false).</param>
        /// <returns>The converted enumeration value or null.</returns>
        public static T? ToNullableValue<T>(string name, bool ignoreCase = true) where T : struct
        {
            if (name == null)
            {
                return null;
            }
            return ToValue<T>(name);
        }

        /// <summary>
        /// Maps the provided name into a nullable enumeration value by the use of a dictionary. If the input name is null, then null is 
        /// returned. If no mapping can be made an exception is thrown.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="name">The name that is mapped into an enumeration.</param>
        /// <param name="map">The dictionary that maps the name into an enumeration.</param>
        /// <returns></returns>
        public static T? ToNullableValue<T>(string name, IDictionary<string, T> map) where T : struct
        {
            if (name == null)
            {
                return null;
            }
            return ToValue<T>(name, map);
        }
    }
}
