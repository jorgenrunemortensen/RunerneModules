namespace Runerne.Computation.String
{
    /// <summary>
    /// Converts the string from the input to lower cases.
    /// </summary>
    public class ToLowerInvariant : IStringComputable
    {
        /// <summary>
        /// The string from the input converted into lower case.
        /// </summary>
        public string Value => Input.Value.ToLowerInvariant();

        /// <summary>
        /// The input computable node.
        /// </summary>
        public IStringComputable Input { get; }

        /// <summary>
        /// Creates an instance of this computable with the required input.
        /// </summary>
        /// <param name="input">The input computable that provides a string.</param>
        public ToLowerInvariant(IStringComputable input)
        {
            Input = input;
        }
    }
}
