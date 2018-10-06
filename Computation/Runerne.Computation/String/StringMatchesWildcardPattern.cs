using Runerne.Computation.Logic;
using Runerne.Utilities;

namespace Runerne.Computation.String
{
    /// <summary>
    /// Compares the string provided by an input with a string pattern using wildcard matching. The pattern is a string where ‘*’ and ‘?’ 
    /// have special functions as described in <see cref="StringUtilities.WildcardMatch(string, string)"/>.
    /// </summary>
    public class StringMatchesWildcardPattern : IBooleanComputable
    {
        /// <summary>
        /// The computables, which output will be compared with the pattern.
        /// </summary>
        public IStringComputable Input { get; }

        /// <summary>
        /// The computables, which output will be used as the pattern for comparison.
        /// </summary>
        public IStringComputable Pattern { get; }

        /// <summary>
        /// Evaluates to true, if the provided input matches the specified pattern; otherwise false.
        /// </summary>
        public bool Value => StringUtilities.WildcardMatch(Pattern.Value, Input.Value);

        /// <summary>
        /// Creates an instance of this class with the input as string computable and the pattern as a constant string.
        /// </summary>
        /// <param name="input">The input, which provides the string which contents will be compared to the pattern.</param>
        /// <param name="pattern">The pattern that the input will be compared against.</param>
        public StringMatchesWildcardPattern(IStringComputable input, string pattern) : this(input, new StringConstant(pattern))
        {
        }

        /// <summary>
        /// Creates an instance of this class with the input and the pattern as string computables.
        /// </summary>
        /// <param name="input">The input, which provides the string which contents will be compared to the pattern.</param>
        /// <param name="pattern">The string computable that provides the pattern.</param>
        public StringMatchesWildcardPattern(IStringComputable input, IStringComputable pattern)
        {
            Input = input;
            Pattern = pattern;
        }
    }
}
