namespace Runerne.Computation.String
{
    /// <summary>
    /// This computable returns a constant string value.
    /// </summary>
    public class StringConstant : IStringComputable
    {
        /// <summary>
        /// The constant value provided to the constructor.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Creates an instance of the string constant from the specified value.
        /// </summary>
        /// <param name="value"></param>
        public StringConstant(string value)
        {
            Value = value;
        }
    }
}
