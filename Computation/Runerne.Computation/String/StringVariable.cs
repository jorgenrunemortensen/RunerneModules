namespace Runerne.Computation.String
{
    /// <summary>
    /// A string computable which value can be set and thereby controlled from code.
    /// </summary>
    public class StringVariable : IStringComputable
    {
        /// <summary>
        /// The string value that can be set and thereby controlled from code.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Creates an instance of this variable with a null string as the initial value.
        /// </summary>
        public StringVariable()
        {
        }

        /// <summary>
        /// Creates an instance of this variable with the initial value.
        /// </summary>
        /// <param name="initialValue">This is the initial value that the variable will be given during construction.</param>
        public StringVariable(string initialValue)
        {
            Value = initialValue;
        }
    }
}
