namespace Runerne.Computation.String
{
    /// <summary>
    /// Converts the value provided by the input to a string. This is done by calling the <see cref="System.Object.ToString()"/>-method of
    /// value provided by the input computable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ToString<T> : IStringComputable
    {
        /// <summary>
        /// Returns the result of the ToString-operation made on the value provided by the input.
        /// </summary>
        public string Value => Input.Value.ToString();

        /// <summary>
        /// The input computable.
        /// </summary>
        public IComputable<T> Input { get; }

        /// <summary>
        /// Creates an instance of this computable with the required input.
        /// </summary>
        /// <param name="input"></param>
        public ToString(IComputable<T> input)
        {
            Input = input;
        }
    }
}
