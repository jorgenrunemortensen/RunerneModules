using Runerne.Computation.Numeric;

namespace Runerne.Computation.String
{
    /// <summary>
    /// This computable determines the length of the strings provided by an input node.
    /// </summary>
    public class StringLength : INumericComputable
    {
        /// <summary>
        /// Returns the length of the string provided by the input node.
        /// </summary>
        public double Value => Input.Value.Length;

        /// <summary>
        /// The input node.
        /// </summary>
        public IStringComputable Input { get; }

        /// <summary>
        /// Creates an instance of this computable with the input node.
        /// </summary>
        /// <param name="input">The input node.</param>
        public StringLength(IStringComputable input)
        {
            Input = input;
        }
    }
}
