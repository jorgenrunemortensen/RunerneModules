namespace Runerne.Computation.DateTime
{
    /// <summary>
    /// A DateTime computable which value can be set and thereby controlled from code.
    /// </summary>
    public class DateTimeVariable : IDateTimeComputable
    {
        /// <summary>
        /// The date time value that can be set and thereby controlled from code.
        /// </summary>
        public System.DateTime Value { get; set; }

        /// <summary>
        /// Creates an instance of this variable with the initial value.
        /// </summary>
        /// <param name="initialValue">This is the initial value that the variable will be given during construction.</param>
        public DateTimeVariable(System.DateTime initialValue)
        {
            Value = initialValue;
        }
    }
}
