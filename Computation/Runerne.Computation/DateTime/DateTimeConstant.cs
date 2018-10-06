namespace Runerne.Computation.DateTime
{
    /// <summary>
    /// A datetime node, which always provides the same date and time.
    /// </summary>
    public class DateTimeConstant : IDateTimeComputable
    {
        /// <summary>
        /// The specified date and time provided by this node.
        /// </summary>
        public System.DateTime Value => Input;

        /// <summary>
        /// The date and time, which will be returned.
        /// </summary>
        public System.DateTime Input { get; }

        /// <summary>
        /// Creates an instance of this type with the date and time that it will always return.
        /// </summary>
        /// <param name="input">The date and time to return.</param>
        public DateTimeConstant(System.DateTime input)
        {
            Input = input;
        }
    }
}
