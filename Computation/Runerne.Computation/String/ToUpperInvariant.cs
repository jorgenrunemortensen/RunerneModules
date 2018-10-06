namespace Runerne.Computation.String
{
    /// <summary>
    /// Converts the string from the input to upper cases.
    /// </summary>
    public class ToUpperInvariant : IStringComputable
    {
        /// <summary>
        /// The string from the input converted into upper case.
        /// </summary>
        public string Value => Input.Value.ToUpperInvariant();

        /// <summary>
        /// The input computable node.
        /// </summary>
        public IStringComputable Input { get; }

        /// <summary>
        /// Creates an instance of this computable with the required input.
        /// </summary>
        /// <param name="input">The input computable that provides a string.</param>
        public ToUpperInvariant(IStringComputable input)
        {
            Input = input;
        }
    }
}
