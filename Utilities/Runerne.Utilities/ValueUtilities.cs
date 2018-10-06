using System;
using System.Collections.Generic;
using System.Linq;

namespace Runerne.Utilities
{
    /// <summary>
    /// ValueUtilities is a utility class containing methods and definitions for converting between basic numeric values of different types
    /// and strings. This includes methods for converting and casting between basic types.
    /// </summary>
    public static class ValueUtilities
    {
        /// <summary>
        /// Defines a parsing method, which parses the provided string into the best matching type.
        /// </summary>
        /// <param name="value">The string which is parsed.</param>
        /// <returns>The result of the parsing.</returns>
        public delegate object Parser(string value);

        /// <summary>
        /// Defines the integral parsers per type. The order of the keys defines the order by which parsing methods in the class is
        /// performed.
        /// </summary>
        public static IReadOnlyDictionary<Type, Parser> IntegralParsers => new Dictionary<Type, Parser>()
        {
            { typeof(sbyte), o => sbyte.Parse(o) },
            { typeof(byte), o => byte.Parse(o) },
            { typeof(short), o => short.Parse(o) },
            { typeof(ushort), o => ushort.Parse(o) },
            { typeof(int), o => int.Parse(o) },
            { typeof(uint), o => uint.Parse(o) },
            { typeof(long), o => long.Parse(o) },
            { typeof(ulong), o => ulong.Parse(o) },
        };

        /// <summary>
        /// Defines the decimal parsers per type. The order of the keys defines the order by which parsing methods in the class is
        /// performed.
        /// </summary>
        public static IDictionary<Type, Parser> DecimalParsers => new Dictionary<Type, Parser>()
        {
            { typeof(float), o => float.Parse(o) },
            { typeof(double), o => double.Parse(o) },
        };

        /// <summary>
        /// Defines the numeric parsers per type. The order of the keys defines the order by which parsing methods in the class is
        /// performed. The dictionary is the concatenation of the <see cref="IntegralParsers"/> and the <see cref="DecimalParsers"/>.
        /// </summary>
        public static IDictionary<Type, Parser> NumericParsers => IntegralParsers.Concat(DecimalParsers).ToDictionary(x => x.Key, x => x.Value);

        /// <summary>
        /// <para>Maps a predefined type name into the corresponding .NET type.</para>
        /// <para>The following types are mapped:</para>
        /// <para>sbyte, byte, short, ushort, int, uint, long, ulong, float, double, string.</para>
        /// </summary>
        public static IDictionary<string, Type> TypeNames => new Dictionary<string, Type>()
        {
            { "sbyte", typeof(sbyte) },
            { "byte", typeof(byte) },
            { "short", typeof(short) },
            { "ushort", typeof(ushort) },
            { "int", typeof(int) },
            { "uint", typeof(uint) },
            { "long", typeof(long) },
            { "ulong", typeof(ulong) },
            { "float", typeof(float) },
            { "double", typeof(double) },
            { "string", typeof(string) },
            { "boolean", typeof(bool) },
        };

        /// <summary>
        /// Parses the provided string into an integral type by using the parsers defined by the <see cref="IntegralParsers"/>.
        /// The method starts with the first defined parser in <see cref="IntegralParsers"/> or the parser which matches the provided 
        /// <see cref="minimumType"/> if provided. When any of the parser succeeds the parsing, then the result is returned. If the string could not be parsed, then 
        /// an <see cref="ParseException"/> is thrown.
        /// </summary>
        /// <param name="s">The string which will be parsed.</param>
        /// <param name="minimumType">The minimum type to which the method will parse the provided string.</param>
        /// /// <returns>The integral type containing the result of the parsing.</returns>
        public static object ParseIntegral(string s, Type minimumType = null)
        {
            var foundMinimum = minimumType == null || minimumType == typeof(sbyte);
            foreach (var integralParser in IntegralParsers)
            {
                if (!foundMinimum)
                {
                    foundMinimum = integralParser.Key == minimumType;
                    if (!foundMinimum)
                        continue;
                }

                try
                {
                    return integralParser.Value(s);
                }
                catch (OverflowException)
                {
                }
                catch (FormatException)
                {
                }
            }
            throw new ParseException(s, "The value does not represent an integral value.");
        }

        /// <summary>
        /// Parses the provided string into a decimal type by using the parsers defined by the <see cref="DecimalParsers"/>.
        /// The method starts with the first defined parser in DecimalParsers or the <see cref="minimumType"/> if provided. When any of the parser succeeds the parsing, then the result
        /// is returned. If the string could not be parsed, then an <see cref="ParseException"/> is thrown.
        /// </summary>
        /// <param name="s">The string which will be parsed.</param>
        /// <param name="minimumType">The minimum type to which the method will parse the provided string.</param>
        /// <returns>The decimal type containing the result of the parsing.</returns>
        public static object ParseDecimals(string s, Type minimumType = null)
        {
            var foundMinimum = minimumType == null || minimumType == typeof(sbyte);
            foreach (var decimalParser in DecimalParsers)
            {
                if (!foundMinimum)
                {
                    foundMinimum = decimalParser.Key == minimumType;
                    if (!foundMinimum)
                        continue;
                }

                try
                {
                    return decimalParser.Value(s);
                }
                catch (OverflowException)
                {
                }
                catch (FormatException)
                {
                }
            }
            throw new ParseException(s, "The value does not represent a decimal value.");
        }

        /// <summary>
        /// Parses the provided string into a numeric type by using the parsers defined by the <see cref="NumericParsers"/>.
        /// The method starts with the first defined parser in NumericParsers or the one which defined by the <see cref="minimumType"/>.
        /// When any of the parser succeeds the parsing, then the result is returned. If the string could not be parsed, then an 
        /// <see cref="ParseException"/> is thrown.
        /// </summary>
        /// <param name="s">The string which will be parsed.</param>
        /// <param name="minimumType">The minimum type to which the method will parse the provided string.</param>
        /// <returns>The numeric type containing the result of the parsing.</returns>
        public static object ParseNumeric(string s, Type minimumType = null)
        {
            var foundMinimum = minimumType == null || minimumType == typeof(sbyte);
            foreach (var numericParser in NumericParsers)
            {
                if (!foundMinimum)
                {
                    foundMinimum = numericParser.Key == minimumType;
                    if (!foundMinimum)
                        continue;
                }

                try
                {
                    return numericParser.Value(s);
                }
                catch (OverflowException)
                {
                }
                catch (FormatException)
                {
                }
            }
            throw new ParseException(s, "The value does not represent a numeric value.");
        }

        /// <summary>
        /// Parses the provided string into a numeric type or the string itself by using the parsers defined by the <see cref="NumericParsers"/>.
        /// The method starts with the first defined parser in <see cref="NumericParsers"/> or the parse matching <see cref="minimumType"/>
        /// if provided. When any of the parser succeeds the parsing, then the result  is returned. If the string could not be parsed, then
        /// the string itself is returned. The provided string cannot be null.
        /// </summary>
        /// <param name="s">The string which will be parsed.</param>
        /// <param name="minimumType">The minimum type to which the method will parse the provided string.</param>
        /// <returns>The parsed type.</returns>
        public static object Parse(string s, Type minimumType = null)
        {
            if (s == null)
                throw new NullParameterException("s");

            try
            {
                return ParseNumeric(s, minimumType);
            }
            catch (ParseException)
            {
                return s;
            }
        }

        private static Type GetType(string fullName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var type = assembly.GetType(fullName);
                if (type == null)
                    continue;

                return type;
            }
            return null;
        }

        /// <summary>
        /// <para>Parses the provided string into the specified type. This is done in the following way:</para>
        /// <para>1.	If the provided value is null, then an <see cref="NullParameterException"/> is thrown.</para>
        /// <para>2.	If the specified type is an enum type, then the provided value is parsed into that enum value and returned.</para>
        /// <para>3.	If the specified type is not contained in <see cref="TypeNames"/> then an <see cref="UnsupportedTypeException"/> is thrown.</para>
        /// <para>4.	If the specified type is a string, then the provided string value is returned.</para>
        /// <para>5.	If the specified type is a boolean, then the provided string value is converted to the boolean value.</para>
        /// <para>6.	If the specified type can be mapped to an entry in <see cref="NumericParsers"/>, then that parser is used to parse the provided string</para>
        /// <para>value into a numeric value and the numeric value is returned.</para>
        /// <para>Otherwise an <see cref="UnsupportedTypeException"/> is thrown.</para>
        /// </summary>
        /// <param name="value">The value, which will be parsed.</param>
        /// <param name="typeName">The name of the type, which is used to determine how the parsing is performed.</param>
        /// <returns>The result of the parsing.</returns>
        public static object ParseToType(string value, string typeName)
        {
            if (value == null)
                throw new NullParameterException("value");

            if (!TypeNames.ContainsKey(typeName))
            {
                var type = GetType(typeName);
                if (type == null)
                    throw new UnsupportedTypeException(value, typeName);

                if (type.IsEnum)
                    return Enum.Parse(type, value);

                throw new UnsupportedTypeException(value, typeName);
            }

            {
                var type = TypeNames[typeName];

                if (type == typeof(string))
                    return value;

                if (type == typeof(bool))
                    return BooleanUtilities.ToBool(value);

                if (NumericParsers.ContainsKey(type))
                    return NumericParsers[type](value);
            }

            throw new UnsupportedTypeException(value, typeName);
        }

        /// <summary>
        /// <para>Casts the specified value into the specified target type.</para>
        /// <para>If the specified value is null, then null is returned.</para>
        /// <para>If the specified target type is of the type as the specified value, then the specified value is returned.</para>
        /// <para>If the specified target type is assignable from the specified value, then specified value is returned.</para>
        /// <para>If the specified target type is a string then the specified value is returned as a string (Using the<see cref="object.ToString()"/> method.</para>
        /// <para>Otherwise; the specified value is attempted converted to the specified type by using the <see cref="Convert.ChangeType(object, Type)"/> method.</para>
        /// </summary>
        /// <param name="value">The value, which will be attempted casted to the specified type.</param>
        /// <param name="targetType">The type to which the specified value is attempted casted.</param>
        /// <returns>The result of the cast.</returns>
        public static object CastTo(object value, Type targetType)
        {
            if (value == null)
                return null;

            if (value.GetType() == targetType)
                return value;

            if (targetType.IsInstanceOfType(value))
                return value;

            if (targetType == typeof(string))
                return value.ToString();

            return Convert.ChangeType(value, targetType);
        }

        /// <summary>
        /// <para>Casts the specified value into the specified target type.</para>
        /// <para>If the specified value is null, then null is returned.</para>
        /// <para>If the specified target type is of the type as the specified value, then the specified value is returned.</para>
        /// <para>If the specified target type is assignable from the specified value, then specified value is returned.</para>
        /// <para>If the specified target type is a string then the specified value is returned as a string (Using the<see cref="object.ToString()"/> method.</para>
        /// <para>Otherwise; the specified value is attempted converted to the specified type by using the <see cref="Convert.ChangeType(object, Type)"/> method.</para>
        /// </summary>
        /// <typeparam name="T">The type to which the specified value is attempted casted.</typeparam>
        /// <param name="value">The value, which will be attempted casted to the specified type.</param>
        /// <returns>The result of the cast.</returns>
        public static T CastTo<T>(object value)
        {
            return (T)CastTo(value, typeof(T));
        }
    }
}
