using System.Linq;

namespace Runerne.Utilities
{
    /// <summary>
    /// General methods for working with boolean value and converting between boolean value to and texts.
    /// </summary>
    public static class BooleanUtilities
    {
        /// <summary>
        /// Tokens, which are interpreted as false value.<para/>
        /// The false value are:<para/>
        /// <para>0, FALSE, FALSK, NO, N, N, NEJ, -</para>
        /// </summary>
        public static readonly string[] FalseTokens = { "0", "FALSE", "FALSK", "NO", "N", "N", "NEJ", "-" };

        /// <summary>
        /// Tokens, which are interpreted as true value.
        /// The true value are:<para/>
        /// <para>1, TRUE, SAND, SANDT, YES, Y, J, JA, +</para>
        /// </summary>
        public static readonly string[] TrueTokens = { "1", "TRUE", "SAND", "SANDT", "YES", "Y", "J", "JA", "+" };

        /// <summary>
        /// Converts a string into a Boolean. This is done by using two arrays of strings. One array containing tokens, 
        /// which are interpreted as true-value (<see cref="TrueTokens"/>) and one containing tokens, which are interpreted as
        /// false-value (<see cref="FalseTokens"/>).
        /// 
        /// <para>If the string is contained in the <see cref="TrueTokens"/>, then the method returns true.</para>
        /// <para>If the string is contained in the <see cref="FalseTokens"/>, then the method returns true.</para>
        /// <para>If the string is not found in neither <see cref="TrueTokens"/> nor <see cref="FalseTokens"/> then an exception is thrown.</para>
        /// </summary>
        /// <param name="value">The string which is converted into a boolean.</param>
        /// <returns>The boolean value of the string.</returns>
        public static bool ToBool(string value)
        {
            value = value.ToUpperInvariant();
            if (FalseTokens.Contains(value))
                return false;

            if (TrueTokens.Contains(value))
                return true;

            throw new ParseException(value, "The value does not define a boolean value.");
        }
    }
}
