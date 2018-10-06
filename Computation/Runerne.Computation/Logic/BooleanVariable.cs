namespace Runerne.Computation.Logic
{
    /// <summary>
    /// A boolean computable which value can be set and thereby controlled from code.
    /// </summary>
    public class BooleanVariable : IBooleanComputable
    {
        /// <summary>
        /// The boolean value that can be set and thereby controlled from code.
        /// </summary>
        public bool Value { get; set; }

        /// <summary>
        /// Creates an instance of this variable with the initial value.
        /// </summary>
        /// <param name="initialValue">This is the initial value that the variable will be given during construction.</param>
        public BooleanVariable(bool initialValue = false)
        {
            Value = initialValue;
        }
    }
}
